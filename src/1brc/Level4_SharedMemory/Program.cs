using Level4_SharedMemory;
using Shared;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;

// "Working with Pointers in C#" is a standalone walkthrough rather than part of the 1BRC
// implementation, so it lives in Pointers.cs behind an argument:
//   dotnet run -c Release -- pointers
if (args.Length > 0 && args[0].Equals("pointers", StringComparison.OrdinalIgnoreCase))
{
    Pointers.Run();
    return;
}

Console.WriteLine("=== Level 4: Memory Mapped Files ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
Console.WriteLine();

if (!File.Exists(GlobalConstants.FilePath))
{
    Console.WriteLine($"ERROR: File not found at {GlobalConstants.FilePath}");
    Console.WriteLine("Please create a test file or update GlobalConstants.FilePath");
    return;
}

// Force garbage collection before measurement for accurate timing
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

var fileInfo = new FileInfo(GlobalConstants.FilePath);
var fileSize = fileInfo.Length;

if (fileSize == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

var threadCount = Environment.ProcessorCount;
var threadLocalResults = new Dictionary<int, (string Name, StationStatsStruct Stats)>[threadCount];
var lineCounters = new long[threadCount];

using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath,
                                                FileMode.Open,
                                                null,
                                                0,
                                                MemoryMappedFileAccess.Read);

using var accessor = mmf.CreateViewAccessor(0,
                                            fileSize,
                                            MemoryMappedFileAccess.Read);

unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);

    try
    {
        // Skip UTF-8 BOM if present (EF BB BF)
        long dataStart = 0;
        if (fileSize >= 3 && basePtr[0] == 0xEF && basePtr[1] == 0xBB && basePtr[2] == 0xBF)
        {
            dataStart = 3;
        }

        var dataSize = fileSize - dataStart;
        var chunkSize = dataSize / threadCount;

        Parallel.For(0, threadCount, threadIndex =>
        {
            // calculate the chunk boundaries (relative to data start)
            var startPos = dataStart + (threadIndex * chunkSize);
            var endPos = (threadIndex == threadCount - 1)
                            ? fileSize
                            : dataStart + ((threadIndex + 1) * chunkSize);

            // Adjust start to next line boundary (except for the first chunk)
            if (startPos > dataStart)
            {
                while (startPos < fileSize && basePtr[startPos - 1] != '\n')
                {
                    startPos++;
                }
            }

            // Adjust end to line boundary
            if (endPos < fileSize && threadIndex < threadCount - 1)
            {
                while (endPos < fileSize && basePtr[endPos - 1] != '\n')
                {
                    endPos++;
                }
            }

            var localStats = new Dictionary<int, (string Name, StationStatsStruct Stats)>();
            long localLineCount = 0;
            var pos = startPos;

            while (pos < endPos)
            {
                var semicolonPos = pos;
                while (semicolonPos < endPos && basePtr[semicolonPos] != ';')
                {
                    semicolonPos++;
                }

                if (semicolonPos >= endPos)
                    break;

                // find new line
                var newLinePos = semicolonPos + 1;
                while (newLinePos < endPos && basePtr[newLinePos] != '\n')
                {
                    newLinePos++;
                }

                if (newLinePos >= endPos && threadIndex < threadCount - 1)
                {
                    break;
                }

                var nameSpan = new ReadOnlySpan<byte>(basePtr + pos,
                                                      (int)(semicolonPos - pos));

                var hash = ComputeHash(nameSpan);

                // Parse temp length
                var tempLen = (int)(newLinePos - semicolonPos - 1);

                // Handle \r\n endings
                if (tempLen > 0 && newLinePos > 0
                   && basePtr[newLinePos - 1] == '\r')
                {
                    tempLen--;
                }

                var tempSpan = new ReadOnlySpan<byte>(basePtr + semicolonPos + 1,
                                                      tempLen);
                var temperature = ParseTemperature(tempSpan);

                // The lesson writes `if (localStats.TryGetValue(hash, out var entry))
                // entry.Stats.Update(temperature);`, which updates a copy of the tuple and never
                // stores it back, so every station would end up with Count = 1. Taking a ref into
                // the dictionary slot is the same one lookup and does update in place.
                ref var entry = ref CollectionsMarshal.GetValueRefOrNullRef(localStats, hash);
                if (!Unsafe.IsNullRef(ref entry))
                {
                    entry.Stats.Update(temperature);
                }
                else
                {
                    string name = Encoding.UTF8.GetString(nameSpan); // new allocation
                    var stats = StationStatsStruct.Create();
                    stats.Update(temperature);
                    localStats[hash] = (name, stats);
                }

                localLineCount++;
                pos = newLinePos + 1;
            }

            threadLocalResults[threadIndex] = localStats;
            lineCounters[threadIndex] = localLineCount;
        });
    }
    finally
    {
        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
}

// MERGE Dictionary

var finalResults = new Dictionary<string, StationStatsStruct>(GlobalConstants.ExpectedStationCount);

foreach (var localStats in threadLocalResults)
{
    if (localStats == null) continue;

    foreach (var (_, (name, stats)) in localStats)
    {
        if (finalResults.TryGetValue(name, out var existingStats))
        {
            existingStats.Merge(in stats);
            finalResults[name] = existingStats;
        }
        else
        {
            finalResults[name] = stats;
        }
    }
}

var totalLines = lineCounters.Sum();
stopwatch.Stop();


// =============================================================================
// OUTPUT: Format results as required by 1BRC
// =============================================================================

var sortedResults = finalResults.OrderBy(kvp => kvp.Key).ToList();

var output = ResultLogger.FormatOutput(sortedResults, s => $"{s.Min:F1}/{s.Mean:F1}/{s.Max:F1}");
Console.WriteLine(output);

Console.WriteLine();
Console.WriteLine($"Processed {totalLines:N0} rows using {threadCount} threads");
Console.WriteLine($"Found {finalResults.Count} unique stations");
Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");

// Save results to file
ResultLogger.SaveResult(
    projectName: "Level04_SharedMemory",
    output: output,
    elapsed: stopwatch.Elapsed,
    rowCount: totalLines,
    stationCount: finalResults.Count);

// Per-thread statistics
Console.WriteLine();
Console.WriteLine("Per-Thread Statistics:");
for (var i = 0; i < threadCount; i++)
{
    var stationCount = threadLocalResults[i]?.Count ?? 0;
    Console.WriteLine($"  Thread {i}: {lineCounters[i]:N0} lines, {stationCount} stations");
}




static int ComputeHash(ReadOnlySpan<byte> span)
{
    unchecked
    {
        var hash = unchecked((int)2166136261);
        foreach (var b in span)
        {
            hash ^= b;
            hash *= 16777619;
        }

        return hash;
    }
}

static double ParseTemperature(ReadOnlySpan<byte> span)
{
    // double.Parse() // IEEE-754 Standards
    // London;36.1\r
    if (span.Length > 0 && span[^1] == '\r')
    {
        span = span[..^1];
    }

    var negative = false;
    var index = 0;

    // check for negative sign
    if (span[0] == '-')
    {
        negative = true;
        index = 1;
    }

    double result = 0;
    var decimalFound = false;
    var decimalPlace = 0.1;

    while (index < span.Length)
    {
        var c = span[index++];

        if (c == '.')
        {
            decimalFound = true;
            continue;
        }

        int digit = c - '0';

        if (decimalFound)
        {
            result += digit * decimalPlace;
            decimalPlace *= 0.1;
        }
        else
        {
            result = (result * 10) + digit;
        }
    }

    return !negative ? result : -result;
}

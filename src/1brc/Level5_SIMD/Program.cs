using Level5_SIMD;
using Shared;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

Console.WriteLine("=== Level 5: SIMD (AVX2) Implementation ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
Console.WriteLine($"AVX2 Supported: {Avx2.IsSupported}"); // 32 bytes
Console.WriteLine($"AVX-512 Supported: {Avx512F.IsSupported}"); // 64 bytes
Console.WriteLine($"ARM AdvSimd Supported: {AdvSimd.IsSupported}"); // 16 bytes
Console.WriteLine();

if (!File.Exists(GlobalConstants.FilePath))
{
    Console.WriteLine($"ERROR: File not found at {GlobalConstants.FilePath}");
    return;
}

if (!Avx2.IsSupported)
{
    Console.WriteLine("WARNING: AVX2 not supported. Performance will be limited.");
}

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
var threadLocalResults = new FastHashTable[threadCount];
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
        // Skip UTF-8 BOM if present
        long dataStart = 0;
        if (fileSize >= 3 && basePtr[0] == 0xEF && basePtr[1] == 0xBB && basePtr[2] == 0xBF)
        {
            dataStart = 3;
        }

        var dataSize = fileSize - dataStart;
        var chunkSize = dataSize / threadCount;

        Parallel.For(0, threadCount, threadIndex =>
        {
            var startPos = dataStart + (threadIndex * chunkSize);
            var endPos = (threadIndex == threadCount - 1)
                            ? fileSize
                            : dataStart + ((threadIndex + 1) * chunkSize);

            // Align to line boundaries
            if (startPos > dataStart)
            {
                while (startPos < fileSize && basePtr[startPos - 1] != '\n')
                    startPos++;
            }

            if (endPos < fileSize && threadIndex < threadCount - 1)
            {
                while (endPos < fileSize && basePtr[endPos - 1] != '\n')
                    endPos++;
            }

            var localTable = new FastHashTable(
                expectedCount: GlobalConstants.ExpectedStationCount,
                allowResize: true);  // Enable dynamic growth by default

            long localLineCount = 0;
            var pos = startPos;

            // SIMD vectors for delimiter search
            Vector256<byte> semicolonVec = Vector256.Create((byte)';');
            Vector256<byte> newlineVec = Vector256.Create((byte)'\n');

            while (pos < endPos)
            {
                var lineStart = pos;

                // Find semicolon using SIMD
                var semicolonPos = FindByteFast(basePtr, pos, endPos, semicolonVec, (byte)';');
                if (semicolonPos >= endPos)
                    break;

                // Find newline using SIMD
                var newlinePos = FindByteFast(basePtr, semicolonPos + 1, endPos, newlineVec, (byte)'\n');
                if (newlinePos >= endPos)
                    break;

                // Station name: [lineStart, semicolonPos)
                var nameLen = (int)(semicolonPos - lineStart);
                var namePtr = basePtr + lineStart;

                // Temperature: [semicolonPos+1, newlinePos) - handle \r\n
                var tempPtr = basePtr + semicolonPos + 1;
                var tempLen = (int)(newlinePos - semicolonPos - 1);
                if (tempLen > 0 && basePtr[newlinePos - 1] == '\r')
                    tempLen--;

                // Parse temperature as integer (branchless) - scaled by 10
                var temperature = ParseTemperatureBranchless(tempPtr, tempLen);

                // Update hash table (zero-copy)
                localTable.AddOrUpdate(namePtr, nameLen, temperature);
                localLineCount++;

                pos = newlinePos + 1;
            }

            threadLocalResults[threadIndex] = localTable;
            lineCounters[threadIndex] = localLineCount;
        });
    }
    finally
    {
        accessor.SafeMemoryMappedViewHandle.ReleasePointer();
    }
}

// =============================================================================
// MERGE: thread-local tables into one dictionary, keyed by the cached station name
// =============================================================================

var finalResults = new Dictionary<string, (int Min, int Max, long Sum, long Count)>(
    GlobalConstants.ExpectedStationCount);

foreach (var localTable in threadLocalResults)
{
    if (localTable == null) continue;

    foreach (var entry in localTable.GetEntries())
    {
        var name = entry.StationName!;  // <- Use cached string (created only once)

        if (finalResults.TryGetValue(name, out var existing))
        {
            finalResults[name] = (
                Math.Min(existing.Min, entry.Min),
                Math.Max(existing.Max, entry.Max),
                existing.Sum + entry.Sum,
                existing.Count + entry.Count
            );
        }
        else
        {
            finalResults[name] = (entry.Min, entry.Max, entry.Sum, entry.Count);
        }
    }
}

var totalLines = lineCounters.Sum();
stopwatch.Stop();

// =============================================================================
// OUTPUT: the integer-scaled temperatures go back to doubles for the 1BRC format
// =============================================================================

var sortedResults = finalResults.OrderBy(kvp => kvp.Key).ToList();

var output = ResultLogger.FormatOutput(
    sortedResults,
    s => $"{s.Min / 10.0:F1}/{s.Sum / 10.0 / s.Count:F1}/{s.Max / 10.0:F1}");

Console.WriteLine(output);

Console.WriteLine();
Console.WriteLine($"Processed {totalLines:N0} rows using {threadCount} threads");
Console.WriteLine($"Found {finalResults.Count} unique stations");
Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");

ResultLogger.SaveResult(
    projectName: "Level05_SIMD",
    output: output,
    elapsed: stopwatch.Elapsed,
    rowCount: totalLines,
    stationCount: finalResults.Count);

Console.WriteLine();
Console.WriteLine("Per-Thread Statistics:");
for (var i = 0; i < threadCount; i++)
{
    var stationCount = threadLocalResults[i]?.GetEntries().Count() ?? 0;
    Console.WriteLine($"  Thread {i}: {lineCounters[i]:N0} lines, {stationCount} stations");
}

// "Introduction to SIMD" / "Finalizing the Approach": compare a whole vector of bytes against
// the delimiter in one instruction instead of walking the buffer a byte at a time.
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe long FindByteFast(byte* basePtr,
                                long start,
                                long end,
                                Vector256<byte> targetVec,
                                byte target)
{
    var pos = start;

    // The course guards this block with `Avx.IsSupported` while calling Avx2 methods. AVX2 is the
    // stricter of the two, and it is what CompareEqual/MoveMask on byte vectors actually need.
    if (Avx2.IsSupported)
    {
        while (pos + 32 <= end)
        {
            var data = Avx.LoadVector256(basePtr + pos);
            var cmp = Avx2.CompareEqual(data, targetVec);
            var mask = (uint)Avx2.MoveMask(cmp);

            if (mask != 0)
                return pos + BitOperations.TrailingZeroCount(mask);

            pos += 32;
        }
    }
    else if (Vector128.IsHardwareAccelerated)
    {
        // Not in the course, which targets x86 only. This machine is win-arm64, so Avx2.IsSupported
        // is false and the course code would fall all the way back to the scalar loop below,
        // leaving a SIMD chapter with no SIMD in it. Vector128 is the cross-platform API over the
        // same idea: 16 bytes per step on NEON, and the JIT emits the AdvSimd instructions.
        // GetLower is a register extract, not a broadcast: targetVec already holds the byte in
        // all 32 lanes, so its lower half is the 16-lane vector this path needs.
        var targetVec128 = targetVec.GetLower();

        while (pos + 16 <= end)
        {
            var data = Vector128.Load(basePtr + pos);
            var mask = Vector128.Equals(data, targetVec128).ExtractMostSignificantBits();

            if (mask != 0)
                return pos + BitOperations.TrailingZeroCount(mask);

            pos += 16;
        }
    }

    while (pos < end)
    {
        if (basePtr[pos] == target)
            return pos;

        pos++;
    }

    return end;
}

// "Int Parser instead of Double.Parse()": the input always carries exactly one decimal digit, so
// the three or four byte positions can be read directly and the result returned scaled by 10.
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe int ParseTemperatureBranchless(byte* ptr, int len)
{
    var sign = 1;

    if (ptr[0] == '-')
    {
        sign = -1;
        ptr++;
        len--;
    }

    int value;

    if (len == 3)
    {
        // "D.D" -> D*10 + D (e.g. "9.1" -> 91)
        value = ((ptr[0] - '0') * 10) + (ptr[2] - '0');
    }
    else
    {
        // "DD.D" -> D*100 + D*10 + D (e.g. "32.4" -> 324)
        value = ((ptr[0] - '0') * 100)
              + ((ptr[1] - '0') * 10)
              + (ptr[3] - '0');
    }

    return sign * value;
}

using System.Diagnostics;
using System.Text;

namespace Benchmarks.Level4_SharedMemory;

/// <summary>
/// The "Hash Comparison" analyzer: times the three hashing approaches over a simulated 1BRC
/// workload and extrapolates the result to one billion measurements. Run with
/// <c>dotnet run --project Benchmarks -c Release -- hash-analysis</c>.
/// </summary>
public static class HashDistributionAnalyzer
{
    private const int StationCount = 413;

    /// <summary>
    /// Both hashers take a span, so a plain <c>Func&lt;byte[], int&gt;</c> would not bind to them.
    /// </summary>
    private delegate int Hasher(ReadOnlySpan<byte> bytes);

    public static void AnalyzeHashPerformance(int measurementCount = 10_000_000)
    {
        Console.WriteLine($"=== Hash Algorithm Performance for Dictionary<int, Stats> ===");
        Console.WriteLine($"Test: {measurementCount:N0} dictionary operations (lookup/insert)");
        Console.WriteLine($"Keys: {StationCount} unique station names (each repeated ~{measurementCount / StationCount:N0} times)\n");

        var stationNames = GenerateStationNames(StationCount);
        var stationBytes = stationNames.Select(Encoding.UTF8.GetBytes).ToArray();

        // Simulate 1BRC: repeated measurements from same stations
        var random = new Random(42);
        var measurementIndices = new int[measurementCount];
        for (int i = 0; i < measurementCount; i++)
        {
            measurementIndices[i] = random.Next(StationCount);
        }

        Console.WriteLine("=== Algorithm 1: String.GetHashCode() with allocation ===");
        Console.WriteLine("    Implementation: Encoding.UTF8.GetString(bytes).GetHashCode()");
        Console.WriteLine("    Issue: Creates string object for every hash computation!");
        var stringMs = TestStringHashCodeAlgorithm(stationBytes, measurementIndices);
        Console.WriteLine($"    Elapsed: {stringMs:N0} ms\n");

        Console.WriteLine("=== Algorithm 2: Simple multiplicative hash (hash * 31 + b) ===");
        Console.WriteLine("    Implementation: byte-based, no allocation");
        var simpleMs = TestByteHashAlgorithm(stationBytes, measurementIndices, ComputeSimpleHash);
        Console.WriteLine($"    Elapsed: {simpleMs:N0} ms\n");

        Console.WriteLine("=== Algorithm 3: FNV-1a ===");
        Console.WriteLine("    Implementation: byte-based, no allocation, avalanche effect");
        var fnvMs = TestByteHashAlgorithm(stationBytes, measurementIndices, ComputeHash);
        Console.WriteLine($"    Elapsed: {fnvMs:N0} ms\n");

        ReportDistribution("Simple Hash", stationBytes, ComputeSimpleHash);
        ReportDistribution("FNV-1a Hash", stationBytes, ComputeHash);

        var scale = 1_000_000_000.0 / measurementCount;
        Console.WriteLine();
        Console.WriteLine("EXTRAPOLATION TO 1 BILLION MEASUREMENTS");
        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine($"String.GetHashCode(): {stringMs * scale / 1000:F1}s ({stringMs * scale / 60000:F1} minutes)");
        Console.WriteLine($"Simple Hash:          {simpleMs * scale / 1000:F1}s ({simpleMs * scale / 60000:F1} minutes)");
        Console.WriteLine($"FNV-1a Hash:          {fnvMs * scale / 1000:F1}s ({fnvMs * scale / 60000:F1} minutes)");
        Console.WriteLine();
        Console.WriteLine($"Time saved (FNV-1a vs String): {(stringMs - fnvMs) * scale / 1000:F1}s ({(stringMs - fnvMs) * scale / 60000:F1} minutes)");
        Console.WriteLine("+ Additional GC overhead avoided from string allocations");
    }

    private static long TestStringHashCodeAlgorithm(byte[][] stationBytes, int[] measurementIndices)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var sw = Stopwatch.StartNew();
        var dict = new Dictionary<int, long>();

        foreach (var idx in measurementIndices)
        {
            var bytes = stationBytes[idx];
            var hash = Encoding.UTF8.GetString(bytes).GetHashCode(StringComparison.Ordinal); // String allocation!

            if (dict.TryGetValue(hash, out var count))
                dict[hash] = count + 1;
            else
                dict[hash] = 1;
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    private static long TestByteHashAlgorithm(
        byte[][] stationBytes,
        int[] measurementIndices,
        Hasher hasher)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var sw = Stopwatch.StartNew();
        var dict = new Dictionary<int, long>();

        foreach (var idx in measurementIndices)
        {
            var hash = hasher(stationBytes[idx]);

            if (dict.TryGetValue(hash, out var count))
                dict[hash] = count + 1;
            else
                dict[hash] = 1;
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }

    /// <summary>
    /// Counts how many of the 413 keys land in each bucket of a 512-slot table, which is the
    /// "distribution quality" the lesson weighs FNV-1a on.
    /// </summary>
    private static void ReportDistribution(string label, byte[][] stationBytes, Hasher hasher)
    {
        const int Buckets = 512;
        var counts = new int[Buckets];

        foreach (var bytes in stationBytes)
            counts[(hasher(bytes) & int.MaxValue) % Buckets]++;

        var used = counts.Count(c => c > 0);
        Console.WriteLine($"{label} distribution over {Buckets} buckets: " +
                          $"{used} used, {Buckets - used} empty, max {counts.Max()} keys in one bucket");
    }

    /// <summary>
    /// 413 station names. The lesson does not show this helper's body, so the names here are
    /// generated with mixed lengths rather than taken from the real station list.
    /// </summary>
    public static string[] GenerateStationNames(int count)
    {
        var random = new Random(1337);
        var names = new string[count];
        var builder = new StringBuilder();

        for (var i = 0; i < count; i++)
        {
            builder.Clear();
            var length = 3 + random.Next(18); // 3..20 characters, like real station names

            builder.Append((char)('A' + random.Next(26)));
            for (var c = 1; c < length; c++)
                builder.Append((char)('a' + random.Next(26)));

            names[i] = builder.ToString();
        }

        return names;
    }

    public static int ComputeSimpleHash(ReadOnlySpan<byte> bytes)
    {
        unchecked
        {
            var hash = 0;
            foreach (var b in bytes)
            {
                hash = hash * 31 + b;
            }
            return hash;
        }
    }

    public static int ComputeHash(ReadOnlySpan<byte> span)
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
}

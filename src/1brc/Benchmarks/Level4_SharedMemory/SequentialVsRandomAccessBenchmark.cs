using BenchmarkDotNet.Attributes;

namespace Benchmarks.Level4_SharedMemory;

/// <summary>
/// Benchmark: Cache Hit vs Cache Miss
///
/// Tests: sequential vs random access, inside and outside the L3 cache
/// Purpose: show why Level 4 favours sequential, cache-line friendly scans
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 1, iterationCount: 3)]
[BenchmarkCategory("Level04", "MemoryAccess")]
public class SequentialVsRandomAccessBenchmark
{
    // Small: Fits in L3 Cache including indices
    // 4M ints = 16MB data + 16MB indices = 32MB total (half of 64MB L3, safe margin)
    private const int SmallSize = 4 * 1024 * 1024;

    // Large: Far exceeds L3 Cache
    // 32M ints = 128MB data + 128MB indices = 256MB total (4x L3, guaranteed RAM)
    private const int LargeSize = 32 * 1024 * 1024;

    private int[] smallData = [];
    private int[] largeData = [];

    private int[] smallRandomIndices = [];
    private int[] largeRandomIndices = [];

    [GlobalSetup]
    public void Setup()
    {
        var rng = new Random(42);

        // Small data (32MB - fits in L3)
        smallData = new int[SmallSize];
        for (var i = 0; i < SmallSize; i++)
            smallData[i] = rng.Next();

        // Large data (128MB - exceeds L3)
        largeData = new int[LargeSize];
        for (var i = 0; i < LargeSize; i++)
            largeData[i] = rng.Next();

        // Generate random indices
        smallRandomIndices = GenerateRandomIndices(SmallSize, rng);
        largeRandomIndices = GenerateRandomIndices(LargeSize, rng);
    }

    private static int[] GenerateRandomIndices(int size, Random rng)
    {
        var indices = new int[size];
        for (var i = 0; i < size; i++)
            indices[i] = i;

        // Fisher-Yates shuffle
        for (var i = size - 1; i > 0; i--)
        {
            var j = rng.Next(i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }

        return indices;
    }

    [Benchmark]
    [BenchmarkCategory("Sequential", "CacheHit")]
    public long SmallSequential()
    {
        long sum = 0;
        var data = smallData;

        for (var i = 0; i < data.Length; i++)
            sum += data[i];

        return sum;
    }

    [Benchmark]
    [BenchmarkCategory("Random", "CacheHit")]
    public long SmallRandom()
    {
        long sum = 0;
        var data = smallData;
        var indices = smallRandomIndices;

        for (var i = 0; i < indices.Length; i++)
            sum += data[indices[i]];

        return sum;
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Sequential", "RAMAccess")]
    public long LargeSequential()
    {
        long sum = 0;
        var data = largeData;

        for (var i = 0; i < data.Length; i++)
            sum += data[i];

        return sum;
    }

    [Benchmark]
    [BenchmarkCategory("Random", "RAMAccess")]
    public long LargeRandom()
    {
        long sum = 0;
        var data = largeData;
        var indices = largeRandomIndices;

        for (var i = 0; i < indices.Length; i++)
            sum += data[indices[i]];

        return sum;
    }
}

using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.ValueVsReference.Benchmarks;

/// <summary>
/// The honest access benchmark. Both arms draw from the same pool with the same
/// allocation layout; only the access order changes. That isolates access order
/// from allocation order and exposes the real cost of chasing references.
/// </summary>
[ShortRunJob]
[HideColumns("Error", "Gen1")]
public class RandomizedAccess
{
    /// <summary>
    /// When true, sample refs at random positions from the pool (cache-hostile).
    /// When false, take the first Size entries in order: same pool, same allocation
    /// layout, but a prefetcher-friendly access pattern.
    /// </summary>
    [Params(false, true)]
    public bool Shuffle { get; set; }

    [Params(100, 10_000, 1_000_000)]
    public int Size { get; set; }

    /// <summary>
    /// Size of the backing pool. Large enough that sampled refs are scattered across
    /// many cache lines and pages. Even when Size is small, the working array points
    /// all over the pool, defeating the hardware prefetcher.
    /// </summary>
    private const int PoolSize = 1_000_000;

    private Point[] _points = [];
    private PointRef[] _pointRefs = [];

    [GlobalSetup]
    public void Setup()
    {
        var pool = new PointRef[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            pool[i] = new PointRef(i, i);
        }

        // The struct array is contiguous no matter what Shuffle says. That is the
        // control arm: its timing should barely move between the two cases.
        _points = new Point[Size];
        for (int i = 0; i < Size; i++)
        {
            _points[i] = new Point(i, i);
        }

        _pointRefs = new PointRef[Size];
        if (Shuffle)
        {
            // Slice Size refs from random positions in the pool.
            var rng = new Random(42);
            for (int i = 0; i < Size; i++)
            {
                _pointRefs[i] = pool[rng.Next(PoolSize)];
            }
        }
        else
        {
            // Same pool, sequential slice: refs point at adjacent pool entries,
            // preserving allocation order.
            for (int i = 0; i < Size; i++)
            {
                _pointRefs[i] = pool[i];
            }
        }
    }

    [Benchmark]
    public int ConsumeArrayOfPointRefs()
    {
        int sum = 0;
        // Indirection
        foreach (var pr in _pointRefs)
        {
            sum += pr.X;
        }

        return sum;
    }

    [Benchmark]
    public int ConsumeArrayOfPoints()
    {
        int sum = 0;
        // Direct access
        foreach (var p in _points)
        {
            sum += p.X;
        }

        return sum;
    }
}

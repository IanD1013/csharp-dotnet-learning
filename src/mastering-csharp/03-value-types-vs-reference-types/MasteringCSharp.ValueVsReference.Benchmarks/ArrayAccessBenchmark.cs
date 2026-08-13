using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.ValueVsReference.Benchmarks;

/// <summary>
/// The naive access benchmark, and the one that misleads you.
/// Everything is allocated back to back in GlobalSetup, so the reference objects
/// land next to each other on the heap and the hardware prefetcher hides most of
/// the indirection cost. Run <see cref="RandomizedAccess"/> to see what the number
/// looks like once memory is realistically fragmented.
/// </summary>
[ShortRunJob]
[HideColumns("Error", "Gen1")]
public class ArrayAccessBenchmark
{
    [Params(100, 10_000, 1_000_000)]
    public int Size { get; set; }

    private Point[] _points = [];
    private PointRef[] _pointRefs = [];

    [GlobalSetup]
    public void Setup()
    {
        // Contiguous allocation: struct values sit inline in the array,
        // and ref objects land next to each other on the heap.
        _points = new Point[Size];
        _pointRefs = new PointRef[Size];
        for (int i = 0; i < Size; i++)
        {
            _points[i] = new Point(i, i);
            _pointRefs[i] = new PointRef(i, i);
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

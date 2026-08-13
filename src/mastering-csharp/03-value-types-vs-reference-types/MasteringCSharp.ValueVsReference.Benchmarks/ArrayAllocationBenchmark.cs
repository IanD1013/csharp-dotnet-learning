using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.ValueVsReference.Benchmarks;

/// <summary>
/// Storage model, measured. Filling an array of N classes costs N + 1 allocations,
/// filling an array of N structs costs exactly one. Watch the Allocated column.
/// </summary>
[MemoryDiagnoser]
[ShortRunJob]
[HideColumns("Error", "Gen1")]
public class ArrayAllocationBenchmark
{
    [Params(10, 100, 1000)]
    public int Size { get; set; }

    [Benchmark]
    public PointRef[] CreateArrayOfPointRefs()
    {
        var array = new PointRef[Size];
        for (int i = 0; i < array.Length; i++)
        {
            // Creating the instance on the heap and storing a reference in the array.
            array[i] = new PointRef(i, i);
        }

        return array;
    }

    [Benchmark]
    public Point[] CreateArrayOfPoints()
    {
        var array = new Point[Size];
        for (int i = 0; i < array.Length; i++)
        {
            // Creating the value and storing it inline in the array.
            array[i] = new Point(i, i);
        }

        return array;
    }
}

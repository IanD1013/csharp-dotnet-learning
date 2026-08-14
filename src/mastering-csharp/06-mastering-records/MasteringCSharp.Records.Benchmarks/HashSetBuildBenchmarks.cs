using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.Records.Benchmarks;

/// <summary>
/// The complexity claim, made visible. Building the set is O(N) when hashing works and
/// O(N^2) when every key collides, so the gap should widen roughly 100x each time Count
/// grows 10x. Run this one with a small Count range: 10,000 default-struct items is
/// already tens of millions of boxed comparisons.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(iterationCount: 3, warmupCount: 3)]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
public class HashSetBuildBenchmarks
{
    [Params(100, 1_000, 10_000)]
    public int Count { get; set; }

    private LocationRecordStruct[] _recordStructLocations = [];
    private LocationDefaultStruct[] _defaultStructLocations = [];

    [GlobalSetup]
    public void Setup()
    {
        _recordStructLocations = [.. Enumerable.Range(1, Count)
            .Select(static n => new LocationRecordStruct(Path: "", Position: n))];

        _defaultStructLocations = [.. Enumerable.Range(1, Count)
            .Select(static n => new LocationDefaultStruct(path: "", position: n))];
    }

    [Benchmark(Baseline = true)]
    public int BuildRecordStructSet() =>
        new HashSet<LocationRecordStruct>(_recordStructLocations).Count;

    [Benchmark]
    public int BuildDefaultStructSet() =>
        new HashSet<LocationDefaultStruct>(_defaultStructLocations).Count;
}

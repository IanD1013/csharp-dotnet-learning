using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.Records.Benchmarks;

/// <summary>
/// The chapter's headline benchmark: a miss lookup in a HashSet keyed by a struct.
/// Every Path is the empty string, so the default struct hashes every element identically
/// and the set degrades into one long bucket. The record struct hashes both fields.
/// Watch the Allocated column: ValueType.Equals boxes both sides of every comparison.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(iterationCount: 3, warmupCount: 3)]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
public class LocationContainsBenchmarks
{
    [Params(100, 1_000, 10_000)]
    public int Count { get; set; }

    private HashSet<LocationRecordStruct> _recordStructLocations = null!;
    private HashSet<LocationDefaultStruct> _defaultStructLocations = null!;

    [GlobalSetup]
    public void Setup()
    {
        _recordStructLocations = Enumerable.Range(1, Count)
            .Select(static n => new LocationRecordStruct(Path: "", Position: n))
            .ToHashSet();

        _defaultStructLocations = Enumerable.Range(1, Count)
            .Select(static n => new LocationDefaultStruct(path: "", position: n))
            .ToHashSet();
    }

    [Benchmark(Baseline = true)]
    public bool RecordStructEquality() =>
        _recordStructLocations.Contains(new LocationRecordStruct(Path: "", Position: 0));

    [Benchmark]
    public bool DefaultStructEquality() =>
        _defaultStructLocations.Contains(new LocationDefaultStruct(path: "", position: 0));
}

using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.TuplesAndUnions.Benchmarks;

/// <summary>
/// The chapter's own benchmark from "Union Types Under the Hood": the same dictionary
/// lookup reached directly, through the compiler-shaped union, and through the manual
/// union. The Allocated column is the whole story - a compiler-generated union stores its
/// case in an <c>object?</c>, so the int case boxes on the way in.
/// </summary>
// Every case here runs in single-digit nanoseconds, so the default 5 iterations is not
// enough: a single outlier moved ManualKeyById from 1.6ns to 29.7ns between runs.
[MemoryDiagnoser]
[SimpleJob(iterationCount: 15, warmupCount: 5)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Alloc Ratio")]
public class LookupBenchmarks
{
    private readonly Entries _entries = new();

    [Benchmark(Baseline = true)]
    public int DirectById() => _entries.Lookup(42).Id;

    [Benchmark]
    public int DirectByName() => _entries.Lookup("Launch Plan").Id;

    [Benchmark]
    public int LookupKeyById() => _entries.Lookup((GeneratedLookupKey)42).Id;

    [Benchmark]
    public int LookupKeyByName() => _entries.Lookup((GeneratedLookupKey)"Launch Plan").Id;

    [Benchmark]
    public int ManualKeyById() => _entries.Lookup((ManualLookupKey)42).Id;

    [Benchmark]
    public int ManualKeyByName() => _entries.Lookup((ManualLookupKey)"Launch Plan").Id;
}

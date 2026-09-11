using BenchmarkDotNet.Attributes;
using System.Text;

namespace Benchmarks.Level4_SharedMemory;

/// <summary>
/// Benchmark: String.GetHashCode (Marvin32) vs simple multiplicative vs FNV-1a
///
/// Tests: 413 unique station names hashed into a Dictionary&lt;int, long&gt;
/// Purpose: show the allocation cost of hashing through string, from "Hash Comparison"
/// </summary>
[MemoryDiagnoser]
[BenchmarkCategory("Level04", "Hashing")]
public class HashAlgorithmBenchmark
{
    private byte[][] _stationBytes = [];
    private int[] _measurementIndices = [];

    [Params(1_000_000)]
    public int MeasurementCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var stationNames = HashDistributionAnalyzer.GenerateStationNames(413);
        _stationBytes = [.. stationNames.Select(Encoding.UTF8.GetBytes)];

        var random = new Random(42);
        _measurementIndices = new int[MeasurementCount];
        for (var i = 0; i < MeasurementCount; i++)
        {
            _measurementIndices[i] = random.Next(413);
        }
    }

    [Benchmark(Baseline = true)]
    public int DotNetStringHash_Dictionary()
    {
        var dict = new Dictionary<int, long>();

        foreach (var idx in _measurementIndices)
        {
            var bytes = _stationBytes[idx];
            var hash = Encoding.UTF8.GetString(bytes).GetHashCode(StringComparison.Ordinal); // String allocation!

            if (dict.TryGetValue(hash, out var count))
                dict[hash] = count + 1;
            else
                dict[hash] = 1;
        }

        return dict.Count;
    }

    [Benchmark]
    public int SimpleHash_Dictionary()
    {
        var dict = new Dictionary<int, long>();

        foreach (var idx in _measurementIndices)
        {
            var hash = HashDistributionAnalyzer.ComputeSimpleHash(_stationBytes[idx]);

            if (dict.TryGetValue(hash, out var count))
                dict[hash] = count + 1;
            else
                dict[hash] = 1;
        }

        return dict.Count;
    }

    [Benchmark]
    public int FNV1aHash_Dictionary()
    {
        var dict = new Dictionary<int, long>();

        foreach (var idx in _measurementIndices)
        {
            var hash = HashDistributionAnalyzer.ComputeHash(_stationBytes[idx]);

            if (dict.TryGetValue(hash, out var count))
                dict[hash] = count + 1;
            else
                dict[hash] = 1;
        }

        return dict.Count;
    }
}

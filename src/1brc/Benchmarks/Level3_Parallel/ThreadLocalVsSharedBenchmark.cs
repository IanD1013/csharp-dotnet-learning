using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace Benchmarks.Level03_Parallel;

/// <summary>
/// Benchmark: Thread-Local vs Shared Dictionary
///
/// Tests: Lock-free thread-local vs ConcurrentDictionary
/// Purpose: Demonstrate why Level 3 uses thread-local pattern
/// </summary>
[MemoryDiagnoser]
[BenchmarkCategory("Level03", "Concurrency")]
public class ThreadLocalVsSharedBenchmark
{
    private List<(string Station, double Temp)> _measurements = [];
    private readonly object _lock = new();

    [Params(100_000, 1_000_000, 10_000_000)]
    public int MeasurementCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(0197);

        // Generate 413 unique station names (matching 1BRC challenge)
        var stations = new string[413];
        for (var i = 0; i < 413; i++)
        {
            stations[i] = $"Station_{i:D3}"; // Station_000 to Station_412
        }

        _measurements = [];
        for (var i = 0; i < MeasurementCount; i++)
        {
            var station = stations[random.Next(stations.Length)];
            var temp = random.NextDouble() * 40 - 10;
            _measurements.Add((station, temp));
        }
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("ThreadLocal")]
    public int ThreadLocal()
    {
        var threadCount = Environment.ProcessorCount;
        var chunkSize = MeasurementCount / threadCount;
        var locals = new Dictionary<string, (double, double, double, int)>[threadCount];

        Parallel.For(0, threadCount, i =>
        {
            var start = i * chunkSize;
            var end = (i == threadCount - 1) ? MeasurementCount : (i + 1) * chunkSize;
            var local = new Dictionary<string, (double, double, double, int)>();

            for (var j = start; j < end; j++)
            {
                var (station, temp) = _measurements[j];

                if (!local.TryGetValue(station, out var stats))
                    stats = (temp, temp, temp, 1);
                else
                    stats = (Math.Min(stats.Item1, temp), Math.Max(stats.Item2, temp), stats.Item3 + temp, stats.Item4 + 1);

                local[station] = stats;
            }

            locals[i] = local;
        });

        // Merge
        var final = new Dictionary<string, (double, double, double, int)>();
        foreach (var local in locals)
        {
            if (local == null) continue;
            foreach (var (station, stats) in local)
            {
                if (!final.TryGetValue(station, out var existing))
                    final[station] = stats;
                else
                    final[station] = (
                        Math.Min(existing.Item1, stats.Item1),
                        Math.Max(existing.Item2, stats.Item2),
                        existing.Item3 + stats.Item3,
                        existing.Item4 + stats.Item4
                    );
            }
        }

        return final.Count;
    }

    [Benchmark]
    [BenchmarkCategory("ConcurrentDict")]
    public int ConcurrentDictionary()
    {
        var shared = new ConcurrentDictionary<string, (double, double, double, int)>();
        var threadCount = Environment.ProcessorCount;
        var chunkSize = MeasurementCount / threadCount;

        Parallel.For(0, threadCount, i =>
        {
            var start = i * chunkSize;
            var end = (i == threadCount - 1) ? MeasurementCount : (i + 1) * chunkSize;

            for (var j = start; j < end; j++)
            {
                var (station, temp) = _measurements[j];

                shared.AddOrUpdate(
                    station,
                    (temp, temp, temp, 1),
                    (key, existing) => (
                        Math.Min(existing.Item1, temp),
                        Math.Max(existing.Item2, temp),
                        existing.Item3 + temp,
                        existing.Item4 + 1
                    )
                );
            }
        });

        return shared.Count;
    }

    [Benchmark]
    [BenchmarkCategory("LockedDict")]
    public int LockedDictionary()
    {
        var shared = new Dictionary<string, (double, double, double, int)>();
        var threadCount = Environment.ProcessorCount;
        var chunkSize = MeasurementCount / threadCount;

        Parallel.For(0, threadCount, i =>
        {
            var start = i * chunkSize;
            var end = (i == threadCount - 1) ? MeasurementCount : (i + 1) * chunkSize;

            for (var j = start; j < end; j++)
            {
                var (station, temp) = _measurements[j];

                lock (_lock)
                {
                    if (!shared.TryGetValue(station, out var stats))
                        stats = (temp, temp, temp, 1);
                    else
                        stats = (Math.Min(stats.Item1, temp), Math.Max(stats.Item2, temp), stats.Item3 + temp, stats.Item4 + 1);

                    shared[station] = stats;
                }
            }
        });

        return shared.Count;
    }
}

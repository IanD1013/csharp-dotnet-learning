using BenchmarkDotNet.Attributes;

namespace MasteringCSharp.TuplesAndUnions.Benchmarks;

/// <summary>
/// Lesson "System.Tuple vs. System.ValueTuple" makes one performance claim: the old tuple
/// is heap allocated and the new one is not. This measures it, and adds the composite-key
/// case from "Tuples in C#", where a ValueTuple key hashes without allocating.
/// <para>
/// The creation benchmarks return the tuple so that it escapes the method. Without that,
/// .NET 10's escape analysis stack-allocates the System.Tuple and both cases measure zero,
/// which flatters the old tuple for a reason that has nothing to do with the lesson.
/// NoEscape below keeps that case visible on purpose.
/// </para>
/// </summary>
[MemoryDiagnoser]
[SimpleJob(iterationCount: 15, warmupCount: 5)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Alloc Ratio")]
public class TupleAllocationBenchmarks
{
    // Read through fields rather than inlined as literals, so the JIT cannot fold the
    // construction away and the creation benchmarks measure the same work.
    private readonly string _host = "api.example.com";
    private readonly int _port = 443;

    private readonly Dictionary<(string Host, int Port), string> _routes =
        new()
        {
            [("api.example.com", 443)] = "production",
            [("api.example.com", 8443)] = "staging",
        };

    private readonly Dictionary<Tuple<string, int>, string> _refRoutes =
        new()
        {
            [Tuple.Create("api.example.com", 443)] = "production",
            [Tuple.Create("api.example.com", 8443)] = "staging",
        };

    /// <summary>A struct returned by value. Nothing reaches the heap however it is used.</summary>
    [Benchmark(Baseline = true)]
    public (string Host, int Port) CreateValueTuple() => (_host, _port);

    /// <summary>The same pair as a class, escaping to the caller: one 32-byte allocation.</summary>
    [Benchmark]
    public Tuple<string, int> CreateSystemTuple() => Tuple.Create(_host, _port);

    /// <summary>
    /// The same construction, but the instance never leaves the method. On .NET 10 the JIT
    /// can prove that and keep it off the heap, so "System.Tuple always allocates" is no
    /// longer literally true. It is still true wherever the tuple is returned or stored.
    /// </summary>
    [Benchmark]
    public int CreateSystemTupleNoEscape()
    {
        Tuple<string, int> endpoint = Tuple.Create(_host, _port);
        return endpoint.Item2;
    }

    /// <summary>A ValueTuple key hashes and compares in place.</summary>
    [Benchmark]
    public string ValueTupleKeyLookup() => _routes[(_host, _port)];

    /// <summary>
    /// The same lookup with a System.Tuple key. The key itself escapes into the dictionary
    /// call, and Tuple's structural hashing and equality box the int on the way through.
    /// </summary>
    [Benchmark]
    public string SystemTupleKeyLookup() => _refRoutes[Tuple.Create(_host, _port)];
}

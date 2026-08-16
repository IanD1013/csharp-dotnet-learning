using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace MasteringCSharp.ArgumentValidation.Benchmarks;

/// <summary>
/// The chapter's own benchmark from "Analyzing ThrowIfNull Boxing Allocations",
/// with three rows added: a reference-type argument as a control, a generic guard,
/// and the BCL's <c>ArgumentNullException.ThrowIfNull</c> on .NET 10.
/// Covered in notes lesson 7.
/// </summary>
[ShortRunJob(RuntimeMoniker.Net10_0)]
[ShortRunJob(RuntimeMoniker.Net48)]
[MemoryDiagnoser]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Alloc Ratio")]
// Both suppressions are about BenchmarkDotNet's requirements rather than about this code:
// underscores keep the generated report columns readable and match the lesson's own names,
// and [Benchmark] methods must be instance methods even when they touch no instance state.
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Benchmark names mirror the lesson and read better in the report.")]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "BenchmarkDotNet only discovers instance methods.")]
public class BoxingBenchmarks
{
    private const int Iterations = 256;

    private readonly Payload _payload = new("Launch Plan");

    [Benchmark(Baseline = true)]
    public int Inlinable_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessInt(42);
        return total;
    }

    [Benchmark]
    public int NoInline_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessIntNoInline(42);
        return total;
    }

    /// <summary>
    /// Control row: the same non-inlinable guard, called with a reference type.
    /// If this allocates, the cost is the call. If it does not, the cost is boxing.
    /// </summary>
    [Benchmark]
    public int NoInline_Reference()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessReferenceNoInline(_payload);
        return total;
    }

    /// <summary>
    /// A generic guard cannot box, because T is instantiated as int and there is no
    /// object? parameter to convert to.
    /// </summary>
    [Benchmark]
    public int NoInline_Generic_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessIntGenericNoInline(42);
        return total;
    }

    private static int ProcessInt(int value)
    {
        Guard.ThrowIfNull(value);
        return 42;
    }

    private static int ProcessIntNoInline(int value)
    {
        Guard.ThrowIfNullNoInline(value);
        return 42;
    }

    private static int ProcessIntGenericNoInline(int value)
    {
        Guard.ThrowIfNullGenericNoInline(value);
        return 42;
    }

    private static int ProcessReferenceNoInline(Payload value)
    {
        Guard.ThrowIfNullNoInline(value);
        return 42;
    }
}

/// <summary>Reference-type argument for the control row.</summary>
public sealed class Payload(string name)
{
    public string Name { get; } = name;
}

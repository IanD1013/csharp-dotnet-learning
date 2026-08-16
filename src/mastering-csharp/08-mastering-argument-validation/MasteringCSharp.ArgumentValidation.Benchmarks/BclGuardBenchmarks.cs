#if NET
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace MasteringCSharp.ArgumentValidation.Benchmarks;

/// <summary>
/// The BCL's own <c>ArgumentNullException.ThrowIfNull</c>, which the chapter summary
/// says differs from a custom guard only by an [Intrinsic] attribute.
/// Covered in notes lesson 10.
/// </summary>
/// <remarks>
/// This lives in its own class, and the class is compiled only for .NET, for a reason
/// worth knowing before writing any multi-runtime benchmark: BenchmarkDotNet discovers
/// benchmarks by reflecting over the host assembly, then generates boilerplate that
/// calls every one of them for each runtime job. A single [Benchmark] method behind
/// #if NET therefore compiles fine and then breaks the net48 leg with CS0103, taking
/// every other row in the class down with it. Whole classes are safe because the job
/// attribute lives on the class: no net48 job here means no net48 boilerplate.
/// Not in the course - found while getting the chapter's benchmark to run here.
/// </remarks>
[ShortRunJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Alloc Ratio")]
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Benchmark names mirror the lesson and read better in the report.")]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "BenchmarkDotNet only discovers instance methods.")]
public class BclGuardBenchmarks
{
    private const int Iterations = 256;

    /// <summary>The custom guard again, as the baseline to compare the BCL against.</summary>
    [Benchmark(Baseline = true)]
    public int Custom_Inlinable_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessIntCustom(42);
        return total;
    }

    [Benchmark]
    public int Bcl_ThrowIfNull_Int()
    {
        var total = 0;
        for (int i = 0; i < Iterations; i++)
            total += ProcessIntBcl(42);
        return total;
    }

    private static int ProcessIntCustom(int value)
    {
        Guard.ThrowIfNull(value);
        return 42;
    }

    private static int ProcessIntBcl(int value)
    {
        // CA2264 says this call is a no-op because an int can never be null, and it is
        // right. Measuring what that no-op costs is the point of the row: the claim
        // under test is that it costs nothing, not that it does something.
#pragma warning disable CA2264
        ArgumentNullException.ThrowIfNull(value);
#pragma warning restore CA2264
        return 42;
    }
}
#endif

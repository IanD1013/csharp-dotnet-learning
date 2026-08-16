using System.Runtime.CompilerServices;

namespace MasteringCSharp.ArgumentValidation.Demos;

/// <summary>
/// Lesson "Analyzing ThrowIfNull Boxing Allocations" and the chapter summary.
/// Covered in notes lessons 7 and 10.
/// The benchmark project measures the same thing properly; this section exists so the
/// allocation is visible without waiting several minutes for BenchmarkDotNet, and so
/// the tier-0 behaviour the summary lesson mentions is visible at all.
/// </summary>
internal static class BoxingDemo
{
    private const int Iterations = 100_000;
    private const int ColdIterations = 500;

    public static void Run()
    {
        Console.WriteLine($"-- Guard.ThrowIfNull(object?) called with an int, on {RuntimeName} --");
        Console.WriteLine();

        // Measured before anything has warmed up, so this runs as unoptimised tier-0
        // code. It has to come first: once a method is promoted there is no way back.
        var cold = Measure(() => DriveInlinable(ColdIterations));

        var payload = new Item("Launch Plan");

        // Promote all three loops to tier 1 before measuring. Two details matter here,
        // and getting either wrong quietly measures tier-0 code instead:
        //   - promote by call count, not by one long call. A single long call only
        //     triggers on-stack replacement, which switches over partway through and
        //     leaves the first few thousand iterations running unoptimised.
        //   - sleep between rounds. Tiered compilation ignores calls made during a
        //     ~100ms startup window, so a tight warmup loop can count for nothing.
        for (int round = 0; round < 3; round++)
        {
            for (int i = 0; i < 100; i++)
            {
                DriveInlinable(200);
                DriveNoInline(200);
                DriveReference(payload, 200);
            }

            Thread.Sleep(250);
        }

        var inlinable = Measure(() => DriveInlinable(Iterations));
        var noInline = Measure(() => DriveNoInline(Iterations));

        Console.WriteLine($"  cold, before promotion           = {cold,10:N0} bytes over {ColdIterations:N0} calls  ({(double)cold / ColdIterations,5:0.0} per call)");
        Console.WriteLine($"  warm, inlinable ThrowIfNull      = {inlinable,10:N0} bytes over {Iterations:N0} calls  ({(double)inlinable / Iterations,5:0.0} per call)");
        Console.WriteLine($"  warm, [MethodImpl(NoInlining)]   = {noInline,10:N0} bytes over {Iterations:N0} calls  ({(double)noInline / Iterations,5:0.0} per call)");

        Console.WriteLine();
        Console.WriteLine("  All three call the same one-line guard with the same int.");
        Console.WriteLine();
        Console.WriteLine("  Once the guard is inlined, the JIT can see an int being compared against null,");
        Console.WriteLine("  prove the comparison can never be true, and delete the box along with the");
        Console.WriteLine("  branch that needed it. That is the lesson's result, and it is the middle row.");
        Console.WriteLine();
        Console.WriteLine("  [MethodImpl(NoInlining)] forbids that inlining, so the box is load-bearing again:");
        Console.WriteLine("  a real call has to be made and its parameter really is typed object?.");
        Console.WriteLine();
        Console.WriteLine("  24 bytes is the size of a boxed int on a 64-bit runtime: 8 object header");
        Console.WriteLine("  + 8 method table pointer + 4 payload, rounded up to the 8-byte granularity.");
        Console.WriteLine();
        Console.WriteLine("  The first row is the one the lesson does not measure, and the two runtimes");
        Console.WriteLine("  disagree on it. Run this on both targets and compare. Notes lesson 10.");

        Console.WriteLine();
        Console.WriteLine("-- The same call with a reference type, for contrast --");

        var reference = Measure(() => DriveReference(payload, Iterations));

        Console.WriteLine($"  warm, no-inline, Item argument   = {reference,10:N0} bytes over {Iterations:N0} calls  ({(double)reference / Iterations,5:0.0} per call)");
        Console.WriteLine();
        Console.WriteLine("  Inlining is still blocked and the call still takes object?, yet nothing allocates.");
        Console.WriteLine("  The cost was never the guard. It was converting a value type into something");
        Console.WriteLine("  an object? parameter can hold, and a reference needs no conversion.");
    }

    private static string RuntimeName =>
#if NET
        $".NET {Environment.Version}";
#else
        ".NET Framework 4.8";
#endif

    // The loops are spelled out rather than parameterised over a delegate: passing the
    // guard as an Action<object?> would block inlining on both sides and erase the
    // difference this section exists to show.
    private static void DriveInlinable(int iterations)
    {
        for (int i = 0; i < iterations; i++)
            BoxingGuard.ThrowIfNull(42);
    }

    private static void DriveNoInline(int iterations)
    {
        for (int i = 0; i < iterations; i++)
            BoxingGuard.ThrowIfNullNoInline(42);
    }

    private static void DriveReference(Item payload, int iterations)
    {
        for (int i = 0; i < iterations; i++)
            BoxingGuard.ThrowIfNullNoInline(payload);
    }

    private static long Measure(Action body)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var before = GC.GetAllocatedBytesForCurrentThread();
        body();
        return GC.GetAllocatedBytesForCurrentThread() - before;
    }
}

/// <summary>
/// The lesson's benchmark subject: two identical guards, one of which the JIT is
/// forbidden from inlining.
/// </summary>
internal static class BoxingGuard
{
    public static void ThrowIfNull(object? argument, string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowIfNullNoInline(object? argument, string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);
}

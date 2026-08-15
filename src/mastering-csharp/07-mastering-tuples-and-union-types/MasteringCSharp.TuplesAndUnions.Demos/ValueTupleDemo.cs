using System.Runtime.CompilerServices;

namespace MasteringCSharp.TuplesAndUnions.Demos;

/// <summary>
/// Section 1 of the notes: the two tuple families.
/// Lesson: "System.Tuple vs. System.ValueTuple".
/// </summary>
public static class ValueTupleDemo
{
    public static void Run()
    {
        TwoFamilies();
        Console.WriteLine();
        WhatTheyCost();
    }

    private static void TwoFamilies()
    {
        Console.WriteLine("-- The same pair, twice --");

        // System.Tuple: heap allocated, no language syntax, Item1/Item2 only.
        Tuple<string, int> refTuple = Tuple.Create("api.example.com", 443);

        // System.ValueTuple: a struct, with language syntax and named elements.
        (string host, int port) valTuple = ("api.example.com", 443);

        Console.WriteLine($"  Tuple<string,int>   = {refTuple}");
        Console.WriteLine($"  ValueTuple          = {valTuple}");
        Console.WriteLine($"  Tuple runtime type      = {refTuple.GetType().Name}   (reference type: {!refTuple.GetType().IsValueType})");
        Console.WriteLine($"  ValueTuple runtime type = {valTuple.GetType().Name}   (value type: {valTuple.GetType().IsValueType})");
        Console.WriteLine($"  Access by name only works on the ValueTuple: {valTuple.host}:{valTuple.port}");
        Console.WriteLine($"  System.Tuple has no names, only positions:   {refTuple.Item1}:{refTuple.Item2}");
    }

    /// <summary>
    /// "Heap allocated" and "value type" are the whole argument in that lesson, so measure it.
    /// A ValueTuple local never reaches the heap; every Tuple.Create is an allocation.
    /// </summary>
    private static void WhatTheyCost()
    {
        Console.WriteLine("-- What 10,000 of each allocate --");

        // Warm up so JIT compilation is not counted in the measured window.
        MakeReferenceTuples(1);
        MakeValueTuples(1);

        long before = GC.GetAllocatedBytesForCurrentThread();
        int refSum = MakeReferenceTuples(10_000);
        long refBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        before = GC.GetAllocatedBytesForCurrentThread();
        int valSum = MakeValueTuples(10_000);
        long valBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        Console.WriteLine($"  10,000 x Tuple.Create(string, int) = {refBytes,7:N0} bytes  ({refBytes / 10_000d:N1} per instance)");
        Console.WriteLine($"  10,000 x (string, int)             = {valBytes,7:N0} bytes");
        Console.WriteLine($"  (checksums {refSum} / {valSum}, printed so the loops cannot be optimised away)");
        Console.WriteLine("  The ValueTuple lives in the caller's frame, so there is nothing for the GC to collect.");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int MakeReferenceTuples(int count)
    {
        int sum = 0;

        for (int i = 0; i < count; i++)
        {
            Tuple<string, int> t = Tuple.Create("api.example.com", i);
            sum += t.Item2;
        }

        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int MakeValueTuples(int count)
    {
        int sum = 0;

        for (int i = 0; i < count; i++)
        {
            (string host, int port) t = ("api.example.com", i);
            sum += t.port;
        }

        return sum;
    }
}

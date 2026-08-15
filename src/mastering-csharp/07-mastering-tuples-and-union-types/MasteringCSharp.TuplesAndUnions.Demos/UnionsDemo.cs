using System.Runtime.CompilerServices;

namespace MasteringCSharp.TuplesAndUnions.Demos;

/// <summary>
/// Sections 4 and 5 of the notes, run against hand-written stand-ins for the C# 15 feature.
/// Lessons: "Union Types: Why Do We Need Them?", "Union Types: the Basics",
/// "Union Types Under the Hood", "Union Types: the Deep Dive", "UnionTypes - Recap".
/// </summary>
public static class UnionsDemo
{
    private const int Count = 10_000;

    public static void Run()
    {
        TheShape();
        Console.WriteLine();
        Boxing();
        Console.WriteLine();
        TheUninitializedCase();
        Console.WriteLine();
        WhatIsNotGenerated();
        Console.WriteLine();
        TheManualUnion();
        Console.WriteLine();
        TheClassUnion();
    }

    /// <summary>
    /// A union is one field plus one constructor per case. The implicit conversions are
    /// what make `LookupKey key = 42;` compile.
    /// </summary>
    private static void TheShape()
    {
        Console.WriteLine("-- The struct the compiler would generate --");

        GeneratedLookupKey byId = 42;                 // implicit conversion from int
        GeneratedLookupKey byName = "Launch Plan";    // implicit conversion from string

        Console.WriteLine($"  GeneratedLookupKey key = 42            -> Describe() = {byId.Describe()}");
        Console.WriteLine($"  GeneratedLookupKey key = \"Launch Plan\" -> Describe() = {byName.Describe()}");
        Console.WriteLine($"  Value holds a {byId.Value!.GetType().Name} in the first case and a {byName.Value!.GetType().Name} in the second.");

        var entries = new Entries();
        Console.WriteLine($"  entries.Lookup((LookupKey)42)            = {entries.Lookup(byId)}");
        Console.WriteLine($"  entries.Lookup((LookupKey)\"Launch Plan\") = {entries.Lookup(byName)}");
        Console.WriteLine("  One method accepts both key kinds, and the caller cannot pass anything else.");
    }

    /// <summary>
    /// The chapter's measurable claim: the int case costs a boxing allocation because the
    /// storage is object?. The string case costs nothing, because a string is already
    /// a reference.
    /// </summary>
    private static void Boxing()
    {
        Console.WriteLine($"-- What {Count:N0} of each case allocate --");

        // Warm up so JIT work lands outside the measured window.
        MakeIdKeys(1);
        MakeNameKeys(1);

        long before = GC.GetAllocatedBytesForCurrentThread();
        int idSum = MakeIdKeys(Count);
        long idBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        before = GC.GetAllocatedBytesForCurrentThread();
        int nameSum = MakeNameKeys(Count);
        long nameBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        before = GC.GetAllocatedBytesForCurrentThread();
        int manualSum = MakeManualIdKeys(Count);
        long manualBytes = GC.GetAllocatedBytesForCurrentThread() - before;

        Console.WriteLine($"  GeneratedLookupKey from int    = {idBytes,8:N0} bytes  ({idBytes / (double)Count:N1} per instance)");
        Console.WriteLine($"  GeneratedLookupKey from string = {nameBytes,8:N0} bytes  ({nameBytes / (double)Count:N1} per instance)");
        Console.WriteLine($"  ManualLookupKey    from int    = {manualBytes,8:N0} bytes  ({manualBytes / (double)Count:N1} per instance)");
        Console.WriteLine($"  (checksums {idSum} / {nameSum} / {manualSum})");
        Console.WriteLine("  24 bytes is a boxed int on x64: 8 header + 8 method table + 4 payload, rounded to 24.");
        Console.WriteLine("  The manual union stores the int in an int? field instead, so nothing reaches the heap.");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int MakeIdKeys(int count)
    {
        int sum = 0;

        for (int i = 0; i < count; i++)
        {
            GeneratedLookupKey key = i;
            sum += key.Value is int id ? id : 0;
        }

        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int MakeNameKeys(int count)
    {
        int sum = 0;

        for (int i = 0; i < count; i++)
        {
            GeneratedLookupKey key = "Launch Plan";
            sum += key.Value is string name ? name.Length : 0;
        }

        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int MakeManualIdKeys(int count)
    {
        int sum = 0;

        for (int i = 0; i < count; i++)
        {
            ManualLookupKey key = i;
            sum += key.TryGetValue(out int id) ? id : 0;
        }

        return sum;
    }

    /// <summary>
    /// A union declared with the keyword is always a struct, and every struct has a default
    /// value. That is why the compiler makes you handle a null case that is not in the
    /// declared list.
    /// </summary>
    private static void TheUninitializedCase()
    {
        Console.WriteLine("-- The case nobody declared: default --");

        GeneratedLookupKey uninitialized = default;
        Console.WriteLine($"  default(GeneratedLookupKey).Value      = {uninitialized.Value?.ToString() ?? "null"}");
        Console.WriteLine($"  default(GeneratedLookupKey).Describe() = {uninitialized.Describe()}");
        Console.WriteLine("  `int Id | string Name` has two cases. The runtime type has three.");

        // The manual union backs the int case with int?, so id 0 and default stay distinct.
        ManualLookupKey zero = 0;
        ManualLookupKey empty = default;
        Console.WriteLine($"  ManualLookupKey zero    = {zero}");
        Console.WriteLine($"  ManualLookupKey default = {empty}");
        Console.WriteLine($"  zero.Equals(default)    = {zero.Equals(empty)}   <- int? keeps 'id 0' and 'no case' apart");
        Console.WriteLine("  With a plain int field those two would be the same value.");
    }

    /// <summary>
    /// The recap lesson's list of what the compiler does not write for you.
    /// </summary>
    private static void WhatIsNotGenerated()
    {
        Console.WriteLine("-- What a generated union does not come with --");

        GeneratedLookupKey a = 42;
        GeneratedLookupKey b = 42;

        Console.WriteLine($"  a.ToString()   = {a}   <- the type name, not the value");
        Console.WriteLine($"  a.Equals(b)    = {a.Equals(b)}   (two unions over the same id)");
        Console.WriteLine($"  hashes equal   = {a.GetHashCode() == b.GetHashCode()}");
        Console.WriteLine("  Equality falls back to ValueType.Equals, which boxes and compares the object field.");

        long before = GC.GetAllocatedBytesForCurrentThread();
        bool equal = a.Equals(b);
        long bytes = GC.GetAllocatedBytesForCurrentThread() - before;
        Console.WriteLine($"  one a.Equals(b) call allocated {bytes} bytes (result {equal})");

        ManualLookupKey ma = 42;
        ManualLookupKey mb = 42;
        Console.WriteLine($"  record struct version: ma == mb = {ma == mb}, ToString = {ma}");
        Console.WriteLine("  Declaring the union as a record struct is the cheapest way to get both back.");
    }

    private static void TheManualUnion()
    {
        Console.WriteLine("-- The manual union: same API, no boxing --");

        ManualLookupKey byId = 42;
        ManualLookupKey byName = "Launch Plan";

        Console.WriteLine($"  ManualLookupKey key = 42            -> {byId.Describe()}");
        Console.WriteLine($"  ManualLookupKey key = \"Launch Plan\" -> {byName.Describe()}");

        var entries = new Entries();
        Console.WriteLine($"  entries.Lookup(byId)   = {entries.Lookup(byId)}");
        Console.WriteLine($"  entries.Lookup(byName) = {entries.Lookup(byName)}");
        Console.WriteLine($"  byId.Value still boxes on demand: {byId.Value!.GetType().Name}");
        Console.WriteLine("  TryGetValue is the member the compiler prefers, so the boxed path is never taken.");
    }

    private static void TheClassUnion()
    {
        Console.WriteLine("-- The class-based union: no uninitialized case at all --");

        ClassLookupKey byId = 42;
        ClassLookupKey byName = "Launch Plan";

        Console.WriteLine($"  ClassLookupKey key = 42            -> {byId.Describe()}");
        Console.WriteLine($"  ClassLookupKey key = \"Launch Plan\" -> {byName.Describe()}");
        Console.WriteLine($"  Value is non-nullable: {byId.Value.GetType().Name} / {byName.Value.GetType().Name}");
        Console.WriteLine("  There is no default(ClassLookupKey) that is a valid instance, so a switch");
        Console.WriteLine("  over it is exhaustive with two arms and needs no null branch.");
        Console.WriteLine("  The trade is the obvious one: every key is now a heap allocation.");
    }
}

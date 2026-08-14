using System.Diagnostics;

namespace MasteringCSharp.Records.Demos;

/// <summary>
/// Section 4 of the notes: what default struct equality costs, and why record struct fixes it.
/// Lessons: "Issues With the Default Structs Equality", "Record Structs vs. Default Structs".
/// </summary>
public static class StructEqualityDemo
{
    private const int Count = 10_000;

    public static void Run()
    {
        FirstFieldOnly();
        Console.WriteLine();
        FieldOrderMatters();
        Console.WriteLine();
        BlittableIsDifferent();
        Console.WriteLine();
        RecordStructFixesIt();
        Console.WriteLine();
        TheCost();
    }

    /// <summary>
    /// The lesson's central claim: for a non-blittable struct the default GetHashCode
    /// uses the first field only, so instances differing elsewhere collide.
    /// </summary>
    private static void FirstFieldOnly()
    {
        Console.WriteLine("-- Non-blittable struct: only the first field reaches GetHashCode --");

        for (int i = 0; i < 5; i++)
        {
            var l = new LocationDefaultStruct(path: "", position: i);
            Console.WriteLine($"  new LocationDefaultStruct(\"\", {i}).GetHashCode() = {l.GetHashCode()}");
        }

        Console.WriteLine("  Position never enters the hash, so all five land in one bucket.");
    }

    /// <summary>
    /// Swap the declaration order and the hash changes, which is the cleanest available
    /// proof that "first field" is meant literally.
    /// </summary>
    private static void FieldOrderMatters()
    {
        Console.WriteLine("-- Same data, fields declared in the other order --");

        for (int i = 0; i < 5; i++)
        {
            var l = new LocationReorderedStruct(path: "", position: i);
            Console.WriteLine($"  new LocationReorderedStruct(\"\", {i}).GetHashCode() = {l.GetHashCode()}");
        }

        Console.WriteLine("  Position is the first field now, so the hash finally varies.");

        // Control the lesson does not show. If only the first field is hashed, then holding
        // Position fixed and varying Path must leave the hash alone. It does.
        var samePosition1 = new LocationReorderedStruct(path: "alpha", position: 7);
        var samePosition2 = new LocationReorderedStruct(path: "omega", position: 7);
        Console.WriteLine($"  (\"alpha\", 7) hash = {samePosition1.GetHashCode()}");
        Console.WriteLine($"  (\"omega\", 7) hash = {samePosition2.GetHashCode()}");
        Console.WriteLine($"  same hash?        = {samePosition1.GetHashCode() == samePosition2.GetHashCode()}   <- Path is now the ignored field");
        Console.WriteLine($"  but equal?        = {samePosition1.Equals(samePosition2)}   <- Equals still compares everything");
    }

    /// <summary>
    /// The behaviour is conditional, not universal: a struct with no reference fields
    /// and no padding gets a hash over all of its bits.
    /// </summary>
    private static void BlittableIsDifferent()
    {
        Console.WriteLine("-- Blittable struct (int, int): all fields are used --");

        for (int i = 0; i < 3; i++)
        {
            var p = new BlittablePoint(0, i);
            Console.WriteLine($"  new BlittablePoint(0, {i}).GetHashCode() = {p.GetHashCode()}");
        }

        Console.WriteLine("  X is identical across all three and the hash still differs.");
    }

    private static void RecordStructFixesIt()
    {
        Console.WriteLine("-- record struct: generated, typed, all-field hashing --");

        var a = new PersonKey("Alice", 25);
        var b = new PersonKey("Alice", 99);
        Console.WriteLine($"  PersonKey(Alice,25) hash    = {a.GetHashCode()}");
        Console.WriteLine($"  PersonKey(Alice,99) hash    = {b.GetHashCode()}");
        Console.WriteLine($"  same hash?                  = {a.GetHashCode() == b.GetHashCode()}   <- only Name was used");

        var ra = new PersonRecord("Alice", 25);
        var rb = new PersonRecord("Alice", 99);
        Console.WriteLine($"  PersonRecord(Alice,25) hash = {ra.GetHashCode()}");
        Console.WriteLine($"  PersonRecord(Alice,99) hash = {rb.GetHashCode()}");
        Console.WriteLine($"  same hash?                  = {ra.GetHashCode() == rb.GetHashCode()}");
    }

    /// <summary>
    /// The whole point: colliding hashes turn HashSet construction from O(N) into O(N^2),
    /// with two boxing allocations per comparison on the way.
    /// </summary>
    private static void TheCost()
    {
        Console.WriteLine($"-- What the collisions cost, {Count:N0} items --");

        var defaults = Enumerable.Range(1, Count)
            .Select(static n => new LocationDefaultStruct(path: "", position: n))
            .ToArray();
        var records = Enumerable.Range(1, Count)
            .Select(static n => new LocationRecordStruct(Path: "", Position: n))
            .ToArray();

        var sw = Stopwatch.StartNew();
        var defaultSet = new HashSet<LocationDefaultStruct>(defaults);
        long defaultMs = sw.ElapsedMilliseconds;

        sw.Restart();
        var recordSet = new HashSet<LocationRecordStruct>(records);
        long recordMs = sw.ElapsedMilliseconds;

        Console.WriteLine($"  HashSet<LocationDefaultStruct> built in {defaultMs,6:N0} ms  ({defaultSet.Count:N0} items)");
        Console.WriteLine($"  HashSet<LocationRecordStruct>  built in {recordMs,6:N0} ms  ({recordSet.Count:N0} items)");
        Console.WriteLine("  Same data, same collection type. The only difference is how the key hashes.");
    }
}

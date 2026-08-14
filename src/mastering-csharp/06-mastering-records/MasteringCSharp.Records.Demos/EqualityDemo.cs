namespace MasteringCSharp.Records.Demos;

/// <summary>
/// Section 1 of the notes: the three ways to compare, and what a record replaces.
/// Lessons: "Manual Value-based Equality", "Referential and Value-based Equality".
/// </summary>
public static class EqualityDemo
{
    public static void Run()
    {
        ClassEquality();
        Console.WriteLine();
        ManualValueEquality();
        Console.WriteLine();
        RecordEquality();
    }

    private static void ClassEquality()
    {
        Console.WriteLine("-- Class: reference equality, and a ToString that tells you nothing --");

        var c1 = new PointClass(1, 2);
        var c2 = new PointClass(1, 2);

        Console.WriteLine($"  c1 == c2                 = {c1 == c2}   <- same data, different instances");
        Console.WriteLine($"  c1.Equals(c2)            = {c1.Equals(c2)}");
        Console.WriteLine($"  ReferenceEquals(c1, c2)  = {ReferenceEquals(c1, c2)}");
        Console.WriteLine($"  c1.ToString()            = {c1}   <- the type name, not the data");
    }

    private static void ManualValueEquality()
    {
        Console.WriteLine("-- Hand-written value equality: 5 members to keep in sync --");

        var v1 = new PointValue(1, 2);
        var v2 = new PointValue(1, 2);

        Console.WriteLine($"  v1 == v2                 = {v1 == v2}    <- only because operator== was overloaded");
        Console.WriteLine($"  v1.Equals(v2)            = {v1.Equals(v2)}");
        Console.WriteLine($"  ReferenceEquals(v1, v2)  = {ReferenceEquals(v1, v2)}   <- cannot be customised, and correctly says no");
        Console.WriteLine($"  v1.ToString()            = {v1}");
    }

    private static void RecordEquality()
    {
        Console.WriteLine("-- record Point(int X, int Y): the same behaviour in one line --");

        var p1 = new Point(1, 2);
        var p2 = new Point(1, 2);

        Console.WriteLine($"  p1 == p2                 = {p1 == p2}    <- generated operator==");
        Console.WriteLine($"  p1.Equals(p2)            = {p1.Equals(p2)}");
        Console.WriteLine($"  ReferenceEquals(p1, p2)  = {ReferenceEquals(p1, p2)}   <- still two heap objects; a record is a class");
        Console.WriteLine($"  p1.ToString()            = {p1}   <- generated from PrintMembers");
        Console.WriteLine($"  p1.GetHashCode()         = {p1.GetHashCode()}");
        Console.WriteLine($"  p2.GetHashCode()         = {p2.GetHashCode()}   <- equal values hash equal, so hash collections work");
    }
}

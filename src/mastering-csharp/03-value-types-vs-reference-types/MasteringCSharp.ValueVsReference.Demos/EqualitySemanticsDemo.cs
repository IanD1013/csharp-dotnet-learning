namespace MasteringCSharp.ValueVsReference.Demos;

/// <summary>
/// Equality semantics: structs compare content, classes compare identity,
/// and ReferenceEquals lies to you about structs.
/// </summary>
public static class EqualitySemanticsDemo
{
    public static void Run()
    {
        StructEquality();
        Console.WriteLine();
        ClassEquality();
        Console.WriteLine();
        BoxingTrap();
    }

    private static void StructEquality()
    {
        Console.WriteLine("-- Struct equality is value based --");

        var s1 = new Point { X = 1, Y = 1 };
        var s2 = new Point { X = 1, Y = 1 };

        Console.WriteLine($"  s1.Equals(s2)               = {s1.Equals(s2)}   <- same content, so equal");

        // s1 == s2 does not compile: the compiler emits no default operator== for a struct.
        Console.WriteLine("  s1 == s2                    = does not compile without an explicit operator==");

        s1.X++;
        Console.WriteLine($"  s1.Equals(s2) after s1.X++  = {s1.Equals(s2)}   <- content changed, so no longer equal");
    }

    private static void ClassEquality()
    {
        Console.WriteLine("-- Class equality is referential --");

        var r1 = new PointRef { X = 1, Y = 1 };
        var r2 = new PointRef { X = 1, Y = 1 };

        Console.WriteLine($"  r1.Equals(r2)               = {r1.Equals(r2)}  <- identical content, different instances");
        Console.WriteLine($"  r1 == r2                    = {r1 == r2}  <- same check");
        Console.WriteLine($"  ReferenceEquals(r1, r2)     = {ReferenceEquals(r1, r2)}  <- same check again");

        r2 = r1;
        Console.WriteLine($"  after r2 = r1, r1 == r2     = {r1 == r2}   <- one instance, two names");
    }

    /// <summary>
    /// ReferenceEquals takes object parameters, so a struct is boxed on the way in.
    /// Each argument becomes its own heap object, so the answer is always false.
    /// </summary>
    private static void BoxingTrap()
    {
        Console.WriteLine("-- The ReferenceEquals boxing trap --");

        var s = new Point { X = 1, Y = 1 };

        // CA2013 fires on the next line, which is the whole point of this demo:
        // the analyzer catches exactly the mistake the lesson warns about. Suppressed
        // here so the trap can actually run and print its surprising answer.
#pragma warning disable CA2013 // Do not use ReferenceEquals with value types
        Console.WriteLine($"  ReferenceEquals(s, s)       = {ReferenceEquals(s, s)}  <- the SAME variable, still false");
#pragma warning restore CA2013

        Console.WriteLine("  The compiler analyzer flags this call as CA2013 before you even run it.");
        Console.WriteLine("  Both arguments were boxed into two separate heap objects.");
        Console.WriteLine("  Never use ReferenceEquals on a struct.");
    }
}

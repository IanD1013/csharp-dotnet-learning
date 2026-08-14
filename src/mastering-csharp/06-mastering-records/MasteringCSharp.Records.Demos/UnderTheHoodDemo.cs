namespace MasteringCSharp.Records.Demos;

/// <summary>
/// Section 2 of the notes: what the compiler actually generates.
/// Lesson: "Records Under the Hood".
/// </summary>
public static class UnderTheHoodDemo
{
    public static void Run()
    {
        WithExpression();
        Console.WriteLine();
        Deconstruction();
        Console.WriteLine();
        InitOnly();
        Console.WriteLine();
        EqualityContract();
    }

    /// <summary>
    /// `with` lowers to a call to the hidden &lt;Clone&gt;$ method (which calls the protected
    /// copy constructor) followed by the init accessor for each changed property.
    /// </summary>
    private static void WithExpression()
    {
        Console.WriteLine("-- Non-destructive mutation: with --");

        var p1 = new Point(1, 2);
        var p2 = p1 with { X = 10 };

        Console.WriteLine($"  p1                       = {p1}   <- untouched");
        Console.WriteLine($"  p2 = p1 with {{ X = 10 }}  = {p2}");
        Console.WriteLine($"  p1 == p2                 = {p1 == p2}   <- a whole new instance, every time");
    }

    private static void Deconstruction()
    {
        Console.WriteLine("-- Deconstruct, generated for positional records --");

        var (x, y) = new Point(10, 2);
        Console.WriteLine($"  var (x, y) = new Point(10, 2)  ->  x = {x}, y = {y}");
    }

    /// <summary>
    /// Positional parameters become properties with init accessors, so object initializer
    /// syntax works as long as the record also offers a parameterless constructor.
    /// </summary>
    private static void InitOnly()
    {
        Console.WriteLine("-- init accessors --");

        var p = new Point { X = 3, Y = 4 };
        Console.WriteLine($"  new Point {{ X = 3, Y = 4 }}   = {p}");
        Console.WriteLine("  p.X = 5;                     does not compile: init means construction only");
    }

    /// <summary>
    /// EqualityContract is the protected virtual Type property the compiler generates,
    /// and it is the reason a derived record is never equal to its base.
    /// </summary>
    private static void EqualityContract()
    {
        Console.WriteLine("-- EqualityContract: why inheritance does not break equality --");

        Point flat = new(1, 2);
        Point deep = new Point3D(1, 2, 3);

        Console.WriteLine($"  flat                     = {flat}");
        Console.WriteLine($"  deep                     = {deep}   <- static type Point, runtime type Point3D");
        Console.WriteLine($"  flat == deep             = {flat == deep}   <- EqualityContract differs, so no");
        Console.WriteLine($"  deep.Equals(flat)        = {deep.Equals(flat)}");
        Console.WriteLine("  The hash code seeds from EqualityContract too, so the two types stay in different buckets.");
    }
}

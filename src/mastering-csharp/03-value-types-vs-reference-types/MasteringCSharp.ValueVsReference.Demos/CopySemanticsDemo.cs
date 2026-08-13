namespace MasteringCSharp.ValueVsReference.Demos;

/// <summary>
/// Copy semantics: assignment and parameter passing always copy "the value".
/// For a struct the value is the data; for a class the value is the reference.
/// One rule, two outcomes.
/// </summary>
public static class CopySemanticsDemo
{
    public static void Run()
    {
        Assignment();
        Console.WriteLine();
        PassByValue();
        Console.WriteLine();
        PassByRef();
        Console.WriteLine();
        ReadOnlyAliases();
    }

    /// <summary>
    /// Assigning a class aliases the same instance; assigning a struct clones the data.
    /// </summary>
    private static void Assignment()
    {
        Console.WriteLine("-- Assignment --");

        PointRef r1 = new() { X = 1, Y = 2 };
        PointRef r2 = r1;   // r2 is an alias for r1
        r2.X++;
        r2.Y++;

        Point v1 = new() { X = 3, Y = 4 };
        Point v2 = v1;      // v2 is a separate copy of v1
        v2.X++;
        v2.Y++;

        Console.WriteLine($"  class : r1 = ({r1.X},{r1.Y})  r2 = ({r2.X},{r2.Y})  <- mutating r2 was visible through r1");
        Console.WriteLine($"  struct: v1 = ({v1.X},{v1.Y})  v2 = ({v2.X},{v2.Y})  <- v1 never moved");
    }

    /// <summary>
    /// The default parameter mode is a copy, which for a class copies the reference.
    /// </summary>
    private static void PassByValue()
    {
        Console.WriteLine("-- Passing by value (the default) --");

        var v = new Point { X = 1, Y = 1 };
        var r = new PointRef { X = 1, Y = 1 };

        Mutate(v, r);

        Console.WriteLine($"  struct: X = {v.X}  <- the callee mutated its own copy");
        Console.WriteLine($"  class : X = {r.X}  <- the callee mutated the shared instance");

        static void Mutate(Point value, PointRef reference)
        {
            value.X++;      // mutates a COPY of the struct
            reference.X++;  // mutates the SHARED instance
        }
    }

    /// <summary>
    /// ref creates an alias to the caller's variable, which for a class means the
    /// callee can repoint that variable at a different instance entirely.
    /// </summary>
    private static void PassByRef()
    {
        Console.WriteLine("-- Passing by ref --");

        var v = new Point { X = 1, Y = 1 };
        var r = new PointRef { X = 1, Y = 1 };

        Mutate(ref v, ref r);

        Console.WriteLine($"  struct: X = {v.X}  <- the caller's struct itself changed");
        Console.WriteLine($"  class : X = {r.X}  <- the caller's variable now points at a brand new instance");

        static void Mutate(ref Point value, ref PointRef reference)
        {
            value.X++;                                  // mutates the caller's struct
            reference.X++;                              // mutates the shared object
            reference = new PointRef { X = 0, Y = 0 };  // reassigns the caller's variable
        }
    }

    /// <summary>
    /// in and ref readonly avoid the copy without granting write access.
    /// The commented lines are compile errors, kept here as executable documentation.
    /// </summary>
    private static void ReadOnlyAliases()
    {
        Console.WriteLine("-- Read-only aliases --");

        var p = new Point { X = 1, Y = 2 };

        // 'in' accepts r-values; 'ref' and 'ref readonly' require variables (l-values).
        Variants(readOnlyAlias: new Point(), mutableAlias: ref p, strictAlias: in p);

        Console.WriteLine($"  after the call: p = ({p.X},{p.Y})  <- only the ref parameter could write");

        static void Variants(in Point readOnlyAlias, ref Point mutableAlias, ref readonly Point strictAlias)
        {
            // readOnlyAlias = new Point();  // error: 'in' cannot be reassigned
            // readOnlyAlias.X++;            // error: 'in' blocks field writes
            // strictAlias = new Point();    // error: 'ref readonly' cannot be reassigned
            // strictAlias.X++;              // error: 'ref readonly' blocks field writes

            mutableAlias.X++;                // allowed: mutates the caller
            _ = readOnlyAlias.X + strictAlias.X;
        }
    }
}

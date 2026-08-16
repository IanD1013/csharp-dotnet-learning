namespace MasteringCSharp.ArgumentValidation.Demos;

/// <summary>
/// Lessons "Argument Null Checks in C#" and "Checking for Null in C#".
/// Covered in notes lessons 4 and 5.
/// </summary>
internal static class NullChecksDemo
{
    public static void Run()
    {
        Console.WriteLine("-- Five ways to ask 'is this null?', on a non-null object --");

        // The course's own snippet, verbatim. X overloads == to always return true.
        X x = new X();

        Console.WriteLine($"  x == null: {x == null}");
        Console.WriteLine($"  x is null: {x is null}");
        Console.WriteLine($"  ReferenceEquals(x, null): {ReferenceEquals(x, null)}");
        Console.WriteLine($"  x is not object: {x is not object}");
        Console.WriteLine($"  x is not {{ }}: {x is not { }}");

        Console.WriteLine();
        Console.WriteLine("  Only the first line is wrong, and it is wrong because X overloaded ==.");
        Console.WriteLine("  `is null`, ReferenceEquals and the two patterns cannot be intercepted by user code.");

        // Beyond the course: the same overload viewed from the guard's side, in both
        // directions. Which direction it fails in depends on what the operator returns.
        Console.WriteLine();
        Console.WriteLine("-- What that does to a guard clause --");

        Console.WriteLine($"  RejectsValidArgument(new X())  = {RejectsValidArgument(new X())}   <- a valid argument is thrown out");
        Console.WriteLine($"  AcceptsNull(null)              = {AcceptsNull(null)}   <- a null argument is waved through");
        Console.WriteLine();
        Console.WriteLine("  X.op_Equality always returns true, so `arg == null` rejects everything.");
        Console.WriteLine("  Y.op_Equality always returns false, so `arg == null` accepts everything, null included.");
        Console.WriteLine("  Rewriting both guards as `arg is null` fixes both cases.");
    }

    /// <summary>
    /// A guard written with <c>== null</c> against a type whose <c>==</c> always returns true.
    /// </summary>
    private static bool RejectsValidArgument(X argument)
    {
#pragma warning disable CA1508 // the analyzer calls this always-false; the overload is the point
        return argument == null;
#pragma warning restore CA1508
    }

    /// <summary>
    /// A guard written with <c>== null</c> against a type whose <c>==</c> always returns false.
    /// It returns false for a null argument, so the guard never fires.
    /// </summary>
    private static bool AcceptsNull(Y? argument)
    {
#pragma warning disable CA1508 // same again: the whole point is that this check is unreliable
        return argument == null;
#pragma warning restore CA1508
    }
}

/// <summary>
/// The course's example type: <c>==</c> is overloaded to always return <c>true</c>,
/// which is what makes <c>x == null</c> unreliable as a null check.
/// </summary>
internal sealed class X
{
    public static bool operator ==(X? left, X? right) => true;
    public static bool operator !=(X? left, X? right) => true;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj);
    public override int GetHashCode() => 0;
}

/// <summary>
/// The other half of the same problem, added by these notes: an <c>==</c> that always
/// returns <c>false</c> makes a null argument look non-null.
/// </summary>
internal sealed class Y
{
    public static bool operator ==(Y? left, Y? right) => false;
    public static bool operator !=(Y? left, Y? right) => true;
    public override bool Equals(object? obj) => ReferenceEquals(this, obj);
    public override int GetHashCode() => 0;
}

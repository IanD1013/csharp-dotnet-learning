namespace MasteringCSharp.Records.Demos;

/// <summary>
/// Section 3 of the notes: where generated equality stops being what you want.
/// Lesson: "Records Limitations".
/// </summary>
public static class LimitationsDemo
{
    public static void Run()
    {
        CaseSensitiveByDefault();
        Console.WriteLine();
        ComposedComparison();
        Console.WriteLine();
        ShallowImmutability();
    }

    private static void CaseSensitiveByDefault()
    {
        Console.WriteLine("-- Equality is not configurable: every property, default comparer --");

        var l1 = new LocationCaseSensitive(Path: "/users/sergey/readme.md", Position: 42);
        var l2 = new LocationCaseSensitive(Path: "/users/sergey/ReadMe.md", Position: 42);

        Console.WriteLine($"  l1        = {l1}");
        Console.WriteLine($"  l2        = {l2}");
        Console.WriteLine($"  l1 == l2  = {l1 == l2}   <- same file on Windows, still not equal");
    }

    /// <summary>
    /// The fix that scales: move the comparison rule into the property's type, so the
    /// record keeps its generated members and nothing has to be kept in sync by hand.
    /// </summary>
    private static void ComposedComparison()
    {
        Console.WriteLine("-- The composition fix: a readonly record struct that owns the rule --");

        // The implicit operator means the call sites did not have to change.
        var l1 = new Location("/users/sergey/readme.md", Position: 42);
        var l2 = new Location("/users/sergey/ReadMe.md", Position: 42);

        Console.WriteLine($"  l1        = {l1}");
        Console.WriteLine($"  l2        = {l2}");
        Console.WriteLine($"  l1 == l2  = {l1 == l2}    <- NormalizedPath compares OrdinalIgnoreCase");
        Console.WriteLine($"  hashes equal = {l1.GetHashCode() == l2.GetHashCode()}   <- GetHashCode was overridden to match, so HashSet agrees");
    }

    /// <summary>
    /// Immutability stops at the reference. Nothing about a record protects what it points at.
    /// </summary>
    private static void ShallowImmutability()
    {
        Console.WriteLine("-- Immutability is shallow --");

        var basket = new Basket("Ian", ["apple"]);
        var snapshot = basket with { Owner = "Copy" };

        basket.Items.Add("banana");

        Console.WriteLine($"  basket           = {basket}");
        Console.WriteLine($"  items            = [{string.Join(", ", basket.Items)}]");
        Console.WriteLine($"  snapshot items   = [{string.Join(", ", snapshot.Items)}]   <- with copied the reference, not the list");
        Console.WriteLine($"  same list?       = {ReferenceEquals(basket.Items, snapshot.Items)}");
    }
}

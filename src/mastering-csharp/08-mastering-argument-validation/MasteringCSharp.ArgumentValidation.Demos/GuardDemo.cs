namespace MasteringCSharp.ArgumentValidation.Demos;

/// <summary>
/// Lesson "Recreating ThrowIfNull Manually".
/// Covered in notes lesson 6.
/// </summary>
internal static class GuardDemo
{
    public static void Run()
    {
        Console.WriteLine("-- Step 1: the naive guard loses the parameter name --");

        try
        {
            NaiveGuard.ThrowIfNull(null);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  NaiveGuard.ThrowIfNull(item)  ParamName = '{ex.ParamName ?? "<null>"}'");
            Console.WriteLine($"  Message: {ex.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("  Nothing filled paramName in, so the exception cannot say what was wrong.");
        Console.WriteLine("  Passing nameof(item) by hand works and rots the moment the parameter is renamed.");

        Console.WriteLine();
        Console.WriteLine("-- Step 2: [CallerArgumentExpression] fills it in at the call site --");

        Item? item = null;
        try
        {
            Guard.ThrowIfNull(item);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  Guard.ThrowIfNull(item)  ParamName = '{ex.ParamName}'");
        }

        Console.WriteLine();
        Console.WriteLine("-- Step 3: it captures the whole expression, not just a name --");

        try
        {
            Guard.ThrowIfNull(GetItem());
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  Guard.ThrowIfNull(GetItem())  ParamName = '{ex.ParamName}'");
        }

        try
        {
            Guard.ThrowIfNull(Lookup()?.Name);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  Guard.ThrowIfNull(Lookup()?.Name)  ParamName = '{ex.ParamName}'");
        }

        Console.WriteLine();
        Console.WriteLine("  The compiler injects the source text of the argument as a string literal.");
        Console.WriteLine("  That is a feature for readability and a trap for anything that parses ParamName:");
        Console.WriteLine("  'GetItem()' is not a parameter name, and ArgumentNullException will happily report it.");

        Console.WriteLine();
        Console.WriteLine("-- Step 4: [NotNull] is what makes the line after the guard compile --");
        Console.WriteLine("  Guard.ThrowIfNull(item); _items.Add(item);");
        Console.WriteLine("  Without [NotNull] on the parameter, `item` is still Item? on the next line and");
        Console.WriteLine("  the Add call warns CS8604. With it, the compiler knows a returning call means non-null.");
        Console.WriteLine("  See Guard.cs - removing the attribute reproduces the warning.");

        Console.WriteLine();
        Console.WriteLine("-- The finished guard, in use --");

        var items = new GuardedByCustomGuard();
        items.AddItem(new Item("Launch Plan"));
        Console.WriteLine($"  AddItem(new Item(\"Launch Plan\")) -> accepted, count = {items.Count}");

        try
        {
            items.AddItem(null);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  AddItem(null) -> ArgumentNullException, ParamName = '{ex.ParamName}'");
        }
    }

    private static Item? GetItem() => null;

    private static Item? Lookup() => new Item(null!);
}

/// <summary>
/// The lesson's end state: a nullable parameter, a guard, and no nullability warning
/// on the line after it.
/// </summary>
public sealed class GuardedByCustomGuard
{
    private readonly List<Item> _items = [];

    public int Count => _items.Count;

    public void AddItem(Item? item)
    {
        Guard.ThrowIfNull(item);
        _items.Add(item);
    }
}

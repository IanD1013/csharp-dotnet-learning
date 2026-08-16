namespace MasteringCSharp.ArgumentValidation.Demos;

/// <summary>
/// Lesson "Argument Validation and Nullable Reference Types".
/// Covered in notes lesson 3.
/// Every route below compiles without a single nullability warning and still puts a
/// null into a collection declared as <c>List&lt;Item&gt;</c>.
/// </summary>
internal static class NullableReferenceTypesDemo
{
    public static void Run()
    {
        Console.WriteLine("-- Three ways a null reaches a non-nullable parameter, with no warning --");

        var unguarded = new UnguardedItems();

        // Route 1: the caller compiles with nullability off.
        DisabledNullabilityCaller.AddNull(unguarded);
        Console.WriteLine("  1. #nullable disable at the call site        -> accepted");

        // Route 2: the null-forgiving operator.
        unguarded.AddItem(GetItem()!);
        Console.WriteLine("  2. the null-forgiving operator `!`           -> accepted");

        // Route 3: the array analysis gap. `new Item[42]` is 42 nulls and the compiler
        // treats every element as initialised.
        Item[] array = new Item[42];
        unguarded.AddItem(array[0]);
        Console.WriteLine("  3. an element of a freshly allocated array   -> accepted");

        Console.WriteLine();
        Console.WriteLine($"  List<Item> now holds {unguarded.Count} entries, of which {unguarded.NullCount} are null.");
        Console.WriteLine("  Nothing has thrown yet. That is the butterfly effect the lesson warns about:");
        Console.WriteLine("  the NullReferenceException happens somewhere else, later, with no trace of this call.");

        Console.WriteLine();
        Console.WriteLine("-- Where it actually blows up --");
        try
        {
            Console.WriteLine(unguarded.DescribeAll());
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"  NullReferenceException in {nameof(UnguardedItems.DescribeAll)}: {ex.Message}");
            Console.WriteLine("  The stack trace points at DescribeAll. The bug was in AddItem, three calls ago.");
        }

        Console.WriteLine();
        Console.WriteLine("-- The same three routes against a guarded method --");

        var guarded = new GuardedItems();
        Report("#nullable disable", () => DisabledNullabilityCaller.AddNull(guarded));
        Report("null-forgiving `!`", () => guarded.AddItem(GetItem()!));
        Report("array[0]", () => guarded.AddItem(array[0]));

        Console.WriteLine();
        Console.WriteLine($"  List<Item> holds {guarded.Count} entries. Every bad call failed at the boundary,");
        Console.WriteLine("  naming the argument that was wrong.");
    }

    private static Item? GetItem() => null;

    private static void Report(string route, Action call)
    {
        try
        {
            call();
            Console.WriteLine($"  {route,-20} -> accepted (no exception)");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  {route,-20} -> ArgumentNullException, ParamName = '{ex.ParamName}'");
        }
    }
}

/// <summary>Chapter's sample payload type.</summary>
public sealed record Item(string Name);

/// <summary>
/// The lesson's starting point: a public method that trusts its caller because the
/// compiler said the parameter is non-nullable.
/// </summary>
public sealed class UnguardedItems
{
    private readonly List<Item> _items = [];

    public int Count => _items.Count;

    public int NullCount => _items.Count(i => i is null);

    // This is a public method: should
    // we validate the arguments?
    // For public methods: Yes!
    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    /// <summary>Reads the items back. This is where the delayed failure surfaces.</summary>
    public string DescribeAll() => string.Join(", ", _items.Select(i => i.Name));
}

/// <summary>The same class with the guard the lesson argues for.</summary>
public sealed class GuardedItems
{
    private readonly List<Item> _items = [];

    public int Count => _items.Count;

    // The lesson's conditional-compilation shape. ArgumentNullException.ThrowIfNull
    // arrived in .NET 6 and does not exist on .NET Framework, so a multi-targeted
    // project needs both halves. Notes lessons 4 and 5.
    public void AddItem(Item item)
    {
#if NET
        // Only available in .NET 6.0 and later.
        ArgumentNullException.ThrowIfNull(item);
#else
        // an old style validation for .NET Framework
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }
#endif

        _items.Add(item);
    }
}

#nullable disable

/// <summary>
/// Route 1 from the lesson: a caller compiled with nullability off. Passing null here
/// produces no warning, and the callee cannot tell the difference.
/// </summary>
internal static class DisabledNullabilityCaller
{
    public static void AddNull(UnguardedItems items) => items.AddItem(item: null);

    public static void AddNull(GuardedItems items) => items.AddItem(item: null);
}

#nullable restore

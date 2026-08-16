namespace MasteringCSharp.ArgumentValidation.Demos;

/// <summary>
/// Lessons "Issues With Nullable Strings in .NET Framework" and
/// "Implementing Custom String Validation".
/// Covered in notes lessons 8 and 9.
/// </summary>
internal static class StringValidationDemo
{
    public static void Run()
    {
        Console.WriteLine($"-- Running on {RuntimeName} --");
        Console.WriteLine();

        Console.WriteLine("-- Does this BCL annotate string.IsNullOrEmpty? --");
        Console.WriteLine($"  {AnnotationReport()}");
        Console.WriteLine();
        Console.WriteLine("  [NotNullWhen(false)] is what tells the compiler that a false return means non-null.");
        Console.WriteLine("  Without it, the compiler cannot narrow the string and warns CS8604 on the next line,");
        Console.WriteLine("  even though the check is correct. The code is identical on both targets; only the");
        Console.WriteLine("  reference assembly differs.");

        Console.WriteLine();
        Console.WriteLine("-- The guard that compiles clean on both targets --");

        var items = new KeyedItems();
        items.AddItem("launch", new Item("Launch Plan"));
        Console.WriteLine($"  AddItem(\"launch\", ...) -> accepted, count = {items.Count}");

        Report(() => items.AddItem(null, new Item("x")), "AddItem(null, ...)");
        Report(() => items.AddItem("", new Item("x")), "AddItem(\"\", ...)");

        Console.WriteLine();
        Console.WriteLine("  Two different exception types out of one guard: the throw helper looks at the");
        Console.WriteLine("  argument again and picks ArgumentNullException for null, ArgumentException for empty.");
        Console.WriteLine("  That matches what ArgumentException.ThrowIfNullOrEmpty does on .NET 7 and later.");

        Console.WriteLine();
        Console.WriteLine("-- Where the lesson's guard still warns, and the one-line fix --");
        Console.WriteLine("  The lesson's ThrowIfNullOrEmpty delegates its null test to string.IsNullOrEmpty.");
        Console.WriteLine("  On net48 that method is unannotated, so the compiler cannot verify the [NotNull]");
        Console.WriteLine("  promise and warns CS8777 at the guard's own closing brace. The call sites are");
        Console.WriteLine("  clean; the warning simply moved into the guard. See Guard.cs.");
        Console.WriteLine();
        Console.WriteLine("  ThrowIfNullOrEmptyPortable writes the test out as `argument is null || argument.Length == 0`.");
        Console.WriteLine("  Same behaviour, and the compiler can now check the contract itself:");

        Report(() => Guard.ThrowIfNullOrEmptyPortable(null), "ThrowIfNullOrEmptyPortable(null)");
        Report(() => Guard.ThrowIfNullOrEmptyPortable(""), "ThrowIfNullOrEmptyPortable(\"\")");
        Report(() => Guard.ThrowIfNullOrEmptyPortable("launch"), "ThrowIfNullOrEmptyPortable(\"launch\")");

#if NET
        Console.WriteLine();
        Console.WriteLine("-- The BCL version, for comparison (net10.0 only) --");
        Report(() => ArgumentException.ThrowIfNullOrEmpty(null), "ArgumentException.ThrowIfNullOrEmpty(null)");
        Report(() => ArgumentException.ThrowIfNullOrEmpty(""), "ArgumentException.ThrowIfNullOrEmpty(\"\")");
#endif
    }

    private static string RuntimeName =>
#if NET
        $".NET {Environment.Version} (net10.0 build)";
#else
        $".NET Framework {Environment.Version} (net48 build)";
#endif

    /// <summary>
    /// Reads the attributes off <c>string.IsNullOrEmpty</c>'s parameter at runtime.
    /// On net10.0 the reference assembly carries [NotNullWhen(false)]; on net48 it does not.
    /// </summary>
    private static string AnnotationReport()
    {
        var parameter = typeof(string)
            .GetMethod(nameof(string.IsNullOrEmpty), [typeof(string)])!
            .GetParameters()[0];

        var names = parameter.GetCustomAttributesData()
            .Select(a => a.AttributeType.Name)
            .ToArray();

        return names.Length == 0
            ? "string.IsNullOrEmpty(string?) parameter attributes: <none>"
            : $"string.IsNullOrEmpty(string?) parameter attributes: [{string.Join(", ", names)}]";
    }

    private static void Report(Action call, string label)
    {
        try
        {
            call();
            Console.WriteLine($"  {label,-45} -> accepted");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"  {label,-45} -> ArgumentNullException  (ParamName '{ex.ParamName}')");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  {label,-45} -> ArgumentException      (ParamName '{ex.ParamName}')");
        }
    }
}

/// <summary>
/// The lesson's dictionary example. The key is nullable on the way in and non-nullable
/// by the time it reaches <c>Dictionary.Add</c>, and the only thing bridging the two
/// is the [NotNull] on <see cref="Guard.ThrowIfNullOrEmpty"/>.
/// </summary>
public sealed class KeyedItems
{
    private readonly Dictionary<string, Item> _items = [];

    public int Count => _items.Count;

    public void AddItem(string? name, Item item)
    {
        Guard.ThrowIfNullOrEmpty(name);

        _items.Add(name, item);
    }
}

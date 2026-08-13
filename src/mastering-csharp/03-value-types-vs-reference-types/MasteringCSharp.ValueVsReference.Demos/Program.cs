using MasteringCSharp.ValueVsReference.Demos;

// Chapter 3 of Dometrain's "Mastering: C#", made runnable.
// Usage: dotnet run -c Release [-- storage|copy|equality]
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "storage":
        Section("Storage semantics", StorageDemo.Run);
        break;
    case "copy":
        Section("Copy semantics", CopySemanticsDemo.Run);
        break;
    case "equality":
        Section("Equality semantics", EqualitySemanticsDemo.Run);
        break;
    case "all":
        Section("Storage semantics", StorageDemo.Run);
        Section("Copy semantics", CopySemanticsDemo.Run);
        Section("Equality semantics", EqualitySemanticsDemo.Run);
        break;
    default:
        Console.Error.WriteLine($"Unknown section '{section}'. Expected: storage, copy, equality, all.");
        return 1;
}

return 0;

static void Section(string title, Action body)
{
    Console.WriteLine();
    Console.WriteLine(new string('=', 70));
    Console.WriteLine($"  {title}");
    Console.WriteLine(new string('=', 70));
    Console.WriteLine();
    body();
    Console.WriteLine();
}

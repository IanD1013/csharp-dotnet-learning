using MasteringCSharp.Records.Demos;

// Chapter 6 of Dometrain's "Mastering: C#", made runnable.
// Usage: dotnet run -c Release [-- equality|underhood|limitations|structs]
// Notes: src/mastering-csharp/notes/06-mastering-records.md
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "equality":
        Section("Referential vs. value-based equality", EqualityDemo.Run);
        break;
    case "underhood":
        Section("Records under the hood", UnderTheHoodDemo.Run);
        break;
    case "limitations":
        Section("Records limitations", LimitationsDemo.Run);
        break;
    case "structs":
        Section("Default struct equality vs. record struct", StructEqualityDemo.Run);
        break;
    case "all":
        Section("Referential vs. value-based equality", EqualityDemo.Run);
        Section("Records under the hood", UnderTheHoodDemo.Run);
        Section("Records limitations", LimitationsDemo.Run);
        Section("Default struct equality vs. record struct", StructEqualityDemo.Run);
        break;
    default:
        Console.Error.WriteLine($"Unknown section '{section}'. Expected: equality, underhood, limitations, structs, all.");
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

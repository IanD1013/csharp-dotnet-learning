using MasteringCSharp.TuplesAndUnions.Demos;

// Chapter 7 of Dometrain's "Mastering: C#", made runnable.
// Usage: dotnet run -c Release [-- valuetuple|tuples|limits|unions]
// Notes: src/mastering-csharp/notes/07-mastering-tuples-and-union-types.md
//
// The union sections run hand-written equivalents of the shape the C# 15 compiler
// generates, because the `union` keyword does not compile on the .NET 10 SDK.
// See the notes section "What does not compile here, and why".
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "valuetuple":
        Section("System.Tuple vs. System.ValueTuple", ValueTupleDemo.Run);
        break;
    case "tuples":
        Section("Tuples in C#", TuplesDemo.Run);
        break;
    case "limits":
        Section("Knowing the limits: when to move to actual types", LimitsDemo.Run);
        break;
    case "unions":
        Section("Union types, by hand", UnionsDemo.Run);
        break;
    case "all":
        Section("System.Tuple vs. System.ValueTuple", ValueTupleDemo.Run);
        Section("Tuples in C#", TuplesDemo.Run);
        Section("Knowing the limits: when to move to actual types", LimitsDemo.Run);
        Section("Union types, by hand", UnionsDemo.Run);
        break;
    default:
        Console.Error.WriteLine($"Unknown section '{section}'. Expected: valuetuple, tuples, limits, unions, all.");
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

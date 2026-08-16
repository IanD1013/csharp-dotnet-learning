using MasteringCSharp.ArgumentValidation.Demos;

// Chapter 8 of Dometrain's "Mastering: C#", made runnable.
// Usage: dotnet run -c Release -f net10.0 [-- nrt|nulls|guard|strings|boxing]
//        dotnet run -c Release -f net48   [-- ...]
// Notes: src/mastering-csharp/notes/08-mastering-argument-validation.md
//
// The project multi-targets net10.0 and net48 on purpose: several of the chapter's
// claims are about a BCL that predates nullable reference types, and the only honest
// way to show them is to build against that BCL and run it.
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "nrt":
        Section("Argument validation and nullable reference types", NullableReferenceTypesDemo.Run);
        break;
    case "nulls":
        Section("Checking for null in C#", NullChecksDemo.Run);
        break;
    case "guard":
        Section("Recreating ThrowIfNull manually", GuardDemo.Run);
        break;
    case "strings":
        Section("Nullable strings and custom string validation", StringValidationDemo.Run);
        break;
    case "boxing":
        Section("ThrowIfNull boxing allocations", BoxingDemo.Run);
        break;
    case "all":
        Section("Argument validation and nullable reference types", NullableReferenceTypesDemo.Run);
        Section("Checking for null in C#", NullChecksDemo.Run);
        Section("Recreating ThrowIfNull manually", GuardDemo.Run);
        Section("Nullable strings and custom string validation", StringValidationDemo.Run);
        Section("ThrowIfNull boxing allocations", BoxingDemo.Run);
        break;
    default:
        Console.Error.WriteLine($"Unknown section '{section}'. Expected: nrt, nulls, guard, strings, boxing, all.");
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

using DependencyInjection.CustomFramework.Demos;

// Chapter 8 of Dometrain's "From Zero to Hero: Dependency Injection in .NET with C#", made runnable.
// Usage: dotnet run [-- design|implementation|extending|recap]
// Notes: src/dependency-injection/notes/08-creating-our-own-dependency-injection-framework.md
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "design":
        Section("The design", DesignDemo.Run);
        break;
    case "implementation":
        Section("The implementation", ImplementationDemo.Run);
        break;
    case "extending":
        Section("Extending the main implementation", ExtendingDemo.Run);
        break;
    case "recap":
        Section("Section recap", RecapDemo.Run);
        break;
    case "all":
        Section("The design", DesignDemo.Run);
        Section("The implementation", ImplementationDemo.Run);
        Section("Extending the main implementation", ExtendingDemo.Run);
        Section("Section recap", RecapDemo.Run);
        break;
    default:
        Console.Error.WriteLine(
            $"Unknown section '{section}'. Expected: design, implementation, extending, recap, all.");
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

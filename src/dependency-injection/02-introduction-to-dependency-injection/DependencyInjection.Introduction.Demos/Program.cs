using DependencyInjection.Introduction.Demos;

// Chapter 2 of Dometrain's "From Zero to Hero: Dependency Injection in .NET with C#", made runnable.
// Usage: dotnet run [-- car|data|clock|container]
// Notes: src/dependency-injection/notes/02-introduction-to-dependency-injection.md
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "car":
        Section("The problem with dependencies, and why DI fixes it", CarDemo.Run);
        break;
    case "data":
        await SectionAsync("A practical example: service, repository, database", DataDemo.RunAsync);
        break;
    case "clock":
        Section("A less obvious dependency: the system clock", ClockDemo.Run);
        break;
    case "container":
        await SectionAsync("Doing it manually vs. the built-in container", ContainerDemo.RunAsync);
        break;
    case "all":
        Section("The problem with dependencies, and why DI fixes it", CarDemo.Run);
        await SectionAsync("A practical example: service, repository, database", DataDemo.RunAsync);
        Section("A less obvious dependency: the system clock", ClockDemo.Run);
        await SectionAsync("Doing it manually vs. the built-in container", ContainerDemo.RunAsync);
        break;
    default:
        Console.Error.WriteLine($"Unknown section '{section}'. Expected: car, data, clock, container, all.");
        return 1;
}

return 0;

static void Section(string title, Action body)
{
    WriteHeader(title);
    body();
    Console.WriteLine();
}

static async Task SectionAsync(string title, Func<Task> body)
{
    WriteHeader(title);
    await body();
    Console.WriteLine();
}

static void WriteHeader(string title)
{
    Console.WriteLine();
    Console.WriteLine(new string('=', 70));
    Console.WriteLine($"  {title}");
    Console.WriteLine(new string('=', 70));
    Console.WriteLine();
}

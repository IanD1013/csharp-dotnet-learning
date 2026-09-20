using DependencyInjection.Advanced.Demos;

// Chapter 6 of Dometrain's "From Zero to Hero: Dependency Injection in .NET with C#", made runnable.
// Usage: dotnet run [-- scope|locator|orchestrator|capturing|providers|decorator|sourcegen]
// Notes: src/dependency-injection/notes/06-advanced-techniques.md
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "scope":
        Section("Creating a custom scope", CustomScopeDemo.Run);
        break;
    case "locator":
        await SectionAsync("The service locator anti-pattern", ServiceLocatorDemo.RunAsync);
        break;
    case "orchestrator":
        await SectionAsync("When service locator makes sense", HandlerOrchestratorDemo.RunAsync);
        break;
    case "capturing":
        await SectionAsync("Avoiding capturing dependencies", CapturedDependencyDemo.RunAsync);
        break;
    case "providers":
        await SectionAsync("Avoiding multiple service providers", MultipleProvidersDemo.RunAsync);
        break;
    case "decorator":
        await SectionAsync("Creating decorators", DecoratorDemo.RunAsync);
        break;
    case "sourcegen":
        Section("The future of dependency injection", SourceGeneratedDemo.Run);
        break;
    case "all":
        Section("Creating a custom scope", CustomScopeDemo.Run);
        await SectionAsync("The service locator anti-pattern", ServiceLocatorDemo.RunAsync);
        await SectionAsync("When service locator makes sense", HandlerOrchestratorDemo.RunAsync);
        await SectionAsync("Avoiding capturing dependencies", CapturedDependencyDemo.RunAsync);
        await SectionAsync("Avoiding multiple service providers", MultipleProvidersDemo.RunAsync);
        await SectionAsync("Creating decorators", DecoratorDemo.RunAsync);
        Section("The future of dependency injection", SourceGeneratedDemo.Run);
        break;
    default:
        Console.Error.WriteLine(
            $"Unknown section '{section}'. Expected: scope, locator, orchestrator, capturing, providers, decorator, sourcegen, all.");
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

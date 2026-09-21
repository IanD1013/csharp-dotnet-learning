using DependencyInjection.Scrutor.Demos;

// Chapter 7 of Dometrain's "From Zero to Hero: Dependency Injection in .NET with C#", made runnable.
// Usage: dotnet run [-- intro|decorate|timed|scanning|interfaces|attributes|namespaces|descriptor|strategies|pitfalls]
// Notes: src/dependency-injection/notes/07-extending-dependency-injection-with-scrutor.md
string section = args.Length > 0 ? args[0].ToLowerInvariant() : "all";

switch (section)
{
    case "intro":
        Section("What is Scrutor?", IntroDemo.Run);
        break;
    case "decorate":
        await SectionAsync("Registering service decorators", DecoratorDemo.RunAsync);
        break;
    case "timed":
        await SectionAsync("Surprise optional refactoring lecture", TimedOperationDemo.RunAsync);
        break;
    case "scanning":
        Section("Service registration by scanning", ScanningDemo.Run);
        break;
    case "interfaces":
        Section("Interface marking", InterfaceMarkingDemo.Run);
        break;
    case "attributes":
        Section("Attribute marking", AttributeMarkingDemo.Run);
        break;
    case "namespaces":
        Section("Namespace filtering", NamespaceFilteringDemo.Run);
        break;
    case "descriptor":
        Section("Using the ServiceDescriptor attribute", ServiceDescriptorDemo.Run);
        break;
    case "strategies":
        Section("Using RegistrationStrategies", RegistrationStrategyDemo.Run);
        break;
    case "pitfalls":
        Section("Potential pitfalls", PitfallsDemo.Run);
        break;
    case "all":
        Section("What is Scrutor?", IntroDemo.Run);
        await SectionAsync("Registering service decorators", DecoratorDemo.RunAsync);
        await SectionAsync("Surprise optional refactoring lecture", TimedOperationDemo.RunAsync);
        Section("Service registration by scanning", ScanningDemo.Run);
        Section("Interface marking", InterfaceMarkingDemo.Run);
        Section("Attribute marking", AttributeMarkingDemo.Run);
        Section("Namespace filtering", NamespaceFilteringDemo.Run);
        Section("Using the ServiceDescriptor attribute", ServiceDescriptorDemo.Run);
        Section("Using RegistrationStrategies", RegistrationStrategyDemo.Run);
        Section("Potential pitfalls", PitfallsDemo.Run);
        break;
    default:
        Console.Error.WriteLine(
            $"Unknown section '{section}'. Expected: intro, decorate, timed, scanning, interfaces, "
            + "attributes, namespaces, descriptor, strategies, pitfalls, all.");
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

using DependencyInjection.Scrutor.Demos.Output;
using DependencyInjection.Scrutor.Demos.Scanning;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 4: Service registration by scanning.
/// </summary>
public static class ScanningDemo
{
    // All ten sections live in one assembly, so every filter below is anchored to this
    // namespace. The course scans a project that contains nothing but its own examples.
    private const string Namespace = "DependencyInjection.Scrutor.Demos.Scanning";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine("AsMatchingInterface: register ExampleAService as IExampleAService, by name.");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace))
                .AsMatchingInterface());
        Console.WriteLine("  -> ExampleABService is missing: no IExampleABService exists to match its name");
        Console.WriteLine();

        Console.WriteLine("AsSelf: register every class as its own type.");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace))
                .AsSelf());
        Console.WriteLine();

        Console.WriteLine("AsImplementedInterfaces, with the default transient lifetime replaced by singleton.");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        Console.WriteLine("  -> ExampleABService appears twice, once per interface it implements");
        Console.WriteLine();

        Console.WriteLine("AsSelfWithInterfaces: self plus interfaces, wired through a factory.");
        var shared = new ServiceCollection();
        shared.Scan(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).Where(t => t == typeof(ExampleABService)))
                .AsSelfWithInterfaces()
                .WithSingletonLifetime());
        Registrations.Print(shared, Namespace);

        using (var provider = shared.BuildServiceProvider())
        {
            var self = provider.GetRequiredService<ExampleABService>();
            var asA = provider.GetRequiredService<IExampleAService>();
            var asB = provider.GetRequiredService<IExampleBService>();
            Console.WriteLine($"  all three resolve to one instance: {ReferenceEquals(self, asA) && ReferenceEquals(self, asB)}");
        }

        Console.WriteLine("  -> the two interfaces are factories that forward to the self registration,");
        Console.WriteLine("     which is why the singleton is not duplicated three times over");
        Console.WriteLine();

        Console.WriteLine("Where(...EndsWith(\"Repository\")): the whole repository layer in one declaration.");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).Where(t => t.Name.EndsWith("Repository", StringComparison.Ordinal)))
                .AsMatchingInterface()
                .WithScopedLifetime());
        Console.WriteLine();

        Console.WriteLine("Two AddClasses calls in one Scan: services singleton, repositories scoped.");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).Where(t => t.Name.EndsWith("Service", StringComparison.Ordinal)))
                    .AsMatchingInterface()
                    .WithSingletonLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).Where(t => t.Name.EndsWith("Repository", StringComparison.Ordinal)))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        Console.WriteLine("  -> each AddClasses resets the context, so the two rules do not bleed into each other");
    }

    private static void Show(Action<ITypeSourceSelector> configure)
    {
        var services = new ServiceCollection();
        services.Scan(configure);
        Registrations.Print(services, Namespace);
    }
}

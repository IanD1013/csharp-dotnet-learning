using DependencyInjection.Scrutor.Demos.InterfaceMarking;
using DependencyInjection.Scrutor.Demos.Output;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 5: Interface marking.
/// </summary>
public static class InterfaceMarkingDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.InterfaceMarking";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine("Name-based filtering gives one lifetime to everything it catches:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).Where(t => t.Name.EndsWith("Service", StringComparison.Ordinal)))
                .AsMatchingInterface()
                .WithScopedLifetime());
        Console.WriteLine("  -> three services, three scoped registrations, whether that was wanted or not");
        Console.WriteLine();

        Console.WriteLine("AssignableTo<T> over marker interfaces, one AddClasses per lifetime:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).AssignableTo<ISingletonService>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).AssignableTo<ITransientService>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).AssignableTo<IScopedService>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        Console.WriteLine("  -> each class got the lifetime it asked for, and the marker interfaces");
        Console.WriteLine("     were registered as service types too, because they are implemented interfaces");
        Console.WriteLine();

        Console.WriteLine("The same scan with AsMatchingInterface instead:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).AssignableTo<ISingletonService>())
                    .AsMatchingInterface()
                    .WithSingletonLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).AssignableTo<ITransientService>())
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).AssignableTo<IScopedService>())
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        Console.WriteLine("  -> the markers are gone, but only because every class is named after its interface");
    }

    private static void Show(Action<ITypeSourceSelector> configure)
    {
        var services = new ServiceCollection();
        services.Scan(configure);
        Registrations.Print(services, Namespace);
    }
}

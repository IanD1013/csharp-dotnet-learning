using DependencyInjection.Scrutor.Demos.AttributeMarking;
using DependencyInjection.Scrutor.Demos.Output;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 7: Namespace filtering.
/// </summary>
public static class NamespaceFilteringDemo
{
    private const string Root = "DependencyInjection.Scrutor.Demos.NamespaceFiltering";
    private const string ServicesNamespace = Root + ".Services";
    private const string InternalNamespace = Root + ".Internal";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine($"InNamespaces(\"{ServicesNamespace}\"), everything singleton:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(ServicesNamespace))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        Console.WriteLine("  -> ExampleAService still carries [Singleton], but the scan never looked:");
        Console.WriteLine("     the lifetime came from the Scan call, and the attribute was ignored");
        Console.WriteLine();

        Console.WriteLine("Every class under the chapter's namespace gets the same lifetime, wanted or not:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Root))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        Console.WriteLine("  -> the two internal types came along for the ride, as singletons");
        Console.WriteLine();

        Console.WriteLine($"NotInNamespaces(\"{InternalNamespace}\") puts them back out:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Root).NotInNamespaces(InternalNamespace))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        Console.WriteLine();

        Console.WriteLine("WithoutAttribute<ScopedAttribute> excludes the one edge case instead:");
        Show(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Root).WithoutAttribute<ScopedAttribute>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        Console.WriteLine("  -> DiagnosticsService is gone, CacheWarmerService stayed");
        Console.WriteLine();

        Console.WriteLine($"And the whole thing hinges on a string: \"{ServicesNamespace}\"");
        Console.WriteLine("  -> rename the folder in a refactor and the compiler says nothing; the app");
        Console.WriteLine("     starts and fails on the first resolve, which is what the unit tests are for");
    }

    private static void Show(Action<ITypeSourceSelector> configure)
    {
        var services = new ServiceCollection();
        services.Scan(configure);
        Registrations.Print(services, Root);
    }
}

using DependencyInjection.Scrutor.Demos.AttributeMarking;
using DependencyInjection.Scrutor.Demos.Output;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 9: Using RegistrationStrategies.
/// </summary>
public static class RegistrationStrategyDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.Strategies";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine("ExampleBService carries both [Singleton] and [Transient], so two passes claim it.");
        Console.WriteLine();

        Console.WriteLine("Default (Append):");
        Show(strategy: null);
        Console.WriteLine("  -> registered twice, the same as calling Add twice");
        Console.WriteLine();

        Console.WriteLine("RegistrationStrategy.Skip:");
        Show(RegistrationStrategy.Skip);
        Console.WriteLine("  -> the transient pass found IExampleBService already there and left it alone");
        Console.WriteLine();

        Console.WriteLine("RegistrationStrategy.Replace():");
        Show(RegistrationStrategy.Replace());
        Console.WriteLine("  -> the singleton row is gone; the transient pass overwrote it");
        Console.WriteLine();

        Console.WriteLine("RegistrationStrategy.Throw:");
        try
        {
            Show(RegistrationStrategy.Throw);
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"  {exception.GetType().Name}: {exception.Message}");
        }

        Console.WriteLine("  -> the app refuses to start, which is the point: the duplicate was a mistake");
    }

    private static void Show(RegistrationStrategy? strategy)
    {
        var services = new ServiceCollection();
        services.Scan(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace).WithAttribute<SingletonAttribute>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
                .AddClasses(f => f.InNamespaces(Namespace).WithAttribute<TransientAttribute>())
                    .UsingRegistrationStrategy(strategy ?? RegistrationStrategy.Append)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        Registrations.Print(services, Namespace);
    }
}

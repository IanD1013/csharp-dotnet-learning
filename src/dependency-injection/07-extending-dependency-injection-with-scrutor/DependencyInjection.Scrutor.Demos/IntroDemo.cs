using DependencyInjection.Scrutor.Demos.Output;
using DependencyInjection.Scrutor.Demos.Scanning;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 1: What is Scrutor?
/// </summary>
public static class IntroDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.Scanning";

    /// <summary>Runs the section.</summary>
    public static void Run()
    {
        Console.WriteLine($"Scrutor version in use: {typeof(RegistrationStrategy).Assembly.GetName().Version}");
        Console.WriteLine();

        Console.WriteLine("Registered by hand, one line per service:");
        var manual = new ServiceCollection();
        manual.AddTransient<IExampleAService, ExampleAService>();
        manual.AddTransient<IExampleBService, ExampleBService>();
        manual.AddTransient<IUserRepository, UserRepository>();
        manual.AddTransient<IOrderRepository, OrderRepository>();
        Registrations.Print(manual, Namespace);
        Console.WriteLine();

        Console.WriteLine("The same four descriptors, produced by one scan instead:");
        var scanned = new ServiceCollection();
        scanned.Scan(selector => selector
            .FromAssemblyOf<Program>()
                .AddClasses(f => f.InNamespaces(Namespace))
                .AsMatchingInterface());
        Registrations.Print(scanned, Namespace);
        Console.WriteLine();

        Console.WriteLine("  -> the container cannot tell the two apart, and the second one keeps working");
        Console.WriteLine("     as services are added, because it describes a rule rather than a list");
    }
}

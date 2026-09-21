using DependencyInjection.CustomFramework.Demos.Services;

using Vax;

namespace DependencyInjection.CustomFramework.Demos;

/// <summary>
/// Lesson 2 - The design. The target API, written before any of it existed: a
/// ServiceCollection, AddSingleton, BuildServiceProvider, GetRequiredService.
/// Nothing here mentions Microsoft.Extensions.DependencyInjection.
/// </summary>
public static class DesignDemo
{
    public static void Run()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConsoleWriter, ConsoleWriter>();

        var serviceProvider = services.BuildServiceProvider();

        var service = serviceProvider.GetRequiredService<IConsoleWriter>();

        service.WriteLine("Hello from DI");

        Console.WriteLine($"  resolved {service.GetType().Name} for {nameof(IConsoleWriter)}");
        Console.WriteLine("  -> same five lines you would write against the built-in container,");
        Console.WriteLine("     running entirely on the Vax project next door");

        Console.WriteLine();
        Console.WriteLine("GetRequiredService on something nobody registered:");
        try
        {
            serviceProvider.GetRequiredService<IIdGenerator>();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"  {exception.GetType().Name}: {exception.Message}");
        }

        Console.WriteLine($"  GetService returned {serviceProvider.GetService<IIdGenerator>()?.ToString() ?? "null"} instead of throwing");
    }
}

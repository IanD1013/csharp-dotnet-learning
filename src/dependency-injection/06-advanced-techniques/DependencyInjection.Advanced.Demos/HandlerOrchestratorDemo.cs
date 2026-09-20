using System.Reflection;
using DependencyInjection.Advanced.Demos.Handlers;
using DependencyInjection.Advanced.Demos.Output;
using DependencyInjection.Advanced.Demos.Time;
using DependencyInjection.Advanced.Demos.Weather;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 3: When service locator makes sense.
/// </summary>
public static class HandlerOrchestratorDemo
{
    public static async Task RunAsync()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IConsoleWriter, ConsoleWriter>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IWeatherService, LocalWeatherService>();

        // Scans the assembly, registers every IHandler it finds, and registers the orchestrator.
        services.AddCommandHandlers(Assembly.GetExecutingAssembly());

        services.AddSingleton<Application>();

        var serviceProvider = services.BuildServiceProvider();
        var application = serviceProvider.GetRequiredService<Application>();
        var orchestrator = serviceProvider.GetRequiredService<HandlerOrchestrator>();

        Console.WriteLine("Commands discovered by scanning for [CommandName] on IHandler types:");
        foreach (var (command, handlerType) in orchestrator.KnownCommands.OrderBy(x => x.Key, StringComparer.Ordinal))
        {
            Console.WriteLine($"  \"{command}\" -> {handlerType.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Dispatching, with Application injecting only the orchestrator:");

        Console.Write("  weather -> ");
        await application.RunAsync(["weather"]);

        Console.Write("  time    -> ");
        await application.RunAsync(["time"]);

        Console.Write("  coffee  -> ");
        await application.RunAsync(["coffee"]);

        Console.WriteLine("  -> a new command is a new class plus an attribute, nothing else changes");
    }
}

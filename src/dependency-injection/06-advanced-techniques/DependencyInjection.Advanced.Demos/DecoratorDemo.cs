using DependencyInjection.Advanced.Demos.Api;
using DependencyInjection.Advanced.Demos.Weather;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 6: Creating decorators.
/// </summary>
public static class DecoratorDemo
{
    public static async Task RunAsync()
    {
        var services = new ServiceCollection();
        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddProvider(new InlineLoggerProvider());
        });

        // The concrete service is registered as itself, so the factory below can ask for it
        // without asking for IWeatherService and resolving the decorator recursively.
        services.AddTransient<LocalWeatherService>();
        services.AddTransient<IWeatherService>(provider =>
            new LoggedWeatherService(provider.GetRequiredService<LocalWeatherService>(),
                provider.GetRequiredService<ILogger<IWeatherService>>()));

        var serviceProvider = services.BuildServiceProvider();

        var weatherService = serviceProvider.GetRequiredService<IWeatherService>();
        Console.WriteLine($"IWeatherService resolved to {weatherService.GetType().Name}, which wraps {nameof(LocalWeatherService)}.");
        Console.WriteLine();

        Console.WriteLine("Calling it, with the timing written by the decorator and nothing else:");
        var weather = await weatherService.GetCurrentWeatherAsync("Athens");
        Console.WriteLine($"  The temperature in {weather!.Name} is {weather.Main.Temp}C");
        Console.WriteLine("  -> LocalWeatherService never learned what a Stopwatch is");
    }
}

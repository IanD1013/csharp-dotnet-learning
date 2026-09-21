using DependencyInjection.Scrutor.Demos.Logging;
using DependencyInjection.Scrutor.Demos.Output;
using DependencyInjection.Scrutor.Demos.Weather;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 3: Surprise optional refactoring lecture.
/// </summary>
public static class TimedOperationDemo
{
    /// <summary>Runs the section.</summary>
    public static async Task RunAsync()
    {
        var services = new ServiceCollection();
        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddProvider(new InlineLoggerProvider());
        });
        services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        services.AddTransient<IWeatherService, LocalWeatherService>();
        services.Decorate<IWeatherService, TimedLoggedWeatherService>();

        await using var provider = services.BuildServiceProvider();

        Console.WriteLine("Before: LoggedWeatherService held a Stopwatch, a try and a finally around one await.");
        Console.WriteLine("After:  TimedLoggedWeatherService is a using statement and a return.");
        Console.WriteLine();

        var weatherService = provider.GetRequiredService<IWeatherService>();
        Console.WriteLine("Calling the decorated service:");
        var weather = await weatherService.GetCurrentWeatherAsync("Athens");
        Console.WriteLine($"  it is {weather!.Main.Temp}C in {weather.Name}");
        Console.WriteLine("  -> the timing line above was written by Dispose, not by the service");
        Console.WriteLine();

        Console.WriteLine("The same operation used inline, the way the course uses it in a controller:");
        var logger = provider.GetRequiredService<ILoggerAdapter<WeatherEndpoint>>();
        using (var _ = logger.TimedOperation("{0} response", nameof(WeatherEndpoint)))
        {
            await Task.Delay(40);
            Console.WriteLine("  ...building the response...");
        }

        Console.WriteLine("  -> the block above timed itself, with no Stopwatch in sight");
    }
}

/// <summary>
/// Stands in for the course's WeatherForecastController, so the inline TimedOperation above has
/// a realistic category to log under. Not part of the course code.
/// </summary>
public sealed class WeatherEndpoint;

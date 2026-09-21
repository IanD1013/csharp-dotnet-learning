using DependencyInjection.Scrutor.Demos.Output;
using DependencyInjection.Scrutor.Demos.Weather;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos;

/// <summary>
/// Lesson 2: Registering service decorators.
/// </summary>
public static class DecoratorDemo
{
    private const string Namespace = "DependencyInjection.Scrutor.Demos.Weather";

    /// <summary>Runs the section.</summary>
    public static async Task RunAsync()
    {
        Console.WriteLine("The native way: register the concrete type, then hand-build the decorator in a factory.");
        var manual = new ServiceCollection();
        AddLogging(manual);
        manual.AddTransient<LocalWeatherService>();
        manual.AddTransient<IWeatherService>(provider =>
            new LoggedWeatherService(provider.GetRequiredService<LocalWeatherService>(),
                provider.GetRequiredService<ILogger<IWeatherService>>()));
        Registrations.Print(manual, Namespace);
        await Resolve(manual, "Athens");
        Console.WriteLine();

        Console.WriteLine("The Scrutor way: register the service normally, then decorate it.");
        var decorated = new ServiceCollection();
        AddLogging(decorated);
        decorated.AddTransient<IWeatherService, LocalWeatherService>();
        decorated.Decorate<IWeatherService, LoggedWeatherService>();
        Registrations.Print(decorated, Namespace);
        await Resolve(decorated, "London");
        Console.WriteLine();
        Console.WriteLine("  -> Decorate moved the original registration onto a generated key and put the");
        Console.WriteLine("     decorator's factory at IWeatherService, which is how the decorator receives");
        Console.WriteLine("     the inner service without asking for IWeatherService and recursing");
        Console.WriteLine();

        Console.WriteLine("Decorate on a service nobody registered:");
        var missing = new ServiceCollection();
        AddLogging(missing);
        try
        {
            missing.Decorate<IWeatherService, LoggedWeatherService>();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"  {exception.GetType().Name}: {exception.Message}");
        }

        Console.WriteLine($"  TryDecorate on the same empty collection returned {missing.TryDecorate<IWeatherService, LoggedWeatherService>()}");
        Console.WriteLine("  -> that is the Try the name refers to: \"decorate if the service is there\",");
        Console.WriteLine("     not \"decorate unless it is already decorated\"");
        Console.WriteLine();

        Console.WriteLine("Which means calling it twice does stack two decorators:");
        var tried = new ServiceCollection();
        AddLogging(tried);
        tried.AddTransient<IWeatherService, LocalWeatherService>();
        Console.WriteLine($"  first TryDecorate returned {tried.TryDecorate<IWeatherService, LoggedWeatherService>()}");
        Console.WriteLine($"  second TryDecorate returned {tried.TryDecorate<IWeatherService, LoggedWeatherService>()}");
        await Resolve(tried, "Auckland");
        Console.WriteLine("  -> two timing lines, one per layer: a registration helper run twice double-wraps");
    }

    private static void AddLogging(IServiceCollection services) =>
        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddProvider(new InlineLoggerProvider());
        });

    private static async Task Resolve(IServiceCollection services, string city)
    {
        await using var provider = services.BuildServiceProvider();
        var weatherService = provider.GetRequiredService<IWeatherService>();
        Console.WriteLine($"  resolved {weatherService.GetType().Name}, calling it for {city}:");
        var weather = await weatherService.GetCurrentWeatherAsync(city);
        Console.WriteLine($"  it is {weather!.Main.Temp}C in {weather.Name}");
    }
}

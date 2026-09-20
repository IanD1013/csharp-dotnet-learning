using DependencyInjection.Advanced.Demos.Api;
using DependencyInjection.Advanced.Demos.Weather;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 2: The service locator anti-pattern.
/// </summary>
public static class ServiceLocatorDemo
{
    public static async Task RunAsync()
    {
        var builder = InProcessApi.CreateBuilder();
        builder.Services.AddControllers();
        builder.Services.AddTransient<IWeatherService, LocalWeatherService>();
        builder.Services.AddScoped<DurationLoggerFilter>();

        var app = builder.Build();
        app.MapControllers();

        using var client = await InProcessApi.StartAsync(app);

        Console.WriteLine("[DurationLogger] pulls ILogger out of HttpContext.RequestServices:");
        await client.GetAsync(new Uri("located/weather/London", UriKind.Relative));
        Console.WriteLine("  -> the filter works, but nothing in its signature admits it needs a logger");
        Console.WriteLine();

        Console.WriteLine("[ServiceFilter<DurationLoggerFilter>] takes ILogger in its constructor:");
        await client.GetAsync(new Uri("injected/weather/London", UriKind.Relative));
        Console.WriteLine("  -> same log line, and the dependency is now part of the type's contract");

        await app.StopAsync();
    }
}

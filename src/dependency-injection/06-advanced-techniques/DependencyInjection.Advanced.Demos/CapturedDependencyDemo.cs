using System.Net.Http.Json;
using DependencyInjection.Advanced.Demos.Api;
using DependencyInjection.Advanced.Demos.Weather;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 4: Avoiding capturing dependencies.
/// </summary>
public static class CapturedDependencyDemo
{
    public static async Task RunAsync()
    {
        await CapturedInAClosure();
        Console.WriteLine();
        await InjectedAsAParameter();
    }

    private static async Task CapturedInAClosure()
    {
        var builder = InProcessApi.CreateBuilder();
        builder.Services.AddTransient<IWeatherService, LocalWeatherService>();

        var app = builder.Build();

        // Resolved once, from the root provider, before any request exists. The lambda below
        // closes over it, so every request for the rest of the process shares this one instance.
        var weatherService = app.Services.GetRequiredService<IWeatherService>();

        app.MapGet("weather/{city}", async ([FromRoute] string city) =>
        {
            var weather = await weatherService.GetCurrentWeatherAsync(city);
            return weather is null ? Results.NotFound() : Results.Ok(weatherService.InstanceId);
        });

        using var client = await InProcessApi.StartAsync(app);

        Console.WriteLine("Resolved from app.Services and captured by the endpoint lambda:");
        Console.WriteLine($"  request 1 used instance {await GetInstanceIdAsync(client)}");
        Console.WriteLine($"  request 2 used instance {await GetInstanceIdAsync(client)}");
        Console.WriteLine("  -> registered Transient, behaving like a singleton for the app's whole life");

        await app.StopAsync();
    }

    private static async Task InjectedAsAParameter()
    {
        var builder = InProcessApi.CreateBuilder();
        builder.Services.AddTransient<IWeatherService, LocalWeatherService>();

        var app = builder.Build();

        app.MapGet("weather/{city}", async ([FromRoute] string city, IWeatherService weatherService) =>
        {
            var weather = await weatherService.GetCurrentWeatherAsync(city);
            return weather is null ? Results.NotFound() : Results.Ok(weatherService.InstanceId);
        });

        using var client = await InProcessApi.StartAsync(app);

        Console.WriteLine("Declared as a handler parameter, so it comes from the request scope:");
        Console.WriteLine($"  request 1 used instance {await GetInstanceIdAsync(client)}");
        Console.WriteLine($"  request 2 used instance {await GetInstanceIdAsync(client)}");
        Console.WriteLine("  -> a fresh instance per request, which is what Transient promised");

        await app.StopAsync();
    }

    private static async Task<Guid> GetInstanceIdAsync(HttpClient client)
        => await client.GetFromJsonAsync<Guid>(new Uri("weather/London", UriKind.Relative));
}

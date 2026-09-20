namespace DependencyInjection.Advanced.Demos.Weather;

/// <summary>
/// Stands in for the course's OpenWeatherService, which calls api.openweathermap.org with a
/// hardcoded API key. Every lesson in this chapter is about lifetimes, scopes, decorators and
/// service location, none of which need a real HTTP call, so this returns canned data and the
/// demo stays runnable offline and deterministic.
/// </summary>
public sealed class LocalWeatherService : IWeatherService
{
    private static readonly Dictionary<string, double> Temperatures = new(StringComparer.OrdinalIgnoreCase)
    {
        ["London"] = 11.4,
        ["Athens"] = 24.8,
        ["Auckland"] = 17.2,
    };

    public Guid InstanceId { get; } = Guid.NewGuid();

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        // Just enough latency that the decorator in lesson 6 has something to time.
        await Task.Delay(25);

        return Temperatures.TryGetValue(city, out var temperature)
            ? new WeatherResponse { Name = city, Main = new Main { Temp = temperature } }
            : null;
    }
}

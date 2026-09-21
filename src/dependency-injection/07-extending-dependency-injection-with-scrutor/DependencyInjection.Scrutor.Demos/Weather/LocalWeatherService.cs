namespace DependencyInjection.Scrutor.Demos.Weather;

/// <summary>
/// Stands in for the course's OpenWeatherService, which calls api.openweathermap.org with a key
/// that expired years ago. Local data keeps the demo offline and instant; the decorator on top
/// of it is unchanged. Not part of the course code.
/// </summary>
public sealed class LocalWeatherService : IWeatherService
{
    private static readonly Dictionary<string, double> Temperatures = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Athens"] = 27.4,
        ["London"] = 14.1,
        ["Auckland"] = 18.9,
    };

    /// <inheritdoc />
    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        // A real HTTP call would not be instant either, and a 0ms timing line teaches nothing.
        await Task.Delay(25);

        return Temperatures.TryGetValue(city, out var temperature)
            ? new WeatherResponse(city, new MainWeather(temperature))
            : null;
    }
}

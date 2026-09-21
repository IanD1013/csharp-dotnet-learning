using DependencyInjection.Scrutor.Demos.Logging;

namespace DependencyInjection.Scrutor.Demos.Weather;

/// <summary>
/// Lesson 3: the same decorator after the refactoring, with the Stopwatch and the try-finally
/// hidden behind a using statement.
/// </summary>
public sealed class TimedLoggedWeatherService : IWeatherService
{
    private readonly IWeatherService _weatherService;
    private readonly ILoggerAdapter<IWeatherService> _logger;

    /// <summary>Wraps an inner weather service.</summary>
    public TimedLoggedWeatherService(IWeatherService weatherService,
        ILoggerAdapter<IWeatherService> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        using var _ = _logger.TimedOperation("Weather retrieval for city: {0},", city);
        return await _weatherService.GetCurrentWeatherAsync(city);
    }
}

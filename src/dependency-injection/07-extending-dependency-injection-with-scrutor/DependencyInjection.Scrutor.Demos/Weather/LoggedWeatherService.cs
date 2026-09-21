using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Scrutor.Demos.Weather;

/// <summary>
/// Lesson 2: the decorator, with the Stopwatch and try-finally written out by hand.
/// Lesson 3 refactors this shape into <see cref="TimedLoggedWeatherService"/>.
/// </summary>
public sealed class LoggedWeatherService : IWeatherService
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<IWeatherService> _logger;

    /// <summary>Wraps an inner weather service.</summary>
    public LoggedWeatherService(IWeatherService weatherService,
        ILogger<IWeatherService> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string city)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            return await _weatherService.GetCurrentWeatherAsync(city);
        }
        finally
        {
            sw.Stop();

            // CA2253 wants named placeholders. The course writes {0} and {1}, and lesson 3
            // carries that same template into TimedLogOperation, so it stays as recorded.
#pragma warning disable CA2253
            _logger.LogInformation("Weather retrieval for city: {0}, took {1}ms",
                city, sw.ElapsedMilliseconds);
#pragma warning restore CA2253
        }
    }
}

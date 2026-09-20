using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Advanced.Demos.Weather;

/// <summary>
/// Lesson 6. Adds timing and logging around any other <see cref="IWeatherService"/> without
/// touching that service's own implementation.
/// </summary>
public sealed class LoggedWeatherService : IWeatherService
{
    private readonly IWeatherService _weatherService; //<-- LocalWeatherService
    private readonly ILogger<IWeatherService> _logger;

    public LoggedWeatherService(IWeatherService weatherService,
        ILogger<IWeatherService> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    public Guid InstanceId => _weatherService.InstanceId;

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
            _logger.LogInformation("Weather retrieval for city: {City}, took {Elapsed}ms",
                city, sw.ElapsedMilliseconds);
        }
    }
}

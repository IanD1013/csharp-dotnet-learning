using DependencyInjection.Advanced.Demos.Weather;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Advanced.Demos.Api;

[ApiController]
public sealed class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(IWeatherService weatherService) => _weatherService = weatherService;

    [DurationLogger]
    [HttpGet("located/weather/{city}")]
    public async Task<IActionResult> GetCurrentWeatherLocated([FromRoute] string city)
        => await GetCurrentWeather(city);

    [ServiceFilter<DurationLoggerFilter>]
    [HttpGet("injected/weather/{city}")]
    public async Task<IActionResult> GetCurrentWeatherInjected([FromRoute] string city)
        => await GetCurrentWeather(city);

    private async Task<IActionResult> GetCurrentWeather(string city)
    {
        var weather = await _weatherService.GetCurrentWeatherAsync(city);
        if (weather is null)
        {
            return NotFound();
        }

        return Ok(weather);
    }
}

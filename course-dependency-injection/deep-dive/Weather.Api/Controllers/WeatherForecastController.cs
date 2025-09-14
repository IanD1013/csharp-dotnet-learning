using Microsoft.AspNetCore.Mvc;
using Weather.Api.Logging;
using Weather.Api.Mappers;
using Weather.Api.Weather;

namespace Weather.Api.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly IEnumerable<IWeatherService> _weatherServices;
    private readonly ILoggerAdapter<WeatherForecastController> _logger;

    public WeatherForecastController(IEnumerable<IWeatherService> weatherServices, ILoggerAdapter<WeatherForecastController> logger)
    {
        _weatherServices = weatherServices;
        _logger = logger;
    }

    [HttpGet("weather/{city}")]
    public async Task<IActionResult> GetCurrentWeather([FromRoute] string city)
    {
        var first = _weatherServices.First();
        
        var weather = await first.GetCurrentWeatherAsync(city);
        if (weather == null)
        {
            return NotFound();
        }
        
        var weatherResponse = weather.MapToWeatherResponse();
        return Ok(weatherResponse);
    }
}

namespace DependencyInjection.Scrutor.Demos.Weather;

/// <summary>The service the whole chapter decorates.</summary>
public interface IWeatherService
{
    /// <summary>Gets the current weather for a city.</summary>
    Task<WeatherResponse?> GetCurrentWeatherAsync(string city);
}

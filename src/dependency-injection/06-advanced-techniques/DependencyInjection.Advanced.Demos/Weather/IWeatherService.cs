namespace DependencyInjection.Advanced.Demos.Weather;

public interface IWeatherService
{
    /// <summary>
    /// Identifies the resolved instance. Not part of the course's interface: it is what makes a
    /// lifetime visible in the console, which is the whole point of lessons 1, 4 and 5.
    /// </summary>
    Guid InstanceId { get; }

    Task<WeatherResponse?> GetCurrentWeatherAsync(string city);
}

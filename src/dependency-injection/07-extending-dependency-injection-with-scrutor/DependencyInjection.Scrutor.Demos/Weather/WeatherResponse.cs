namespace DependencyInjection.Scrutor.Demos.Weather;

/// <summary>The slice of the OpenWeather response the course's controller actually reads.</summary>
public sealed record WeatherResponse(string Name, MainWeather Main);

/// <summary>The "main" block of the OpenWeather response.</summary>
public sealed record MainWeather(double Temp);

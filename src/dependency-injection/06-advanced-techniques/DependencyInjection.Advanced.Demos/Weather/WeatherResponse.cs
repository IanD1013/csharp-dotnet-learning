namespace DependencyInjection.Advanced.Demos.Weather;

/// <summary>
/// The slice of OpenWeather's response the course actually reads.
/// </summary>
public sealed class WeatherResponse
{
    public required string Name { get; init; }

    public required Main Main { get; init; }
}

public sealed class Main
{
    public required double Temp { get; init; }
}

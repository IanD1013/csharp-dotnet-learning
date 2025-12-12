using _6_OptionsPatternValidation.Features;
using Microsoft.Extensions.Options;

namespace _6_OptionsPatternValidation;

public class Worker(
    ILogger<Worker> logger,
    IOptionsMonitor<FeatureOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("TODO API feature options: {options}", options.Get("TodoApi"));
                logger.LogInformation("Weather Station feature options: {options}", options.Get("WeatherStation"));
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
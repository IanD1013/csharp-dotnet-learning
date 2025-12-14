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
                logger.LogInformation("TODO API feature options: {Options}", GetNamedOptionsAsLogString("TodoApi"));
                logger.LogInformation("Weather Station feature options: {Options}", GetNamedOptionsAsLogString("WeatherStation"));
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private string GetNamedOptionsAsLogString(string name)
    {
        try
        {
            return options.Get(name)
                .ToString() ?? "";
        }
        catch (OptionsValidationException ex)
        {
            logger.LogError("{Name} ({Type}): {Errors}", ex.OptionsName, ex.OptionsType, ex.Message);
            
            return "";
        }
    }
}
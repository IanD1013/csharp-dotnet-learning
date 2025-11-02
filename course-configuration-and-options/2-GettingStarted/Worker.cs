namespace _2_GettingStarted;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            if (Random.Shared.Next(100) < 30 && logger.IsEnabled(LogLevel.Warning))
            {
                logger.LogWarning("Seeing this roughly 30% off the time...");           
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
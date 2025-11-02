namespace _2_GettingStarted;

public class Worker(
    ILogger<Worker> logger,
    IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            
            var delay = TimeSpan.Parse(config["Delay"] ?? "00:00:05");

            await Task.Delay(delay, stoppingToken);
        }
    }
}
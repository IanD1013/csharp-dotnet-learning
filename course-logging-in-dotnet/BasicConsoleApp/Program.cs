using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    services.Configure<TelemetryConfiguration>(x => x.TelemetryChannel = channel);
    services.AddLogging(x =>
    {
        x.AddApplicationInsights(configureTelemetryConfiguration: teleConfig =>
                teleConfig.ConnectionString = "InstrumentationKey=***REDACTED***",
            configureApplicationInsightsLoggerOptions: _ => { });
    });
    
    var serviceProvider = services.BuildServiceProvider();
    
    var logger =  serviceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Hello World!");
}
finally
{
    await channel.FlushAsync(CancellationToken.None);
    await Task.Delay(1000);
}
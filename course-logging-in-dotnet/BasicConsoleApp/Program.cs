using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.IncludeScopes = true;
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
    builder.SetMinimumLevel(LogLevel.Warning);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

if (logger.IsEnabled(LogLevel.Information))
{
    logger.LogInformation("Start");
}

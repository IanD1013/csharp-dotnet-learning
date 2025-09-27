using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss ";
        options.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });

    builder.ClearProviders();

    builder.AddSystemdConsole();
    
    builder.SetMinimumLevel(LogLevel.Debug);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);

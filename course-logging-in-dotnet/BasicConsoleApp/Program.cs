using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Hello World!");

// logger.Log(LogLevel.Information, 0,"Hello World!");
using System.Text.Json;
using BasicConsoleApp;
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
    builder.SetMinimumLevel(LogLevel.Information);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

using (logger.BeginTimedOperation("Handling task {TaskId}", 1))
{
    logger.LogInformation("Hello World!");
    await Task.Delay(10);
}

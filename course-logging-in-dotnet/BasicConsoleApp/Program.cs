using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

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
    
    // builder.SetMinimumLevel(LogLevel.Debug);
    builder.AddFilter(x => x >= LogLevel.Debug);
    builder.AddFilter((provider, category, logLevel) =>
    {
        return provider!.Contains("Console") &&
               category!.Contains("Microsoft.Extensions.Hosting.Internal.Host") &&
               logLevel >= LogLevel.Debug;
    });

    builder
        .AddFilter("System", LogLevel.Debug)
        .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Information);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "nick";
var age = 30;

try
{
    throw new Exception("Something went wrong.");
}
catch (Exception ex)
{
    logger.LogError(ex,"Failure during birthday of {Name} who is {Age}", name, age);
}


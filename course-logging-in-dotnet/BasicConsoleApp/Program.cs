using BasicConsoleApp;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(LogLevel.Information)
        // .SetMinimumLevel(LogLevel.None)
        .AddJsonConsole();
});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

var name = "nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
logger.LogInformation(LogEvents.UserBirthday,"{Name} just turned: {Age}", name, age);
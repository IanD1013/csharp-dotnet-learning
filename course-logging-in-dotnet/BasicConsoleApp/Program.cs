using BasicConsoleApp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddProvider(NullLoggerProvider.Instance);

});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

var name = "nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
logger.LogInformation(LogEvents.UserBirthday,"{Name} just turned: {Age}", name, age);
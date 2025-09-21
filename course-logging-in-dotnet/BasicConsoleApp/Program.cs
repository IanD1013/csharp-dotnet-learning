using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddJsonConsole();
        // .AddConsole();
});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

var name = "nick";
var age = 30;

logger.LogInformation($"Name: {name}, Age: {age}");

// {"EventId":0,
//  "LogLevel":"Information",
//  "Category":"Program",
//  "Message":"Name: nick, Age: 30",
//  "State":{"{OriginalFormat}":"Name: nick, Age: 30"}}
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

logger.LogInformation("{Name} just turned: {Age}", name, age);
// {"EventId":0,"LogLevel":"Information","Category":"Program","Message":"nick just turned: 30","State":{"Name":"nick","Age":30,"{OriginalFormat}":"{Name} just turned: {Age}"}}

using BasicConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(x =>
    {
        x.AddJsonConsole();       
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Hello World!");

host.Run();

/*
return;
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddProvider(NullLoggerProvider.Instance);

});

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

var name = "nick";
var age = 30;

logger.LogDebug("{Name} just turned: {Age}", name, age);
logger.LogInformation(LogEvents.UserBirthday,"{Name} just turned: {Age}", name, age);
*/
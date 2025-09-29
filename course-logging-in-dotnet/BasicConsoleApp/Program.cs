using BasicConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

IServiceCollection services = new ServiceCollection();

services.AddLogging(x =>
{
    x.AddProvider(new IanLoggerProvider()); 
});

var serviceProvider = services.BuildServiceProvider();

var logger =  serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Hello World!");


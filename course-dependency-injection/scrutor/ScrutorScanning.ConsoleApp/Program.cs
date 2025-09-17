using Microsoft.Extensions.DependencyInjection;
using ScrutorScanning.ConsoleApp.Attributes;
using ScrutorScanning.ConsoleApp.Services;

var services = new ServiceCollection();

services.Scan(selector =>
{
    selector
        .FromAssemblyOf<Program>()
        .AddClasses()
        .UsingAttributes();
});

PrintRegisteredServices(services);

var serviceProvider = services.BuildServiceProvider();

void PrintRegisteredServices(IServiceCollection serviceCollection)
{
    foreach (var service in serviceCollection)
    {
        Console.WriteLine($"{service.ServiceType.Name} -> {service.ImplementationType?.Name} as {service.Lifetime.ToString()}");
    }
}
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;

namespace ScrutorScanning.ConsoleApp.Services;

// [Singleton]
[ServiceDescriptor(typeof(ExampleAService), ServiceLifetime.Singleton)]
[ServiceDescriptor(typeof(IExampleAService), ServiceLifetime.Singleton)]
public class ExampleAService : IExampleAService
{

}

public interface IExampleAService
{

}

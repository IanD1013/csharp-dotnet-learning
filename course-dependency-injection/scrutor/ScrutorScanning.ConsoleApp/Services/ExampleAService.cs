using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using ScrutorScanning.ConsoleApp.Attributes;

namespace ScrutorScanning.ConsoleApp.Services;

[Singleton]
public class ExampleAService : IExampleAService
{

}

public interface IExampleAService
{

}

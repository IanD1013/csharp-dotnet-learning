using DependencyInjection.Advanced.Demos.Output;
using DependencyInjection.Advanced.Demos.SourceGenerated;

namespace DependencyInjection.Advanced.Demos;

/// <summary>
/// Lesson 7: The future of dependency injection.
/// </summary>
public static class SourceGeneratedDemo
{
    public static void Run()
    {
        var serviceProvider = new MyServiceProvider();

        var consoleWriter = serviceProvider.GetService<IConsoleWriter>();
        consoleWriter.WriteLine("Hi From Source Generated DI");

        Console.WriteLine($"  resolved {consoleWriter.GetType().Name} with no reflection involved");
        Console.WriteLine("  -> a missing registration would have been a compiler error, not a runtime one");
    }
}

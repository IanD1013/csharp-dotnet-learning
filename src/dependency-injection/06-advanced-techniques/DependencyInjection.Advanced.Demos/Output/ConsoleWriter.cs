namespace DependencyInjection.Advanced.Demos.Output;

public sealed class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string text) => Console.WriteLine(text);
}

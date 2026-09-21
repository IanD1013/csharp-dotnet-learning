namespace DependencyInjection.CustomFramework.Demos.Services;

/// <summary>
/// The course's reason for wrapping Console.WriteLine: a static call cannot be mocked,
/// an interface can. Every service below writes through this one.
/// </summary>
public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}

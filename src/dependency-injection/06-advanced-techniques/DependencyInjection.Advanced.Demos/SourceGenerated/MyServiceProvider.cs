using DependencyInjection.Advanced.Demos.Output;
using Jab;

namespace DependencyInjection.Advanced.Demos.SourceGenerated;

/// <summary>
/// Lesson 7. Jab generates the other half of this partial class at compile time, so resolving
/// <see cref="IConsoleWriter"/> is a plain constructor call with no reflection behind it.
/// </summary>
[ServiceProvider]
[Transient(typeof(IConsoleWriter), typeof(ConsoleWriter))]
public partial class MyServiceProvider
{
}

namespace DependencyInjection.Scrutor.Demos.Scanning;

/// <summary>Lesson 4: a service whose name matches its interface, so AsMatchingInterface finds it.</summary>
public sealed class ExampleAService : IExampleAService;

/// <summary>The interface <see cref="ExampleAService"/> is matched against.</summary>
public interface IExampleAService;

/// <summary>Lesson 4: a second matching pair, so the scan has more than one thing to find.</summary>
public sealed class ExampleBService : IExampleBService;

/// <summary>The interface <see cref="ExampleBService"/> is matched against.</summary>
public interface IExampleBService;

/// <summary>
/// Lesson 4: implements two interfaces and matches neither by name. AsMatchingInterface skips it
/// entirely; AsImplementedInterfaces and AsSelfWithInterfaces are what pick it up.
/// </summary>
public sealed class ExampleABService : IExampleAService, IExampleBService;

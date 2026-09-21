using DependencyInjection.Scrutor.Demos.AttributeMarking;

namespace DependencyInjection.Scrutor.Demos.Strategies;

/// <summary>Lesson 9: matches one filter only, so no strategy ever applies to it.</summary>
[Singleton]
public sealed class ExampleAService : IExampleAService;

/// <summary>The functional interface of <see cref="ExampleAService"/>.</summary>
public interface IExampleAService;

/// <summary>
/// Lesson 9: carries both markers, so the singleton pass and the transient pass both claim it.
/// That collision is what Append, Skip, Replace and Throw each answer differently.
/// </summary>
[Singleton]
[Transient]
public sealed class ExampleBService : IExampleBService;

/// <summary>The functional interface of <see cref="ExampleBService"/>.</summary>
public interface IExampleBService;

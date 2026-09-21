using DependencyInjection.Scrutor.Demos.AttributeMarking;

namespace DependencyInjection.Scrutor.Demos.NamespaceFiltering.Services;

/// <summary>
/// Lesson 7: still carries [Singleton] from the previous lesson. A namespace scan does not read
/// attributes, so the lifetime in the Scan call wins and this marker is simply ignored.
/// </summary>
[Singleton]
public sealed class ExampleAService : IExampleAService;

/// <summary>The functional interface of <see cref="ExampleAService"/>.</summary>
public interface IExampleAService;

/// <summary>Lesson 7: no attribute at all, and the namespace scan registers it anyway.</summary>
public sealed class ExampleBService : IExampleBService;

/// <summary>The functional interface of <see cref="ExampleBService"/>.</summary>
public interface IExampleBService;

/// <summary>Lesson 7: a third service, so the uniform lifetime is visible across the namespace.</summary>
public sealed class ExampleCService : IExampleCService;

/// <summary>The functional interface of <see cref="ExampleCService"/>.</summary>
public interface IExampleCService;

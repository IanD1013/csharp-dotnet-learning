namespace DependencyInjection.Scrutor.Demos.AttributeMarking;

/// <summary>Lesson 6: the lifetime is visible on the class, with no marker interface in sight.</summary>
[Singleton]
public sealed class ExampleAService : IExampleAService;

/// <summary>The functional interface of <see cref="ExampleAService"/>.</summary>
public interface IExampleAService;

/// <summary>Lesson 6: transient, declared by attribute.</summary>
[Transient]
public sealed class ExampleBService : IExampleBService;

/// <summary>The functional interface of <see cref="ExampleBService"/>.</summary>
public interface IExampleBService;

/// <summary>Lesson 6: scoped, declared by attribute.</summary>
[Scoped]
public sealed class ExampleCService : IExampleCService;

/// <summary>The functional interface of <see cref="ExampleCService"/>.</summary>
public interface IExampleCService;

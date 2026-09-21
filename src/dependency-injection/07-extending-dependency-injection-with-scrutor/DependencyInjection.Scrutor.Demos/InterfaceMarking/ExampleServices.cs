namespace DependencyInjection.Scrutor.Demos.InterfaceMarking;

/// <summary>Lesson 5: marked as a singleton by implementing <see cref="ISingletonService"/>.</summary>
public sealed class ExampleAService : IExampleAService, ISingletonService;

/// <summary>The functional interface of <see cref="ExampleAService"/>.</summary>
public interface IExampleAService;

/// <summary>Lesson 5: marked as transient by implementing <see cref="ITransientService"/>.</summary>
public sealed class ExampleBService : IExampleBService, ITransientService;

/// <summary>The functional interface of <see cref="ExampleBService"/>.</summary>
public interface IExampleBService;

/// <summary>Lesson 5: marked as scoped by implementing <see cref="IScopedService"/>.</summary>
public sealed class ExampleCService : IExampleCService, IScopedService;

/// <summary>The functional interface of <see cref="ExampleCService"/>.</summary>
public interface IExampleCService;

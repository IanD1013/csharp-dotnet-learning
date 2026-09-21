namespace DependencyInjection.Scrutor.Demos.InterfaceMarking;

/// <summary>Lesson 5: an empty marker interface that means "register me as a singleton".</summary>
public interface ISingletonService;

/// <summary>Lesson 5: an empty marker interface that means "register me as transient".</summary>
public interface ITransientService;

/// <summary>Lesson 5: an empty marker interface that means "register me as scoped".</summary>
public interface IScopedService;

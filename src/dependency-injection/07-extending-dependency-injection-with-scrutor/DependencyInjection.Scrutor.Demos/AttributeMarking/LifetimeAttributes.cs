namespace DependencyInjection.Scrutor.Demos.AttributeMarking;

/// <summary>Lesson 6: marks a class for singleton registration.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SingletonAttribute : Attribute;

/// <summary>Lesson 6: marks a class for transient registration.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class TransientAttribute : Attribute;

/// <summary>Lesson 6: marks a class for scoped registration.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ScopedAttribute : Attribute;

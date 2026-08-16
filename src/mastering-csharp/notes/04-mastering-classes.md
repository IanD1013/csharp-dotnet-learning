# Mastering Classes

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 4
> 9 lessons · ~10:42
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/) | 0:30 | [↓](#1-overview) |
| 2 | [Object Initialization](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/) | 0:24 | [↓](#2-object-initialization) |
| 3 | [Analyzing Object Construction Order](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/) | 1:34 | [↓](#3-analyzing-object-construction-order) |
| 4 | [Virtual Calls in the Base class Constructor](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/) | 1:53 | [↓](#4-virtual-calls-in-the-base-class-constructor) |
| 5 | [Using Analyzers to Avoid Making Virtual Calls in Base Constructors](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/) | 0:36 | [↓](#5-using-analyzers-to-avoid-making-virtual-calls-in-base-constructors) |
| 6 | [A Custom Factory Method and Error Handling](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/) | 0:28 | [↓](#6-a-custom-factory-method-and-error-handling) |
| 7 | [Exploring a Custom Factory Method Behavior at Runtime](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/) | 1:45 | [↓](#7-exploring-a-custom-factory-method-behavior-at-runtime) |
| 8 | [Benchmarking the new() Constraint](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/) | 1:46 | [↓](#8-benchmarking-the-new-constraint) |
| 9 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/) | 1:46 | [↓](#9-conclusion) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/) · 0:30

This lesson introduces advanced topics in C# class management, focusing on the nuances of object construction.
It explores the specific execution order of field initializers and constructors in inheritance hierarchies, the risks associated with virtual method dispatch during object creation, and the mechanics of the `new()` generic constraint.
Additionally, it examines performance considerations for object instantiation, particularly when targeting older frameworks.

### Key concepts

- Object initialization order in inheritance hierarchies.
- Hazards of virtual dispatch on partially constructed objects.
- The `new()` generic constraint and its compilation behavior.
- Exception handling differences between direct construction and generic constraints.
- Performance optimization using custom activators and expression trees.

### Lesson notes

#### Object Initialization Order

Understanding the precise order of execution during object construction is critical for avoiding bugs in complex hierarchies.
In C#, when a derived class is instantiated, the execution follows a specific sequence: derived field initializers run first, followed by base field initializers, then the base constructor body, and finally the derived constructor body.
This order ensures that fields are initialized before constructor bodies execute, but it also means that a base constructor runs before the derived constructor body has had a chance to execute its logic.

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init(
        [CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=9)

#### Virtual Calls in Constructors

Calling virtual members from a constructor is a dangerous pattern in C#.
Because of the initialization order, a virtual call in a base constructor will dispatch to the derived class's override.
However, since the base constructor executes before the derived constructor body, the derived implementation of the virtual method may attempt to access fields or state that have not yet been fully initialized, leading to unpredictable behavior or `NullReferenceException`.

```csharp
using System.Runtime.CompilerServices;

Base b = new Derived();
b.ReportState();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    public virtual void ReportState()
    {
        Console.WriteLine($"Base.ReportState, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();
    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }

    public override void ReportState()
    {
        Console.WriteLine($"Derived.ReportState, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=15)

#### The new() Constraint and Generic Instantiation

The `where T : new()` constraint allows for the instantiation of generic types.
Historically, on .NET Framework, this constraint was implemented using `Activator.CreateInstance`, which incurred a performance penalty and wrapped any exceptions thrown by the constructor in a `TargetInvocationException`.
In modern .NET, the JIT compiler can often optimize this into a direct constructor call, significantly improving performance and simplifying exception handling.

```csharp
using System.Linq.Expressions;

try
{
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass() 
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}

static class Factory
{
    public static T Create<T>() where T : new() => new();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=19)

#### Custom Activators for Performance

When targeting older frameworks or requiring specialized instantiation logic, a custom activator using compiled expression trees can be used.
By caching a compiled `Func<T>`, developers can achieve performance near that of a direct constructor call, bypassing the overhead of `Activator.CreateInstance` while maintaining clean exception propagation.

```csharp
public static class CachedFactory<T> where T : new()
{
    public static readonly Func<T> Create =
        Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
}

public static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => CachedFactory<T>.Create();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=24)

---

## 2. Object Initialization

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/) · 0:24

### Summary

This lesson examines the execution order of field initializers and constructors within a C# inheritance hierarchy.
By observing a base and derived class that both utilize field-like initialization, the lesson clarifies the sequence in which the runtime prepares an object, ensuring fields are properly set up before constructor logic executes.

### Key concepts

- **Field-like initialization**: Initializing fields at their declaration point rather than inside a constructor.
- **Initialization order**: The specific sequence of field and constructor execution in inheritance.
- **Base vs. Derived execution**: How the runtime traverses the class hierarchy during instantiation.
- **Tracing execution**: Using `[CallerMemberName]` to monitor the initialization lifecycle.

### Lesson notes

In C#, when a class inherits from another, the order in which fields are initialized and constructors are executed is strictly defined.
This lesson presents a scenario involving a `Base` class and a `Derived` class to determine this sequence.

Both classes utilize field-like initialization, where a field is assigned a value at its declaration point.
To track the execution, a static helper method `Init` is used, which leverages the `CallerMemberName` attribute to identify which field is being processed.

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init(
        [CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/?t=10)

When `new Derived()` is called, the runtime must determine the sequence for field initializers and constructors across the hierarchy.
The core technical question is whether initialization proceeds from base to derived, derived to base, or a combination of both.
This sequence is essential for ensuring that fields are initialized before they are accessed within any constructor in the chain.
The provided code serves as a test harness to observe this behavior through console output.

---

## 3. Analyzing Object Construction Order

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/) · 1:34

### Summary

This lesson analyzes the precise execution order of field initializers and constructors when a derived class is instantiated in C#.
It demonstrates that field initialization for the derived class occurs before the base class's field initialization and constructor body.
By examining the lowered C# code, the lesson explains how the compiler injects field initializers at the beginning of the constructor body, preceding the call to the base constructor, to ensure that object invariants are established before any logic is executed.

### Key concepts

- **Execution Sequence**: The order is (1) Derived field initializers, (2) Base field initializers, (3) Base constructor body, and (4) Derived constructor body.
- **Compiler Lowering**: Field initializers are not just syntactical sugar; the compiler explicitly moves this code to the top of the constructor method.
- **Base Call Timing**: The derived class's field initializers run before the `base()` constructor call is executed.
- **Object Invariants**: Early field initialization ensures that fields are populated and observable in a valid state throughout the construction process.

### Lesson notes

The lesson begins by examining a standard inheritance hierarchy where a `Derived` class inherits from a `Base` class.
Both classes contain field initializers and constructors that log their execution to the console.

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/?t=10)

While it might be intuitive to assume the base class is initialized first, the actual execution order is different.
When creating an instance of the `Derived` class, the sequence is:
1. Derived field initialization.
2. Base field initialization.
3. Base constructor body execution.
4. Derived constructor body execution.

This behavior is visible when looking at the "lowered" C# code (the code generated by the compiler).
The compiler takes the field initializers and injects them at the very top of the constructor body, even before the call to the base constructor (`base..ctor()`). 

```csharp
internal class Derived : Base
{
    [Nullable(1)]
    private readonly string _derivedField;

    public Derived()
    {
        // Field initializer is injected before the base constructor call
        this._derivedField = Base.Init("_derivedField");
        base..ctor();
        Console.WriteLine();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/?t=40)

This order remains consistent regardless of whether the fields are marked as `readonly`.
The primary purpose of this early initialization is to establish object invariants.
By initializing fields before any constructor logic runs, C# ensures that fields are in a valid, observable state as soon as the object begins its lifecycle.
This becomes particularly important when considering more complex scenarios, such as virtual method calls within a constructor.

---

## 4. Virtual Calls in the Base class Constructor

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/) · 1:53

### Summary

Calling virtual methods from a base class constructor is a dangerous practice in C# due to the specific order of object initialization.
While field initializers in a derived class are executed before the base class constructor runs, the derived class's constructor body is executed only after the base class constructor has completed.
If a base constructor invokes a virtual method that is overridden in a derived class, that override will execute before the derived constructor body has run.
If the overridden method relies on state initialized within the derived constructor body, the application will likely encounter a NullReferenceException or operate on a partially constructed instance.

### Key concepts

- **Initialization Order**: Field initializers run from most-derived to base, followed by constructor bodies running from base to most-derived.
- **Virtual Dispatch in Constructors**: C# dispatches virtual calls to the most-derived implementation even if that type's constructor has not yet executed.
- **Partially Constructed Objects**: The state where a base class is fully initialized but the derived class's constructor logic has not yet run.
- **Fragility of Field vs. Constructor Initialization**: Code that appears safe when using field initializers can break if refactored to use constructor-body initialization.

### Lesson notes

In C#, the order of execution during object instantiation is critical when dealing with inheritance.
When a new instance of a derived class is created, the runtime follows a specific sequence: it first executes the field initializers for the derived class, then the field initializers for the base class, and finally the constructor bodies starting from the base class down to the derived class.

This order creates a significant hazard when a base class constructor calls a virtual method.
Because C# uses virtual dispatch, the call is routed to the implementation in the derived class, even though the derived class's constructor body has not yet executed.

#### Scenario 1: Field-Like Initialization

In the following example, the derived class uses field-like initialization.
Because field initializers run before any constructor bodies, the `_derivedField` is already populated when the base constructor calls `ReportState()`.

```csharp
class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
        ReportState();
    }

    public virtual void ReportState()
    {
        Console.WriteLine($"Base.ReportState, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();
    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }

    public override void ReportState()
    {
        Console.WriteLine($"Derived.ReportState, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/?t=10)

In this case, the code runs without error because `_derivedField` is initialized during the field-initialization phase, which occurs before the `Base` constructor body is entered.
The output shows the derived field length is 25 because the string "initialized:_derivedField" is 25 characters long.

#### Scenario 2: Constructor-Body Initialization

The danger becomes apparent if the initialization logic is moved from the field level into the constructor body.
This is a common refactoring, but it fundamentally changes the execution order.

```csharp
class Derived : Base
{
    private readonly string _derivedField;
    public Derived()
    {
        _derivedField = Init();
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }

    public override void ReportState()
    {
        Console.WriteLine($"Derived.ReportState, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/?t=40)

When this version of the code runs, it results in a `NullReferenceException`.
The execution flow follows these steps:
1. The `Derived` constructor is called.
2. Field-like initializers for the `Derived` class are executed (none in this case).
3. The `Base` class constructor is called.
4. The `Base` field initializers are executed.
5. The `Base` constructor body begins execution.
6. The `Base` constructor calls the virtual method `ReportState()`.
7. Virtual dispatch sends the call to `Derived.ReportState()`.
8. `Derived.ReportState()` attempts to access `_derivedField.Length`.
9. Since the `Derived` constructor body (where `_derivedField` is assigned) has not yet run, `_derivedField` is still `null`, causing the crash.

#### Conclusion

Calling virtual methods from a constructor is extremely fragile.
It forces the derived class to handle calls while it is in a partially constructed state, where its invariants may not yet be established.
To maintain code stability, virtual calls should be avoided within constructors.
Developers should utilize static analysis tools and IDE analyzers to detect and prevent this pattern in their codebases.

---

## 5. Using Analyzers to Avoid Making Virtual Calls in Base Constructors

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/) · 0:36

Analyzers provide a mechanism to catch the "virtual call in constructor" hazard automatically.
The simplest way to enable these rules is by adding the relevant configuration to an `.editorconfig` file.
Once enabled, the analyzer provides immediate feedback through warnings in the IDE and during the compilation process.

### Key concepts

- Enabling analyzers via `.editorconfig`.
- IDE and build-time warnings for virtual calls in constructors.
- Breaking the build using "Treat Warnings as Errors" to prevent unsafe check-ins.
- Suppressing warnings with descriptive intent when the behavior is intentional.

### Lesson notes

If a project is configured to treat warnings as errors, any instance of a virtual call in a constructor will prevent a successful build, ensuring that such code cannot be checked into source control.
The following example demonstrates the scenario that triggers these analyzer warnings, where the `Base` constructor invokes the virtual `ReportState` method:

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

public class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
        ReportState();
    }

    protected virtual void ReportState()
    {
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

public class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived() => Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");

    protected override void ReportState()
    {
        Console.WriteLine($"Derived override, _derivedField.Length = {_derivedField.Length}"  );
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/?t=10)

In rare cases where calling a virtual method from a base constructor is intentional and the risks are understood, the warning can be suppressed.
However, suppression should always be accompanied by a descriptive message explaining the developer's intent.
Even with suppression, developers must remain extremely cautious to ensure that the overridden methods in derived classes do not attempt to access state that has not yet been initialized by the derived constructor.

---

## 6. A Custom Factory Method and Error Handling

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/) · 0:28

### Summary

This lesson explores the nuances of object instantiation using the generic `new()` constraint versus custom factory methods.
It specifically examines how exceptions thrown within a constructor are handled when an object is created via a generic factory, highlighting potential pitfalls where exceptions may be wrapped in a `TargetInvocationException`.
The lesson also introduces a high-performance alternative using expression-compiled factories, which provide direct exception propagation and better execution speed by caching compiled delegates.

### Key concepts

- Generic `new()` constraint behavior
- Exception wrapping in `TargetInvocationException` during reflection-based instantiation
- Performance overhead of the `new()` constraint on older frameworks
- Custom factory implementation using `Expression.Lambda` and `Expression.New`
- Caching compiled delegates in static generic classes for efficiency

### Lesson notes

When implementing generic factory methods, the `new()` constraint is the standard way to ensure a type has a parameterless constructor.
However, this approach has implications for error handling and performance.

Consider a scenario where a class constructor throws an exception:

```csharp
try
{
    // MyClass ctor might fail with InvalidOperationException,
    // so handling that case explicitly.
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass() 
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}

static class Factory
{
    public static T Create<T>() where T : new() => new();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/?t=10)

In the example above, `MyClass` throws an `InvalidOperationException`.
While the `try-catch` block specifically looks for this exception, the `new()` constraint can behave unexpectedly.
In some environments, specifically when the constraint is implemented via reflection (such as on .NET Framework), the `InvalidOperationException` may be wrapped inside a `TargetInvocationException`.
This would cause the specific `catch (InvalidOperationException)` block to be bypassed, with the exception instead being caught by the general `Exception` block.

To provide more predictable exception propagation and improved performance, a custom factory can be implemented using expression trees:

```csharp
using System.Linq.Expressions;

static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => Cache<T>.Factory();

    private static class Cache<T> where T : new()
    {
        public static readonly Func<T> Factory =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/?t=10)

The `CustomActivator` uses `Expression.Lambda` to compile a factory delegate for the type `T`.
This approach offers two primary advantages:

1. **Exception Handling**: Exceptions thrown by the constructor surface directly to the caller, rather than being wrapped in a `TargetInvocationException`.
2. **Performance**: This method is significantly faster than the `new()` constraint on platforms where the constraint relies on `Activator.CreateInstance`. By caching the compiled delegate in a static generic `Cache<T>` class, the overhead of compilation is only incurred once per type.

---

## 7. Exploring a Custom Factory Method Behavior at Runtime

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/) · 1:45

### Summary

This lesson explores the runtime behavior of the C# new() generic constraint, revealing how its underlying implementation can lead to a 'leaky abstraction' during exception handling.
Because the compiler implements the new() constraint using reflection-based Activator.CreateInstance, any exception thrown by a constructor is wrapped in a TargetInvocationException, making error handling difficult and less descriptive.
To resolve this, the lesson demonstrates how to implement a CustomActivator using Expression Trees to compile a direct delegate for object instantiation, which preserves the original exception type and improves performance on legacy frameworks.

### Key concepts

- **Leaky Abstraction**: The `new()` constraint implementation details (reflection) surface to the developer through unexpected exception types.
- **TargetInvocationException**: The wrapper exception thrown by reflection when the invoked member (the constructor) throws an exception.
- **Activator.CreateInstance**: The underlying mechanism used by the C# compiler for the `new()` constraint.
- **Expression Trees**: A technique for lightweight code generation at runtime to create efficient delegates.
- **Custom Activator**: A pattern using cached, compiled expressions to replace the default `new()` behavior for better performance and error handling.

### Lesson notes

When a class constructor throws an exception, we typically expect to catch that specific exception type.
However, when an instance is created through a factory method using the `new()` generic constraint, the behavior changes unexpectedly at runtime.
Instead of catching the specific exception, the application often hits a generic fallback handler because the original exception is wrapped.

```csharp
try
{
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass()
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}

static class Factory
{
    public static T Create<T>() where T : new() => new T();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/?t=10)

In the example above, even though `MyClass` throws an `InvalidOperationException`, the specific `catch` block is bypassed.
The error message received is often undescriptive, stating only that an "exception has been thrown by the target of the invocation."
This makes debugging significantly harder in production environments where logs may only capture the top-level exception.

This behavior occurs because the `new()` constraint is a leaky abstraction.
Examining the compiled code with an IL viewer reveals that the `new()` constraint is compiled to use `Activator.CreateInstance`.
This implementation detail dates back to C# 2.0.
When an operation is called via reflection and fails, the runtime wraps the actual exception in a `TargetInvocationException`.
This behavior persists in modern .NET to avoid breaking changes for codebases that rely on catching the wrapper exception.

To solve this for application code, we can implement a custom activator using Expression Trees (introduced in C# 3.0).
This allows for lightweight code generation where we compile an expression into a delegate.
This delegate can then be used instead of the default `new()` behavior.

```csharp
static class Factory
{
    public static T Create<T>() where T : new() => CustomActivator.CreateInstance<T>();
}

#region CustomActivator
// CustomActivator: cached, expression-compiled factory per T.
// Two reasons it differs from `new()` under the constraint:
//   1. Exception handling — a throwing constructor surfaces directly here,
//      whereas going through the new() constraint wraps the exception in TargetInvocationException.
//   2. Performance — significantly faster than `new()` on .NET Framework,
//      where the constraint goes through Activator and pays per-call cost.
static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => Cache<T>.Factory();

    private static class Cache<T> where T : new()
    {
        public static readonly Func<T> Factory =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/?t=85)

By updating the `Factory` to use the `CustomActivator`, the application no longer wraps exceptions.
When the constructor throws an `InvalidOperationException`, it is surfaced directly to the caller, allowing the specific `catch` block to handle the error properly.

```csharp
try
{
    // MyClass ctor might fail with InvalidOperationException,
    // so handling that case explicitly.
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass()
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/?t=100)

---

## 8. Benchmarking the new() Constraint

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/) · 1:46

### Summary

This lesson evaluates the performance of the new() constraint compared to other object instantiation methods across different .NET runtimes.
While .NET 10 shows nearly zero overhead for the new() constraint, .NET Framework 4.8 reveals a 20x performance penalty compared to direct constructor calls.
The lesson demonstrates that manual IL generation can provide a 10x performance improvement over the new() constraint on legacy frameworks, explaining why high-performance libraries like Entity Framework and Newtonsoft.Json utilize custom IL-based factories.

### Key concepts

- Performance disparity of the new() constraint between .NET 10 and .NET Framework 4.8.
- The relationship between the new() constraint and Activator.CreateInstance.
- Optimization of language features in modern .NET runtimes.
- Performance benefits of manual IL generation for object creation in legacy environments.
- Implementation of custom factories using Expression Trees and DynamicMethod.

### Lesson notes

The performance of the new() constraint presents a significant challenge, particularly when targeting older frameworks.
While modern runtimes like .NET 10 have optimized these language features to have almost zero overhead, legacy frameworks still incur a heavy cost.

To measure this, a benchmark compares several instantiation strategies: direct constructor calls (the baseline), delegates, Activator.CreateInstance (which the compiler generates for the new() constraint), custom activators using expression trees, and manual IL generation.

```csharp
[MemoryDiagnoser]
[Config(typeof(BenchmarkConfig))]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class FactoryBenchmarks
{
    private static readonly Func<Demo> _delegate = () => new Demo();

    [Benchmark(Baseline = true)]
    public Demo DirectConstructor() => new();

    [Benchmark]
    public Demo CreateViaDelegate() => _delegate();

    [Benchmark]
    public Demo ActivatorCreate() => Activator.CreateInstance<Demo>()!;

    [Benchmark]
    public Demo CustomActivatorCreate() => CustomActivator.CreateInstance<Demo>();

    [Benchmark]
    public Demo CreateViaEmittedIL() => EmittedFactory<Demo>.Create();
}

public sealed class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddJob(Job.ShortRun.WithRuntime(ClrRuntime.Net48).WithId("net48"));
        AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core10_0).WithId("net10.0"));
    }
}

public sealed class Demo
{
    public int Value { get; } = 42;
}

public static class CachedFactory<T> where T : new()
{
    public static readonly Func<T> Create =
        System.Linq.Expressions.Expression.Lambda<Func<T>>(System.Linq.Expressions.Expression.New(typeof(T))).Compile();
}

public static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => CachedFactory<T>.Create();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/?t=25)

In .NET 10, all instantiation methods perform comparably, indicating that the overhead of the new() constraint has been virtually eliminated.
In this context, the primary motivation for using a custom activator would be to address exception handling behavior, as the new() constraint continues to wrap exceptions in a TargetInvocationException for backward compatibility.

In contrast, .NET Framework 4.8 shows a 20x performance difference between direct instantiation and the new() constraint.
While expression-based custom activators offer some improvement, they remain slower than regular delegates due to security constraints and legacy overhead.

The most efficient approach for legacy frameworks is manual IL generation.
Although complex and rarely used in standard application code, an IL-based implementation provides a 10x performance improvement over the new() constraint.
This level of optimization is a game-changer for performance-critical libraries such as Entity Framework or Newtonsoft.Json, which frequently create large numbers of objects.

```csharp
public static class EmittedFactory<T> where T : new()
{
    public static readonly Func<T> Create = DynamicModuleLambdaCompiler.GenerateFactory<T>();
}

public static class DynamicModuleLambdaCompiler
{
    public static Func<T> GenerateFactory<T>() where T : new()
    {
        System.Linq.Expressions.Expression<Func<T>> expr = () => new T();
        System.Linq.Expressions.NewExpression newExpr = (System.Linq.Expressions.NewExpression)expr.Body;

        var method = new System.Reflection.Emit.DynamicMethod(
            name: "lambda",
            returnType: newExpr.Type,
            parameterTypes: Type.EmptyTypes,
            m: typeof(DynamicModuleLambdaCompiler).Module,
            skipVisibility: true);

        System.Reflection.Emit.ILGenerator ilGen = method.GetILGenerator();
        if (newExpr.Constructor != null)
        {
            ilGen.Emit(System.Reflection.Emit.OpCodes.Newobj, newExpr.Constructor);
        }
        else
        {
            System.Reflection.Emit.LocalBuilder temp = ilGen.DeclareLocal(newExpr.Type);
            ilGen.Emit(System.Reflection.Emit.OpCodes.Ldloca, temp);
            ilGen.Emit(System.Reflection.Emit.OpCodes.Initobj, newExpr.Type);
            ilGen.Emit(System.Reflection.Emit.OpCodes.Ldloc, temp);
        }

        ilGen.Emit(System.Reflection.Emit.OpCodes.Ret);

        return (Func<T>)method.CreateDelegate(typeof(Func<T>));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/?t=79)

---

## 9. Conclusion

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/) · 1:46

### Summary

This lesson concludes the module on mastering classes by recapping the nuances of object construction and the internal mechanics of the new() constraint.
It emphasizes the specific execution order where field initializers run before base constructors, the hazards of virtual method calls within constructors that can expose partially initialized state, and the performance and exception-handling trade-offs of the new() generic constraint.
The lesson highlights that while .NET Core has optimized the new() constraint, .NET Framework applications may require custom IL-based or expression-compiled factories to avoid significant performance penalties and exception wrapping.

### Key concepts

* **Construction Order**: Field initializers execute before the base class constructor, which in turn executes before the derived class constructor body.
* **Virtual Call Hazard**: Calling virtual methods in a constructor dispatches to the most derived type, potentially accessing fields before the derived constructor has initialized them.
* **Static Analysis**: Rule CA2214 should be enabled to prevent virtual member calls in constructors.
* **new() Constraint Internals**: The compiler implements the `new()` constraint using `Activator.CreateInstance`.
* **Exception Handling**: Exceptions thrown during object creation via the `new()` constraint are wrapped in a `TargetInvocationException`.
* **Performance**: The `new()` constraint is highly optimized in .NET Core but can be up to 22x slower than direct construction in .NET Framework.
* **Custom Factories**: Expression-compiled or IL-emitted factories can resolve performance and exception-wrapping issues.

### Lesson notes

#### Construction Semantics and Field Initializers

In C#, the construction of a derived class follows a specific sequence.
Field initializers are executed first, even before the base class constructor is called.
This behavior is independent of whether the field is redundant or not.
Once field initializers have run, the base class constructor executes, followed finally by the body of the derived class constructor.

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init(
        [CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=5)

#### The Virtual Call Hazard

A significant risk in class design is calling a virtual method from a base constructor.
Because C# dispatches virtual calls to the most derived type, the derived implementation of the method will execute before the derived constructor's body has run.
If the derived method relies on fields initialized in its constructor, it will observe the object in a partial, potentially invalid state.
To mitigate this, developers should enable static analysis tool CA2214 to avoid virtual calls in constructors.

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

public class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
        ReportState();
    }

    protected virtual void ReportState()
    {
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

public class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived() => Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");

    protected override void ReportState()
    {
        // This may access _derivedField before the Derived constructor body runs
        Console.WriteLine($"Derived override, _derivedField.Length = {_derivedField.Length}"  );
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=34)

#### The new() Constraint and Performance

Under the hood, the compiler translates the `new()` generic constraint into a call to `Activator.CreateInstance`.
This has two primary consequences.
First, for historical reasons, any exception thrown during construction is wrapped in a `TargetInvocationException`.
Second, while performance in .NET Core is excellent, in .NET Framework the overhead of `Activator` can make construction up to 22 times slower than direct instantiation.

To address these issues, a `CustomActivator` using expression trees can be used to cache a compiled factory, which allows exceptions to surface directly and improves performance on older runtimes.

```csharp
using System.Linq.Expressions;

static class Factory
{
    public static T Create<T>() where T : new() => new();
}

// CustomActivator: cached, expression-compiled factory per T.
static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => Cache<T>.Factory();

    private static class Cache<T> where T : new()
    {
        public static readonly Func<T> Factory =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=65)

For scenarios requiring maximum performance, especially in legacy environments, IL-based co-generation using `DynamicMethod` can be employed to generate a factory that mimics the performance of a direct constructor call.

```csharp
using System.Reflection.Emit;

public static class DynamicModuleLambdaCompiler
{
    public static Func<T> GenerateFactory<T>() where T : new()
    {
        Expression<Func<T>> expr = () => new T();
        NewExpression newExpr = (NewExpression)expr.Body;

        var method = new DynamicMethod(
            name: "lambda",
            returnType: newExpr.Type,
            parameterTypes: Type.EmptyTypes,
            m: typeof(DynamicModuleLambdaCompiler).Module,
            skipVisibility: true);

        ILGenerator ilGen = method.GetILGenerator();
        if (newExpr.Constructor != null)
        {
            ilGen.Emit(OpCodes.Newobj, newExpr.Constructor);
        }
        else
        {
            LocalBuilder temp = ilGen.DeclareLocal(newExpr.Type);
            ilGen.Emit(OpCodes.Ldloca, temp);
            ilGen.Emit(OpCodes.Initobj, newExpr.Type);
            ilGen.Emit(OpCodes.Ldloc, temp);
        }

        ilGen.Emit(OpCodes.Ret);

        return (Func<T>)method.CreateDelegate(typeof(Func<T>));
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=93)

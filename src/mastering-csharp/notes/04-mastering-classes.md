# Mastering Classes

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 4
> 9 lessons · ~10.7 minutes
> Source: Dometrain. Every section links back to the lesson it came from.
> No companion project for this chapter, by request. See [No companion project](#no-companion-project).

---

## The mental model

The chapter is two independent investigations that share one theme: **the moment an object comes into existence is less simple than it looks, and both halves of the surprise are compiler decisions rather than runtime magic.**

1. **Construction order is not top-down.** Field initializers run from the most derived type upward, and only then do constructor bodies run from the base downward. The two halves travel in opposite directions, and a virtual call caught in the middle sees an object that is half built.
2. **`new()` under a generic constraint is not `new`.** The constraint compiles to `Activator.CreateInstance`, which is a decision from C# 2.0 that still leaks today: exceptions arrive wrapped, and on .NET Framework the call is an order of magnitude slower.

The order of events for `new Derived()`:

| Step | What runs | Direction |
| --- | --- | --- |
| 1 | `Derived` field initializers | most derived first |
| 2 | `Base` field initializers | up the chain |
| 3 | `Base` constructor body | base first |
| 4 | `Derived` constructor body | down the chain |

The reason for steps 1 and 2 running before any constructor body is **object invariants**: fields are populated and observable in a valid state before any logic can look at them.
The hazard is that this guarantee covers field initializers only, and stops exactly where constructor-body assignment begins.

---

## Lesson index

| # | Lesson | Length | Covered in |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/) | 0:30 | [The mental model](#the-mental-model) |
| 2 | [Object Initialization](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/) | 0:24 | [1.1](#11-the-question-the-chapter-opens-with) |
| 3 | [Analyzing Object Construction Order](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/) | 1:34 | [1.2](#12-the-answer-and-the-lowered-code-that-proves-it) |
| 4 | [Virtual Calls in the Base class Constructor](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/) | 1:53 | [1.3](#13-the-virtual-call-hazard) |
| 5 | [Using Analyzers to Avoid Making Virtual Calls in Base Constructors](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/) | 0:36 | [1.4](#14-letting-the-analyzer-catch-it) |
| 6 | [A Custom Factory Method and Error Handling](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/) | 0:28 | [2.1](#21-the-setup-a-factory-and-a-throwing-constructor) |
| 7 | [Exploring a Custom Factory Method Behavior at Runtime](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/) | 1:45 | [2.2](#22-the-leak-targetinvocationexception) · [2.3](#23-the-fix-a-cached-expression-compiled-activator) |
| 8 | [Benchmarking the new() Constraint](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/) | 1:46 | [2.4](#24-what-it-costs-and-where) |
| 9 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/) | 1:46 | [The mental model](#the-mental-model) · [Common misconceptions](#common-misconceptions) |

Every lesson in this chapter has a document; nothing was skipped.

---

## Part 1 · Object construction

### 1.1 The question the chapter opens with

> [Object Initialization](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/)

The chapter opens as a quiz rather than a statement, and the quiz is worth answering before reading on.

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

The instrumentation is the clever part.
`[CallerMemberName]` on a static helper makes the compiler pass the name of the field being initialized, so a plain `Console.WriteLine` becomes a trace of the initialization lifecycle without a debugger.

Both classes use **field-like initialization**, meaning the value is assigned at the declaration site rather than inside a constructor.
The question the lesson poses: does initialization go base to derived, derived to base, or something interleaved?

### 1.2 The answer, and the lowered code that proves it

> [Analyzing Object Construction Order](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/)

The intuitive answer is that the base class is fully set up first.
That is wrong.
The actual sequence is:

1. `Derived` field initialization
2. `Base` field initialization
3. `Base` constructor body
4. `Derived` constructor body

The chapter does not ask you to take this on faith, and this is where it earns its keep: it shows the **lowered C#**, the code the compiler actually generated.

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

Field initializers are not syntax that lives in some separate initialization phase.
The compiler **moves them to the top of the constructor body, above the `base..ctor()` call**.
Once you have seen that, the ordering stops being a rule to memorize and becomes the only thing the generated code could possibly do.

Two consequences worth carrying forward:

- The order is identical whether or not the field is `readonly`.
- The purpose is establishing **object invariants**: every field has its declared initial value before any constructor logic anywhere in the hierarchy can observe it.

### 1.3 The virtual call hazard

> [Virtual Calls in the Base class Constructor](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/)

The chapter now springs the trap it spent two lessons setting, and it does so in two takes.
Keep them in order; the first is what makes the second alarming.

#### Take 1: it works, which is the problem

The base constructor calls a virtual method, and the derived class overrides it.

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

This runs cleanly and prints a `_derivedField.Length` of 25, the length of `"initialized:_derivedField"`.
Virtual dispatch sent the call to `Derived.ReportState()` even though `Derived`'s constructor body had not run, and it still worked, because `_derivedField` was assigned during the field-initializer phase in step 1.

The code is fine **by accident**.
Its correctness rests entirely on a field being initialized at its declaration rather than in the constructor.

#### Take 2: one ordinary refactoring, one `NullReferenceException`

Move the assignment into the constructor body.
Nothing else changes.

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

This throws a `NullReferenceException`, and the walkthrough is worth following step by step:

1. `Derived` constructor is called.
2. `Derived` field initializers run - there are none any more.
3. The `Base` constructor is called.
4. `Base` field initializers run.
5. The `Base` constructor body starts.
6. It calls the virtual `ReportState()`.
7. Virtual dispatch routes to `Derived.ReportState()`.
8. That reads `_derivedField.Length`.
9. `_derivedField` is still `null`, because the `Derived` constructor body has not run yet.

The lesson to take away is not "null-check in overrides".
It is that a virtual call in a constructor **forces every present and future derived class to be correct while partially constructed**, and nothing in the base class's signature communicates that obligation.
Whether the code works depends on where a subclass happens to put an assignment, which makes an innocuous refactoring in a different file a breaking change.

### 1.4 Letting the analyzer catch it

> [Using Analyzers to Avoid Making Virtual Calls in Base Constructors](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/)

The rule is **CA2214, "Do not call overridable methods in constructors"**, and the chapter's answer is to stop relying on discipline and let the toolchain enforce it.

- Enable the rule in `.editorconfig`.
- The warning then appears live in the IDE and again at build time.
- With **treat warnings as errors** turned on, the build fails, so the pattern cannot be committed.

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

The chapter allows that the call is occasionally intentional, and if it is, suppress the warning **with a message explaining the intent** rather than silently.
The obligation does not go away with the warning: every override still has to tolerate running before its own constructor body.

---

## Part 2 · The `new()` constraint

### 2.1 The setup: a factory and a throwing constructor

> [A Custom Factory Method and Error Handling](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/)

`where T : new()` is the standard way to say "this generic type can be constructed".
The setup is deliberately boring: a factory, a type whose constructor fails, and a caller that handles the specific failure.

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

Read it and predict which `catch` block runs.
Everything about the code says the first one.

### 2.2 The leak: `TargetInvocationException`

> [Exploring a Custom Factory Method Behavior at Runtime](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/)

The second block runs.
The specific `catch (InvalidOperationException)` is bypassed entirely, and what arrives instead is a `TargetInvocationException` whose message says only that "an exception has been thrown by the target of an invocation" - a message with no information in it, which is exactly what lands in a production log.

The cause, visible in an IL viewer: **the `new()` constraint compiles to `Activator.CreateInstance`**.
That is reflection, and reflection wraps whatever the invoked member throws.
The decision dates to C# 2.0, and it survives because changing it would break code that catches the wrapper.

The chapter's term for this is the right one: a **leaky abstraction**.
`new()` looks like `new`, is written like `new`, and behaves like reflection.

### 2.3 The fix: a cached, expression-compiled activator

Expression trees, from C# 3.0, give lightweight runtime code generation: build an expression describing `new T()`, compile it to a delegate, and cache the delegate per type.

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

With `Factory` routed through `CustomActivator`, the original `InvalidOperationException` reaches the caller unwrapped and the specific `catch` block finally runs.

The nested `Cache<T>` is doing real work.
A static field on a generic type gets **one instance per closed generic type**, so the runtime gives you a per-`T` cache with no dictionary, no lock, and thread-safe lazy initialization courtesy of the static constructor.
`Compile()` is expensive and runs exactly once per type.

### 2.4 What it costs, and where

> [Benchmarking the new() Constraint](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/)

The benchmark runs the same five strategies against two runtimes in one job, which is what makes the result legible.

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
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/?t=25)

Results as reported in the lesson:

| Runtime | `new()` vs. direct construction | What to do about it |
| --- | --- | --- |
| **.NET 10** | effectively **no overhead**; every strategy performs comparably | nothing - the JIT already optimizes the constraint into a direct call |
| **.NET Framework 4.8** | roughly **20x slower** (the conclusion lesson says up to 22x) | a custom activator earns its place |

On .NET Framework the ranking among the alternatives is itself informative.
Expression-compiled activators beat the constraint but still trail a plain delegate, held back by legacy security checks and overhead.
**Manual IL emission** is the fastest, around **10x better than the constraint**, which is why Entity Framework and Newtonsoft.Json ship exactly this kind of factory: at their instantiation volumes it is the difference that shows up end to end.

```csharp
public static class EmittedFactory<T> where T : new()
{
    public static readonly Func<T> Create = DynamicModuleLambdaCompiler.GenerateFactory<T>();
}

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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/?t=93)

The `else` branch is the tell that this is a genuinely general factory.
`newExpr.Constructor` is `null` for a value type with no declared parameterless constructor, so the emitter falls back to `initobj` on a local rather than `newobj`.

The decision this leaves you with, stated plainly:

- **Targeting modern .NET only**: keep `new()`. The performance argument is gone. Replace it only if you are being bitten by the exception wrapping.
- **Multi-targeting or on .NET Framework**: a cached expression-compiled activator is a reasonable default, and IL emission is reserved for libraries creating objects in bulk.

---

## Verified on this machine

The chapter states that exception wrapping "persists in modern .NET to avoid breaking changes".
That is a claim worth checking rather than assuming, since the performance half of the story did change between runtimes.

Running the chapter's own example on **.NET 10.0.11, Windows 11 x64**:

```
Runtime: .NET 10.0.11
--- via new() constraint ---
  caught TargetInvocationException: Exception has been thrown by the target of an invocation.
--- via Activator.CreateInstance<T>() ---
  caught TargetInvocationException: Exception has been thrown by the target of an invocation.
--- via Activator.CreateInstance(Type) ---
  caught TargetInvocationException: Exception has been thrown by the target of an invocation.
```

Confirmed, and the middle line is the interesting one.
`new()` and a hand-written `Activator.CreateInstance<T>()` produce identical behaviour, which is direct evidence for the chapter's claim that the constraint compiles down to exactly that call.

So on current .NET the two halves of the `new()` story have diverged: the **performance** cost is gone, the **exception-wrapping** cost is not.
That makes exception handling, not speed, the only remaining reason to reach for a custom activator in a modern-only codebase.

---

## Common misconceptions

**"The base class is fully constructed before the derived class starts."**
Half true, and the useful half is the false half.
Base *constructor bodies* do run before derived ones, but derived *field initializers* run before everything, including the base field initializers.
Construction is not a single pass in one direction.

**"A virtual call from a constructor dispatches to the base implementation, since the derived type is not ready yet."**
No. Virtual dispatch is driven by the object's actual type, which is fixed from the moment allocation happens.
The override runs, ready or not.

**"`new()` under a constraint is the same as writing `new`."**
It is written the same and compiles to `Activator.CreateInstance`.
On .NET Framework that costs about 20x; on modern .NET the speed is back, but the wrapped exceptions are still there.

**"If the virtual-call code works, the design is fine."**
The [take 1 / take 2 pair](#13-the-virtual-call-hazard) exists precisely to kill this one.
Working code became a `NullReferenceException` because someone moved an assignment from a declaration into a constructor - the most routine refactoring imaginable, in a class that was not the one containing the bug.

---

## Self-test

1. `new Derived()` runs four groups of code. Name them in order, and say which direction each travels through the hierarchy.
2. What single line in the lowered `Derived` constructor explains the entire ordering rule?
3. In take 1 of the virtual-call demo the code works and prints length 25. What exact property of the code makes it work, and why is that not reassuring?
4. Why does the chapter say a virtual call in a constructor imposes an obligation that the base class's signature cannot express?
5. Which analyzer rule catches this, and what has to be configured for it to stop a commit rather than just decorate the editor?
6. A caller wraps `Factory.Create<T>()` in `catch (InvalidOperationException)` and the constructor throws exactly that. Which catch block runs, and why?
7. Why is `Cache<T>` a nested static generic class rather than a `Dictionary<Type, Delegate>`?
8. You are on .NET 10 only. Give the one reason left to replace `new()` with a custom activator, and one reason that no longer applies.

<details>
<summary>Answer key</summary>

1. (1) `Derived` field initializers, (2) `Base` field initializers - these two run most-derived first, up the chain; (3) `Base` constructor body, (4) `Derived` constructor body - these run base first, down the chain.
2. `this._derivedField = Base.Init("_derivedField");` appearing **above** `base..ctor()`. The compiler injects field initializers at the top of the constructor, before the base call, so the ordering is just what the generated code does.
3. `_derivedField` is assigned by a **field initializer**, which runs in step 1, before the base constructor body that triggers the virtual call. It is not reassuring because the correctness lives in the choice of assignment site, not in the design - moving the assignment into the constructor body gives a `NullReferenceException`.
4. Because the base constructor calls the override while the derived instance is partially constructed, so every derived class must be correct in that state - and nothing in the base type's public surface tells an implementer that constraint exists.
5. CA2214, "Do not call overridable methods in constructors". Enable it in `.editorconfig` and turn on treat-warnings-as-errors, so the build fails rather than merely warning.
6. The general `catch (Exception)` block. The `new()` constraint compiles to `Activator.CreateInstance`, and reflection wraps constructor exceptions in `TargetInvocationException`, so the specific handler never matches.
7. Static fields on a generic type get one instance per closed generic type, so the runtime supplies a per-`T` cache with no dictionary lookup, no locking, and thread-safe one-time initialization via the static constructor. `Compile()` then runs exactly once per type.
8. Still applies: exceptions from constructors are wrapped in `TargetInvocationException`, confirmed on .NET 10 above. No longer applies: performance - the JIT has removed the constraint's overhead, so the ~20x .NET Framework penalty is not a modern concern.

</details>

---

## No companion project

Skipped by request for this chapter.

It is worth recording that this chapter would otherwise justify one: the construction-order trace and the two virtual-call takes produce console output you can only really internalize by watching it, and `FactoryBenchmarks` is a measurable claim that changes with the runtime.
The one thing that would **not** reproduce here is the headline `net48` benchmark arm, which needs .NET Framework 4.8 on the machine.

The exception-wrapping check in [Verified on this machine](#verified-on-this-machine) was run as a throwaway console app outside the repo, so nothing was added under `src/`.

---

## Threads into later chapters

| Deferred here | Picked up in |
| --- | --- |
| Value-based equality that classes must implement by hand | Mastering Records |
| Struct construction and the `initobj` path the emitter falls back to | Mastering Structs |
| Expression trees and compiled delegates as an allocation source | Mastering Delegates and Lambda Expressions |
| Multi-targeting so .NET Framework performance is your problem at all | [Mastering the Modern C# Stack](02-mastering-the-modern-csharp-stack.md) |

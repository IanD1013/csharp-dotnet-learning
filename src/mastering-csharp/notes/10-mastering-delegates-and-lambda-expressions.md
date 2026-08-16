# Mastering Delegates and Lambda Expressions

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 10
> 10 lessons · ~16:03
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958897/) | 0:49 | [↓](#1-overview) |
| 2 | [Lambdas Are Ubiquitous](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-are-ubiquitous-69958898/) | 0:41 | [↓](#2-lambdas-are-ubiquitous) |
| 3 | [Lambdas Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-under-the-hood-69958899/) | 2:30 | [↓](#3-lambdas-under-the-hood) |
| 4 | [Benchmarking Lambda Expressions](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-lambda-expressions-69958900/) | 2:35 | [↓](#4-benchmarking-lambda-expressions) |
| 5 | [Profiling Closure Allocations](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/) | 2:49 | [↓](#5-profiling-closure-allocations) |
| 6 | [.NET 10 Delegate De-Abstraction](https://dometrain.com/take/course/mastering-csharp-3256129/dotnet-10-delegate-de-abstraction-69958902/) | 0:59 | [↓](#6-net-10-delegate-de-abstraction) |
| 7 | [Scope-based Code Generation for Lambda Expressions](https://dometrain.com/take/course/mastering-csharp-3256129/scope-based-code-generation-for-lambda-expressions-69958903/) | 1:48 | [↓](#7-scope-based-code-generation-for-lambda-expressions) |
| 8 | [Issues With Capturing Loop Variables](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-capturing-loop-variables-69958904/) | 1:49 | [↓](#8-issues-with-capturing-loop-variables) |
| 9 | [Method Group Conversion vs. Lambda Expressions](https://dometrain.com/take/course/mastering-csharp-3256129/method-group-conversion-vs-lambda-expressions-69958905/) | 0:23 | [↓](#9-method-group-conversion-vs-lambda-expressions) |
| 10 | [Method Group Conversion vs. Lambda Expressions in Action](https://dometrain.com/take/course/mastering-csharp-3256129/method-group-conversion-vs-lambda-expressions-in-action-69958906/) | 1:40 | [↓](#10-method-group-conversion-vs-lambda-expressions-in-action) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958897/) · 0:49

This lesson provides a comprehensive overview of delegates and lambda expressions in C#, focusing on the compiler's lowering process and runtime performance optimizations.
It explores the three primary capture shapes—static, instance, and local—and details how each affects code generation, from cached delegates to the creation of DisplayClasses.
Additionally, the lesson covers the impact of lexical scope on closures, the evolution of method group caching in C# 11, and significant performance improvements in .NET 10 through delegate elision.

### Key concepts

- **Lambda Lowering Patterns**: The compiler transforms lambdas into static methods, instance methods, or methods on generated DisplayClasses depending on the captured state.
- **Capture Shapes**: Categorization of closures into no-capture (static), instance capture, and local capture, each with distinct allocation profiles.
- **DisplayClass and Hoisting**: The mechanism by which local variables are moved to fields in a compiler-generated class to persist state beyond the local scope.
- **Delegate Elision**: A .NET 10 optimization driven by Dynamic PGO and escape analysis that can eliminate delegate allocations entirely.
- **Scope-Based Closures**: Understanding that DisplayClasses are generated per capturing scope, affecting how variables are shared or isolated in nested and sibling scopes.
- **Method Group Conversions**: The performance differences between lambdas and method groups, specifically the introduction of static method group caching in C# 11.

### Lesson notes

The C# compiler transforms lambda expressions into standard classes and methods through a process called lowering.
The specific implementation depends on what the lambda captures from its surrounding environment.

#### Capture Shapes and Lowering

There are three primary patterns for lambda captures, each resulting in different code generation and allocation behavior:

1.  **No Capture (Static)**: When a lambda captures nothing or only static members, it is lowered to a static method. The compiler generates a static field to cache the `Func` or `Action` delegate, ensuring only one allocation occurs for the lifetime of the application.
2.  **Instance Capture**: If a lambda captures an instance field or the `this` reference, it is lowered to a private instance method. The delegate is not cached because doing so would pin the instance in memory indefinitely; consequently, a new delegate is allocated per call.
3.  **Local Capture**: If a lambda captures a local variable, the compiler generates a `DisplayClass` (also known as a closure object) to hold the captured state. Both the `DisplayClass` and the delegate are allocated on every call.

```csharp
public class LambdaCodegen
{
    private readonly List<int> _values = [1, 2, 3, 42];
    private readonly int _instanceValue = 42;
    private static readonly int s_staticValue = 42;

    public bool StaticValue()
    {
        return _values.Any(x => x == s_staticValue);
    }

    public bool InstanceValue()
    {
        return _values.Any(x => x == _instanceValue);
    }

    public bool ParamsValue()
    {
        var value = 42;
        return _values.Any(x => x == value);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958897/?t=4)

#### Performance and .NET 10 Optimizations

Performance implications vary significantly across .NET versions.
In .NET 8, all three capture shapes typically involve per-call allocations (except for cached static lambdas).
However, .NET 10 introduces a feature known as "Delegates the Abstractions" or delegate elision.
By utilizing Dynamic PGO and escape analysis, the .NET 10 runtime can often elide the allocation of the delegate object itself for static and instance captures.
In local captures, while the `DisplayClass` allocation remains necessary to hold the hoisted local variables, the delegate allocation can still be optimized away.

#### Scope and Closures

Closures are generated based on the capturing scope rather than the lambda itself.
When a local variable is captured, it is "hoisted" into a field of a generated `DisplayClass`.
Every read or write of that variable—both inside and outside the lambda—is rewritten by the compiler to access the field on the `DisplayClass` instance.

Nested capturing scopes result in a hierarchy of DisplayClasses, where inner classes hold a reference to their parent scope's `DisplayClass` to access outer variables.

```csharp
public static class Scopes
{
    public static void ClosurePerScope(int topLevel)
    {
        int l1 = topLevel;
        if (topLevel == 42)
        {
            Action topLevelCapture = 
                () => Console.WriteLine(topLevel + l1);
            topLevelCapture();
        }

        Console.WriteLine(l1);

        if (topLevel == 42)
        {
            int local = topLevel;
            Action nestedCapture = 
                () => Console.WriteLine(local + topLevel);
            nestedCapture();
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958897/?t=31)

This scope-based behavior is critical in loops.
In C# 5 and later, the `foreach` loop declares its iteration variable fresh per iteration, resulting in a unique `DisplayClass` instance for each loop pass.
Conversely, a `for` loop declares its iteration variable once for the entire loop statement.
If captured, all iterations share the same `DisplayClass` field, which often leads to every captured lambda reading the final value of the loop variable (the value that terminated the loop).

#### Method Groups vs. Lambda Expressions

The choice between using a lambda expression and a method group conversion can affect performance depending on the C# language version.
While no-capture lambdas have been cached by the compiler for many years, static method group conversions were only optimized in C# 11.
In C# 11 and later, the compiler generates a static cache field for static method groups, eliminating the per-call delegate allocation found in earlier versions.

```csharp
public sealed class CodeGen
{
    public static void StaticMethod() { }
    public void InstanceMethod() { }

    public Action StaticMethodGroup()
        => StaticMethod;

    public Action StaticLambda()
        => () => StaticMethod();

    public Action InstanceMethodGroup()
        => InstanceMethod;
    
    public Action InstanceLambda() => 
        () => InstanceMethod();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958897/?t=37)

---

## 2. Lambdas Are Ubiquitous

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-are-ubiquitous-69958898/) · 0:41

### Summary

Lambda expressions are a fundamental part of modern C#, especially within LINQ, where they replace the need for named helper methods for filtering, projection, and searching.
Understanding how the compiler "lowers" these expressions is essential for performance and memory management, as the generated code varies significantly depending on whether the lambda captures static members, instance members, or local variables.

### Key concepts

*   Lambdas as anonymous delegates for inline logic.
*   Ubiquity of lambdas in LINQ operations (Any, Where, Select).
*   Compiler "lowering" and the generation of backing code.
*   Variable capturing: Static vs. Instance vs. Local scope.

### Lesson notes

Lambda expressions are used extensively in C#, particularly when working with LINQ.
They allow developers to provide localized logic for operations like sequence filtering, searching, or projection without the overhead of creating named helper methods.
This is particularly useful when the logic relies on local information that is only relevant to the immediate operation.

```csharp
var hasZero = values.Any(x => x == 0);
var longNames = names.Where(x => x.Length > 10);
var squares = values.Select(x => x * x);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-are-ubiquitous-69958898/?t=10)

While lambdas appear as simple syntax, it is important to maintain a mental model of what the compiler generates under the hood.
The process of "lowering"—where the compiler transforms high-level C# into lower-level structures—differs based on what the lambda captures from its environment.

To explore how code generation changes based on the captured values, consider a class that implements three different types of capturing: static fields, instance fields, and local variables.

```csharp
public class LambdaCodegen
{
    private readonly List<int> _values = [1, 2, 3, 42];
    private readonly int _instanceValue = 42;
    private static readonly int s_staticValue = 42;

    public bool StaticValue()
    {
        return _values.Any(x => x == s_staticValue);
    }

    public bool InstanceValue()
    {
        return _values.Any(x => x == _instanceValue);
    }

    public bool ParamsValue()
    {
        var value = 42;
        return _values.Any(x => x == value);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-are-ubiquitous-69958898/?t=34)

In these examples, the compiler must decide how to represent the lambda.
If a lambda captures a local variable (as in `ParamsValue`), the compiler typically generates a hidden "display class" to hold that variable so it can persist for the lifetime of the delegate.
If it captures an instance field (as in `InstanceValue`), it must capture the `this` reference.
If it only uses static data or no external data at all, the compiler can often optimize the delegate by caching it or using a static method.

---

## 3. Lambdas Under the Hood

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-under-the-hood-69958899/) · 2:30

### Summary

The C# compiler optimizes lambda expressions by analyzing the scope of the variables they capture.
Depending on whether a lambda captures static members, instance members, or local variables, the compiler transforms the code into different underlying structures—ranging from cached singleton delegates to full "display classes" (closures).
Understanding these transformations is critical for performance tuning, as they directly impact memory allocation and execution overhead.

### Key concepts

- **Static Context Capture**: Optimized into a cached singleton delegate to avoid repeated allocations.
- **Instance State Capture**: Transformed into instance methods, requiring a new delegate allocation on every invocation.
- **Local Variable Capture (Closures)**: Generates a "display class" where variables are "lifted" into fields, incurring both class and delegate allocations.
- **Lowering**: The compiler's process of translating high-level lambdas into primitive IL-compatible classes and methods.
- **Allocation-Free Execution**: Achieved when the compiler can reuse a delegate instance, primarily in static capture scenarios.

### Lesson notes

The C# compiler does not treat all lambda expressions equally.
It analyzes what a lambda captures to generate the most efficient code possible for that specific scenario.

#### Static Context Capture

When a lambda expression captures only static fields or methods, the compiler optimizes the generated code by creating a singleton.

```csharp
using System.Collections.Generic;
using System.Linq;
public class C {
    private static int target = 42;
    public bool Capture(List<int> list)
    {
        return list.Any(x => x == target);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-under-the-hood-69958899/?t=10)

In this case, the compiler generates a private class containing the lambda's logic as an instance method.
It then exposes a singleton instance of the delegate.
At the call site, the compiler checks if this singleton is null; if it is, it instantiates the delegate once and reuses it for all subsequent calls.
This makes the lambda effectively allocation-free after the first invocation.

#### Instance State Capture

If a lambda captures instance state, such as an instance field or another instance method, the compiler cannot use a singleton.

```csharp
private readonly int _instanceValue = 42;
public bool Instance()
    => Any(_values, x => x == _instanceValue);

private bool InstanceLambda(int x) => x == _instanceValue;
public bool Instance_Lowered()
    => Any(_values, new Func<int, bool>(InstanceLambda));
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-under-the-hood-69958899/?t=115)

Instead of a singleton, the compiler generates an instance method within the enclosing class to hold the lambda's body.
However, because the delegate must be bound to the specific instance (`this`), a new delegate instance is allocated every time the method containing the lambda is called.

#### Local Variable Capture (Closures)

The most complex scenario occurs when a lambda captures local variables or parameters.
This is known as a closure.

```csharp
public bool Local(int value)
    => Any(_values, x => x == value);

private sealed class LocalDisplayClass
{
    public int value;
    public bool Lambda(int x) => x == value;
}

public bool Local_Lowered(int value)
{
    var dc = new LocalDisplayClass { value = value };
    return Any(_values, new Func<int, bool>(dc.Lambda));
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-under-the-hood-69958899/?t=115)

To preserve the state of local variables after the method returns, the compiler generates a "display class."
The local variables are "lifted" from the stack into fields of this generated class.
Every time the method is executed, a new instance of the display class is allocated, followed by a new delegate allocation that points to the display class's method.

If multiple local variables are captured, they are all represented as fields within the same generated display class.

```csharp
using System.Collections.Generic;
using System.Linq;
public class C {
    public bool Capture(List<int> list)
    {
        int lower = 42;
        int higher = 43;
        return list.Any(x => x >= lower && x <= higher);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lambdas-under-the-hood-69958899/?t=90)

---

## 4. Benchmarking Lambda Expressions

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-lambda-expressions-69958900/) · 2:35

### Summary

This lesson explores the performance implications of lambda expressions and closures by benchmarking different capture scenarios across .NET 4.8, .NET 8.0, and .NET 10.0.
By comparing a manual loop against static, instance, and local variable captures, the lesson demonstrates how the .NET runtime has evolved to minimize or eliminate heap allocations.
A key focus is the delegate de-abstraction feature introduced in .NET 10.0, which allows the JIT compiler to inline delegates that do not escape their containing method, significantly reducing the garbage collection overhead typically associated with closures on hot paths.

### Key concepts

- Benchmarking capture types: static, instance, and local variables.
- Runtime performance comparison: .NET 4.8 vs. .NET 8.0 vs. .NET 10.0.
- Memory allocation and GC pressure in high-frequency code paths.
- Delegate de-abstraction in .NET 10.0.
- JIT compiler optimizations for non-escaping delegates.

### Lesson notes

The benchmark compares a manual `foreach` loop (the baseline) against several lambda-based implementations.
To isolate the performance of the delegates themselves, a custom `Any` helper method is used instead of `LINQ.Any`, avoiding any performance gains inherent to the LINQ library itself.
The benchmark is configured to run across .NET 4.8, .NET 8.0, and .NET 10.0 to highlight the evolution of the runtime.

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<AnyBenchmarks>();

[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class AnyBenchmarks
{
    private readonly List<int> _values = [1, 2, 3, 42];
    private readonly int _instanceValue = 42;
    private static readonly int s_staticValue = 42;

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        foreach (var x in _values)
        {
            if (x == _instanceValue)
                return true;
        }
        return false;
    }
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-lambda-expressions-69958900/?t=10)

The benchmark covers three types of code generation for lambda expressions: static capture, instance capture, and local variable capture.
Static captures are the most efficient as they do not require a closure object.
Instance captures involve capturing fields from the containing class, while local captures involve variables defined within the method scope.

```csharp
    [Benchmark]
    public bool StaticValue()
        => Any(_values, x => x == s_staticValue);

    [Benchmark]
    public bool InstanceValue()
        => Any(_values, x => x == _instanceValue);

    [Benchmark]
    public bool ParamsValue()
    {
        var value = 42;
        return Any(_values, x => x == value);
    }
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-lambda-expressions-69958900/?t=25)

The custom `Any` method used in these benchmarks is a simple generic implementation that iterates over a source list and applies a predicate.

```csharp
    private static bool Any<T>(List<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-lambda-expressions-69958900/?t=40)

#### Performance and Allocation Analysis

While execution time improvements in newer .NET versions are significant, the reduction in allocated memory is often more critical.
High allocation rates force the application to spend more time in Garbage Collection (GC) rather than performing its intended work.
This is particularly important for methods like `Any`, which are frequently used on the "hot path" of applications.

In .NET 10.0, the introduction of delegate de-abstraction allows the JIT compiler to detect when a delegate is not used outside of the method.
In such cases, the delegate can be fully inlined or stack-allocated, eliminating heap allocations.

Key findings from the benchmark results include:
- **Instance Captures:** In .NET 10.0, allocations for delegates capturing instance state are completely eliminated.
- **Local Captures:** For the most common case where lambda expressions capture local state, allocations dropped from 88 bytes to 24 bytes in .NET 10.0, effectively removing 64 bytes of overhead.

These optimizations ensure that using modern C# features like lambda expressions does not come with a hidden performance penalty in high-performance scenarios.

---

## 5. Profiling Closure Allocations

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/) · 2:49

### Summary

This lesson explores the practical profiling of closure allocations across different .NET versions, specifically comparing .NET 8 and .NET 10.
It demonstrates how tiered compilation and Dynamic PGO (Profile-Guided Optimization) influence memory allocation, as the JIT compiler performs advanced optimizations like escape analysis only in later compilation tiers.
By using a controlled harness with dedicated warmup and profiling phases, developers can observe how the .NET 10 JIT can eliminate delegate allocations entirely in certain scenarios, such as instance method captures, whereas earlier versions would consistently allocate both the delegate and the display class.

### Key concepts

- **Tiered Compilation**: The JIT compiler balances startup speed (Tier 0) with execution performance (Tier 1), applying aggressive optimizations only to frequently called methods.
- **Escape Analysis**: A JIT optimization that determines if an object's lifetime is restricted to the current scope, allowing the compiler to potentially eliminate heap allocations.
- **Allocation Profiling**: Using `dotnet-trace` with the `gc-verbose` profile to capture fine-grained memory allocation data.
- **NoInlining Attribute**: Using `[MethodImpl(MethodImplOptions.NoInlining)]` to ensure methods remain distinct in profiling traces and are not merged by the compiler.
- **Warmup Phase**: Executing code multiple times before profiling to ensure the JIT has promoted the code to its most optimized tier.

### Lesson notes

To accurately profile closure allocations, it is necessary to use a console application that targets multiple frameworks, such as .NET 8 and .NET 10.
This allows for a direct comparison of how different JIT versions handle delegate and display class allocations.
The methods under test are marked with the `NoInlining` attribute to ensure they appear as distinct entries in the profiler's call tree.

```csharp
// Profile call.
//
// Build:
//   dotnet build -c Release
//
// Capture allocation traces:
//   dotnet-trace collect --profile gc-verbose --output net10.nettrace -- bin/Release/net10.0/ClosuresProfiling.exe
//   dotnet-trace collect --profile gc-verbose --output net8.nettrace  -- bin/Release/net8.0/ClosuresProfiling.exe
//
// Open the .nettrace files in PerfView, Visual Studio Profiler, or
// JetBrains dotTrace and look at Closures.Static / .Instance / .Local.

Warmup(1_000_000);
GC.Collect();
Thread.Sleep(2000);
Profile(1_000_000);
Console.WriteLine("Done.");

[MethodImpl(MethodImplOptions.NoInlining)]
static void Warmup(int count)
{
    var c = new Closures();
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/?t=10)

Profiling is complicated by tiered compilation.
The JIT compiler initially produces code quickly to minimize startup latency, but this code is often unoptimized.
As a method is called repeatedly, the JIT recompiles it with more aggressive optimizations, such as escape analysis.
To capture the behavior of the most optimized code, the harness includes a `Warmup` method to trigger these later tiers before the actual `Profile` method runs.

```csharp
Warmup(1_000_000);
GC.Collect();
Thread.Sleep(2000);
Profile(1_000_000);
Console.WriteLine("Done.");

[MethodImpl(MethodImplOptions.NoInlining)]
static void Warmup(int count)
{
    var c = new Closures();
    for (int i = 0; i < count; i++)
    {
        c.Static();
        c.Instance();
        c.Local();
    }
}

[MethodImpl(MethodImplOptions.NoInlining)]
static void Profile(int count)
{
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/?t=25)

The `Profile` method is identical to the `Warmup` method.
By the time `Profile` executes, tiered compilation and Dynamic PGO have promoted the call sites based on the profile data collected during the warmup phase.

```csharp
[MethodImpl(MethodImplOptions.NoInlining)]
static void Warmup(int count)
{
    var c = new Closures();
    for (int i = 0; i < count; i++)
    {
        c.Static();
        c.Instance();
        c.Local();
    }
}

[MethodImpl(MethodImplOptions.NoInlining)]
static void Profile(int count)
{
    var c = new Closures();
    for (int i = 0; i < count; i++)
    {
        c.Static();
        c.Instance();
        c.Local();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/?t=85)

To capture the traces, the application is built in Release mode.
The `dotnet-trace` tool is used with the `gc-verbose` profile to track as many allocations as possible.
This produces `.nettrace` files that can be analyzed in tools like Visual Studio, PerfView, or JetBrains dotTrace.

```csharp
// By the time Profile runs, tiered compilation + dynamic PGO have promoted
// the call sites with profile data. Capture the trace ONLY around the
// Profile call.
//
// Build:
//   dotnet build -c Release
//
// Capture allocation traces:
//   dotnet-trace collect --profile gc-verbose --output net10.nettrace -- bin/Release/net10.0/ClosuresProfiling.exe
//   dotnet-trace collect --profile gc-verbose --output net8.nettrace  -- bin/Release/net8.0/ClosuresProfiling.exe
//
// Open the .nettrace files in PerfView, Visual Studio Profiler, or
// JetBrains dotTrace and look at Closures.Static / .Instance / .Local.

Warmup(1_000_000);
GC.Collect();
Thread.Sleep(2000);
Profile(1_000_000);
Console.WriteLine("Done.");

[MethodImpl(MethodImplOptions.NoInlining)]
static void Warmup(int count)
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/?t=100)

The specific command to collect the trace for a specific framework version targets the compiled executable in the release folder:

```powershell
dotnet-trace collect --profile gc-verbose --output net8.nettrace -- bin/Release/net8.0/ClosuresProfiling.exe
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/profiling-closure-allocations-69958901/?t=115)

When comparing the results in a call tree, the differences between .NET 8 and .NET 10 become clear:
1. **Local Method Closures**: In .NET 8, both the display class and the `Func` delegate are allocated. In .NET 10, only the display class is allocated; the `Func` allocation is eliminated.
2. **Instance Method Closures**: In .NET 8, the delegate is allocated for every call. In .NET 10, the JIT is able to eliminate the delegate allocation entirely.

These results confirm that the .NET 10 JIT is significantly more effective at using escape analysis to optimize away delegate allocations that do not need to persist beyond the scope of the method call.

---

## 6. .NET 10 Delegate De-Abstraction

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/dotnet-10-delegate-de-abstraction-69958902/) · 0:59

In .NET 10, the Just-In-Time (JIT) compiler introduces advanced de-abstraction capabilities for delegates and lambda expressions.
By leveraging Benchmark.NET's DisassemblyDiagnoser, it is possible to observe how the JIT optimizes higher-order functions by removing delegate allocations and inlining the lambda body directly into the calling method's loop.
This optimization ensures that using modern functional constructs like Any with a lambda results in native code that is as efficient as a hand-written manual loop, even when capturing instance state.

### Key concepts

*   **JIT De-Abstraction**: The process where the JIT compiler removes the abstraction layer of a delegate to optimize performance.
*   **DisassemblyDiagnoser**: A Benchmark.NET attribute used to inspect the native assembly code generated by the JIT.
*   **Delegate Inlining**: The JIT's ability to embed the body of a lambda expression directly into the execution path of a higher-order function.
*   **Allocation Removal**: Eliminating the heap allocation typically required for delegate instances and closure objects in .NET 10.

### Lesson notes

To fully understand how the JIT handles delegates in .NET 10, benchmarks can be configured with the `DisassemblyDiagnoser`.
This attribute allows Benchmark.NET to emit detailed results containing the JIT output, which reveals the underlying native code generated for C# methods.

In this analysis, a manual loop is compared against a lambda expression that captures an instance value.
The goal is to determine if the abstraction of the delegate and the closure results in a performance penalty.

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
[DisassemblyDiagnoser(maxDepth: 5)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class AnyBenchmarks
{
    private readonly List<int> _values = [1, 2, 3, 42];
    private readonly int _instanceValue = 42;

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        foreach (var x in _values)
        { // Manual check against instance field
            if (x == _instanceValue)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool InstanceValue()
        => Any(_values, x => x == _instanceValue);

    private static bool Any<T>(List<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/dotnet-10-delegate-de-abstraction-69958902/?t=10)

When comparing the JIT output for these two cases, the results are nearly identical.
In the case of the lambda expression, the JIT compiler is able to perform "de-abstraction."
It not only removes the delegate allocation entirely but also inlines the body of the delegate (the comparison `x == _instanceValue`) directly into the loop within the `Any` method.

The following assembly comparison demonstrates that the `InstanceValue` benchmark generates logic that directly compares the item to the instance field, just like the `ManualLoop`.

```assembly
// ManualLoop - hand-written foreach
M00_L00:
    cmp edx,[rax+10]        ; i < list._size ?
    jae EXIT_FALSE
    mov r8,[rax+8]          ; r8 = list._items
    cmp edx,[r8+8]          ; bounds check
    jae THROW_RANGE
    mov r8d,[r8+rdx*4+10]   ; item = _items[i]
    inc edx
    cmp r8d,[rcx+10]        ; item == this._instanceValue ?
    jne M00_L00

// InstanceValue - Any(_values, x => x == _instanceValue)
M00_L00:
    cmp r8d,[rax+10]        ; i < list._size ?
    jae EXIT_FALSE
    mov r10,[rax+8]         ; r10 = list._items
    cmp r8d,[r10+8]         ; bounds check
    jae THROW_RANGE
    mov r10d,[r10+r8*4+10]  ; item = _items[i]
    inc r8d
    cmp r10d,[rcx+10]       ; INLINED LAMBDA: item == this._instanceValue
    je  EXIT_TRUE
    cmp edx,[rax+14]        ; enumerator _version still valid?
    je  M00_L00
    jmp THROW_VERSION
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/dotnet-10-delegate-de-abstraction-69958902/?t=10)

This de-abstraction capability ensures that developers can use clean, functional abstractions without sacrificing the performance of low-level, manual implementations.
Similar JIT optimizations also apply to other abstractions, such as enumeration.

---

## 7. Scope-based Code Generation for Lambda Expressions

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/scope-based-code-generation-for-lambda-expressions-69958903/) · 1:48

### Summary

The C# compiler manages closures by generating "display classes" that correspond to the lexical scopes where variables are captured.
When a lambda expression accesses a local variable, that variable is "hoisted" into a field of a heap-allocated display class, and the compiler rewrites all references to that variable to use the display class field instead.
This mechanism ensures that the captured state remains available even if the delegate outlives the original stack frame, and it allows for complex scenarios where different scopes require independent state management.

### Key concepts

- **Display Class Generation**: The compiler creates hidden classes to store captured state on the heap.
- **Variable Hoisting**: Local variables and parameters used in lambdas are converted into fields within a display class.
- **Scope-Specific Classes**: Separate display classes are generated for different lexical scopes, such as nested `if` blocks.
- **Reference Rewriting**: All usages of a captured variable, including those in the surrounding method, are updated to access the display class field.
- **Lifetime Management**: Hoisting state to the heap allows delegates to safely outlive the execution of their defining method, preventing security risks associated with accessing expired stack frames.

### Lesson notes

When a lambda expression captures state from its surrounding environment, the compiler performs a significant transformation of the code.
This is best demonstrated by examining a method with multiple scopes and captures.

```csharp
public static void ClosurePerScope(int topLevel)
{
    int l1 = topLevel;
    if (topLevel == 42)
    {
        Action topLevelCapture = 
            () => Console.WriteLine(topLevel + l1);
        topLevelCapture();
    }

    Console.WriteLine(l1);

    if (topLevel == 42)
    {
        int local = topLevel;
        Action nestedCapture = 
            () => Console.WriteLine(local + topLevel);
        nestedCapture();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/scope-based-code-generation-for-lambda-expressions-69958903/?t=15)

In the example above, the compiler generates separate display classes for each scope that captures state.
The first lambda captures `topLevel` and `l1` from the top-level scope of the method.
Consequently, the compiler "lifts" these variables into fields of a generated display class.

Once a variable is hoisted, it is no longer a local variable on the stack.
Instead, it becomes a field in the display class instance.
Crucially, every usage of that variable—even those outside the lambda expression, such as the `Console.WriteLine(l1)` call in the middle of the method—is rewritten to access the field on the display class instance.

The display class instance is typically created at the top of the method.
This transformation occurs even if the code containing the lambda is unreachable or never executed.
This is necessary because the compiler must ensure that any reference to the hoisted variable throughout the method is consistent.

When a second scope is introduced, such as the second `if` block in the example, the compiler generates a dedicated display class for that specific block.
This allows for more granular control over which variables are captured and how they are stored.
In cases where a nested lambda captures variables from both the inner and outer scopes, the inner display class will maintain a reference to the outer display class to access the parent scope's variables.

To visualize this, the "lowered" code produced by the compiler looks similar to the following structure:

```csharp
private sealed record OuterDisplayClass {
    public int topLevel; public int l1;
    public void TopLevelCapture() 
        => Console.WriteLine(topLevel + l1);
}

private sealed class InnerDisplayClass {
    public OuterDisplayClass parent = null!;
    public int local;

    public void NestedCapture() 
        => Console.WriteLine(local + parent.topLevel);
}

public static void ClosurePerScope_Lowered(int topLevel)
{
    var dcOuter = new OuterDisplayClass { topLevel = topLevel };
    // l1 hoisted into the field
    dcOuter.l1 = dcOuter.topLevel;
    
    // All reads now go through dcOuter
    if (dcOuter.topLevel == 42)
    {
        Action topLevelCapture = new Action(dcOuter.TopLevelCapture);
        topLevelCapture();
    }

    // Usage outside the lambda is also rewritten to use the DisplayClass
    Console.WriteLine(dcOuter.l1);

    if (dcOuter.topLevel == 42)
    {
        var dcInner = new InnerDisplayClass { parent = dcOuter };
        dcInner.local = dcOuter.topLevel;
        Action nestedCapture = new Action(dcInner.NestedCapture);
        nestedCapture();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/scope-based-code-generation-for-lambda-expressions-69958903/?t=63)

This architecture is essential because delegates are objects that can outlive the stack frame of the method that created them.
If the compiler did not hoist these variables to the heap via a display class, the delegate would attempt to access a stack frame that no longer exists once the method returns, which would be a significant security and stability risk.

---

## 8. Issues With Capturing Loop Variables

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-capturing-loop-variables-69958904/) · 1:49

### Summary

Capturing loop variables in lambda expressions can lead to unexpected behavior because lambdas capture variables, not their current values.
While C# 5 updated foreach loops to treat the iteration variable as local to each iteration, for loops still share a single variable across all iterations if the variable is declared in the outer scope.
This often results in all captured lambdas referencing the final state of the loop variable, potentially causing errors like IndexOutOfRangeException when the delegates are eventually invoked.

### Key concepts

*   **Variable Capture**: Lambda expressions capture the variable itself, meaning they see the most recent value of that variable when the lambda is executed.
*   **Foreach Loop Scoping**: Since C# 5, the iteration variable in a `foreach` loop is logically local to each iteration, preventing common capture bugs.
*   **For Loop Scoping**: The loop variable in a `for` loop is often shared across all iterations, especially if declared outside the loop header.
*   **Closure Issues**: If a captured variable changes before the lambda is called, the lambda will use the updated value.

### Lesson notes

When implementing lambda expressions that reference variables defined in a loop, it is important to distinguish between how `foreach` and `for` loops handle variable scoping and capture.

Consider an example where we attempt to store actions that print elements from an array:

```csharp
int[] numbers = [1, 2, 3 ];

var actions = new List<Action>();
foreach (var n in numbers)
{
    actions.Add(
        () => Console.Write($"{n} "));
}

Console.WriteLine("foreach: ");
foreach (Action a in actions)
    a();
Console.WriteLine();

Console.WriteLine("for: ");
var forActions = new List<Action>();
int i;
for (i = 0; i < numbers.Length; i++)
{
    forActions.Add(
        () => Console.Write($"{numbers[i]} "));
}

foreach (var a in forActions)
    a();

Console.WriteLine();
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-capturing-loop-variables-69958904/?t=15)

In the `foreach` loop, the output is `1 2 3`.
This is the expected behavior because the C# compiler generates a unique display class or variable instance for each iteration of the loop.
This change was introduced in C# 5 to resolve common points of confusion regarding closures.

However, the `for` loop behavior remains unchanged.
In the code above, the variable `i` is declared in the outer scope.
As the loop iterates, the same variable `i` is incremented.
The lambda expressions added to `forActions` capture the variable `i`, not its value at the time of addition.
When the loop finishes, `i` is equal to `3`.
When the actions are finally invoked, they all attempt to access `numbers[3]`, which results in an `IndexOutOfRangeException` (or `ArgumentOutOfRangeException`).

To fix this, you must ensure that each lambda captures a unique variable that does not change after the iteration.
This is achieved by introducing a local variable inside the loop body to hold a snapshot of the current value:

```csharp
var forActions = new List<Action>();
int i;
for (i = 0; i < numbers.Length; i++)
{
    int j = i;
    forActions.Add(
        () => Console.Write($"{numbers[j]} "));
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/issues-with-capturing-loop-variables-69958904/?t=98)

By capturing `j`, which is unique to each iteration's scope, the lambdas will correctly print `1 2 3` when invoked.

---

## 9. Method Group Conversion vs. Lambda Expressions

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/method-group-conversion-vs-lambda-expressions-69958905/) · 0:23

### Summary

This lesson introduces the two primary methods for converting C# methods into delegates: method group conversion and lambda expressions.
It demonstrates how both static and instance methods can be assigned to Action delegates using these two distinct syntaxes, providing a foundation for analyzing the differences in the resulting compiled code.

### Key concepts

* Method group conversion (implicit conversion of a method name to a delegate)
* Lambda expressions as wrappers for method calls
* Static vs. instance method delegate assignment
* Code generation implications for delegate creation

### Lesson notes

In C#, there are two primary ways a method can be converted to a delegate instance.
The first approach is **method group conversion**, which allows for the implicit conversion of a method name directly to a delegate type (such as `Action`).
The second approach is using a **lambda expression** to wrap the method call.

These two techniques can be applied to both static and instance methods, as shown in the following `CodeGen` class:

```csharp
public sealed class CodeGen
{
    public static void StaticMethod() { }
    public void InstanceMethod() { }

    public Action StaticMethodGroup()
        => StaticMethod;

    public Action StaticLambda()
        => () => StaticMethod();

    public Action InstanceMethodGroup()
        => InstanceMethod;
    
    public Action InstanceLambda() => 
        () => InstanceMethod();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/method-group-conversion-vs-lambda-expressions-69958905/?t=10)

While these approaches appear functionally identical in source code, they can result in different underlying code generation.
Method group conversion provides a concise syntax for assigning a method directly to a delegate, whereas a lambda expression creates an anonymous function that then invokes the target method.
Understanding these differences is essential for analyzing compiler behavior and performance in performance-sensitive applications.

---

## 10. Method Group Conversion vs. Lambda Expressions in Action

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/method-group-conversion-vs-lambda-expressions-in-action-69958906/) · 1:40

### Summary

This lesson explores the historical performance discrepancy between method group conversions and lambda expressions in C#.
While lambda expressions have long benefited from compiler-level delegate caching, method group conversions traditionally required a new delegate allocation on every call due to language specification constraints.
With the release of C# 11, the specification was updated to allow the compiler to generate more efficient, cached code for method groups, bringing their performance in line with lambda expressions.

### Key concepts

* **Method Group Conversion**: Assigning a method name directly to a delegate.
* **Lambda Expressions**: Using the `() => Method()` syntax to define a delegate.
* **Delegate Caching**: The compiler's ability to reuse a single delegate instance instead of allocating a new one on every call.
* **C# 11 Specification Change**: The update that unified code generation for method groups and lambdas.
* **Performance Impact**: The significance of delegate allocations on "hot paths" in an application.

### Lesson notes

Historically, method group conversions and lambda expressions resulted in different underlying code generation.
In older versions of the C# language and the .NET Framework, using a method group conversion (e.g., `Action a = StaticMethod;`) forced the compiler to instantiate a new delegate every time the code was executed.
This was a requirement of the C# language specification at the time.

In contrast, lambda expressions (e.g., `Action a = () => StaticMethod();`) allowed the compiler more flexibility.
Because the specification did not mandate a new instantiation for lambdas, the compiler could optimize the code by caching the delegate and allocating it only once.
This difference is visible when inspecting the lowered code of a class containing both approaches.

```csharp
using System;

public class Program
{
    public static void Main()
    {}
}

public sealed class CodeGen
{
    public static void StaticMethod() { }
    public void InstanceMethod() { }

    public Action StaticMethodGroup()
        => StaticMethod;

    public Action StaticLambda()
        => () => StaticMethod();

    public Action InstanceMethodGroup()
        => InstanceMethod;
    
    public Action InstanceLambda() => 
        () => InstanceMethod();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/method-group-conversion-vs-lambda-expressions-in-action-69958906/?t=10)

When examining the generated code in a pre-C# 11 environment, the `StaticMethodGroup` method produces a new allocation on every call, while the `StaticLambda` method uses a cached field.
This behavior remains consistent for instance methods as well.

Starting with C# 11, the language specification was updated to allow method group conversions to be cached.
When the language version is set to C# 11 or later, the code generation for both method groups and lambda expressions becomes identical and more efficient.
Both cases will now benefit from delegate caching, reducing the allocation overhead.

While the performance difference might be negligible in many scenarios, it can become significant when the code is executed on a "hot path"—a frequently called section of the application where excessive allocations can lead to increased garbage collection pressure and reduced throughput.

# Mastering LINQ

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 12
> 10 lessons · ~11:11
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Section Overview](https://dometrain.com/take/course/mastering-csharp-3256129/section-overview-69958917/) | 1:04 | [↓](#1-section-overview) |
| 2 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958918/) | 0:39 | [↓](#2-overview) |
| 3 | [Iterator Design Pattern in C#](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-design-pattern-in-csharp-69958919/) | 0:48 | [↓](#3-iterator-design-pattern-in-c) |
| 4 | [Iterator as a State Machine](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-state-machine-69958920/) | 1:08 | [↓](#4-iterator-as-a-state-machine) |
| 5 | [Foreach Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-under-the-hood-69958921/) | 1:26 | [↓](#5-foreach-under-the-hood) |
| 6 | [Foreach and IEnumerable](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-and-ienumerable-69958922/) | 1:03 | [↓](#6-foreach-and-ienumerable) |
| 7 | [The Cost of Boxed Iterators](https://dometrain.com/take/course/mastering-csharp-3256129/the-cost-of-boxed-iterators-69958923/) | 1:12 | [↓](#7-the-cost-of-boxed-iterators) |
| 8 | [Enumerable De-Abstraction in .NET 10](https://dometrain.com/take/course/mastering-csharp-3256129/enumerable-de-abstraction-in-dotnet-10-69958924/) | 2:05 | [↓](#8-enumerable-de-abstraction-in-net-10) |
| 9 | [Iterator as a Mutable Struct](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-mutable-struct-69958925/) | 0:48 | [↓](#9-iterator-as-a-mutable-struct) |
| 10 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/) | 0:58 | [↓](#10-summary) |

## 1. Section Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/section-overview-69958917/) · 1:04

### Summary

This section introduces Language Integrated Query (LINQ), a declarative API for processing data from diverse sources.
While LINQ supports various providers like SQL and XML, this module focuses on the foundational IEnumerable interface for in-memory data processing.
By mastering the underlying mechanics of iterator blocks and IEnumerable, developers can leverage LINQ's power while avoiding common performance pitfalls and misconceptions.

### Key concepts

* Language Integrated Query (LINQ) as a unified, declarative data processing API.
* The role of IEnumerable and iterator blocks in LINQ's architecture.
* Comparison between Query Expression syntax and Fluent syntax.
* Performance optimizations in modern .NET Core implementations of LINQ.

### Lesson notes

Language Integrated Query (LINQ) provides a unified set of APIs for processing data from various sources in a declarative manner.
While there are multiple flavors of LINQ—including LINQ to SQL, LINQ to Entities, and LINQ to XML—the foundation of the technology lies in processing in-memory data via the `IEnumerable` interface.

Common misconceptions suggest that LINQ is inherently slow or difficult to reason about.
However, these issues typically stem from a lack of understanding of the underlying fundamentals.
LINQ is built upon two primary concepts: `IEnumerable` and iterator blocks.
Understanding these concepts is essential for predicting performance implications and managing error handling effectively.

LINQ has evolved significantly over time.
Modern .NET Core versions include numerous optimizations that address performance concerns present in older iterations of the framework.
When used correctly, LINQ is a powerful, declarative, and efficient tool for data manipulation.

LINQ supports two primary styles of syntax: Query Expression syntax (resembling SQL) and Fluent syntax (using method chaining).

```csharp
IEnumerable<int> source = [1, 2, 3];

// Query expression syntax
IEnumerable<string> query =
    from x in source
    where x > 0 && x < 3
    select x.ToString();

// Fluent syntax
IEnumerable<string> query2 = source
    .Where(x => x > 0 && x < 3)
    .Select(x => x.ToString());
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/section-overview-69958917/?t=10)

## 2. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958918/) · 0:39

### Summary

This lesson introduces the foundational concepts of LINQ by exploring the Iterator design pattern and the mechanics of the foreach loop in C#.
It covers the implementation of IEnumerable and IEnumerator interfaces, the behavior of enumerators as state machines, and how the C# compiler translates foreach into lower-level code.
Additionally, it highlights significant performance optimizations introduced in .NET 10 regarding enumeration de-abstraction and JIT devirtualization.

### Key concepts

- Iterator design pattern implementation via IEnumerable and IEnumerator.
- Enumerator state management (Before, During, and After iteration).
- Pattern-based foreach implementation and duck typing.
- Performance differences between struct-based and interface-based enumeration.
- .NET 10 enumeration de-abstraction for well-known collection types.

### Lesson notes

LINQ is built upon the fundamentals of the Iterator design pattern.
In C#, this pattern is primarily implemented through the `IEnumerable` and `IEnumerator` interfaces.
Before diving into complex LINQ queries, it is essential to understand how these interfaces facilitate iteration and how the `foreach` loop interacts with them.

#### Manual Iteration and the Iterator Pattern

The process of iteration involves retrieving an enumerator from a collection and advancing it through the sequence.
The `IEnumerator` interface provides the `Current` property to access the current element and the `MoveNext()` method to advance to the next element.

```csharp
List<int> list = [1, 2, 3];
var e = list.GetEnumerator();

// 0, but technically, undefined
Console.WriteLine(e.Current);

e.MoveNext(); // True
Console.WriteLine(e.Current); // 1

e.MoveNext(); // True
Console.WriteLine(e.Current); // 2

e.MoveNext(); // True
Console.WriteLine(e.Current); // 3

e.MoveNext(); // False
// 0, but technically, undefined
Console.WriteLine(e.Current);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958918/?t=9)

#### Enumerator as a State Machine

An enumerator acts as a state machine.
Its behavior changes based on its position in the sequence.
Accessing `Current` before the first call to `MoveNext()` or after `MoveNext()` returns `false` is generally undefined or may throw an exception, depending on the implementation.
This stateful behavior is critical for understanding iterator blocks.

```csharp
Console.WriteLine("=== Current before the first MoveNext ===");
var sequence = new StatefulSequence();
var e = sequence.GetEnumerator();
ReadCurrent("before MoveNext", e);

Console.WriteLine();
Console.WriteLine("=== Current during iteration ===");
while (e.MoveNext())
{
    Console.WriteLine($"inside loop: {e.Current}");
}

Console.WriteLine();
Console.WriteLine("=== Current after iteration ends ===");
ReadCurrent("after MoveNext returned false", e);

static void ReadCurrent(string label, StatefulSequence.Enumerator enumerator)
{
    try
    {
        Console.WriteLine($"{label}: {enumerator.Current}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"{label}: {ex.Message}");
    }
}

readonly struct StatefulSequence
{
    public Enumerator GetEnumerator() => new([10, 20, 30]);

    public struct Enumerator
    {
        private readonly int[] _items;
        private int _index;

        public Enumerator(int[] items)
        {
            _items = items;
            _index = -1;
        }

        public int Current
        {
            get
            {
                if (_index < 0)
                    throw new InvalidOperationException("Current is invalid before the first MoveNext().");

                if (_index >= _items.Length)
                    throw new InvalidOperationException("Current is invalid after MoveNext() returned false.");

                return _items[_index];
            }
        }

        public bool MoveNext()
        {
            if (_index < _items.Length)
                _index++;

            return _index < _items.Length;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958918/?t=17)

#### Foreach Implementation Under the Hood

The `foreach` loop in C# is a syntactic sugar that the compiler expands.
Interestingly, `foreach` does not strictly require a type to implement `IEnumerable`.
It uses a "pattern-based" approach, looking for a `GetEnumerator()` method that returns an object with a `MoveNext()` method and a `Current` property.
This allows for efficient, non-allocating iteration when using struct-based enumerators.

```csharp
using System.Collections;

ForeachOverList(new List<int> { 1, 2, 3 });
Console.WriteLine();
ForeachOverRange(new Range(1, 3));

static void ForeachOverList(List<int> list)
{
    Console.WriteLine("=== List<T> uses its enumerator ===");
    foreach (var value in list)
    {
        Console.WriteLine(value);
    }

    List<int>.Enumerator listEnumerator = list.GetEnumerator();
}

static void ForeachOverRange(Range range)
{
    Console.WriteLine("=== Pattern-based foreach ===");

    foreach (var value in range)
    {
        Console.WriteLine(value);
    }
}

readonly struct Range(int start, int end)
{
    public RangeEnumerator GetEnumerator() => new(start, end);
}

struct RangeEnumerator(int start, int end)
{
    private int _current = start - 1;

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= end;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958918/?t=24)

#### Performance Changes in .NET 10

Historically, enumerating over an `IEnumerable<T>` interface was slower than enumerating over a concrete collection like `List<T>`.
This was because `IEnumerable<T>.GetEnumerator()` returns an `IEnumerator<T>` interface, which often causes a struct-based enumerator to be boxed on the heap and requires virtual method calls for `MoveNext()` and `Current`.

In .NET 10, "enumeration de-abstraction" allows the JIT compiler to recognize well-known collection types even when they are hidden behind an `IEnumerable<T>` interface.
This enables the JIT to devirtualize calls and elide boxing, bringing the performance of interface-based enumeration closer to that of direct collection enumeration.

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class ForeachBenchmarks
{
    private readonly List<int> _list = [1, 2, 3, 42];

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] == 42)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool EnumerateAsList()
        => AnyList(_list, x => x == 42);

    [Benchmark]
    public bool EnumerateAsIEnumerable()
        => AnyEnumerable(_list, x => x == 42);

    private static bool AnyList<T>(List<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }

    private static bool AnyEnumerable<T>(IEnumerable<T> source, Func<T, bool> predicate)
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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958918/?t=30)

## 3. Iterator Design Pattern in C#

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-design-pattern-in-csharp-69958919/) · 0:48

### Summary

The Iterator design pattern in C# provides a standardized mechanism for traversing the elements of a collection or sequence without exposing its internal implementation.
This pattern is implemented via the `IEnumerable<T>` and `IEnumerator<T>` interfaces, where the former represents the data source and the latter maintains the state of the traversal, including the current position and the logic for moving to the next item.

### Key concepts

- **Iterator Pattern**: Decouples element access from the underlying data structure.
- **IEnumerable<T>**: An interface representing a sequence or collection in memory.
- **IEnumerator<T>**: An interface representing the iterator or pointer within a sequence.
- **Current**: A property providing access to the element at the current iterator position.
- **MoveNext()**: A method that advances the iterator and returns a boolean indicating if more elements remain.
- **Dispose()**: A method for cleaning up resources used by the iterator.
- **Reset()**: An optional method to restart iteration; implementation is inconsistent and usage is discouraged.

### Lesson notes

The Iterator design pattern allows for sequential access to an object's elements without revealing its internal details.
In the .NET ecosystem, this is primarily handled through two interfaces: `IEnumerable<T>` and `IEnumerator<T>`.
`IEnumerable<T>` represents the collection or sequence itself, while `IEnumerator<T>` acts as the iterator—a stateful pointer that tracks the current position within that sequence.

C# supports a pattern-based `foreach` loop.
While collections like `List<T>` implement these interfaces explicitly, any type that provides a `GetEnumerator()` method returning an object with a `Current` property and a `MoveNext()` method can be used with `foreach` without strictly implementing the interfaces.

```csharp
using System.Collections;

ForeachOverList(new List<int> { 1, 2, 3 });
Console.WriteLine();
ForeachOverRange(new Range(1, 3));

static void ForeachOverList(List<int> list)
{
    Console.WriteLine("=== List<T> uses its enumerator ===");
    foreach (var value in list)
    {
        Console.WriteLine(value);
    }

    List<int>.Enumerator listEnumerator = list.GetEnumerator();
}

static void ForeachOverRange(Range range)
{
    Console.WriteLine("=== Pattern-based foreach ===");

    foreach (var value in range)
    {
        Console.WriteLine(value);
    }
}

readonly struct Range(int start, int end)
{
    public RangeEnumerator GetEnumerator() => new(start, end);
}

struct RangeEnumerator(int start, int end)
{
    private int _current = start - 1;

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= end;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-design-pattern-in-csharp-69958919/?t=8)

The `IEnumerator<T>` interface defines four members:
- **Current**: Accesses the current item in the sequence.
- **MoveNext()**: Advances the position to the next element. It returns `false` if the end of the sequence is reached.
- **Dispose()**: Releases managed resources associated with the iterator.
- **Reset()**: Resets the iterator to its initial state. This is not always implemented and should generally be avoided.

When interacting with an enumerator manually, the iterator starts in an undefined state.
`MoveNext()` must be called to advance to the first element before `Current` can be safely accessed.

```csharp
List<int> list = [1, 2, 3];
var e = list.GetEnumerator();

// 0, but technically, undefined
Console.WriteLine(e.Current);

e.MoveNext(); // True
Console.WriteLine(e.Current); // 1

e.MoveNext(); // True
Console.WriteLine(e.Current); // 2

e.MoveNext(); // True
Console.WriteLine(e.Current); // 3

e.MoveNext(); // False
// 0, but technically, undefined
Console.WriteLine(e.Current);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-design-pattern-in-csharp-69958919/?t=21)

## 4. Iterator as a State Machine

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-state-machine-69958920/) · 1:08

### Summary

An enumerator in C# acts as a state machine that tracks its current position within a collection.
When first created, the enumerator points to a position before the first element, making the Current property undefined.
Calling MoveNext() advances the pointer and returns a boolean indicating if a valid element was reached.
Once the end of the collection is surpassed, the enumerator returns to an undefined state where Current should no longer be accessed.

### Key concepts

- **Initial State**: Upon creation, an enumerator is positioned before the first element (index -1).
- **MoveNext()**: A method that advances the enumerator to the next element and returns true if successful, or false if the end of the collection is reached.
- **Current Property**: Accesses the element at the current position; its value is undefined if MoveNext() has not been called or if it has returned false.
- **State Machine Logic**: Iterators maintain internal state (like an index) to manage traversal.

### Lesson notes

When consuming an enumerator manually rather than using a foreach loop, it is important to understand its internal state transitions.
When an enumerator is first created via GetEnumerator(), its state is undefined.
At this stage, the enumerator conceptually points to the element before the first item in the collection.

Accessing the Current property at this point is technically undefined behavior.
While it may return a default value (such as 0 for integers), some implementations may throw an exception.

```csharp
List<int> list = [1, 2, 3];
var e = list.GetEnumerator();

// 0, but technically, undefined
Console.WriteLine(e.Current);

e.MoveNext(); // True
Console.WriteLine(e.Current); // 1

e.MoveNext(); // True
Console.WriteLine(e.Current); // 2

e.MoveNext(); // True
Console.WriteLine(e.Current); // 3

e.MoveNext(); // False
// 0, but technically, undefined
Console.WriteLine(e.Current);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-state-machine-69958920/?t=10)

To retrieve the first item, MoveNext() must be called for the first time.
This advances the iterator to the first element.
Subsequent calls to MoveNext() advance the pointer through the collection.
Once MoveNext() returns false, the iterator has reached the end of the source.
At this point, the state becomes undefined again, and Current should not be accessed.
Any further calls to MoveNext() will continue to return false.

While foreach handles this logic automatically, the underlying implementation relies on a state machine.
A custom implementation of an enumerator demonstrates how the internal index is managed, starting at -1 and validating the bounds before returning a value from Current.

```csharp
readonly struct StatefulSequence
{
    public Enumerator GetEnumerator() => new([10, 20, 30]);

    public struct Enumerator
    {
        private readonly int[] _items;
        private int _index;

        public Enumerator(int[] items)
        {
            _items = items;
            _index = -1;
        }

        public int Current
        {
            get
            {
                if (_index < 0)
                    throw new InvalidOperationException("Current is invalid before the first MoveNext().");

                if (_index >= _items.Length)
                    throw new InvalidOperationException("Current is invalid after MoveNext() returned false.");

                return _items[_index];
            }
        }

        public bool MoveNext()
        {
            if (_index < _items.Length)
                _index++;

            return _index < _items.Length;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-state-machine-69958920/?t=62)

## 5. Foreach Under the Hood

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-under-the-hood-69958921/) · 1:26

### Summary

The foreach loop in C# is a high-level syntactic convenience that the compiler lowers into a specific pattern involving a while loop and an enumerator.
This process includes wrapping the iteration in a try-finally block to ensure the enumerator is properly disposed of, which is vital for resource management.
Crucially, foreach does not strictly require the IEnumerable interface; it employs a pattern-based approach (duck typing) that looks for a GetEnumerator method returning an object with MoveNext and Current members.
By using specialized value-type enumerators, such as List<T>.Enumerator, the compiler can also avoid unnecessary heap allocations during iteration.

### Key concepts

- Compiler lowering of foreach into a while loop.
- Automatic resource management via try-finally and IDisposable.Dispose().
- Performance optimization using struct-based enumerators to avoid heap allocation.
- Pattern-based foreach (duck typing) which allows iteration without formal interface implementation.
- The iteration protocol: GetEnumerator() -> MoveNext() -> Current.

### Lesson notes

The foreach statement is not a primitive operation in the C# runtime; instead, the compiler "lowers" the code into a more verbose structure.
When you write a foreach loop over a collection like List<int>, the compiler generates a while loop that manages an enumerator.

```csharp
[CompilerGenerated]
internal static void ForeachOverList(List<int> list)
{
    Console.WriteLine("=== List<T> uses its enumerator ===");
    List<int>.Enumerator enumerator = list.GetEnumerator();
    try
    {
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }
    }
    finally
    {
        enumerator.Dispose();
    }
}

[CompilerGenerated]
internal static void ForeachOverRange(Range range)
{
    Console.WriteLine("=== Pattern-based foreach ===");
    RangeEnumerator enumerator = range.GetEnumerator();
    while (enumerator.MoveNext())
    {
        Console.WriteLine(enumerator.Current);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-under-the-hood-69958921/?t=10)

As shown in the lowered code, the compiler calls GetEnumerator() to obtain an enumerator.
It then uses a while loop to call MoveNext().
If MoveNext() returns true, the Current property is accessed to get the value for that iteration.
If the collection is empty, MoveNext() returns false immediately, and the loop body is skipped.
Importantly, the loop is wrapped in a try-finally block where Dispose() is called on the enumerator, ensuring resources are cleaned up even if an exception occurs.

```csharp
using System.Collections;

ForeachOverList(new List<int> { 1, 2, 3 });
Console.WriteLine();
ForeachOverRange(new Range(1, 3));

static void ForeachOverList(List<int> list)
{
    Console.WriteLine("=== List<T> uses its enumerator ===");
    foreach (var value in list)
    {
        Console.WriteLine(value);
    }

    List<int>.Enumerator listEnumerator = list.GetEnumerator();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-under-the-hood-69958921/?t=45)

One significant detail is the type of the enumerator.
For a List<int>, the compiler does not use the generic IEnumerator<int> interface.
Instead, it uses the specific List<int>.Enumerator type.

```csharp
public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
{
    public T Current { get; }
    object IEnumerator.Current { get; }
    public void Dispose();
    public bool MoveNext();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-under-the-hood-69958921/?t=55)

Because List<int>.Enumerator is defined as a struct, the foreach loop can iterate over the list without allocating an object on the heap.
This optimization is possible because foreach is pattern-based.
The compiler does not require the collection to implement IEnumerable; it only requires that the collection has a GetEnumerator() method, and that the returned object has a MoveNext() method and a Current property.

```csharp
static void ForeachOverRange(Range range)
{
    Console.WriteLine("=== Pattern-based foreach ===");

    foreach (var value in range)
    {
        Console.WriteLine(value);
    }
}

readonly struct Range(int start, int end)
{
    public RangeEnumerator GetEnumerator() => new(start, end);
}

struct RangeEnumerator(int start, int end)
{
    private int _current = start - 1;

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= end;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-under-the-hood-69958921/?t=70)

In this Range example, neither the Range struct nor the RangeEnumerator implement any interfaces.
However, because they follow the expected naming pattern, the compiler is able to use them within a foreach loop.

## 6. Foreach and IEnumerable

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-and-ienumerable-69958922/) · 1:03

This lesson explores the relationship between the foreach keyword and the IEnumerable interface, highlighting how the C# compiler uses a pattern-based approach (duck typing) to optimize iteration.
By binding to concrete types like List<T> instead of their interfaces, the compiler can utilize struct-based enumerators to avoid heap allocations and virtual method calls.
The lesson demonstrates these performance differences through benchmarking and explains how modern .NET versions use enumeration de-abstraction to mitigate the overhead traditionally associated with the IEnumerable interface.

### Key concepts

*   **Pattern-based Foreach**: The C# compiler does not strictly require a collection to implement `IEnumerable`; it only requires a `GetEnumerator` method that returns an object with `MoveNext` and `Current` members.
*   **Struct Enumerators**: Collections like `List<T>` implement a custom `GetEnumerator` that returns a `struct` rather than a class, avoiding heap allocations during iteration.
*   **Interface Overhead**: When a collection is treated as an `IEnumerable<T>`, the struct enumerator is boxed into an `IEnumerator<T>`, leading to heap allocations and virtual call overhead.
*   **Enumeration De-abstraction**: A .NET 10+ optimization where the JIT compiler recognizes concrete types behind an `IEnumerable<T>` abstraction to devirtualize calls and elide boxing.
*   **Legacy Context**: In older versions of C# (pre-generics), non-generic `IEnumerable` usage resulted in significant boxing allocations for every iteration.

### Lesson notes

The `foreach` loop is often synonymous with the `IEnumerable` interface, but the compiler's implementation is actually more flexible.
To support `foreach`, a type simply needs to follow a specific pattern—often referred to as duck typing—where it provides a `GetEnumerator()` method.
This method must return an object (the iterator) that contains a `bool MoveNext()` method and a `Current` property.

This distinction is critical for performance.
For example, `List<T>` defines its own `GetEnumerator()` which returns a `List<T>.Enumerator`.
This is a `struct` rather than a class.
When you iterate over a `List<T>` directly, the compiler uses this struct on the stack, avoiding heap allocations.
However, if you cast that same list to an `IEnumerable<T>`, the compiler is forced to use the interface members.
This results in the struct being boxed and every call to `MoveNext` or `Current` becoming a virtual interface call.

```csharp
private static bool AnyList<T>(List<T> source, Func<T, bool> predicate)
{
    // List<T>.Enumerator (a struct) is used directly, avoiding boxing.
    foreach (var item in source)
    {
        if (predicate(item))
            return true;
    }
    return false;
}

private static bool AnyEnumerable<T>(IEnumerable<T> source, Func<T, bool> predicate)
{
    // IEnumerable<T>.Enumerator is used, which may cause boxing and virtual calls.
    foreach (var item in source)
    {
        if (predicate(item))
            return true;
    }
    return false;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-and-ienumerable-69958922/?t=10)

Historically, before generic interfaces were introduced, boxing allocations occurred on every single iteration because only non-generic versions of the interfaces existed.
While modern .NET has improved this significantly, the performance gap between concrete type iteration and interface-based iteration remains a consideration for high-performance code.

In .NET 10 and later, a feature called "enumeration de-abstraction" allows the JIT compiler to identify well-known collection types even when they are hidden behind an `IEnumerable<T>` abstraction.
This allows the runtime to devirtualize the calls and elide the boxing that would otherwise occur.

Because `foreach` is pattern-based, you can implement it on custom types without ever implementing `IEnumerable`.
This is useful for specialized types where you want to provide iteration capabilities without the overhead or requirements of the full interface.

```csharp
static void ForeachOverRange(Range range)
{
    // This works because Range defines GetEnumerator(), even without IEnumerable.
    foreach (var value in range)
    {
        Console.WriteLine(value);
    }
}

readonly struct Range(int start, int end)
{
    public RangeEnumerator GetEnumerator() => new(start, end);
}

struct RangeEnumerator(int start, int end)
{
    private int _current = start - 1;

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= end;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/foreach-and-ienumerable-69958922/?t=40)

## 7. The Cost of Boxed Iterators

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/the-cost-of-boxed-iterators-69958923/) · 1:12

### Summary

This lesson explores the performance overhead associated with iterating over collections through interfaces like IEnumerable<T> compared to concrete types like List<T>.
While List<T> provides a struct-based enumerator to avoid heap allocations, casting the list to an interface forces this struct to be boxed in versions of .NET prior to .NET 10.
Understanding this "boxed iterator" cost is crucial for high-performance applications where frequent allocations can lead to significant GC pressure and reduced throughput, though .NET 10 introduces optimizations to elide these allocations through enumeration de-abstraction.

### Key concepts

- **Struct Enumerators**: `List<T>` defines a nested struct `Enumerator` to provide allocation-free iteration during a `foreach` loop.
- **Interface Boxing**: Accessing a list via `IEnumerable<T>` requires the `GetEnumerator()` method to return the `IEnumerator<T>` interface, which causes the underlying struct to be boxed on the managed heap.
- **Performance Impact**: In high-frequency or nested loops, boxed iterators can generate gigabytes of garbage per minute, severely impacting application throughput.
- **Runtime Differences**: While .NET 8 and .NET Framework 4.8 exhibit boxing behavior, .NET 10 introduces "enumeration de-abstraction" to devirtualize calls and elide boxing.
- **Benchmarking with MemoryDiagnoser**: Using BenchmarkDotNet's `MemoryDiagnoser` is essential for identifying hidden heap allocations in iteration logic.

### Lesson notes

The performance cost of boxed iterators is demonstrated using a BenchmarkDotNet suite.
The benchmark compares a manual `for` loop (the baseline) against two `foreach` implementations: one using a concrete `List<T>` and another using the `IEnumerable<T>` interface.
The suite is configured to run across .NET Framework 4.8, .NET 8, and .NET 10 to observe how runtime optimizations affect allocation behavior.

```csharp
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class ForeachBenchmarks
{
    private readonly List<int> _list = [1, 2, 3, 42];

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] == 42)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool EnumerateAsList()
        => AnyList(_list, x => x == 42);

    [Benchmark]
    public bool EnumerateAsIEnumerable()
        => AnyEnumerable(_list, x => x == 42);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-cost-of-boxed-iterators-69958923/?t=10)

The difference in allocation behavior stems from how the compiler and runtime handle the `foreach` loop.
When the source is a concrete `List<T>`, the compiler calls the non-virtual `GetEnumerator()` method, which returns a `List<T>.Enumerator` struct.
This struct remains on the stack, resulting in zero heap allocations.
However, when the source is typed as `IEnumerable<T>`, the compiler must call the interface method, which returns an `IEnumerator<T>`.
This requires the struct to be boxed on the managed heap.

```csharp
private static bool AnyList<T>(List<T> source, Func<T, bool> predicate)
{
    // List<T>.Enumerator is used
    foreach (var item in source)
    {
        if (predicate(item))
            return true;
    }
    return false;
}

private static bool AnyEnumerable<T>(IEnumerable<T> source, Func<T, bool> predicate)
{
    // IEnumerable<T>.Enumerator is used
    foreach (var item in source)
    {
        if (predicate(item))
            return true;
    }
    return false;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-cost-of-boxed-iterators-69958923/?t=25)

In .NET 8 and .NET Framework 4.8, the `EnumerateAsIEnumerable` benchmark shows heap allocations due to this boxing.
While a single allocation might seem negligible, it becomes a significant bottleneck in nested loops or high-traffic services.
Real-world profiling of production services has revealed scenarios where boxed iterators generated gigabytes of allocations per minute, significantly degrading overall throughput.

Notably, .NET 10 introduces "enumeration de-abstraction."
This JIT optimization allows the runtime to recognize well-known collection types behind the `IEnumerable<T>` interface, devirtualize the `GetEnumerator` call, and elide the boxing allocation entirely, bringing the performance of interface-based iteration in line with concrete type iteration.

## 8. Enumerable De-Abstraction in .NET 10

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/enumerable-de-abstraction-in-dotnet-10-69958924/) · 2:05

### Summary

This lesson explores the significant performance improvements in .NET 10 regarding enumerable de-abstraction, demonstrating how it overcomes the overhead and boxing allocations found in .NET 8.
By leveraging the JIT compiler to devirtualize interface calls and inline code when underlying types are known, .NET 10 achieves performance parity with concrete types.
The optimization process involves a multi-step strategy using Profile Guided Optimization (PGO) to identify runtime types, region-based cloning for optimized code paths, and stack allocation of enumerators to eliminate virtual calls and heap allocations entirely.

### Key concepts

- Enumerable De-abstraction
- Zero-cost abstractions
- JIT devirtualization
- Boxing allocations
- .NET 10 Performance
- Profile Guided Optimization (PGO)
- Iterator Inlining
- Region-based Cloning
- Escape Analysis
- Stack Allocation
- Field Promotion

### Lesson notes

#### Performance Comparison: .NET 8 vs. .NET 10

In .NET 8, there is a measurable performance gap between iterating over a concrete collection and iterating over an `IEnumerable<T>`.
While a `foreach` loop over a `List<T>` is efficient because it uses a struct enumerator, using the `IEnumerable<T>` interface introduces overhead.
This is primarily due to boxing allocations—where the struct enumerator must be boxed to be returned as an `IEnumerator<T>`—and the cost of virtual interface calls.

In .NET 10, the performance numbers for these different iteration methods are roughly the same.
This improvement is due to a major feature called **de-abstraction**.

#### Enumerable De-abstraction

The goal of de-abstraction is to achieve "zero-cost abstractions," ensuring there is no performance penalty when using interfaces or delegates.
This feature allows the JIT (Just-In-Time) compiler to identify the actual concrete type behind an interface at runtime.

When the JIT determines that an interface like `IEnumerable<T>` is actually a `List<T>`, it can devirtualize the calls and emit highly optimized code tailored to that specific type.
This is the same mechanism used for delegate inlining, where the JIT compiler fully inlines the logic to eliminate call overhead.

The following benchmark illustrates the scenarios where .NET 10's de-abstraction eliminates the traditional penalties associated with interface-based enumeration:

```csharp
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class ForeachBenchmarks
{
    private readonly List<int> _list = [1, 2, 3, 42];

    [Benchmark(Baseline = true)]
    public bool ManualLoop()
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] == 42)
                return true;
        }
        return false;
    }

    [Benchmark]
    public bool EnumerateAsList()
        => AnyList(_list, x => x == 42);

    [Benchmark]
    public bool EnumerateAsIEnumerable()
        => AnyEnumerable(_list, x => x == 42);

    private static bool AnyList<T>(List<T> source, Func<T, bool> predicate)
    {
        // List<T>.Enumerator is used
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }

    private static bool AnyEnumerable<T>(IEnumerable<T> source, Func<T, bool> predicate)
    {
        // IEnumerable<T>.Enumerator is used
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/enumerable-de-abstraction-in-dotnet-10-69958924/?t=0)

#### The Mechanics of De-abstraction

Enumerable de-abstraction is not a single feature but a combination of several JIT compiler optimizations working in tandem.
The process follows a specific sequence:

1.  **Profile Guided Optimization (PGO):** The JIT tracks the actual concrete type passed to a method at runtime.
2.  **Inlining:** The JIT attempts to inline the iterator. This is a critical step; if inlining fails, no further de-abstraction optimizations are possible.
3.  **Escape Analysis:** The compiler verifies that the enumerator remains local to the method and does not "escape" (e.g., by being passed to another method or assigned to a field).
4.  **Region-based Cloning:** If the enumerator is local, the JIT duplicates the code block to create a specialized path for that specific type. This is why the process of duplicating the logic for a specific enumerator type is referred to as region-based cloning.
5.  **Stack Allocation and Field Promotion:** Finally, the JIT ensures the enumerator can be allocated on the stack rather than the heap, and its fields are promoted to local variables to further improve performance.

Conceptually, the JIT transforms a generic `IEnumerable` loop into a specialized version that avoids virtual calls, similar to the following logic:

```csharp
public static void ForEachImpl_JIT(IEnumerable<int> sequence)
{
    using IEnumerator<int> enumerator = sequence.GetEnumerator();
    // Each loop requires two virtual calls: MoveNext and Current

    if (enumerator is List<int>.Enumerator listE)
    {
        // Special casing for List<int>.Enumerator to avoid virtual calls
        while (listE.MoveNext())
        {
            var item = listE.Current;
        }
    }
    else
    {
        // Fallback to the general case
        while (enumerator.MoveNext())
        {
            var item = enumerator.Current;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/enumerable-de-abstraction-in-dotnet-10-69958924/?t=70)

#### Observing JIT Behavior

To inspect these optimizations in practice, you can apply the disassembly attribute in Benchmark.NET.
This allows you to analyze the generated assembly code and observe how the JIT handles different enumerator types.

#### Mutable Structs and Safety

While these optimizations provide significant performance gains, they often involve enumerators implemented as mutable structs.
It is important to remember that mutable structs can be dangerous if their state is managed incorrectly, a topic that requires careful attention when dealing with low-level enumeration mechanics.

## 9. Iterator as a Mutable Struct

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-mutable-struct-69958925/) · 0:48

### Summary

This lesson demonstrates a critical pitfall when using mutable structs, such as List<int>.Enumerator, as readonly fields in C#.
Because the compiler creates a defensive copy of a readonly struct whenever a potentially mutating member is accessed, the internal state of the iterator is never updated on the original field.
This behavior results in logic errors like infinite loops, highlighting why mutable structs should be handled with caution and why standard foreach loops are generally preferred for collection consumption.

### Key concepts

* **Mutable Structs**: Structs that change their internal state, such as the enumerator returned by `List<T>.GetEnumerator()`.
* **Defensive Copies**: A compiler mechanism that copies a `readonly` struct field before calling its members to ensure the original field remains unchanged.
* **Readonly Field Pitfalls**: Using `readonly` with mutable structs can lead to unexpected behavior because state changes are applied to a temporary copy rather than the actual field.
* **Enumerator Mechanics**: How manual iteration using `MoveNext()` and `Current` interacts with the C# memory model.

### Lesson notes

When implementing a class that manages its own iteration state, it is common to store an enumerator in a field.
In the following example, a `Driver` class takes a list of integers and stores its enumerator in a `readonly` field.
The `Drain` method is intended to iterate through the collection and print its values.

```csharp
var driver = new Driver([1, 2, 3]);
driver.Drain();

sealed class Driver
{
    private readonly List<int>.Enumerator _enumerator;

    public Driver(List<int> source)
    {
        _enumerator = source.GetEnumerator();
    }

    public void Drain()
    {
        var safety = 0;
        while (_enumerator.MoveNext())
        {
            Console.WriteLine(_enumerator.Current);

        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-mutable-struct-69958925/?t=10)

While this code appears correct, it contains a significant bug.
The `List<int>.Enumerator` is a mutable struct.
Because the `_enumerator` field is marked as `readonly`, the C# compiler enforces the `readonly` contract by creating a defensive copy of the struct every time a member (like `MoveNext()`) is accessed.

In the `while` loop, the compiler generates code that copies `_enumerator` to a hidden local variable and calls `MoveNext()` on that copy.
The copy's state is updated, but the original `_enumerator` field remains unchanged.
Consequently, the next iteration of the loop copies the original (unmoved) enumerator again, leading to an infinite loop where the first element (or the default value) is processed repeatedly.

To observe this behavior safely, a safety check can be added to break the loop after a certain number of iterations.
Without this check, the program would hang or print zeros indefinitely.

```csharp
sealed class Driver
{
    private readonly List<int>.Enumerator _enumerator;

    public Driver(List<int> source)
    {
        _enumerator = source.GetEnumerator();
    }

    public void Drain()
    {
        var safety = 0;
        while (_enumerator.MoveNext())
        {
            Console.WriteLine(_enumerator.Current);

#region Safety
            if (++safety > 10)
            {
                Console.WriteLine("safety break: stuck in a loop");
                return;
            }
#endregion Safety
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-as-a-mutable-struct-69958925/?t=40)

This scenario serves as a reminder of two important principles in C# development:
1. **Mutable structs are dangerous**: They are difficult to manage correctly, especially when combined with `readonly` modifiers.
2. **Prefer foreach**: High-level constructs like `foreach` handle the complexities of enumerator state and lifecycle automatically, avoiding the manual pitfalls of defensive copying.

## 10. Summary

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/) · 0:58

### Summary

This lesson summarizes the mechanics of iteration in C#, focusing on the implementation of the foreach loop and the Iterator design pattern.
It explores the state machine nature of enumerators, the performance benefits of duck typing, and the advancements in .NET 10 that eliminate abstraction costs.
Additionally, it highlights the risks associated with storing mutable struct enumerators in readonly contexts, which can lead to silent infinite loops due to defensive copying.

### Key concepts

*   `IEnumerable<T>` and `IEnumerator<T>` interfaces.
*   Enumerator state machine lifecycle (Invalid, Iterating, Finished).
*   Duck typing (pattern-based) requirements for the `foreach` loop.
*   Performance optimization via struct-based enumerators to avoid heap allocations.
*   .NET 10 Enumeration De-abstraction and JIT devirtualization.
*   The danger of mutable struct enumerators in `readonly` fields.

### Lesson notes

#### The Iterator Pattern and State Machines

Iteration in C# is built upon two fundamental interfaces: `IEnumerable<T>` and `IEnumerator<T>`.
An enumerator functions as a state machine.
When an enumerator is first created, its state is invalid; you cannot access the `Current` property immediately.
You must call `MoveNext()` at least once to advance the state to the first element.
If `MoveNext()` returns `false`, the enumeration is complete, and the state becomes invalid again.

```csharp
List<int> list = [1, 2, 3];
var e = list.GetEnumerator();

// 0, but technically, undefined
Console.WriteLine(e.Current);

e.MoveNext(); // True
Console.WriteLine(e.Current); // 1

e.MoveNext(); // True
Console.WriteLine(e.Current); // 2

e.MoveNext(); // True
Console.WriteLine(e.Current); // 3

e.MoveNext(); // False
// 0, but technically, undefined
Console.WriteLine(e.Current);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/?t=0)

To ensure robust implementations, the enumerator should explicitly throw an `InvalidOperationException` if `Current` is accessed before the first `MoveNext()` or after `MoveNext()` has returned `false`.

```csharp
readonly struct StatefulSequence
{
    public Enumerator GetEnumerator() => new([10, 20, 30]);

    public struct Enumerator
    {
        private readonly int[] _items;
        private int _index;

        public Enumerator(int[] items)
        {
            _items = items;
            _index = -1;
        }

        public int Current
        {
            get
            {
                if (_index < 0)
                    throw new InvalidOperationException("Current is invalid before the first MoveNext().");

                if (_index >= _items.Length)
                    throw new InvalidOperationException("Current is invalid after MoveNext() returned false.");

                return _items[_index];
            }
        }

        public bool MoveNext()
        {
            if (_index < _items.Length)
                _index++;

            return _index < _items.Length;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/?t=12)

#### Duck Typing and Performance

The `foreach` loop in C# uses duck typing, meaning it does not strictly require a type to implement `IEnumerable` or `IEnumerable<T>`.
Instead, it looks for a `GetEnumerator()` method that returns an object with a `MoveNext()` method and a `Current` property.
This pattern-based approach is used for performance reasons: it allows collections like `List<T>` to return a struct-based enumerator, avoiding a heap allocation for every loop.

```csharp
using System.Collections;

ForeachOverList(new List<int> { 1, 2, 3 });
Console.WriteLine();
ForeachOverRange(new Range(1, 3));

static void ForeachOverList(List<int> list)
{
    Console.WriteLine("=== List<T> uses its enumerator ===");
    foreach (var value in list)
    {
        Console.WriteLine(value);
    }

    List<int>.Enumerator listEnumerator = list.GetEnumerator();
}

static void ForeachOverRange(Range range)
{
    Console.WriteLine("=== Pattern-based foreach ===");

    foreach (var value in range)
    {
        Console.WriteLine(value);
    }
}

readonly struct Range(int start, int end)
{
    public RangeEnumerator GetEnumerator() => new(start, end);
}

struct RangeEnumerator(int start, int end)
{
    private int _current = start - 1;

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= end;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/?t=27)

#### .NET 10 Enumeration De-abstraction

Historically, casting a collection to `IEnumerable<T>` forced the use of interface-based iteration, which involved boxing the struct enumerator and using virtual calls.
In .NET 10, the JIT compiler introduced "enumeration de-abstraction."
This allows the JIT to recognize well-known collection types behind an `IEnumerable<T>` interface, devirtualize the calls, and elide the heap allocation, bringing the performance of interface-based iteration close to that of concrete type iteration.

```csharp
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net48)]
[ShortRunJob(RuntimeMoniker.Net80)]
[ShortRunJob(RuntimeMoniker.Net10_0)]
public class ForeachBenchmarks
{
    private readonly List<int> _list = [1, 2, 3, 42];

    [Benchmark]
    public bool EnumerateAsList()
        => AnyList(_list, x => x == 42);

    [Benchmark]
    public bool EnumerateAsIEnumerable()
        => AnyEnumerable(_list, x => x == 42);

    private static bool AnyList<T>(List<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item))
                return true;
        }
        return false;
    }

    private static bool AnyEnumerable<T>(IEnumerable<T> source, Func<T, bool> predicate)
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

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/?t=38)

#### The Mutable Struct Trap

Because many enumerators (like `List<T>.Enumerator`) are mutable structs, they are susceptible to issues in `readonly` contexts.
If a struct enumerator is stored in a `readonly` field, the compiler will create a defensive copy of the field every time a member (like `MoveNext()`) is called.
Consequently, the state of the original field never changes, which can result in an infinite loop where the same element is processed repeatedly.

```csharp
var driver = new Driver([1, 2, 3]);
driver.Drain();

sealed class Driver
{
    private readonly List<int>.Enumerator _enumerator;

    public Driver(List<int> source)
    {
        _enumerator = source.GetEnumerator();
    }

    public void Drain()
    {
        var safety = 0;
        while (_enumerator.MoveNext())
        {
            Console.WriteLine(_enumerator.Current);

            if (++safety > 10)
            {
                Console.WriteLine("safety break: stuck in a loop");
                return;
            }
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958926/?t=51)

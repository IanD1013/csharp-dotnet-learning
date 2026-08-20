# Mastering Iterator Blocks

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 13
> 11 lessons · ~11:34
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958927/) | 0:25 | [↓](#1-overview) |
| 2 | [Manual Iterator Implementation](https://dometrain.com/take/course/mastering-csharp-3256129/manual-iterator-implementation-69958930/) | 0:28 | [↓](#2-manual-iterator-implementation) |
| 3 | [Refactor To Use Iterator Blocks](https://dometrain.com/take/course/mastering-csharp-3256129/refactor-to-use-iterator-blocks-69958933/) | 1:09 | [↓](#3-refactor-to-use-iterator-blocks) |
| 4 | [Iterator Block Execution Semantics](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-block-execution-semantics-69958936/) | 1:08 | [↓](#4-iterator-block-execution-semantics) |
| 5 | [Lowered C# Code for Generated State Machines](https://dometrain.com/take/course/mastering-csharp-3256129/lowered-csharp-code-for-generated-state-machines-69958939/) | 1:03 | [↓](#5-lowered-c-code-for-generated-state-machines) |
| 6 | [State Machine Under the Hood](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-under-the-hood-69958942/) | 1:44 | [↓](#6-state-machine-under-the-hood) |
| 7 | [State Machine Allocations](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/) | 2:12 | [↓](#7-state-machine-allocations) |
| 8 | [Benchmarking State Machine Allocations](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-state-machine-allocations-69958947/) | 0:00 | [↓](#8-benchmarking-state-machine-allocations) |
| 9 | [De-Abstracting Iterator Blocks in .NET 10](https://dometrain.com/take/course/mastering-csharp-3256129/de-abstracting-iterator-blocks-in-dotnet-10-69958950/) | 1:34 | [↓](#9-de-abstracting-iterator-blocks-in-net-10) |
| 10 | [Error Handling in Iterator Block](https://dometrain.com/take/course/mastering-csharp-3256129/error-handling-in-iterator-block-69958951/) | 1:01 | [↓](#10-error-handling-in-iterator-block) |
| 11 | [Resources in Iterator Blocks](https://dometrain.com/take/course/mastering-csharp-3256129/resources-in-iterator-blocks-69958952/) | 0:50 | [↓](#11-resources-in-iterator-blocks) |

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958927/) · 0:25

### Summary

Iterator blocks are a fundamental C# feature that simplify the creation of lazy sequences by using the yield return keyword.
The compiler transforms these blocks into a state machine that implements the IEnumerable<T> and IEnumerator<T> interfaces, handling the complex logic of suspending and resuming execution.
This lesson explores the underlying mechanics of these state machines, their performance characteristics—including heap allocations and .NET 10 optimizations—and the specific patterns required for robust error handling and resource management in deferred execution scenarios.

### Key concepts

- Iterator blocks use yield return and yield break to produce sequences lazily.
- The C# compiler transforms these blocks into a state machine class implementing IEnumerable<T> and IEnumerator<T>.
- Execution is deferred; code inside the block only runs when the sequence is enumerated via MoveNext().
- Performance overhead includes heap allocations for the state machine, though .NET 10 introduces optimizations for inlining.
- Eager validation is required for parameters because exceptions inside the block are deferred until enumeration.
- try/finally blocks and using statements ensure resource cleanup, triggered by the Dispose() method called by foreach.

### Lesson notes

Iterator blocks serve as the foundation for LINQ in C#.
By using the yield return keyword, developers can define how a sequence is produced without manually implementing the complex state management required by IEnumerator<T>.
A common use case is implementing a Select method that transforms elements of a source sequence:

```csharp
public static IEnumerable<TTarget> Select<TSource, TTarget>(
    IEnumerable<TSource> source, Func<TSource, TTarget> selector)
{
    foreach (var item in source)
        yield return selector(item);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958927/?t=10)

#### State Machine Transformation

When the compiler encounters an iterator block, it transforms the method into a private sealed class that implements both IEnumerable<T> and IEnumerator<T>.
This class acts as a state machine, maintaining the current state of execution, the current value, and any local variables as fields.
The logic of the method body is moved into the MoveNext() method, which uses a switch statement to jump to the correct resume point based on the current state (e.g., state 0 for the start, state 1 for the first suspension point).

The Current property is only defined between a successful MoveNext() call and the subsequent call to MoveNext() or Dispose().
Before the first MoveNext() and after the sequence ends, the value of Current is technically undefined, though in practice it often returns the default value of the type or the last yielded element.

#### Performance and Allocations

Because the compiler-generated state machine is a class, each enumeration typically results in a heap allocation.
If a cold iterator is consumed twice, two separate state machine objects are allocated.
However, for the first enumeration, the state machine often doubles as both the IEnumerable<T> and its own IEnumerator<T>, which can avoid a second allocation.

Performance varies across runtimes.
While traditional iterators involve virtual calls for MoveNext() and Current, .NET 10 introduces optimizations that can de-abstract simple iterators and inline these calls.
This narrows the performance gap between custom iterators and optimized collections like List<T>.

#### Error Handling and Eager Validation

Error handling in iterator blocks is unique because of deferred execution.
Validation logic placed inside the iterator block does not execute when the method is called; instead, it is deferred until the first call to MoveNext().
This means a try/catch block around the call that builds the query will not catch validation exceptions.

To ensure immediate validation, the "eager validation" pattern is used.
A standard method performs the necessary checks and then returns the result of a private iterator helper method:

```csharp
static IEnumerable<int> Generate_Eager(int seed)
{
    // Validation happens immediately at the call site
    ArgumentOutOfRangeException.ThrowIfNegative(seed);
    return IteratorCore(seed);

    static IEnumerable<int> IteratorCore(int s)
    {
        yield return s;
        yield return s + 1;
    }
}
```

#### Resource Management

Iterator blocks can own IDisposable resources.
A try/finally block or a using statement within the iterator ensures that resources are released when enumeration completes or is terminated early.
The compiler maps the finally block to the Dispose() method of the generated state machine.
Because foreach loops are compiled to call Dispose() in their own finally block, cleanup is guaranteed even if the loop is exited via a break or an exception.

```csharp
static IEnumerable<int> Generate()
{
    try
    {
        Console.WriteLine("Acquiring resource");
        yield return 1;
        yield return 2;
    }
    finally
    {
        Console.WriteLine("Releasing resource");
    }
}
```

## 2. Manual Iterator Implementation

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/manual-iterator-implementation-69958930/) · 0:28

### Summary

Implementing custom iteration in C# requires manually creating a state machine by implementing the IEnumerator<T> and IEnumerable<T> interfaces.
This process involves managing a cursor, typically an index, and implementing the MoveNext, Current, Reset, and Dispose members.
While this manual approach provides full control, it requires significant boilerplate even for simple collections, leading to the more efficient alternative of using iterator blocks with the yield keyword.

### Key concepts

- Manual IEnumerator<T> implementation.
- State management via a cursor (e.g., _index).
- The contract of MoveNext() and Current.
- Implementing both generic (IEnumerator<T>) and non-generic (IEnumerator) interfaces.
- The transition from manual state machines to iterator blocks.

### Lesson notes

To implement an iterator manually for a custom collection like MyList<T>, you must implement the IEnumerable<T> interface.
This requires providing a GetEnumerator method that returns an object implementing IEnumerator<T>.

The manual implementation involves creating a nested class (often named Enumerator) that tracks the current state of the iteration.
This class maintains a private field, such as _index, to keep track of the current position.
By convention, the index starts at -1, which represents the position before the first element in the sequence.

The MoveNext method is responsible for advancing the cursor.
It increments the index and returns a boolean indicating whether the new position is valid.
The Current property then provides access to the element at that index.
It is important to note that Current is technically undefined until MoveNext has been called at least once and has returned true.

Additionally, the implementation must satisfy both the generic IEnumerator<T> and the non-generic IEnumerator interfaces, which includes providing a Reset method to return the cursor to its initial state and a Dispose method for cleanup.

```csharp
public sealed class MyList<T> : IReadOnlyList<T>
{
    private readonly T[] _items;

    public MyList(T[] items) => _items = items;

    public int Count => _items.Length;
    public T this[int index] => _items[index];

    // foreach binds here
    public IEnumerator<T> GetEnumerator()
        => new Enumerator(_items);
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    // Hand-written state machine
    private sealed class Enumerator : IEnumerator<T>
    {
        private readonly T[] _items;

        // -1 means "before the first element";
        // Current is invalid until MoveNext is called
        private int _index = -1;

        public Enumerator(T[] items) => _items = items;

        // Defined only after successful MoveNext call
        public T Current => _items[_index];
        object? IEnumerator.Current => Current;

        // Advance the cursor. False if the sequence is ended
        public bool MoveNext() => ++_index < _items.Length;

        // Resets the state of the enumerator
        public void Reset() => _index = -1;

        public void Dispose() { } // No op
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/manual-iterator-implementation-69958930/?t=10)

Even for a simple wrapper around an array, the manual implementation requires a significant amount of code.
For more complex scenarios, such as iterating over trees or graphs, the state management becomes much more difficult to implement correctly.
This complexity is why C# provides iterator blocks, which allow you to use the yield return statement to let the compiler generate the state machine for you.

## 3. Refactor To Use Iterator Blocks

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/refactor-to-use-iterator-blocks-69958933/) · 1:09

### Summary

This lesson demonstrates how to simplify collection iteration by replacing a manually implemented IEnumerator state machine with an iterator block.
By using the yield return keyword, the C# compiler automatically generates the necessary state machine, significantly reducing boilerplate code while maintaining the same functionality for foreach loops.
The lesson covers the execution flow of iterator blocks, including how state is preserved between calls to MoveNext and how the iteration concludes.

### Key concepts

- Iterator Blocks: Methods, properties, or indexers using `yield return` to produce sequences.
- State Machine Generation: The compiler automatically generates the underlying `IEnumerator` implementation when `yield return` is present.
- Execution Pausing: `yield return` suspends method execution and preserves the current state of the iterator.
- Execution Resumption: `MoveNext` resumes execution from the last `yield return` point.
- Iteration Completion: Reaching the end of the block or using `yield break` causes `MoveNext` to return `false`.

### Lesson notes

The lesson begins by examining a manual implementation of a collection that implements `IReadOnlyList<T>`.
To support iteration via `foreach`, the class must provide a `GetEnumerator` method.
In a manual implementation, this requires a nested class that implements `IEnumerator<T>`, acting as a state machine to track the current index and handle navigation logic.

```csharp
private sealed class Enumerator : IEnumerator<T>
{
    private readonly T[] _items;

    // -1 means "before the first element";
    // Current is invalid until MoveNext is called
    private int _index = -1;

    public Enumerator(T[] items) => _items = items;

    // Defined only after successful MoveNext call
    public T Current => _items[_index];
    object? IEnumerator.Current => Current;

    // Advance the cursor. False if the sequence is ended
    public bool MoveNext() => ++_index < _items.Length;

    // Resets the state of the enumerator
    public void Reset() => _index = -1;

    public void Dispose() { } // No op
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/refactor-to-use-iterator-blocks-69958933/?t=10)

This manual approach involves significant boilerplate, including managing an internal index, implementing `MoveNext` to advance the cursor, and providing the `Current` property.
This entire implementation can be refactored using an iterator block.

```csharp
// foreach binds here
public IEnumerator<T> GetEnumerator()
{
    // Returning each element of the underlying
    // array one by one.
    foreach (var item in _items)
    {
        yield return item;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/refactor-to-use-iterator-blocks-69958933/?t=20)

By using the `yield return` keyword, the compiler is forced to transform the method into a state machine automatically.
It is important to note that it is the presence of `yield return` within the method body, rather than the return type itself, that triggers this transformation.

When the code reaches a `yield return` statement, execution pauses and the current state is preserved as the state of the iterator.
When `MoveNext` is called on the iterator (typically by a `foreach` loop), execution resumes from that exact point until the next `yield return` is executed or the end of the iteration is reached.

If the execution reaches the end of the iterator block or encounters a `yield break` statement, `MoveNext` will return `false`, indicating that the iteration is finished.

## 4. Iterator Block Execution Semantics

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-block-execution-semantics-69958936/) · 1:08

### Summary

Iterator blocks in C# are executed lazily, meaning that calling an iterator method does not immediately execute any code within its body.
Instead, execution only begins or resumes when the MoveNext() method is called on the resulting enumerator.
The C# compiler implements this behavior by transforming the iterator block into a state machine, which tracks the current execution state and preserves local variables across suspension points marked by yield return.

### Key concepts

- **Lazy Execution**: The body of an iterator block is not executed when the method is called; it only runs when the enumerator is advanced.
- **MoveNext Trigger**: Execution of the iterator body starts or resumes only upon calling MoveNext().
- **Current Property State**: The value of Current is undefined before the first MoveNext() call and after MoveNext() returns false.
- **State Machine Transformation**: The compiler generates a private class to maintain the state of the iterator, implementing both IEnumerable<T> and IEnumerator<T>.
- **Suspension Points**: Each yield return statement serves as a point where the state machine saves its state and yields control back to the caller.

### Lesson notes

Iterator blocks do not execute like standard methods.
When an iterator method is called, it returns an enumerator (or enumerable) without executing any of the code inside the method body.
This is known as lazy execution.

To begin execution, MoveNext() must be called.
Before this first call, the Current property of the iterator is technically undefined (though in practice it often returns the default value of the type).
Each call to MoveNext() executes the code until it reaches a yield return or the end of the block.

```csharp
static IEnumerable<int> Iterator()
{
    Console.WriteLine("Step 1");
    yield return 1;

    Console.WriteLine("Step 2");
    yield return 2;

    Console.WriteLine("Step 3");
}

var iterator = Iterator().GetEnumerator();

// Technically, undefined. In practice, default(T).
Console.WriteLine(iterator.Current);

iterator.MoveNext(); // returns true
Console.WriteLine(iterator.Current); // prints 1

iterator.MoveNext(); // returns true
Console.WriteLine(iterator.Current); // prints 2

iterator.MoveNext(); // returns false
// Technically, undefined. In practice, last element.
Console.WriteLine(iterator.Current);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-block-execution-semantics-69958936/?t=10)

When MoveNext() is called for the first time, "Step 1" is printed, and the method yields the value 1.
The second call to MoveNext() resumes execution immediately after the first yield return, prints "Step 2", and yields 2.
The third call prints "Step 3" and returns false, indicating the end of the sequence.
Once MoveNext() returns false, the state of Current is again undefined, and subsequent calls to MoveNext() will continue to return false.

#### The Compiler-Generated State Machine

The C# compiler achieves this resumable execution by generating a private class that acts as a state machine.
This class implements the necessary interfaces and uses an internal state field to track progress.

```csharp
internal static class Generated
{
    public static IEnumerable<int> Iterator()
        => new IteratorStateMachine(-2);

    private sealed class IteratorStateMachine :
        IEnumerable<int>,
        IEnumerator<int>
    {
        private int __state;
        private int __current;
        private readonly int l__initialThreadId;

        public IteratorStateMachine(int state)
        {
            __state = state;
            l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        public bool MoveNext()
        {
            switch (__state)
            {
                case 0:
                    __state = -1;
                    Console.WriteLine("Step 1");
                    __current = 1;
                    __state = 1; // next resume point
                    return true;

                case 1:
                    __state = -1;
                    Console.WriteLine("Step 2");
                    __current = 2;
                    __state = 2;
                    return true;

                case 2:
                    __state = -1;
                    Console.WriteLine("Step 3");
                    return false;

                default:
                    return false;
            }
        }

        public int Current => __current;
        object IEnumerator.Current => __current;

        public void Reset() => throw new NotSupportedException();

        public void Dispose() => __state = -2;

        public IEnumerator<int> GetEnumerator()
        {
            if (__state == -2 && 
                l__initialThreadId == Environment.CurrentManagedThreadId)
            {
                __state = 0;
                return this;
            }
            
            return new IteratorStateMachine(0);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/iterator-block-execution-semantics-69958936/?t=64)

The state machine uses a switch statement inside MoveNext().
Each yield return updates the __current field and the __state field before returning true.
When the method resumes, the switch jumps to the logic corresponding to the saved state.

## 5. Lowered C# Code for Generated State Machines

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/lowered-csharp-code-for-generated-state-machines-69958939/) · 1:03

This lesson explores the "lowered" C# code generated by the compiler when an iterator block is used.
It demonstrates how a simple method containing yield return statements is transformed into a complex, compiler-generated class that implements the state machine pattern.
This class manages the execution state, current value, and thread affinity to ensure the iterator behaves correctly across multiple calls and threads.

### Key concepts

- Compiler lowering of iterator methods into state machine classes.
- Implementation of `IEnumerable<T>`, `IEnumerator<T>`, and `IDisposable` by the generated type.
- State management via an internal integer field.
- Thread affinity tracking to determine instance reuse in `GetEnumerator`.
- Transformation of method logic into a `switch` statement within the `MoveNext` method.

### Lesson notes

The C# compiler performs a significant transformation on iterator methods.
When a method contains the `yield` keyword, its original body is removed and replaced with logic that instantiates a compiler-generated state machine.

Consider a standard iterator implementation and its usage:

```csharp
var enumerable = Iterator();
var iterator = enumerable.GetEnumerator();

// Technically, undefined.
// In practice, default(T).
Console.WriteLine(iterator.Current);

iterator.MoveNext(); // returns true
Console.WriteLine(iterator.Current); // 1

iterator.MoveNext();
Console.WriteLine(iterator.Current); // 2

iterator.MoveNext(); // returns false

// Technically, undefined.
// In practice, last element.
Console.WriteLine(iterator.Current);

static IEnumerable<int> Iterator()
{
    Console.WriteLine("Step 1");
    yield return 1;

    Console.WriteLine("Step 2");
    yield return 2;

    Console.WriteLine("Step 3");
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lowered-csharp-code-for-generated-state-machines-69958939/?t=10)

When this code is compiled, the `Iterator` method is lowered into a factory method.
This method creates an instance of a generated private sealed class (the state machine), passing `-2` as the initial state sentinel, and returns it to the caller.
The generated class implements `IEnumerable<int>`, `IEnumerator<int>`, and `IDisposable`.

```csharp
// The original method is replaced by a factory method
internal static IEnumerable<int> Iterator()
{
    return new IteratorStateMachine(-2);
}

// The compiler generates a private sealed class to manage state
private sealed class IteratorStateMachine :
    IEnumerable<int>,
    IEnumerable,
    IEnumerator<int>,
    IEnumerator,
    IDisposable
{
    private int __state;
    private int __current;
    private int l__initialThreadId;

    public IteratorStateMachine(int state)
    {
        this.__state = state;
        this.l__initialThreadId = Environment.CurrentManagedThreadId;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lowered-csharp-code-for-generated-state-machines-69958939/?t=25)

The generated class uses three primary fields: `__state` to track the machine's progress, `__current` to store the value for the `Current` property, and `l__initialThreadId` to remember which thread created the instance.
The logic from the original method is moved entirely into the `MoveNext` method.

In `MoveNext`, the code is organized into a `switch` statement based on the current state.
Each `yield return` represents a suspension point where the state is updated, the current value is set, and the method returns `true`.
When the method reaches its end or a `yield break`, it returns `false`.

```csharp
public bool MoveNext()
{
    switch (__state)
    {
        case 0:
            __state = -1;
            Console.WriteLine("Step 1");
            __current = 1;
            __state = 1; // next resume point
            return true;

        case 1:
            __state = -1;
            Console.WriteLine("Step 2");
            __current = 2;
            __state = 2;
            return true;

        case 2:
            __state = -1;
            Console.WriteLine("Step 3");
            return false;

        default:
            return false;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lowered-csharp-code-for-generated-state-machines-69958939/?t=40)

Another critical component is the `GetEnumerator` method.
Because the generated class implements both `IEnumerable` and `IEnumerator`, it must decide whether to return itself or a new instance.
If `GetEnumerator` is called on the same thread that created the instance and the state is still the initial sentinel (`-2`), the class reuses itself by transitioning to state `0`.
If these conditions are not met (e.g., a second enumeration or a call from a different thread), a new instance of the state machine is allocated.

```csharp
public IEnumerator<int> GetEnumerator()
{
    // Check if the instance can be reused (initial state and same thread)
    if (__state == -2 && 
        l__initialThreadId == Environment.CurrentManagedThreadId)
    {
        __state = 0;
        return this;
    }
    
    // Otherwise, create a new instance for the new enumeration
    return new IteratorStateMachine(0);
}

IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

public int Current => __current;
object IEnumerator.Current => __current;

public void Dispose() => __state = -2;
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/lowered-csharp-code-for-generated-state-machines-69958939/?t=55)

## 6. State Machine Under the Hood

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-under-the-hood-69958942/) · 1:44

### Summary

This lesson explores the internal mechanics of C# iterator blocks, detailing how the compiler transforms high-level yield return statements into a complex state machine.
It examines the generated class that implements both IEnumerable<T> and IEnumerator<T>, explaining how state fields track execution progress and how the MoveNext method uses a switch statement to resume execution at specific points.
The lesson also covers the optimization where the compiler attempts to reuse the state machine instance when GetEnumerator is called on the same thread that created the enumerable.

### Key concepts

*   **Compiler-Generated State Machine**: Iterator methods are transformed into a private sealed class that implements `IEnumerable<T>` and `IEnumerator<T>`.
*   **State Management**: A state field (`__state`) tracks the current position in the iteration, using specific values like -2 for the initial factory state and 0 for the start of enumeration.
*   **Instance Reuse**: The generated code includes logic to reuse the state machine instance if `GetEnumerator` is called on the same thread that created the enumerable, provided it is in the initial state.
*   **MoveNext Logic**: The original method body is partitioned into a switch statement within `MoveNext`, where each `yield return` represents a suspension point.
*   **Current Property**: The `Current` property stores the value yielded at the current state; its value is technically undefined before the first `MoveNext` or after the iteration completes.

### Lesson notes

When an iterator method is defined using `yield return`, the C# compiler does not execute the code immediately.
Instead, it generates a state machine class.
The original method is replaced with a factory method that instantiates this state machine with an initial state of -2.

```csharp
static IEnumerable<int> Iterator()
{
    Console.WriteLine("Step 1");
    yield return 1;

    Console.WriteLine("Step 2");
    yield return 2;

    Console.WriteLine("Step 3");
}

public bool MoveNext()
{
    switch (__state)
    {
        case 0:
            __state = -1;
            Console.WriteLine("Step 1");
            __current = 1;
            __state = 1; // next resume point
            return true;
        case 1:
            __state = -1;
            Console.WriteLine("Step 2");
            __current = 2;
            __state = 2;
            return true;
        case 2:
            __state = -1;
            Console.WriteLine("Step 3");
            return false;
        default:
            return false;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-under-the-hood-69958942/?t=10)

#### The State Machine Lifecycle

When the iterator method is called, the generated code creates the state machine and passes -2 as the original state.
This information is stored in the `__state` field.
At this point, the `__current` field is set to the default value for the type (e.g., 0 for `int`).

When `GetEnumerator` is called, the state machine checks if it can reuse the current instance.
If the state is -2 and the call is occurring on the same thread that created the instance (`l__initialThreadId == Environment.CurrentManagedThreadId`), the state is updated to 0 and the same instance is returned.
Otherwise, a new instance of the state machine is allocated with an initial state of 0.

```csharp
public static IEnumerable<int> Iterator()
    => new IteratorStateMachine(-2);

public IEnumerator<int> GetEnumerator()
{
    if (__state == -2 &&
        l__initialThreadId == Environment.CurrentManagedThreadId)
    {
        __state = 0;
        return this;
    }
    return new IteratorStateMachine(0);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-under-the-hood-69958942/?t=35)

#### Execution via MoveNext

Execution only begins when `MoveNext()` is called for the first time.
The method evaluates the `__state` field:

1.  **First Call**: If the state is 0, the first block of code executes until it hits a `yield return`. The `__current` field is set to the yielded value, and `__state` is updated to 1 to mark the next resume point. The method returns `true`.
2.  **Subsequent Calls**: When `MoveNext()` is called again, the switch statement resumes execution from the state stored previously (e.g., state 1). It executes the next block of code, updates `__current`, and sets the state to the next suspension point (e.g., state 2).
3.  **Completion**: When the final statement is reached and there are no more `yield return` statements, the state machine sets the state to -1 and returns `false`, signaling that the iteration is complete.

```csharp
var enumerable = Iterator();
var iterator = enumerable.GetEnumerator();

// Technically, undefined. In practice, default(T).
Console.WriteLine(iterator.Current);

iterator.MoveNext();
Console.WriteLine(iterator.Current); // 1

iterator.MoveNext();
Console.WriteLine(iterator.Current); // 2

iterator.MoveNext();
// Technically, undefined. In practice, the last yielded value (2).
Console.WriteLine(iterator.Current);
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-under-the-hood-69958942/?t=85)

While the `Current` property is technically undefined before the first `MoveNext()` or after the iteration ends, in practice, it typically holds the default value of the type initially and retains the last yielded value once the iteration finishes.

## 7. State Machine Allocations

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/) · 2:12

### Summary

This lesson explores the allocation behavior of iterator blocks, demonstrating how the C# compiler optimizes memory usage by reusing the generated state machine instance.
By implementing both IEnumerable<T> and IEnumerator<T> within the same class, the state machine can act as its own iterator.
This reuse is possible during sequential enumeration on the same thread, provided the previous iteration has been disposed or completed, effectively minimizing the heap allocations required for multiple passes over the same data source.

### Key concepts

- **Dual Implementation**: The compiler-generated state machine implements both `IEnumerable<T>` and `IEnumerator<T>`.
- **Instance Reuse**: The runtime attempts to reuse the same state machine instance for the iterator if specific conditions are met.
- **Thread Affinity**: Reuse is restricted to the thread that originally created the enumerable instance.
- **State Sentinel (-2)**: The state value `-2` indicates the state machine is in a "factory" or "disposed" state, making it eligible for reuse.
- **Sequential vs. Concurrent Enumeration**: Sequential enumeration allows for reuse, while concurrent enumeration forces the allocation of new state machine instances.

### Lesson notes

When using iterator blocks, the compiler generates a class to manage the state of the iteration.
A key performance characteristic of these generated classes is how they handle allocations during single or multiple iterations.
Using a benchmark with `MemoryDiagnoser`, we can observe that iterating over a source once versus twice results in the same number of allocations.

```csharp
[MemoryDiagnoser]
[ShortRunJob(RuntimeMoniker.Net10_0)]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Alloc Ratio")]
public class IteratorBenchmarks
{
    [Benchmark(Baseline = true)]
    public int IterateOnce()
    {
        var source = Iterate();
        return source.Sum();
    }

    [Benchmark]
    public int IterateOnceConsumedTwice()
    {
        var source = Iterate();
        return source.Sum() + source.Sum();
    }

    internal static IEnumerable<int> Iterate()
    {
        yield return 1;
        yield return 2;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/?t=25)

This behavior occurs because the compiler-generated state machine implements both `IEnumerable<T>` and `IEnumerator<T>`.
In many cases, the `GetEnumerator()` method returns the current instance (`this`) rather than allocating a new one.

```csharp
[CompilerGenerated]
private sealed class <Iterate>d__2 :
    IEnumerable<int>,
    IEnumerable,
    IEnumerator<int>,
    IEnumerator,
    IDisposable
{
    private int <>1__state;
    private int <>2__current;
    private int <>l__initialThreadId;

    [DebuggerHidden]
    public <Iterate>d__2(int _param1)
    {
        this.<>1__state = _param1;
        this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
    }
    // ...
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/?t=40)

We can verify this reuse by comparing the reference of the enumerable source with the reference of the iterator it produces.
If they are the same instance, `ReferenceEquals` returns true.

```csharp
var source = IteratorBenchmarks.Iterate();

var enumerator = source.GetEnumerator();
Console.WriteLine($"Equality: {ReferenceEquals(source, enumerator)}");
Console.WriteLine($"Types. Source: {source.GetType()}, Enumerator: {enumerator.GetType()}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/?t=65)

However, because iteration is a stateful process, the same instance cannot be reused if an enumeration is already in progress.
If we attempt to retrieve a second iterator while the first is still active, a new instance must be allocated, and the equality check will return false.

```csharp
var source = IteratorBenchmarks.Iterate();

var e1 = source.GetEnumerator();
var enumerator = source.GetEnumerator();

Console.WriteLine($"Equality: {ReferenceEquals(source, enumerator)}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/?t=75)

If the first iterator is disposed, the state machine is reset to a state where it can be reused.
Calling `Dispose()` on the first iterator allows the subsequent call to `GetEnumerator()` to return the original instance again.

```csharp
var source = IteratorBenchmarks.Iterate();

var e1 = source.GetEnumerator();
e1.Dispose();

var enumerator = source.GetEnumerator();

Console.WriteLine($"Equality: {ReferenceEquals(source, enumerator)}");
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/?t=90)

The logic governing this reuse is found within the `GetEnumerator` and `Dispose` methods of the generated state machine.
The runtime checks if the state is `-2` (indicating it is either newly created or recently disposed) and ensures the call is coming from the same thread that created the instance.

```csharp
public void Dispose() => __state = -2;

public IEnumerator<int> GetEnumerator()
{
    // We're either in initial state (GetEnumerator is not called)
    // or enumerator is disposed.
    // And we're called from the original thread
    if (__state == -2 &&
        l__initialThreadId == Environment.CurrentManagedThreadId)
    {
        __state = 0;
        return this;
    }

    // We can't re-use an existing instance. Creating a new one!
    return new IteratorStateMachine(0);
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/state-machine-allocations-69958945/?t=115)

## 8. Benchmarking State Machine Allocations

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-state-machine-allocations-69958947/) · 0:00

### Summary

This lesson explores the performance implications of iterator blocks, specifically focusing on heap allocations for the generated state machines in .NET 8, 9, and 10.
It demonstrates how the .NET 10 JIT compiler can optimize away these allocations through a "data abstraction" feature, though this optimization is currently sensitive to the complexity of the iterator block's implementation.
By comparing standard lists, custom wrappers, and Protobuf's RepeatedField, the lesson highlights the conditions under which iterator state machines can be stack-allocated to reduce memory traffic in high-load applications.

### Key concepts

- Heap allocation overhead of iterator state machines in .NET 8 and 9.
- .NET 10's "data abstraction" feature for optimizing iterator allocations.
- Sensitivity of JIT optimizations to iterator block complexity (e.g., for vs. foreach).
- Requirement of IEnumerator<T> return types for state machine abstraction.
- Performance impact of iterator allocations on high-load applications using Protobuf RepeatedField.

### Lesson notes

The benchmark analysis compares four distinct cases to evaluate allocation behavior across .NET versions:
1. A standard foreach loop over a List<int>.
2. A custom ReadOnlyList<T> wrapper using a for loop in its iterator block.
3. A ReadOnlyListForeach<T> wrapper using a foreach loop in its iterator block.
4. A RepeatedField<int> from the Protobuf library.

While .NET 8 and .NET 9 produce identical allocation results for these cases, .NET 10 introduces a "data abstraction" feature that can significantly improve performance by eliminating heap allocations for iterator state machines.

```csharp
public sealed class ReadOnlyList<T>(List<T> inner) : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        // Single for-loop works.
        // Two loops, or foreach loop over a list - won't!
        for(int i = 0; i < inner.Count; i++)
            yield return inner[i];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-state-machine-allocations-69958947/?t=10)

However, this JIT optimization is currently brittle and depends on the implementation details of the iterator block.
The optimization is typically successful when the method returns an IEnumerator<T>, but it fails if the return type is IEnumerable<T>.
Additionally, increasing the complexity of the iterator block body—such as adding a second loop or using a foreach loop instead of a for loop—prevents the JIT from fully abstracting the state machine.

```csharp
[Benchmark]
public int ReadOnlyListForeach()
{
    int sum = 0;
    foreach (var x in _readOnlyForeach)
        sum += x;
    return sum;
}

private readonly RepeatedField<int> _repeated = [1, 2];
[Benchmark]
public int RepeatedField()
{
    int sum = 0;
    foreach (var x in _repeated)
        sum += x;
    return sum;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-state-machine-allocations-69958947/?t=25)

These limitations are significant for real-world scenarios, such as iterating over Protobuf RepeatedField types.
In high-load applications, the repeated allocation of iterator state machines can cause substantial memory traffic.
The improvements in .NET 10 allow these allocations to be optimized away in certain cases, providing a performance boost even if the underlying implementation details are subject to change in future versions.

## 9. De-Abstracting Iterator Blocks in .NET 10

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/de-abstracting-iterator-blocks-in-dotnet-10-69958950/) · 1:34

### Summary

Iterator blocks in C# exhibit lazy execution, meaning code within the method body—including argument validation—does not execute until the resulting sequence is enumerated.
This behavior can cause exceptions to surface far from the original call site, making error handling difficult.
To ensure eager argument validation, developers should use a "de-abstracted" pattern: a standard method that performs validation and then returns the result of a separate, private iterator block, often implemented as a local function.

### Key concepts

- **Lazy Execution**: Iterator blocks do not execute any code until `MoveNext()` is called on the enumerator.
- **Deferred Exceptions**: Validation logic inside an iterator block will not throw until the sequence is consumed, potentially bypassing `try-catch` blocks at the call site.
- **Eager Validation Pattern**: Splitting a method into a validation wrapper (regular method) and an iterator core (local function).
- **Local Functions**: A common way to implement the `IteratorCore` helper within the scope of the validation method to maintain encapsulation.

### Lesson notes

#### The Problem with Lazy Validation

In a standard iterator block, the magic transformation performed by the compiler changes the execution semantics.
Because the method returns an `IEnumerable<T>`, the code inside the method body is not executed when the method is invoked.
Instead, execution only begins when the sequence is consumed (e.g., via `foreach`, `Any()`, or `ToList()`).

Consider a scenario where a method validates its arguments before yielding results.
If this method is called with invalid arguments inside a `try-catch` block, the exception will not be caught because the validation logic hasn't run yet.

```csharp
using System.Linq;

IEnumerable<int> source;

try
{
    // BUG: Generate is a lazy iterator, so the argument validation inside it
    // is deferred. Nothing throws here, so this try/catch catches nothing.
    source = Generate(-1);
}
catch (ArgumentOutOfRangeException)
{
    source = [];
}

// Processed in middle layer
IEnumerable<int> sequence = source.Where(x => x > 0);

// Consumed later on — the ArgumentOutOfRangeException surfaces HERE,
// during enumeration, far from the call that built the query.
Console.WriteLine($"IsEmpty: {!sequence.Any()}");

static IEnumerable<int> Generate(int seed)
{
    // Validation written inside an iterator block runs only on the first
    // MoveNext(), not when Generate() is called.
    ArgumentOutOfRangeException.ThrowIfNegative(seed);
    yield return seed;
    yield return seed + 1;
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/de-abstracting-iterator-blocks-in-dotnet-10-69958950/?t=10)

In the example above, the `ArgumentOutOfRangeException` is not thrown when `Generate(-1)` is called.
Instead, it is thrown when `sequence.Any()` is called.
This is often surprising to developers who expect arguments to be validated eagerly.
If the result of this method is passed through multiple layers of an application, the error may appear in a completely different subsystem, making debugging difficult.

#### Implementing Eager Validation

To ensure that arguments are validated at the moment the method is called, you must extract the iterator block into a helper method or a local function.
The outer method should be a regular method (not an iterator block) that performs the validation and then returns the enumerable produced by the helper.

```csharp
static IEnumerable<int> Generate_Eager(int seed)
{
    // A normal (non-iterator) method validates immediately, then delegates
    // to a private iterator helper. The throw now happens at the call site.
    ArgumentOutOfRangeException.ThrowIfNegative(seed);
    return IteratorCore(seed);

    static IEnumerable<int> IteratorCore(int s)
    {
        yield return s;
        yield return s + 1;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/de-abstracting-iterator-blocks-in-dotnet-10-69958950/?t=55)

By using this pattern, the `Generate_Eager` method has regular execution semantics.
When it is called, it immediately executes the `ThrowIfNegative` check.
If the check passes, it returns the iterator produced by `IteratorCore`.
This ensures that the caller receives an immediate exception if the arguments are invalid, while still maintaining the benefits of lazy evaluation for the actual data generation.

```csharp
// The error happens here immediately
IEnumerable<int> source = Generate_Eager(-1);

// These lines are never reached if the seed is invalid
IEnumerable<int> filtered = source.Where(x => x > 0);
Console.WriteLine(filtered.Any());
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/de-abstracting-iterator-blocks-in-dotnet-10-69958950/?t=70)

This "de-abstracting" approach is a best practice when the result of an iterator block is used across different subsystems, as it prevents invalid state from propagating through the system.

## 10. Error Handling in Iterator Block

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/error-handling-in-iterator-block-69958951/) · 1:01

### Summary

Iterator blocks in C# support sequential constructs like try-finally and using blocks, allowing for safe resource management within the generated state machine.
Because the compiler transforms these blocks, the execution of a finally block is tied to the disposal of the enumerator.
While a foreach loop automatically handles disposal—ensuring resources are released even during early exits or exceptions—manual enumeration requires an explicit call to Dispose() to avoid resource leaks.

### Key concepts

- Support for try-finally and using blocks within iterators.
- Resource management (acquisition and release) in stateful iteration.
- The role of IEnumerator.Dispose() in triggering finally blocks.
- Comparison between manual enumeration and foreach loop behavior.
- Implementation of disposable iterators for managed resource safety.

### Lesson notes

One of the primary advantages of iterator blocks is their ability to maintain a sequential appearance while handling complex state.
This allows developers to use standard C# constructs such as try-finally or using blocks to manage resources.
Within an iterator, you can acquire a resource, use it to yield results, and ensure its release in a finally block.

However, the execution of the finally block depends on the lifecycle of the enumerator.
If you manually consume an enumerator without disposing of it, the finally block may never execute.

```csharp
var source = Generate();
var enumerator = source.GetEnumerator();
Console.WriteLine("Running MoveNext");

enumerator.MoveNext();
if (enumerator.Current == 1)
{
    Console.WriteLine("We're done!");
    return;
}

static IEnumerable<int> Generate()
{
    try
    {
        Console.WriteLine("Acquiring resource");
        Console.WriteLine("1");
        yield return 1;
        Console.WriteLine("2");
        yield return 2;
        Console.WriteLine("3");
    }
    finally
    {
        Console.WriteLine("Releasing resource");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/error-handling-in-iterator-block-69958951/?t=10)

In the example above, the finally block is not executed because the enumerator is never disposed of after the early return.
This illustrates why the foreach loop is the preferred method for consuming iterators; the compiler-generated code for foreach includes a finally block that calls Dispose() on the enumerator.

```csharp
var source = Generate();

foreach (var element in source)
{
    if (element == 1)
    {
        Console.WriteLine("We're done!");
        return;
    }
}

static IEnumerable<int> Generate()
{
    try
    {
        Console.WriteLine("Acquiring resource");
        Console.WriteLine("1");
        yield return 1;
        Console.WriteLine("2");
        yield return 2;
        Console.WriteLine("3");
    }
    finally
    {
        Console.WriteLine("Releasing resource");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/error-handling-in-iterator-block-69958951/?t=35)

When using foreach, the finally block inside the Generate method is executed correctly even if the loop terminates early due to a return statement or an exception.
This mechanism ensures that managed resources, such as file handles or database connections, are properly released.

A practical application of this is using a using statement within an iterator to handle file I/O.
By combining this with a local function, you can perform immediate argument validation before the iterator begins execution.

```csharp
static IEnumerable<int> ParseIdsFromFile(string path)
{
    ArgumentException.ThrowIfNullOrWhiteSpace(path);

    return Iterator(path);

    static IEnumerable<int> Iterator(string path)
    {
        using var reader = File.OpenText(path);
        // Using pattern matching for null check
        while (reader.ReadLine() is { } line)
        {
            if (int.TryParse(line, out var id))
                yield return id;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/error-handling-in-iterator-block-69958951/?t=55)

Because iterator blocks are inherently disposable, failing to call Dispose() on them when they own resources can lead to resource leaks.
Always ensure that iterators are consumed in a way that guarantees disposal.

## 11. Resources in Iterator Blocks

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/resources-in-iterator-blocks-69958952/) · 0:50

### Summary

Iterator blocks are the foundational building blocks of LINQ, providing resumable execution through a compiler-generated state machine.
This mechanism allows a method to yield values one at a time, maintaining its internal state between calls to MoveNext.
However, because iterators are evaluated lazily, developers must be cautious with argument validation and resource management, ensuring that finally blocks and using statements are properly triggered through the disposal of the enumerator to prevent resource leaks.

### Key concepts

- **Resumable Execution**: Iterator blocks use a state machine to pause and resume execution at `yield return` statements.
- **State Machine Mechanics**: The `MoveNext` method advances the internal state and determines which block of code to execute next.
- **Lazy Evaluation**: Code inside an iterator block, including argument validation, does not execute until the sequence is actually enumerated.
- **Resource Ownership**: Iterators can manage resources using `try/finally` or `using` blocks, which are tied to the enumerator's lifecycle.
- **JIT Optimizations**: In simple cases, the Just-In-Time (JIT) compiler can eliminate the overhead associated with iterator blocks.

### Lesson notes

Iterator blocks are the foundational building blocks for LINQ.
Understanding how they function is essential for mastering data processing in C#.
At its core, an iterator block provides resumable execution based on a compiler-generated state machine.

```csharp
var enumerable = Iterator();

var iterator = enumerable.GetEnumerator();

// Technically, undefined.
// In practice, default(T).
Console.WriteLine(iterator.Current);

iterator.MoveNext(); // returns true

Console.WriteLine(iterator.Current); // 1

iterator.MoveNext();

Console.WriteLine(iterator.Current); // 2

iterator.MoveNext(); // returns false

// Technically, undefined.
// In practice, last element.
Console.WriteLine(iterator.Current);

static IEnumerable<int> Iterator()
{
    Console.WriteLine("Step 1");
    yield return 1;

    Console.WriteLine("Step 2");
    yield return 2;

    Console.WriteLine("Step 3");
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/resources-in-iterator-blocks-69958952/?t=0)

The generated state machine implements both `IEnumerable<T>` and `IEnumerator<T>`, allowing it to act as both the source and the iterator itself.
The `MoveNext` method is responsible for changing the internal state, which is how the machine remembers which block of code to execute upon resumption.

```csharp
using System.Collections;

namespace IteratorStateMachineDemo;

internal static class Generated
{
    // Replaces the original `static IEnumerable<int> Iterator()` method.
    public static IEnumerable<int> Iterator()
        => new IteratorStateMachine(-2);

    private sealed class IteratorStateMachine :
        IEnumerable<int>,
        IEnumerator<int>
    {
        private int __state;
        private int __current;
        private readonly int l__initialThreadId;

        public IteratorStateMachine(int state)
        {
            __state = state;
            l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        public bool MoveNext()
        {
            switch (__state)
            {
                case 0:
                    __state = -1;
                    Console.WriteLine("Step 1");
                    __current = 1;
                    __state = 1; // next resume point
                    return true;

                case 1:
                    __state = -1;
                    Console.WriteLine("Step 2");
                    __current = 2;
                    __state = 2;
                    return true;

                case 2:
                    __state = -1;
                    Console.WriteLine("Step 3");
                    return false;

                default:
                    return false;
            }
        }

        public int Current => __current;
        object IEnumerator.Current => __current;

        public void Reset() => throw new NotSupportedException();

        public void Dispose() => __state = -2;

        public IEnumerator<int> GetEnumerator()
        {
            if (__state == -2 && 
                l__initialThreadId == Environment.CurrentManagedThreadId)
            {
                __state = 0;
                return this;
            }            
            return new IteratorStateMachine(0);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/resources-in-iterator-blocks-69958952/?t=12)

While the state machine is complex, the Just-In-Time (JIT) compiler can often remove the overhead of using iterator blocks in simple scenarios.

#### Lazy Evaluation and Error Handling

One of the most critical aspects of iterators is their lazy nature.
This significantly impacts error handling.
For instance, if you perform argument validation inside an iterator block, the exception will not be thrown when the method is called.
Instead, it will be deferred until the first call to `MoveNext()`.

```csharp
using System.Linq;

IEnumerable<int> source;

try
{
    // BUG: Generate is a lazy iterator, so the argument validation inside it
    // is deferred. Nothing throws here, so this try/catch catches nothing.
    source = Generate(-1);
}
catch (ArgumentOutOfRangeException)
{
    source = [];
}

// Processed in middle layer
IEnumerable<int> sequence = source.Where(x => x > 0);

// Consumed later on — the ArgumentOutOfRangeException surfaces HERE,
// during enumeration, far from the call that built the query.
Console.WriteLine($"IsEmpty: {!sequence.Any()}");

static IEnumerable<int> Generate(int seed)
{
    // Validation written inside an iterator block runs only on the first
    // MoveNext(), not when Generate() is called.
    ArgumentOutOfRangeException.ThrowIfNegative(seed);
    yield return seed;
    yield return seed + 1;
}

#region Solution — eager validation
static IEnumerable<int> Generate_Eager(int seed)
{
    // A normal (non-iterator) method validates immediately, then delegates
    // to a private iterator helper. The throw now happens at the call site.
    ArgumentOutOfRangeException.ThrowIfNegative(seed);
    return IteratorCore(seed);

    static IEnumerable<int> IteratorCore(int s)
    {
        yield return s;
        yield return s + 1;
    }
}
#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/resources-in-iterator-blocks-69958952/?t=39)

To ensure eager validation, the validation logic should be placed in a standard method that then calls a private iterator helper method.

#### Resource Management

Iterators can own resources, such as file handles or database connections.
Because of the state machine's structure, `finally` blocks and `using` statements work as expected, but they are only executed when the iteration completes or when the consumer disposes of the enumerator.
Failure to dispose of the enumerator can lead to resource leaks.

```csharp
var source = Generate();

foreach (var element in source)
{
    if (element == 1)
    {
        Console.WriteLine("We're done!");
        return;
    }
}

static IEnumerable<int> Generate()
{
    try
    {
        Console.WriteLine("Acquiring resource");
        Console.WriteLine("1");
        yield return 1;
        Console.WriteLine("2");
        yield return 2;
        Console.WriteLine("3");
    }
    finally
    {
        Console.WriteLine("Releasing resource");
    }
}

#region ParseIdsFromFile
static IEnumerable<int> ParseIdsFromFile(string path)
{
    ArgumentException.ThrowIfNullOrWhiteSpace(path);

    return Iterator(path);

    static IEnumerable<int> Iterator(string path)
    {
        using var reader = File.OpenText(path);
        // Using pattern matching for null check
        while (reader.ReadLine() is { } line)
        {
            if (int.TryParse(line, out var id))
                yield return id;
        }
    }
}
#endregion
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/resources-in-iterator-blocks-69958952/?t=43)

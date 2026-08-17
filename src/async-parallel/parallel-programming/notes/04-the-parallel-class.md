# The Parallel class

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 4
> 5 lessons · ~51:34
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Parallel.Invoke](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/) | 17:14 | [↓](#1-parallelinvoke) |
| 2 | [Parallel.For](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/) | 8:27 | [↓](#2-parallelfor) |
| 3 | [Parallel.ForAsync](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/) | 8:24 | [↓](#3-parallelforasync) |
| 4 | [Parallel.ForEach](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/) | 11:51 | [↓](#4-parallelforeach) |
| 5 | [Parallel.ForEachAsync](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/) | 5:38 | [↓](#5-parallelforeachasync) |

---

## 1. Parallel.Invoke

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/) · 17:14

### Summary

Parallel.Invoke is a static method in the System.Threading.Tasks.Parallel class that enables the concurrent execution of multiple Action delegates.
It is a synchronous, blocking operation, meaning the calling thread is held until all provided actions have finished.
This method is particularly useful for offloading multiple independent, void-returning operations to the thread pool while providing granular control over concurrency and cancellation through ParallelOptions.
Compared to Task.WaitAll, Parallel.Invoke offers superior performance through optimizations like task inlining and simplifies error handling by automatically unwrapping AggregateException to throw the specific underlying exception.

### Key concepts

- **Parallel Action Execution**: Runs multiple `Action` delegates concurrently.
- **Blocking Behavior**: The calling thread is blocked at the point of invocation until all actions complete; it is not an asynchronous awaitable call.
- **ParallelOptions**: Configures execution with a `CancellationToken` and `MaxDegreeOfParallelism`.
- **MaxDegreeOfParallelism**: Limits the number of concurrent operations to manage CPU and thread pool resources.
- **Performance Optimizations**: Includes task inlining and reduced overhead for single actions.
- **Exception Unwrapping**: Automatically handles `AggregateException` to surface the primary exception.

### Lesson notes

`Parallel.Invoke` is a static method used to execute multiple operations simultaneously.
A common use case is performing multiple independent I/O operations, such as writing different file streams in parallel.

```csharp
using var dentalRecordsFileStream =
    new FileStream(dentalRecordsPath, FileMode.Create, FileAccess.Write);

using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

using var visionRecordsFileStream =
    new FileStream(visionRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

Parallel.Invoke(options,
    () => dentalRecordsFileStream.Write(dentalRecords),
    () => medicalRecordsFileStream.Write(medicalRecords),
    () => visionRecordsFileStream.Write(visionRecords));

// Alternatively, use Action[]
Action[] actions =
[
    () => dentalRecordsFileStream.Write(dentalRecords),
    () => medicalRecordsFileStream.Write(medicalRecords),
    () => visionRecordsFileStream.Write(visionRecords)
];

Parallel.Invoke(options, actions);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=40)

#### Blocking and UI Threads

It is critical to understand that `Parallel.Invoke` is a **blocking call**.
Unlike asynchronous methods, it does not return a `Task` and cannot be awaited.
When a thread calls `Parallel.Invoke`, it is held at that line of code until all background tasks finish.
If this occurs on the UI thread (Main thread), the application will become unresponsive to user input, such as scrolling or button clicks, for the duration of the operations.

#### Implementation Example

To demonstrate parallel execution, we can use a `Food` class where the `Cook` method uses `Thread.Sleep` to simulate a blocking workload.
This is appropriate for `Parallel.Invoke` because the method itself is designed for synchronous actions.

```csharp
using System.Diagnostics;

namespace ParallelInvoke;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public void Cook()
	{
		Trace.WriteLine($"Cooking {Name}");
		Thread.Sleep(_cookTime);
		Trace.WriteLine($"{Name} Completed");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=475)

In the main execution logic, we wrap the call in a `try-catch` block to handle `OperationCanceledException`.
By setting `MaxDegreeOfParallelism`, we can control how many items are cooked at once.
For instance, if set to 2, only two items will cook simultaneously, even if four are requested.
This prevents thread pool exhaustion and CPU overload.

```csharp
using System.Diagnostics;
using ParallelInvoke;

Trace.WriteLine("Cooking Started");

var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
	CancellationToken = cts.Token,
	MaxDegreeOfParallelism = 2
};

try
{
	Parallel.Invoke(options, () => turkey.Cook(), () => gravy.Cook(), () => mashedPotatoes.Cook(), () => stuffing.Cook());
}
catch (OperationCanceledException)
{
	Trace.WriteLine("ERROR: Cooking took too long");
}

Trace.WriteLine("Cooking Complete");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=565)

#### Parallel.Invoke vs. Task.WaitAll

`Parallel.Invoke` is often preferred over `Task.WaitAll` for several reasons:

1.  **Performance**: It includes logic to run actions "in-line" on the calling thread if it saves resources, and it avoids the overhead of task creation if only a single action is provided.
2.  **Exception Handling**: `Task.WaitAll` wraps exceptions in an `AggregateException`. To catch a specific error like a cancellation, you would need to inspect the `InnerExceptions` collection. `Parallel.Invoke` simplifies this by unwrapping the aggregate and throwing the actual exception encountered.

```csharp
try
{
    Task.WaitAll(tasks);
}
catch (AggregateException aggregateException) when (aggregateException.InnerExceptions.OfType<OperationCanceledException>().Any())
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=925)

#### Internal Implementation

Under the hood, `Parallel.Invoke` validates the input, creates a defensive copy of the actions, and eventually uses `Task.WaitAll`.
However, it does so only after applying various performance optimizations and ensuring the cancellation token is checked before work begins.

```csharp
public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
{
    ArgumentNullException.ThrowIfNull(parallelOptions);
    ArgumentNullException.ThrowIfNull(actions);

    parallelOptions.CancellationToken.ThrowIfCancellationRequested();

    // ... validation and defensive copy ...

    try
    {
        Task.WaitAll(tasks);
    }
    catch (AggregateException aggExp)
    {
        ThrowSingleCancellationExceptionOrOtherException(aggExp.InnerExceptions, parallelOptions);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=980)

---

## 2. Parallel.For

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/) · 8:27

### Summary

Parallel.For is a blocking method in the Task Parallel Library that executes a for-loop in parallel by distributing iterations across background threads.
It is configured using ParallelOptions to manage concurrency limits and cancellation.
Because it blocks the calling thread until all iterations are finished, it is not suitable for use on UI threads without additional asynchronous wrapping.
The method returns a ParallelLoopResult, which provides information about the loop's completion status and progress.

### Key concepts

- **Blocking Call**: The method does not return until the entire loop is finished.
- **Index-Based Action**: The loop body is an Action<int> that receives the current iteration index.
- **ParallelOptions**: Used to set MaxDegreeOfParallelism and provide a CancellationToken.
- **Sequential Loop Execution**: Multiple Parallel.For calls execute one after another, though iterations within each call run in parallel.
- **ParallelLoopResult**: Returns status information, including IsCompleted and LowestBreakIteration.

### Lesson notes

Parallel.For provides a way to execute a loop where each iteration can run in parallel.
It is a blocking call, meaning the thread that initiates the call will wait until all iterations of the loop have finished.
This makes it unsuitable for use on a UI thread if the operations are long-running, as it will freeze the interface.

In a scenario involving processing medical records, Parallel.For can be configured with ParallelOptions to limit the number of concurrent operations.

```csharp
using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

Parallel.For(
    0,
    100,
    options,
    (int patientNumber) =>
        medicalRecordsFileStream.Write(GetRecords(patientNumber))
);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=10)

The method takes a starting index (inclusive), an ending index (exclusive), the options, and an action that accepts the current index as an integer.
For example, if the loop starts at 0 and goes to 100, and the current iteration is 50, the index passed to the action will be 50.

#### Implementation Example: Parallel Cooking

To demonstrate Parallel.For in a more complex scenario, consider a cooking application.
The setup involves defining various food items and a cancellation token source.

```csharp
using System.Diagnostics;
using ParallelFor;

const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

var turkey = new Turkey();
var mashedPotatoes = new MashedPotatoes();
var gravy = new Gravy();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.FromSeconds(10));

var options = new ParallelOptions
{
    CancellationToken = cancellationTokenSource.Token,
    MaxDegreeOfParallelism = 3
};

Trace.WriteLine("Cooking Started");

try
{
}
catch (OperationCanceledException)
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=115)

When executing the loop, the MaxDegreeOfParallelism can be adjusted to control how many threads are used simultaneously.
The action delegate must accept an integer representing the current iteration index.

```csharp
try
{
    Parallel.For(1, numOrdersTurkey, options, int orderNumber => turkey.Cook(orderNumber));
}
catch (OperationCanceledException)
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=160)

The Food base class and its derivatives handle the simulated cooking time using Thread.Sleep.
The Cook method prints the current order number and the thread ID to demonstrate parallel execution.

```csharp
using System.Diagnostics;

namespace ParallelFor;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public void Cook(int orderNumber = 0)
	{
		Trace.WriteLine($"Cooking {Name} for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
		Thread.Sleep(_cookTime);
		Trace.WriteLine($"{Name} Completed for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=175)

#### Sequential Execution of Parallel Loops

Because Parallel.For is a blocking call, if multiple Parallel.For calls are placed one after another, they execute sequentially relative to each other.
In the following example, all turkey orders must complete before the mashed potatoes begin cooking.

```csharp
try
{
    Parallel.For(1, numOrdersTurkey, options, body: (int orderNumber) => turkey.Cook(orderNumber));
    Parallel.For(1, numOrdersMashedPotatoes, options, body: (int orderNumber) => mashedPotatoes.Cook(orderNumber));
    Parallel.For(1, numOrdersGravy, options, body: (int orderNumber) => gravy.Cook(orderNumber));
    Parallel.For(1, numOrdersStuffing, options, body: (int orderNumber) => stuffing.Cook(orderNumber));
}
catch (OperationCanceledException)
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=205)

#### Handling Results

Parallel.For returns a ParallelLoopResult object.
This can be used to verify if the loop completed all iterations or to find the LowestBreakIteration if the loop was interrupted.
If a CancellationToken is triggered, the loop will stop and throw an OperationCanceledException.

```csharp
try
{
	var turkeyResult = Parallel.For(0, numOrdersTurkey, options, orderNumber => turkey.Cook(orderNumber));
	var mashedPotatoesResult = Parallel.For(0, numOrdersMashedPotatoes, options, orderNumber => mashedPotatoes.Cook(orderNumber));
	var gravyResult = Parallel.For(0, numOrdersGravy, options, orderNumber => gravy.Cook(orderNumber));
	var stuffingResult = Parallel.For(0, numOrdersStuffing, options, orderNumber => stuffing.Cook(orderNumber));

	if (turkeyResult.IsCompleted && mashedPotatoesResult.IsCompleted && gravyResult.IsCompleted && stuffingResult.IsCompleted)
		Trace.WriteLine("All Meals Complete");
	else
		Trace.WriteLine("Cooking Failed");
}
catch (OperationCanceledException)
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=430)

---

## 3. Parallel.ForAsync

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/) · 8:24

### Summary

`Parallel.ForAsync` is an asynchronous version of the `Parallel.For` loop that enables non-blocking parallel execution.
By returning a `Task`, it allows the calling thread to remain available while background operations proceed, making it particularly suitable for UI-bound or high-throughput applications.
It supports asynchronous delegates and provides better integration with `CancellationToken` for graceful task cancellation compared to its synchronous counterpart.

### Key concepts

- **Non-blocking execution**: Returns a `Task` that can be awaited, releasing the calling thread to the thread pool or UI message loop.
- **Asynchronous body**: Supports `Func<T, CancellationToken, ValueTask>` allowing the use of `await` inside the loop body.
- **Cancellation propagation**: Passes a `CancellationToken` directly into each iteration's delegate, allowing asynchronous operations to fail gracefully.
- **Thread safety**: Prevents UI freezing in desktop or mobile applications by avoiding blocking calls on the main thread.
- **Task Composition**: Multiple `Parallel.ForAsync` operations can be initiated concurrently and awaited collectively using `Task.WhenAll`.

### Lesson notes

`Parallel.ForAsync` is functionally similar to `Parallel.For` but is designed for asynchronous workflows.
It utilizes the `ParallelOptions` class to configure `CancellationToken` and `MaxDegreeOfParallelism`.
The primary advantage of `Parallel.ForAsync` is that it is non-blocking.
When the `await` keyword is used, the calling thread is returned to the system while the background operations—governed by the degree of parallelism—execute.

In the following example, `Parallel.ForAsync` is used to write to a file stream asynchronously.
The calling thread hits the `await` keyword and is immediately freed, while 101 background actions (with a maximum of 10 running simultaneously) perform the work.

```csharp
using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

await Parallel.ForAsync(
    0,
    100,
    options,
    (int patientNumber, CancellationToken token) =>
        medicalRecordsFileStream.WriteAsync(GetRecords(patientNumber), token)
);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=10)

A significant difference in `Parallel.ForAsync` is how it handles cancellation.
The loop body accepts a `CancellationToken` which is the same token passed via `ParallelOptions`.
This allows the token to be propagated forward into asynchronous methods (like `WriteAsync`), enabling them to handle cancellation gracefully rather than being terminated abruptly.

To demonstrate this in a more complex scenario, consider a cooking application.
The `Food` base class and its derivatives define asynchronous `Cook` methods that simulate work using `Task.Delay`.

```csharp
using System.Diagnostics;

namespace ParallelForAsync;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task Cook(int orderNumber, CancellationToken token)
	{
		Trace.WriteLine($"Cooking {Name} for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
		await Task.Delay(_cookTime, token);
		Trace.WriteLine($"{Name} Completed for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=160)

When executing multiple parallel loops, awaiting them sequentially ensures that one set of tasks completes before the next begins.
In the following implementation, the turkey must finish cooking before the mashed potatoes start, because each `Parallel.ForAsync` call is awaited individually.

```csharp
using ParallelForAsync;

const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

var turkey = new Turkey();
var gravy = new Gravy();
var stuffing = new Stuffing();
var mashedPotatoes = new MashedPotatoes();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var options = new ParallelOptions
{
    CancellationToken = cancellationTokenSource.Token,
    MaxDegreeOfParallelism = 40
};

Trace.WriteLine("Cooking Started");

try
{
    await Parallel.ForAsync(1, numOrdersTurkey, options, async (int orderNumber, CancellationToken token) => await turkey.Cook(orderNumber, token));
    await Parallel.ForAsync(1, numOrdersMashedPotatoes, options, async (int orderNumber, CancellationToken token) => await mashedPotatoes.Cook(orderNumber, token));
    await Parallel.ForAsync(1, numOrdersGravy, options, async (int orderNumber, CancellationToken token) => await gravy.Cook(orderNumber, token));
    await Parallel.ForAsync(1, numOrdersStuffing, options, async (int orderNumber, CancellationToken token) => await stuffing.Cook(orderNumber, token));

    Trace.WriteLine("All Meals Complete");
}
catch
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=250)

To run these parallel loops simultaneously, you can capture the `Task` returned by each `Parallel.ForAsync` call without immediately awaiting it.
By using `Task.WhenAll`, all parallel loops are kicked off at once, and the application waits for the entire set of operations to complete.
This combines the power of the `Parallel` class with standard asynchronous task composition.

```csharp
using System.Diagnostics;
using ParallelForAsync;

const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

var turkey = new Turkey();
var gravy = new Gravy();
var stuffing = new Stuffing();
var mashedPotatoes = new MashedPotatoes();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var options = new ParallelOptions { CancellationToken = cancellationTokenSource.Token, MaxDegreeOfParallelism = 40 };

Trace.WriteLine("Cooking Started");

try
{
	var cookTurkeysTask = Parallel.ForAsync(0, numOrdersTurkey, options, async (orderNumber, token) => await turkey.Cook(orderNumber, token));
	var cookGravyTask = Parallel.ForAsync(0, numOrdersGravy, options, async (orderNumber, token) => await gravy.Cook(orderNumber, token));
	var cookStuffingTask = Parallel.ForAsync(0, numOrdersStuffing, options, async (orderNumber, token) => await stuffing.Cook(orderNumber, token));
	var cookMashedPotatoesTask = Parallel.ForAsync(0, numOrdersMashedPotatoes, options, async (orderNumber, token) => await mashedPotatoes.Cook(orderNumber, token));

	await Task.WhenAll(cookTurkeysTask, cookMashedPotatoesTask, cookGravyTask, cookStuffingTask);

	Trace.WriteLine("All Meals Complete");
}
catch
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=475)

---

## 4. Parallel.ForEach

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/) · 11:51

### Summary

Parallel.ForEach is a parallel alternative to the standard foreach loop, designed to iterate over an IEnumerable<T> or any collection that can be iterated.
Unlike Parallel.For, which requires explicit start and end indices, Parallel.ForEach automatically partitions the provided collection and executes a specified action for each element.
It is a blocking operation, meaning the calling thread is held until all iterations are finished.
The method provides a delegate that grants access to the current item, the iteration index, and a ParallelLoopState object, which allows for fine-grained control over the loop's execution, including the ability to break or stop processing.

### Key concepts

- Parallel iteration over `IEnumerable<T>` collections.
- Blocking behavior on the calling thread.
- Access to the current item, iteration index, and `ParallelLoopState`.
- Loop control using `state.Break()` and `state.Stop()`.
- Monitoring execution status via `ParallelLoopResult`.
- Handling thread-safe operations within the loop body.

### Lesson notes

`Parallel.ForEach` allows for the parallel execution of a loop over a collection, similar to how a standard `foreach` loop operates.
Instead of specifying a starting and finishing index, a list or an `IEnumerable` is passed to the method.
`Parallel.ForEach` then executes the provided action multiple times based on the items in the list.

```csharp
List<Patient> patientList = GetPatients();

using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

Parallel.ForEach(
    patientList,
    options,
    (Patient patient, ParallelLoopState state, long index) =>
        medicalRecordsFileStream.Write(patient.MedicalRecords));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=10)

#### Blocking Behavior

`Parallel.ForEach` is a blocking call.
If the UI thread or any calling thread reaches a `Parallel.ForEach` block, that thread is frozen and held captive until the entire list has been iterated over in parallel.
Execution only continues once the parallel work is complete.

#### Implementation and Setup

To demonstrate `Parallel.ForEach`, we can initialize collections using `Enumerable.Range`.
This is a shorthand for creating an `IEnumerable` and selecting items to instantiate objects.

```csharp
const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

ParallelLoopResult? turkeyResult = null, mashedPotatoesResult = null, gravyResult = null, stuffingResult = null;

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var options = new ParallelOptions { CancellationToken = cancellationTokenSource.Token, MaxDegreeOfParallelism = 40 };

List<Turkey> turkeyOrders = 
    Enumerable.Range(1, numOrdersTurkey).Select(static _ => new Turkey()).ToList();

List<MashedPotatoes> mashedPotatoesOrders = 
    Enumerable.Range(1, numOrdersMashedPotatoes).Select(static _ => new MashedPotatoes()).ToList();

List<Gravy> gravyOrders = 
    Enumerable.Range(1, numOrdersGravy).Select(static _ => new Gravy()).ToList();

List<Stuffing> stuffingOrders = 
    Enumerable.Range(1, numOrdersStuffing).Select(static _ => new Stuffing()).ToList();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=130)

The loop body receives the current item, the `ParallelLoopState`, and the current index.
In this example, the `Cook` method is called synchronously because `Parallel.ForEach` is a blocking call and does not natively support `async/await` in this specific overload.

```csharp
try
{
    turkeyResult = Parallel.ForEach(turkeyOrders, options, body: (Turkey turkey, ParallelLoopState state, long index) => turkey.Cook(index));
    mashedPotatoesResult = Parallel.ForEach(mashedPotatoesOrders, options, body: (MashedPotatoes mashedPotatoes, ParallelLoopState state, long index) => mashedPotatoes.Cook(index));
    gravyResult = Parallel.ForEach(gravyOrders, options, body: (Gravy gravy, ParallelLoopState state, long index) => gravy.Cook(index));
    stuffingResult = Parallel.ForEach(stuffingOrders, options, body: (Stuffing stuffing, ParallelLoopState state, long index) => stuffing.Cook(index));
}
catch
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=265)

#### Controlling Loop State

The `ParallelLoopState` object provides methods to interrupt the loop: `Break()` and `Stop()`.

- **Break**: Tells the loop to cease execution of iterations beyond the current iteration at the system's earliest convenience. Iterations with an index lower than the break index will eventually be completed.
- **Stop**: Tells the loop to cease execution for everything immediately, even if other iterations are currently running.

```csharp
turkeyResult = Parallel.ForEach(turkeyOrders, options, body: (Turkey turkey, ParallelLoopState state, long index) =>
{
    if (index == 5)
        state.Break();

    turkey.Cook(index);
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=550)

When using `Stop()`, the loop terminates more aggressively:

```csharp
turkeyResult = Parallel.ForEach(turkeyOrders, options, body: (Turkey turkey, ParallelLoopState state, long index) =>
{
    if(index == 5)
        state.Stop();

    turkey.Cook(index);
});
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=595)

#### Evaluating Results

The `ParallelLoopResult` structure contains information about whether the loop finished and where it was interrupted.

```csharp
public struct ParallelLoopResult
{
    public bool IsCompleted { get; }
    public long? LowestBreakIteration { get; }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=655)

If `state.Break()` was never called (e.g., if the loop finished or if `state.Stop()` was used instead), `LowestBreakIteration` will be `null`.
To ensure a loop finished successfully, check both `IsCompleted` and the `LowestBreakIteration` value.

```csharp
if (turkeyResult.HasValue
    && turkeyResult.Value.IsCompleted
    && turkeyResult.Value.LowestBreakIteration is null)
{
    Trace.WriteLine("Turkey Cooked Successfully");
}
else
{
    Trace.WriteLine($"Turkey Stopping Cooking at {turkeyResult?.LowestBreakIteration}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=400)

---

## 5. Parallel.ForEachAsync

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/) · 5:38

### Summary

`Parallel.ForEachAsync` is an asynchronous version of the `Parallel.ForEach` method that returns a `Task`, allowing for non-blocking parallel execution.
It is particularly useful in UI-bound applications where maintaining responsiveness is critical, as it allows the calling thread to remain free while background threads handle the parallel workload.
The method supports `ParallelOptions` for controlling concurrency and cancellation, and its delegate provides a `CancellationToken` to ensure that asynchronous operations within the loop can fail gracefully.

### Key concepts

- **Asynchronous Execution**: Returns a `Task` that can be awaited, making it compatible with `async/await` patterns.
- **Non-blocking**: Prevents the calling thread (e.g., the UI thread) from freezing while parallel work is processed.
- **ParallelOptions**: Allows configuration of the `CancellationToken` and `MaxDegreeOfParallelism`.
- **Cancellation Support**: The loop delegate receives a `CancellationToken` to propagate cancellation to internal asynchronous calls.
- **Task Composition**: Multiple parallel loops can be initiated as tasks and managed collectively using `Task.WhenAll`.

### Lesson notes

`Parallel.ForEachAsync` provides the same parallel iteration capabilities as `Parallel.ForEach` but is designed for asynchronous workflows.
By returning a `Task`, it allows the application to await the completion of the parallel loop without blocking the execution thread.
This is especially important for UI applications where blocking the main thread would result in a frozen interface.

```csharp
List<Patient> patientList = GetPatients();

using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

await Parallel.ForEachAsync(
    patientList,
    options,
    (Patient patient, CancellationToken token) =>
        medicalRecordsFileStream.WriteAsync(patient.MedicalRecords, token));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=10)

In this example, the `WriteAsync` method is passed the `CancellationToken` provided by the `Parallel.ForEachAsync` delegate.
This ensures that if the loop is canceled or times out, the asynchronous write operation can terminate gracefully.

To demonstrate this in a more complex scenario, consider a food preparation system where different types of food have different cooking times.
The `Food` base class and its derived types use `Task.Delay` to simulate asynchronous work.

```csharp
using System.Diagnostics;

namespace ParallelForEachAsync;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task Cook(CancellationToken cancellationToken)
	{
		Trace.WriteLine($"Cooking {Name} on Thread {Environment.CurrentManagedThreadId}");
		await Task.Delay(_cookTime, cancellationToken);
		Trace.WriteLine($"{Name} Completed on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=145)

When executing multiple parallel loops, awaiting them sequentially means that the second loop will not start until the first one has completely finished all its parallel iterations.
While the code within each loop runs in parallel, the loops themselves are processed one after another.

```csharp
try
{
    await Parallel.ForEachAsync(turkeyOrders, options, body: async (Turkey turkey, CancellationToken token) => await turkey.Cook(token));
    await Parallel.ForEachAsync(mashedPotatoesOrders, options, body: async (MashedPotatoes mashedPotatoes, CancellationToken token) => await mashedPotatoes.Cook(token));
    await Parallel.ForEachAsync(gravyOrders, options, body: async (Gravy gravy, CancellationToken token) => await gravy.Cook(token));
    await Parallel.ForEachAsync(stuffingOrders, options, body: async (Stuffing stuffing, CancellationToken token) => await stuffing.Cook(token));

    Trace.WriteLine("All Meals Complete");
}
catch
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
finally
{
    Trace.WriteLine("Cooking Ended");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=195)

To achieve higher throughput, you can initiate each `Parallel.ForEachAsync` call without immediately awaiting it.
By capturing the returned `Task` for each loop, you can use `Task.WhenAll` to run all the parallel loops simultaneously.
This allows the system to process turkey, mashed potatoes, gravy, and stuffing orders concurrently across all available threads, subject to the `MaxDegreeOfParallelism` limit.

```csharp
try
{
	var turkeyCookingTasks = Parallel.ForEachAsync(turkeyOrders, options, static async (turkey, token) => await turkey.Cook(token));
	var mashedPotatoesTasks = Parallel.ForEachAsync(mashedPotatoesOrders, options, static async (mashedPotatoes, token) => await mashedPotatoes.Cook(token));
	var gravyCookingTasks = Parallel.ForEachAsync(gravyOrders, options, static async (gravy, token) => await gravy.Cook(token));
	var stuffingCookingTasks = Parallel.ForEachAsync(stuffingOrders, options, static async (stuffing, token) => await stuffing.Cook(token));

	await Task.WhenAll(turkeyCookingTasks, mashedPotatoesTasks, gravyCookingTasks, stuffingCookingTasks);

	Trace.WriteLine("All Meals Complete");
}
catch
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
finally
{
	Trace.WriteLine("Cooking Ended");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=285)

# Creating Async from Scratch

> Course: [From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · Chapter 4
> 6 lessons · ~46:16
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197135/) | 1:12 | [↓](#1-introduction) |
| 2 | [Run](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/) | 8:14 | [↓](#2-run) |
| 3 | [ContinueWith](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/) | 17:15 | [↓](#3-continuewith) |
| 4 | [Wait](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/) | 7:09 | [↓](#4-wait) |
| 5 | [Delay](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/) | 5:18 | [↓](#5-delay) |
| 6 | [Await](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/) | 7:08 | [↓](#6-await) |

---

## 1. Introduction

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197135/) · 1:12

### Summary

This lesson introduces the process of building a custom implementation of the Task pattern, referred to as DomeTrainTask.
By manually constructing core asynchronous primitives such as Run, Wait, and ContinueWith, and implementing the necessary machinery for async-await support, developers can gain a hands-on understanding of how the .NET runtime and C# compiler handle asynchronous operations under the hood.

### Key concepts

- **Custom Task Implementation**: Creating a `DomeTrainTask` class to replicate the behavior of the standard .NET `Task` type.
- **Core Async Primitives**: Implementing `Run` for thread pooling, `Wait` for blocking synchronization, and `ContinueWith` for task chaining.
- **Async-Await Integration**: Enabling the `await` keyword on custom types by implementing the Awaiter pattern (`GetAwaiter` and `INotifyCompletion`).
- **Internal Mechanics**: Understanding how state, exceptions, and continuations are managed within a task-like object.

### Lesson notes

While the .NET runtime provides a robust `Task` implementation out of the box, building one from scratch reveals the underlying mechanics of asynchronous programming in C#.
This implementation, `DomeTrainTask`, serves as a functional model to demonstrate how the compiler-generated state machine interacts with task objects.

#### The DomeTrainTask Structure

The core of the implementation is the `DomeTrainTask` class.
It manages the state of the operation, including whether it has completed, any exceptions that occurred during execution, and the continuation action to be executed upon completion.
To ensure thread safety when accessing these shared states, a `Lock` object is utilized.

```csharp
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace CreatingTaskFromScratch;

public class DomeTrainTask
{
	readonly Lock _lock = new();

	bool _completed;
	Exception? _exception;
	Action? _action;
	ExecutionContext? _context;

	public bool IsCompleted
	{
		get
		{
			lock (_lock)
			{
				return _completed;
			}
		}
	}

	public static DomeTrainTask Delay(TimeSpan delay)
	{
		DomeTrainTask task = new();

		new Timer(_ => task.SetResult()).Change(delay, Timeout.InfiniteTimeSpan);

		return task;
	}

	public static DomeTrainTask Run(Action action)
	{
		DomeTrainTask task = new();

		ThreadPool.QueueUserWorkItem(_ =>
		{
			try
			{
				action();
				task.SetResult();
			}
			catch (Exception e)
			{
				task.SetException(e);
			}
		});

		return task;
	}

	public void Wait()
	{
		ManualResetEventSlim? resetEventSlim = null;

		lock (_lock)
		{
			if (!_completed)
			{
				resetEventSlim = new();
				ContinueWith(() => resetEventSlim.Set());
			}
		}

		resetEventSlim?.Wait();

		if (_exception is not null)
		{
			ExceptionDispatchInfo.Throw(_exception);
		}
	}

	public DomeTrainTask ContinueWith(Action action)
	{
		DomeTrainTask task = new();

		lock (_lock)
		{
			if (_completed)
			{
				ThreadPool.QueueUserWorkItem(_ =>
				{
					try
					{
						action();
						task.SetResult();
					}
					catch (Exception e)
					{
						task.SetException(e);
					}
				});
			}
			else
			{
				_action = action;
				_context = ExecutionContext.Capture();
			}
		}

		return task;
	}

	public DomeTrainTaskAwaiter GetAwaiter() => new(this);

	public void SetResult() => CompleteTask(null);

	public void SetException(Exception exception) => CompleteTask(exception);

	void CompleteTask(Exception? exception)
	{
		lock (_lock)
		{
			if (_completed)
				throw new InvalidOperationException(
					"DomeTrainTask already completed. Cannot set result of a completed DomeTrainTask");

			_completed = true;
			_exception = exception;

			if (_action is not null)
			{
				if (_context is null)
				{
					_action.Invoke();
				}
				else
				{
					ExecutionContext.Run(_context, state => ((Action?)state)?.Invoke(), _action);
				}
			}
		}
	}
}
```

#### Enabling Async-Await Support

To allow the `await` keyword to be used with `DomeTrainTask`, the class must provide a `GetAwaiter()` method that returns an object implementing the Awaiter pattern.
This includes the `INotifyCompletion` interface, which provides the `OnCompleted` method used by the compiler to attach continuations to the task.

```csharp
public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
	readonly DomeTrainTask _task;

	internal DomeTrainTaskAwaiter(DomeTrainTask task) => _task = task;

	public bool IsCompleted => _task.IsCompleted;

	public void OnCompleted(Action continuation) => _task.ContinueWith(continuation);

	public DomeTrainTaskAwaiter GetAwait() => this;

	public void GetResult() => _task.Wait();
}
```

#### Practical Usage

The following example demonstrates how `DomeTrainTask` can be used in a standard C# program, utilizing `Run` to offload work to the ThreadPool and `Delay` to simulate asynchronous waiting, all while being compatible with the `await` keyword.

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

await DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));
```

---

## 2. Run

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/) · 8:14

### Summary

This lesson demonstrates how to build a custom task implementation from scratch, starting with the DomeTrainTask class.
It covers the internal state management required for asynchronous operations, including thread safety using the C# 13 Lock class, tracking completion status, and capturing exceptions.
The lesson concludes by implementing a static Run method that offloads work to the .NET thread pool, verifying the asynchronous execution by comparing thread IDs.

### Key concepts

* **Custom Task Implementation**: Building `DomeTrainTask` to mimic the behavior of the standard `System.Threading.Tasks.Task`.
* **Thread Safety**: Utilizing the `System.Threading.Lock` class (introduced in C# 13 / .NET 9) to prevent race conditions when accessing task state across multiple threads.
* **State Management**: Tracking completion status via a boolean flag and capturing exceptions thrown during background execution to be re-thrown later.
* **Thread Pool Integration**: Using `ThreadPool.QueueUserWorkItem` to execute actions on background threads.
* **Verification**: Using managed thread IDs to confirm that work is successfully deferred to a background thread.

### Lesson notes

The implementation begins with a basic console application and a shared `Food` base class used in previous examples to represent asynchronous work.

```csharp
namespace CreatingTaskFromScratch;

public abstract class Food
{
    readonly TimeSpan _cookTime;

    protected Food(TimeSpan cookTime)
    {
        _cookTime = cookTime;
        Name = GetType().Name;
    }

    public string Name { get; }

    public async Task Cook()
    {
        Console.WriteLine($"Cooking {Name}");
        await Task.Delay(_cookTime);
        Console.WriteLine($"{Name} Completed");
    }
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=40)

To create a custom task implementation, we define the `DomeTrainTask` class.
This class requires internal fields to manage its state, including a `Lock` object to ensure thread safety, a boolean to track completion, and an `Exception` field to store any errors that occur during execution.

```csharp
namespace CreatingTaskFromScratch;

public class DomeTrainTask
{
    private readonly Lock _lock = new();

    private bool _completed;
    private Exception? _exception;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=115)

The `IsCompleted` property provides a public way to check the task's status.
Because multiple threads may check or modify this value simultaneously, the getter is wrapped in a `lock` statement using the `_lock` field.

```csharp
public bool IsCompleted
{
    get
    {
        lock (_lock)
        {
            return _completed;
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=175)

To transition the task to a completed state, we implement `SetResult` and `SetException`.
These methods must also be thread-safe.
If a task is already completed, attempting to set a result or an exception again is an invalid operation and should throw an exception.

```csharp
public void SetResult()
{
    lock (_lock)
    {
        if (_completed)
            throw new InvalidOperationException("DomeTrainTask already completed. Cannot set result of a completed DomeTrainTask");

        _completed = true;
    }
}

public void SetException(Exception exception)
{
    lock (_lock)
    {
        if (_completed)
            throw new InvalidOperationException("DomeTrainTask already completed. Cannot set result of a completed DomeTrainTask");

        _completed = true;
        _exception = exception;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=265)

The `Run` method is a static entry point that allows a user to pass an `Action` to be executed on a background thread.
It uses `ThreadPool.QueueUserWorkItem` to schedule the work.
Inside the thread pool's execution block, a `try-catch` wrapper ensures that successful execution calls `SetResult`, while any thrown exceptions are captured via `SetException` so they can be re-thrown when the task is eventually awaited.

```csharp
public static DomeTrainTask Run(Action action)
{
    DomeTrainTask task = new();

    ThreadPool.QueueUserWorkItem(_ =>
    {
        try
        {
            action();
            task.SetResult();
        }
        catch (Exception e)
        {
            task.SetException(e);
        }
    });

    return task;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=385)

To verify the implementation, we can compare the `ManagedThreadId` of the main thread against the thread ID inside the `DomeTrainTask.Run` action.
Since the custom task does not yet support the `await` keyword, `Console.ReadLine()` is used to prevent the application from exiting before the background thread completes.

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");

DomeTrainTask.Run(() => 
    Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}"));

Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=445)

---

## 3. ContinueWith

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/) · 17:15

### Summary

This lesson covers the implementation of a continuation mechanism for a custom task type, allowing code to execute automatically after a task completes.
It introduces the `ContinueWith` method, the concept of `ExecutionContext` for preserving thread state (such as security and culture settings), and refactors the task completion logic to handle stored continuations.

### Key concepts

- Implementation of `ContinueWith` for task chaining.
- Capturing and restoring `ExecutionContext` to preserve thread-local state.
- Handling immediate vs. deferred continuation execution.
- Refactoring task completion into a unified `CompleteTask` method.
- The limitations and readability issues of callback-based asynchronous code.

### Lesson notes

The `ContinueWith` method is implemented to allow a `DomeTrainTask` to execute a follow-up action once it finishes.
This method returns a new `DomeTrainTask`, enabling the chaining of asynchronous operations.
Inside `ContinueWith`, the code first checks if the current task has already completed.
If it has, the continuation action is immediately queued to the `ThreadPool`.
To ensure robustness, the action is wrapped in a try-catch block to capture any exceptions and propagate them to the continuation task.

```csharp
public DomeTrainTask ContinueWith(Action action)
{
    DomeTrainTask task = new();

    lock (_lock)
    {
        if (_completed)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    action();
                    task.SetResult();
                }
                catch (Exception e)
                {
                    task.SetException(e);
                }
            });
        }
    }

    return task;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=160)

To support continuations for tasks that are still running, the class must store the state of the continuation.
This requires two new private fields: an `Action` to store the callback and an `ExecutionContext`.
The `ExecutionContext` is critical because it captures the logical thread state, including security identity and culture information.
This prevents data leakage or loss of context when the task resumes on a different thread pool thread.

```csharp
private Action? _action;
private ExecutionContext? _context;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=205)

If the task is not yet completed when `ContinueWith` is called, the method captures the current `ExecutionContext` and stores both the context and the action for later execution.

```csharp
else
{
    _action = action;
    _context = ExecutionContext.Capture();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=325)

The task completion logic is refactored into a unified `CompleteTask` method.
This method is responsible for setting the task's completion state, storing any exceptions, and triggering the continuation if one was registered.
If an `ExecutionContext` was captured, the action is invoked using `ExecutionContext.Run` to ensure the environment is correctly restored.

```csharp
public void SetResult() => CompleteTask(null);

public void SetException(Exception exception) => CompleteTask(exception);

private void CompleteTask(Exception? exception)
{
    lock (_lock)
    {
        if (_completed)
            throw new InvalidOperationException("DomeTrainTask already completed. Cannot set result of a completed DomeTrainTask");

        _completed = true;
        _exception = exception;

        if (_action is not null)
        {
            if (_context is null)
            {
                _action.Invoke();
            }
            else
            {
                ExecutionContext.Run(_context, state => ((Action?)state)?.Invoke(), _action);
            }
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=505)

In a console application, this implementation allows for complex chaining.
However, as shown in the example below, using multiple nested `ContinueWith` and `Run` calls leads to code that is difficult to read and prone to bugs, as the execution order does not follow the visual top-to-bottom flow of the source code.
This highlights the need for more structured synchronization primitives like `.Wait()`.

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask task = DomeTrainTask.Run(() =>
{
    Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}");
});

task.ContinueWith(() =>
{
    DomeTrainTask.Run(() =>
    {
        Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}");
    });

    Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");
});

Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=850)

---

## 4. Wait

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/) · 7:09

### Summary

This lesson covers the implementation of a blocking Wait method for a custom Task implementation.
By utilizing ManualResetEventSlim, the task can block the calling thread until the asynchronous operation completes.
The implementation leverages existing ContinueWith logic to signal the event and uses ExceptionDispatchInfo to preserve stack traces when re-throwing exceptions.
Crucially, the lesson demonstrates how to avoid deadlocks by ensuring the blocking wait occurs outside of the synchronization lock.

### Key concepts

*   **ManualResetEventSlim**: A .NET primitive used to manage thread waiting behavior by allowing threads to block until a signal is received.
*   **Blocking Calls**: Implementing a synchronous wait on an asynchronous operation.
*   **ExceptionDispatchInfo**: A utility to re-throw captured exceptions while preserving the original stack trace.
*   **Deadlock Prevention**: The necessity of performing blocking operations outside of critical sections (locks).
*   **Code Readability**: Transitioning from nested continuations to a linear, sequential execution flow using blocking waits.

### Lesson notes

#### Implementing the Wait Method

The `Wait` method is a blocking call that prevents the calling thread from proceeding until the task has finished.
To manage this behavior, the implementation uses `ManualResetEventSlim`.
This class provides a `Wait` method to block the current thread and a `Set` method to release all waiting threads.

Initially, the method checks if the task is already completed.
If it has not, a `ManualResetEventSlim` is initialized, and the task's `ContinueWith` method is used to schedule the `Set` call.

```csharp
public void Wait()
{
    ManualResetEventSlim? resetEventSlim = null;

    if (!_completed)
    {
        resetEventSlim = new ManualResetEventSlim();
        ContinueWith(() => resetEventSlim.Set());
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/?t=75)

#### Exception Handling and Thread Safety

When a task completes with an error, the `Wait` method must propagate that exception.
Simply using `throw _exception` is insufficient because it overwrites the stack trace, losing the information about where the error originally occurred.
To preserve the full stack trace, `ExceptionDispatchInfo.Throw(_exception)` is used.

Furthermore, because the task state is accessed across multiple threads, the logic must be thread-safe.
The check for `_completed` and the initialization of the `ManualResetEventSlim` are wrapped in a lock.
However, the actual call to `resetEventSlim.Wait()` must occur **outside** the lock.
If the thread were to wait while holding the lock, it would create a deadlock, as the background thread completing the task would be unable to enter the lock to update the task state or trigger the continuation.

```csharp
public void Wait()
{
    ManualResetEventSlim? resetEventSlim = null;

    lock (_lock)
    {
        if (!_completed)
        {
            resetEventSlim = new ManualResetEventSlim();
            ContinueWith(() => resetEventSlim.Set());
        }
    }

    resetEventSlim?.Wait();

    if (_exception is not null)
    {
        ExceptionDispatchInfo.Throw(_exception);
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/?t=205)

#### Refactoring for Readability

Implementing `Wait` allows for a significant cleanup of the consumer code.
Instead of nesting multiple `ContinueWith` calls—which leads to "callback hell" and difficult-to-read code—the logic can be written sequentially.
This makes the execution flow clear: the main thread starts, queues a background task, waits for it to finish, and then proceeds to the next line.

```csharp
// See https://aka.ms/new-console-template for more information

using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/?t=280)

By using `.Wait()`, the program no longer requires `Console.ReadLine()` to prevent the main thread from exiting prematurely.
The main thread is now explicitly held until the background work is complete.

---

## 5. Delay

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/) · 5:18

### Summary

This lesson demonstrates how to implement a static Delay method for a custom task implementation using the .NET Timer class.
It explains the mechanism of triggering task completion via a callback and discusses the critical architectural difference between blocking a thread with .Wait() versus using the non-blocking await keyword.

### Key concepts

- Implementing asynchronous delays using `System.Threading.Timer`.
- Using `Timer.Change` with `Timeout.InfiniteTimeSpan` for one-shot execution.
- Understanding the thread-blocking nature of `.Wait()`.
- Transitioning to the `await` keyword for non-blocking asynchronous flow.

### Lesson notes

To implement a `Delay` method in the custom `DomeTrainTask` class, we initialize a new task and use a `System.Threading.Timer` to schedule its completion.
The timer is configured to execute a callback that calls `SetResult()` on the task, effectively signaling that the delay period has elapsed.

```csharp
public static DomeTrainTask Delay(TimeSpan delay)
{
	DomeTrainTask task = new();

	new Timer(_ => task.SetResult()).Change(delay, Timeout.InfiniteTimeSpan);

	return task;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=115)

The `Timer.Change` method allows us to specify the delay duration.
By providing `Timeout.InfiniteTimeSpan` as the second argument, we disable periodic signaling, ensuring the timer fires only once.
When the timer triggers, it invokes the `CompleteTask` logic to handle task state transitions and any registered continuations.

```csharp
void CompleteTask(Exception? exception)
{
	lock (_lock)
	{
		if (_completed)
			throw new InvalidOperationException(
				"DomeTrainTask already completed. Cannot set result of a completed DomeTrainTask");

		_completed = true;
		_exception = exception;

		if (_action is not null)
		{
			if (_context is null)
			{
				_action.Invoke();
			}
			else
			{
				ExecutionContext.Run(_context, state => ((Action?)state)?.Invoke(), _action);
			}
		}
	}
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=130)

When using this delay in a program, simply calling the method is insufficient because the calling thread will continue execution immediately after the timer is scheduled.
To pause execution, we must wait for the task to complete.
Initially, this can be done using the `.Wait()` method.

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();

DomeTrainTask.Delay(TimeSpan.FromSeconds(1)).Wait();

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Delay(TimeSpan.FromSeconds(1)).Wait();

DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=200)

While `.Wait()` works, it is dangerous because it blocks the calling thread (e.g., the main thread), preventing it from doing other work.
A more idiomatic and efficient approach is to use the `await` keyword.
This makes the code easier to read—flowing from top to bottom—and ensures that the thread is not blocked while waiting for the task to finish.

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

await DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=295)

Implementing the `await` keyword requires the `DomeTrainTask` class to provide a `GetAwaiter` method, which returns an object implementing `INotifyCompletion`.

---

## 6. Await

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/) · 7:08

### Summary

This lesson demonstrates how to enable async/await support for a custom task type by implementing the awaiter pattern.
By creating a DomeTrainTaskAwaiter struct that implements INotifyCompletion and providing a GetAwaiter method on the DomeTrainTask class, the implementation leverages C#'s duck typing to integrate with the language's asynchronous state machine.
The lesson also explores why console applications behave differently than UI applications when awaiting tasks due to the absence of a SynchronizationContext, which prevents the execution from automatically returning to the original calling thread.

### Key concepts

* **Duck Typing**: The C# compiler looks for a specific method signature (`GetAwaiter()`) rather than a specific interface to determine if a type is awaitable.
* **Awaiter Pattern**: To be awaitable, a type must return an object (an "awaiter") that has an `IsCompleted` property, a `GetResult()` method, and implements `INotifyCompletion`.
* **INotifyCompletion**: This interface requires the `OnCompleted` method, which the state machine uses to hook up continuations when a task is not yet finished.
* **SynchronizationContext**: A mechanism that allows asynchronous operations to resume on the original calling thread; its absence in console apps results in continuations running on thread pool threads.

### Lesson notes

To make a custom task type compatible with the `await` keyword, you must implement an awaiter.
This is typically done by creating a companion struct.
In this case, we define `DomeTrainTaskAwaiter`, which must implement the `INotifyCompletion` interface.
The awaiter acts as a bridge between the task and the .NET asynchronous state machine.

Initially, the awaiter requires an internal constructor that takes the task it is awaiting and an implementation of `OnCompleted`.
The `OnCompleted` method is called when the task completes, and it should trigger the continuation action.

```csharp
public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
    private readonly DomeTrainTask _task;

    internal DomeTrainTaskAwaiter(DomeTrainTask task) => _task = task;

    public void OnCompleted(Action continuation) => _task.ContinueWith(continuation);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=115)

The .NET state machine also requires the awaiter to expose an `IsCompleted` property.
This property allows the state machine to check if the task has already finished; if it has, the state machine can continue execution synchronously instead of yielding.

```csharp
public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
    private readonly DomeTrainTask _task;

    internal DomeTrainTaskAwaiter(DomeTrainTask task) => _task = task;

    public bool IsCompleted => _task.IsCompleted;

    public void OnCompleted(Action continuation) => _task.ContinueWith(continuation);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=130)

Finally, the awaiter must provide a `GetResult()` method.
This method is called by the state machine once the task is complete to retrieve the result or propagate any exceptions that occurred during execution.
In our implementation, `GetResult()` calls the task's `Wait()` method to ensure any exceptions are thrown correctly.

```csharp
public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
    private readonly DomeTrainTask _task;

    internal DomeTrainTaskAwaiter(DomeTrainTask task) => _task = task;

    public bool IsCompleted => _task.IsCompleted;

    public void OnCompleted(Action continuation) => _task.ContinueWith(continuation);

    public DomeTrainTaskAwaiter GetAwaiter() => this;

    public void GetResult() => _task.Wait();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=145)

With the awaiter defined, the `DomeTrainTask` itself must be updated to include a `GetAwaiter()` method.
This is the "magic" method that the C# compiler looks for when it encounters the `await` keyword.

```csharp
public class DomeTrainTask
{
    public DomeTrainTaskAwaiter GetAwaiter() => new(this);

    // ... existing SetResult, SetException, and CompleteTask implementation
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=175)

This pattern relies on "duck typing." The `await` keyword does not require the task to implement a specific interface or inherit from a specific base class.
Instead, the compiler simply checks if the type has a method named `GetAwaiter()` that returns a type implementing `INotifyCompletion`.
If these conditions are met, the type is awaitable.

When running this in a console application, you will notice that while the `await` keyword correctly pauses execution until the task completes, the code may not resume on the original thread.

```csharp
// Program.cs
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

await DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=325)

In UI applications (like WinForms or WPF), a `SynchronizationContext` ensures that after an `await`, execution returns to the UI thread so the application remains responsive.
However, console applications do not have a `SynchronizationContext`.
Consequently, once the `await` is hit and the task completes, the continuation runs on whatever thread is available (typically a ThreadPool thread) rather than returning to the original thread (Thread 1).

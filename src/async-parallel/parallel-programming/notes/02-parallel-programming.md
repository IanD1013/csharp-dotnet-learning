# Parallel programming

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 2
> 5 lessons · ~58:20
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Parallel vs Asynchronous](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-vs-asynchronous-69955972/) | 1:18 | [↓](#1-parallel-vs-asynchronous) |
| 2 | [Introduction to Asynchronous Programming](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/) | 15:17 | [↓](#2-introduction-to-asynchronous-programming) |
| 3 | [Introduction to async / await](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/) | 14:15 | [↓](#3-introduction-to-async--await) |
| 4 | [Introduction to Parallel Programming](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/) | 4:14 | [↓](#4-introduction-to-parallel-programming) |
| 5 | [Deadlocks and Race Conditions](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/) | 23:16 | [↓](#5-deadlocks-and-race-conditions) |

---

## 1. Parallel vs Asynchronous

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-vs-asynchronous-69955972/) · 1:18

### Summary

This lesson introduces the distinction between asynchronous and parallel programming in C#.
It establishes the prerequisite knowledge required for the course, specifically focusing on the Task-based Asynchronous Pattern (TAP), the mechanics of the async-await state machine, and the proper application of asynchronous best practices.

### Key concepts

* The distinction between asynchronous and parallel programming.
* The role of Task and Task<T> in C#.
* Compiler-level transformations of async and await code.
* Asynchronous best practices and the use of ConfigureAwait(false).

### Lesson notes

This lesson provides an introduction to parallel programming by distinguishing it from asynchronous programming.
While both are central to concurrency in C#, they address different execution scenarios.

A prerequisite for this course is a firm understanding of asynchronous programming in C#, specifically the Task-based Asynchronous Pattern (TAP).
Key topics that form the basis for parallel implementation include:

* **Tasks**: The fundamental units of work represented by Task and Task<T>.
* **Async-Await Mechanics**: How the C# compiler modifies code to implement the async and await state machine.
* **Best Practices**: Standard patterns for implementing asynchronous logic.
* **ConfigureAwait**: The use of ConfigureAwait(false) to manage how continuations are handled by the task scheduler.

---

## 2. Introduction to Asynchronous Programming

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/) · 15:17

### Summary

Asynchronous programming in C# centers on the Task class, which represents an operation that will complete in the future.
This lesson introduces the fundamental concepts of tasks, including their execution on background threads, the lifecycle states defined by the TaskStatus enum, and the three possible completion outcomes: success, faulting due to exceptions, or cancellation via tokens.
By monitoring properties like IsCompleted and Status, developers can track the progress of background operations without blocking the main execution thread, ensuring applications remain responsive during long-running tasks.

### Key concepts

*   The `Task` class as a representation of code that executes asynchronously.
*   Task outcomes: Successful completion, Faulted (exceptions), or Canceled.
*   The `TaskStatus` enum and the eight distinct states in a task's lifecycle.
*   Using `Task.Run` to offload work to the .NET thread pool.
*   Monitoring task state using properties such as `IsCompleted`, `IsFaulted`, and `IsCanceled`.
*   The role of `CancellationTokenSource` and `CancellationToken` in managing task termination.

### Lesson notes

Asynchronous programming allows code to execute on a background thread, preventing the main or UI thread from freezing during long-running operations.
In C#, a `Task` is essentially a promise of work that will finish later.
Once a task is initiated, its progress can be monitored through several boolean properties.

```csharp
var task = Task.Run(() => SimulateLongRunningFunction(CancellationToken.None));

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=25)

A completed task always results in one of three outcomes:

1.  **Successful Completion**: The task finished its work without errors or cancellation.
2.  **Faulted**: An unhandled exception occurred within the task.
3.  **Canceled**: The task was stopped early via a `CancellationToken`.

#### Task States

While there are three final outcomes, a task can move through eight different states during its lifecycle, defined by the `TaskStatus` enum.

```csharp
public enum TaskStatus
{
    /// <summary>The task has been initialized but has not yet been scheduled.</summary>
    Created,
    /// <summary>The task is waiting to be activated and scheduled internally by the .NET infrastructure.</summary>
    WaitingForActivation,
    /// <summary>The task has been scheduled for execution but has not yet begun executing.</summary>
    WaitingToRun,
    /// <summary>The task is running but has not yet completed.</summary>
    Running,
    /// <summary>The task has finished executing and is implicitly waiting for attached child tasks to complete.</summary>
    WaitingForChildrenToComplete,
    /// <summary>The task completed execution successfully.</summary>
    RanToCompletion,
    /// <summary>Cancellation by throwing an OperationCanceledException.</summary>
    Canceled,
    /// <summary>The task completed due to an unhandled exception.</summary>
    Faulted,
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=130)

Key states include:

*   **Created**: The state after the task constructor has exited but before the task is scheduled.
*   **WaitingToRun**: The task is scheduled with the `TaskScheduler` and is waiting for a background thread to become available.
*   **WaitingForActivation**: The task is waiting for a dependent operation (common when using `ContinueWith` or `TaskCompletionSource`).
*   **Running**: The code inside the task is currently executing.

#### Monitoring Task Execution

In a typical scenario, `Task.Run` is used to offload a function to a background thread.
Because the main thread continues execution immediately, checking the task status milliseconds after creation often reveals the task is in the `WaitingToRun` state, as the .NET runtime requires a small amount of overhead to locate and assign a background thread.

```csharp
using System.Diagnostics;

Trace.WriteLine($"Program Started on Thread {Environment.CurrentManagedThreadId}");

var task = Task.Run(() => SimulateLongRunningFunction(CancellationToken.None));

Trace.WriteLine($"What is the Task status? {task.Status}");

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Thread.Sleep( timeout: TimeSpan.FromSeconds(3));

Trace.WriteLine($"What is the Task status? {task.Status}");

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Trace.WriteLine("Program Completed");
return;

void SimulateLongRunningFunction(CancellationToken token)
{
    Trace.WriteLine($"Long running function started on Thread {Environment.CurrentManagedThreadId}");

    token.ThrowIfCancellationRequested();
    Thread.Sleep( timeout: TimeSpan.FromSeconds(2));

    Trace.WriteLine($"Long running function completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=385)

#### Handling Faults and Cancellation

If an exception is thrown inside the task, the task immediately stops and sets its `IsFaulted` property to `true`.
When the task is eventually awaited, this exception is re-thrown to the developer.

```csharp
void SimulateLongRunningFunction(CancellationToken token)
{
    Trace.WriteLine($"Long running function started on Thread {Environment.CurrentManagedThreadId}");

    throw new Exception();

    token.ThrowIfCancellationRequested();
    Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

    Trace.WriteLine($"Long running function completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=610)

Cancellation is managed using a `CancellationTokenSource`.
If a token is canceled (e.g., by setting a `TimeSpan.Zero` delay), the task should transition to the `Canceled` state.
However, simply throwing an `OperationCanceledException` inside the method may sometimes result in a `Faulted` state if the task itself was not initialized with the token.

To ensure the .NET runtime correctly identifies a task as canceled, the `CancellationToken` must be passed directly into the `Task.Run` method as well as the function it executes.
This allows the runtime to check the token status before the task even begins or during its execution.

```csharp
using System.Diagnostics;

Trace.WriteLine($"Program Started on Thread {Environment.CurrentManagedThreadId}");

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.Zero);
var task = Task.Run(() => SimulateLongRunningFunction(cancellationTokenSource.Token), cancellationTokenSource.Token);

Trace.WriteLine($"What is the Task status? {task.Status}");

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Thread.Sleep(TimeSpan.FromSeconds(3));

Trace.WriteLine($"What is the Task status? {task.Status}");

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Trace.WriteLine("Program Completed");

void SimulateLongRunningFunction(CancellationToken token)
{
	Trace.WriteLine($"Long running function started on Thread {Environment.CurrentManagedThreadId}");

	token.ThrowIfCancellationRequested();
	Thread.Sleep(TimeSpan.FromSeconds(2));

	Trace.WriteLine($"Long running function completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=850)

---

## 3. Introduction to async / await

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/) · 14:15

### Summary

This lesson introduces the async and await keywords in C#, explaining how they transform methods into state machines to handle asynchronous operations without blocking threads.
By transitioning from blocking calls like Thread.Sleep to non-blocking alternatives like Task.Delay, developers can prevent thread pool exhaustion and ensure that applications remain responsive, particularly in UI-heavy or high-volume API environments.
The discussion further highlights how these keywords simplify development by allowing standard try-catch blocks to handle exceptions from faulted tasks and demonstrates the implementation of task cancellation using CancellationToken to maintain efficient resource management.

### Key concepts

- async and await keywords
- IAsyncStateMachine
- Blocking vs. Non-blocking calls
- Thread Pool starvation
- UI Thread responsiveness
- Task-based Asynchronous Pattern (TAP)
- Thread Pool Exhaustion
- Non-blocking Await
- Exception Propagation
- Task Faulting
- Asynchronous Cancellation

### Lesson notes

#### The Async and Await Keywords

In C#, `async` and `await` are the primary keywords used to manage asynchronous operations.
The `await` keyword allows the program to pause the execution of a method until a specific task has completed.
The `async` keyword is used in the method signature to enable the use of `await` within that method.

Under the hood, the .NET compiler transforms a method marked with `async` into an `IAsyncStateMachine`.
This state machine manages the execution flow, allowing the thread to be released back to the thread pool while waiting for an operation to finish, and then resuming execution once the task is complete.

```csharp
async Task SimulateLongRunningFunction(CancellationToken token)
{
    Trace.WriteLine($"Started on Thread {Environment.CurrentManagedThreadId}");

    await Task.Delay(TimeSpan.FromSeconds(2), token);

    Trace.WriteLine($"Completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=55)

#### The Problem with Blocking Calls

Using `Thread.Sleep` is considered a "locking" or blocking call.
When a thread calls `Thread.Sleep`, it is locked and cannot perform any other work until the timer expires.
In a high-scale environment, such as a web API serving thousands of users, this leads to inefficiency.
Even on powerful servers, locking threads prevents the .NET thread pool from utilizing those threads for other incoming requests.

In the following example, the main thread (Thread 1) is locked for three seconds, rendering it unavailable to the system:

```csharp
using System.Diagnostics;

Trace.WriteLine($"Program Started on Thread {Environment.CurrentManagedThreadId}");

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.Zero);
var task = Task.Run(() => SimulateLongRunningFunction(CancellationToken.None));

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Thread.Sleep(timeout: TimeSpan.FromSeconds(3));

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Trace.WriteLine("Program Completed");
return;

void SimulateLongRunningFunction(CancellationToken token)
{
    Trace.WriteLine($"Started on Thread {Environment.CurrentManagedThreadId}");

    token.ThrowIfCancellationRequested();
    Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

    Trace.WriteLine($"Completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=115)

#### Transitioning to Non-blocking Code

To optimize the code, `Thread.Sleep` should be replaced with `await`.
When you `await` a task, the code execution pauses until that specific task is finished, rather than waiting for an arbitrary amount of time.
This eliminates wasted CPU cycles.
For instance, if a task completes in two seconds but you used `Thread.Sleep` for three seconds, you would burn a full second of unnecessary CPU time.

```csharp
// Replacing Thread.Sleep(3) with a non-blocking await
await task;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=220)

#### Benefits of Non-blocking Await

Using `await` provides two primary benefits:

1.  **Efficiency**: The .NET runtime notifies the code to resume exactly when the task completes, avoiding unnecessary delays.
2.  **Responsiveness**: The calling thread is not blocked. This is critical for UI-based applications (Desktop, Mobile, or Web), where Thread 1 is typically the UI thread. If the UI thread is blocked by `Thread.Sleep`, the application freezes and cannot interact with the user or redraw the screen. Using `await` frees the UI thread to remain responsive while the background task runs.

As a best practice, methods using the `async` keyword should return a `Task` (or `Task<T>`) rather than `void` to avoid the dangers associated with `async void` methods.

```csharp
using System.Diagnostics;

Trace.WriteLine($"Program Started on Thread {Environment.CurrentManagedThreadId}");

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.Zero);
var task = Task.Run(() => SimulateLongRunningFunction(CancellationToken.None));

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

try
{
	await task; // Thread 1 will not be blocked and can now do other things
}
catch (Exception e)
{
	Trace.WriteLine(e);
}

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Trace.WriteLine("Program Completed");

async Task SimulateLongRunningFunction(CancellationToken token)
{
	Trace.WriteLine($"Started on Thread {Environment.CurrentManagedThreadId}");

	token.ThrowIfCancellationRequested();
	await Task.Delay(TimeSpan.FromSeconds(2), token); // Thread will return to the Thread Pool until Task.Delay is completed

	Trace.WriteLine($"Completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=385)

#### Thread Pool Efficiency and Responsiveness

Unlike `Thread.Sleep`, which is a blocking call that locks the current thread, the `await` keyword is non-blocking.
When a program encounters an `await` on a task (such as `Task.Delay`), the executing thread is released.
If it is a UI thread, it returns to the message loop to keep the interface responsive.
If it is a background thread, it returns to the thread pool to be used by other operations.

This mechanism is critical for preventing **thread pool exhaustion**.
In a high-traffic API receiving thousands of calls per second, using blocking calls would eventually consume all available threads in the pool.
Once the pool is exhausted, the server cannot process new requests.
By using `async` and `await`, threads are freed during I/O-bound or delayed operations, allowing a smaller number of threads to handle a much larger volume of concurrent work.

```csharp
using System.Diagnostics;

Trace.WriteLine($"Program Started on Thread {Environment.CurrentManagedThreadId}");

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.Zero);
var task = Task.Run(() => SimulateLongRunningFunction(CancellationToken.None));

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

await task; // Thread 1 will not be blocked and can now do other things

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Trace.WriteLine("Program Completed");
return;

async Task SimulateLongRunningFunction(CancellationToken token)
{
    Trace.WriteLine($"Started on Thread {Environment.CurrentManagedThreadId}");

    token.ThrowIfCancellationRequested();
    await Task.Delay(TimeSpan.FromSeconds(2), token); // Thread will return to the Thread Pool until Task.Delay is completed

    Trace.WriteLine($"Completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=445)

#### Exception Handling with Await

A significant advantage of the `await` keyword is how it handles exceptions.
If a task enters a faulted state (i.e., an exception was thrown inside the task), the `await` keyword will automatically re-throw that exception at the point of the await.

Without `await`, a developer would need to manually check the `Task.IsFaulted` property and inspect the `Task.Exception` property.
With `await`, you can wrap the call in a standard `try-catch` block.
This ensures that exceptions occurring on background threads are properly propagated to the calling context, preventing the application from silently failing or crashing without logs.

```csharp
Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

try
{
    await task; // The await keyword re-throws the exception if the task is faulted
}
catch (Exception e)
{
    Trace.WriteLine(e);
}

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

Trace.WriteLine("Program Completed");
return;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=670)

#### Task Cancellation

Asynchronous tasks should respect cancellation tokens to allow for clean shutdowns or user-initiated cancellations.
By passing a `CancellationToken` to `Task.Run` and internal methods like `Task.Delay`, the .NET runtime becomes aware of the cancellation request.
If a token is already expired (e.g., created with `TimeSpan.Zero`), the task will transition to the `Canceled` state immediately, throwing an `OperationCanceledException` when awaited.

```csharp
var cancellationTokenSource = new CancellationTokenSource(TimeSpan.Zero);
var task = Task.Run(() => SimulateLongRunningFunction(cancellationTokenSource.Token), cancellationTokenSource.Token);

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");

try
{
    await task; // Thread 1 will not be blocked and can now do other things
}
catch (Exception e)
{
    Trace.WriteLine(e);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=835)

---

## 4. Introduction to Parallel Programming

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/) · 4:14

### Summary

Parallel programming in .NET involves executing multiple tasks concurrently, typically by leveraging background threads.
Unlike sequential execution, tasks can begin running as soon as they are initiated, allowing the application to perform multiple operations—such as API requests or long-running computations—simultaneously.
The await keyword is then used to synchronize these tasks, ensuring they finish and any exceptions are properly handled before the program continues.

### Key concepts

*   **Concurrency**: Running multiple tasks at the same time, often on different background threads.
*   **Task Scheduling**: The .NET runtime automatically schedules tasks to run on background threads as soon as they are created.
*   **Immediate Execution**: Tasks start running upon invocation (e.g., via `GetAsync` or `Task.Run`), not when they are awaited.
*   **Exception Propagation**: Awaiting a task is required to re-throw exceptions that occurred during background execution, allowing for graceful error handling.
*   **Thread Management**: The .NET task scheduler manages the assignment of tasks to available threads, which involves a small amount of initialization overhead.

### Lesson notes

Parallel programming builds upon the foundations of asynchronous programming, utilizing tasks, threads, and the `async/await` pattern.
At its core, it allows two or more tasks to run concurrently on background threads.

#### Concurrent API Requests

A common use case for parallel programming is making multiple network requests simultaneously.
In the following example, an `HttpClient` is used to fetch blog posts and authors from a website.

```csharp
var client = new HttpClient();

Task<HttpResponseMessage> getPostsTask = client.GetAsync(requestUri: "https://codetraveler.io/GetPosts");
Task<HttpResponseMessage> getAuthorsTask = client.GetAsync(requestUri: "https://codetraveler.io/GetAuthors");

await getPostsTask;
await getAuthorsTask;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/?t=25)

In .NET, tasks are scheduled and begin running on background threads as soon as they are created.
In this example, `getPostsTask` and `getAuthorsTask` both begin their network operations immediately after their respective `GetAsync` calls.
The `await` keyword is used later to ensure both tasks complete before the code proceeds.
Furthermore, `await` ensures that if an exception occurs during the background operation (such as a lost network connection), that exception is re-thrown at the point of the await so it can be handled.

#### Task Execution and Threading

When using `Task.Run`, the .NET task scheduler picks up the task and assigns it to a thread.
While there is a minor overhead for task initialization and scheduling, for most purposes, the tasks start running immediately upon invocation.

```csharp
using System.Diagnostics;

Trace.WriteLine($"Program Started on Thread {Environment.CurrentManagedThreadId}");

var backgroundTask1 = Task.Run(() => SimulateLongRunningFunction("Background Task 1", CancellationToken.None));
var backgroundTask2 = Task.Run(() => SimulateLongRunningFunction("Background Task 2", CancellationToken.None));

await backgroundTask1;
await backgroundTask2;

Trace.WriteLine("Program Completed");

async Task SimulateLongRunningFunction(string name, CancellationToken token)
{
	Trace.WriteLine($"{name} started on Thread {Environment.CurrentManagedThreadId}");

	await Task.Delay(TimeSpan.FromSeconds(2), token);

	Trace.WriteLine($"{name} completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/?t=145)

When executing this code, the main program starts on one thread (e.g., Thread 1), while the background tasks are assigned to different threads (e.g., Thread 6 and Thread 8) by the scheduler.

```plaintext
Program Started on Thread 1
Started Thread 2194981
Started Thread 2194982
Started Thread 2194983
Started Thread 2194952
Background Task 1 started on Thread 6
Background Task 2 started on Thread 8
Started Thread 2194984
Background Task 1 completed on Thread 6
Background Task 2 completed on Thread 8
Program Completed
Exited Thread 2194953
Exited Thread 2194971
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/?t=230)

This demonstrates parallel execution where multiple operations progress at the same time on distinct threads, eventually synchronizing back to the main execution flow via `await`.

---

## 5. Deadlocks and Race Conditions

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/) · 23:16

### Summary

Deadlocks and race conditions are common challenges in parallel programming that occur when multiple threads interact with shared resources or depend on each other's completion.
A deadlock arises from circular dependencies where threads wait indefinitely for one another, while a race condition occurs when concurrent access to a variable leads to unpredictable results.
C# provides several mechanisms to mitigate these issues, including the lock keyword for synchronous code, SemaphoreSlim for asynchronous scenarios, Interlocked for high-performance atomic operations, and Lazy<T> for thread-safe singleton initialization.

### Key concepts

- **Deadlock**: A situation where two or more threads are blocked indefinitely, each waiting for the other to release a resource.
- **Race Condition**: An anomaly where the output of a process is unexpectedly dependent on the sequence or timing of other events, typically when multiple threads modify shared state.
- **Locking**: Synchronous mutual exclusion that prevents multiple threads from entering a code block simultaneously.
- **SemaphoreSlim**: A lightweight alternative to Semaphore that supports async/await, allowing threads to return to the pool while waiting.
- **Interlocked**: A class providing atomic operations for variables shared by multiple threads, offering high-performance locking for specific value types.
- **Lazy<T>**: A wrapper for thread-safe, deferred initialization of objects, commonly used in singleton patterns.

### Lesson notes

#### Deadlocks and Circular Dependencies

A deadlock occurs when multiple threads are waiting for each other to finish before continuing.
This often happens due to circular references.
For example, if `backgroundTask1` awaits the completion of `backgroundTask2`, while `backgroundTask2` is simultaneously awaiting `backgroundTask1`, neither can ever complete.

```csharp
backgroundTask1 =
    Task.Run(() => SimulateLongRunningFunction(backgroundTask2, CancellationToken.None));
backgroundTask2 =
    Task.Run(() => SimulateLongRunningFunction(backgroundTask1, CancellationToken.None));

await backgroundTask1;
await backgroundTask2;

async Task SimulateLongRunningFunction(Task otherLongRunningFunction, CancellationToken token)
{
    await Task.Delay(TimeSpan.FromSeconds(2), token);
    await otherLongRunningFunction;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=70)

To avoid deadlocks, developers must ensure that tasks do not have circular dependencies where they wait on each other to perform the same operation.

#### Race Conditions

Race conditions occur when multiple threads access and modify the same code or variable at the same time.
Code that appears safe in a single-threaded context can fail in parallel execution.
In the following example, a loop calls an update method rapidly without awaiting the result, allowing multiple threads to overwrite a shared field.

```csharp
for (int i = 0; i < 100; i++)
{
    _database.UpdateResult(i % 2 == 0);
}

bool _result;

public async Task UpdateResult(bool result)
{
    await SaveResultToDatabase(result);
    _result = result;

    if (_result != result)
    {
        throw new Exception("Race Condition Detected");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=160)

When this logic is executed in parallel, one thread may change the value of `_result` after another thread has set it but before that first thread performs its validation check.

#### Synchronous Locking

The `lock` statement can be used to ensure that only one thread enters a specific block of code at a time.
When a thread encounters a lock, it acquires a mutual exclusion object.
If another thread attempts to enter the same lock, it is blocked until the first thread exits.

```csharp
async Task SimulateLongRunningFunction(string name, bool setResult, CancellationToken token)
{
    Trace.WriteLine($"{name} started on Thread {Environment.CurrentManagedThreadId}");

    await Task.Delay(TimeSpan.FromSeconds(2));

    lock (lockingMechanism)
    {
        result = setResult;

        Trace.WriteLine($"Set {nameof(result)} to {setResult}");

        if (result != setResult)
        {
            throw new Exception("Race Condition Detected");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=580)

While effective, the standard `lock` is a blocking call.
The calling thread cannot return to the thread pool while waiting for the lock to be released.

#### Asynchronous Locking with SemaphoreSlim

For asynchronous code, `SemaphoreSlim` is the preferred mechanism.
By initializing it with a count of `(1, 1)`, it acts as an asynchronous lock.
The `WaitAsync` method allows the calling thread to return to the thread pool if the semaphore is currently held by another thread, making it more efficient than a synchronous lock.

```csharp
async Task SimulateLongRunningFunction(string name, bool setResult, CancellationToken token)
{
    Trace.WriteLine($"{name} started on Thread {Environment.CurrentManagedThreadId}");

    await Task.Delay(TimeSpan.FromMilliseconds(2));

    await semaphoreSlim.WaitAsync(token);
    try
    {
        result = setResult;

        Trace.WriteLine($"Set {nameof(result)} to {setResult}");

        if (result != setResult)
        {
            throw new Exception("Race Condition Detected");
        }
    }
    finally
    {
        semaphoreSlim.Release();
    }

    Trace.WriteLine($"{name} completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=820)

#### High-Performance Atomic Operations

The `Interlocked` class provides the highest performance for locking when working with specific value types like `int`, `long`, `uint`, and `ulong`.
It performs atomic operations without the overhead of thread switching or blocking.

```csharp
async Task SimulateLongRunningFunction(string name, int setNumber, CancellationToken token)
{
	Trace.WriteLine($"{name} started on Thread {Environment.CurrentManagedThreadId}");

	await Task.Delay(TimeSpan.FromMilliseconds(2));

	Interlocked.CompareExchange(ref number, setNumber, number);

	Trace.WriteLine($"{name} completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=1120)

In this example, `CompareExchange` compares the current value of `number` at its memory address.
If it matches the expected value, it updates it to `setNumber` atomically.

#### Thread-Safe Singletons with Lazy<T>

When implementing the Singleton pattern, race conditions can occur if two threads attempt to initialize the instance for the first time simultaneously.
Using `Lazy<T>` ensures that the initialization function only runs once and is completely thread-safe.

```csharp
public class Database
{
	static readonly Lazy<Database> _database = new(() => new Database());
	bool _result;

	Database()
	{

	}

	public static Database Current => _database.Value;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=1330)

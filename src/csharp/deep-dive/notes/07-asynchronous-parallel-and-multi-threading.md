# Asynchronous, Parallel, and Multi-Threading

> Course: [Deep Dive: C#](https://dometrain.com/course/deep-dive-csharp/) · Chapter 7
> 6 lessons · ~1:00:23
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Async & Concurrency Patterns Intro](https://dometrain.com/take/course/deep-dive-csharp-2732260/async-concurrency-patterns-intro-54135872/) | 5:38 | [↓](#1-async--concurrency-patterns-intro) |
| 2 | [Threads](https://dometrain.com/take/course/deep-dive-csharp-2732260/threads-54135873/) | 8:09 | [↓](#2-threads) |
| 3 | [Background Workers](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/) | 9:36 | [↓](#3-background-workers) |
| 4 | [Task Objects](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/) | 9:18 | [↓](#4-task-objects) |
| 5 | [Async/Await](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/) | 18:39 | [↓](#5-asyncawait) |
| 6 | [Cancellation Tokens](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/) | 9:03 | [↓](#6-cancellation-tokens) |

---

## 1. Async & Concurrency Patterns Intro

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/async-concurrency-patterns-intro-54135872/) · 5:38

### Summary

This lesson provides a conceptual introduction to asynchronous programming, concurrency, parallelism, and multi-threading in C#.
It explores how modern multi-core processors can be leveraged to improve application efficiency by moving away from strictly sequential, single-threaded execution.
By distinguishing between IO-bound tasks (waiting for external resources) and CPU-bound tasks (intensive processing), the lesson highlights how interleaving and parallel execution can prevent application blocking and maximize resource utilization.

### Key concepts

* **Concurrency**: The ability to manage multiple tasks by interleaving their execution, making them appear to run simultaneously even on a single core.
* **Parallelism**: The true simultaneous execution of multiple tasks across different processor cores.
* **IO-Bound vs. CPU-Bound**: Distinguishing between tasks that wait for external resources (IO) and tasks that require intensive processing power (CPU).
* **Asynchronous Programming**: A model that allows a program to remain responsive by surrendering control during long-running tasks, often utilizing callbacks or deferred execution.
* **Multi-threading**: Running different chains of execution across multiple threads to increase performance, which also increases software complexity and debugging difficulty.

### Lesson notes

Modern software development requires moving beyond strictly sequential execution.
In a standard single-threaded program, the operating system schedules instructions line-by-line.
This model becomes inefficient when the program encounters tasks that are either IO-bound or CPU-intensive.
IO-bound operations—such as querying a database, accessing a file system, or making network requests—force the thread to wait for a response.
CPU-bound operations, such as expensive mathematical calculations or complex loops, can saturate a single core while leaving other available cores idle.

**Concurrency and Parallelism**
Concurrency is the broad concept of handling multiple tasks at once.
Even on a system with a single processor core, the operating system can achieve concurrency by interleaving tasks.
This process involves switching between different sets of work so rapidly that they appear to happen at the same time.
Parallelism builds upon this by utilizing multiple cores to truly execute different chains of instructions simultaneously across different threads.

**Asynchronous Programming and Callbacks**
Asynchronous programming is a specific approach to achieving concurrency, often implemented through a callback model.
When a program hits an IO-bound task, it can surrender control back to the thread of execution, allowing other work to proceed.
Once the IO operation completes, a callback is triggered to resume the original task.
This deferred execution prevents the application from sitting idle and ensures better resource utilization.

**Complexity and System Architecture**
Transitioning from sequential programming to multi-threading and parallelism significantly increases the complexity of the software.
It makes debugging more challenging and requires developers to have a clearer understanding of how the code behaves at any given point.
To master these concepts, it is helpful to understand the underlying mechanics of how a processor works and how the operating system schedules work.
While C# provides high-level constructs for these patterns, the fundamental principles of task scheduling and execution remain the same.

---

## 2. Threads

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/threads-54135873/) · 8:09

### Summary

The Thread class in C# provides a direct interface for interacting with the operating system's thread scheduler.
This lesson explores the foundational mechanics of thread creation, starting execution, and passing state into threads using ParameterizedThreadStart.
It also distinguishes between foreground threads, which keep an application alive, and background threads, which are terminated automatically when the main application thread exits.

### Key concepts

*   The `Thread` class as a low-level interface for operating system thread scheduling.
*   Thread instantiation using anonymous delegates or `ParameterizedThreadStart`.
*   Passing data to threads via object casting within the thread body.
*   The distinction between foreground threads (default) and background threads.
*   Manual thread management, including naming and lifecycle control.

### Lesson notes

In C#, the `Thread` object serves as the primary interface for working with threads scheduled by the operating system.
While modern C# development often uses higher-level abstractions, understanding the `Thread` class is essential for maintaining legacy codebases and understanding the underlying mechanics of concurrency.

To create a basic thread, you instantiate a `Thread` object and provide a delegate representing the work to be performed.
The thread does not begin execution until the `Start()` method is called.

```csharp
// Thread objects in C# allow us to create and manage threads.

Thread thread = new Thread(() =>
{
    // do stuff
});
thread.Start();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/threads-54135873/?t=40)

#### Passing Parameters to Threads

Most concurrent tasks require input data.
While a thread's method body might have access to variables in its surrounding scope, it is safer and more maintainable to pass required data explicitly when the thread starts.
This avoids unpredictable access patterns and state management issues.

C# facilitates this through the `ParameterizedThreadStart` delegate.
This delegate accepts a single parameter of type `object`.
Because it is not generic, the data must be cast back to its original type inside the thread's execution block.

```csharp
// Thread objects in C# allow us to create and manage threads.

Thread thread = new Thread(() =>
{
    // do stuff
});
thread.Start();

record ThreadContext(
    string Name,
    string Message);


// we can also pass parameters to the thread
ThreadContext thread1Context = new(
    Name: "Thread 1",
    Message: "Hello from thread 1!");

Thread thread1 = new Thread(new ParameterizedThreadStart(o =>
{
    ThreadContext context = (ThreadContext)o;

    Thread.CurrentThread.Name = context.Name;
    Console.WriteLine($"{Thread.CurrentThread.Name}: {context.Message}");
}));
thread1.Start(thread1Context);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/threads-54135873/?t=85)

#### Background Work and Loops

Threads are frequently used to process data in the background using loops.
By default, a C# program will not exit as long as a foreground thread is still running.
If a thread contains an infinite loop, the application will continue to execute indefinitely unless the thread is terminated or the process is killed.

```csharp
Console.WriteLine($"{Thread.CurrentThread.Name}: {context.Message}");
}));
thread1.Start(thread1Context);

// threads can be useful for running work in the background for us
ThreadContext thread2Context = new(
    Name: "Thread 2",
    Message: "Hello from thread 2!");
Thread thread2 = new Thread(new ParameterizedThreadStart(o =>
{
    ThreadContext context = (ThreadContext)o;

    Thread.CurrentThread.Name = context.Name;

    while (true)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}: {context.Message}");
        Thread.Sleep(1000);
    }
}));
thread2.Start(thread2Context);

record ThreadContext(
    string Name,
    string Message);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/threads-54135873/?t=295)

#### Foreground vs. Background Threads

To prevent a background task from keeping the application alive, you can mark a thread as a background thread by setting its `IsBackground` property to `true`.
When the main application thread finishes its work, any remaining background threads are automatically terminated by the runtime.

```csharp
// we can also set a thread to be a background thread
// which will automatically stop when the main thread stops
ThreadContext thread3Context = new(
    Name: "Thread 3",
    Message: "Hello from thread 3!");
Thread thread3 = new Thread(new ParameterizedThreadStart(o =>
{
    ThreadContext context = (ThreadContext)o;

    Thread.CurrentThread.Name = context.Name;

    while (true)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}: {context.Message}");
        Thread.Sleep(1000);
    }
}));
thread3.IsBackground = true;
thread3.Start(thread3Context);

Console.WriteLine("Press enter to stop Thread3.");
Console.ReadLine();

record ThreadContext(
    string Name,
    string Message);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/threads-54135873/?t=340)

Using threads directly is a primitive approach that requires the developer to manually manage scheduling, lifecycle conditions, and resource access.
While higher-level abstractions in C# simplify these tasks, understanding these fundamentals is necessary for troubleshooting complex concurrent behavior.

---

## 3. Background Workers

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/) · 9:36

### Summary

The `BackgroundWorker` class, found in the `System.ComponentModel` namespace, provides a high-level abstraction over threads for executing long-running tasks in the background.
While largely superseded by modern asynchronous patterns, it remains a common historical pattern in desktop applications like WinForms and WPF to maintain UI responsiveness.
It operates primarily through an event-driven model, utilizing the `DoWork` event for background execution and the `RunWorkerCompleted` event for post-processing, while offering built-in support for task cancellation and argument passing.

### Key concepts

- `System.ComponentModel.BackgroundWorker` class
- `DoWork` event for background execution logic
- `RunWorkerAsync` method for starting the worker
- `RunWorkerCompleted` event for post-execution tasks
- Argument passing via `DoWorkEventArgs.Argument`
- Cooperative cancellation using `WorkerSupportsCancellation` and `CancellationPending` properties

### Lesson notes

The `BackgroundWorker` class is a component in the `System.ComponentModel` namespace designed to run operations on a separate thread.
Historically, this component was heavily utilized in Windows Forms (WinForms) and Windows Presentation Foundation (WPF) applications to perform heavy lifting without freezing the user interface.

To use a `BackgroundWorker`, instantiate the class and subscribe to its `DoWork` event.
This event handler contains the code that will execute on a background thread.
Unlike the `Thread` class which often uses anonymous delegates, `BackgroundWorker` uses an event-based model.
The syntax for hooking up an event handler uses the `+=` operator, often combined with an anonymous delegate or lambda expression that accepts an `object sender` and `DoWorkEventArgs`.

```csharp
// we can use a BackgroundWorker to run a method in the background
// and historically this was used a lot in WinForms applications

// here's how we'd create a new BackgroundWorker
using System.ComponentModel;

BackgroundWorker worker1 = new BackgroundWorker();

// we can then subscribe to the DoWork event
worker1.DoWork += (object sender, DoWorkEventArgs e) =>
{
    // all of this code is what's run in the background
    while (true)
    {
        Console.WriteLine("Worker 1: Working in the background...");
        Thread.Sleep(1000);
    }

    Console.WriteLine("Worker 1: DoWork has completed.");
};

worker1.RunWorkerAsync();

Console.WriteLine("Press enter to exit.");
Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/?t=100)

By default, `BackgroundWorker` threads are background threads, meaning the application will not wait for them to finish before exiting.
In a console application, a `Console.ReadLine()` is necessary to keep the process alive while the worker runs.

#### Passing Arguments

Similar to parameterized threads, `BackgroundWorker` supports passing data to the background task.
The `RunWorkerAsync` method accepts an `object` argument, which is then accessible within the `DoWork` handler via the `Argument` property of the `DoWorkEventArgs` object.
This allows for dynamic configuration of the background task, such as specifying the number of iterations for a loop.

```csharp
while (true)
        {
            Console.WriteLine("Worker 1: Working in the background...");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Worker 1: DoWork has completed.");
    };
    worker1.RunWorkerAsync();


    // like with threads, we can pass parameters into the background worker
    // when we start it
    BackgroundWorker worker2 = new BackgroundWorker();
    worker2.DoWork += (sender, e) =>
    {
        int iterations = (int)e.Argument;
        for (int i = 0; i < iterations; i++)
        {
            Console.WriteLine($"Worker 2: Working in the background on iteration {i}...");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Worker 2: DoWork has completed.");
    };
    worker2.RunWorkerAsync(5);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/?t=235)

#### Handling Completion

To execute code after the background task finishes, subscribe to the `RunWorkerCompleted` event.
This is particularly useful in UI applications because it allows you to transition back to the main UI thread to update visual elements after the background work is done.
This event fires automatically once the `DoWork` handler has finished its execution.

```csharp
// like with threads, we can pass parameters into the background worker
    // when we start it
    BackgroundWorker worker2 = new BackgroundWorker();
    worker2.DoWork += (sender, e) =>
    {
        int iterations = (int)e.Argument;
        for (int i = 0; i < iterations; i++)
        {
            Console.WriteLine($"Worker 2: Working in the background on iteration {i}...");
            Thread.Sleep(1000);
        }

        Console.WriteLine("Worker 2: DoWork has completed.");
    };
    worker2.RunWorkerAsync(5);

    // we can also subscribe to the RunWorkerCompleted event
    worker2.RunWorkerCompleted += (sender, e) =>
    {
        Console.WriteLine("Background Worker 2 has completed.");
    };


    Console.WriteLine("Press enter to exit.");
    Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/?t=295)

#### Cancellation

`BackgroundWorker` provides a built-in mechanism for cooperative cancellation, which is a safer alternative to the obsolete `Thread.Abort` method.
To enable this, set the `WorkerSupportsCancellation` property to `true`.
Inside the `DoWork` loop, the code must periodically check the `CancellationPending` property.
If it is true, the loop should exit gracefully.

```csharp
// here's how we'd create a new BackgroundWorker
using System.ComponentModel;

BackgroundWorker worker1 = new BackgroundWorker();
worker1.DoWork += (object sender, DoWorkEventArgs e) =>
{
    // all of this code is what's run in the background
    while (!worker1.CancellationPending)
    {
        Console.WriteLine("Worker 1: Working in the background...");
        Thread.Sleep(1000);
    }

    Console.WriteLine("Worker 1: DoWork has completed.");
};

worker1.WorkerSupportsCancellation = true;
worker1.RunWorkerAsync();


// like with threads, we can pass parameters into the background worker
// when we start it
BackgroundWorker worker2 = new BackgroundWorker();
worker2.DoWork += (sender, e) =>
{
    int iterations = (int)e.Argument;
    for (int i = 0; i < iterations; i++)
    {
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/?t=415)

Cancellation is triggered by calling the `CancelAsync()` method on the worker instance.
It is important to note that `CancelAsync()` does not immediately interrupt the thread; if the worker is currently in a `Thread.Sleep` call, it will finish the sleep before checking the `CancellationPending` flag and exiting.

```csharp
{
            int iterations = (int)e.Argument;
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"Worker 2: Working in the background on iteration {i}...");
                Thread.Sleep(1000);
            }
        };
        worker2.RunWorkerAsync(5);

        // we can also subscribe to the RunWorkerCompleted event
        //worker2.RunWorkerCompleted += (sender, e) =>
        //{
        //    Console.WriteLine("Background Worker 2 has completed.");
        //};

        // we can cancel the background worker too! let's modify
        // worker2 to cancel worker1 when it finishes.

        worker2.RunWorkerCompleted += (sender, e) =>
        {
            Console.WriteLine("Background Worker 2 has completed.");
            worker1.CancelAsync();
        };

        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/background-workers-54135874/?t=460)

#### Threading and UI

In multi-threaded applications, UI updates must typically occur on the main UI thread.
Attempting to update UI components directly from the `DoWork` thread will often cause errors.
The `RunWorkerCompleted` event is the standard location to perform UI updates because it fires after the background work is finished, allowing for a safe transition back to the main thread.
If unsure which thread is active, debugging tools can be used to check thread IDs to ensure the code is executing on the main thread.

---

## 4. Task Objects

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/) · 9:18

### Summary

The Task object in C# provides a high-level abstraction for asynchronous programming, offering more flexibility and control than traditional threads or background workers.
Tasks are managed by a task scheduler that determines thread assignment, and they behave like background threads by default, requiring explicit synchronization methods such as Task.WaitAll or Task.WaitAny to ensure completion.
Beyond basic execution, tasks support a fluent builder pattern through continuations, allowing developers to chain operations and implement specialized error handling using AggregateException to manage multiple concurrent failures.

### Key concepts

* Task.Run for offloading work to the thread pool
* Task Scheduler's role in thread assignment
* Synchronization with Task.WaitAll and Task.WaitAny
* Fluent task chaining using ContinueWith
* Conditional continuations with TaskContinuationOptions
* Exception aggregation via AggregateException

### Lesson notes

Tasks represent a significant evolution in C# asynchronous programming, providing a more intuitive flow compared to raw threads.
When a task is created using `Task.Run`, it is passed a delegate containing the code to be executed.
While tasks often run on background threads, the specific thread assignment is handled by the Task Scheduler.

```csharp
// Tasks in C# allow us to perform asynchronous operations.
// Using Task objects, we can get more control over
// how we'd like our asynchronous operations to be executed.

// Let's run some tasks and see which threads they are executed on:
Console.WriteLine($"Main Thread Id: {Thread.CurrentThread.ManagedThreadId}");

Task task1 = Task.Run(() =>
{
    Console.WriteLine($"Task 1 Thread Id: {Thread.CurrentThread.ManagedThreadId}");
});

Task task2 = Task.Run(() =>
{
    Console.WriteLine($"Task 2 Thread Id: {Thread.CurrentThread.ManagedThreadId}");
});

Task task3 = Task.Run(() =>
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Task 3 Thread Id: {Thread.CurrentThread.ManagedThreadId} ({i})");
        Thread.Sleep(1000);
    }
});
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/?t=70)

Tasks behave similarly to background threads; if the main program finishes execution, the tasks are terminated immediately.
To ensure all tasks complete, use `Task.WaitAll`.
This method blocks the current thread until every task passed to it has finished.

```csharp
Task task3 = Task.Run(() =>
{
    for (int i = 0; i < 10; i++)
    {
        Console.WriteLine($"Task 3 Thread Id: {Thread.CurrentThread.ManagedThreadId} ({i})");
        Thread.Sleep(1000);
    }
});

// we should wait for the tasks to complete
// before allowing continued execution:
Task.WaitAll(task1, task2, task3);
Console.WriteLine("Tasks 1, 2, and 3 have completed.");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/?t=145)

For more granular control, `Task.WaitAny` can be used to wait for the first task in a collection to finish.
Additionally, individual tasks can be awaited using the `Wait` method.
This allows for structured parallel execution where multiple tasks run concurrently, followed by a specific task that only begins once the others are complete.

```csharp
// we should wait for the tasks to complete
// before allowing continued execution:
Task.WaitAll(task1, task2, task3);
Console.WriteLine("Tasks 1, 2, and 3 have completed.");

// we can even wait for all three tasks to complete
// before we start a 4th task, which we will also wait on
Task task4 = Task.Run(() =>
{
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"Task 4 Thread Id: {Thread.CurrentThread.ManagedThreadId} ({i})");
        Thread.Sleep(500);
    }
});
task4.Wait();
Console.WriteLine("Task 4 has completed.");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/?t=205)

Tasks also support a fluent builder pattern using the `ContinueWith` method.
This allows you to chain a second task to run immediately after a previous task completes.
The continuation task receives a reference to the previous task, enabling it to access results or status information.

```csharp
Task task4 = Task.Run(() =>
{
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"Task 4 Thread Id: {Thread.CurrentThread.ManagedThreadId} ({i})");
        Thread.Sleep(500);
    }
});
//task4.Wait();
Console.WriteLine("Task 4 has completed.");

// we can also use the "builder pattern" to chain things together
// on task objects:
Task task5 = Task.Run(() =>
{
    Console.WriteLine($"Task 5 Thread Id: {Thread.CurrentThread.ManagedThreadId}");
}).ContinueWith((prevTask) =>
{
    Console.WriteLine($"Task 5 Continuation Thread Id: {Thread.CurrentThread.ManagedThreadId}");
});
task5.Wait();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/?t=265)

Continuations can be configured to run only under specific conditions using `TaskContinuationOptions`.
For example, `OnlyOnFaulted` ensures a continuation only executes if the preceding task threw an exception.
This provides a mechanism for asynchronous error handling outside of standard try-catch blocks.

```csharp
Task task5 = Task.Run(() =>
{
    Console.WriteLine($"Task 5 Thread Id: {Thread.CurrentThread.ManagedThreadId}");
}).ContinueWith((prevTask) =>
{
    Console.WriteLine($"Task 5 Continuation Thread Id: {Thread.CurrentThread.ManagedThreadId}");
    throw new Exception("We intended to do this!");
}).ContinueWith((prevTask) =>
{
    Console.WriteLine($"Task 5 Continuation 2 Thread Id: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine($"{prevTask.Exception.GetType().Name}: {prevTask.Exception.Message}");
}, TaskContinuationOptions.OnlyOnFaulted);
task5.Wait();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/?t=355)

When tasks fail, they often throw an `AggregateException`.
Unlike standard exceptions that hold one inner exception, an `AggregateException` can contain multiple inner exceptions, representing all errors that occurred during the asynchronous operation.
Developers must inspect the `InnerExceptions` property to handle each failure appropriately.

```csharp
// aggregate exceptions are a way to handle multiple exceptions
// that can occur when working with tasks
AggregateException aggregateException = new(
    "This is the aggregate exception message.",
    new InvalidOperationException("This is the first inner exception."),
    new ArgumentException("This is the second inner exception."));

try
{
    throw aggregateException;
}
catch (AggregateException ex)
{
    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
    foreach (Exception innerEx in ex.InnerExceptions)
    {
        Console.WriteLine($"    {innerEx.GetType().Name}: {innerEx.Message}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/task-objects-54135875/?t=460)

---

## 5. Async/Await

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/) · 18:39

C# integrates asynchronous programming into the language through the async and await keywords, which allow developers to write non-blocking code that maintains a fluent, readable structure.
By returning Task or Task<T> instead of void or direct types, methods can surrender control to the system scheduler during long-running operations, enabling other work to execute concurrently.
Proper implementation requires propagating the async pattern throughout the call stack, as async void methods break exception handling and can lead to unhandled crashes or unpredictable application behavior.

### Key concepts

- **async/await Keywords**: Language-level constructs that simplify working with Task objects.
- **Return Types**: Use Task for methods that return nothing (replacing void) and Task<T> for methods returning a value.
- **Control Flow**: The await keyword surrenders control to the scheduler, allowing other tasks to run while the current operation is pending.
- **Concurrency**: Using Task.WhenAll or Task.WhenAny to manage multiple asynchronous operations simultaneously.
- **Async Void Dangers**: async void should be avoided (except for event handlers) because it cannot be awaited and exceptions cannot be caught by the caller.
- **Task.Yield**: A method to explicitly surrender control back to the scheduler to allow interleaving of other work.

### Lesson notes

The async and await keywords in C# provide a way to structure asynchronous code without explicitly chaining Task objects manually.
To define an asynchronous method, the async keyword is added to the method signature, and the return type is changed to Task (for void-equivalent methods) or Task<T> (for methods returning a value).
When a method returns a value, the compiler automatically wraps the returned object in a Task.

```csharp
// in order to make an async method, we use a new keyword
// and the Task object as the return type
async Task FirstAsyncMethod()
{
    await Task.Delay(TimeSpan.FromSeconds(1));
    // write code that is async here!
}

// if we need to return anything, we use Task<T>, the generic
// version, to be able to pass back data:
async Task<int> SecondAsyncMethod()
{
    await Task.Delay(TimeSpan.FromSeconds(1));
    return 42;
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=40)

When calling an asynchronous method, the await keyword is used to wait for completion.
Unlike a blocking .Wait() or .Result call, await tells the scheduler that the current execution path is suspended, allowing the scheduler to run other work in the meantime.
This surrendering of control is a fundamental difference from blocking.

```csharp
// within our context, we will not run code after the await
// until the async method has completed.
Console.WriteLine("awaiting FirstAsyncMethod...");
await FirstAsyncMethod();

// alternatively...
Console.WriteLine("awaiting FirstAsyncMethod again...");
Task firstAsyncMethodTask = FirstAsyncMethod();
await firstAsyncMethodTask;
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=175)

#### Concurrent Execution

Multiple asynchronous methods can be started simultaneously by calling them without an immediate await.
These tasks can then be managed collectively using Task.WhenAll (to wait for all to finish) or Task.WhenAny (to wait for the first one to finish).

```csharp
Console.WriteLine("Starting 3 async methods...");
Task<string> task1 = ThirdAsyncMethod(
    TimeSpan.FromSeconds(1),
    "Task 1 has completed.");
Task<string> task2 = ThirdAsyncMethod(
    TimeSpan.FromSeconds(2),
    "Task 2 has completed.");
Task<string> task3 = ThirdAsyncMethod(
    TimeSpan.FromSeconds(3),
    "Task 3 has completed.");

// and we can wait for them all to complete:
Console.WriteLine("Waiting for 3 async methods...");
await Task.WhenAll(task1, task2, task3);
Console.WriteLine("All 3 async methods have completed.");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=325)

If only the first result is needed, Task.WhenAny can be used to return the first task that completes:

```csharp
// alternatively, we could wait until any of them completes:
Task<string> firstTaskToComplete = await Task.WhenAny(task1, task2, task3);
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=400)

#### Asynchronous vs. Synchronous Behavior

Simply marking a method as async does not make it run asynchronously.
If the method contains blocking code (like Thread.Sleep) and no await expressions, it will execute synchronously on the calling thread, preventing other work from interleaving.

```csharp
async Task NotActuallyAsync()
{
    Console.WriteLine("Entering NotActuallyAsync...");
    Thread.Sleep(1000);
    Console.WriteLine("Exiting NotActuallyAsync...");
}

// we can call this method and await it, but it will not
// actually run asynchronously
Console.WriteLine("Calling NotActuallyAsync...");
Task notActuallyAsyncTask = NotActuallyAsync();
Console.WriteLine("awaiting NotActuallyAsync...");
await notActuallyAsyncTask;
Console.WriteLine("Finished awaiting NotActuallyAsync.");
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=415)

To ensure a method surrenders control even if it performs synchronous work, Task.Yield() can be used.
This explicitly allows the scheduler to interleave other tasks before continuing execution.

```csharp
async Task LeverageTaskYield()
{
    Console.WriteLine("Entering LeverageTaskYield...");
    await Task.Yield();
    Console.WriteLine("Continuing from LeverageTaskYield...");
    Thread.Sleep(1000);
    Console.WriteLine("Exiting LeverageTaskYield...");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=595)

#### Exception Handling and Async Void

It is critical to propagate async and await throughout the entire call stack.
When using async Task, exceptions can be caught using standard try-catch blocks because the Task object carries the exception information back to the caller.

```csharp
async Task TestCatchingExceptions()
{
    Console.WriteLine("Calling async method...");
    try
    {
        await ThisIsATask();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Caught exception from async method: {ex.Message}");
    }
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=760)

However, if a method is defined as async void, it cannot be awaited.
This breaks the call chain.
If an exception is thrown inside an async void method, it will not be caught by a try-catch block in the calling method, resulting in an unhandled exception that can crash the application.
async void should be restricted to event handlers.

```csharp
async Task ThisIsATask()
{
    Console.WriteLine("Entering ThisIsATask...");
    await Task.Delay(TimeSpan.FromSeconds(1));
    Console.WriteLine("Finished delay inside ThisIsATask...");

    throw new Exception("ThisIsATask has thrown an exception!");
}

async void ThisIsNotATask()
{
    Console.WriteLine("Entering ThisIsNotATask...");
    await Task.Delay(TimeSpan.FromSeconds(1));
    Console.WriteLine("Finished delay inside ThisIsNotATask...");

    throw new Exception("ThisIsNotATask has thrown an exception!");
}
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=790)

To prevent the application from closing before an async void exception occurs in a console environment, a ReadLine call is often necessary, though this does not fix the underlying exception handling issue.

```csharp
await TestCatchingExceptions();
Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/asyncawait-54135876/?t=985)

---

## 6. Cancellation Tokens

> [Watch the lesson](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/) · 9:03

### Summary

Cancellation in C# asynchronous programming is managed through the CancellationTokenSource and CancellationToken types.
This pattern allows developers to signal and respond to requests for task termination gracefully, either by polling the cancellation status or by handling OperationCanceledException.
By propagating tokens through the call stack and leveraging linked token sources, applications can efficiently manage resources and terminate long-running operations, such as web requests or background processing, when they are no longer needed.

### Key concepts

* **CancellationTokenSource (CTS)**: The controller object used to signal that operations should be cancelled.
* **CancellationToken**: A lightweight struct passed to asynchronous methods to monitor the cancellation state.
* **IsCancellationRequested**: A boolean property on the token used to manually poll for a cancellation signal.
* **ThrowIfCancellationRequested()**: A helper method that throws an `OperationCanceledException` if cancellation has been triggered.
* **OperationCanceledException**: The standard exception thrown when a task is cancelled; `TaskCanceledException` inherits from this type.
* **Linked Token Sources**: A mechanism to aggregate multiple cancellation tokens into a single source, allowing downstream tasks to be cancelled by any of the parent tokens.

### Lesson notes

In C#, cancellation is a first-class feature of the Task-based Asynchronous Pattern (TAP).
To implement cancellation, you begin with a `CancellationTokenSource`, which provides the `Token` that is passed into asynchronous methods.

```csharp
// we can use cancellation tokens with our async/await code
// to cancel tasks that are running:

// we can get a token from a CancellationTokenSource:
CancellationTokenSource cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

async Task LoopUntilCancelledAsync(
    CancellationToken cancellationToken)
{
    await Task.Yield();
    Console.WriteLine("Looping until cancelled...");

    while (!cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine("Waiting...");

        await Task.Delay(3000, cancellationToken);

        /*
        try
        {
            await Task.Delay(3000, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            break;
        }
        */
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/?t=55)

The `CancellationTokenSource` is responsible for triggering the cancellation via its `Cancel()` method.
The `CancellationToken` itself is read-only regarding the cancellation state; it can only be used to check if cancellation has been requested.

```csharp
// we can get a token from a CancellationTokenSource:
CancellationTokenSource cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

async Task LoopUntilCancelledAsync(
    CancellationToken cancellationToken)
{
    await Task.Yield();
    Console.WriteLine("Looping until cancelled...");

    while (!cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine("Waiting...");

        await Task.Delay(3000, cancellationToken);

        /* ... */
    }

    Console.WriteLine("Cancelled.");
}

Console.WriteLine("Press enter to cancel the loop.");
Task loopTask = LoopUntilCancelledAsync(cancellationToken);

Console.ReadLine();
cts.Cancel();

await loopTask;
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/?t=100)

There are two primary ways to handle cancellation within a method.
The first is manual polling using the `IsCancellationRequested` property.
The second is using the `ThrowIfCancellationRequested()` method, which immediately terminates the method by throwing an exception.
While using exceptions for control flow is generally discouraged, this is the standard way to ensure control is returned immediately to the caller.

```csharp
async Task LoopUntilCancelledAsync(
    CancellationToken cancellationToken)
{
    await Task.Yield();
    Console.WriteLine("Looping until cancelled...");

    cancellationToken.ThrowIfCancellationRequested();
    while (!cancellationToken.IsCancellationRequested)
    {
        Console.WriteLine("Waiting...");

        await Task.Delay(3000, cancellationToken);

        /* ... */
    }

    Console.WriteLine("Cancelled.");
}

Console.WriteLine("Press enter to cancel the loop.");
Task loopTask = LoopUntilCancelledAsync(cancellationToken);

Console.ReadLine();
cts.Cancel();

await loopTask;
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/?t=145)

Many built-in asynchronous APIs, such as `Task.Delay`, accept a `CancellationToken`.
If the token is cancelled while the API is awaiting, it will throw an `OperationCanceledException`.
To handle this gracefully and perform cleanup or finalization logic, you should wrap the awaitable call in a try-catch block.

```csharp
CancellationToken cancellationToken)
    {
        await Task.Yield();
        Console.WriteLine("Looping until cancelled...");

        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Waiting...");

            // await Task.Delay(3000, cancellationToken);

            try
            {
                await Task.Delay(3000, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }

        Console.WriteLine("Cancelled.");
    }

    Console.WriteLine("Press enter to cancel the loop.");
    Task loopTask = LoopUntilCancelledAsync(cancellationToken);

    Console.ReadLine();
    cts.Cancel();
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/?t=265)

In complex asynchronous workflows, you may need to support cancellation at different scopes.
You can use `CancellationTokenSource.CreateLinkedTokenSource` to create a new source that triggers when any of its parent tokens are cancelled.
This ensures that a high-level cancellation signal propagates automatically to all downstream operations.

```csharp
        // we can chain cancellation tokens together:
        CancellationTokenSource cts2 = new CancellationTokenSource();
        var cancellationToken2 = cts2.Token;
        var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken2);
        var linkedToken = linkedTokenSource.Token;

        Console.WriteLine("Using a linked token source!");
        Console.WriteLine("Press enter to cancel the loop.");
        Task loopTask = LoopUntilCancelledAsync(linkedToken);

        Console.ReadLine();
        cts2.Cancel();

        await loopTask;
```

[▶ Watch](https://dometrain.com/take/course/deep-dive-csharp-2732260/cancellation-tokens-54135877/?t=370)

It is highly recommended to include a `CancellationToken` parameter in every asynchronous method you write.
This allows the system to abort long-running tasks, such as web requests, to save CPU and I/O cycles when the result is no longer required.

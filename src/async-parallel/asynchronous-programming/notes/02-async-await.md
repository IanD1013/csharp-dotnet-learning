# Async/Await

> Course: [From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · Chapter 2
> 4 lessons · ~24:33
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Life Before Async Await](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/) | 11:30 | [↓](#1-life-before-async-await) |
| 2 | [Using Async](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/) | 5:47 | [↓](#2-using-async) |
| 3 | [Parallel vs Async](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/) | 4:03 | [↓](#3-parallel-vs-async) |
| 4 | [What is Task? What is Thread?](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-is-task-what-is-thread-62197112/) | 3:13 | [↓](#4-what-is-task-what-is-thread) |

---

## 1. Life Before Async Await

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/) · 11:30

### Summary

This lesson explores the challenges of asynchronous programming in C# before the introduction of the async and await keywords.
It demonstrates how calling asynchronous methods without proper synchronization causes the main thread to continue execution, often leading to premature application termination.
The lesson introduces the ContinueWith method as a historical way to handle task completion via callbacks, illustrating the resulting complexity known as callback hell and the unintuitive execution flow that occurs when background tasks are managed manually.

### Key concepts

- **Asynchronous Execution Flow**: Understanding how the main thread continues execution while a Task runs in the background.
- **Task-based Methods**: Defining methods that return a Task to represent an ongoing operation.
- **Callbacks with ContinueWith**: Using the TPL (Task Parallel Library) approach to execute code after a task completes.
- **Process Lifetime**: Managing the lifecycle of a console application to ensure background tasks finish before the process exits.
- **Callback Hell**: The readability and maintenance issues that arise from deeply nested asynchronous callbacks.

### Lesson notes

To understand the evolution of asynchronous programming in C#, we begin with a basic class hierarchy representing different food items.
The `Food` abstract class defines a `Cook` method that simulates a long-running operation using `Task.Delay`.

```csharp
namespace LifeBeforeAsync;

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

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=70)

When attempting to use this code in a standard console application, a common pitfall occurs.
If we call the `Cook` method without awaiting it, the application continues to the next line immediately.

```csharp
using LifeBeforeAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
turkey.Cook();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=130)

In this scenario, the console will output "Cooking Started" and "Cooking Turkey," but the program will terminate before the turkey finishes cooking.
This happens because when the `Cook` method hits the `await Task.Delay` line, control is returned to the calling method (the main program), which then reaches the end of its execution block and closes the process.

#### Using Callbacks with ContinueWith

Before the `async` and `await` keywords were standard for flow control, developers used the `ContinueWith` method.
This allows you to pass an `Action` that will execute once the initial task is complete.
To prevent the console application from closing while these background tasks are running, we can use `Console.ReadLine()` to keep the main thread alive.

```csharp
using LifeBeforeAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
turkey.Cook()
	.ContinueWith(_ =>
	{
		var gravy = new Gravy();
		gravy.Cook();
	});

//Code continues running on this line
Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=340)

#### The Execution Order and Callback Hell

Asynchronous code using callbacks does not execute in a linear, top-to-bottom fashion.
The following trace demonstrates the unintuitive jump in execution steps required to coordinate multiple tasks:

```csharp
Step 1 Console.WriteLine("Cooking Started");
Step 2 Console.WriteLine("Cooking Turkey");

Step 3 var turkey = new Turkey();
Step 4 turkey.Cook()
        .ContinueWith(() =>
        {
            Step 6     Console.WriteLine("Turkey Completed");
            Step 7     Console.WriteLine("Making Gravy");

            Step 8     var gravy = new Gravy();
            Step 9     gravy.Cook()
                    .ContinueWith(() =>
                    {
                        Step 10         Console.WriteLine("Gravy Completed");
                        Step 11         Console.WriteLine("Ready to eat");
                    });
        });

Step 5 Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=610)

In this flow, Step 5 (waiting for user input) occurs before Step 6 (the turkey finishing).
This pattern is known as "callback hell." It makes code difficult to read and maintain because the logical sequence of events is fragmented across nested lambda expressions rather than following the standard procedural flow of the language.

---

## 2. Using Async

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/) · 5:47

### Summary

This lesson demonstrates the transition from manual task continuation using ContinueWith to the modern async/await syntax in C#.
By utilizing the await keyword, developers can write asynchronous code that maintains a sequential, top-to-bottom readability similar to synchronous code.
The lesson explains how await pauses the execution of the current method and frees the calling thread while a background task completes, allowing the thread to perform other work before returning to finish the program execution.

### Key concepts

- **Async/Await Syntax**: Introduced in C# 5, these keywords allow asynchronous code to be written and read like synchronous code.
- **Sequential Readability**: Code execution follows a top-to-bottom, left-to-right flow, which reduces the cognitive load required to track callbacks.
- **Non-blocking Execution**: The `await` keyword frees the calling thread (such as the main thread) to perform other work while the awaited task runs in the background.
- **Task Completion and Resumption**: Once a background task finishes, it signals the calling thread to resume execution from the point of the `await` expression.

### Lesson notes

In earlier versions of C#, asynchronous programming relied heavily on manual callbacks using the `ContinueWith` method.
This approach often led to code that was difficult to follow because the logic was fragmented across different blocks.
Furthermore, developers frequently had to use hacks like `Console.ReadLine()` to prevent a console application from terminating before background tasks finished.

```csharp
using UsingAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
turkey.Cook()
    .ContinueWith(_ =>
    {
        var gravy = new Gravy();
        gravy.Cook();
    });

//Code continues running on this line
Console.ReadLine();
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/?t=25)

With the `async` and `await` keywords, this complexity is removed.
The code becomes much more intuitive because it can be read sequentially.
The program starts on the main thread, prints the starting message, and initializes the first task.
When it hits the `await` keyword, the `Cook` method runs in the background while the main thread is freed to handle other potential tasks.
Once the task completes, it returns to the calling thread to pick up exactly where it left off.

```csharp
using UsingAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
await turkey.Cook();

var gravy = new Gravy();
await gravy.Cook();

Console.WriteLine("Ready to eat");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/?t=90)

The underlying implementation of these food items relies on an abstract `Food` class that utilizes `Task.Delay` to simulate the time taken to cook asynchronously:

```csharp
namespace UsingAsync;

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
public class Gravy() : Food(TimeSpan.FromSeconds(1));
```

By using `await`, the program flow follows a clear, step-by-step progression.
This prevents bugs associated with "jumping" code execution and ensures that resources are not locked while waiting for I/O-bound or long-running operations to complete.
The final execution flow clearly logs each stage of the process from start to finish.

```csharp
Console.WriteLine("Cooking Started");
Console.WriteLine("Cooking Turkey");

var turkey = new Turkey();
await turkey.Cook();

Console.WriteLine("Turkey Completed");
Console.WriteLine("Making Gravy");

var gravy = new Gravy();
await gravy.Cook();

Console.WriteLine("Gravy Completed");
Console.WriteLine("Ready to eat");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/?t=220)

---

## 3. Parallel vs Async

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/) · 4:03

### Summary

This lesson demonstrates how to transition from sequential asynchronous operations to parallel execution using Task.WhenAll.
By initiating multiple tasks simultaneously rather than awaiting them one by one, developers can significantly reduce the total execution time of a workflow, provided the tasks are independent.
The lesson also highlights the conceptual flow of parallel tasks on background threads and warns against potential deadlocks when tasks have circular dependencies.

### Key concepts

- **Sequential Async**: Awaiting tasks one by one, which pauses execution of the method until each specific task completes.
- **Parallel Programming**: Executing multiple tasks concurrently to utilize resources more efficiently and reduce total wait time.
- **Task.WhenAll**: A method that accepts multiple tasks and returns a single task that completes only when all provided tasks have finished.
- **Background Threads**: The mechanism by which multiple asynchronous operations can progress simultaneously under the hood.
- **Deadlocks**: A state where two or more tasks are waiting for each other to finish, preventing the program from continuing.

### Lesson notes

In a standard asynchronous implementation, tasks are often awaited sequentially.
While this prevents blocking the main thread, it still forces tasks to wait for one another.
For example, in a cooking simulation, initializing and awaiting a turkey before starting the gravy results in unnecessary idle time.

```csharp
using ParallelProgramming;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
await turkey.Cook();

var gravy = new Gravy();
await gravy.Cook();

Console.WriteLine("Ready to eat");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/?t=10)

To optimize this, we can use parallel programming.
Instead of awaiting each task individually, we initialize the objects and then use `Task.WhenAll`.
This method allows us to pass in multiple tasks that will execute in parallel.

```csharp
using ParallelProgramming;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
var gravy = new Gravy();

await Task.WhenAll(turkey.Cook(), gravy.Cook());

Console.WriteLine("Ready to eat");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/?t=70)

When running this code, both the turkey and the gravy start cooking immediately.
Because the gravy has a shorter cook time (1 second) compared to the turkey (5 seconds), the gravy completes first.
However, the `await Task.WhenAll` line ensures that the program does not proceed to the final "Ready to eat" line until both tasks have finished.

#### Execution Flow

The execution can be broken down into distinct steps.
While the code is read top-to-bottom, the asynchronous nature allows for concurrent background operations:

1.  **Step 1**: Initialize the `Turkey` object.
2.  **Step 2**: Initialize the `Gravy` object.
3.  **Step 3**: Invoke `Task.WhenAll`. This triggers **Step 3a** (Turkey cooking) and **Step 3b** (Gravy cooking) on background threads simultaneously.
4.  **Step 4**: Once both background tasks complete, the program proceeds to the final output.

```csharp
Step 1 var turkey = new Turkey();
Step 2 var gravy = new Gravy();

                          Step 3a          Step 3b
Step 3 await Task.WhenAll(turkey.Cook(), gravy.Cook());

Step 4 Console.WriteLine("Ready to eat");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/?t=175)

#### Risks of Parallelism

While parallel programming is powerful, it introduces the risk of deadlocks.
A deadlock occurs if tasks become mutually dependent.
For instance, if `turkey.Cook()` was modified to wait for `gravy.Cook()` to finish, and `gravy.Cook()` was modified to wait for `turkey.Cook()` to finish, neither could ever complete.
The program would remain stuck at the `await Task.WhenAll` line indefinitely.
Developers must ensure that tasks executed in parallel are independent or properly synchronized to avoid these bugs.

---

## 4. What is Task? What is Thread?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-is-task-what-is-thread-62197112/) · 3:13

### Summary

In .NET, a Task represents an asynchronous operation and serves as a high-level abstraction over the System.Threading.Thread class.
While a Thread is an individual unit of execution with its own state and metadata, a Task allows developers to focus on the logic of the operation without manually managing thread pool allocation or context switching.
Understanding the relationship between these two is fundamental to mastering asynchronous programming in C#, as the .NET runtime handles the underlying complexity of thread management and execution context.

### Key concepts

- **Task**: An abstraction representing an asynchronous operation, found in the `System.Threading.Tasks` namespace.
- **Thread**: An individual unit of execution, functioning like a mini-program with its own set of instructions.
- **Execution Context**: A data structure containing security permissions, access levels, and caller information, enabling thread communication and state management.
- **Thread Metadata**: Properties including Thread ID, state (running/alive), priority, and culture settings.

### Lesson notes

In C#, a `Task` is an object that represents an asynchronous operation.
Located in the `System.Threading.Tasks` namespace, it acts as an abstraction over the `System.Threading.Thread` class.
Using a `Task` does not necessarily result in the creation of a new thread; nor does it guarantee that the operation will stay on a single thread or run on the calling thread.
The primary benefit of this abstraction is that it allows the .NET runtime to manage thread pool resources and context switching automatically.

```csharp
public async Task Cook()
{
    Console.WriteLine($"Cooking {Name}");
    await Task.Delay(_cookTime);
    Console.WriteLine($"{Name} Completed");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-is-task-what-is-thread-62197112/?t=10)

A `Thread` is an individual unit of execution.
It can be thought of as a mini-program: while a standard program is loaded into memory and executed line-by-line by the CPU, a thread contains only the specific steps required for its own execution path.

Threads maintain significant internal information managed by the runtime, including:

- **Thread ID and State**: Indicators of whether the thread is currently running, completed, or still alive.
- **Priority**: The execution importance assigned to the thread.
- **Current Culture**: Settings for language and dialect detection, allowing the system to automatically switch languages on screen.
- **Background Status**: Whether the thread is designated as a background thread.
- **Execution Context**: This context contains essential information such as security permissions, access levels, and the identity of the caller. It is the mechanism that allows the .NET runtime to move between threads and facilitates communication between different threads during execution.

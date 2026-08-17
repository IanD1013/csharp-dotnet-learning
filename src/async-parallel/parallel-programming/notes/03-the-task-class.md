# The Task class

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 3
> 4 lessons · ~45:51
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Tasks in Parallel](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/tasks-in-parallel-69955977/) | 1:31 | [↓](#1-tasks-in-parallel) |
| 2 | [Task.WhenAll](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/) | 18:18 | [↓](#2-taskwhenall) |
| 3 | [Task.WhenAny](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/) | 14:05 | [↓](#3-taskwhenany) |
| 4 | [Task.WhenEach (IAsyncEnumerable)](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/) | 11:57 | [↓](#4-taskwheneach-iasyncenumerable) |

---

## 1. Tasks in Parallel

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/tasks-in-parallel-69955977/) · 1:31

### Summary

This lesson introduces the 'Tasks in Parallel' section, focusing on .NET Parallel Task Library features like Task.WhenAll, Task.WhenAny, and Task.WhenEach.
It defines the course's scope as parallel programming and identifies prerequisite knowledge in asynchronous programming, including task internals and compiler transformations.

### Key concepts

- Parallel Task Library (PTL) options in .NET.
- Coordination of multiple tasks using `Task.WhenAll`, `Task.WhenAny`, and `Task.WhenEach`.
- Distinction between parallel programming and foundational asynchronous programming.
- Prerequisite knowledge: Task internals, async/await compiler behavior, `ConfigureAwait`, and cancellation tokens.

### Lesson notes

This section focuses on the practical implementation of parallel tasks using the .NET Parallel Task Library.
The instruction covers the coordination of multiple tasks to optimize performance and resource utilization.

The following methods are central to managing parallel task execution:

- **Task.WhenAll**: Creates a task that completes when all provided tasks have finished. It is the standard approach for aggregating results from multiple parallel operations.
- **Task.WhenAny**: Creates a task that completes when any one of the provided tasks has finished. This is useful for scenarios like timeouts or redundant requests where only the first result is needed.
- **Task.WhenEach**: Allows for iterating through tasks as they complete, enabling immediate processing of results without waiting for the entire set to finish.

This course specifically targets parallel programming techniques.
It assumes that the developer is already familiar with the fundamentals of asynchronous programming in C#, including how the compiler modifies code under the hood to create state machines, the proper use of `ConfigureAwait` to manage synchronization contexts, and the implementation of `CancellationToken` for cooperative cancellation.

---

## 2. Task.WhenAll

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/) · 18:18

### Summary

Task.WhenAll is a fundamental method in the System.Threading.Tasks library used to coordinate the parallel execution of multiple asynchronous operations.
It creates a single task that completes only when all provided tasks have finished, allowing developers to fire off background operations simultaneously and wait for the entire set to conclude before proceeding.
This lesson demonstrates its usage through HTTP requests and a simulated cooking application, highlighting how to manage task lists and integrate cancellation logic using both individual task tokens and the WaitAsync extension method.

### Key concepts

* Parallel execution of multiple Task objects using Task.WhenAll.
* Immediate execution of "hot" tasks upon initialization.
* Awaiting a collection of tasks via params or IEnumerable<Task>.
* Processing results from completed tasks in a collection.
* Implementing timeouts and cancellation with CancellationTokenSource.
* Using the WaitAsync extension method to apply cancellation to the aggregate task.

### Lesson notes

Task.WhenAll is part of the System.Threading.Tasks library and is used to run multiple tasks in parallel.
In .NET, as soon as a task is initialized, it typically begins running in the background.
For example, when making multiple HTTP requests, each GetAsync call returns a task that starts immediately.
By passing these tasks into Task.WhenAll and awaiting the result, the execution of the current method is paused until every task in the set has completed.

```csharp
var client = new HttpClient();

Task<HttpResponseMessage> getPostsTask = client.GetAsync("https://codetraveler.io/GetPosts");
Task<HttpResponseMessage> getAuthorsTask = client.GetAsync("https://codetraveler.io/GetAuthors");

await Task.WhenAll(getPostsTask, getAuthorsTask);

// All Tasks passed into Task.WhenAll are now Completed

var apiResponseTaskList = new List<Task<HttpResponseMessage>>();

// Run 5 simultaneous API Requests on background threads
for(int i = 0; i < 5; i++)
{
    Task<HttpResponseMessage> getAuthorResponse = client.GetAsync($"https://codetraveler.io/authors/{i}");
    apiResponseTaskList.Add(getAuthorResponse);
}

await Task.WhenAll(apiResponseTaskList);

// All Tasks in List are now Completed

foreach(Task<HttpResponseMessage> responseTask in apiResponseTaskList)
{
    // Deserialize HttpResponseMessage
    using var response = await responseTask;
    using var contentStream = await response.Content.ReadAsStreamAsync();

    Author author = await JsonSerializer.DeserializeAsync<Author>(contentStream);

    // Update UI
    AddAuthorToUI(author);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=10)

This pattern is also applicable when dealing with a dynamic number of tasks.
You can add multiple tasks to a List<Task<T>> and pass that list to Task.WhenAll.
Once the aggregate task completes, you can iterate through the list to process the results, such as deserializing JSON responses.

To demonstrate this in a practical scenario, consider a cooking simulation where different food items take different amounts of time to prepare.
We define an abstract Food class with a Cook method that simulates the cooking process using Task.Delay.

```csharp
using System.Diagnostics;

namespace TaskWhenAll;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task Cook(CancellationToken token = default)
	{
		Trace.WriteLine($"Cooking {Name}");
		await Task.Delay(_cookTime, token);
		Trace.WriteLine($"{Name} Completed");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=540)

In the main program, we initialize our food objects and a CancellationTokenSource.
We then use Task.WhenAll to start cooking all items simultaneously.
The logs will show that all items start cooking at once and complete according to their individual cookTime values.

```csharp
using System.Diagnostics;
using TaskWhenAll;

var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

Trace.WriteLine("Cooking Started");

await Task.WhenAll(
    turkey.Cook(cancellationTokenSource.Token),
    gravy.Cook(cancellationTokenSource.Token),
    mashedPotatoes.Cook(cancellationTokenSource.Token),
    stuffing.Cook(cancellationTokenSource.Token));

Trace.WriteLine("Dinner is ready!");
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=715)

When working with asynchronous tasks, it is a best practice to implement cancellation logic.
If the cooking process exceeds a specific threshold—simulating hungry guests leaving—a TaskCanceledException is thrown.
By wrapping the Task.WhenAll call in a try-catch block, we can handle these timeouts gracefully.

```csharp
using System.Diagnostics;
using TaskWhenAll;

var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(4));

Trace.WriteLine("Cooking Started");

try
{
	await Task.WhenAll(
		turkey.Cook(cancellationTokenSource.Token), 
		gravy.Cook(cancellationTokenSource.Token), 
		mashedPotatoes.Cook(cancellationTokenSource.Token), 
		stuffing.Cook(cancellationTokenSource.Token));

	Trace.WriteLine("Dinner is ready!");
}
catch (TaskCanceledException)
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=895)

While Task.WhenAll does not natively accept a CancellationToken, you can apply cancellation to the entire operation using the WaitAsync extension method.
This allows you to bolt a timeout onto the aggregate task returned by WhenAll, which is often cleaner than passing the same token into every individual method call.

```csharp
var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.FromSeconds(4));

Trace.WriteLine("Cooking Started");

try
{
    await Task.WhenAll(turkey.Cook(), gravy.Cook(), mashedPotatoes.Cook(), stuffing.Cook())
        .WaitAsync(cancellationTokenSource.Token);

    Trace.WriteLine("Dinner is ready!");
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Error: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=1030)

---

## 3. Task.WhenAny

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/) · 14:05

### Summary

Task.WhenAny provides a mechanism to monitor multiple tasks and respond as soon as the first one completes, enabling a "streaming" approach to data processing.
Unlike Task.WhenAll, which blocks until every task is finished, Task.WhenAny returns the specific task that reached a terminal state, allowing for immediate UI updates or early exits.
This lesson demonstrates the standard pattern of using Task.WhenAny within a loop to process a collection of tasks one by one as they finish, while emphasizing the importance of proper cancellation and result handling.

### Key concepts

*   **Immediate Completion**: `Task.WhenAny` returns a task that completes as soon as any of the tasks in the provided collection complete.
*   **Task Collection**: It accepts either a `params Task[]` or an `IEnumerable<Task>`.
*   **Return Value**: The method returns the `Task` object that finished first, not the result of that task.
*   **The While-Loop Pattern**: To process all tasks as they finish, developers typically use a `while` loop that awaits `WhenAny`, processes the returned task, and removes it from the collection.
*   **Result Extraction**: Although the returned task is already completed, it is best practice to `await` it to retrieve the result or handle exceptions rather than using `.Result`.
*   **Background Task Management**: Passing `CancellationToken` to individual tasks is essential to ensure that background work actually stops if a timeout or cancellation occurs.

### Lesson notes

While `Task.WhenAll` is useful for waiting for a batch of operations to finish, it can lead to a poor user experience if the user must wait for the slowest operation before seeing any results.
`Task.WhenAny` allows for a more responsive design where data can be processed or displayed as it streams in.

Consider an application making multiple API calls.
Using `Task.WhenAny`, the UI can be updated as soon as the first response arrives:

```csharp
var client = new HttpClient();
var apiResponseTaskList = new List<Task<HttpResponseMessage>>();

// Run 5 Simultaneous API Requests on background threads
for(int i = 0; i < 5; i++)
{
    Task<HttpResponseMessage> getAuthorResponse = client.GetAsync($"https://codetraveler.io/authors/{i}");
    apiResponseTaskList.Add(getAuthorResponse);
}

while(apiResponseTaskList.Count > 0)
{
    // Receive completed Task as soon as one is Completed
    Task<HttpResponseMessage> completedApiResponseTask = await Task.WhenAny(apiResponseTaskList);

    // Remove completed Task from List
    apiResponseTaskList.Remove(completedApiResponseTask);

    // Deserialize HttpResponseMessage
    using var response = await completedApiResponseTask;
    using var contentStream = await response.Content.ReadAsStreamAsync();
    Author author = await JsonSerializer.DeserializeAsync<Author>(contentStream);

    // Update UI
    AddAuthorToUI(author);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=10)

#### Implementing Task.WhenAny in the Cooking Example

To demonstrate this in a practical scenario, we modify the cooking simulation.
First, the `Food` base class and its derivatives are updated so that the `Cook` method returns a `Task<string>` containing the name of the food finished.

```csharp
using System.Diagnostics;

namespace TaskWhenAny;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task<string> Cook(CancellationToken token)
	{
		Trace.WriteLine($"Cooking {Name}");
		await Task.Delay(_cookTime, token);
		Trace.WriteLine($"{Name} Completed");

		return Name;
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=325)

In the main program, we initialize a list of tasks.
By using a `while` loop and `Task.WhenAny`, we can allow "guests" to start eating each food item as soon as it is ready, rather than waiting for the entire meal to be prepared.

```csharp
var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.FromSeconds(30));

Trace.WriteLine("Cooking Started");

List<Task<string>> cookingTaskList = 
[
    turkey.Cook(cancellationTokenSource.Token),
    gravy.Cook(cancellationTokenSource.Token),
    mashedPotatoes.Cook(cancellationTokenSource.Token),
    stuffing.Cook(cancellationTokenSource.Token)
];

try
{
    while (cookingTaskList.Count is not 0)
    {
        var completedCookingTask = await Task.WhenAny(cookingTaskList);
        cookingTaskList.Remove(completedCookingTask);

        var name = await completedCookingTask;

        Trace.WriteLine($"Eating {name}");
    }

    Trace.WriteLine("Dinner is ready!");
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Error: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=475)

#### Awaiting Completed Tasks vs. .Result

When `Task.WhenAny` returns, the task it provides is guaranteed to be in a completed state.
Because of this, accessing `.Result` would not block the calling thread.
However, it is still recommended to use the `await` keyword to retrieve the result.

```csharp
// While this is technically safe here because the task is completed:
var name = completedCookingTask.Result;

// This is preferred to maintain consistency and prevent issues if code is refactored:
var name = await completedCookingTask;
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=625)

Using `await` ensures that if the code is ever copied to a context where the task might not be completed, it won't cause a sync-over-async deadlock.
It also provides a cleaner way to handle exceptions that may have occurred within the task.

#### Cancellation and Background Tasks

If a timeout occurs (e.g., using `.WaitAsync(token)` on the `WhenAny` call), the `Task.WhenAny` call will throw a `TaskCanceledException`.
However, the underlying tasks in the list may continue to run on background threads if they were not also passed the cancellation token.

To ensure all resources are cleaned up and background work stops immediately upon a timeout, the `CancellationToken` must be passed into the individual asynchronous methods (like `Cook` in this example).

```csharp
List<Task<string>> cookingTaskList = 
[
    turkey.Cook(cancellationTokenSource.Token),
    gravy.Cook(cancellationTokenSource.Token),
    mashedPotatoes.Cook(cancellationTokenSource.Token),
    stuffing.Cook(cancellationTokenSource.Token)
];
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=805)

---

## 4. Task.WhenEach (IAsyncEnumerable)

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/) · 11:57

Task.WhenEach is a utility that returns an `IAsyncEnumerable`, providing a concise and readable way to stream data from multiple asynchronous operations as they complete.
This approach replaces the older pattern of using a `while` loop with `Task.WhenAny`, which required manual management and removal of tasks from a collection.

### Key concepts

*   **IAsyncEnumerable**: The return type of `Task.WhenEach`, enabling asynchronous iteration over a stream of tasks.
*   **await foreach**: The C# syntax used to consume the stream, executing the loop body every time a task in the collection completes.
*   **WithCancellation**: An extension method required to apply a `CancellationToken` to the `IAsyncEnumerable` stream.
*   **ToAsyncEnumerable**: A .NET 10 extension method for task collections that serves as a functional alternative to `Task.WhenEach`.
*   **Parallel Execution Order**: Tasks are processed in the order they finish, which is not necessarily the order in which they were started or added to the collection.

### Lesson notes

`Task.WhenEach` allows for efficient data streaming.
In scenarios where multiple background operations are running, such as API requests, it enables the application to process and display results immediately as they arrive rather than waiting for the entire batch to finish.

Consider an example where five simultaneous API requests are initiated and added to a list.
Using `Task.WhenEach`, we can iterate through them as they complete:

```csharp
var client = new HttpClient();
var apiResponseTaskList = new List<Task<HttpResponseMessage>>();

// Run 5 simultaneous API Requests on background threads
for(int i = 0; i < 5; i++)
{
    Task<HttpResponseMessage> getAuthorResponse = client.GetAsync($"https://codetraveler.io/authors/{i}");
    apiResponseTaskList.Add(getAuthorResponse);
}

await foreach(var completedApiResponseTask in Task.WhenEach(apiResponseTaskList))
{
    // Deserialize HttpResponseMessage
    using var response = await completedApiResponseTask;
    using var contentStream = await response.Content.ReadAsStreamAsync();
    Author author = await JsonSerializer.DeserializeAsync<Author>(contentStream);

    // Update UI
    AddAuthorToUI(author);
}

// All Tasks are now Completed, and the UI was updated immediately as each API Response was received
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=65)

#### Refactoring from Task.WhenAny

Before the introduction of `Task.WhenEach`, developers had to implement a `while` loop to achieve similar streaming behavior.
This required manually removing the completed task from the list to avoid processing it multiple times:

```csharp
var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.FromSeconds(230));

Trace.WriteLine("Cooking Started");

List<Task<string>> cookingTaskList =
[
    turkey.Cook(cancellationTokenSource.Token),
    gravy.Cook(cancellationTokenSource.Token),
    mashedPotatoes.Cook(cancellationTokenSource.Token),
    stuffing.Cook(cancellationTokenSource.Token)
];

try
{
    while (cookingTaskList.Count is not 0)
    {
        Task<string> completedCookingTask = await Task.WhenAny(cookingTaskList).WaitAsync(cancellationTokenSource.Token);
        cookingTaskList.Remove(completedCookingTask);

        var name = await completedCookingTask;

        Trace.WriteLine($"Eating {name}");
    }
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Error: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=235)

With `Task.WhenEach`, .NET handles the task tracking internally, allowing for much cleaner code:

```csharp
try
{
    await foreach (Task<string> completedCookingTask in Task.WhenEach(cookingTaskList))
    {
        var name = await completedCookingTask;
        Trace.WriteLine($"Eating {name}");
    }
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Error: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=490)

#### Parallelism and Race Conditions

When tasks run in parallel, their completion order is non-deterministic.
Even if one task starts a few nanoseconds before another, the second task might finish first due to CPU scheduling, thread priority, or internal overhead.
Parallel code should be written with the expectation that tasks may complete in any order.

#### Cancellation with IAsyncEnumerable

`Task.WhenEach` does not accept a `CancellationToken` as a direct parameter.
Instead, because it returns an `IAsyncEnumerable`, you use the `WithCancellation` extension method to pass the token to the enumerator:

```csharp
try
{
    await foreach (Task<string> completedCookingTask in Task.WhenEach(cookingTaskList).WithCancellation(cancellationTokenSource.Token))
    {
        var name = await completedCookingTask;
        Trace.WriteLine($"Eating {name}");
    }
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Error: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=565)

#### ToAsyncEnumerable Extension Method

In .NET 10 (C# 14), a new extension method called `ToAsyncEnumerable()` was introduced for `IEnumerable` types.
This method can be called directly on a list of tasks and provides the same functionality as `Task.WhenEach`.
The choice between the two is primarily a matter of coding style and readability preference.

```csharp
try
{
    await foreach (Task<string> completedCookingTask in cookingTaskList.ToAsyncEnumerable().WithCancellation(cancellationTokenSource.Token))
    {
        var name = await completedCookingTask;
        Trace.WriteLine($"Eating {name}");
    }
}
catch (TaskCanceledException)
{
    Trace.WriteLine("Error: Cooking took too long");
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=610)

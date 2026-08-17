# Async/Await Best Practices

> Course: [From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · Chapter 5
> 6 lessons · ~1:24:30
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/) | 2:10 | [↓](#1-introduction) |
| 2 | [Async Void](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/) | 14:18 | [↓](#2-async-void) |
| 3 | [CancellationToken, ConfigureAwait, .Wait() and .Result](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/) | 20:05 | [↓](#3-cancellationtoken-configureawait-wait-and-result) |
| 4 | [IAsyncEnumerable](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/) | 21:17 | [↓](#4-iasyncenumerable) |
| 5 | [Returning a Task and ValueTask](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/) | 12:12 | [↓](#5-returning-a-task-and-valuetask) |
| 6 | [Review](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197176/) | 14:28 | [↓](#6-review) |

---

## 1. Introduction

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/) · 2:10

This lesson introduces the "Async/Await Best Practices" section of the course, focusing on practical implementation details within a .NET MAUI application.
It establishes a comparison between suboptimal asynchronous patterns and industry-standard best practices, utilizing a Hacker News client as the reference project.
The lesson also highlights the AsyncAwaitBestPractices NuGet library, which provides utility classes like WeakEventManager to handle common asynchronous challenges such as memory leaks in event handling.

### Key concepts

*   Practical application of async/await in UI frameworks (.NET MAUI).
*   Comparison of "Bad" vs. "Good" asynchronous implementation patterns.
*   Introduction to the `AsyncAwaitBestPractices` library and its `WeakEventManager`.
*   Handling long-running tasks and UI updates in view models.

### Lesson notes

The focus shifts from the theoretical underpinnings of asynchronous programming—such as compiler-generated state machines and custom task implementations—to practical, real-world application.
This section utilizes a .NET MAUI application called "Hacker News" to demonstrate common pitfalls and their solutions.
The app is a single-page client that fetches top stories from the Hacker News API and allows users to navigate to the source articles.

To assist with robust asynchronous implementation, the lesson introduces the `AsyncAwaitBestPractices` NuGet library.
This library provides specialized tools, such as the `WeakEventManager`, which helps manage events in an asynchronous context without creating memory leaks or keeping objects alive longer than necessary.

The demonstration project contains two primary view models to illustrate the transition from poor to optimal code: `NewsViewModel_BadAsyncAwaitPractices` and `NewsViewModel_GoodAsyncAwaitPractices`.

The "Bad" implementation contains several anti-patterns, such as calling asynchronous methods directly from a constructor without proper handling, leading to "fire and forget" scenarios that can cause unhandled exceptions or race conditions.

```csharp
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HackerNews;

partial class NewsViewModel_BadAsyncAwaitPractices : BaseViewModel
{
	readonly HackerNewsAPIService _hackerNewsAPIService;
	readonly AsyncAwaitBestPractices.WeakEventManager _pullToRefreshEventManager = new();

	public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
	{
		_hackerNewsAPIService = hackerNewsAPIService;

		//ToDo Refactor
		Refresh(CancellationToken.None);
	}

	public event EventHandler<string> PullToRefreshFailed
	{
		add => _pullToRefreshEventManager.AddEventHandler(value);
		remove => _pullToRefreshEventManager.RemoveEventHandler(value);
	}
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/?t=85)

In contrast, the "Good" implementation refactors these patterns, utilizing modern C# features like primary constructors and more efficient asynchronous streams.
This version serves as the reference for the target state of the application after applying the best practices discussed throughout this section.

```csharp
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HackerNews;

partial class NewsViewModel(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : BaseViewModel(dispatcher)
{
	readonly HackerNewsAPIService _hackerNewsAPIService = hackerNewsAPIService;
	readonly WeakEventManager _pullToRefreshEventManager = new();

	public event EventHandler<string> PullToRefreshFailed
	{
		add => _pullToRefreshEventManager.AddEventHandler(value);
		remove => _pullToRefreshEventManager.RemoveEventHandler(value);
	}

	[ObservableProperty]
	public partial bool IsListRefreshing { get; set; }

	[RelayCommand]
	async Task Refresh(CancellationToken token)
	{
		// Implementation details for optimized refresh
	}
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/?t=115)

By comparing these two classes side-by-side, developers can observe the impact of proper task management, cancellation token usage, and UI thread synchronization.

---

## 2. Async Void

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/) · 14:18

### Summary

This lesson explores the technical dangers of using async void in C#, specifically when attempting to perform asynchronous initialization within a class constructor.
While constructors cannot be asynchronous, wrapping an unawaited task in an async void method introduces significant risks, including race conditions and uncatchable exceptions that can crash the application.
The lesson demonstrates why these issues occur by tracing thread execution and provides a robust alternative using the SafeFireAndForget extension method from the AsyncAwaitBestPractices library, which ensures background tasks are executed with proper error handling and explicit intent.

### Key concepts

* **Constructor Limitations**: C# constructors cannot be marked as `async` because their purpose is immediate object initialization and memory allocation, not long-running task management.
* **Unawaited Tasks**: Calling an `async Task` method without awaiting it results in a compiler warning and causes the task to run in the background, potentially hiding exceptions.
* **Async Void Dangers**: Using `async void` (outside of event handlers) is dangerous because it returns control to the caller at the first `await`, making it impossible for the caller to catch exceptions or synchronize execution.
* **Race Conditions**: `async void` methods often lead to race conditions where multiple threads attempt to modify the same collection or state simultaneously.
* **SafeFireAndForget**: A specialized extension method that provides a safe way to execute background tasks by internally wrapping them in a `try-catch` block and allowing for explicit exception handling.

### Lesson notes

In C# development, a common challenge arises when an asynchronous operation must be triggered during object construction.
In the following example, a constructor calls an asynchronous `Refresh` method.
Because the method is not awaited, the compiler generates a warning indicating that the task will execute in the background and exceptions may be lost.

```csharp
namespace HackerNews;

partial class NewsViewModel_BadAsyncAwaitPractices : BaseViewModel
{
    readonly HackerNewsAPIService _hackerNewsAPIService;
    readonly AsyncAwaitBestPractices.WeakEventManager _pullToRefreshEventManager = new();

    public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
    {
        _hackerNewsAPIService = hackerNewsAPIService;

        //ToDo Refactor
        Refresh(CancellationToken.None);
    }

    public event EventHandler<string> PullToRefreshFailed
    {
        add => _pullToRefreshEventManager.AddEventHandler(value);
        remove => _pullToRefreshEventManager.RemoveEventHandler(value);
    }

    [ObservableProperty]
    public partial bool IsListRefreshing { get; set; }

    [RelayCommand]
    async Task Refresh(CancellationToken token)
    {
        var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2));
        // ... implementation
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=10)

Attempting to resolve this by marking the constructor as `async` is invalid.
Constructors are designed to initialize an object and assign it a location in memory; they are not intended for long-running tasks and do not support the `async` and `await` keywords.

```csharp
// This code will not compile
public async NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
{
    _hackerNewsAPIService = hackerNewsAPIService;

    //ToDo Refactor
    await Refresh(CancellationToken.None);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=40)

#### The Async Void Wrapper

A common but dangerous workaround is to wrap the asynchronous call in an `async void` method.
While this removes the compiler warning, it introduces severe architectural risks.

```csharp
public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
{
    _hackerNewsAPIService = hackerNewsAPIService;

    // Calling an async void method
    Refresh();
}

async void Refresh()
{
    await Refresh(CancellationToken.None);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=100)

#### Risk 1: Race Conditions

When `Thread 1` enters the constructor and calls the `async void Refresh()` method, it eventually hits the `await` keyword inside that method.
At this point, control is returned to the caller (the constructor).
`Thread 1` continues executing the remaining lines in the constructor, while a background thread (e.g., `Thread 2`) continues the work inside the `Refresh` method.
If both threads attempt to modify the same state, such as a `TopStoryCollection`, a race condition occurs.

```csharp
public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
{
    _hackerNewsAPIService = hackerNewsAPIService;

    Refresh(); // Control returns here as soon as Refresh hits an await

    // Thread 1 continues here immediately
    TopStoryCollection.Clear();
    TopStoryCollection.Add(new StoryModel());
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=265)

#### Risk 2: Uncatchable Exceptions

Exceptions thrown from an `async void` method are notoriously difficult to catch.
If an exception is thrown after the first `await` in an `async void` method, it cannot be caught by a `try-catch` block in the calling method.
This is because the calling thread has already moved past the call site.
When the exception eventually occurs on the background thread, there is no handler to catch it, causing the entire application to crash.

```csharp
public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
{
    try
    {
        Refresh();
    }
    catch (Exception ex)
    {
        // This will NOT catch exceptions thrown after the 'await' in Refresh()
    }
}

async void Refresh()
{
    await Refresh(CancellationToken.None);
    throw new Exception(); // This crashes the app
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=175)

#### Safe Fire and Forget

To handle background tasks safely, the `SafeFireAndForget` extension method (from the `AsyncAwaitBestPractices` library) should be used.
This method explicitly signals the intent to run a task in the background while providing a mechanism to handle exceptions safely.

```csharp
public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) :
    base(dispatcher)
{
    _hackerNewsAPIService = hackerNewsAPIService;

    // Safe background execution with explicit exception handling
    Refresh(CancellationToken.None).SafeFireAndForget(onException: ex => Trace.WriteLine(ex));
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=640)

Internally, `SafeFireAndForget` uses `async void` in a controlled manner, wrapping the task in a `try-catch` block to ensure that any exceptions are caught and passed to the provided `onException` handler rather than crashing the process.

```csharp
static async void HandleSafeFireAndForget<TException>(Task task, bool continueOnCapturedContext, Action<TException>? onException) where TException : Exception
{
    try
    {
        await task.ConfigureAwait(continueOnCapturedContext);
    }
    catch (TException ex) when (_onException is not null || onException is not null)
    {
        HandleException(ex, onException);
        // ... additional logic
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=685)

You can also specify the type of exception you wish to handle, allowing for granular error management, such as logging specific properties of an `HttpRequestException`.

```csharp
Refresh(CancellationToken.None).SafeFireAndForget<HttpRequestException>(ex => 
    Trace.WriteLine(ex.HttpRequestError));
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=760)

While `SafeFireAndForget` still allows the calling thread to continue immediately, it provides the necessary guardrails to prevent application crashes and makes the asynchronous nature of the call obvious to other developers.

---

## 3. CancellationToken, ConfigureAwait, .Wait() and .Result

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/) · 20:05

### Summary

This lesson covers essential best practices for asynchronous programming in C#, focusing on the proper use of CancellationToken to handle task cancellation, ConfigureAwait to optimize thread management and prevent UI deadlocks, and the critical avoidance of blocking calls like .Wait() and .Result.
It also introduces the ConfigureAwaitOptions enum introduced in .NET 8, which provides granular control over task continuation behavior, including context capturing, forced yielding, and exception suppression.

### Key concepts

* **CancellationToken**: Always propagate cancellation tokens to asynchronous methods to allow callers to terminate long-running operations and improve user experience.
* **WaitAsync**: Use this extension method to apply a cancellation token to tasks that do not natively support cancellation parameters.
* **ConfigureAwait(false)**: Prevents the task from capturing the current synchronization context, allowing continuations to run on any available thread pool thread. This improves performance and prevents deadlocks on the UI thread.
* **Blocking Calls**: Avoid `.Wait()` and `.Result` as they block the calling thread, leading to potential UI freezes and thread pool exhaustion.
* **ConfigureAwaitOptions (.NET 8)**: A flags-based enum providing advanced control over task awaitables, including `ForceYielding` and `SuppressThrowing`.

### Lesson notes

#### Cancellation Tokens and WaitAsync

Asynchronous methods should always accept a `CancellationToken` to allow the consumer to manage the task's lifecycle.
For example, in a mobile application, a developer might want to cancel a refresh operation after 15 seconds if the connection is poor.
While many built-in methods like `Task.Delay` accept a token directly, some third-party or legacy APIs do not.

```csharp
[RelayCommand]
async Task Refresh(CancellationToken token)
{
    // Passing the token directly to Task.Delay
    var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2), token);

    try
    {
        var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories);
        // ... implementation
    }
    catch (Exception e)
    {
        OnPullToRefreshFailed(message: e.ToString());
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=190)

If an asynchronous method does not support a `CancellationToken` parameter, the `WaitAsync` extension method can be used to "bolt on" cancellation support to the existing task.

```csharp
// Using WaitAsync to add cancellation support to a task that doesn't natively support it
var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2)).WaitAsync(token);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=235)

#### Optimizing Context with ConfigureAwait(false)

By default, when a task is awaited, the runtime attempts to return to the original synchronization context (e.g., the UI thread).
If the code following the `await` is computationally expensive—such as iterating over thousands of items and performing complex sorting—it can lock the UI thread and make the application unresponsive.

Using `ConfigureAwait(false)` instructs the runtime that it does not need to return to the calling thread.
Instead, the continuation can run on any available thread from the thread pool.

```csharp
[RelayCommand]
async Task Refresh(CancellationToken token)
{
    var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2), token);

    try
    {
        // ConfigureAwait(false) allows the subsequent foreach loop to run on a background thread
        var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories).ConfigureAwait(false);

        TopStoryCollection.Clear();

        foreach (var story in topStoriesList)
        {
            if (!TopStoryCollection.Any(x => x.Title.Equals(story.Title, StringComparison.Ordinal)))
                InsertIntoSortedCollection(TopStoryCollection, comparison: (a, b) => b.Score.CompareTo(a.Score), story);
        }
    }
    // ... catch and finally blocks
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=460)

#### Avoiding Blocking Calls

Methods like `.Wait()` and `.Result` are synchronous blocking calls.
They force the calling thread to wait until the task completes.
If called on the UI thread, the application will freeze.
Furthermore, in high-scale environments like ASP.NET Core, these calls lead to thread pool exhaustion because they consume two threads (the calling thread and the task thread) for a single operation.

Always use the `await` keyword, even within `finally` blocks.

```csharp
finally
{
    // Use await instead of .Wait() to avoid blocking the calling thread
    await minimumRefreshTimeTask;
    IsListRefreshing = false;
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=655)

#### ConfigureAwaitOptions (.NET 8)

.NET 8 introduced the `ConfigureAwaitOptions` enum to provide more descriptive and flexible control over task awaitables.

*   `ConfigureAwaitOptions.None` is equivalent to `ConfigureAwait(false)`.
*   `ConfigureAwaitOptions.ContinueOnCapturedContext` is equivalent to `ConfigureAwait(true)`.

```csharp
// Equivalent to ConfigureAwait(true)
var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories)
                            .ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=895)

```csharp
// Equivalent to ConfigureAwait(false)
var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories)
                            .ConfigureAwait(ConfigureAwaitOptions.None);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=925)

Advanced flags include `ForceYielding`, which behaves like `Task.Yield()`, forcing the task to yield even if it has already completed.
This can be useful for ensuring the UI thread has a chance to update.

```csharp
// Combining flags to prevent context capture and force yielding
var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories)
                            .ConfigureAwait(ConfigureAwaitOptions.None | ConfigureAwaitOptions.ForceYielding);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=955)

Finally, `SuppressThrowing` can be used to prevent exceptions from being re-thrown when the task is awaited.
Note that this option is only supported for non-generic tasks (e.g., `Task`, not `Task<T>`).

```csharp
// Suppressing exceptions in a non-generic task (e.g., Task.Delay)
await minimumRefreshTimeTask.ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext
                                          | ConfigureAwaitOptions.ForceYielding
                                          | ConfigureAwaitOptions.SuppressThrowing);
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=1090)

---

## 4. IAsyncEnumerable

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/) · 21:17

### Summary

This lesson demonstrates how to refactor sequential asynchronous operations into parallel streams using IAsyncEnumerable and the .NET 9 Task.WhenEach API.
By moving away from Task.WhenAll, which forces a user to wait for an entire batch of operations to complete, developers can use async iterators to stream data to the UI as it becomes available.
The lesson also details the use of FrozenSet for immutability, the [EnumeratorCancellation] attribute for proper token handling in async streams, and the importance of maintaining ConfigureAwait(false) even on completed tasks to ensure code robustness during future refactors.

### Key concepts

* **FrozenSet**: A .NET 8 collection that is truly immutable and optimized for read-heavy scenarios, though it carries a creation performance hit.
* **Parallel Task Execution**: Using LINQ `Select` to kick off multiple background tasks simultaneously rather than awaiting them sequentially in a loop.
* **IAsyncEnumerable**: An interface that enables asynchronous iteration, allowing values to be streamed to the caller as they are produced.
* **Task.WhenEach**: A .NET 9 API that returns an `IAsyncEnumerable` of tasks in the order they complete.
* **EnumeratorCancellation**: An attribute used on `CancellationToken` parameters within async iterators to correctly integrate with the .NET runtime's iteration logic.
* **Yield Return**: A keyword used in async iterators to provide the next element of the stream without exiting the method.

### Lesson notes

#### Immutability with FrozenSet

When handling data from external sources like APIs or databases, it is best practice to keep that data immutable.
.NET 8 introduced `FrozenSet<T>`, which provides a more rigid guarantee of immutability than `IReadOnlyList<T>`.
While `IReadOnlyList` prevents direct list manipulation, the underlying values at specific indices can sometimes still be modified.
A `FrozenSet` is truly frozen.
However, creating a `FrozenSet` is more computationally expensive than creating a standard list, so it should be used judiciously in performance-sensitive environments like high-traffic ASP.NET Core APIs.

```csharp
// Initial sequential implementation returning a FrozenSet
async Task<FrozenSet<StoryModel>> GetTopStories(CancellationToken token, int storyCount = int.MaxValue)
{
    List<StoryModel> topStoryList = [];

    var topStoryIds = await GetTopStoryIDs(token).ConfigureAwait(false);

    foreach (var topStoryId in topStoryIds)
    {
        var story = await GetStory(topStoryId, token).ConfigureAwait(false);
        topStoryList.Add(story);

        if (topStoryList.Count >= storyCount)
            break;
    }

    return topStoryList.OrderByDescending(x => x.Score).ToFrozenSet();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=10)

#### Transitioning to Parallelism

The sequential approach above is inefficient because it waits for each API call to finish before starting the next.
To improve performance, we can kick off all tasks simultaneously using LINQ and store them in a list of tasks.

```csharp
// Refactoring to kick off tasks in parallel
async Task<FrozenSet<StoryModel>> GetTopStories(CancellationToken token, int storyCount = int.MaxValue)
{
    var topStoryIds = await GetTopStoryIDs(token).ConfigureAwait(false);

    List<Task<StoryModel>> getTopStoriesTasks = topStoryIds.Select(id => GetStory(id, token)).ToList();

    var topStories = await Task.WhenAll(getTopStoriesTasks).ConfigureAwait(false);

    return topStories.ToFrozenSet();
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=370)

While `Task.WhenAll` is faster than sequential execution, it creates a poor user experience because the UI remains blank until the entire batch of tasks completes.
If fetching 100 stories takes five seconds, the user sees a spinner for the full duration before all stories appear at once.

#### Streaming with IAsyncEnumerable and Task.WhenEach

To provide a more responsive experience, we can use `IAsyncEnumerable<T>` to stream results as they arrive.
In .NET 9, the `Task.WhenEach` method allows us to iterate over a collection of tasks and receive each one as soon as it completes.

```csharp
// Using IAsyncEnumerable and Task.WhenEach for streaming
async IAsyncEnumerable<StoryModel> GetTopStories([EnumeratorCancellation] CancellationToken token, int storyCount = int.MaxValue)
{
    var topStoryIds = await GetTopStoryIDs(token).ConfigureAwait(false);

    List<Task<StoryModel>> getTopStoriesTasks = topStoryIds.Select(id => GetStory(id, token)).ToList();

    await foreach (var topStoryTask in Task.WhenEach(getTopStoriesTasks).WithCancellation(token))
    {
        if (storyCount is 0)
            break;

        var topStory = await topStoryTask.ConfigureAwait(false);

        yield return topStory;

        storyCount--;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=1165)

In this implementation:

1.  **`yield return`**: This keyword returns a value to the caller's `await foreach` loop without exiting the method. The method state is preserved, and execution resumes after the caller processes the item.
2.  **`[EnumeratorCancellation]`**: This attribute ensures that the `CancellationToken` passed to the method is correctly utilized by the .NET runtime's async iterator machinery.
3.  **`.WithCancellation(token)`**: This extension method is used to pass the token into the `IAsyncEnumerable` returned by `Task.WhenEach`.
4.  **`ConfigureAwait(false)`**: Even though `Task.WhenEach` only returns completed tasks, using `ConfigureAwait(false)` is still recommended. This prevents the code from potentially switching threads unnecessarily and ensures that if the code is later copy-pasted into a context where the task is not yet complete, it remains thread-safe.

#### Consuming the Async Stream

The calling method uses an `await foreach` loop to consume the stream.
This allows the UI to update incrementally as each story is returned, making the wait time feel significantly shorter to the user.

```csharp
[RelayCommand]
async Task Refresh(CancellationToken token)
{
    var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2), token);

    try
    {
        TopStoryCollection.Clear();

        await foreach (var story in GetTopStories(token, storyCount: StoriesConstants.NumberOfStories).ConfigureAwait(false))
        {
            if (!TopStoryCollection.Any(x => x.Title.Equals(story.Title, StringComparison.Ordinal)))
                InsertIntoSortedCollection(TopStoryCollection, comparison: (a, b) => b.Score.CompareTo(a.Score), story);
        }
    }
    catch (Exception e)
    {
        OnPullToRefreshFailed(message: e.ToString());
    }
    finally
    {
        await minimumRefreshTimeTask.ConfigureAwait(false);
        IsListRefreshing = false;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=1250)

---

## 5. Returning a Task and ValueTask

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/) · 12:12

### Summary

This lesson explores performance optimizations in asynchronous C# code, focusing on when to return a Task directly versus using the async/await keywords.
It demonstrates how eliding async/await can reduce unnecessary context switches and thread pool overhead in simple wrapper methods, while cautioning against this practice when local exception handling (try/catch) or specific stack trace preservation is required.
Additionally, the lesson introduces ValueTask as a memory-efficient alternative for methods where the 'hot path' often completes synchronously, such as when returning cached data, thereby avoiding heap allocations.

### Key concepts

*   **Context Switch Reduction**: Returning a `Task` directly instead of awaiting it avoids the overhead of the state machine and potential thread transitions.
*   **Eliding Async/Await**: Removing the `async` and `await` keywords in one-line pass-through methods to improve performance.
*   **Stack Trace Implications**: Eliding `async` can cause a method to be omitted from the call stack in an exception, which may impact debugging.
*   **Async/Await in Try/Catch**: You must use `await` inside a `try/catch` block to ensure exceptions are caught locally; returning the `Task` directly will bypass the catch block.
*   **ValueTask**: A value-type (struct) alternative to `Task` that reduces heap allocations, ideal for methods that frequently complete synchronously.
*   **Hot Path Optimization**: Using `ValueTask` when the most common execution path does not require asynchronous waiting (e.g., returning cached results).

### Lesson notes

#### Eliding Async/Await for Performance

When a method simply wraps another asynchronous call and returns the same type, using the `async` and `await` keywords can introduce unnecessary overhead.
In a standard `async await` flow, a thread enters the method, hits the `await`, and returns to the thread pool.
When the underlying task completes, a potentially different thread is assigned to finish the method.
For a one-line method, this results in multiple context switches just to return a value.

Consider the following implementation of `GetStory` using `async await` and `ConfigureAwait(false)`:

```csharp
//ToDo Refactor
async Task<StoryModel> GetStory(long storyId, CancellationToken token)
{
    return await _hackerNewsAPIService.GetStory(storyId, token).ConfigureAwait(false);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=25)

By removing the `async` and `await` keywords, the method returns the `Task` directly to the caller.
This defers the state machine logic to the higher-level caller, saving a context switch and improving performance.

```csharp
//Refactored to return Task directly
Task<StoryModel> GetStory(long storyId, CancellationToken token)
{
    return _hackerNewsAPIService.GetStory(storyId, token);
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=115)

**Note on Stack Traces**: When you return a `Task` directly, the method may not appear in the stack trace if an exception occurs within that task.
While this can make debugging slightly more difficult in complex scenarios, the performance gain is often worth the trade-off in high-performance library code.

#### The Try/Catch Exception

A common mistake is attempting to elide `async/await` inside a method that contains a `try/catch` block.
If you return the `Task` directly, the method execution finishes immediately after the `return` statement.
If the task subsequently fails, the exception will be thrown back to the caller of this method, bypassing the local `catch` block entirely.

```csharp
// BAD PRACTICE: Returning Task inside try/catch
async Task<FrozenSet<long>> GetTopStoryIDs(CancellationToken token)
{
    if (IsDataRecent(TimeSpan.FromHours(1)))
        return Task.FromResult(TopStoryCollection.Select(x => x.Id).ToFrozenSet());

    try
    {
        // This will exit the method before the task completes, bypassing the catch block
        return _hackerNewsAPIService.GetTopStoryIDs(token);
    }
    catch (Exception e)
    {
        Trace.WriteLine(e.Message);
        throw;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=370)

To ensure the exception is caught and logged locally, the `await` keyword must be used to keep the method context alive until the task completes.

#### Optimizing with ValueTask

While `Task` is a reference type allocated on the heap, `ValueTask` is a value type (a read-only struct) allocated on the stack.
Adding a value to the stack is an O(1) operation and is significantly faster than heap allocation, which requires indexing and garbage collection.

`ValueTask` is most effective when the "hot path" of a method—the code path executed most frequently—completes synchronously.
For example, if data is cached and can be returned immediately without an API call, `ValueTask` avoids the overhead of creating a `Task` object.

```csharp
// Using ValueTask for hot-path optimization
async ValueTask<FrozenSet<long>> GetTopStoryIDs(CancellationToken token)
{
    if (IsDataRecent(TimeSpan.FromHours(1)))
        return TopStoryCollection.Select(x => x.Id).ToFrozenSet();

    try
    {
        return await _hackerNewsAPIService.GetTopStoryIDs(token).ConfigureAwait(false);
    }
    catch (Exception e)
    {
        Trace.WriteLine(e.Message);
        throw;
    }
}
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=505)

**Best Practices for ValueTask**:

1.  **Do not reuse**: Unlike `Task`, a `ValueTask` should never be awaited multiple times.
2.  **Do not store**: Avoid passing `ValueTask` around or storing it in fields; consume it immediately by awaiting it.
3.  **Use for Hot Paths**: Only switch from `Task` to `ValueTask` if the method often returns a result synchronously.

---

## 6. Review

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197176/) · 14:28

### Summary

This lesson reviews essential best practices for asynchronous programming in C#, covering synchronization context management, exception handling, and performance optimizations.
It emphasizes the use of await over blocking calls, the strategic use of ConfigureAwait(false), and the benefits of modern features like IAsyncEnumerable and IAsyncDisposable.

### Key concepts

*   Prefer `await` over `.Wait()` or `.Result()`.
*   Use `.GetAwaiter().GetResult()` when synchronous execution is unavoidable.
*   Avoid `return await` except in `try-catch` or `using` blocks.
*   Apply `ConfigureAwait(false)` in non-UI layers.
*   Leverage `ValueTask` for performance-critical synchronous paths.
*   Stream data with `IAsyncEnumerable`.
*   Implement `IAsyncDisposable` for asynchronous resource cleanup.

### Lesson notes

#### Blocking Calls and Exception Handling

The primary rule of asynchronous programming in C# is to avoid blocking calls like `.Wait()` or `.Result()`.
These methods block the calling thread until the background task completes, which can lead to deadlocks and performance degradation.
In modern .NET development, the `await` keyword should be the default choice.

In rare scenarios where you are forced to perform sync-over-async—such as when implementing an interface that does not allow for a `Task` return type—the recommended approach is to use `.GetAwaiter().GetResult()`.
While this is still a blocking call, it is superior to `.Wait()` or `.Result()` because it does not wrap exceptions in a `System.AggregateException`.
Instead, it re-throws the original exception (e.g., a `TimeoutException`), preserving the expected stack trace and making debugging significantly easier.

#### Fire and Forget Tasks

For tasks where the completion timing is not critical to the subsequent logic, avoid using `async void`.
Instead, use the `SafeFireAndForget` extension method from the `AsyncAwaitBestPractices` NuGet package.
This approach explicitly signals to other developers that the task is intended to run independently and provides guardrails for graceful exception handling.

#### Eliding Async and Await

When the only use of the `await` keyword in a method is in the return statement, you can optimize performance by eliding the `async` and `await` keywords and returning the `Task` directly.
This reduces the overhead of the state machine and unnecessary thread switches.

For example, in a service layer, you might return the task directly:

```csharp
public Task<StoryModel> GetStory(long storyId, CancellationToken token) => _hackerNewsClient.GetStory(storyId, token);
```

However, there are two critical exceptions where you must keep the `await` keyword:

1.  **Try-Catch Blocks**: If you return a task without awaiting it, the method exits the try block immediately, and the catch block will never intercept exceptions thrown by that task.
2.  **Using Blocks**: Returning a task from a `using` block without awaiting it will cause the disposable object to be disposed of before the task completes, often resulting in an `ObjectDisposedException`.

#### Context Management with ConfigureAwait

`ConfigureAwait(false)` is used to signal that the task does not need to resume on the original synchronization context.
This is highly recommended for non-UI layers, such as services, databases, and models, to improve performance and avoid deadlocks.
In UI-heavy frameworks like MAUI or WinForms, the UI layer should generally avoid `ConfigureAwait(false)` to ensure updates happen on the UI thread.

.NET 8 and 9 introduced `ConfigureAwaitOptions`, which provide more granular control:

*   **None**: Equivalent to `ConfigureAwait(false)`.
*   **ContinueOnCapturedContext**: Equivalent to `ConfigureAwait(true)`.
*   **ForceYielding**: Forces an asynchronous yield even if the task is already completed, which can be useful for keeping the UI responsive.
*   **SuppressThrowing**: Prevents the `await` from re-throwing exceptions, though this should be used with caution.

#### Performance and Streaming

For high-performance scenarios where a method often returns synchronously, use `ValueTask`.
Unlike `Task`, which is a class allocated on the heap, `ValueTask` is a struct that lives on the stack, reducing allocation overhead.

When dealing with APIs that return multiple results, `IAsyncEnumerable` is the preferred choice for streaming data.
This allows the UI to update as results arrive rather than waiting for the entire collection.
When implementing these methods, use the `[EnumeratorCancellation]` attribute to ensure the `CancellationToken` is correctly handled by the runtime iterator.

#### Asynchronous Resource Cleanup

`IAsyncDisposable` allows for asynchronous cleanup of unmanaged resources, which is particularly useful for expensive operations like closing file streams.
This ensures the calling thread is not blocked during the disposal process.

```csharp
await using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate).ConfigureAwait(false))
{
    // Save data to file...
} // await executes here
```

[▶ Watch](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197176/?t=685)

In the example above, the `await` associated with the `using` block executes at the closing brace, ensuring the stream is disposed of asynchronously.

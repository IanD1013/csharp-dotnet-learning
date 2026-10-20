# Async/Await Best Practices

> 课程:[From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · 第 5 章
> 共 6 课 · 约 1:24:30
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/) | 2:10 | [↓](#1-introduction) |
| 2 | [Async Void](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/) | 14:18 | [↓](#2-async-void) |
| 3 | [CancellationToken, ConfigureAwait, .Wait() and .Result](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/) | 20:05 | [↓](#3-cancellationtoken-configureawait-wait-and-result) |
| 4 | [IAsyncEnumerable](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/) | 21:17 | [↓](#4-iasyncenumerable) |
| 5 | [Returning a Task and ValueTask](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/) | 12:12 | [↓](#5-returning-a-task-and-valuetask) |
| 6 | [Review](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197176/) | 14:28 | [↓](#6-review) |

---

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/) · 2:10

本课介绍课程的 "Async/Await Best Practices" 部分,重点是在一个 .NET MAUI 应用中的实际实现细节。
它以一个 Hacker News 客户端作为参考项目,对比欠佳的异步模式与业界标准的最佳实践。
本课还着重介绍了 AsyncAwaitBestPractices NuGet 库,它提供了 WeakEventManager 等工具类,用来应对常见的异步难题,例如事件处理中的内存泄漏。

### 核心概念

*   在 UI 框架(.NET MAUI)中实际运用 async/await。
*   对比 "Bad" 与 "Good" 两种异步实现模式。
*   介绍 `AsyncAwaitBestPractices` 库及其 `WeakEventManager`。
*   在 view model 中处理长时间运行的任务与 UI 更新。

### 课程笔记

关注点从异步编程的理论基础(例如编译器生成的状态机和自定义 task 实现)转向实际的、真实世界中的应用。
本部分使用一个名为 "Hacker News" 的 .NET MAUI 应用来演示常见陷阱及其解决方案。
该应用是一个单页客户端,它从 Hacker News API 获取热门文章,并允许用户跳转到原文。

为了帮助实现健壮的异步代码,本课介绍了 `AsyncAwaitBestPractices` NuGet 库。
该库提供了专门的工具,例如 `WeakEventManager`,它有助于在异步上下文中管理事件,而不会造成内存泄漏,也不会让对象存活得比必要的更久。

演示项目包含两个主要的 view model,用来展示从糟糕代码到最优代码的转变:`NewsViewModel_BadAsyncAwaitPractices` 和 `NewsViewModel_GoodAsyncAwaitPractices`。

"Bad" 实现包含若干反模式,例如在构造函数中直接调用异步方法而不做适当处理,从而形成 "fire and forget" 场景,可能导致未处理的异常或竞态条件。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/?t=85)

相比之下,"Good" 实现重构了这些模式,使用了主构造函数(primary constructor)等现代 C# 特性以及更高效的异步流。
这个版本作为参考,展示应用在运用本部分讨论的最佳实践之后的目标状态。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197171/?t=115)

通过并排比较这两个类,开发者可以看到正确的任务管理、cancellation token 的使用以及 UI 线程同步所带来的影响。

---

## 2. Async Void

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/) · 14:18

### 总结

本课探讨在 C# 中使用 async void 的技术风险,特别是在类的构造函数中尝试执行异步初始化的情形。
虽然构造函数不能是异步的,但把一个未被 await 的 task 包进 async void 方法会带来重大风险,包括竞态条件以及可能导致应用崩溃的无法捕获的异常。
本课通过追踪线程执行来演示这些问题为何会发生,并给出一个健壮的替代方案:使用 AsyncAwaitBestPractices 库中的 SafeFireAndForget 扩展方法,它确保后台任务在具备适当错误处理和明确意图的情况下执行。

### 核心概念

* **构造函数的限制**:C# 构造函数不能标记为 `async`,因为它的用途是立即完成对象初始化和内存分配,而不是管理长时间运行的任务。
* **未被 await 的任务**:调用一个 `async Task` 方法而不 await 它,会产生编译器警告,并导致该任务在后台运行,可能隐藏异常。
* **Async Void 的危险**:使用 `async void`(事件处理程序之外)是危险的,因为它会在第一个 `await` 处把控制权交还给调用方,使调用方无法捕获异常或同步执行。
* **竞态条件**:`async void` 方法经常导致竞态条件,即多个线程同时尝试修改同一个集合或状态。
* **SafeFireAndForget**:一个专门的扩展方法,它在内部把后台任务包在 `try-catch` 块中并允许显式的异常处理,从而提供一种安全执行后台任务的方式。

### 课程笔记

在 C# 开发中,当一个异步操作必须在对象构造期间触发时,就会出现一个常见的难题。
在下面的示例中,构造函数调用了一个异步的 `Refresh` 方法。
由于该方法没有被 await,编译器会生成一条警告,指出该任务将在后台执行,异常可能会丢失。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=10)

试图通过把构造函数标记为 `async` 来解决这个问题是无效的。
构造函数的设计目的是初始化一个对象并为它分配内存中的位置;它们不用于长时间运行的任务,也不支持 `async` 和 `await` 关键字。

```csharp
// This code will not compile
public async NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) : base(dispatcher)
{
    _hackerNewsAPIService = hackerNewsAPIService;

    //ToDo Refactor
    await Refresh(CancellationToken.None);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=40)

#### Async Void 包装方法

一种常见但危险的变通做法,是把异步调用包进一个 `async void` 方法中。
虽然这样能消除编译器警告,但它会带来严重的架构风险。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=100)

#### 风险 1:竞态条件

当 `Thread 1` 进入构造函数并调用 `async void Refresh()` 方法时,它最终会碰到该方法内部的 `await` 关键字。
此时,控制权被交还给调用方(构造函数)。
`Thread 1` 继续执行构造函数中剩余的代码行,而一个后台线程(例如 `Thread 2`)继续执行 `Refresh` 方法内部的工作。
如果两个线程都尝试修改同一个状态,例如 `TopStoryCollection`,就会发生竞态条件。

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

```text
Thread 1                              Thread 2
--------                              --------
enters constructor
calls async void Refresh()
hits await inside Refresh
control returns to constructor  ----> continues work inside Refresh
continues remaining lines             |
  TopStoryCollection.Clear()          |
  TopStoryCollection.Add(...)         modifies TopStoryCollection
          \                          /
           +--- same state: race ---+
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=265)

#### 风险 2:无法捕获的异常

从 `async void` 方法中抛出的异常是出了名的难以捕获。
如果异常是在 `async void` 方法中第一个 `await` 之后抛出的,调用方法中的 `try-catch` 块就无法捕获它。
这是因为调用线程已经越过了调用点。
当异常最终在后台线程上发生时,没有任何处理程序来捕获它,从而导致整个应用崩溃。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=175)

#### Safe Fire and Forget

为了安全地处理后台任务,应当使用 `SafeFireAndForget` 扩展方法(来自 `AsyncAwaitBestPractices` 库)。
该方法明确表明在后台运行任务的意图,同时提供一种安全处理异常的机制。

```csharp
public NewsViewModel_BadAsyncAwaitPractices(IDispatcher dispatcher, HackerNewsAPIService hackerNewsAPIService) :
    base(dispatcher)
{
    _hackerNewsAPIService = hackerNewsAPIService;

    // Safe background execution with explicit exception handling
    Refresh(CancellationToken.None).SafeFireAndForget(onException: ex => Trace.WriteLine(ex));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=640)

在内部,`SafeFireAndForget` 以受控的方式使用 `async void`,把任务包在一个 `try-catch` 块中,确保任何异常都会被捕获并传给所提供的 `onException` 处理程序,而不是让进程崩溃。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=685)

你还可以指定希望处理的异常类型,从而实现细粒度的错误管理,例如记录 `HttpRequestException` 的特定属性。

```csharp
Refresh(CancellationToken.None).SafeFireAndForget<HttpRequestException>(ex => 
    Trace.WriteLine(ex.HttpRequestError));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-void-62197172/?t=760)

虽然 `SafeFireAndForget` 仍然允许调用线程立即继续执行,但它提供了必要的护栏来防止应用崩溃,并让其他开发者一眼就能看出这次调用是异步的。

---

## 3. CancellationToken, ConfigureAwait, .Wait() and .Result

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/) · 20:05

### 总结

本课介绍 C# 异步编程的基本最佳实践,重点是正确使用 CancellationToken 来处理任务取消、使用 ConfigureAwait 来优化线程管理并防止 UI 死锁,以及务必避免 .Wait() 和 .Result 这类阻塞调用。
本课还介绍了 .NET 8 引入的 ConfigureAwaitOptions 枚举,它对任务的延续行为提供细粒度的控制,包括上下文捕获、强制让出(forced yielding)和异常抑制。

### 核心概念

* **CancellationToken**:始终把 cancellation token 传递给异步方法,让调用方能够终止长时间运行的操作,并改善用户体验。
* **WaitAsync**:使用这个扩展方法,为原生不支持取消参数的任务应用 cancellation token。
* **ConfigureAwait(false)**:阻止任务捕获当前的同步上下文,让延续可以在任何可用的线程池线程上运行。这能提升性能,并防止 UI 线程上的死锁。
* **阻塞调用**:避免使用 `.Wait()` 和 `.Result`,因为它们会阻塞调用线程,可能导致 UI 冻结和线程池耗尽。
* **ConfigureAwaitOptions (.NET 8)**:一个基于标志位的枚举,对任务的 awaitable 提供高级控制,包括 `ForceYielding` 和 `SuppressThrowing`。

### 课程笔记

#### Cancellation Token 与 WaitAsync

异步方法应当始终接受一个 `CancellationToken`,让使用方能够管理任务的生命周期。
例如,在移动应用中,开发者可能希望在网络连接较差时,于 15 秒后取消刷新操作。
虽然 `Task.Delay` 等许多内置方法可以直接接受 token,但一些第三方或遗留 API 并不支持。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=190)

如果某个异步方法不支持 `CancellationToken` 参数,可以使用 `WaitAsync` 扩展方法为现有任务"外挂"取消支持。

```csharp
// Using WaitAsync to add cancellation support to a task that doesn't natively support it
var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2)).WaitAsync(token);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=235)

#### 用 ConfigureAwait(false) 优化上下文

默认情况下,当一个任务被 await 时,运行时会尝试回到原来的同步上下文(例如 UI 线程)。
如果 `await` 之后的代码计算开销很大(例如遍历数千个条目并执行复杂排序),它就可能锁住 UI 线程,使应用失去响应。

使用 `ConfigureAwait(false)` 会告诉运行时它不需要回到调用线程。
相反,延续可以在线程池中任何可用的线程上运行。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=460)

#### 避免阻塞调用

`.Wait()` 和 `.Result` 这类方法是同步的阻塞调用。
它们迫使调用线程一直等到任务完成。
如果在 UI 线程上调用,应用就会冻结。
此外,在 ASP.NET Core 这类高规模环境中,这些调用会导致线程池耗尽,因为它们为单个操作占用了两个线程(调用线程和任务线程)。

始终使用 `await` 关键字,即使在 `finally` 块中也是如此。

```csharp
finally
{
    // Use await instead of .Wait() to avoid blocking the calling thread
    await minimumRefreshTimeTask;
    IsListRefreshing = false;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=655)

#### ConfigureAwaitOptions (.NET 8)

.NET 8 引入了 `ConfigureAwaitOptions` 枚举,对任务的 awaitable 提供更具描述性、更灵活的控制。

*   `ConfigureAwaitOptions.None` 等价于 `ConfigureAwait(false)`。
*   `ConfigureAwaitOptions.ContinueOnCapturedContext` 等价于 `ConfigureAwait(true)`。

```csharp
// Equivalent to ConfigureAwait(true)
var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories)
                            .ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=895)

```csharp
// Equivalent to ConfigureAwait(false)
var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories)
                            .ConfigureAwait(ConfigureAwaitOptions.None);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=925)

高级标志位包括 `ForceYielding`,它的行为类似 `Task.Yield()`,即使任务已经完成也会强制让出。
这有助于确保 UI 线程有机会进行更新。

```csharp
// Combining flags to prevent context capture and force yielding
var topStoriesList = await GetTopStories(token, storyCount: StoriesConstants.NumberOfStories)
                            .ConfigureAwait(ConfigureAwaitOptions.None | ConfigureAwaitOptions.ForceYielding);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=955)

最后,`SuppressThrowing` 可用于在 await 任务时阻止异常被重新抛出。
注意,此选项只支持非泛型任务(例如 `Task`,而不是 `Task<T>`)。

```csharp
// Suppressing exceptions in a non-generic task (e.g., Task.Delay)
await minimumRefreshTimeTask.ConfigureAwait(ConfigureAwaitOptions.ContinueOnCapturedContext
                                          | ConfigureAwaitOptions.ForceYielding
                                          | ConfigureAwaitOptions.SuppressThrowing);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/cancellationtoken-configureawait-wait-and-result-62197173/?t=1090)

---

## 4. IAsyncEnumerable

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/) · 21:17

### 总结

本课演示如何使用 IAsyncEnumerable 和 .NET 9 的 Task.WhenEach API,把顺序执行的异步操作重构为并行的流。
Task.WhenAll 会迫使用户等待整批操作全部完成,而摆脱它之后,开发者可以使用异步迭代器在数据可用时就把它流式传送到 UI。
本课还详细介绍了用 FrozenSet 实现不可变性、在异步流中用 [EnumeratorCancellation] 特性正确处理 token,以及即使在已完成的任务上也保留 ConfigureAwait(false) 的重要性,以确保代码在未来重构时依然健壮。

### 核心概念

* **FrozenSet**:.NET 8 中一种真正不可变、针对读多场景优化的集合,不过它在创建时有性能损耗。
* **并行任务执行**:使用 LINQ `Select` 同时启动多个后台任务,而不是在循环中逐个 await。
* **IAsyncEnumerable**:一个支持异步迭代的接口,允许值在产生时就流式传送给调用方。
* **Task.WhenEach**:一个 .NET 9 API,它按任务完成的顺序返回一个由任务组成的 `IAsyncEnumerable`。
* **EnumeratorCancellation**:用于异步迭代器中 `CancellationToken` 参数的特性,使其与 .NET 运行时的迭代逻辑正确集成。
* **Yield Return**:异步迭代器中使用的关键字,用于提供流的下一个元素而不退出方法。

### 课程笔记

#### 用 FrozenSet 实现不可变性

在处理来自 API 或数据库等外部来源的数据时,最佳实践是保持这些数据不可变。
.NET 8 引入了 `FrozenSet<T>`,它对不可变性的保证比 `IReadOnlyList<T>` 更严格。
虽然 `IReadOnlyList` 阻止了对列表的直接操作,但特定索引处的底层值有时仍然可以被修改。
`FrozenSet` 则是真正冻结的。
然而,创建 `FrozenSet` 的计算开销比创建普通列表更大,因此在高流量的 ASP.NET Core API 这类对性能敏感的环境中应当审慎使用。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=10)

#### 过渡到并行

上面的顺序做法效率低下,因为它要等每个 API 调用完成后才开始下一个。
为了提升性能,我们可以用 LINQ 同时启动所有任务,并把它们存放在一个任务列表中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=370)

虽然 `Task.WhenAll` 比顺序执行更快,但它的用户体验很差,因为在整批任务完成之前 UI 一直是空白的。
如果获取 100 篇文章需要五秒,用户就会在整个过程中一直看到一个加载圈,然后所有文章才一次性出现。

#### 用 IAsyncEnumerable 和 Task.WhenEach 实现流式传输

为了提供响应更及时的体验,我们可以使用 `IAsyncEnumerable<T>` 在结果到达时就流式传送它们。
在 .NET 9 中,`Task.WhenEach` 方法让我们可以遍历一组任务,并在每个任务一完成时就拿到它。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=1165)

在这个实现中:

1.  **`yield return`**:这个关键字把一个值返回给调用方的 `await foreach` 循环,而不退出方法。
    方法的状态会被保留,并在调用方处理完该条目后恢复执行。
2.  **`[EnumeratorCancellation]`**:这个特性确保传给方法的 `CancellationToken` 被 .NET 运行时的异步迭代器机制正确使用。
3.  **`.WithCancellation(token)`**:这个扩展方法用于把 token 传入 `Task.WhenEach` 返回的 `IAsyncEnumerable`。
4.  **`ConfigureAwait(false)`**:即使 `Task.WhenEach` 只返回已完成的任务,仍然建议使用 `ConfigureAwait(false)`。
    这可以防止代码不必要地切换线程,并确保如果这段代码之后被复制粘贴到任务尚未完成的上下文中,它依然是线程安全的。

```text
GetTopStoryIDs(token)
        |
        v
topStoryIds.Select(id => GetStory(id, token)).ToList()
        |
        v
Task.WhenEach(getTopStoriesTasks).WithCancellation(token)
        |   tasks in the order they complete
        v
await topStoryTask --> yield return topStory
                               |
                               v
               caller: await foreach (var story in GetTopStories(...))
```

#### 消费异步流

调用方法使用 `await foreach` 循环来消费这个流。
这让 UI 能在每篇文章返回时逐步更新,使用户感觉等待时间明显缩短。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/iasyncenumerable-62197174/?t=1250)

---

## 5. Returning a Task and ValueTask

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/) · 12:12

### 总结

本课探讨异步 C# 代码中的性能优化,重点是何时直接返回 Task、何时使用 async/await 关键字。
它演示了在简单的包装方法中省略 async/await 如何减少不必要的上下文切换和线程池开销,同时提醒在需要本地异常处理(try/catch)或保留特定堆栈跟踪时不要这样做。
此外,本课还介绍了 ValueTask,它是一种节省内存的替代方案,适用于"热路径"经常同步完成的方法,例如返回缓存数据时,从而避免堆分配。

### 核心概念

*   **减少上下文切换**:直接返回 `Task` 而不是 await 它,可以避免状态机的开销以及可能的线程切换。
*   **省略 Async/Await**:在只有一行的透传方法中去掉 `async` 和 `await` 关键字以提升性能。
*   **对堆栈跟踪的影响**:省略 `async` 可能导致某个方法在异常的调用栈中缺失,这可能影响调试。
*   **Try/Catch 中的 Async/Await**:你必须在 `try/catch` 块内使用 `await`,才能确保异常在本地被捕获;直接返回 `Task` 会绕过 catch 块。
*   **ValueTask**:`Task` 的一种值类型(struct)替代方案,可减少堆分配,非常适合经常同步完成的方法。
*   **热路径优化**:当最常见的执行路径不需要异步等待时(例如返回缓存结果)使用 `ValueTask`。

### 课程笔记

#### 为了性能省略 Async/Await

当一个方法只是包装另一个异步调用并返回相同类型时,使用 `async` 和 `await` 关键字可能会引入不必要的开销。
在标准的 `async await` 流程中,一个线程进入方法,碰到 `await`,然后回到线程池。
当底层任务完成时,可能会分配另一个不同的线程来完成该方法。
对于只有一行的方法来说,这会仅仅为了返回一个值而产生多次上下文切换。

考虑下面这个使用 `async await` 和 `ConfigureAwait(false)` 的 `GetStory` 实现:

```csharp
//ToDo Refactor
async Task<StoryModel> GetStory(long storyId, CancellationToken token)
{
    return await _hackerNewsAPIService.GetStory(storyId, token).ConfigureAwait(false);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=25)

去掉 `async` 和 `await` 关键字后,该方法会直接把 `Task` 返回给调用方。
这把状态机逻辑推迟到更上层的调用方,从而省下一次上下文切换并提升性能。

```csharp
//Refactored to return Task directly
Task<StoryModel> GetStory(long storyId, CancellationToken token)
{
    return _hackerNewsAPIService.GetStory(storyId, token);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=115)

**关于堆栈跟踪的说明**:当你直接返回 `Task` 时,如果该任务内部发生异常,这个方法可能不会出现在堆栈跟踪中。
虽然这在复杂场景下会让调试稍微困难一些,但在高性能的库代码中,性能收益通常值得这样的取舍。

#### Try/Catch 这个例外

一个常见错误是在包含 `try/catch` 块的方法中尝试省略 `async/await`。
如果你直接返回 `Task`,方法的执行会在 `return` 语句之后立即结束。
如果该任务随后失败,异常会被抛回给这个方法的调用方,完全绕过本地的 `catch` 块。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=370)

为了确保异常在本地被捕获并记录,必须使用 `await` 关键字,让方法上下文一直存活到任务完成。

#### 用 ValueTask 优化

`Task` 是分配在堆上的引用类型,而 `ValueTask` 是分配在栈上的值类型(一个只读 struct)。
向栈中添加一个值是 O(1) 操作,比堆分配快得多,因为堆分配需要索引和垃圾回收。

当一个方法的"热路径"(即执行最频繁的代码路径)同步完成时,`ValueTask` 最为有效。
例如,如果数据已被缓存,无需调用 API 就能立即返回,`ValueTask` 就能避免创建 `Task` 对象的开销。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/returning-a-task-and-valuetask-62197175/?t=505)

**ValueTask 的最佳实践**:

1.  **不要重复使用**:与 `Task` 不同,`ValueTask` 永远不应被 await 多次。
2.  **不要存储**:避免到处传递 `ValueTask` 或把它存放在字段中;应当立即 await 它来消费。
3.  **用于热路径**:只有当方法经常同步返回结果时,才把 `Task` 换成 `ValueTask`。

---

## 6. Review

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197176/) · 14:28

### 总结

本课回顾 C# 异步编程的基本最佳实践,涵盖同步上下文管理、异常处理和性能优化。
它强调用 await 代替阻塞调用、有策略地使用 ConfigureAwait(false),以及 IAsyncEnumerable 和 IAsyncDisposable 等现代特性的好处。

### 核心概念

*   优先使用 `await`,而不是 `.Wait()` 或 `.Result()`。
*   当同步执行无法避免时,使用 `.GetAwaiter().GetResult()`。
*   除了在 `try-catch` 或 `using` 块中,避免使用 `return await`。
*   在非 UI 层中应用 `ConfigureAwait(false)`。
*   在性能关键的同步路径上利用 `ValueTask`。
*   用 `IAsyncEnumerable` 流式传输数据。
*   实现 `IAsyncDisposable` 以异步清理资源。

### 课程笔记

#### 阻塞调用与异常处理

C# 异步编程的首要规则是避免 `.Wait()` 或 `.Result()` 这类阻塞调用。
这些方法会阻塞调用线程,直到后台任务完成,这可能导致死锁和性能下降。
在现代 .NET 开发中,`await` 关键字应当是默认选择。

在少数被迫执行 sync-over-async 的场景中(例如实现一个不允许返回 `Task` 类型的接口时),推荐的做法是使用 `.GetAwaiter().GetResult()`。
虽然这仍然是一个阻塞调用,但它优于 `.Wait()` 或 `.Result()`,因为它不会把异常包装进 `System.AggregateException`。
相反,它会重新抛出原始异常(例如 `TimeoutException`),保留预期的堆栈跟踪,使调试明显更容易。

#### Fire and Forget 任务

对于完成时机对后续逻辑并不关键的任务,避免使用 `async void`。
相反,应使用 `AsyncAwaitBestPractices` NuGet 包中的 `SafeFireAndForget` 扩展方法。
这种做法明确地向其他开发者表明该任务是要独立运行的,并为优雅的异常处理提供护栏。

#### 省略 Async 和 Await

当方法中 `await` 关键字的唯一用途是在 return 语句中时,你可以省略 `async` 和 `await` 关键字并直接返回 `Task`,以此优化性能。
这减少了状态机的开销和不必要的线程切换。

例如,在服务层中,你可以直接返回任务:

```csharp
public Task<StoryModel> GetStory(long storyId, CancellationToken token) => _hackerNewsClient.GetStory(storyId, token);
```

不过,有两个关键的例外情况必须保留 `await` 关键字:

1.  **Try-Catch 块**:如果你返回一个任务而不 await 它,方法会立即退出 try 块,catch 块永远不会拦截该任务抛出的异常。
2.  **Using 块**:在 `using` 块中返回一个任务而不 await 它,会导致可释放对象在任务完成之前就被释放,往往引发 `ObjectDisposedException`。

#### 用 ConfigureAwait 管理上下文

`ConfigureAwait(false)` 用于表明任务不需要在原来的同步上下文上恢复执行。
强烈推荐在服务、数据库和模型等非 UI 层中使用它,以提升性能并避免死锁。
在 MAUI 或 WinForms 这类以 UI 为主的框架中,UI 层通常应避免使用 `ConfigureAwait(false)`,以确保更新发生在 UI 线程上。

.NET 8 和 9 引入了 `ConfigureAwaitOptions`,它提供更细粒度的控制:

*   **None**:等价于 `ConfigureAwait(false)`。
*   **ContinueOnCapturedContext**:等价于 `ConfigureAwait(true)`。
*   **ForceYielding**:即使任务已经完成,也强制进行一次异步让出,这有助于保持 UI 的响应性。
*   **SuppressThrowing**:阻止 `await` 重新抛出异常,不过应谨慎使用。

#### 性能与流式传输

对于方法经常同步返回的高性能场景,使用 `ValueTask`。
与分配在堆上的类 `Task` 不同,`ValueTask` 是一个存在于栈上的 struct,可以减少分配开销。

在处理返回多个结果的 API 时,`IAsyncEnumerable` 是流式传输数据的首选。
这让 UI 能在结果到达时就更新,而不必等待整个集合。
在实现这些方法时,使用 `[EnumeratorCancellation]` 特性,确保 `CancellationToken` 被运行时迭代器正确处理。

#### 异步资源清理

`IAsyncDisposable` 允许异步清理非托管资源,这对于关闭文件流这类开销较大的操作尤其有用。
这确保调用线程在释放过程中不会被阻塞。

```csharp
await using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate).ConfigureAwait(false))
{
    // Save data to file...
} // await executes here
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197176/?t=685)

在上面的示例中,与 `using` 块关联的 `await` 在右花括号处执行,确保流被异步释放。

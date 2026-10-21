# The Task class

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 3 章
> 共 4 课 · 约 45:51
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Tasks in Parallel](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/tasks-in-parallel-69955977/) | 1:31 | [↓](#1-tasks-in-parallel) |
| 2 | [Task.WhenAll](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/) | 18:18 | [↓](#2-taskwhenall) |
| 3 | [Task.WhenAny](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/) | 14:05 | [↓](#3-taskwhenany) |
| 4 | [Task.WhenEach (IAsyncEnumerable)](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/) | 11:57 | [↓](#4-taskwheneach-iasyncenumerable) |

---

## 1. Tasks in Parallel

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/tasks-in-parallel-69955977/) · 1:31

### 总结

本课介绍 "Tasks in Parallel" 这一部分,重点是 .NET Parallel Task Library 的功能,例如 Task.WhenAll、Task.WhenAny 和 Task.WhenEach。
它把本课程的范围界定为并行编程,并指出需要预先掌握的异步编程知识,包括任务的内部机制和编译器转换。

### 核心概念

- .NET 中的 Parallel Task Library(PTL)选项。
- 使用 `Task.WhenAll`、`Task.WhenAny` 和 `Task.WhenEach` 协调多个任务。
- 并行编程与基础异步编程之间的区别。
- 预备知识:任务的内部机制、async/await 的编译器行为、`ConfigureAwait` 以及取消令牌。

### 课程笔记

本部分重点讲解如何使用 .NET Parallel Task Library 实际实现并行任务。
讲解内容涵盖如何协调多个任务,以优化性能和资源利用率。

以下方法是管理并行任务执行的核心:

- **Task.WhenAll**:创建一个任务,在所有给定任务都完成时完成。它是汇总多个并行操作结果的标准做法。
- **Task.WhenAny**:创建一个任务,在给定任务中任意一个完成时完成。这在超时或冗余请求等只需要第一个结果的场景中很有用。
- **Task.WhenEach**:允许在任务完成时逐个遍历它们,从而可以立即处理结果,而无需等待整组任务全部完成。

本课程专门针对并行编程技术。
它假定开发者已经熟悉 C# 异步编程的基础知识,包括编译器如何在底层修改代码以创建状态机、如何正确使用 `ConfigureAwait` 管理同步上下文,以及如何实现 `CancellationToken` 以进行协作式取消。

---

## 2. Task.WhenAll

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/) · 18:18

### 总结

Task.WhenAll 是 System.Threading.Tasks 库中的一个基础方法,用于协调多个异步操作的并行执行。
它创建一个只有在所有给定任务都完成后才会完成的任务,让开发者可以同时发起多个后台操作,并在继续之前等待整组操作结束。
本课通过 HTTP 请求和一个模拟烹饪的应用演示它的用法,重点介绍如何管理任务列表,以及如何同时使用单个任务的令牌和 WaitAsync 扩展方法来整合取消逻辑。

### 核心概念

* 使用 Task.WhenAll 并行执行多个 Task 对象。
* "热"任务在初始化时立即执行。
* 通过 params 或 IEnumerable<Task> 等待一组任务。
* 处理集合中已完成任务的结果。
* 使用 CancellationTokenSource 实现超时和取消。
* 使用 WaitAsync 扩展方法对聚合任务应用取消。

### 课程笔记

Task.WhenAll 是 System.Threading.Tasks 库的一部分,用于并行运行多个任务。
在 .NET 中,任务一旦初始化,通常就会开始在后台运行。
例如,在发起多个 HTTP 请求时,每次 GetAsync 调用都会返回一个立即开始运行的任务。
把这些任务传给 Task.WhenAll 并 await 其结果,当前方法的执行就会暂停,直到这组任务中的每一个都已完成。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=10)

这种模式同样适用于任务数量不固定的情况。
你可以把多个任务添加到一个 List<Task<T>> 中,再把这个列表传给 Task.WhenAll。
聚合任务完成后,你可以遍历该列表来处理结果,例如反序列化 JSON 响应。

为了在实际场景中演示这一点,设想一个烹饪模拟,其中不同的食物需要不同的准备时间。
我们定义一个抽象的 Food 类,它有一个 Cook 方法,使用 Task.Delay 模拟烹饪过程。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=540)

在主程序中,我们初始化各个食物对象和一个 CancellationTokenSource。
然后使用 Task.WhenAll 同时开始烹饪所有食物。
日志会显示所有食物同时开始烹饪,并按照各自的 cookTime 值完成。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=715)

在使用异步任务时,实现取消逻辑是一种最佳实践。
如果烹饪过程超过了某个阈值(模拟饥饿的客人离开),就会抛出 TaskCanceledException。
把 Task.WhenAll 调用包在 try-catch 块中,我们就可以优雅地处理这些超时。

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

```text
t(s)            0    1    2    3    4    5
Turkey          |========================|
Gravy           |====|
MashedPotatoes  |=========|
Stuffing        |=========|
token (4s)                          ^ TaskCanceledException
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=895)

虽然 Task.WhenAll 本身不接受 CancellationToken,但你可以使用 WaitAsync 扩展方法对整个操作应用取消。
这让你可以把超时附加到 WhenAll 返回的聚合任务上,这通常比把同一个令牌传入每一个单独的方法调用更简洁。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenall-69955978/?t=1030)

---

## 3. Task.WhenAny

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/) · 14:05

### 总结

Task.WhenAny 提供了一种机制,可以监视多个任务,并在第一个任务完成时立即作出响应,从而实现一种"流式"的数据处理方式。
与会一直阻塞到所有任务都完成的 Task.WhenAll 不同,Task.WhenAny 返回那个已到达终止状态的具体任务,从而可以立即更新 UI 或提前退出。
本课演示在循环中使用 Task.WhenAny、在任务完成时逐个处理一组任务的标准模式,同时强调正确处理取消和结果的重要性。

### 核心概念

*   **Immediate Completion(立即完成)**:`Task.WhenAny` 返回一个任务,只要给定集合中的任意一个任务完成,它就会完成。
*   **Task Collection(任务集合)**:它接受 `params Task[]` 或 `IEnumerable<Task>`。
*   **Return Value(返回值)**:该方法返回最先完成的那个 `Task` 对象,而不是该任务的结果。
*   **The While-Loop Pattern(While 循环模式)**:为了在任务完成时处理所有任务,开发者通常使用一个 `while` 循环,在其中 await `WhenAny`、处理返回的任务,并把它从集合中移除。
*   **Result Extraction(提取结果)**:尽管返回的任务已经完成,最佳实践仍是 `await` 它来获取结果或处理异常,而不是使用 `.Result`。
*   **Background Task Management(后台任务管理)**:把 `CancellationToken` 传给各个单独的任务至关重要,这样才能确保在发生超时或取消时后台工作真正停止。

### 课程笔记

虽然 `Task.WhenAll` 适合用来等待一批操作完成,但如果用户必须等到最慢的操作结束才能看到任何结果,就可能导致糟糕的用户体验。
`Task.WhenAny` 支持一种响应更快的设计,数据可以在流入时就被处理或显示。

设想一个发起多个 API 调用的应用。
使用 `Task.WhenAny`,第一个响应一到达就可以更新 UI:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=10)

#### 在烹饪示例中实现 Task.WhenAny

为了在实际场景中演示这一点,我们修改烹饪模拟。
首先,更新 `Food` 基类及其派生类,让 `Cook` 方法返回一个 `Task<string>`,其中包含已做好的食物的名称。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=325)

在主程序中,我们初始化一个任务列表。
通过使用 `while` 循环和 `Task.WhenAny`,我们可以让"客人"在每道菜一做好时就开始吃,而不是等整顿饭都准备好。

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

```text
cookingTaskList not empty?
   |
   +-- no --> "Dinner is ready!"
   |
   yes
   v
await Task.WhenAny(cookingTaskList)  --> completedCookingTask
   v
cookingTaskList.Remove(completedCookingTask)
   v
var name = await completedCookingTask
   v
Trace.WriteLine($"Eating {name}")
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=475)

#### await 已完成的任务与 .Result

当 `Task.WhenAny` 返回时,它提供的任务保证处于已完成状态。
正因如此,访问 `.Result` 不会阻塞调用线程。
不过,仍然建议使用 `await` 关键字来获取结果。

```csharp
// While this is technically safe here because the task is completed:
var name = completedCookingTask.Result;

// This is preferred to maintain consistency and prevent issues if code is refactored:
var name = await completedCookingTask;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=625)

使用 `await` 可以确保,如果这段代码将来被复制到任务可能尚未完成的上下文中,也不会造成 sync-over-async 死锁。
它还提供了一种更简洁的方式来处理任务内部可能发生的异常。

#### 取消与后台任务

如果发生超时(例如在 `WhenAny` 调用上使用 `.WaitAsync(token)`),`Task.WhenAny` 调用会抛出 `TaskCanceledException`。
然而,如果列表中的底层任务没有同样收到取消令牌,它们可能会继续在后台线程上运行。

为了确保在超时时所有资源都被清理、后台工作立即停止,必须把 `CancellationToken` 传入各个单独的异步方法(例如本例中的 `Cook`)。

```csharp
List<Task<string>> cookingTaskList = 
[
    turkey.Cook(cancellationTokenSource.Token),
    gravy.Cook(cancellationTokenSource.Token),
    mashedPotatoes.Cook(cancellationTokenSource.Token),
    stuffing.Cook(cancellationTokenSource.Token)
];
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-whenany-69955979/?t=805)

---

## 4. Task.WhenEach (IAsyncEnumerable)

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/) · 11:57

Task.WhenEach 是一个返回 `IAsyncEnumerable` 的工具方法,它提供了一种简洁、易读的方式,在多个异步操作完成时以流的形式获取它们的数据。
这种方式取代了以前在 `while` 循环中使用 `Task.WhenAny` 的模式,那种模式需要手动管理任务并把它们从集合中移除。

### 核心概念

*   **IAsyncEnumerable**:`Task.WhenEach` 的返回类型,支持对任务流进行异步迭代。
*   **await foreach**:用于消费该流的 C# 语法,每当集合中的一个任务完成时就执行一次循环体。
*   **WithCancellation**:一个扩展方法,要对 `IAsyncEnumerable` 流应用 `CancellationToken` 就必须使用它。
*   **ToAsyncEnumerable**:.NET 10 中针对任务集合的一个扩展方法,可以作为 `Task.WhenEach` 在功能上的替代。
*   **Parallel Execution Order(并行执行顺序)**:任务按照完成的顺序被处理,这不一定是它们启动或被添加到集合中的顺序。

### 课程笔记

`Task.WhenEach` 可以实现高效的数据流处理。
在有多个后台操作(例如 API 请求)同时运行的场景中,它让应用可以在结果到达时立即处理并显示,而不是等待整批操作完成。

设想一个示例:发起五个同时进行的 API 请求,并把它们添加到一个列表中。
使用 `Task.WhenEach`,我们可以在它们完成时逐个遍历:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=65)

#### 从 Task.WhenAny 重构

在 `Task.WhenEach` 出现之前,开发者必须实现一个 `while` 循环才能达到类似的流式行为。
这需要手动把已完成的任务从列表中移除,以免对它重复处理:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=235)

有了 `Task.WhenEach`,.NET 会在内部处理任务跟踪,代码因此简洁得多:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=490)

#### 并行与竞态条件

当任务并行运行时,它们的完成顺序是不确定的。
即使一个任务比另一个早几纳秒启动,由于 CPU 调度、线程优先级或内部开销,第二个任务也可能先完成。
编写并行代码时,应当预期任务可能以任意顺序完成。

#### 在 IAsyncEnumerable 上取消

`Task.WhenEach` 不接受 `CancellationToken` 作为直接参数。
相反,由于它返回 `IAsyncEnumerable`,你需要使用 `WithCancellation` 扩展方法把令牌传给枚举器:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=565)

#### ToAsyncEnumerable 扩展方法

在 .NET 10(C# 14)中,为 `IEnumerable` 类型引入了一个名为 `ToAsyncEnumerable()` 的新扩展方法。
这个方法可以直接在任务列表上调用,提供与 `Task.WhenEach` 相同的功能。
两者之间的选择主要取决于编码风格和对可读性的偏好。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/task-wheneach-iasyncenumerable-69955980/?t=610)

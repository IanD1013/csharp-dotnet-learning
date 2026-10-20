# Compiler Generated Code

> 课程:[From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · 第 3 章
> 共 3 课 · 约 33:41
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Async Await Example](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-await-example-62197123/) | 6:48 | [↓](#1-async-await-example) |
| 2 | [Decompiled Code](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/) | 24:37 | [↓](#2-decompiled-code) |
| 3 | [Review](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197125/) | 2:16 | [↓](#3-review) |

## 1. Async Await Example

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-await-example-62197123/) · 6:48

### 总结

本课探讨异步方法的执行流程,重点关注 await 关键字如何管理 UI 线程与后台线程之间的线程切换。
通过追踪一个 GetLibraries 示例,本课演示了异步编程如何在 I/O 密集型操作期间释放主线程、并在后台任务完成后恢复执行,从而避免 UI 卡死。

### 核心概念

- 通过 await 关键字让出线程并恢复执行。
- 通过释放主线程来保持 UI 的响应性。
- 从 .NET 线程池(Thread Pool)中分配后台线程。
- 通过同步完成("快速路径",fast path)进行性能优化。
- 线程切换对 CPU 周期的影响。

### 课程笔记

GetLibraries 方法演示了 async 与 await 如何协同工作,以管理多线程和应用程序的响应性。

```csharp
public async Task<List<LibraryModel>> GetLibraries()
{
    var response = await httpClient.GetAsync("https://codeTraveler.io/MauiLibraries");
    response.EnsureSuccessStatusCode();

    var stream = await response.Content.ReadAsStreamAsync();
    var libraries = await JsonSerializer.DeserializeAsync<List<LibraryModel>>(stream);

    return libraries ?? throw new InvalidOperationException("Libraries cannot be null");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/async-await-example-62197123/?t=40)

在 UI 驱动的应用程序中(例如 .NET MAUI、WPF 或 WinForms),主线程(Thread1)既负责执行逻辑,也负责渲染用户界面。
如果这个线程被一个长时间运行的同步操作阻塞,应用程序就会失去响应。

调用 GetLibraries 时,Thread1 开始执行,并在 httpClient.GetAsync 处遇到第一个 await。
此时,Thread1 会被立即释放回系统,去处理滚动或按钮点击之类的用户交互。
与此同时,.NET 运行时从线程池中取出一个可用的后台线程(例如 Thread2)来处理这个网络请求。

如果这个请求耗时很长,比如 15 秒,应用程序仍会保持响应,因为 UI 线程没有被阻塞。
在显示后台活动指示器的同时,用户仍然可以滚动或导航。
如果不使用 async/await,Thread1 会在请求期间一直被锁住,导致应用程序卡死,并可能让用户强制退出。

后台操作完成后,它会通知运行时,然后 Thread1 恢复执行以处理响应。
这种让出与恢复的循环会在每个 await 点发生。
例如,Thread6 处理 ReadAsStreamAsync 时 Thread1 会再次让出,Thread5 处理 DeserializeAsync 时它又会再让出一次。

```text
Thread1 (UI)           后台线程
------------           --------
GetLibraries()
await GetAsync  -----> Thread2: 网络请求
   (让出)                  |
恢复执行        <----------+
await ReadAsStreamAsync -> Thread6
   (让出)                  |
恢复执行        <----------+
await DeserializeAsync --> Thread5
   (让出)                  |
恢复执行        <----------+
```

.NET 运行时会在切换线程之前检查任务是否已经完成,以此优化这个过程。
如果结果已经可用("快速路径"),运行时会在当前线程上继续执行,以避免与上下文切换相关的 CPU 开销。
这一关于线程管理的高层概览,为接下来深入研究编译器为实现这些切换而生成的具体代码做好了铺垫。

## 2. Decompiled Code

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/) · 24:37

### 总结

C# 编译器会把异步方法转换为一个状态机,以管理跨越各个挂起点的执行。
本课使用 SharpLab.io 探讨一个 async 方法的反编译代码,详细说明 IAsyncStateMachine 接口、AsyncTaskMethodBuilder 和 MoveNext 方法如何协同工作,来处理任务完成、线程恢复和异常传播。

### 核心概念

- **编译器生成的状态机**:标记了 `async` 关键字的方法会被重写为一个实现 `IAsyncStateMachine` 接口的私有 struct(在 Release 配置下)或 class(在 Debug 配置下)。
- **AsyncTaskMethodBuilder**:一个管理任务生命周期的辅助 struct,包括启动状态机以及设置最终结果或异常。
- **状态转换**:一个内部的 `state` 字段(从 -1 开始)跟踪方法在各个 `await` 表达式之间的进度。
- **MoveNext 方法**:状态机的核心引擎,其中包含一个 switch 语句,使方法能在被等待的任务完成后从正确的位置恢复执行。
- **异常处理**:状态机把方法体包裹在一个 `try-catch` 块中,捕获异常并将其交给 builder,以便在任务被等待时重新抛出。

### 课程笔记

当一个方法被标记为 `async` 关键字时,.NET 编译器会生成大量基础设施代码来处理异步流程。
借助 SharpLab.io 之类的工具,开发者可以查看中间语言(IL)或反编译后的 C# 代码,以理解这种转换。
编译器本质上是把方法拆分成由 `await` 点分隔开的若干块。

一个库服务的原始高层 C# 代码可能如下所示:

```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class LibraryService(HttpClient httpClient)
{
    public async Task<List<LibraryModel>> GetLibraries()
    {
        var response = await httpClient.GetAsync("https://codeTraveler.io/Main");
        response.EnsureSuccessStatusCode();

        var contentStream = await response.Content.ReadAsStreamAsync();
        var libraries = await JsonSerializer.DeserializeAsync<List<LibraryModel>>(contentStream);

        return libraries ?? throw new InvalidOperationException("Libraries cannot be null");
    }
}

class LibraryModel
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string ImageSource { get; init; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/?t=35)

#### 状态机入口点

编译器把 `GetLibraries` 方法的方法体替换为初始化一个状态机的代码。
这个状态机通常是一个私有 struct,名字类似 `<GetLibraries>d__2`。

```csharp
[AsyncStateMachine(typeof(<GetLibraries>d__2))]
public Task<List<LibraryModel>> GetLibraries()
{
    <GetLibraries>d__2 stateMachine = default(<GetLibraries>d__2);
    stateMachine.<>t__builder = AsyncTaskMethodBuilder<List<LibraryModel>>.Create();
    stateMachine.<>4__this = this;
    stateMachine.<>1__state = -1;
    stateMachine.<>t__builder.Start(ref stateMachine);
    return stateMachine.<>t__builder.Task;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/?t=295)

入口点执行以下步骤:

1. 实例化状态机 struct。
2. 初始化 `AsyncTaskMethodBuilder`。
3. 把 `state` 设为 -1,表示任务尚未开始。
4. 调用 `builder.Start`,它会立即执行状态机的第一部分。
5. 返回由 builder 管理的 `Task`。

#### 状态机结构

生成的 struct 实现了 `IAsyncStateMachine`,并包含用于方法参数、局部变量、builder 以及当前状态的字段。
在 Release 构建中使用 struct 以获得性能;在 Debug 构建中则生成 class,以便更好地支持调试和断点。

```csharp
[StructLayout(LayoutKind.Auto)]
[CompilerGenerated]
private struct <GetLibraries>d__2 : IAsyncStateMachine
{
    public int <>1__state;
    public AsyncTaskMethodBuilder<List<LibraryModel>> <>t__builder;
    public LibraryService <>4__this;
    private TaskAwaiter<HttpResponseMessage> <>u__1;
    private TaskAwaiter<Stream> <>u__2;
    private ValueTaskAwaiter<List<LibraryModel>> <>u__3;

    private void MoveNext() { /* ... */ }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/?t=385)

编译器为这些字段使用"不可言说的名称"(unspeakable names,包含尖括号和下划线),以避免与用户定义的变量发生命名冲突。

#### MoveNext 方法

`MoveNext` 方法包含原始方法的实际逻辑,它被包裹在一个 `try-catch` 块中,并由一个针对 `state` 字段的 `switch` 语句驱动。

```csharp
private void MoveNext()
{
    int num = <>1__state;
    try
    {
        TaskAwaiter<HttpResponseMessage> awaiter3;
        switch (num)
        {
            default:
                awaiter3 = libraryService.httpClient.GetAsync("...").GetAwaiter();
                if (!awaiter3.IsCompleted)
                {
                    num = (<>1__state = 0);
                    <>u__1 = awaiter3;
                    <>t__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
                    return;
                }
                goto IL_007e;
            case 0:
                awaiter3 = <>u__1;
                <>u__1 = default(TaskAwaiter<HttpResponseMessage>);
                num = (<>1__state = -1);
                goto IL_007e;
        }
        // ...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/?t=490)

遇到 `await` 时:

1. 编译器检查 `awaiter.IsCompleted`。
   如果任务已经完成,它会跳转到下一段代码(例如 `goto IL_007e`),以避免不必要的上下文切换。
2. 如果任务尚未完成,它会设置状态(例如 `state = 0`),保存 awaiter,并调用 `AwaitUnsafeOnCompleted`。
   这会释放当前线程,使其返回给调用方或线程池。
3. 当被等待的任务完成时,`MoveNext` 会被再次调用。
   `switch` 语句利用保存下来的状态直接跳转到正确的 `case`(例如 `case 0`),恢复 awaiter,然后继续执行。

```text
state = -1 --> MoveNext() --> awaiter.IsCompleted?
                                 |            |
                                是           否
                                 |            |
                                 v            v
                          goto IL_007e   state = 0
                                         保存 awaiter (<>u__1)
                                         AwaitUnsafeOnCompleted
                                         return (释放线程)
                                              |
                                        任务完成后
                                              v
                                         MoveNext()
                                         case 0: 恢复 awaiter
                                         state = -1
                                         goto IL_007e
```

#### 异常处理与完成

`MoveNext` 的整个方法体都被封装在一个 `try-catch` 块中。
如果有任何代码抛出异常,状态机会捕获它并交给 builder。

```csharp
catch (Exception exception)
{
    <>1__state = -2;
    <>t__builder.SetException(exception);
    return;
}
<>1__state = -2;
<>t__builder.SetResult(result3);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/?t=850)

如果一个任务没有被等待(例如使用 `Task.Run` 却不加 `await`),在这里捕获的任何异常都会被状态机吞掉、永远不会浮现出来,这可能让应用程序处于不一致的状态。
始终等待任务可以确保 `SetException` 被调用,并且异常会被重新抛给调用方。
当方法成功完成时,状态会被设为 -2,最终结果通过 `SetResult` 传给 builder。

```csharp
void IAsyncStateMachine.MoveNext()
{
    this.MoveNext();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/decompiled-code-62197124/?t=1015)

## 3. Review

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197125/) · 2:16

### 总结

本课回顾 C# 中 async 关键字在架构层面的影响,重点关注编译器生成的状态机带来的内存开销,以及等待任务对于确保异常被正确传播和处理的关键重要性。
本课强调,虽然每个 async 方法 80 字节的开销对大多数现代应用程序来说可以忽略不计,但对于 IoT 这类资源受限的环境,它是一个关键的考量因素。
此外,本课再次强调,等待任务对于异常传播必不可少,因为未被等待的任务会把异常困在编译器生成的 MoveNext 方法中,导致静默失败。

### 核心概念

- **内存开销**:由于生成的状态机,每个 async 方法会让编译后的程序集增加大约 80 字节。
- **状态机实现**:在 Release 构建中,编译器把状态机优化为 struct;在 Debug 构建中,它被实现为 class。
- **异常处理**:异步方法中的异常会在生成的 MoveNext 方法内部的 try-catch 块中被捕获。
- **等待任务**:任务必须被等待,异常才能传播给调用方;否则异常会被吞掉,无法处理。

### 课程笔记

使用 async 关键字会促使编译器生成大量底层基础设施来管理方法的状态。
这些基础设施会让生成的 DLL 中每个异步方法增加大约 80 字节。

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
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197125/?t=7)

虽然这种内存增长在一般的应用开发中通常无关紧要,但对于内存受到严格限制的嵌入式系统或 IoT 设备来说,它是一个至关重要的考量因素。
编译器在 Release 配置下会进一步优化:把状态机生成为 struct 而不是 class,从而减少堆分配。
在 Debug 配置下,状态机仍然是 class,以便于调试。

使用这些生成的结构时,一个关键点是必须等待每一个 Task。
当异步方法中发生异常时,它会被状态机 MoveNext 方法内部由编译器生成的 try-catch 块捕获。

```csharp
using UsingAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
await turkey.Cook();

var gravy = new Gravy();
await gravy.Cook();

Console.WriteLine("Ready to eat");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/review-62197125/?t=87)

如果任务没有被等待,异常就会一直被困在状态机中,永远不会传播回调用代码。
为了确保异常能够被捕获并得到妥善处理,每个返回 task 的方法都必须被正确地等待。
这可以防止异常丢失,并让应用程序能够应对意外的失败。

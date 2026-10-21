# Parallel programming

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 2 章
> 共 5 课 · 约 58:20
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Parallel vs Asynchronous](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-vs-asynchronous-69955972/) | 1:18 | [↓](#1-parallel-vs-asynchronous) |
| 2 | [Introduction to Asynchronous Programming](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/) | 15:17 | [↓](#2-introduction-to-asynchronous-programming) |
| 3 | [Introduction to async / await](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/) | 14:15 | [↓](#3-introduction-to-async--await) |
| 4 | [Introduction to Parallel Programming](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/) | 4:14 | [↓](#4-introduction-to-parallel-programming) |
| 5 | [Deadlocks and Race Conditions](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/) | 23:16 | [↓](#5-deadlocks-and-race-conditions) |

---

## 1. Parallel vs Asynchronous

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-vs-asynchronous-69955972/) · 1:18

### 总结

本课介绍 C# 中异步编程与并行编程之间的区别。
它确立了本课程所需的先修知识,重点是基于任务的异步模式(Task-based Asynchronous Pattern,TAP)、async-await 状态机的运作机制,以及异步最佳实践的正确应用。

### 核心概念

* 异步编程与并行编程之间的区别。
* Task 和 Task<T> 在 C# 中的作用。
* 编译器层面对 async 和 await 代码的转换。
* 异步最佳实践以及 ConfigureAwait(false) 的使用。

### 课程笔记

本课通过把并行编程与异步编程区分开来,对并行编程做一个入门介绍。
虽然两者都是 C# 并发的核心,但它们针对的是不同的执行场景。

本课程的一个先修条件是扎实理解 C# 中的异步编程,特别是基于任务的异步模式(TAP)。
构成并行实现基础的关键主题包括:

* **Tasks**:由 Task 和 Task<T> 表示的基本工作单元。
* **Async-Await Mechanics(Async-Await 机制)**:C# 编译器如何修改代码来实现 async 和 await 状态机。
* **Best Practices(最佳实践)**:实现异步逻辑的标准模式。
* **ConfigureAwait**:使用 ConfigureAwait(false) 来管理任务调度器如何处理后续操作(continuation)。

---

## 2. Introduction to Asynchronous Programming

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/) · 15:17

### 总结

C# 中的异步编程以 Task 类为中心,它表示一个将在未来完成的操作。
本课介绍任务的基本概念,包括任务在后台线程上的执行、由 TaskStatus 枚举定义的生命周期状态,以及三种可能的完成结果:成功、因异常而出错(faulted),或通过令牌取消。
通过监视 IsCompleted 和 Status 这样的属性,开发者可以在不阻塞主执行线程的情况下跟踪后台操作的进度,确保应用在长时间运行的任务期间保持响应。

### 核心概念

*   `Task` 类作为异步执行代码的一种表示。
*   任务的结果:成功完成、Faulted(异常)或 Canceled。
*   `TaskStatus` 枚举以及任务生命周期中的八种不同状态。
*   使用 `Task.Run` 把工作卸载到 .NET 线程池。
*   使用 `IsCompleted`、`IsFaulted` 和 `IsCanceled` 等属性监视任务状态。
*   `CancellationTokenSource` 和 `CancellationToken` 在管理任务终止中的作用。

### 课程笔记

异步编程允许代码在后台线程上执行,防止主线程或 UI 线程在长时间运行的操作期间卡死。
在 C# 中,`Task` 本质上是对稍后会完成的工作的一个承诺。
任务一旦启动,就可以通过几个布尔属性来监视它的进度。

```csharp
var task = Task.Run(() => SimulateLongRunningFunction(CancellationToken.None));

Trace.WriteLine($"Is the Task completed? {task.IsCompleted}");
Trace.WriteLine($"Is the Task completed successfully? {task.IsCompletedSuccessfully}");
Trace.WriteLine($"Is the Task faulted? {task.IsFaulted}");
Trace.WriteLine($"Is the Task canceled? {task.IsCanceled}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=25)

一个已完成的任务总会得到三种结果之一:

1.  **Successful Completion(成功完成)**:任务完成了它的工作,没有出错,也没有被取消。
2.  **Faulted(出错)**:任务内部发生了未处理的异常。
3.  **Canceled(已取消)**:任务通过 `CancellationToken` 被提前停止。

#### 任务状态

虽然最终结果只有三种,但任务在其生命周期中可以经历八种不同的状态,这些状态由 `TaskStatus` 枚举定义。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=130)

关键状态包括:

*   **Created**:任务构造函数已经退出、但任务尚未被调度时的状态。
*   **WaitingToRun**:任务已经在 `TaskScheduler` 中排定,正在等待某个后台线程变为可用。
*   **WaitingForActivation**:任务正在等待一个它所依赖的操作(在使用 `ContinueWith` 或 `TaskCompletionSource` 时很常见)。
*   **Running**:任务内部的代码当前正在执行。

#### 监视任务执行

在一个典型场景中,`Task.Run` 被用来把一个函数卸载到后台线程。
由于主线程会立即继续执行,在创建任务几毫秒后检查其状态,往往会发现任务处于 `WaitingToRun` 状态,因为 .NET 运行时需要少量开销来找到并分配一个后台线程。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=385)

#### 处理出错与取消

如果任务内部抛出了异常,任务会立即停止,并把它的 `IsFaulted` 属性设为 `true`。
当这个任务最终被 await 时,该异常会被重新抛给开发者。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=610)

取消是通过 `CancellationTokenSource` 来管理的。
如果令牌被取消(例如设置一个 `TimeSpan.Zero` 的延迟),任务就应当转换到 `Canceled` 状态。
然而,如果任务本身没有用该令牌初始化,仅仅在方法内部抛出 `OperationCanceledException`,有时可能会得到 `Faulted` 状态。

为了确保 .NET 运行时正确地把任务识别为已取消,必须把 `CancellationToken` 直接传入 `Task.Run` 方法,同时也传入它所执行的函数。
这让运行时能够在任务开始之前或在其执行期间检查令牌的状态。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-asynchronous-programming-69955973/?t=850)

---

## 3. Introduction to async / await

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/) · 14:15

### 总结

本课介绍 C# 中的 async 和 await 关键字,解释它们如何把方法转换为状态机,从而在不阻塞线程的情况下处理异步操作。
通过从 Thread.Sleep 这样的阻塞调用转向 Task.Delay 这样的非阻塞替代方案,开发者可以防止线程池耗尽,并确保应用保持响应,尤其是在 UI 密集或高流量 API 的环境中。
讨论还进一步强调了这些关键字如何简化开发,让标准的 try-catch 块就能处理出错任务的异常,并演示了如何使用 CancellationToken 实现任务取消,以保持高效的资源管理。

### 核心概念

- async 和 await 关键字
- IAsyncStateMachine
- 阻塞调用与非阻塞调用
- 线程池饥饿(Thread Pool starvation)
- UI 线程的响应性
- 基于任务的异步模式(TAP)
- 线程池耗尽(Thread Pool Exhaustion)
- 非阻塞的 Await
- 异常传播
- 任务出错(Task Faulting)
- 异步取消

### 课程笔记

#### Async 和 Await 关键字

在 C# 中,`async` 和 `await` 是用来管理异步操作的主要关键字。
`await` 关键字让程序可以暂停一个方法的执行,直到某个特定任务完成。
`async` 关键字用在方法签名中,以便在该方法内部使用 `await`。

在底层,.NET 编译器会把标记为 `async` 的方法转换为一个 `IAsyncStateMachine`。
这个状态机管理执行流程,让线程在等待操作完成期间被释放回线程池,并在任务完成后恢复执行。

```csharp
async Task SimulateLongRunningFunction(CancellationToken token)
{
    Trace.WriteLine($"Started on Thread {Environment.CurrentManagedThreadId}");

    await Task.Delay(TimeSpan.FromSeconds(2), token);

    Trace.WriteLine($"Completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=55)

#### 阻塞调用的问题

使用 `Thread.Sleep` 被视为一种“锁定”调用,也就是阻塞调用。
当一个线程调用 `Thread.Sleep` 时,它会被锁定,在计时器到期之前无法执行任何其他工作。
在高规模的环境中,例如一个为成千上万用户提供服务的 Web API,这会导致效率低下。
即使在性能强大的服务器上,锁定线程也会让 .NET 线程池无法把这些线程用于其他传入的请求。

在下面的示例中,主线程(Thread 1)被锁定了三秒钟,在此期间系统无法使用它:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=115)

#### 转向非阻塞代码

为了优化代码,应当用 `await` 替换 `Thread.Sleep`。
当你 `await` 一个任务时,代码执行会暂停到这个特定任务完成为止,而不是等待一段随意设定的时间。
这消除了被浪费的 CPU 周期。
例如,如果一个任务在两秒内完成,而你用 `Thread.Sleep` 等了三秒,你就会白白消耗整整一秒不必要的 CPU 时间。

```csharp
// Replacing Thread.Sleep(3) with a non-blocking await
await task;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=220)

#### 非阻塞 Await 的好处

使用 `await` 带来两个主要好处:

1.  **Efficiency(效率)**:.NET 运行时恰好在任务完成时通知代码恢复执行,避免了不必要的延迟。
2.  **Responsiveness(响应性)**:调用线程不会被阻塞。这对基于 UI 的应用(桌面、移动或 Web)至关重要,在这些应用中 Thread 1 通常就是 UI 线程。如果 UI 线程被 `Thread.Sleep` 阻塞,应用就会卡死,无法与用户交互,也无法重绘屏幕。使用 `await` 可以释放 UI 线程,让它在后台任务运行期间保持响应。

作为最佳实践,使用 `async` 关键字的方法应当返回 `Task`(或 `Task<T>`)而不是 `void`,以避免与 `async void` 方法相关的危险。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=385)

#### 线程池效率与响应性

`Thread.Sleep` 是一个会锁定当前线程的阻塞调用,而 `await` 关键字与之不同,它是非阻塞的。
当程序遇到对某个任务(例如 `Task.Delay`)的 `await` 时,正在执行的线程会被释放。
如果它是 UI 线程,它会回到消息循环,让界面保持响应。
如果它是后台线程,它会回到线程池,供其他操作使用。

这一机制对于防止**线程池耗尽**至关重要。
在一个每秒接收数千次调用的高流量 API 中,使用阻塞调用最终会耗光池中所有可用的线程。
一旦线程池耗尽,服务器就无法处理新的请求。
通过使用 `async` 和 `await`,线程在 I/O 密集型或延迟操作期间被释放,从而让较少数量的线程就能处理大得多的并发工作量。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=445)

#### 使用 Await 处理异常

`await` 关键字的一大优势在于它处理异常的方式。
如果一个任务进入出错状态(即任务内部抛出了异常),`await` 关键字会自动在 await 所在的位置重新抛出该异常。

如果没有 `await`,开发者就需要手动检查 `Task.IsFaulted` 属性,并查看 `Task.Exception` 属性。
有了 `await`,你可以把调用包在一个标准的 `try-catch` 块中。
这确保了发生在后台线程上的异常能被正确地传播到调用上下文,防止应用悄无声息地失败,或者在没有日志的情况下崩溃。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=670)

#### 任务取消

异步任务应当遵从取消令牌,以便实现干净的关闭或由用户发起的取消。
通过把 `CancellationToken` 传给 `Task.Run` 以及 `Task.Delay` 这样的内部方法,.NET 运行时就能感知到取消请求。
如果令牌已经过期(例如用 `TimeSpan.Zero` 创建),任务会立即转换到 `Canceled` 状态,并在被 await 时抛出 `OperationCanceledException`。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-async-await-69955974/?t=835)

---

## 4. Introduction to Parallel Programming

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/) · 4:14

### 总结

.NET 中的并行编程是指并发地执行多个任务,通常借助后台线程来实现。
与顺序执行不同,任务一经发起就可以开始运行,使应用程序能够同时执行多个操作,例如 API 请求或长时间运行的计算。
随后使用 await 关键字来同步这些任务,确保它们在程序继续之前已经完成,并且任何异常都得到妥善处理。

### 核心概念

*   **Concurrency(并发)**:同时运行多个任务,通常位于不同的后台线程上。
*   **Task Scheduling(任务调度)**:.NET 运行时会在任务创建后立即自动调度它们在后台线程上运行。
*   **Immediate Execution(立即执行)**:任务在调用时(例如通过 `GetAsync` 或 `Task.Run`)就开始运行,而不是在被 await 时。
*   **Exception Propagation(异常传播)**:必须 await 任务,才能重新抛出后台执行期间发生的异常,从而得以优雅地处理错误。
*   **Thread Management(线程管理)**:.NET 任务调度器负责把任务分配给可用线程,这会带来少量的初始化开销。

### 课程笔记

并行编程建立在异步编程的基础之上,利用任务、线程以及 `async/await` 模式。
其核心在于,它允许两个或更多任务在后台线程上并发运行。

#### 并发 API 请求

并行编程的一个常见用例是同时发起多个网络请求。
在下面的示例中,使用一个 `HttpClient` 从网站获取博客文章和作者。

```csharp
var client = new HttpClient();

Task<HttpResponseMessage> getPostsTask = client.GetAsync(requestUri: "https://codetraveler.io/GetPosts");
Task<HttpResponseMessage> getAuthorsTask = client.GetAsync(requestUri: "https://codetraveler.io/GetAuthors");

await getPostsTask;
await getAuthorsTask;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/?t=25)

在 .NET 中,任务一经创建就会被调度,并开始在后台线程上运行。
在这个示例中,`getPostsTask` 和 `getAuthorsTask` 都在各自的 `GetAsync` 调用之后立即开始它们的网络操作。
随后使用 `await` 关键字来确保这两个任务在代码继续执行之前完成。
此外,`await` 还确保如果后台操作期间发生异常(例如网络连接断开),该异常会在 await 处被重新抛出,以便对其进行处理。

#### 任务执行与线程

使用 `Task.Run` 时,.NET 任务调度器会接收该任务并把它分配给一个线程。
虽然任务的初始化和调度存在少量开销,但在大多数情况下,任务在调用时就会立即开始运行。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/?t=145)

执行这段代码时,主程序在一个线程上启动(例如 Thread 1),而后台任务则由调度器分配到不同的线程上(例如 Thread 6 和 Thread 8)。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-parallel-programming-69955975/?t=230)

这展示了并行执行:多个操作在不同的线程上同时推进,最终通过 `await` 同步回主执行流程。

---

## 5. Deadlocks and Race Conditions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/) · 23:16

### 总结

死锁和竞态条件是并行编程中常见的难题,它们发生在多个线程与共享资源交互或依赖彼此完成的时候。
死锁源于循环依赖,线程会无限期地相互等待;而竞态条件则发生在对某个变量的并发访问导致不可预测结果的时候。
C# 提供了多种机制来缓解这些问题,包括用于同步代码的 lock 关键字、用于异步场景的 SemaphoreSlim、用于高性能原子操作的 Interlocked,以及用于线程安全单例初始化的 Lazy<T>。

### 核心概念

- **Deadlock(死锁)**:两个或多个线程被无限期阻塞、各自等待对方释放资源的情况。
- **Race Condition(竞态条件)**:一种异常情况,进程的输出意外地依赖于其他事件的顺序或时机,通常发生在多个线程修改共享状态的时候。
- **Locking(加锁)**:同步的互斥机制,防止多个线程同时进入同一个代码块。
- **SemaphoreSlim**:Semaphore 的轻量级替代方案,支持 async/await,允许线程在等待时返回线程池。
- **Interlocked**:一个为多个线程共享的变量提供原子操作的类,为特定值类型提供高性能的加锁方式。
- **Lazy<T>**:用于对对象进行线程安全的延迟初始化的包装器,常用于单例模式。

### 课程笔记

#### 死锁与循环依赖

当多个线程都在等待彼此完成才能继续时,就会发生死锁。
这通常是由循环引用造成的。
例如,如果 `backgroundTask1` 在 await `backgroundTask2` 完成,而 `backgroundTask2` 同时又在 await `backgroundTask1`,那么两者都永远无法完成。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=70)

```text
        await
backgroundTask1 ------> backgroundTask2
       ^                       |
       |         await         |
       +-----------------------+
```

为了避免死锁,开发者必须确保任务之间不存在相互等待对方来执行同一操作的循环依赖。

#### 竞态条件

当多个线程同时访问并修改同一段代码或同一个变量时,就会出现竞态条件。
在单线程环境下看似安全的代码,在并行执行时可能会出错。
在下面的示例中,一个循环快速地调用一个更新方法而不 await 其结果,从而让多个线程得以覆盖同一个共享字段。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=160)

当这段逻辑并行执行时,一个线程可能会在另一个线程设置了 `_result` 之后、但在那个线程执行校验检查之前,修改 `_result` 的值。

#### 同步加锁

`lock` 语句可以用来确保同一时间只有一个线程进入某个特定的代码块。
当一个线程遇到 lock 时,它会获取一个互斥对象。
如果另一个线程试图进入同一个 lock,它就会被阻塞,直到第一个线程退出。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=580)

标准的 `lock` 虽然有效,但它是一个阻塞调用。
在等待锁被释放期间,调用线程无法返回线程池。

#### 使用 SemaphoreSlim 进行异步加锁

对于异步代码,`SemaphoreSlim` 是首选机制。
用 `(1, 1)` 的计数初始化它,它就相当于一个异步锁。
如果信号量当前被另一个线程持有,`WaitAsync` 方法允许调用线程返回线程池,这使它比同步锁更高效。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=820)

#### 高性能原子操作

在处理 `int`、`long`、`uint` 和 `ulong` 等特定值类型时,`Interlocked` 类提供了性能最高的加锁方式。
它执行原子操作,没有线程切换或阻塞的开销。

```csharp
async Task SimulateLongRunningFunction(string name, int setNumber, CancellationToken token)
{
	Trace.WriteLine($"{name} started on Thread {Environment.CurrentManagedThreadId}");

	await Task.Delay(TimeSpan.FromMilliseconds(2));

	Interlocked.CompareExchange(ref number, setNumber, number);

	Trace.WriteLine($"{name} completed on Thread {Environment.CurrentManagedThreadId}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=1120)

在这个示例中,`CompareExchange` 会比较 `number` 在其内存地址上的当前值。
如果它与预期值相符,就以原子方式把它更新为 `setNumber`。

#### 使用 Lazy<T> 实现线程安全的单例

在实现单例模式时,如果两个线程同时尝试首次初始化实例,就可能出现竞态条件。
使用 `Lazy<T>` 可以确保初始化函数只运行一次,并且完全线程安全。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/deadlocks-and-race-conditions-69955976/?t=1330)

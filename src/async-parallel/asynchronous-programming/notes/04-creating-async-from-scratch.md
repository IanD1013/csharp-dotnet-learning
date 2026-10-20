# Creating Async from Scratch

> 课程:[From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · 第 4 章
> 共 6 课 · 约 46:16
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197135/) | 1:12 | [↓](#1-introduction) |
| 2 | [Run](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/) | 8:14 | [↓](#2-run) |
| 3 | [ContinueWith](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/) | 17:15 | [↓](#3-continuewith) |
| 4 | [Wait](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/) | 7:09 | [↓](#4-wait) |
| 5 | [Delay](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/) | 5:18 | [↓](#5-delay) |
| 6 | [Await](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/) | 7:08 | [↓](#6-await) |

---

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197135/) · 1:12

### 总结

本课介绍如何自行构建 Task 模式的自定义实现,即 DomeTrainTask。
通过手动构建 Run、Wait 和 ContinueWith 等核心异步原语,并实现 async-await 支持所需的机制,开发者可以亲手理解 .NET 运行时和 C# 编译器在底层是如何处理异步操作的。

### 核心概念

- **Custom Task Implementation**(自定义 Task 实现):创建一个 `DomeTrainTask` 类,复刻标准 .NET `Task` 类型的行为。
- **Core Async Primitives**(核心异步原语):实现用于线程池的 `Run`、用于阻塞同步的 `Wait`,以及用于任务链接的 `ContinueWith`。
- **Async-Await Integration**(Async-Await 集成):通过实现 Awaiter 模式(`GetAwaiter` 和 `INotifyCompletion`),让自定义类型支持 `await` 关键字。
- **Internal Mechanics**(内部机制):理解在一个类 task 对象内部,状态、异常和延续(continuation)是如何管理的。

### 课程笔记

虽然 .NET 运行时开箱即用地提供了健壮的 `Task` 实现,但从零构建一个 task 能揭示 C# 异步编程的底层机制。
这个实现 `DomeTrainTask` 作为一个可运行的模型,用来演示编译器生成的状态机如何与 task 对象交互。

#### DomeTrainTask 的结构

整个实现的核心是 `DomeTrainTask` 类。
它管理操作的状态,包括操作是否已完成、执行期间发生的任何异常,以及完成时要执行的延续动作。
为了确保访问这些共享状态时的线程安全,这里使用了一个 `Lock` 对象。

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

#### 启用 Async-Await 支持

为了让 `DomeTrainTask` 能够使用 `await` 关键字,这个类必须提供一个 `GetAwaiter()` 方法,返回一个实现了 Awaiter 模式的对象。
这包括 `INotifyCompletion` 接口,它提供编译器用来把延续挂接到 task 上的 `OnCompleted` 方法。

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

#### 实际用法

下面的示例演示了如何在一个标准 C# 程序中使用 `DomeTrainTask`:用 `Run` 把工作卸载到 ThreadPool,用 `Delay` 模拟异步等待,同时全程兼容 `await` 关键字。

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

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/) · 8:14

### 总结

本课演示如何从零构建一个自定义 task 实现,从 DomeTrainTask 类开始。
内容涵盖异步操作所需的内部状态管理,包括使用 C# 13 的 Lock 类实现线程安全、跟踪完成状态,以及捕获异常。
本课最后实现了一个静态 Run 方法,把工作卸载到 .NET 线程池,并通过比较线程 ID 来验证异步执行。

### 核心概念

* **Custom Task Implementation**(自定义 Task 实现):构建 `DomeTrainTask` 来模仿标准 `System.Threading.Tasks.Task` 的行为。
* **Thread Safety**(线程安全):使用 `System.Threading.Lock` 类(在 C# 13 / .NET 9 中引入),防止多个线程访问 task 状态时出现竞态条件。
* **State Management**(状态管理):通过一个布尔标志跟踪完成状态,并捕获后台执行期间抛出的异常,以便之后重新抛出。
* **Thread Pool Integration**(线程池集成):使用 `ThreadPool.QueueUserWorkItem` 在后台线程上执行动作。
* **Verification**(验证):使用托管线程 ID 来确认工作已成功推迟到后台线程上执行。

### 课程笔记

实现从一个基础的控制台应用程序开始,并使用之前示例中用来表示异步工作的共享 `Food` 基类。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=40)

为了创建自定义 task 实现,我们定义 `DomeTrainTask` 类。
这个类需要一些内部字段来管理其状态,包括一个确保线程安全的 `Lock` 对象、一个跟踪完成状态的布尔值,以及一个存储执行期间发生的任何错误的 `Exception` 字段。

```csharp
namespace CreatingTaskFromScratch;

public class DomeTrainTask
{
    private readonly Lock _lock = new();

    private bool _completed;
    private Exception? _exception;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=115)

`IsCompleted` 属性提供了一种公开的方式来检查 task 的状态。
由于多个线程可能同时检查或修改这个值,getter 被包裹在一个使用 `_lock` 字段的 `lock` 语句中。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=175)

为了把 task 转换到已完成状态,我们实现 `SetResult` 和 `SetException`。
这些方法同样必须是线程安全的。
如果 task 已经完成,再次尝试设置结果或异常就是一个无效操作,应当抛出异常。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=265)

`Run` 方法是一个静态入口点,允许用户传入一个要在后台线程上执行的 `Action`。
它使用 `ThreadPool.QueueUserWorkItem` 来调度工作。
在线程池的执行块内部,一个 `try-catch` 包装确保执行成功时调用 `SetResult`,而任何抛出的异常都会通过 `SetException` 捕获,以便在 task 最终被 await 时重新抛出。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=385)

```text
Main thread                          ThreadPool thread
-----------                          -----------------
DomeTrainTask.Run(action)
  task = new()
  ThreadPool.QueueUserWorkItem ----> try
  return task                          action()
                                       task.SetResult()
                                     catch (Exception e)
                                       task.SetException(e)
```

为了验证这个实现,我们可以比较主线程的 `ManagedThreadId` 与 `DomeTrainTask.Run` 动作内部的线程 ID。
由于这个自定义 task 还不支持 `await` 关键字,这里使用 `Console.ReadLine()` 来防止应用程序在后台线程完成之前退出。

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");

DomeTrainTask.Run(() => 
    Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}"));

Console.ReadLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/run-62197136/?t=445)

---

## 3. ContinueWith

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/) · 17:15

### 总结

本课讲解如何为自定义 task 类型实现延续机制,让代码在 task 完成后自动执行。
本课引入了 `ContinueWith` 方法,以及用于保留线程状态(例如安全和区域性设置)的 `ExecutionContext` 概念,并重构了 task 完成逻辑,以处理已存储的延续。

### 核心概念

- 实现用于任务链接的 `ContinueWith`。
- 捕获并恢复 `ExecutionContext`,以保留线程本地状态。
- 处理立即执行与延迟执行的延续。
- 把 task 完成逻辑重构为一个统一的 `CompleteTask` 方法。
- 基于回调的异步代码的局限性和可读性问题。

### 课程笔记

实现 `ContinueWith` 方法是为了让 `DomeTrainTask` 在完成后执行一个后续动作。
这个方法返回一个新的 `DomeTrainTask`,从而可以把异步操作链接起来。
在 `ContinueWith` 内部,代码首先检查当前 task 是否已经完成。
如果已经完成,延续动作会被立即排入 `ThreadPool`。
为了确保健壮性,这个动作被包裹在一个 try-catch 块中,以捕获任何异常并将其传播给延续 task。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=160)

为了支持对仍在运行的 task 设置延续,这个类必须存储延续的状态。
这需要两个新的私有字段:一个存储回调的 `Action`,以及一个 `ExecutionContext`。
`ExecutionContext` 至关重要,因为它捕获了逻辑线程状态,包括安全标识和区域性信息。
这可以防止 task 在另一个线程池线程上恢复时出现数据泄漏或上下文丢失。

```csharp
private Action? _action;
private ExecutionContext? _context;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=205)

如果调用 `ContinueWith` 时 task 尚未完成,该方法会捕获当前的 `ExecutionContext`,并把上下文和动作都存储起来以便之后执行。

```csharp
else
{
    _action = action;
    _context = ExecutionContext.Capture();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=325)

task 完成逻辑被重构为一个统一的 `CompleteTask` 方法。
这个方法负责设置 task 的完成状态、存储任何异常,并在注册了延续时触发它。
如果捕获了 `ExecutionContext`,就使用 `ExecutionContext.Run` 调用该动作,以确保环境被正确恢复。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=505)

```text
ContinueWith(action)
  |
  +-- _completed == true  --> ThreadPool.QueueUserWorkItem(action)
  |
  +-- _completed == false --> _action  = action
                              _context = ExecutionContext.Capture()
                                 |
                                 v
              SetResult() / SetException(e) --> CompleteTask(exception)
                                                  _completed = true
                                                  _exception = exception
                                                  _context is null
                                                    ? _action.Invoke()
                                                    : ExecutionContext.Run(...)
```

在控制台应用程序中,这个实现允许进行复杂的链接。
然而,正如下面的示例所示,使用多个嵌套的 `ContinueWith` 和 `Run` 调用会导致代码难以阅读且容易出错,因为执行顺序并不遵循源代码从上到下的视觉流程。
这凸显了对 `.Wait()` 这类更结构化的同步原语的需求。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/continuewith-62197137/?t=850)

---

## 4. Wait

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/) · 7:09

### 总结

本课讲解如何为自定义 Task 实现一个阻塞式的 Wait 方法。
通过使用 ManualResetEventSlim,task 可以阻塞调用线程,直到异步操作完成。
这个实现借助已有的 ContinueWith 逻辑来发出事件信号,并使用 ExceptionDispatchInfo 在重新抛出异常时保留堆栈跟踪。
关键在于,本课演示了如何通过确保阻塞等待发生在同步锁之外来避免死锁。

### 核心概念

*   **ManualResetEventSlim**:一个 .NET 原语,用于管理线程的等待行为,允许线程阻塞直到收到信号。
*   **Blocking Calls**(阻塞调用):在异步操作上实现同步等待。
*   **ExceptionDispatchInfo**:一个工具,用于重新抛出已捕获的异常,同时保留原始堆栈跟踪。
*   **Deadlock Prevention**(死锁预防):必须在临界区(锁)之外执行阻塞操作。
*   **Code Readability**(代码可读性):借助阻塞等待,从嵌套的延续过渡到线性、顺序的执行流程。

### 课程笔记

#### 实现 Wait 方法

`Wait` 方法是一个阻塞调用,在 task 完成之前阻止调用线程继续执行。
为了管理这种行为,实现中使用了 `ManualResetEventSlim`。
这个类提供一个阻塞当前线程的 `Wait` 方法,以及一个释放所有等待线程的 `Set` 方法。

最初,该方法检查 task 是否已经完成。
如果没有完成,就初始化一个 `ManualResetEventSlim`,并使用 task 的 `ContinueWith` 方法来调度 `Set` 调用。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/?t=75)

#### 异常处理与线程安全

当 task 以错误结束时,`Wait` 方法必须传播该异常。
仅仅使用 `throw _exception` 是不够的,因为它会覆盖堆栈跟踪,丢失错误最初发生位置的信息。
为了保留完整的堆栈跟踪,这里使用 `ExceptionDispatchInfo.Throw(_exception)`。

此外,由于 task 状态会被多个线程访问,这段逻辑必须是线程安全的。
对 `_completed` 的检查和 `ManualResetEventSlim` 的初始化被包裹在一个锁中。
然而,对 `resetEventSlim.Wait()` 的实际调用必须发生在锁**之外**。
如果线程在持有锁的同时等待,就会造成死锁,因为负责完成 task 的后台线程将无法进入锁来更新 task 状态或触发延续。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/?t=205)

```text
Calling thread                         Background thread
--------------                         -----------------
lock (_lock)
  if (!_completed)
    resetEventSlim = new()
    ContinueWith(() => resetEventSlim.Set())
(lock released)
resetEventSlim?.Wait()  ... blocked    CompleteTask: lock (_lock)
                                         _completed = true
         <------------------------------ resetEventSlim.Set()
if (_exception is not null)
  ExceptionDispatchInfo.Throw(_exception)
```

#### 为可读性而重构

实现 `Wait` 之后,可以对使用方代码进行大幅清理。
不再嵌套多个 `ContinueWith` 调用(这会导致"回调地狱"和难以阅读的代码),逻辑可以按顺序编写。
这让执行流程变得清晰:主线程启动,排入一个后台 task,等待它完成,然后继续执行下一行。

```csharp
// See https://aka.ms/new-console-template for more information

using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/wait-62197138/?t=280)

通过使用 `.Wait()`,程序不再需要 `Console.ReadLine()` 来防止主线程过早退出。
现在主线程会被明确地挂住,直到后台工作完成。

---

## 5. Delay

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/) · 5:18

### 总结

本课演示如何使用 .NET 的 Timer 类为自定义 task 实现一个静态 Delay 方法。
本课解释了通过回调触发 task 完成的机制,并讨论了用 .Wait() 阻塞线程与使用非阻塞的 await 关键字之间的关键架构差异。

### 核心概念

- 使用 `System.Threading.Timer` 实现异步延迟。
- 将 `Timer.Change` 与 `Timeout.InfiniteTimeSpan` 搭配使用,实现一次性执行。
- 理解 `.Wait()` 会阻塞线程的本质。
- 过渡到使用 `await` 关键字实现非阻塞的异步流程。

### 课程笔记

为了在自定义的 `DomeTrainTask` 类中实现 `Delay` 方法,我们初始化一个新的 task,并使用一个 `System.Threading.Timer` 来调度它的完成。
这个计时器被配置为执行一个回调,在 task 上调用 `SetResult()`,从而发出延迟时间已经过去的信号。

```csharp
public static DomeTrainTask Delay(TimeSpan delay)
{
	DomeTrainTask task = new();

	new Timer(_ => task.SetResult()).Change(delay, Timeout.InfiniteTimeSpan);

	return task;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=115)

`Timer.Change` 方法允许我们指定延迟时长。
通过把 `Timeout.InfiniteTimeSpan` 作为第二个参数传入,我们禁用了周期性触发,确保计时器只触发一次。
当计时器触发时,它会调用 `CompleteTask` 逻辑来处理 task 状态的转换以及所有已注册的延续。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=130)

在程序中使用这个延迟时,仅仅调用该方法是不够的,因为调用线程会在计时器被调度之后立即继续执行。
为了暂停执行,我们必须等待 task 完成。
一开始,可以使用 `.Wait()` 方法来做到这一点。

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();

DomeTrainTask.Delay(TimeSpan.FromSeconds(1)).Wait();

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Delay(TimeSpan.FromSeconds(1)).Wait();

DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}")).Wait();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=200)

虽然 `.Wait()` 可行,但它很危险,因为它会阻塞调用线程(例如主线程),使其无法做其他工作。
一种更地道、更高效的做法是使用 `await` 关键字。
这让代码更易读(从上到下顺序流动),并确保线程在等待 task 完成期间不被阻塞。

```csharp
using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

await DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId}"));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/delay-62197139/?t=295)

要支持 `await` 关键字,`DomeTrainTask` 类需要提供一个 `GetAwaiter` 方法,该方法返回一个实现了 `INotifyCompletion` 的对象。

---

## 6. Await

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/) · 7:08

### 总结

本课演示如何通过实现 awaiter 模式,为自定义 task 类型启用 async/await 支持。
通过创建一个实现 INotifyCompletion 的 DomeTrainTaskAwaiter 结构体,并在 DomeTrainTask 类上提供一个 GetAwaiter 方法,这个实现借助 C# 的鸭子类型(duck typing)与语言的异步状态机集成。
本课还探讨了为什么控制台应用程序在 await task 时的行为与 UI 应用程序不同:原因是缺少 SynchronizationContext,这使得执行无法自动回到最初的调用线程。

### 核心概念

* **Duck Typing**(鸭子类型):C# 编译器通过查找特定的方法签名(`GetAwaiter()`),而不是特定的接口,来判断一个类型是否可 await。
* **Awaiter Pattern**(Awaiter 模式):一个类型要可 await,必须返回一个对象(即"awaiter"),该对象拥有 `IsCompleted` 属性和 `GetResult()` 方法,并实现 `INotifyCompletion`。
* **INotifyCompletion**:这个接口要求实现 `OnCompleted` 方法,状态机在 task 尚未完成时用它来挂接延续。
* **SynchronizationContext**:一种让异步操作在最初调用线程上恢复的机制;控制台应用中没有它,导致延续在线程池线程上运行。

### 课程笔记

要让自定义 task 类型兼容 `await` 关键字,你必须实现一个 awaiter。
这通常通过创建一个配套的结构体来完成。
在这里,我们定义 `DomeTrainTaskAwaiter`,它必须实现 `INotifyCompletion` 接口。
awaiter 充当 task 与 .NET 异步状态机之间的桥梁。

最初,awaiter 需要一个接收其所 await 的 task 的 internal 构造函数,以及一个 `OnCompleted` 的实现。
`OnCompleted` 方法在 task 完成时被调用,它应当触发延续动作。

```csharp
public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
    private readonly DomeTrainTask _task;

    internal DomeTrainTaskAwaiter(DomeTrainTask task) => _task = task;

    public void OnCompleted(Action continuation) => _task.ContinueWith(continuation);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=115)

.NET 状态机还要求 awaiter 暴露一个 `IsCompleted` 属性。
这个属性让状态机能够检查 task 是否已经完成;如果已完成,状态机可以同步地继续执行,而不必让出。

```csharp
public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
    private readonly DomeTrainTask _task;

    internal DomeTrainTaskAwaiter(DomeTrainTask task) => _task = task;

    public bool IsCompleted => _task.IsCompleted;

    public void OnCompleted(Action continuation) => _task.ContinueWith(continuation);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=130)

最后,awaiter 必须提供一个 `GetResult()` 方法。
状态机在 task 完成后调用这个方法,以获取结果或传播执行期间发生的任何异常。
在我们的实现中,`GetResult()` 调用 task 的 `Wait()` 方法,以确保任何异常都被正确抛出。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=145)

定义好 awaiter 之后,`DomeTrainTask` 本身必须更新,加入一个 `GetAwaiter()` 方法。
这就是 C# 编译器遇到 `await` 关键字时要查找的那个"魔法"方法。

```csharp
public class DomeTrainTask
{
    public DomeTrainTaskAwaiter GetAwaiter() => new(this);

    // ... existing SetResult, SetException, and CompleteTask implementation
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=175)

```text
await task
  |
  v
task.GetAwaiter() --> DomeTrainTaskAwaiter : INotifyCompletion
                        IsCompleted       -> _task.IsCompleted
                        OnCompleted(...)  -> _task.ContinueWith(continuation)
                        GetResult()       -> _task.Wait()
```

这个模式依赖于"鸭子类型"。
`await` 关键字并不要求 task 实现特定接口或继承特定基类。
相反,编译器只是检查该类型是否有一个名为 `GetAwaiter()` 的方法,并且该方法返回一个实现了 `INotifyCompletion` 的类型。
如果满足这些条件,该类型就是可 await 的。

在控制台应用程序中运行这段代码时,你会注意到,虽然 `await` 关键字正确地暂停了执行直到 task 完成,但代码可能不会在最初的线程上恢复。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/await-62197140/?t=325)

在 UI 应用程序(例如 WinForms 或 WPF)中,`SynchronizationContext` 确保在 `await` 之后执行会回到 UI 线程,从而使应用程序保持响应。
然而,控制台应用程序没有 `SynchronizationContext`。
因此,一旦遇到 `await` 并且 task 完成,延续就会在任何可用的线程上运行(通常是一个 ThreadPool 线程),而不是回到最初的线程(Thread 1)。

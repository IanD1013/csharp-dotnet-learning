# Async/Await

> 课程:[From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · 第 2 章
> 共 4 课 · 约 24:33
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Life Before Async Await](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/) | 11:30 | [↓](#1-life-before-async-await) |
| 2 | [Using Async](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/) | 5:47 | [↓](#2-using-async) |
| 3 | [Parallel vs Async](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/) | 4:03 | [↓](#3-parallel-vs-async) |
| 4 | [What is Task? What is Thread?](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-is-task-what-is-thread-62197112/) | 3:13 | [↓](#4-what-is-task-what-is-thread) |

---

## 1. Life Before Async Await

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/) · 11:30

### 总结

本课探讨在 async 和 await 关键字引入之前,C# 异步编程所面临的挑战。
它演示了在没有适当同步的情况下调用异步方法,会导致主线程继续执行,常常造成应用程序过早终止。
本课介绍了 ContinueWith 方法,它是通过回调处理任务完成的一种历史做法,并展示了由此产生的被称为回调地狱(callback hell)的复杂性,以及手动管理后台任务时出现的不直观的执行流程。

### 核心概念

- **Asynchronous Execution Flow**:理解当一个 Task 在后台运行时,主线程如何继续执行。
- **Task-based Methods**:定义返回 Task 的方法,以表示一个正在进行的操作。
- **Callbacks with ContinueWith**:使用 TPL(Task Parallel Library)的方式,在任务完成后执行代码。
- **Process Lifetime**:管理控制台应用程序的生命周期,确保后台任务在进程退出前完成。
- **Callback Hell**:由深度嵌套的异步回调带来的可读性和维护性问题。

### 课程笔记

为了理解 C# 异步编程的演变,我们从一个表示不同食物的基础类层次结构开始。
`Food` 抽象类定义了一个 `Cook` 方法,它使用 `Task.Delay` 来模拟一个长时间运行的操作。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=70)

当尝试在一个标准的控制台应用程序中使用这段代码时,会出现一个常见的陷阱。
如果我们调用 `Cook` 方法却不 await 它,应用程序会立即继续执行下一行。

```csharp
using LifeBeforeAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
turkey.Cook();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=130)

在这种情况下,控制台会输出 "Cooking Started" 和 "Cooking Turkey",但程序会在火鸡烹饪完成之前终止。
之所以会这样,是因为当 `Cook` 方法执行到 `await Task.Delay` 这一行时,控制权被交还给调用方法(主程序),而主程序随后到达其执行块的末尾并关闭了进程。

#### 使用 ContinueWith 进行回调

在 `async` 和 `await` 关键字成为流程控制的标准做法之前,开发者使用的是 `ContinueWith` 方法。
它允许你传入一个 `Action`,该 `Action` 会在初始任务完成后执行。
为了防止控制台应用程序在这些后台任务运行期间关闭,我们可以使用 `Console.ReadLine()` 来让主线程保持存活。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=340)

#### 执行顺序与回调地狱

使用回调的异步代码并不会以线性的、从上到下的方式执行。
下面的执行轨迹展示了协调多个任务时执行步骤之间不直观的跳转:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/life-before-async-await-62197109/?t=610)

在这个流程中,Step 5(等待用户输入)发生在 Step 6(火鸡烹饪完成)之前。
这种模式被称为"回调地狱"(callback hell)。
它让代码难以阅读和维护,因为事件的逻辑顺序被分散在嵌套的 lambda 表达式中,而不是遵循语言标准的过程式流程。

---

## 2. Using Async

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/) · 5:47

### 总结

本课演示了如何从使用 ContinueWith 手动进行任务延续,过渡到 C# 中现代的 async/await 语法。
通过使用 await 关键字,开发者可以编写出保持顺序的、从上到下可读性的异步代码,读起来与同步代码类似。
本课解释了 await 如何暂停当前方法的执行,并在后台任务完成期间释放调用线程,让该线程可以先去做其他工作,然后再回来完成程序的执行。

### 核心概念

- **Async/Await Syntax**:在 C# 5 中引入,这些关键字让异步代码可以像同步代码一样编写和阅读。
- **Sequential Readability**:代码执行遵循从上到下、从左到右的流程,这降低了追踪回调所需的认知负担。
- **Non-blocking Execution**:`await` 关键字会释放调用线程(例如主线程),让它在被等待的任务于后台运行时去执行其他工作。
- **Task Completion and Resumption**:一旦后台任务完成,它会通知调用线程从 `await` 表达式所在的位置恢复执行。

### 课程笔记

在早期版本的 C# 中,异步编程在很大程度上依赖于使用 `ContinueWith` 方法的手动回调。
这种方式常常导致代码难以理解,因为逻辑被分散在不同的代码块中。
此外,开发者经常不得不使用像 `Console.ReadLine()` 这样的 hack,来防止控制台应用程序在后台任务完成之前终止。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/?t=25)

有了 `async` 和 `await` 关键字,这种复杂性就消除了。
代码变得直观得多,因为它可以按顺序阅读。
程序在主线程上启动,打印开始消息,并初始化第一个任务。
当执行到 `await` 关键字时,`Cook` 方法在后台运行,而主线程被释放出来,去处理其他可能的任务。
一旦任务完成,它会回到调用线程,准确地从上次离开的地方继续执行。

```csharp
using UsingAsync;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
await turkey.Cook();

var gravy = new Gravy();
await gravy.Cook();

Console.WriteLine("Ready to eat");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/?t=90)

这些食物的底层实现依赖于一个抽象的 `Food` 类,它使用 `Task.Delay` 来异步地模拟烹饪所花费的时间:

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

通过使用 `await`,程序流程遵循清晰的、一步接一步的推进。
这避免了与"跳跃式"代码执行相关的 bug,并确保在等待 I/O 密集型或长时间运行的操作完成时,资源不会被锁住。
最终的执行流程清楚地记录了整个过程从开始到结束的每一个阶段。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/using-async-62197110/?t=220)

---

## 3. Parallel vs Async

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/) · 4:03

### 总结

本课演示了如何使用 Task.WhenAll,从顺序执行的异步操作过渡到并行执行。
通过同时启动多个任务,而不是逐个 await 它们,只要这些任务彼此独立,开发者就可以显著缩短一个工作流的总执行时间。
本课还着重介绍了并行任务在后台线程上的概念性流程,并警告了当任务之间存在循环依赖时可能出现的死锁。

### 核心概念

- **Sequential Async**:逐个 await 任务,这会暂停方法的执行,直到每个特定任务完成。
- **Parallel Programming**:并发执行多个任务,以更高效地利用资源并减少总等待时间。
- **Task.WhenAll**:一个接受多个任务并返回单个任务的方法,返回的任务只有在所有传入的任务都完成后才会完成。
- **Background Threads**:在底层让多个异步操作能够同时推进的机制。
- **Deadlocks**:两个或更多任务互相等待对方完成,导致程序无法继续的一种状态。

### 课程笔记

在标准的异步实现中,任务常常是按顺序 await 的。
虽然这避免了阻塞主线程,但它仍然迫使任务彼此等待。
例如,在一个烹饪模拟中,先初始化并 await 火鸡,然后才开始做肉汁,会造成不必要的空闲时间。

```csharp
using ParallelProgramming;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
await turkey.Cook();

var gravy = new Gravy();
await gravy.Cook();

Console.WriteLine("Ready to eat");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/?t=10)

为了优化这一点,我们可以使用并行编程。
我们不再逐个 await 每个任务,而是先初始化这些对象,然后使用 `Task.WhenAll`。
这个方法允许我们传入多个将并行执行的任务。

```csharp
using ParallelProgramming;

Console.WriteLine("Cooking Started");

var turkey = new Turkey();
var gravy = new Gravy();

await Task.WhenAll(turkey.Cook(), gravy.Cook());

Console.WriteLine("Ready to eat");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/?t=70)

运行这段代码时,火鸡和肉汁都会立即开始烹饪。
由于肉汁的烹饪时间(1 秒)比火鸡(5 秒)短,肉汁会先完成。
不过,`await Task.WhenAll` 这一行确保了程序在两个任务都完成之前,不会继续执行到最后的 "Ready to eat" 那一行。

#### 执行流程

执行过程可以拆分为几个不同的步骤。
虽然代码是从上到下阅读的,但其异步特性允许后台操作并发进行:

1.  **Step 1**:初始化 `Turkey` 对象。
2.  **Step 2**:初始化 `Gravy` 对象。
3.  **Step 3**:调用 `Task.WhenAll`。
    这会在后台线程上同时触发 **Step 3a**(火鸡烹饪)和 **Step 3b**(肉汁烹饪)。
4.  **Step 4**:一旦两个后台任务都完成,程序就继续执行到最后的输出。

```csharp
Step 1 var turkey = new Turkey();
Step 2 var gravy = new Gravy();

                          Step 3a          Step 3b
Step 3 await Task.WhenAll(turkey.Cook(), gravy.Cook());

Step 4 Console.WriteLine("Ready to eat");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/parallel-vs-async-62197111/?t=175)

#### 并行的风险

虽然并行编程很强大,但它引入了死锁的风险。
如果任务之间变得相互依赖,就会发生死锁。
例如,如果 `turkey.Cook()` 被修改为等待 `gravy.Cook()` 完成,而 `gravy.Cook()` 又被修改为等待 `turkey.Cook()` 完成,那么两者都永远无法完成。
程序会无限期地卡在 `await Task.WhenAll` 这一行。
开发者必须确保并行执行的任务是彼此独立的,或者经过了正确的同步,以避免这类 bug。

---

## 4. What is Task? What is Thread?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-is-task-what-is-thread-62197112/) · 3:13

### 总结

在 .NET 中,Task 表示一个异步操作,并作为 System.Threading.Thread 类之上的一个高层抽象。
Thread 是一个拥有自身状态和元数据的独立执行单元,而 Task 让开发者可以专注于操作本身的逻辑,无需手动管理线程池分配或上下文切换。
理解这两者之间的关系,是掌握 C# 异步编程的基础,因为 .NET 运行时会处理线程管理和执行上下文的底层复杂性。

### 核心概念

- **Task**:表示一个异步操作的抽象,位于 `System.Threading.Tasks` 命名空间中。
- **Thread**:一个独立的执行单元,像一个拥有自己一组指令的迷你程序一样运作。
- **Execution Context**:一种包含安全权限、访问级别和调用方信息的数据结构,用于实现线程间通信和状态管理。
- **Thread Metadata**:包括 Thread ID、状态(运行中/存活)、优先级和区域性(culture)设置在内的属性。

### 课程笔记

在 C# 中,`Task` 是一个表示异步操作的对象。
它位于 `System.Threading.Tasks` 命名空间中,充当 `System.Threading.Thread` 类之上的一个抽象。
使用 `Task` 并不一定会创建一个新线程;它也不保证该操作会始终停留在同一个线程上,或在调用线程上运行。
这种抽象的主要好处在于,它让 .NET 运行时可以自动管理线程池资源和上下文切换。

```csharp
public async Task Cook()
{
    Console.WriteLine($"Cooking {Name}");
    await Task.Delay(_cookTime);
    Console.WriteLine($"{Name} Completed");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-is-task-what-is-thread-62197112/?t=10)

`Thread` 是一个独立的执行单元。
可以把它看作一个迷你程序:一个标准程序被加载到内存中并由 CPU 逐行执行,而一个线程只包含它自身执行路径所需的那些特定步骤。

线程维护着大量由运行时管理的内部信息,包括:

- **Thread ID and State**:表示线程当前是正在运行、已完成还是仍然存活的指示信息。
- **Priority**:分配给该线程的执行重要程度。
- **Current Culture**:用于语言和方言检测的设置,让系统可以自动切换屏幕上显示的语言。
- **Background Status**:该线程是否被指定为后台线程。
- **Execution Context**:这个上下文包含安全权限、访问级别以及调用方身份等关键信息。
  它是让 .NET 运行时能够在线程之间切换的机制,并在执行期间促进不同线程之间的通信。

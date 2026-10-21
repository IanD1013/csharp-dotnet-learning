# The Parallel class

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 4 章
> 共 5 课 · 约 51:34
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Parallel.Invoke](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/) | 17:14 | [↓](#1-parallelinvoke) |
| 2 | [Parallel.For](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/) | 8:27 | [↓](#2-parallelfor) |
| 3 | [Parallel.ForAsync](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/) | 8:24 | [↓](#3-parallelforasync) |
| 4 | [Parallel.ForEach](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/) | 11:51 | [↓](#4-parallelforeach) |
| 5 | [Parallel.ForEachAsync](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/) | 5:38 | [↓](#5-parallelforeachasync) |

---

## 1. Parallel.Invoke

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/) · 17:14

### 总结

Parallel.Invoke 是 System.Threading.Tasks.Parallel 类中的一个静态方法,可以并发执行多个 Action 委托。
它是一个同步的阻塞操作,也就是说调用线程会被挂住,直到所有给定的 action 都执行完毕。
当需要把多个相互独立、返回 void 的操作交给线程池执行,同时又想通过 ParallelOptions 对并发度和取消进行细粒度控制时,这个方法尤其有用。
与 Task.WaitAll 相比,Parallel.Invoke 通过任务内联(task inlining)等优化提供了更好的性能,并且会自动解包 AggregateException、抛出具体的底层异常,从而简化了错误处理。

### 核心概念

- **并行执行 Action**:并发运行多个 `Action` 委托。
- **阻塞行为**:调用线程会在调用处被阻塞,直到所有 action 完成;它不是一个可 await 的异步调用。
- **ParallelOptions**:通过 `CancellationToken` 和 `MaxDegreeOfParallelism` 配置执行方式。
- **MaxDegreeOfParallelism**:限制并发操作的数量,以管理 CPU 和线程池资源。
- **性能优化**:包括任务内联,以及在只有单个 action 时减少开销。
- **异常解包**:自动处理 `AggregateException`,把主要异常暴露出来。

### 课程笔记

`Parallel.Invoke` 是一个用于同时执行多个操作的静态方法。
一个常见的用例是执行多个相互独立的 I/O 操作,例如并行写入不同的文件流。

```csharp
using var dentalRecordsFileStream =
    new FileStream(dentalRecordsPath, FileMode.Create, FileAccess.Write);

using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

using var visionRecordsFileStream =
    new FileStream(visionRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

Parallel.Invoke(options,
    () => dentalRecordsFileStream.Write(dentalRecords),
    () => medicalRecordsFileStream.Write(medicalRecords),
    () => visionRecordsFileStream.Write(visionRecords));

// Alternatively, use Action[]
Action[] actions =
[
    () => dentalRecordsFileStream.Write(dentalRecords),
    () => medicalRecordsFileStream.Write(medicalRecords),
    () => visionRecordsFileStream.Write(visionRecords)
];

Parallel.Invoke(options, actions);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=40)

#### 阻塞与 UI 线程

关键是要理解 `Parallel.Invoke` 是一个**阻塞调用**。
与异步方法不同,它不返回 `Task`,也不能被 await。
当某个线程调用 `Parallel.Invoke` 时,它会停在那一行代码上,直到所有后台任务完成。
如果这发生在 UI 线程(主线程)上,那么在这些操作执行期间,应用程序将无法响应用户输入,例如滚动或按钮点击。

#### 实现示例

为了演示并行执行,我们可以使用一个 `Food` 类,其中的 `Cook` 方法使用 `Thread.Sleep` 来模拟阻塞型工作负载。
这很适合 `Parallel.Invoke`,因为该方法本身就是为同步 action 设计的。

```csharp
using System.Diagnostics;

namespace ParallelInvoke;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public void Cook()
	{
		Trace.WriteLine($"Cooking {Name}");
		Thread.Sleep(_cookTime);
		Trace.WriteLine($"{Name} Completed");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=475)

在主执行逻辑中,我们把调用包在一个 `try-catch` 块里,以处理 `OperationCanceledException`。
通过设置 `MaxDegreeOfParallelism`,我们可以控制同时烹饪多少道菜。
例如,如果设为 2,即使请求了四道菜,也只会有两道同时烹饪。
这可以防止线程池耗尽和 CPU 过载。

```csharp
using System.Diagnostics;
using ParallelInvoke;

Trace.WriteLine("Cooking Started");

var turkey = new Turkey();
var gravy = new Gravy();
var mashedPotatoes = new MashedPotatoes();
var stuffing = new Stuffing();

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
	CancellationToken = cts.Token,
	MaxDegreeOfParallelism = 2
};

try
{
	Parallel.Invoke(options, () => turkey.Cook(), () => gravy.Cook(), () => mashedPotatoes.Cook(), () => stuffing.Cook());
}
catch (OperationCanceledException)
{
	Trace.WriteLine("ERROR: Cooking took too long");
}

Trace.WriteLine("Cooking Complete");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=565)

#### Parallel.Invoke 与 Task.WaitAll 的对比

出于以下几个原因,`Parallel.Invoke` 通常比 `Task.WaitAll` 更受青睐:

1.  **性能**:如果能节省资源,它包含在调用线程上"内联"运行 action 的逻辑;如果只提供了一个 action,它还会避免创建任务的开销。
2.  **异常处理**:`Task.WaitAll` 会把异常包装在 `AggregateException` 中。要捕获取消这类特定错误,你需要检查 `InnerExceptions` 集合。`Parallel.Invoke` 通过解包这个聚合异常并抛出实际遇到的异常,简化了这一点。

```csharp
try
{
    Task.WaitAll(tasks);
}
catch (AggregateException aggregateException) when (aggregateException.InnerExceptions.OfType<OperationCanceledException>().Any())
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=925)

#### 内部实现

在底层,`Parallel.Invoke` 会验证输入、为这些 action 创建一份防御性副本,并最终使用 `Task.WaitAll`。
不过,它只会在应用了各种性能优化、并确保在工作开始前检查过取消令牌之后才这样做。

```csharp
public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
{
    ArgumentNullException.ThrowIfNull(parallelOptions);
    ArgumentNullException.ThrowIfNull(actions);

    parallelOptions.CancellationToken.ThrowIfCancellationRequested();

    // ... validation and defensive copy ...

    try
    {
        Task.WaitAll(tasks);
    }
    catch (AggregateException aggExp)
    {
        ThrowSingleCancellationExceptionOrOtherException(aggExp.InnerExceptions, parallelOptions);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-invoke-69955981/?t=980)

---

## 2. Parallel.For

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/) · 8:27

### 总结

Parallel.For 是 Task Parallel Library 中的一个阻塞方法,它把各次迭代分配到后台线程上,从而并行执行一个 for 循环。
它通过 ParallelOptions 进行配置,以管理并发上限和取消。
由于它会阻塞调用线程直到所有迭代都完成,如果不额外做异步包装,它不适合在 UI 线程上使用。
该方法返回一个 ParallelLoopResult,提供关于循环完成状态和进度的信息。

### 核心概念

- **阻塞调用**:在整个循环结束之前,该方法不会返回。
- **基于索引的 Action**:循环体是一个 Action<int>,它接收当前迭代的索引。
- **ParallelOptions**:用于设置 MaxDegreeOfParallelism 并提供 CancellationToken。
- **循环按顺序执行**:多个 Parallel.For 调用会一个接一个地执行,但每个调用内部的迭代是并行运行的。
- **ParallelLoopResult**:返回状态信息,包括 IsCompleted 和 LowestBreakIteration。

### 课程笔记

Parallel.For 提供了一种执行循环的方式,其中每次迭代都可以并行运行。
它是一个阻塞调用,也就是说,发起调用的线程会一直等待,直到循环的所有迭代都完成。
因此,如果操作耗时较长,它就不适合在 UI 线程上使用,因为它会让界面卡死。

在一个处理病历的场景中,可以用 ParallelOptions 配置 Parallel.For,以限制并发操作的数量。

```csharp
using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

Parallel.For(
    0,
    100,
    options,
    (int patientNumber) =>
        medicalRecordsFileStream.Write(GetRecords(patientNumber))
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=10)

该方法接收一个起始索引(包含)、一个结束索引(不包含)、选项,以及一个以整数形式接收当前索引的 action。
例如,如果循环从 0 开始到 100,而当前迭代是第 50 次,那么传给 action 的索引就是 50。

#### 实现示例:并行烹饪

为了在更复杂的场景中演示 Parallel.For,设想一个烹饪应用。
准备工作包括定义各种食物以及一个取消令牌源。

```csharp
using System.Diagnostics;
using ParallelFor;

const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

var turkey = new Turkey();
var mashedPotatoes = new MashedPotatoes();
var gravy = new Gravy();
var stuffing = new Stuffing();

var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.FromSeconds(10));

var options = new ParallelOptions
{
    CancellationToken = cancellationTokenSource.Token,
    MaxDegreeOfParallelism = 3
};

Trace.WriteLine("Cooking Started");

try
{
}
catch (OperationCanceledException)
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=115)

执行循环时,可以调整 MaxDegreeOfParallelism 来控制同时使用多少个线程。
action 委托必须接收一个表示当前迭代索引的整数。

```csharp
try
{
    Parallel.For(1, numOrdersTurkey, options, int orderNumber => turkey.Cook(orderNumber));
}
catch (OperationCanceledException)
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=160)

Food 基类及其派生类使用 Thread.Sleep 来处理模拟的烹饪时间。
Cook 方法会打印当前的订单号和线程 ID,以展示并行执行。

```csharp
using System.Diagnostics;

namespace ParallelFor;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public void Cook(int orderNumber = 0)
	{
		Trace.WriteLine($"Cooking {Name} for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
		Thread.Sleep(_cookTime);
		Trace.WriteLine($"{Name} Completed for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=175)

#### 并行循环的顺序执行

由于 Parallel.For 是一个阻塞调用,如果把多个 Parallel.For 调用一个接一个地放置,它们彼此之间会按顺序执行。
在下面的示例中,所有火鸡订单都必须完成,土豆泥才会开始烹饪。

```csharp
try
{
    Parallel.For(1, numOrdersTurkey, options, body: (int orderNumber) => turkey.Cook(orderNumber));
    Parallel.For(1, numOrdersMashedPotatoes, options, body: (int orderNumber) => mashedPotatoes.Cook(orderNumber));
    Parallel.For(1, numOrdersGravy, options, body: (int orderNumber) => gravy.Cook(orderNumber));
    Parallel.For(1, numOrdersStuffing, options, body: (int orderNumber) => stuffing.Cook(orderNumber));
}
catch (OperationCanceledException)
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=205)

#### 处理结果

Parallel.For 返回一个 ParallelLoopResult 对象。
它可以用来验证循环是否完成了所有迭代,或者在循环被中断时找出 LowestBreakIteration。
如果 CancellationToken 被触发,循环会停止并抛出 OperationCanceledException。

```csharp
try
{
	var turkeyResult = Parallel.For(0, numOrdersTurkey, options, orderNumber => turkey.Cook(orderNumber));
	var mashedPotatoesResult = Parallel.For(0, numOrdersMashedPotatoes, options, orderNumber => mashedPotatoes.Cook(orderNumber));
	var gravyResult = Parallel.For(0, numOrdersGravy, options, orderNumber => gravy.Cook(orderNumber));
	var stuffingResult = Parallel.For(0, numOrdersStuffing, options, orderNumber => stuffing.Cook(orderNumber));

	if (turkeyResult.IsCompleted && mashedPotatoesResult.IsCompleted && gravyResult.IsCompleted && stuffingResult.IsCompleted)
		Trace.WriteLine("All Meals Complete");
	else
		Trace.WriteLine("Cooking Failed");
}
catch (OperationCanceledException)
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-for-69955982/?t=430)

---

## 3. Parallel.ForAsync

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/) · 8:24

### 总结

`Parallel.ForAsync` 是 `Parallel.For` 循环的异步版本,可以实现非阻塞的并行执行。
它返回一个 `Task`,因此在后台操作进行期间,调用线程仍然可用,这使它特别适合受 UI 约束或高吞吐量的应用程序。
与其同步版本相比,它支持异步委托,并能更好地与 `CancellationToken` 集成,从而优雅地取消任务。

### 核心概念

- **非阻塞执行**:返回一个可以 await 的 `Task`,把调用线程释放回线程池或 UI 消息循环。
- **异步循环体**:支持 `Func<T, CancellationToken, ValueTask>`,从而可以在循环体内使用 `await`。
- **取消传播**:把 `CancellationToken` 直接传入每次迭代的委托,让异步操作可以优雅地失败。
- **线程安全**:通过避免在主线程上进行阻塞调用,防止桌面或移动应用程序的 UI 卡死。
- **任务组合**:可以同时发起多个 `Parallel.ForAsync` 操作,并使用 `Task.WhenAll` 统一等待它们。

### 课程笔记

`Parallel.ForAsync` 在功能上与 `Parallel.For` 类似,但它是为异步工作流设计的。
它使用 `ParallelOptions` 类来配置 `CancellationToken` 和 `MaxDegreeOfParallelism`。
`Parallel.ForAsync` 的主要优势在于它是非阻塞的。
使用 `await` 关键字时,调用线程会归还给系统,而后台操作则在并行度的控制下执行。

在下面的示例中,`Parallel.ForAsync` 用于异步写入文件流。
调用线程遇到 `await` 关键字后立即被释放,而 101 个后台操作(最多 10 个同时运行)负责完成这些工作。

```csharp
using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

await Parallel.ForAsync(
    0,
    100,
    options,
    (int patientNumber, CancellationToken token) =>
        medicalRecordsFileStream.WriteAsync(GetRecords(patientNumber), token)
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=10)

`Parallel.ForAsync` 的一个重要区别在于它处理取消的方式。
循环体接收一个 `CancellationToken`,它与通过 `ParallelOptions` 传入的是同一个令牌。
这样就可以把令牌继续传递给异步方法(例如 `WriteAsync`),让它们能够优雅地处理取消,而不是被突然终止。

为了在更复杂的场景中演示这一点,设想一个烹饪应用程序。
`Food` 基类及其派生类定义了异步的 `Cook` 方法,使用 `Task.Delay` 模拟工作。

```csharp
using System.Diagnostics;

namespace ParallelForAsync;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task Cook(int orderNumber, CancellationToken token)
	{
		Trace.WriteLine($"Cooking {Name} for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
		await Task.Delay(_cookTime, token);
		Trace.WriteLine($"{Name} Completed for Order Number {orderNumber} on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=160)

执行多个并行循环时,依次 await 它们可以确保一组任务完成后下一组才开始。
在下面的实现中,火鸡必须先烹饪完成,土豆泥才会开始,因为每个 `Parallel.ForAsync` 调用都是单独 await 的。

```csharp
using ParallelForAsync;

const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

var turkey = new Turkey();
var gravy = new Gravy();
var stuffing = new Stuffing();
var mashedPotatoes = new MashedPotatoes();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var options = new ParallelOptions
{
    CancellationToken = cancellationTokenSource.Token,
    MaxDegreeOfParallelism = 40
};

Trace.WriteLine("Cooking Started");

try
{
    await Parallel.ForAsync(1, numOrdersTurkey, options, async (int orderNumber, CancellationToken token) => await turkey.Cook(orderNumber, token));
    await Parallel.ForAsync(1, numOrdersMashedPotatoes, options, async (int orderNumber, CancellationToken token) => await mashedPotatoes.Cook(orderNumber, token));
    await Parallel.ForAsync(1, numOrdersGravy, options, async (int orderNumber, CancellationToken token) => await gravy.Cook(orderNumber, token));
    await Parallel.ForAsync(1, numOrdersStuffing, options, async (int orderNumber, CancellationToken token) => await stuffing.Cook(orderNumber, token));

    Trace.WriteLine("All Meals Complete");
}
catch
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=250)

要让这些并行循环同时运行,可以捕获每个 `Parallel.ForAsync` 调用返回的 `Task`,而不立即 await 它。
通过使用 `Task.WhenAll`,所有并行循环会一次性全部启动,应用程序则等待整组操作完成。
这把 `Parallel` 类的能力与标准的异步任务组合结合了起来。

```csharp
using System.Diagnostics;
using ParallelForAsync;

const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

var turkey = new Turkey();
var gravy = new Gravy();
var stuffing = new Stuffing();
var mashedPotatoes = new MashedPotatoes();

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var options = new ParallelOptions { CancellationToken = cancellationTokenSource.Token, MaxDegreeOfParallelism = 40 };

Trace.WriteLine("Cooking Started");

try
{
	var cookTurkeysTask = Parallel.ForAsync(0, numOrdersTurkey, options, async (orderNumber, token) => await turkey.Cook(orderNumber, token));
	var cookGravyTask = Parallel.ForAsync(0, numOrdersGravy, options, async (orderNumber, token) => await gravy.Cook(orderNumber, token));
	var cookStuffingTask = Parallel.ForAsync(0, numOrdersStuffing, options, async (orderNumber, token) => await stuffing.Cook(orderNumber, token));
	var cookMashedPotatoesTask = Parallel.ForAsync(0, numOrdersMashedPotatoes, options, async (orderNumber, token) => await mashedPotatoes.Cook(orderNumber, token));

	await Task.WhenAll(cookTurkeysTask, cookMashedPotatoesTask, cookGravyTask, cookStuffingTask);

	Trace.WriteLine("All Meals Complete");
}
catch
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-forasync-69955983/?t=475)

---

## 4. Parallel.ForEach

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/) · 11:51

### 总结

Parallel.ForEach 是标准 foreach 循环的并行替代方案,用于遍历 IEnumerable<T> 或任何可遍历的集合。
Parallel.For 需要显式指定起始和结束索引,而 Parallel.ForEach 则不同,它会自动对传入的集合进行分区,并为每个元素执行指定的操作。
它是一个阻塞操作,也就是说,调用线程会被一直占住,直到所有迭代都完成。
该方法提供一个委托,通过它可以访问当前项、迭代索引以及一个 ParallelLoopState 对象,该对象可以对循环的执行进行细粒度控制,包括中断或停止处理的能力。

### 核心概念

- 对 `IEnumerable<T>` 集合进行并行迭代。
- 对调用线程的阻塞行为。
- 访问当前项、迭代索引以及 `ParallelLoopState`。
- 使用 `state.Break()` 和 `state.Stop()` 控制循环。
- 通过 `ParallelLoopResult` 监控执行状态。
- 在循环体内处理线程安全的操作。

### 课程笔记

`Parallel.ForEach` 可以对一个集合并行执行循环,与标准 `foreach` 循环的运作方式类似。
它不是指定起始索引和结束索引,而是把一个列表或一个 `IEnumerable` 传给该方法。
随后 `Parallel.ForEach` 会根据列表中的项,多次执行所提供的操作。

```csharp
List<Patient> patientList = GetPatients();

using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

Parallel.ForEach(
    patientList,
    options,
    (Patient patient, ParallelLoopState state, long index) =>
        medicalRecordsFileStream.Write(patient.MedicalRecords));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=10)

#### 阻塞行为

`Parallel.ForEach` 是一个阻塞调用。
如果 UI 线程或任何调用线程执行到一个 `Parallel.ForEach` 代码块,该线程就会被冻结并被一直占住,直到整个列表都被并行遍历完毕。
只有在并行工作完成之后,执行才会继续。

#### 实现与准备

为了演示 `Parallel.ForEach`,我们可以使用 `Enumerable.Range` 来初始化集合。
这是一种简写方式,用来创建一个 `IEnumerable` 并通过选择其中的项来实例化对象。

```csharp
const int numOrdersTurkey = 10;
const int numOrdersMashedPotatoes = 50;
const int numOrdersGravy = 50;
const int numOrdersStuffing = 20;

ParallelLoopResult? turkeyResult = null, mashedPotatoesResult = null, gravyResult = null, stuffingResult = null;

var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var options = new ParallelOptions { CancellationToken = cancellationTokenSource.Token, MaxDegreeOfParallelism = 40 };

List<Turkey> turkeyOrders = 
    Enumerable.Range(1, numOrdersTurkey).Select(static _ => new Turkey()).ToList();

List<MashedPotatoes> mashedPotatoesOrders = 
    Enumerable.Range(1, numOrdersMashedPotatoes).Select(static _ => new MashedPotatoes()).ToList();

List<Gravy> gravyOrders = 
    Enumerable.Range(1, numOrdersGravy).Select(static _ => new Gravy()).ToList();

List<Stuffing> stuffingOrders = 
    Enumerable.Range(1, numOrdersStuffing).Select(static _ => new Stuffing()).ToList();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=130)

循环体会接收当前项、`ParallelLoopState` 以及当前索引。
在这个示例中,`Cook` 方法是同步调用的,因为 `Parallel.ForEach` 是一个阻塞调用,并且在这个特定的重载中并不原生支持 `async/await`。

```csharp
try
{
    turkeyResult = Parallel.ForEach(turkeyOrders, options, body: (Turkey turkey, ParallelLoopState state, long index) => turkey.Cook(index));
    mashedPotatoesResult = Parallel.ForEach(mashedPotatoesOrders, options, body: (MashedPotatoes mashedPotatoes, ParallelLoopState state, long index) => mashedPotatoes.Cook(index));
    gravyResult = Parallel.ForEach(gravyOrders, options, body: (Gravy gravy, ParallelLoopState state, long index) => gravy.Cook(index));
    stuffingResult = Parallel.ForEach(stuffingOrders, options, body: (Stuffing stuffing, ParallelLoopState state, long index) => stuffing.Cook(index));
}
catch
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=265)

#### 控制循环状态

`ParallelLoopState` 对象提供了两个用于中断循环的方法:`Break()` 和 `Stop()`。

- **Break**:告诉循环在系统最早方便的时候,停止执行当前迭代之后的迭代。索引低于中断索引的迭代最终仍会被完成。
- **Stop**:告诉循环立即停止所有执行,即使其他迭代当前正在运行。

```csharp
turkeyResult = Parallel.ForEach(turkeyOrders, options, body: (Turkey turkey, ParallelLoopState state, long index) =>
{
    if (index == 5)
        state.Break();

    turkey.Cook(index);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=550)

使用 `Stop()` 时,循环会以更激进的方式终止:

```csharp
turkeyResult = Parallel.ForEach(turkeyOrders, options, body: (Turkey turkey, ParallelLoopState state, long index) =>
{
    if(index == 5)
        state.Stop();

    turkey.Cook(index);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=595)

#### 评估结果

`ParallelLoopResult` 结构体包含了循环是否已完成以及它在何处被中断的信息。

```csharp
public struct ParallelLoopResult
{
    public bool IsCompleted { get; }
    public long? LowestBreakIteration { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=655)

如果从未调用过 `state.Break()`(例如循环已经完成,或者改用了 `state.Stop()`),`LowestBreakIteration` 将为 `null`。
要确保循环已成功完成,需要同时检查 `IsCompleted` 和 `LowestBreakIteration` 的值。

```csharp
if (turkeyResult.HasValue
    && turkeyResult.Value.IsCompleted
    && turkeyResult.Value.LowestBreakIteration is null)
{
    Trace.WriteLine("Turkey Cooked Successfully");
}
else
{
    Trace.WriteLine($"Turkey Stopping Cooking at {turkeyResult?.LowestBreakIteration}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreach-69955984/?t=400)

---

## 5. Parallel.ForEachAsync

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/) · 5:38

### 总结

`Parallel.ForEachAsync` 是 `Parallel.ForEach` 方法的异步版本,它返回一个 `Task`,从而实现非阻塞的并行执行。
它在需要保持界面响应的 UI 应用中尤其有用,因为它能让调用线程保持空闲,同时由后台线程处理并行工作负载。
该方法支持用 `ParallelOptions` 控制并发度和取消,它的委托还会提供一个 `CancellationToken`,确保循环内的异步操作能够优雅地失败。

### 核心概念

- **异步执行**:返回一个可以 await 的 `Task`,因此与 `async/await` 模式兼容。
- **非阻塞**:在处理并行工作时,防止调用线程(例如 UI 线程)被冻结。
- **ParallelOptions**:允许配置 `CancellationToken` 和 `MaxDegreeOfParallelism`。
- **取消支持**:循环委托会接收一个 `CancellationToken`,用于把取消传递给内部的异步调用。
- **任务组合**:可以把多个并行循环作为任务发起,并使用 `Task.WhenAll` 统一管理。

### 课程笔记

`Parallel.ForEachAsync` 提供与 `Parallel.ForEach` 相同的并行迭代能力,但它是为异步工作流设计的。
由于返回一个 `Task`,它让应用可以 await 并行循环的完成,而不会阻塞执行线程。
这对 UI 应用尤其重要,因为阻塞主线程会导致界面冻结。

```csharp
List<Patient> patientList = GetPatients();

using var medicalRecordsFileStream =
    new FileStream(medicalRecordsPath, FileMode.Create, FileAccess.Write);

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var options = new ParallelOptions
{
    CancellationToken = cts.Token,
    MaxDegreeOfParallelism = 10
};

await Parallel.ForEachAsync(
    patientList,
    options,
    (Patient patient, CancellationToken token) =>
        medicalRecordsFileStream.WriteAsync(patient.MedicalRecords, token));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=10)

在这个示例中,`WriteAsync` 方法接收的是 `Parallel.ForEachAsync` 委托提供的 `CancellationToken`。
这确保了当循环被取消或超时时,异步写入操作能够优雅地终止。

为了在更复杂的场景中演示这一点,设想一个食物准备系统,其中不同种类的食物有不同的烹饪时间。
`Food` 基类及其派生类型使用 `Task.Delay` 来模拟异步工作。

```csharp
using System.Diagnostics;

namespace ParallelForEachAsync;

public abstract class Food
{
	readonly TimeSpan _cookTime;

	protected Food(TimeSpan cookTime)
	{
		_cookTime = cookTime;
		Name = GetType().Name;
	}

	public string Name { get; }

	public async Task Cook(CancellationToken cancellationToken)
	{
		Trace.WriteLine($"Cooking {Name} on Thread {Environment.CurrentManagedThreadId}");
		await Task.Delay(_cookTime, cancellationToken);
		Trace.WriteLine($"{Name} Completed on Thread {Environment.CurrentManagedThreadId}");
	}
}

public class Turkey() : Food(TimeSpan.FromSeconds(5));
public class MashedPotatoes() : Food(TimeSpan.FromSeconds(2));
public class Gravy() : Food(TimeSpan.FromSeconds(1));
public class Stuffing() : Food(TimeSpan.FromSeconds(2));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=145)

执行多个并行循环时,如果依次 await 它们,第二个循环要等到第一个循环的所有并行迭代全部完成后才会开始。
虽然每个循环内部的代码是并行运行的,但这些循环本身是一个接一个处理的。

```csharp
try
{
    await Parallel.ForEachAsync(turkeyOrders, options, body: async (Turkey turkey, CancellationToken token) => await turkey.Cook(token));
    await Parallel.ForEachAsync(mashedPotatoesOrders, options, body: async (MashedPotatoes mashedPotatoes, CancellationToken token) => await mashedPotatoes.Cook(token));
    await Parallel.ForEachAsync(gravyOrders, options, body: async (Gravy gravy, CancellationToken token) => await gravy.Cook(token));
    await Parallel.ForEachAsync(stuffingOrders, options, body: async (Stuffing stuffing, CancellationToken token) => await stuffing.Cook(token));

    Trace.WriteLine("All Meals Complete");
}
catch
{
    Trace.WriteLine("ERROR: Cooking took too long");
}
finally
{
    Trace.WriteLine("Cooking Ended");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=195)

为了获得更高的吞吐量,你可以发起每个 `Parallel.ForEachAsync` 调用,而不立即 await 它。
通过捕获每个循环返回的 `Task`,你可以使用 `Task.WhenAll` 让所有并行循环同时运行。
这让系统能够在所有可用线程上并发处理火鸡、土豆泥、肉汁和填馅的订单,但受 `MaxDegreeOfParallelism` 限制的约束。

```csharp
try
{
	var turkeyCookingTasks = Parallel.ForEachAsync(turkeyOrders, options, static async (turkey, token) => await turkey.Cook(token));
	var mashedPotatoesTasks = Parallel.ForEachAsync(mashedPotatoesOrders, options, static async (mashedPotatoes, token) => await mashedPotatoes.Cook(token));
	var gravyCookingTasks = Parallel.ForEachAsync(gravyOrders, options, static async (gravy, token) => await gravy.Cook(token));
	var stuffingCookingTasks = Parallel.ForEachAsync(stuffingOrders, options, static async (stuffing, token) => await stuffing.Cook(token));

	await Task.WhenAll(turkeyCookingTasks, mashedPotatoesTasks, gravyCookingTasks, stuffingCookingTasks);

	Trace.WriteLine("All Meals Complete");
}
catch
{
	Trace.WriteLine("ERROR: Cooking took too long");
}
finally
{
	Trace.WriteLine("Cooking Ended");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/parallel-foreachasync-69955985/?t=285)

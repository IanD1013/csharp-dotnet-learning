# Parallel LINQ

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 7 章
> 共 2 课 · 约 17:02
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to PLINQ](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/) | 8:52 | [↓](#1-introduction-to-plinq) |
| 2 | [Using PLINQ in Code](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/) | 8:10 | [↓](#2-using-plinq-in-code) |

---

## 1. Introduction to PLINQ

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/) · 8:52

### 总结

Parallel LINQ(PLINQ)是 LINQ to Objects 的并行实现,它让标准 LINQ 查询可以跨多个处理器执行。
借助 AsParallel 扩展方法,.NET 引擎可以对数据源进行分区,并发地执行查询操作。
PLINQ 内置了一个精密的优化引擎,它会综合考虑线程管理和资源分配的开销,判断并行化是否真的能带来性能收益。
它还提供对排序、并发上限和缓冲策略的细粒度控制,以确保在各种应用场景下都能高效执行。

### 核心概念

*   **AsParallel**:把 `IEnumerable<T>` 转换为 `ParallelQuery<T>` 的入口,从而启用并行执行。
*   **Optimization Engine**:PLINQ 会分析查询,如果并行化的开销(线程创建、内存分配)超过性能收益,它可能会选择顺序执行。
*   **AsSequential**:把并行查询转回顺序的 `IEnumerable<T>`。
*   **AsOrdered / AsUnordered**:控制结果是否保留原始序列的顺序,这一点很重要,因为并行线程返回结果的顺序是不确定的。
*   **WithDegreeOfParallelism**:限制并发线程的最大数量,以防止资源耗尽。
*   **WithMergeOptions**:配置结果如何被缓冲并交付给消费者。
*   **WithExecutionMode**:让开发者即使在引擎通常会默认顺序执行的情况下,也能强制并行执行。

### 课程笔记

标准 LINQ 查询是顺序执行的,本质上相当于高度优化的 `foreach` 循环。
当执行像 `numbers.Where(x => x % 2 == 0)` 这样的查询时,.NET 会逐项遍历集合来测试谓词。
虽然 .NET 工程师多年来一直在优化这些顺序操作,但它们默认仍然是单线程的。

#### 启用并行

要启用并行执行,请使用 `AsParallel()` 扩展方法。
这让 .NET 能够审视该操作,并决定如何最高效地利用可用的 CPU 资源来执行它。

```csharp
// Get Even Numbers
var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();

// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel().Where(x => x % 2 == 0).ToList();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=25)

需要注意的是,`AsParallel()` 只是一个请求,而不是保证。
PLINQ 引擎会分析集合的大小和操作的复杂度。
如果集合很小(例如只有几个元素),启动新线程并在堆上分配内存的开销可能会超过并行带来的好处。
在这种情况下,.NET 仍可能选择顺序执行查询,以确保开销最小。

#### 控制顺序与排序

如果之后需要在代码中把并行查询转回顺序查询,可以使用 `AsSequential()` 方法。

```csharp
// Get Even Numbers
ParallelQuery<int> evenNumbersInParallel = numbers.AsParallel().Where(x => x % 2 == 0);

// Get Even Numbers Sequentially
IEnumerable<int> evenNumbers = evenNumbersInParallel.AsSequential();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=205)

由于并行操作运行在优先级各不相同的不同线程上,结果通常会以随机顺序返回。
要确保输出与源序列保持相同的顺序,请使用 `AsOrdered()`。
反过来,`AsUnordered()` 可以用来移除此前被标记为有序的查询上的排序约束。

```csharp
// Get Even Numbers in Parallel in Order
ParallelQuery<int> evenNumbersInParallel = numbers.AsParallel().AsOrdered().Where(x => x % 2 == 0 );

// Get Even Numbers in Parallel without Preserving Order
ParallelQuery<int> unorderedNumbersInParallel = evenNumbersInParallel.AsUnordered();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=235)

#### 并发与缓冲

为了防止线程池耗尽,并确保应用的其他部分不会缺少资源,可以设置最大并行度。

```csharp
// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel().WithDegreeOfParallelism(5).Where(x => x % 2 == 0);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=310)

缓冲行为可以通过 `WithMergeOptions` 来控制。
`AutoBuffered`(默认)选项让系统选择一个经过优化的缓冲区大小。
`NoBuffered` 会在结果处理完成后立即返回,而 `FullyBuffered` 会等到整个查询完成后才返回任何结果。

```csharp
// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel()
                            .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                            .Where(x => x % 2 == 0);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=355)

#### 执行模式

如果你已经对代码做过基准测试,并确定尽管 PLINQ 引擎决定顺序执行,并行执行却更快,那么可以使用 `WithExecutionMode` 强制并行。

```csharp
// Get Even Numbers in Parallel
var evenNumbersInParallel = numbers.AsParallel()
                                   .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                                   .Where(x => x % 2 == 0);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/introduction-to-plinq-69955988/?t=430)

#### 实现示例:查找质数

下面的示例演示了 PLINQ 查找质数的一个实际应用,其中使用了 `AsParallel`、`AsOrdered` 和取消令牌,以确保执行稳健。

```csharp
using System.Diagnostics;

IReadOnlyList<int> iterationsList = [.. Enumerable.Range(1, 10_000)];

Trace.WriteLine("***Finding prime numbers in series using LINQ...");
var seriesStopwatch = new Stopwatch();
seriesStopwatch.Start();

var primeNumbers = iterationsList.Select(FindNextPrimeNumber).ToList();
seriesStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in series took {seriesStopwatch.Elapsed.TotalSeconds:F3}s.");

Trace.WriteLine("***Finding prime in Parallel using PLINQ...");
var parallelStopwatch = new Stopwatch();
parallelStopwatch.Start();

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
var primeNumbersInParallel = iterationsList
										.AsParallel()
										.AsOrdered()
										.WithCancellation(cts.Token)
										.Select(FindNextPrimeNumber).ToList();

parallelStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in parallel took {parallelStopwatch.Elapsed.TotalSeconds:F3}s.");

foreach (var primeNumber in primeNumbersInParallel)
{
	Trace.Write($"{primeNumber}, ");
}

static long FindNextPrimeNumber(int n)
{
	int count = 0;
	long a = 2;
	while (count < n)
	{
		long b = 2;
		int prime = 1;
		while (b * b <= a)
		{
			if (a % b is 0)
			{
				prime = 0;
				break;
			}

			b++;
		}

		if (prime > 0)
		{
			count++;
		}

		a++;
	}

	return (--a);
}
```

---

## 2. Using PLINQ in Code

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/) · 8:10

### 总结

本课演示如何在实践中使用 Parallel LINQ(PLINQ)来优化计算密集型任务。
通过用一个质数计算来对比顺序 LINQ 查询和并行化版本,本课展示了如何使用 `AsParallel` 把工作分配到多个 CPU 核心上、使用 `AsOrdered` 保持序列的完整顺序,以及使用 `WithCancellation` 管理执行超时。
结果显示,执行时间显著缩短,硬件资源的利用也更加高效。

### 核心概念

- 使用 `AsParallel()` 把 LINQ 转换为 PLINQ。
- 使用 `AsOrdered()` 保留序列顺序。
- 使用 `WithCancellation()` 处理超时和取消。
- PLINQ 中的延迟执行。
- 性能基准测试的注意事项。
- 顺序处理与并行处理下的 CPU 利用模式。

### 课程笔记

本课首先搭建一个测试环境,用来对比顺序处理和并行处理。
程序生成一个包含 10,000 个整数的列表,并定义了一个计算开销很大的方法 `FindNextPrimeNumber`。
该方法使用嵌套循环为给定输入查找下一个质数,其复杂度为 $O(n^2)$,因此非常适合作为并行化的对象。

```csharp
using System.Diagnostics;

IReadOnlyList<int> iterationsList = [.. Enumerable.Range(1, 10_000)];

Trace.WriteLine("***Finding prime numbers in series using LINQ...");
var seriesStopwatch = new Stopwatch();
seriesStopwatch.Start();

// ToDo: Retrieve Prime Numbers Sequentially

seriesStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in series took {seriesStopwatch.Elapsed.TotalSeconds:F3}s.");

Trace.WriteLine("***Finding prime in Parallel using PLINQ...");
var parallelStopwatch = new Stopwatch();
parallelStopwatch.Start();

// ToDo: Retrieve Prime Numbers in Parallel

parallelStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in parallel took {parallelStopwatch.Elapsed.TotalSeconds:F3}s.");

return;

static long FindNextPrimeNumber(int n)
{
	int count = 0;
	long a = 2;
	while (count < n)
	{
		long b = 2;
		int prime = 1;
		while (b * b <= a)
		{
			if (a % b is 0)
			{
				prime = 0;
				break;
			}

			b++;
		}

		if (prime > 0)
		{
			count++;
		}

		a++;
	}

	return (--a);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=10)

#### 顺序执行

顺序实现使用标准 LINQ 的 `Select` 方法。
这里使用了"方法组"(method group)简写,把 `FindNextPrimeNumber` 直接传给 `Select` 调用。
由于 LINQ 使用延迟执行,这里调用 `ToList()` 方法来强制查询立即执行,这样 `Stopwatch` 才能准确测量处理整个列表所花的时间。
注意,这里为了简单使用了 `Stopwatch`,但考虑到 JIT 优化和开销等因素,专业的性能分析推荐使用 `Benchmark.net`。

```csharp
// ToDo: Retrieve Prime Numbers Sequentially
var primeNumbers = iterationsList.Select(FindNextPrimeNumber).ToList();

seriesStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in series took {seriesStopwatch.Elapsed.TotalSeconds:F3}s.");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=130)

#### 使用 PLINQ 并行执行

要切换到并行执行,需要在源集合上调用 `AsParallel()` 扩展方法。
为了确保输出列表与输入范围(1 到 10,000)保持相同的顺序,这里应用了 `AsOrdered()` 方法。
此外,还创建了一个超时时间为 30 秒的 `CancellationTokenSource`,并通过 `WithCancellation()` 传入查询。
这是一种最佳实践,可以防止长时间运行的并行操作无限期挂起。
与标准 LINQ 类似,PLINQ 查询也会延迟到调用 `ToList()` 这样的方法时才执行。

```csharp
// ToDo: Retrieve Prime Numbers in Parallel
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
var primeNumbersInParallel = iterationsList
										.AsParallel()
										.AsOrdered()
										.WithCancellation(cts.Token)
										.Select(FindNextPrimeNumber).ToList();

parallelStopwatch.Stop();

Trace.WriteLine($"***Finding prime numbers in parallel took {parallelStopwatch.Elapsed.TotalSeconds:F3}s.");
```

```text
iterationsList (1 .. 10_000)
   |
   v
.AsParallel()                      -> ParallelQuery<int>
   |
   v
.AsOrdered()                       保持与输入相同的顺序
   |
   v
.WithCancellation(cts.Token)       30 秒超时
   |
   v
.Select(FindNextPrimeNumber)       延迟执行
   |
   v
.ToList()                          查询在此执行
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=280)

#### 性能与利用率

运行代码时,顺序执行通常要花明显更长的时间(例如 17 秒),而且只使用单个核心,在 CPU 监控中表现为一条平坦的线。
相比之下,PLINQ 版本利用所有可用的 CPU 核心,完成得快得多(例如 3 秒),在 CPU 利用率上表现为一个明显的尖峰。
`AsOrdered()` 调用确保了尽管进行了并行处理,最终输出仍然保持有序。

```csharp
foreach (var primeNumber in primeNumbersInParallel)
{
	Trace.Write($"{primeNumber}, ");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/using-plinq-in-code-69955989/?t=355)

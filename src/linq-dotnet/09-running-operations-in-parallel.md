# Running operations in parallel

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 9 章
> 共 5 课 · 约 32:14
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Why would we want to parallelize our queries?](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-would-we-want-to-parallelize-our-queries-54280479/) | 2:26 | [↓](#1-why-would-we-want-to-parallelize-our-queries) |
| 2 | [Introduction to PLINQ](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/) | 14:19 | [↓](#2-introduction-to-plinq) |
| 3 | [Things to consider when using PLINQ](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/things-to-consider-when-using-plinq-54280482/) | 6:47 | [↓](#3-things-to-consider-when-using-plinq) |
| 4 | [Potential pitfalls](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/potential-pitfalls-54280483/) | 7:44 | [↓](#4-potential-pitfalls) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280484/) | 0:58 | [↓](#5-section-recap) |

## 1. Why would we want to parallelize our queries?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-would-we-want-to-parallelize-our-queries-54280479/) · 2:26

### 总结

Parallel LINQ(PLINQ)是一项旨在利用现代 CPU 的多核架构来优化计算密集型内存查询性能的特性。
标准的针对 IEnumerable 的 LINQ 查询在单个线程上顺序执行,而 PLINQ 允许把工作负载分布到多个线程上,从而显著缩短那些受 CPU 处理或长时间运行的条件所制约的操作的总执行时间。

### 核心概念

- **PLINQ(Parallel LINQ)**:LINQ 的一种功能扩展,使查询能够并行处理。
- **硬件利用率**:现代 CPU 通常具有多个核心和线程(例如 14 个核心、20 个线程),而顺序执行的 LINQ 无法充分利用它们。
- **顺序瓶颈**:标准的 `IEnumerable` 查询在单个线程上逐个处理元素,这对于长时间运行的操作来说效率很低。
- **性能扩展**:把工作分布到可用线程上,理论上可以按可用逻辑处理器的数量成比例地加快查询。

### 课程笔记

PLINQ 提供了一种并行执行 LINQ 查询各部分的方式。
这对现代软件开发至关重要,因为现代 CPU 被设计为拥有很高的核心数和线程数。
例如,一颗标准的笔记本处理器可能支持 20 个并发线程。

在标准 LINQ 中,针对 `IEnumerable` 运行的查询是顺序执行的。
在幕后,这被当作一个简单的 `foreach` 循环来处理,意味着在单个核心上一次只处理一个元素。
如果查询中包含一个计算代价高昂或被人为延迟的条件或操作,整个过程就会变慢。

看下面这个例子,其中查询因为 `where` 子句里的一个 `Thread.Sleep` 调用而变慢:

```csharp
namespace Dometrain.Linq.Cmd._9_ParallelExecution;

public class PlinqExamples : QueryRunner
{
    public override void Run()
    {
        ASlowQueryAppeared();
    }

    void ASlowQueryAppeared()
    {
        var allMovies = Repository.GetAllMovies();

        var query =
            from movie in allMovies
            where SlowCondition(movie.Phase < 3)
            select movie;

        PrintAll(query);
    }

    private bool SlowCondition(bool inputCondition)
    {
        Thread.Sleep(1000);
        return inputCondition;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-would-we-want-to-parallelize-our-queries-54280479/?t=10)

在这段代码中,查询每秒只处理一个元素。
如果把条件扩展为包含更多元素,执行时间也会相应增加:

```csharp
namespace Dometrain.Linq.Cmd._9_ParallelExecution;

public class PlinqExamples : QueryRunner
{
    public override void Run()
    {
        ASlowQueryAppeared();
    }

    void ASlowQueryAppeared()
    {
        var allMovies = Repository.GetAllMovies();

        var query =
            from movie in allMovies
            where SlowCondition(movie.Phase < 5)
            select movie;

        PrintAll(query);
    }

    private bool SlowCondition(bool inputCondition)
    {
        Thread.Sleep(1000);
        return inputCondition;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-would-we-want-to-parallelize-our-queries-54280479/?t=70)

如果 `SlowCondition` 代表的是真正的繁重计算,就可以通过在所有可用 CPU 核心上并行运行 `where` 子句来优化这个查询。
在一台能处理 20 个线程的机器上,这有可能让查询加快 20 倍。
PLINQ 使这种优化成为可能,让内存查询能够以最优方式利用硬件。

## 2. Introduction to PLINQ

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/) · 14:19

### 总结

Parallel LINQ(PLINQ)能把 LINQ 查询的处理分布到多个 CPU 核心上以提升性能,尤其适用于受 CPU 制约或高延迟的操作。
通过对源数据进行分区(这一过程被称为"剥离"(peeling)),PLINQ 并发地执行查询逻辑,并把结果合并回来。
虽然并行化能显著缩短执行时间,但它默认会引入不确定的顺序。
开发者可以使用专门的扩展方法来管理这一行为,以保持顺序、限制线程使用、控制结果缓冲,或者为对线程敏感的操作切换回顺序处理。

### 核心概念

- **分区(剥离)**:把源集合切成若干块,以便在不同的 CPU 核心上独立处理的过程。
- **`AsParallel()`**:PLINQ 的入口,把 `IEnumerable<T>` 转换为 `ParallelQuery<T>`。
- **`AsOrdered()`**:确保并行查询的结果按与源集合相同的顺序返回,代价是少量的性能开销。
- **`WithDegreeOfParallelism(int)`**:限制查询使用的最大并发线程数。
- **`ForAll(Action<T>)`**:对每个元素并行执行一个操作,而不需要把结果合并回单个线程。
- **`WithMergeOptions(ParallelMergeOptions)`**:控制结果如何被缓冲,以及如何从工作线程返回到主线程。
- **`AsSequential()`**:让查询中后续的运算符切换回顺序执行。

### 课程笔记

#### 并行化对性能的影响

当对一个每个元素都需要大量处理时间(例如每个元素延迟一秒)的集合执行标准 LINQ 查询时,总执行时间会随元素数量线性增长。
对于一个包含 36 个元素的集合,顺序查询大约需要 36 秒。

```csharp
void RunInParallel()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    var query = allMovies
        .Where(movie => SlowCondition(movie.Phase < 5));

    PrintAll(query);

    Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=110)

加上 `.AsParallel()` 之后,PLINQ 会把数据分区到可用的 CPU 核心上。
在一台有 20 个核心的机器上,同样的 36 个元素的查询大约 2 秒就能完成,因为工作被同时分布到了 20 个线程上。

```csharp
void RunInParallel()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    var query = allMovies
        .AsParallel()
        .Where(movie => SlowCondition(movie.Phase < 5));

    PrintAll(query);

    Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=170)

```text
allMovies (36 items)
      |
      | .AsParallel()  -> partitioning (peeling)
      v
+----------+ +----------+       +----------+
| thread 1 | | thread 2 |  ...  | thread 20|
|  .Where  | |  .Where  |       |  .Where  |
+----------+ +----------+       +----------+
      \           |                  /
       v          v                 v
              merge results
                   |
                   v
            PrintAll(query)       ~36 s -> ~2 s
```

#### 保持顺序

并行执行本质上是不确定的;元素在线程可用时被处理,这意味着输出顺序很可能与输入顺序不同。
要保持集合原有的顺序,请使用 `.AsOrdered()` 方法。
它确保结果按正确的顺序合并回来,不过由于跟踪索引的开销,会带来轻微的性能损失。

```csharp
void PreserveOrdering()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    var query = allMovies
        .AsParallel()
        .AsOrdered()
        .Where(movie => SlowCondition(movie.Phase < 5));

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=295)

#### 控制资源使用

在你希望限制对 CPU 的影响的场景中(例如为其他后台任务留出资源),可以用 `.WithDegreeOfParallelism()` 指定 PLINQ 应使用的最大线程数。

```csharp
void LimitParallelization()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    var query = allMovies
        .AsParallel()
        .WithDegreeOfParallelism(6)
        .Where(movie => SlowCondition(movie.Phase < 5));

    PrintAll(query);

    Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=355)

#### 使用 ForAll 并行执行

标准的 `foreach` 循环是顺序执行的。
即使查询被并行化了,对结果的最终迭代通常也发生在单个线程上。
要让整个管道(包括最后的操作)都保持并行,请使用 `.ForAll()`。
这是在并行结果完成时对其执行某个操作(比如写入控制台)的最高效方式。

```csharp
void UseForAll()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    allMovies
        .AsParallel()
        .Where(movie => SlowCondition(movie.Phase < 5))
        .ForAll(movie => Console.WriteLine(movie));

    Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=475)

#### 合并选项与缓冲

当结果从工作线程返回到主线程时,PLINQ 会使用缓冲。
你可以用 `ParallelMergeOptions` 控制这一行为:
- **`AutoBuffered`(默认)**:由 PLINQ 决定缓冲区大小,以平衡性能与延迟。
- **`FullyBuffered`**:每个工作线程都必须完成其全部任务,之后才会有结果返回到主线程。
  这会导致最后一次性出现一整"块"数据。
- **`NotBuffered`**:元素一经处理就立即返回到主线程。
  这降低了延迟,但由于线程之间频繁通信,可能会增加开销。

```csharp
void MergingThreadResults()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    var query = allMovies
        .AsParallel()
        .WithDegreeOfParallelism(6)
        .WithMergeOptions(ParallelMergeOptions.NotBuffered)
        .Where(movie => SlowCondition(movie.Phase < 5));

    PrintAll(query);

    Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=625)

#### 回到顺序处理

如果查询的一部分是线程安全的,而另一部分不是(或者必须按特定顺序处理),可以使用 `.AsSequential()`。
`.AsSequential()` 之后的运算符会在单个线程上运行,从而实现混合查询:在可能的地方最大化并行吞吐量,同时在需要的地方保持安全。

```csharp
void ReturnToSequential()
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var allMovies = Repository.GetAllMovies();

    var query = allMovies
        .AsParallel()
        .Where(movie => SlowCondition(movie.Phase < 5))
        .AsSequential()
        .Where(movie => SlowCondition(movie.ReleaseDate.Year > 2011));

    PrintAll(query);

    Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds} ms");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-plinq-54280480/?t=775)

```text
allMovies
   |
   | .AsParallel()
   v
.Where(Phase < 5)                parallel, many threads
   |
   | .AsSequential()
   v
.Where(ReleaseDate.Year > 2011)  sequential, single thread
   |
   v
PrintAll(query)
```

## 3. Things to consider when using PLINQ

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/things-to-consider-when-using-plinq-54280482/) · 6:47

### 总结

用 PLINQ 并行化 LINQ 查询并不能保证性能提升,有时还会引入足以拖慢执行的开销。
要获得净收益,查询必须包含大量的计算工作,运行在多核硬件上,并尽量减少需要线程同步或在主线程上物化数据的操作。
理解数据如何被分区和合并,对于同时优化并行操作的实际吞吐量和感知响应性至关重要。

### 核心概念

*   **计算开销**:对于小型或快速的工作负载,创建和管理线程的成本可能超过其收益。
*   **硬件约束**:PLINQ 需要多个 CPU 核心才能带来任何好处;单核环境只会承受开销。
*   **运算符瓶颈**:`OrderBy`、`GroupBy` 和 `Join` 这类操作通常需要同步数据或把数据带回单个线程,从而降低并行效率。
*   **执行方式**:使用 `ForAll` 让处理保留在工作线程上,而 `foreach` 或物化为列表会强制把数据带回主线程。
*   **合并选项**:控制结果如何缓冲,可以提升查询的感知速度(响应性)。
*   **分区**:把源数据高效地分布到各个线程上,对于防止"倾斜"(skew)至关重要,所谓倾斜就是某个线程做的工作明显多于其他线程。

### 课程笔记

#### 工作负载与硬件

并非每个查询都适合并行化。
首先要考虑的是,是否有足够多有意义的计算工作可以分布到各个线程上。
如果查询执行的是简单、快速的操作,那么创建线程并合并其结果的开销可能会超过并行执行节省的时间。
然而,如果查询包含缓慢且无副作用的操作,并行化就能带来显著的性能提升。

硬件是一项硬性约束。
在单核环境中(例如小型云实例),PLINQ 不会提升性能。
它只会引入管理开销。
并行化在拥有多个核心、工作负载能够真正被分布的服务器上最为有效。

#### 操作的影响

查询中使用的具体运算符决定了有多少工作能保持并行。
`OrderBy`、`GroupBy` 和 `Join` 这类运算符在并行环境中的计算代价很高,因为它们通常需要引擎在所有分区之间比较或聚合数据。
例如,`OrderBy` 操作需要引擎查看所有结果才能确定第一个元素,这可能会抵消在不同线程上处理这些元素所带来的好处。

当数据被分派到各个线程并尽可能长时间地留在那里时,PLINQ 的效率最高。
如果你能把重新排序或分组推迟到最后(或者完全避免),查询的执行速度会快得多。

#### 执行与物化

查询的执行方式对性能有显著影响。
把结果物化为集合(例如使用 `ToList()` 或 `ToArray()`),或者用标准的 `foreach` 循环迭代它们,都需要把数据合并回主线程。

相比之下,使用 `ForAll` 方法可以让最后的处理步骤直接在工作线程上进行。
这让数据保持分布状态,并避免了为最后的操作把所有数据带回单个线程所造成的瓶颈。

```text
ToList() / ToArray() / foreach        ForAll

worker   worker   worker           worker   worker   worker
    \      |      /                   |        |        |
     v     v     v                    v        v        v
      main thread                  final    final    final
      final step                   step     step     step
```

#### 合并选项与分区

合并选项控制查询的缓冲行为。
虽然减少缓冲可能会增加线程之间的总通信开销,但它能让查询对用户而言"感觉"更快,因为结果一准备好就会被产出,而不必等待整个缓冲区填满。

最后,分区是把源数据分布到可用线程上的过程。
虽然 PLINQ 的默认分区通常是高效的,但它偶尔会导致工作负载不均衡,即某个线程承担了大部分工作。
这种"倾斜"会让整个查询等待最慢的线程完成。
在存在可测量的不均衡的高级场景中,可以实现自定义分区器,手动平衡各线程之间的工作负载。

## 4. Potential pitfalls

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/potential-pitfalls-54280483/) · 7:44

### 总结

Parallel LINQ(PLINQ)为并发数据处理提供了强大的工具,但也引入了一些特定的风险,包括同步开销、竞态条件以及违反线程亲和性。
开发者必须评估工作负载的规模以避免过度并行化,并确保操作保持线程安全,同时尽量减少由排序或把结果合并回单个线程所造成的瓶颈。

### 核心概念

- 性能开销与工作负载规模
- 共享状态中的竞态条件
- 过度并行化与资源耗尽
- 线程安全与同步成本
- 排序操作作为单线程瓶颈
- ForAll 与 ForEach 的执行上下文
- CPU 亲和性(UI 线程和 COM 组件)
- 不确定的并行执行

### 课程笔记

Parallel LINQ 并不总是比顺序 LINQ 更快。
对于小型数据集或简单查询,对数据进行分区和管理多个线程的开销可能会超过并行执行带来的性能收益。
只有在能获得明显的性能收益时,才应该实施并行化。

PLINQ 中的一个重大风险是写入共享内存。
从多个线程同时修改共享状态会导致竞态条件和数据不一致。
为了保持正确性,PLINQ 查询中的操作应当彼此隔离,并避免对共享变量产生副作用。

当查询被拆分到的线程数超过可用 CPU 核心能够高效处理的数量时,就会发生过度并行化。
由此带来的线程创建和上下文切换开销会降低性能,而不是提升性能。
开发者在决定是否并行化时,应当留意硬件环境和工作负载的性质。

线程安全对正确性至关重要。
在并行查询中使用非线程安全的方法会导致不可预测的结果。
虽然线程安全的方法(例如静态方法或纯函数)更安全,但它们仍可能引入影响性能的同步开销。
为了获得最佳速度,逻辑应当使用委托和 lambda 来实现,PLINQ 被设计为能够高效地处理它们,而无需不必要的同步。

`OrderBy` 这类排序操作在并行环境中的代价尤其高。
排序需要数据的全局视图,这通常会迫使 PLINQ 把多个线程的结果合并回单个线程,以确保顺序正确。
如果并不需要结果的特定顺序,省略这些操作会显著提高效率。

在迭代结果时,应当使用 `ForAll` 而不是 `ForEach`。
标准的 `foreach` 循环(或 `ForEach` 扩展方法)会强制把数据合并回调用线程进行顺序处理。
相比之下,`ForAll` 允许迭代逻辑在处理数据的那些工作线程上并行执行,从而保持并行化带来的性能收益。

某些环境有 CPU 亲和性要求,即特定的操作必须在特定的线程上运行。
例如,WPF 或 WinForms 这类框架中的 UI 组件必须在 UI 线程上更新。
试图从 PLINQ 工作线程更新这些组件会导致线程冲突异常。
某些需要特定单元状态(apartment state)的 COM 组件也可能出现类似问题。

最后,开发者不应假定 PLINQ 总是会并行执行。
如果 PLINQ 引擎判断并行化不会带来好处、系统只有一个核心,或者数据分区效率低下,它可能会决定顺序运行查询。
代码应当被设计得足够健壮,无论实际是否发生了并行执行都能正常工作,因为并不能保证每个查询都会被并行化。

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280484/) · 0:58

### 总结

本课对 Parallel LINQ(PLINQ)做了总结性的概述,强调它能够通过一个简单的 API 把计算密集型操作扩展到多个系统线程上。
虽然 PLINQ 能带来可观的性能提升,但理解其底层机制和潜在风险至关重要,以确保只在有益时才使用它。

### 核心概念

- Parallel LINQ API 的简洁性。
- 把工作分布到可用的系统线程上。
- 计算密集型查询的显著性能提升。
- 理解 PLINQ 内部行为对于降低风险的重要性。

### 课程笔记

Parallel LINQ(PLINQ)被设计得非常易于使用,它提供的 API 几乎不会给现有的 LINQ 代码增加语法复杂度。
尽管如此简单,底层引擎却处理着高级操作,包括对数据进行分区,以及把工作分布到系统中所有可用的线程上。

PLINQ 的主要好处是可能带来巨大的加速。
通过利用并行执行,原本会受单线程处理制约的查询可以快得多地完成。
这对于每个元素处理时间都很长的计算密集型查询尤为重要。

然而,并行化并非没有风险。
开发者必须了解 PLINQ 在幕后是如何运作的,才能就何时使用它做出明智的决定。
虽然它是解决性能问题的强大工具,但重要的是要在其收益与并行化的开销以及并发执行的复杂性之间进行权衡。

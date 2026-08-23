# Mastering LINQ over IEnumerables

> 课程:[Mastering: C#](https://dometrain.com/course/mastering-csharp/) · 第 14 章
> 共 8 课 · 约 12:55
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958928/) | 0:45 | [↓](#1-overview) |
| 2 | [Two Syntaxes, One Model](https://dometrain.com/take/course/mastering-csharp-3256129/two-syntaxes-one-model-69958931/) | 0:23 | [↓](#2-two-syntaxes-one-model) |
| 3 | [Query Comprehensions vs. Method Calls](https://dometrain.com/take/course/mastering-csharp-3256129/query-comprehensions-vs-method-calls-69958934/) | 2:51 | [↓](#3-query-comprehensions-vs-method-calls) |
| 4 | [What Is IEnumerable&lt;T&gt;?](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-ienumerable-t-69958937/) | 0:50 | [↓](#4-what-is-ienumerablet) |
| 5 | [Multiple Enumerations with IEnumerable&lt;T&gt;](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/) | 2:03 | [↓](#5-multiple-enumerations-with-ienumerablet) |
| 6 | [LINQ Deferred Execution Model](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/) | 3:24 | [↓](#6-linq-deferred-execution-model) |
| 7 | [Using Static Analysis to Avoid Multiple Enumerations](https://dometrain.com/take/course/mastering-csharp-3256129/using-static-analysis-to-avoid-multiple-enumerations-69958948/) | 1:19 | [↓](#7-using-static-analysis-to-avoid-multiple-enumerations) |
| 8 | [Summary](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958949/) | 1:20 | [↓](#8-summary) |

## 1. Overview

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958928/) · 0:45

### 总结

本课介绍 LINQ over IEnumerables,它通常被称为 LINQ to Objects,提供了一组用于内存中数据处理的 API。
本课探讨两种主要的语法形式 - query syntax 和 method call syntax - 并解释 C# 编译器如何把查询表达式翻译成方法调用。
讨论进一步延伸到 IEnumerable 接口的基础行为,重点关注延迟执行(deferred execution)的影响,以及在应用程序 API 中多次枚举所带来的性能代价。

### 核心概念

- 用于内存中数据处理的 LINQ to Objects。
- Query syntax 与 Method call syntax 的对比。
- 编译器对 LINQ 查询的翻译。
- IEnumerable 接口与延迟执行。
- 在公有和私有 API 中多次枚举的风险。
- 使用 TryGetNonEnumeratedCount 的优化技巧。

### 课程笔记

LINQ to Objects 是一组专门用于处理存储在内存中的数据的 API。
它提供了两种不同的查询编写语法:query syntax 和 method call syntax。
虽然 query syntax 常常与 LINQ to Entities 之类的其他 provider 关联在一起,但它同样是操作内存中数据的强大工具。

```csharp
int[] input = [1, 2, 3];
int sum = EvenNumbersQuery(input).Sum();
Console.WriteLine(sum);
sum = EvenNumbersMethodCalls(input).Sum();
Console.WriteLine(sum);

static IEnumerable<int> EvenNumbersQuery(int[] nums)
{
    return from n in nums
        where n % 2 == 0
        select n * 2;
}

static IEnumerable<int> EvenNumbersMethodCalls(int[] numbers)
{
    return numbers
        .Where(n => n % 2 == 0)
        .Select(n => n * 2);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958928/?t=9)

C# 编译器在很多方面把这两种语法视为可互换的,在编译期把 query syntax 翻译成对应的方法调用。
这让开发者可以针对具体逻辑的可读性需求选择最合适的语法,例如在复杂查询中使用 `let` 子句来处理中间结果。

```csharp
static IEnumerable<(string Directory, double AvgBytes)> AverageDirectoryQuery(DirectoryInfo root)
{
    return from dir in root.EnumerateDirectories()
        let files = dir.GetFiles("*", SearchOption.AllDirectories)
        where files.Length > 0
        select (dir.Name, AvgBytes: files.Average(f => f.Length));
}

static IEnumerable<(string Directory, double AvgBytes)> AverageDirectoryMethodCalls(DirectoryInfo root)
{
    return root.EnumerateDirectories()
        .Select(dir => new { dir, files = dir.GetFiles("*", SearchOption.AllDirectories) })
        .Where(t => t.files.Length > 0)
        .Select(t => (t.dir.Name, AvgBytes: t.files.Average(f => f.Length)));
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958928/?t=13)

使用 `IEnumerable` 时一个关键点是理解延迟执行。
LINQ 查询不是在定义时执行,而是在对结果序列进行枚举时才执行。
这一行为可能导致意料之外的副作用,尤其是在处理异步操作或对同一序列多次枚举时,底层逻辑可能被执行比预期更多的次数。

```csharp
var ids = Enumerable.Range(1, 3);
IEnumerable<Task<int>> tasks = ProcessConcurrently(ids);
ProcessTasks(tasks);

static IEnumerable<Task<int>> ProcessConcurrently(IEnumerable<int> input)
{
    return input.Select(id => GetAsync(id));
}

static async Task<int> GetAsync(int id)
{
    Console.WriteLine($"  start {id}");
    await Task.Delay(10);
    return id * 2;
}

static void ProcessTasks(IEnumerable<Task<int>> tasks)
{
    Task.WaitAll(tasks);
    foreach (var task in tasks)
    {
        Console.WriteLine(task.GetAwaiter().GetResult());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958928/?t=35)

在设计 API 时,必须考虑多次枚举带来的影响。
如果把一个 `IEnumerable` 传给会多次遍历它的方法,查询逻辑可能被重新执行,这通常是低效的。
开发者可以使用 `TryGetNonEnumeratedCount` 之类的方法,在不触发枚举的前提下检查集合的元素数量是否可用,或者把序列物化成列表以确保它只被处理一次。

```csharp
public static int ProcessData(IEnumerable<string> data)
{
    LogStartDataProcessing(data);

    int processed = 0;

    foreach (var item in data)
    {
        ProcessItem(item);
        processed++;
    }

    return processed;
}

private static void LogStartDataProcessing(IEnumerable<string> data)
{
    if (data.TryGetNonEnumeratedCount(out int count))
    {
        // Log the exact count without triggering enumeration
    }
}

static class EnumerableExtensions
{
    public static IReadOnlyList<T> ToReadOnlyListIfNeeded<T>(this IEnumerable<T> source) =>
        source as IReadOnlyList<T> ?? source.ToArray();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958928/?t=35)

## 2. Two Syntaxes, One Model

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/two-syntaxes-one-model-69958931/) · 0:23

### 总结

LINQ(Language Integrated Query)在 C# 内部提供了一种领域特定语言(DSL),让数据查询可以写得富有表达力。
虽然 LINQ 常常与 Entity Framework 或 LINQ to SQL 这类数据库技术联系在一起,但它在处理内存中的集合时同样强大。
C# 为 LINQ 提供了两种主要语法 - Query Syntax 和 Method Syntax - 它们都会被编译成同一套底层的方法调用模型,从而保证声明式风格与命令式风格在功能上等价。

### 核心概念

- 把 Language Integrated Query(LINQ)视为 C# 的领域特定语言(DSL)。
- Query Syntax:一种声明式的、类似 SQL 的数据操作方式。
- Method Syntax:使用 lambda 表达式的扩展方法链式调用。
- 功能等价:两种语法都映射到同一套底层执行模型。
- 使用 `IEnumerable<T>` 进行内存中的数据处理。

### 课程笔记

LINQ 是一种直接集成到 C# 中的领域特定语言(DSL)。
它的声明式语法以 SQL 为蓝本 - 这让它天然适配 LINQ to Entities 或 LINQ to SQL 这类数据库 provider - 但它同时也是为处理内存中的数据结构而设计的。

编写 LINQ 查询主要有两种方式:Query Syntax 和 Method Syntax。
Query Syntax 是声明式的,在涉及多个 join 或中间变量的复杂查询中通常更易读。
Method Syntax 使用扩展方法和 lambda 表达式。
下面的示例展示两种语法执行同一操作:从数组中筛选出偶数并把结果翻倍。

```csharp
static IEnumerable<int> EvenNumbersQuery(int[] nums)
{
    return from n in nums
        where n % 2 == 0
        select n * 2;
}

static IEnumerable<int> EvenNumbersMethodCalls(int[] numbers)
{
    return numbers
        .Where(n => n % 2 == 0)
        .Select(n => n * 2);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/two-syntaxes-one-model-69958931/?t=10)

这两种语法在功能上等价,并且会被编译成同样的底层方法调用。
对于更复杂的操作,例如计算各个目录下的平均值,Query Syntax 会用 `let` 关键字来创建中间变量。
相比之下,Method Syntax 通常需要投影成匿名类型,才能把状态沿着管道传递下去。

```csharp
static IEnumerable<(string Directory, double AvgBytes)> AverageDirectoryQuery(DirectoryInfo root)
{
    return from dir in root.EnumerateDirectories()
        let files = dir.GetFiles("*", SearchOption.AllDirectories)
        where files.Length > 0
        select (dir.Name, AvgBytes: files.Average(f => f.Length));
}

static IEnumerable<(string Directory, double AvgBytes)> AverageDirectoryMethodCalls(DirectoryInfo root)
{
    return root.EnumerateDirectories()
        .Select(dir => new { dir, files = dir.GetFiles("*", SearchOption.AllDirectories) })
        .Where(t => t.files.Length > 0)
        .Select(t => (t.dir.Name, AvgBytes: t.files.Average(f => f.Length)));
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/two-syntaxes-one-model-69958931/?t=19)

LINQ 也支持并行处理。
通过使用 `.AsParallel()` 扩展,一个查询可以在多个核心上执行,同时保持同样的声明式 Query Syntax。
这体现了 LINQ 模型的灵活性:同一套 DSL 只需对查询逻辑做极小的改动,就能面向不同的执行引擎(顺序执行与并行执行)。

```csharp
static ParallelQuery<(string Directory, double AvgBytes)> AverageDirectoryParallelQuery(DirectoryInfo root) =>
    from dir in root.EnumerateDirectories().AsParallel()
    let files = dir.GetFiles("*", SearchOption.AllDirectories)
    where files.Length > 0
    select (dir.Name, AvgBytes: files.Average(f => f.Length));
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/two-syntaxes-one-model-69958931/?t=19)

## 3. Query Comprehensions vs. Method Calls

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/query-comprehensions-vs-method-calls-69958934/) · 2:51

### 总结

本课探讨 LINQ 的 query comprehension 语法与 method call 语法之间的关系,展示 C# 编译器会把前者直接转换成后者。
虽然两种写法生成的代码是等价的,但在复杂场景中 query syntax 的可读性更好 - 例如需要用 let 关键字声明局部变量或需要多个 join 的场景 - 而在简单查询中 method syntax 往往更简洁。
本课还着重展示了 LINQ 的可扩展性:更换底层 provider,例如通过 AsParallel 使用 Parallel LINQ(PLINQ),就能让同样的 query syntax 面向不同的执行引擎,而无需改变查询结构。

### 核心概念

- **编译器转换**:查询表达式是语法糖,会被翻译成方法调用(例如 `where` 子句会变成 `Where()` 方法调用)。
- **语法选择**:简单操作通常更适合 method syntax,而涉及复杂 join、union 或需要中间变量的查询往往更适合 query syntax。
- **`let` 关键字**:query syntax 的一个特性,允许在查询内部创建局部变量。
  在 method syntax 中,必须借助 `Select` 调用中的匿名类型来复现这一点。
- **LINQ 可扩展性**:由于编译器是基于源类型来解析方法的,LINQ 可以扩展到 PLINQ、Entity Framework 或 Reactive Extensions 等各种 provider。
- **Parallel LINQ (PLINQ)**:通过在 `IEnumerable` 上标注 `AsParallel()`,编译器会解析到 Parallel LINQ 中定义的方法,而不是标准的 LINQ to Objects。

### 课程笔记

当 C# 编译器遇到查询表达式时,它会执行一次简单的转换。
`where` 子句被翻译成 `Where` 方法调用,`select` 子句被翻译成 `Select` 方法调用。
这些方法既可以是实例方法,也可以是扩展方法。
在更底层的 C# 代码层面,编译器为两种语法产生完全相同的结果,通常调用的是 `System.Linq` 中提供的扩展方法。

```csharp
int[] input = [1, 2, 3];
int sum = EvenNumbersQuery(input).Sum();
Console.WriteLine(sum);
sum = EvenNumbersMethodCalls(input).Sum();
Console.WriteLine(sum);

static IEnumerable<int> EvenNumbersQuery(int[] nums)
{
    return from n in nums
        where n % 2 == 0
        select n * 2;
}

static IEnumerable<int> EvenNumbersMethodCalls(int[] numbers)
{
    return numbers
        .Where(n => n % 2 == 0)
        .Select(n => n * 2);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/query-comprehensions-vs-method-calls-69958934/?t=10)

在两种语法之间做选择,往往是可读性的问题。
虽然在简单查询中 method syntax 通常更简洁,但当逻辑变得复杂时,query comprehension 语法的可读性明显更好。
例如,在计算各目录中文件的平均大小时,query syntax 允许使用 `let` 关键字来创建局部变量。
若要在 method syntax 中达到同样的效果,就必须用一次 `Select` 调用生成匿名类型,以把中间状态传递下去。

```csharp
static IEnumerable<(string Directory, double AvgBytes)> AverageDirectoryQuery(DirectoryInfo root)
{
    return from dir in root.EnumerateDirectories()
        let files = dir.GetFiles("*", SearchOption.AllDirectories)
        where files.Length > 0
        select (dir.Name, AvgBytes: files.Average(f => f.Length));
}

static IEnumerable<(string Directory, double AvgBytes)> AverageDirectoryMethodCalls(DirectoryInfo root)
{
    return root.EnumerateDirectories()
        .Select(dir => new { dir, files = dir.GetFiles("*", SearchOption.AllDirectories) })
        .Where(t => t.files.Length > 0)
        .Select(t => (t.dir.Name, AvgBytes: t.files.Average(f => f.Length)));
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/query-comprehensions-vs-method-calls-69958934/?t=55)

LINQ 具有很强的可扩展性,因为编译器并不要求 `Select` 和 `Where` 方法定义在任何特定位置;它只是在源类型上寻找拥有这些名字的方法。
这使得不同的 LINQ provider 成为可能。
例如,在一个 `IEnumerable` 上调用 `AsParallel()` 后,类型变成 `ParallelQuery`,编译器随后会把查询方法解析到为 Parallel LINQ(PLINQ)定义的那些方法上。

```csharp
public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
{
    ArgumentNullException.ThrowIfNull(source);
    ArgumentNullException.ThrowIfNull(predicate);

    return new WhereQueryOperator<TSource>(source, predicate);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/query-comprehensions-vs-method-calls-69958934/?t=100)

许多不同的 provider 都利用了这种可扩展性:

- **Entity Framework**:使用 `IQueryable` 和表达式树在运行时生成代码,把 LINQ 翻译成 SQL。
- **PLINQ**:并行处理数据。
- **ZLinq**:提供零分配的枚举。
- **AsyncEnumerable**:用于异步处理大量数据。
- **Reactive Extensions (Rx)**:使用 observable 实现推送式的流。

## 4. What Is IEnumerable&lt;T&gt;?

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-ienumerable-t-69958937/) · 0:50

### 总结

IEnumerable<T> 是 C# 中的一个基础接口,它让我们能够对某个特定类型的元素序列进行简单的迭代。
所有标准集合类型都实现了它,但它同时也是通过迭代器生成的无限序列,以及只在被消费时才执行的延迟查询的抽象。
区分这些实现非常重要,因为它们表现出的运行时特征和性能特征差别很大。

### 核心概念

* **正式定义**:该接口公开一个枚举器,支持对序列进行迭代。
* **内存中的集合**:数据已经存放在 RAM 中的实现(例如 `List<T>`、数组)。
* **生成器**:动态产生的序列,通常使用 `yield return` 关键字,并且有可能是无限的。
* **延迟查询**:代表一组操作(例如 LINQ 的 `Where` 子句)的序列,这些操作会在序列被枚举时执行。
* **性能差异**:`IEnumerable<T>` 的底层实现决定了它的内存占用和执行时机。

### 课程笔记

`IEnumerable<T>` 接口是 LINQ 的基础契约。
官方文档把它定义为一个"公开枚举器,该枚举器支持对指定类型的集合进行简单迭代"的接口。
尽管所有标准集合都实现了这个接口,但要认识到 `IEnumerable<T>` 并不总是代表一个存放在内存中的集合。

`IEnumerable<T>` 的实现主要有三种场景:

1. **内存中的集合**:接口指向已经在 RAM 中物化的数据,例如数组或列表。
2. **无限序列**:接口可以代表一个即时产生值的生成器,通常使用 `yield return` 模式。
3. **查询**:接口可以代表一个尚未执行的 LINQ 查询。
   只有当序列被消费时,其中的逻辑才会执行。

```csharp
// numbers is a collection in memory
IEnumerable<int> numbers = [1, 2, 3];

// numbers is an infinite sequence
IEnumerable<int> numbers = GenerateNumbers();

static IEnumerable<int> GenerateNumbers()
{
    while (true)
        yield return 42;
}

// numbers is a query
int[] input = [1, 2, 3];
IEnumerable<int> numbers = FilterIds(input);

static IEnumerable<int> FilterIds(int[] ids)
{
    return ids.Where(id => IsInDatabase(id));
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-ienumerable-t-69958937/?t=10)

这些不同的实现有着截然不同的运行时特征。
例如,已物化的集合有固定的内存开销但访问很快,而惰性查询可能内存占用很低,却在每次枚举时都要付出处理成本。

当一个 `IEnumerable<T>` 被多次枚举时,会出现明显的性能问题。
在下面的示例中,序列被枚举了一次以获取元素数量,又在 foreach 循环中被枚举了第二次。
如果这个序列是一个惰性查询,那么筛选或生成逻辑会执行两次。

```csharp
private static int ProcessData(IEnumerable<string> data)
{
    // First enumeration: calling Count()
    LogStartDataProcessing(data.Count());

    int processed = 0;

    // Second enumeration: foreach loop
    foreach (var item in data)
    {
        ProcessItem(item);
        processed++;
    }

    LogEndDataProcessing(processed);
    return processed;
}
```

为缓解这些问题,可以用 `.ToList()` 之类的方法把惰性的 `IEnumerable<T>` 物化成集合。
这样可以确保查询只执行一次,并把结果存放在内存中供后续使用。

## 5. Multiple Enumerations with IEnumerable&lt;T&gt;

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/) · 2:03

### 总结

多次枚举指的是对一个 IEnumerable<T> 遍历超过一次,如果底层数据源使用延迟执行,这会导致明显的性能下降。
像 List<T> 这样的内存集合能高效地应对多次枚举,而 LINQ 查询或其他惰性序列则会在每次迭代时重新求值其逻辑。
本课演示当输入没有被物化时,一次看似无害的 .Count() 调用加上随后的 foreach 循环,如何触发重复处理和过多的内存分配,由此强调理解 IEnumerable<T> 这一抽象背后实现的重要性。

### 核心概念

* **延迟执行**:LINQ 查询不是在定义时执行,而是在对结果 IEnumerable<T> 进行迭代时执行。
* **多次枚举**:对一个 IEnumerable<T> 遍历超过一次的行为,这可能导致底层查询逻辑被重新执行。
* **物化**:把惰性序列转换成内存集合(例如通过 .ToList() 或 .ToArray())以避免重复执行的过程。
* **抽象的风险**:IEnumerable<T> 只保证能够取到下一个元素;它并不保证迭代是廉价的,也不保证序列是有限的。

### 课程笔记

IEnumerable<T> 的难点在于它作为高层抽象的角色。
它提供了一种遍历序列的方式,但并不透露这些元素是如何产生的。
考虑一个标准的数据处理方法,它会记录输入的大小以及成功处理的元素数量。

```csharp
private static int ProcessData(IEnumerable<string> data)
{
    LogStartDataProcessing(data.Count());

    int processed = 0;

    foreach (var item in data)
    {
        ProcessItem(item);
        processed++;
    }

    LogEndDataProcessing(processed);
    return processed;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/?t=10)

在这个实现中,`data` 序列被枚举了两次:第一次是 `Count()` 扩展方法,第二次是 `foreach` 循环。
如果输入是像 `List<string>` 这样的具体集合,性能影响可以忽略不计。
`Count()` 方法针对实现了 `ICollection` 的集合做了优化,而且数据本来就在内存中。

```csharp
[MemoryDiagnoser]
[ShortRunJob]
[HideColumns(Column.Error, Column.StdDev, Column.Median, Column.RatioSD)]
public class LinqPerformanceChallenges
{
    private IEnumerable<string> _source = null!;

    [Params(100, 1_000, 10_000, 30_000)]
    public int N { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _source = Enumerable.Range(0, N).Select(i => i.ToString()).ToList();
    }

    [Benchmark]
    public int ProcessData()
        => ProcessData(_source);
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/?t=25)

然而,如果把数据源换成惰性的 LINQ 查询,性能特征会发生剧烈变化。
通过在基准测试中引入一个 `Lazy` 参数,我们可以基于数据源的实际类型来比较性能。

```csharp
[Params(true, false)]
public bool Lazy { get; set; }
[Params(100, 1_000, 10_000, 30_000)]
public int N { get; set; }

[GlobalSetup]
public void Setup()
{
    _source = Enumerable.Range(0, N).Select(i => i.ToString());
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/?t=70)

当数据源是惰性的时,查询会被执行多次,并即时产生结果。
对于 30,000 个元素的输入,仅仅为了处理这份输入就可能产生接近 2MB 的分配,因为这些字符串为计数生成了一次,又为循环生成了一次。

```csharp
private static int ProcessData(IEnumerable<string> data)
{
    LogStartDataProcessing(data.Count());

    int processed = 0;

    foreach (var item in data)
    {
        ProcessItem(item);
        processed++;
    }

    LogEndDataProcessing(processed);
    return processed;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/?t=85)

这一行为凸显了为什么 `IEnumerable<T>` 是一个有难度的抽象。
它只保证你可以请求下一个元素;它并没有规定 `MoveNext` 如何取到这个元素、迭代是否终会结束,或者数据源是否是一个无限序列。
如果数据源是一个查询,多次枚举可能引发严重的性能问题。

在处理异步任务时,延迟执行还会带来额外的复杂性。
在下面的示例中,对任务序列的多次枚举会导致任务被创建两次,进而产生逻辑错误:被等待的任务与被检查结果的任务并不是同一批任务。

```csharp
var ids = Enumerable.Range(1, 3);

IEnumerable<Task<int>> tasks = ProcessConcurrently(ids);
ProcessTasks(tasks);

static IEnumerable<Task<int>> ProcessConcurrently(IEnumerable<int> input)
{
    return input.Select(id => GetAsync(id));
}

static async Task<int> GetAsync(int id)
{
    Console.WriteLine($"  start {id}");
    await Task.Delay(10);
    return id * 2;
}

static void ProcessTasks(IEnumerable<Task<int>> tasks)
{
    Task.WaitAll(tasks.ToArray());
    foreach (var task in tasks)
    {
        Console.WriteLine(task.GetAwaiter().GetResult());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/multiple-enumerations-with-ienumerable-t-69958940/?t=118)

## 6. LINQ Deferred Execution Model

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/) · 3:24

LINQ 查询建立在迭代器块之上,遵循延迟执行模型,这意味着在序列被消费之前不会执行任何代码。
如果处理不当,这种惰性会引发严重的性能问题和逻辑问题,例如重复执行开销很大的筛选逻辑、重新启动异步任务,或者在遍历未物化序列的循环中使用 Count()、ElementAt() 之类的方法而引入平方级复杂度。

### 核心概念

- LINQ 查询是惰性的,通过迭代器块实现延迟执行。
- 只有当序列被消费时才会执行(例如通过 `Sum`、`foreach` 或 `ToList`)。
- 再次消费一个基于查询的 `IEnumerable` 会重新执行整个查询逻辑(双重枚举)。
- 在循环内不当使用 `Count()`、`ElementAt()` 之类的 `IEnumerable` 方法可能导致 $O(n^2)$ 的复杂度。
- 延迟执行可能造成逻辑上的副作用,例如每次迭代该可枚举对象时都会重新启动异步任务。

### 课程笔记

LINQ 建立在迭代器块之上,而迭代器块天生是惰性的。
当一个 LINQ 查询被定义时,实际上没有任何代码被执行;执行被推迟到序列被消费的时候。

```csharp
#pragma warning disable CS8321 // Local function is declared but never used

var numbers = Enumerable.Range(1, 5);

IEnumerable<int> sequence = GetSequence(numbers);
Console.WriteLine("Got the sequence");

Console.WriteLine(sequence.Sum());

IEnumerable<int> GetSequence(IEnumerable<int> numbers)
{
    return numbers.Where(n =>
    {
        Console.WriteLine($"Filtering {n}");
        return n % 2 == 0;
    });
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/?t=10)

在上面的示例中,"Got the sequence" 会在任何筛选发生之前被打印出来。
筛选逻辑只有在调用 `sequence.Sum()` 时才会运行,因为该调用消费了这个可枚举对象。

#### 双重枚举

如果一个序列被多次消费,底层的查询逻辑每次都会被重新执行。
这不仅是性能问题,也可能导致逻辑上的问题。

```csharp
#pragma warning disable CS8321 // Local function is declared but never used

var numbers = Enumerable.Range(1, 5);

IEnumerable<int> sequence = GetSequence(numbers);
Console.WriteLine("Got the sequence");

Console.WriteLine(sequence.Sum());
Console.WriteLine("Getting sum again");
Console.WriteLine(sequence.Sum());

IEnumerable<int> GetSequence(IEnumerable<int> numbers)
{
    return numbers.Where(n =>
    {
        Console.WriteLine($"Filtering {n}");
        return n % 2 == 0;
    });
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/?t=30)

#### 异步任务带来的逻辑问题

一个常见的实际问题出现在使用 `IEnumerable<Task>` 来并发处理数据时。
由于延迟执行,每次消费这个可枚举对象时,查询逻辑(这里是调用 `GetAsync` 的 `Select`)都会重新运行,实际上调度了新的操作。

```csharp
var ids = Enumerable.Range(1, 3);

IEnumerable<Task<int>> tasks = ProcessConcurrently(ids);
ProcessTasks(tasks);

static IEnumerable<Task<int>> ProcessConcurrently(IEnumerable<int> input)
{
    return input.Select(id => GetAsync(id));
}

static async Task<int> GetAsync(int id)
{
    Console.WriteLine($"  start {id}");
    await Task.Delay(10);
    return id * 2;
}

static void ProcessTasks(IEnumerable<Task<int>> tasks)
{
    Task.WaitAll(tasks);
    foreach (var task in tasks)
    {
        Console.WriteLine(task.GetAwaiter().GetResult());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/?t=45)

在 `ProcessTasks` 中,`Task.WaitAll(tasks)` 消费了一次可枚举对象,以取得需要等待的任务。
接着,`foreach` 循环第二次消费了这个可枚举对象。
由于这个可枚举对象是一个查询(`input.Select(...)`),第二次消费会再次调用 `GetAsync`,创建出全新的任务,而不是访问刚刚完成的那些任务的结果。

#### 性能挑战与平方级复杂度

当一个 `IEnumerable` 是查询而不是已物化的集合时,某些操作的复杂度是线性的($O(n)$),而不是人们对列表所期望的常数时间($O(1)$)。
这类操作包括 `Count()`、`ElementAt()` 和 `Last()`。
如果在遍历同一个可枚举对象的循环内部使用这些操作,循环的整体复杂度就变成平方级($O(n^2)$)。

```csharp
static void ProcessData(IEnumerable<string> data)
{
    for (int i = 0; i < data.Count(); i++)
    {
        var item = data.ElementAt(i);
        ProcessItem(item);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/?t=100)

即便在知名项目中也能找到这种模式。
例如,ASP.NET 仓库中的一个测试在传给某个校验方法的输入上使用了 `OrderBy`。
由于 `OrderBy` 的复杂度是 $O(n \log n)$,在遍历这个已排序可枚举对象的循环中调用 `Count()` 和 `ElementAt()`,使渐进复杂度变成了 $O(n^2 \log n)$。

```csharp
private void VerifySelectList(IEnumerable<SelectListItem> expected, IEnumerable<SelectListItem> actual)
{
    Assert.NotNull(actual);
    Assert.Equal(expected.Count(), actual.Count());
    for (var i = 0; i < actual.Count(); i++)
    {
        var expectedItem = expected.ElementAt(i);
        var actualItem = actual.ElementAt(i);
    }
}

// OrderBy is used because the order of the results may vary depending on the platform
VerifySelectList(
    expected.OrderBy(item => item.Text, StringComparer.Ordinal),
    result.OrderBy(item => item.Text, StringComparer.Ordinal));
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/linq-deferred-execution-model-69958943/?t=130)

## 7. Using Static Analysis to Avoid Multiple Enumerations

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/using-static-analysis-to-avoid-multiple-enumerations-69958948/) · 1:19

静态分析是维护性能敏感代码的有力工具。
通过启用特定的规则,开发者可以在编译期或在持续集成(CI)流水线中捕获对 `IEnumerable` 的多次枚举。

### 核心概念

- **规则 CA1851**:一条用于检测对 `IEnumerable` 序列多次枚举的静态分析规则。
- **.editorconfig 配置**:强制让诊断规则以警告形式暴露出来,即使它们默认是关闭的。
- **收窄接口**:使用 `IReadOnlyCollection<T>` 或 `IReadOnlyList<T>` 来保证集合已经被物化。
- **条件式物化**:借助扩展方法,只在序列还不是兼容的集合类型时才把它物化。
- **非枚举计数**:利用 `TryGetNonEnumeratedCount` 在不触发延迟执行的情况下安全地访问集合的元数据。

### 课程笔记

诊断规则 CA1851 专门用于检测多次枚举。
虽然在某些环境中它默认是关闭的,但可以在项目的 `.editorconfig` 文件中显式启用它,并把严重级别设为 warning。
这样可以确保潜在的性能问题在 IDE 中可见,并且能在自动化构建过程中被捕获。

```ini
# Force CA1851 to surface in this project, even though it ships disabled by default.
root = false

[*.cs]
dotnet_diagnostic.CA1851.severity = warning
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-static-analysis-to-avoid-multiple-enumerations-69958948/?t=10)

当这条规则生效时,任何对 `IEnumerable` 枚举超过一次的代码都会被标记出来。
一个常见的例子是:在 `IEnumerable` 上调用 `.Count()` 来记录它的大小,随后又用 `foreach` 循环遍历同一个对象。
这会导致底层序列被求值两次,如果这个 `IEnumerable` 代表一个复杂的 LINQ 查询或一次数据库调用,代价会很高。

```csharp
Console.WriteLine("Demo");
static class TheBenchmark
{
    public static int ProcessData(IEnumerable<string> data)
    {
        // First enumeration: Count()
        LogStartDataProcessing(data.Count());

        int processed = 0;

        // Second enumeration: foreach
        foreach (var item in data)
        {
            ProcessItem(item);
            processed++;
        }

        LogEndDataProcessing(processed);
        return processed;
    }

    private static void LogStartDataProcessing(int count) { }
    private static void LogEndDataProcessing(int count) { }
    private static void ProcessItem(string value) { }
}

static class EnumerableExtensions
{
    public static IReadOnlyList<T> ToReadOnlyListIfNeeded<T>(this IEnumerable<T> source) =>
        source as IReadOnlyList<T> ?? source.ToArray();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-static-analysis-to-avoid-multiple-enumerations-69958948/?t=25)

要消除这些警告,可以根据上下文采用以下几种策略:

1. **收窄输入类型**:对于私有或内部方法,把参数类型从 `IEnumerable<T>` 改成 `IReadOnlyList<T>` 这类更具体的接口,可以确保集合已经被物化,从而能够多次计数或迭代而不带来性能损失。
2. **条件式物化**:不要无条件调用 `.ToList()` 或 `.ToArray()`(那可能会克隆一个已有的集合),而是使用像 `ToReadOnlyListIfNeeded` 这样的辅助扩展方法。
   该方法会检查数据源是否已经实现了 `IReadOnlyList<T>`,如果是就直接返回它;否则把序列物化成数组。
3. **非枚举计数**:在是否知道元素数量并非必需的场景中(例如用于日志),使用 `System.Linq` 中的 `TryGetNonEnumeratedCount` 方法。
   该方法通过检查底层类型是否是集合(例如 `List<T>` 或 `Array`),尝试在不触发完整枚举的情况下取得数量。

```csharp
Console.WriteLine("Demo");
static class TheBenchmark
{
    public static int ProcessData(IEnumerable<string> data)
    {
        LogStartDataProcessing(data);

        int processed = 0;

        foreach (var item in data)
        {
            ProcessItem(item);
            processed++;
        }

        LogEndDataProcessing(processed);
        return processed;
    }

    private static void LogStartDataProcessing(IEnumerable<string> data)
    {
        if (data.TryGetNonEnumeratedCount(out int count))
        {
            // Log the exact count
        }

        // Log without the exact count.
    }
    private static void LogEndDataProcessing(int count) { }
    private static void ProcessItem(string value) { }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-static-analysis-to-avoid-multiple-enumerations-69958948/?t=70)

使用 `TryGetNonEnumeratedCount` 让代码保持高效:它只访问底层集合类型的 count 属性,同时避免了仅为获取元数据就执行延迟 LINQ 查询所带来的副作用。

## 8. Summary

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958949/) · 1:20

### 总结

本课为 LINQ over IEnumerable 的探讨作结,强调 query 语法与 method 语法之间的区别、IEnumerable 实现的多样性,以及延迟执行对性能的影响。
它指出了把 IEnumerable 当作已物化集合来假设的风险,并给出了在公有 API 中管理复杂度与副作用的策略,例如通过物化来避免重复枚举。

### 核心概念

*   **语法糖**:query 语法与 method invocation 语法是同一套底层逻辑的可互换形式。
*   **IEnumerable 抽象**:一个 `IEnumerable` 可以代表内存中的集合、无限序列或延迟查询。
*   **复杂度风险**:如果在未物化的数据源上于循环内使用 `Count()` 或 `ElementAt()` 之类的方法,把 `IEnumerable` 当作集合来对待会导致平方级复杂度($O(N^2)$)。
*   **公有 API 设计**:`IEnumerable` 的运行时行为由调用方而不是实现方控制,这让它在面向外部的方法中很难驾驭。
*   **物化**:把查询转换成具体集合(例如 `ToList()` 或 `ToArray()`)可以保证性能可预测,并避免多次遍历时产生副作用。

### 课程笔记

LINQ over `IEnumerable` 提供了一种灵活的数据查询方式,让开发者可以借助扩展方法在不同的 provider 之间切换。
尽管这套库提供了两种主要语法 - query 语法和 method invocation 语法 - 但它们本质上都是语法糖。
编译器会把查询表达式翻译成方法调用,而在两者之间的选择通常取决于具体的使用场景或对可读性的偏好。

```csharp
static IEnumerable<int> EvenNumbersQuery(int[] nums)
{
    return from n in nums
        where n % 2 == 0
        select n * 2;
}

static IEnumerable<int> EvenNumbersMethodCalls(int[] numbers)
{
    return numbers
        .Where(n => n % 2 == 0)    
        .Select(n => n * 2);
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958949/?t=8)

`IEnumerable` 接口非常通用,可以代表多种底层结构,包括内存中的集合、无限序列或延迟查询。
使用 `IEnumerable` 时,关键是不要对底层类型做任何假设。
开发者通常应当假定自己面对的是一个查询,而不是已物化的集合。
否则可能引发严重的性能问题;例如在循环内调用 `Count()`、`ElementAt()` 或 `Last()` 这类扩展方法,会不经意地造成平方级复杂度。

```csharp
private static int ProcessData(IEnumerable<string> data)
{
    LogStartDataProcessing(data.Count());

    int processed = 0;

    foreach (var item in data)
    {
        ProcessItem(item);
        processed++;
    }

    LogEndDataProcessing(processed);
    return processed;
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958949/?t=41)

在公有 API 中,`IEnumerable` 尤其难以驾驭,因为方法的运行时特性不由实现方控制,而由调用方控制。
如果实现只需要对数据源做一次遍历,那么 `IEnumerable` 是合适的。
但如果实现需要多次遍历,或者使用了假定随机访问的操作,那么更好的做法是把参数类型改成更具体的集合类型,或者按需物化集合,以避免意外的副作用和重复执行。

```csharp
static class EnumerableExtensions
{
    public static IReadOnlyList<T> ToReadOnlyListIfNeeded<T>(this IEnumerable<T> source) =>
        source as IReadOnlyList<T> ?? source.ToArray();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/summary-69958949/?t=73)

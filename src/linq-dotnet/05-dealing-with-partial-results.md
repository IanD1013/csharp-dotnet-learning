# Dealing with partial results

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 5 章
> 共 5 课 · 约 25:12
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to getting partial results](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-getting-partial-results-54280365/) | 2:57 | [↓](#1-introduction-to-getting-partial-results) |
| 2 | [Splitting results into chunks](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/splitting-results-into-chunks-54280367/) | 6:20 | [↓](#2-splitting-results-into-chunks) |
| 3 | [Using Skip, Take and TakeWhile to retrieve a specific result](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/) | 7:30 | [↓](#3-using-skip-take-and-takewhile-to-retrieve-a-specific-result) |
| 4 | [Dividing results into logical parts with GroupBy](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/) | 7:30 | [↓](#4-dividing-results-into-logical-parts-with-groupby) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280370/) | 0:55 | [↓](#5-section-recap) |

## 1. Introduction to getting partial results

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-getting-partial-results-54280365/) · 2:57

### 总结

本课介绍从 LINQ 查询中获取部分结果的概念框架,重点关注分页和数据分块等场景。
它讲解了如何通过跳过和获取元素来分离出数据的特定片段,以及如何按共同属性对结果进行分组,从而以逻辑批次处理数据,类似于 SQL 的分组操作。

### 核心概念

- 为表格(grid)等 UI 组件进行分页和数据分块。
- 跳过特定数量的元素以到达目标片段的逻辑。
- 获取指定数量的元素以构成一个批次或一页。
- 按属性对结果分组,以逻辑集合的方式处理数据(SQL 风格的分组)。
- 在由 API 支撑的场景中高效处理部分数据的获取。

### 课程笔记

在许多软件应用中,需要分段处理查询结果,而不是把它当作一个单一的整体集合。
这在实现分页的用户界面表格时很常见,或者当应用只关心具有特定属性的元素时也是如此。
虽然这些模式可以通过手动迭代来实现,但 LINQ 提供了内置的解决方案,能更高效地处理这些场景。

一个主要的用例是分页。
例如,如果一个表格每页显示六个元素,应用就会以六个为一块来处理数据。
其逻辑是按顺序从一块跳到下一块,以填充界面。

在页面表格由 API 支撑的场景中,范围通常限于一次 API 调用,用于获取数据的一个特定片段。
从概念上讲,这是通过跳过特定数量的元素(X),然后获取特定数量的元素(Y)来实现的。
这样就能从整个数据集中获取一个小而有针对性的批次,而无需把不必要的记录加载到内存中。

```text
  全部数据集
+-------------------+-------------------+------------------------+
|   跳过 X 个元素   |  获取 Y 个元素    |        其余元素        |
+-------------------+-------------------+------------------------+
                    \_________________/
                       一次 API 调用
```

另一种处理部分结果的方法是分组。
与 SQL 中的 GROUP BY 子句类似,LINQ 允许你按特定属性对元素进行分组。
从概念上讲,可以把它想象成在一条时间线上按颜色(例如紫色、黄色和粉色)对元素进行分组。
这样就能以逻辑集合的形式获取结果,让应用可以从一个组跳到另一个组,把每个组当作整个结果集中一个独立的块。

## 2. Splitting results into chunks

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/splitting-results-into-chunks-54280367/) · 6:20

### 总结

LINQ 中的 Chunk 方法允许开发者把一个集合拆分成更小的、大小相等的数组,这对于批处理或分页显示特别有用。
本课演示如何使用 Chunk 对数据进行分区,并介绍 LINQ 扩展方法(例如 Select)中感知索引的重载,用于跟踪元素在序列中的位置。
借助这些重载,开发者可以很容易地为数据附加块编号之类的元数据,而无需维护外部计数器变量。

### 核心概念

*   **Chunk(size)**:把一个 `IEnumerable<T>` 拆分成一个数组序列(`IEnumerable<T[]>`),其中每个数组的最大长度为指定的大小。
*   **不完整的块(Partial Chunks)**:如果源集合的总数不能被块大小整除,最后一个数组只会包含剩余的元素。
*   **感知索引的重载(Index-aware Overloads)**:许多 LINQ 扩展方法(包括 `Select` 和 `Where`)都提供一个重载,接受带两个参数的 lambda:元素本身及其从零开始的索引。
*   **仅限方法语法**:`Chunk` 运算符只能通过方法语法使用;在 LINQ 查询语法中没有对应的关键字。

### 课程笔记

为了把一个集合划分成大小相等的片段,LINQ 提供了 `Chunk` 方法。
在 `IEnumerable<T>` 上调用它时,它会返回一个 `IEnumerable<T[]>`。
每个内部数组表示数据的一个"块"。
这适用于需要按特定大小分批处理元素的场景。

在下面的示例中,一个电影查询被拆分成每块五个元素。
由于返回类型是一个数组序列,你必须使用嵌套循环来遍历结果:外层循环遍历各个块,内层循环遍历每个数组中的元素。

```csharp
/// Split the source query into equally sized chunks ...
void SimpleChunks()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var chunks = query.Chunk(5);

    foreach (var chunk in chunks)
    {
        Console.WriteLine("CHUNK:");
        foreach (var movie in chunk)
        {
            Console.WriteLine(movie);
        }
        Console.WriteLine(string.Empty);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/splitting-results-into-chunks-54280367/?t=110)

如果元素总数不是块大小的倍数(例如 13 个元素拆分成每块 5 个),最后一块只会包含剩余的元素(在这个例子中是 3 个)。

```text
query (13 items) --Chunk(5)--> IEnumerable<T[]>
  [ 5 items ]  [ 5 items ]  [ 3 items ]
```

#### 使用感知索引的 Lambda

你常常会希望在处理各个块时跟踪它们的索引,例如为输出编号。
虽然可以维护一个外部计数器变量,但 LINQ 提供了一种更简洁的方式,即使用暴露元素索引的重载。

通过使用 `Select((item, index) => ...)` 签名,你可以访问每个块从零开始的索引。
这个索引可以用来创建一个匿名类型,同时保存块数据及其计算出的编号。
这种模式也存在于 `Where` 等其他 LINQ 方法中,可以实现诸如使用取模运算符筛选每隔一个或每隔两个元素之类的逻辑。

```csharp
void ChunksWithNumbers()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var chunks = query.Chunk(5)
        .Select((chunk, index) => new { Movies = chunk, Number = index + 1 });

    foreach (var chunk in chunks)
    {
        Console.WriteLine($"CHUNK {chunk.Number}:");
        foreach (var movie in chunk.Movies)
        {
            Console.WriteLine(movie);
        }
        Console.WriteLine(string.Empty);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/splitting-results-into-chunks-54280367/?t=305)

## 3. Using Skip, Take and TakeWhile to retrieve a specific result

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/) · 7:30

### 总结

本课讲解如何使用 LINQ 的 `Skip` 和 `Take` 方法获取集合的特定片段。
内容涵盖基于数量的方法(如 `Take` 和 `TakeLast`)、基于谓词的方法(如 `TakeWhile` 和 `SkipWhile`),以及组合 `Skip` 和 `Take` 实现分页的实际应用。
本课强调这些方法是"尽力而为"的,也就是说,当请求的元素数量超过集合大小时它们不会抛出异常,这使它们非常适合构建带有分页或无限加载数据的健壮 API 和用户界面。

### 核心概念

*   `Take(n)` 和 `TakeLast(n)`:从序列的开头或末尾获取指定数量的元素。
*   `TakeWhile(predicate)`:只要满足指定条件,就持续获取元素。
*   `Skip(n)` 和 `SkipLast(n)`:从序列的开头或末尾跳过指定数量的元素。
*   `SkipWhile(predicate)`:只要满足指定条件,就持续跳过元素。
*   分页:组合 `Skip` 和 `Take` 来获取特定的"一页"或一块数据。
*   尽力而为的执行:当请求的数量或跳过的数量超过集合大小时,这些方法不会抛出异常。

### 课程笔记

在使用 LINQ 查询时,常常只需要获取结果的一部分,例如为 UI 预览获取前几个元素,或者为 API 响应获取特定的一页数据。
LINQ 提供了多个方法来高效地处理这些场景。

#### 获取结果

`Take` 方法用于从集合开头获取指定数量的元素。
例如,获取前五部拥有不止一位制片人的电影:

```csharp
void TakeFirstItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var result = query.Take(5);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=55)

类似地,`TakeLast` 从集合末尾获取指定数量的元素:

```csharp
void TakeLastItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var result = query.TakeLast(5);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=85)

对于更动态的需求,`TakeWhile` 接受一个谓词,只要条件保持为真就持续获取元素。
这对于已经排好序的序列很有用,例如获取 MCU 前三个阶段(phase)的所有电影:

```csharp
void TakeMatchingItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var result = query.TakeWhile(movie => movie.Phase <= 3);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=180)

#### 跳过结果

跳过类方法允许你绕过集合的一部分。
`Skip` 会忽略从开头起指定数量的元素:

```csharp
void SkipFirstItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var result = query.Skip(5);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=230)

`SkipLast` 会绕过序列末尾的元素:

```csharp
void SkipLastItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var result = query.SkipLast(5);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=270)

`SkipWhile` 只要条件为真就持续绕过元素,一旦条件不成立,就返回集合的剩余部分:

```csharp
void SkipMatchingItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    var result = query.SkipWhile(movie => movie.Phase <= 3);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=310)

```text
query:  [ Phase <= 3 ... ][ remainder ... ]
        \________________/\_______________/
        TakeWhile(...)      SkipWhile(...)
```

#### 组合 Skip 和 Take 实现分页

这些方法最常见的用例是组合 `Skip` 和 `Take` 来获取特定的一块数据。
这是在 API 或无限加载的表格中实现分页的标准做法。
例如,要获取每页包含五个元素的第三"页",你需要跳过前 10 个元素,然后获取接下来的 5 个:

```csharp
void GetChunkUsingSkipAndTake()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        select movie;

    // Retrieve the third chunk of 5 items
    var result = query.Skip(10).Take(5);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-skip-take-and-takewhile-to-retrieve-a-specific-result-54280368/?t=400)

```text
items:  [ 1 .. 5 ][ 6 .. 10 ][ 11 .. 15 ][ ... ]
        \___________________/\__________/
              Skip(10)          Take(5)
```

即使集合比请求跳过或获取的数量还小,这些方法也可以安全使用。
LINQ 执行的是"尽力而为"的操作;如果你试图跳过比实际存在更多的元素,它会返回一个空集合,而不是抛出异常。

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
using ...

namespace Dometrain.Linq.Cmd;

public abstract class QueryRunner
{
    protected readonly MarvelMovieRepository Repository = new();

    public abstract void Run();

    protected void Print<T>(T objectToPrint)
    {
        Console.WriteLine(objectToPrint);
    }

    protected void PrintAll<T>(IEnumerable<T> objectsToPrint)
    {
        foreach (var objectToPrint in objectsToPrint)
        {
            Console.WriteLine(objectToPrint);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=85)

## 4. Dividing results into logical parts with GroupBy

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/) · 7:30

### 总结

LINQ 的 GroupBy 运算符把一个序列组织成一组分组的集合,其中每个分组由一个 IGrouping<TKey, TElement> 对象表示。
与 SQL 不同,LINQ 的 GroupBy 会保留每个分组中的各个元素,既可以进行聚合操作,也可以直接访问分组后的元素。
开发者可以使用带有 'group...by...into' 关键字的查询语法,也可以通过 GroupBy 方法使用流式(fluent)语法。
两种方式都支持为分组的元素选择特定属性,并使用 Key 属性在分组后进行筛选。

### 核心概念

*   **IGrouping<TKey, TElement>**:分组操作的返回类型,表示共享同一个键的对象集合。
*   **Key 属性**:`IGrouping` 接口上的一个属性,保存用于对元素进行归类的值。
*   **查询语法**:使用 `group [element] by [key] into [alias]` 模式。
*   **流式语法**:使用 `GroupBy(keySelector)` 或 `GroupBy(keySelector, elementSelector)` 方法重载。
*   **元素选择**:可以把特定属性投影到分组中,而不是整个源对象。
*   **分组后筛选**:对分组结果(即 `IGrouping` 对象)而不是源元素应用 `where` 子句。

### 课程笔记

在 LINQ 中,分组比在 SQL 中更灵活。
SQL 分组通常只允许你使用聚合函数或被分组的字段,而 LINQ 允许你保留每个分组中的各个元素,以便进一步处理或遍历。

#### 使用查询语法分组

要在查询语法中对元素分组,使用 `group` 关键字。
你需要指定要分组的元素、分组依据的属性,并使用 `into` 关键字为得到的分组指定一个别名。
这个别名让你可以把分组本身选为结果。

```csharp
/// Grouped query results, query syntax ...
private void GroupedResults_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        group movie by movie.Phase into phase
        select phase;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/?t=85)

这个查询的结果是一个 `IEnumerable<IGrouping<int, Movie>>`。
每个 `IGrouping` 都有一个 `Key`(即 Phase 整数),并且作为该阶段电影的一个 `IEnumerable`。

```text
IEnumerable<IGrouping<int, Movie>>
 +- IGrouping  Key = Phase (int) --> Movie, Movie, ...
 +- IGrouping  Key = Phase (int) --> Movie, Movie, ...
 +- ...
```

#### 筛选与键

你可以在分组创建之后对它们应用筛选。
通过访问分组别名的 `Key` 属性,你可以基于分组条件筛选结果。

```csharp
/// Grouped query results, query syntax ...
private void GroupedResults_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        group movie by movie.Phase into phase
        where phase.Key > 2
        select phase;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/?t=160)

#### 遍历分组结果

由于结果是一个由集合组成的集合,你需要使用嵌套循环来遍历数据。
外层循环遍历各个分组(阶段),内层循环遍历每个分组中的元素(电影)。

```csharp
foreach (var phase in query)
{
    Console.WriteLine($"PHASE {phase.Key}:");
    foreach (var movie in phase)
    {
        Console.WriteLine(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/?t=220)

#### 元素选择

你不必对整个对象进行分组。
你可以指定一个属性或一个转换,作为分组中的元素。
例如,只对电影名称进行分组,会把结果类型变为 `IEnumerable<IGrouping<int, string>>`。

```csharp
var query =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        group movie.Name by movie.Phase into phase
        where phase.Key > 2
        select phase;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/?t=260)

#### 使用流式语法分组

在流式语法中,使用的是 `GroupBy` 方法。
最简单的重载接受一个键选择器。
要对分组进行筛选,在 `.GroupBy()` 之后链式调用 `.Where()`。

```csharp
var groupedQuery = sourceMovies
        .Where(movie => movie.Producers.Count > 1)
        .GroupBy(movie => movie.Phase)
        .Where(phase => phase.Key > 2);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/?t=385)

要在流式语法中进行元素选择,使用同时接受键选择器和元素选择器的重载。

```csharp
var groupedQuery = sourceMovies
        .Where(movie => movie.Producers.Count > 1)
        .GroupBy(
            movie => movie.Phase,
            movie => movie.Name)
        .Where(phase => phase.Key > 2);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/dividing-results-into-logical-parts-with-groupby-54280369/?t=415)

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280370/) · 0:55

### 总结

本课总结了在 LINQ 中处理部分结果的技巧,重点关注分块、分页和分组。
这些方法让开发者能够精确控制要获取和处理的数据,这在使用数据库等外部数据源时对性能特别有益。

### 核心概念

- **分块**:把一个集合划分成更小的、固定大小的片段,以便增量处理。
- **分页**:使用 `Skip` 和 `Take` 实现数据获取模式,以取得集合的特定子集。
- **分组**:基于共同属性或自定义逻辑把数据组织成不同类别。
- **数据效率**:只从外部数据源获取必要的数据,从而优化网络和内存的使用。

### 课程笔记

LINQ 提供了多种管理部分结果的策略,这些策略对于构建可扩展且高效的应用至关重要。
这些策略让你可以处理大型数据集,而无需一次性把整个集合加载到内存中。

一种方法是分块,即把一个查询拆分成离散的块。
这使应用可以一次处理一个块,非常适合后台任务或批处理操作。

分页是另一种核心技巧。
通过跳过一定数量的元素并获取特定数量的元素,开发者可以实现高效的数据浏览。
这确保了只获取用户或流程当前需要的数据。

分组提供了一种基于特定条件组织数据的方式。
通过把元素组织成分组,你可以对相关的数据子集执行操作,使逻辑更具表达力,数据也更易于管理。

能够明确表达数据需求,是 LINQ 最强大的特性之一。
当通过网络拉取数据时(例如从数据库),这些运算符确保只传输所需的块或分组。
这种精确性减少了开销,并提升了数据管道的整体性能。

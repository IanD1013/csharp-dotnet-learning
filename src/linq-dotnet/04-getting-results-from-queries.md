# Getting results from queries

> 课程：[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 4 章
> 共 5 课 · 约 42:06
> 来源：Dometrain。由课程文档翻译整理；每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is deferred execution in LINQ?](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/) | 9:33 | [↓](#1-what-is-deferred-execution-in-linq) |
| 2 | [Retrieving all results](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/) | 9:48 | [↓](#2-retrieving-all-results) |
| 3 | [Retrieving a single item](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/) | 10:40 | [↓](#3-retrieving-a-single-item) |
| 4 | [Projecting the data to another shape](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/) | 11:22 | [↓](#4-projecting-the-data-to-another-shape) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280339/) | 0:43 | [↓](#5-section-recap) |

## 1. What is deferred execution in LINQ?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/) · 9:33

### 总结

延迟执行（deferred execution）是 LINQ 的一项核心原则：查询的执行会被推迟到真正需要数据的那一刻。
LINQ 查询对象在定义时并不会立即执行，而是保存检索数据所需的逻辑。
只有在对查询进行迭代、将其物化为集合（例如 List 或 Array），或请求某个特定的单个元素时，才会触发执行。
这使得资源可以被高效利用，并且能够逐步组合复杂查询，而不会产生中间处理的开销。

### 核心概念

- LINQ 查询不会在定义处执行；它们以 `IEnumerable<T>` 的形式保存。
- 使用 `foreach` 循环迭代结果会触发执行。
- `.ToList()`、`.ToArray()` 或 `.ToDictionary()` 等物化方法会强制查询立即执行。
- 使用 `.First()` 等方法检索单个元素时，只会执行到找到第一个匹配元素为止。
- 查询可以被组合或扩展而不触发执行，直到请求最终结果为止。

### 课程笔记

延迟执行保证 LINQ 会尽可能久地等待，直到别无选择时才运行任何代码。
LINQ 查询只有在显式请求结果时才会被执行。

#### 通过迭代执行

定义查询时，它通常是 `IEnumerable<T>` 类型。
在集合被迭代之前（例如在 `foreach` 循环中），查询逻辑不会执行。

```csharp
private void SimpleQueryWithForeach()
{
    var sourceMovies = Repository.GetAllMovies();

    // This defines the LINQ query
    var query =
        from movie in sourceMovies
        where IsSpiderManMovie(movie)
        select movie;

    // Triggering the iterator will start execution
    foreach (var movie in query)
    {
        Console.WriteLine(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/?t=45)

在这个例子中，如果在 `IsSpiderManMovie` 方法内部设置断点，给 `query` 变量赋值时并不会命中断点。
只有当 `foreach` 循环开始请求元素时，断点才会被命中。

#### 物化为集合

可以通过把查询结果物化为具体的集合类型来触发执行。
在 `IEnumerable` 查询上调用 `.ToList()` 会让整个查询立即执行，并填充一个 `List<T>`。

```csharp
private void SimpleQueryWithToList()
{
    var sourceMovies = Repository.GetAllMovies();

    // This defines the LINQ query
    var query =
        from movie in sourceMovies
        where IsSpiderManMovie(movie)
        select movie;

    // Materializing to a collection will execute the query
    var result = query.ToList();

    foreach (var movie in result)
    {
        Console.WriteLine(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/?t=205)

也可以在定义处直接进行物化：

```csharp
private void SimpleQueryWithImmediateToList()
{
    var sourceMovies = Repository.GetAllMovies();

    // This defines the LINQ query,
    // and immediately executes it by materializing to a collection
    var result =
        (from movie in sourceMovies
        where IsSpiderManMovie(movie)
        select movie).ToList();

    foreach (var movie in result)
    {
        Console.WriteLine(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/?t=295)

#### 检索单个元素

从查询中检索单个元素同样会触发执行。
不过，`.First()` 这类方法是高效的，因为它们只会执行查询逻辑直到找到第一个匹配元素为止。

```csharp
private void SimpleQueryWithFirst()
{
    var sourceMovies = Repository.GetAllMovies();

    // This defines the LINQ query
    var query =
        from movie in sourceMovies
        where IsSpiderManMovie(movie)
        select movie;

    // Retrieving a single result also triggers execution
    var result = query.First();

    Console.WriteLine(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/?t=405)

#### 查询的组合与扩展

延迟执行的主要好处之一，是能够在查询之上继续构建查询。
你可以通过添加更多子句来细化查询，而不会执行底层逻辑。
整个链条只有在最终细化后的查询被迭代时才会执行。

```csharp
private void ExtendedWithForeach()
{
    var sourceMovies = Repository.GetAllMovies();

    // This defines a LINQ query
    var query =
        from movie in sourceMovies
        where IsSpiderManMovie(movie)
        select movie;

    // Extending the query does not trigger execution
    var refinedQuery =
        from movie in query
        where movie.ReleaseDate.Year < 2020
        select movie;

    // Triggering the iterator will start execution
    foreach (var movie in refinedQuery)
    {
        Console.WriteLine(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-is-deferred-execution-in-linq-54280335/?t=475)

在这个场景中，对 `refinedQuery` 的 `foreach` 循环触发了执行。
它向 `refinedQuery` 请求一个元素，而 `refinedQuery` 又转而向 `query` 请求一个元素。
`IsSpiderManMovie` 内部的逻辑只会随着迭代的推进而被调用。

```text
foreach (var movie in refinedQuery)
        |
        | 请求一个元素
        v
  refinedQuery   (where movie.ReleaseDate.Year < 2020)
        |
        | 请求一个元素
        v
     query       (where IsSpiderManMovie(movie))
        |
        v
  sourceMovies
```

```csharp
private static bool IsSpiderManMovie(Movie movie)
{
    return movie.Name.Contains("Spider-Man");
}
```

## 2. Retrieving all results

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/) · 9:48

### 总结

本课探讨从 LINQ 查询中检索并物化结果的各种方法。
虽然用 foreach 循环迭代查询是触发延迟执行最直接的方式，但 LINQ 还提供了一整套扩展方法，用于把结果物化为特定的集合类型，例如列表、数组和字典。
这些方法对于结束延迟执行至关重要，使数据能够以具体集合而非待执行查询的形式在整个代码库中传递。

### 核心概念

- **Deferred Execution（延迟执行）**：查询在被迭代或物化之前不会执行。
- **Interface Preservation（接口保留）**：LINQ 通常会保留输入类型最接近的接口（例如 `IEnumerable<T>` 或 `IQueryable<T>`）。
- **Materialization（物化）**：执行查询并把结果存储到 `List<T>` 或 `T[]` 这类集合中的过程。
- **Collection Types（集合类型）**：支持 `ToList`、`ToArray`、`ToDictionary`、`ToHashSet`、`ToLookup` 以及不可变（immutable）/冻结（frozen）集合。
- **Dictionary Mapping（字典映射）**：物化为字典时，使用 lambda 表达式来定义键选择器和值选择器。

### 课程笔记

使用 LINQ 时最常见的场景，是检索查询的全部结果。
这可以通过直接迭代查询，或者把它物化为特定的集合类型来实现。

#### 迭代结果

定义 LINQ 查询时，得到的对象通常是 `IEnumerable<T>`。
你可以用 `foreach` 循环迭代这个结果，这会触发延迟执行，并让你按顺序处理每一个元素。

```csharp
/// Just iterate over an IEnumerable to get the results.
private void LoopOverResults()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(IsSpiderManMovie);

    foreach (var movie in query)
    {
        Console.WriteLine(movie);
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/?t=85)

#### IEnumerable 与 IQueryable

LINQ 在定义查询时会保留输入类型，但会把它投射到最接近的接口。
如果数据源是泛型列表，查询结果就是 `IEnumerable<T>`。
然而，如果数据源是 `IQueryable<T>`（例如 Entity Framework 中的 DBSet），LINQ 就会返回 `IQueryable<T>`。
由于 `IQueryable<T>` 实现了 `IEnumerable<T>`，它同样可以被直接迭代。

```csharp
/// Input interface = output interface.
private void LoopOverQueryableResults()
{
    var sourceMovies = Repository.GetAllMoviesAsQueryable();

    var query = sourceMovies
        .Where(movie => IsSpiderManMovie(movie));

    foreach (var movie in query)
    {
        Console.WriteLine(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/?t=185)

#### 物化为集合

迭代并不总是处理结果最理想的方式。
LINQ 提供了扩展方法，可以把查询物化为集合。
这会立即执行查询并把元素存储在内存中，当你需要把数据传递给应用程序的其他部分时，这很有用。

要把查询物化为泛型列表，请使用 `ToList()` 方法：

```csharp
/// Materializing to a collection type is pretty common too.
private void ResultAsGenericList()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(IsSpiderManMovie);

    var result = query.ToList();

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/?t=265)

类似地，你可以用 `ToArray()` 把查询物化为数组：

```csharp
/// Apart from lists, there are many collection types to choose from.
private void ResultAsArray()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(IsSpiderManMovie);

    var result = query.ToArray();

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/?t=330)

LINQ 还支持物化为其他类型，包括 `HashSet`、`Lookup` 以及各种不可变或冻结集合。

#### 物化为字典

把查询转换为字典比转换为列表或数组需要更多信息，因为你必须为每个条目同时指定键和值。
这是通过两个 lambda 表达式完成的。
虽然编译器通常可以推断出泛型类型，但方法签名涉及输入类型、键类型和值类型。

```csharp
/// Getting the results out as a dictionary is just a tiny bit different.
private void ResultAsDictionary()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(IsSpiderManMovie);

    var result = query.ToDictionary<Movie, Guid, string>(
        movie => movie.MovieId,
        movie => movie.Name);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/?t=505)

物化之后，你就可以用标准的集合模式与字典交互，例如遍历键来取得对应的值。

```csharp
    var result = query.ToDictionary(
        movie => movie.MovieId,
        movie => movie.Name);

    foreach (var movieId in result.Keys)
    {
        Console.WriteLine(result[movieId]);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-all-results-54280336/?t=550)

## 3. Retrieving a single item

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/) · 10:40

### 总结

本课探讨用于从集合中检索单个元素的 LINQ 方法，具体是 First、Last 和 Single。
它详细说明了这些方法之间的区别、用于在缺少元素时不抛出异常的 'OrDefault' 变体，以及使用谓词重载（predicate overload）让 LINQ 优化迭代所带来的性能优势，例如对列表进行反向索引。

### 核心概念

- **First, Last, and Single**：根据位置或唯一性提取单个元素的核心方法。
- **Predicate Overloads（谓词重载）**：直接接受 lambda 的扩展方法，通常比链式调用 `.Where().First()` 更高效。
- **OrDefault Variants（OrDefault 变体）**：返回 `default(T)`（例如引用类型返回 `null`）而不是抛出 `Sequence contains no elements` 异常的方法。
- **Efficiency and Optimization（效率与优化）**：LINQ 如何针对 `IList` 数据源优化 `Last()`，以及 `Single()` 为何必须校验整个序列。
- **Exception Handling（异常处理）**：理解 LINQ 何时会因为空序列或多个匹配而抛出异常。

### 课程笔记

虽然 LINQ 查询通常返回集合，但你也经常只需要检索单个元素。
这些操作是通过扩展方法完成的；单元素检索没有专门的查询语法替代写法。
如果你使用查询语法，就必须把查询包起来，或者在表达式末尾追加这些扩展方法。

#### First 方法

`First()` 方法返回序列中第一个满足条件的元素。
如果没有提供谓词，它就直接返回集合中的第一个元素。

```csharp
/// Get the first matching item ...
void GetFirstItem()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(movie => movie.Name.StartsWith("Spider-Man"));

    var result = query.First();

    Print(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/?t=55)

#### 谓词重载与效率

虽然你可以链式调用 `.Where(predicate).First()`，但大多数 IDE 都会建议把它重构为一次调用：`.First(predicate)`。
所有单元素方法都支持这种谓词语法。
它在功能上完全相同，但可能更高效，因为它允许底层的 LINQ 提供程序应用优化。

```csharp
/// You can add the Where to the Single item operations. ...
void GetFirstItemWithPredicate()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .First(movie => movie.Name.StartsWith("Spider-Man"));

    Print(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/?t=100)

#### Last 方法

`Last()` 方法检索集合中最后一个匹配的元素。
如果你在实现了 `IList<T>` 的数据源（例如标准的 `List<T>`）上使用谓词重载，LINQ 可以通过使用反向枚举器来优化搜索。
它不是从集合开头开始迭代，而是从末尾开始，这在大型数据集中能显著提升性能。

```csharp
/// You can also retrieve the last item ...
void GetLastItem()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Last(movie => movie.Name.StartsWith("Spider-Man"));

    Print(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/?t=275)

#### 用 OrDefault 处理缺失元素

默认情况下，如果找不到匹配项，`First()`、`Last()` 和 `Single()` 会抛出 `InvalidOperationException`（"Sequence contains no matching element"）。
要避免这种情况，请使用 `OrDefault` 变体。
它们会返回该类型的默认值（例如类返回 `null`，整数返回 `0`，`DateTime` 返回最小值）。

```csharp
/// Adding 'OrDefault' prevents exceptions when no item is found. ...
void GetFirstItemWithDefault()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .FirstOrDefault(movie => movie.Name.StartsWith("Batman"));

    Print(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/?t=385)

#### Single 方法

当你预期恰好只有一个元素满足条件时（例如按唯一 ID 查找一条记录），就使用 `Single()`。
与找到匹配项就立即停止的 `First()` 不同，`Single()` 会继续迭代，以确保不存在第二个匹配项。
如果找到不止一个匹配项，它会抛出异常："Sequence contains more than one matching element."

```csharp
/// When you only expect a single match, you can throw if more are found. ...
void ExpectSingleMatch()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Single(movie => movie.Name.StartsWith("Spider-Man: Homecoming"));

    Print(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/?t=475)

#### 行为汇总表

| 方法 | 返回 | 无匹配 | 2 个及以上匹配 | 说明 |
| :--- | :--- | :--- | :--- | :--- |
| **First** | 第一个匹配项 | 抛出异常 | OK | 找到第一个匹配项后停止 |
| **Last** | 最后一个匹配项 | 抛出异常 | OK | 可能迭代整个数据源（除非经过优化） |
| **Single** | 唯一匹配项 | 抛出异常 | 抛出异常 | 迭代以确保唯一性 |
| **FirstOrDefault** | 第一个匹配项 | default(T) | OK | 找到第一个匹配项后停止 |
| **LastOrDefault** | 最后一个匹配项 | default(T) | OK | 可能迭代整个数据源 |
| **SingleOrDefault**| 唯一匹配项 | default(T) | 抛出异常 | 迭代以确保唯一性 |

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/retrieving-a-single-item-54280337/?t=570)

## 4. Projecting the data to another shape

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/) · 11:22

LINQ 投影（projection）让开发者可以把查询结果的形状从原始模型类转换为另一种结构，例如单个属性、匿名类型、值元组或自定义类。
通过在查询语法中使用 `select` 子句，或在流式语法（fluent syntax）中使用 `Select` 方法，可以同时在可读性和性能上优化查询。
当与 Entity Framework 这类提供程序一起使用时，投影能确保只从数据库中检索必要的列，从而减少带宽并提升效率。

### 核心概念

- **Property Selection（属性选择）**：从复杂对象中提取单个字段。
- **Anonymous Types（匿名类型）**：创建临时的、无名称的类型，供方法内部局部使用。
- **Value Tuples（值元组）**：使用具名元组把数据投影成一种比匿名类型更容易在函数之间传递的形状。
- **Custom Type Projection（自定义类型投影）**：把查询结果直接映射到已有的 record 或类中。
- **Performance Optimization（性能优化）**：在基于数据库的提供程序中，只选择所需字段以减少数据传输。
- **Materialization Compatibility（物化兼容性）**：将投影与 `ToList()` 等方法结合，以最终确定查询结果。

### 课程笔记

默认情况下，LINQ 查询通常会返回数据源集合中完整的模型类。
不过，可以把结果投影成另一种形状，只选择某个任务所需的特定数据。

#### 选择单个属性

在查询语法中，`select` 子句决定了结果的最终形状。
要只返回某个特定属性，例如电影的名称，就把 `select` 子句改为引用该属性。
这会把返回类型从 `IEnumerable<Movie>` 变为 `IEnumerable<string>`。

```csharp
void SelectSingleProperty_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Name.StartsWith("Iron Man")
        select movie.Name;

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=55)

在流式语法中，使用 `Select` 扩展方法来完成这种投影。

```csharp
void SelectSingleProperty_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(movie => movie.Name.StartsWith("Iron Man"))
        .Select(movie => movie.Name);

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=115)

#### 性能影响

投影不仅仅是为了美观；它还可以作为一项重要的性能优化。
当 LINQ 与 Entity Framework 这类底层提供程序一起使用时，只选择特定属性就相当于在 SQL 中只选择特定的列。
这限制了数据库与应用程序之间通过网络传输的数据量。

#### 投影为匿名类型

当需要多个属性、但又不需要专门的类时，可以使用匿名类型。
这些类型是用 `new` 关键字即时创建的。

```csharp
void SelectAnonymousType_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Name.StartsWith("Iron Man")
        select new { movie.Name, movie.ReleaseDate.Year };

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=205)

同样的效果也可以用流式语法实现：

```csharp
void SelectAnonymousType_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(movie => movie.Name.StartsWith("Iron Man"))
        .Select(movie => new { movie.Name, movie.ReleaseDate.Year });

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=235)

#### 投影为值元组

匿名类型的一个缺点是很难在函数之间传递，因为它们的类型只在定义它们的作用域内可知。
值元组提供了一种更灵活的替代方案，既允许为属性命名，也让得到的形状可以轻松传递给其他方法。

```csharp
void ProjectToValueTuple_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var query =
        from movie in sourceMovies
        where movie.Name.StartsWith("Iron Man")
        select ( Title: movie.Name, Year: movie.ReleaseDate.Year );

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=340)

使用流式语法：

```csharp
void ProjectToValueTuple_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(movie => movie.Name.StartsWith("Iron Man"))
        .Select(movie => (Title: movie.Name, Year: movie.ReleaseDate.Year));

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=375)

#### 投影为已有类型

投影也可以以已有的类或 record 为目标。
这在把领域模型映射为视图模型或 DTO 时很有用。

```csharp
void ProjectToOtherType_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = 
        from movie in sourceMovies
        where movie.Name.StartsWith("Iron Man")
        select new MovieTitle(movie.Name, movie.ReleaseDate.Year);

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=475)

使用流式语法：

```csharp
void ProjectToOtherType_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .Where(movie => movie.Name.StartsWith("Iron Man"))
        .Select(movie => new MovieTitle(movie.Name, movie.ReleaseDate.Year));

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=505)

#### 物化投影结果

投影与 `ToList()` 等物化方法完全兼容。
你可以把数据投影成新的形状，然后立即把得到的 `IEnumerable` 转换为集合。

```csharp
void MaterializeProjectedResult_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result =
        (from movie in sourceMovies
        where movie.Name.StartsWith("Iron Man")
        select new MovieTitle(movie.Name, movie.ReleaseDate.Year))
        .ToList();

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=610)

在流式语法中，写成一条连续的方法链通常更具可读性：

```csharp
void MaterializeProjectedResult_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Where(movie => movie.Name.StartsWith("Iron Man"))
        .Select(movie => new MovieTitle(movie.Name, movie.ReleaseDate.Year))
        .ToList();

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/projecting-the-data-to-another-shape-54280338/?t=640)

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280339/) · 0:43

### 总结

本课全面回顾了 "Getting results from queries" 一章，总结了 LINQ 执行与数据检索的基本机制。
它回顾了从定义查询到物化结果的过渡过程，涵盖延迟执行、集合转换、单元素选择和结果投影。

### 核心概念

- 延迟执行及其在 LINQ 中的作用。
- 通过迭代或集合转换来物化查询。
- 从结果集中检索特定的单个元素。
- 把数据投影成自定义的形状和类型。

### 课程笔记

"Getting results from queries" 一节确立了使用 LINQ 数据的核心工作流程，重点是如何从查询定义过渡到可用的结果。

#### 执行与物化

本章的一个主要重点是**延迟执行**。
在 LINQ 中，查询不会在定义的那一刻执行；相反，执行会被推迟到真正访问数据的时候。
本课回顾了物化这些结果的两种主要方式：

- **迭代**：逐个访问结果，通常通过 `foreach` 循环。
- **集合转换**：通过把结果转换为具体集合（例如 `List` 或 `Array`）来强制查询立即执行。

#### 选择与塑形数据

本章还介绍了在查询中隔离和转换数据的技术：

- **单元素检索**：从查询结果集中提取某个特定的单个元素的方法。
- **投影**：把查询结果投影成另一种形状的能力。
  这让开发者可以把源数据映射为特定的数据传输对象（DTO）、匿名类型，或应用程序所需的任何其他结构。

这些概念为 LINQ 熟练掌握的下一阶段打下了基础：验证查询结果中是否存在特定数据。

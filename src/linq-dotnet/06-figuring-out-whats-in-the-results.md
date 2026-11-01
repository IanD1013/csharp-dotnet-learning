# Figuring out what's in the results

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 6 章
> 共 7 课 · 约 25:50
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-54280379/) | 2:00 | [↓](#1-introduction) |
| 2 | [Removing duplicates](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/removing-duplicates-54280380/) | 3:10 | [↓](#2-removing-duplicates) |
| 3 | [Checking for a single item](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-for-a-single-item-54280381/) | 3:09 | [↓](#3-checking-for-a-single-item) |
| 4 | [Verifying all items](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/) | 4:10 | [↓](#4-verifying-all-items) |
| 5 | [Using aggregate functions](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/) | 9:51 | [↓](#5-using-aggregate-functions) |
| 6 | [Checking if 2 sequences are equal](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-if-2-sequences-are-equal-54280384/) | 2:51 | [↓](#6-checking-if-2-sequences-are-equal) |
| 7 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280385/) | 0:39 | [↓](#7-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-54280379/) · 2:00

### 总结

本课介绍 LINQ 中从数据转换到数据分析的过渡。
查询管道的前几个阶段侧重于对数据进行过滤、排序,并重塑为可用的模型,而本章则侧重于从结果集合中提取有意义的洞见。
关键操作包括去除重复项、执行平均值或最大值之类的聚合计算,以及在无需手动迭代的情况下,验证整个数据集中是否存在特定元素或是否满足特定条件。

### 核心概念

- **结果分析(Result Analysis)**:不止于重塑数据,而是去理解结果集合的属性。
- **去除重复项(Duplicate Removal)**:根据定义的条件过滤掉冗余的项。
- **聚合函数(Aggregate Functions)**:计算平均值、总和或最大值等汇总统计数据。
- **量词(Quantifiers)**:检查特定项是否存在,或验证所有项是否都满足某个条件。

### 课程笔记

LINQ 的主要目标通常是获取源数据并运行查询,把它转换成应用程序可用的形状。
这通常涉及排序、过滤、重塑、分组和分区(取数据块)等操作。
一旦数据成为所需的形状,LINQ 就提供了一组工具来分析结果,并提取元数据或汇总信息。

#### 去除重复项

一个常见的需求是确保集合只包含唯一的项。
LINQ 允许根据特定条件从结果集合中去除重复项。
这确保了应用程序对每个唯一实体只处理一次。

#### 聚合函数

聚合函数允许从集合中提取数据,而无需手动迭代每一项。
这些函数可以找出某个特定属性值最大的项,或者计算集合中所有项某个属性的平均值。
借助这些内置函数,开发者可以高效地执行复杂的计算。

#### 存在性与条件匹配

LINQ 还提供了检查集合内容是否遵守特定规则的方法。
这包括:
- 检查某个特定项是否存在于集合中。
- 判断集合中是否有任何项符合某组特定条件。
- 验证集合中的每一项是否都满足某个特定条件。

这些分组函数使得对结果集进行高层次的验证和查询成为可能,在初始转换阶段之后提供了一种与数据交互的强大方式。

## 2. Removing duplicates

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/removing-duplicates-54280380/) · 3:10

### 总结

LINQ 提供了两个从集合中去除重复元素的主要方法:Distinct 和 DistinctBy。
Distinct 方法会过滤序列,根据默认相等比较器只返回唯一的项,这对整数之类的基元类型非常有效。
对于涉及对象的更复杂场景,DistinctBy 允许开发者指定一个键选择器,根据某个特定属性来判断唯一性。
这两个方法都会保留遇到的每个唯一元素的第一个实例,并返回一个 IEnumerable<T>,从而可以继续链式调用其他 LINQ 操作。

### 核心概念

- **Distinct()**:使用默认相等比较器返回集合中的唯一元素。
- **DistinctBy()**:根据指定的键或属性过滤集合以保证唯一性。
- **默认相等性(Default Equality)**:基元类型使用其内置比较;对象使用引用相等,除非重写了 Equals 和 GetHashCode。
- **保留首次出现(First-Occurrence Retention)**:检测到重复项时,保留序列中找到的第一个实例,丢弃后续的匹配项。

### 课程笔记

要从源集合中去除重复项,LINQ 提供了 `Distinct` 方法。
当应用于简单类型的集合(例如整数数组)时,`Distinct` 根据它们的默认相等比较器来识别唯一值。

```csharp
private void RemoveDuplicateItems()
{
    int[] sourceItems = [1, 3, 5, 3, 7, 7, 1, 6, 15, 23];

    var result = sourceItems.Distinct();

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/removing-duplicates-54280380/?t=55)

在上面的例子中,结果集合只包含唯一的整数:`1, 3, 5, 7, 6, 15, 23`。

```text
sourceItems:  1  3  5  3  7  7  1  6  15  23
              |  |  |  x  |  x  x  |  |   |
Distinct():   1  3  5     7        6  15  23
```

处理复杂数据类型(例如对象集合)时,`Distinct` 依赖于对象的相等性实现。
如果一个类没有重写相等性方法,`Distinct` 将使用引用相等。
在很多情况下,你可能希望根据某个特定属性而不是整个对象来过滤重复项。
这可以通过 `DistinctBy` 方法实现。

```csharp
private void RemoveDuplicateItemsByKey()
{
    var sourceMovies = Repository.GetAllMovies();

    var query = sourceMovies
        .DistinctBy(movie => movie.Phase);

    PrintAll(query);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/removing-duplicates-54280380/?t=130)

通过将 `DistinctBy` 与一个选择器(例如 `movie => movie.Phase`)一起使用,LINQ 会过滤集合,使得每个唯一的 phase 只有遇到的第一部电影被包含在结果中。
具有相同 phase 值的后续电影会被忽略。
输出仍然是 `IEnumerable<Movie>`,因此可以继续进行过滤或转换等 LINQ 操作。

## 3. Checking for a single item

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-for-a-single-item-54280381/) · 3:09

### 总结

LINQ 中的 Contains 方法用于判断某个特定元素是否存在于集合中。
默认情况下,对于引用类型,Contains 使用引用相等,也就是说,只有当集合中存在完全相同的对象实例时它才返回 true。
要基于属性值进行检查(逻辑相等),开发者可以向该方法提供一个 IEqualityComparer<T> 的实现,从而使用自定义的比较逻辑,例如按唯一的名称或 ID 进行匹配。

### 核心概念

- 用于成员检查的 `Contains` 方法。
- 引用相等与逻辑相等。
- 实现 `IEqualityComparer<T>` 以进行自定义的对象比较。
- 向 `Contains` 和 `Distinct` 等 LINQ 方法提供自定义比较器。

### 课程笔记

要判断集合是否包含某个特定元素,LINQ 提供了 `Contains` 方法。
当一个对象直接从数据源中取出,然后再拿它与同一数据源进行检查时,`Contains` 会返回 `true`,因为对象引用是相同的。

```csharp
void CheckIfItemIsPresent()
{
    var blackWidow = Repository.GetByName("Black Widow");

    var sourceMovies = Repository.GetAllMovies();

    var isMoviePresent = sourceMovies.Contains(blackWidow);

    Console.WriteLine(isMoviePresent);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-for-a-single-item-54280381/?t=55)

当比较一个新的对象实例时就会出现问题:它与集合中的某一项数据相同,但内存引用不同。
在这种情况下,即使所有属性值都完全相同,默认的引用相等检查也会返回 `false`。

```csharp
void CheckIfItemIsPresentByComparer()
{
    var blackWidow = new Movie
    {
        MovieId = Guid.NewGuid(),
        Name = "Black Widow",
        ReleaseDate = DateOnly.MinValue
    };

    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies.Contains(blackWidow);

    Console.WriteLine(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-for-a-single-item-54280381/?t=130)

要处理逻辑相等,你可以实现 `IEqualityComparer<T>` 接口。
这需要为 `Equals` 和 `GetHashCode` 方法定义自定义逻辑。
下面的实现根据 `Name` 属性来比较电影,同时也处理了 null 检查和类型验证。

```csharp
class MovieComparer : IEqualityComparer<Movie>
{
    public bool Equals(Movie x, Movie y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name;
    }

    public int GetHashCode(Movie obj)
    {
        return obj.Name.GetHashCode();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-for-a-single-item-54280381/?t=100)

一旦定义好比较器,就可以把它作为第二个参数传给 `Contains` 方法。
这会指示 LINQ 使用自定义逻辑来判断相等性,使得该方法在按名称找到匹配项时返回 `true`,而不管对象引用是什么。

```csharp
void CheckIfItemIsPresentByComparer()
{
    var blackWidow = new Movie
    {
        MovieId = Guid.NewGuid(),
        Name = "Black Widow",
        ReleaseDate = DateOnly.MinValue
    };

    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Contains(blackWidow, new MovieComparer());

    Console.WriteLine(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-for-a-single-item-54280381/?t=145)

这种提供自定义 `IEqualityComparer<T>` 的模式同样适用于其他 LINQ 方法,例如 `Distinct`,用来控制如何把元素识别为重复项。

## 4. Verifying all items

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/) · 4:10

### 总结

本课演示如何使用 LINQ 方法 Any 和 All 来验证集合中的元素是否满足特定条件。
Any 在至少有一个元素匹配谓词时返回 true,而 All 只有在序列中每个元素都满足条件时才返回 true。
这两个方法都很高效,因为它们利用了短路逻辑:Any 一找到匹配项就停止求值,All 一遇到不匹配项就停止。

### 核心概念

- **Any()**:返回一个布尔值,表示集合中是否至少有一个元素匹配所提供的谓词。
- **All()**:返回一个布尔值,表示集合中是否每个元素都匹配所提供的谓词。
- **短路(Short-circuiting)**:一种优化,一旦最终结果确定(Any 为 true,All 为 false),LINQ 就停止处理集合。
- **谓词(Predicates)**:用于定义验证条件的 Lambda 表达式。
- **IEnumerable 验证**:这两个方法都是 IEnumerable 的扩展方法,用于在不手动迭代的情况下检查集合内容。

### 课程笔记

处理集合时,经常需要验证特定项是否存在,或者整个集合是否遵守某些规则。
`Any` 和 `All` 方法提供了执行这些检查的简洁方式。

首先,我们搭建一个基本结构,用于在电影仓储中检查匹配项。

```csharp
void CheckIfMatchIsPresent()
{
    var sourceMovies = Repository.GetAllMovies();
}

void CheckIsAllItemsMatch()
{
    var sourceMovies = Repository.GetAllMovies();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/?t=10)

#### 使用 Any 验证存在性

`Any` 方法接受一个谓词,如果集合中有任何一项满足它,就返回 `true`。
例如,检查 "Black Widow" 是否存在于集合中:

```csharp
void CheckIfMatchIsPresent()
{
    var sourceMovies = Repository.GetAllMovies();

    var isBlackWidowPresent = sourceMovies
        .Any(movie => movie.Name == "Black Widow");

    Console.WriteLine(isBlackWidowPresent);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/?t=70)

如果谓词不匹配任何项(例如在 Marvel 集合中搜索 "Batman"),该方法返回 `false`。
`Any` 也适用于借助 `StartsWith` 之类的字符串方法进行部分匹配。
由于 `Any` 会短路,即使存在多个匹配项,它也会在遇到第一个匹配元素时立即返回 `true`。

```csharp
void CheckIfMatchIsPresent()
{
    var sourceMovies = Repository.GetAllMovies();
    var isIronMoviePresent = sourceMovies
        .Any(movie => movie.Name.StartsWith("Iron"));

    Console.WriteLine(isIronMoviePresent);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/?t=115)

#### 使用 All 验证集合

当你需要确保集合中的每一项都符合某个特定条件时,就使用 `All` 方法。
它经常与 `Where` 结合使用,以验证数据的某个特定子集。
例如,我们可以过滤集合,只保留 "Iron Man" 电影,然后验证它们是否都属于该系列的早期阶段(Phase 1 或 2)。

```csharp
void CheckIsAllItemsMatch()
{
    var ironManMovies = Repository.GetAllMovies()
        .Where(movie => movie.Name.StartsWith("Iron Man"));

    var areAllIronManEarlyPhases = ironManMovies
        .All(movie => movie.Phase <= 2);

    Console.WriteLine(areAllIronManEarlyPhases);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/?t=205)

如果去掉过滤,在整个集合上调用 `All`,那么只要有一部电影不在 Phase 2 及以内,它就会返回 `false`。
和 `Any` 一样,`All` 也会短路:一旦找到不满足谓词的项,它就停止求值并返回 `false`。

```csharp
void CheckIsAllItemsMatch()
{
    var ironManMovies = Repository.GetAllMovies();
        // .Where(movie => movie.Name.StartsWith("Iron Man"));

    var areAllIronManEarlyPhases = ironManMovies
        .All(movie => movie.Phase <= 2);

    Console.WriteLine(areAllIronManEarlyPhases);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/verifying-all-items-54280382/?t=235)

## 5. Using aggregate functions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/) · 9:51

### 总结

本课介绍如何使用 LINQ 聚合函数对集合执行计算和数据分析。
它讲解了如何使用 Min 和 Max 求最小值和最大值,以及如何使用 MinBy 和 MaxBy 取得与这些值关联的具体对象。
本课还演示了用 Average 计算平均值、用 Sum 求总和、用 Count 统计元素个数。
最后,它探讨了如何在查询语法和方法语法中把这些聚合函数与 GroupBy 结合使用,为分组后的子集生成汇总数据。

### 核心概念

- **Min 和 Max**:取得集合中某个特定属性的最小值或最大值。
- **MinBy 和 MaxBy**:取得包含指定属性最小值或最大值的整个对象。
- **Average**:计算集合中某个数值属性的平均值。
- **Sum**:计算集合中某个数值属性的总和。
- **Count**:返回集合中的元素个数,或满足某个条件的元素个数。
- **聚合与分组(Aggregates with Grouping)**:在 `Select` 子句中使用聚合来提供关于分组的元数据(例如,找出某个特定电影阶段中最晚的上映日期)。

### 课程笔记

#### 最小值和最大值

LINQ 提供了直接的方法来找出数据的边界。
`Min` 方法让你可以找出某个特定表达式的最小值,例如电影集合中最早的上映日期。

```csharp
/// Get the minimum value for a certain expression ...
void MinimumValue()
{
    var sourceMovies = Repository.GetAllMovies();

    var firstReleaseDate = sourceMovies
        .Min(movie => movie.ReleaseDate);

    Console.WriteLine(firstReleaseDate);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=70)

如果你需要的是持有该最小值的实际对象,而不仅仅是值本身,就使用 `MinBy`。
这样可以避免为了找出与该值匹配的项而再次查询集合。

```csharp
/// Get the item with the minimum value for a certain expression ...
void MinimumItem()
{
    var sourceMovies = Repository.GetAllMovies();

    var firstMovie = sourceMovies
        .MinBy(movie => movie.ReleaseDate);

    Console.WriteLine(firstMovie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=130)

```text
sourceMovies --Min(movie => movie.ReleaseDate)----> firstReleaseDate (值)
sourceMovies --MinBy(movie => movie.ReleaseDate)--> firstMovie       (Movie)
```

类似的逻辑也适用于 `Max` 和 `MaxBy`。
这些函数可以与 `Where` 等其他 LINQ 运算符结合使用,在过滤后的子集中找出边界,例如找出当前日期之前最近上映的电影。

```csharp
/// Get the maximum value for a certain expression ...
void MaximumValue()
{
    var sourceMovies = Repository.GetAllMovies();

    var lastReleaseDate = sourceMovies
        .Where(movie => movie.ReleaseDate < DateOnly.FromDateTime(DateTime.Now))
        .Max(movie => movie.ReleaseDate);

    Console.WriteLine(lastReleaseDate);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=200)

```csharp
/// Get the item with the maximum value for a certain expression ...
void MaximumItem()
{
    var sourceMovies = Repository.GetAllMovies();

    var lastMovie = sourceMovies
        .Where(movie => movie.ReleaseDate < DateOnly.FromDateTime(DateTime.Now))
        .MaxBy(movie => movie.ReleaseDate);

    Console.WriteLine(lastMovie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=220)

#### 平均值、总和与计数

除了找出边界,LINQ 还支持标准的算术聚合。
`Average` 计算某个数值属性的平均值。
注意,`Average` 没有对应的 "AverageBy",因为它返回的是一个计算出来的值,而不是集合中已有的某一项。

```csharp
/// Get the average value for a certain expression ...
void AverageValue()
{
    var sourceMovies = Repository.GetAllMovies();

    var averageProducers = sourceMovies
        .Average(movie => movie.Producers.Count);

    Console.WriteLine(averageProducers);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=295)

`Sum` 用于计算整个集合中某个数值属性的总和。

```csharp
/// Get the added value for a certain expression ...
void SumValue()
{
    var sourceMovies = Repository.GetAllMovies();

    var totalProducers = sourceMovies
        .Sum(movie => movie.Producers.Count);

    Console.WriteLine(totalProducers);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=325)

`Count` 只是返回集合中的项的总数,或者匹配某个特定查询的项的个数。

```csharp
/// Count the number of items ...
void CountItems()
{
    var sourceMovies = Repository.GetAllMovies();

    var movieCount = sourceMovies.Count();

    Console.WriteLine(movieCount);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=355)

#### 聚合与 GroupBy

聚合函数与 `group by` 结合使用时尤其强大。
这让你可以为每个分组计算统计数据。
在下面的查询语法示例中,我们按 phase 对电影分组,并使用 `Max` 找出每个 phase 中最晚的上映日期。

```csharp
/// Combining the use of aggregate functions with a group by, query syntax ...
void AggregateFunctionsWithGroupBy_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var groupedQuery =
        from movie in sourceMovies
        where movie.Producers.Count > 1
        group movie by movie.Phase into movies
        where movies.Key > 2
        select new
        {
            Movies = movies,
            LastMovie = movies.Max(film => film.ReleaseDate)
        };

    foreach (var phase in groupedQuery)
    {
        Console.WriteLine($"PHASE {phase.Movies.Key} (until {phase.LastMovie}):");
        foreach (var movie in phase.Movies)
        {
            Console.WriteLine(movie);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=475)

```text
sourceMovies
    |
    v  where movie.Producers.Count > 1
    |
    v  group movie by movie.Phase into movies
    |
    v  where movies.Key > 2
    |
    v  select new { Movies = movies,
                    LastMovie = movies.Max(film => film.ReleaseDate) }
groupedQuery
```

同样的逻辑也可以用流式(方法)语法实现,做法是在 `GroupBy` 操作之后的 `Select` 方法中应用聚合。

```csharp
/// Combining the use of aggregate functions with a group by, fluent syntax ...
void AggregateFunctionsWithGroupBy_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var groupedQuery = sourceMovies
        .Where(movie => movie.Producers.Count > 1)
        .GroupBy(
            movie => movie.Phase,
            movie => movie)
        .Where(phase => phase.Key > 2)
        .Select(group => new
        {
            Movies = group,
            LastMovie = group.Max(film => film.ReleaseDate)
        } );

    foreach (var phase in groupedQuery)
    {
        Console.WriteLine($"PHASE {phase.Movies.Key} (until {phase.LastMovie}):");
        foreach (var movie in phase.Movies)
        {
            Console.WriteLine(movie);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-aggregate-functions-54280383/?t=550)

## 6. Checking if 2 sequences are equal

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-if-2-sequences-are-equal-54280384/) · 2:51

### 总结

`SequenceEqual` 运算符通过以一对一、按顺序的方式比较元素,来判断两个序列是否相等。
要让两个序列被视为相等,它们必须有相同数量的元素,并且每一对对应的元素都必须满足相等条件。
该方法默认使用该类型的标准相等比较器,但它也接受一个自定义的 `IEqualityComparer<T>`,用于处理逻辑相等不同于引用相等的场景。

### 核心概念

- **按序比较(Ordinal Comparison)**:元素按它们在序列中出现的顺序逐一比较。
- **长度验证(Length Validation)**:序列必须有相同数量的元素才能被视为相等。
- **默认相等性(Default Equality)**:使用该类型的默认相等比较器(例如,类使用引用相等)。
- **自定义比较器(Custom Comparers)**:支持用 `IEqualityComparer<T>` 实现自定义逻辑(例如,按 `Name` 这样的特定属性比较对象)。

### 课程笔记

处理两个序列时,你可能需要验证它们的内容是否完全相同。
`SequenceEqual` 方法提供了这个功能。
在最简单的情况下,比较两个以相同顺序包含相同数据的序列会返回 `true`。

```csharp
/// Check if the items in 2 sequences, one-by-one, are equal.
void CheckIfSequencesMatch()
{
    var phaseOneMovies = Repository.GetPhase1Movies();

    var firstSixMovies = Repository.GetAllMovies()
        .Take(6);

    var areMoviesEqual = phaseOneMovies
        .SequenceEqual(firstSixMovies);

    Console.WriteLine(areMoviesEqual);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-if-2-sequences-are-equal-54280384/?t=70)

`SequenceEqual` 执行两项主要检查:
1. 它验证两个集合的长度相同。
2. 它同时遍历两个集合,根据默认相等比较器逐一检查各项是否匹配。

```text
phaseOneMovies:  [0]  [1]  [2]  [3]  [4]  [5]
                  |    |    |    |    |    |
                  ==   ==   ==   ==   ==   ==
                  |    |    |    |    |    |
firstSixMovies:  [0]  [1]  [2]  [3]  [4]  [5]
```

然而,对于复杂对象,默认的相等比较往往不够用。
如果你有两个序列,其中的对象数据相同但内存引用不同(或内部标识符不同,例如 `Guid`),`SequenceEqual` 默认会返回 `false`。

```csharp
/// Check if the items in 2 sequences, one-by-one, are equal.
void CheckIfSequencesMatchWithComparer()
{
    Movie[] phaseOneMovies = [
        new Movie { Name = "Iron Man", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "The incredible Hulk", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "Iron Man 2", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "Thor", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "Captain America: The first Avenger", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "The Avengers", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
    ];

    var firstSixMovies = Repository.GetAllMovies()
        .Take(6);

    var areSequencesEqual = phaseOneMovies
        .SequenceEqual(firstSixMovies);

    Console.WriteLine(areSequencesEqual);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-if-2-sequences-are-equal-54280384/?t=110)

在上面的例子中,尽管电影名称相同,但 `MovieId` 的值不同,因为每个新实例都使用了 `Guid.NewGuid()`。
要解决这个问题,你可以向 `SequenceEqual` 方法传入一个自定义的 `IEqualityComparer<T>`。
这让你可以基于特定属性(例如电影名称)来定义相等性。

```csharp
void CheckIfSequencesMatchWithComparer()
{
    Movie[] phaseOneMovies = [
        new Movie { Name = "Iron Man", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "The incredible Hulk", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "Iron Man 2", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "Thor", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "Captain America: The first Avenger", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
        new Movie { Name = "The Avengers", MovieId = Guid.NewGuid(), ReleaseDate = DateOnly.MinValue },
    ];

    var firstSixMovies = Repository.GetAllMovies()
        .Take(6);

    var areSequencesEqual = phaseOneMovies
        .SequenceEqual(firstSixMovies, new MovieComparer());

    Console.WriteLine(areSequencesEqual);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/checking-if-2-sequences-are-equal-54280384/?t=145)

通过使用 `MovieComparer`,序列会基于该比较器中定义的逻辑(例如只比较名称)进行比较。
如果长度一致,并且每个名称都按正确的顺序匹配,`SequenceEqual` 就返回 `true`。
如果长度不同,或者任何一项未通过比较,它会立即返回 `false`。

## 7. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280385/) · 0:39

### 总结

本课回顾了用于分析集合内容的核心 LINQ 操作,包括去除重复项、存在性检查和数据聚合。
借助这些方法,开发者无需编写手动循环,就能对整个数据集或分组结果执行复杂的计算和条件匹配,从而写出更具表达力、更高效的代码。

### 核心概念

- 从集合中去除重复项。
- 判断项是否存在。
- 使用谓词在集合上验证条件。
- 对数据集和分组结果应用聚合函数。
- 用富有表达力的 LINQ 操作替代手动迭代。

### 课程笔记

"Figuring out what's in the results" 这一章探讨了使用 LINQ 检查、过滤和汇总数据的各种方式。
这些操作对于把原始集合转换为有意义的信息至关重要。

#### 数据检查与过滤

LINQ 提供了内置方法来处理去除重复项之类的常见数据任务。
这确保了结果集合只包含根据指定条件判定为唯一的元素。
此外,框架还提供了验证集合内容的工具,例如检查某个特定项是否存在,或者集合中是否有任何元素。

#### 条件匹配

除了简单的存在性检查,LINQ 还允许根据特定逻辑评估元素。
这包括:
- 判断集合中的所有项是否都符合某个条件。
- 判断集合中是否至少有一项符合某个条件。

#### 聚合与计算

本章的很大一部分内容聚焦于聚合函数。
这些函数允许在整个集合上,或在查询过程中创建的特定分组内执行计算。
常见的操作包括计算总和、平均值、最小值、最大值和计数。
借助这些内置函数,开发者可以避免手动循环和状态管理带来的样板代码,从而写出更易维护、更易读的代码。

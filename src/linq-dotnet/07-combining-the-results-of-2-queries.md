# Combining the results of 2 queries

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 7 章
> 共 6 课 · 约 46:25
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-54280411/) | 1:26 | [↓](#1-introduction) |
| 2 | [Concatenate two results](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/concatenate-two-results-54280412/) | 1:50 | [↓](#2-concatenate-two-results) |
| 3 | [Set operations on queries with the same type](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/) | 10:02 | [↓](#3-set-operations-on-queries-with-the-same-type) |
| 4 | [Exploding a subquery with SelectMany](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/) | 7:00 | [↓](#4-exploding-a-subquery-with-selectmany) |
| 5 | [Join operations between 2 results](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/) | 25:25 | [↓](#5-join-operations-between-2-results) |
| 6 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280416/) | 0:42 | [↓](#6-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-54280411/) · 1:26

### 总结

本课介绍组合多个 LINQ 查询结果的各种方式。
内容涵盖追加集合这类基础操作、交集与差集等基于集合的操作,以及处理属性中包含嵌套集合的复杂对象图。

### 核心概念

- 追加查询结果
- 集合操作:Intersect、Except 和 Union
- 在对象图中组合嵌套集合

### 课程笔记

LINQ 提供了若干方法,用于合并或比较两个独立查询的结果。
一种常见的场景是直接把一个查询的结果追加到另一个查询的结果之后,从而得到单个序列。

基于集合的操作允许根据两个结果集之间的关系进行更具体的筛选。
以维恩图作为心智模型,开发者可以执行以下操作:

- **Intersection(交集)**:找出同时存在于两个结果集中的项。
- **Except(差集)**:找出一个集合中的项,同时排除出现在第二个集合中的任何项。
- **Union(并集)**:把两个集合中的项组合成单个集合,同时去除重复项。

除了简单的基于行的操作之外,LINQ 在处理对象图时也很有效。
当查询结果中的对象包含本身就是集合的属性时,可以用 LINQ 在整个结果集范围内组合这些嵌套集合。
这样就能把对象层级深处的数据聚合成单一数据源,以便进一步查询。

## 2. Concatenate two results

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/concatenate-two-results-54280412/) · 1:50

LINQ 中的 Concat 运算符用于把两个元素类型相同的序列组合成单个更大的序列。
通过在现有的 IEnumerable<T> 上调用 Concat 并传入第二个相同类型的集合,开发者可以把来自多个数据源的结果聚合成一个连续的流,以便进一步处理或展示。

### 核心概念

- 组合元素类型相同的序列。
- IEnumerable<T>.Concat 扩展方法。
- 流式链接多个 Concat 调用。
- 把分区数据聚合成单个逻辑序列。

### 课程笔记

当你有两个元素类型相同的序列,并需要把它们组合成单个更大的序列时,Concat 方法就是合适的工具。
当你有分区数据(例如按系列阶段划分的电影列表),并希望把它们当作一个统一的集合来处理时,这种操作很常见。

下面的示例演示了如何准备三个独立的电影集合:

```csharp
// ReSharper disable UnusedMember.Local
namespace Dometrain.Linq.Cmd._7_CombiningResults;

public class AppendSequences : QueryRunner
{
    public override void Run()
    {
        ConcatenateTwoSequences();
    }

    /// Concatenate 2 sequences with similar the same element type. ...
    void ConcatenateTwoSequences()
    {
        var phaseOneMovies = Repository.GetPhase1Movies();
        var phaseTwoMovies = Repository.GetPhase2Movies();
        var phaseThreeMovies = Repository.GetPhase3Movies();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/concatenate-two-results-54280412/?t=10)

Concat 方法是 IEnumerable<T> 上的一个扩展方法。
它接受第二个 IEnumerable<T> 作为参数,前提是二者的元素类型相同。
其结果是一个新的 IEnumerable<T>,先包含第一个序列中的所有项,后面接着第二个序列中的所有项。

```csharp
// ReSharper disable UnusedMember.Local
namespace Dometrain.Linq.Cmd._7_CombiningResults;

public class AppendSequences : QueryRunner
{
    public override void Run()
    {
        ConcatenateTwoSequences();
    }

    /// Concatenate 2 sequences with similar the same element type. ...
    void ConcatenateTwoSequences()
    {
        var phaseOneMovies = Repository.GetPhase1Movies();
        var phaseTwoMovies = Repository.GetPhase2Movies();
        var phaseThreeMovies = Repository.GetPhase3Movies();

        var infinitySage = phaseOneMovies
            .Concat(phaseTwoMovies);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/concatenate-two-results-54280412/?t=50)

由于 Concat 返回的是 IEnumerable<T>,它可以作为后续 LINQ 操作的数据源。
这使得后续的 Concat 调用可以链接在一起,把多个集合聚合成一个。

```csharp
// ReSharper disable UnusedMember.Local
namespace Dometrain.Linq.Cmd._7_CombiningResults;

public class AppendSequences : QueryRunner
{
    public override void Run()
    {
        ConcatenateTwoSequences();
    }

    /// Concatenate 2 sequences with similar the same element type. ...
    void ConcatenateTwoSequences()
    {
        var phaseOneMovies = Repository.GetPhase1Movies();
        var phaseTwoMovies = Repository.GetPhase2Movies();
        var phaseThreeMovies = Repository.GetPhase3Movies();

        var infinitySage = phaseOneMovies
            .Concat(phaseTwoMovies)
            .Concat(phaseThreeMovies);

        PrintAll(infinitySage);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/concatenate-two-results-54280412/?t=100)

```text
phaseOneMovies   phaseTwoMovies   phaseThreeMovies
      |                |                 |
      +--- .Concat ----+                 |
               |                         |
               +-------- .Concat --------+
                            |
                            v
                      infinitySage
```

执行这段代码会得到单个序列,其中包含从最初的 Iron Man 到 Spider-Man: Far From Home 的所有电影,实际上就是把三个阶段合并成了一个统一的 "Infinity Saga" 集合。

## 3. Set operations on queries with the same type

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/) · 10:02

### 总结

本课探讨 LINQ 的集合操作(Union、Intersect 和 Except),它们允许基于集合逻辑来组合或筛选相同类型的序列。
内容涵盖这些操作默认如何处理重复项、如何使用 IEqualityComparer<T> 提供自定义相等逻辑,以及如何使用 ExceptBy 这类 "By" 变体,借助特定的属性键而不是要求整个对象匹配来执行集合操作。

### 核心概念

- **Union**:把两个序列组合成一个,并去除所有重复元素。
- **Intersect**:只返回同时存在于两个序列中的元素。
- **Except**:返回第一个序列中不存在于第二个序列中的元素。
- **Distinct 行为**:与 `Concat` 不同,`Union` 会对结果集执行去重操作。
- **自定义相等性**:集合操作依赖于相等性;可以通过 `IEqualityComparer<T>` 提供自定义逻辑。
- **基于键的操作**:`UnionBy`、`IntersectBy` 和 `ExceptBy` 允许基于特定属性(键)而不是整个对象来应用集合逻辑。

### 课程笔记

LINQ 中的集合操作允许你用类似维恩图的逻辑来比较两个相同类型的序列。
这些操作包括求交集(两边都有的项)、求差集(在一边而不在另一边的项),或者求并集(两边的所有项,排除重复项)。

#### Union

`Union` 运算符组合两个序列。
`Concat` 只是把一个序列追加到另一个序列之后,而 `Union` 则不同,它会在幕后执行去重操作,确保结果序列只包含唯一的项。

```csharp
public override void Run()
{
    UnionSequences();
}

/// <summary>
/// Take all elements that appear in either source.
/// </summary>
void UnionSequences()
{
    var infinitySaga = Repository.GetInfinitySagaMovies();
    var multiversesSaga = Repository.GetMultiverseSagaMovies();

    var allMovies = infinitySaga
        .Union(multiversesSaga);

    PrintAll(allMovies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/?t=145)

如果两个集合之间存在重叠,重复项会被过滤掉。
例如,如果你把整个 Infinity Saga 与仅包含 Phase 3 的电影(它们是 Infinity Saga 的子集)求并集,结果包含的项数将与单独的 Infinity Saga 相同。

```csharp
/// <summary>
/// Take all elements that appear in either source.
/// </summary>
void UnionSequencesWithOverlap()
{
    var infinitySaga = Repository.GetInfinitySagaMovies();
    var phase3Movies = Repository.GetPhase3Movies();

    var movies = infinitySaga
        .Union(phase3Movies);

    PrintAll(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/?t=175)

#### Intersect

`Intersect` 运算符找出两个序列之间的共同部分,只返回同时出现在两个集合中的项。

```csharp
/// <summary>
/// Take all elements that appear in both sources.
/// </summary>
void IntersectSequences()
{
    var infinitySaga = Repository.GetInfinitySagaMovies();
    var phase3Movies = Repository.GetPhase3Movies();

    var movies = infinitySaga
        .Intersect(phase3Movies);

    PrintAll(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/?t=295)

#### Except

`Except` 运算符从第一个序列中减去第二个序列。
它返回第一个集合中所有不存在于第二个集合中的项。

```csharp
/// <summary>
/// Take all elements that appear in the first sequence, but not the second.
/// </summary>
void ExceptSequences()
{
    var infinitySaga = Repository.GetInfinitySagaMovies();
    var phase3Movies = Repository.GetPhase3Movies();

    var movies = infinitySaga
        .Except(phase3Movies);

    PrintAll(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/?t=340)

```text
+------------------------ infinitySaga ------------------------+
|                                                              |
|                          +-------- phase3Movies ---------+   |
|   .Except(phase3Movies)   |   .Intersect(phase3Movies)    |   |
|                          +-------------------------------+   |
|                                                              |
+--------------------------------------------------------------+

.Union(phase3Movies) -> infinitySaga
```

#### Custom Equality Comparers

默认情况下,这些操作使用该类型的默认相等比较器。
对于引用类型,这通常意味着检查对象是否为同一个实例。
如果你有代表相同数据的不同对象实例(例如电影名称相同但 GUID 不同),就必须提供一个自定义的 `IEqualityComparer<T>`。

```csharp
var movies = infinitySaga
    .Except(phase1Movies, new MovieComparer());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/?t=460)

#### Set Operations "By" Key

有时你可能希望使用特定的键(比如字符串或 ID)而不是整个对象来执行集合操作。
LINQ 为此提供了 `UnionBy`、`IntersectBy` 和 `ExceptBy`。
这些方法接受第二个由键组成的集合,以及一个用于源集合的键选择器。

```csharp
/// <summary>
/// The above operations have overloads to select items by key
/// </summary>
void ExceptSequencesByKey()
{
    var infinitySaga = Repository.GetInfinitySagaMovies();
    string[] phase1MovieNames = [
        "Iron Man", "The incredible Hulk", "Iron Man 2",
        "Thor", "Captain America: The first Avenger", "The Avengers"
    ];

    var movies = infinitySaga
        .ExceptBy(phase1MovieNames, movie => movie.Name);

    PrintAll(movies);
}

class MovieComparer : IEqualityComparer<Movie>
{
    public bool Equals(Movie? x, Movie? y)
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/set-operations-on-queries-with-the-same-type-54280413/?t=550)

## 4. Exploding a subquery with SelectMany

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/) · 7:00

### 总结

SelectMany 是一个 LINQ 运算符,用于把集合的集合展平成单个连续的序列。
当源对象包含一个本身就是集合的属性时,它尤其有用,开发者可以借此"炸开"这些子集合,从而在整个数据集范围内对单个子元素执行操作。

### 核心概念

- 把嵌套集合展平成单个序列。
- 在查询语法中使用多个 from 子句深入子集合。
- 在流式语法中使用 SelectMany 方法。
- 使用 Distinct 处理展平结果中的重复项。
- 使用结果选择器重载,把父级和子级数据一起投影到新的模型中。

### 课程笔记

当你处理的数据模型中父对象包含一个子对象集合时(例如一个 Movie 拥有一个 Directors 列表),你可能需要把所有父对象的全部子对象聚合到单个列表中。
SelectMany 执行的就是这种"炸开"操作,把所有子集合连接成一个大集合。

```text
allMovies                        SelectMany
+---------+--------------+
| movie   | Directors    |       director
+---------+--------------+       director
| movie   | Directors    |  -->  director
+---------+--------------+       director
| movie   | Directors    |       ...
+---------+--------------+       (单个序列)
```

#### Query Syntax

在 LINQ 查询语法中,SelectMany 是通过使用多个 from 子句来实现的。
第一个 from 子句遍历源集合,第二个 from 子句遍历当前项的集合属性。

```csharp
void SelectManyFromProperty_Q()
{
    var allMovies = Repository.GetAllMovies();

    var allDirectors =
        from movie in allMovies
        from director in movie.Directors
        orderby director.ToString()
        select director;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/?t=100)

由于 SelectMany 只是简单地聚合所有项,如果某个子项关联到多个父项(例如一位导演执导了多部电影),就会出现重复项。
要得到唯一的列表,可以把查询用括号包起来,再调用 Distinct() 方法。

```csharp
void SelectManyFromProperty_Q()
{
    var allMovies = Repository.GetAllMovies();

    var allDirectors =
        (from movie in allMovies
        from director in movie.Directors
        orderby director.ToString()
        select director).Distinct();

    PrintAll(allDirectors);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/?t=150)

#### Fluent Syntax

在流式语法中,使用的是 SelectMany 方法。
它接受一个 lambda 表达式,用来指明要展平的集合属性。

```csharp
void SelectManyFromProperty_F()
{
    var allMovies = Repository.GetAllMovies();

    var allDirectors = allMovies
        .SelectMany(movie => movie.Directors)
        .OrderBy(director => director.ToString())
        .Distinct();

    PrintAll(allDirectors);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/?t=220)

#### Projections with SelectMany

展平集合往往会让你丢失父对象的上下文。
为了同时保留父级和子级的信息,你可以把结果投影到一个新的模型或元组中。

在查询语法中,这是通过 select 一个包含两个 from 别名属性的新对象或元组来实现的。

```csharp
void SelectManyWithProjection_Q()
{
    var allMovies = Repository.GetAllMovies();

    var allDirectors =
        from movie in allMovies
        from director in movie.Directors
        select (Movie: movie.Name, Director: director.ToString());

    PrintAll(allDirectors);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/?t=295)

#### Fluent Projection

在流式语法中,要同时投影父级和子级数据,需要使用 SelectMany 的一个接受两个参数的重载:一个集合选择器和一个结果选择器。
结果选择器 lambda 同时接收父项和子项作为参数。

```csharp
void SelectManyWithProjection_F()
{
    var allMovies = Repository.GetAllMovies();

    var allDirectors = allMovies
        .SelectMany(
            movie => movie.Directors,
            (movie, director) => (Movie: movie.Name, Director: director.ToString()));

    PrintAll(allDirectors);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/exploding-a-subquery-with-selectmany-54280414/?t=370)

对于需要把子项展开成独立记录、以便进一步处理或与其他数据集连接的复杂数据操作,这种方法非常有效。

## 5. Join operations between 2 results

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/) · 25:25

### 总结

LINQ 中的连接操作允许开发者基于匹配的属性关联来自不同集合的数据,类似于关系数据库中的连接。
本课探讨内连接(只返回匹配的记录)、分组连接(产生层级化的结果)以及左外连接(即使右侧没有匹配,也包含左侧集合中的所有记录)。
查询语法和流式(方法)语法都有演示,还包括一些高级场景,比如使用匿名类型在多个字段上连接,以及利用 DefaultIfEmpty 方法来实现外连接。

### 核心概念

*   **内连接(Inner Join)**:只返回两个集合中满足连接条件的元素。
*   **分组连接(Group Join)**:关联两个集合,并按第一个集合的元素对结果分组,形成层级结构。
*   **左外连接(Left Outer Join)**:返回左侧集合中的所有元素,以及右侧集合中匹配的元素;如果不存在匹配,右侧返回默认值(null)。
*   **多字段连接(Join on Multiple Fields)**:通过在连接条件中使用匿名类型来同时匹配多个属性实现。
*   **DefaultIfEmpty**:一个在集合为空时为其提供默认值的方法,是在 LINQ 中实现左外连接必不可少的。

### 课程笔记

LINQ 中的连接在概念上与关系数据库中的连接相似,不过它们有特定的语法要求。
连接两个集合时,你会基于共享的属性来匹配记录。
**内连接(Inner Join)** 只包含两侧都有匹配的项。
**左外连接(Left Outer Join)** 包含左侧的所有内容,即使右侧没有匹配。
**右外连接(Right Outer Join)** 对右侧做同样的事,而 **笛卡尔积(Cartesian Product)** 则把左侧的每一项与右侧的每一项相匹配(不过这很少有用,通常用嵌套循环来解决)。

#### Inner Joins

在查询语法中,内连接使用 `join` 关键字,后面跟着 `on` 和 `equals`。
注意,`equals` 是连接所要求的特定关键字;这里不能使用标准的 `==` 运算符。

```csharp
public record CastMember(string Movie, string Actor, string Role);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=155)

```csharp
void InnerJoin_Q()
{
    var allMovies = Repository.GetAllMovies();
    var castMembers = Repository.GetSomeCastMembers();

    var allCast =
        from movie in allMovies
        join castMember in castMembers on movie.Name equals castMember.Movie
        select (Movie: movie.Name, Released: movie.ReleaseDate, Actor: castMember.Actor);

    PrintAll(allCast);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=280)

在流式语法中,使用的是 `.Join()` 方法。
它需要内部集合、外部集合的键选择器、内部集合的键选择器,以及一个结果选择器函数。

```csharp
void InnerJoin_F()
{
    var allMovies = Repository.GetAllMovies();
    var castMembers = Repository.GetSomeCastMembers();

    var allCast = allMovies.Join(
        castMembers,
        movie => movie.Name,
        castMember => castMember.Movie,
        (movie, castMember) =>
            (Movie: movie.Name, Released: movie.ReleaseDate, Actor: castMember.Actor));

    PrintAll(allCast);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=415)

```text
allMovies.Join(
    castMembers,                         <- 内部集合
    movie => movie.Name,                 <- 外部集合的键选择器
    castMember => castMember.Movie,      <- 内部集合的键选择器
    (movie, castMember) => (...))        <- 结果选择器
```

#### Joining on Multiple Fields

如果连接需要匹配多个属性,你必须为左右两侧的键都创建一个匿名类型。
LINQ 会比较这些匿名类型来判断是否匹配。

```csharp
void InnerJoinMultiField_Q()
{
    var movieDirectors = Repository.GetAllMovies()
        .SelectMany(
            movie => movie.Directors,
            (movie, director) => (Movie: movie, Director: director));
    var directors = Repository.GetSomeDirectors();

    var result =
        from movieDirector in movieDirectors
        join director in directors
            on new { movieDirector.Director.FirstName, movieDirector.Director.LastName }
            equals new { director.FirstName, director.LastName }
        select (Movie: movieDirector.Movie.Name, Director: director.FullName);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=660)

在流式语法中,过程完全相同:提供返回匿名类型的 lambda,且这些匿名类型的属性名和类型要相互匹配。

```csharp
void InnerJoinMultiField_F()
{
    var movieDirectors = Repository.GetAllMovies()
        .SelectMany(
            movie => movie.Directors,
            (movie, director) => (Movie: movie, Director: director));
    var directors = Repository.GetSomeDirectors();

    var result = movieDirectors.Join(
        directors,
        movieDirector => new { movieDirector.Director.FirstName, movieDirector.Director.LastName },
        director => new { director.FirstName, director.LastName },
        (movieDirector, director) => (Movie: movieDirector.Movie.Name, Director: director.FullName));

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=825)

#### Group Joins

分组连接会关联两个集合,但它不会展平结果,而是为左侧的每一项把匹配项保留为一个集合。
在查询语法中,这是通过 `into` 关键字实现的。

```csharp
void GroupJoin_Q()
{
    var allMovies = Repository.GetAllMovies();
    var castMembers = Repository.GetSomeCastMembers();

    var allCast =
        from movie in allMovies
        join castMember in castMembers
            on movie.Name
            equals castMember.Movie
            into cast
        select (Movie: movie.Name, Actors: cast);

    foreach (var movieWithCast in allCast)
    {
        Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actors.Count()})");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=1000)

在流式语法中,使用的是 `.GroupJoin()` 方法。
结果选择器 lambda 接收外部元素,以及由所有匹配的内部元素组成的一个 `IEnumerable`。

```csharp
void GroupJoin_F()
{
    var allMovies = Repository.GetAllMovies();
    var castMembers = Repository.GetSomeCastMembers();

    var allCast = allMovies.GroupJoin(
        castMembers,
        movie => movie.Name,
        castMember => castMember.Movie,
        (movie, cast) =>
            (Movie: movie.Name, Actors: cast));

    foreach (var movieWithCast in allCast)
    {
        Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actors.Count()})");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=1135)

#### Left Outer Joins

要在 LINQ 中执行左外连接,你必须先执行一次分组连接,然后使用 `.DefaultIfEmpty()` 方法。
这样可以确保当匹配组为空时,会提供单个默认值(null),从而让左侧的记录仍然出现在输出中。

```csharp
void LeftOuterJoin_Q()
{
    var allMovies = Repository.GetAllMovies();
    var castMembers = Repository.GetSomeCastMembers();

    var allCast =
        from movie in allMovies
        join castMember in castMembers
            on movie.Name
            equals castMember.Movie
            into cast
        from actor in cast.DefaultIfEmpty()
        select (Movie: movie.Name, Actor: actor);

    foreach (var movieWithCast in allCast)
    {
        Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actor?.Actor})");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=1335)

在流式语法中,其实现方式是先调用 `.GroupJoin()`,再调用 `.SelectMany()` 来展平结果,同时对内部集合应用 `.DefaultIfEmpty()`。

```csharp
void LeftOuterJoin_F()
{
    var allMovies = Repository.GetAllMovies();
    var castMembers = Repository.GetSomeCastMembers();

    var allCast = allMovies.GroupJoin(
        castMembers,
        movie => movie.Name,
        castMember => castMember.Movie,
        (movie, cast) =>
            (Movie: movie.Name, Actors: cast))
        .SelectMany(
            movieWithActors => movieWithActors.Actors.DefaultIfEmpty(),
            (movieWithActors, actor) =>
                (Movie: movieWithActors.Movie, Actor: actor));

    foreach (var movieWithCast in allCast)
    {
        Console.WriteLine($"{movieWithCast.Movie} ({movieWithCast.Actor?.Actor})");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/join-operations-between-2-results-54280415/?t=1465)

```text
allMovies --GroupJoin--> (Movie, Actors)
                              |
                              | Actors.DefaultIfEmpty()
                              |   空组 -> 单个 null
                              v
              --SelectMany--> (Movie, Actor)
```

## 6. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280416/) · 0:42

### 总结

本课为合并多个 LINQ 数据源这一章收尾,回顾了从简单拼接到复杂连接以及基于集合的操作等各种技术。

### 核心概念

* `IEnumerable` 集合的拼接。
* 使用 `SelectMany` 展平嵌套集合。
* 集合操作:`Intersect`、`Except` 和 `Union`。
* 连接不同数据类型的集合。

### 课程笔记

本章探讨了把来自不同 LINQ 查询的结果合并成单个结果集的各种方法。
涵盖的技术包括:

* **拼接(Concatenation)**:按顺序组合多个 `IEnumerable` 序列的最简单方法。
* **使用 SelectMany 展平(Flattening with SelectMany)**:使用 `SelectMany` 投影并展平结果集中的嵌套集合(通常被称为"炸开"一个集合)。
* **集合操作(Set Operations)**:利用标准的集合逻辑比较序列,包括 `Intersect`(共同元素)、`Except`(在一边而不在另一边的元素)和 `Union`(两边的唯一元素)。
* **复杂连接(Complex Joins)**:在包含不同数据类型的集合之间执行连接,从而在 LINQ 中实现关系型风格的数据合并。

这些操作为把分散的数据源合并成统一集合、以便进一步处理或展示提供了必要的工具。

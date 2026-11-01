# Filtering & ordering

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 3 章
> 共 8 课 · 约 25:51
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to our demo solution](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/) | 3:05 | [↓](#1-introduction-to-our-demo-solution) |
| 2 | [Using a simple where clause](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-simple-where-clause-54280320/) | 4:29 | [↓](#2-using-a-simple-where-clause) |
| 3 | [Refactoring statements into functions](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/refactoring-statements-into-functions-54280321/) | 3:45 | [↓](#3-refactoring-statements-into-functions) |
| 4 | [Chaining multiple where clauses](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/chaining-multiple-where-clauses-54280322/) | 3:18 | [↓](#4-chaining-multiple-where-clauses) |
| 5 | [Ordering data](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/) | 4:00 | [↓](#5-ordering-data) |
| 6 | [Multi-level ordering](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/multi-level-ordering-54280324/) | 3:24 | [↓](#6-multi-level-ordering) |
| 7 | [Using a custom comparer to order](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-custom-comparer-to-order-54280325/) | 2:56 | [↓](#7-using-a-custom-comparer-to-order) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280326/) | 0:54 | [↓](#8-section-recap) |

## 1. Introduction to our demo solution

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/) · 3:05

### 总结

本课介绍整个课程中用来演示 LINQ 功能的演示解决方案。
该解决方案由一个提供漫威电影宇宙(MCU)电影仓储的数据项目,以及一个用于执行查询的命令行应用程序组成。
关键组件包括表示人物和电影的领域模型、一个按 MCU 阶段(Phase)和传奇(Saga)组织数据的仓储,以及一个用于统一查询执行和输出的 QueryRunner 基类。

### 核心概念

*   **数据项目结构**:一个专门用于模型和数据生成的项目。
*   **领域模型**:使用 C# record(例如 `Person`)和 DTO(例如 `Movie`),并重写 `ToString` 方法以便于调试。
*   **MarvelMovieRepository**:一个内存数据源,包含按阶段(Phase 1 到 5)和传奇(Infinity 和 Multiverse)分类的 MCU 电影。
*   **QueryRunner 基类**:一个抽象类,提供共享的仓储实例,以及把结果打印到控制台的辅助方法。
*   **命名约定**:用 `_Q` 后缀表示查询语法(Query syntax)示例,用 `_F` 后缀表示流式语法(Fluent syntax)示例,从而并排演示两种 LINQ 风格。

### 课程笔记

演示解决方案的设计力求简单,由一个用于生成信息的数据项目和一个用于执行 LINQ 代码的命令行应用程序组成。

#### 领域模型

数据项目包含简单的模型。
例如,`Person` 模型是一个 record,它重写了 `ToString` 方法,以便在控制台中得到整洁的输出。

```csharp
namespace Dometrain.Linq.Data.Models;

public record Person(string FirstName, string LastName)
{
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=25)

#### Marvel 电影仓储

`MarvelMovieRepository` 是主要的数据源。
它包含截至 Phase 5 上映的所有漫威电影宇宙电影。
该仓储提供了按特定阶段或传奇获取电影的方法。

```csharp
public class MarvelMovieRepository
{
    public IEnumerable<Movie> GetPhase1Movies()
    {
        return _phase1Movies;
    }

    public IEnumerable<Movie> GetPhase2Movies()
    {
        return _phase2Movies;
    }

    public IEnumerable<Movie> GetPhase3Movies()
    {
        return _phase3Movies;
    }

    public IEnumerable<Movie> GetPhase4Movies()
    {
        return _phase4Movies;
    }

    public IEnumerable<Movie> GetPhase5Movies()
    {
        return _phase5Movies;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=40)

该仓储还把电影归入两大传奇:Infinity Saga(Phase 1-3)和 Multiverse Saga(Phase 4-5)。

```csharp
    public IEnumerable<Movie> GetInfinitySagaMovies()
    {
        List<Movie> movies = [];
        movies.AddRange(_phase1Movies);
        movies.AddRange(_phase2Movies);
        movies.AddRange(_phase3Movies);
        return movies;
    }

    public IEnumerable<Movie> GetMultiverseSagaMovies()
    {
        List<Movie> movies = [];
        movies.AddRange(_phase4Movies);
        movies.AddRange(_phase5Movies);
        return movies;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=55)

```text
GetInfinitySagaMovies()          GetMultiverseSagaMovies()
  +-- _phase1Movies                +-- _phase4Movies
  +-- _phase2Movies                +-- _phase5Movies
  +-- _phase3Movies
```

数据通过仓储内部的内联类实例化来初始化,提供一组可供查询的静态对象。

```csharp
#region Data

private readonly List<Movie> _phase1Movies =
[
    new Movie
    {
        MovieId = Guid.Parse("bc908a6a-926f-4886-a377-ea2ff2b969c4"),
        Name = "Iron Man",
        ReleaseDate = new DateOnly(2008, 05, 02),
        Directors = [new Person("Jon", "Favreau")],
        Producers = [new Person("Avi", "Arad"), new Person("Kevin", "Feige")]
    },
    new Movie
    {
        MovieId = Guid.Parse("2b050b53-bc87-49d0-8634-c1a237021df6"),
        Name = "The Incredible Hulk",
        ReleaseDate = new DateOnly(2008, 06, 13),
        Directors = [new Person("Louis", "Leterrier")],
        Producers =
        [
            new Person("Avi", "Arad"),
            new Person("Gale Ann", "Hurd"),
            new Person("Kevin", "Feige")
        ]
    }
];
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=70)

#### 查询执行基础设施

为了统一查询的运行和展示方式,解决方案使用了一个 `QueryRunner` 基类。
这个类会实例化仓储,并提供 `Print` 和 `PrintAll` 方法把数据输出到控制台。

```csharp
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

`Program.cs` 文件用于触发特定的查询类,例如用于过滤和排序的那些类。

```csharp
using Dometrain.Linq.Cmd._3_FilteringAndOrdering;

new WhereConditions().Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=115)

#### LINQ 语法的命名约定

本课程会并排演示两种 LINQ 语法。
为了区分它们,方法会以特定的后缀命名:
*   `_Q`:查询语法(例如 `from m in movies select m`)。
*   `_F`:流式/方法语法(例如 `movies.Select(m => m)`)。

```csharp
namespace Dometrain.Linq.Cmd._3_FilteringAndOrdering;

public class WhereConditions : QueryRunner
{
    public override void Run()
    {
        SingleCondition_Q();
    }

    /// <summary>
    /// Single condition, query syntax
    /// </summary>
    private void SingleCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();
    }

    /// <summary>
    /// Single condition, fluent syntax
    /// </summary>
    private void SingleCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-to-our-demo-solution-54280319/?t=175)

## 2. Using a simple where clause

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-simple-where-clause-54280320/) · 4:29

### 总结

过滤数据是 LINQ 中的一项基础操作,它让开发者能够把源集合缩小到自己感兴趣的特定子集。
与 SQL 类似,LINQ 使用 where 子句对每个元素应用布尔条件。
本课探讨如何同时使用查询语法和流式语法来实现这些过滤,借助 string.Contains 这样的标准 C# 方法来定义过滤逻辑,并强调使用描述性的范围变量来表达意图的重要性。

### 核心概念

* **Where 子句**:用于根据布尔条件过滤数据,类似于 SQL 的 WHERE 子句。
* **查询语法**:一种声明式语法,使用 `from`、`where` 和 `select` 等关键字。
* **流式语法**:一种使用扩展方法和 lambda 表达式的方式,例如 `.Where()`。
* **语言集成**:能够在 LINQ 查询中直接使用标准 C# 方法(例如 `string.Contains`)。
* **范围变量**:在查询中用来表示当前正在处理的项的别名;为了提高可读性,描述性的名称优于 `x` 或 `y` 这样的通用名称。
* **隐式选择**:在流式语法中,如果数据的类型没有改变,对象的选择是隐含的,不需要 `.Select()` 方法。

### 课程笔记

LINQ 最常见的用途是从源数据集中过滤数据。
与 SQL 非常相似,LINQ 利用 `where` 子句来分离出感兴趣的项。
在处理电影集合时,可以编写查询来过滤特定的标题,例如包含 "Spider-Man" 的标题。

首先,我们建立一个源数据集和一个执行查询的方法:

```csharp
{
    public override void Run()
    {
        SingleCondition_Q();
    }

    /// Single condition, query syntax ...
    private void SingleCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();
    }

    /// Single condition, fluent syntax ...
    private void SingleCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-simple-where-clause-54280320/?t=10)

#### 查询语法

在查询语法中,`from` 关键字定义一个范围变量(或别名)。
虽然有些开发者会使用 `x` 或 `y` 这样的通用名称,但使用 `movie` 这样的描述性名称可以提高代码可读性并表达意图。
`where` 子句接受任何返回布尔值的 C# 表达式。
由于 LINQ 是语言集成的,`.Contains()` 这样的标准 C# 字符串方法可以直接在查询中使用。
最后,`select` 关键字指定查询应该返回什么。

```csharp
    /// Single condition, query syntax ...
    private void SingleCondition_Q()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result =
            from movie in sourceMovies
            where movie.Name.Contains("Spider-Man")
            select movie;

        PrintAll(result);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-simple-where-clause-54280320/?t=130)

结果变量是一个 `IEnumerable<Movie>`,只包含满足条件的对象。

#### 流式语法

流式语法使用 `.Where()` 扩展方法实现相同的结果。
这个方法需要一个谓词函数(通常是一个 lambda 表达式),它接收源类型并返回一个布尔值。

流式语法的一个优点是简洁:如果你没有把数据转换成另一种类型,`select` 语句就是隐含的。
如果你想取回原始对象,不需要显式调用 `.Select(movie => movie)`。

```csharp
    /// Single condition, fluent syntax ...
    private void SingleCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Where(movie => movie.Name.Contains("Spider-Man"));

        PrintAll(result);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-simple-where-clause-54280320/?t=250)

这种方式非常灵活;例如,把过滤条件改为 "Iron",就会立即更新结果集,使用同样的扩展方法逻辑包含相关的电影。

```csharp
    /// Single condition, fluent syntax ...
    private void SingleCondition_F()
    {
        var sourceMovies = Repository.GetAllMovies();

        var result = sourceMovies
            .Where(movie => movie.Name.Contains("Iron"));

        PrintAll(result);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-simple-where-clause-54280320/?t=265)

## 3. Refactoring statements into functions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/refactoring-statements-into-functions-54280321/) · 3:45

把 LINQ 语句重构为函数可以增强代码可读性,并更清晰地表达意图。
通过把 where 子句中的复杂逻辑提取到专门的布尔方法中,开发者可以同时简化查询语法和流式语法。
这种方式还使得在流式语法中可以使用方法组(method group),进一步减少语法上的开销,把注意力集中在所执行的逻辑上。

### 核心概念

- 为了意图和可读性而重构
- 把复杂的谓词提取为具名方法
- 在查询语法中使用布尔方法
- 在流式语法中使用布尔方法
- 用方法组简化流式语法

### 课程笔记

LINQ 查询是标准的 C# 代码,这意味着可以用整洁代码原则来重构它们,以更好地表达意图。
当一个 `where` 条件变得复杂或难以一眼看懂时,最佳实践是把这段逻辑提取到一个专门的函数中。

#### 重构查询语法

在查询语法中,复杂的内联条件可以替换为对一个布尔函数的调用。
这让查询更具表达力,因为它描述的是过滤的*是什么*,而不是*如何*过滤。

```csharp
private void SingleFunctionCondition_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = 
        from movie in sourceMovies
        where movie.Name.Contains("Spider-Man")
        select movie;

    PrintAll(result);
}

private void SingleFunctionCondition_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Where(movie => movie.Name.Contains("Iron Man"));

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/refactoring-statements-into-functions-54280321/?t=10)

把逻辑提取到一个名为 `IsSpiderManMovie` 的静态方法后,查询变得明显更容易阅读。

```csharp
private void SingleFunctionCondition_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = 
        from movie in sourceMovies
        where IsSpiderManMovie(movie)
        select movie;

    PrintAll(result);
}

private static bool IsSpiderManMovie(Movie movie)
{
    return movie.Name.Contains("Spider-Man");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/refactoring-statements-into-functions-54280321/?t=100)

#### 重构流式语法

同样的原则也适用于流式语法。
一开始,你可能会用一个 lambda 表达式来调用提取出来的函数。

```csharp
private void SingleFunctionCondition_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Where(movie => IsIronManMovie(movie));

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/refactoring-statements-into-functions-54280321/?t=160)

#### 使用方法组

当一个 lambda 表达式只是把单个参数直接传给一个函数并返回该函数的结果时,C# 允许使用**方法组**。
这去掉了 lambda 语法(`movie => ...`)的"冗余",只留下方法名。
这让语句更加简洁易读。

```csharp
private void SingleFunctionCondition_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Where(IsIronManMovie);

    PrintAll(result);
}

private static bool IsIronManMovie(Movie movie)
{
    return movie.Name.Contains("Iron Man");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/refactoring-statements-into-functions-54280321/?t=190)

把逻辑提取到函数中不仅能提高可读性,还能让代码在将来更容易重构和维护。

## 4. Chaining multiple where clauses

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/chaining-multiple-where-clauses-54280322/) · 3:18

### 总结

在 LINQ 中链接多个 where 子句,让开发者可以把复杂的过滤逻辑拆分成清晰、可读的不同步骤。
虽然 && 这样的标准 C# 逻辑运算符可以在单个子句中使用,但 LINQ 在查询语法和流式语法中都支持多个顺序的 where 语句。
这种方式保留了短路求值的效率(后续的过滤器只处理满足了前面过滤器的元素),同时通过把不相关的逻辑条件分开来改善代码组织。

### 核心概念

* **逻辑与(&&)与链式 where 子句**:两者得到相同的结果,但对于复杂逻辑,链式写法可以提高可读性。
* **短路求值**:LINQ 只对通过了前面过滤器的项求值后续过滤器,从而保持效率。
* **查询语法链式写法**:在单个查询表达式中多次使用 `where` 关键字。
* **流式语法链式写法**:在集合上依次调用 `.Where()` 扩展方法。
* **关注点分离**:把相关的条件组合在一起,同时把逻辑上不同的过滤器分开。

### 课程笔记

在过滤数据时,经常会有多个必须同时满足的条件。
例如,你可能先用查询语法或流式语法写一个按名称查找电影的基本过滤器。

```csharp
/// Multiple chained conditions, query syntax ...
private void MultipleConditions_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result =
        from movie in sourceMovies
        where movie.Name.Contains("Spider-Man")
        select movie;

    PrintAll(result);
}

/// Multiple chained conditions, fluent syntax ...
private void MultipleConditions_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .Where(movie => movie.Name.Contains("Iron Man"));

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/chaining-multiple-where-clauses-54280322/?t=10)

要过滤出 2020 年之前上映的 "Spider-Man" 电影,一种标准做法是在单个 `where` 子句中使用 C# 的逻辑与运算符(`&&`)。

```csharp
/// Multiple chained conditions, query syntax ...
private void MultipleConditions_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result =
        from movie in sourceMovies
        where movie.Name.Contains("Spider-Man")
        && movie.ReleaseDate.Year < 2020
        select movie;

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/chaining-multiple-where-clauses-54280322/?t=35)

这种方式是高效的,因为它利用了短路求值:只有在名称条件已经满足时才会检查上映年份。
然而,随着条件语句不断增长,包含各种 `AND` 和 `OR` 运算符,就很难追踪哪些条件是一组的。

LINQ 内置了对多个 `where` 子句的支持来解决这个问题。
在查询语法中,你只需再添加一个 `where` 关键字。
这保持了同样的短路性能(第二个子句只对通过了第一个过滤器的电影求值),但提供了更好的逻辑分离。

```csharp
/// Multiple chained conditions, query syntax ...
private void MultipleConditions_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result =
        from movie in sourceMovies
        where movie.Name.Contains("Spider-Man")
        where movie.ReleaseDate.Year < 2020
        select movie;

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/chaining-multiple-where-clauses-54280322/?t=85)

```text
sourceMovies
     |
     v
where movie.Name.Contains("Spider-Man")   --- 不满足 ---> 丢弃
     |  满足
     v
where movie.ReleaseDate.Year < 2020       --- 不满足 ---> 丢弃
     |  满足
     v
   result
```

这种模式同样适用于流式语法。
如果你想过滤出 2000 年代早期上映的初代 Iron Man 电影,可以链接多个 `.Where()` 调用。
当逻辑条件(例如名称和上映日期)彼此独立时,这是首选的风格。

```csharp
/// Multiple chained conditions, fluent syntax ...
        private void MultipleConditions_F()
        {
            var sourceMovies = Repository.GetAllMovies();

            var result = sourceMovies
                .Where(movie => movie.Name.Contains("Iron Man"))
                .Where(movie => movie.ReleaseDate.Year <= 2010);


            PrintAll(result);
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/chaining-multiple-where-clauses-54280322/?t=160)

## 5. Ordering data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/) · 4:00

### 总结

本课演示如何在 LINQ 中同时使用查询语法和流式语法对数据进行排序。
内容涵盖升序和降序、在方法链中使用键选择器,以及一个底层要求:被排序的属性或对象必须实现 IComparable 接口,才能保证结果可预测。

### 核心概念

- 查询语法中的 orderby 子句默认按升序排列。
- descending 关键字修饰 orderby 子句以进行反向排序。
- 流式语法使用 OrderBy 和 OrderByDescending 方法。
- lambda 表达式充当键选择器,用于指定排序所依据的属性。
- 排序要求目标类型实现 IComparable。

### 课程笔记

对数据进行排序是一项基础操作,用来保证信息按合乎逻辑的顺序呈现。
在 LINQ 中,这通过查询语法中的 orderby 子句或流式语法中的特定扩展方法来实现。

#### 查询语法

在查询语法中,orderby 关键字用于对结果进行排序。
与 SQL 中指定列名不同,LINQ 使用模型类的属性。
默认情况下,orderby 会根据所选属性把集合按升序排列。

```csharp
private void SingleOrderBy_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result =
        from movie in sourceMovies
        orderby movie.Name
        select movie;

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/?t=40)

要在查询语法中按降序排序,在 orderby 子句后追加 descending 关键字。

```csharp
private void SingleOrderByDescending_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result =
        from movie in sourceMovies
        orderby movie.Name descending
        select movie;

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/?t=145)

#### 流式语法

在流式(或方法)语法中,排序通过 OrderBy 方法完成。
这个方法需要一个键选择器(一个 lambda 表达式)来确定用哪个属性进行排序。

```csharp
private void SingleOrderBy_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .OrderBy(movie => movie.Name);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/?t=130)

要按降序排序,使用 OrderByDescending 方法。
虽然 LINQ 中也存在 OrderDescending 方法,但它与 OrderByDescending 不同,因为它尝试使用对象本身的默认比较器,而不是某个特定属性。

```csharp
private void SingleOrderByDescending_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .OrderByDescending(movie => movie.Name);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/?t=235)

#### IComparable 要求

LINQ 中的所有排序操作都依赖于 IComparable 接口。
string、int 和 DateTime 这样的 .NET 内置类型已经实现了这个接口。
如果你尝试按对象本身(而不是某个属性)对集合排序,而该类没有实现 IComparable,LINQ 会抛出一个异常,指出至少有一个对象必须实现 IComparable。

```csharp
private void SingleOrderByDescending_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .OrderByDescending(movie => movie);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/ordering-data-54280323/?t=220)

## 6. Multi-level ordering

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/multi-level-ordering-54280324/) · 3:24

### 总结

本课演示如何在 LINQ 中同时使用查询语法和流式语法进行多级排序。
它解释了如何先按主字段排序,再按后续字段进行子排序,并重点介绍维持排序层级所需的特定关键字和方法,以及如何避免覆盖之前排序顺序这样的常见陷阱。

### 核心概念

* 在查询语法中使用逗号分隔的表达式进行多级排序。
* 流式语法中的 `ThenBy` 和 `ThenByDescending` 方法。
* 排序方向:`ascending`(默认)和 `descending` 可以独立应用于每一级。
* 在应用次要条件时保留主排序顺序。
* 链接 `ThenBy` 与多次调用 `OrderBy` 之间的技术区别。

### 课程笔记

在很多场景中,只按单个字段排序是不够的。
LINQ 支持多级排序:集合先按主字段排序,然后主字段值相同的元素再按次要(或第三)字段排序。
这在功能上等价于 SQL 中带多个列的 `ORDER BY` 子句。

#### 查询语法

在查询语法中,多级排序通过在 `orderby` 关键字后提供一个逗号分隔的属性列表来实现。
列表中的每个属性都可以有自己的排序方向。
虽然 `ascending` 是默认值,通常省略不写,但 `descending` 关键字可以应用于序列中的任何特定字段以反转其顺序。

```csharp
/// Multiple order by, query syntax ...
private void MultipleOrderBy_Q()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = 
        from movie in sourceMovies
        orderby movie.ReleaseDate.Year descending, movie.Name
        select movie;

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/multi-level-ordering-54280324/?t=70)

#### 流式语法

在流式(方法)语法中,你不能在单个 `OrderBy` 调用中使用逗号分隔的列表。
相反,你必须使用 `ThenBy` 或 `ThenByDescending` 扩展方法。
这个过程从一个初始的 `OrderBy` 或 `OrderByDescending` 调用开始,它返回一个 `IOrderedEnumerable<T>`。
随后的各级排序再通过 `ThenBy` 或 `ThenByDescending` 链接起来。

```csharp
/// Multiple order by, fluent syntax ...
private void MultipleOrderBy_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .OrderByDescending(movie => movie.ReleaseDate.Year)
        .ThenBy(movie => movie.Name);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/multi-level-ordering-54280324/?t=190)

```text
sourceMovies
     |
     |  .OrderByDescending(movie => movie.ReleaseDate.Year)
     v
IOrderedEnumerable<T>
     |
     |  .ThenBy(movie => movie.Name)
     v
   result
```

#### 保留排序顺序

使用 `ThenBy` 进行次要排序是至关重要的。
如果你在链中多次调用 `OrderBy`,每次后续调用都会完全重新排序集合,实际上覆盖并丢弃了前一次 `OrderBy` 调用的结果。
`ThenBy` 专门设计为作用于 `IOrderedEnumerable<T>`,确保它只对按前面的排序条件相等的元素进行排序。
你可以根据需要链接任意多个 `ThenBy` 调用,以达到所需的粒度。

## 7. Using a custom comparer to order

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-custom-comparer-to-order-54280325/) · 2:56

### 总结

LINQ 的排序方法要求被排序的元素或键实现 IComparable 接口。
当对没有实现该接口的复杂对象排序,或者需要特定的多属性排序逻辑时,可以把一个实现了 IComparer<T> 的自定义比较器作为参数传给 OrderBy 或 OrderByDescending 方法。
这种方式只适用于流式(方法)语法,并能精确控制比较逻辑,包括在单个方法调用中处理 null 和次要排序条件。

### 核心概念

*   **IComparable 要求**:默认情况下,LINQ 排序要求类型实现 `IComparable`。
    尝试对未实现它的类型排序会导致 `InvalidOperationException`,提示至少有一个对象必须实现 `IComparable`。
*   **IComparer<T> 接口**:用于在被排序的类之外定义自定义比较逻辑。
    它要求实现一个 `Compare` 方法。
*   **比较逻辑**:`Compare` 方法返回一个整数:如果第一个对象较小则为负值,较大则为正值,相等则为零。
*   **仅限流式语法**:自定义比较器只能在流式(方法)语法中作为参数传入;查询表达式语法不支持它们。

### 课程笔记

当尝试对没有实现 `IComparable` 的对象集合排序时,例如自定义的 `Movie` 类,LINQ 会在运行时抛出异常。

```csharp
/// Single order by using a custom comparer, fluent syntax ...
private void OrderByCustomComparer_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .OrderBy(movie => movie);

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-custom-comparer-to-order-54280325/?t=10)

要解决这个问题,你可以创建一个实现 `IComparer<T>` 的类。
这个类必须定义一个 `Compare` 方法,接收该类型的两个实例,并返回一个表示它们相对顺序的整数。
实现通常会先处理引用相等和 null 检查,然后再进行排序所需的具体属性比较。

在下面的示例中,`MovieComparer` 主要按上映年份对电影排序。
如果年份相同,它会退而对电影名称进行序数(ordinal)比较。

```csharp
class MovieComparer : IComparer<Movie>
{
    public int Compare(Movie? first, Movie? second)
    {
        // Same instance
        if (ReferenceEquals(first, second)) return 0;

        // Null is smaller than everything
        if (first is null) return -1;
        if (second is null) return 1;

        // If the years are different, sort by year
        if (first.ReleaseDate.Year < second.ReleaseDate.Year)
            return -1;
        if (first.ReleaseDate.Year > second.ReleaseDate.Year)
            return 1;

        // If the years are equal, sort by name
        return string.Compare(first.Name, second.Name, StringComparison.Ordinal);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-custom-comparer-to-order-54280325/?t=55)

```text
Compare(first, second)
  ReferenceEquals(first, second)          --> 0
  first is null                           --> -1
  second is null                          --> 1
  first.ReleaseDate.Year <  second's      --> -1
  first.ReleaseDate.Year >  second's      --> 1
  otherwise                               --> string.Compare(first.Name,
                                                second.Name,
                                                StringComparison.Ordinal)
```

比较器定义好之后,把它作为第二个参数传给 `OrderBy` 方法。
这让查询能够在不抛出异常的情况下执行,并应用自定义逻辑来确定结果的顺序。

```csharp
private void OrderByCustomComparer_F()
{
    var sourceMovies = Repository.GetAllMovies();

    var result = sourceMovies
        .OrderBy(movie => movie, new MovieComparer());

    PrintAll(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/using-a-custom-comparer-to-order-54280325/?t=130)

当你选择某个没有实现 `IComparable` 的特定属性进行排序时,这种模式同样适用。
通过为该特定属性类型提供一个比较器,你可以保持流式 API 的灵活性。

## 8. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280326/) · 0:54

### 总结

本课总结了 LINQ 中关于过滤和排序的这一部分,强调这些功能是在代码库中采用 LINQ 的主要切入点。
通过用声明式查询替代命令式的循环和条件语句,LINQ 显著提升了代码的表达力和长期可维护性。

### 核心概念

- LINQ 的主要用例:查询、过滤和排序数据。
- 替代命令式模式:LINQ 消除了对手动循环和嵌套条件语句的需要。
- 代码表达力:声明式语法提高了可读性和可理解性。
- 可维护性:编写更整洁的代码可以减少留给未来开发者的技术债务。

### 课程笔记

LINQ 的核心价值在于它能够高效地查询、过滤和排序数据。
这些操作通常是开发者最先应用 LINQ 来改进代码库的地方。
通过采用 LINQ,开发者可以摆脱手动迭代和条件逻辑那种冗长的模式。

从命令式循环转向 LINQ 查询,会让代码更具表达力。
这种表达力对可维护性至关重要;它让未来的开发者能够快速理解代码的逻辑和意图。
今天编写可维护的代码可以避免技术债务并简化将来的更新,这使得 LINQ 成为数据操作中优于传统控制流结构的首选方式。

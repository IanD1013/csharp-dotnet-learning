# How LINQ works and what to use it for

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 8 章
> 共 6 课 · 约 30:34
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-54280428/) | 1:02 | [↓](#1-introduction) |
| 2 | [IQueryable vs IEnumerable](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-vs-ienumerable-54280423/) | 4:38 | [↓](#2-iqueryable-vs-ienumerable) |
| 3 | [What can we write LINQ against?](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-can-we-write-linq-against-54280424/) | 3:48 | [↓](#3-what-can-we-write-linq-against) |
| 4 | [Making some IEnumerable methods ourselves](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/) | 9:07 | [↓](#4-making-some-ienumerable-methods-ourselves) |
| 5 | [IQueryable behind the scenes](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/) | 10:47 | [↓](#5-iqueryable-behind-the-scenes) |
| 6 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280427/) | 1:12 | [↓](#6-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/introduction-54280428/) · 1:02

### 总结

本课引入对 LINQ 内部原理的技术深挖,强调理解一个库的底层机制对于有效使用它的重要性。
通过"揭开魔法",开发者可以更清楚地看到 LINQ 如何处理数据,以及它在底层的行为应当如何预期。

### 核心概念

- **内部机制(Internal Mechanics)**:超越语法,理解 LINQ 是如何构建的。
- **揭开魔法(Taking Away the Magic)**:一种学习框架底层实现以确保正确使用的理念。
- **可预测的使用(Predictable Usage)**:了解"底层"的操作如何带来更好的架构决策。

### 课程笔记

前面的内容侧重于 LINQ 语法、查询执行和数据操作,但理解该框架的内部运作同样至关重要。
"揭开魔法"这一概念是指深入研究一个库的实现细节,从而让它的行为不再神秘。

理解这些内部原理并不是为了重新构建这个库,而是为了获得正确使用它所需的洞察。
当开发者理解幕后发生了什么时,就能更好地预测性能特征并避开常见陷阱。
本章将探讨 LINQ 的基础机制,以及在 .NET 开发中使用它时应有的行为预期。

## 2. IQueryable vs IEnumerable

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-vs-ienumerable-54280423/) · 4:38

### 总结

LINQ 通过两种不同的机制运作:用于内存中集合的 IEnumerable,以及用于外部数据源的 IQueryable。
IEnumerable 利用迭代器在应用程序的内存中处理数据,而 IQueryable 借助 LINQ 提供程序把查询翻译成 SQL 等原生格式,在远程服务器上执行。
理解这两种类型之间的转换(尤其是在物化期间)对于优化查询性能、确保逻辑在预期的环境中执行至关重要。

### 核心概念

- **IEnumerable**:表示一个可以在内存中借助迭代器(指针)遍历的元素集合。
- **IQueryable**:表示针对某种底层数据结构或技术的查询,该技术支持自己的查询语言。
- **LINQ 提供程序(LINQ Providers)**:像 Entity Framework 这样的技术,把 LINQ 表达式翻译成提供程序特定的语言(例如 SQL、XML、OData)。
- **物化(Materialization)**:执行查询并把结果转换为内存中集合的过程(例如通过 `.ToList()`),这会让对象从 `IQueryable` 转换为 `IEnumerable`。

### 课程笔记

#### IEnumerable 与内存中迭代

LINQ to `IEnumerable` 是为已经存在于应用程序内存中的集合设计的。
`IEnumerable` 提供一个迭代器,它本质上是一个逐个元素在集合中移动的指针。

针对 `IEnumerable` 编写 LINQ 查询时,这些操作是以扩展方法的形式实现的。
这些方法执行的代码会遍历集合,以过滤、投影或转换数据。
由于逻辑是在 .NET 运行时内执行的,集合中的每一个元素都必须由迭代器处理,才能判断它是否符合查询条件。

#### IQueryable 与查询翻译

`IQueryable` 的运作方式不同,它面向支持原生查询的底层技术。
一个常见的例子是通过 Entity Framework 访问的关系型数据库。
在这种场景下,`IQueryable` 代表的是数据库中的表和数据,而不是一个本地的对象列表。

针对 `IQueryable` 编写 LINQ 查询时,LINQ 提供程序(例如 Entity Framework)并不会立即遍历数据。
相反,它会把 LINQ 表达式翻译成数据源的原生语言,例如 SQL。
这个翻译后的查询随后由数据库引擎执行。
对于大型数据集,这种方式要高效得多,因为返回给应用程序的只有查询结果,而不是把整个数据集拉进内存再过滤。

除了关系型数据库,`IQueryable` 还可以用来为其他各种提供程序翻译 LINQ 查询,包括:
- XML 文件
- Google 搜索查询
- OData API
- 其他外部数据结构

#### 转换:物化

在同一条数据管道中在 `IQueryable` 和 `IEnumerable` 之间切换是很常见的。
这种转换发生在物化期间。
当在 `IQueryable` 上调用 `.ToList()`、`.ToArray()` 或 `.First()` 这样的方法时,查询会针对提供程序执行,结果会被加载到一个内存中的集合里。

一旦物化,该对象就变成了 `IEnumerable`。
此后在该对象上执行的任何 LINQ 查询都会使用 `IEnumerable` 机制在内存中执行,而不会被翻译成 SQL 或提供程序的原生语言。
开发者必须留意在哪里跨越了这道"鸿沟",以免不小心在内存中执行了本应由数据库服务器处理的繁重运算。

```text
  IQueryable                                IEnumerable
  (provider: SQL, ...)                      (in-memory)
+----------------------+  .ToList()     +----------------------+
| LINQ query           |  .ToArray()    | results loaded into  |
| translated to SQL,   |--------------->| memory; later LINQ   |
| run by the database  |  .First()      | runs in-memory       |
+----------------------+                +----------------------+
```

## 3. What can we write LINQ against?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/what-can-we-write-linq-against-54280424/) · 3:48

### 总结

自 2008 年推出以来,LINQ(Language Integrated Query,语言集成查询)已经成为 .NET 生态中查询数据的通用标准。
它提供统一的 API 和语法,让开发者无需学习提供程序特定的查询语言,就能与从关系型数据库、搜索引擎到软件 API 和 Web 服务等大量数据源进行交互。
借助 IEnumerable 和 IQueryable 等接口,LINQ 确保代码在不同的存储技术之间保持一致、易于维护。

### 核心概念

* **在 .NET 中无处不在**:15 年多以来,LINQ 一直是数据查询的标准。
* **ORM 集成**:主要的对象关系映射器都完整支持它,包括 Entity Framework、NHibernate 和 LLBLGen Pro。
* **多样的数据源**:能够查询数据库、搜索引擎、Excel 电子表格以及遗留的 DataSet。
* **Web API 查询**:与 OData 等基于图的 API 集成,LINQ 表达式会被翻译成远程 API 请求。
* **统一的语法**:用同一套 C# API 查询各种不同类型的数据,提升代码可读性和开发者生产力。
* **IQueryable 的威力**:许多 NuGet 包借助 IQueryable 为复杂查询提供深度集成。

### 课程笔记

LINQ 在 .NET 框架中的长久存在,使它几乎被生态中所有与数据相关的技术所采用。
这种广泛采用主要得益于大量为各种平台提供 LINQ 提供程序的 NuGet 包。

#### 数据库与 ORM 支持

LINQ 最常见的用途是与数据库交互。
所有主要的 .NET 对象关系映射器(ORM)都支持 LINQ,包括 Entity Framework、NHibernate 和 LLBLGen Pro。
除了 ORM 之外,许多原生数据库提供程序也直接支持 LINQ,从而无需中间映射层就能高效查询。

#### 搜索引擎与软件 API

LINQ 的用途不止于传统的关系型数据库。
它经常被用来对接搜索引擎,无论这些搜索引擎是本地部署的还是以云服务形式提供的。
此外,软件特定的 API 允许把 LINQ 用于 Microsoft Excel 这样的应用程序,让开发者能够像查询普通集合一样查询电子表格数据。

#### 遗留技术与 Web 技术

虽然 DataSet 这类技术被认为比较老旧,但 LINQ 仍然为查询它们提供了可靠的支持,即使它们连接着一个活动的数据库。
在现代 Web 开发中,LINQ 在查询 OData 这类基于图的 API 方面发挥着重要作用。
在这些场景中,LINQ 查询会被转换为特定的 API 请求,并在服务器端执行。

#### NuGet 与 IQueryable 的作用

现代 .NET 生态在很大程度上依赖 NuGet 包来扩展 LINQ 的覆盖范围。
虽然任何暴露 IEnumerable 的 API 都可以使用 LINQ 的基础功能,但如今许多包会暴露 IQueryable。
这使得查询翻译更加强大,LINQ 表达式树会针对被查询的具体数据源进行分析和优化。
这种统一的方式确保了无论底层技术是什么,用来操作数据的 C# 代码都保持一致。
由于 LINQ 很快就成为事实上的标准,Microsoft 在它发布后不久就停止维护官方的提供程序列表,因为它的采用已经普及。

## 4. Making some IEnumerable methods ourselves

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/) · 9:07

### 总结

本课通过实现自定义版本的 Where 和 FirstOrDefault 扩展方法来探究 LINQ 的内部机制。
它演示了 IEnumerable<T> 和 yield 关键字如何借助一种拉取式模型实现延迟执行和高效的数据处理:元素只在终结运算符需要时才被处理,而不是被物化到中间集合中。

### 核心概念

- **扩展方法(Extension Methods)**:LINQ 操作以静态类中的静态方法实现,通过 `this` 关键字扩展 `IEnumerable<T>`。
- **Yield Return**:一个允许方法逐个返回元素的关键字,在每次请求之间暂停执行并保持状态。
- **延迟执行(Deferred Execution)**:LINQ 查询是对工作的定义,在结果真正被迭代或调用终结方法之前不会执行。
- **拉取模型(Pull Model)**:执行的责任从链中的最后一个方法向上流回源集合,确保只处理必要数量的数据。

### 课程笔记

为了理解 LINQ 如何针对 `IEnumerable<T>` 运作,我们可以用自定义实现替换标准的 LINQ 操作。
来看一个过滤集合并获取第一个匹配项的标准查询:

```csharp
namespace Dometrain.Linq.Cmd._8_HowLinqWorks;

public class EnumerableExample : QueryRunner
{
    public override void Run()
    {
        HomeLinqMethods();
    }

    void HomeLinqMethods()
    {
        var allMovies = Repository.GetAllMovies();

        var result = allMovies
            .Where(movie => movie.Phase == 4)
            .FirstOrDefault();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/?t=10)

我们可以在一个静态类中把它们重新实现为 `MyWhere` 和 `MyFirstOrDefault`。
LINQ 方法本质上就是以 `IEnumerable<T>` 为目标的扩展方法。

```csharp
namespace Dometrain.Linq.Cmd._8_HowLinqWorks;

public class EnumerableExample : QueryRunner
{
    public override void Run()
    {
        HomeLinqMethods();
    }

    void HomeLinqMethods()
    {
        var allMovies = Repository.GetAllMovies();

        var result = allMovies
            .MyWhere(movie => movie.Phase == 4)
            .MyFirstOrDefault();
    }
}

public static class MyLinqMethods
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/?t=55)

#### 实现 MyWhere

`MyWhere` 方法需要一个源集合和一个定义过滤条件的谓词。
我们不去填充一个新的 `List<T>` 再返回它(那样效率很低),而是使用 `yield return` 关键字。
这使得该方法可以返回一个惰性产生元素的 `IEnumerable<T>`。

```csharp
public static class MyLinqMethods
{
    public static IEnumerable<T> MyWhere<T>(
        this IEnumerable<T> source,
        Predicate<T> condition)
    {
        foreach (var sourceItem in source)
        {
            if (condition(sourceItem))
                yield return sourceItem;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/?t=175)

当向 `MyWhere` 请求一个元素时,代码会一直执行到遇到 `yield return`,然后暂停,直到下一个元素被请求。
正是这种责任的委托让 LINQ 变得高效。

#### 实现 MyFirstOrDefault

`MyFirstOrDefault` 方法是一个终结运算符。
它遍历源集合,并返回它遇到的第一个元素。
如果源集合为空,它就返回该类型的默认值。

```csharp
    public static class MyLinqMethods
    {
        public static IEnumerable<T> MyWhere<T>(
            this IEnumerable<T> source,
            Predicate<T> condition)
        {
            foreach (var sourceItem in source)
            {
                if (condition(sourceItem))
                    yield return sourceItem;
            }
        }

        public static T? MyFirstOrDefault<T>(this IEnumerable<T> source)
        {
            foreach (var sourceItem in source)
            {
                return sourceItem;
            }

            return default;
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/?t=315)

#### 执行流程与延迟执行

在 `allMovies.MyWhere(...).MyFirstOrDefault()` 这样的查询中,执行遵循一种"拉取"模型:
1. `MyFirstOrDefault` 开始它的 `foreach` 循环,并向它的源(即 `MyWhere` 的结果)请求第一个元素。
2. `MyWhere` 开始它的 `foreach` 循环,并逐个向 `allMovies` 请求元素。
3. 对于来自 `allMovies` 的每个元素,`MyWhere` 都会检查谓词。如果匹配,它就 `yield return` 该元素。
4. 一旦 `MyWhere` 产出一个元素,`MyFirstOrDefault` 就会收到它,立即返回它,并终止自己的循环。
5. 由于 `MyFirstOrDefault` 停止了迭代,`MyWhere` 也就不再向源集合请求元素。

```text
allMovies            MyWhere                 MyFirstOrDefault
    |                   |   <-- request item ---   |   (1)
    |  <-- request -----|                          |   (2)
    |  --- item ------> |  condition(item)?        |   (3)
    |                   |  --- yield return -----> |   (4) return item
    |   (no more items requested)                  |   (5) loop ends
```

这展示了延迟执行:`MyWhere` 方法调用本身只是定义查询,什么也不做。
直到 `MyFirstOrDefault`(终结运算符)开始沿着链拉取结果,才会发生迭代。

```csharp
void HomeLinqMethods()
{
    var allMovies = Repository.GetAllMovies();

    var result = allMovies
        .MyWhere(movie => movie.Phase == 4)
        .MyFirstOrDefault();

    Console.WriteLine(result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/making-some-ienumerable-methods-ourselves-54280425/?t=445)

## 5. IQueryable behind the scenes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/) · 10:47

### 总结

IQueryable 是 LINQ 提供程序的基础,它允许把 C# 表达式翻译成针对数据库或 API 等外部数据源的查询。
与操作内存中集合的 IEnumerable 不同,IQueryable 维护着一棵表示查询逻辑的表达式树。
实现一个自定义提供程序需要创建一个 IQueryProvider 来管理查询的创建与执行,并利用一个 ExpressionVisitor 来遍历表达式树、把它翻译成目标查询语言。
这个过程实现了延迟执行,即只有在真正请求数据时,查询才会被翻译和执行。

### 核心概念

- **表达式树(Expression Trees)**:以数据结构而非编译后的 IL 来表示代码逻辑,从而可以被检查和翻译。
- **IQueryProvider**:负责创建 `IQueryable` 实例并执行它们所表示的查询的接口。
- **ExpressionVisitor**:`System.Linq.Expressions` 中的一个工具类,用于遍历并可能修改表达式树。
- **延迟执行(Deferred Execution)**:`IQueryable` 查询的翻译和执行只在请求迭代器时才会发生(例如通过 `foreach` 循环或 `ToList()` 这样的终结运算符)。
- **翻译策略(Translation Strategy)**:递归访问表达式树中的节点,以构建特定技术的查询(例如 SQL 或 REST API 调用)的过程。

### 课程笔记

实现一个自定义的 `IQueryable` 提供程序是一项复杂的任务,需要把 LINQ 查询转换成底层技术可以执行的格式。
这涉及访问整棵表达式树,并一块一块地把它转换成查询。

#### 查询提供程序

任何 LINQ 提供程序的核心都是 `IQueryProvider`。
该接口必须实现 `CreateQuery` 以返回一个 `IQueryable`,并实现 `Execute` 以执行实际的数据获取。

```csharp
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
    public class TerraServerQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableTerraServerData<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        // Queryable's collection-returning standard query operators
        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new QueryableTerraServerData<TResult>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return TerraServerQueryContext.Execute(expression, false);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=65)

除了提供程序之外,你还必须定义提供程序将要返回的数据模型。
这些模型表示底层数据源的结构(schema)。

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToTerraServerProvider
{
    public class Place
    {
        // Properties.
        public string Name { get; private set; }
        public string State { get; private set; }
        public PlaceType PlaceType { get; private set; }

        // Constructor.
        internal Place(string name,
                       string state,
                       int placeType)
        {
            Name = name;
            State = state;
            PlaceType = (PlaceType)placeType;
        }
    }

    public enum PlaceType
    {
        Unknown,
        AirRailStation,
        BayGulf,
        CapePeninsula,
        CityTown
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=75)

#### 表达式访问器

为了弄清用户在 LINQ 查询中请求的是什么,提供程序会使用一个 `ExpressionVisitor`。
这个类会遍历表达式树,识别特定的子句,例如最内层的 `Where` 子句。

```csharp
using System;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
    internal class InnermostWhereFinder : ExpressionVisitor
    {
        // Implementation details for finding specific nodes
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=100)

#### 可视化表达式树

当你使用 `IQueryable` 定义一个查询时,查询本身是以表达式树的形式存储的。
你可以通过 `.Expression` 属性访问这棵树。

```csharp
using System.Linq.Expressions;

namespace Dometrain.Linq.Cmd._8_HowLinqWorks;

public class QueryableExample : QueryRunner
{
    public override void Run()
    {
        QueryExplorer();
    }

    void QueryExplorer()
    {
        var allMovies = Repository.GetAllMoviesAsQueryable();

        var query = allMovies
            .Where(movie => movie.Phase == 4)
            .OrderBy(movie => movie.ReleaseDate)
            .Take(3);

        var queryExpression = query.Expression;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=160)

直接把 `queryExpression` 打印到控制台,会得到所调用 LINQ 方法的一个精简字符串表示,但它不会显示底层的树结构。

```csharp
// Console.WriteLine(queryExpression) output:
[Dometrain.Linq.Data.Models.Movie].Where(movie => (movie.Phase == 4)).OrderBy(movie => movie.ReleaseDate).Take(3)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=200)

要看到真实的结构,你可以实现一个自定义的 `ExpressionVisitor`,在访问每个节点时把它记录下来。
表达式树的入口点是链中最后调用的那个方法(例如 `Take`)。

```csharp
void QueryExplorer()
{
    var allMovies = Repository.GetAllMoviesAsQueryable();

    var query = allMovies
        .Where(movie => movie.Phase == 4)
        .OrderBy(movie => movie.ReleaseDate)
        .Take(3);

    var queryExpression = query.Expression;

    new ExpressionFormatter().Visit(queryExpression);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=265)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
using System.Linq.Expressions;

namespace Dometrain.Linq.Cmd._8_HowLinqWorks;

public class QueryableExample : QueryRunner
{
    public override void Run()
    {
        QueryExplorer();
    }

    void QueryExplorer()
    {
        var allMovies = Repository.GetAllMoviesAsQueryable();

        var query = allMovies
            .Where(movie => movie.Phase == 4)
            .OrderBy(movie => movie.ReleaseDate)
            .Take(3);

        new ExpressionFormatter().Visit(query.Expression);
    }
}

public class ExpressionFormatter : ExpressionVisitor
{
    private string _indentation = string.Empty;

    protected override Expression VisitBinary(BinaryExpression node)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=115)

生成的树形可视化显示了 `Take` 调用如何包裹 `OrderBy` 调用,而 `OrderBy` 调用又包裹着 `Where` 调用,最终到达常量数据源。

```text
CALL - Take
|   CALL - OrderBy
|   |   CALL - Where
|   |   |   CONSTANT - System.Collections.Generic.List`1[Dometrain.Linq.Data.Models.Movie]
|   |   |   UNARY - Lambda
|   |   |   |   BINARY - Equal
|   |   |   |   |   MEMBER - Phase
|   |   |   |   |   |   PARAMETER - movie
|   |   |   |   |   CONSTANT - 4
|   |   |   |   PARAMETER - movie
|   |   UNARY - Lambda
|   |   |   MEMBER - ReleaseDate
|   |   |   |   PARAMETER - movie
|   |   |   PARAMETER - movie
|   CONSTANT - 3
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/iqueryable-behind-the-scenes-54280426/?t=325)

#### 查询翻译

LINQ 提供程序的策略是用特定技术的逻辑替换这棵树的各个部分。
例如,`Where` 节点会被解析以创建一个 SQL `WHERE` 子句,`OrderBy` 节点创建一个 `ORDER BY` 子句,`Take` 节点则被转换成一个 `LIMIT` 或 `TOP` 子句。
一旦整棵树被解析并合并成一个单一查询,它就会针对数据源执行,结果随后返回给调用方。

```text
Expression tree node        SQL clause
--------------------        -------------------
Where              ------>  WHERE
OrderBy            ------>  ORDER BY
Take               ------>  LIMIT / TOP
                               |
                   merged into a single query
                               |
                   executed against the data source
                               |
                   results returned to the caller
```

## 6. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280427/) · 1:12

### 总结

本课为对 LINQ 内部机制的探索收尾,重点是 IEnumerable 与 IQueryable 之间的关键区别。
它强调了查询执行边界对性能的影响,特别是从 IQueryable 转换到 IEnumerable 时,会如何触发底层查询并把数据移入内存。
本课还谈到了 LINQ 提供程序在解析查询中的作用,以及让查询能够跨越各种不同数据源所需要的架构投入。

### 核心概念

- **IEnumerable 与 IQueryable**:理解查询从提供程序特定的执行(如 SQL)转换为内存中处理的边界。
- **执行时机(Execution Timing)**:在 IQueryable 被转换为或传入 IEnumerable 上下文的那一刻,底层查询就会执行。
- **性能考量(Performance Considerations)**:把大型数据集从外部数据源移入内存可能造成严重的性能瓶颈。
- **LINQ 提供程序(LINQ Providers)**:解析 LINQ 表达式以查询特定底层技术的专门实现。

### 课程笔记

理解 LINQ 在幕后如何运作,对于编写高效的代码至关重要。
一个主要的收获是 `IEnumerable` 与 `IQueryable` 之间的区别。
当一个查询被定义为 `IQueryable` 时,它通常表示一个面向外部数据源(例如数据库)的查询。
从 `IQueryable` 到 `IEnumerable` 的转换标志着执行发生的位置。
在这个边界上,底层查询会被发送给数据提供程序并执行,得到的数据会被加载到应用程序的内存空间中。

这个转换是性能优化的一个关键点。
如果开发者在过滤或投影数据之前,不小心从底层技术中把大量数据拉进了内存,应用程序就可能遭受相当大的性能损失。
务必确保在数据进入 `IEnumerable` 的内存空间之前,尽可能多的处理都在数据源层面完成。

`IEnumerable` 的实现以及让 `IQueryable` 得以运作的机制都涉及复杂的查询解析。
LINQ 提供程序就是负责这种解析的组件,它们把 LINQ 表达式翻译成目标数据源能够理解的格式。
虽然可以实现自定义的 LINQ 提供程序来查询专有数据源,但这并非易事。
大多数开发者会依赖现有的、健壮的提供程序,它们专为对接常见的数据技术而设计。

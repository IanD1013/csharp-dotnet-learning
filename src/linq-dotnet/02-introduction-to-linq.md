# Introduction to LINQ

> 课程:[From Zero to Hero: LINQ in .NET](https://dometrain.com/course/from-zero-to-hero-linq-in-dotnet/) · 第 2 章
> 共 5 课 · 约 17:10
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Why do we have LINQ?](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-do-we-have-linq-54280306/) | 5:36 | [↓](#1-why-do-we-have-linq) |
| 2 | [A language for querying data](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-language-for-querying-data-54280307/) | 2:02 | [↓](#2-a-language-for-querying-data) |
| 3 | [A first LINQ example](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/) | 5:21 | [↓](#3-a-first-linq-example) |
| 4 | [Why would you want to use LINQ?](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-would-you-want-to-use-linq-54280309/) | 3:34 | [↓](#4-why-would-you-want-to-use-linq) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280310/) | 0:37 | [↓](#5-section-recap) |

## 1. Why do we have LINQ?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-do-we-have-linq-54280306/) · 5:36

LINQ(Language Integrated Query,语言集成查询)的诞生,是为了给 C# 和 VB.NET 提供一种统一的、强类型的查询语言,取代以往查询内存对象、SQL 数据库、XML 文件等不同数据源时那些零散且往往基于字符串的方法。

### 核心概念

- **统一的查询语言(Unified Query Language)**:用同一种语法查询包括对象、数据库和 XML 在内的多种数据源。
- **语言集成(Language Integration)**:查询能力内置于 C#(以及 VB.NET)之中,从而支持语法高亮和编译期检查。
- **强类型(Strong Typing)**:用带类型的表达式取代"魔法字符串"(如原始 SQL 或 XPath),更易于维护和重构。
- **提供程序模型(Provider Model)**:一种翻译架构,由 LINQ 提供程序把标准查询语法映射为特定数据源的方言(例如 SQL、API 或自定义文件格式)。
- **物化(Materialization)**:把原始数据结果(来自数据库读取器或 XML 节点)转换为强类型 C# 对象的过程。

### 课程笔记

在过去,与数据打交道要求开发者根据数据所在的位置学习不同的方言。
处理已经在内存中的对象时,开发者通常使用命令式的循环和条件逻辑来筛选数据集。

```csharp
// Working with in-memory objects
var objectsResult = new List<Movie>();

var movieObjects = GetMovies();

foreach (var movie in movieObjects)
{
    if (movie.Name.Contains("Spider"))
    {
        objectsResult.Add(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-do-we-have-linq-54280306/?t=30)

当数据源换成关系型数据库时,实现方式就完全变了。
开发者必须管理连接和命令,而查询逻辑也不再用 C# 编写,而是写在一个 SQL 字符串字面量里。
这种方式缺乏强类型,并且让重构变得困难,因为编译器无法验证字符串的内容。
此外,开发者还要负责手动把 `SqlDataReader` 中的结果物化为对象。

```csharp
// Working with SQL databases
var sqlResult = new List<Movie>();

var connection = new SqlConnection(GetConnectionString());
connection.Open();

var command = connection.CreateCommand();
command.CommandText = """
                      SELECT Id, Name, ReleaseDate
                      FROM dbo.Movies
                      WHERE Name LIKE '%Spider%'
                      """;

var reader = command.ExecuteReader();
while (reader.Read())
{
    sqlResult.Add(new Movie
    {
        MovieId = (Guid)reader["Id"],
        Name = (string)reader["Name"],
        ReleaseDate = (DateOnly)reader["ReleaseDate"]
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-do-we-have-linq-54280306/?t=70)

查询 XML 数据则引入了第三种范式。
这需要加载一个 `XmlDocument`,并使用 XPath 查询来提取特定节点。
与 SQL 方式类似,它依赖字符串字面量进行筛选,并需要从 XML 节点中手动提取数据来物化对象。

```csharp
// Working with XML
var xmlResult = new List<Movie>();

var document = new XmlDocument();
document.Load("movies.xml");

var namespaceMgr = new XmlNamespaceManager(document.NameTable);
namespaceMgr.AddNamespace("mcu", "urn:mcu-movie-schema");

var movieNodes = document.DocumentElement!.SelectNodes(
    "descendant::mcu:movie[mcu:Name[contains(text(),'Spider')]]", namespaceMgr);
foreach (XmlNode movieNode in movieNodes!)
{
    xmlResult.Add(new Movie
    {
        MovieId = Guid.Parse(movieNode
            .SelectSingleNode("descendant::mcu:movie-id", namespaceMgr)!.InnerText),
        Name = movieNode
            .SelectSingleNode("descendant::mcu:movie-name", namespaceMgr)!.InnerText,
        ReleaseDate = DateOnly.Parse(movieNode
            .SelectSingleNode("descendant::mcu:movie-release", namespaceMgr)!.InnerText)
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-do-we-have-linq-54280306/?t=145)

LINQ 的设计目的就是统一这些各不相同的模式。
通过在 C# 内部提供单一的、集成的查询语言,它让开发者无论底层数据源是什么,都能使用相同的语法。
这是通过提供程序实现的,提供程序会把 LINQ 查询翻译成数据源特有的语言(例如 SQL 或 XPath)。
这种集成确保查询是强类型的,能受益于语法高亮和重构工具等 IDE 功能,并消除了对脆弱的基于字符串的查询的需求。

```text
                      +--> provider --> SQL   --> database
  LINQ query (C#) ----+
                      +--> provider --> XPath --> XML
```

## 2. A language for querying data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-language-for-querying-data-54280307/) · 2:02

LINQ 是作为 C# 内部的一种领域特定语言而开发的,目的是提供一种统一、描述性强且易于维护的方式来查询各种数据源中的数据。
通过评估现有的模式,例如命令式的对象筛选、声明式的 SQL 字符串以及复杂的 XPath 查询,Microsoft 力求创造一种语法,既具备 SQL 的可读性,又能享有作为一等 C# 语言特性所带来的类型安全和重构能力。

### 核心概念

- **命令式与声明式查询**:理解从描述*如何*筛选数据到描述*需要什么*数据的转变。
- **可读性**:以 SQL 作为描述性查询逻辑的基准。
- **维护与重构**:在 C# 代码中使用字符串字面量编写查询(SQL、XPath)所带来的风险。
- **领域特定语言(DSL)**:LINQ 是集成在 C# 中、专门用于数据操作的语言。

### 课程笔记

在设计 LINQ 时,Microsoft 分析了数据检索的常见模式,以确定一门现代查询语言应当是什么样子。
C# 中传统的命令式做法,例如筛选一个内存对象列表,往往不能很好地描述其背后的意图。

```csharp
// Working with in-memory objects
var objectsResult = new List<Movie>();

var movieObjects = GetMovies();

foreach (var movie in movieObjects)
{
    if (movie.Name.Contains("Spider"))
    {
        objectsResult.Add(movie);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-language-for-querying-data-54280307/?t=10)

在上面的示例中,代码需要一个手动循环和条件判断来筛选结果。
它不像其他语言那样能清楚地表达"选出名称中包含 'Spider' 的所有结果"这一意思。

相比之下,SQL 提供了一种描述性很强的声明式语法,只用几行就能清楚地说明正在发生什么。

```csharp
// Working with SQL databases
var sqlResult = new List<Movie>();

var connection = new SqlConnection(GetConnectionString());
connection.Open();

var command = connection.CreateCommand();
command.CommandText = """
            SELECT Id, Name, ReleaseDate
            FROM dbo.Movies
            WHERE Name LIKE '%Spider%'
            """;

var reader = command.ExecuteReader();
while (reader.Read())
{
    sqlResult.Add(new Movie
    {
        MovieId = (Guid)reader["Id"],
        Name = (string)reader["Name"],
        ReleaseDate = (DateOnly)reader["ReleaseDate"]
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-language-for-querying-data-54280307/?t=85)

虽然 SQL 语法具有描述性,但它在 C# 中带来了一个重大问题:查询被包含在字符串字面量中。
字符串字面量难以维护,而且几乎不可能安全地重构。

其他替代方案,例如用于查询 XML 的 XPath,往往会产生复杂、难以阅读且难以管理的代码。

```csharp
// Working with XML
var xmlResult = new List<Movie>();

var document = new XmlDocument();
document.Load("movies.xml");

var namespaceMgr = new XmlNamespaceManager(document.NameTable);
namespaceMgr.AddNamespace("mcu", "urn:mcu-movie-schema");

var movieNodes = document.DocumentElement!.SelectNodes(
    "descendant::mcu:movie[mcu:Name[contains(text(),'Spider')]]", namespaceMgr);
foreach (XmlNode movieNode in movieNodes!)
{
    xmlResult.Add(new Movie
    {
        MovieId = Guid.Parse(movieNode
            .SelectSingleNode("descendant::mcu:movie-id", namespaceMgr)!.InnerText),
        Name = movieNode
            .SelectSingleNode("descendant::mcu:movie-name", namespaceMgr)!.InnerText,
        ReleaseDate = DateOnly.Parse(movieNode
            .SelectSingleNode("descendant::mcu:movie-release", namespaceMgr)!.InnerText)
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-language-for-querying-data-54280307/?t=70)

LINQ 的设计正是为了解决这些问题,它创造了一种直接集成在 C# 中、用于操作数据的领域特定语言。
它借鉴了 SQL 的描述性,同时保持易读、易懂、易于重构。

## 3. A first LINQ example

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/) · 5:21

### 总结

本课介绍 .NET 中的语言集成查询(LINQ),演示如何使用查询语法(Query syntax)和流式(方法)语法(Fluent (Method) syntax)来查询集合。
它解释了 LINQ 背后的设计决策,例如为支持 IntelliSense 而安排的子句顺序,并强调了强类型和编译期检查相对于传统基于字符串的 SQL 查询或命令式循环的优势。

### 核心概念

* 查询语法:一种受 SQL 启发的声明式语法,以 `from` 子句开头,从而让 IDE 能提供 IntelliSense。
* 流式(方法)语法:一种利用扩展方法和 lambda 表达式的语法。
* 强类型:LINQ 查询在编译期得到验证,因此比基于字符串的查询更安全、更易于重构。
* 性能:查询语法和流式语法都编译为相同的底层代码,因此性能完全相同。
* 可读性:LINQ 比命令式的 `foreach` 循环和 `if` 语句更清楚地表达意图。

### 课程笔记

为了演示 LINQ,我们从一个简单的电影对象集合开始。
LINQ 让我们可以使用一种直接集成在 C# 语言中的语法,对这些数据进行筛选和转换。

```csharp
// Working with LINQ
var movies = GetMovies();

var linqResult = ;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/?t=10)

LINQ 提供两种不同的语法:查询语法和流式(也称为方法)语法。
查询语法借鉴了 SQL,这是一种为数据操作而优化的领域特定语言。
不过,LINQ 通过改变操作的顺序,对 SQL 的结构做了改进。

```csharp
// Working with LINQ
var movies = GetMovies();

var linqResult = 
    from movie in movies
    where movie.Name.Contains("Spider")
    select movie;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/?t=100)

在传统 SQL 中,开发者通常先在 `SELECT` 语句中指定列,然后才在 `FROM` 子句中指定表。
这常常会妨碍 IntelliSense,因为开发者在输入字段时,IDE 还不知道数据源是什么。
开发者经常发现自己要先写 `FROM` 子句,再把光标移回上方去填写列名。

```csharp
// Working with SQL databases
var sqlResult = new List<Movie>();

var connection = new SqlConnection(GetConnectionString());
connection.Open();

var command = connection.CreateCommand();
command.CommandText = """

SELECT Id, Name, ReleaseDate
FROM dbo.Movies
WHERE Name LIKE '%Spider%'
""";

var reader = command.ExecuteReader();
while (reader.Read())
{

    sqlResult.Add(new Movie
    {
        MovieId = (Guid)reader["Id"],
        Name = (string)reader["Name"],
        ReleaseDate = (DateOnly)reader["ReleaseDate"]
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/?t=115)

LINQ 的查询语法通过以 `from` 子句开头解决了这个问题。
由于先定义了数据源和别名(`movie`),IDE 就能为查询的其余部分提供完整的 IntelliSense。
此外,由于 LINQ 是强类型的,底层数据模型的任何改动都会导致编译期错误而不是运行时故障,这使得重构比处理 SQL 字符串字面量安全得多。

```text
  SQL:   SELECT Id, Name, ReleaseDate   <-- data source not known yet
         FROM   dbo.Movies
         WHERE  Name LIKE '%Spider%'

  LINQ:  from movie in movies          <-- data source + alias first
         where movie.Name.Contains("Spider")
         select movie                  <-- IntelliSense knows movie
```

第二种方式是流式语法,它依赖扩展方法和 lambda 表达式。
建议在 lambda 中使用有意义的别名,而不是像 `x` 这样泛泛的字符,以保持可读性。

```csharp
// Working with LINQ
var movies = GetMovies();

// var linqResult = 
//     from movie in movies
//     where movie.Name.Contains("Spider")
//     select movie;

var linqResult = movies
    .Where(movie => movie.Name.Contains("Spider"))
    .Select(movie => movie);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/?t=265)

在流式语法中,如果返回的是整个对象,`.Select()` 方法通常是可选的,因为它是隐含的。
两种语法编译出的代码完全相同,因此在二者之间如何选择取决于可读性和个人偏好;性能上没有任何差别。

```csharp
// Working with LINQ
var movies = GetMovies();

var linqResult = 
    from movie in movies
    where movie.Name.Contains("Spider")
    select movie;

// var linqResult = movies
//     .Where(movie => movie.Name.Contains("Spider"))
//     .Select(movie => movie);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/a-first-linq-example-54280308/?t=310)

与使用 `for` 或 `foreach` 循环并嵌套 `if` 语句的命令式 C# 代码相比,LINQ 对其意图的表达要明确得多,使代码更易于阅读和维护。

## 4. Why would you want to use LINQ?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/why-would-you-want-to-use-linq-54280309/) · 3:34

### 总结

语言集成查询(LINQ)提供了一种统一的、强类型的语法,用于查询包括 SQL 数据库、XML 和内存集合在内的多种数据源。
通过消除对字符串字面量的需求,并同时提供查询语法和流式语法,LINQ 提升了代码的可维护性,并清楚地表达开发者的意图,使数据操作逻辑更易于阅读和重构。

### 核心概念

- **统一的查询语言**:用同一种语法查询 SQL、XML、内存列表和 API。
- **强类型**:受益于 IntelliSense、更容易的重构以及编译期错误检查。
- **没有字符串字面量**:避免把查询嵌入为字符串所带来的陷阱。
- **双重语法**:可以在查询语法(类似 SQL)和流式语法(带 lambda 的扩展方法)之间选择。
- **表达意图**:编写整洁、可读、能描述正在对数据做什么的代码。

### 课程笔记

语言集成查询(LINQ)是一种直接集成在 C# 中的查询语言,让开发者可以使用一致的语法与来自各种来源的数据交互。
LINQ 让你无需为不同的提供程序学习不同的语言,就能跨 SQL 数据库、XML 文件、内存集合,甚至外部 API(例如使用 OData 的 API)进行查询。

LINQ 的一个重要好处是它是强类型的。
这种与 C# 类型系统的集成带来了几项开发上的优势:
- **IntelliSense**:IDE 可以根据被查询的数据结构提供建议。
- **重构**:重命名属性或更改类型更加安全,因为编译器能识别出被破坏的查询。
- **消除字符串字面量**:与原始 SQL 字符串不同,LINQ 查询是代码的一部分,降低了查询字符串在运行时出现语法错误的风险。

LINQ 提供两种不同的语法,以适应不同的偏好和场景。
**查询语法**提供一种声明式的方式,类似于 SQL 的改进版。
另一种是**流式语法**(也称为方法语法),它利用扩展方法和 lambda 表达式把操作链接在一起。

归根结底,使用 LINQ 最有说服力的理由是它能够表达意图。
通过专注于数据转换的逻辑,而不是迭代和筛选的机制,LINQ 产出的代码更易读、更易维护。
这种清晰性确保了原作者和未来的维护者都能轻松理解数据操作的目的。

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-linq-in-dotnet-2737459/section-recap-54280310/) · 0:37

### 总结

本课对 LINQ 的入门部分做了总结,回顾了历史背景、设计原则,以及在 C# 应用程序中使用 LINQ 的主要动机。

### 核心概念

* LINQ 的历史背景和动机。
* Microsoft 对 LINQ 的设计与工程过程。
* LINQ 基本查询语法与执行。
* LINQ 相对于传统数据查询方法的优势。

### 课程笔记

本课程的入门部分为理解 LINQ(语言集成查询)奠定了基础。
它阐述了 LINQ 旨在解决的具体问题,以及 Microsoft 在创建它时所考虑的架构因素。

通过简单的代码示例,本部分展示了 LINQ 如何简化数据操作和查询任务。
这些示例用于展示该语言相较于手动迭代和筛选逻辑的可读性和表达力。

入门部分很大一部分内容聚焦于采用 LINQ 作为 C# 主要查询语言的理由。
主要好处包括类型安全、IntelliSense 支持,以及用于查询内存集合、XML 和数据库等多种数据源的统一语法。

后续章节将从这些高层概念过渡到深入的技术实现,涵盖 LINQ 的全部方法、高级特性以及该查询语言的内部机制。

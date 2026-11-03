# Performance & Concurrency

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 9 章
> 共 8 课 · 约 59:02
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The typical types of performance issues](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-typical-types-of-performance-issues-54130250/) | 4:19 | [↓](#1-the-typical-types-of-performance-issues) |
| 2 | [Slow Queries & DB indices](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/slow-queries-db-indices-54130251/) | 7:58 | [↓](#2-slow-queries--db-indices) |
| 3 | [Using compiled queries to improve runtime performance](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compiled-queries-to-improve-runtime-performance-54130252/) | 6:59 | [↓](#3-using-compiled-queries-to-improve-runtime-performance) |
| 4 | [Using batching to execute bulk queries](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/) | 8:38 | [↓](#4-using-batching-to-execute-bulk-queries) |
| 5 | [Loading related data - strategies](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/) | 15:40 | [↓](#5-loading-related-data---strategies) |
| 6 | [Untracked query results](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/untracked-query-results-54130256/) | 3:23 | [↓](#6-untracked-query-results) |
| 7 | [Solving Concurrency Issues](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/) | 10:58 | [↓](#7-solving-concurrency-issues) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130258/) | 1:07 | [↓](#8-section-recap) |

## 1. The typical types of performance issues

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-typical-types-of-performance-issues-54130250/) · 4:19

### 总结

使用 Entity Framework Core 的数据库驱动应用中,性能问题往往源于低效的数据库访问模式,而不是框架的内部配置。
常见瓶颈包括:过度获取不必要的数据,从而增加网络延迟;过多的数据库往返,使网络开销叠加;数据库引擎内部的查询执行缓慢;以及把 LINQ 表达式翻译成 SQL 的计算成本。
此外,当 DbContext 跟踪过多对象时会产生内存压力,而当多个 context 实例试图更新同一数据时可能出现并发冲突,导致更新丢失。

### 核心概念

- **数据过度获取(Data Over-fetching)**:通过网络传输的数据多于应用逻辑所需。
- **网络往返(Network Roundtrips)**:执行多个小查询而不是单个优化查询所带来的性能损失。
- **数据库执行延迟(Database Execution Latency)**:数据库引擎处理查询并生成结果所花费的时间。
- **LINQ 翻译成本(LINQ Translation Cost)**:把 LINQ 表达式转换为可执行 SQL 语句的计算开销。
- **变更跟踪膨胀(Change Tracking Bloat)**:由于 DbContext 跟踪大量对象,应用服务器上的内存消耗增加。
- **并发冲突(Concurrency Conflicts)**:多个 DbContext 实例对同一条数据记录执行相互冲突的更新时出现的问题。

### 课程笔记

#### Entity Framework 请求周期

要理解性能瓶颈,首先必须考虑 Entity Framework Core 应用中标准的数据库访问流程。
发出请求时,应用会执行包含面向 DbContext 的 LINQ 语句的代码。
DbContext 把这些 LINQ 语句翻译成 SQL,然后在数据库上执行。
数据库返回原始数据,DbContext 再把它们实体化为对象,供应用代码使用。

```text
Application code --LINQ--> DbContext --SQL--> Database
Application code <-objects- DbContext <-data-- Database
```

#### 常见的性能瓶颈

随着应用使用量的增长和扩展,会出现若干典型的性能问题:

##### 过多的数据传输(过度获取)

通过网络发送超出必要的数据是延迟的主要来源。
如果结果集包含应用不使用的列或行,数据库就要花费不必要的时间把这些数据传输到应用服务器。
尽量减少发回应用的数据量是一项关键的优化策略。

##### 网络往返

在内存中访问数据比网络通信快几个数量级。
与执行单个较大的查询相比,执行许多小查询会产生显著的网络开销。
随着往返次数增加,这种开销会严重影响应用的整体响应时间。

##### 数据库执行缓慢

如果数据库引擎处理查询并把它转换为结果需要很长时间,性能就会下降。
这通常与 Entity Framework Core 无关,而与数据库端的问题有关,例如索引或查询复杂度。

##### LINQ 到 SQL 的翻译开销

把 LINQ 语句转换成 SQL 的过程不是瞬间完成的。
在复杂场景中,Entity Framework Core 执行这种翻译所花的时间可能成为一个可测量的性能因素,累加到请求的总时间中。

##### 变更跟踪与内存消耗

默认情况下,DbContext 会跟踪它获取的每个对象的状态,以便于更新。
在长生命周期的 context 或检索大量对象的场景中,这种跟踪机制会在应用服务器上消耗大量内存,即使这些对象已不再被活跃使用。

##### 并发与数据完整性

在不断扩展的应用中,多个 DbContext 实例常常同时运行。
当一个 context 选取了数据,另一个 context 更新了同一数据,随后第一个 context 又写回自己的更改,从而实际上覆盖了第二个 context 的更新时,就会发生并发问题。
管理这些相互冲突的更新对于维护数据完整性至关重要。

```text
Context A: SELECT row ----------------------------> write changes (overwrites B)
Context B:            ----> UPDATE same row
```

## 2. Slow Queries & DB indices

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/slow-queries-db-indices-54130251/) · 7:58

### 总结

慢查询的定义是:从数据库引擎接收到 SQL 命令到它开始返回数据之间的时长。
识别这些瓶颈需要结合可观测性工具(例如 EF Core 日志和 OpenTelemetry)以及针对具体数据库的分析。
虽然大数据集可能因网络延迟而导致耽搁,但真正的慢查询通常源于低效的执行计划,例如在大表上进行聚集索引扫描(clustered index scan)。
要在 Entity Framework Core 环境中解决这些问题,开发者应避免手动修改数据库,而是在迁移中使用 Fluent API 配置聚集索引和非聚集索引,确保 schema 在所有环境中保持同步。

### 核心概念

- **慢查询的定义**:从引擎接收到查询到它开始返回数据所经过的时间,与网络延迟不同。
- **可观测性**:使用 EF Core 日志适配器、OpenTelemetry 或 Application Insights 来识别执行时间长的查询。
- **执行计划**:在 Azure Data Studio 等工具中分析执行计划图,以识别聚集索引扫描之类的低效操作。
- **SQL Server Profiler**:捕获 trace 文件,记录特定时间段内的所有数据库事件和性能指标。
- **Database Engine Tuning Advisor**:一个自动化工具,分析 trace 文件以推荐具体的索引改进。
- **Fluent API 配置**:使用 `HasIndex`、`IsClustered` 和 `IsDescending` 在代码中定义数据库性能结构。
- **迁移同步**:确保所有 schema 变更都通过 EF Core 迁移处理,以保持开发环境和生产环境之间的一致性。

### 课程笔记

#### 识别慢查询

慢查询专门由数据库引擎在开始把数据传回应用之前处理请求所花的时间来定义。
虽然一个大数据集可能需要很长时间才能通过网络传输,但这种延迟并不一定表示在引擎层面存在慢查询。
为了调查性能,开发者可以使用 EF Core 日志适配器把查询详情发送到日志系统,或发送到 Application Insights 之类的可观测性平台。
一旦识别出有问题的查询,就可以在 Azure Data Studio 等工具中手动分析它。

在 Azure Data Studio 中启用查询计划后,你可以直观地看到引擎如何执行一个查询。
例如,一个查询可能导致聚集索引扫描,这表明引擎正在扫描表中的每一条记录来找到结果。

```sql
SELECT * FROM dbo.Pictures WHERE ChannelFirstAiredOn = 'Dummy'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/slow-queries-db-indices-54130251/?t=125)

#### 自动化性能调优

对于更复杂的系统,手动调查可能不够。
SQL Server 提供了 SQL Server Profiler,它允许你生成一个 trace 文件,其中包含一次会话期间执行的所有查询和性能数据。
这个 trace 文件可以导入 Database Engine Tuning Advisor。
该工具会对照数据库 schema 分析 trace,并给出具体建议,例如哪些列需要新索引,或者现有索引应在哪里修改。

```text
SQL Server Profiler --> trace file --> Database Engine Tuning Advisor
                                            |
                                            v
                                index recommendations
```

#### 在 EF Core 中实现索引

使用 Entity Framework Core 时,通过迁移而不是手动修改数据库来实施这些建议至关重要。
这确保所有环境保持同步。
Fluent API 提供了一整套方法,用于在 `IEntityTypeConfiguration<T>` 实现中控制索引元数据。

最初,一个映射可能会定义基本的表结构、键和属性类型:

```csharp
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .UseTphMappingStrategy()
            .HasQueryFilter(movie => movie.ReleaseDate > new DateTime(1990,1,1))
            .HasKey(movie => movie.Identifier);

        builder
            .HasAlternateKey(movie => new { movie.Title, movie.ReleaseDate });

        builder.Property(movie => movie.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(movie => movie.ReleaseDate)
            .HasColumnType("char(8)")
            .HasConversion(new DateTimeToChar8Converter());

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/slow-queries-db-indices-54130251/?t=355)

为了优化性能,你可以修改哪个索引是聚集索引。
默认情况下,主键是聚集索引。
如果某个备用键(alternate key)更适合作为聚集索引,你可以在主键上禁用聚集,并在备用键上启用它:

```csharp
builder
    .HasKey(movie => movie.Identifier)
    .IsClustered(false);

builder
    .HasAlternateKey(movie => new { movie.Title, movie.ReleaseDate })
    .IsClustered(true);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/slow-queries-db-indices-54130251/?t=370)

可以在经常用于过滤或排序的列上添加额外的非聚集索引。
例如,`AgeRating` 上的索引可以配置为按降序存储:

```csharp
builder.HasIndex(movie => movie.AgeRating)
    .IsDescending();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/slow-queries-db-indices-54130251/?t=415)

虽然索引很强大,但应当审慎使用。
给每一列都添加索引会显著降低写入性能。
每个索引都应服务于特定目的,并针对特定的查询目标。

## 3. Using compiled queries to improve runtime performance

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compiled-queries-to-improve-runtime-performance-54130252/) · 6:59

### 总结

Entity Framework Core 中的编译查询(compiled queries)让开发者可以避开每次执行时把 LINQ 表达式解析成 SQL 的开销。
通过使用 EF.CompileQuery 方法,查询会在第一次调用时被解析并缓存,后续调用则复用生成的 SQL。
为了最大化性能,这些查询应存储在静态字段中,确保编译成本在应用的生命周期内只付出一次。
这项技术对于高频的 API 端点以及解析时间占总执行时间很大比例的复杂查询特别有效。

### 核心概念

- 由 LINQ 到 SQL 的解析开销导致的性能瓶颈。
- 用于预编译查询的 EF.CompileQuery 静态方法。
- 使用 lambda 参数对编译查询进行参数化。
- 必须把编译查询存储在静态字段中才能有效复用。
- 在编译 lambda 之外处理终结操作(例如 ToList)。
- 通过执行日志验证查询复用。

### 课程笔记

在调优应用时,性能瓶颈有时并不在数据库执行本身,而在于 Entity Framework Core 把 LINQ 查询解析成 SQL 的开销。
这在调用量很高的 API 或相当复杂的查询中很常见。
虽然使用原始 SQL 字面量可以绕过这种解析,但它牺牲了 LINQ 的强类型特性,并使重构更加困难。
编译查询提供了一种解决方案,让查询解析一次并被复用。

考虑控制器中的一个标准 LINQ 查询:

```csharp
[HttpGet("until-age/{ageRating}")]
[ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllUntilAge([FromRoute] AgeRating ageRating)
{
    var filteredTitles = await _context.Movies
        .Where(movie => movie.AgeRating <= ageRating)
        .Select(movie => new MovieTitle { Id = movie.Identifier, Title = movie.Title })
        .ToListAsync();

    return Ok(filteredTitles);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compiled-queries-to-improve-runtime-performance-54130252/?t=55)

要优化它,可以使用 EF.CompileQuery 方法。
该方法接受一个 lambda 表达式,其第一个参数始终是 DbContext(本例中是 MoviesContext)。
后续参数表示传入查询的值,例如 ageRating。
需要注意的是,ToListAsync 或 ToList 之类的终结方法绝不应包含在 CompileQuery 的 lambda 内;它们必须在编译查询委托的结果上调用。

```csharp
var compiledQuery = EF.CompileQuery((MoviesContext context, AgeRating ageRating)
    => context.Movies
        .Where(movie => movie.AgeRating <= ageRating)
        .Select(movie => new MovieTitle { Id = movie.Identifier, Title = movie.Title }));

var filteredTitles = compiledQuery(_context, ageRating).ToList();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compiled-queries-to-improve-runtime-performance-54130252/?t=220)

在方法内部定义编译查询是不够的,因为每次调用该方法时查询仍然会被解析。
为确保查询只被解析一次,请把它存储在一个 private static readonly 字段中。
这确保解析只在第一次执行时发生,之后的每次调用都复用生成的 SQL。

```csharp
private static readonly Func<MoviesContext, AgeRating, IEnumerable<MovieTitle>> CompiledQuery = 
    EF.CompileQuery((MoviesContext context, AgeRating ageRating)
        => context.Movies
            .Where(movie => movie.AgeRating <= ageRating)
            .Select(movie => new MovieTitle { Id = movie.Identifier, Title = movie.Title }));

[HttpGet("until-age/{ageRating}")]
[ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
public IActionResult GetAllUntilAge([FromRoute] AgeRating ageRating)
{
    var filteredTitles = CompiledQuery(_context, ageRating).ToList();

    return Ok(filteredTitles);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compiled-queries-to-improve-runtime-performance-54130252/?t=355)

性能提升可以通过检查应用日志来验证。
第一次执行时,日志会显示一条 "planned query execution" 以及对 SQL 语句的解析。
在后续执行中,日志会显示直接执行参数化的 SQL 语句,没有解析步骤。

```text
1st call:  CompiledQuery(...) -> parse LINQ -> SQL (cached) -> execute
next call: CompiledQuery(...) ----------------> SQL (reused) -> execute
```

## 4. Using batching to execute bulk queries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/) · 8:38

### 总结

Entity Framework Core 通过把多条 SQL 语句批处理到单次数据库往返中来优化批量更新。
本课探讨如何使用 SQL Server Profiler 监控这一行为,如何通过 SQL Server provider 选项配置最大批大小,以及如何通过预先获取实体来避免逐个查找的查询,从而进一步优化性能。

### 核心概念

* EF Core 中的批处理行为
* 用于监控数据库活动的 SQL Server Profiler
* MaxBatchSize 配置
* 减少往返次数
* 预先获取实体以利用 Change Tracker 缓存

### 课程笔记

Entity Framework Core 包含一项性能特性,可以把多条数据修改语句批处理到单次数据库往返中。
这一行为默认启用,并且可以使用数据库分析工具观察到。

在一个典型的批量更新场景中,控制器可能会接收一个待更新实体的列表。
下面 `GenresController` 中的实现把更新逻辑委托给一个批处理服务:

```csharp
[HttpPut("batch-update")]
[ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status201Created)]
public async Task<IActionResult> UpdateAll([FromBody] List<Genre> genres)
{
    var response = await _batchService.UpdateGenres(genres);

    return CreatedAtAction(nameof(GetAll), new{}, response);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=25)

通过 curl 之类的工具执行批量更新,可以演示默认行为:

```bash
curl -X 'PUT' \
  'https://localhost:5001/Genres/batch-update' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '[
  {"id" : "6", "name": "", "description": "Update Test Genre 1" },
  {"id" : "7", "name": "", "description": "Update Test Genre 2" },
  {"id" : "8", "name": "", "description": "Update Test Genre 3" },
  {"id" : "9", "name": "", "description": "Update Test Genre 4" },
  {"id" : "10", "name": "", "description": "Update Test Genre 5" },
  {"id" : "11", "name": "", "description": "Update Test Genre 6" },
  {"id" : "12", "name": "", "description": "Update Test Genre 7" },
  {"id" : "13", "name": "", "description": "Update Test Genre 8" },
  {"id" : "14", "name": "", "description": "Update Test Genre 9" },
  {"id" : "15", "name": "", "description": "Update Test Genre 10" }
]'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=110)

使用 SQL Server Profiler 监控 `RPC:Completed` 和 `SQL:BatchCompleted` 事件可以看到,EF Core 执行了多条 `SELECT TOP(1)` 语句(每次实体查找一条),随后是一条批处理的 `UPDATE` 语句。
默认情况下,EF Core 最多批处理 42 条语句。

```sql
OUTPUT 1
WHERE [Id] = @p1;
UPDATE [Genres] SET [Description] = @p2
OUTPUT 1
WHERE [Id] = @p3;
UPDATE [Genres] SET [Description] = @p4
-- ... (repeated for all 10 genres)
,N'@p1 int,@p0 varchar(1024),@p3 int,@p2 varchar(1024),@p5 int,@p4 varchar(1024),@p7 int,@p6 varchar(1024),@p9 int,@p8 varchar(1024),@p11 int,@p10 varchar(1024),@p13 int,@p12 varchar(1024),@p15 int,@p14 varchar(1024),@p17 int,@p16 varchar(1024),@p19 int,@p18 varchar(1024)',@p1=6,@p0='Update Test Genre 1',@p3=7,@p2='Update Test Genre 2',@p5=8,@p4='Update Test Genre 3',@p7=9,@p6='Update Test Genre 4',@p9=10,@p8='Update Test Genre 5',@p11=11,@p10='Update Test Genre 6',@p13=12,@p12='Update Test Genre 7',@p15=13,@p14='Update Test Genre 8',@p17=14,@p16='Update Test Genre 9',@p19=15,@p18='Update Test Genre 10'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=160)

最大批大小可以在 `DbContext` 注册中,通过 SQL Server provider 设置里的 `MaxBatchSize` 选项进行配置。

```csharp
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
    optionsBuilder
        .UseSqlServer(connectionString, sqlBuilder =>
            sqlBuilder.MaxBatchSize(2))
        .LogTo(Console.WriteLine);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=280)

如果把 `MaxBatchSize` 设置为一个较低的值,例如 2,EF Core 就会把更新拆分成多次往返。
对于 10 次更新,这会导致 5 次单独的数据库往返。

```sql
exec sp_executesql N'SET NOCOUNT ON;
UPDATE [Genres] SET [Description] = @p0
OUTPUT 1
WHERE [Id] = @p1;
UPDATE [Genres] SET [Description] = @p2
OUTPUT 1
WHERE [Id] = @p3;
',N'@p1 int,@p0 varchar(1024),@p3 int,@p2 varchar(1024)',@p1=6,@p0='Test Genre 1',@p3=7,@p2='Test Genre 2'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=325)

为了进一步优化性能,可以在更新循环之前用单个查询预先获取所有实体,从而解决 "N+1" 查找问题。
这确保之后对 repository 的 update 方法的调用命中 Change Tracker 的缓存,而不是数据库。

```csharp
public async Task<IEnumerable<Genre>> UpdateGenres(IEnumerable<Genre> genres)
{
    List<Genre> response = new ();
    _uowManager.StartUnitOfWork();
    
    var existingGenres = await _repository.GetAll(genres.Select(g => g.Id));
    
    foreach (var genre in genres)
    {
        var updatedGenre = await _repository.Update(genre.Id, genre);
        if(updatedGenre is not null)
            response.Add(updatedGenre);
    }
    await _uowManager.SaveChangesAsync();
    return response;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=450)

经过这一优化后,profiler 显示一条使用 `IN` 子句的 `SELECT` 语句,随后是批处理的 `UPDATE`,把整个操作减少到只有两次往返。

```sql
SELECT [g].[Id], [g].[CreatedAt], [g].[Deleted], [g].[Description], [g].[Name]
FROM [Genres] AS [g]
WHERE [g].[Deleted] = CAST(0 AS bit) AND [g].[Id] IN (6, 7, 8, 9, 10, 11, 12, 13, 14, 15)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-batching-to-execute-bulk-queries-54130253/?t=480)

```text
Before: SELECT TOP(1) per entity lookup -> batched UPDATE
After:  SELECT ... IN (6, 7, ..., 15)  -> batched UPDATE  (two round trips)
```

## 5. Loading related data - strategies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/) · 15:40

### 总结

本课介绍 Entity Framework Core 中加载关联数据的三种主要策略:预先加载(eager loading)、显式加载(explicit loading)和延迟加载(lazy loading)。
它解释了 N+1 select 问题,即一次针对集合的查询会引发后续针对关联实体的逐个查询,并演示如何使用 .Include() 来避免这种情况。
本课还详细说明了如何使用显式加载进行有针对性的数据检索,以及如何使用代理(proxies)配置延迟加载,同时告诫不要在 Web 应用中使用它。

### 核心概念

- **N+1 Select 问题**:一种性能问题,一个查询获取 N 条记录,随后再执行 N 个额外查询,为每条记录获取关联数据。
- **预先加载**:使用 `.Include()` 方法在初始查询中获取关联数据,通常产生一个 SQL JOIN。
- **显式加载**:使用 `Entry` API,为 context 已跟踪的特定实体手动加载导航属性。
- **延迟加载**:访问导航属性时自动加载关联数据;需要 `Microsoft.EntityFrameworkCore.Proxies` 包以及 `virtual` 属性。
- **权衡**:当大多数记录都需要关联数据时首选预先加载,而对于只是偶尔需要的数据,显式加载更好,可以让初始结果集保持较小。

### 课程笔记

#### N+1 Select 问题

ORM 中一个常见的性能陷阱是 N+1 select。
如果你获取所有电影(1 个查询),然后遍历它们来访问各自的演员,EF Core 可能会为每部电影触发一次单独的数据库往返(N 个查询)。
虽然 EF Core 默认情况下比旧版 Entity Framework 更难触发这个问题,但在选择加载策略时,它仍然是一个必须理解的关键概念。

```text
SELECT movies              -> 1 query
  movie 1 -> SELECT actors -> query
  movie 2 -> SELECT actors -> query
  ...
  movie N -> SELECT actors -> query    (N queries)
```

#### 预先加载

预先加载是最常用的策略。
通过使用 `.Include()` 方法,你可以确保关联实体通过 SQL join 在单次往返中获取。
当你知道所有返回的记录都需要关联数据时,这是默认的选择。

```csharp
[HttpGet]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAll()
{
    var movies = await _context.Movies
        .Include(movie => movie.Actors)
        .ToListAsync();

    return Ok(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/?t=445)

这会产生一个包含多个 join 的 SQL 查询,用来获取电影及其关联的演员:

```sql
SELECT [p].[GrossRevenue], [p].[ChannelFirstAiredOn], [t].[MovieId], [t].[ActorId], [t].[Id], [t].[FirstName], [t].[LastName]
FROM [Pictures] AS [p]
LEFT JOIN (
    SELECT [m].[MovieId], [m].[ActorId], [a].[Id], [a].[FirstName], [a].[LastName]
    FROM [Movie_Actor] AS [m]
    INNER JOIN [Actors] AS [a] ON [m].[ActorId] = [a].[Id]
) AS [t] ON [p].[Identifier] = [t].[MovieId]
WHERE [p].[ReleasedDate] > '1990-01-01'
ORDER BY [p].[Identifier], [t].[MovieId], [t].[ActorId]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/?t=480)

#### 显式加载

当你只需要为初始结果集中一小部分记录获取关联数据时,显式加载很有用。
这样可以避免让初始查询过大。
你通过 `_context.Entry()` 使用 `ChangeTracker` 来加载特定的关联。

```csharp
[HttpGet]
public async Task<IActionResult> GetAll()
{
    var movies = await _context.Movies.ToListAsync();

    foreach (var televisionMovie in movies.OfType<TelevisionMovie>())
    {
        await _context.Entry(televisionMovie)
            .Collection(movie => movie.Actors)
            .LoadAsync();
    }

    return Ok(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/?t=600)

使用显式加载时,EF Core 会先对主实体执行一个初始查询,随后再针对循环中识别出的特定实体的关联集合执行查询。

#### 延迟加载

访问导航属性时,延迟加载会自动触发数据库查询。
这需要安装 `Microsoft.EntityFrameworkCore.Proxies` NuGet 包,并在 `DbContext` 配置中启用它。

```csharp
// In Program.cs or OnConfiguring
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesDB");
    optionsBuilder
        .UseSqlServer(connectionString)
        .UseLazyLoadingProxies();
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/?t=745)

此外,实体模型中的所有导航属性都必须标记为 `virtual`,以便 EF Core 能够创建重写它们的代理对象来注入加载逻辑。

```csharp
public abstract class Movie
{
    public int Identifier { get; set; }
    // ... other properties

    public virtual required Genre Genre { get; set; }
    public virtual List<Actor> Actors { get; set; } = new();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/?t=760)

启用延迟加载后,只要访问该属性就会触发加载:

```csharp
foreach (var televisionMovie in movies.OfType<TelevisionMovie>())
{
    // This line triggers a database query if the data isn't already loaded
    var actors = televisionMovie.Actors;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/loading-related-data-strategies-54130255/?t=810)

**警告**:在 Web 应用中通常不建议使用延迟加载,因为它把数据库往返隐藏在简单的属性访问之后,很容易意外引入 N+1 性能问题。
它更适合拥有长生命周期 `DbContext` 实例的桌面应用。

## 6. Untracked query results

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/untracked-query-results-54130256/) · 3:23

### 总结

默认情况下,Entity Framework Core 会跟踪从 DbContext 获取的每个实体,以监控可能需要更新的更改。
虽然这种跟踪机制对数据修改必不可少,但它在只读场景中会引入开销,例如仅仅返回数据的 API 端点。
使用 AsNoTracking 扩展方法可以让开发者绕过变更跟踪器,通过避免创建跟踪代理和对象图,减少内存消耗并提升查询性能。

### 核心概念

- **默认变更跟踪**:EF Core 为所有获取的实体创建跟踪条目,以支持 `SaveChangesAsync`。
- **只读场景**:当数据只是被展示或返回给用户而不做修改时,跟踪是不必要的。
- **内存效率**:未跟踪的查询消耗更少的内存,因为它们不会在变更跟踪器中维护对象图。
- **性能提升**:由于 EF Core 跳过了创建跟踪代理的开销,查询执行得更快。
- **AsNoTracking() 方法**:用于指定结果不应被跟踪的 LINQ 扩展方法。

### 课程笔记

通过 `DbContext` 查询实体时,Entity Framework Core 默认使用变更跟踪。
对于获取到的每个实体,EF Core 都会在内存中创建跟踪条目和代理来监控状态变化。
这让 context 能够检测到修改,并在调用 `SaveChangesAsync` 时把更新传播到数据库。

在标准的 API 场景中,数据只是被获取然后返回给用户,这种跟踪机制会引入不必要的开销。
例如,在下面的实现中,实体被获取后立即返回,没有任何修改的意图:

```csharp
[HttpGet]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAll()
{
    var movies = await _context.Movies.ToListAsync();
    return Ok(movies);
}

[HttpGet("{id:int}")]
[ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Get([FromRoute] int id)
{
    // Queries database, returns first match, null if not found.
    // var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    // Similar to FirstOrDefault, but throws if more than one match is found.
    // var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
    // Serves match from memory if already fetched, otherwise queries DB.
    //var movie = await _context.Movies.FindAsync(id);

    var movie = await _context.Movies
        .Include(movie => movie.Genre)
        .SingleOrDefaultAsync(m => m.Identifier == id);

    return movie == null
        ? NotFound()
        : Ok(movie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/untracked-query-results-54130256/?t=15)

即使是 `TelevisionMovie` 或 `CinemaMovie` 这样的简单对象,变更跟踪器也会维护一整个对象图。
如果一个查询返回两个实体,`ChangeTracker` 就会包含两个条目。
这些跟踪条目是内存中的对象,用于记录各种状态;如果下一个请求将使用新的 `DbContext`,而当前实体在当前作用域内永远不会被更新,那么它们就是多余的。

为了在这些只读情况下优化性能,EF Core 提供了 `AsNoTracking` 扩展方法。
把它添加到查询中,就可以阻止在变更跟踪器中创建跟踪条目。

```csharp
[HttpGet]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAll()
{
    var movies = await _context.Movies
        .AsNoTracking()
        .ToListAsync();

    return Ok(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/untracked-query-results-54130256/?t=145)

使用 `AsNoTracking` 带来几项性能好处:
- **更低的内存消耗**:EF Core 不需要在内存中维护跟踪代理或对象图。
- **更快的执行**:由于绕过了创建跟踪对象的开销,查询执行得更快。
- **零跟踪条目**:通过 `ChangeTracker.Entries` 属性验证时,未跟踪的查询恰好产生零个条目,而已跟踪的查询则是每个实体一个条目。

对于数据被查询但从不在同一个 context 中更新的读密集型 API,强烈推荐这种做法。

## 7. Solving Concurrency Issues

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/) · 10:58

### 总结

在多用户系统中,当多个进程试图同时修改同一条数据库记录时会出现并发问题,可能导致更新丢失。
Entity Framework Core 通过乐观并发来处理这一问题,主要使用行版本(row version)来检测记录在最初被检索之后是否发生了变化。
发生冲突时,EF Core 会阻止更新并抛出 DbUpdateConcurrencyException,要求应用去解决这种不一致,而不是悄悄覆盖更改,从而确保数据完整性。

### 核心概念

- **悲观并发**:假定冲突会发生,并通过在数据库记录上加锁来防止冲突,这可能会限制可扩展性。
- **乐观并发**:假定冲突很少发生;它允许多个用户读取和修改数据,但在提交前检查是否有更改。
- **行版本**:一种特殊的 SQL Server 数据类型(`rowversion`),每当行被更新时会自动递增。
- **并发令牌(Concurrency Token)**:EF Core 用来跟踪实体版本的一个属性。
- **DbUpdateConcurrencyException**:当数据库中的并发令牌与实体所持有的不匹配,导致更新失败时,EF Core 抛出的异常。

### 课程笔记

当两个线程或进程实例访问同一条记录时,就会出现并发问题。
例如,如果线程 A 和线程 B 都读取了版本 A 的记录,它们都会在本地应用各自的更改。
如果线程 A 先写入更改,记录就变成版本 A1。
当线程 B 随后尝试写入它的更改时,它可能会覆盖线程 A 的工作,产生版本 A2,并丢失版本 A1 独有的任何数据。

```text
Thread A: read Version A --> write --> Version A1
Thread B: read Version A ------------------------> write --> Version A2
                                                  (A1 changes lost)
```

为防止这种情况,开发者可以通过锁定记录来使用悲观并发,但这种方式在高并发环境中扩展性不好。
另一种方式是,乐观并发使用一个版本列来跟踪更改。
执行更新时,数据库会检查表中当前的版本是否与线程最初获取的版本一致。
如果不一致,更新就会被拒绝。

#### 实现行版本

要在 Entity Framework Core 中实现乐观并发,请在模型中添加一个 `byte[]` 属性作为并发令牌。
建议使用 `[JsonIgnore]`,以防止该令牌暴露在 API 响应中。

```csharp
using System.Text.Json.Serialization;

namespace Dometrain.EFCore.API.Models;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public byte[] ConcurrencyToken { get; set; } = new byte[0];

    [JsonIgnore]
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/?t=280)

在 Fluent API 配置中,使用 `IsRowVersion()` 方法把这个属性映射到 SQL Server 的 `rowversion` 数据类型。
这确保由数据库自动管理令牌的值。

```csharp
builder.Property(g => g.ConcurrencyToken)
    .IsRowVersion();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/?t=295)

生成的迁移会指定 `rowversion` 类型,这是一个二进制字段,每当记录被任何进程修改时,它都会在整个数据库范围内递增。

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<byte[]>(
        name: "ConcurrencyToken",
        table: "Genres",
        type: "rowversion",
        rowVersion: true,
        nullable: false,
        defaultValue: new byte[0]);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/?t=335)

#### 数据库与 SQL 行为

配置了 `IsRowVersion` 后,EF Core 会自动把并发令牌包含在 `UPDATE` 和 `DELETE` 语句的 `WHERE` 子句中。
它还会使用 `OUTPUT` 子句,在更新成功后取回数据库生成的新令牌。

```sql
exec sp_executesql N'SET IMPLICIT_TRANSACTIONS OFF;
SET NOCOUNT ON;
UPDATE [Genres] SET [Description] = @p0
OUTPUT INSERTED.[ConcurrencyToken]
WHERE [Id] = @p1 AND [ConcurrencyToken] = @p2;
',N'@p1 int,@p2 varbinary(8),@p0 varchar(1024)',@p1=1,@p2=0x000000000001DCB8,@p0='Hallo concurrency'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/?t=475)

#### 处理并发异常

如果在记录被获取之后、调用 `SaveChangesAsync()` 之前,另一个进程修改了该记录,那么 `WHERE` 子句将找不到与原始 `ConcurrencyToken` 匹配的行。
EF Core 检测到受影响的行数为零,并抛出 `DbUpdateConcurrencyException`。

```text
UPDATE ... WHERE [Id] = @p1 AND [ConcurrencyToken] = @p2
   |
   +-- token matches    -> row updated, OUTPUT INSERTED.[ConcurrencyToken]
   +-- token changed    -> 0 rows affected -> DbUpdateConcurrencyException
```

在 repository 模式中,更新逻辑通常会先获取现有记录并应用更改,然后再调用 `SaveChangesAsync`。

```csharp
public async Task<Genre?> Update(int id, Genre genre)
{
    var existingGenre = await _context.Genres.FindAsync(id);

    if (existingGenre is null)
        return null;

    existingGenre.Description = genre.Description;

    if(!_uowManager.IsUnitOfWorkStarted)
        await _context.SaveChangesAsync();

    return existingGenre;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/?t=535)

#### 自定义并发令牌

如果你不想使用 SQL Server 的 `rowversion` 类型,可以使用 `IsConcurrencyToken()` 把任意属性配置为并发令牌。
这要求你自己确保每次数据库中的记录被更新时,该属性的值都会改变。

```csharp
builder.Property(g => g.ConcurrencyToken)
    .IsConcurrencyToken();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/solving-concurrency-issues-54130257/?t=625)

## 8. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130258/) · 1:07

### 总结

本课回顾了 Entity Framework Core 的性能与并发特性,强调其可扩展性。
它着重介绍了非跟踪查询、查询编译和批处理等用于优化数据库交互的技术。
本课的结论是,EF Core 提供了一个安全、强类型的环境,能够应对高性能需求,并且可以根据需要灵活地优化特定代码路径。

### 核心概念

- 使用 `AsNoTracking` 优化查询性能。
- 通过批处理和 `MaxBatchSize` 减少数据库往返。
- 使用 `EF.CompileQuery` 预编译查询以提升执行速度。
- 使用并发令牌和行版本管理数据完整性。
- 在高需求的企业环境中扩展 EF Core。

### 课程笔记

Entity Framework Core 提供了多种机制,确保应用能够有效扩展。
选择正确的跟踪行为可以显著提升性能。
对于实体不需要被更新的只读查询,`AsNoTracking` 通过绕过变更跟踪器来减少开销。

```csharp
[HttpGet]
[ProducesResponseType(typeof(List<Movie>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAll()
{
    var movies = await _context.Movies
        .AsNoTracking()
        .ToListAsync();
    
    return Ok(movies);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130258/?t=7)

为了进一步优化查询执行,开发者可以使用编译查询。
这项技术让 EF Core 能够为频繁执行的 LINQ 查询跳过查询编译步骤,从而带来性能提升。

```csharp
private static readonly Func<MoviesContext, AgeRating, IEnumerable<MovieTitle>> CompiledQuery = 
    EF.CompileQuery((MoviesContext context, AgeRating ageRating)
        => context.Movies
            .Where(movie => movie.AgeRating <= ageRating)
            .Select(movie => new MovieTitle { Id = movie.Identifier, Title = movie.Title }));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130258/?t=7)

尽量减少到数据库的往返次数对于扩展至关重要。
这是通过在 `DbContext` 设置中配置批处理来实现的。
通过设置 `MaxBatchSize`,EF Core 可以把多条命令合并到单次数据库调用中。

```csharp
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
        optionsBuilder
            .UseSqlServer(connectionString, sqlBuilder => 
                sqlBuilder.MaxBatchSize(50))
            .LogTo(Console.WriteLine);
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130258/?t=7)

并发问题通过使用并发令牌来管理。
把一个属性映射为行版本,可以确保数据库能够检测到某条记录自上次获取以来是否已被另一个进程修改。

```csharp
builder.Property(g => g.ConcurrencyToken)
    .IsRowVersion();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130258/?t=7)

虽然 EF Core 常常因为被认为无法扩展而受到批评,但对于绝大多数应用代码来说,它仍然是一个可靠的选择。
在需要极致优化的场景中,开发者可以针对特定的性能关键路径有选择地切换到原始 SQL 或其他方法,同时让应用的其余部分保持在 ORM 之内。

# Advanced modelling techniques

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 8 章
> 共 10 课 · 约 80:16
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [More DbContext features?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-dbcontext-features-54130222/) | 1:27 | [↓](#1-more-dbcontext-features) |
| 2 | [Using inheritance in your model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/) | 27:26 | [↓](#2-using-inheritance-in-your-model) |
| 3 | [Using Alternate keys](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/) | 5:37 | [↓](#3-using-alternate-keys) |
| 4 | [One-to-one relationships](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/) | 5:39 | [↓](#4-one-to-one-relationships) |
| 5 | [Many-to-many relationships](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/) | 9:30 | [↓](#5-many-to-many-relationships) |
| 6 | [Using compound keys](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compound-keys-54130227/) | 3:14 | [↓](#6-using-compound-keys) |
| 7 | [Using raw SQL queries for your entities](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/) | 7:36 | [↓](#7-using-raw-sql-queries-for-your-entities) |
| 8 | [Using keyless entities to query your database](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-keyless-entities-to-query-your-database-54130229/) | 5:31 | [↓](#8-using-keyless-entities-to-query-your-database) |
| 9 | [Using the ChangeTracker for fun & profit](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/) | 12:56 | [↓](#9-using-the-changetracker-for-fun--profit) |
| 10 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130231/) | 1:20 | [↓](#10-section-recap) |

## 1. More DbContext features?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/more-dbcontext-features-54130222/) · 1:27

### 总结

本课是 Entity Framework Core 高级建模技术的引言,承认该框架在模型创建、查询和数据持久化方面的能力极其广泛。
本课并不逐一详尽讲解每个 API,而是把重点转向在真实咨询场景中遇到的、实用且影响大的功能。
这种方式确保开发者在复杂实现中优先使用最相关的工具,同时也知道还有更广泛的文档可用于小众需求。

### 核心概念

* **高级建模(Advanced Modeling)**:探索超出基本实体定义的复杂配置选项。
* **查询与保存(Querying and Saving)**:实现复杂的数据检索和持久化逻辑。
* **实用优先(Practical Prioritization)**:聚焦于能解决常见专业难题的功能。
* **文档素养(Documentation Literacy)**:借助 Microsoft Learn 等外部资源获取详尽的技术参考。

### 课程笔记

Entity Framework Core 是一个全面的框架,其范围远远超出基本操作。
模型创建、数据查询和数据保存这几个领域包含大量并不显而易见、但对专业级软件开发至关重要的功能。

本章的目标是超越基础,深入高级主题。
由于 EF Core 的功能数量之多可能令人应接不暇,课程内容经过筛选,聚焦于在专业咨询工作中遇到的特定技术。
这些是开发者乍看之下可能不会发现、但在复杂项目中会经常需要的最关键功能。

虽然本课程涵盖了最关键的高级功能,但并不详尽。
Microsoft Learn 网站仍是框架内每项功能的权威资料库。
鼓励开发者根据需要使用该资源探索专门的功能,而本章则为最常见的高级场景提供所需的深度。

## 2. Using inheritance in your model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/) · 27:26

### 总结

本课探讨在 Entity Framework Core 中建模继承的三种主要策略:Table Per Hierarchy(TPH)、Table Per Type(TPT)和 Table Per Concrete Class(TPC)。
TPH 是默认策略,它使用单张表,带有一个鉴别器列,并用可空字段存放子类特有的属性。
TPT 把每个类映射到各自的表,用外键把派生数据链接到基表;而 TPC 为每个具体类创建单独的表,重复基类属性,并需要数据库序列来生成唯一主键。
本课演示如何使用 Fluent API 配置这些策略,并评估它们在查询性能、数据完整性和外键约束方面的取舍。

### 核心概念

- **Table Per Hierarchy (TPH)**:EF Core 的默认策略;层次结构中的所有类都通过一个鉴别器列映射到单张表。
- **Table Per Type (TPT)**:把层次结构中的每个类型映射到各自的表;派生表通过一个同时也是外键的主键链接到基表。
- **Table Per Concrete Class (TPC)**:只把具体类映射到表;每张表包含该类型的所有属性(基类的和派生的)。
- **鉴别器列(Discriminator Column)**:在 TPH 中使用(并在 TPT/TPC 查询中生成)的一列,用来标识记录的具体实体类型。
- **数据库序列(Database Sequences)**:在 TPC 中需要,用以确保在多张独立表之间主键唯一。
- **映射策略(Mapping Strategies)**:EF Core 7 引入的 Fluent API 方法 `UseTphMappingStrategy()`、`UseTptMappingStrategy()` 和 `UseTpcMappingStrategy()`。

### 课程笔记

要在 C# 中建模继承,通常会定义一个抽象基类,以及针对特定类型的继承类。
在这个场景中,基类 `Movie` 被 `CinemaMovie`(带有 `GrossRevenue` 属性)和 `TelevisionMovie`(带有 `ChannelFirstAiredOn` 属性)扩展。

```csharp
namespace Dometrain.EFCore.API.Models;

public abstract class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public decimal InternetRating { get; set; }

    public required Genre Genre { get; set; }
    public required string MainGenreName { get; set; }

    public ExternalInformation? ExternalInformation { get; set; }

    public List<Actor> Actors { get; set; } = new();
}

public class CinemaMovie : Movie
{
    public required decimal GrossRevenue { get; set; }
}

public class TelevisionMovie : Movie
{
    public required string ChannelFirstAiredOn { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=325)

```text
              Movie (abstract)
                     |
         +-----------+------------+
         |                        |
    CinemaMovie            TelevisionMovie
    GrossRevenue           ChannelFirstAiredOn
```

要注册这些类型,`DbContext` 必须知道这些继承类。
这是通过在 `OnModelCreating` 中为每个类型应用配置来实现的。

```csharp
public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base (options)
    { }
    
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<ExternalInformation> ExternalInformations => Set<ExternalInformation>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<GenreName> GenreNames => Set<GenreName>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
        modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
        modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
        modelBuilder.ApplyConfiguration(new ActorMapping());

        modelBuilder.Entity<GenreName>()
            .HasNoKey()
            .ToSqlQuery($"SELECT Name FROM dbo.Genres");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SaveChangesInterceptor());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=355)

#### Table Per Hierarchy (TPH)

默认情况下,EF Core 使用 TPH。
这种策略把整个层次结构映射到单张表(例如 `Pictures`)。
它会添加一个 `Discriminator` 列来标识类型,并把子类特有的列设为可空,因为这些列只包含各自类型的数据。

```csharp
/// <inheritdoc />
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<string>(
        name: "ChannelFirstAiredOn",
        table: "Pictures",
        type: "nvarchar(max)",
        nullable: true);

    migrationBuilder.AddColumn<string>(
        name: "Discriminator",
        table: "Pictures",
        type: "nvarchar(21)",
        maxLength: 21,
        nullable: false,
        defaultValue: "");

    migrationBuilder.AddColumn<decimal>(
        name: "GrossRevenue",
        table: "Pictures",
        type: "decimal(18,2)",
        nullable: true);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=460)

查询 TPH 结构时,EF Core 会从这张单表中选择所有列,包括鉴别器列。

```sql
SELECT [p].[Identifier], [p].[AgeRating], [p].[Discriminator], [p].[InternetRating], [p].[MainGenreId], [p].[ReleaseDate], [p].[Plot], 
[p].[Title], [p].[GrossRevenue], [p].[ChannelFirstAiredOn]
FROM [Pictures] AS [p]
WHERE [p].[ReleaseDate] > '1990-01-01'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=655)

#### Table Per Type (TPT)

TPT 把每个类映射到各自的表。
基表包含共享字段,而派生表只包含特有字段,以及一个充当指向基表的外键的主键。
这可以通过在派生实体的配置上调用 `.ToTable()` 来配置。

```csharp
public class CinemaMovieMapping : IEntityTypeConfiguration<CinemaMovie>
{
    public void Configure(EntityTypeBuilder<CinemaMovie> builder)
    {
        builder.ToTable("CinemaMovie");
    }
}

public class TelevisionMovieMapping : IEntityTypeConfiguration<TelevisionMovie>
{
    public void Configure(EntityTypeBuilder<TelevisionMovie> builder)
    {
        builder.ToTable("TelevisionMovie");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=715)

或者,在 EF Core 7+ 中,可以在基实体的配置上使用 `UseTptMappingStrategy()`。

```csharp
public void Configure(EntityTypeBuilder<Movie> builder)
{
    builder
        .ToTable("Pictures")
        .UseTptMappingStrategy()
        .HasQueryFilter(movie => movie.ReleaseDate > new DateTime(1990,1,1))
        .HasKey(movie => movie.Identifier);

    builder.Property(movie => movie.Title)
        .HasColumnType("varchar")
        .HasMaxLength(128)
        .IsRequired();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=1105)

查询 TPT 需要把基表与所有派生表进行连接。
EF Core 会生成一个 `CASE` 语句,根据哪张被连接的表包含该记录来确定鉴别器的值。

```sql
SELECT [p].[Identifier], [p].[AgeRating], [p].[InternetRating], 
CASE
    WHEN [t].[Identifier] IS NOT NULL THEN N'TelevisionMovie'
    WHEN [c].[Identifier] IS NOT NULL THEN N'CinemaMovie'
END AS [Discriminator]
FROM [Pictures] AS [p]
LEFT JOIN [CinemaMovie] AS [c] ON [p].[Identifier] = [c].[Identifier]
LEFT JOIN [TelevisionMovie] AS [t] ON [p].[Identifier] = [t].[Identifier]
WHERE [p].[ReleaseDate] > '1990-01-01'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=965)

```text
                 Pictures
        (Identifier, AgeRating, ...)
          ^                      ^
          | LEFT JOIN            | LEFT JOIN
          | Identifier           | Identifier
   CinemaMovie              TelevisionMovie
   (Identifier,             (Identifier,
    GrossRevenue)            ChannelFirstAiredOn)
```

#### Table Per Concrete Class (TPC)

TPC 只为具体类创建表。
每张表包含该类型的所有列,包括从基类继承来的列。
由于没有共享的基表,主键必须借助数据库序列跨表管理,以防止重复。
TPC 使用 `UseTpcMappingStrategy()` 进行配置。

```csharp
public void Configure(EntityTypeBuilder<Movie> builder)
{
    builder
        // .ToTable("Pictures") // Base table is removed in TPC
        .UseTpcMappingStrategy()
        .HasQueryFilter(movie => movie.ReleaseDate > new DateTime(1990,1,1))
        .HasKey(movie => movie.Identifier);

    builder.Property(movie => movie.Title)
        .HasColumnType("varchar")
        .HasMaxLength(128)
        .IsRequired();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=1170)

在数据库中,主键列使用该序列作为其默认值。

```sql
CREATE TABLE [dbo].[TelevisionMovie] (
    [Identifier]        INT             DEFAULT (NEXT VALUE FOR [MovieSequence]) NOT NULL,
    [ChannelFirstAiredOn] NVARCHAR (MAX) NOT NULL,
    [AgeRating]         INT             DEFAULT ((0)) NOT NULL,
    [InternetRating]    DECIMAL (18, 2) DEFAULT ((0.0)) NOT NULL,
    [MainGenreId]       INT             DEFAULT ((0)) NOT NULL,
    [Plot]              VARCHAR (MAX)   NULL,
    -- ...
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=1280)

TPC 的查询使用 `UNION ALL` 合并所有具体表的结果。

```sql
SELECT [t].[Identifier], [t].[AgeRating], [t].[InternetRating], [t].[MainGenreId], [t].[ReleaseDate], [t].[Plot], [t].[Title], [t].[GrossRevenue], [t].[ChannelFirstAiredOn], [t].[Discriminator]
FROM (
    SELECT [c].[Identifier], [c].[AgeRating], [c].[InternetRating], [c].[MainGenreId], [c].[ReleaseDate], [c].[Plot], [c].[Title], [c].[GrossRevenue], NULL AS [ChannelFirstAiredOn], N'CinemaMovie' AS [Discriminator]
    FROM [CinemaMovie] AS [c]
    UNION ALL
    SELECT [t0].[Identifier], [t0].[AgeRating], [t0].[InternetRating], [t0].[MainGenreId], [t0].[ReleaseDate], [t0].[Plot], [t0].[Title], NULL AS [GrossRevenue], [t0].[ChannelFirstAiredOn], N'TelevisionMovie' AS [Discriminator]
    FROM [TelevisionMovie] AS [t0]
) AS [t]
WHERE [t].[ReleaseDate] > '1990-01-01'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-inheritance-in-your-model-54130223/?t=1380)

#### 策略对比

- **查询性能**:TPH 最快(单张表)。
  TPC 也很快(没有连接,但使用 union)。
  TPT 由于多次连接而最慢。
- **数据完整性**:TPT 和 TPC 允许列在数据库层面为非空。
  TPH 要求子类特有的列可空。
- **外键**:TPH 和 TPT 支持指向基实体(`Movie`)的外键。
  TPC 不支持指向基实体的外键,因为不存在基表;外键必须指向具体的表。

## 3. Using Alternate keys

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/) · 5:37

Entity Framework Core 中的备用键(alternate key)提供了一种使用主键以外的属性来标识实体的机制。
虽然人工主键(例如整数 ID)是标准做法,但唯一的名称或电子邮件地址这类自然键同样可以唯一标识一条记录。
通过把它们映射为备用键,EF Core 会借助数据库约束强制唯一性,并允许这些属性充当外键关系的主体键,从而让数据库架构更具描述性:外键指向人类可读的字符串,而不是不透明的标识符。

### 核心概念

- 备用键作为次级唯一标识符。
- 通过 Fluent API 使用 `HasAlternateKey` 进行配置。
- 在数据库层面通过唯一约束和非聚集索引实现。
- 借助 `HasPrincipalKey` 把备用键用作外键关系的目标。
- 外键与备用键之间在数据类型和长度上保持一致的要求。

### 课程笔记

在许多场景中,实体不仅能通过主键来标识。
例如,`Genre` 实体通常有一个人工主键(`Id` 字段),但只要不允许重复,`Name` 字段也应该能唯一标识该类型。

```csharp
using System.Text.Json.Serialization;

namespace Dometrain.EFCore.API.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=10)

要在模型中显式定义这一点,可以在实体映射中配置一个备用键。
这是通过 `EntityTypeBuilder` 上的 `HasAlternateKey` 方法实现的。

```csharp
public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
        
        builder.Property(g => g.Name)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        builder.HasAlternateKey(g => g.Name);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=70)

把一个属性映射为备用键会在数据库中生成一个唯一索引。
由于该字段保证唯一,它也可以作为其他表外键的目标。
例如,如果你希望 `Pictures` 表存储 `MainGenreName` 这样具有描述性的字符串,而不是数字型的 `MainGenreId`,就可以把关系改为指向该备用键。

首先,更新 `Movie` 模型,加入新的外键属性:

```csharp
public abstract class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public decimal InternetRating { get; set; }

    public required Genre Genre { get; set; }
    public required string MainGenreName { get; set; }

    public ExternalInformation? ExternalInformation { get; set; }

    public List<Actor> Actors { get; set; } = new();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=100)

接下来,必须调整 `Movie` 实体的映射。
外键属性必须与目标备用键的数据类型和长度一致(本例中为 `varchar(256)`)。

```csharp
builder.Property(movie => movie.MainGenreName)
    .HasMaxLength(256)
    .HasColumnType("varchar");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=140)

最后,使用 `HasPrincipalKey` 配置该关系,指明外键指向的是 `Genre` 实体的 `Name` 属性,而不是它的主键。

```csharp
builder
    .HasOne(movie => movie.Genre)
    .WithMany(genre => genre.Movies)
    .HasPrincipalKey(genre => genre.Name)
    .HasForeignKey(movie => movie.MainGenreName);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=190)

```text
   Pictures                          Genres
+-----------------------+       +-----------------------------+
| MainGenreName         |------>| Name (AK_Genres_Name)       |
| varchar(256)          |       | varchar(256)                |
+-----------------------+       +-----------------------------+
   HasForeignKey                   HasPrincipalKey
```

为这些更改生成迁移时,EF Core 会删除现有的外键和列,添加新列,并在主体表上应用唯一约束。

```csharp
migrationBuilder.AddUniqueConstraint(
    name: "AK_Genres_Name",
    table: "Genres",
    column: "Name");

migrationBuilder.CreateIndex(
    name: "IX_Pictures_MainGenreName",
    table: "Pictures",
    column: "MainGenreName");

migrationBuilder.AddForeignKey(
    name: "FK_Pictures_Genres_MainGenreName",
    table: "Pictures",
    column: "MainGenreName",
    principalTable: "Genres",
    principalColumn: "Name",
    onDelete: ReferentialAction.Cascade);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=235)

在 SQL Server 中,备用键被实现为一个 `UNIQUE NONCLUSTERED` 约束。

```sql
ALTER TABLE [dbo].[Genres] ADD CONSTRAINT
[AK_Genres_Name] UNIQUE NONCLUSTERED
(
    [Name] ASC
)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-alternate-keys-54130224/?t=325)

## 4. One-to-one relationships

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/) · 5:39

### 总结

Entity Framework Core 中的一对一关系通常这样建模:依赖实体与主体实体共享主键,该主键同时充当外键。
当相关数据需要被单独查询或按需加载时,这种模式比从属类型(owned type)更可取。
配置时需要在两侧都定义导航属性,并使用 Fluent API 的 HasOne 和 WithOne 方法建立关系。
在数据库中,这会生成一张依赖表,其主键受指向主体表的外键约束,通常还会启用级联删除以维护引用完整性。

### 核心概念

- **实体与从属类型**:如果需要单独查询这些数据,或者希望能灵活地在某些查询中排除它,就为一对一关系使用一个独立的实体。
- **共享主键**:实现一对一关系的标准方式,是让依赖实体的主键同时充当指向主体实体的外键。
- **主体与依赖**:在一对一关系中,一个实体是主要(主体)实体,另一个(依赖)实体指向它。
- **Fluent API 配置**:关系在 `IEntityTypeConfiguration` 实现中使用 `HasOne` 和 `WithOne` 来定义。
- **可选性**:一对一关系可以在一个方向上是可选的,使主体在没有依赖记录的情况下也能存在。

### 课程笔记

虽然一对多关系最为常见,但 EF Core 也支持一对一和多对多关系。
一对一关系适用于建模这样的数据:它只属于单个父级,但应存放在单独的表中。
例如,`Movie` 实体可能有一个 `ExternalInformation`,其中包含 IMDb 或 Rotten Tomatoes 等外部网站的 URL。

虽然这可以建模为从属类型,但把它建模为完整的实体可以带来灵活性:既可以单独查询它,也可以在获取主体实体时选择是否包含它。

```csharp
namespace Dometrain.EFCore.API.Models;

public class ExternalInformation
{
    public string? ImdbUrl { get; set; }
    public string? RottenTomatoesUrl { get; set; }
    public string? TmdbUrl { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/?t=25)

要把它集成到数据模型中,必须把该实体添加到 `DbContext`,并在 `OnModelCreating` 中应用它的配置。

```csharp
using Dometrain.EFCore.API.Data.EntityMapping;
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.API.Data;

public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base (options)
    {}

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<ExternalInformation> ExternalInformations => Set<ExternalInformation>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
        modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
        modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/?t=70)

在一对一关系中,依赖实体(即离开另一方就无法存在的那个)使用自己的主键作为指向主体实体的外键。
这确保了关系的每一侧只能存在一条记录。
在本例中,`ExternalInformation` 是依赖实体,`Movie` 是主体实体。

```csharp
namespace Dometrain.EFCore.API.Models;

public class ExternalInformation
{
    public int MovieId { get; set; }
    
    public string? ImdbUrl { get; set; }
    public string? RottenTomatoesUrl { get; set; }
    public string? TmdbUrl { get; set; }

    public Movie Movie { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/?t=145)

主体实体 `Movie` 也应包含一个指向 `ExternalInformation` 实体的导航属性。
如果并非每部电影都需要外部信息,可以把这个属性设为可选。

```csharp
namespace Dometrain.EFCore.API.Models;

public abstract class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public decimal InternetRating { get; set; }

    public required Genre Genre { get; set; }
    public required string MainGenreName { get; set; }

    public ExternalInformation? ExternalInformation { get; set; }

    public List<Actor> Actors { get; set; } = new();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/?t=175)

关系在 `IEntityTypeConfiguration` 实现中使用 Fluent API 配置。
你必须为依赖实体指定主键,然后使用 `HasOne` 和 `WithOne` 定义关系。
以这种方式配置时,EF Core 会假定依赖实体的主键就是外键。

```csharp
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class ExternalInformationMapping : IEntityTypeConfiguration<ExternalInformation>
{
    public void Configure(EntityTypeBuilder<ExternalInformation> builder)
    {
        builder.HasKey(info => info.MovieId);

        builder.HasOne(info => info.Movie)
            .WithOne(movie => movie.ExternalInformation);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/?t=250)

为这一配置生成迁移时,EF Core 会为依赖实体创建一张表,其主键同时也是指向主体表的外键。
默认情况下,这个关系带有级联删除约束,这意味着如果删除了主体实体(电影),依赖实体(外部信息)也会被删除。

```csharp
migrationBuilder.CreateTable(
    name: "ExternalInformations",
    columns: table => new
    {
        MovieId = table.Column<int>(type: "int", nullable: false),
        ImdbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
        RottenTomatoesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
        TmdbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_ExternalInformations", x => x.MovieId);
        table.ForeignKey(
            name: "FK_ExternalInformations_Pictures_MovieId",
            column: x => x.MovieId,
            principalTable: "Pictures",
            principalColumn: "Identifier",
            onDelete: ReferentialAction.Cascade);
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-one-relationships-54130225/?t=295)

```text
   Pictures (principal)          ExternalInformations (dependent)
+----------------------+       +----------------------------------+
| Identifier (PK)      |<------| MovieId (PK + FK)                |
| ...                  |       | ImdbUrl                          |
+----------------------+       | RottenTomatoesUrl                |
                               | TmdbUrl                          |
     onDelete: Cascade         +----------------------------------+
```

## 5. Many-to-many relationships

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/) · 9:30

当一种实体类型中的多条记录与另一种实体类型中的多条记录相关联时,就出现了多对多关系。
一个经典的例子是电影与演员之间的关系:一部电影有多名演员出演,一名演员也出演多部电影。

### 核心概念

- **连接表(链接实体)**:为实现多对多关系而必需的数据库表,存储来自两张相关表的外键。
- **导航属性**:两个实体上的集合属性(例如 `List<T>`),让 EF Core 能够识别该关系。
- **隐式多对多**:如果两侧都存在导航属性而没有显式配置,EF Core 会自动创建连接表。
- **显式配置(`UsingEntity`)**:一个 Fluent API 方法,用于自定义连接表,包括它的名称、外键名称和主键约束。
- **复合主键**:在连接表中,主键通常由指向相关实体的两个外键组成。

### 课程笔记

#### 数据库建模

要在关系型数据库中建模多对多关系,必须引入第三张表(通常称为连接表或链接表)。
如果我们有一张 `Movies` 表和一张 `Actors` 表,各自有自己的主键,那么连接表(例如 `MovieActor`)会包含两个外键列。
一列指向 `Movies` 表的主键,另一列指向 `Actors` 表的主键。
这两个外键合在一起,构成链接实体的复合主键。

```text
   Movies              MovieActor                 Actors
+---------+      +----------------------+      +---------+
| PK      |<-----| FK -> Movies   (PK)  |      | PK      |
|         |      | FK -> Actors   (PK)  |----->|         |
+---------+      +----------------------+      +---------+
```

如果该关系需要额外的属性(例如角色名或审计字段),链接表就会成为一个完整的实体。
在这种情况下,多对多关系实际上被建模为两个分别指向该链接实体的一对多关系。

#### 实体模型配置

要在 EF Core 中实现它,先定义导航属性。
在 `Actor` 类中,添加一个电影集合:

```csharp
namespace Dometrain.EFCore.API.Models;

public class Actor
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public List<Movie> Movies { get; set; } = new();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/?t=160)

同样地,更新 `Movie` 类,加入一个演员集合:

```csharp
namespace Dometrain.EFCore.API.Models;

public abstract class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public decimal InternetRating { get; set; }

    public required Genre Genre { get; set; }
    public required string MainGenreName { get; set; }

    public ExternalInformation? ExternalInformation { get; set; }

    public List<Actor> Actors { get; set; } = new();
}

public class CinemaMovie : Movie
{
    public required decimal GrossRevenue { get; set; }
}

public class TelevisionMovie : Movie
{
    public required string ChannelFirstAiredOn { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/?t=190)

这些实体必须作为 `DbSet` 属性注册到 `DbContext` 中,并在 `OnModelCreating` 中应用它们的配置:

```csharp
public class MoviesContext : DbContext
{
    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base (options)
    { }
    
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<ExternalInformation> ExternalInformations => Set<ExternalInformation>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<GenreName> GenreNames => Set<GenreName>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
        modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
        modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
        modelBuilder.ApplyConfiguration(new ActorMapping());

        modelBuilder.Entity<GenreName>()
            .HasNoKey()
            .ToSqlQuery($"SELECT Name FROM dbo.Genres");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SaveChangesInterceptor());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/?t=140)

#### Fluent API 配置

虽然 EF Core 通常可以按约定推断出多对多关系,但 Fluent API 提供了完全的控制。
基本语法使用 `HasMany().WithMany()`:

```csharp
public class ActorMapping : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.HasMany(actor => actor.Movies)
            .WithMany(movie => movie.Actors);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/?t=235)

如需更细粒度的控制,请使用 `UsingEntity` 方法。
它允许你定义连接表的名称,并配置从连接表回到各主体实体的关系。
由于连接实体通常是在幕后创建的,在你的模型中并不作为类存在,因此配置时必须使用基于类型的语法和基于字符串的属性名:

```csharp
public class ActorMapping : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.HasMany(actor => actor.Movies)
            .WithMany(movie => movie.Actors)
            .UsingEntity("Movie_Actor",
                left => left.HasOne(typeof(Movie))
                    .WithMany()
                    .HasForeignKey("MovieId")
                    .HasPrincipalKey(nameof(Movie.Identifier))
                    .HasConstraintName("FK_MovieActor_Movie")
                    .OnDelete(DeleteBehavior.Cascade),
                right => right.HasOne(typeof(Actor))
                    .WithMany()
                    .HasForeignKey("ActorId")
                    .HasPrincipalKey(nameof(Actor.Id))
                    .HasConstraintName("FK_MovieActor_Actor")
                    .OnDelete(DeleteBehavior.Cascade),
                linkBuilder => linkBuilder.HasKey("MovieId", "ActorId"));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/?t=490)

#### 数据库迁移

为这一配置生成迁移时,EF Core 会创建主表(如果它们尚不存在)和连接表。
连接表包含指定的列、一个复合主键,以及指向相关表的外键约束。

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(
        name: "Actors",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Actors", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Movie_Actor",
        columns: table => new
        {
            MovieId = table.Column<int>(type: "int", nullable: false),
            ActorId = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Movie_Actor", x => new { x.MovieId, x.ActorId });
            table.ForeignKey(
                name: "FK_MovieActor_Actor",
                column: x => x.ActorId,
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_MovieActor_Movie",
                column: x => x.MovieId,
                principalTable: "Pictures",
                principalColumn: "Identifier",
                onDelete: ReferentialAction.Cascade);
        });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/many-to-many-relationships-54130226/?t=520)

配置完成后,这些关系可以像其他任何导航属性一样使用。
在查询中使用 `.Include()` 会让 EF Core 在数据库中通过链接表执行必要的连接。

## 6. Using compound keys

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compound-keys-54130227/) · 3:14

在单个字段不足以标识实体的情况下,Entity Framework Core 允许使用复合键(compound key,也称组合键)。
这在实现自然键时很常见,此时某个特定的字段组合在模型中保证唯一。

### 核心概念

- **复合键**:由多个属性组成的键。
- **自然键**:使用已有的数据字段(例如 Title 和 Release Date)而不是生成的 ID 来标识实体。
- **Fluent API 映射**:配合匿名类型使用 `HasKey`、`HasAlternateKey` 或 `HasForeignKey` 来定义多列键。
- **匿名类型**:语法 `new { movie.Title, movie.ReleaseDate }`,用于为键定义组合属性。
- **数据库约束**:EF Core 把复合键转换为数据库架构中跨多列的唯一约束或主键约束。

### 课程笔记

虽然人工主键(例如自增整数)很常见,但许多领域模型需要通过字段组合来标识实体。
Entity Framework Core 可以为主键、备用键和外键定义复合键。

来看 `Movie` 实体,它使用 `Identifier` 作为主键:

```csharp
public abstract class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public decimal InternetRating { get; set; }

    public required Genre Genre { get; set; }
    public required string MainGenreName { get; set; }

    public ExternalInformation? ExternalInformation { get; set; }

    public List<Actor> Actors { get; set; } = new();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compound-keys-54130227/?t=10)

在这个模型中,由于存在翻拍,单个标题可能并不唯一,但 `Title` 和 `ReleaseDate` 的组合可以作为唯一标识符。
要把它映射为复合备用键,请在实体配置中使用 `HasAlternateKey` 方法。
虽然该方法接受一组字符串形式的属性名,但为了可维护性,更推荐的做法是使用带匿名对象的强类型键表达式。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compound-keys-54130227/?t=130)

为这一配置生成迁移时,Entity Framework Core 会在数据库中跨指定的列创建一个唯一约束。
这确保了数据库层面的数据完整性。

```csharp
namespace Dometrain.EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCompoundKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Pictures_Title_ReleaseDate",
                table: "Pictures",
                columns: new[] { "Title", "ReleaseDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Pictures_Title_ReleaseDate",
                table: "Pictures");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-compound-keys-54130227/?t=175)

这种模式用途广泛:你可以从另一个实体指向这些字段的组合,创建一个复合外键,引用复合主键或复合备用键。

## 7. Using raw SQL queries for your entities

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/) · 7:36

虽然 Entity Framework Core(EF Core)被设计为从 LINQ 表达式生成 SQL,但在某些场景下你需要完全掌控所执行的 SQL。
EF Core 提供了几项功能,用于执行原始 SQL,同时仍能享受实体跟踪和 LINQ 组合带来的好处。

### 核心概念

- **FromSql**:执行返回实体的原始 SQL 查询的主要方法。
  它使用字符串插值自动创建数据库参数,从而防范 SQL 注入。
- **FromSqlRaw**:使用普通字符串字面量执行原始 SQL 的方法。
  它需要手动参数化(例如使用 `{0}` 占位符)来确保安全。
- **LINQ 组合**:原始 SQL 查询可以使用 `.Where()` 或 `.OrderBy()` 等 LINQ 运算符进一步细化。
  EF Core 会把它们转换为最终 SQL 中的子查询结构。
- **SQL 注入防护**:在 `FromSql` 中使用插值字符串,或在 `FromSqlRaw` 中使用参数数组,可以确保用户输入被当作数据而不是可执行代码。

### 课程笔记

为了演示原始 SQL 功能,我们在 `GenreRepository` 中实现一个按最小 ID 查询类型的方法。
它通过一个控制器 action 暴露出来,便于测试不同的查询技术。

#### 使用 FromSql 执行基本的原始 SQL

执行原始 SQL 最直接的方式是使用 `DbSet` 上的 `FromSql` 方法。
来看一条按主键筛选类型的标准 SQL 查询:

```sql
SELECT * FROM [dbo].[Genres] WHERE ID >= 2
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/?t=40)

在 C# 中,这是用字符串插值实现的。
当你把插值字符串传给 `FromSql` 时,EF Core 会把插入的值当作参数,而不是拼出一个原始字符串。
这是一项关键的安全特性。

```csharp
public async Task<IEnumerable<Genre>> GetAllFromQuery()
{
    var minimumGenreId = 2;

    var genres = await _context.Genres
        .FromSql($"SELECT * FROM [dbo].[Genres] WHERE ID >= {minimumGenreId}")
        .ToListAsync();

    return genres;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/?t=145)

这段代码执行时,EF Core 会生成一条参数化查询(例如使用 `@p0`),确保应用程序免受 SQL 注入攻击。

#### 使用 FromSqlRaw

另一个方法是 `FromSqlRaw`。
这个方法在字符串插值方面的行为有所不同。
如果你直接把插值字符串传给 `FromSqlRaw`,它会被当作字符串字面量处理,当变量包含用户提供的数据时,这是不安全的。

```csharp
// VULNERABLE: This does not parameterize the query
var genres = await _context.Genres
    .FromSqlRaw($"SELECT * FROM [dbo].[Genres] WHERE ID >= {minimumGenreId}")
    .ToListAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/?t=235)

要安全地使用 `FromSqlRaw`,你必须使用编号占位符,并把参数作为单独的实参传入:

```csharp
public async Task<IEnumerable<Genre>> GetAllFromQuery()
{
    var minimumGenreId = 2;

    var genres = await _context.Genres
        .FromSqlRaw("SELECT * FROM [dbo].[Genres] WHERE ID >= {0}", minimumGenreId)
        .ToListAsync();

    return genres;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/?t=295)

#### 用 LINQ 组合原始 SQL

EF Core 中原始 SQL 最强大的一点是,结果仍然是 `IQueryable`。
这意味着你可以把原始 SQL 与强类型的 LINQ 运算符结合使用。
EF Core 足够智能,会把你的原始 SQL 包装进一个子查询,并在数据库中应用 LINQ 筛选。

```csharp
public async Task<IEnumerable<Genre>> GetAllFromQuery()
{
    var minimumGenreId = 2;

    var genres = await _context.Genres
        .FromSql($"SELECT * FROM [dbo].[Genres] WHERE ID >= {minimumGenreId}")
        .Where(genre => genre.Name != "Comedy")
        .ToListAsync();

    return genres;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-raw-sql-queries-for-your-entities-54130228/?t=385)

在这个例子中,EF Core 执行的查询类似于:
`SELECT [t].[Id], [t].[CreatedAt], [t].[Name] FROM (SELECT * FROM [dbo].[Genres] WHERE ID >= @p0) AS [t] WHERE [t].[Name] <> N'Comedy'`。
这确保了筛选发生在数据库层面,而不是先把所有记录拉到内存中。

```text
SELECT ... FROM (                                  <- LINQ .Where(...)
    SELECT * FROM [dbo].[Genres] WHERE ID >= @p0   <- FromSql(...)
) AS [t]
WHERE [t].[Name] <> N'Comedy'
```

#### 查询非实体类型

如果你需要执行不返回完整实体的原始 SQL(例如只是一个名称列表),可以使用 `DbContext` 上的 `Database.SqlQuery`。
这对于把结果投影到简单的 DTO 或基元类型很有用。

```csharp
public async Task<IEnumerable<GenreName>> GetNames()
{
    var names = await _context.Database
        .SqlQuery<GenreName>($"SELECT Name FROM dbo.Genres")
        .ToListAsync();

    return names;
}
```

## 8. Using keyless entities to query your database

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-keyless-entities-to-query-your-database-54130229/) · 5:31

### 总结

无键实体允许开发者把查询结果映射到没有主键的类上,因此非常适合数据库视图、函数,或不需要身份跟踪的原始 SQL 查询。
传统上这需要在 DbContext 中使用 HasNoKey() 进行显式配置,而 .NET 8 通过 Database.SqlQuery<T> 引入了一种更精简的方式。
这个较新的方法可以把 SQL 查询直接执行到对象中,而无需 DbSet 或手动模型映射的开销,在仍处于 Entity Framework 生态内的同时,提供类似 Dapper 等微型 ORM 的轻量体验。

### 核心概念

- **无键实体类型**:用于查询的类,它们没有唯一标识符,也不会被跟踪变更。
- **HasNoKey()**:一个 Fluent API 配置,用于告知 Entity Framework 某个类型没有主键。
- **映射到 SQL**:使用 `ToSqlQuery` 把无键实体映射到某个特定的原始 SQL 字符串或数据库视图。
- **Database.SqlQuery<T>**:.NET 8 的一项功能,允许把原始 SQL 直接查询到某个类型中,而无需 `DbSet` 或模型配置。
- **只读性质**:无键实体不支持插入、更新或删除操作,因为它们缺少用于变更跟踪的身份。

### 课程笔记

在 Entity Framework 的标准用法中,实体是带有唯一标识符的类,这让 `DbContext` 能够执行变更跟踪和 CRUD 操作。
然而在某些场景下,你需要把数据查询到一个没有唯一标识符的类中,例如从数据库视图或特定的 SQL 投影中获取结果时。
这些就称为无键实体。

为了演示这一点,来看一个简单的 `GenreName` 类,它只包含一个字符串属性,没有标识符。

```csharp
namespace Dometrain.EFCore.API.Models;

public class GenreName
{
    public required string Name { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-keyless-entities-to-query-your-database-54130229/?t=10)

#### 传统方式(映射无键实体)

如果你试图在没有配置的情况下把 `GenreName` 这样的类放进 `DbSet`,Entity Framework 会因为找不到主键而抛出运行时异常。
要解决这个问题,你必须在 `DbContext` 的 `OnModelCreating` 方法中使用 `HasNoKey()` 方法显式配置该实体。
这会告诉 Entity Framework,该类型只用于查询,不应被变更跟踪。

一旦用 `HasNoKey()` 配置好,你就可以使用 `ToSqlQuery` 把该实体映射到视图、函数或原始 SQL 查询。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfiguration(new GenreMapping());
    modelBuilder.ApplyConfiguration(new MovieMapping());
    modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
    modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
    modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
    modelBuilder.ApplyConfiguration(new ActorMapping());

    modelBuilder.Entity<GenreName>()
        .HasNoKey()
        .ToSqlQuery($"SELECT Name FROM dbo.Genres");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-keyless-entities-to-query-your-database-54130229/?t=145)

有了这个映射,你就可以像查询其他任何集合一样查询 `DbSet<GenreName>`,不过需要身份的操作(例如 `Find` 或更新)会失败。

```csharp
public async Task<IEnumerable<GenreName>> GetNames()
{
    var names = await _context.GenreNames
        .ToListAsync();

    return names;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-keyless-entities-to-query-your-database-54130229/?t=160)

#### .NET 8 方式(Database.SqlQuery)

虽然传统的映射方式可行,但为每个只读查询或视图都创建一个 `DbSet` 和模型映射可能显得繁琐。
.NET 8 引入了一种更简洁的替代方案:`Database.SqlQuery<T>`。
这个方法允许你执行原始 SQL 并直接以对象形式返回结果,类似于 Dapper 等微型 ORM 的工作方式。

对于无键实体,更推荐这种方式,因为它不再需要在 `DbContext` 中定义 `DbSet`,也省去了 `OnModelCreating` 中的 `HasNoKey()` 配置。
它默认假定目标类型是无键实体。

```csharp
public async Task<IEnumerable<GenreName>> GetNames()
{
    var names = await _context.Database
        .SqlQuery<GenreName>($"SELECT Name FROM dbo.Genres")
        .ToListAsync();

    return names;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-keyless-entities-to-query-your-database-54130229/?t=280)

这个方法支持参数化,在只需检索数据、不需要完整 Entity Framework 跟踪机制开销的场景中非常高效。

## 9. Using the ChangeTracker for fun & profit

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/) · 12:56

### 总结

ChangeTracker 是 DbContext 的一项强大功能,它监视所有被跟踪实体的状态,使开发者能够在操作提交到数据库之前拦截并修改它们。
本课演示如何在 SaveChangesInterceptor 中利用 ChangeTracker 实现逻辑删除。
通过拦截被标记为删除的实体、更新一个 "Deleted" 影子属性,并把实体状态改为 Modified,开发者可以避免物理删除行,同时在应用代码中保持标准的 DbSet.Remove 用法。

### 核心概念

- **ChangeTracker**:监视所有连接到 DbContext 的实体的状态(Added、Modified、Deleted、Unchanged、Detached)。
- **ISaveChangesInterceptor**:一个接口,用于覆盖或增强 SaveChanges 和 SaveChangesAsync 的行为。
- **逻辑删除**:一种模式,通过标志把记录标记为已删除,而不是从数据库中物理移除。
- **影子属性**:在 EF Core 模型中定义、但在 C# 类中不存在的属性,这里用来存储删除状态。
- **全局查询筛选器**:自动应用于某实体类型所有查询的筛选器,这里用于默认排除已逻辑删除的记录。
- **实体状态操纵**:手动更改实体的 EntityState,以改变 EF Core 生成的 SQL 命令的过程。

### 课程笔记

逻辑删除的实现从配置实体模型开始。
在 Genre 实体上添加一个名为 "Deleted" 的影子属性,默认值为 false。
为了确保已逻辑删除的记录被排除在标准查询之外,使用 EF.Property 语法应用一个全局查询筛选器。

```csharp
public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
        
        builder.Property(g => g.Name)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        builder
            .Property<bool>("Deleted")
            .HasDefaultValue(false);

        builder
            .HasQueryFilter(g => EF.Property<bool>(g, "Deleted") == false)
            .HasAlternateKey(g => g.Name);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/?t=40)

为了拦截删除过程,需要创建一个实现 ISaveChangesInterceptor 的拦截器。
这个接口提供了在 SaveChanges 之前或之后执行的方法。
通过重写 SavingChanges,可以把自定义逻辑注入持久化管道。
如果拦截器内部不需要异步操作,异步版本 SavingChangesAsync 可以包装同步调用。

```csharp
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dometrain.EFCore.API.Data.Interceptors;

public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        throw new NotImplementedException();
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/?t=100)

拦截器必须注册到 DbContext。
这是在上下文类的 OnConfiguring 方法中,通过 DbContextOptionsBuilder 上的 AddInterceptors 方法完成的。

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.AddInterceptors(new SaveChangesInterceptor());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/?t=205)

在 SavingChanges 方法内部,通过 eventData 中提供的 DbContext 访问 ChangeTracker。
ChangeTracker.Entries<T>() 方法可以检查当前被跟踪的、某个特定类型的所有实体。
通过筛选出处于 EntityState.Deleted 状态的条目,拦截器就能识别出应用程序请求删除的记录。

```csharp
var deleteEntries = tracker.Entries<Genre>()
    .Where(entry => entry.State == EntityState.Deleted);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/?t=460)

对于每个识别出的条目,拦截器执行两个关键操作:
1. 使用条目上的 Property<T>(string) 方法把 "Deleted" 影子属性设为 true。
2. 把条目的 State 从 EntityState.Deleted 改为 EntityState.Modified。

这一状态变化会让 EF Core 生成 SQL UPDATE 语句,而不是 DELETE 语句。
因此,记录仍保留在数据库中并设置了 Deleted 标志,而全局查询筛选器确保它在后续查询中被隐藏。

```text
DbSet.Remove(genre)
        |
        v
State = EntityState.Deleted
        |
        v  SaveChanges -> SaveChangesInterceptor.SavingChanges
        |    "Deleted" CurrentValue = true
        |    State = EntityState.Modified
        v
SQL UPDATE (not DELETE)
        |
        v
HasQueryFilter: EF.Property<bool>(g, "Deleted") == false
```

```csharp
public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        var context = eventData.Context as MoviesContext;

        if (context is null) return result;

        var tracker = context.ChangeTracker;

        var deleteEntries = tracker.Entries<Genre>()
            .Where(entry => entry.State == EntityState.Deleted);

        foreach (var deleteEntry in deleteEntries)
        {
            deleteEntry.Property<bool>("Deleted").CurrentValue = true;
            deleteEntry.State = EntityState.Modified;
        }

        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-changetracker-for-fun-profit-54130230/?t=550)

这种方式让应用程序的其余部分可以使用标准的 DbSet 功能(例如 Remove),而底层的持久化逻辑则透明地处理逻辑删除。
除了逻辑删除之外,ChangeTracker 和拦截器还可以用于审计(例如设置 "CreatedBy" 或 "LastModified" 字段)、日志记录(journaling)以及其他横切关注点。

## 10. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130231/) · 1:20

### 总结

本课为 "Advanced modelling techniques" 一节收尾,总结了对 Entity Framework Core 复杂功能的探索。
本课强调,虽然 EF Core 的功能集非常庞大,但构建复杂系统最关键的组成部分(包括继承策略、值转换器、拦截器和高级关系配置)都已讲到,对于小众需求,开发者应借助官方文档。

### 核心概念

- **功能完整性**:认识到 EF Core 中核心的高级建模工具集已经讲完。
- **依赖文档**:对于主课程未涵盖的专门或小众功能,使用官方 EF Core 文档的重要性。
- **项目架构**:`MoviesContext` 的最终状态包含了继承、影子属性和全局筛选器。

### 课程笔记

"Advanced modelling techniques" 一节聚焦于扩展基本的 EF Core 模型,以应对现实世界的复杂情况。
最终实现包含了以下几种成熟的模式:

1.  **继承映射**:项目对 `Movie` 实体使用 Table-Per-Hierarchy(TPH)策略,让 `CinemaMovie` 和 `TelevisionMovie` 共享单张表(`Pictures`),并带有一个鉴别器列。
2.  **自定义值转换**:使用 `DateTimeToChar8Converter` 把 `DateTime` 属性映射到数据库中的 `char(8)` 列,演示了如何处理非标准的数据存储需求。
3.  **影子属性与值生成**:`Genre` 实体包含一个 `CreatedDate` 影子属性,在创建实体时由 `CreatedDateGenerator` 自动填充。
4.  **软删除与拦截器**:`Genre` 上的 "Deleted" 影子属性由 `SaveChangesInterceptor` 管理。
    这个拦截器捕获删除尝试并把它们转换为更新,同时一个全局查询筛选器确保已删除的记录被排除在标准查询之外。
5.  **复杂关系**:
    -   **多对多**:在 `Actor` 和 `Movie` 之间配置,使用自定义连接表(`Movie_Actor`),带有特定的外键约束和级联删除行为。
    -   **一对一**:在 `Movie` 和 `ExternalInformation` 之间配置,使用 `MovieId` 作为依赖实体的主键。
    -   **备用键**:模型使用 `Name` 作为 `Genre` 的备用键,并为 `Movie` 使用由 `Title` 和 `ReleaseDate` 组成的复合键,以确保数据完整性。

# Fine-tuning the model

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 4 章
> 共 10 课 · 约 71:23
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Taking a look at the DB schema](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/taking-a-look-at-the-db-schema-54129916/) | 2:34 | [↓](#1-taking-a-look-at-the-db-schema) |
| 2 | [Introducing DataAnnotations for our Model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/) | 4:49 | [↓](#2-introducing-dataannotations-for-our-model) |
| 3 | [Achieving similar results with FluentAPI](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/) | 9:42 | [↓](#3-achieving-similar-results-with-fluentapi) |
| 4 | [One-to-many relationships between entities](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/) | 14:22 | [↓](#4-one-to-many-relationships-between-entities) |
| 5 | [Converting between different data types](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/) | 12:08 | [↓](#5-converting-between-different-data-types) |
| 6 | [Using owned and complex types to group data](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/) | 10:03 | [↓](#6-using-owned-and-complex-types-to-group-data) |
| 7 | [Generating values in the model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/) | 9:00 | [↓](#7-generating-values-in-the-model) |
| 8 | [Hiding some database fields from our model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/) | 4:09 | [↓](#8-hiding-some-database-fields-from-our-model) |
| 9 | [Applying global query filters](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/) | 3:28 | [↓](#9-applying-global-query-filters) |
| 10 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129926/) | 1:08 | [↓](#10-section-recap) |

## 1. Taking a look at the DB schema

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/taking-a-look-at-the-db-schema-54129916/) · 2:34

### 总结

Entity Framework Core 使用一套约定把 C# 模型类映射到数据库 schema,自动确定表名、列类型和主键。
虽然这些约定简化了初期开发,但它们常常导致"最坏情况"的数据库配置,例如所有字符串都使用 NVARCHAR(MAX),日期使用高精度的 DATETIME2。
理解这些默认行为,是微调模型以满足特定数据库需求、并避免因破坏基于约定的发现机制而引发运行时异常的第一步。

### 核心概念

- **表命名**:默认情况下,EF Core 会让数据库表名与 `DbContext` 中定义的 `DbSet` 属性名一致。
- **列映射**:模型类上的公共属性会被映射为同名、且 SQL 数据类型兼容的列。
- **主键发现**:EF Core 会自动把名为 `Id` 或 `[ClassName]Id` 的属性识别为主键。
- **Identity 规范**:按约定识别出的整数主键,在 SQL Server 中会被自动配置为 identity 列。
- **默认类型映射**:为确保最大兼容性,字符串默认映射为 `nvarchar(max)`,`DateTime` 属性默认映射为 `datetime2(7)`。
- **运行时校验**:如果模型缺少可被发现的主键,且没有提供显式配置,EF Core 会在运行时抛出异常。

### 课程笔记

默认情况下,Entity Framework Core 依靠约定来生成数据库 schema。
检查一个由包含 `DbSet<Genre>` 和 `DbSet<Movie>` 的 `DbContext` 生成的数据库时,得到的表分别名为 `Genres` 和 `Movies`。
`DbSet` 属性名与表名之间的这种直接映射就是标准约定。

在这些表中,列对应模型类的公共属性。
EF Core 会通过查找名为 `Id`(不区分大小写)的属性,或由类名加 "Id" 组合而成的属性(例如 `MovieId`),来自动识别主键。
对于基于整数的主键,EF Core 还会启用 identity 规范,让数据库来处理自增值。

```sql
CREATE TABLE [dbo].[Movies] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (MAX) NULL,
    [ReleaseDate] DATETIME2 (7)  NOT NULL,
    [Synopsis]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/taking-a-look-at-the-db-schema-54129916/?t=55)

虽然约定能让你快速起步,但它们常常导致不够理想的 schema 定义。
由于 EF Core 为防止数据丢失而假设"最坏情况",它会把所有 C# `string` 属性映射为 `nvarchar(max)`。
同样,`DateTime` 属性会被映射为 `datetime2(7)`,这是 SQL Server 中可用的最精确的日期时间格式。
在许多生产场景中,应当把它们限制为特定长度(例如 `nvarchar(256)`)或精度更低的类型,以优化存储和性能。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/taking-a-look-at-the-db-schema-54129916/?t=115)

在不提供显式配置的情况下偏离这些约定会导致错误。
例如,如果把 `Movie` 类中的 `Id` 属性重命名为 `Identifier`,EF Core 将不再把它识别为主键。
这不会触发编译时错误;相反,它会导致一个运行时异常,提示该实体类型需要定义主键。

为了在开发期间管理 schema,当前实现采用了一种临时做法来重置数据库状态:确保每次应用启动时都删除并重新创建数据库。

```csharp
// DIRTY HACK, we WILL come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/taking-a-look-at-the-db-schema-54129916/?t=130)

要修改这些默认行为并微调 schema,开发者必须使用 Data Annotations 或 Fluent API,后续课程会讲到它们。

## 2. Introducing DataAnnotations for our Model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/) · 4:49

### 总结

本课介绍 Data Annotations,作为微调 Entity Framework Core 模型 schema 的一种方法。
它探讨了 System.ComponentModel.DataAnnotations 和 System.ComponentModel.DataAnnotations.Schema 命名空间中的特性,用于定义主键、设置字符串长度、重命名表和列,以及指定数据库特定的数据类型。
本课演示了这些特性如何在 C# 模型与底层 SQL 数据库 schema 之间架起桥梁,确保诸如可空性和字段长度之类的约束被正确施加。

### 核心概念

- **System.ComponentModel.DataAnnotations**:通用的 .NET 特性,用于数据验证和基本的 schema 映射。
- **System.ComponentModel.DataAnnotations.Schema**:专门用于配置数据库特定存储细节的特性。
- **[Key]**:当某个属性不遵循标准命名约定时,显式将其标识为主键。
- **[MaxLength]**:定义数据库中字符串列的最大字符长度。
- **[Table] 和 [Column]**:覆盖表和列的默认命名,并允许指定数据库特定的数据类型。
- **[Required]**:强制数据库列不可为空,即使 C# 属性是可空的。

### 课程笔记

在 Entity Framework Core 中控制数据库 schema 的第一种方式,是使用 `System.ComponentModel.DataAnnotations` 命名空间中的特性。
这些特性并非 EF Core 专有;它们是在整个 .NET 生态中使用的通用数据验证特性,包括 ASP.NET 的模型验证。

起初,`Movie` 模型可能没有显式的 schema 配置:

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}

public class MovieTitle
{
    public int Id { get; set; }
    public string? Title { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=10)

要限制数据库字段的长度,例如把电影标题限制为 128 个字符,可以在属性上应用 `[MaxLength]` 特性:

```csharp
using System.ComponentModel.DataAnnotations;

namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Identifier { get; set; }
    [MaxLength(128)]
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=25)

如果属性名不遵循 EF Core 关于主键的默认约定(如 `Id` 或 `MovieId`),就必须使用 `[Key]` 特性显式标识它。
在本例中,`Identifier` 属性被标记为主键:

```csharp
using System.ComponentModel.DataAnnotations;

namespace Dometrain.EFCore.API.Models;

public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=55)

第二组特性位于 `System.ComponentModel.DataAnnotations.Schema` 命名空间中,用于影响数据库存储 schema 的配置。

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dometrain.EFCore.API.Models;

public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=70)

`[Column]` 特性允许你为属性指定确切的 SQL 数据类型。
例如,你可以把字符串属性从默认的 `nvarchar` 改为 `varchar`:

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dometrain.EFCore.API.Models;

public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    [Column(TypeName = "varchar")]
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=100)

你还可以使用 `[Table]` 特性重命名数据库中的表。
这样 C# 类名可以保持为 `Movie`,而数据库表名为 `Pictures`:

```csharp
[Table("Pictures")]
public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    [Column(TypeName = "varchar")]
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=115)

`[Column]` 特性还可以同时用于重命名列和设置其类型。
这里,`Synopsis` 属性被映射到名为 `Plot`、类型为 `varchar(max)` 的列:

```csharp
[Table("Pictures")]
public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    [Column(TypeName = "varchar")]
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    [Column("Plot", TypeName = "varchar(max)")]
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=145)

对于日期属性,EF Core 默认使用 `datetime2`。
如果应用只需要日期部分,可以指定 `date` 类型以获得更好的存储效率:

```csharp
[Table("Pictures")]
public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    [Column(TypeName = "varchar")]
    public string? Title { get; set; }
    [Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; }
    [Column("Plot", TypeName = "varchar(max)")]
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=175)

最后,你可以控制数据库中的可空性。
虽然 C# 属性可能是可空的(例如 `string?`),但你可以使用 `[Required]` 特性强制数据库列不可为空:

```csharp
public class Movie
{
    [Key]
    public int Identifier { get; set; }
    [MaxLength(128)]
    [Column(TypeName = "varchar")]
    [Required]
    public string? Title { get; set; }
    [Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; }
    [Column("Plot", TypeName = "varchar(max)")]
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/introducing-dataannotations-for-our-model-54129918/?t=250)

注意,虽然许多特性会影响数据库 schema,但一些通用验证特性(如 `[MinLength]`)对 SQL schema 没有直接影响,不过仍可用于应用层面的验证。

## 3. Achieving similar results with FluentAPI

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/) · 9:42

### 总结

本课演示如何在 Entity Framework Core 中用 Fluent API 取代 Data Annotations,以实现更清晰的关注点分离。
通过把映射逻辑移入 DbContext 的 OnModelCreating 方法,或移入使用 IEntityTypeConfiguration<T> 的专用配置类,开发者可以让领域模型摆脱持久化专用的特性,并更好地处理数据库特定的需求。

### 核心概念

- 关注点分离:从领域模型中移除持久化逻辑。
- 数据库可移植性:在不修改领域类的情况下处理引擎特定的数据类型。
- OnModelCreating 方法:Fluent API 配置的入口点。
- ModelBuilder API:用于配置实体、表、键和属性的方法。
- IEntityTypeConfiguration<T>:用于把实体配置模块化的接口。
- ApplyConfiguration:用于在 DbContext 中注册外部配置类的方法。

### 课程笔记

使用 Data Annotations 配置数据库 schema 会带来两个主要问题。
第一,它以特性的形式把持久化逻辑加到领域类中,违反了关注点分离。
第二,它可能妨碍数据库可移植性,因为某些数据类型或列名在不同数据库引擎之间可能不同(例如 SQL Server 与 Oracle)。

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dometrain.EFCore.API.Models;

[Table("Pictures")]
public class Movie
{
    [Key]
    public int Identifier { get; set; }

    [MaxLength(128)]
    [Column(TypeName = "varchar")]
    [Required]
    public string? Title { get; set; }

    [Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; }

    [Column("Plot", TypeName = "varchar(max)")]
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=10)

为了解决这些问题,可以从领域类中移除所有与持久化相关的特性,留下一个干净的 POCO(Plain Old CLR Object)。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=70)

然后通过重写 OnModelCreating 方法,把映射逻辑移到 DbContext 中。
该方法会在上下文首次初始化时调用一次,用来构建 Entity Framework 使用的内部模型。

```csharp
public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("""
            Data Source=localhost;
            Initial Catalog=MoviesDB;
            User Id=sa;
            Password=MySaPassword123;
            TrustServerCertificate=True;
            """);
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=100)

在 OnModelCreating 内部,ModelBuilder 提供了一种构建器模式来配置实体。
你可以用 ToTable 指定表名,用 HasKey 定义主键。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Movie>()
        .ToTable("Pictures")
        .HasKey(movie => movie.Identifier);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=205)

单个属性使用 Property 方法进行配置。
这样可以对列类型、最大长度、可空性和列名进行细粒度控制。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Movie>()
        .ToTable("Pictures")
        .HasKey(movie => movie.Identifier);

    modelBuilder.Entity<Movie>().Property(movie => movie.Title)
        .HasColumnType("varchar")
        .HasMaxLength(128)
        .IsRequired();

    modelBuilder.Entity<Movie>().Property(movie => movie.ReleaseDate)
        .HasColumnType("date");

    modelBuilder.Entity<Movie>().Property(movie => movie.Synopsis)
        .HasColumnType("varchar(max)")
        .HasColumnName("Plot");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=325)

随着实体数量增长,把所有映射都定义在 OnModelCreating 中会导致 DbContext 杂乱且难以管理。
推荐的做法是通过实现 IEntityTypeConfiguration<T> 接口,把每个实体的配置分离到各自的类中。

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dometrain.EFCore.API.Models;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        // Configuration goes here
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=460)

配置逻辑从 DbContext 移入映射类的 Configure 方法中,使用传入的 EntityTypeBuilder<T>。

```csharp
public void Configure(EntityTypeBuilder<Movie> builder)
{
    builder
        .ToTable("Pictures")
        .HasKey(movie => movie.Identifier);

    builder.Property(movie => movie.Title)
        .HasColumnType("varchar")
        .HasMaxLength(128)
        .IsRequired();

    builder.Property(movie => movie.ReleaseDate)
        .HasColumnType("date");

    builder.Property(movie => movie.Synopsis)
        .HasColumnType("varchar(max)")
        .HasColumnName("Plot");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=575)

最后,必须在 OnModelCreating 中使用 ApplyConfiguration 方法,让 DbContext 知道这些外部配置。
虽然可以逐个应用配置,但开发者也可以使用 ApplyConfigurationsFromAssembly 从指定程序集加载所有配置。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfiguration(new MovieMapping());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/achieving-similar-results-with-fluentapi-54129919/?t=565)

## 4. One-to-many relationships between entities

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/) · 14:22

### 总结

本课讲解如何在 Entity Framework Core 中实现一对多关系,演示了如何为电影与类型(genre)这类实体之间的关系建模。
它解释了在关系两端使用导航属性、按约定自动生成外键,以及在不遵循约定时使用 Fluent API 显式配置关系。
此外,本课还探讨了使用 HasData 进行数据种子填充、使用 Include 方法预先加载相关实体,以及使用 JsonIgnore 特性解决 JSON 序列化过程中的循环引用问题。

### 核心概念

- 一对多关系建模(例如一个 Genre 对应多个 Movie)。
- 导航属性:"多"的一端是单个引用,"一"的一端是集合。
- 外键约定:自动映射 [Entity]Id 属性。
- Fluent API 配置:使用 HasOne、WithMany 和 HasForeignKey 进行自定义映射。
- 数据种子:使用 HasData 向数据库插入初始数据。
- 预先加载(Eager Loading):使用 Include 方法显式获取相关实体。
- 循环引用处理:使用 [JsonIgnore] 防止序列化时出现无限循环。

### 课程笔记

在关系型数据库中,一对多关系由"一"端表中的主键和"多"端表中对应的外键来定义。
例如,如果一部电影属于单一类型,`Genres` 表包含主键,而 `Movies` 表包含一个指向该类型的外键。

要在 Entity Framework Core 中为此建模,首先在关系的"多"端添加一个导航属性。
在 `Movie` 实体中添加一个 `Genre` 类型的属性,就足以让 EF Core 识别出这个关系,并在数据库中自动生成所需的外键。

```csharp
public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }

    public Genre Genre { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=85)

应用此模型后,EF Core 会在数据库中生成一个外键约束,把 `Pictures` 表(映射到 `Movie` 实体的表)中的 `GenreId` 链接到 `Genres` 表中的 `Id`。

```sql
ALTER TABLE [dbo].[Pictures] WITH CHECK ADD CONSTRAINT [FK_Pictures_Genres_GenreId] FOREIGN KEY([GenreId])
    REFERENCES [dbo].[Genres] ([Id])
    ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Genres_GenreId]
GO
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=130)

```text
 Genres                    Pictures (Movie)
+-----------+             +-------------------+
| Id   (PK) |<------------| GenreId (FK)      |
| Name      |   1 : many  | Identifier (PK)   |
+-----------+             | Title ...         |
                          +-------------------+
```

你还可以在"一"端定义一个反向导航属性,以便查询与某个特定类型关联的所有电影。
这通常使用 `ICollection<T>` 来实现。

```csharp
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=205)

虽然 EF Core 能按约定推断关系,但你可能希望在模型中显式声明外键,或使用自定义名称。
如果外键属性遵循 `[NavigationPropertyName]Id` 或 `[TargetEntityName]Id` 模式,EF Core 会自动关联它。
然而,如果偏离这些约定,例如把外键命名为 `MainGenreId`,EF Core 就不会自动把它与 `Genre` 导航属性关联起来,可能导致出现两个独立的列(一个是基于约定的外键,一个是自定义属性)。

```csharp
public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }

    public Genre Genre { get; set; }
    public int MainGenreId { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=255)

要解决这个问题,可以在 `IEntityTypeConfiguration<Movie>` 实现中使用 Fluent API。
`HasOne` 和 `WithMany` 方法定义导航属性,而 `HasForeignKey` 显式地把自定义属性链接为外键。
`HasPrincipalKey` 也可以用来指定所引用的是主体实体上的哪个属性,不过它默认就是主键。

```csharp
builder
    .HasOne(movie => movie.Genre)
    .WithMany(genre => genre.Movies)
    .HasPrincipalKey(genre => genre.Id)
    .HasForeignKey(movie => movie.MainGenreId);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=430)

为了确保有数据可供测试,可以使用 `HasData` 方法为数据库填充种子数据。
每当数据库被创建时,这些数据都会被插入。

```csharp
builder.HasData(new Movie
{
    Identifier = 1,
    Title = "Fight Club",
    ReleaseDate = new DateTime(1999, 9, 10),
    Synopsis = "Ed Norton and Brad Pitt have a couple of fist fights with each other.",
    MainGenreId = 1
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=550)

```csharp
public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasData(new Genre { Id = 1, Name = "Drama" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=565)

默认情况下,EF Core 不使用延迟加载。
要获取相关实体,必须在查询中通过 `Include` 方法进行预先加载。

```csharp
[HttpGet("{id:int}")]
public async Task<IActionResult> Get([FromRoute] int id)
{
    var movie = await _context.Movies
        .Include(movie => movie.Genre)
        .SingleOrDefaultAsync(m => m.Identifier == id);
    
    return movie == null
        ? NotFound()
        : Ok(movie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=655)

当两个方向上都存在导航属性时,JSON 序列化可能因循环引用而失败(例如 Movie 引用 Genre,而 Genre 又引用 Movies 集合)。
为防止这种情况,在关系的一端应用 `[JsonIgnore]` 特性。

```text
 Movie.Genre ------> Genre
     ^                 |
     |                 | Genre.Movies
     +-----------------+   [JsonIgnore]
```

```csharp
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/one-to-many-relationships-between-entities-54129920/?t=790)

## 5. Converting between different data types

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/) · 12:08

### 总结

Entity Framework Core 中的值转换器(value converter)可以在 C# 模型类型与不同的数据库存储类型之间实现无缝转换。
虽然 EF Core 为常见场景(如把日期映射为字符串、把枚举映射为整数)提供了许多内置转换器,但开发者也可以通过继承 ValueConverter 类,为特殊格式实现自定义逻辑。
不过,把枚举转换为字符串时需要谨慎;因为数据库查询对字符串执行的是字典序比较,而不是对底层枚举值做数值比较,所以"小于"或"大于"之类的筛选操作可能产生意料之外的结果。

### 核心概念

- **值转换器**:在数据于 C# 模型与数据库提供程序之间传递时对其进行转换的机制。
- **内置转换器**:预定义的映射(例如 `DateTimeToStringConverter`),在使用 `HasConversion<T>` 时由 EF Core 自动应用。
- **自定义值转换器**:继承自 `ValueConverter<TModel, TProvider>` 的类,用于定义特定的转换逻辑。
- **字典序比较陷阱**:在数据库中比较映射为字符串的枚举时可能得到错误的查询结果,因为字符串按字母顺序而非数值顺序排序。
- **JSON 字符串枚举转换**:配置 API 在 JSON 负载中把枚举作为字符串处理,与数据库映射相互独立。

### 课程笔记

#### 基本值转换

默认情况下,Entity Framework Core 会把 C# 类型映射为最接近的数据库对应类型。
例如,`DateTime` 属性通常映射为 `datetime2` 或 `date` 列。
然而,如果你需要把 `DateTime` 存储为字符串(例如用 `char(23)` 以容纳毫秒),就必须使用值转换器。

```csharp
public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }

    public Genre Genre { get; set; }
    public int MainGenreId { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=10)

要实现到字符串的简单转换,在实体映射配置中使用 `HasConversion<T>` 方法。
EF Core 会利用内置转换器自动处理转换。

```csharp
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .HasKey(movie => movie.Identifier);

        builder.Property(movie => movie.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(movie => movie.ReleaseDate)
            .HasColumnType("char(23)")
            .HasConversion<string>();

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Id)
            .HasForeignKey(movie => movie.MainGenreId);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=70)

#### 自定义值转换器

当内置转换器不能满足特定需求时,例如以不带分隔符的紧凑 `char(8)` 格式(`yyyyMMdd`)存储日期,你可以通过继承 `ValueConverter<TModel, TProvider>` 创建自定义转换器。
这需要向基类构造函数提供两个表达式:一个用于从模型转换到提供程序(C# 到 DB),一个用于从提供程序转换到模型(DB 到 C#)。

```csharp
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dometrain.EFCore.API.Data.ValueConverters;

public class DateTimeToChar8Converter : ValueConverter<DateTime, string>
{
    public DateTimeToChar8Converter() : base(
        dateTime => dateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
        stringValue => DateTime.ParseExact(stringValue, "yyyyMMdd", CultureInfo.InvariantCulture)
        )
    { }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=310)

```text
            model -> provider  (C# to DB)
 DateTime  ---------------------------------->  string "yyyyMMdd"
           <----------------------------------  char(8)
            provider -> model  (DB to C#)
```

要应用自定义转换器,在映射配置中把它的实例传给 `HasConversion` 方法:

```csharp
builder.Property(movie => movie.ReleaseDate)
    .HasColumnType("char(8)")
    .HasConversion(new DateTimeToChar8Converter());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=340)

#### 枚举转字符串的陷阱

在数据库中把枚举映射为字符串是一个常见需求,但它会给基于范围的查询带来显著风险。
来看一个 `AgeRating` 枚举:

```csharp
public enum AgeRating
{
    All = 0,
    ElementarySchool = 6,
    HighSchool = 12,
    Adolescent = 16,
    Adult = 18
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=445)

假设你实现了一个控制器方法,用数值比较按年龄分级筛选电影:

```csharp
[HttpGet("until-age/{ageRating}")]
[ProducesResponseType(typeof(List<MovieTitle>), StatusCodes.Status200OK)]
public async Task<IActionResult> GetAllUntilAge([FromRoute] AgeRating ageRating)
{
    var filteredTitles = await _context.Movies
        .Where(movie => movie.AgeRating <= ageRating)
        .Select(movie => new MovieTitle { Id = movie.Identifier, Title = movie.Title})
        .ToListAsync();

    return Ok(filteredTitles);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=460)

为确保 API 在 JSON 中把这些枚举作为字符串处理(以便在 Swagger 等工具中更易读),可以在 `Program.cs` 中配置 JSON 序列化器选项:

```csharp
builder.Services.AddControllers()	
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=505)

然而,如果数据库映射也把枚举转换为字符串:

```csharp
builder.Property(movie => movie.AgeRating)
    .HasColumnType("varchar(32)")
    .HasConversion<string>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/converting-between-different-data-types-54129921/?t=625)

EF Core 会把 C# 中的比较(`<=`)翻译为 SQL 字符串比较。
由于字符串按字典序比较,"All" 被认为大于 "Adolescent"(因为 'A-l' 排在 'A-d' 之后)。
因此,查询适合 "All" 年龄的电影时,可能会错误地返回分级为 "Adolescent" 的电影,因为数据库执行的是字母排序,而不是基于枚举底层值的数值排序。

## 6. Using owned and complex types to group data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/) · 10:03

有时我们希望把多个数据库列归组到单个 C# 类型中。
Entity Framework Core 提供了两种主要方式来实现这一点:**Complex Properties**(复杂属性)和 **Owned Types**(从属类型)。

### 核心概念

- **Complex Properties**:一组列的归组,适用于类值类型(例如地址)。
  默认情况下,它们与父实体映射到同一张表。
- **Owned Types (`OwnsOne`)**:它们在幕后被视为独立的实体,但在所有者之外没有自己的标识。
  它们可以映射到同一张表,也可以映射到单独的表。
- **表拆分(Table Splitting)**:使用 `.ToTable()` 方法把从属类型移到单独数据库表中的能力。
- **`OwnsMany`**:用于映射从属类型的集合,通常会生成一张使用复合主键的单独表。
- **影子属性(Shadow Properties)**:EF Core 使用内部属性(如 `MovieIdentifier`)来管理所有者与从属类型之间的关系。
- **自动包含**:复杂属性和从属类型都会随父实体自动获取,无需显式调用 `.Include()`。

### 课程笔记

为了演示这些概念,我们定义一个 `Person` 类来表示导演和演员等人物。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=10)

然后我们给 `Movie` 实体添加一个 `Person` 类型的 `Director` 属性。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public Person Director { get; set; }

    public Genre Genre { get; set; }
    public int MainGenreId { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=45)

#### 复杂属性

通过使用 `ComplexProperty`,EF Core 会把 `Person` 类的属性映射为 `Pictures` 表中的列。
按照约定,这些列以属性名作为前缀来命名(例如 `Director_FirstName` 和 `Director_LastName`)。

```csharp
builder.ComplexProperty(movie => movie.Director);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=85)

```text
 Pictures
+---------------------+
| Identifier          |
| Title               |
| ...                 |
| Director_FirstName  |
| Director_LastName   |
+---------------------+
```

#### 从属类型

从属类型使用 `OwnsOne` 进行配置。
虽然它们起初看起来与复杂属性类似,都映射到同一张表,但由于 EF Core 把它们视为依赖实体,它们提供了更强的能力。

```csharp
builder.OwnsOne(movie => movie.Director);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=145)

从属类型的一个显著优势,是能够把数据移到单独的表中,同时在代码中保持逻辑上的关系。
这通过 `.ToTable()` 实现。

```csharp
builder.OwnsOne(movie => movie.Director)
    .ToTable("Movie_Directors");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=205)

#### 从属类型的集合

如果实体拥有某个应当作为从属类型的类型的集合,就使用 `OwnsMany`。
例如,给 `Movie` 类添加一个演员集合:

```csharp
public class Movie
{
    public int Identifier { get; set; }
    // ... other properties
    public Person Director { get; set; }
    public ICollection<Person> Actors { get; set; }
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=295)

在映射配置中,使用 `OwnsMany` 来定义这一关系。
这会生成一张单独的表,其主键由所有者的标识符和一个内部序列 ID 组合而成。

```csharp
builder.OwnsMany(movie => movie.Actors)
    .ToTable("Movie_Actors");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=325)

```text
 Pictures               Movie_Actors
+--------------+       +--------------------------------+
| Identifier   |<------| MovieIdentifier  \  composite  |
| Title ...    |       | Id               /  PK         |
+--------------+       | FirstName                      |
                       | LastName                       |
                       +--------------------------------+
```

#### 为从属类型填充种子数据

为从属类型填充种子数据需要特定的做法。
由于 `Person` 类不包含 EF Core 用来把它链接到 `Movie` 的外键(影子属性),你必须在 `HasData` 方法中使用匿名类型来提供 `MovieIdentifier`。

```csharp
builder.OwnsOne(movie => movie.Director)
    .HasData(new { MovieIdentifier = 1, FirstName = "David", LastName = "Fincher" });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=445)

对于 `OwnsMany` 集合,你还必须提供 EF Core 在从属表中用于复合主键的内部 `Id`。

```csharp
builder.OwnsMany(movie => movie.Actors)
    .HasData(
        new { MovieIdentifier = 1, Id = 1, FirstName = "Edward", LastName = "Norton" },
        new { MovieIdentifier = 1, Id = 2, FirstName = "Brad", LastName = "Pitt" }
    );
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-owned-and-complex-types-to-group-data-54129922/?t=560)

## 7. Generating values in the model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/) · 9:00

### 总结

本课探讨 Entity Framework Core 中超越简单 identity 列的高级值生成技术。
它演示了如何为实体实现自动时间戳,既可以通过 SQL 函数使用数据库端默认值,也可以通过自定义值生成器使用客户端逻辑。
本课强调了这些做法之间的关键区别,例如 HasDefaultValue 在配置时的行为与 HasDefaultValueSql 在执行时的行为,以及 ValueGenerator 实现适用或受限的具体场景,特别是在数据种子方面。

### 核心概念

- 使用 `[JsonIgnore]` 配置仅供内部使用的属性。
- `HasDefaultValue`(静态)与 `HasDefaultValueSql`(动态)之间的区别。
- 在 `SaveChanges` 之后自动取回数据库生成的值。
- 使用 `ValueGenerator<T>` 实现自定义的客户端逻辑。
- 使用 `HasData` 填充种子数据时值生成器的局限。

### 课程笔记

为了跟踪实体的创建时间,可以向模型添加一个 `CreatedDate` 属性。
在本例中,该属性被添加到 `Genre` 类。
由于它仅供内部使用,因此应用了 `[JsonIgnore]` 特性,防止它在 API 中暴露。

```csharp
using System.Text.Json.Serialization;

namespace Dometrain.EFCore.API.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/?t=35)

#### 数据库端生成

值生成可以在实体映射中配置。
一个常见错误是使用 `HasDefaultValue(DateTime.Now)`。
这会把默认值设为配置代码运行时(例如应用启动或生成迁移期间)的那个特定时间戳,意味着此后创建的每条记录都会共享同一个静态时间戳。

```csharp
public void Configure(EntityTypeBuilder<Genre> builder)
{
    builder.Property(genre => genre.CreatedDate)
        .HasDefaultValue(DateTime.Now);

    builder.HasData(new Genre { Id = 1, Name = "Drama" });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/?t=85)

要在插入的那一刻正确生成时间戳,请使用 `HasDefaultValueSql`。
这会使用 `getdate()` 之类的 SQL 函数,为数据库列配置一个默认约束。

```csharp
public void Configure(EntityTypeBuilder<Genre> builder)
{
    builder.Property(genre => genre.CreatedDate)
        .HasDefaultValueSql("getdate()");

    builder.HasData(new Genre { Id = 1, Name = "Drama" });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/?t=125)

当某个值由数据库生成时,EF Core 会在调用 `SaveChanges` 之后自动把它取回,确保内存中的实体实例与数据库保持同步。
如果 EF Core 没有自动检测到这一点(例如对于计算列或触发器),你可以使用 `ValueGeneratedOnAdd()` 或 `ValueGeneratedOnUpdate()` 显式配置此行为。

#### 客户端值生成器

对于更复杂的场景,或者当生成应在应用内部进行时,EF Core 提供了 `ValueGenerator` 类。
自定义生成器可以检查整个实体条目(entity entry)来确定值。

```csharp
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Dometrain.EFCore.API.Data.ValueGenerators;

public class CreatedDateGenerator : ValueGenerator<DateTime>
{
    public override DateTime Next(EntityEntry entry)
    {
        return DateTime.UtcNow;
    }

    public override bool GeneratesTemporaryValues => false;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/?t=415)

生成器在映射中通过 `HasValueGenerator<T>()` 注册。
这确保每当有新实体被添加到 `DbContext` 且该属性尚未设置值时,生成器都会运行。

```csharp
public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(genre => genre.CreatedDate)
            .HasValueGenerator<CreatedDateGenerator>();

        builder.HasData(new Genre { Id = 1, Name = "Drama" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/generating-values-in-the-model-54129923/?t=445)

##### 种子数据的局限

虽然 `ValueGenerator` 很强大,但它只在实体被显式添加到上下文时才会触发。
它对在迁移中通过 `HasData` 填充的种子数据不起作用,因为在迁移过程中,这些数据不会经过标准的 `DbContext` 变更跟踪流程。
相比之下,数据库级别的默认值(`HasDefaultValueSql`)在所有场景下都有效,包括种子数据。

## 8. Hiding some database fields from our model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/) · 4:09

影子属性(shadow property)是 Entity Framework Core(EF Core)会跟踪、但并未在 C# 实体类中定义的属性。
这让你可以把数据持久化到数据库中,同时对应用的其余部分隐藏它,防止开发者意外修改内部元数据。

### 核心概念

- **影子属性**:存在于 EF Core 模型和数据库中、但不存在于 C# 类中的属性。
- **Fluent API 配置**:使用 `builder.Property<T>("Name")` 语法定义影子属性。
- **值生成器**:无需手动赋值即可自动填充影子属性(例如用于时间戳)。
- **变更跟踪器访问**:通过 `DbContext.Entry` API 访问或修改影子属性的值。
- **列映射**:使用 `HasColumnName` 把影子属性映射到特定的数据库列名。

### 课程笔记

要实现影子属性,首先从 C# 实体类中移除该属性。
在本例中,`CreatedDate` 属性从 `Genre` 模型中移除,以防它在 API 中暴露或被开发者修改。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/?t=40)

由于该属性已不存在于类上,标准的 lambda 选择器(例如 `builder.Property(genre => genre.CreatedDate)`)将无法再编译。
你必须改用 `Property` 方法基于字符串的重载,并把属性类型指定为泛型参数。
如果提供的名称与类上现有的任何属性都不匹配,EF Core 就会把它视为影子属性。

```csharp
using Dometrain.EFCore.API.Data.ValueGenerators;
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasValueGenerator<CreatedDateGenerator>();

        builder.HasData(new Genre { Id = 1, Name = "Drama" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/?t=70)

影子属性支持与标准属性相同的配置选项。
例如,你可以使用 `.HasColumnName()` 把影子属性映射到数据库中的另一个列名。

```csharp
using Dometrain.EFCore.API.Data.ValueGenerators;
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
        
        builder.HasData(new Genre { Id = 1, Name = "Drama" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/?t=105)

```text
 C# class Genre      EF Core model                DB table
+------------+      +------------------------+   +-------------+
| Id         |----->| Id                     |-->| Id          |
| Name       |----->| Name                   |-->| Name        |
|            |      | CreatedDate (shadow)   |-->| CreatedAt   |
+------------+      +------------------------+   +-------------+
```

与 API 交互时,影子属性对模型是完全隐藏的。
创建新资源时,该属性不会出现在请求 schema 中。

```json
{
  "id": 0,
  "name": "string"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/?t=115)

同样,提交 POST 请求时,只需要 C# 类中定义的属性。

```json
{
  "name": "Comedy"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/?t=125)

虽然该属性对模型隐藏,但它仍然被持久化到数据库中,并且可以通过 EF Core 的变更跟踪器(Change Tracker)访问。
在控制器或服务中,可以使用 `DbContext` 上的 `Entry` 方法来获取影子属性的值。

```csharp
[HttpGet("{id:int}")]
[ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Get([FromRoute] int id)
{
    var genre = await _context.Genres.FindAsync(id);
    
    return genre == null
        ? NotFound()
        : Ok(genre);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/hiding-some-database-fields-from-our-model-54129924/?t=160)

要在运行时检查该值(例如在调试时的即时窗口中),可以使用:
`_context.Entry(genre).Property("CreatedDate").CurrentValue`

必要时,同样的机制也可以用来手动设置影子属性的值,同时仍将其排除在公开的实体模型之外。

## 9. Applying global query filters

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/) · 3:28

### 总结

Entity Framework Core 中的全局查询筛选器提供了一种机制,可以把一个谓词自动应用到针对特定实体类型的所有 LINQ 查询上。
这对于实现软删除或多租户等横切关注点特别有用,它确保某些数据默认就被排除在结果之外,而无需在每个查询中手动筛选。

### 核心概念

- `HasQueryFilter`:在模型配置期间用于定义筛选谓词的 Fluent API 方法。
- **自动应用**:筛选器会应用于针对该实体的所有查询,包括使用 `FindAsync`、`FirstOrDefault` 或 `Include` 的查询。
- **全局作用域**:一旦定义,筛选器就能确保不符合条件的数据默认永远不会被应用逻辑获取。
- **绕过筛选器**:必要时,可以在 `IQueryable` 上使用 `IgnoreQueryFilters()` 扩展方法来绕过全局筛选器。
- **常见用例**:实现软删除(例如 `IsDeleted == false`)或多租户隔离。

### 课程笔记

在某些场景下,数据库中的某些记录应当对应用模型隐藏,例如逻辑上已删除的数据,或与当前应用上下文无关的记录。
Entity Framework Core 允许使用全局查询筛选器把这些规则定义在一个地方。

来看应用中使用的以下 `Movie` 模型:

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public Person Director { get; set; }
    public ICollection<Person> Actors { get; set; }

    public Genre Genre { get; set; }
    public int MainGenreId { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/?t=10)

要应用全局查询筛选器,请在 `IEntityTypeConfiguration<T>` 实现中使用 `HasQueryFilter` 方法。
该方法接受一个 lambda 表达式,用来定义要应用到该实体所有查询上的 `Where` 子句。

起初,`MovieMapping` 配置可能是这样的:

```csharp
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .HasKey(movie => movie.Identifier);

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

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/?t=25)

通过添加 `HasQueryFilter`,你可以限制返回的数据。
例如,只显示 2000 年及以后上映的电影:

```csharp
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .HasQueryFilter(movie => movie.ReleaseDate >= new DateTime(2000,1,1))
            .HasKey(movie => movie.Identifier);

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

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/?t=70)

当此筛选器生效时,即使数据库中存在一条 `ReleaseDate` 在 1999 年的记录,它也会被排除在所有查询之外。

```sql
SELECT TOP (1000) [Identifier]
      ,[Title]
      ,[ReleaseDate]
      ,[Plot]
      ,[AgeRating]
      ,[MainGenreId]
  FROM [MoviesDB].[dbo].[Pictures]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/?t=90)

如果应用尝试按 ID 获取一条被筛选掉的记录,EF Core 将不返回任何结果,在 API 上下文中会导致 `404 Not Found` 响应:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-8ca27e73366e9408a48e50e7d7ae018b-4cce5a73eec6db0c-00"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/?t=115)

出现这种行为,是因为 EF Core 会把筛选器的谓词作为 `WHERE` 子句自动追加到为每个查询生成的 SQL 中。
这是一种防止数据意外暴露的强大安全机制。
如果调整筛选器使其包含相关的日期范围,数据就又可以访问了:

```csharp
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .HasQueryFilter(movie => movie.ReleaseDate >= new DateTime(1990,1,1))
            .HasKey(movie => movie.Identifier);

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

        builder.Property(movie => movie.AgeRating)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();

        builder.OwnsOne(movie => movie.Director)
            .ToTable("Movie_Directors");
        
        builder.OwnsMany(movie => movie.Actors)
            .ToTable("Movie_Actors");

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Id)
            .HasForeignKey(movie => movie.MainGenreId);
        
        builder.HasData(new Movie
        {
            Identifier = 1,
            Title = "Fight Club",
            ReleaseDate = new DateTime(1999, 9, 10),
            Synopsis = "Ed Norton and Brad Pitt have a couple of fist fights with each other.",
            MainGenreId = 1,
            AgeRating = AgeRating.Adolescent
        });

        builder.OwnsOne(movie => movie.Director)
            .HasData(new { MovieIdentifier = 1, FirstName = "David", LastName = "Fincher" });

        builder.OwnsMany(movie => movie.Actors)
            .HasData(
                new { MovieIdentifier = 1, Id = 1, FirstName = "Edward", LastName = "Norton" },
                new { MovieIdentifier = 1, Id = 2, FirstName = "Brad", LastName = "Pitt" }
            );
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/applying-global-query-filters-54129925/?t=190)

## 10. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129926/) · 1:08

### 总结

本课全面回顾了本章所讲的高级模型配置技术。
它着重说明了从基础的 Data Annotations 过渡到更强大的 Fluent API,以控制数据库 schema 和实体行为。
所总结的关键主题包括关系映射、复杂类型归组、值生成、数据转换、影子属性和全局查询筛选器,为构建可用于生产环境的 Entity Framework Core 应用打下基础。

### 核心概念

- 从 Data Annotations 过渡到 Fluent API 进行模型配置。
- 映射一对多关系并配置外键。
- 使用从属类型(`OwnsOne` 和 `OwnsMany`)实现复杂类型。
- 自动为数据库列生成值。
- 使用值转换器在应用与数据库之间转换数据。
- 利用影子属性管理内部元数据。
- 应用全局查询筛选器以强制执行一致的数据访问规则。

### 课程笔记

本章着重于对 Entity Framework Core 模型、`DbContext` 以及由此生成的数据库 schema 获得细粒度的控制。
虽然初始配置常常依赖 Data Annotations,但 Fluent API 为高级场景提供了必要的灵活性。

#### 模型配置与关系

Fluent API 可以精确地把实体映射到表、定义主键并建立关系。
例如,`Genre` 与 `Movie` 之间的一对多关系是通过指定主体键和外键来配置的。
复杂类型通过从属类型来管理,使属性可以被归组到 `Director` 或 `Actors` 这样的对象中,同时仍属于实体数据结构的一部分。

```csharp
using Dometrain.EFCore.API.Data.ValueConverters;
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .HasQueryFilter(movie => movie.ReleaseDate >= new DateTime(1990,1,1))
            .HasKey(movie => movie.Identifier);

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

        builder.Property(movie => movie.AgeRating)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();

        builder.OwnsOne(movie => movie.Director)
            .ToTable("Movie_Directors");
        
        builder.OwnsMany(movie => movie.Actors)
            .ToTable("Movie_Actors");

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Id)
            .HasForeignKey(movie => movie.MainGenreId);
        
        // Seed - data that needs to be created always
        builder.HasData(new Movie
        {
            Identifier = 1,
            Title = "Fight Club",
            ReleaseDate = new DateTime(1999, 9, 10),
            Synopsis = "Ed Norton and Brad Pitt have a couple of fist fights with each other.",
            MainGenreId = 1,
            AgeRating = AgeRating.Adolescent
        });

        builder.OwnsOne(movie => movie.Director)
            .HasData(new { MovieIdentifier = 1, FirstName = "David", LastName = "Fincher" });

        builder.OwnsMany(movie => movie.Actors)
            .HasData(
                new { MovieIdentifier = 1, Id = 1, FirstName = "Edward", LastName = "Norton" },
                new { MovieIdentifier = 1, Id = 2, FirstName = "Brad", LastName = "Pitt" }
            );
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129926/?t=13)

#### 值转换

值转换器负责在应用与数据库之间转换数据,例如把 `DateTime` 转换为特定的字符串格式进行存储,或把枚举映射为字符串。

```csharp
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dometrain.EFCore.API.Data.ValueConverters;

public class DateTimeToChar8Converter : ValueConverter<DateTime, string>
{
    public DateTimeToChar8Converter() : base(
        dateTime => dateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
        stringValue => DateTime.ParseExact(stringValue, "yyyyMMdd", CultureInfo.InvariantCulture)
        )
    { }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129926/?t=13)

#### 影子属性与全局查询筛选器

影子属性允许数据库存储 C# 实体类中不存在的元数据,例如 `CreatedDate`。
它们通常与值生成器结合使用,以实现数据录入的自动化。
全局查询筛选器提供了一种机制,可以把 `Where` 子句自动应用到针对给定实体类型的所有查询上,确保业务规则(如基于日期的筛选)得到一致的应用。

```csharp
using Dometrain.EFCore.API.Data.ValueGenerators;
using Dometrain.EFCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
        
        builder.HasData(new Genre { Id = 1, Name = "Drama" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129926/?t=29)

借助这些高级建模功能,构建应用时可以对数据如何被查询和持久化抱有高度信心。
迈向生产就绪的下一步,是通过迁移来管理数据库 schema 的变更。

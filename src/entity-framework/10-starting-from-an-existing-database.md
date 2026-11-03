# Starting from an existing database

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 10 章
> 共 6 课 · 约 26:05
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [When the DB already exists](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/when-the-db-already-exists-54130338/) | 0:26 | [↓](#1-when-the-db-already-exists) |
| 2 | [Using the DB to generate a model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-db-to-generate-a-model-54130339/) | 9:43 | [↓](#2-using-the-db-to-generate-a-model) |
| 3 | [How can we further evolve the model?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/how-can-we-further-evolve-the-model-54130340/) | 5:57 | [↓](#3-how-can-we-further-evolve-the-model) |
| 4 | [Regenerating every time: a demo](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/) | 4:54 | [↓](#4-regenerating-every-time-a-demo) |
| 5 | [Keeping in sync manually: a demo](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/) | 4:58 | [↓](#5-keeping-in-sync-manually-a-demo) |
| 6 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130343/) | 0:07 | [↓](#6-section-recap) |

## 1. When the DB already exists

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/when-the-db-already-exists-54130338/) · 0:26

### 总结

使用现有数据库需要从定义 schema 转变为映射已有的 schema。
本课介绍了让 Entity Framework Core 的 DbContext 和实体模型与预先定义好的表和数据保持一致的必要性,这在企业环境中是一个常见需求,因为那里的数据库是独立于应用代码进行管理的。

### 核心概念

- **现有 Schema 集成(Existing Schema Integration)**:让 EF Core 适配并非由当前应用创建的数据库。
- **映射要求(Mapping Requirements)**:确保 DbContext 和实体准确反映现有的表结构。
- **数据保留(Data Preservation)**:与已经包含生产数据的数据库进行交互。
- **映射到现有表(Mapping to Existing Tables)**:创建一个与预先定义的数据库结构保持一致的 DbContext 的必要性。

### 课程笔记

在许多开发项目中,数据库 schema 在编写应用代码之前就已经建立并填充了数据。
在这种情况下,开发者无法使用 Entity Framework Core 来定义 schema;相反,他们必须让应用去适配数据库。

要在这种情境下有效使用 EF Core,必须创建一个直接映射到现有表、列和关系的 DbContext。
这能确保 ORM 可以针对预先存在的结构正确执行 CRUD 操作和查询。
本章探讨在数据库是主要事实来源(source of truth)时实现 EF Core 的方法,从而在不改变底层 schema 的前提下实现顺畅的数据访问和修改。

## 2. Using the DB to generate a model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-db-to-generate-a-model-54130339/) · 9:43

### 总结

本课演示 Entity Framework Core 中的 "Database First" 方法,即使用一个现有的 SQL Server 数据库来搭建(scaffold)数据模型。
内容涵盖所需的 NuGet 包、使用 Entity Framework Core UI for Rider 这类 IDE 插件来提供可视化的 scaffolding 体验,以及 scaffolding 过程的配置,包括 schema 选择、复数化和连接字符串的放置位置。
本课还探讨了生成的 DbContext 和实体类,重点说明 SQL Server 元数据是如何以代码注释的形式保留下来的,最后给出一个使用 LINQ 和投影查询 scaffold 出的模型的可运行示例。

### 核心概念

- **NuGet 依赖**:Scaffolding 需要 `Microsoft.EntityFrameworkCore.SqlServer` 和 `Microsoft.EntityFrameworkCore.Tools`。
- **Scaffolding 工具**:Visual Studio 内置了支持,而 JetBrains Rider 用户可以使用 "Entity Framework Core UI" 插件获得类似的向导式体验。
- **Scaffold 配置**:选项包括指定实体的输出文件夹、启用复数化(类名为单数,`DbSet` 名为复数),以及按特定数据库 schema 过滤(例如 `SalesLT`)。
- **元数据提取**:EF Core 可以从 SQL Server 扩展属性(extended properties)中提取描述,并将其转换为 C# XML 文档注释。
- **Fluent API 生成**:scaffolding 过程会在 `DbContext` 的 `OnModelCreating` 方法中生成详细的实体映射。

### 课程笔记

#### 项目设置

要从现有数据库开始 scaffolding,项目必须包含 SQL Server provider 和 EF Core 工具。
在本例中,使用的是一个控制台应用,并包含以下包:
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

对于使用 JetBrains Rider 的开发者,"Entity Framework Core UI" 插件为 scaffolding、迁移和数据库生成提供了图形界面,与 Visual Studio 中的体验相仿。

#### Scaffolding DB Context

scaffolding 过程通过 IDE 工具启动(例如 Tools -> Entity Framework Core -> Scaffold DB Context)。
该向导需要一个数据库连接字符串,并需要选择一个数据库 provider。
关键配置选项包括:
- **模型位置(Model Location)**:为生成的 POCO 类指定一个文件夹(例如 `Entities`)。
- **复数化器(Pluralizer)**:确保 `DbSet` 属性是复数形式,而实体类名保持单数。
- **连接字符串(Connection String)**:选择是否在 `OnConfiguring` 方法中包含连接字符串。
  虽然这在演示中很方便,但在生产环境中建议通过 `DbContextOptions` 来处理。
- **Context 配置(Context Configuration)**:为 context 命名(例如 `AdventureWorksContext`)并指定其输出目录。
- **Schema 过滤(Schema Filtering)**:选择要 scaffold 的特定 schema,例如 AdventureWorks LT 2022 数据库中的 `SalesLT` schema。

```text
SQL Server: AdventureWorksLT2022
  schema SalesLT
        |
        |  Tools -> Entity Framework Core -> Scaffold DB Context
        v
+-------------------------------+     +------------------------------+
| Context/AdventureWorksContext |     | Entities/                    |
|   DbSet<Address> Addresses    |     |   Address, Customer, ...     |
|   DbSet<Customer> Customers   |     |   (singular class names)     |
|   OnModelCreating(...)        |     |                              |
+-------------------------------+     +------------------------------+
```

#### 分析生成的 Context

生成的 `DbContext` 包含一个接受 `DbContextOptions` 的构造函数、对应每张表的 `DbSet` 属性,以及一个详细的 `OnModelCreating` 方法。

```csharp
public partial class AdventureWorksContext : DbContext
{
    public AdventureWorksContext()
    {
    }

    public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    // ... other DbSets
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-db-to-generate-a-model-54130339/?t=265)

默认情况下,连接字符串可能会被硬编码在 `OnConfiguring` 中并附带一条警告,不过这可以在 scaffolding 时禁用。

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=AdventureWorksLT2022; User Id=sa; Password=MySaPassword123; TrustServerCertificate=True;");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-db-to-generate-a-model-54130339/?t=280)

#### 实体映射与元数据

EF Core 将数据库表映射为类,并在 `OnModelCreating` 中使用 Fluent API 来定义键、索引和表注释。
如果 SQL Server 数据库在字段元数据(扩展属性)中包含描述,EF Core 会提取这些描述,并将其作为 XML 注释应用到生成的 C# 代码中。

```csharp
/// <summary>
/// Street address information for customers.
/// </summary>
public partial class Address
{
    /// <summary>
    /// Primary key for Address records.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// First street address line.
    /// </summary>
    public string AddressLine1 { get; set; } = null!;
    
    // ... other properties and navigation properties
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-db-to-generate-a-model-54130339/?t=340)

#### 查询模型

scaffold 完成后,context 立即可用。
你可以执行复杂的 LINQ 查询,包括过滤、投影以及访问导航属性(例如统计关联记录的数量)。

```csharp
using Dometrain.EFCore.DatabaseFirst.Context;

var context = new AdventureWorksContext();

var query = context.Customers
    .Where(c => c.FirstName.StartsWith("C"))
    .Select(c => new { c.FirstName, c.LastName, Count = c.SalesOrderHeaders.Count });

foreach (var customer in query)
{
    Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Count}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-the-db-to-generate-a-model-54130339/?t=505)

虽然生成的 `OnModelCreating` 可能会变得相当庞大(可能有数百行),但它是一个可用的起点,之后可以进一步重构为独立的配置类,或者在数据库 schema 发生变化时重新生成。

## 3. How can we further evolve the model?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/how-can-we-further-evolve-the-model-54130340/) · 5:57

### 总结

在 Entity Framework Core 中基于现有数据库演进模型时,开发者必须选择一种同步策略,因为 EF6 中的 "Update Model from Database" 功能目前在 EF Core 中并不存在。
主要方法包括:通过生成初始 schema 转向 code-first 迁移、在每次数据库变更时重新生成整个模型和 context,或者手动更新 C# 实体和 Fluent API 配置。
对于需要在 database-first 工作流中实现自动同步的团队,LLBL Gen Pro 或 DevArt Entity Developer 等第三方工具提供了比标准 EF Core CLI 工具更强大的替代方案。

### 核心概念

- **Code-First 迁移(Code-First Migrations)**:在首次 scaffold 之后,把 schema 的控制权交给 EF Core。
- **重新生成策略(Regeneration Strategy)**:重新运行 scaffolding 命令以确保模型与数据库一致,代价是丢失手动的代码定制。
- **手动映射(Manual Mapping)**:手动更新实体和配置以反映数据库变更,从而得到更精炼的领域模型。
- **EF Core Backlog**:"Update Model from Database" 功能请求的状态。
- **第三方设计器(Third-Party Designers)**:支持数据库到模型同步的外部工具,例如 LLBL Gen Pro 和 DevArt Entity Developer。

### 课程笔记

当必须演进一个现有数据库时,有多种策略可用,取决于 schema 变更的事实来源是代码还是数据库。

#### 转向迁移

如果开发团队能够接管未来的数据库变更,那么推荐的路径是 Entity Framework Core 迁移。
这需要生成一个代表当前数据库状态的初始迁移。
通过确保这个初始迁移不会尝试重新创建已有的表,开发者就可以继续使用标准的 code-first 工作流。

#### EF Core Backlog

与 Entity Framework 6 不同,EF Core 原生不支持从现有数据库更新已生成的模型。
虽然这个功能自 2014 年起就在 backlog 中,但至今仍未实现。
尽管该 issue 有人跟踪并偶尔有更新,但无法保证这一功能何时或是否会被加入核心工具。
因此,开发者无法仅靠标准 EF Core 命令简单地刷新 context 来获取新的列或表。

#### 重新生成 vs. 手动映射

在没有原生更新支持的情况下,开发者通常在两种手动工作流之间选择:

1. **重新生成(Regeneration)**:这种策略把生成的代码视为不可变的,每当数据库 schema 变化时就重新生成整个 `DbContext` 及其实体。
   这能确保模型始终与数据库同步,但要求不对生成的文件做任何手动定制,因为它们会被覆盖。
2. **手动映射(Manual Mapping)**:在这种方法中,开发者手动更新 C# 模型和 Fluent API 配置。
   这提供了最高程度的控制,可以得到一个更精炼的领域模型,其命名约定或结构可以与底层数据库不同。

#### 第三方解决方案

对于数据库由外部团队或数据库管理员管理的复杂场景,第三方工具可以弥补这一空缺。
正如 Microsoft Learn 中关于 EF Core 工具和扩展的文档所述,LLBL Gen Pro 和 DevArt Entity Developer 等解决方案充当数据库设计器,可以从现有 schema 拉取更新并输出兼容 Entity Framework Core 的代码。
这些是付费方案,但提供了 EF Core 原生工具目前缺失的同步功能。

## 4. Regenerating every time: a demo

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/) · 4:54

### 总结

本课演示在 Database First 方法中维护 Entity Framework Core 模型的重新生成策略。
它借助 C# partial 类来解决这种方法的主要缺点,即重新 scaffold 时会丢失手动修改的代码。
通过把生成的代码和手动扩展分离到不同的文件中,开发者可以确保自定义逻辑(例如计算属性)在数据库 schema 变化、模型被完整重新生成时依然保留。

### 核心概念

- **重新生成策略(Regeneration Strategy)**:每当数据库 schema 变化时,完整替换 context 和实体。
- **Partial 类(Partial Classes)**:一种 C# 特性,允许把一个类的定义拆分到多个文件中。
- **扩展实体(Extended Entities)**:一种把对生成实体的手动定制存放在单独文件中的模式,以防止它们被覆盖。
- **Scaffold-DbContext**:用于让 EF Core 模型与现有数据库同步的 CLI 命令。
- **重新生成的局限(Limitations of Regeneration)**:难以实现与数据库 schema 不同的自定义命名约定或复杂映射,因为这些会在重新 scaffold 时被重置。

### 课程笔记

重新生成策略是指每当数据库 schema 变化时,完整替换 scaffold 出的 `DbContext` 和实体类。
这种方法的主要风险在于,对生成文件所做的任何手动修改都会在重新 scaffold 的过程中丢失。

```csharp
// Program.cs initial state
using Dometrain.EFCore.DatabaseFirst.Context;

Console.WriteLine("Hello, World!");

var context = new AdventureWorksContext();

var query = context.Customers
    .Where(c => c.FirstName.StartsWith("C"))
    .Select(c => new { c.FirstName, c.LastName, c.SalesOrderHeaders.Count });

foreach (var customer in query)
{
    Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Count}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/?t=10)

为了防止丢失,这里使用 C# partial 类。
由于 scaffold 出的实体被声明为 `public partial class`,它们的实现可以拆分到多个文件中。
所有部分都必须位于同一个命名空间中,并遵守标准的 C# 规则,例如只能继承自一个基类,且所有成员必须唯一。

```csharp
namespace Dometrain.EFCore.DatabaseFirst.Entities;

/// <summary>
/// Customer information.
/// </summary>
public partial class Customer
{
    /// <summary>
    /// Primary key for Customer records.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// 0 = The data in FirstName and LastName are stored in western style
    /// </summary>
    public bool NameStyle { get; set; }

    /// <summary>
    /// A courtesy title. For example, Mr. or Ms.
    /// </summary>
    public string? Title { get; set; }
    
    // ... other generated properties
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/?t=70)

通过创建一个单独的文件夹(例如 `ExtendedEntities`)并为同一个实体定义第二个 partial 类文件,开发者可以添加在重新生成后依然保留的自定义属性或方法。
这个文件必须使用与生成的类相同的命名空间。

```csharp
namespace Dometrain.EFCore.DatabaseFirst.Entities;

public partial class Customer
{
    public string FullName => $"{FirstName} {LastName}";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/?t=130)

```text
namespace Dometrain.EFCore.DatabaseFirst.Entities
public partial class Customer
  |
  +-- generated file  (replaced by Scaffold-DbContext)
  |     CustomerId, NameStyle, Title, ...
  |
  +-- ExtendedEntities/ file  (manual, kept)
        FullName => $"{FirstName} {LastName}"
```

一旦把手动逻辑移到 partial 类中,就可以更新应用代码,直接使用这些扩展属性,而无需在每个查询中都做投影。

```csharp
// Updated Program.cs using the FullName property
using Dometrain.EFCore.DatabaseFirst.Context;

Console.WriteLine("Hello, World!");

var context = new AdventureWorksContext();

var query = context.Customers
    .Where(c => c.FirstName.StartsWith("C"));

foreach (var customer in query)
{
    Console.WriteLine($"{customer.FullName}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/?t=175)

当数据库发生变化时(例如新增一个 `OrderCount` 列),会使用相同的参数再次执行 `Scaffold-DbContext` 命令。
EF Core 会替换生成的文件,把新的数据库列合并到实体类中。
由于 `FullName` 属性位于单独的文件中,它不会受到影响,而生成的文件则会更新出新的属性。

```csharp
/// <summary>
/// First name of the person.
/// </summary>
public string FirstName { get; set; } = null!;

/// <summary>
/// Last name of the person.
/// </summary>
public string LastName { get; set; } = null!;

public int? OrderCount { get; set; }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/regenerating-every-time-a-demo-54130341/?t=265)

虽然这种策略在添加逻辑方面很有效,但它存在局限。
它不容易支持重命名属性、更改导航名称或映射到不同的类型,因为每次重新生成模型时这些更改都会被覆盖。

## 5. Keeping in sync manually: a demo

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/) · 4:58

### 总结

Entity Framework Core 中的手动映射提供了一种把应用的领域模型与数据库 schema 解耦的方式,允许使用以业务为中心的命名和结构定制。
尽管这种方法要求每当数据库 schema 变化时都进行手动更新,但与自动重新生成相比,它提供了更高的灵活性。
本课涵盖重构 scaffold 出的类、更新导航属性,以及在数据库新增列时手动同步模型。

### 核心概念

- **领域驱动的重构(Domain-Driven Refactoring)**:把以数据库为中心的名称(例如 SalesOrderHeader)重命名为以业务为中心的名称(例如 Order)。
- **导航属性同步(Navigation Property Synchronization)**:确保关系被更新,以匹配重命名后的实体。
- **手动 Schema 更新(Manual Schema Updates)**:使用 SQL Compare 等工具识别数据库变更,并手动将其应用到 EF Core 模型。
- **映射定制(Mapping Customization)**:在 OnModelCreating 中使用 HasColumnName 和 ToTable,保持重命名后的 C# 属性与其对应数据库对象之间的关联。
- **灵活性与自动化的权衡(Flexibility vs. Automation Trade-off)**:选择手动控制,而放弃自动 scaffolding 的便利。

### 课程笔记

手动映射是一种很有价值的方法,因为它让开发者能够摆脱初始 scaffolding 所提供的僵化结构。
类不必完全保持生成时的样子,而是可以被重构以更好地契合应用的领域。
例如,一个最初名为 `SalesOrderHeader` 的类可以重命名为 `Order`,以与业务术语保持一致。

```csharp
namespace Dometrain.EFCore.DatabaseFirst.Entities;

/// <summary>
/// General sales order information.
/// </summary>
public partial class Order
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public int SalesOrderId { get; set; }

    /// <summary>
    /// Incremental number to track changes to the sales order over time.
    /// </summary>
    public byte RevisionNumber { get; set; }

    /// <summary>
    /// Dates the sales order was created.
    /// </summary>
    public DateTime OrderDate { get; set; }
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/?t=25)

同样,`SalesOrderDetail` 可以重构为 `OrderLine`。
做出这些更改时,导航属性也必须随之更新。
例如,`Order` 类中的明细集合应重命名为 `OrderLines`,以反映新的实体名称。

```text
scaffolded name        refactored name    navigation in Order
--------------------   ----------------   -------------------
SalesOrderHeader  -->  Order
SalesOrderDetail  -->  OrderLine          OrderLines
```

在 `Customer` 实体中,可以识别出多个与人名相关的字段,例如 `Title`、`FirstName`、`MiddleName`、`LastName` 和 `Suffix`。
在 C# 中把它们组合成一个内聚的结构,就可以加入自定义逻辑,即使它们在数据库中仍然是独立的列。

```csharp
public partial class Customer
{
    public int CustomerId { get; set; }
    public bool NameStyle { get; set; }
    public string? Title { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string? Suffix { get; set; }
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/?t=100)

实体重构完成后,必须更新 `DbContext`。
`DbSet` 属性应重命名以匹配新的类名,同时仍然指向正确的数据库表。

```csharp
public partial class AdventureWorksContext : DbContext
{
    public virtual DbSet<OrderLine> SalesOrderDetails { get; set; }
    public virtual DbSet<Order> SalesOrderHeaders { get; set; }
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/?t=130)

这种方法的主要缺点是,模型再也无法在不丢失这些定制的情况下从数据库自动重新生成。
开发者必须手动跟踪数据库 schema 的变化。
Redgate SQL Compare 等工具可以通过识别开发和生产 schema 之间的差异来提供帮助。

当数据库中新增一个列(例如 `TotalOrderAmount`)时,必须手动将其添加到对应的 C# 实体中。

```csharp
public partial class Customer
{
    // ...
    public int? OrderCount { get; set; }
    public int? TotalOrderAmount { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
    public virtual ICollection<Order> SalesOrderHeaders { get; set; } = new List<Order>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/?t=220)

最后,必须在 `DbContext` 的 `OnModelCreating` 方法中映射这个新属性。
如果代码中的属性名被改得与数据库列名不同,就需要使用 `HasColumnName` 构建器方法来确保映射保持正确。

```csharp
modelBuilder.Entity<Customer>(entity =>
{
    // ...
    entity.Property(e => e.FirstName)
        .HasMaxLength(50)
        .HasComment("First name of the person.");
    entity.Property(e => e.LastName)
        .HasMaxLength(50)
        .HasComment("Last name of the person.");
    entity.Property(e => e.OrderCount).HasDefaultValueSql("((0))");
    entity.Property(e => e.TotalOrderAmount).HasDefaultValueSql("((0))");
    // ...
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/keeping-in-sync-manually-a-demo-54130342/?t=265)

这种手动方法提供了清理映射代码、把配置移到单独的 `IEntityTypeConfiguration` 类中的灵活性,从而得到一个比数据库 schema 更贴近业务领域的模型。

## 6. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130343/) · 0:07

### 总结

本课回顾 Entity Framework Core 中的 "Database First" 方法,重点是把现有数据库逆向工程为 .NET 模型的过程。
内容涵盖使用 scaffolding 工具生成 DbContext 和实体类、数据库视图的映射,以及 partial 类在用自定义业务逻辑扩展生成代码、又不必担心未来重新 scaffold 时丢失数据方面的关键作用。

### 核心概念

- 使用 CLI 工具从现有数据库 scaffold 模型。
- 通过 Fluent API 配置生成的实体和视图。
- 使用 partial 类安全地扩展自动生成的实体。
- 在 scaffold 出的 context 中管理连接字符串与安全性。

### 课程笔记

Database First 工作流让开发者能够从现有数据库 schema 生成完整的 EF Core 模型。
这是通过 scaffolding 实现的,它会创建一个 DbContext 和若干实体类,用来表示数据库中的表、列和关系。

#### Scaffold 出的 Context

生成的 DbContext(例如 `AdventureWorksContext`)同时包含对应表和视图的 `DbSet` 属性。
它还在 `OnModelCreating` 中包含 Fluent API 配置,以确保 C# 模型正确反映数据库的约束和 schema。

```csharp
public partial class AdventureWorksContext : DbContext
{
    public AdventureWorksContext() { }

    public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
        : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<VGetAllCategory> VGetAllCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=AdventureWorksLT2022; User Id=sa; Password=MySaPassword123; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer_CustomerID");
            entity.ToTable("Customer", "SalesLT");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130343/?t=0)

#### 用 Partial 类扩展实体

EF Core scaffolding 的一个关键优势是生成的类都被标记为 `partial`。
这让开发者可以在单独的文件中为实体扩展自定义属性或方法。
即使数据库 schema 发生变化、模型被重新 scaffold,这些扩展也会被保留。

例如,可以扩展 `Customer` 实体,加入一个 `FullName` 属性:

```csharp
namespace Dometrain.EFCore.DatabaseFirst.Entities;

public partial class Customer
{
    public string FullName => $"{FirstName} {LastName}";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130343/?t=0)

这段自定义逻辑随后可以在整个应用中使用,如下面这个过滤客户并打印其全名的查询所示:

```csharp
var context = new AdventureWorksContext();

var query = context.Customers
    .Where(c => c.FirstName.StartsWith("C"));

foreach (var customer in query)
{
    Console.WriteLine($"{customer.FullName}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130343/?t=0)

# Transforming the DB schema with migrations

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 5 章
> 共 8 课 · 约 43:45
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Why do we need migrations?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/why-do-we-need-migrations-54129952/) | 2:50 | [↓](#1-why-do-we-need-migrations) |
| 2 | [The 2 migration philosophies](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-2-migration-philosophies-54129953/) | 6:00 | [↓](#2-the-2-migration-philosophies) |
| 3 | [Adding our first migration](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/) | 8:47 | [↓](#3-adding-our-first-migration) |
| 4 | [Adding migrations after changing the model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/) | 7:15 | [↓](#4-adding-migrations-after-changing-the-model) |
| 5 | [Executing Migrations from the CLI](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/) | 5:23 | [↓](#5-executing-migrations-from-the-cli) |
| 6 | [Dealing with an out-of-sync model](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dealing-with-an-out-of-sync-model-54129957/) | 6:11 | [↓](#6-dealing-with-an-out-of-sync-model) |
| 7 | [The proper way to execute migrations](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/) | 6:38 | [↓](#7-the-proper-way-to-execute-migrations) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129960/) | 0:41 | [↓](#8-section-recap) |

## 1. Why do we need migrations?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/why-do-we-need-migrations-54129952/) · 2:50

### 总结

本课解释了在专业软件开发中数据库迁移的必要性。
虽然删除并重建数据库架构对快速原型开发和学习很有用,但它不适用于数据持久性至关重要的生产环境。
迁移提供了一种结构化的方式,让数据库架构从一个版本演进到另一个版本(处理诸如新增表或修改列之类的变更),而不会丢失现有数据。

### 核心概念

- EnsureDeleted() 和 EnsureCreated() 的局限性。
- 数据持久性与架构演进。
- 软件版本的生命周期(V1 到 V2)。
- 并行开发与架构变更。
- 迁移工具和脚本在保留数据方面的作用。

### 课程笔记

到目前为止,应用程序每次启动时都会删除并重建数据库架构。
这种做法是在 Program.cs 中使用 context.Database.EnsureDeleted() 和 context.Database.EnsureCreated() 实现的。

```csharp
// DIRTY HACK, we WILL come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
context.Database.EnsureDeleted();
context.Database.EnsureCreated();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/why-do-we-need-migrations-54129952/?t=7)

虽然这是一个有用的教学工具,可以用来观察对 DbContext 或实体映射的修改如何影响架构,但它并不适合真实世界的开发。
数据库的用途是长期存储数据,删除它们会导致数据全部丢失。

在典型的软件开发生命周期中,多名开发者会并行开发功能。
随着功能在分支中开发并合并到主代码库,其中许多变更都需要更新数据库架构。
例如,产品从版本 1 升级到版本 2 可能涉及:
- 新增或删除表。
- 新增或移除列。
- 更改数据类型。

如果版本 1 的数据库是"形状 A",而版本 2 需要"形状 B",就必须有一种方式在保留所有现有数据的同时完成架构的转换。
这正是迁移的主要目的。
迁移使用数据库脚本或专门的工具来应用增量变更,确保数据库随应用代码一起演进,同时不损害所存储信息的完整性。

```text
  V1 database            migrations             V2 database
+-------------+   (scripts / tools,      +-------------+
|   Shape A   | ----- incremental -----> |   Shape B   |
|   + data    |       changes)           |   + data    |
+-------------+                          +-------------+
```

## 2. The 2 migration philosophies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-2-migration-philosophies-54129953/) · 6:00

### 总结

数据库架构的演进主要通过两种不同的方法来管理:基于快照的迁移和基于增量的迁移。
基于快照的工具会比较两个数据库的当前状态来生成同步脚本,而基于增量的工具(例如 Entity Framework Core Migrations)则把增量变更记录为一个个独立的脚本。
本课探讨这两种方法之间的技术权衡,并强调在版本控制中让数据库演进与应用代码保持同步的好处。

### 核心概念

- **基于快照的工具(Snapshot-based Tooling)**:计算两个数据库架构(例如生产环境与开发环境)之间的差异,以生成同步脚本。
- **基于增量的工具(Delta-based Tooling)**:随代码提交一起记录小的增量变更("mini delta")。
- **双向迁移(Two-Way Migrations)**:能够使用迁移脚本对数据库架构进行升级和降级。
- **版本控制集成(Version Control Integration)**:把迁移脚本存放在与应用代码相同的仓库和分支中,以确保二者同步。
- **EF Core Migrations**:Entity Framework Core 内置的、基于增量的迁移框架。

### 课程笔记

用于转换数据库架构的工具分为两大类:基于快照的和基于增量的。

#### 基于快照的方法

这种方法会对两个数据库架构拍摄快照(通常是生产数据库(形状 A)和开发数据库(形状 B 或其变体形状 B')),并计算它们之间的差异。
生成的脚本旨在让生产数据库与开发环境保持一致。

虽然有效,但这种方法在大型发布时可能会遇到困难。
生成的脚本可能变得非常庞大,难以审查。
此外,这些工具可能难以理解开发者的意图;例如,一个被重命名的列可能被解读为先删除再创建,如果不手动纠正,就可能导致数据丢失。
基于快照的工具(例如 Redgate 出品的那些)通常更受这样一些组织的青睐:数据库管理员(DBA)对数据库拥有独占控制权,不希望开发者直接修改架构。

```text
  Production DB        Development DB
  (Shape A)            (Shape B / B')
      |                     |
   snapshot              snapshot
      +---------+-----------+
                |
         calculate delta
                |
                v
       sync script -> Production DB
```

#### 基于增量的方法

基于增量的方法注重让变更保持小而增量。
每一个需要更新数据库的变更都会被记录为一个"mini delta",通常是一个小的 C# 或 SQL 脚本。
这些脚本通常被设计为双向的,既可以升级也可以降级数据库架构。
虽然降级并不总是必需的,但能够增量升级是一个显著的优势。

基于增量的方法的主要优势在于它与源代码管理的集成。
迁移脚本会被提交到与其所支持的功能代码相同的分支中。
这确保了数据库架构与应用逻辑保持同步。
当一个分支合并到主分支时,相应的数据库变更也会一起合并,从而防止各个环境失去同步。
其他流行的基于增量的工具包括 DBUp 和 Fluent Migrator。

```text
  feature branch                         main branch
+------------------------------+  merge  +------------------------------+
| feature code                 | ------> | feature code                 |
| mini delta (C# / SQL script) |         | mini delta (C# / SQL script) |
+------------------------------+         +------------------------------+

  Shape A  --- mini delta (upgrade) -->  Shape B
  Shape A  <-- mini delta (downgrade) -- Shape B
```

#### EF Core Migrations

Entity Framework Core 使用一种基于增量的迁移机制。
这对开发者来说非常高效,因为它免去了手写 SQL 脚本的需要。
相反,开发者修改 `DbContext` 和实体映射,然后基于这些修改生成迁移脚本。

这就形成了一种迭代式的工作流:开发者可以生成脚本、审查其准确性、在必要时调整映射,并重新生成脚本,直到它正确无误,然后再提交到仓库。
这种方法确保数据库变更作为标准拉取请求流程的一部分接受审查,从而保持较高的代码和架构质量。

## 3. Adding our first migration

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/) · 8:47

### 总结

要在 Entity Framework Core 中实现数据库迁移,项目首先必须包含 Microsoft.EntityFrameworkCore.Tools NuGet 包,并且开发机器上必须安装 dotnet-ef 全局工具。
执行 dotnet ef migrations add 命令会生成一组描述架构变更的 C# 类,其中包括:一个带有用于应用和撤销变更的 Up 和 Down 方法的迁移文件、一个包含该特定迁移元数据的 designer 文件,以及一个维护整个模型当前状态的模型快照。
EF Core 使用这些文件来确定当前 code-first 模型与现有数据库架构之间的差异,从而实现增量且可撤销的更新。

### 核心概念

- **工具要求(Tooling Requirements)**:`Microsoft.EntityFrameworkCore.Tools` 包和 `dotnet-ef` 全局 CLI 工具对于管理迁移是必不可少的。
- **迁移命令(Migration Command)**:`dotnet ef migrations add <Name>` 会创建转换数据库架构所需的代码。
- **Up 和 Down 方法(Up and Down Methods)**:`Up` 方法包含应用变更的逻辑(例如 `CreateTable`),而 `Down` 方法包含撤销这些变更的逻辑(例如 `DropTable`)。
- **模型快照(Model Snapshot)**:`MoviesContextModelSnapshot.cs` 跟踪最近一次迁移时模型的状态,作为今后架构比较的基准。
- **种子数据处理(Seed Data Handling)**:在模型配置中使用 `HasData` 定义的数据,会被自动转换为迁移 `Up` 方法中的 `InsertData` 操作。
- **空迁移(Empty Migrations)**:如果当前模型与快照之间没有检测到任何变更,EF Core 会生成一个 `Up` 和 `Down` 方法都为空的迁移。

### 课程笔记

#### 工具与设置

在创建迁移之前,必须先把 `Microsoft.EntityFrameworkCore.Tools` 包添加到项目中。
此外,还必须确保系统上已全局安装 `dotnet-ef` 工具。
这个工具提供了用于管理迁移、数据库更新和 `DbContext` 类型的命令行界面。

```powershell
$ dotnet tool install -g dotnet-ef
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=55)

#### 生成第一个迁移

要生成初始架构,请在终端中进入项目文件夹并执行 `migrations add` 命令。
这个命令会构建项目,检查 `DbContext` 及其配置(包括数据库引擎),然后生成迁移文件。

```powershell
$ dotnet-ef migrations add InitialSchema
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=100)

#### 分析生成的文件

EF Core 会在 `Migrations` 文件夹中生成三个主要文件:

```text
Migrations/
 +-- MoviesContextModelSnapshot.cs          model snapshot (current state)
 +-- migration class                        Up / Down
 +-- [Timestamp]_InitialSchema.Designer.cs  metadata + target model
```

1.  **模型快照(Model Snapshot)**:`MoviesContextModelSnapshot.cs` 表示 EF Core 所理解的数据库模型的当前状态。
    每次添加新的迁移时,它都会被更新。

```csharp
[DbContext(typeof(MoviesContext))]
partial class MoviesContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.0-rc.2.23480.1")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("Dometrain.EFCore.API.Models.Genre", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime2")
                    .HasColumnName("CreatedAt");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Genres");
            });

        modelBuilder.Entity("Dometrain.EFCore.API.Models.Movie", b =>
            {
                b.Property<int>("Identifier")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Identifier"));

                b.Property<int>("AgeRating")
                    .HasColumnType("int");

                b.Property<int>("MainGenreId")
                    .HasColumnType("int");

                b.Property<string>("ReleaseDate")
                    .IsRequired()
                    .HasColumnType("char(8)");

                b.Property<string>("Synopsis")
                    .HasColumnType("varchar(max)")
                    .HasColumnName("Plot");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnType("varchar");

                b.HasKey("Identifier");

                b.HasIndex("MainGenreId");

                b.ToTable("Pictures", (string)null);
            });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=160)

2.  **迁移类(Migration Class)**:这个文件包含 `Up` 和 `Down` 方法。
    `Up` 方法定义了要应用的架构变更,例如创建表和外键。

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(
        name: "Genres",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Genres", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Pictures",
        columns: table => new
        {
            Identifier = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            Title = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
            ReleaseDate = table.Column<string>(type: "char(8)", nullable: false),
            Plot = table.Column<string>(type: "varchar(max)", nullable: true),
            AgeRating = table.Column<int>(type: "int", nullable: false),
            MainGenreId = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Pictures", x => x.Identifier);
            table.ForeignKey(
                name: "FK_Pictures_Genres_MainGenreId",
                column: x => x.MainGenreId,
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=235)

3.  **Designer 文件(Designer File)**:`[Timestamp]_InitialSchema.Designer.cs` 包含该特定迁移的元数据和目标模型结构。

#### 处理种子数据

如果你在映射配置中使用了 `HasData`,EF Core 会在 `Up` 方法中包含 `migrationBuilder.InsertData` 调用。
这些数据会在应用迁移时执行一次。

```csharp
migrationBuilder.InsertData(
    table: "Genres",
    columns: new[] { "Id", "CreatedAt", "Name" },
    values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drama" });

migrationBuilder.InsertData(
    table: "Pictures",
    columns: new[] { "Identifier", "AgeRating", "MainGenreId", "ReleaseDate", "Plot" },
    values: new object[] { 1, 16, 1, new DateTime(1999, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fight Club" });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=310)

为了让迁移保持整洁,如果你更倾向于通过其他机制来处理数据播种,可以选择从映射文件中移除 `HasData`。

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

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Id)
            .HasForeignKey(movie => movie.MainGenreId);

        builder.OwnsOne(movie => movie.Director).ToTable("Pictures_Directors"); 
        builder.OwnsMany(movie => movie.Actors);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=325)

修改模型或映射之后,你可以删除已生成的迁移文件,然后再次运行 `add` 命令,重新生成一个干净的迁移。

```shell
$ dotnet-ef migrations add InitialSchema
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=385)

#### 撤销变更

`Down` 方法是 `Up` 方法的逆操作。
它定义了如何撤销该迁移,通常是删除在 `Up` 方法中创建的表或列。

```csharp
protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable(name: "Pictures_Actors");
    migrationBuilder.DropTable(name: "Pictures_Directors");
    migrationBuilder.DropTable(name: "Pictures");
    migrationBuilder.DropTable(name: "Genres");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=415)

#### 空迁移

如果在模型与当前快照相比没有任何变更时运行 `migrations add` 命令,EF Core 会生成一个空迁移。
这就确认了模型与快照是同步的。

```powershell
$ dotnet-ef migrations add Migration2
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-our-first-migration-54129954/?t=490)

## 4. Adding migrations after changing the model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/) · 7:15

### 总结

本课演示了在 Entity Framework Core 中修改领域模型之后如何更新数据库架构。
它解释了 EF Core 如何通过将当前模型与现有快照进行比较来生成迁移,并强调了手动审查和编辑生成的迁移以防止数据丢失的重要性,特别是在重命名属性或更改数据类型时。
通过使用 AlterColumn 和 RenameColumn 等方法,开发者可以确保在复杂的架构转换过程中保留数据,否则自动化工具可能会把这些转换解读为破坏性的先删除再重建操作。

### 核心概念

- **模型与快照比较(Model-to-Snapshot Comparison)**:EF Core 通过将当前的实体模型和映射与 `ModelSnapshot` 文件进行比较来生成迁移。
- **迁移生命周期(Migration Lifecycle)**:添加迁移时,EF Core 会生成迁移文件,然后更新 `ModelSnapshot` 以反映新的目标状态。
- **防止数据丢失(Data Loss Prevention)**:默认的迁移生成在处理重命名或类型变更时可能使用 `DropColumn` 和 `AddColumn`,这会销毁现有数据。
- **手动编辑迁移(Manual Migration Editing)**:开发者可以手动修改迁移文件中的 `Up` 和 `Down` 方法,改用 `RenameColumn` 和 `AlterColumn` 等非破坏性操作。
- **迁移原子性(Migration Atomicity)**:让迁移保持小而专注,可以使它们在自动生成未能体现开发者意图时更容易审查、测试和修复。

### 课程笔记

当模型发生变化时,Entity Framework Core 必须确定如何同步数据库架构。
例如,给 `Movie` 实体添加一个 `ImdbRating` 属性,就会造成代码与数据库之间的不一致。

```csharp
namespace Dometrain.EFCore.API.Models;

public class Movie
{
    public int Identifier { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
    public AgeRating AgeRating { get; set; }

    public int ImdbRating { get; set; }

    public Person Director { get; set; }
    public ICollection<Person> Actors { get; set; }

    public Genre Genre { get; set; }
    public int MainGenreId { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/?t=20)

为了同步这些变更,通过 .NET CLI 添加一个新的迁移:

```powershell
dotnet ef migrations add AddedImdbRating
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/?t=40)

EF Core 通过将 `DbContext` 及其实体的当前状态与 `MoviesContextModelSnapshot.cs` 文件进行比较来生成迁移。
生成的迁移在 `Up` 方法中包含一个 `AddColumn` 操作,并在 `Down` 方法中包含一个对应的 `DropColumn`。

```text
  DbContext + entities          MoviesContextModelSnapshot.cs
  (current model)               (previous state)
           |                              |
           +----------- compare ----------+
                          |
                          v
          AddedImdbRating migration
            Up:   AddColumn  ImdbRating
            Down: DropColumn ImdbRating
                          |
                          v
          ModelSnapshot updated to new target state
```

```csharp
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dometrain.EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedImdbRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImdbRating",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbRating",
                table: "Pictures");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/?t=70)

在复杂的变更中,EF Core 并不总能正确推断开发者的意图。
如果一个属性被重命名的同时其类型也被更改(例如把 `int ImdbRating` 改为 `decimal InternetRating`),EF Core 通常会把它解读为删除一列并新增另一列。
这会导致该列的数据全部丢失。

```csharp
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dometrain.EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToInternetRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbRating",
                table: "Pictures");

            migrationBuilder.AddColumn<decimal>(
                name: "InternetRating",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/?t=220)

为了防止数据丢失,必须手动编辑迁移文件。
迁移不应先删除再新增,而应使用 `AlterColumn` 来更改数据类型,并使用 `RenameColumn` 来更新名称。
这些操作会保留数据库中的现有数据,不过开发者在更改类型时应注意可能出现的截断。

```csharp
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dometrain.EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToInternetRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImdbRating",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.RenameColumn(
                name: "ImdbRating",
                table: "Pictures",
                newName: "InternetRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "InternetRating",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.RenameColumn(
                name: "InternetRating",
                table: "Pictures",
                newName: "ImdbRating");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/adding-migrations-after-changing-the-model-54129955/?t=430)

一旦迁移被手动修正,它就会保持修正后的状态,因为工具永远不会重新生成迁移文件。
让迁移保持小而增量是最佳实践,这可以限制潜在错误的范围,并简化手动修正。

## 5. Executing Migrations from the CLI

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/) · 5:23

### 总结

本课演示如何使用 .NET CLI 将 Entity Framework Core 迁移应用到数据库。
它解释了如何使用 `dotnet ef database update` 命令让数据库架构与当前迁移状态同步,并介绍了 `__EFMigrationsHistory` 表,EF Core 用它来跟踪已应用的迁移。
此外,本课还讲解了如何通过指定某个迁移的名称将数据库还原到该迁移,这会触发相关 Down 方法的执行,并相应地更新历史表。

### 核心概念

- **dotnet ef database update**:用于把所有待应用迁移应用到目标数据库的主要命令。
- **__EFMigrationsHistory 表(__EFMigrationsHistory Table)**:由 EF Core 在数据库中创建并维护的元数据表,用于记录哪些迁移已经执行过。
- **迁移 ID(Migration ID)**:每个迁移的唯一标识符,由时间戳和描述性名称组成。
- **定向更新(Targeted Updates)**:通过把迁移名称作为参数传入,将数据库架构向前或向后移动到某个特定迁移状态的能力。
- **执行日志(Execution Logging)**:监控 EF Core 在迁移过程中执行的 SQL 命令和内部检查(例如 `OBJECT_ID` 查找)。

### 课程笔记

要把迁移应用到数据库,请通过命令行使用 Entity Framework Core 工具。
确保你位于包含 `DbContext` 和迁移的项目文件夹中。

```powershell
cd .\Dometrain.EFCore.API\
dotnet ef database update
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/?t=55)

执行这个命令时,EF Core 会检查项目中的连接字符串,并找出哪些迁移尚未应用到目标数据库。

#### 迁移历史表

在第一次更新时,EF Core 会创建一个名为 `__EFMigrationsHistory` 的特殊表。
这个表用于跟踪应用到架构上的每一个迁移,防止同一个迁移被多次运行。

```sql
SELECT TOP (1000) [MigrationId]
      ,[ProductVersion]
  FROM [MoviesDB].[dbo].[__EFMigrationsHistory]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/?t=80)

该表包含 `MigrationId`(时间戳和名称)以及应用该迁移时所用的 EF Core 的 `ProductVersion`。
EF Core 使用这些信息来确定数据库的当前状态。

#### 执行流程

运行更新时,EF Core 会按以下特定顺序执行操作:

1.  **连接(Connection)**:它会打开到配置中指定的数据库的连接。
2.  **历史检查(History Check)**:它会检查 `__EFMigrationsHistory` 表是否存在。
3.  **建表(Table Creation)**:如果历史表不存在,EF Core 会自动创建它。
4.  **应用迁移(Applying Migrations)**:它会按时间顺序应用每一个待应用的迁移。
    对于初始架构,EF Core 会执行迁移 `Up` 方法中定义的 `CREATE TABLE` 命令。

```sql
CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/?t=160)

5.  **记录进度(Recording Progress)**:成功执行一个迁移之后,EF Core 会向历史表中插入一条记录。

```sql
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230813224156_InitialSchema', N'7.0.10');
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/?t=190)

这个过程会对每一个待应用的迁移重复进行,包括那些新增列或更改数据类型的迁移。

```text
 dotnet ef database update
          |
          v
 1. open connection
          |
          v
 2. __EFMigrationsHistory exists? --no--> 3. create it
          |                                     |
          +<------------------------------------+
          v
 4. apply next pending migration (Up)  <----------------+
          |                                             |
          v                                             |
 5. INSERT INTO [__EFMigrationsHistory] --more pending--+
```

#### 还原迁移

EF Core 允许通过指定一个目标迁移名称,把数据库架构回滚到之前的状态。
这会触发所有比目标更新的迁移的 `Down` 方法。

```powershell
dotnet ef database update Migration2
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/?t=250)

在还原过程中,EF Core 会在历史表中找出比目标更新的迁移,并按时间倒序执行它们的 `Down` 步骤。
最后,它会从 `__EFMigrationsHistory` 表中删除对应的记录。

```shell
DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20230813232446_AddedImdbRating';
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/executing-migrations-from-the-cli-54129956/?t=295)

切勿手动删除 `__EFMigrationsHistory` 表,这一点至关重要,因为 EF Core 依赖它来管理数据库架构的状态。

## 6. Dealing with an out-of-sync model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dealing-with-an-out-of-sync-model-54129957/) · 6:11

### 总结

本课讨论在数据库架构与 Entity Framework Core 模型不同步时运行应用程序的风险。
它探讨了如何从破坏性的架构重建过渡到使用 `MigrateAsync` 进行自动更新,同时指出了授予应用程序修改架构权限在安全和架构设计上的缺点。
最终,它演示了一种更安全的模式:在启动时检查是否有待应用的迁移,如果数据库没有完全更新就抛出异常,从而确保数据完整性并强制执行正确的部署流程。

### 核心概念

- **运行时架构不匹配(Runtime Schema Mismatch)**:当 EF Core 模型期望的列(例如 `InternetRating`)在物理数据库中并不存在时,应用程序会在查询期间抛出运行时异常。
- **自动迁移(Automatic Migrations)**:可以使用 `context.Database.MigrateAsync()` 方法在应用程序启动时自动将所有待应用的迁移应用到数据库。
- **最小权限原则(Principle of Least Privilege)**:授予应用程序的数据库用户修改架构(DDL)所需的权限是一种安全风险。
  应用程序通常只应拥有 DML 权限(查询和修改数据)。
- **待应用迁移校验(Pending Migration Validation)**:使用 `context.Database.GetPendingMigrationsAsync()` 可以让应用程序在不尝试修改数据库本身的情况下验证架构兼容性。
- **迁移历史(Migration History)**:EF Core 在 `__EFMigrationsHistory` 表中跟踪已应用的迁移,以确定需要执行哪些脚本。

### 课程笔记

当数据库处于不一致的状态时(例如四个迁移中只应用了两个),应用程序可能会成功启动,但在数据访问期间失败。
查询一个与模型相符的表(例如 `Genres`)可能没问题,但查询一个缺少列的表(例如缺少 `InternetRating` 列的 `Movies`)就会导致运行时异常。

要解决这个问题,第一步是移除开发阶段使用 `EnsureDeleted()` 和 `EnsureCreated()` 删除并重建数据库架构的"dirty hack"。

```csharp
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add a DbContext here
builder.Services.AddDbContext<MoviesContext>();

var app = builder.Build();

// DIRTY HACK, we WILL come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
// context.Database.EnsureDeleted();
// context.Database.EnsureCreated();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dealing-with-an-out-of-sync-model-54129957/?t=40)

你可以不再重建架构,而是使用 `context.Database.MigrateAsync()` 把数据库升级到迁移中定义的最新版本。

```csharp
builder.Services.AddDbContext<MoviesContext>();

var app = builder.Build();

// DIRTY HACK, we WILL come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
await context.Database.MigrateAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dealing-with-an-out-of-sync-model-54129957/?t=115)

虽然 `MigrateAsync()` 可以工作,但由于**最小权限原则(Principle of Least Privilege)**,通常不建议在生产环境中使用它。
这种做法要求应用程序的数据库用户拥有修改架构的权限,而这些权限对于查询或创建数据之类的常规操作来说是不必要的。
在专业的工作流程中,架构变更应该由部署流水线或专用脚本来处理,而不是由应用程序本身在运行时处理。

EF Core 使用一个特定的系统表来跟踪这些迁移:

```sql
SELECT TOP (1000) [MigrationId]
      ,[ProductVersion]
  FROM [MoviesDB].[dbo].[__EFMigrationsHistory]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dealing-with-an-out-of-sync-model-54129957/?t=130)

一种更稳健的模式是检查是否有待应用的迁移,如果数据库没有完全迁移,就阻止应用程序启动。
这可以避免数据损坏和意外的运行时错误。
你可以使用 `GetPendingMigrationsAsync()` 获取一个列表,其中是存在于代码中但尚未应用到数据库的迁移。

```csharp
// Add a DbContext here
builder.Services.AddDbContext<MoviesContext>();

var app = builder.Build();

// DIRTY HACK, we WILL come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
if (pendingMigrations.Any())
    throw new Exception("Database is not fully migrated for MoviesContext.");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/dealing-with-an-out-of-sync-model-54129957/?t=355)

实现这项检查之后,如果数据库被回滚到较早的迁移(例如使用 `dotnet ef database update Migration2`),应用程序会在启动时立即抛出异常。
一旦通过终端或部署脚本应用了这些迁移,应用程序就会检测到 `pendingMigrations.Count()` 为零,并正常启动。

```text
 app start
     |
     v
 GetPendingMigrationsAsync()
     |
     +-- pendingMigrations.Any() --> throw Exception(
     |                               "Database is not fully migrated
     |                                for MoviesContext.")
     |
     +-- none pending ------------> start normally
```

## 7. The proper way to execute migrations

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/) · 6:38

### 总结

本课探讨在应用程序运行时环境之外执行 Entity Framework Core 迁移的专业策略。
它涵盖三种主要方法:在 CI/CD 流水线中使用带显式参数的 .NET EF CLI、生成幂等 SQL 脚本供 DBA 手动审查和部署,以及创建自包含的迁移包(migration bundle)。
其中,打包被强调为适用于现代开发生命周期的最稳健方法,因为它会产出一个一致的可执行制品,可以在不同环境(例如开发、预发布和生产)之间逐级推进,从而确保在所有地方应用的都是完全相同的迁移逻辑。

### 核心概念

*   面向 CI/CD 流水线的外部迁移执行。
*   针对连接字符串、上下文和项目的 CLI 参数覆盖。
*   用于手动审查和部署的 SQL 脚本生成。
*   作为自包含、与环境无关的部署制品的迁移包。
*   通过制品推进实现环境一致性。

### 课程笔记

在应用程序运行时内部执行迁移,通常不适合生产环境。
相反,迁移应该在外部执行,通常作为部署脚本或构建流水线的一部分。
实现这一点有三种主要方法。

#### 方法 1:.NET EF CLI 工具

应用迁移最直接的方式是使用 .NET EF CLI 工具。
虽然这些工具默认使用项目中的配置(例如 `OnConfiguring` 中的连接字符串),但它们支持若干参数,以便在不同的环境中执行,例如在可以获取源代码的构建流水线中。

```powershell
$ cd .\Dometrain.EFCore.API\
$ dotnet-ef database update --help
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/?t=30)

通过 help 命令,我们可以看到可用于覆盖连接字符串、目标迁移和特定 `DbContext` 实例的选项:

```powershell
<MIGRATION>  The target migration. If '0', all migrations will be reverted. Defaults to the last migration.

Options:
  --connection <CONNECTION>              The connection string to the database. Defaults to the one specified in AddDbContext or OnConfiguring.
  -c|--context <DBCONTEXT>               The DbContext to use.
  -p|--project <PROJECT>                 The project to use. Defaults to the current working directory.
  -s|--startup-project <PROJECT>         The startup project to use. Defaults to the current working directory.
  --framework <FRAMEWORK>                The target framework. Defaults to the first one in the project.
  --configuration <CONFIGURATION>        The configuration to use.
  --runtime <RUNTIME_IDENTIFIER>         The runtime to use.
  --msbuildprojectextensionspath <PATH>  The MSBuild project extensions path. Defaults to "obj".
  --no-build                             Don't build the project. Intended to be used when the build is up-to-date.
  -h|--help                              Show help information
  -v|--verbose                           Show verbose output.
  --no-color                             Don't colorize output.
  --prefix-output                        Prefix output with level.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/?t=55)

#### 方法 2:生成 SQL 脚本

在数据库管理员(DBA)必须先审查变更、然后才能应用它们的环境中,生成 SQL 脚本很有用。
这种方法让你可以为所有迁移或某个特定范围的迁移生成脚本。

要为所有迁移生成脚本,请使用以下命令:

```powershell
$ dotnet-ef migrations script > script.txt
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/?t=160)

要为某个特定范围的迁移生成脚本(例如从 `Migration2` 开始),请指定迁移名称:

```powershell
$ dotnet-ef migrations script Migration2 > script.txt
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/?t=145)

生成的 SQL 包含处理架构变更的逻辑,例如修改列类型和删除约束:

```sql
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [c].[object_id] = [d].[parent_object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pictures]') AND [c].[name] = N'ImdbRating');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Pictures] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Pictures] ALTER COLUMN [ImdbRating] decimal(18,2) NOT NULL;
ALTER TABLE [Pictures] ADD DEFAULT 0.0 FOR [ImdbRating];
GO
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/?t=220)

它还会使用 `sp_rename` 之类的系统存储过程来处理列重命名,并更新 `__EFMigrationsHistory` 表以跟踪已应用的迁移:

```sql
ALTER TABLE [Pictures] ADD DEFAULT 0.0 FOR [ImdbRating];
GO

EXEC sp_rename N'[Pictures].[ImdbRating]', N'InternetRating', 'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230813232734_ChangedToInternetRating', N'7.0.10');
GO

COMMIT;
GO
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-proper-way-to-execute-migrations-54129958/?t=235)

#### 方法 3:迁移包

对于现代部署流水线(尤其是使用 Docker 容器的流水线)来说,最稳健的方法是使用迁移包。
通过运行 `dotnet ef migrations bundle`,EF Core 会生成一个自包含的可执行文件(例如 `efbundle.exe`),其中包含更新数据库所需的全部逻辑和迁移。

这个迁移包充当一个一致的制品,可以从一个环境交付到另一个环境。
执行时,可以把它指向某个特定的连接字符串,从而确保在开发、预发布和生产环境中应用完全相同的迁移逻辑,而无需在目标机器上具备源代码或 .NET SDK。

```text
 dotnet ef migrations bundle
            |
            v
       efbundle.exe   (same artifact everywhere)
            |
   +--------+-----------+------------------+
   v                    v                  v
 development         staging           production
 (connection string) (connection string) (connection string)
```

## 8. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129960/) · 0:41

### 总结

本课为 Entity Framework Core 迁移这一章作结,回顾了从初始设置到部署的端到端过程。
它强调了理解 EF Core 内部迁移逻辑的重要性,并指出了安全执行架构变更的最佳实践。
主要收获包括:从开发阶段的迁移命令过渡到稳健的部署策略,将修改架构的权限与常规的应用程序数据访问分离开来。

### 核心概念

- 迁移的前提条件与初始化
- EF Core 推断架构变更的内部机制
- 进程外的迁移执行策略
- 通过对数据库用户实施最小权限原则来保障安全

### 课程笔记

关于 Entity Framework Core 迁移的这一章,奠定了有效管理数据库架构变更所需的基础知识。
它从在 .NET 项目中启用迁移的前提条件开始,接着讲解了生成和应用这些迁移的过程。

这个过程的一个核心部分,是理解 Entity Framework Core 如何推断架构变更。
通过将实体模型的当前状态与模型快照以及之前的迁移进行比较,框架会确定将数据库转换到期望状态所需的 SQL 命令。

对于生产环境,建议把迁移的执行从应用程序的启动逻辑中移出。
虽然开发工作流通常依赖自动迁移,但生产系统应该使用部署流水线。
这种策略允许一个拥有特权的数据库用户在部署阶段执行架构变更(数据定义语言,即 DDL)。

架构更新之后,应用程序就可以使用一个无特权的数据库用户来运行。
这个用户仅限于与数据相关的操作(数据操作语言,即 DML),例如读写记录,而没有修改数据库结构的权限。
这种关注点分离通过遵循最小权限原则,显著增强了应用程序的安全态势。
为了确保数据库处于正确的状态,应用程序可以在启动时实现一项检查,在继续运行之前验证没有待应用的迁移。

```text
 deployment phase                     runtime
+-----------------------------+     +-----------------------------+
| privileged DB user          |     | unprivileged DB user        |
| executes schema changes     | --> | data operations only        |
| (DDL)                       |     | (DML: read / write records) |
+-----------------------------+     | startup check: no pending   |
                                    | migrations                  |
                                    +-----------------------------+
```

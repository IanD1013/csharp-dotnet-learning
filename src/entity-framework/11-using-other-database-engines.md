# Using other database engines

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 11 章
> 共 5 课 · 约 34:32
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [EF Core isn't just for SQL server](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/ef-core-isnt-just-for-sql-server-54130410/) | 0:59 | [↓](#1-ef-core-isnt-just-for-sql-server) |
| 2 | [Using another RDBMS's with EFCore](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/) | 10:18 | [↓](#2-using-another-rdbmss-with-efcore) |
| 3 | [What is CosmosDB?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-is-cosmosdb-54130412/) | 6:36 | [↓](#3-what-is-cosmosdb) |
| 4 | [Switching our system to CosmosDB](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/) | 15:30 | [↓](#4-switching-our-system-to-cosmosdb) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130414/) | 1:09 | [↓](#5-section-recap) |

## 1. EF Core isn't just for SQL server

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/ef-core-isnt-just-for-sql-server-54130410/) · 0:59

### 总结

Entity Framework Core 是一个与提供程序无关的 ORM,除 SQL Server 之外,它还支持多种数据库引擎,包括 MySQL、PostgreSQL 和 SQLite 等关系型数据库,以及 Azure Cosmos DB 等非关系型文档数据库。
虽然核心 API 保持一致,但切换提供程序需要安装特定的 NuGet 包,并且可能带来行为上的差异,尤其是在关系型与 NoSQL 存储模型之间切换时。

### 核心概念

- **数据库提供程序架构**:EF Core 使用一种基于插件的系统,由特定的提供程序负责转换为不同的数据库方言。
- **关系型支持**:完整支持 MySQL、PostgreSQL、Oracle 和 SQLite。
- **NoSQL 支持**:支持 Azure Cosmos DB 等文档数据库。
- **行为一致性**:在各关系型提供程序之间,高层 API 保持相似,但在使用 NoSQL 提供程序时会发生显著变化。

### 课程笔记

Entity Framework Core 被设计为与数据库无关,这意味着它并不局限于只与 SQL Server 配合使用。
它支持范围广泛的数据库提供程序,这些提供程序以独立的 NuGet 包形式提供。

#### 支持的数据库提供程序

EF Core 支持多种多样的数据库引擎。
其中既包括传统的关系型数据库,也包括现代的 NoSQL 方案:

- **关系型数据库**:MySQL、PostgreSQL、Oracle 和 SQLite。
- **文档数据库**:Azure Cosmos DB。

#### 在引擎之间切换

在关系型数据库之间切换时,例如从 SQL Server 迁移到 MySQL,过渡通常很直接,因为两者都遵循关系型原则。
开发者通常只需要替换特定于提供程序的 NuGet 包,并更新 `DbContext` 的配置。

然而,切换到 Azure Cosmos DB 这样的文档数据库会带来显著的行为变化。
由于 Cosmos DB 是 NoSQL 文档存储,而不是基于表的关系型系统,EF Core 的内部行为以及它将实体映射到存储的方式都会有所不同。
例如,与关系型环境相比,连接(join)和外键约束等概念在面向文档的环境中的处理方式有所不同。

## 2. Using another RDBMS's with EFCore

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/) · 10:18

### 总结

把一个 Entity Framework Core 应用从 SQL Server 切换到 MariaDB 等其他 RDBMS,需要更换数据库提供程序的 NuGet 包、更新连接字符串,并调整 DbContext 的配置。
虽然 EF Core 抽象了大部分数据库逻辑,但开发者必须考虑数据类型(例如 varchar(max) 与显式长度)和 SQL 语法方面因提供程序而异的差异。
管理多个数据库引擎通常需要分版本的迁移以及针对特定环境的实体映射。

### 核心概念

- 数据库提供程序包(适用于 MySQL/MariaDB 的 Pomelo)
- 连接字符串语法的差异
- 适用于 MySQL/MariaDB 的 ServerVersion.AutoDetect
- 特定于提供程序的迁移生成
- 数据类型映射的差异(例如 varchar(max))
- 支持多引擎的策略

### 课程笔记

要把一个现有的 EF Core 应用从 SQL Server 切换到 MariaDB,第一步是替换数据库提供程序。
MariaDB 是 MySQL 的一个开源版本,在大多数场景下行为与之相似。

#### 1. 数据库提供程序与配置

必须卸载 `Microsoft.EntityFrameworkCore.SqlServer` 包,并替换为一个兼容 MySQL 的提供程序。
对于 EF Core 来说,最流行的选择是 `Pomelo.EntityFrameworkCore.MySql`。
此外,还应安装设计包 `Pomelo.EntityFrameworkCore.MySql.Design` 以支持迁移。

`appsettings.json` 中的连接字符串必须更新以符合 MariaDB 的要求,指定服务器、端口(默认 3306)、数据库名称、用户 ID 和密码。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MoviesContext": "Server=localhost;Port=3306;Database=MoviesDB;Uid=root;Pwd=MyRootPassword123;"
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.File" ],
    "MinimumLevel": {
      "Default": "Debug"
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "%TEMP%/Logs/dometrain-efcore-api.json",
          "rollingInterval": "Day",
          "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact"
        }
      }
    ]
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/?t=205)

在 `Program.cs` 中,`UseSqlServer` 调用被替换为 `UseMySql`。
与 SQL Server 提供程序不同,MySQL 提供程序需要一个 `ServerVersion` 参数。
使用 `ServerVersion.AutoDetect(connectionString)` 可以让应用在启动时查询数据库,以确定正确的协议版本。

```csharp
// Add the DbContext
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
        optionsBuilder
            .UseMySql(connectionString
                , ServerVersion.AutoDetect(connectionString)
                , sqlBuilder => sqlBuilder.MaxBatchSize(50))
            .LogTo(Console.WriteLine);
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/?t=265)

#### 2. 迁移与数据类型映射

切换提供程序时,应删除现有的迁移并重新生成,以确保生成的 SQL 与新引擎兼容。
使用命令 `dotnet ef migrations add MariaDBCreate` 创建一个全新的迁移。

在初始迁移或数据库更新期间,你可能会遇到与不兼容的数据类型相关的异常。
例如,SQL Server 的 `varchar(max)` 在 MariaDB 中并不存在。

```csharp
builder.Property(movie => movie.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(movie => movie.ReleaseDate)
            .HasColumnType("date")
            .HasConversion(new DateOnlyConverter());

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/?t=430)

要解决这个问题,必须调整映射,使用目标引擎支持的特定最大长度(例如 4096 个字符)。

```csharp
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .UseTphMappingStrategy()
            .HasQueryFilter(movie => movie.ReleaseDate > new DateOnly(1990,1,1))
            .HasKey(movie => movie.Identifier);

        builder
            .HasAlternateKey(movie => new { movie.Title, movie.ReleaseDate });

        builder.HasIndex(movie => movie.AgeRating)
            .IsDescending();

        builder.Property(movie => movie.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(movie => movie.ReleaseDate)
            .HasColumnType("date")
            .HasConversion(new DateOnlyConverter());

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar")
            .HasMaxLength(4096)
            .HasColumnName("Plot");

        builder.Property(movie => movie.MainGenreName)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Name)
            .HasForeignKey(movie => movie.MainGenreName);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/?t=505)

#### 3. 多引擎支持策略

如果一个应用需要同时支持多个数据库引擎,推荐的策略是为每个引擎维护单独的实体映射文件夹。
在应用启动期间,根据当前使用的连接加载相应的映射。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfiguration(new GenreMapping());
    modelBuilder.ApplyConfiguration(new MovieMapping());
    modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
    modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
    modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
    modelBuilder.ApplyConfiguration(new ActorMapping());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/?t=595)

配置完成后,EF Core 会生成特定于引擎的 SQL,例如在 MariaDB 中使用反引号(`)而不是方括号([])来包裹标识符。

```sql
SELECT `g`.`Id`, `g`.`ConcurrencyToken`, `g`.`CreatedAt`, `g`.`Deleted`, `g`.`Description`, `g`.`Name` 
FROM `Genres` AS `g` 
WHERE NOT (`g`.`Deleted`)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-another-rdbmss-with-efcore-54130411/?t=565)

## 3. What is CosmosDB?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-is-cosmosdb-54130412/) · 6:36

### 总结

Azure Cosmos DB 是一种全球分布式、多模型的数据库服务,它为传统关系型数据库提供了一种面向文档的替代方案。
关系型系统使用规范化把数据分散到多张表中(例如 Movies、Genres 和 Actors),以确保一致性并尽量减少存储,而 Cosmos DB 则把整个对象图以反规范化的 JSON 文档形式存储在集合中。
这种方式针对完整数据图的高性能检索进行了优化,并提供更出色的水平扩展和地理扩展能力。
不过,它需要转变建模策略,因为在请求单位(Request Unit,RU)计价模型下,跨多个文档查询可能会变得低效且昂贵。

### 核心概念

- **规范化与反规范化**:关系型数据库使用多张表和外键来尽量减少冗余,而文档数据库把相关数据一起存储在单个文档中。
- **集合与表**:在 Cosmos DB 中,数据存储在集合中,集合可以包含不同类型的文档,这与 SQL 表中刚性的行结构不同。
- **以 JSON 表示的对象图**:Cosmos DB 允许把整个对象层次结构存储为 JSON 文档,并可以直接查询这些文档。
- **水平扩展与地理扩展**:由于操作的范围限定在单个文档内,Cosmos DB 更容易分布到多个节点和全球多个区域。
- **请求单位(RU)**:操作的成本以 RU 计量,1 RU 大致相当于检索一个 1KB 的文档。
- **预配吞吐量与自动缩放吞吐量**:用户可以预配固定数量的吞吐量,也可以使用自动缩放来应对峰值,同时在低利用率期间降低成本。

### 课程笔记

#### 关系型建模与规范化

在传统关系型数据库中,一个电影应用的简单数据模型通常需要四张不同的表:Movie 表、Genre 表、Actor 表,以及一张多对多的中间表(MovieActor)。
Genre 表通过外键以一对多关系连接到 Movie 表,而中间表则实现电影与演员之间的多对多关系。

```text
Genre 1 ----< Movie 1 ----< MovieActor >---- 1 Actor
  (一对多,外键)          (多对多中间表)
```

这种建模方法称为规范化。
在过去,规范化至关重要,因为存储很昂贵,开发者的目标是让每条信息恰好只存储一次。
这种方式还能确保数据一致性;更新单条记录会自动更新所有引用该数据的地方。

#### 文档数据库的方式

Cosmos DB 作为一种文档数据库,运作方式有所不同。
它使用的不是表,而是"集合"。
主要区别在于,集合可以把整个对象图作为 JSON 文档来存储。
在电影的例子中,不需要把数据拆分到四张表里,电影、它的类型以及完整的演员列表可以作为单个文档存储在一个集合中。

```text
Collection
+------------------------------+
| Document                     |
|   Movie                      |
|   Genre                      |
|   Actors: [ ..., ..., ... ]  |
+------------------------------+
```

数据以存储时的形状被检索出来。
由于没有原生的方法能够在不造成显著性能损失的情况下以不同的形状查询数据,开发者常常会把同一份数据以多种形状存储,以支持不同的查询模式。
文档数据库针对在单个文档范围内执行的操作(创建、读取、更新、删除)进行了高度优化。
跨许多不同文档的查询是低效、缓慢且昂贵的。

#### SQL 与 Cosmos DB 的比较

关系型 SQL 数据库与 Cosmos DB 之间存在几个根本性的差异:

1.  **模式刚性**:SQL 数据库的行是刚性的,具有严格的数据验证。
    Cosmos DB 则很灵活,允许不同类型的文档存在于同一个集合中。
2.  **数据验证**:由于结构刚性,在 SQL 数据库中验证数据正确性通常更容易。
3.  **可扩展性**:文档数据库的扩展要容易得多。
    由于它们在单个文档的范围内工作,文档可以很容易地分布到集群中的多个节点上,或者在全球范围内进行地理分布,而对性能的影响极小。
4.  **查询灵活性**:SQL 是一种强大的领域特定语言,专为灵活地查询和连接规范化数据而设计。
    当应用的查询形状事先未知时,这是理想的选择。
    相比之下,Cosmos DB 的扩展能力更强,但查询形状必须由文档最初的存储方式来决定。

#### Azure Cosmos DB 定价

Cosmos DB 的定价使用请求单位(RU)来计算。
一个 RU 被定义为检索单个 1KB 文档的成本。
例如,每秒 400 个文档的预配吞吐量每月大约需要 46 美元。

Cosmos DB 还支持自动缩放,你预配一个最大峰值(例如 1,000 RU),但只需根据实际利用率支付其中的一部分。
如果利用率只有 25%,成本会显著下降,同时仍保持处理峰值流量的能力。
如果使用得当,Cosmos DB 可以成为 Azure 中一种非常具有成本效益的存储方案。

## 4. Switching our system to CosmosDB

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/) · 15:30

### 总结

Azure Cosmos DB 提供一种面向文档的存储模型,它通过一个专门的提供程序与 Entity Framework Core 集成。
本课演示如何从关系型数据库过渡到 Cosmos DB,重点介绍使用 GUID 作为标识符、配置容器,以及利用从属类型(owned types)把相关数据嵌入到单个文档中。
本课还讨论了文档数据库的根本局限,特别是缺乏关系型连接,以及由此需要对未嵌入的关系采用手动的数据获取策略。

### 核心概念

- **面向文档的存储**:与关系型数据库不同,Cosmos DB 以 JSON 文档的形式存储数据。
- **Cosmos DB 提供程序**:`Microsoft.EntityFrameworkCore.Cosmos` 包使 EF Core 能够与 Cosmos DB 通信。
- **容器映射**:使用 `ToContainer` 方法把实体映射到特定的 Cosmos DB 容器。
- **用于嵌入的从属类型**:使用 `OwnsOne` 和 `OwnsMany` 可以让相关实体被序列化到同一个 JSON 文档中,而不是放在单独的容器里。
- **分区键**:Cosmos DB 使用分区键来实现扩展;如果没有另行指定,EF Core 默认使用 `__partitionKey`。
- **连接的局限**:文档数据库不支持关系型连接;位于不同容器中的相关文档必须通过手动的往返请求来获取。

### 课程笔记

为了演示 Cosmos DB 的能力,项目使用了一个简化的数据模型,其中 `Movie` 和 `Actor` 等实体使用 `Guid` 作为主标识符。
这一选择与 Cosmos DB 面向文档的结构相契合。
`Movie` 实体包含一个指向 `Genre` 的导航属性,以及一个 `Role` 对象(角色)列表,而每个 `Role` 又引用一个 `Actor`。

#### 提供程序配置

要切换到 Cosmos DB,必须把现有的关系型提供程序(例如 MySQL)替换为 `Microsoft.EntityFrameworkCore.Cosmos` NuGet 包。
配置在 `Program.cs` 中通过 `UseCosmos` 方法完成,该方法需要一个连接字符串和一个数据库名称。

```csharp
// Add the DbContext
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
        optionsBuilder
            .UseCosmos(connectionString!,
                databaseName: "MoviesDB")
            .LogTo(Console.WriteLine);
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/?t=165)

#### 容器与文档映射

在 `DbContext` 的 `OnModelCreating` 方法中,实体被映射到容器。
在文档数据库中,并不是每个实体都需要自己的容器。
总是与父实体一起访问的相关数据,可以使用 `OwnsOne` 和 `OwnsMany` 直接嵌入到父文档中。
这确保这些数据被序列化到单个 JSON 文档中。

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Movie>()
        .ToContainer("Movies");

    modelBuilder.Entity<Movie>()
        .OwnsOne(movie => movie.Genre);
    
    modelBuilder.Entity<Movie>()
        .OwnsMany(movie => movie.Characters);
    
    modelBuilder.Entity<Actor>()
        .ToContainer("Actors");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/?t=430)

```text
Container "Movies"                 Container "Actors"
+---------------------------+      +------------------+
| Movie document            |      | Actor document   |
|   Genre       (OwnsOne)   |      |                  |
|   Characters  (OwnsMany)  |      |                  |
+---------------------------+      +------------------+
```

保存数据时,Cosmos DB 中生成的 JSON 会包含实体的基础属性以及嵌入的从属类型。
Cosmos DB 还会添加内部元数据字段(例如 `_rid`、`_self` 和 `_etag`),用于在集群中管理文档。

```json
{
    "Id": "7e4e3c5a-094c-4432-99d2-d862e38d5794",
    "Discriminator": "Movie",
    "ReleaseDate": "1999-11-10T00:00:00Z",
    "Synopsis": "Ed Norton and Brad Pitt go at it in a couple of fist fights.",
    "Title": "Fight Club",
    "id": "Movie|7e4e3c5a-094c-4432-99d2-d862e38d5794",
    "Characters": [
        {
            "ActorId": "a6e6d7a9-59a8-4a92-8b15-0fa18a28d6a1",
            "CharacterName": "Narrator"
        },
        {
            "ActorId": "7b418f45-a367-4907-9dd3-cf5cd4c5ef20",
            "CharacterName": "Tyler Durden"
        }
    ],
    "Genre": {
        "Description": "Movies with dramatic qualities.",
        "Name": "Drama"
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/?t=555)

#### 处理关系与连接

Cosmos DB 与关系型数据库之间的一个关键区别是不支持连接。
对指向另一个容器的导航属性使用 `.Include()` 会导致异常,因为 EF Core 无法在单次操作中跨容器查询依赖文档。

```csharp
// This will throw an exception in Cosmos DB if Actors are in a separate container
var movie = await _context.Movies
    .Include(movie => movie.Characters)
    .ThenInclude(c => c.Actor)
    .SingleOrDefaultAsync(m => m.Id == id);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/?t=665)

要解决这个问题,你必须执行手动的往返请求。
首先,确保嵌入文档中有可用的关联 ID(例如 `ActorId`)。
如果需要,你可以使用 `[JsonIgnore]` 让这个 ID 不出现在 API 响应中。

```csharp
public class Role
{
    public required string CharacterName { get; set; }
    
    [JsonIgnore]
    public Guid ActorId { get; set; }
    
    public Actor Actor { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/?t=750)

在控制器中,获取主文档之后,遍历相关的 ID,并从它们各自的容器中获取对应的文档。
虽然这会增加数据库往返次数并消耗更多请求单位(Request Units,RU),但这是在 Cosmos DB 中管理未嵌入关系的标准模式。

```csharp
[HttpGet("{id:guid}")]
public async Task<IActionResult> Get([FromRoute] Guid id)
{
    var movie = await _context.Movies
        .SingleOrDefaultAsync(m => m.Id == id);

    if (movie == null) return NotFound();

    foreach (var role in movie.Characters)
    { // Manual lookup for related documents
        var actor = _context.Actors.Find(role.ActorId);
        role.Actor = actor;
    }
    
    return Ok(movie);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/switching-our-system-to-cosmosdb-54130413/?t=850)

```text
Get(id)
  1. Movies container: SingleOrDefaultAsync(m => m.Id == id)
  2. foreach role in movie.Characters:
       Actors container: Find(role.ActorId)  -> role.Actor
  3. Ok(movie)
```

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54130414/) · 1:09

### 总结

Entity Framework Core 提供了一套强大而统一的 API,用于与多种数据库引擎交互,包括关系型系统和基于文档的系统。
虽然这种抽象让开发者能够在不同技术之间复用已有的 EF Core 知识,但它并不是实现数据库可移植性的"银弹"。
各引擎之间在功能支持和映射要求上的差异意味着,切换提供程序仍然需要大量的规划和实现工作。

### 核心概念

- **统一的 API**:EF Core 为与关系型数据库(例如 SQL Server、MariaDB)和文档数据库(例如 Azure Cosmos DB)交互提供了一致的接口。
- **开发者的可迁移性**:EF Core 的知识可以在不同的数据库后端之间通用,让团队能够快速扩展和适应。
- **功能支持的陷阱**:并非所有数据库引擎都支持同一组 EF Core 功能;开发者必须针对具体功能验证兼容性。
- **映射方法**:不同的数据库引擎需要不同的映射配置和策略才能正确工作。
- **迁移工作量**:虽然 EF Core 简化了这个过程,但在生产环境中切换数据库引擎仍然需要大量的实现工作。

### 课程笔记

Entity Framework Core 的主要优势在于,它能够作为单一的、强类型的对象关系映射器(ORM),服务于多种多样的数据库引擎。
通过提供一致的 API,EF Core 让开发者能够使用相同的模式与关系型数据库和面向文档的数据库交互。
这种共享的知识基础对团队来说是一项强大的资产,因为它让开发者能够以最小的阻力在使用不同数据库技术的项目之间切换。

尽管有这种抽象,仍有几个关键的陷阱需要考虑。
数据库技术在功能层面上并不能互相替换。
使用 EF Core 时,你必须经常评估某个特定功能(例如某种类型的查询、索引策略或事务行为)是否受到底层数据库提供程序的支持。
如果某个功能不受支持,应用可能会失败,或者需要大量的变通处理。

除了功能支持之外,不同数据库引擎之间的映射方法也有显著差异。
实体映射到关系型模式(使用表、列和外键)的方式,与映射到文档存储(使用容器和 JSON 结构)的方式有根本的不同。
由于这些架构上的差异,不可能"一夜之间"把一个数据库引擎换成另一个。
即使有 EF Core 提供的抽象,在不同的数据库提供程序之间迁移一个大型项目,总是需要相当可观的工作量来重新配置映射,并确保应用逻辑与新引擎的能力保持一致。

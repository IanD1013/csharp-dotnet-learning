# Testing with EF Core

> 课程:[From Zero to Hero: Entity Framework Core in .NET](https://dometrain.com/course/from-zero-to-hero-entity-framework-core-in-dotnet/) · 第 6 章
> 共 7 课 · 约 50:41
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What are the challenges in tests with ORMs?](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-are-the-challenges-in-tests-with-orms-54129991/) | 4:59 | [↓](#1-what-are-the-challenges-in-tests-with-orms) |
| 2 | [Using Test databases](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-test-databases-54129992/) | 5:17 | [↓](#2-using-test-databases) |
| 3 | [Using a database in memory](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/) | 13:29 | [↓](#3-using-a-database-in-memory) |
| 4 | [Faking DbSets in unit tests](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/) | 10:09 | [↓](#4-faking-dbsets-in-unit-tests) |
| 5 | [The repository pattern](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/) | 8:49 | [↓](#5-the-repository-pattern) |
| 6 | [Integration testing with EF Core](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/integration-testing-with-ef-core-54129996/) | 4:26 | [↓](#6-integration-testing-with-ef-core) |
| 7 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129997/) | 3:32 | [↓](#7-section-recap) |

## 1. What are the challenges in tests with ORMs?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/what-are-the-challenges-in-tests-with-orms-54129991/) · 4:59

### 总结

自动化测试对可维护的软件至关重要,但由于业务逻辑、ORM 与底层数据库之间的紧密耦合,集成 Entity Framework Core 这样的 ORM 会带来复杂性。
为了应对这些挑战,开发者通常会采用四种策略之一:使用专用的测试数据库、使用内存数据库提供程序、伪造 DbSet 实例以隔离 ORM,或者实现仓储模式来抽象数据访问。

### 核心概念

* **自动化测试的价值**:确保代码在未来的改动中保持稳定,并阐明软件的预期行为。
* **依赖链**:业务逻辑依赖 ORM(EF Core),而 ORM 又依赖数据库引擎。
* **测试数据库策略**:使用真实的数据库引擎和一个独立的 schema,以保持高保真度。
* **内存数据库策略**:使用 SQLite(内存模式)或 EF Core 的 In-Memory 提供程序等提供程序来提高测试速度。
* **伪造 DbSet**:用内存集合替换 ORM 的数据集,从而隔离逻辑。
* **仓储模式**:在逻辑与 ORM 之间加入一个抽象层,以便更容易进行 mock。

### 课程笔记

自动化测试(包括单元测试、集成测试和自动化验收测试)是可维护软件的基石。
这些测试有双重用途:在未来的开发中防止回归,并阐明代码库的技术意图。
然而,引入 Entity Framework Core 这样的对象关系映射器(ORM)会让测试过程变得复杂,因为逻辑层、ORM 和数据库之间存在依赖。

在典型的应用架构中,请求来自 UI、API 端点或消息总线,并流入逻辑层。
这个逻辑层包含核心业务规则和应用独有的价值,它依赖 ORM 来抽象针对数据库的 CRUD 和查询操作。
由于逻辑层是测试工作的主要关注点,开发者必须设法处理它对 ORM 和数据库的依赖。

```text
UI / API endpoint / message bus
              |
              v
         Logic layer
              |
              v
       ORM (EF Core)
              |
              v
          Database
```

测试涉及 EF Core 的逻辑有四种主要策略:

1. **测试数据库**:把生产数据库的一个副本(最好使用缩减后的数据集)部署到专用的测试服务器上。
   然后将应用配置为让 ORM 指向这个测试 schema。
   这种方式使用真实的数据库引擎,保证了高保真度,但执行可能较慢。

2. **内存数据库**:这种策略用内存中的等价物替换物理数据库,例如 SQLite 内存模式或 EF Core In-Memory 提供程序。
   这能显著提高测试执行速度,并提供一个更封闭的环境。

3. **模拟 ORM(伪造 DbSet)**:不使用数据库引擎,而是用内存数据结构伪造 ORM 的 `DbSet` 属性。
   这样通过 mock ORM 及其下方的一切来隔离逻辑层。

4. **仓储模式**:通过在业务逻辑与 ORM 之间引入一个抽象层,逻辑层依赖的是一个接口,而不是直接依赖 `DbContext`。
   这使得在测试中 mock 数据访问变得很直接。

## 2. Using Test databases

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-test-databases-54129992/) · 5:17

### 总结

测试数据库方式是指针对一个镜像生产环境的真实数据库实例运行测试。
通过把 DbContext 指向专用的测试服务器,开发者可以确保整个应用栈(从业务逻辑经过 ORM 一直到数据库引擎)都按预期工作,而无需 mock 框架。
虽然这能为系统的集成提供很高的信心,但它也带来了数据清理、测试并发方面的挑战,并且与内存测试相比执行时间明显更慢。

### 核心概念

- **与生产环境一致**:使用与生产环境相同的数据库提供程序和配置,以确保行为完全一致。
- **数据匿名化**:在开发或测试环境中使用生产数据之前,先清除其中的敏感数据。
- **集成测试**:验证应用逻辑、ORM(EF Core)与物理数据库之间的交互。
- **准备与清理**:要求在每次测试执行之前和之后保持一致的数据库状态。
- **性能开销**:网络延迟和数据库 I/O 对测试套件速度的影响。

### 课程笔记

测试数据库方式使用一个真实的数据库实例来进行测试。
这通常是通过创建生产数据库的一个副本并将其部署到专用测试服务器上来实现的。
然后将应用的 `DbContext` 配置为指向这个测试数据库,从而可以在一个镜像生产环境的环境中测试整个技术栈。

使用生产数据时,关键是要使用有限的数据集,以防测试因不可预测的数据量而变得脆弱。
此外,任何敏感的用户信息都必须匿名化,以符合安全和隐私标准。

#### 优点

使用真实的测试数据库有几个好处:
- **完全相同的技术栈**:测试运行在与生产环境完全相同的技术栈上,确保所有组件和功能的行为都一致。
- **无需 mock**:由于测试与真实数据库交互,不需要 mock 框架或伪造数据,这降低了测试套件的复杂度。
- **全栈验证**:这种方式测试从业务逻辑向下经过 ORM 直到数据库本身的一切。
  如果测试失败,很可能说明存在一个在生产环境中同样会出现的真实问题。

#### 缺点

尽管保真度很高,但也存在明显的缺点:
- **失败面广**:数据库或 ORM 中的一个故障就可能导致整个测试套件失败,使得难以定位失败的具体原因。
- **准备与清理的复杂性**:每个测试都必须从一致的数据库状态开始。
  测试期间创建或修改的数据,无论测试通过与否都必须被清理。
  未能正确清理常常会导致连锁的测试失败。
- **并发问题**:如果多个开发者或 CI/CD 流水线使用同一个测试数据库,测试必须设计成互不干扰。
- **性能延迟**:数据库往返和网络跳转使这些测试比内存中的单元测试慢得多。
  在大型代码库中,这会拖慢开发周期。

#### 实现

要实现这种方式,需要用指向测试数据库的选项来初始化 `DbContext`。
在测试的准备阶段,通常会重置数据库以确保一个干净的状态。

```csharp
public IntegrationTest()
{
    var options = new DbContextOptionsBuilder<MoviesContext>()
        .UseSqlServer("""
                      Data Source=localhost;
                      Initial Catalog=TestDB;
                      User Id=sa;
                      Password=MySaPassword123;
                      TrustServerCertificate=True;
                      """)
        .Options;

    _testContext = new MoviesContext(options);
    _verificationContext = new MoviesContext(options);

    _testContext.Database.EnsureDeleted();
    _testContext.Database.EnsureCreated();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-test-databases-54129992/?t=16)

一个典型的集成测试随后会使用这个上下文执行操作,并直接验证数据库的状态。

```csharp
[Fact]
public async Task WhenGenreCreated_GenreIsInDatabase()
{
    // Arrange
    var repository = new GenreRepository(_testContext);
    
    // Act
    var genreToCreate = new Genre { Name = "MyAwesomeGenre" };
    var createdGenre = await repository.Create(genreToCreate);
    
    // Assert
    Assert.NotNull(createdGenre);
    Assert.True(createdGenre.Id > 0);
    var verificationGenre = _verificationContext.Genres.Find(createdGenre.Id);
    Assert.NotNull(verificationGenre);
    Assert.Equal(genreToCreate.Name, verificationGenre.Name);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-test-databases-54129992/?t=111)

## 3. Using a database in memory

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/) · 13:29

### 总结

使用内存数据库进行测试,是用一个运行在系统 RAM 中的临时数据库引擎替换 SQL Server 这样的持久化数据库引擎,从而加快执行速度。
虽然 Microsoft 提供了一个内置的 in-memory 提供程序,但由于 SQLite 功能更全面且支持事务,它是这种策略的推荐选择。
这种方式保持了相同的技术栈(Entity Framework Core),但需要通过底层连接仔细管理数据库的生命周期。
开发者必须对提供程序特有的差异保持警惕,因为不兼容的数据类型、原生 SQL 方言或特定的 SQL Server 功能可能导致测试失败,而这些失败并不反映真正的逻辑错误。

### 核心概念

- 内存数据库测试(基于 RAM 执行)。
- SQLite 与 Microsoft In-Memory 提供程序的对比。
- 通过 SqliteConnection 管理数据库生命周期。
- 用 DbContextOptionsBuilder 配置提供程序。
- 提供程序特有的兼容性(数据类型和 SQL 方言)。
- 使用 EnsureCreated 生成 schema。

### 课程笔记

测试 EF Core 应用的第二种策略是使用内存数据库。
这种方式用一个完全运行在系统 RAM 中的数据库替换 SQL Server 引擎。
主要优势是速度;由于避免了磁盘 I/O 和网络开销,测试运行得明显更快。
然而,这是以功能一致性为代价的。
更换数据库引擎可能导致功能缺失、数据类型不兼容或 SQL 方言差异等问题,尤其是在使用原生 SQL 时。

虽然 Microsoft 提供了一个专门的 "In-Memory" 提供程序,但它功能有限,并且不支持事务。
Microsoft 推荐改用 SQLite 内存模式,因为它提供了更健全的功能集。

为了演示这一点,我们使用一个带有基础 Genre 模型的简化 API。

```csharp
namespace Dometrain.EFCore.SimpleAPI.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=215)

为了支持切换提供程序,MoviesContext 必须包含一个接受 DbContextOptions<MoviesContext> 的构造函数。
这使得测试套件可以注入 SQLite 配置,而生产应用则使用 SQL Server。

```csharp
namespace Dometrain.EFCore.SimpleAPI.Data;

public class MoviesContext : DbContext
{
    public MoviesContext() { }
    
    public MoviesContext(DbContextOptions<MoviesContext> options)
        :base(options)
    { }
    
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Not proper logging
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GenreMapping());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=400)

在 SQLite 中,内存数据库与连接的生命周期绑定。
数据库在连接打开时创建,在连接被释放时销毁。
因此,测试类必须维护一个私有的 SqliteConnection 字段。

```text
_connection.Open()        -> in-memory database created
   ... tests use the database ...
_connection.Dispose()     -> in-memory database destroyed
```

```csharp
public class SqlLiteTest : IDisposable
{
    private readonly SqliteConnection _connection;

    public SqlLiteTest()
    {
        // Test setup
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
    }

    public void Dispose()
    {
        // Test teardown
        _connection.Dispose();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=370)

为了在测试中实例化上下文,使用 DbContextOptionsBuilder 指定 SQLite 提供程序,并传入已有的连接。

```csharp
private MoviesContext CreateInMemoryContext()
{
    var contextOptions = new DbContextOptionsBuilder<MoviesContext>()
        .UseSqlite(_connection)
        .Options;

    var context = new MoviesContext(contextOptions);

    return context;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=660)

在运行测试之前,必须先生成 schema 并填充种子数据。
由于内存数据库一开始是空的,所以使用 context.Database.EnsureCreated() 根据 EF Core 模型创建表。

```csharp
public SqlLiteTest()
{
    // Test setup
    _connection = new SqliteConnection("Filename=:memory:");
    _connection.Open();

    var context = CreateInMemoryContext();

    context.Database.EnsureCreated();
    
    context.Genres.AddRange(
        new Genre { Name = "Drama" },
        new Genre { Name = "Action" },
        new Genre { Name = "Comedy" }
        );

    context.SaveChanges();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=610)

然后测试方法与控制器交互,并传入内存上下文。

```csharp
[Fact]
public async Task IfGenreExists_ReturnsGenre()
{
    // Arrange
    var context = CreateInMemoryContext();
    var controller = new GenresController(context);
    
    // Act
    var response = await controller.Get(2);
    var okResult = response as OkObjectResult;
    
    // Assert
    Assert.NotNull(okResult);
    Assert.Equal(200, okResult.StatusCode);
    Assert.Equal("Action", (okResult.Value as Genre)?.Name);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=625)

这种方式的一个重大风险是提供程序特有的映射。
例如,在映射配置中使用 nvarchar(max) 这样的 SQL Server 类型,会导致 SQLite 在创建 schema 时失败,因为它不认识这个特定类型。

```csharp
public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();

        builder.Property(genre => genre.Name)
            .HasColumnType("nvarchar(max)");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/using-a-database-in-memory-54129993/?t=730)

由于这些差异,内存测试可能无法提供与生产数据库引擎完全相同的体验,如果涉及复杂的映射,可能会削弱测试的价值。

## 4. Faking DbSets in unit tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/) · 10:09

### 总结

伪造 `DbSet` 实例是指 mock `DbContext` 及其属性,从而在与 Entity Framework Core 和底层数据库提供程序完全隔离的情况下执行应用逻辑。
这种策略执行速度很快,并能确保测试失败只归因于逻辑错误,而不是基础设施或映射问题。
然而,由于需要 mock `IQueryable` 的行为、LINQ 扩展方法和异步操作,它引入了相当大的复杂性,通常需要借助 `MockQueryable` 这样的专门库,在内存集合与 Entity Framework 期望的接口之间架起桥梁。

### 核心概念

- **与 EF Core 隔离**:在不依赖 Entity Framework 或特定数据库提供程序的情况下测试逻辑。
- **Mock DbContext**:使用 `NSubstitute` 为 `DbContext` 类创建一个替身。
- **MockQueryable.NSubstitute**:一个用于把标准内存列表转换为 mock `DbSet` 对象的库,这些对象支持 `IQueryable` 和异步操作。
- **验证调用**:mock 让开发者可以验证特定方法(例如 `SaveChangesAsync`)是否被调用过。
- **手动 mock EF 特有的方法**:像 `FindAsync` 这样的方法不是标准的 LINQ 扩展,即使使用辅助库也需要手动设置。

### 课程笔记

伪造 `DbSet` 是一种测试策略,它向应用逻辑提供一个 mock 版本的 `DbContext`。
这使得逻辑可以独立运行,不依赖 Entity Framework 和底层数据库提供程序。
主要优势是隔离和速度;由于不涉及任何数据库提供程序(甚至连 SQLite 都没有),测试执行得非常快。
此外,如果测试失败,就可以确认是逻辑中的 bug,而不是数据库映射或框架本身的问题。

然而,伪造 `DbSet` 会引入复杂性。
LINQ 方法是作为 `IQueryable` 上的扩展方法实现的,而不是 `DbSet` 上的接口方法,这使得它们很难 mock。
此外,异步操作使 mock 过程更加复杂。
为了实现这一点,使用了 `NSubstitute` 和 `MockQueryable.NSubstitute` 库。
下面的测试演示了验证 genre 被正确返回,并且在读操作期间从未调用 `SaveChangesAsync`。

```csharp
[Fact]
public async Task IfGenreExists_ReturnsGenre()
{
    // Arrange
    var context = CreateFakeDbContext();
    var controller = new GenresController(context);

    // Act
    var response = await controller.Get(2);
    var okResult = response as OkObjectResult;

    // Assert
    Assert.NotNull(okResult);
    Assert.Equal(200, okResult.StatusCode);
    Assert.Equal("Action", (okResult.Value as Genre)?.Name);
    await context.DidNotReceive().SaveChangesAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=185)

要设置伪造的上下文,首先使用 `NSubstitute` 为 `MoviesContext` 创建一个替身。

```csharp
var context = Substitute.For<MoviesContext>();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=295)

接下来,把一个内存数据列表转换为 mock `DbSet`。
`MockQueryable` 提供的 `BuildMockDbSet` 扩展方法会把 `IQueryable` 实现接好,使其能与 `NSubstitute` 一起工作。

```csharp
var genresSet = genres.AsQueryable().BuildMockDbSet();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=355)

然后配置 mock 上下文,使其在访问 `Genres` 属性时返回这个伪造的集合。

```csharp
context.Genres.Returns(genresSet);

return context;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=400)

```text
List<Genre> genres
      |  .AsQueryable().BuildMockDbSet()
      v
  genresSet
      |  context.Genres.Returns(genresSet)
      v
Substitute.For<MoviesContext>()  -->  GenresController
```

这种方式的一个重大限制是,`MockQueryable` 主要处理标准的 LINQ 扩展方法。
如果控制器使用 `FirstOrDefaultAsync`,mock 会自动生效。

```csharp
[HttpGet("{id:int}")]
[ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> Get([FromRoute] int id)
{
    var genre = await _context.Genres.FirstOrDefaultAsync(genre => genre.Id == id);

    return genre == null
        ? NotFound()
        : Ok(genre);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=465)

然而,如果控制器使用 `FindAsync`(这是一个 EF 特有的方法,而不是 LINQ 扩展),测试就会失败,因为 mock 库不会自动实现 `FindAsync`。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=490)

为了解决这个问题,必须在 `genresSet` 上手动 mock `FindAsync`。
伪造上下文设置的最终实现包含了这个手动 mock,它返回一个包含内存列表中预期实体的 `ValueTask`。

```csharp
private MoviesContext CreateFakeDbContext()
{
    List<Genre> genres = new()
    {
        new Genre { Id = 1, Name = "Drama" },
        new Genre { Id = 2, Name = "Action" },
        new Genre { Id = 3, Name = "Comedy" }
    };

    var context = Substitute.For<MoviesContext>();

    var genresSet = genres.AsQueryable().BuildMockDbSet();

    genresSet.FindAsync(2)!.Returns(new ValueTask<Genre>(genres.ElementAt(1)));

    context.Genres.Returns(genresSet);

    return context;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/faking-dbsets-in-unit-tests-54129994/?t=595)

## 5. The repository pattern

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/) · 8:49

### 总结

仓储模式在应用逻辑与 Entity Framework Core 之间引入一个抽象层,使代码更整洁,并简化单元测试。
通过注入一个接口而不是 DbContext,开发者可以在完全隔离的情况下 mock 数据访问,从而得到更快、更可靠的测试。
虽然这种方式通过让逻辑不受持久化细节影响来支持以领域为中心的架构,但它需要维护额外的文件,并且可能限制对高级 ORM 功能的直接访问。

### 核心概念

- 在 ORM 与应用逻辑之间引入一个抽象层。
- mock 简洁的接口,而不是复杂的 DbContext 实例。
- 支持以领域为中心的架构(Clean Architecture、Onion Architecture)。
- 将领域逻辑与持久化实现细节隔离。
- 权衡:可测试性提升,与维护量增加、ORM 功能可访问性受限之间的取舍。

### 课程笔记

仓储模式是位于 ORM(Entity Framework Core)与应用逻辑之间的一个架构组件。
通过为数据操作定义一个明确的接口,并把该接口注入到逻辑中,实现就与具体的持久化机制解耦了。
这种抽象使逻辑测试可以在与数据库完全隔离的情况下执行。

```text
Application logic (controller)
          |  depends on
          v
  IGenreRepository  (interface)
          ^                         ^
          | implements              | mocked in tests
  GenreRepository              Substitute.For<IGenreRepository>()
          |
          v
  MoviesContext (EF Core)
          |
          v
       Database
```

#### 优点与缺点

使用仓储有几个好处:
1. **易于 mock**:接口比完整的 DbContext 容易 mock 得多。
2. **测试速度**:测试运行得更快,因为它们不依赖复杂的实现或内存数据库。
3. **持久化无知**:这种方式让应用逻辑不受 Entity Framework Core 影响,这是 Clean Architecture 或 Ports and Adapters 等以领域为中心的架构的核心原则。

然而,也存在明显的缺点:
1. **ORM 功能受限**:虽然让逻辑不受 Entity Framework Core 影响对隔离来说是一个优点,但在需要高级 ORM 功能时它可能成为缺点,因为这些功能只有在仓储接口中显式定义后才能访问。
2. **维护开销**:实现大量方法,并在生产系统中管理更多的文件,会增加维护负担。

#### 在控制器中的实现

使用仓储模式时,控制器注入的是仓储接口,而不是 DbContext。
这次重构减少了控制器的代码量,并使整体实现更加简洁。

```csharp
[ApiController]
[Route("[controller]")]
public class GenresWithRepositoryController : Controller
{
    private readonly IGenreRepository _repository;

    public GenresWithRepositoryController(IGenreRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var genre = await _repository.Get(id);
        
        return genre == null
            ? NotFound()
            : Ok(genre);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Genre genre)
    {
        var createdGenre = await _repository.Create(genre);

        return CreatedAtAction(nameof(Get), new { id = createdGenre.Id }, createdGenre);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/?t=235)

#### 仓储接口与实现

仓储接口定义了数据访问的契约,而实现则使用 DbContext 来执行实际的操作。

```csharp
public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAll();
    Task<Genre?> Get(int id);
    Task<Genre> Create(Genre genre);
    Task<Genre?> Update(int id, Genre genre);
    Task<bool> Delete(int id);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/?t=265)

实现封装了所有 Entity Framework 特有的代码,例如使用 `ToListAsync`、`FindAsync` 和 `SaveChangesAsync`:

```csharp
public async Task<IEnumerable<Genre>> GetAll()
{
    return await _context.Genres.ToListAsync();
}

public async Task<Genre?> Get(int id)
{
    return await _context.Genres.FindAsync(id);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/?t=295)

```csharp
public async Task<Genre> Create(Genre genre)
{
    await _context.Genres.AddAsync(genre);
    
    await _context.SaveChangesAsync();

    return genre;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/?t=310)

#### 依赖注入注册

仓储实现必须注册到依赖注入容器中,通常注册为 transient 服务。

```csharp
builder.Services.AddTransient<IGenreRepository, GenreRepository>();

builder.Services.AddDbContext<MoviesContext>(opt =>
{
    opt.UseSqlServer("""
        Data Source=localhost;
        Initial Catalog=MoviesDB;
        User Id=sa;
        Password=MySaPassword123;
        TrustServerCertificate=True;
        """);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/?t=325)

#### 使用 mock 进行单元测试

这种模式的主要优势是易于测试。
使用 NSubstitute 这样的库,与 mock 完整的 DbContext 相比,只需极少的配置就能 mock 仓储。
mock 框架提供了灵活性,可以验证应用逻辑是否按预期与仓储交互,例如使用 `Received()` 验证特定方法是否被调用。

```csharp
[Fact]
public async Task IfGenreExists_ReturnsGenre()
{
    // Arrange
    var repository = Substitute.For<IGenreRepository>();
    repository.Get(2)!.Returns(Task.FromResult(new Genre { Id = 2, Name = "Action" }));
    var controller = new GenresWithRepositoryController(repository);
    
    // Act
    var response = await controller.Get(2);
    var okResult = response as OkObjectResult;
    
    // Assert
    Assert.NotNull(okResult);
    Assert.Equal(200, okResult.StatusCode);
    Assert.Equal("Action", (okResult.Value as Genre)?.Name);
    await repository.Received().Get(2);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/the-repository-pattern-54129995/?t=490)

## 6. Integration testing with EF Core

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/integration-testing-with-ef-core-54129996/) · 4:26

### 总结

Entity Framework Core 中的集成测试验证从数据库和 ORM 一直到仓储或逻辑层的整个技术栈。
通过使用与生产环境相同的数据库技术,这些测试可以确保数据库 schema、EF Core 模型和迁移正确同步,并且数据操作能够在不同的上下文实例之间正确地持久化和读取。

### 核心概念

- **数据库集成测试**:验证数据库、ORM 与应用逻辑之间的交互。
- **与生产环境一致**:测试使用实际的数据库技术(例如 SQL Server),而不是内存替代品,以确保兼容性。
- **同步验证**:发现缺失的迁移或不同步的 `DbContext` 模型等问题。
- **双上下文验证**:使用不同的 `DbContext` 实例分别执行和验证,以绕过 EF Core 的本地缓存。
- **仓储测试**:验证仓储方法能否正确地向物理存储持久化数据并从中读取数据。

### 课程笔记

集成测试旨在验证系统从逻辑层向下直到数据库的行为。
与使用 mock 隔离逻辑的单元测试不同,集成测试检查数据库、ORM 和仓储协同工作得如何。
这些测试对于发现单元测试无法发现的问题至关重要,例如缺失的迁移,或与实际数据库 schema 不同步的 `DbContext` 模型。

对于这些测试,建议使用一个与生产环境采用相同技术的测试数据库。
这确保了在测试期间能够考虑到任何提供程序特有的行为。

#### 测试准备

为了执行一个干净的集成测试,准备工作通常包括创建两个指向同一个测试数据库的相同 `DbContext` 实例。
一个上下文用于执行被测试的操作,而第二个上下文用于验证。
这可以防止测试因为 EF Core 内部的跟踪和缓存而意外通过;通过使用第二个上下文,可以确保数据确实是从数据库中读取的。

```text
                    options (TestDB)
                   /                \
         _testContext         _verificationContext
              |                        |
   repository.Create(...)   Genres.Find(createdGenre.Id)
              |                        |
              v                        v
              +--------> TestDB <------+
```

```csharp
private readonly MoviesContext _testContext;
private readonly MoviesContext _verificationContext;

public IntegrationTest()
{
    var options = new DbContextOptionsBuilder<MoviesContext>()
        .UseSqlServer("""
                      Data Source=localhost;
                      Initial Catalog=TestDB;
                      User Id=sa;
                      Password=MySaPassword123;
                      TrustServerCertificate=True;
                      """)
        .Options;

    _testContext = new MoviesContext(options);
    _verificationContext = new MoviesContext(options);

    // Usually not needed, as a deploy of the test database
    // before the test run will migrate the schema.
    _testContext.Database.EnsureDeleted();
    _testContext.Database.EnsureCreated();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/integration-testing-with-ef-core-54129996/?t=115)

虽然这里为了演示使用了 `EnsureDeleted()` 和 `EnsureCreated()`,但在标准的 CI/CD 流水线中,测试数据库通常会在测试执行之前作为部署步骤的一部分完成迁移。

#### 实现测试

数据访问的集成测试通常以仓储层为目标。
测试遵循 Arrange-Act-Assert 模式:
1.  **Arrange**:使用测试上下文初始化仓储。
2.  **Act**:执行一个数据库操作,例如创建一个新实体。
3.  **Assert**:验证仓储返回的结果,然后使用验证上下文直接从数据库获取该实体,以确认其已持久化。

```csharp
[Fact]
public async Task WhenGenreCreated_GenreIsInDatabase()
{
    // Arrange
    var repository = new GenreRepository(_testContext);

    // Act
    var genreToCreate = new Genre { Name = "MyAwesomeGenre" };
    var createdGenre = await repository.Create(genreToCreate);

    // Assert
    Assert.NotNull(createdGenre);
    Assert.True(createdGenre.Id > 0);
    var verificationGenre = _verificationContext.Genres.Find(createdGenre.Id);
    Assert.NotNull(verificationGenre);
    Assert.Equal(genreToCreate.Name, verificationGenre.Name);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/integration-testing-with-ef-core-54129996/?t=190)

这种方式确认了当仓储调用完成时,数据确实以正确的值存在于数据库中,从而确保整个数据访问栈按预期工作。

## 7. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-entity-framework-core-in-dotnet-2732043/section-recap-54129997/) · 3:32

### 总结

本课从策略层面概述了如何测试使用 Entity Framework Core 的应用,提倡采用多层次的方法。
它强调了仓储模式在将领域逻辑与 ORM 解耦方面的作用,从而可以通过 mock 进行隔离的单元测试。
该策略还结合了用于验证数据库特有逻辑的集成测试,以及用于对业务场景进行端到端验证的验收测试。

### 核心概念

- **仓储模式**:作为业务逻辑与 ORM 之间的抽象层,使领域不受基础设施依赖的影响。
- **集成测试**:针对真实数据库验证数据库 schema、迁移、DbContext 配置和仓储实现。
- **单元测试**:通过 mock 仓储接口来专注于核心业务逻辑,把被测代码与数据访问的关注点隔离开。
- **验收测试**:对已部署的应用进行黑盒测试,通常使用 SpecFlow 等 BDD 框架来验证业务场景。
- **策略选择**:测试的深度和类型应当与项目需求以及与业务干系人的协作程度相权衡。

### 课程笔记

使用仓储是将业务逻辑与对象关系映射器(ORM)分离的常见做法。
在以领域为中心的架构中,这种抽象对于让核心领域和逻辑不受 Entity Framework Core 等外部依赖的影响至关重要。
虽然仓储增加了额外一层的实现工作量,但它们通过防止领域与数据访问技术紧密耦合,显著提升了可测试性并维护了架构的完整性。

集成测试是验证数据访问层的主要工具。
这些测试应当覆盖从数据库 schema 到仓储实现的整个技术栈。
具体来说,它们确保迁移被正确应用、DbContext 模型配置准确,并且仓储按预期与数据库交互。
这一层测试对于发现单元测试无法发现的问题至关重要,例如错误的 SQL 生成或 schema 不匹配。

对于仓储之上的各层,单元测试是首选方法。
通过 mock 仓储接口,开发者可以在没有数据库连接开销的情况下隔离地测试核心逻辑。
这种方式确保业务规则正确,同时保持测试套件快速且聚焦。
它允许对业务逻辑中的边界情况进行详尽的测试,而无需准备复杂的数据库状态。

验收测试通过把应用当作黑盒来测试,提供了最后一层验证。
这些测试通常使用 SpecFlow 等行为驱动开发(BDD)框架实现自动化。
是否实施大量验收测试,往往取决于业务干系人的参与程度。
如果干系人参与审查和定义这些场景,那么构建和维护它们的投入就非常有价值。
然而,如果业务方对这些场景兴趣不大,资源通常更应该投入到提升单元测试和集成测试的覆盖率与质量上。

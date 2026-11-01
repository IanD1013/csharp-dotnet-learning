# Unit testing in the real world

> 课程:[From Zero to Hero: Unit testing for C# Developers](https://dometrain.com/course/from-zero-to-hero-unit-testing-for-csharp-developers/) · 第 6 章
> 共 11 课 · 约 1:15:15
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The project we will be adding unit tests to](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/) | 3:38 | [↓](#1-the-project-we-will-be-adding-unit-tests-to) |
| 2 | [The scope of our testing](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-scope-of-our-testing-53927160/) | 3:59 | [↓](#2-the-scope-of-our-testing) |
| 3 | [Testing the application layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/) | 22:35 | [↓](#3-testing-the-application-layer) |
| 4 | [Exercise: Testing the rest of the application layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-application-layer-53927163/) | 1:24 | [↓](#4-exercise-testing-the-rest-of-the-application-layer) |
| 5 | [Exercise answer: Testing the rest of the application layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/) | 15:21 | [↓](#5-exercise-answer-testing-the-rest-of-the-application-layer) |
| 6 | [But was all that necessary?](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/but-was-all-that-necessary-53927165/) | 2:10 | [↓](#6-but-was-all-that-necessary) |
| 7 | [Testing the data layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-data-layer-53927166/) | 1:13 | [↓](#7-testing-the-data-layer) |
| 8 | [Testing the API layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/) | 9:51 | [↓](#8-testing-the-api-layer) |
| 9 | [Exercise: Testing the rest of the API layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-api-layer-project-source-under-the-video-53927169/) | 1:03 | [↓](#9-exercise-testing-the-rest-of-the-api-layer) |
| 10 | [Exercise answer: Testing the rest of the API layer](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/) | 13:19 | [↓](#10-exercise-answer-testing-the-rest-of-the-api-layer) |
| 11 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927182/) | 0:42 | [↓](#11-section-recap) |

## 1. The project we will be adding unit tests to

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/) · 3:38

### 总结

本课介绍 User API 项目,它是本课程真实世界单元测试示例的主要对象。
该项目是一个可运行的 ASP.NET Core Web API,使用 SQLite 数据库和 Dapper 进行数据访问来管理用户数据。
它展示了标准的架构模式,包括依赖注入、仓储模式和服务层抽象,为在应用的不同层实现单元测试提供了一个真实的环境。

### 核心概念

*   针对 User 实体进行 CRUD 操作的 RESTful API 端点。
*   SQLite 作为一个持久化的文件系统数据库。
*   Dapper 用于轻量级的对象关系映射(ORM)。
*   依赖注入(DI)用于管理服务、仓储和数据库工厂。
*   用于创建表结构和种子数据的数据库初始化逻辑。
*   Controller、Service 和 Repository 之间的关注点分离。

### 课程笔记

User API 项目是一个可运行的 RESTful 服务,用于管理用户记录。
它提供的端点用于获取所有用户、按唯一标识符获取特定用户、创建新用户以及删除已有用户。
该 API 与一个存储在文件系统上的持久化 SQLite 数据库交互。

应用的入口是 `UserController`,它利用依赖注入,通过 `IUserService` 接口访问业务逻辑层。

```csharp
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        var usersResponse = users.Select(x => x.ToUserResponse());
        return Ok(usersResponse);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/?t=120)

该项目遵循标准的分层架构。
`UserController` 依赖于 `IUserService`,而后者又依赖于 `IUserRepository`。
数据访问由 `UserRepository` 处理,它使用 Dapper 对 SQLite 数据库执行 SQL 查询。

```text
UserController --> IUserService --> IUserRepository
                                        |
                                  UserRepository --(Dapper)--> SQLite
```

```csharp
public class UserRepository : IUserRepository
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public UserRepository(
        ISqliteDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string query = "select * from Users where Id = @Id";
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/?t=135)

依赖在 `Program.cs` 文件中注册。
大多数服务,包括仓储和数据库连接工厂,都被注册为单例,以便在整个应用中注入。

```csharp
builder.Services.AddSwaggerGen();

var dbConnectionOptionsSection = builder.Configuration.GetSection(nameof(DbConnectionOptions));
builder.Services.Configure<DbConnectionOptions>(dbConnectionOptionsSection);
builder.Services.AddSingleton<ISqliteDbConnectionFactory, SqliteDbConnectionFactory>();
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/?t=145)

为了确保数据库在应用启动时已就绪,使用了一个 `DatabaseInitializer` 类。
这个类处理 SQLite 特有的 GUID 类型映射,在 `Users` 表不存在时创建它,并为演示目的向数据库填充初始数据。

```csharp
public async Task InitializeAsync()
{
    SqlMapper.AddTypeHandler(new SqLiteGuidTypeHandler());
    SqlMapper.RemoveTypeMap(typeof(Guid));
    SqlMapper.RemoveTypeMap(typeof(Guid?));

    using var connection = await _connectionFactory.CreateDbConnectionAsync();
    await connection.ExecuteAsync("CREATE TABLE IF NOT EXISTS Users (Id TEXT PRIMARY KEY, FullName TEXT NOT NULL)");
    var nickChapsas =
        await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users where FullName = @FullName",
            new { FullName = "Nick Chapsas" });

    if (nickChapsas is null)
    {
        await connection.ExecuteAsync("INSERT INTO Users (Id, FullName) VALUES (@Id, @FullName)"
            , new { Id = Guid.NewGuid().ToString(), FullName = "Nick Chapsas" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/?t=160)

`Program.cs` 文件负责编排应用的设置,包括控制器注册、Swagger 配置以及数据库初始化逻辑的执行。

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnectionOptionsSection = builder.Configuration.GetSection(nameof(DbConnectionOptions));
builder.Services.Configure<DbConnectionOptions>(dbConnectionOptionsSection);
builder.Services.AddSingleton<ISqliteDbConnectionFactory, SqliteDbConnectionFactory>();
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/?t=190)

`UserRepository` 实现了 `IUserRepository` 接口,使用 Dapper 把结果映射到 `User` 模型,为数据库交互提供了具体实现。

```csharp
public class UserRepository : IUserRepository
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public UserRepository(
        ISqliteDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-project-we-will-be-adding-unit-tests-to-53927159/?t=205)

## 2. The scope of our testing

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-scope-of-our-testing-53927160/) · 3:59

### 总结

本课界定了多层 C# 应用中单元测试的范围,重点关注 API 层、应用层和数据层。
它评估了测试每一层的技术价值,结论是应用层(服务)由于包含业务逻辑和可观测性需求,能提供最显著的投资回报。
虽然对数据层做单元测试往往是多余的,但 API 层仍然是验证契约映射和 HTTP 响应逻辑的有价值的目标。

### 核心概念

*   **分层架构**:理解 API 层(Controllers)、应用层(Services)和数据层(Repositories)之间的区别。
*   **测试价值**:优先为包含业务逻辑的层编写单元测试,而不是那些主要管理外部依赖的层。
*   **数据层的范围**:认识到仓储往往更适合做集成测试,因为 mock 掉它们的依赖后几乎没有剩下可验证的逻辑。
*   **应用层的范围**:聚焦于业务逻辑、错误处理和可观测性(日志与指标)所在的服务。
*   **API 层的范围**:利用单元测试验证领域到契约的映射以及正确的 HTTP 状态码响应(例如 404 与 200)。

### 课程笔记

在一个标准的 C# 系统中,架构通常分为三个不同的层:API 层(Controllers)、应用层或领域层(Services),以及数据层(Repositories)。
确定测试范围需要识别出哪些层在被隔离时能提供最大的价值。

```text
API layer          (Controllers)   -> 契约映射、HTTP 状态码
Application layer  (Services)      -> 业务逻辑、异常处理、日志与指标
Data layer         (Repositories)  -> 连接管理、执行查询
```

#### The Data Layer

数据层封装了与数据库相关的操作,例如管理连接和执行查询。
在这个实现中,仓储使用 Dapper 与数据库交互。

```csharp
public async Task<IEnumerable<User>> GetAllAsync()
{
    using var connection = await _connectionFactory.CreateDbConnectionAsync();
    return await connection.QueryAsync<User>("select * from Users");
}

public async Task<User?> GetByIdAsync(Guid id)
{
    const string query = "select * from Users where Id = @Id";
    using var connection = await _connectionFactory.CreateDbConnectionAsync();
    return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-scope-of-our-testing-53927160/?t=55)

对这一层做单元测试往往价值不大。
如果数据库连接被 mock 掉,几乎就没有剩下可测试的逻辑了。
验证数据库连接确实发生以及查询是否有效,更适合通过集成测试来处理。

#### The Application Layer

大部分业务逻辑位于应用层。
这包括异常处理、指标收集和日志记录。
这一层是单元测试的主要目标。

```csharp
public async Task<IEnumerable<User>> GetAllAsync()
{
    _logger.LogInformation("Retrieving all users");
    var stopWatch = Stopwatch.StartNew();
    try
    {
        return await _userRepository.GetAllAsync();
    }
    catch (Exception e)
    {
        _logger.LogError(e, "Something went wrong while retrieving all users");
        throw;
    }
    finally
    {
        stopWatch.Stop();
        _logger.LogInformation("All users retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-scope-of-our-testing-53927160/?t=115)

在这里进行测试对可观测性至关重要。
例如,如果一个系统依赖特定的日志字符串来进行告警或监控,单元测试就能确保这些字符串在成功和失败场景中都被正确生成。
这确保了业务逻辑及其副作用(例如日志记录)的行为符合预期。

#### The API Layer

API 层处理内部领域模型与外部 API 契约之间的转换。
关于这一层应该做单元测试,还是只由集成测试覆盖,常常存在争论。

```csharp
[HttpGet("users/{id:guid}")]
public async Task<IActionResult> GetById(Guid id)
{
    var user = await _userService.GetByIdAsync(id);
    if (user is null)
    {
        return NotFound();
    }

    var userResponse = user.ToUserResponse();

    return Ok(userResponse);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-scope-of-our-testing-53927160/?t=170)

对控制器做单元测试可以提供一个快速的迭代循环,用来验证两个关键方面:
1.  **契约映射**:确保领域 `User` 对象被正确转换为 `UserResponse` 契约。
2.  **状态码**:验证 API 在资源缺失时返回 `404 Not Found`,在资源存在时返回 `200 OK`。

```csharp
namespace Users.Api.Contracts;

public class UserResponse
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = default!;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-scope-of-our-testing-53927160/?t=190)

虽然有些团队更喜欢用集成测试来做这些检查,但单元测试能为验证 API 层是否正确实现了映射和响应处理的预期逻辑提供更快的反馈循环。

## 3. Testing the application layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/) · 22:35

### 总结

本课演示如何通过聚焦 `UserService` 来对一个 C# API 的应用层进行单元测试。
它涵盖了搭建测试项目、使用 NSubstitute mock 仓储和 logger 依赖,以及用 Fluent Assertions 断言结果。
本课相当一部分内容讨论了测试静态扩展方法(特别是 `ILogger` 上的那些方法)的困难,并引入适配器(Adapter)模式,把不可测试的代码包装在一个可测试的接口中。
本课最后介绍了验证日志消息的技巧,以及断言系统能正确处理并重新抛出来自数据层的异常。

### 核心概念

- 使用 NSubstitute mock 依赖。
- 使用 `Received` 方法进行行为测试。
- 用于包装不可测试的静态扩展方法的适配器模式。
- 使用 `Arg.Is` 和 `Arg.Any` 验证日志消息及其参数。
- 使用 Fluent Assertions 测试异步异常处理和重新抛出。

### 课程笔记

本课首先创建一个名为 `Users.Api.Tests.Unit` 的单元测试项目。
该项目遵循标准的命名约定,并包含用于 mock 的 `NSubstitute` 和用于编写可读断言的 `FluentAssertions` 依赖。

被测系统(System Under Test,SUT)是 `UserService` 类。
要实例化它,我们必须 mock 它的两个依赖:`IUserRepository` 和 `ILogger<UserService>`。
最初,该服务使用的是标准的 `ILogger` 接口。

```csharp
namespace Users.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ILogger<UserService> _logger = Substitute.For<ILogger<UserService>>();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository, _logger);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=190)

第一个测试用例确保当仓储中没有数据时,`GetAllAsync` 返回一个空列表。
我们使用 NSubstitute 的 `.Returns()` 来配置 mock 仓储。

```csharp
[Fact]
public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
{
    // Arrange
    _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

    // Act
    var result = await _sut.GetAllAsync();

    // Assert
    result.Should().BeEmpty();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=250)

接下来,我们测试存在用户的场景。
使用 Fluent Assertions 的 `BeEquivalentTo`,我们可以比较集合中对象的属性,而不是检查引用相等性。

```csharp
[Fact]
public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExist()
{
    // Arrange
    var nickChapsas = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    var expectedUsers = new[]
    {
        nickChapsas
    };
    _userRepository.GetAllAsync().Returns(expectedUsers);

    // Act
    var result = await _sut.GetAllAsync();

    // Assert
    result.Should().BeEquivalentTo(expectedUsers);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=350)

#### 日志问题与适配器模式

测试 `ILogger` 时会遇到一个难题。
`LogInformation` 和 `LogError` 方法是静态扩展方法。
扩展方法很难 mock,因为它们本质上只是静态调用的语法糖。
此外,底层的 `Log` 方法使用了一个内部结构体 `FormattedLogValues`,它无法被访问用于断言。

为了解决这个问题,我们实现适配器模式。
我们创建一个 `ILoggerAdapter<T>` 接口和对应的 `LoggerAdapter<T>` 实现,用来包装标准的 `ILogger`。

```csharp
namespace Users.Api.Logging;

public interface ILoggerAdapter<TType>
{
    void LogInformation(string? message, params object?[] args);

    void LogError(Exception? exception, string? message, params object?[] args);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=730)

该实现把调用转发给实际的 logger,为 SUT 提供了一个可测试的接口。

```csharp
namespace Users.Api.Logging;

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<TType> _logger;

    public LoggerAdapter(ILogger<TType> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string? message, params object?[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogError(Exception? exception, string? message, params object?[] args)
    {
        _logger.LogError(exception, message, args);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=790)

```text
UserService --> ILoggerAdapter<TType>
                     ^
                     | implements
             LoggerAdapter<TType> --forwards--> ILogger<TType>
                                                (LogInformation / LogError
                                                 extension methods)
```

有了适配器之后,我们就可以验证特定的日志消息是否被记录。
我们使用 `Received(1)` 确保该方法恰好被调用一次,并使用 `Arg.Is` 验证字符串内容。
对于不确定的值,例如执行时间,我们使用 `Arg.Any<long>()`。

```csharp
[Fact]
public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
{
    // Arrange
    _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

    // Act
    await _sut.GetAllAsync();

    // Assert
    _logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));
    _logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=900)

#### 测试异常

最后,我们测试异常处理。
我们把仓储配置为抛出一个 `SqliteException`,并验证服务记录了错误日志并重新抛出了该异常。
这里使用 Fluent Assertions 的 `ThrowAsync` 来验证被重新抛出的异常。

```csharp
[Fact]
public async Task GetAllAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
{
    // Arrange
    var sqliteException = new SqliteException("Something went wrong", 500);
    _userRepository.GetAllAsync()
        .Throws(sqliteException);

    // Act
    var requestAction = async () => await _sut.GetAllAsync();

    // Assert
    await requestAction.Should()
        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while retrieving all users"));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-application-layer-53927162/?t=1215)

## 4. Exercise: Testing the rest of the application layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-application-layer-53927163/) · 1:24

### 总结

这个练习的重点是补全应用层 UserService 的单元测试套件。
在已有的 GetAllAsync 测试基础上,本课要求开发者为 GetByIdAsync、CreateAsync 和 DeleteByIdAsync 实现全面的测试覆盖。
实现时需要使用 NSubstitute 进行 mock、使用 FluentAssertions 进行验证,检验成功路径、null 或失败场景、使用 ILoggerAdapter 的正确日志行为,以及正确的异常传播。

### 核心概念

*   使用 NSubstitute mock 仓储和日志依赖。
*   使用 `_logger.Received()` 验证日志调用及其参数。
*   使用 `ThrowAsync` 测试异步方法和异常处理。
*   使用 FluentAssertions 断言对象等价性和集合状态。
*   把 Arrange-Act-Assert(AAA)模式应用到真实世界的服务逻辑中。

### 课程笔记

这个实践练习涉及在 `Users.Api.Tests.Unit.Application` 项目中扩展 `UserService` 的单元测试。
目标是遵循已有 `GetAllAsync` 测试中确立的模式,让服务的 CRUD 操作获得完整覆盖。

`UserService` 依赖于 `IUserRepository` 和 `ILoggerAdapter<UserService>`。
在测试涉及日志记录的方法时,你必须验证正确的消息和参数被传给了 logger,尤其是在发生异常时。
例如,当仓储抛出异常时,服务应当先记录错误日志,再重新抛出该异常。

```csharp
[Fact]
public async Task GetAllAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
{
    // Arrange
    var sqliteException = new SqliteException("Something went wrong", 500);
    _userRepository.GetAllAsync()
        .Throws(sqliteException);

    // Act
    var requestAction = async () => await _sut.GetAllAsync();

    // Assert
    await requestAction.Should()
        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    _logger.Received(1).LogError(Arg.Is(sqliteException), Arg.Is("Something went wrong while retrieving all users"));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-application-layer-53927163/?t=10)

对于标准的获取场景,测试应当同时验证数据存在的情况,以及仓储返回空集合或 null 的情况。
使用 FluentAssertions 的 `BeEmpty()` 或 `BeEquivalentTo()` 提供了一种可读的方式来验证这些结果。

```csharp
[Fact]
public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
{
    // Arrange
    _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>());

    // Act
    var result = await _sut.GetAllAsync();

    // Assert
    result.Should().BeEmpty();
}

[Fact]
public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExist()
{
    // Arrange
    var nickChapsas = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    var expectedUsers = new[]
    {
        nickChapsas
    };
    _userRepository.GetAllAsync().Returns(expectedUsers);

    // Act
    var result = await _sut.GetAllAsync();

    // Assert
    result.Should().BeEquivalentTo(expectedUsers);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-application-layer-53927163/?t=25)

#### 练习要求

为其余的服务方法实现以下测试用例:

**GetByIdAsync**
*   当用户存在时应返回该用户。
*   当用户不存在时应返回 null。
*   在获取用户时应记录正确的信息日志(包括 ID)。
*   当仓储抛出异常时应记录正确的错误消息和异常。

**CreateAsync**
*   当详细信息有效时应返回 `true` 并创建用户。
*   在创建用户时应记录正确的信息日志(包括 ID 和名称)。
*   当仓储抛出异常时应记录正确的错误消息和异常。

**DeleteByIdAsync**
*   当用户存在并被删除时应返回 `true`。
*   当用户不存在时应返回 `false`。
*   在删除用户时应记录正确的信息日志。
*   当仓储抛出异常时应记录正确的错误消息和异常。

## 5. Exercise answer: Testing the rest of the application layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/) · 15:21

### 总结

本课给出了对 UserService 类其余方法(GetByIdAsync、CreateAsync 和 DeleteByIdAsync)进行单元测试的答案。
它演示了如何针对数据存在和不存在两种情况验证返回值,如何断言特定的日志消息以正确的参数被记录(包括模板参数和执行时间),以及如何使用 NSubstitute 和 FluentAssertions 验证异常处理和错误日志。

### 核心概念

* 使用 `Arg.Any<Guid>()` 或特定值,为特定参数 mock 仓储的响应。
* 使用 `_logger.Received(1).LogInformation(...)` 验证日志消息。
* 使用 `Arg.Is<T>(...)` 验证结构化日志中的特定参数。
* 使用 `Throws` 和 `ThrowAsync` 测试异常传播和作为副作用的日志记录。
* 使用 FluentAssertions 断言布尔结果和对象等价性。
* 使用 `Arg.Any<long>()` 处理已用毫秒数这类不确定的值。

### 课程笔记

#### Testing GetByIdAsync

`GetByIdAsync` 方法针对四个主要场景进行测试:用户不存在时、用户存在时、调用时的正确日志,以及发生异常时的正确日志。

为了测试用户不存在的场景,仓储被配置为对任意 `Guid` 都返回 `null`。
然后测试断言服务也返回 `null`。

```csharp
[Fact]
public async Task GetByIdAsync_ShouldReturnNull_WhenNoUserExists()
{
    // Arrange
    _userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

    // Act
    var result = await _sut.GetByIdAsync(Guid.NewGuid());

    // Assert
    result.Should().BeNull();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=55)

当用户确实存在时,仓储被配置为在以特定 ID 调用时返回一个特定的 `User` 对象。
测试验证服务返回的结果与该用户等价。

```csharp
[Fact]
public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
{
    // Arrange
    var existingUser = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    _userRepository.GetByIdAsync(existingUser.Id).Returns(existingUser);

    // Act
    var result = await _sut.GetByIdAsync(existingUser.Id);

    // Assert
    result.Should().BeEquivalentTo(existingUser);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=150)

`GetByIdAsync` 的日志测试验证入口日志和完成日志(包括执行时间)都被记录了。
结构化日志的参数使用 `Arg.Is` 验证 ID,使用 `Arg.Any<long>()` 验证毫秒数。

```csharp
[Fact]
public async Task GetByIdAsync_ShouldLogMessages_WhenInvoked()
{
    // Arrange
    var userId = Guid.NewGuid();
    _userRepository.GetByIdAsync(userId).ReturnsNull();

    // Act
    await _sut.GetByIdAsync(userId);

    // Assert
    _logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}"),
        Arg.Is(userId));
    _logger.Received(1).LogInformation(Arg.Is("User with id {0} retrieved in {1}ms"),
        Arg.Is(userId), Arg.Any<long>());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=340)

最后,通过强制仓储抛出异常来测试异常处理。
测试确保异常被重新抛出,并且 `LogError` 以异常对象和正确的消息模板被调用。

```csharp
[Fact]
public async Task GetByIdAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
{
    // Arrange
    var userId = Guid.NewGuid();
    var sqliteException = new SqliteException("Something went wrong", 500);
    _userRepository.GetByIdAsync(userId)
        .Throws(sqliteException);

    // Act
    var requestAction = async () => await _sut.GetByIdAsync(userId);

    // Assert
    await requestAction.Should()
        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    _logger.Received(1).LogError(Arg.Is(sqliteException),
        Arg.Is("Something went wrong while retrieving user with id {0}"),
        Arg.Is(userId));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=415)

#### Testing CreateAsync

`CreateAsync` 方法的测试遵循类似的模式。
成功用例验证当仓储成功创建用户时,服务返回 `true`。

```csharp
[Fact]
public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValid()
{
    // Arrange
    var user = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    _userRepository.CreateAsync(user).Returns(true);

    // Act
    var result = await _sut.CreateAsync(user);

    // Assert
    result.Should().BeTrue();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=545)

`CreateAsync` 的日志在初始日志消息中包含用户的 ID 和全名。

```csharp
[Fact]
public async Task CreateAsync_ShouldLogMessages_WhenInvoked()
{
    // Arrange
    var user = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    _userRepository.CreateAsync(user).Returns(true);

    // Act
    await _sut.CreateAsync(user);

    // Assert
    _logger.Received(1).LogInformation(Arg.Is("Creating user with id {0} and name: {1}"),
        Arg.Is(user.Id), Arg.Is(user.FullName));
    _logger.Received(1).LogInformation(Arg.Is("User with id {0} created in {1}ms"),
        Arg.Is(user.Id), Arg.Any<long>());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=625)

`CreateAsync` 的异常测试确认创建过程中的错误被正确记录。

```csharp
[Fact]
public async Task CreateAsync_ShouldLogMessages_WhenExceptionIsThrown()
{
    // Arrange
    var user = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    var sqliteException = new SqliteException("Something went wrong", 500);
    _userRepository.CreateAsync(user)
        .Throws(sqliteException);

    // Act
    var requestAction = async () => await _sut.CreateAsync(user);

    // Assert
    await requestAction.Should()
        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    _logger.Received(1).LogError(Arg.Is(sqliteException),
        Arg.Is("Something went wrong while creating a user"));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=790)

#### Testing DeleteByIdAsync

`DeleteByIdAsync` 的测试覆盖了成功删除和用户不存在两种情况。

```csharp
[Fact]
public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExists()
{
    // Arrange
    var userId = Guid.NewGuid();
    _userRepository.DeleteByIdAsync(userId).Returns(true);

    // Act
    var result = await _sut.DeleteByIdAsync(userId);

    // Assert
    result.Should().BeTrue();
}

[Fact]
public async Task DeleteByIdAsync_ShouldNotDeleteUser_WhenUserDoesntExists()
{
    // Arrange
    var userId = Guid.NewGuid();
    _userRepository.DeleteByIdAsync(userId).Returns(false);

    // Act
    var result = await _sut.DeleteByIdAsync(userId);

    // Assert
    result.Should().BeFalse();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=735)

删除操作的日志和异常测试遵循已确立的模式,确保 ID 被正确传给 logger。

```csharp
[Fact]
public async Task DeleteByIdAsync_ShouldLogMessages_WhenInvoked()
{
    // Arrange
    var userId = Guid.NewGuid();
    _userRepository.DeleteByIdAsync(userId).Returns(true);

    // Act
    await _sut.DeleteByIdAsync(userId);

    // Assert
    _logger.Received(1).LogInformation(Arg.Is("Deleting user with id: {0}"),
        Arg.Is(userId));
    _logger.Received(1).LogInformation(Arg.Is("User with id {0} deleted in {1}ms"),
        Arg.Is(userId), Arg.Any<long>());
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=850)

```csharp
[Fact]
public async Task DeleteByIdAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
{
    // Arrange
    var userId = Guid.NewGuid();
    var sqliteException = new SqliteException("Something went wrong", 500);
    _userRepository.DeleteByIdAsync(userId)
        .Throws(sqliteException);

    // Act
    var requestAction = async () => await _sut.DeleteByIdAsync(userId);

    // Assert
    await requestAction.Should()
        .ThrowAsync<SqliteException>().WithMessage("Something went wrong");
    _logger.Received(1).LogError(Arg.Is(sqliteException),
        Arg.Is("Something went wrong while deleting user with id {0}"),
        Arg.Is(userId));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-application-layer-53927164/?t=890)

## 6. But was all that necessary?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/but-was-all-that-necessary-53927165/) · 2:10

### 总结

本课强调全面单元测试的长期价值,即便这个过程让人感觉重复。
通过围绕应用逻辑构建一套测试"护具"(harness),覆盖返回值、异常、日志和性能监控,开发者获得了重构和修改代码的信心。
这些测试确保任何改动,无论是修改一条日志消息还是对一个类进行结构性重构,只要破坏了已有的预期就会被立即发现,从而提供一张保护软件完整性的安全网。

### 核心概念

- **安全护具(Safety Harness)**:单元测试提供了一个保护层,能在任何代码改动之后立即捕获回归问题。
- **重构信心**:全面的测试覆盖让开发者可以重新组织代码(例如把逻辑移到单独的类中),而不必担心破坏功能。
- **副作用验证**:测试日志和性能监控这类非功能性需求,可以确保下游系统不受改动影响。
- **全面覆盖**:处理各种场景,包括返回值、异常以及对时间敏感的操作。

### 课程笔记

在真实世界的应用中进行单元测试,往往涉及大量的重复。
一旦理解了基本原则,为每个组件编写测试的任务就会让人觉得机械。
然而,这种重复是有意为之的,而且对于构建一个健壮的应用是必要的。

这项工作的首要目标是围绕逻辑打造一套"护具"。
例如,在测试与仓储的交互时,测试会验证系统在标准条件下行为正确。

```csharp
Arg.Is("Something went wrong while creating a user"));
}

[Fact]
public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExists()
{
    // Arrange
    var userId = Guid.NewGuid();
    _userRepository.DeleteByIdAsync(userId).Returns(true);

    // Act
    var result = await _sut.DeleteByIdAsync(userId);

    // Assert
    result.Should().BeTrue();
}

[Fact]
public async Task DeleteByIdAsync_ShouldNotDeleteUser_WhenUser
{
    // Arrange
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/but-was-all-that-necessary-53927165/?t=10)

这些测试的价值在代码库需要改动时得到了证明。
如果开发者不小心删掉了一条像 "Retrieving user with id..." 这样的日志条目,他们可能会在无意中破坏一个监控这些特定日志消息的下游系统。
同样,如果日志逻辑被改为记录一个单独的耗时指标,而不是 stopwatch 消息,单元测试就会失败,立即发出行为已改变的警报。

```csharp
_logger.LogInformation("All users retrieved in {0}ms", stopWatch.Elapse
}

public async Task<User?> GetByIdAsync(Guid id)
{
    _logger.LogInformation("Retrieving user with id: {0}", id);
    var stopWatch = Stopwatch.StartNew();
    try
    {
        return await _userRepository.GetByIdAsync(id);
    }
    catch (Exception e)
    {
        _logger.LogError(e, "Something went wrong while retrieving user with id {
        throw;
    }
    finally
    {
        stopWatch.Stop();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/but-was-all-that-necessary-53927165/?t=40)

这张安全网在重构时尤其重要。
如果为了提升模块化程度,把使用 `Stopwatch` 计时操作的逻辑移到一个单独的类中,已有的测试就能确保这次重构没有破坏服务的预期行为。

```csharp
_logger.LogInformation("User with id {0} retrieved in {1}ms", id, stopW
}

public async Task<bool> CreateAsync(User user)
{
    _logger.LogInformation("Creating user with id {0} and name: {1}", user.Id, us
    var stopWatch = Stopwatch.StartNew();
    try
    {
        return await _userRepository.CreateAsync(user);
    }
    catch (Exception e)
    {
        _logger.LogError(e, "Something went wrong while creating a user");
        throw;
    }
    finally
    {
        stopWatch.Stop();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/but-was-all-that-necessary-53927165/?t=85)

这套测试覆盖了各种复杂场景,包括不返回值的方法、抛出异常的方法以及对时间敏感的逻辑。
它还演示了如何 mock 数据库调用,以及在必要时如何使用参数匹配器忽略特定参数。

```csharp
_logger.LogError(e, "Something went wrong while creating a user");
        throw;
    }
    finally
    {
        stopWatch.Stop();
        _logger.LogInformation("User with id {0} created in {1}ms", user.Id, stop
    }
}

public async Task<bool> DeleteByIdAsync(Guid id)
{
    _logger.LogInformation("Deleting user with id: {0}", id);
    var stopWatch = Stopwatch.StartNew();
    try
    {
        return await _userRepository.DeleteByIdAsync(id);
    }
    catch (Exception e)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/but-was-all-that-necessary-53927165/?t=115)

通过保持这种严格的测试标准,开发者可以确保未来的任何改动都不会悄无声息地破坏应用的逻辑,从而提供一种让开发和维护更有信心的安全保障。

## 7. Testing the data layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-data-layer-53927166/) · 1:13

### 总结

本课解释了为什么当数据层代码主要由数据库连接管理和简单 SQL 查询组成时,对它做单元测试往往是不必要的。
它强调这类代码缺乏复杂逻辑,更适合做集成测试,在集成测试中可以验证真实的数据库交互。

### 核心概念

- 数据仓储中的逻辑极少。
- 集成测试与单元测试在数据访问方面各自的角色。
- 为什么数据库特有的逻辑(SQL、连接)不在单元测试的范围之内。
- 把测试精力集中在消费仓储的应用层上。

### 课程笔记

在一个标准的数据层实现中,仓储通常专注于两项主要任务:创建数据库连接和执行 SQL 查询。

```csharp
public UserRepository(
    ISqliteDbConnectionFactory connectionFactory)
{
    _connectionFactory = connectionFactory;
}

public async Task<IEnumerable<User>> GetAllAsync()
{
    using var connection = await _connectionFactory.CreateDbConnectionAsync();
    return await connection.QueryAsync<User>("select * from Users");
}

public async Task<User?> GetByIdAsync(Guid id)
{
    const string query = "select * from Users where Id = @Id";
    using var connection = await _connectionFactory.CreateDbCon
    return await connection.QuerySingleOrDefaultAsync<User>(que
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-data-layer-53927166/?t=10)

当仓储遵循这种模式时,编写单元测试通常不会提供显著的价值。
对这段代码做单元测试需要 mock 数据库连接和底层的查询执行,这往往导致测试的是库(例如 Dapper)的实现细节,而不是业务逻辑。

涉及数据库连接、原始 SQL 查询和数据库特有逻辑的功能被认为不在单元测试的范围之内。
这些组件更适合在更高层级上通过集成测试来验证,在那里可以验证与真实数据库或容器化数据库的交互。

单元测试的重点应当保持在应用层(例如 `UserService`)如何消费仓储抽象上。
由于仓储的行为已经被抽象在一个接口之后,应用层可以通过 mock 该接口来测试,而数据层的实现则留给集成测试覆盖。

## 8. Testing the API layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/) · 9:51

### 总结

对 API 层(具体来说是控制器)进行单元测试,是保护映射逻辑、确保 HTTP 响应码正确的一种有价值的做法。
虽然这通常由集成测试覆盖,但单元测试通过 mock 服务依赖、并针对 IActionResult 的具体实现(例如 OkObjectResult 和 NotFoundResult)进行断言,能提供更快的反馈。
这种做法确保领域对象被正确转换为 API 契约,并且控制器逻辑能恰当地处理成功和失败两种场景。

### 核心概念

- **控制器单元测试**:聚焦于验证 HTTP 状态码以及领域模型与 API 契约之间的映射。
- **Mock 依赖**:使用 NSubstitute 把控制器与底层的服务层隔离开。
- **结果类型转换**:把通用的 `IActionResult` 转换为 `OkObjectResult` 或 `NotFoundResult` 这样的具体类型,以访问 `StatusCode` 和 `Value` 等属性。
- **契约分离**:保持领域对象(例如 `User`)与 API 响应(例如 `UserResponse`)之间的区分,以防止破坏性变更。
- **Fluent Assertions**:使用 `Should().BeEquivalentTo()` 进行复杂对象比较,使用 `As<T>()` 进行类型安全的值断言。

### 课程笔记

如果已经有集成测试,测试控制器层有时会被认为是多余的,但它是映射逻辑和响应码准确性的一道关键保障。
下面的实现展示了一个标准的 `UserController`,它带有 `GetAll` 和 `GetById` 端点,依赖一个 `IUserService` 和一个映射器来返回 API 专用的契约。

```csharp
[HttpGet("users")]
public async Task<IActionResult> GetAll()
{
    var users = await _userService.GetAllAsync();
    var usersResponse = users.Select(x => x.ToUserResponse());
    return Ok(usersResponse);
}

[HttpGet("users/{id:guid}")]
public async Task<IActionResult> GetById(Guid id)
{
    var user = await _userService.GetByIdAsync(id);
    if (user is null)
    {
        return NotFound();
    }

    var userResponse = user.ToUserResponse();

    return Ok(userResponse);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/?t=10)

要测试这个控制器,先创建一个测试类,并使用 `NSubstitute` mock `IUserService` 依赖来初始化被测系统(SUT)。

```csharp
using NSubstitute;
using Users.Api.Controllers;
using Users.Api.Services;

namespace Users.Api.Tests.Unit;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly IUserService _userService = Substitute.For<IUserService>();

    public UserControllerTests()
    {        _sut = new UserController(_userService);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/?t=100)

#### Testing GetById

在测试 `GetById` 的成功用例时,测试必须验证返回了 200 OK 状态,并且响应体包含预期的 `UserResponse`。
由于控制器返回的是 `IActionResult`,必须把结果转换为 `OkObjectResult` 才能检查 `StatusCode` 和 `Value`。

```csharp
[Fact]
public async Task GetById_ReturnOkAndObject_WhenUserExists()
{
    // Arrange
    var user = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    _userService.GetByIdAsync(user.Id).Returns(user);
    var userResponse = user.ToUserResponse();

    // Act
    var result = (OkObjectResult)await _sut.GetById(user.Id);

    // Assert
    result.StatusCode.Should().Be(200);
    result.Value.Should().BeEquivalentTo(userResponse);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/?t=295)

对于用户不存在的失败用例,服务被配置为返回 null。
然后把结果转换为 `NotFoundResult`,以验证 404 状态码。

```csharp
[Fact]
public async Task GetById_ReturnNotFound_WhenUserDoesntExist()
{
    // Arrange
    _userService.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

    // Act
    var result = (NotFoundResult)await _sut.GetById(Guid.NewGuid());

    // Assert
    result.StatusCode.Should().Be(404);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/?t=430)

```text
IActionResult  --cast-->  OkObjectResult  -> StatusCode 200, Value
               --cast-->  NotFoundResult  -> StatusCode 404
```

#### Testing GetAll

在测试 `GetAll` 时,处理列表为空的情况很重要。
`OkObjectResult` 的 `Value` 属性通过 Fluent Assertions 的 `.As<IEnumerable<UserResponse>>()` 进行转换,以验证集合为空。

```csharp
[Fact]
public async Task GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
{
    // Arrange
    _userService.GetAllAsync().Returns(Enumerable.Empty<User>());

    // Act
    var result = (OkObjectResult)await _sut.GetAll();

    // Assert
    result.StatusCode.Should().Be(200);
    result.Value.As<IEnumerable<UserResponse>>().Should().BeEmpty();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/?t=510)

最后,当用户确实存在时,测试确保服务的领域对象被正确映射为响应契约,并在结果值中返回。

```csharp
[Fact]
public async Task GetAll_ShouldReturnUsersResponse_WhenUsersExist()
{
    // Arrange
    var user = new User
    {
        Id = Guid.NewGuid(),
        FullName = "Nick Chapsas"
    };
    var users = new[] { user };
    var usersResponse = users.Select(x => x.ToUserResponse());
    _userService.GetAllAsync().Returns(users);

    // Act
    var result = (OkObjectResult)await _sut.GetAll();

    // Assert
    result.StatusCode.Should().Be(200);
    result.Value.As<IEnumerable<UserResponse>>().Should().BeEquivalentTo(usersResponse);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-the-api-layer-53927167/?t=580)

## 9. Exercise: Testing the rest of the API layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-api-layer-project-source-under-the-video-53927169/) · 1:03

### 总结

本课提供了一个实践练习,用来补全一个 C# API 项目中 UserController 的单元测试套件。
在之前 GetById 和 GetAll 测试实现的基础上,这个练习要求编写四个新的测试用例,覆盖 Create 和 DeleteById 方法。
重点是验证控制器根据底层服务操作的成功或失败,返回恰当的 HTTP 状态码:201 Created、400 Bad Request、200 OK 和 404 Not Found。

### 核心概念

- 测试 HTTP POST(Create)在成功(201 Created)和失败(400 Bad Request)两种场景下的响应。
- 测试 HTTP DELETE(DeleteById)在成功(200 OK)和失败(404 Not Found)两种场景下的响应。
- 把已有的单元测试用作新测试用例的结构模板。
- Mock 服务层依赖,以模拟不同的业务逻辑结果。

### 课程笔记

要完成 API 层的单元测试,你必须为 `UserController` 中的 `Create` 和 `DeleteById` 方法实现测试。
这些方法的实现依赖 `IUserService` 来执行底层的数据操作。
`Create` 方法在成功时返回一个 `CreatedAtAction` 结果,在创建失败时返回 `BadRequest`;而 `DeleteById` 方法根据用户是否存在返回 `Ok` 或 `NotFound`。

```csharp
var created = await _userService.CreateAsync(user);
        if (!created)
        {
            // Implement validation
            return BadRequest();
        }

        var userResponse = user.ToUserResponse();

        return CreatedAtAction(nameof(GetById), new {id = userResponse.Id}, userRespon
    }

    [HttpDelete("users/{id:guid}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var deleted = await _userService.DeleteByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-api-layer-project-source-under-the-video-53927169/?t=10)

在实现这些测试时,你可以把已有的 `GetById` 单元测试作为 Arrange-Act-Assert 模式的参考。
这些测试演示了如何 mock 服务响应、调用控制器方法,并同时断言状态码和返回的对象值。

```csharp
public async Task GetById_ReturnOkAndObject_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Nick Chapsas"
        };
        _userService.GetByIdAsync(user.Id).Returns(user);
        var userResponse = user.ToUserResponse();

        // Act
        var result = (OkObjectResult)await _sut.GetById(user.Id);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task GetById_ReturnNotFound_WhenUserDoesnt()
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-api-layer-project-source-under-the-video-53927169/?t=25)

这个练习要求在 `UserControllerTests` 类中实现以下四个具体的测试用例:

```markdown
- `Create` Should return the user with a 201 status code when user was created
- `Create` Should return 400 status code when the user wasn't created
- `DeleteById` Should return 200 when the user was deleted successfully
- `DeleteById` Should return 404 when the user was not deleted
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-testing-the-rest-of-the-api-layer-project-source-under-the-video-53927169/?t=40)

## 10. Exercise answer: Testing the rest of the API layer

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/) · 13:19

### 总结

本课演示如何对一个 C# API 控制器的其余端点进行单元测试,具体聚焦于 Create 和 Delete 操作。
它讨论了测试那些在内部实例化对象的方法时的复杂性,探索了多种技巧:用 Arg.Is 进行属性匹配、在断言中排除属性,以及使用 NSubstitute 的 Arg.Do 捕获内部状态,从而精确验证状态码、响应体和路由值。

### 核心概念

* 测试 `CreatedAtActionResult` 和状态码 201。
* 在单元测试中处理内部对象的实例化。
* 在 NSubstitute 中使用 `Arg.Is<T>` 进行条件参数匹配。
* 在 FluentAssertions 中从对象比较里排除属性。
* 使用 `Arg.Do<T>` 捕获传给被 mock 依赖的参数。
* 验证 API 响应中的路由值和 location 头。
* 测试 `Delete` 操作的成功和失败路径。

### 课程笔记

API 控制器中的 `Create` 方法在调用服务层之前,会把一个 `CreateUserRequest` 映射为内部的 `User` 领域对象。
这个方法在成功时返回一个 `CreatedAtActionResult`,其中包含被创建的对象以及生成 location 头所需的路由值。

```csharp
[HttpPost("users")]
public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserReq
{
    var user = new User
    {
        FullName = createUserRequest.FullName
    };

    var created = await _userService.CreateAsync(user);
    if (!created) {
        // Implement validation
        return BadRequest();
    }

    var userResponse = user.ToUserResponse();

    return CreatedAtAction(nameof(GetById), new {id = userRespon
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=25)

测试这个方法时会遇到一个难题,因为 `User` 对象是在控制器内部实例化的。
这意味着为 ID 生成的 `Guid` 不会与测试准备阶段中任何预先定义的对象匹配,如果 NSubstitute 的 `Returns` 设置期望的是某个特定实例,它就会失效。

```csharp
{
    FullName = "Nick Chapsas"
};
var user = new User
{
    Id = Guid.NewGuid(),
    FullName = createUserRequest.FullName
};
var expectedUserResponse = user.ToUserResponse();
_userService.CreateAsync(user).Returns(true);

// Act
var result = (CreatedAtActionResult)await _sut.Create(createUserRequest);

// Assert
result.StatusCode.Should().Be(201);
result.Value.As<UserResponse>().Should().BeEquivalentTo(expected
result.RouteValues!["id"].Should().BeEquivalentTo(user.Id);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=265)

为了解决匹配问题,可以使用 NSubstitute 的 `Arg.Is<T>`,根据特定属性(例如 `FullName`)而不是对象相等性来匹配参数。

```csharp
[Fact]
public async Task Create_ShouldCreateUser_WhenCreateUserRequestIsValid()
{
    // Arrange
    var createUserRequest = new CreateUserRequest
    {
        FullName = "Nick Chapsas"
    };
    var user = new User
    {
        Id = Guid.NewGuid(),
        FullName = createUserRequest.FullName
    };
    var expectedUserResponse = user.ToUserResponse();
    _userService.CreateAsync(Arg.Is<User>(x => x.FullName == user.Ful
    
    // Act
    var result = (CreatedAtActionResult)await _sut.Create(createUser
    
    // Assert
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=355)

另一种做法是,如果 ID 对断言来说并不关键,FluentAssertions 允许使用 `Excluding` 选项在比较时排除特定属性。

```csharp
// Act
var result = (CreatedAtActionResult)await _sut.Create(createUserRequest);

// Assert
result.StatusCode.Should().Be(201);
result.Value.As<UserResponse>().Should()
    .BeEquivalentTo(expectedUserResponse, options => options.Excluding(x => x.Id)

// result.Value.As<UserResponse>().Should().BeEquivalentTo(expectedUserResponse);
// result.RouteValues!["id"].Should().BeEquivalentTo(user.Id);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=430)

一种更稳健的做法是使用 `Arg.Do<T>` 捕获在控制器内部创建的 `User` 对象。
通过把捕获到的参数赋给一个局部变量,测试就可以针对执行期间实际生成的 ID 进行断言,从而确保 `CreatedAtAction` 的路由值是正确的。

```csharp
_userService.CreateAsync(Arg.Do<User>(x => user = x)).Returns(true);

// Act
var result = (CreatedAtActionResult)await _sut.Create(createUserRequest);

// Assert
var expectedUserResponse = user.ToUserResponse();
result.StatusCode.Should().Be(201);
result.Value.As<UserResponse>().Should().BeEquivalentTo(expectedUserResponse);
result.RouteValues!["id"].Should().BeEquivalentTo(user.Id);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=595)

```text
Test                    UserController.Create          IUserService (mock)
  |  Create(request)          |                              |
  |-------------------------->| new User { FullName }        |
  |                           |  CreateAsync(user) --------->|
  |                           |                 Arg.Do<User>(x => user = x)
  |  user (captured) <-------------------------------------- |
  |  assert RouteValues["id"] == user.Id                     |
```

测试创建的失败场景要简单得多。
如果服务返回 `false`,控制器应当返回一个 `400 Bad Request` 结果。

```csharp
[Fact]
public async Task Create_ShouldReturnBadRequest_WhenCreateUserRequestIsInvalid()
{
    // Arrange
    _userService.CreateAsync(Arg.Any<User>()).Returns(false);

    // Act
    var result = (BadRequestResult) await _sut.Create(new CreateUserRequest());

    // Assert
    result.StatusCode.Should().Be(400);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=685)

最后,对 `Delete` 端点的成功和失败两种情况进行测试。
成功删除返回一个 `OkResult`(状态 200),而删除失败返回一个 `NotFoundResult`(状态 404)。

```csharp
[Fact]
public async Task DeleteById_ReturnsOk_WhenUserWasDeleted()
{
    // Arrange
    _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(true);

    // Act
    var result = (OkResult)await _sut.DeleteById(Guid.NewGuid());

    // Assert
    result.StatusCode.Should().Be(200);
}

[Fact]
public async Task DeleteById_ReturnsNotFound_WhenUserWasNotDeleted()
{
    // Arrange
    _userService.DeleteByIdAsync(Arg.Any<Guid>()).Returns(false);

    // Act
    var result = (NotFoundResult)await _sut.DeleteById(Guid.NewGuid());

    // Assert
    result.StatusCode.Should().Be(404);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/exercise-answer-testing-the-rest-of-the-api-layer-53927170/?t=775)

## 11. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927182/) · 0:42

### 总结

本课全面回顾了如何把单元测试策略应用到一个多层的 C# 应用中。
它涵盖了选择特定层进行测试的理由、应用层和 API 层的实现细节,以及在数据层省略单元测试的技术依据。

### 核心概念

* 在真实世界的项目中有策略地识别单元测试的目标。
* 为应用层实现单元测试。
* 把数据层排除在单元测试之外的理由。
* 为 API 层实现单元测试。
* 在所有软件层中应用通用的测试原则。

### 课程笔记

本节的重点是在一个结构化的 C# 项目中实际应用单元测试,强调应当把测试精力集中在哪里才能产生最大的效果。

#### Test Target Identification

并非所有代码都值得做单元测试。
在真实世界的场景中,区分逻辑密集的组件与简单的基础设施或样板代码至关重要。
这确保了测试精力被集中在应用中最可能出现 bug 的部分。

#### Application Layer

应用层包含核心业务逻辑和编排。
单元测试在这里被实现,用来验证领域需求是否得到满足。
这个过程涉及把应用逻辑与外部依赖隔离开,以确保测试执行快速而可靠。

#### Data Layer Considerations

一般会避免对数据层做单元测试。
由于数据层的主要职责是与外部系统(例如数据库)对接,而依赖隔离的单元测试无法有效验证数据库查询或表结构映射的正确性。
这些问题更适合通过集成测试来解决。

#### API Layer

对 API 层进行测试,是为了确保客户端与应用逻辑之间的接口是健壮的。
这包括验证控制器返回正确的 HTTP 状态码,以及它们正确地把任务委派给应用层。

#### Conclusion

用于测试这些层的原则在整个应用中是一致的。
通过理解如何在应用层和 API 层中 mock 依赖并断言结果,开发者可以把同样的逻辑应用到软件栈中的任何其他组件上。

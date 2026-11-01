# Unit testing concepts

> 课程:[From Zero to Hero: Unit testing for C# Developers](https://dometrain.com/course/from-zero-to-hero-unit-testing-for-csharp-developers/) · 第 5 章
> 共 6 课 · 约 19:31
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The problem with dependencies](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/) | 3:54 | [↓](#1-the-problem-with-dependencies) |
| 2 | [Dependency injection to the rescue](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/) | 2:55 | [↓](#2-dependency-injection-to-the-rescue) |
| 3 | [What are fakes?](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-are-fakes-53927124/) | 3:03 | [↓](#3-what-are-fakes) |
| 4 | [What is mocking?](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-mocking-53927125/) | 6:11 | [↓](#4-what-is-mocking) |
| 5 | [Moq vs NSubstitute](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/moq-vs-nsubstitute-53927126/) | 2:35 | [↓](#5-moq-vs-nsubstitute) |
| 6 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927136/) | 0:53 | [↓](#6-section-recap) |

## 1. The problem with dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/) · 3:54

### 总结

在类的构造函数中硬编码依赖会造成紧耦合,从而妨碍有效的单元测试。
当一个类自己实例化它的依赖(例如数据库 repository 或 logger)时,就无法把这个类的逻辑与它所依赖的基础设施隔离开来。
这会导致脆弱的测试:测试失败是由于缺少数据库表或环境配置这类外部因素,而不是被测系统(SUT)内部的逻辑错误。
要实现真正的单元测试,必须在隔离状态下测试这个单元,而不触发其依赖的副作用。

### 核心概念

*   **紧耦合(Tight Coupling)**:当一个类负责实例化它自己的依赖时就会出现,这使得在测试时很难替换这些依赖。
*   **被测系统(System Under Test,SUT)**:在单元测试中被隔离并验证的特定类或方法。
*   **基础设施依赖**:一个单元正常运行所依赖的外部系统,例如数据库、文件系统或日志框架。
*   **测试隔离**:确保单元测试只执行 SUT 的逻辑、而不依赖外部依赖的状态或可用性的做法。

### 课程笔记

在一个标准的 ASP.NET Core API 中,逻辑通常分布在控制器、服务和 repository 之间。
一种常见但有问题的模式是控制器在构造函数中直接实例化它的服务。

```csharp
[ApiController]

public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController()
    {
        _userService = new UserService();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=30)

这种手动实例化的模式会沿着调用栈一路向下延续。
`UserService` 可能会实例化一个用于数据访问的 `UserRepository` 和一个用于遥测的 `Logger`。
由于它们是在构造函数内部创建的,所以它们是该服务的硬编码依赖。

```csharp
namespace UnderstandingDependencies.Api.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService()
    {
        _userRepository = new UserRepository();
        _logger = new Logger<UserService>(new LoggerFactory());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopWatch = Stopwatch.StartNew();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=45)

服务中的 `GetAllAsync` 方法把 repository 调用包裹在一个 try-finally 块中,以确保无论结果如何都会记录性能指标。

```csharp
public async Task<IEnumerable<User>> GetAllAsync()
{
    _logger.LogInformation("Retrieving all users");
    var stopwatch = Stopwatch.StartNew();
    try
    {
        var users = await _userRepository.GetAllAsync();
        return users;
    }
    finally
    {
        stopwatch.Stop();
        _logger.LogInformation("All users retrieved in {0}ms", stopwatch.ElapsedMi
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=130)

在调用栈的最底层,`UserRepository` 管理实际的数据库连接。
它实例化一个 `SqliteDbConnectionFactory`,并对一张数据库表执行原始 SQL 查询。

```csharp
public class UserRepository
{
    private readonly SqliteDbConnectionFactory _connectionFactory;

    public UserRepository()
    {
        _connectionFactory = new SqliteDbConnectionFactory();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=70)

```text
  UsersController
       | new UserService()
       v
  UserService
       | new UserRepository()        new Logger<UserService>(...)
       v
  UserRepository
       | new SqliteDbConnectionFactory()
       v
  SQLite: "select * from Users"
```

在尝试为 `UserService` 编写单元测试时,目标是验证该服务的逻辑,例如当不存在任何用户时它会返回一个空列表。

```csharp
using System.Threading.Tasks;
using UnderstandingDependencies.Api.Services;
using Xunit;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut = new ();

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange

        // Act

        // Assert
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=145)

然而,由于 `UserService` 与 `UserRepository` 紧耦合,执行这个测试会触发真实的数据库代码。

```csharp
namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut = new ();

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=160)

运行这个测试会失败,并出现类似 "no such table: Users" 的错误。
发生这种情况是因为 `UserRepository` 试图连接一个在测试环境中并不存在的真实 SQLite 数据库。

```csharp
public class UserRepository
{
    private readonly SqliteDbConnectionFactory _connectionFactory;

    public UserRepository()
    {
        _connectionFactory = new SqliteDbConnectionFactory();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-problem-with-dependencies-53927122/?t=190)

这展示了依赖的核心问题:你无法在不同时测试其依赖(数据库)的情况下测试这个单元(服务逻辑)。
要解决这个问题,必须重构代码,使依赖在测试期间可以被替换为受控的版本。

## 2. Dependency injection to the rescue

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/) · 2:55

### 总结

依赖注入(DI)和依赖倒置是通过依赖抽象来把类与其具体实现解耦的架构模式。
通过重构代码、让它通过构造函数接收接口(这一过程称为构造函数注入),开发者可以反转控制,让软件更加灵活。
这种解耦对单元测试至关重要,因为它允许把真实的实现(例如访问数据库的 repository)替换为 fake 或 mock 这样的测试替身。

### 核心概念

- **依赖倒置(Dependency Inversion)**:高层模块不应依赖低层模块;两者都应依赖抽象。
- **构造函数注入(Constructor Injection)**:一种通过构造函数提供类的依赖、而不是在内部实例化它们的技术。
- **面向接口编程**:使用接口作为契约来定义行为,从而可以无缝地替换多种实现。
- **可测试性**:解耦依赖使得在单元测试时能够隔离逻辑。

### 课程笔记

为了解决紧耦合的问题(即一个类负责实例化它自己的依赖),我们使用依赖注入和控制反转。
在 `UserService` 的最初实现中,`UserRepository` 被硬编码在构造函数中,导致在测试时无法替换这个 repository。

```csharp
public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService()
    {
        _userRepository = new UserRepository();
        _logger = new Logger<UserService>(new LoggerFactory());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopWatch = Stopwatch.StartNew();
        try
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("All users retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/?t=25)

为可测试性而重构的第一步,是为依赖定义一个抽象。
我们创建一个接口 `IUserRepository`,它定义了任何 repository 实现都要遵守的契约。

```csharp
namespace UnderstandingDependencies.Api.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/?t=55)

接下来,更新具体的 `UserRepository`,让它实现这个接口。
这确保了 repository 遵守所定义的契约。

```csharp
namespace UnderstandingDependencies.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SqliteDbConnectionFactory _connectionFactory;

    public UserRepository()
    {
        _connectionFactory = new SqliteDbConnectionFactory();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnection();
        return await connection.QueryAsync<User>("select * from Users");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/?t=70)

有了这个接口之后,就可以重构 `UserService`,让它依赖 `IUserRepository`,而不是具体的 `UserRepository` 类。
一开始,只是把字段类型改为接口,不过在过渡期间实例化可能仍然是硬编码的。

```csharp
public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService()
    {
        _userRepository = new UserRepository();
        _logger = new Logger<UserService>(new LoggerFactory());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopWatch = Stopwatch.StartNew();
        try
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("All users retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/?t=85)

要完整地实现依赖注入,必须把依赖传入构造函数。
这让调用方可以决定服务应当使用 `IUserRepository` 的哪个实现,从而有效地反转了控制。

```csharp
namespace UnderstandingDependencies.Api.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _logger = new Logger<UserService>(new LoggerFactory());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopWatch = Stopwatch.StartNew();
        try
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("All users retrieved in {0}ms", stopWatch.ElapsedMilliseconds);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/?t=130)

```text
  caller --- new UserService(new UserRepository()) ---+
                                                      v
  UserService(IUserRepository userRepository)
       |
       | depends on
       v
  IUserRepository  <----- implements -----  UserRepository
```

这种依赖抽象的模式会贯穿整个应用。
例如,控制器可以注入 `UserService`。
虽然 .NET 提供了内置的依赖注入容器来自动管理这些生命周期,但手动实例化能说明依赖是如何沿着链条向下传递的。

```csharp
namespace UnderstandingDependencies.Api.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController()
    {
        _userService = new UserService(new UserRepository());
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/dependency-injection-to-the-rescue-53927123/?t=145)

通过依赖抽象,`UserService` 变得可以进行单元测试。
任何实现了 `IUserRepository` 的对象都可以被注入,从而可以使用 "fake" 把服务逻辑与数据库这类外部依赖隔离开来。

## 3. What are fakes?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-are-fakes-53927124/) · 3:03

### 总结

Fake 是接口的手动实现,用于在单元测试期间替换真实的依赖。
通过用硬编码的响应来实现 IUserRepository 这样的接口,开发者可以把被测系统(SUT)与数据库这类外部基础设施隔离开来。
虽然 fake 在简单场景中很有效,但它们的扩展性不好,因为每一个不同的测试用例(例如返回空列表与返回有数据的列表)都需要创建一个单独的类,从而带来维护开销和类爆炸。

### 核心概念

- **接口实现**:为测试而手动创建满足依赖契约的类。
- **隔离**:把代码与外部系统(例如数据库)解耦,以确保测试快速且可靠。
- **构造函数注入**:通过 SUT 的构造函数把 fake 实现传进去,以满足它的依赖。
- **扩展性问题**:需要为不同的测试场景创建多个 fake 类,与 mocking 相比这很低效。

### 课程笔记

当像 `UserService` 这样的类使用构造函数注入来接收它的依赖时,就可以在测试期间用一个 fake 实现替换真实的实现。
这让测试可以控制依赖的行为,而不必依赖外部系统。

```csharp
namespace UnderstandingDependencies.Api.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _logger = new Logger<UserService>(new LoggerFactory());
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopWatch = Stopwatch.StartNew();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-are-fakes-53927124/?t=10)

为了测试不存在任何用户的场景,可以创建一个 `FakeUserRepository`。
这个类实现了 `IUserRepository`,并使用 `Task.FromResult` 返回一个硬编码的空 enumerable。

```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnderstandingDependencies.Api.Models;
using UnderstandingDependencies.Api.Repositories;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class FakeUserRepository : IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(Enumerable.Empty<User>());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-are-fakes-53927124/?t=55)

在单元测试类中,通过把 fake repository 传入构造函数来实例化被测系统(SUT)。
这满足了依赖要求,并让测试可以在与真实数据库隔离的情况下运行。

```csharp
namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _sut = new UserService()
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-are-fakes-53927124/?t=70)

```text
  UserServiceTests
       |
       v
  UserService(IUserRepository)  <---  FakeUserRepository : IUserRepository
                                        GetAllAsync() ->
                                        Task.FromResult(Enumerable.Empty<User>())
```

虽然这种做法在简单的情况下可行,但当需要测试多个场景时它就无法扩展了。
例如,如果某个测试需要 repository 返回一个用户列表,就必须再创建第二个 fake 类来处理这个特定的返回值。

```csharp
public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(Enumerable.Empty<User>());
    }
}

public class FakeUserRepositoryWithUsers : IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(new []
        {
            new User
            {
                Id = Guid.NewGuid(),
                FullName = "Nick Chapsas"
            }
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-are-fakes-53927124/?t=160)

为每一种可能的返回值手动创建一个新类是低效的,并且会产生大量样板类。
这种"类爆炸"正是 fake 经常被 mocking 框架取代的主要原因,mocking 框架允许动态地定义行为,而不必承担手动创建类的开销。

## 4. What is mocking?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-mocking-53927125/) · 6:11

### 总结

Mocking 是单元测试中使用的一种技术,用于在运行时创建接口或抽象类的实现,让开发者无需编写手动的 fake 类就能定义特定的行为。
通过使用 NSubstitute 这样的库,你可以生成模拟依赖(例如 repository)的内存代理对象,并把它们配置为返回特定的数据或空集合。
这种做法把被测系统(SUT)与数据库这类外部依赖隔离开来,使测试更快、更可靠、更易于维护。

### 核心概念

*   **运行时实现**:Mocking 在执行期间为接口或带有 virtual 成员的抽象类创建实现。
*   **NSubstitute**:一个 .NET 的 mocking 框架,用于为依赖创建代理对象(substitute)。
*   **代理对象(Proxy Objects)**:内存中的实现,会严格按照测试中的配置来响应方法调用。
*   **行为配置**:使用 `.Returns()` 这样的扩展方法来定义当某个特定方法被调用时 mock 应当返回什么。
*   **隔离**:通过用 mock 替换真实的数据库调用或手动 fake 类,消除对它们的需要。

### 课程笔记

Mocking 解决了必须为每个测试场景手动创建和维护多个 fake 实现的问题。
你不必构建 `FakeUserRepository` 或 `FakeUserRepositoryWithUsers`,而是可以在运行时创建接口的 mock。
这个 mock 是一个内存中的实现,其行为完全符合特定测试用例的要求。

一开始,一个测试套件可能依赖于注入到被测系统(SUT)中的手动 fake:

```csharp
namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _sut = new UserService(new FakeUserRepository());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-mocking-53927125/?t=10)

要用 mock 替换手动 fake,你必须引入一个对 NSubstitute 这类 mocking 库的依赖。
一旦这个库可用,你就可以为接口定义一个字段,并使用 `Substitute.For<T>()` 来初始化它。
这会创建一个实现了指定接口的代理对象。

```csharp
namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    public UserServiceTests()
    {
        _sut = new UserService(new FakeUserRepository());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-mocking-53927125/?t=105)

初始化 substitute 之后,更新 SUT 的构造函数调用,让它使用这个 mock 实例。
然后在测试的 Arrange 部分,你就可以配置 mock 的行为。
例如,为了测试不存在任何用户的场景,你可以使用 `.Returns()` 扩展方法,指示 mock 在 `GetAllAsync()` 被调用时返回一个空数组。

```csharp
{
        _sut = new UserService(_userRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Array.Empty<User>());

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsers
    {
        // Arrange
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-mocking-53927125/?t=160)

当测试执行时,`UserService` 会调用 repository。
由于这个 repository 是一个 substitute,它会立即返回预先配置好的空数组,而不会尝试连接真实的数据库。
这让测试可以验证服务能正确处理空结果。

```text
  Arrange: _userRepository.GetAllAsync().Returns(Array.Empty<User>())
  Act:     _sut.GetAllAsync()
             -> UserService
             -> _userRepository (Substitute.For<IUserRepository>())
             <- Array.Empty<User>()      (no real database)
  Assert:  users.Should().BeEmpty()
```

Mocking 在测试涉及数据的场景时同样有效。
你可以定义一个预期用户的数组,并把 mock 配置为返回这个特定的集合。
这使得可以针对返回的数据进行精确的断言。

```csharp
[Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsersExist()
    {
        // Arrange
        var expectedUsers = new[]
        {
            new User
            {
                Id = Guid.NewGuid(),
                FullName = "Nick Chapsas"
            }
        };
        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().ContainSingle(x => x.FullName == "Nick Chapsas");
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-mocking-53927125/?t=325)

通过使用 NSubstitute,测试始终聚焦于 `UserService` 内部的逻辑,而外部依赖则在内存中被完全控制和模拟。

## 5. Moq vs NSubstitute

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/moq-vs-nsubstitute-53927126/) · 2:35

### 总结

本课评估 Moq 与 NSubstitute 这两个 C# 主要 mocking 库之间的差异。
它指出,虽然 Moq 被广泛使用,但与 NSubstitute 相比,它涉及多得多的"仪式感"(ceremony)和样板代码。
本课演示了 Moq 如何要求把接口包裹在一个 Mock<T> 容器中并使用显式的 .Setup() 调用,最终推荐 NSubstitute,因为它的语法更简洁、可维护性更好。

### 核心概念

* Moq 与 NSubstitute 语法的比较。
* mocking 库中"仪式感"(ceremony)的概念。
* 在 Moq 中使用 `Setup` 和 `ReturnsAsync` 配置 mock 行为。
* 在 Moq 中通过 `.Object` 属性访问底层的 mock 对象。
* 为什么 NSubstitute 能带来更易维护、更易读的测试代码。

### 课程笔记

在 C# 单元测试中,Moq 是目前使用最广泛的 mocking 库。
然而,它经常被批评"仪式感很重"(high ceremony),也就是说,与 NSubstitute 这类现代替代品相比,它需要更多的样板代码。

在 NSubstitute 中,创建一个 substitute 并配置它的行为很简洁。
接口被直接替换,行为则通过接口本身上的简单扩展方法来定义。

```csharp
namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Array.Empty<User>());

        // Act
        var users = await _sut.GetAllAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/moq-vs-nsubstitute-53927126/?t=10)

要在 Moq 中实现同样的结果,语法会更冗长。
你不是直接使用接口类型,而是必须使用一个 `Mock<T>` 包装器。
使用 Moq 时,你不能把 mock 对象直接传入被测系统(SUT)。
相反,你必须访问 mock 实例的 `.Object` 属性。

```csharp
public class UserServiceTests
{
    private readonly UserService _sut;
    //private readonly IUserRepository _userRepository = Substitute.For<IUserRepositor
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    public UserServiceTests()
    {
        _sut = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        //_userRepository.GetAllAsync().Returns(Array.Empty<User>());

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/moq-vs-nsubstitute-53927126/?t=70)

```text
  NSubstitute:  IUserRepository          --------------------> UserService
                (Substitute.For<IUserRepository>())

  Moq:          Mock<IUserRepository> --> .Object -----------> UserService
                (_userRepositoryMock)
```

在 Moq 中配置行为需要使用 `.Setup()` 方法,它通过一个 lambda 表达式来指定被 mock 的方法,随后再调用一个特定的返回方法,例如针对异步操作的 `.ReturnsAsync()`。
这增加了一层在 NSubstitute 中不存在的复杂性。

```csharp
}

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        //_userRepository.GetAllAsync().Returns(Array.Empty<User>());
        _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Array.Empty<User>

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsers
    {
        // Arrange
        var expectedUsers = new[]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/moq-vs-nsubstitute-53927126/?t=100)

虽然这两个库都能正常工作,测试也会同样通过,但通常推荐 NSubstitute,因为它的 API 更简洁、更直观。
更少的仪式感让测试随着时间推移更易读、更易维护。

```csharp
public UserServiceTests()
    {
        _sut = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        //_userRepository.GetAllAsync().Returns(Array.Empty<User>());
        _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Array.Empty<User>());

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers(
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/moq-vs-nsubstitute-53927126/?t=145)

## 6. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927136/) · 0:53

### 总结

本课总结了单元测试的核心原则,重点关注依赖管理,以及从手动 fake 到动态 mocking 框架的演进。
它强调了构造函数注入对于编写可测试代码的必要性,并确立 NSubstitute 为在 C# 项目中 mock 接口的更优工具。

### 核心概念

* 基于构造函数的依赖注入,以实现可测试性。
* 手动 fake 与动态 mock 相比的局限。
* 接口的内存实现。
* NSubstitute 作为比 Moq 更受推荐的 mocking 库。

### 课程笔记

有效的单元测试始于理解依赖如何影响代码的可测试性。
通过类的构造函数传入依赖,代码会变得容易隔离和测试得多。
这种模式让测试套件可以控制外部组件的行为,而不必依赖它们的实际实现。

虽然手动的 "fake" 提供了一种满足依赖的方式,但随着项目增长,它们往往变得笨重且难以维护。
Mock 通过提供接口的动态内存实现,给出了一个更优雅的解决方案。
这种做法让开发者能够定义预期行为并验证交互,而不必为每个依赖编写样板代码。

下面的示例演示了如何使用 NSubstitute 搭建一个测试套件,在服务测试中 mock 一个 repository 依赖:

```csharp
using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using UnderstandingDependencies.Api.Models;
using UnderstandingDependencies.Api.Repositories;
using UnderstandingDependencies.Api.Services;
using Xunit;

namespace UnderstandingDependencies.Api.Tests.Unit;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    //private readonly Mock<IUserRepository> _userRepositoryMock = n

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927136/?t=10)

比较不同的 mocking 库可以发现,NSubstitute 通常提供比 Moq 这类替代品更简洁、更直观的 API。
通过使用 `Substitute.For<T>()`,开发者可以快速生成所需的抽象,从而在隔离状态下测试被测系统(SUT)。

在这些基础概念确立之后,重点将转向在一个真实项目的所有层中应用这些技术。
这包括测试复杂的场景,以及那些乍看之下难以测试的组件,以确保对整个应用架构的全面覆盖。

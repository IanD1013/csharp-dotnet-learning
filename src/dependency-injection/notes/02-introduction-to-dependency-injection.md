# Introduction to Dependency Injection

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 2 章
> 共 8 课 · 约 32:21
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The problem with dependencies](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/) | 6:47 | [↓](#1-the-problem-with-dependencies) |
| 2 | [Why Dependency injection is necessary](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/) | 5:13 | [↓](#2-why-dependency-injection-is-necessary) |
| 3 | [A practical example of the dependency problem](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/) | 10:25 | [↓](#3-a-practical-example-of-the-dependency-problem) |
| 4 | [A less obvious example of a Dependency Injection use-case](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/) | 4:24 | [↓](#4-a-less-obvious-example-of-a-dependency-injection-use-case) |
| 5 | [Dependency injection benefits past testability](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-injection-benefits-past-testability-53953087/) | 1:11 | [↓](#5-dependency-injection-benefits-past-testability) |
| 6 | [Injecting Classes vs Abstract classes vs Interfaces](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/) | 1:51 | [↓](#6-injecting-classes-vs-abstract-classes-vs-interfaces) |
| 7 | [So do you have to do all that manually??](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/) | 1:21 | [↓](#7-so-do-you-have-to-do-all-that-manually) |
| 8 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953090/) | 1:09 | [↓](#8-section-recap) |

---

## 1. The problem with dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/) · 6:47

### 总结

本课探讨当软件组件与其依赖紧密耦合时所产生的根本性问题。
通过一个汽车建模的练习,它演示了在高层类中硬编码具体实现会如何导致僵化的继承结构和严重的测试难题。
理解这些陷阱,对于认识依赖注入为何是构建可维护、可测试的 .NET 应用所必需的模式至关重要。

### 核心概念

- **Tight Coupling(紧耦合)**:高层类直接负责实例化自己的依赖,导致难以替换实现。
- **Inheritance Bloat(继承膨胀)**:倾向于为不同的依赖类型创建专门的子类(例如 `PetrolCar`、`DieselCar`),从而导致复杂而脆弱的层次结构。
- **Testability Constraints(可测试性受限)**:由于组件被绑定到其依赖的某个特定的具体实现,无法把它隔离出来进行测试。
- **High-Level vs. Low-Level Modules(高层模块与低层模块)**:高层模块(比如 `Car`)不应该依赖低层的实现细节(比如某个具体的 `PetrolEngine`)。

### 课程笔记

#### 建模的挑战

在 C# 中对汽车这类系统建模时,常见的做法是按对象的区分特征来分类,比如发动机类型(汽油、柴油或电动)。
一种常见但有缺陷的做法,是用继承为每一种变体创建具体的类型。

例如,你可能会创建一个抽象的 `Car` 类,然后派生出 `PetrolCar`、`DieselCar` 和 `ElectricCar`。
这会带来建模上的挑战,因为任何新增的区分特征都会导致继承层次越来越深。
`ElectricCar` 可能还需要进一步拆分成 `BatteryElectricCar` 或 `HydrogenElectricCar`。
这种模型很脆弱;对父类的修改会在整个层次结构中产生显著的连锁反应。

#### 紧耦合与实例化

核心问题不只是继承,还在于这些类内部是如何管理依赖的。
在一个紧耦合的系统里,高层类(`Car`)要负责实例化自己的低层依赖(发动机)。

```csharp
namespace TheDependencyProblem.CarExample;

public class Car
{
    private readonly PetrolEngine _carEngine = new();

    public void StartEngine()
    {
        _carEngine.Start();
    }

    //More methods
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/?t=275)

如果你决定做一个柴油版本的汽车,就不得不创建一个新类,或者修改现有的类,以容纳另一种具体的发动机类型。

```csharp
public class PetrolCar
{
    private readonly PetrolEngine _engine = new();

    public void StartEngine()
    {
        _engine.Start();
    }
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/?t=285)

要支持另一种燃料类型,你需要定义一个新的发动机类和一个与之对应的汽车类:

```csharp
public class DieselEngine
{
    public void Start()
    {
        //Ignites based on pressure
    }
}

public class DieselCar
{
    private readonly DieselEngine _engine = new();

    public void StartEngine()
    {
        _engine.Start();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-problem-with-dependencies-53953083/?t=295)

在这种情况下,`Car` 已经不再只是一辆车了;因为它被绑定到了具体实现,它具体成了 `PetrolCar` 或者 `DieselCar`。
理想情况下,一辆车就应该只是一辆*拥有*发动机的车。

#### 对可测试性的影响

依赖具体实现会让一个系统无法被隔离测试。
如果你需要在实验室环境中测试汽车的车轮转动系统或转向系统,而这辆车又直接依赖 `PetrolEngine`,那你就做不到。
如果发动机还没准备好或者拿不到,车就发动不了,轮子也转不起来。

由于高层模块(`Car`)依赖的是实现而不是抽象,你无法出于测试目的把真实的发动机替换成一个 "mock" 或者一个简单的车轮转动装置。
这种对整洁架构原则的违背,使得你不可能在不牵扯所有低层依赖的情况下验证高层逻辑。

依赖注入正是这个问题的解决方案,它让高层模块依赖抽象,从而解耦各个组件,并实现完整的可测试性。

---

## 2. Why Dependency injection is necessary

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/) · 5:13

### 总结

依赖注入(DI)通过确保高层组件依赖抽象而不是具体实现,来解决紧耦合的问题。
借助接口,系统会变得模块化且可测试,开发者可以在不修改消费方类的前提下替换实现,比如把真实的发动机换成一个测试替身。
这种做法把依赖的创建与依赖的使用解耦开来,有助于形成更整洁的架构和更轻松的维护。

### 核心概念

- **Abstractions over Implementations(抽象优先于实现)**:高层模块不应该依赖低层的实现细节。
- **Interface Contracts(接口契约)**:使用 `ICarEngine` 这样的接口,定义了任何实现都必须满足的一组行为。
- **Constructor Injection(构造函数注入)**:通过类的构造函数传入依赖,使得消费方可以提供任何符合所需抽象的实现。
- **Testability(可测试性)**:DI 让我们可以使用 "fake" 或 "test" 实现,从而在没有外部副作用的情况下隔离地测试代码。
- **Modularity(模块化)**:系统变得更灵活,因为替换组件对代码库其余部分的影响极小。

### 课程笔记

在一个紧耦合的系统里,像 `Car` 这样的高层组件可能会直接实例化自己的依赖,比如 `PetrolEngine`。
这会形成对某个特定实现的硬依赖,使系统变得僵化,难以扩展和测试。

```csharp
namespace TheDependencyProblem.CarExample;

public class Car
{
    private readonly PetrolEngine _engine = new();

    public void StartEngine()
    {
        _engine.Start();
    }
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=10)

要解决这个问题,应该通过引入一个抽象来反转依赖。
像 `ICarEngine` 这样的接口,定义了任何发动机都必须遵循的契约,具体来说就是具备 `Start()` 的能力。
任何想要充当发动机的类,都必须实现这个接口。

```csharp
public void StartEngine()
    {
        _engine.Start();
    }
}

public interface ICarEngine
{
    void Start();
}

public class PetrolEngine
{
    public void Start()
    {
        //Battery, induction coil, spark plug, fuel
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=75)

接口定义好之后,`PetrolEngine` 和 `DieselEngine` 这样的具体实现就可以去实现它。
接着把 `Car` 类重构为通过构造函数接收一个 `ICarEngine`。
这就是所谓的构造函数注入。
只要传进来的对象遵守 `ICarEngine` 契约,`Car` 就不再知道、也不再关心自己用的是哪一种具体的发动机。

```csharp
public class Car
{
    private readonly ICarEngine _carEngine;

    public Car(ICarEngine carEngine)
    {
        _carEngine = carEngine;
    }

    public void StartEngine()
    {
        _carEngine.Start();
    }
}

public interface ICarEngine
{
    void Start();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=160)

这种模式显著提升了可测试性。
在测试环境中,可以创建一个实现了 `ICarEngine` 的 `TestEngine`,它不需要真实的燃料,也不需要复杂的准备工作。
这让 `Car` 模块可以脱离真实发动机实现的复杂性,被隔离地测试。

```csharp
void Start();

        //Keep adding
    }

    public class TestEngine : ICarEngine
    {
        public void Start()
        {
            
        }
    }

    public class PetrolEngine : ICarEngine
    {
        public void Start()
        {
            //Battery, induction coil, spark plug, fuel
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=250)

最后,消费方代码(比如 `Program.cs`)负责把具体实现 "注入" 到 `Car` 中。
这让应用变得模块化,因为可以在应用的入口处换用不同的发动机类型,而不必改动 `Car` 类的内部逻辑。

```csharp
using TheDependencyProblem.CarExample;

var dieselCar = new Car(new DieselEngine());
var petrolCar = new Car(new PetrolEngine());


var testCar = new Car(new TestEngine());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-dependency-injection-is-necessary-53953084/?t=265)

---

## 3. A practical example of the dependency problem

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/) · 10:25

### 总结

本课用一个数据库驱动的例子,演示了 .NET 应用中紧耦合带来的实际后果。
它说明了硬编码 `UserRepository` 或 `SqliteDbConnectionFactory` 这类具体实现,会如何阻碍单元测试并限制模块化。
通过应用依赖注入的原则,具体来说就是借助构造函数注入实现控制反转并使用接口,开发者可以把业务逻辑与基础设施解耦,从而能够使用 mock 对象和可互换的数据库提供程序。

### 核心概念

- 紧耦合与松耦合
- 通过构造函数注入实现控制反转(IoC)
- 面向抽象(接口)编程,而不是面向实现编程
- 可测试性:通过 mock 依赖来隔离业务逻辑
- 模块化:在不改动业务逻辑的情况下替换基础设施组件

### 课程笔记

在典型的 .NET 应用中,业务逻辑常常依赖数据库这类外部基础设施。
如果没有依赖注入(DI),这些依赖通常会被硬编码,造成组件之间紧耦合的 "依赖问题"。

来看一个直接实例化 `UserRepository` 的 `UserService`。
这让这个服务依赖于一个特定的实现,而不是一个抽象:

```csharp
public class UserService
{
    private readonly UserRepository _userRepository = new();

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Do stuff here
        var users = await _userRepository.GetAllAsync();
        // Do stuff here
        return users;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        // Do stuff here
        var user = await _userRepository.GetByIdAsync(id);
        // Do stuff here
        return user;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=55)

随着我们深入到调用栈更下层,问题会进一步加剧。
`UserRepository` 自身也常常通过直接实例化一个工厂,被绑定到某种特定的数据库技术上,比如 SQLite:

```csharp
namespace TheDependencyProblem.Data;

public class UserRepository
{
    private readonly SqliteDbConnectionFactory _connectionFactory = new();

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=100)

这种紧耦合主要有两方面的问题。
第一,它妨碍了**可测试性**;没有一个真实运行的数据库,你就无法对 `UserService` 做单元测试,因为你没办法拦截对仓储的调用。
第二,它降低了**模块化程度**;要从 SQLite 切换到 MySQL 之类的另一种提供程序,就得手工修改仓储的代码。

为了解决这个问题,我们引入抽象。
我们定义一个 `IDbConnectionFactory` 接口,并让具体的工厂去实现它:

```csharp
namespace TheDependencyProblem.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateDbConnectionAsync();
}

public class SqliteDbConnectionFactory : IDbConnectionFactory
{
    private readonly DbConnectionOptions _connectionOptions;

    public SqliteDbConnectionFactory()
    {
        _connectionOptions = new DbConnectionOptions
        {
            ConnectionString = "Data Source=./database.db"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=175)

接下来,我们把 `UserRepository` 重构为使用**构造函数注入**。
仓储不再自己创建工厂,而是通过构造函数接收一个 `IDbConnectionFactory`。
这就是所谓的反转控制:

```csharp
public class UserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=205)

这让仓储可以与不同的数据库提供程序互换配合使用。
例如,现在可以在不改动仓储内部逻辑的情况下使用 `MySqlDbConnectionFactory`:

```csharp
public class MySqlDbConnectionFactory : IDbConnectionFactory
{
    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        return new MySqlConnection();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=265)

最后,我们通过引入 `IUserRepository` 接口,把同样的原则应用到 `UserService` 上。
这让服务与具体的数据访问实现解耦:

```csharp
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Do stuff here
        var users = await _userRepository.GetAllAsync();
        // Do stuff here
        return users;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=430)

有了这些抽象之后,代码就变得完全可测试了。
在单元测试项目中,我们可以提供一个 `FakeUserRepository`,它用一个内存中的字典来代替真实的数据库:

```csharp
public class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult(_users.Values);
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=505)

更贴近实际的做法是,开发者会使用 **NSubstitute** 这类 mock 库来动态定义行为。
这样就不必为每一种测试场景手工创建 fake 类:

```csharp
public class UserRepositoryTests
{
    private readonly UserService _userService;
    private readonly IUserRepository _mockUserRepository = Substitute.For<IUserRepository>();

    public UserRepositoryTests()
    {
        _userService = new UserService(_mockUserRepository);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-practical-example-of-the-dependency-problem-53953085/?t=565)

通过依赖抽象并把控制反转到构造函数,应用变得模块化、可维护、易于测试,单元测试时也不再需要数据库这类外部依赖在场。

---

## 4. A less obvious example of a Dependency Injection use-case

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/) · 4:24

### 总结

本课演示如何把依赖倒置原则应用到那些常被忽视的系统级依赖上,具体来说就是系统时钟。
通过把 DateTime.Now 抽象到一个接口背后,开发者可以把不确定、不可测试的代码,变成一个模块化的系统,其中基于时间的逻辑可以借助 mock 的提供程序通过单元测试来验证。

### 核心概念

- 识别对系统资源的隐藏依赖。
- 不确定性代码在单元测试中带来的问题。
- 使用 IDateTimeProvider 接口抽象 DateTime.Now。
- 为生产环境实现一个 SystemDateTimeProvider。
- 通过注入抽象来获得模块化与可测试性。

### 课程笔记

依赖注入的一些使用场景一眼就能看出来,另一些则涉及那些明明就摆在眼前、却容易被忽略的系统级实现。
一个典型的例子,就是通过 `DateTime.Now` 使用系统时钟。

来看一个 `Greeter` 类,它有一个根据当前时间生成问候语的方法:

```csharp
public class Greeter
{
    public string CreateGreetMessage()
    {
        var dateTimeNow = DateTime.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=25)

在标准的运行环境中,这个类能按预期工作。
例如,如果在早上调用它,它会正确地返回 "Good morning"。

```csharp
using ...

var greeter = new Greeter();

var message = greeter.CreateGreetMessage();

Console.WriteLine(message);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=70)

然而,这个实现有一个严重的缺陷:它无法针对所有场景做单元测试。
由于这个类与系统时钟(`DateTime.Now`)紧耦合,开发者除非在那个特定的时间段运行测试,否则没办法写测试去验证 "Good afternoon" 这条逻辑。
这段代码依赖的是操作系统的一个具体实现细节,而不是一个抽象。

要解决这个问题,就应用依赖倒置原则,为日期时间提供程序创建一个抽象。
这需要定义一个接口,以及一个包装系统时钟的具体实现。

```csharp
public interface IDateTimeProvider
{
    public DateTime DateTimeNow { get; }
}

public class SystemDateTimeProvider
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=175)

把 `Greeter` 类重构为通过构造函数接收 `IDateTimeProvider` 接口。
这让这个类可以使用该提供程序的任意实现,无论是真实的系统时钟,还是用于测试的 mock 版本。

```csharp
public Greeter(IDateTimeProvider dateTimeProvider)
{
    _dateTimeProvider = dateTimeProvider;
}

public string CreateGreetMessage()
{
    var dateTimeNow = DateTime.Now;
    return dateTimeNow.Hour switch
    {
        >= 5 and < 12 => "Good morning",
        >= 12 and < 18 => "Good afternoon",
        _ => "Good evening"
    };
    }
}

public interface IDateTimeProvider
{
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=235)

注意,要完整落实这项改动,还必须把 `CreateGreetMessage` 方法改为使用注入进来的 `_dateTimeProvider.DateTimeNow`,而不是静态的 `DateTime.Now` 调用。

最后,应用可以通过注入具体的 `SystemDateTimeProvider` 来完成初始化。

```csharp
using ...


var greeter = new Greeter(new SystemDateTimeProvider());

var message = greeter.CreateGreetMessage();

Console.WriteLine(message);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/a-less-obvious-example-of-a-dependency-injection-use-case-53953086/?t=225)

由于依赖的是抽象,代码变得模块化且完全可测试,因为现在可以注入一个 mock 的提供程序,返回某个测试用例所需的任意特定时间。

---

## 5. Dependency injection benefits past testability

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-injection-benefits-past-testability-53953087/) · 1:11

### 总结

依赖注入(DI)带来的重要优势不止于单元测试,还包括对依赖生命周期的精细管理,以及 Decorator、Interceptor 这类结构型设计模式的实现。
通过把抽象与它们的实现解耦,DI 让代码更整洁、更可复用,并借助更好的可测试性让重构更安全。

### 核心概念

- **Lifetime Management(生命周期管理)**:DI 容器允许精确控制依赖的生命周期(例如 Singleton、Scoped 或 Transient)。
- **Structural Design Patterns(结构型设计模式)**:DI 便于使用 Decorator 与 Interceptor,让横切关注点能够被干净地处理。
- **Code Reusability(代码可复用性)**:依赖抽象而不是具体实现,会让代码更模块化、更可复用。
- **Refactoring Safety(重构安全性)**:即便可测试性是唯一的好处,能够写出可靠的单元测试也让重构代码安全得多。

### 课程笔记

可测试性往往是依赖注入最直接的好处,但它并不是唯一的好处。
DI 是若干高级架构模式与实践的基础,而这些模式与实践能提升整个代码库的质量与可维护性。

其中一个主要优势,是能够管理依赖的**生命周期**。
不再由类自己负责创建和销毁自己的依赖,而是由 DI 容器来管理这些生命周期,确保资源按照应用的需要被正确地共享或释放。

此外,DI 使得 **Decorator** 与 **Interceptor** 模式的实现成为可能。
这些模式让开发者可以包装或拦截对依赖的调用,在不修改原有实现的情况下增加日志、缓存或校验之类的功能。
之所以能做到这一点,是因为消费方类依赖的是一个抽象(接口),而不是某个特定的具体类。

```csharp
public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}

public interface IDateTimeProvider
{
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/dependency-injection-benefits-past-testability-53953087/?t=10)

即便可测试性是唯一的好处,它也仍然是采用 DI 的一个关键理由。
易于测试的代码在本质上就更安全。
当单元测试足够全面时,开发者可以放心地重构逻辑,因为任何回归问题都会被立刻发现。
DI 让这一点更容易做到,因为它允许在测试时注入 `IDateTimeProvider` 这类接口的 mock 或 stub 实现。

---

## 6. Injecting Classes vs Abstract classes vs Interfaces

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/) · 1:51

### 总结

本课探讨在 .NET 中注入具体类、抽象类和接口之间的技术差异与架构差异。
虽然依赖注入容器在技术上可以解析具体类型,但遵循依赖倒置原则要求我们依赖抽象,以确保代码保持解耦、可测试和可维护。
接口是推荐的选择,因为它们完全可以被 mock,也契合组合优于继承的原则,不过应该根据是否需要可测试性或可替换性来务实地使用它们。

### 核心概念

- **Dependency Inversion Principle (DIP,依赖倒置原则)**:高层组件应该依赖抽象,而不是依赖具体实现。
- **Concrete Injection(注入具体类)**:注入某个特定的类(例如 `SystemDateTimeProvider`)在技术上是可行的,但会把消费方耦合到那个特定实现上。
- **Abstract Classes(抽象类)**:它们提供了一定程度的抽象,但无法被完全 mock,而且会强制形成继承层次。
- **Interfaces(接口)**:C# 中首选的抽象方式,因为它们完全可以被 mock,并且支持组合优于继承。
- **Pragmatic Abstraction(务实的抽象)**:应该在需要单元可测试性或可替换性时使用接口,而不是把它当成每个类的默认选项。

### 课程笔记

在 .NET 中,你可以灵活地注入具体类、抽象类或接口。
虽然依赖注入容器这三种都能处理,但这个选择会显著影响应用的架构和可测试性。

来看一个依赖于接口的 `Greeter` 类:

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=10)

如果你把构造函数改成接收 `SystemDateTimeProvider` 这样的具体实现,代码仍然能正常工作,而且在技术上也确实 "注入" 了一个依赖。
然而,这种做法违背了依赖倒置原则。
依赖某个特定实现,就失去了抽象带来的好处。

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(SystemDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=25)

抽象类在一定程度上可以充当抽象。
不过在 C# 生态中,通常还是更推荐接口。
在很多测试框架中,抽象类无法被完全 mock,而接口可以。
此外,现代 C# 开发倾向于组合优于继承;抽象类会强制形成一套继承层次,而它可能随着时间推移变得僵化、越来越不理想。

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(DateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=70)

最终的建议是,优先选择接口而不是具体类或抽象类,因为接口让单元测试和替换实现都容易得多。
不过,你并不需要为每一个类都做一个接口。
是否创建接口,应该取决于这个组件是否需要可被单元测试,或者它的实现是否需要可被替换。

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/injecting-classes-vs-abstract-classes-vs-interfaces-53953088/?t=85)

---

## 7. So do you have to do all that manually??

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/) · 1:21

### 总结

本课讨论手工做依赖注入是否现实,并介绍依赖注入(DI)框架(或称 IoC 容器)作为 .NET 中的标准解决方案。
虽然手工实例化在技术上是可行的,但 .NET 提供了一个内置的高性能 DI 框架,它会自动完成对象创建与生命周期管理,比如单例和作用域服务,从而让开发者不必再手工把依赖接起来。

### 核心概念

- **DI Framework / IoC Container(DI 框架 / IoC 容器)**:一个自动完成依赖创建与注入的库或工具。
- **Manual Injection(手工注入)**:手工实例化类,并通过构造函数把它们的依赖传进去的过程。
- **Service Lifetimes(服务生命周期)**:对一个对象存活多久的管理(例如 Singleton、Scoped 或 Transient),这由 DI 框架自动处理。
- **Built-in .NET Support(.NET 内置支持)**:ASP.NET Core 和现代 .NET 开箱即带一个原生的高性能 DI 容器。

### 课程笔记

在实现依赖注入时,一个常见的问题是:开发者是不是必须手工实例化每一个类及其依赖。
在一个简单的场景中,你可能有一个像 `Greeter` 这样的类,它依赖 `IDateTimeProvider` 来根据一天中的时间决定合适的问候语:

```csharp
namespace TheDependencyProblem;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/?t=10)

要使用这个类,传统做法是每次想用这个服务时,都手工创建依赖的实现并把它传入构造函数:

```csharp
using ...

var greeter = new Greeter(new SystemDateTimeProvider());

var message = greeter.CreateGreetMessage();

Console.WriteLine(message);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/so-do-you-have-to-do-all-that-manually-53953089/?t=25)

虽然这种手工做法在技术上可行,但随着应用不断变大,它会变得难以管理。
管理对象的生命周期,比如确保某个服务表现为单例、或者只存在于很窄的作用域内,需要大量样板代码和手工跟踪。

为了解决这个问题,开发者会使用依赖注入(DI)框架,也就是所谓的控制反转(IoC)容器。
这个工具作为一个库,自动管理服务的实例化与注入。
自 .NET Core 1.0 起,微软就内置了一个既快速又功能丰富的 DI 框架。
这个框架是现代 .NET 开发(包括 ASP.NET Core)的标准。
它免去了手工接线的必要,让开发者专注于应用逻辑,而由容器来处理依赖解析与生命周期管理的复杂性。

---

## 8. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953090/) · 1:09

### 总结

本课对 .NET 中依赖注入(DI)的基础原则做了一次全面回顾。
它回顾了 DI 所要解决的核心问题,特别是在数据库、I/O 和网络这些场景下的问题,并探讨了它在日期时间提供程序这类系统级依赖上的应用。
本课强调了 DI 在可测试性之外的好处,并介绍了 .NET 内置的依赖注入框架,它是该平台所有现代版本中管理依赖的标准机制。

### 核心概念

- 识别依赖注入所解决的架构问题。
- 常见的 DI 使用场景:数据库、I/O 操作和网络。
- 系统级 DI:抽象 DateTime 这类系统组件。
- DI 实现方式的比较:具体类、抽象类和接口。
- .NET 内置依赖注入框架的作用与稳定性。

### 课程笔记

本章通过识别依赖注入(DI)所解决的架构挑战,确立了它的根本必要性。
在现代软件开发中,当需要管理与数据库、文件 I/O 和网络服务等外部资源的交互时,DI 尤为关键。

DI 的一个不那么明显、但同样重要的使用场景,涉及系统级依赖。
例如,为 DateTime 使用一个提供程序,可以让开发者把自己的逻辑与系统时钟解耦,这对于编写确定性的测试和管理时间敏感的逻辑来说至关重要。

DI 显著提升了可测试性,但它带来的架构层面的好处更为广泛。
在实现 DI 时,开发者可以使用具体类、抽象类或接口。
基于接口的 DI 是推荐的做法,它能获得最高程度的解耦与灵活性。

在 .NET 生态中,很少需要手工实现 DI 模式。
这个平台内置了一个依赖注入框架,自 .NET Core 1.0 起它就是核心组件。
这个框架在 .NET 5、.NET 6 及未来版本中保持一致,为管理对象生命周期与依赖提供了稳定的基础。

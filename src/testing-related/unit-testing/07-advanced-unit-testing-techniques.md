# Advanced unit testing techniques

> 课程:[From Zero to Hero: Unit testing for C# Developers](https://dometrain.com/course/from-zero-to-hero-unit-testing-for-csharp-developers/) · 第 7 章
> 共 11 课 · 约 41:24
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Observing the default test execution behavior](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/observing-the-default-test-execution-behavior-53927196/) | 2:41 | [↓](#1-observing-the-default-test-execution-behavior) |
| 2 | [The Class Fixture](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/) | 4:12 | [↓](#2-the-class-fixture) |
| 3 | [The Collection Fixture](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/) | 5:04 | [↓](#3-the-collection-fixture) |
| 4 | [Running test in parallel](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/) | 5:43 | [↓](#4-running-test-in-parallel) |
| 5 | [Advanced parameterization](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/) | 4:53 | [↓](#5-advanced-parameterization) |
| 6 | [Setting test execution timeouts](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/setting-test-execution-timeouts-53927202/) | 1:17 | [↓](#6-setting-test-execution-timeouts) |
| 7 | [Testing Date and Time](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/) | 5:19 | [↓](#7-testing-date-and-time) |
| 8 | [Continuous testing](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/continuous-testing-53927204/) | 1:55 | [↓](#8-continuous-testing) |
| 9 | [What is code coverage?](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-code-coverage-53927205/) | 2:57 | [↓](#9-what-is-code-coverage) |
| 10 | [Measuring code coverage](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/) | 6:35 | [↓](#10-measuring-code-coverage) |
| 11 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927207/) | 0:48 | [↓](#11-section-recap) |

## 1. Observing the default test execution behavior

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/observing-the-default-test-execution-behavior-53927196/) · 2:41

### 总结

默认情况下,xUnit 通过为它执行的每一个测试方法各实例化一次测试类来保证测试隔离。
在单个测试类内部,xUnit 把这些测试视为一个集合(collection),即使它们是异步的,也会按顺序执行。
并行化是在类的层面实现的;位于不同类中的测试被视为相互独立的集合,并由测试运行器并发执行,从而在隔离性与性能之间取得平衡。

### 核心概念

- **按测试实例化(Per-test Instantiation)**:xUnit 为每一个 `[Fact]` 创建测试类的一个全新实例,确保实例级别的状态(例如字段)不会在测试之间共享。
- **类内顺序执行(Sequential Class Execution)**:默认情况下,定义在同一个类中的测试一个接一个地运行。
- **集合并行执行(Parallel Collection Execution)**:xUnit 并行运行不同的测试类(集合),以提升执行速度。
- **测试集合(Test Collections)**:默认的分组机制,一个类中的所有测试都属于同一个集合。

### 课程笔记

在 xUnit 中,测试执行的默认行为被设计为提供最大程度的隔离。
当一个测试类包含多个测试时,xUnit 会为每个测试分别实例化这个类。
这意味着,任何在构造函数中或通过内联初始化而初始化的字段,在每次测试运行中都会有一个唯一的值。

```csharp
using ...

namespace AdvancedTechniques.Tests.Unit;


public class DefaultBehaviorTests
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
    }

    [Fact]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/observing-the-default-test-execution-behavior-53927196/?t=10)

如果你运行这个类中的多个测试,由于这种按测试实例化的机制,每个测试输出的 `_id` 都会不同。

```text
  ExampleTest1 --> new DefaultBehaviorTests() --> _id = Guid A
  ExampleTest2 --> new DefaultBehaviorTests() --> _id = Guid B
```

#### Sequential Execution within a Class

默认情况下,同一个类中的测试被归为单个"集合"。
这种分组决定了这些测试会按顺序运行。
即使使用 `async Task` 和 `await Task.Delay`,这些测试在执行上也不会重叠。

```csharp
using ...

namespace AdvancedTechniques.Tests.Unit;

public class DefaultBehaviorTests
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    [Fact]
    public async Task ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
        await Task.Delay(2000);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/observing-the-default-test-execution-behavior-53927196/?t=60)

当同一个类中存在多个异步测试时,它们会一个接一个地执行。
例如,如果三个测试各有两秒的延迟,总执行时间大约会是六秒,因为它们并没有并行运行。

```csharp
private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
        await Task.Delay(2000);
    }

    [Fact]
    public async Task ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
        await Task.Delay(2000);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/observing-the-default-test-execution-behavior-53927196/?t=80)

#### Parallel Execution across Classes

要启用并行执行,测试必须放在不同的类中。
xUnit 把不同的类视为相互独立的集合。
当测试被拆分到多个类中时,测试运行器就可以并发地执行它们,因为它们不再共享同一个执行范围。

```csharp
}

public class DefaultBehaviorTests2
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;

    public DefaultBehaviorTests2(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/observing-the-default-test-execution-behavior-53927196/?t=130)

在这种场景下,如果一个测试位于 `DefaultBehaviorTests` 中,另一个测试位于 `DefaultBehaviorTests2` 中,它们会并行运行;与把它们放在单个类中运行相比,这会显著减少完成整个测试套件所需的总时间。

```text
  DefaultBehaviorTests   : test  --+
                                   +--> 并行运行
  DefaultBehaviorTests2  : test  --+
```

## 2. The Class Fixture

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/) · 4:12

xUnit 中的类夹具(class fixture)提供了一种机制,用于在单个测试类内的所有测试之间共享状态和上下文。
通过实现 IClassFixture<T> 接口并使用构造函数注入,开发者可以确保那些开销很大的资源(例如数据库连接或容器化服务)在每次测试类执行中只初始化一次,而不是为每个测试方法各初始化一次。
这种做法在保持测试类实例自身隔离性的同时,优化了性能和资源使用。

### 核心概念

- **共享上下文(Shared Context)**:允许同一个类中的测试共享数据或状态。
- **IClassFixture<T>**:用于表明某个测试类应当共享一个夹具实例的接口。
- **构造函数注入(Constructor Injection)**:xUnit 向测试类提供夹具实例的方式。
- **生命周期管理(Lifecycle Management)**:夹具在任何测试运行之前实例化一次,并在该类中的所有测试都完成之后被释放。
- **资源效率(Resource Efficiency)**:非常适合数据库初始化或 Docker 容器管理这类繁重的准备工作。

### 课程笔记

默认情况下,xUnit 会为每一个测试方法创建测试类的一个新实例。
这确保了在类中实例化的内容不会在测试之间共享,正如每个实例生成唯一标识符所展示的那样。

```csharp
public class DefaultBehaviorTests
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
    }

    [Fact]
    public void ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=25)

要在测试之间共享上下文,你必须创建一个单独的夹具类。
这个类将持有共享的状态或逻辑,例如一个数据库连接或一个共享的 Guid。

```csharp
using System;

namespace AdvancedTechniques.Tests.Unit;

public class MyClassFixture
{
    public Guid Id { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=70)

要使用这个夹具,测试类必须实现 `IClassFixture<T>` 接口,其中 `T` 是夹具类。
然后夹具实例会通过测试类的构造函数注入进来。

```csharp
using ...

namespace AdvancedTechniques.Tests.Unit;

public class DefaultBehaviorTests : IClassFixture<MyClassFixture>
{
    //private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _fixture;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper,
        MyClassFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=115)

夹具的构造函数在整个测试类的执行过程中只会被调用一次。
共享的准备代码应当放在这里。

```csharp
using System;

namespace AdvancedTechniques.Tests.Unit;

public class MyClassFixture
{
    public Guid Id { get; } = Guid.NewGuid();

    public MyClassFixture()
    {
        
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=135)

如果共享上下文需要在所有测试完成之后进行清理,夹具类就应当实现 `IDisposable`。
`Dispose` 方法会在该类中的所有测试完成之后被调用一次。

```csharp
public class MyClassFixture : IDisposable
{
    public Guid Id { get; } = Guid.NewGuid();

    public MyClassFixture()
    {
        
    }

    public void Dispose()
    {
        // Cleanup code here
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=175)

测试执行时,它们通过注入的夹具访问共享状态。
尽管测试类本身被实例化了多次,夹具始终是同一个实例。

```csharp
public void ExampleTest1()
{
    _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
}

[Fact]
public void ExampleTest2()
{
    _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=190)

执行流程遵循以下顺序:
1.  **夹具构造函数**:在最开始时调用一次。
2.  **测试类构造函数**:在每一个单独的测试执行之前调用。
3.  **测试方法**:具体的测试运行。
4.  **夹具 Dispose**:在该类中的所有测试都完成之后调用一次。

```text
  MyClassFixture()                       (once)
     |
     +--> DefaultBehaviorTests() --> ExampleTest1
     +--> DefaultBehaviorTests() --> ExampleTest2
     |
  MyClassFixture.Dispose()               (once)
```

```csharp
using Xunit;
using Xunit.Abstractions;

namespace AdvancedTechniques.Tests.Unit;

public class DefaultBehaviorTests : IClassFixture<MyClassFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _fixture;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper,
        MyClassFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: { _fixture.Id }");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-class-fixture-53927197/?t=220)

虽然类夹具主要用于集成测试和端到端测试中管理外部连接,但对于任何需要在单元测试之间共享状态的场景,它都是一个很有价值的工具。

## 3. The Collection Fixture

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/) · 5:04

### 总结

xUnit 中的集合夹具(collection fixture)把共享上下文的概念扩展到单个测试类之外,允许单个夹具实例在多个测试类之间共享。
通过定义一个集合并把多个类与它关联,开发者可以确保那些开销很大的准备和清理逻辑(例如数据库迁移或启动服务器)对整组相关测试只执行一次。
这一机制依赖于一个用 CollectionDefinition 特性修饰的标记类,以及对 ICollectionFixture 接口的实现,为集成测试和端到端测试场景提供了一个强大的工具。

### 核心概念

* **ICollectionFixture<T>**:用于定义哪个夹具类应当在测试集合中共享的接口。
* **CollectionDefinitionAttribute**:应用在标记类上的特性,用于为集合命名并把它与一个夹具类型关联起来。
* **CollectionAttribute**:应用在测试类上的特性,用于把它们登记到某个特定的命名集合中。
* **共享生命周期(Shared Lifecycle)**:与按类实例化的类夹具不同,集合夹具为集合中的所有类只实例化一次,并且只有在这些类中的每一个测试都完成之后才被释放。
* **构造函数注入(Constructor Injection)**:共享的夹具实例被注入到集合内各测试类的构造函数中。

### 课程笔记

类夹具允许在单个类内的测试之间共享状态,但经常会出现需要在多个测试类之间共享状态的场景。
如果没有集合夹具,即使两个独立的测试类都实现了 `IClassFixture<T>`,它们也会各自获得一个属于自己的唯一夹具实例。

```csharp
_testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
    }

    [Fact]
    public void ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
    }
}

public class CollectionFixturesBehaviorTestsAgain
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _fixture;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/?t=75)

要在这些类之间共享单个实例,你必须创建一个"集合定义(Collection Definition)"。
这是一个不包含任何行为的标记类,只是充当夹具逻辑的锚点。
这个类必须实现 `ICollectionFixture<T>`,并用 `[CollectionDefinition]` 特性修饰,该特性为集合提供一个唯一的字符串名称。

```csharp
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

[CollectionDefinition("My awesome collection fixture")]
public class TestCollectionFixture : ICollectionFixture<MyClassFixture>
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/?t=145)

集合定义好之后,你需要把 `[Collection]` 特性应用到每一个需要参与的测试类上。
传给该特性的字符串名称必须与 `CollectionDefinition` 中定义的名称一致。

```csharp
using Xunit;
using Xunit.Abstractions;

namespace AdvancedTechniques.Tests.Unit;

[Collection("My awesome collection fixture")]
public class CollectionFixturesBehaviorTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _fixture;

    public CollectionFixturesBehaviorTests(ITestOutputHelper testOutputHelper,
        MyClassFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    [Fact]
    public void ExampleTest1()
    {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/?t=160)

通过把同一个特性应用到第二个类上,xUnit 会确保两个类都通过各自的构造函数获得完全相同的 `MyClassFixture` 实例。

```csharp
}
}

[Collection("My awesome collection fixture")]
public class CollectionFixturesBehaviorTestsAgain
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _fixture;

    public CollectionFixturesBehaviorTestsAgain(ITestOutputHelper testOutputHelper,
        MyClassFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/?t=190)

```text
  [CollectionDefinition("My awesome collection fixture")]
  TestCollectionFixture : ICollectionFixture<MyClassFixture>
                 |
                 v
          MyClassFixture (one instance)
           /                        \
  [Collection("My awesome        [Collection("My awesome
   collection fixture")]          collection fixture")]
  CollectionFixturesBehaviorTests CollectionFixturesBehaviorTestsAgain
```

集合夹具的执行流程如下:
1. 夹具的构造函数被调用一次。
2. 对于第一个类中的每个测试:调用测试类构造函数,运行测试,然后释放测试类。
3. 对于第二个类中的每个测试:调用测试类构造函数,运行测试,然后释放测试类。
4. 最后,夹具的 `Dispose` 方法被调用一次。

```csharp
public Guid Id { get; } = Guid.NewGuid();

        public MyClassFixture()
        {

        }

        public void Dispose()
        {

        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-collection-fixture-53927198/?t=235)

这种模式在集成测试中尤其有用,在那里你可能需要在多个测试文件之间共享单个数据库连接或 Web 服务器实例,以提升性能并保持一致性。

## 4. Running test in parallel

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/) · 5:43

### 总结

xUnit 内置了基于测试集合的并行测试执行支持。
默认情况下,同一个类中的测试按顺序运行,而不同的类并行运行。
这种行为可以在程序集级别进行自定义,用于控制并行度、针对数据库这类共享资源完全禁用并行,或限制并发线程的数量。

### 核心概念

- xUnit 默认行为:类内顺序执行,跨类并行执行。
- 测试集合(Test Collections):xUnit 中并行化的单位。
- 程序集级别配置:使用 `CollectionBehavior` 特性修改全局执行行为。
- 禁用并行化:适用于共享单个数据库实例或其他共享资源的测试。
- 线程管理:配置 `MaxParallelThreads` 来控制并发程度。

### 课程笔记

在 xUnit 中,默认的执行模型是围绕测试集合设计的。
默认情况下,每一个测试类都被视为一个唯一的集合。
单个集合内的测试按顺序一个接一个地执行。

```csharp
public class DefaultBehaviorTests
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
    }

    [Fact]
    public void ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=10)

如果一个类中的测试包含异步延迟,它们仍然会按顺序运行,因为它们属于同一个集合。

```csharp
public DefaultBehaviorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
        await Task.Delay(2000);
    }

    [Fact]
    public async Task ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_id}");
        await Task.Delay(2000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=25)

这种顺序行为同样适用于使用类夹具或集合夹具的测试。
如果多个测试共享一个集合,它们会被排队,一个接一个地运行。

```csharp
namespace AdvancedTechniques.Tests.Unit;

    public class ClassFixtureBehaviorTests : IClassFixture<MyClassFixture>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MyClassFixture _fixture;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=60)

不过,xUnit 会并行运行相互独立的集合。
如果你有多个不共享集合定义的测试类,默认情况下它们会同时执行。

```csharp
public CollectionFixturesBehaviorTestsAgain(ITestOutputHelper testOutputHelper
        MyClassFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    [Fact]
    public async Task ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
        await Task.Delay(2000);
    }

    [Fact]
    public void ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
        await Task.Delay(2000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=80)

#### Assembly-Level Configuration

你可以在程序集级别使用 `CollectionBehavior` 特性全局修改这种默认行为。
例如,你可以把集合的范围改为整个程序集,不过在标准项目中很少需要这样做。

```csharp
using Xunit;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=235)

程序集级别配置更常见的用途是完全禁用并行化。
当测试与共享资源(例如单个数据库实例)交互、并发访问会引起冲突时,这很有用。
虽然这通常表明测试设计还可以改进,但它是防止测试相互冲突的一种有效方式。

```csharp
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=250)

你还可以指定 `MaxParallelThreads`。
虽然你可以把它设为一个较大的数字来匹配 CPU 的能力,但实际的并行度仍然受限于项目中不同测试集合的数量。

```csharp
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 32)]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=295)

#### Collection-Level Control

要进行更细粒度的控制,可以在某个特定集合的定义中,通过在 `CollectionDefinition` 特性上设置 `DisableParallelization = true` 来为该集合禁用并行化。

```csharp
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

[CollectionDefinition("My awesome collection fixture")]
public class TestCollectionFixture : ICollectionFixture<MyClassFixture>
{


}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/running-test-in-parallel-53927199/?t=325)

## 5. Advanced parameterization

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/) · 4:53

### 总结

本课探讨在基础的 InlineData 之外对 xUnit 测试进行参数化的高级技巧。
它涵盖了用于引用测试类内静态成员的 MemberData,以及用于把测试数据逻辑封装到专门类中的 ClassData,从而实现更整洁的测试结构和动态数据生成。

### 核心概念

- InlineData 在处理大型或动态数据集时的局限。
- 使用 public static IEnumerable<object[]> 实现 MemberData。
- 通过创建实现 IEnumerable<object[]> 的类来实现 ClassData。
- 使用 nameof 和 typeof 进行类型安全的数据引用。
- 在枚举器中动态生成数据。

### 课程笔记

虽然 InlineData 适用于简单的场景,但在处理大型数据集或需要动态生成数据时,它可能会变得难以驾驭。

```csharp
private readonly Calculator _sut = new();

[Theory]
[InlineData(5, 5, 10)]
[InlineData(-5, 5, 0)]
[InlineData(-15, -5, -20)]
public void Add_ShouldAddTwoNumbers_WhenTheNumbersAreValidIntegers(
    int firstNumber, int secondNumber, int expectedResult)
{
    // Act
    var result = _sut.Add(firstNumber, secondNumber);

    // Assert
    Assert.Equal(expectedResult, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/?t=10)

要让测试特性更整洁,可以使用 MemberData。
这需要创建一个返回 `IEnumerable<object[]>` 的公共静态成员(通常是属性或方法)。
这种做法把数据从特性中移到一个专门的成员里,提升了可读性,并允许使用更大的数据集。

```csharp
public static IEnumerable<object[]> AddTestData =>
    new List<object[]>
    {
        new object[] { 5, 5, 10 },
        new object[] { -5, 5, 0 },
        new object[] { -15, -5, -20 }
    };
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/?t=100)

然后,测试方法使用 `MemberData` 特性和 `nameof` 运算符引用这个成员,以确保类型安全。

```csharp
namespace AdvancedTechniques.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    // [InlineData(5, 5, 10)]
    // [InlineData(-5, 5, 0)]
    // [InlineData(-15, -5, -20)]
    [MemberData(nameof(AddTestData))]
    public void Add_ShouldAddTwoNumbers_WhenTheNumbersAreValidIntegers(
        int firstNumber, int secondNumber, int expectedResult)
    {
        // Act
        var result = _sut.Add(firstNumber, secondNumber);

        // Assert
        Assert.Equal(expectedResult, result);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/?t=115)

为了更彻底地分离关注点,或进行复杂的动态数据生成,会使用 `ClassData`。
这需要一个实现 `IEnumerable<object[]>` 的专门类。
在 `GetEnumerator` 方法内部,你可以使用 `yield return` 来提供测试用例。
如果你想以编程方式生成测试数据,这尤其有用。

```csharp
public class CalculatorSubtractTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 5, 5, 0 };
        yield return new object[] { -5, -5, 0 };
        yield return new object[] { -15, -5, -10 };
        yield return new object[] { 15, 5, 10 };
        yield return new object[] { 5, 10, -5 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/?t=250)

测试方法使用 `ClassData` 特性和 `typeof` 运算符指向这个类。
测试运行时,xUnit 会实例化这个类,并遍历枚举器,为每一次测试执行提供参数。

```csharp
[Theory]
// [InlineData(5, 5, 0)]
// [InlineData(-5, -5, 0)]
// [InlineData(-15, -5, -10)]
// [InlineData(15, 5, 10)]
// [InlineData(5, 10, -5)]
[ClassData(typeof(CalculatorSubtractTestData))]
public void Subtract_ShouldSubtractTwoNumbers_WhenTheNumbersAreValidIntegers(
    int firstNumber, int secondNumber, int expectedResult)
{
    // Act
    var result = _sut.Subtract(firstNumber, secondNumber);

    // Assert
    Assert.Equal(expectedResult, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/advanced-parameterization-53927201/?t=240)

## 6. Setting test execution timeouts

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/setting-test-execution-timeouts-53927202/) · 1:17

### 总结

本课讲解如何通过应用执行超时来处理那些意外耗时过长的单元测试。
通过在 xUnit 的 [Fact] 特性中使用 Timeout 属性,开发者可以确保测试在达到特定时间阈值时立即失败,防止长时间运行或挂起的测试拖慢整个测试套件。

### 核心概念

* 识别偏离其正常执行速度的测试。
* 在 `[Fact]` 特性中使用 `Timeout` 属性。
* 以毫秒为单位定义超时阈值。
* 解读超时测试的失败消息。

### 课程笔记

在某些场景下,通常瞬间就能完成的单元测试可能会开始耗费过多的时间才能完成。
例如,一个测试可能因为某个底层问题而被拖慢,导致它运行了 10 秒,而它通常应当在一毫秒内完成。

```csharp
using System.Threading.Tasks;
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

public class LongRunningTests
{
    [Fact]
    public async Task SlowTest()
    {
        await Task.Delay(10000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/setting-test-execution-timeouts-53927202/?t=25)

为了防止这些长时间运行的测试阻塞测试套件,你可以在测试级别设置一个特定的超时。
这是通过向 `[Fact]` 特性传入一个以毫秒为单位的 `Timeout` 值来实现的。
如果测试执行超出了这个限制,xUnit 会自动终止该测试并报告失败。

```csharp
using System.Threading.Tasks;
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

public class LongRunningTests
{
    [Fact(Timeout = 2000)]
    public async Task SlowTest()
    {
        await Task.Delay(10000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/setting-test-execution-timeouts-53927202/?t=60)

当测试因为这一约束而失败时,运行器会提供一条特定的错误消息,指出测试执行在指定的毫秒数之后超时。
这样就能立即识别性能问题或潜在的死锁,而不必等待测试跑完那段非预期的完整时长。

## 7. Testing Date and Time

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/) · 5:19

### 总结

测试依赖当前系统时间的逻辑本质上是不确定的,因为 DateTime.Now 的值在不断变化。
为了让这类代码可测试,开发者应当把时间的获取抽象到一个接口(例如 IDateTimeProvider)之后,从而在单元测试中可以 mock 系统时间。
这种做法确保了所有依赖时间的逻辑分支(例如早上、下午和晚上的问候语)都能被可靠地验证,而不论测试套件实际在什么时间执行。

### 核心概念

- **不确定的依赖(Non-deterministic dependencies)**:系统时钟的值每一毫秒都在变化,因此不可能为特定的基于时间的条件编写稳定的测试。
- **DateTime Provider 模式**:把 `DateTime.Now` 抽象到一个接口之后,以便在测试环境中掌控时间。
- **依赖注入**:把时间提供程序注入到被测类(SUT)中,以便替换为 mock。
- **Mock 时间**:使用 NSubstitute 这类库,为不同的测试场景返回特定的 `DateTime` 值。

### 课程笔记

测试依赖系统时钟的代码带来了一个独特的挑战。
考虑一个 `Greeter` 类,它会根据当天的当前小时返回不同的消息。
由于它在内部调用了 `DateTime.Now`,其输出与执行时的真实世界时间绑定在一起。

```csharp
namespace AdvancedTechniques;

public class Greeter
{
    public string GenerateGreetMessage()
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=25)

在尝试对这个类进行单元测试时,一个用来验证 "Good evening" 消息的测试如果在晚上运行就会通过,但如果在早上运行就会失败。

```csharp
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

public class GreeterTests
{
    [Fact]
    public void GenerateGreetMessage_ShouldSayGoodEvening_WhenItsEvening()
    {
        // Arrange
        var sut = new Greeter();

        // Act

        // Assert

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=70)

如果你尝试编写一整套覆盖早上、下午和晚上的测试,那么其中至少有两个测试总是会失败,因为系统时钟在同一时刻只能处于一种状态。

```csharp
// Act
        var result = sut.GenerateGreetMessage();

        // Assert
        result.Should().Be("Good evening");
    }

    [Fact]
    public void GenerateGreetMessage_ShouldSayGoodMorning_WhenItsMorning()
    {
        // Arrange
        var sut = new Greeter();

        // Act
        var result = sut.GenerateGreetMessage();

        // Assert
        result.Should().Be("Good morning");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=115)

要解决这个问题,你必须让这个类与静态的 `DateTime.Now` 属性解耦。
这是通过创建一个接口(通常命名为 `IDateTimeProvider`)以及一个返回真实系统时间的生产实现来实现的。

```csharp
namespace AdvancedTechniques;

public class DateTimeProvider : IDateTimeProvider
{

}

public interface IDateTimeProvider
{
    public DateTime DateTime { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=175)

然后对 `Greeter` 类进行重构,让它通过构造函数接收这个接口。
这是专业项目中的一种常见做法,用于确保任何需要基于时间的逻辑的组件都具备可测试性。

```csharp
public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateGreetMessage()
    {
        var dateTimeNow = DateTime.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=205)

在最终的实现中,`GenerateGreetMessage` 方法使用的是这个提供程序,而不是静态的 `DateTime` 类。
在测试项目中,你可以使用 NSubstitute 这样的 mock 库,强制提供程序返回一个特定的 `DateTime` 对象,从而模拟一天中的任意时间。

```csharp
public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=295)

```text
  Production:  Greeter --> IDateTimeProvider --> DateTimeProvider (system time)
  Tests:       Greeter --> IDateTimeProvider --> NSubstitute mock
                                                 DateTimeNow.Returns(20:00)
```

通过 mock `DateTimeNow` 属性,测试变得具有确定性。
现在你可以验证:当提供程序被配置为返回晚上 8:00(20:00)时,会返回 "Good evening" 消息,而不论测试实际在什么时间运行。

```csharp
public GreeterTests()
{
    _sut = new Greeter(_dateTimeProvider);
}

[Fact]
public void GenerateGreetMessage_ShouldSayGoodEvening_WhenItsEvening()
{
    // Arrange
    _dateTimeProvider.DateTimeNow.Returns(new DateTime(2020, 1, 1, 20, 0, 0));

    // Act
    var result = _sut.GenerateGreetMessage();

    // Assert
    result.Should().Be("Good evening");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-date-and-time-53927203/?t=310)

## 8. Continuous testing

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/continuous-testing-53927204/) · 1:55

### 总结

持续测试(continuous testing)是一种开发实践:每当保存代码改动时,单元测试都会自动执行。
这种技巧为开发者提供了一个即时的反馈循环,在回归或错误一被引入时就把它们识别出来。
虽然它对维护代码质量非常有效,但它需要足够的硬件资源,并且可以根据开发者当前的工作流需要来开启或关闭。

### 核心概念

- **定义**:由代码修改触发的测试套件自动执行。
- **工具**:JetBrains Rider("Auto Start tests on save")和 Visual Studio Enterprise("Live Unit Testing")原生支持,也可以通过 NCrunch 这类第三方工具实现。
- **反馈循环**:几乎即时地通知某次改动是否破坏了现有功能。
- **资源管理**:持续测试可能消耗大量资源;开发者可以选择在配置较低的硬件上或在特定的编码阶段禁用它。

### 课程笔记

持续测试确保每当代码库发生改动时,相关的测试都会自动运行。
这种实践得到了多种 IDE 和工具的支持,包括 Visual Studio Enterprise、JetBrains Rider 和 NCrunch。
在 Rider 中,这是通过 "Auto Start tests on save" 设置来管理的,它可以配置为每次保存文件时运行解决方案中的所有测试或某个特定的子集。

下面的示例展示了一个使用 Xunit、NSubstitute 和 FluentAssertions 的标准单元测试类,此时尚未观察到持续测试的触发:

```csharp
using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

public class GreeterTests
{
    private readonly Greeter _sut;
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimePro

    public GreeterTests()
    {
        _sut = new Greeter(_dateTimeProvider);
    }

    [Fact]
    public void GenerateGreetMessage_ShouldSayGoodEvening_WhenItsEve
    {
        // Arrange
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/continuous-testing-53927204/?t=10)

当持续测试处于启用状态时,保存文件就会触发测试运行器。
如果开发者引入了一个破坏性的改动(例如注释掉被测系统(SUT)的初始化),IDE 会立即给出测试失败的可视化反馈。

```csharp
public class GreeterTests
{
    private readonly Greeter _sut;
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeTin

    public GreeterTests()
    {
        //_sut = new Greeter(_dateTimeProvider);
    }

    [Fact]
    public void GenerateGreetMessage_ShouldSayGoodEvening_WhenItsEve
    {
        // Arrange
        _dateTimeProvider.DateTimeNow.Returns(r

        // Act
        var result = _sut.GenerateGreetMessage(

        // Assert
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/continuous-testing-53927204/?t=55)

虽然这种即时反馈循环对于尽早发现错误非常有益,但它可能会消耗大量资源。
在性能有限的机器上,测试在后台持续执行可能会影响 IDE 的响应速度。
此外,一些开发者在进行复杂的重构时,会觉得不断出现的失败通知令人分心。
在这些情况下,可以把这个功能关闭。

代码被修正并再次保存后,持续测试运行器会自动重新执行测试,以确认修复生效。

```csharp
public class GreeterTests
{
    private readonly Greeter _sut;
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeTin

    public GreeterTests()
    {
        _sut = new Greeter(_dateTimeProvider);
    }

    [Fact]
    public void GenerateGreetMessage_ShouldSayGoodEvening_WhenItsEve
    {
        // Arrange
        _dateTimeProvider.DateTimeNow.Returns(r

        // Act
        var result = _sut.GenerateGreetMessage(

        // Assert
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/continuous-testing-53927204/?t=85)

## 9. What is code coverage?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-code-coverage-53927205/) · 2:57

### 总结

代码覆盖率(code coverage)是一种软件度量指标,用于衡量测试套件运行时被执行到的源代码所占的比例,从而让人看清哪些分支和代码行被单元测试执行到了。
借助覆盖率分析,开发者可以通过 IDE 中的可视化标记和量化的百分比报告找出未经测试的逻辑,确保服务和控制器这类关键组件在所有用例下都得到充分验证。

### 核心概念

- **覆盖率分析(Coverage Analysis)**:识别测试运行期间哪些代码行被执行的过程。
- **可视化标记(Visual Indicators)**:IDE 特有的、显示在编辑器边栏中的标记,表示某一行是已覆盖还是未覆盖。
- **覆盖率百分比(Coverage Percentage)**:一个量化指标,表示一个项目、命名空间或类有多少被测试执行到。
- **语句覆盖率(Statement Coverage)**:一种细粒度的视图,用于找出没有被任何测试触及的具体代码行。

### 课程笔记

代码覆盖率分析用于识别特定的代码行在测试运行期间是否被执行。
在 JetBrains Rider 这类开发环境中,这通常通过编辑器边栏中的标记来可视化。
这些标记表示某一行是否至少被一个测试"单步经过",帮助开发者确认自己的代码得到了有效覆盖。

```csharp
{
        _logger.LogInformation("Creating user with id {0} and name: {1}", user.Id, us
        var stopwatch = Stopwatch.StartNew();
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
            stopwatch.Stop();
            _logger.LogInformation("User with id {0} created in {1}r
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-code-coverage-53927205/?t=15)

要生成这些指标,开发者可以使用 "Cover Selected Unit Tests" 功能。
它会在执行测试的同时,在后台分析所经过的代码路径。
测试通过后,IDE 会显示项目的覆盖率百分比,并允许开发者排除测试套件本身这类无关的项目,以便专注于应用逻辑。

```csharp
public async Task<User?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving user with id: {0}", id);
            var stopwatch = Stopwatch.StartNew();
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
                stopwatch.Stop();
                _logger.LogInformation("User with id {0} retrieved in
            }
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-code-coverage-53927205/?t=40)

测试服务和控制器时的首要目标是达到 100% 的覆盖率,确保每一个逻辑分支和潜在的错误状态都被考虑到。
通过深入查看覆盖率报告,开发者可以直接导航到各个方法,并看到逐条语句的测试情况明细。

```csharp
public async Task<bool> DeleteByIdAsync(Guid id)
{
    _logger.LogInformation("Deleting user with id: {0}", id);
    var stopWatch = Stopwatch.StartNew();
    try
    {
        return await _userRepository.DeleteByIdAsync(id);
    }
    catch (Exception e)
    {

        _logger.LogError(e, "Something went wrong while deleting user with id {0}"
        throw;
    }
    finally
    {
        stopWatch.Stop();
        _logger.LogInformation("User with id {0} deleted in {1}"
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/what-is-code-coverage-53927205/?t=160)

如果某条语句被标识为未覆盖,就说明测试套件存在缺口,可能需要补充测试用例。
虽然这些功能通常内置于 IDE 中,但也可以在进程外的场景中测量代码覆盖率,例如通过命令行,这对于把覆盖率检查集成到自动化的 CI/CD 流水线中至关重要。

## 10. Measuring code coverage

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/) · 6:35

### 总结

对于无法使用 IDE 专属工具的持续集成(CI)流水线来说,通过命令行测量代码覆盖率至关重要。
本课演示如何使用 Coverlet 计算覆盖率指标,以及如何使用 Report Generator 工具把它们可视化。
它涵盖了 .NET 全局工具的安装、针对已编译的测试程序集执行覆盖率分析,以及使用代码特性和命令行参数两种方式把特定命名空间排除在报告之外的技巧。

### 核心概念

- 基于 CLI 的覆盖率测量,用于 CI/CD 集成。
- `coverlet.console` 全局工具的安装和使用。
- 使用 `[ExcludeFromCodeCoverage]` 特性把代码排除在覆盖率之外。
- 通过 CLI 参数筛选覆盖率结果,以保持源代码整洁。
- 使用 `coverlet.collector` 生成跨平台的 Cobertura XML 报告。
- 使用 `reportgenerator` 把 XML 覆盖率数据转换为人类可读的 HTML。

### 课程笔记

虽然 Rider 这类 IDE 提供了内置的覆盖率工具,但在持续集成这类自动化环境中,必须通过命令行来测量覆盖率。
首先,你必须把 Coverlet console 工具安装为一个 .NET 全局工具。

```powershell
>> dotnet tool install -g coverlet.console
You can invoke the tool using the following command: coverlet
Tool 'coverlet.console' (version '3.1.0') was successfully installed.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=60)

在运行覆盖率之前,必须先构建测试项目,以生成所需的 DLL 文件。

```powershell
dotnet build
Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  Restored I:\lab\courses\Course.UnitTesting\4.RealWorld\test\Users.Api.Tests.Unit\Users.Api.Tests.Unit.csproj (in 315 ms).
  1 of 2 projects are up-to-date for restore.
  Users.Api -> I:\lab\courses\Course.UnitTesting\4.RealWorld\src\Users.Api\bin\Debug\net6.0\Users.Api.dll
  Users.Api.Tests.Unit -> I:\lab\courses\Course.UnitTesting\4.RealWorld\test\Users.Api.Tests.Unit\bin\Debug\net6.0\Users.Api.Tests.Unit.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.25
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=70)

项目构建完成后,你就可以把 `coverlet` 指向测试 DLL 来运行它。
你把目标指定为 `dotnet`,并为该目标提供参数,例如 `test --no-build`,以确保测试在运行时不会进行多余的重新构建。

```shell
>> coverlet .\bin\Debug\net6.0\Users.Api.Tests.Unit.dll --target "dotnet" --targetargs "test --no-build"
Test run for I:\lab\courses\Course.UnitTesting\4.RealWorld\test\Users.Api.Tests.Unit\bin\Debug\net6.0\User
s.Api.Tests.Unit.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.0
Copyright (c) Microsoft Corporation.  All rights reserved.
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
Passed!  - Failed:     0, Passed:    23, Skipped:     0, Total:    23, Duration: 42 ms - Users.Api.Tests.U
nit.dll (net6.0)

Calculating coverage result...
  Generating report 'I:\lab\courses\Course.UnitTesting\4.RealWorld\test\Users.Api.Tests.Unit\coverage.json
'
+-----------+---------+--------+--------+
| Module    | Line    | Branch | Method |
+-----------+---------+--------+--------+
| Users.Api | 54.45% | 37.5%  | 51.51% |
+-----------+---------+--------+--------+

+---------+---------+--------+--------+
|         | Line    | Branch | Method |
+---------+---------+--------+--------+
| Total   | 54.45% | 37.5%  | 51.51% |
+---------+---------+--------+--------+
| Average | 54.45% | 37.5%  | 51.51% |
+---------+---------+--------+--------+
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=80)

#### Excluding Code from Coverage

在某些场景下,某些命名空间或类(例如 repository)并不需要覆盖。
排除它们的一种方式是使用 `[ExcludeFromCodeCoverage]` 特性。
IDE 和 CLI 工具都能识别这个特性。

```csharp
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Users.Api.Data;
using Users.Api.Models;

namespace Users.Api.Repositories;

[ExcludeFromCodeCoverage]
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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=145)

为了避免在源代码中"乱撒"特性,你也可以改为在命令行中使用 `--exclude` 参数。
这让你可以在覆盖率运行期间动态地筛除特定的命名空间。

```shell
>> coverlet .\bin\Debug\net6.0\Users.Api.Tests.Unit.dll --target "dotnet" --targetargs "test --no-build" --exclude "[*]Users.Api.Repositories*"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=205)

应用排除之后,覆盖率百分比会上升,因为被忽略的代码不再参与计算。

```shell
-exclude "[*]Users.Api.Repositories*"
Test run for I:\lab\courses\Course.UnitTesting\4.RealWorld\test\Users.Api.Tests.Unit\bin\Debug\net6.0\User
s.Api.Tests.Unit.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.0
Copyright (c) Microsoft Corporation.  All rights reserved.
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
Passed!  - Failed:     0, Passed:    23, Skipped:     0, Total:    23, Duration: 40 ms - Users.Api.Tests.U
nit.dll (net6.0)

Calculating coverage result...
  Generating report 'I:\lab\courses\Course.UnitTesting\4.RealWorld\test\Users.Api.Tests.Unit\coverage.json
'

+-----------+---------+--------+--------+
| Module    | Line    | Branch | Method |
+-----------+---------+--------+--------+
| Users.Api | 61.79% | 37.5%  | 60.71% |
+-----------+---------+--------+--------+

+---------+---------+--------+--------+
|         | Line    | Branch | Method |
+---------+---------+--------+--------+
| Total   | 61.79% | 37.5%  | 60.71% |
+---------+---------+--------+--------+
| Average | 61.79% | 37.5%  | 60.71% |
+---------+---------+--------+--------+
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=245)

#### Generating and Visualizing Reports

为了获得跨平台兼容性和更好的报告,你可以使用测试项目中已包含的 `coverlet.collector` NuGet 包。

```xml
<PackageReference Include="coverlet.collector" Version="3.1.0">
  <IncludeAssets>runtime; build; native; contentfiles; analy
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=255)

带 `--collect` 标志运行 `dotnet test` 会生成一个 Cobertura XML 文件,这是一种覆盖率数据的标准格式。

```shell
>> dotnet test --collect:"XPlat Code Coverage"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=265)

生成的 `coverage.cobertura.xml` 文件包含详细的指标,但人类不容易阅读。

```xml
<?xml version="1.0" encoding="utf-8"?>
<coverage line-rate="0.5445" branch-rate="0.375" version="1.9" timestamp="1638128780"
  <sources>
    <source>I:\lab\courses\Course.UnitTesting\4.RealWorld\src\Users.Api\</source>
  </sources>
  <packages>
    <package name="Users.Api" line-rate="0.5445" branch-rate="0.375" complexity="44">
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=275)

要把这些数据可视化,请安装 `dotnet-reportgenerator-globaltool`,并用它把 XML 转换为 HTML 报告。
你必须把这个工具指向生成的 XML 文件,并指定目标目录和报告类型。

```powershell
reportgenerator -reports:".\TestResults\93ae3d0d-76f3-43fe-bb90-d9d54d0b3b21\coverage.cobertura.xml" -targetdir:"codecoverage" -reporttypes:HTML
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/measuring-code-coverage-53927206/?t=325)

```text
  dotnet test --collect:"XPlat Code Coverage"
          |
          v
  coverage.cobertura.xml
          |
          v
  reportgenerator -reporttypes:HTML
          |
          v
  codecoverage (HTML report)
```

生成的 HTML 报告提供了程序集和类的高层概览,以及源代码内部逐行的覆盖率可视化。
这些报告可以在 CI 流水线中作为构建产物发布,以便让人看清代码质量。

## 11. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927207/) · 0:48

### 总结

本课全面总结了在 C# 中使用 xUnit 的高级单元测试技巧。
它回顾了执行生命周期、数据共享策略、并行化控制以及高级参数化方法。
此外,它还涵盖了处理依赖时间的逻辑、用超时管理长时间运行的测试,以及持续测试和代码覆盖率分析的实际应用。

### 核心概念

- xUnit 的执行范围与上下文
- 通过类夹具(Class Fixtures)和集合夹具(Collection Fixtures)共享数据
- 测试并行化管理
- 使用 MemberData 和 ClassData 的高级参数化
- 测试超时与依赖时间的测试
- 持续测试工作流
- 代码覆盖率收集(IDE 与 CLI)

### 课程笔记

本课程的高级部分聚焦于 xUnit 的内部机制和复杂的测试模式。
关键主题包括:

#### Execution Context and Shared Fixtures

xUnit 默认的执行范围和上下文决定了测试类如何被实例化。
要在一个类内的各个测试之间共享数据或状态,会使用 `IClassFixture<T>`。
要在多个测试类之间共享状态,则使用 `CollectionFixture`。
这些机制对数据如何共享以及整体执行流程如何管理有着显著的影响。

#### Parallelization

xUnit 默认并行运行测试以提升性能。
这一部分讲解了如何管理这种行为,包括为可能相互冲突的特定测试禁用并行化的技巧,以及扩展并行执行配置以满足特定项目需要的技巧。

#### Advanced Parameterization

虽然基础的参数化通常用 `InlineData` 处理,但高级场景需要更多灵活性。
使用 `MemberData` 和 `ClassData` 可以把复杂的数据集和可复用的测试用例注入到基于 theory 的测试中。

#### Time-Sensitive Testing and Timeouts

对长时间运行的操作的测试,通过实现超时来管理,以防止进程挂起。
此外,这一部分还讨论了测试依赖系统时钟的逻辑所面临的挑战,演示了如何有效地抽象并测试日期和时间相关的需求。

#### Continuous Testing and Code Coverage

持续测试在开发周期中提供即时反馈,不过它在资源使用方面有特定的取舍。
最后,这一部分详细说明了代码覆盖率的重要性,演示了如何在集成开发环境(IDE)内以及通过外部命令行工具来收集和分析覆盖率指标。

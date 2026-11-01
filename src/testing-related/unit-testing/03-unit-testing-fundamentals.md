# Unit testing fundamentals

> 课程:[From Zero to Hero: Unit testing for C# Developers](https://dometrain.com/course/from-zero-to-hero-unit-testing-for-csharp-developers/) · 第 3 章
> 共 13 课 · 约 33:41
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The 3 core unit testing concepts](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-3-core-unit-testing-concepts-53926869/) | 1:57 | [↓](#1-the-3-core-unit-testing-concepts) |
| 2 | [Why xUnit?](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/why-xunit-53926870/) | 0:57 | [↓](#2-why-xunit) |
| 3 | [Writing your first unit test](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-your-first-unit-test-53926871/) | 6:23 | [↓](#3-writing-your-first-unit-test) |
| 4 | [Structuring your solution](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/structuring-your-solution-53926873/) | 2:50 | [↓](#4-structuring-your-solution) |
| 5 | [Naming in unit tests](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/) | 3:32 | [↓](#5-naming-in-unit-tests) |
| 6 | [Arrange, Act, Assert](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/arrange-act-assert-53926876/) | 2:09 | [↓](#6-arrange-act-assert) |
| 7 | [xUnit's test execution model](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/xunits-test-execution-model-53926877/) | 2:54 | [↓](#7-xunits-test-execution-model) |
| 8 | [Test Setup and Teardown](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/) | 5:46 | [↓](#8-test-setup-and-teardown) |
| 9 | [Parameterizing tests](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/parameterizing-tests-53926879/) | 3:18 | [↓](#9-parameterizing-tests) |
| 10 | [Ignoring tests](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/ignoring-tests-53926880/) | 1:23 | [↓](#10-ignoring-tests) |
| 11 | [Your turn to practice](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/your-turn-to-practice-53926881/) | 1:04 | [↓](#11-your-turn-to-practice) |
| 12 | [My answer to the exercise](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/my-answer-to-the-exercise-53926882/) | 0:40 | [↓](#12-my-answer-to-the-exercise) |
| 13 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53926883/) | 0:48 | [↓](#13-section-recap) |

## 1. The 3 core unit testing concepts

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/the-3-core-unit-testing-concepts-53926869/) · 1:57

### 总结

C# 中的单元测试建立在三大核心支柱之上:单元测试库、mocking 库和断言库。
本课介绍 .NET 生态中可用的主要框架(例如 xUnit、NSubstitute 和 FluentAssertions),并指出本课程中用于执行测试、隔离依赖和验证结果的具体工具。

### 核心概念

- **单元测试库**:运行测试的框架(例如 xUnit、NUnit、MSTest)。
- **Mocking 库**:用于创建依赖项假实现的工具(例如 NSubstitute、Moq、FakeItEasy)。
- **断言库**:用于验证测试结果的专门库(例如 FluentAssertions、Shouldly)。
- **课程工具集**:本课程选用的具体组合:xUnit、NSubstitute 和 FluentAssertions。

### 课程笔记

在编写单元测试之前,开发者必须了解构建一个可用测试套件所需的三个组成部分。

#### Unit Testing Library

单元测试库是负责运行测试的核心框架。
在 .NET 生态中,三个主要参与者是 xUnit、NUnit 和 MSTest。
本课程使用 xUnit,它因其现代化的设计思路和可扩展性而广受认可。

#### Mocking Library

Mocking 库用于创建依赖项的假实现,从而把被测单元隔离出来。
通过伪造这些依赖,开发者可以确保测试失败是由单元自身的逻辑引起的,而不是由外部因素引起的。
.NET 中的主要选项是 NSubstitute、Moq 和 FakeItEasy。
本课程使用 NSubstitute。

#### Assertions Library

断言库用于验证测试的实际结果是否与预期结果相符。
虽然测试运行器自带断言方法,但 FluentAssertions 和 Shouldly 这样的专门库提供了更具表现力、更易读的语法。
本课程统一使用 FluentAssertions。

#### Course Toolset

本课程的技术栈(xUnit、NSubstitute 和 FluentAssertions)代表了 C# 开发社区中一种常见且备受推崇的组合。

```text
  单元测试套件
  +---------------------+---------------------+---------------------+
  | Unit Testing        | Mocking             | Assertions          |
  | Library             | Library             | Library             |
  | 运行测试            | 伪造依赖            | 验证结果            |
  +---------------------+---------------------+---------------------+
  | xUnit               | NSubstitute         | FluentAssertions    |
  +---------------------+---------------------+---------------------+
```

## 2. Why xUnit?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/why-xunit-53926870/) · 0:57

### 总结

xUnit 是 C# 开发中业界标准的单元测试框架,因其现代化的设计和广泛的采用(包括 Microsoft 在内)而备受青睐。
虽然单元测试的核心原则可以在 NUnit 和 MSTest 等框架之间迁移,但 xUnit 提供了一套基于早期库经验教训而打磨的 API。
本课探讨为什么 xUnit 是本课程以及现代 .NET 开发的首选。

### 核心概念

- **起源**:由 NUnit 的原作者创建,作为一个更现代的替代方案。
- **现代设计**:旨在改进 NUnit 的 API,融入了多年的框架开发经验,并允许通过破坏性变更来改进这个库。
- **业界采用**:目前是 .NET 生态中最流行的测试库。
- **Microsoft 标准**:Microsoft 在内部开发和开源项目中使用的框架。
- **可迁移性**:在 xUnit 中学到的测试概念同样适用于 NUnit 和 MSTest 等其他框架。

### 课程笔记

xUnit 目前是 .NET 开发者中最流行的单元测试库。
它最初由 NUnit 的创建者开发,作为一个更好的替代方案。
通过从一个全新的代码库起步,作者们得以吸收 NUnit 的经验,并在不受向后兼容约束的情况下实现一套新的 API。

该框架的质量和设计使其在业界得到了广泛采用。
值得注意的是,Microsoft 在自己的开发项目中使用 xUnit。
虽然本课程聚焦于 xUnit,但基本的测试原则在 NUnit 和 MSTest 等其他框架中是一致的。

一个典型的 xUnit 测试项目配置包含 `xunit` 包和 Visual Studio 运行器,如下面的项目文件所示:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <Nullable>enable</Nullable>

        <IsPackable>false</IsPackable>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
        <PackageReference Include="xunit" Version="2.4.1" />
        <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="coverlet.collector" Version="3.1.0">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
    </ItemGroup>

    <ItemGroup>
      <ProjectReference Include="..\..\src\CalculatorLibrary\CalculatorLibrary.csproj" />
    </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/why-xunit-53926870/?t=0)

xUnit 中的测试定义在类中,并使用特性来标记,例如普通测试用 `[Fact]`,数据驱动的测试用 `[Theory]`。
下面的示例展示了一个针对计算器库的单元测试类:

```csharp
using Xunit;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(5, 5, 10)]
    [InlineData(-5, 5, 0)]
    [InlineData(-15, -5, -20)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/why-xunit-53926870/?t=29)

## 3. Writing your first unit test

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-your-first-unit-test-53926871/) · 6:23

本课演示如何在 .NET 6 环境中使用 xUnit 创建第一个单元测试。
它逐步讲解如何建立一个专门的测试项目、配置测试 SDK 和运行器等必要依赖,并为一个简单的计算器库实现测试。
本课强调单元测试的基本结构(实例化、调用和断言),并解释 [Fact] 特性如何让测试运行器发现并执行测试方法。

### 核心概念

- xUnit 测试框架
- 测试项目依赖(Microsoft.NET.Test.Sdk、xunit、xunit.runner.visualstudio)
- 用于测试发现的 [Fact] 特性
- Arrange-Act-Assert 模式
- 使用 Assert.Equal 进行断言
- 测试执行与回归安全

### 课程笔记

测试的目标是一个 .NET 6 库中的简单 `Calculator` 类。
这个类提供基本的算术运算:加法、减法、乘法和除法。

```csharp
namespace CalculatorLibrary;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public float Divide(float a, float b)
    {
        return a / b;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-your-first-unit-test-53926871/?t=25)

为了测试这个库,需要新建一个 xUnit Test Project。
项目文件(`.csproj`)定义了编写和运行测试所需的依赖:
- `Microsoft.NET.Test.Sdk`:用于测试的核心 SDK。
- `xunit`:测试框架本身。
- `xunit.runner.visualstudio`:让 Visual Studio 和 Rider 等 IDE 能够执行测试的运行器。
- `coverlet.collector`:用于收集代码覆盖率数据。

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <Nullable>enable</Nullable>

        <IsPackable>false</IsPackable>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
        <PackageReference Include="xunit" Version="2.4.1" />
        <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="coverlet.collector" Version="3.1.0">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
    </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-your-first-unit-test-53926871/?t=100)

默认情况下,xUnit 模板会提供一个带有示例测试方法的占位类。

```csharp
using Xunit;

namespace CalculatorLibraryTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-your-first-unit-test-53926871/?t=175)

要为 `Add` 方法实现一个具体的测试,测试项目必须先引用 `CalculatorLibrary`。
测试遵循标准的三步结构:
1. **Arrange**:实例化 `Calculator` 类。
2. **Act**:用特定的输入(例如 5 和 4)调用 `Add` 方法。
3. **Assert**:使用 `Assert` 类验证结果是否与预期值(9)相符。

`[Fact]` 特性至关重要;它向测试运行器表明该方法是一个单元测试。
没有这个特性,该方法在发现阶段会被忽略。

```csharp
using CalculatorLibrary;
using Xunit;

namespace CalculatorLibraryTests;

public class Tests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-your-first-unit-test-53926871/?t=305)

当测试在 IDE 的单元测试窗口中执行时,它会验证逻辑。
如果代码被错误地修改(例如把 `Add` 方法改为返回 `a - b`),测试就会失败,为开发者提供即时的安全网。

## 4. Structuring your solution

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/structuring-your-solution-53926873/) · 2:50

### 总结

合理的解决方案结构需要使用物理的 src 和 test 文件夹把源代码与测试代码分开。
这种组织方式可以防止项目杂乱,并在生产逻辑与验证代码之间提供清晰的逻辑和物理区分,确保随着解决方案的增长,项目之间的关系仍然直观且易于管理。

### 核心概念

- **物理分离与逻辑分离**:IDE 的解决方案文件夹通常只提供逻辑分组;必须在文件系统中创建物理文件夹,才能确保在各种工具中组织方式一致。
- **标准目录约定**:用 `src` 存放生产项目,用 `test` 存放单元测试、集成测试和功能测试项目。
- **维护项目引用**:移动项目后需要更新 `.csproj` 文件中的相对路径,才能保持构建成功。
- **解决方案的可扩展性**:按文件夹组织可以防止顶层解决方案视图在项目数量增加时变得难以招架。

### 课程笔记

解决方案刚开始时,项目通常创建在顶层。
这对小项目来说是可行的,但随着解决方案的增长就会导致杂乱。
为了保持整洁的架构,项目应当被组织到 `src`(源代码)和 `test` 目录中。

#### Creating Physical Folders

在 Visual Studio 或 JetBrains Rider 这样的 IDE 中,创建一个"解决方案文件夹"(Solution Folder)并不总会在磁盘上创建对应的物理文件夹。
为了确保结构反映在文件系统中,你应该在解决方案根目录手动创建 `src` 和 `test` 文件夹。
生产库和应用被移到 `src` 中,而所有与测试相关的项目被移到 `test` 中。

```text
  <solution root>
  +-- src
  |   +-- CalculatorLibrary
  +-- test
      +-- CalculatorLibraryTests
```

在重新组织之前,测试项目最初可能是这样的:

```csharp
namespace CalculatorLibraryTests;

public class Tests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/structuring-your-solution-53926873/?t=5)

#### Re-adding Projects to the Solution

在物理移动项目文件之后,解决方案文件(`.sln`)将无法再找到它们。
你必须从解决方案中移除旧的项目引用,然后使用"Add Existing Project"对话框,导航到 `src` 和 `test` 文件夹中的新位置,重新添加它们。

#### Updating Project References

移动项目会改变它们的相对路径。
如果测试项目之前以同级关系引用源项目,那么一旦测试项目被移到 `test/`、源项目被移到 `src/`,这个引用就会失效。

测试项目的依赖,例如 xUnit 和测试 SDK,仍然定义在 `.csproj` 文件中:

```xml
<ItemGroup>
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
        <PackageReference Include="xunit" Version="2.4.1" />
        <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="coverlet.collector" Version="3.1.0">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
    </ItemGroup>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/structuring-your-solution-53926873/?t=145)

但是,`ProjectReference` 必须更新,以反映新的目录深度。
例如,一个被移入子文件夹的项目通常需要先向上两级(`../../`)回到根目录,再进入 `src` 目录:

```xml
<ItemGroup>
  <ProjectReference Include="..\..\src\CalculatorLibrary\CalculatorLibrary.csproj" />
</ItemGroup>
```

一旦引用更新完毕,测试就可以在项目结构中的新位置正常执行。

```csharp
using CalculatorLibrary;
using Xunit;

namespace CalculatorLibraryTests;

public class Tests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/structuring-your-solution-53926873/?t=155)

## 5. Naming in unit tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/) · 3:32

恰当的命名对单元测试项目的可读性和可维护性至关重要。
在项目、类和方法各个层级保持一致的命名模式,可以确保测试套件成为被测系统的有效文档。

### 核心概念

- **项目命名**:使用 `[ProjectBeingTested].Tests.[TestType]` 模式(例如 `CalculatorLibrary.Tests.Unit`)。
- **类命名**:与被测类保持一致,并在后面加上 `Tests`(例如 `CalculatorTests`)。
- **方法命名**:使用由下划线分隔的三段式结构:`[MethodName]_[ExpectedBehavior]_[Condition]`。
- **自文档化**:命名良好的测试让开发者无需阅读测试主体,就能理解测试的是什么,以及它为什么应该通过或失败。

### 课程笔记

#### Initial State

在默认或命名不佳的设置中,测试项目和类经常使用 `Tests` 或 `Test1` 这样的通用名称,这些名称对它们所验证的代码没有提供任何上下文。

```csharp
using CalculatorLibrary;
using Xunit;

namespace CalculatorLibraryTests;

public class Tests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/?t=10)

#### Project Naming Convention

测试项目应当以它所测试的库命名,后面跟上 `.Tests`,再跟上具体的测试类型。
这样一个库就可以同时拥有多个测试项目(Unit、Integration、End-to-End),并且仍然保持有序。

**模式**:`[LibraryName].Tests.[Type]`

```csharp
using Xunit;

namespace CalculatorLibrary.Tests.Unit;

public class Tests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/?t=70)

#### Class Naming Convention

测试类应当与源项目中它所负责测试的类直接对应。
标准约定是取目标类的名称,再在后面加上 `Tests` 这个词。

例如,如果测试的是 `Calculator` 类:

```csharp
namespace CalculatorLibrary;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public float Divide(float a, float b)
    {
        return a / b;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/?t=100)

对应的测试类应当命名为 `CalculatorTests`:

```csharp
using Xunit;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/?t=115)

#### Method Naming Convention

为了让单个测试易读且自文档化,使用由下划线分隔的三段式命名方式。
它清楚地定义了被测试的方法、预期结果,以及被测试的具体场景或状态。

**模式**:`[MethodName]_[Should_ExpectedBehavior]_[When_StateUnderTest]`

使用这个模式,`Add` 方法的测试就变得非常有描述性:

```csharp
using Xunit;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/naming-in-unit-tests-53926874/?t=160)

```text
  Add  _  ShouldAddTwoNumbers  _  WhenTwoNumbersAreIntegers
  |       |                       |
  |       |                       +-- [When_StateUnderTest]
  |       +-- [Should_ExpectedBehavior]
  +-- [MethodName]
```

虽然有些开发者喜欢在每个单词之间都加下划线,但使用三个清晰的段落能让测试名称一眼就能看懂。
这种结构确保任何查看测试结果的人都能立即理解什么失败了以及为什么失败,而无需调试测试代码。

## 6. Arrange, Act, Assert

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/arrange-act-assert-53926876/) · 2:09

### 总结

Arrange、Act、Assert(AAA)模式是 C# 单元测试最基本的结构标准。
它通过把测试划分为三个不同的阶段来实现清晰的关注点分离:搭建环境、执行目标逻辑、验证结果。
使用注释这样的物理边界来标记这些部分可以提升可读性,并帮助开发者避免混杂逻辑,这在复杂的测试场景中尤其有益。

### 核心概念

- **Triple A(AAA)结构**:把单元测试代码组织为 Arrange、Act 和 Assert 三个部分的标准约定。
- **Arrange**:初始化阶段,在这里准备被测对象以及任何必要的数据或依赖。
- **Act**:执行阶段,在这里调用被测试的具体方法或动作。
- **Assert**:验证阶段,在这里把动作的结果与预期值进行核对。
- **物理分隔**:使用代码注释显式标记三个阶段之间的边界,以提升可维护性。

### 课程笔记

Triple A(AAA)结构是编写单元测试的标准模式。
虽然简单的测试在没有显式组织的情况下看起来也很直观,但遵循这种结构可以确保整个测试套件的一致性。

```csharp
using Xunit;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        var calculator = new Calculator();

        var result = calculator.Add(5, 4);

        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/arrange-act-assert-53926876/?t=10)

#### The Three Components

1.  **Arrange**:在这里为测试设置状态。
    你实例化想要测试的类(通常称为 System Under Test,即 SUT),并准备任何输入变量或依赖。
    在某些情况下,准备工作可能发生在类的构造函数层面,而不是在测试方法内部。
2.  **Act**:这一部分包含对被测方法的实际调用。
    理想情况下,Act 部分应当简洁,通常只包含一次方法调用。
3.  **Assert**:这是最后一个阶段,在这里验证 SUT 的行为是否符合你的预期。
    这通常需要使用断言库把返回值与预期结果进行比较。

#### Using Comments for Structure

使用物理注释来分隔这三个类别是一种常见且推荐的做法。
虽然有些开发者在非常简单的测试中倾向于省略这些注释,但随着测试复杂度的增加,它们会变得必不可少。
没有清晰的边界,即使是经验丰富的工程师也很容易不小心把断言逻辑混入执行阶段,或者把准备逻辑混入执行阶段。

```csharp
namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/arrange-act-assert-53926876/?t=40)

显式标记这些边界起到了保护作用,能让测试保持可读,并防止逻辑渗透到不同的职责之间。
即使在某些特定框架中 Act 和 Assert 阶段偶尔会被合并,保持 AAA 的思维模型对于编写高质量的测试也至关重要。

## 7. xUnit's test execution model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/xunits-test-execution-model-53926877/) · 2:54

### 总结

本课探讨 xUnit 的测试执行模型,特别是 xUnit 如何管理测试类的实例化以确保测试隔离。
它解释了 xUnit 会为每一个测试方法的执行创建一个全新的测试类实例,从而防止状态在测试之间泄漏。
本课还介绍了"System Under Test"(SUT)命名约定,并演示如何在保持测试独立性的同时使用类级字段进行设置。

### 核心概念

*   **测试隔离**:xUnit 默认确保测试之间不会互相干扰。
*   **按测试实例化**:每次执行 `[Fact]` 或 `[Theory]` 时,都会创建一个新的测试类实例。
*   **System Under Test(SUT)**:对被测主要对象的标准命名约定。
*   **字段级设置**:由于这种执行模型,使用类字段进行初始化是安全且推荐的。
*   **ITestOutputHelper**:一个 xUnit 接口,用于向测试控制台写入输出。

### 课程笔记

在标准的单元测试中,被测试的对象被称为 **System Under Test** 或 **SUT**。
在一个典型的测试类中,你最初可能会在每个测试方法内部实例化 SUT:

```csharp
namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/xunits-test-execution-model-53926877/?t=10)

为了整理代码并遵循标准约定,你可以把实例化移到一个私有字段中。
借助 C# 的目标类型 `new()` 表达式,你可以在类级别定义 SUT。
这样就不必在每个测试方法的"Arrange"部分中实例化对象。

```csharp
namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/xunits-test-execution-model-53926877/?t=55)

#### The Execution Model

使用类级字段之所以安全,是因为 xUnit 的执行模型:xUnit 会为每一次测试执行创建一个**新的实例**。
这确保了每个测试的作用域都是自包含的。
一次单元测试的执行不会与另一次冲突,因为默认情况下实例之间不共享任何状态。

为了证明这种行为,我们可以使用一个 `Guid` 和 `ITestOutputHelper` 接口。
`ITestOutputHelper` 让我们可以把信息打印到测试输出控制台。
通过在构造函数中或作为字段初始化器创建一个新的 `Guid`,我们可以观察到每个测试都会得到一个唯一的 ID。

```csharp
using System;
using Xunit;
using Xunit.Abstractions;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();
    private readonly Guid _guid = Guid.NewGuid();

    private readonly ITestOutputHelper _outputHelper;

    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void TestGuid()
    {
        _outputHelper.WriteLine(_guid.ToString());
    }

    [Fact]
    public void TestGuid2()
    {
        _outputHelper.WriteLine(_guid.ToString());
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/xunits-test-execution-model-53926877/?t=145)

运行这些测试时,`TestGuid` 和 `TestGuid2` 会打印出不同的 GUID。
这证实了 xUnit 实例化了两次 `CalculatorTests` 类(每个测试各一次),从而产生了两个不同的 `_guid` 值。

```text
  TestGuid   --> new CalculatorTests() --> _guid = Guid.NewGuid()  (A)
  TestGuid2  --> new CalculatorTests() --> _guid = Guid.NewGuid()  (B)
```

这种强大的隔离模型可以防止一个测试的副作用影响另一个测试的结果。

## 8. Test Setup and Teardown

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/) · 5:46

### 总结

xUnit 借助标准的 C# 语言特性来处理测试的准备(setup)和清理(teardown),而不是依赖框架专用的特性。
准备工作在测试类的构造函数中完成,而清理工作则通过实现 IDisposable 接口来处理。
对于异步的初始化和清理,xUnit 提供了 IAsyncLifetime 接口。
关键在于,xUnit 会为每个测试方法创建一个新的测试类实例来确保测试隔离,这意味着准备和清理逻辑会针对每个测试独立执行。

### 核心概念

*   **基于构造函数的准备**:xUnit 使用类的构造函数来初始化测试状态。
*   **用 IDisposable 进行清理**:同步的清理逻辑放在 `Dispose` 方法中。
*   **IAsyncLifetime**:一个用于异步准备(`InitializeAsync`)和异步清理(`DisposeAsync`)的接口。
*   **测试隔离**:xUnit 为每个测试方法实例化一次测试类,以确保干净的状态。
*   **执行顺序**:生命周期依次为:Constructor → `InitializeAsync` → Test Method → `DisposeAsync` → `Dispose`。

### 课程笔记

#### Synchronous Setup and Teardown

在 xUnit 中,你不需要 `[SetUp]` 或 `[TearDown]` 这样的特殊特性。
相反,你使用标准的 C# 构造函数来进行准备。
每次运行测试时,xUnit 都会创建一个新的测试类实例。

```csharp
public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/?t=10)

要进行准备,就定义一个构造函数。
你也可以在这里注入 xUnit 提供的依赖,例如 `ITestOutputHelper`,它让你可以把输出写入测试结果。

```csharp
public class CalculatorTests
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    // Setup goes here
    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Hello from the constructor");
    }

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Add(5, 4);

        // Assert
        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/?t=55)

要进行清理或拆除,就实现 `IDisposable` 接口。
`Dispose` 方法会在每个测试结束后被自动调用。

```csharp
public class CalculatorTests : IDisposable
{
    // ... constructor and fields ...

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Add(5, 4);

        // Assert
        Assert.Equal(9, result);

        _outputHelper.WriteLine("Hello from the test");
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("Hello from cleanup");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/?t=115)

#### Test Isolation and Execution Flow

当同一个类中存在多个测试时,xUnit 会为每个测试独立地运行构造函数、测试方法和 `Dispose` 方法。
这确保了一个测试的状态不会泄漏到另一个测试中。

```csharp
    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        var result = _sut.Add(5, 4);
        Assert.Equal(9, result);
        _outputHelper.WriteLine("Hello from the Add test");
    }

    [Fact]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        var result = _sut.Subtract(10, 5);
        Assert.Equal(5, result);
        _outputHelper.WriteLine("Hello from the Subtract test");
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/?t=175)

```text
  Add test:       Constructor -> Add_... test method      -> Dispose
  Subtract test:  Constructor -> Subtract_... test method -> Dispose
```

#### Asynchronous Setup and Teardown

如果你的准备或清理需要异步代码(这在集成测试中很常见),就实现 `IAsyncLifetime` 接口。
它提供了 `InitializeAsync` 和 `DisposeAsync` 方法。

当构造函数和 `IAsyncLifetime` 同时存在时,执行顺序为:
1.  Constructor
2.  `InitializeAsync`
3.  Test Method
4.  `DisposeAsync`
5.  `Dispose`(如果同时实现了 `IDisposable`)

```csharp
public class CalculatorTests : IAsyncLifetime
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Hello from the constructor");
    }

    public async Task InitializeAsync()
    {
        await Task.Delay(1);
        _outputHelper.WriteLine("Hello from InitializeAsync");
    }

    public async Task DisposeAsync()
    {
        await Task.Delay(1);
        _outputHelper.WriteLine("Hello from DisposeAsync");
    }

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        var result = _sut.Add(5, 4);
        Assert.Equal(9, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/test-setup-and-teardown-53926878/?t=310)

## 9. Parameterizing tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/parameterizing-tests-53926879/) · 3:18

### 总结

xUnit 中的参数化测试让开发者可以针对多组输入数据执行同一套测试逻辑,从而显著减少代码重复并提高测试覆盖率。
通过用 [Theory] 替换 [Fact] 特性并使用 [InlineData],单个测试方法就可以接受代表不同场景(例如边界情况或负值)的参数。
这种数据驱动的方式确保测试运行器把每一组输入都视为一个独立的测试用例,在保持代码库整洁、可维护的同时提供清晰的反馈。

### 核心概念

*   **[Fact] 与 [Theory]**:`[Fact]` 用于没有参数、始终成立的测试,而 `[Theory]` 用于针对特定一组数据成立的测试。
*   **[InlineData]**:用于向 `[Theory]` 测试方法的参数传递常量值的特性。
*   **参数映射**:`[InlineData]` 中提供的值按位置映射到测试方法的参数上。
*   **DRY 原则**:参数化避免了为不同的输入值复制粘贴测试逻辑。
*   **测试执行**:xUnit 会为与某个 `Theory` 关联的每一个 `[InlineData]` 特性生成一个独立的测试结果。

### 课程笔记

在标准的单元测试中,标记了 `[Fact]` 特性的测试方法通常用硬编码的值验证单一用例。
例如,计算器 `Add` 方法的测试可能只验证 5 加 4 等于 9。

```csharp
private readonly Calculator _sut = new();

[Fact]
public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
{
    // Act
    var result = _sut.Add(5, 4);

    // Assert
    Assert.Equal(9, result);
}

[Fact]
public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
{
    // Act
    var result = _sut.Subtract(10, 5);

    // Assert
    Assert.Equal(5, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/parameterizing-tests-53926879/?t=10)

虽然这种方式可行,但通常还需要测试多种场景,例如零相加或负数相加。
xUnit 允许优雅地进行参数化,而不必复制测试方法再稍微修改名称和参数。

要实现这一点,需要把 `[Fact]` 特性替换为 `[Theory]`。
`Theory` 告诉测试运行器这个方法需要参数。
这些参数通过 `[InlineData]` 特性提供。
`[InlineData]` 中提供的值必须与方法签名的类型和顺序相匹配。

```csharp
[Theory]
[InlineData(5, 4, 9)]
public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Add(a, b);

    // Assert
    Assert.Equal(expected, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/parameterizing-tests-53926879/?t=115)

在上面的示例中,值 `5` 对应 `a`,`4` 对应 `b`,`9` 对应 `expected`。
这种结构允许添加多个 `[InlineData]` 特性来覆盖各种测试用例,而无需重写测试本身的逻辑。

```text
  [InlineData( 5,      4,      9 )]
               |       |       |
               v       v       v
         (int a, int b, int expected)
```

```csharp
public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(5, 4, 9)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, -5, -10)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/parameterizing-tests-53926879/?t=180)

执行这些测试时,测试运行器会为每个 `[InlineData]` 特性创建一个独立的测试结果。
这种方式最大限度地复用了代码,并确保所有相关的参数组都能被高效地断言。

## 10. Ignoring tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/ignoring-tests-53926880/) · 1:23

### 总结

xUnit 提供了一种直接的机制来忽略那些不应在测试运行中执行的测试,例如当前正在失败的测试,或与特定环境无关的测试。
通过在测试特性中使用 Skip 属性,开发者可以为跳过某个测试提供一个必填的原因。
这项功能可以用于单个测试级别(使用 Fact 特性)、整个参数化集合(使用 Theory 特性),或特定的数据用例(使用 InlineData 特性)。

### 核心概念

- xUnit 特性中的 `Skip` 属性。
- 使用 `[Fact(Skip = "reason")]` 忽略单个测试。
- 使用 `[Theory(Skip = "reason")]` 忽略整个 theory。
- 使用 `[InlineData(..., Skip = "reason")]` 忽略 theory 中的特定测试用例。
- 跳过测试必须提供一个字符串原因。

### 课程笔记

在 xUnit 中,你可以轻松跳过那些不想在测试运行中执行的测试。
这对于正在开发中的测试,或已知会在持续集成(CI)等特定环境中失败的测试很有用。

要忽略一个普通测试,你可以在 `[Fact]` 特性上使用 `Skip` 属性。
这个属性需要一个字符串值,用来说明跳过该测试的原因。

```csharp
public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Fact(Skip = "This is just a test")]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Subtract(10, 5);

        // Assert
        Assert.Equal(5, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/ignoring-tests-53926880/?t=25)

运行这个测试时,测试运行器会忽略它,并显示所提供的消息。
这个测试既不会通过也不会失败;它只是被完全绕过。

同样的逻辑也适用于 theory。
如果你把 `Skip` 属性应用到 `[Theory]` 特性上,由关联的 `[InlineData]` 特性定义的每一个测试用例都会被忽略。

```csharp
[Theory(Skip = "This breaks in CI")]
[InlineData(5, 4, 9)]
[InlineData(0, 0, 0)]
[InlineData(-5, -5, -10)]
public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Add(a, b);

    // Assert
    Assert.Equal(expected, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/ignoring-tests-53926880/?t=55)

或者,你可能只想跳过 theory 中的某一组特定数据,同时让其他用例正常运行。
xUnit 支持这一点,允许在单个 `[InlineData]` 特性上设置 `Skip` 属性。

```csharp
[Theory]
[InlineData(5, 4, 9, Skip = "This breaks in CI")]
[InlineData(0, 0, 0)]
[InlineData(-5, -5, -10)]
public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Add(a, b);

    // Assert
    Assert.Equal(expected, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/ignoring-tests-53926880/?t=75)

在这种场景下,第一个测试用例会以指定的原因被忽略,而其余两个测试用例照常执行。

## 11. Your turn to practice

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/your-turn-to-practice-53926881/) · 1:04

### 总结

本课提供一个动手练习,用于巩固 C# 单元测试的基础知识。
学员的任务是为一个 Calculator 库实现一组单元测试,覆盖加法、减法、乘法和除法。
这个练习强调使用 xUnit 的 [Fact] 和 [Theory] 特性,并配合 [InlineData] 进行参数化测试,以对照预期结果验证数学运算。

### 核心概念

- [Fact] 和 [Theory] 特性的实际应用。
- 使用 [InlineData] 进行数据驱动测试。
- 测试基本算术运算(Add、Subtract、Multiply、Divide)。
- 按照 Arrange-Act-Assert(AAA)模式组织测试方法。
- 使用 Assert.Equal 验证结果。

### 课程笔记

这个练习的首要目标是把单元测试理论应用到一个具体的实现上。
测试的目标是 `Calculator` 库,它包含用于基本算术运算的方法。
练习需要下载提供的项目,并在解决方案中找到 `CalculatorLibrary.Tests.Unit` 项目。

#### Exercise Requirements

练习要求为以下运算编写单元测试,并确保它们能正确地对照预期结果进行断言:

1. **加法**:
   - 5 + 5 = 10
   - -5 + 5 = 0
   - -15 + (-5) = -20
2. **减法**:
   - 5 - 5 = 0
   - 15 - 5 = 10
   - -5 - (-5) = 0
   - -15 - (-5) = -10
   - 5 - 10 = -5
3. **乘法**:
   - 5 * 5 = 25
   - 50 * 0 = 0
   - -5 * 5 = -25
4. **除法**:
   - 5 / 5 = 1
   - 15 / 5 = 3
   - 注意:在这个练习中,应忽略除以零这样的数学边界情况。

#### Implementation Guidance

测试应当在 `CalculatorTests` 类中实现。
对单个方法的多个测试用例,使用 `[Theory]` 和 `[InlineData]` 来高效处理;对单一场景的测试,则使用 `[Fact]`。
下面的代码提供了测试套件的起点,演示了如何为加法构建一个 theory、为减法构建一个 fact:

```csharp
using Xunit;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(5, 4, 9)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, -5, -10)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Subtract(10, 5);

        // Assert
        Assert.Equal(5, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/your-turn-to-practice-53926881/?t=25)

`_sut`(System Under Test)作为私有字段只初始化一次,供所有测试方法使用。
每个测试都遵循标准模式:调用被测方法(Act),并使用 `Assert.Equal` 验证输出(Assert)。
当所有测试都按照提供的数据点如预期般通过时,就算成功完成了实现。

## 12. My answer to the exercise

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/my-answer-to-the-exercise-53926882/) · 0:40

### 总结

本课演示如何使用 xUnit 为基本算术运算实现参数化单元测试。
它展示了如何使用 `[Theory]` 和 `[InlineData]` 特性针对同一套逻辑执行多个测试用例,确保 `Subtract`、`Multiply` 和 `Divide` 等方法在各种整数输入下都能正确运行。

### 核心概念

* 使用 `[Theory]` 和 `[InlineData]` 特性进行参数化测试。
* 一致的测试命名约定:`MethodName_ShouldExpectedBehavior_WhenStateUnderTest`。
* 实现 AAA(Arrange、Act、Assert)模式。
* 在单个测试方法中验证多种场景(正数、负数和零)。

### 课程笔记

计算器练习的解答利用 xUnit 基于 theory 的测试来验证算术运算。
每个测试方法都遵循一致的命名约定,以确保测试结果清晰明了。

对于减法运算,测试验证两个整数之差是否被正确计算。
`[InlineData]` 特性提供了各种测试用例,包括结果为负值的场景。

```csharp
[Theory]
[InlineData(-15, -5, -10)]
[InlineData(5, 10, -5)]
public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Subtract(a, b);

    // Assert
    Assert.Equal(expected, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/my-answer-to-the-exercise-53926882/?t=10)

类似地,乘法测试使用 `[Theory]` 特性来验证两个整数的乘积。
这包括乘以零、正数与负数相乘这样的边界情况,以确保逻辑能处理所有整数变化。

```csharp
[Theory]
[InlineData(5, 5, 25)]
[InlineData(50, 0, 0)]
[InlineData(-5, 5, -25)]
public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Multiply(a, b);

    // Assert
    Assert.Equal(expected, result);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/my-answer-to-the-exercise-53926882/?t=25)

同样的模式也应用于除法方法,确保不同运算的全部 13 个测试用例都成功通过。
使用这些特性可以在保持测试套件整洁、易读的同时实现大量的测试覆盖。

## 13. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53926883/) · 0:48

### 总结

本课全面总结了本章所涵盖的单元测试基础概念。
它回顾了 xUnit 框架的使用、项目组织与命名的最佳实践、Arrange-Act-Assert(AAA)模式,以及生命周期管理和参数化测试等 xUnit 进阶功能。

### 核心概念

* xUnit 框架基础
* 项目与解决方案的组织
* 测试项目、类和方法的命名约定
* Arrange-Act-Assert(AAA)模式
* xUnit 测试执行生命周期
* 同步与异步的准备和清理
* 使用 `[Theory]` 和 `[InlineData]` 进行参数化测试
* 跳过或忽略测试

### 课程笔记

本章建立了使用 xUnit 框架进行 C# 单元测试的基础。
它涵盖了测试项目的初始搭建以及基本测试用例的实现。

本章通过标准化的项目和解决方案布局强调了结构上的组织,确保测试代码与生产代码保持分离。
命名约定被应用于项目、类和方法各个层级,以增强可维护性和可读性。
在单个测试内部,使用 Arrange-Act-Assert(AAA)模式为数据准备、执行和验证提供一致的结构。

本章详细讲解了 xUnit 的测试执行模型,解释了框架如何管理测试类的生命周期。
这包括准备和清理逻辑的实现,同时支持同步和异步两种执行模式。

进阶测试技巧包括使用 `[Theory]` 和 `[InlineData]` 特性进行参数化,让单个测试方法可以用多组数据执行。
本章还演示了如何把特定的测试或数据点排除在执行之外。
最后,这些概念通过实际应用和动手练习得到了巩固。

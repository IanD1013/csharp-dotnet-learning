# Testing techniques (Optional section)

> 课程：[From Zero to Hero: Integration testing in ASP.NET Core](https://dometrain.com/course/from-zero-to-hero-integration-testing-in-asp-net-core/) · 第 4 章
> 共 12 课 · 约 26 分钟
> 来源：Dometrain。由课程文档翻译整理；每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What this section is about](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-this-section-is-about-53953859/) | 0:31 | [↓](#1-what-this-section-is-about) |
| 2 | [Writing fluent assertions](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-fluent-assertions-53953860/) | 3:19 | [↓](#2-writing-fluent-assertions) |
| 3 | [Testing strings](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/) | 2:58 | [↓](#3-testing-strings) |
| 4 | [Testing numbers](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-numbers-53953863/) | 2:00 | [↓](#4-testing-numbers) |
| 5 | [Testing dates](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-dates-53953864/) | 1:37 | [↓](#5-testing-dates) |
| 6 | [Testing objects](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-objects-53953865/) | 3:32 | [↓](#6-testing-objects) |
| 7 | [Testing enumerables](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/) | 3:32 | [↓](#7-testing-enumerables) |
| 8 | [Testing methods that throw Exceptions](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/) | 2:23 | [↓](#8-testing-methods-that-throw-exceptions) |
| 9 | [Testing for raised events](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-for-raised-events-53953868/) | 1:29 | [↓](#9-testing-for-raised-events) |
| 10 | [Testing private methods (members)](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-private-methods-members-53953869/) | 1:24 | [↓](#10-testing-private-methods-members) |
| 11 | [Testing internal methods (members)](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/) | 3:01 | [↓](#11-testing-internal-methods-members) |
| 12 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953871/) | 0:26 | [↓](#12-section-recap) |

## 1. What this section is about

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-this-section-is-about-53953859/) · 0:31

### 总结

这个可选章节介绍在 C# 中使用 Fluent Assertions 的进阶测试技巧。
虽然这些内容来自一门单元测试课程，并非 ASP.NET Core 集成测试所独有，但它提供了对各种数据类型进行断言、处理异常以及监控事件的必备知识。
掌握这些技巧能提升集成测试套件中断言的表达力和可靠性。

### 核心概念

- 使用 Fluent Assertions 编写可读、富有表达力的测试验证。
- 对不同的 C# 数据类型（字符串、数字、日期、对象）进行断言。
- 测试集合和复杂的对象图。
- 验证异常和事件的触发。
- 使用 `InternalsVisibleTo` 特性测试 internal 成员。

### 课程笔记

本章节重点介绍 C# 中的进阶断言技巧。
虽然这些材料并不专门针对 ASP.NET Core 集成测试，但对于需要在测试套件中验证复杂数据类型和行为的开发者来说非常有益。
主要目标是让开发者熟悉 **Fluent Assertions**，以及如何有效地对不同类型的数据进行断言。

本章节使用一个专门的项目来演示这些概念，从基础的 xUnit 断言进一步走向更具表达力的验证逻辑。
在转向 fluent 语法之前，本课先回顾了标准的 xUnit 模式，例如使用 `Theory` 和 `InlineData` 进行数据驱动测试：

```csharp
[Theory]
[InlineData("9FD9A907-E86E-43BC-9A80-F2B0F274133F", Skip = "This doesn't work atm sorry")]
[InlineData("45905930-B282-4330-891F-381BD951D2BC")]
[InlineData("6728cd57-1e42-4fd0-9867-75a090fa2ce3")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-this-section-is-about-53953859/?t=10)

掌握这些技巧后，开发者可以编写更易维护、更易读的测试。
接下来的课程涵盖了广泛的场景，包括字符串、数值和日期断言；对象等价性和集合验证；异常处理和事件监控；以及通过把 internal 成员暴露给测试项目来测试它们。

## 2. Writing fluent assertions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-fluent-assertions-53953860/) · 3:19

### 总结

本课介绍 Fluent Assertions，它是标准 xUnit Assert 类的一个更易读、更具表达力的替代方案。
本课演示了如何通过 NuGet 安装该库，并把现有测试重构为使用 Should().Be() 语法，这既提升了测试代码的清晰度，也提升了断言失败时错误信息的质量。

### 核心概念

*   标准 xUnit 断言在可读性和优雅性方面的局限。
*   Fluent Assertions NuGet 包简介。
*   使用流畅、近似英语的语法带来的可读性收益。
*   测试失败时改进的诊断错误信息。
*   把标准断言重构为 `Should().Be()` 模式。

### 课程笔记

断言是单元测试和集成测试的核心支柱，它是验证系统实际输出与预期结果是否一致的机制。
如果断言失败，它会抛出一个异常，导致测试失败。
在标准的 xUnit 测试中，这通常通过静态的 `Assert` 类来完成。

```csharp
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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-fluent-assertions-53953860/?t=25)

标准的 `Assert` 语法虽然能用，但通常被认为不够优雅，也更难一眼读懂。
Fluent Assertions 是一个广受认可的 NuGet 包，它为这些检查提供了更自然、更流畅的 API。
除了观感上的改进之外，Fluent Assertions 还提供了更出色的错误信息，清楚地说明出了什么问题，让诊断失败更加容易。

要使用 Fluent Assertions，必须把这个包添加到测试项目中。
安装之后，语法就从静态方法调用转变为扩展方法的方式。
你不再把预期值和实际值传给 `Assert.Equal()`，而是在结果上调用 `.Should()`，再接上想要的断言。

```csharp
[InlineData(-5, 5, 0)]
[InlineData(-15, -5, -20)]
public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Add(a, b);

    // Assert
    //Assert.Equal(expected, result);
    result.Should().Be(expected);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-fluent-assertions-53953860/?t=130)

这种语法让断言读起来像普通英语："result should be expected"。
这种方式能与描述性的测试命名约定无缝结合，从而得到一个高度可读、易于维护的测试套件。

```csharp
[InlineData(-5, 5, 0)]
[InlineData(-15, -5, -20)]
public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
    int a, int b, int expected)
{
    // Act
    var result = _sut.Add(a, b);

    // Assert
    Assert.Equal(expected, result);
    result.Should().Be(expected);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-fluent-assertions-53953860/?t=160)

在整个项目中用 Fluent Assertions 替换标准 xUnit 断言之后，每个测试的意图都变得更清晰，失败时的反馈环也得到了改善。

```csharp
    [Theory]
    [InlineData(5, 5, 1)]
    [InlineData(15, 5, 3)]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    { 
        // Act
        var result = _sut.Divide(a, b);

        // Assert
        result.Should().Be(expected);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-fluent-assertions-53953860/?t=190)

## 3. Testing strings

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/) · 2:58

本课介绍如何在 .NET 测试环境中使用 Fluent Assertions 验证字符串值。
本课演示了如何超越简单的相等检查，使用更具描述性的断言，例如验证字符串不为空、以特定前缀开头或以特定后缀结尾。
借助 Fluent Assertions API，开发者可以为基于字符串的数据编写更具表达力、更易维护的测试。

### 核心概念

- **字符串的 Fluent Assertions**：使用 `.Should()` 扩展方法访问字符串专用的验证方法。
- **相等断言**：使用 `.Be()` 验证字符串的确切内容。
- **状态断言**：使用 `.NotBeEmpty()` 确保字符串包含数据。
- **模式断言**：使用 `.StartWith()` 和 `.EndWith()` 验证值开头或结尾的子字符串。
- **API 可扩展性**：Fluent Assertions 还支持其他检查，包括长度、正则表达式匹配和大小写。

### 课程笔记

为了演示字符串断言，我们使用一个 `ValueSamples` 类作为被测系统（SUT）。
这个类包含多个属性，其中包括一个 `FullName` 字符串。

```csharp
namespace TestingTechniques;

public class ValueSamples
{
    public string FullName = "Nick Chapsas";

    public int Age = 21;

    public DateOnly DateOfBirth = new (2000, 6, 9);

    public User AppUser = new()
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new (2000, 6, 9)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=25)

测试类 `ValueSamplesTests` 在其构造函数中或作为私有字段实例化 SUT。
由于 xUnit 会为每个测试方法创建一个新的测试类实例，我们可以放心地共享 SUT 实例，而不必担心测试之间的状态泄漏。

```csharp
namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=40)

在为字符串断言创建测试方法时，我们定义一个标准的 xUnit `[Fact]`。

```csharp
using Xunit;

namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=70)

最常见的断言是检查是否完全相等。
使用 Fluent Assertions 时，我们在字符串变量上调用 `.Should().Be()`。

```csharp
namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=105)

除了简单的相等之外，你可能还需要验证字符串的其他特征。
例如，你可以使用 `.NotBeEmpty()` 断言字符串不为空。

```csharp
public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");

        fullName.Should().NotBeEmpty();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=120)

你还可以验证字符串以特定的子字符串开头。
这对于验证格式或预期的前缀很有用。

```csharp
public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");

        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Nick")
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=130)

类似地，`.EndWith()` 方法让你可以断言字符串的后缀。
Fluent Assertions 还为字符串提供了大量其他方法，包括检查长度、按正则表达式匹配或验证大小写。

```csharp
public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");

        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Nick");
        fullName.Should().EndWith("Chapsas");
        fullName.Should()
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=145)

最终的测试结构把这些断言组合起来，以确保该字符串属性满足所有业务要求。

```csharp
public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");

        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Nick");
        fullName.Should().EndWith("Chapsas");

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-strings-53953861/?t=170)

## 4. Testing numbers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-numbers-53953863/) · 2:00

### 总结

Fluent Assertions 为在 C# 测试中验证数值提供了专门的 API，提供的不只是简单的相等检查。
通过使用 BePositive、BeGreaterThan 和 BeInRange 等方法，开发者可以编写高度可读的断言，并在预期不满足时得到清晰、易于理解的错误信息。

### 核心概念

* **相等断言**：使用 `.Should().Be()` 进行精确匹配。
* **比较断言**：使用 `.BeGreaterThan()` 和 `.BeLessOrEqualTo()` 等方法进行边界测试。
* **范围验证**：使用 `.BeInRange(min, max)` 验证某个值落在特定的闭区间内。
* **符号断言**：使用 `.BePositive()` 确保某个值大于零。
* **描述性错误信息**：Fluent Assertions 会生成易于理解的输出，确切说明预期的是什么、实际找到的是什么。

### 课程笔记

数值断言是使用 Fluent Assertions 库针对属性或变量执行的。
在下面的示例中，被测系统（SUT）是一个包含 `Age` 属性的 `ValueSamples` 类。

```csharp
public class ValueSamples
{
    public string FullName = "Nick Chapsas";

    public int Age = 21;

    public DateOnly DateOfBirth = new (2000, 6, 9);

    public User AppUser = new()
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new (2000, 6, 9)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-numbers-53953863/?t=10)

对数字最基础的断言是检查相等。
Fluent Assertions 能感知内容；当应用于整数时，它会提供相关的方法。
如果断言失败，该库会清楚地解释差异所在。
例如，如果测试预期年龄为 22，而实际是 21，错误信息会是："expected age to be 22, but found 21."

```csharp
[Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(22);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-numbers-53953863/?t=35)

除了简单的相等之外，Fluent Assertions 还包含若干专门的方法，用来处理常见的数值要求。
这些方法让测试代码读起来像自然语言：

- `BePositive()`：断言该值大于零。
- `BeGreaterThan(value)`：断言该值超过某个特定阈值。
- `BeLessOrEqualTo(value)`：断言该值不超过某个特定阈值。
- `BeInRange(min, max)`：断言该值落在指定的最小值和最大值之间（含两端）。

这些断言可以在单个测试中组合使用，以验证一个数值的多个属性。

```csharp
}

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(21);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20);
        age.Should().BeLessOrEqualTo(21);
        age.Should().BeInRange(18, 60);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-numbers-53953863/?t=115)

## 5. Testing dates

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-dates-53953864/) · 1:37

测试日期是集成测试中的常见需求，尤其是在验证领域模型或用户资料时。
FluentAssertions 为针对 `DateOnly` 等日期类型的断言提供了易于阅读的语法。

### 核心概念

- **相等断言**：使用 `.Should().Be()` 验证某个特定日期。
- **范围断言**：使用 `.Should().BeInRange()` 验证日期落在两个边界之间。
- **比较断言**：使用 `.Should().BeGreaterThan()` 或 `.Should().BeLessOrEqualTo()` 进行相对日期检查。
- **描述性失败信息**：FluentAssertions 会提供清晰的错误信息，同时指出预期的和实际的日期值。

### 课程笔记

在测试一个包含日期信息的类（例如用户资料）时，你通常会针对被测系统（SUT）的属性进行断言。
在下面的示例中，`ValueSamples` 类包含一个类型为 `DateOnly` 的 `DateOfBirth` 属性。

```csharp
public class ValueSamples
{
    public string FullName = "Nick Chapsas";

    public int Age = 21;

    public DateOnly DateOfBirth = new (2000, 6, 9);

    public User AppUser = new()
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new (2000, 6, 9)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-dates-53953864/?t=25)

最直接的断言是检查日期是否完全匹配。
如果断言失败，FluentAssertions 会输出一条有用的信息，说明预期的是什么、实际找到的是什么。

```csharp
age.Should().Be(21);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20);
        age.Should().BeLessOrEqualTo(21);
        age.Should().BeInRange(18, 60);
    }

    [Fact]
    public void DateAssertionExample()
    {
        var dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new(2000, 6, 9));
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-dates-53953864/?t=40)

除了简单的相等之外，你还可以断言某个日期落在特定范围内。
这对于验证某个值是否在预期的时间范围内生成很有用。

```csharp
age.Should().Be(21);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20);
        age.Should().BeLessOrEqualTo(21);
        age.Should().BeInRange(18, 60);
    }

    [Fact]
    public void DateAssertionExample()
    {
        var dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new(2000, 6, 9));
        dateOfBirth.Should().BeInRange(new(2000, 1, 1), new(2001, 1, 1));
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-dates-53953864/?t=70)

最后，`BeGreaterThan` 这类标准比较操作也可用于确保某个日期晚于某个时间点，从而保持测试套件高度可读、富有表达力。

```csharp
age.Should().BeGreaterThan(20);
        age.Should().BeLessOrEqualTo(21);
        age.Should().BeInRange(18, 60);
    }

    [Fact]
    public void DateAssertionExample()
    {
        var dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new(2000, 6, 9));
        dateOfBirth.Should().BeInRange(new(2000, 1, 1), new(2001, 1, 1));
        dateOfBirth.Should().BeGreaterThan(new(2000, 1, 1));
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-dates-53953864/?t=90)

## 6. Testing objects

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-objects-53953865/) · 3:32

### 总结

在 ASP.NET Core 中测试对象时，标准的相等断言经常会失败，因为类是引用类型，这意味着两个数据完全相同的对象如果位于不同的内存位置，就会被视为不相等。
虽然开发者常常退而求其次，逐个断言每个属性，但 Fluent Assertions 通过 BeEquivalentTo 方法提供了一个强大的替代方案。
这个方法会在运行时对对象的属性进行动态的深度比较，让断言简洁而稳健，关注的是数据的"外观和感觉"，而不是引用标识或严格的类型匹配。

### 核心概念

* 引用相等与值相等：类是引用类型；标准的相等检查比较的是内存地址，而不是数据。
* xUnit Assert.Equal 的局限：对引用类型会失败，除非重写了相等成员。
* Fluent Assertions BeEquivalentTo：执行递归的、基于属性的比较。
* 结构兼容性：只要属性名和值相匹配，BeEquivalentTo 就能比较不同的类型。

### 课程笔记

在测试对象（具体来说是类）时，开发者会遇到值类型与引用类型之间的区别。
对于 `int`、`string`（在相等性上表现得像值类型）和 `DateOnly` 这样的值类型，相等是基于值的。
然而，类是引用类型。

```csharp
{
    public string FullName = "Nick Chapsas";

    public int Age = 21;

    public DateOnly DateOfBirth = new (2000, 6, 9);

    public User AppUser = new()
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new (2000, 6, 9)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-objects-53953865/?t=10)

如果你尝试使用标准的 xUnit 断言把一个预期对象与一个实际对象进行比较，即使属性值完全相同，测试也会失败。

```csharp
public void ObjectAssertionExample()
    {
        var expected = new User
        {            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new(2000, 6, 9)
        };

        var user = _sut.AppUser;

        Assert.Equal(expected, user);
        user.Should().Be()
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-objects-53953865/?t=60)

出现这种失败，是因为 `Assert.Equal` 和基础的 Fluent Assertions `Should().Be()` 方法检查的是引用相等。
由于 `expected` 和 `user` 对象分配在不同的内存空间中，它们不被视为同一个对象。

```csharp
[Fact]
public void ObjectAssertionExample()
{
    var expected = new User
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new(2000, 6, 9)
    };

    var user = _sut.AppUser;

    //Assert.Equal(expected, user);
    user.Should().Be(expected);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-objects-53953865/?t=100)

```text
 expected ---> +---------------------------------+
               | User                            |
               |   FullName    = "Nick Chapsas"  |
               |   Age         = 21              |
               |   DateOfBirth = new(2000, 6, 9) |
               +---------------------------------+

 user -------> +---------------------------------+
 (_sut.AppUser)| User                            |
               |   FullName    = "Nick Chapsas"  |
               |   Age         = 21              |
               |   DateOfBirth = new(2000, 6, 9) |
               +---------------------------------+

 Assert.Equal / Should().Be()  : 不同的内存空间 -> 失败
 Should().BeEquivalentTo()     : 属性值匹配     -> 通过
```

为了在不手动断言每个属性的情况下解决这个问题，Fluent Assertions 提供了 `BeEquivalentTo` 方法。
这个方法会在运行时动态检查两个对象的属性，并确保它们的值相匹配。

```csharp
[Fact]
public void ObjectAssertionExample()
{
    var expected = new User
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new(2000, 6, 9)
    };

    var user = _sut.AppUser;

    //Assert.Equal(expected, user);
    //user.Should().Be(expected);
    user.Should().BeEquivalentTo(expected);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-objects-53953865/?t=175)

`BeEquivalentTo` 非常灵活；只要不同类型拥有相同的属性名和值，它甚至允许在它们之间进行比较。
这使它成为一个强大的工具，可以断言对象具有正确的"外观和感觉"，而不受严格的引用或类型约束。

## 7. Testing enumerables

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/) · 3:32

### 总结

本课介绍在 ASP.NET Core 测试中使用 Fluent Assertions 对集合和可枚举对象进行断言的技巧。
本课强调了在检查包含关系时值类型与引用类型之间的关键区别，演示了如何对集合元素执行深度的结构相等检查，并介绍了使用 lambda 表达式进行的灵活匹配。

### 核心概念

* 集合中的引用类型与值类型相等
* 使用 `ContainEquivalentOf` 进行结构相等比较
* 使用 `Contain(lambda)` 进行基于谓词的匹配
* 使用 `HaveCount` 验证集合大小
* 使用 `As<T[]>()` 避免多次枚举

### 课程笔记

在测试一个包含对象的可枚举集合（例如一个 `User` 记录数组）时，目标通常是根据属性值而不是内存引用来验证某个特定元素是否存在。

```csharp
FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new (2000, 6, 9)
    };

    public IEnumerable<User> Users = new[]
    {
        new User()
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        },
        new User()
        {
            FullName = "Tom Scott",
            Age = 37,
            DateOfBirth = new (1984, 6, 9)
        },
        new User()
        {
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=10)

开始测试时，先定义一个预期对象，使其与你打算在集合中找到的数据相匹配。

```csharp
[Fact]
    public void EnumerableObjectsAssertionExample()
    {
        var expected = new User
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        };
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=25)

在处理 `IEnumerable` 时，把集合转换为数组（例如使用 `.As<User[]>()`）通常是有益的，可以避免在多次断言时发生多次枚举。

```csharp
[Fact]
    public void EnumerableObjectsAssertionExample()
    {
        var expected = new User
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        };

        var users = _sut.Users.As<User[]>();
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=40)

在引用类型的集合上使用标准的 `.Should().Contain(expected)` 方法时，如果实例不同，即使数据完全相同也会失败。
这是因为对于对象，`Contain` 默认使用引用相等。

```csharp
users.Should().Contain(expected);
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=70)

要执行检查每个属性的结构比较，请使用 `ContainEquivalentOf`。
这能确保只要集合中有任何一个对象的值相匹配，测试就会通过。

```csharp
[Fact]
    public void EnumerableObjectsAssertionExample()
    {
        var expected = new User
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        };

        var users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=85)

Fluent Assertions 还提供了使用 `HaveCount` 验证集合大小的方法。

```csharp
[Fact]
    public void EnumerableObjectsAssertionExample()
    {
        var expected = new User
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        };

        var users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);
        users.Should().HaveCount(3);

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=110)

为了获得更大的灵活性，你可以在 `Contain` 方法中使用 lambda 表达式，根据特定条件匹配元素，而无需一个完整的预期对象。

```csharp
var expected = new User
        {
            FullName = "Nick Chapsas",
            Age = 21,
            DateOfBirth = new (2000, 6, 9)
        };

        var users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);
        users.Should().HaveCount(3);
        users.Should().Contain(x => x.FullName.StartsWith("Nick") && x.Age > 5);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=130)

在处理值类型（如 `int` 或 `string`）时，标准的 `Contain` 方法能按预期工作，因为相等是基于值本身的。

```csharp
};

        var users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);
        users.Should().HaveCount(3);
        users.Should().Contain(x => x.FullName.StartsWith("Nick") && x.Age > 5);
    }

    [Fact]
    public void EnumerableNumbersAssertionExample()
    {
        var numbers = _sut.Numbers.As<int[]>();

        numbers.Should().Contain(5);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-enumerables-53953866/?t=190)

Fluent Assertions 还提供了许多其他集合专用的方法，包括 `OnlyContain`、`IntersectWith`、`HaveElementAt` 和 `BeOfType`。

## 8. Testing methods that throw Exceptions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/) · 2:23

### 总结

测试会抛出异常的方法时，需要把调用包装在 Action 或 Func 中，以防止测试在 Act 阶段就失败。
借助 Fluent Assertions，你接下来可以断言该 action Should().Throw<TException>()，并链式调用 .WithMessage() 等进一步的验证，以确保到达了正确的错误状态。

### 核心概念

- 把可能抛出异常的代码包装在 Action 或 Func 中。
- 防止测试在 Act 阶段失败。
- 使用 Should().Throw<T>() 验证异常类型。
- 使用 .WithMessage() 进行流畅的链式调用，实现更细致的验证。

### 课程笔记

在测试以抛出异常为预期结果的逻辑时（例如在计算器类中除以零），标准的测试结构会过早失败。
如果直接调用该方法，异常会在 Act 阶段抛出，测试运行器会在到达 Assert 阶段之前就停止。

```csharp
[Fact]
public void ExceptionThrownAssertionExample()
{
    var calculator = new Calculator();

    var result = calculator.Divide(1, 0);

    result.Should().Be(0);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/?t=60)

要解决这个问题，必须把会抛出异常的代码包装在 `Action` 或 `Func` 中。
这会把逻辑封装进一个委托中，由断言库去调用和检查，而不是立即执行。

```csharp
[Fact]
public void ExceptionThrownAssertionExample()
{
    var calculator = new Calculator();

    Action result = () => calculator.Divide(1, 0);

    result.Should().Be(0);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/?t=75)

如果返回类型与测试上下文相关，可以使用 `Func<T>`，但对于异常测试来说，`Action` 通常就足够了，因为发生异常时根本拿不到返回值。

```csharp
[Fact]
public void ExceptionThrownAssertionExample()
{
    var calculator = new Calculator();

    Func<int> result = () => calculator.Divide(1, 0);

    result.Should().Be(0);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/?t=85)

把逻辑包装进 `Action` 之后，你就可以使用 Fluent Assertions 的 `Should().Throw<TException>()` 方法来验证抛出了特定的异常类型。
这样，只要在断言阶段捕获到了指定的异常，测试就会通过。

```csharp
[Fact]
public void ExceptionThrownAssertionExample()
{
    var calculator = new Calculator();

    Action result = () => calculator.Divide(1, 0);

    result.Should().Throw<DivideByZeroException>();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/?t=100)

为了让测试更稳健，你可以链式添加更多断言来验证异常的属性，例如错误信息。
`.WithMessage()` 方法让你可以确保异常包含预期的描述文本，形成一条流畅、易读的验证链。

```csharp
[Fact]
public void ExceptionThrownAssertionExample()
{
    var calculator = new Calculator();

    Action result = () => calculator.Divide(1, 0);

    result.Should()
        .Throw<DivideByZeroException>().WithMessage("Attempted to divide by zero.");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-methods-that-throw-exceptions-53953867/?t=125)

## 9. Testing for raised events

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-for-raised-events-53953868/) · 1:29

### 总结

本课演示如何使用 Fluent Assertions 验证在测试执行期间是否触发了某个特定事件。
通过使用 Monitor 扩展方法，开发者可以跟踪一个类上的事件活动并对其进行断言，从而简化了这个传统上难以手动实现的过程。

### 核心概念

- 使用 Fluent Assertions 进行事件监控
- 用于跟踪对象活动的 `Monitor()` 扩展方法
- 使用 `Should().Raise()` 断言事件被触发
- 验证事件驱动代码中的副作用

### 课程笔记

在测试依赖事件驱动架构的组件时，断言某个事件被触发是一个常见需求。
在标准 C# 中，很难验证某个事件是否被触发，因为没有内置的机制来跟踪事件调用。

考虑一个带有事件以及一个触发该事件的方法的类：

```csharp
}
    };

    public IEnumerable<int> Numbers = new[] { 1, 5, 10, 15 };

    public event EventHandler ExampleEvent;

    internal int InternalSecretNumber = 42;

    public virtual void RaiseExampleEvent()
    {
        ExampleEvent(this, EventArgs.Empty);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-for-raised-events-53953868/?t=10)

Fluent Assertions 通过提供 `Monitor()` 扩展方法简化了这一点。
在被测系统（SUT）上调用它时，它会创建一个监控对象，观察该类触发的任何事件。

要断言某个事件被触发，请按以下步骤操作：
1. 在被测实例上调用 `Monitor()`。
2. 调用触发该事件的方法。
3. 在监控对象上使用 `Should().Raise()` 断言，并以字符串形式指定事件的名称。

```csharp
.Throw<DivideByZeroException>()
            .WithMessage("Attempted to divide by zero.");
    }

    [Fact]
    public void EventRaisedAssertionExample()
    {
        var monitorSubject = _sut.Monitor();

        _sut.RaiseExampleEvent();

        monitorSubject.Should().Raise("ExampleEvent");
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-for-raised-events-53953868/?t=85)

如果触发事件的逻辑被省略或未能执行，`Should().Raise()` 断言就会让测试失败。
这种方式提供了一种简洁、易读的方法来验证基于事件的副作用，而无需手动订阅事件并管理标志位。

## 10. Testing private methods (members)

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-private-methods-members-53953869/) · 1:24

不应仅仅为了单元测试而把私有方法改为公共的。
相反，应该通过调用它们的公共成员或 internal 成员来间接测试它们。
这种方式既保持了封装，又能确保私有方法中的逻辑通过类预期的公共接口得到验证。

### 核心概念

*   **封装**：私有方法是实现细节，应该对公共 API 保持隐藏。
*   **间接测试**：私有逻辑通过测试依赖该逻辑的公共方法来验证。
*   **访问修饰符**：避免仅仅为了满足测试需求而把 `private` 改为 `public` 或 `internal`。

### 课程笔记

单元测试中的一个常见难题是如何验证私有方法中包含的逻辑。
例如，考虑一个 `Calculator` 类，其中公共的 `Divide` 方法依赖一个私有静态方法来验证输入。

```csharp
public float Divide(float a, float b)
    {
        EnsureThatDivisorIsNotZero(b);

        return a / b;
    }

    private static void EnsureThatDivisorIsNotZero(float b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-private-methods-members-53953869/?t=15)

由于 `EnsureThatDivisorIsNotZero` 是私有的，无法从测试类中直接访问它。
尝试通过实例或类类型调用它，会导致一个编译错误，提示该方法不可访问。

保持这些访问修饰符是一种最佳实践。
你不应该仅仅为了让方法可测试，就把它从 `private` 改为 `public`。
私有方法是实现细节；它们通过被类的公共方法或 internal 方法调用而得到测试。

要测试私有方法中的逻辑，你需要为使用它的公共成员编写测试。
在下面的示例中，`ExceptionThrownAssertionExample` 测试通过向公共的 `Divide` 方法传入一个为零的除数，来验证私有验证逻辑的行为。

```csharp
    [Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        Action result = () => calculator.Divide(1, 0);

        result.Should()
            .Throw<DivideByZeroException>()
            .WithMessage("Attempted to divide by zero.");
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-private-methods-members-53953869/?t=55)

调试这个测试时，执行流程会进入 `Divide` 方法，随后再步入 `EnsureThatDivisorIsNotZero` 方法。
通过断言最终结果（例如抛出了某个特定异常），私有方法的逻辑就被有效地覆盖并验证了。

```text
ExceptionThrownAssertionExample        (test)
  |
  +--> calculator.Divide(1, 0)         (public)
         |
         +--> EnsureThatDivisorIsNotZero(b)   (private static)
                |
                +--> throw new DivideByZeroException()
                       |
                       v
         result.Should().Throw<DivideByZeroException>()
```

## 11. Testing internal methods (members)

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/) · 3:01

在保持封装的同时确保代码质量时，测试 internal 成员是一个常见需求。
本课演示如何使用 InternalsVisibleTo 特性（通过程序集级特性或直接在 .csproj 文件中），让测试程序集能够访问 internal 字段和方法，而不会把它们暴露给外部使用者。

### 核心概念

- **Internal 访问修饰符**：把可见性限制在当前程序集内，防止实现细节泄漏到外部 API。
- **封装与可测试性**：需要测试那些不应对最终用户公开的逻辑。
- **InternalsVisibleToAttribute**：一种向特定外部程序集授予 internal 成员访问权限的机制。
- **MSBuild 配置**：在项目文件中管理 internal 可见性的一种现代方式，可替代程序集特性。

### 课程笔记

在企业应用开发中，`internal` 关键字经常被用来确保一个库或项目不会把内部组件泄漏给外部使用者。
internal 成员可以被其所在程序集内的任何代码访问，但对外部项目保持隐藏。

考虑一个包含 internal 字段的类：

```csharp
public IEnumerable<int> Numbers = new[] { 1, 5, 10, 15 };

        public event EventHandler ExampleEvent;

        internal int InternalSecretNumber = 42;

        public virtual void RaiseExampleEvent()
        {
            ExampleEvent(this, EventArgs.Empty);
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/?t=25)

当从一个单独的测试程序集对这个类进行单元测试时，internal 成员 `InternalSecretNumber` 将无法访问，如果你尝试对它进行断言，编译器会报错。

```csharp
public void EventRaisedAssertionExample()
        {
            var monitorSubject = _sut.Monitor();

            _sut.RaiseExampleEvent();

            monitorSubject.Should().Raise("ExampleEvent");
        }

        [Fact]
        public void TestingInternalMembersExample()
        {
            var number = _sut.InternalSecretNumber;

            number.Should().Be(42)
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/?t=70)

#### 通过程序集特性暴露 internal 成员

解决这个问题的传统方法是使用 `InternalsVisibleTo` 特性。
你可以在拥有这些 internal 成员的项目中创建一个文件（名称随意），并在程序集级别定义该特性。
这个特性需要传入你希望授予访问权限的程序集名称。

```csharp
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingTechniques.Tests.Unit")]
[assembly: InternalsVisibleTo("TestingTechniques.Tests.Integration")]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/?t=130)

#### 通过项目文件暴露 internal 成员（推荐）

一种更现代、更简洁的方式是直接在包含 internal 成员的项目的 `.csproj` 文件中配置可见性。
通过添加一个带有 `InternalsVisibleTo` 元素的 `ItemGroup`，你就无需为程序集特性额外创建 C# 文件。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/?t=145)

在 `ItemGroup` 中，加入目标测试项目的名称：`<InternalsVisibleTo Include="TestingTechniques.Tests.Unit" />`。
保存之后，测试项目就能访问这些 internal 成员，测试也就能成功编译并运行。

```csharp
public void EventRaisedAssertionExample()
    {
        var monitorSubject = _sut.Monitor();

        _sut.RaiseExampleEvent();

        monitorSubject.Should().Raise("ExampleEvent");
    }

    [Fact]
    public void TestingInternalMembersExample()
    {
        var number = _sut.InternalSecretNumber;

        number.Should().Be(42);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-internal-methods-members-53953870/?t=160)

测试 internal 代码是完全合理的；应该把它视为仅限所有者及其测试套件使用、而不对一般使用者开放的公共代码。

## 12. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953871/) · 0:26

### 总结

本课全面回顾了在 ASP.NET Core 中使用 Fluent Assertions 的进阶测试技巧。
它涵盖了使用 fluent 语法对字符串、数值和复杂对象进行断言的实际应用，同时也涉及异常处理、事件验证以及访问 private 或 internal 成员等特殊测试场景。

### 核心概念

- **Fluent Assertions**：与标准 xUnit 断言相比，使用更易读、更具表达力的语法编写测试断言。
- **特定类型的断言**：用于验证字符串、数字和可枚举对象的方法。
- **异常测试**：断言执行期间抛出了特定异常的技巧。
- **事件监控**：验证被测系统触发了预期的事件。
- **访问非公共成员**：测试封装在 private 或 internal 方法中的逻辑的策略。

### 课程笔记

本章节聚焦于 Fluent Assertions 的使用及其好处。
这个库让开发者编写的断言读起来更像自然语言，从而提升了测试套件的可维护性和清晰度。

例如，在测试字符串输出时，Fluent Assertions 提供了可链式调用的 API，可以在一个逻辑块中验证字符串的多个属性：

```csharp
using System;
using FluentAssertions;
using Xunit;

namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");
        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Nick");
        fullName.Should().EndWith("Chapsas");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953871/?t=10)

除了简单的字符串之外，本章节还探讨了如何对数值以及复杂对象或集合进行断言。
fluent 语法在这些类型之间保持一致，例如为数字提供 `.Should().BeGreaterThan()`，为集合提供 `.Should().Contain()` 等方法。

课程还涉及了更复杂的测试需求：
- **异常**：使用 `.Should().Throw<TException>()` 确保代码在特定条件下能够优雅且正确地失败。
- **事件**：监控对象在其生命周期中是否触发了预期的事件。
- **可见性**：在必要时测试 `internal` 成员（通常借助 `InternalsVisibleTo`）和 `private` 成员的技巧，确保即使是被封装的逻辑也能得到验证。

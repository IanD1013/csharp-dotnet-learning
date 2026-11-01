# Unit testing techniques

> 课程:[From Zero to Hero: Unit testing for C# Developers](https://dometrain.com/course/from-zero-to-hero-unit-testing-for-csharp-developers/) · 第 4 章
> 共 11 课 · 约 25:41
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Writing fluent assertions](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-fluent-assertions-53927091/) | 3:19 | [↓](#1-writing-fluent-assertions) |
| 2 | [Testing strings](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-strings-53927092/) | 2:58 | [↓](#2-testing-strings) |
| 3 | [Testing numbers](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/) | 2:00 | [↓](#3-testing-numbers) |
| 4 | [Testing dates](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-dates-53927094/) | 1:37 | [↓](#4-testing-dates) |
| 5 | [Testing objects](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-objects-53927095/) | 3:32 | [↓](#5-testing-objects) |
| 6 | [Testing enumerables](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/) | 3:32 | [↓](#6-testing-enumerables) |
| 7 | [Testing methods that throw Exceptions](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/) | 2:23 | [↓](#7-testing-methods-that-throw-exceptions) |
| 8 | [Testing for raised events](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-for-raised-events-53927098/) | 1:29 | [↓](#8-testing-for-raised-events) |
| 9 | [Testing private methods (members)](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-private-methods-members-53927100/) | 1:24 | [↓](#9-testing-private-methods-members) |
| 10 | [Testing internal methods  (members)](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/) | 3:01 | [↓](#10-testing-internal-methods--members) |
| 11 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927102/) | 0:26 | [↓](#11-section-recap) |

## 1. Writing fluent assertions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-fluent-assertions-53927091/) · 3:19

### 总结

Fluent Assertions 是一个流行的 .NET 库,它用一种更具表现力、更易读的语法取代标准的 xUnit 断言。
通过使用 .Should() 这样的扩展方法,开发者可以写出读起来像自然英语句子的测试,这提升了可维护性,并在断言失败时提供更详细的错误信息。

### 核心概念

- **断言(Assertions)**:验证代码输出是否符合预期的机制;失败时会抛出异常。
- **Fluent Assertions 库**:一个为断言提供领域特定语言的 NuGet 包。
- **可读性**:从静态类调用(`Assert.Equal`)过渡到面向对象的扩展方法(`result.Should()`)。
- **错误信息**:与标准 xUnit 断言相比,Fluent Assertions 在失败时提供更具描述性的反馈。

### 课程笔记

断言是单元测试的核心支柱。
它们确保方法的实际返回值与预期值相符。
如果两者不相符,断言就会抛出异常,导致测试失败。
在标准 xUnit 中,这通常由 `Assert` 静态类来处理。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-fluent-assertions-53927091/?t=25)

虽然标准的 `Assert` 类能用,但它可能缺乏优雅性和可读性。
`FluentAssertions` NuGet 包提供了一种更自然的方式来表达这些要求。
安装这个包之后,你就可以在 result 对象上使用 `.Should()` 扩展方法,写出读起来像普通英语的断言。

```csharp
[InlineData(5, 5, 10)]
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-fluent-assertions-53927091/?t=130)

`result.Should().Be(expected)` 这种语法与标准 xUnit 断言的长度大致相同,但明显更清晰。
这种做法能很好地扩展到不同类型的属性和值上,使测试套件更易读、更易维护,同时提供更出色的错误信息,直接指向失败的原因。

```csharp
}

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/writing-fluent-assertions-53927091/?t=190)

## 2. Testing strings

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-strings-53927092/) · 2:58

### 总结

本课介绍在 C# 中使用 Fluent Assertions 库对字符串值进行断言的各种技巧。
它演示了如何进行精确相等检查、验证字符串不为空,以及校验特定的前缀或后缀,同时也提到了正则匹配和长度验证等高级功能的存在。

### 核心概念

- 使用 Fluent Assertions 进行字符串校验。
- 使用 `.Should().Be()` 进行精确相等匹配。
- 使用 `.Should().NotBeEmpty()` 进行非空检查。
- 使用 `.Should().StartWith()` 和 `.Should().EndWith()` 进行子字符串校验。
- 高级断言,包括正则匹配、长度验证和大小写检查。

### 课程笔记

为了演示字符串断言,我们使用一个 `ValueSamples` 类,它包含各种属性,其中有一个 `FullName` 字符串。
这个类充当我们单元测试的数据来源。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-strings-53927092/?t=25)

测试类 `ValueSamplesTests` 遵循标准的命名约定,并把被测系统(System Under Test,SUT)实例化为一个私有字段。
由于 xUnit 会为每个测试方法实例化一次测试类,因此不存在状态在各个测试之间泄漏的风险。

```csharp
namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-strings-53927092/?t=40)

对字符串进行断言时,最常见的需求是验证字符串与预期值完全匹配。
这可以通过 `.Should().Be()` 方法实现。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-strings-53927092/?t=105)

Fluent Assertions 提供了其他几个方法来校验字符串的不同特征。
你可以确保字符串不为空,或者验证它以特定字符开头或结尾。
当你只需要校验字符串的某些部分或其大致格式、而不是其全部内容时,这一点尤其有用。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-strings-53927092/?t=170)

除了这些常见检查之外,该库还支持更细粒度的断言:
- **长度**:`.Should().HaveLength(int)` 确保字符串具有特定数量的字符。
- **正则**:`.Should().Match(string)` 允许使用正则表达式进行模式匹配。
- **大小写**:`.Should().BeLowerCased()` 或 `.Should().BeUpperCased()` 校验字符串的大小写。
- **否定**:`.Should().NotBe(string)` 确保字符串不等于某个特定值。

## 3. Testing numbers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/) · 2:00

Fluent Assertions 为数值类型提供了一组专门的扩展方法,增强了单元测试的可读性和表现力。
这些方法让开发者不仅可以断言相等性,还可以断言正负性、相对比较和范围包含等属性。
断言失败时,该库会生成人类可读的错误信息,清楚地说明预期值与实际值,便于更快地调试。

### 核心概念

- 使用 `.Should().Be()` 进行相等断言。
- 针对数值不匹配的人类可读失败信息。
- 符号断言,例如 `.BePositive()`。
- 比较断言,包括 `.BeGreaterThan()` 和 `.BeLessOrEqualTo()`。
- 使用 `.BeInRange(min, max)` 进行范围校验。

### 课程笔记

数值断言针对数值类型(例如 `int`、`double`)的属性或变量进行。
来看一个包含 `Age` 属性、用于测试的示例类:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/?t=10)

最基础的数值断言是检查相等性。
使用 Fluent Assertions 时,`.Should().Be()` 方法用于验证一个数字是否与预期值相符。

```csharp
var fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");
        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Nick");
        fullName.Should().EndWith("Chapsas");
    }

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(21)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/?t=25)

如果断言失败,Fluent Assertions 会提供一条描述性的错误信息。
例如,如果预期值被设为 22 而实际值是 21,错误信息会写道:"Expected age to be 22, but found 21."

```csharp
fullName.Should().EndWith("Chapsas");
    }

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(22);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/?t=35)

Fluent Assertions 能感知上下文,并专门为数值类型提供了额外的方法。
其中包括检查一个数字是否为正数,或使用大于逻辑把它与其他值进行比较。

```csharp
[Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(21);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20)
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/?t=70)

你还可以断言一个值落在特定的闭区间内,或满足特定的边界条件,例如小于或等于某个值。

```csharp
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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-numbers-53927093/?t=95)

这些方法可以组合使用,以高度可读的形式对数值状态进行全面校验,确保代码在各种数值约束下都按预期运行。

## 4. Testing dates

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-dates-53927094/) · 1:37

### 总结

本课演示如何在 C# 中使用 Fluent Assertions 对日期类型进行断言。
它涵盖了对 DateOnly 对象的基本相等检查、范围校验,以及"大于"这类比较断言,突出了该库所提供的人类可读语法。

### 核心概念

- 针对 DateOnly 类型的相等断言。
- 使用 BeInRange 进行基于范围的断言。
- 比较断言,例如 BeGreaterThan 和 BeGreaterOrEqualTo。
- 解读基于日期的断言的失败信息。

### 课程笔记

在测试涉及日期的属性(例如 DateOfBirth)时,Fluent Assertions 为各种类型的比较提供了高度可读的语法。
在下面的示例中,被测系统(SUT)包含一个 DateOnly 字段。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-dates-53927094/?t=25)

最基础的断言是检查相等性。
通过使用 `.Should().Be()`,你可以把该属性与一个特定的 DateOnly 实例进行比较。
如果断言失败,Fluent Assertions 会提供一条描述性的错误信息,同时指出预期值和实际值(例如 "expected date of birth to be equal to [date], but found [date] instead")。

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

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-dates-53927094/?t=40)

除了简单的相等之外,你还可以使用 `.Should().BeInRange()` 断言一个日期落在特定范围内。
这对于验证一个日期位于两个边界之间很有用,例如检查出生日期是否落在 2000 年初与 2001 年初之间。

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

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-dates-53927094/?t=70)

此外,还可以使用 `.Should().BeGreaterThan()` 或 `.Should().BeGreaterOrEqualTo()` 这类比较断言来确保某个日期发生在特定时间点之后,同时保持测试结构人类可读且简单。

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

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-dates-53927094/?t=90)

## 5. Testing objects

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-objects-53927095/) · 3:32

### 总结

本课探讨在 C# 单元测试中对复杂对象断言相等性的复杂之处。
整数和日期这样的值类型按其实际值进行比较,而类是按内存地址进行比较的引用类型。
这常常导致在比较两个属性值完全相同的不同实例时测试失败。
本课演示了 Fluent Assertions 如何通过 `BeEquivalentTo` 方法简化这一过程,该方法会动态地逐个属性进行比较,从而无论对象引用或具体类型如何,都能确保结构相等。

### 核心概念

* **引用相等与值相等**:C# 中类的标准相等性检查的是两个变量是否指向同一内存位置,而值类型比较的是数据本身。
* **字符串的特殊情况**:虽然字符串是引用类型,但与自定义类不同,它们在 C# 中表现出基于值的相等行为。
* **断言失败**:`Assert.Equal` 和 Fluent Assertions 的 `.Should().Be()` 对对象默认都使用引用相等,因此即使所有属性都匹配,测试也会失败。
* **结构相等**:比较对象内所有属性的值,而不是比较对象的身份。
* **BeEquivalentTo**:一个 Fluent Assertions 方法,在运行时动态地检查并匹配属性。
* **类型无关性**:只要两个对象拥有相同的属性名和值,`BeEquivalentTo` 就可以比较不同类型的对象。

### 课程笔记

在测试复杂对象时,例如一个包含多个属性的 `User` 类,标准的相等断言常常会表现出意料之外的行为。
设想这样一个场景:被测系统(SUT)提供了一个 `AppUser` 对象。

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
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-objects-53927095/?t=10)

如果你尝试用 `Assert.Equal` 把这个 `AppUser` 与一个值完全相同的 `expected` 对象进行比较,测试将会失败。

```csharp
public void ObjectAssertionExample()
{
    var expected = new User
    {
        FullName = "Nick Chapsas",
        Age = 21,
        DateOfBirth = new(2000, 6, 9)
    };

    var user = _sut.AppUser;

    Assert.Equal(expected, user);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-objects-53927095/?t=60)

这种失败的原因在于 `User` 是一个类(引用类型)。
在 C# 中,引用类型被分配在内存中的不同空间。
`Assert.Equal` 或 Fluent Assertions 的 `.Should().Be()` 方法这类标准相等方法检查的是两个对象是否是内存中完全相同的实例(引用相等),而不是它们的内容是否相同。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-objects-53927095/?t=100)

```text
  expected ---> [ User: "Nick Chapsas", 21, new(2000, 6, 9) ]
  user     ---> [ User: "Nick Chapsas", 21, new(2000, 6, 9) ]

  Assert.Equal / .Should().Be()  -> same instance in memory?
  .Should().BeEquivalentTo()     -> same property values?
```

为了在不手动逐一断言每个属性(例如 `user.Age.Should().Be(expected.Age)`)的情况下解决这个问题,Fluent Assertions 提供了 `BeEquivalentTo` 方法。
该方法通过在执行时动态检查每个属性来进行深度比较。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-objects-53927095/?t=175)

`BeEquivalentTo` 非常灵活;只要对象拥有相同的属性名和值,即使它们不是完全相同的类类型,也能匹配成功。
这使它成为一个强大的工具,可以断言一个对象具有正确的"外观和感觉",而无需承担手动映射的开销。

## 6. Testing enumerables

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/) · 3:32

### 总结

本课演示如何在 C# 中使用 Fluent Assertions 对集合和可枚举对象进行断言。
它涵盖了测试引用类型与值类型之间的细微差别,包括基于属性的等价性、集合大小验证,以及使用 lambda 表达式进行复杂匹配。
此外,它还谈到了性能方面的考量,例如通过转换为数组来避免多次枚举。

### 核心概念

- 区分集合中引用类型与值类型的断言。
- 使用 `ContainEquivalentOf` 对对象进行基于属性的匹配。
- 通过转换为数组来防止 `IEnumerable` 被多次枚举。
- 使用 `HaveCount` 验证集合大小。
- 在 `Contain` 中使用 lambda 表达式实现灵活的元素匹配。

### 课程笔记

在测试对象集合时,被测系统(SUT)常常以 `IEnumerable` 的形式暴露数据。
在这个示例中,SUT 包含一个 `User` 对象数组。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=10)

要对这些对象进行断言,你首先要定义预期的对象实例。

```csharp
user.Should().BeEquivalentTo(expected);
    }

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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=25)

在使用 `IEnumerable` 时,最佳实践是使用 `.As<T[]>()` 把集合转换为数组或列表,以防止在断言阶段出现多次枚举的问题。

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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=40)

对于引用类型,标准的 `.Contain()` 检查会失败,因为它执行的是引用相等检查。
相反,应使用 `.ContainEquivalentOf()` 进行逐个属性的比较,以在集合中找到匹配的对象。

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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=85)

你还可以使用 `.HaveCount()` 方法断言集合的大小。

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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=110)

若要进行不依赖于某个特定对象实例的更灵活的断言,可以在 `.Contain()` 方法中使用 lambda 表达式,根据特定的属性条件来匹配元素。

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

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=130)

在处理值类型(例如 `int`)时,标准的 `.Contain()` 方法可以直接使用,因为相等性基于值本身,而不是内存引用。

```csharp
[Fact]
    public void EnumerableNumbersAssertionExample()
    {
        var numbers = _sut.Numbers.As<int[]>();
        
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=175)

```csharp
[Fact]
    public void EnumerableNumbersAssertionExample()
    {
        var numbers = _sut.Numbers.As<int[]>();

        numbers.Should().Contain(5);
    }

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-enumerables-53927096/?t=190)

## 7. Testing methods that throw Exceptions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/) · 2:23

### 总结

本课介绍在 C# 中使用 Fluent Assertions 断言某个方法会抛出异常的技巧。
它解释了为什么必须把代码包装在 Action 或 Func 委托中才能在 Act 阶段捕获异常,并演示了如何同时校验异常类型和具体的错误信息。

### 核心概念

- **预期异常**:验证一个方法在特定条件下(例如除以零)会正确地失败。
- **委托包装**:使用 `Action` 或 `Func` 来延迟执行,使测试运行器能够捕获异常并对其进行断言。
- **Fluent Assertions**:利用 `.Should().Throw<T>()` 语法进行可读性强的异常测试。
- **消息校验**:链式调用 `.WithMessage()`,确保异常包含正确的描述性文本。

### 课程笔记

在测试预期会抛出异常的方法时,标准的测试结构会过早地失败。
如果你在"Act"阶段直接调用该方法,异常会立即被抛出,测试执行会在到达"Assert"阶段之前就停止。

```csharp
[Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        var result = calculator.Divide(1, 0);

        result.Should().Be(0);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/?t=60)

要解决这个问题,你必须把这次调用包装在一个委托中。
这让断言库能够在 try-catch 块中执行这段代码。
对于不需要捕获返回值的方法,使用 `Action`。

```csharp
[Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        Action result = () => calculator.Divide(1, 0);

        result.Should().Be(0);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/?t=75)

如果该方法返回一个你原本可能关心的值,你可以使用 `Func<T>` 委托。

```csharp
[Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        Func<int> result = () => calculator.Divide(1, 0);

        result.Should().Be(0);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/?t=85)

一旦调用被包装在委托中,你就可以使用 Fluent Assertions 来验证抛出的是特定的异常类型。
例如,除以零应当抛出 `DivideByZeroException`。

```csharp
[Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        Action result = () => calculator.Divide(1, 0);

        result.Should().Throw<DivideByZeroException>();
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/?t=100)

你可以通过校验异常消息来进一步细化断言。
这是通过在断言上链式调用 `.WithMessage()` 方法来实现的。
这能确保失败的逻辑正是你所预期的那一处。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-methods-that-throw-exceptions-53927097/?t=125)

## 8. Testing for raised events

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-for-raised-events-53927098/) · 1:29

测试某个事件是否被引发是 C# 开发中的常见需求,在处理事件驱动架构时尤其如此。
传统上,这需要手动订阅事件并设置布尔标志,既繁琐又容易出错。
Fluent Assertions 通过提供 `Monitor` 扩展方法解决了这个问题,该方法会创建一个 monitor subject 来跟踪某个类上的事件。
这让开发者可以先执行操作,再使用 `Should().Raise()` 断言来验证某个特定事件已被触发,从而大大简化了基于事件的单元测试。

### 核心概念

- 使用 Fluent Assertions 进行事件监视。
- `Monitor()` 扩展方法。
- 使用 `.Should().Raise()` 断言事件被触发。
- 基于字符串的事件名称识别。
- 通过避免手动订阅来简化事件驱动的单元测试。

### 课程笔记

断言某个事件已被引发是单元测试中的一个常见难题,因为事件通常不会以一种容易观察到的方式修改对象的状态。
Fluent Assertions 通过对象监视提供了一种专门的机制来处理这种场景。

来看一个暴露了一个事件以及一个触发该事件的方法的类:

```csharp
public IEnumerable<int> Numbers = new[] { 1, 5, 10, 15 };

public event EventHandler ExampleEvent;

internal int InternalSecretNumber = 42;

public virtual void RaiseExampleEvent()
{
    ExampleEvent(this, EventArgs.Empty);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-for-raised-events-53927098/?t=10)

为了测试这一点,Fluent Assertions 引入了 `Monitor()` 扩展方法。
在被测系统(SUT)上调用它时,它会创建一个 `monitorSubject`。
这个 subject 充当对象上定义的所有事件的监听者。

```csharp
[Fact]
public void EventRaisedAssertionExample()
{
    var monitorSubject = _sut.Monitor();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-for-raised-events-53927098/?t=40)

在设置好监视器并调用应当触发事件的方法之后,你就可以直接在 `monitorSubject` 上执行断言。
`Should().Raise()` 方法接受事件名称作为字符串参数,以验证该事件已被触发。

```csharp
[Fact]
public void EventRaisedAssertionExample()
{
    var monitorSubject = _sut.Monitor();

    _sut.RaiseExampleEvent();

    monitorSubject.Should().Raise("ExampleEvent");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-for-raised-events-53927098/?t=85)

如果在方法执行期间该事件没有被引发,断言就会失败,并给出一条清晰的错误信息。
这种做法比手动订阅事件并使用局部变量来跟踪处理程序是否被执行要简洁得多。

## 9. Testing private methods (members)

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-private-methods-members-53927100/) · 1:24

### 总结

测试私有方法是开发者经常关心的问题,但首要原则是保持封装。
私有方法不应该仅仅为了方便测试而被改为 public 或 internal。
相反,它们是通过调用它们的 public 或 internal 成员来间接测试的。
这种做法确保了私有辅助方法中的逻辑作为类整体行为的一部分得到验证。

### 核心概念

- **封装**:保持原本设定的访问修饰符;不要为了测试而暴露私有逻辑。
- **间接测试**:私有方法是公共方法的实现细节,应当通过这些公共接口来测试。
- **测试导致的损害(Test-Induced Damage)**:避免仅仅为了让代码更容易测试而改变代码的设计(例如把 `private` 改为 `public`)。
- **通过执行来验证**:如果一个公共方法依赖于某个私有方法,对该公共方法的全面测试自然会覆盖到私有代码路径。

### 课程笔记

单元测试中的一个常见问题是如何处理私有方法。
来看一个 `Calculator` 类,它包含一个公共的 `Divide` 方法和一个用于校验的私有辅助方法。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-private-methods-members-53927100/?t=15)

尝试从测试类中直接调用 `EnsureThatDivisorIsNotZero` 会导致编译错误,因为该方法不可访问。
仅仅为了能够测试就把这个方法的修饰符改为 `public` 是一个错误。
这违反了封装,并暴露了本应保持隐藏的内部逻辑。

相反,私有方法是通过调用依赖它的公共方法来测试的。
例如,要测试 `Calculator` 类中的校验逻辑,你可以为 `Divide` 方法编写一个测试,触发由私有辅助方法处理的那个特定场景。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-private-methods-members-53927100/?t=55)

当这个测试执行时,运行时会进入 `Divide` 方法,随后进入 `EnsureThatDivisorIsNotZero` 方法。
测试公共成员可以确保私有成员也被覆盖到。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-private-methods-members-53927100/?t=65)

```text
  test: calculator.Divide(1, 0)
          |
          v
  public float Divide(float a, float b)
          |
          v
  private static void EnsureThatDivisorIsNotZero(float b)
          |
          v
  throw new DivideByZeroException()
```

通过断言公共方法的行为(例如验证会抛出 `DivideByZeroException`),你实际上就在测试私有方法。
始终专注于测试类的公共接口面。

## 10. Testing internal methods  (members)

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/) · 3:01

当你想验证不应暴露给库的外部使用者的逻辑时,测试 internal 成员是一个常见需求。
`internal` 访问修饰符确保一个成员只能在其所在的程序集内访问,这是企业级软件开发中封装的一个关键原则。

### 核心概念

- **Internal 访问修饰符**:把可见性限制在当前程序集内,防止实现细节泄漏到公共 API 中。
- **封装与可测试性**:既需要为了 API 设计而让成员保持 private/internal,又需要让测试项目能够访问它们以进行验证。
- **InternalsVisibleTo 特性**:一种向特定外部程序集授予 internal 成员访问权限的机制。
- **程序集级配置**:使用一个代码文件,通过 `[assembly: InternalsVisibleTo]` 特性来定义可见性。
- **项目文件配置**:直接在 `.csproj` 文件中定义 internal 可见性的现代做法。

### 课程笔记

当一个成员被标记为 `internal` 时,它对其程序集之外的任何代码都是隐藏的。
例如,一个类可能包含一个用于内部状态的 internal 字段,它不应成为公共接口的一部分。

```csharp
public IEnumerable<int> Numbers = new[] { 1, 5, 10, 15 };

        public event EventHandler ExampleEvent;

        internal int InternalSecretNumber = 42;

        public virtual void RaiseExampleEvent()
        {
            ExampleEvent(this, EventArgs.Empty);
        }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/?t=25)

如果测试项目尝试访问这个 internal 成员,编译器会报错,因为该成员对测试程序集不可见。

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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/?t=70)

#### 程序集特性方式

解决这个问题的一种方式是在源项目中创建一个类文件(例如 `InternalsExposedToTests.cs`)。
通过使用 `[assembly: InternalsVisibleTo]` 特性,你可以指定应当被允许看到 internal 成员的测试程序集的名称。
通过重复该特性,可以把多个程序集作为目标。

```csharp
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestingTechniques.Tests.Unit")]
[assembly: InternalsVisibleTo("TestingTechniques.Tests.Integration")]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/?t=115)

#### 项目文件方式(推荐)

现代且更受推荐的方法是直接在包含 internal 成员的那个项目的 `.csproj` 文件中配置这种可见性。
这避免了创建额外的样板代码文件。
你需要添加一个 `<ItemGroup>`,其中包含一个 `<InternalsVisibleTo>` 元素,并把它的 `Include` 特性设为测试项目的程序集名称。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <InternalsVisibleTo Include="TestingTechniques.Tests.Unit" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/?t=145)

一旦 internal 成员对测试程序集公开,就可以像公共成员一样对它们进行测试。
这使得我们可以在不损害库的公共 API 的前提下,对内部逻辑进行全面测试。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/testing-internal-methods-members-53927101/?t=160)

## 11. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927102/) · 0:26

### 总结

本课全面回顾了 C# 中的进阶单元测试技巧,着重介绍如何使用 Fluent Assertions 编写更易读、更具表现力的测试。
它回顾了如何针对各种数据类型进行断言、如何处理异常和事件,以及如何着手测试非公共成员。

### 核心概念

* 使用 Fluent Assertions 增强可读性。
* 针对字符串、数值类型和对象进行断言。
* 异常与事件测试。
* 测试 internal 和 private 方法。

### 课程笔记

本节介绍了 Fluent Assertions 的使用方式及其好处,与标准 xUnit 断言相比,它为单元测试断言提供了更接近自然语言的语法。
这种做法提升了测试套件的可维护性和清晰度。

断言在多种类型上进行了演示,包括字符串、数字和复杂对象。
例如,在测试字符串输出时,Fluent Assertions 允许链式组合多个条件,例如检查特定内容、前缀或后缀。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-unit-testing-for-csharp-developers-2722824/section-recap-53927102/?t=10)

除了简单的值断言之外,本节还详细介绍了验证在特定条件下会抛出特定异常、以及事件按预期被引发的技巧。
最后,本课探讨了测试 internal 和 private 方法的实际做法,确保类的内部逻辑在必要时能够得到验证。

接下来的几节将转向涉及外部依赖的单元测试概念,以及 mocking 框架的使用。

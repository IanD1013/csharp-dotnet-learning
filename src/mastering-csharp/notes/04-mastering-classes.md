# Mastering Classes

> 课程：[Mastering: C#](https://dometrain.com/course/mastering-csharp/) · 第 4 章
> 共 9 课 · 约 10:42
> 来源：Dometrain。由课程文档翻译整理；每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/) | 0:30 | [↓](#1-overview) |
| 2 | [Object Initialization](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/) | 0:24 | [↓](#2-object-initialization) |
| 3 | [Analyzing Object Construction Order](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/) | 1:34 | [↓](#3-analyzing-object-construction-order) |
| 4 | [Virtual Calls in the Base class Constructor](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/) | 1:53 | [↓](#4-virtual-calls-in-the-base-class-constructor) |
| 5 | [Using Analyzers to Avoid Making Virtual Calls in Base Constructors](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/) | 0:36 | [↓](#5-using-analyzers-to-avoid-making-virtual-calls-in-base-constructors) |
| 6 | [A Custom Factory Method and Error Handling](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/) | 0:28 | [↓](#6-a-custom-factory-method-and-error-handling) |
| 7 | [Exploring a Custom Factory Method Behavior at Runtime](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/) | 1:45 | [↓](#7-exploring-a-custom-factory-method-behavior-at-runtime) |
| 8 | [Benchmarking the new() Constraint](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/) | 1:46 | [↓](#8-benchmarking-the-new-constraint) |
| 9 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/) | 1:46 | [↓](#9-conclusion) |

---

## 1. Overview

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/) · 0:30

本课介绍 C# 类管理中的进阶主题，重点关注对象构造的细微之处。
它探讨继承层次结构中字段初始化器与构造函数的具体执行顺序、在对象创建期间进行虚方法分派所带来的风险，以及 `new()` 泛型约束的工作机制。
此外，它还会考察对象实例化的性能问题，尤其是在面向较旧框架时。

### 核心概念

- 继承层次结构中的对象初始化顺序。
- 在部分构造的对象上进行虚分派的危险。
- `new()` 泛型约束及其编译行为。
- 直接构造与泛型约束在异常处理上的差异。
- 使用自定义激活器（activator）和表达式树进行性能优化。

### 课程笔记

#### Object Initialization Order

理解对象构造期间精确的执行顺序，对于在复杂层次结构中避免 bug 至关重要。
在 C# 中，当实例化一个派生类时，执行遵循特定的顺序：先运行派生类的字段初始化器，接着是基类的字段初始化器，然后是基类构造函数体，最后是派生类构造函数体。
这一顺序确保了字段在构造函数体执行之前完成初始化，但也意味着基类构造函数会在派生类构造函数体有机会执行其逻辑之前运行。

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init(
        [CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=9)

#### Virtual Calls in Constructors

在构造函数中调用虚成员是 C# 中一种危险的模式。
由于初始化顺序的原因，基类构造函数中的虚调用会被分派到派生类的重写方法上。
然而，由于基类构造函数在派生类构造函数体之前执行，虚方法的派生类实现可能会尝试访问尚未完全初始化的字段或状态，从而导致不可预测的行为或 `NullReferenceException`。

```csharp
using System.Runtime.CompilerServices;

Base b = new Derived();
b.ReportState();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    public virtual void ReportState()
    {
        Console.WriteLine($"Base.ReportState, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();
    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }

    public override void ReportState()
    {
        Console.WriteLine($"Derived.ReportState, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=15)

#### The new() Constraint and Generic Instantiation

`where T : new()` 约束使泛型类型可以被实例化。
历史上，在 .NET Framework 中，这一约束是通过 `Activator.CreateInstance` 实现的，这会带来性能损失，并把构造函数抛出的任何异常包装在 `TargetInvocationException` 中。
在现代 .NET 中，JIT 编译器通常可以把它优化为直接的构造函数调用，从而显著提升性能并简化异常处理。

```csharp
using System.Linq.Expressions;

try
{
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass() 
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}

static class Factory
{
    public static T Create<T>() where T : new() => new();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=19)

#### Custom Activators for Performance

在面向较旧框架或需要专门的实例化逻辑时，可以使用基于编译表达式树的自定义激活器。
通过缓存一个编译好的 `Func<T>`，开发者可以获得接近直接调用构造函数的性能，绕过 `Activator.CreateInstance` 的开销，同时保持干净的异常传播。

```csharp
public static class CachedFactory<T> where T : new()
{
    public static readonly Func<T> Create =
        Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
}

public static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => CachedFactory<T>.Create();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958846/?t=24)

---

## 2. Object Initialization

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/) · 0:24

### 总结

本课考察 C# 继承层次结构中字段初始化器和构造函数的执行顺序。
通过观察一个都使用类字段式初始化（field-like initialization）的基类和派生类，本课阐明了运行时准备对象的顺序，确保字段在构造函数逻辑执行之前已正确设置。

### 核心概念

- **类字段式初始化**：在字段的声明处而不是在构造函数内部初始化字段。
- **初始化顺序**：继承中字段与构造函数执行的特定顺序。
- **Base 与 Derived 的执行**：运行时在实例化期间如何遍历类层次结构。
- **追踪执行**：使用 `[CallerMemberName]` 监视初始化的生命周期。

### 课程笔记

在 C# 中，当一个类继承自另一个类时，字段初始化和构造函数执行的顺序是严格定义的。
本课给出一个涉及 `Base` 类和 `Derived` 类的场景来确定这一顺序。

两个类都使用类字段式初始化，即在字段的声明处为其赋值。
为了追踪执行过程，使用了一个静态辅助方法 `Init`，它借助 `CallerMemberName` 特性来识别当前正在处理的是哪个字段。

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init(
        [CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/object-initialization-69958847/?t=10)

当调用 `new Derived()` 时，运行时必须确定整个层次结构中字段初始化器和构造函数的执行顺序。
核心的技术问题是：初始化是从基类到派生类、从派生类到基类，还是两者的某种组合。
这一顺序对于确保字段在链中任何构造函数访问它们之前完成初始化至关重要。
所提供的代码充当一个测试工具（test harness），通过控制台输出来观察这一行为。

---

## 3. Analyzing Object Construction Order

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/) · 1:34

### 总结

本课分析在 C# 中实例化派生类时，字段初始化器和构造函数的精确执行顺序。
它演示了派生类的字段初始化发生在基类的字段初始化和构造函数体之前。
通过考察降级（lowered）后的 C# 代码，本课解释了编译器如何把字段初始化器注入到构造函数体的开头、位于对基类构造函数的调用之前，以确保在执行任何逻辑之前就建立起对象不变式。

### 核心概念

- **执行顺序**：顺序是 (1) Derived 字段初始化器，(2) Base 字段初始化器，(3) Base 构造函数体，以及 (4) Derived 构造函数体。
- **编译器降级**：字段初始化器不只是语法糖；编译器会显式地把这段代码移动到构造函数方法的顶部。
- **基类调用时机**：派生类的字段初始化器在 `base()` 构造函数调用执行之前运行。
- **对象不变式**：提前进行字段初始化可确保字段在整个构造过程中都已被填充，并且可以以有效状态被观察到。

### 课程笔记

本课首先考察一个标准的继承层次结构，其中 `Derived` 类继承自 `Base` 类。
两个类都包含字段初始化器和构造函数，它们会把自己的执行情况记录到控制台。

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/?t=10)

虽然直觉上可能会认为基类先被初始化，但实际的执行顺序并非如此。
创建 `Derived` 类的实例时，顺序是：
1. Derived 字段初始化。
2. Base 字段初始化。
3. Base 构造函数体执行。
4. Derived 构造函数体执行。

查看"降级"后的 C# 代码（即编译器生成的代码）时，就能看到这一行为。
编译器把字段初始化器取出，注入到构造函数体的最顶部，甚至在对基类构造函数的调用（`base..ctor()`）之前。

```csharp
internal class Derived : Base
{
    [Nullable(1)]
    private readonly string _derivedField;

    public Derived()
    {
        // Field initializer is injected before the base constructor call
        this._derivedField = Base.Init("_derivedField");
        base..ctor();
        Console.WriteLine();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/analyzing-object-construction-order-69958848/?t=40)

无论字段是否被标记为 `readonly`，这一顺序都保持不变。
这种提前初始化的主要目的是建立对象不变式。
通过在任何构造函数逻辑运行之前初始化字段，C# 确保了对象一开始其生命周期，字段就处于有效、可观察的状态。
在考虑更复杂的场景时，例如在构造函数中调用虚方法，这一点就变得尤为重要。

---

## 4. Virtual Calls in the Base class Constructor

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/) · 1:53

### 总结

由于对象初始化的特定顺序，在 C# 中从基类构造函数调用虚方法是一种危险的做法。
虽然派生类中的字段初始化器在基类构造函数运行之前执行，但派生类的构造函数体只有在基类构造函数完成之后才会执行。
如果基类构造函数调用了一个在派生类中被重写的虚方法，那么该重写方法会在派生类构造函数体运行之前执行。
如果被重写的方法依赖于在派生类构造函数体内初始化的状态，应用很可能会遇到 NullReferenceException，或者在一个部分构造的实例上进行操作。

### 核心概念

- **初始化顺序**：字段初始化器从最派生类向基类运行，随后构造函数体从基类向最派生类运行。
- **构造函数中的虚分派**：即使最派生类型的构造函数尚未执行，C# 也会把虚调用分派到该类型的实现。
- **部分构造的对象**：基类已完全初始化、但派生类的构造函数逻辑尚未运行的状态。
- **字段初始化与构造函数初始化的脆弱性**：使用字段初始化器时看似安全的代码，如果被重构为在构造函数体中初始化，就可能出错。

### 课程笔记

在 C# 中，处理继承时，对象实例化期间的执行顺序至关重要。
创建派生类的新实例时，运行时遵循特定的顺序：先执行派生类的字段初始化器，然后执行基类的字段初始化器，最后从基类开始、向下到派生类依次执行构造函数体。

```text
new Derived()
  |
  v
Derived field initializers ---> Base field initializers
                                          |
                                          v
Derived constructor body   <--- Base constructor body
```

当基类构造函数调用虚方法时，这一顺序会造成重大隐患。
由于 C# 使用虚分派，即使派生类的构造函数体尚未执行，调用也会被路由到派生类中的实现。

#### Scenario 1: Field-Like Initialization

在下面的示例中，派生类使用类字段式初始化。
由于字段初始化器在任何构造函数体之前运行，当基类构造函数调用 `ReportState()` 时，`_derivedField` 已经被填充。

```csharp
class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
        ReportState();
    }

    public virtual void ReportState()
    {
        Console.WriteLine($"Base.ReportState, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();
    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }

    public override void ReportState()
    {
        Console.WriteLine($"Derived.ReportState, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/?t=10)

在这种情况下，代码运行时不会出错，因为 `_derivedField` 是在字段初始化阶段初始化的，而该阶段发生在进入 `Base` 构造函数体之前。
输出显示派生字段的长度为 25，因为字符串 "initialized:_derivedField" 的长度是 25 个字符。

#### Scenario 2: Constructor-Body Initialization

如果把初始化逻辑从字段级别移到构造函数体中，危险就显现出来了。
这是一种常见的重构，但它从根本上改变了执行顺序。

```csharp
class Derived : Base
{
    private readonly string _derivedField;
    public Derived()
    {
        _derivedField = Init();
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }

    public override void ReportState()
    {
        Console.WriteLine($"Derived.ReportState, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/virtual-calls-in-the-base-class-constructor-69958849/?t=40)

运行这一版本的代码时，会导致 `NullReferenceException`。
执行流程遵循以下步骤：
1. 调用 `Derived` 构造函数。
2. 执行 `Derived` 类的类字段式初始化器（本例中没有）。
3. 调用 `Base` 类的构造函数。
4. 执行 `Base` 的字段初始化器。
5. `Base` 构造函数体开始执行。
6. `Base` 构造函数调用虚方法 `ReportState()`。
7. 虚分派把调用发送到 `Derived.ReportState()`。
8. `Derived.ReportState()` 尝试访问 `_derivedField.Length`。
9. 由于 `Derived` 构造函数体（`_derivedField` 在其中被赋值）尚未运行，`_derivedField` 仍然是 `null`，从而导致崩溃。

#### Conclusion

在构造函数中调用虚方法是极其脆弱的。
它迫使派生类在处于部分构造状态时处理调用，而此时它的不变式可能尚未建立。
为了保持代码的稳定性，应避免在构造函数中进行虚调用。
开发者应借助静态分析工具和 IDE 分析器，在其代码库中检测并防止这种模式。

---

## 5. Using Analyzers to Avoid Making Virtual Calls in Base Constructors

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/) · 0:36

分析器提供了一种自动捕获"构造函数中的虚调用"这一隐患的机制。
启用这些规则最简单的方法是把相关配置添加到 `.editorconfig` 文件中。
一旦启用，分析器就会在 IDE 中以及编译过程中通过警告提供即时反馈。

### 核心概念

- 通过 `.editorconfig` 启用分析器。
- 针对构造函数中虚调用的 IDE 警告和构建时警告。
- 使用"将警告视为错误"（Treat Warnings as Errors）让构建失败，以防止不安全的代码签入。
- 当该行为是有意为之时，带着描述性的意图说明来抑制警告。

### 课程笔记

如果项目被配置为将警告视为错误，那么构造函数中任何一处虚调用都会导致构建无法成功，从而确保这类代码无法被签入源代码管理。
下面的示例演示了触发这些分析器警告的场景，其中 `Base` 构造函数调用了虚方法 `ReportState`：

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

public class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
        ReportState();
    }

    protected virtual void ReportState()
    {
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

public class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived() => Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");

    protected override void ReportState()
    {
        Console.WriteLine($"Derived override, _derivedField.Length = {_derivedField.Length}"  );
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-analyzers-to-avoid-making-virtual-calls-in-base-constructors-69958850/?t=10)

在少数情况下，如果从基类构造函数调用虚方法是有意为之，并且风险已被理解，那么可以抑制该警告。
不过，抑制警告时应始终附上一条描述性的消息，说明开发者的意图。
即使抑制了警告，开发者也必须保持极度谨慎，确保派生类中被重写的方法不会尝试访问尚未由派生类构造函数初始化的状态。

---

## 6. A Custom Factory Method and Error Handling

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/) · 0:28

### 总结

本课探讨使用泛型 `new()` 约束与使用自定义工厂方法进行对象实例化之间的细微差别。
它专门考察通过泛型工厂创建对象时，构造函数内部抛出的异常是如何被处理的，并指出异常可能被包装在 `TargetInvocationException` 中这一潜在陷阱。
本课还介绍了一种使用表达式编译工厂的高性能替代方案，它通过缓存编译好的委托，提供直接的异常传播和更好的执行速度。

### 核心概念

- 泛型 `new()` 约束的行为
- 基于反射的实例化过程中，异常被包装在 `TargetInvocationException` 中
- 在较旧框架上 `new()` 约束的性能开销
- 使用 `Expression.Lambda` 和 `Expression.New` 实现自定义工厂
- 为提高效率，在静态泛型类中缓存编译好的委托

### 课程笔记

在实现泛型工厂方法时，`new()` 约束是确保类型具有无参构造函数的标准方式。
然而，这种做法会对错误处理和性能产生影响。

考虑一个类的构造函数抛出异常的场景：

```csharp
try
{
    // MyClass ctor might fail with InvalidOperationException,
    // so handling that case explicitly.
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass() 
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}

static class Factory
{
    public static T Create<T>() where T : new() => new();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/?t=10)

在上面的示例中，`MyClass` 抛出了 `InvalidOperationException`。
虽然 `try-catch` 块专门捕获这个异常，但 `new()` 约束的行为可能出乎意料。
在某些环境中，特别是当该约束通过反射实现时（例如在 .NET Framework 上），`InvalidOperationException` 可能会被包装在 `TargetInvocationException` 内部。
这会导致专门的 `catch (InvalidOperationException)` 块被绕过，异常转而被通用的 `Exception` 块捕获。

为了提供更可预测的异常传播和更好的性能，可以使用表达式树实现一个自定义工厂：

```csharp
using System.Linq.Expressions;

static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => Cache<T>.Factory();

    private static class Cache<T> where T : new()
    {
        public static readonly Func<T> Factory =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/a-custom-factory-method-and-error-handling-69958851/?t=10)

`CustomActivator` 使用 `Expression.Lambda` 为类型 `T` 编译一个工厂委托。
这种做法有两个主要优势：

1. **异常处理**：构造函数抛出的异常会直接传递到调用方，而不是被包装在 `TargetInvocationException` 中。
2. **性能**：在约束依赖 `Activator.CreateInstance` 的平台上，这种方法比 `new()` 约束快得多。通过把编译好的委托缓存在静态泛型类 `Cache<T>` 中，每个类型只会产生一次编译开销。

```text
CustomActivator.CreateInstance<T>()
  |
  v
Cache<T>.Factory    (static readonly Func<T>, compiled once per T)
  |
  |  Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile()
  v
T                   (exceptions surface directly to the caller)
```

---

## 7. Exploring a Custom Factory Method Behavior at Runtime

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/) · 1:45

### 总结

本课探讨 C# new() 泛型约束的运行时行为，揭示其底层实现如何在异常处理时导致"抽象泄漏"（leaky abstraction）。
由于编译器使用基于反射的 Activator.CreateInstance 来实现 new() 约束，构造函数抛出的任何异常都会被包装在 TargetInvocationException 中，使错误处理变得困难且描述性更差。
为了解决这个问题，本课演示了如何使用表达式树实现一个 CustomActivator，为对象实例化编译出一个直接的委托，从而保留原始的异常类型，并提升在旧版框架上的性能。

### 核心概念

- **抽象泄漏**：`new()` 约束的实现细节（反射）通过意料之外的异常类型暴露给开发者。
- **TargetInvocationException**：当被调用的成员（构造函数）抛出异常时，由反射抛出的包装异常。
- **Activator.CreateInstance**：C# 编译器为 `new()` 约束使用的底层机制。
- **表达式树**：一种在运行时进行轻量级代码生成、以创建高效委托的技术。
- **自定义激活器**：一种使用缓存的编译表达式来替代默认 `new()` 行为的模式，以获得更好的性能和错误处理。

### 课程笔记

当一个类的构造函数抛出异常时，我们通常期望能捕获到那个具体的异常类型。
然而，当实例是通过使用 `new()` 泛型约束的工厂方法创建时，其行为在运行时会出乎意料地改变。
应用往往不会捕获到那个具体的异常，而是落入通用的兜底处理程序，因为原始异常被包装了。

```csharp
try
{
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass()
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}

static class Factory
{
    public static T Create<T>() where T : new() => new T();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/?t=10)

在上面的示例中，尽管 `MyClass` 抛出的是 `InvalidOperationException`，专门的 `catch` 块却被绕过了。
收到的错误消息往往缺乏描述性，只说明"调用的目标引发了异常"（exception has been thrown by the target of the invocation）。
在日志可能只记录顶层异常的生产环境中，这会让调试变得困难得多。

```text
Factory.Create<MyClass>()
  |  new() constraint --> Activator.CreateInstance
  v
MyClass() throws InvalidOperationException
  |
  v
TargetInvocationException
  +-- InnerException: InvalidOperationException
  |
  v
catch (InvalidOperationException)   bypassed
catch (Exception)                   hit
```

出现这种行为，是因为 `new()` 约束是一个抽象泄漏。
用 IL 查看器检查编译后的代码可以发现，`new()` 约束被编译为使用 `Activator.CreateInstance`。
这一实现细节可以追溯到 C# 2.0。
当通过反射调用的操作失败时，运行时会把实际的异常包装在 `TargetInvocationException` 中。
这一行为在现代 .NET 中依然保留，以避免对依赖捕获该包装异常的代码库造成破坏性变更。

为了在应用代码中解决这个问题，我们可以使用表达式树（在 C# 3.0 中引入）实现一个自定义激活器。
这样就能进行轻量级的代码生成，把一个表达式编译成委托。
随后就可以用这个委托来代替默认的 `new()` 行为。

```csharp
static class Factory
{
    public static T Create<T>() where T : new() => CustomActivator.CreateInstance<T>();
}

#region CustomActivator
// CustomActivator: cached, expression-compiled factory per T.
// Two reasons it differs from `new()` under the constraint:
//   1. Exception handling — a throwing constructor surfaces directly here,
//      whereas going through the new() constraint wraps the exception in TargetInvocationException.
//   2. Performance — significantly faster than `new()` on .NET Framework,
//      where the constraint goes through Activator and pays per-call cost.
static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => Cache<T>.Factory();

    private static class Cache<T> where T : new()
    {
        public static readonly Func<T> Factory =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
#endregion
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/?t=85)

把 `Factory` 更新为使用 `CustomActivator` 之后，应用就不再包装异常了。
当构造函数抛出 `InvalidOperationException` 时，它会直接传递到调用方，使专门的 `catch` 块能够正确地处理该错误。

```csharp
try
{
    // MyClass ctor might fail with InvalidOperationException,
    // so handling that case explicitly.
    Factory.Create<MyClass>();
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"The operation failed: {e.Message}");
}
catch (Exception e)
{
    Console.WriteLine($"Something went wrong: {e.GetType().Name}: {e.Message}");
}

sealed class MyClass
{
    public MyClass()
        => throw new InvalidOperationException(
            "The default profile could not be loaded.");
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/exploring-a-custom-factory-method-behavior-at-runtime-69958852/?t=100)

---

## 8. Benchmarking the new() Constraint

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/) · 1:46

### 总结

本课评估 new() 约束与其他对象实例化方法在不同 .NET 运行时上的性能对比。
在 .NET 10 上，new() 约束几乎没有开销，而在 .NET Framework 4.8 上，它与直接调用构造函数相比有 20 倍的性能损失。
本课演示了在旧版框架上，手动生成 IL 可以比 new() 约束带来 10 倍的性能提升，这也解释了为什么 Entity Framework 和 Newtonsoft.Json 等高性能库会使用基于 IL 的自定义工厂。

### 核心概念

- new() 约束在 .NET 10 与 .NET Framework 4.8 之间的性能差距。
- new() 约束与 Activator.CreateInstance 之间的关系。
- 现代 .NET 运行时中对语言特性的优化。
- 在旧版环境中手动生成 IL 来创建对象的性能优势。
- 使用表达式树和 DynamicMethod 实现自定义工厂。

### 课程笔记

new() 约束的性能是一个重大挑战，尤其是在面向较旧框架时。
虽然 .NET 10 等现代运行时已经对这些语言特性进行了优化，使其几乎没有开销，但旧版框架仍然要付出高昂的代价。

为了测量这一点，一个基准测试比较了几种实例化策略：直接调用构造函数（基线）、委托、Activator.CreateInstance（编译器为 new() 约束生成的就是它）、使用表达式树的自定义激活器，以及手动生成 IL。

```csharp
[MemoryDiagnoser]
[Config(typeof(BenchmarkConfig))]
[HideColumns("Error", "StdDev", "Median", "RatioSD", "Job", "Gen0", "Alloc Ratio")]
public class FactoryBenchmarks
{
    private static readonly Func<Demo> _delegate = () => new Demo();

    [Benchmark(Baseline = true)]
    public Demo DirectConstructor() => new();

    [Benchmark]
    public Demo CreateViaDelegate() => _delegate();

    [Benchmark]
    public Demo ActivatorCreate() => Activator.CreateInstance<Demo>()!;

    [Benchmark]
    public Demo CustomActivatorCreate() => CustomActivator.CreateInstance<Demo>();

    [Benchmark]
    public Demo CreateViaEmittedIL() => EmittedFactory<Demo>.Create();
}

public sealed class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddJob(Job.ShortRun.WithRuntime(ClrRuntime.Net48).WithId("net48"));
        AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core10_0).WithId("net10.0"));
    }
}

public sealed class Demo
{
    public int Value { get; } = 42;
}

public static class CachedFactory<T> where T : new()
{
    public static readonly Func<T> Create =
        System.Linq.Expressions.Expression.Lambda<Func<T>>(System.Linq.Expressions.Expression.New(typeof(T))).Compile();
}

public static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => CachedFactory<T>.Create();
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/?t=25)

在 .NET 10 上，所有实例化方法的表现都相当，这表明 new() 约束的开销实际上已被消除。
在这种情况下，使用自定义激活器的主要动机将是解决异常处理行为的问题，因为出于向后兼容的考虑，new() 约束仍然会把异常包装在 TargetInvocationException 中。

相比之下，.NET Framework 4.8 上直接实例化与 new() 约束之间存在 20 倍的性能差异。
虽然基于表达式的自定义激活器带来了一些改进，但由于安全约束和遗留开销，它们仍然比普通委托慢。

对于旧版框架，最高效的做法是手动生成 IL。
虽然它很复杂，在标准应用代码中也很少使用，但基于 IL 的实现比 new() 约束带来了 10 倍的性能提升。
对于 Entity Framework 或 Newtonsoft.Json 这类会频繁创建大量对象、对性能要求极高的库来说，这种程度的优化是颠覆性的。

```csharp
public static class EmittedFactory<T> where T : new()
{
    public static readonly Func<T> Create = DynamicModuleLambdaCompiler.GenerateFactory<T>();
}

public static class DynamicModuleLambdaCompiler
{
    public static Func<T> GenerateFactory<T>() where T : new()
    {
        System.Linq.Expressions.Expression<Func<T>> expr = () => new T();
        System.Linq.Expressions.NewExpression newExpr = (System.Linq.Expressions.NewExpression)expr.Body;

        var method = new System.Reflection.Emit.DynamicMethod(
            name: "lambda",
            returnType: newExpr.Type,
            parameterTypes: Type.EmptyTypes,
            m: typeof(DynamicModuleLambdaCompiler).Module,
            skipVisibility: true);

        System.Reflection.Emit.ILGenerator ilGen = method.GetILGenerator();
        if (newExpr.Constructor != null)
        {
            ilGen.Emit(System.Reflection.Emit.OpCodes.Newobj, newExpr.Constructor);
        }
        else
        {
            System.Reflection.Emit.LocalBuilder temp = ilGen.DeclareLocal(newExpr.Type);
            ilGen.Emit(System.Reflection.Emit.OpCodes.Ldloca, temp);
            ilGen.Emit(System.Reflection.Emit.OpCodes.Initobj, newExpr.Type);
            ilGen.Emit(System.Reflection.Emit.OpCodes.Ldloc, temp);
        }

        ilGen.Emit(System.Reflection.Emit.OpCodes.Ret);

        return (Func<T>)method.CreateDelegate(typeof(Func<T>));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/benchmarking-the-new-constraint-69958853/?t=79)

---

## 9. Conclusion

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/) · 1:46

### 总结

本课为"掌握类"这一模块作结，回顾了对象构造的细微之处以及 new() 约束的内部机制。
它强调了字段初始化器先于基类构造函数运行这一特定执行顺序、在构造函数中调用虚方法可能暴露部分初始化状态的危险，以及 new() 泛型约束在性能和异常处理上的取舍。
本课指出，虽然 .NET Core 已经优化了 new() 约束，但 .NET Framework 应用可能需要基于 IL 或基于表达式编译的自定义工厂，以避免显著的性能损失和异常包装。

### 核心概念

* **构造顺序**：字段初始化器在基类构造函数之前执行，而基类构造函数又在派生类构造函数体之前执行。
* **虚调用隐患**：在构造函数中调用虚方法会分派到最派生的类型，可能在派生类构造函数初始化字段之前就访问这些字段。
* **静态分析**：应启用规则 CA2214，以防止在构造函数中调用虚成员。
* **new() 约束的内部机制**：编译器使用 `Activator.CreateInstance` 来实现 `new()` 约束。
* **异常处理**：通过 `new()` 约束创建对象期间抛出的异常会被包装在 `TargetInvocationException` 中。
* **性能**：`new()` 约束在 .NET Core 中经过了高度优化，但在 .NET Framework 中可能比直接构造慢多达 22 倍。
* **自定义工厂**：基于表达式编译或 IL 发射（IL-emitted）的工厂可以解决性能和异常包装问题。

### 课程笔记

#### Construction Semantics and Field Initializers

在 C# 中，派生类的构造遵循特定的顺序。
字段初始化器最先执行，甚至在调用基类构造函数之前。
这一行为与字段是否冗余无关。
字段初始化器运行完毕后，基类构造函数执行，最后是派生类构造函数体。

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
    }

    protected static string Init(
        [CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived()
    {
        Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=5)

#### The Virtual Call Hazard

类设计中的一个重大风险是从基类构造函数调用虚方法。
由于 C# 会把虚调用分派到最派生的类型，该方法的派生类实现会在派生类构造函数体运行之前执行。
如果派生类方法依赖于在其构造函数中初始化的字段，它就会观察到一个处于部分构造、可能无效状态的对象。
为了缓解这一问题，开发者应启用静态分析规则 CA2214，以避免在构造函数中进行虚调用。

```csharp
using System.Runtime.CompilerServices;

var d = new Derived();

public class Base
{
    private readonly string _baseField = Init();

    public Base()
    {
        Console.WriteLine($"Base .ctor, _baseField.Length = {_baseField.Length}");
        ReportState();
    }

    protected virtual void ReportState()
    {
    }

    protected static string Init([CallerMemberName] string name = "")
    {
        Console.WriteLine($"Field initializer for {name}");
        return $"initialized:{name}";
    }
}

public class Derived : Base
{
    private readonly string _derivedField = Init();

    public Derived() => Console.WriteLine($"Derived .ctor, _derivedField.Length = {_derivedField.Length}");

    protected override void ReportState()
    {
        // This may access _derivedField before the Derived constructor body runs
        Console.WriteLine($"Derived override, _derivedField.Length = {_derivedField.Length}"  );
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=34)

#### The new() Constraint and Performance

在底层，编译器把 `new()` 泛型约束翻译为对 `Activator.CreateInstance` 的调用。
这会带来两个主要后果。
第一，出于历史原因，构造期间抛出的任何异常都会被包装在 `TargetInvocationException` 中。
第二，虽然在 .NET Core 中性能非常出色，但在 .NET Framework 中，`Activator` 的开销可能使构造比直接实例化慢多达 22 倍。

为了解决这些问题，可以使用一个基于表达式树的 `CustomActivator` 来缓存编译好的工厂，这样异常可以直接传递出来，并能提升在较旧运行时上的性能。

```csharp
using System.Linq.Expressions;

static class Factory
{
    public static T Create<T>() where T : new() => new();
}

// CustomActivator: cached, expression-compiled factory per T.
static class CustomActivator
{
    public static T CreateInstance<T>() where T : new() => Cache<T>.Factory();

    private static class Cache<T> where T : new()
    {
        public static readonly Func<T> Factory =
            Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=65)

对于需要极致性能的场景，尤其是在旧版环境中，可以使用 `DynamicMethod` 进行基于 IL 的代码生成，从而生成一个性能接近直接调用构造函数的工厂。

```csharp
using System.Reflection.Emit;

public static class DynamicModuleLambdaCompiler
{
    public static Func<T> GenerateFactory<T>() where T : new()
    {
        Expression<Func<T>> expr = () => new T();
        NewExpression newExpr = (NewExpression)expr.Body;

        var method = new DynamicMethod(
            name: "lambda",
            returnType: newExpr.Type,
            parameterTypes: Type.EmptyTypes,
            m: typeof(DynamicModuleLambdaCompiler).Module,
            skipVisibility: true);

        ILGenerator ilGen = method.GetILGenerator();
        if (newExpr.Constructor != null)
        {
            ilGen.Emit(OpCodes.Newobj, newExpr.Constructor);
        }
        else
        {
            LocalBuilder temp = ilGen.DeclareLocal(newExpr.Type);
            ilGen.Emit(OpCodes.Ldloca, temp);
            ilGen.Emit(OpCodes.Initobj, newExpr.Type);
            ilGen.Emit(OpCodes.Ldloc, temp);
        }

        ilGen.Emit(OpCodes.Ret);

        return (Func<T>)method.CreateDelegate(typeof(Func<T>));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958854/?t=93)

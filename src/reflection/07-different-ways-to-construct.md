# Different Ways To Construct

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 7 章
> 共 3 课 · 约 28:22
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Recap & Introduction](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/recap-introduction-58131040/) | 3:15 | [↓](#1-recap--introduction) |
| 2 | [Activator Create Instance](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/activator-create-instance-58131041/) | 9:47 | [↓](#2-activator-create-instance) |
| 3 | [Example Application](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/) | 15:20 | [↓](#3-example-application) |

## 1. Recap & Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/recap-introduction-58131040/) · 3:15

### 总结

本课从概念上概述了 .NET 中的对象实例化,对比了标准的编译时构造函数调用与基于反射的方法。
它回顾了之前讨论过的技术,例如使用 ConstructorInfo 和 Type.InvokeMember,并概述了需要使用反射的具体场景,包括插件架构、序列化以及 ORM 映射。
本课最后引入了 Activator 类,作为下一种动态创建对象的机制。

### 核心概念

* **直接实例化(Direct Instantiation)**:当类型在编译时已知时,首选的对象创建方式。
* **ConstructorInfo**:一种基于反射的方法,使用 `Type.GetConstructors` 查找并调用特定的构造函数元数据。
* **Type.InvokeMember**:一种通过动态指定绑定标志和成员名称来实例化对象的机制。
* **动态用例(Dynamic Use Cases)**:诸如插件加载、序列化/反序列化以及 ORM 等直到运行时才知道类型的场景。
* **性能考量(Performance Considerations)**:基于反射的实例化通常比直接调用慢,这个话题会在后续课程中探讨。

### 课程笔记

在标准的 .NET 开发中,创建对象的主要方式是直接调用构造函数。
当类型在编译时已知时,使用 `new` 关键字是最高效、类型最安全的方法。
只有当具体的类型或构造函数直到运行时才能确定时,才应该使用反射。

#### 之前的反射方法

我们已经探讨过两种基于反射进行实例化的主要方法:
1. **ConstructorInfo**:通过查询一个 `Type` 对象,开发者可以获取 `ConstructorInfo` 对象。
这允许进行细粒度的控制,例如使用 `BindingFlags` 来定位私有或静态构造函数。
一旦获得了所需的 `ConstructorInfo`,就调用 `Invoke` 方法来创建实例。
2. **Type.InvokeMember**:这种方法允许在不先显式获取 `ConstructorInfo` 的情况下进行实例化。
它使用 `Type` 类上的 `InvokeMember` 方法,需要特定的标志来识别构造函数。

#### 何时使用反射

虽然反射很强大,但如果构造函数在编译时可访问,出于性能开销的考虑,应该避免使用反射。
然而,在若干架构场景中,反射是必不可少的:
* **插件加载(Plugin Loading)**:加载外部程序集,并实例化主应用程序编译时未知的类型。
* **序列化与反序列化(Serialization and Deserialization)**:将数据格式(如 JSON 或 XML)转换为对象实例,其中目标类型由数据或配置决定。
* **ORM(对象关系映射器)**:将数据库记录动态映射为 C# 对象。

像基于 Roslyn 的代码生成这样的现代替代方案,有时可以通过在编译时生成必要的映射和实例化代码来替代反射完成这些任务,不过这是一个进阶话题。

#### Activator 类简介

动态实例化的下一个合乎逻辑的步骤是 `Activator` 类。
这个工具类提供了一种动态创建类型实例的精简方式,它将是下一课的重点。

## 2. Activator Create Instance

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/activator-create-instance-58131041/) · 9:47

### 总结

Activator 类为在 .NET 中动态创建对象实例提供了一个集中的 API,是手动获取 ConstructorInfo 或使用 Type.InvokeMember 之外的一种更直接的替代方案。
本课探讨 Activator.CreateInstance 的各种重载,演示如何实例化具有无参构造函数、需要参数的构造函数以及私有构造函数的类型。
它强调了非泛型版本与泛型版本之间的区别、参数匹配对于构造函数选择的重要性,以及如何使用 BindingFlags 或简写的布尔标志来访问非公共成员。

### 核心概念

- **Activator 类**:一个专门用于后期绑定对象构造的静态工具类。
- **泛型与非泛型**:`Activator.CreateInstance<T>()` 提供编译时类型安全,而 `Activator.CreateInstance(Type)` 返回 `object?`。
- **构造函数匹配**:运行时会根据 `params object[]` 数组中提供的参数自动选择最佳构造函数。
- **非公共构造**:访问私有构造函数需要显式的 `BindingFlags` 或 `nonPublic` 布尔标志。
- **BindingFlags**:用于指定构造函数的搜索条件,例如 `NonPublic` 和 `Instance`。

### 课程笔记

`Activator` 类是一个专门用于构造对象的工具类。
与手动查询 `ConstructorInfo` 或使用 `Type.InvokeMember` 相比,它提供了更直接的接口。
为了演示它的能力,请看以下三个具有不同构造函数访问级别和参数的类型:

```csharp
public sealed class CustomType1
{
    public CustomType1()
    {
        Console.WriteLine("CustomType1 constructor");
    }
}

public sealed class CustomType2
{
    public CustomType2(string parameter1)
    {
        Console.WriteLine("CustomType2 constructor");
        Console.WriteLine($"\tParameter1: {parameter1}");
    }
}

public sealed class CustomType3
{
    private CustomType3()
    {
        Console.WriteLine("CustomType3 constructor without parameters");
    }

    private CustomType3(string parameter1)
    {
        Console.WriteLine("CustomType3 constructor");
        Console.WriteLine($"\tParameter1: {parameter1}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/activator-create-instance-58131041/?t=70)

#### 基础实例化

当实例化一个具有公共无参构造函数的类型时,可以使用 `CreateInstance` 的非泛型版本或泛型版本。
非泛型版本接收一个 `Type` 对象并返回 `object?`,这可能需要进行类型转换。
泛型版本则在编译时返回具体的类型。

```csharp
Type type1 = typeof(CustomType1);

// Returns object?
object? instance1 = Activator.CreateInstance(type1);

// Returns CustomType1 (Generic version)
CustomType1 instance1a = Activator.CreateInstance<CustomType1>();

// Verification
Console.WriteLine($"Is Instance 1 CustomType1: {instance1 is CustomType1}");
Console.WriteLine($"Is Instance 1a CustomType1: {instance1a is CustomType1}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/activator-create-instance-58131041/?t=145)

#### 传递参数

`Activator.CreateInstance` 包含接受 `params object?[]? args` 数组的重载。
运行时使用这些参数来查找最合适的构造函数。
如果提供的参数与任何可用的构造函数都不匹配,系统会抛出 `MissingMethodException`,提示找不到该构造函数。

```csharp
var type2 = typeof(CustomType2);
var instance2 = Activator.CreateInstance(
    type2,
    "This is the parameter!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/activator-create-instance-58131041/?t=280)

#### 访问私有构造函数

默认情况下,`Activator.CreateInstance` 只搜索公共构造函数。
要调用私有构造函数,必须使用允许非公共访问的重载。
这可以通过显式的 `BindingFlags` 来实现,对于无参构造函数也可以使用简化的布尔标志。

对于带参数的构造函数,必须使用指定 `BindingFlags.NonPublic | BindingFlags.Instance` 的完整重载。
对于无参的私有构造函数,存在一个接受 `bool nonPublic` 参数的简写重载。

```csharp
var type3 = typeof(CustomType3);

// Using BindingFlags to access a private constructor with parameters
var instance3 = Activator.CreateInstance(
    type3,
    BindingFlags.NonPublic |
    BindingFlags.Instance,
    null,
    ["This is the parameter!"],
    null);

// Using the shorthand for a private parameterless constructor
var instance4 = Activator.CreateInstance(
    type3,
    nonPublic: true);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/activator-create-instance-58131041/?t=370)

## 3. Example Application

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/) · 15:20

### 总结

本课演示如何开发一个基于反射的动态对象实例化工具,它利用自定义的 ConstructableAttribute 来识别程序集中的特定类型。
该应用程序实现了一个由用户驱动的工作流,允许选择类型和激活方法,具体对比了 Activator.CreateInstance 与 ConstructorInfo.Invoke。
通过检查构造函数元数据,并将控制台输入解析为整数和字符串等相应类型,该工具把用户参数映射到构造函数参数上。
本节最后演示了应用程序同时处理无参构造函数和带参构造函数,并讨论了在处理难以从字符串输入解析的复杂参数类型时遇到的局限。

### 核心概念

- 程序集扫描(Assembly scanning)
- 自定义特性(Custom attributes)
- 类型筛选(Type filtering)
- Activator.CreateInstance
- ConstructorInfo.Invoke
- 参数元数据检查(Parameter metadata inspection)
- 动态参数收集(Dynamic argument collection)
- Activator.CreateInstance 的实现
- ConstructorInfo.Invoke 的实现
- 标记特性(Marker Attributes)
- AttributeUsage 限制
- 动态参数映射(Dynamic argument mapping)
- 基于反射的类型筛选(Reflection-based type filtering)

### 课程笔记

#### 程序集扫描与类型筛选

应用程序首先扫描当前正在执行的程序集,以识别应当可供实例化的类型。
这是通过获取程序集中的所有类型,并根据是否存在自定义的 `[Constructable]` 特性对其进行筛选来实现的。
这个特性充当一个标志,表明反射逻辑被允许实例化哪些类。

```csharp
using System.Reflection;

Console.WriteLine();

var constructableTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.GetCustomAttribute<ConstructableAttribute>() != null)
    .ToArray();
Console.WriteLine(
    $"Found {constructableTypes.Length} types with the [Constructable] attribute.");

while (true)
{
    var typeToCreate = AskForType(constructableTypes);
    if (typeToCreate is null)
    {
        continue;
    }

    var activationMethod = AskForActivationMethod();
    if (!activationMethod.HasValue)
    {
        continue;
    }

    var instance = activationMethod switch
    {
        1 => ActivatorCreate(typeToCreate),
        2 => ConstructorInfoCreate(typeToCreate),
        _ => FailActivationMethod(activationMethod.Value),
    };

    if (instance is null)
    {
        Console.WriteLine("Failed to create an instance.");
        continue;
    }

    Console.WriteLine($"Created an instance of '{typeToCreate.Name}'!");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=40)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
static object? FailActivationMethod(int activationMethod)
{
    Console.WriteLine($"Invalid activation method '{activationMethod}'.");
    return null;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=85)

```text
Assembly.GetExecutingAssembly().GetTypes()
        |
        v  .Where(GetCustomAttribute<ConstructableAttribute>() != null)
constructableTypes
        |
        v
while (true)
  AskForType ------------> null? -> continue
        |
  AskForActivationMethod -> null? -> continue
        |
  activationMethod switch
    1 => ActivatorCreate
    2 => ConstructorInfoCreate
    _ => FailActivationMethod
        |
  instance is null? -> "Failed to create an instance."
        |
  "Created an instance of '{typeToCreate.Name}'!"
```

#### 用户输入与类型选择

应用程序提示用户从筛选后的列表中按名称选择一个类型。
它使用不区分大小写的字符串比较,将用户的输入与集合中 `Type` 对象的 `Name` 属性进行匹配。

```csharp
static Type? AskForType(Type[] constructableTypes)
{
    Console.WriteLine("Available type names are:");
    foreach (var type in constructableTypes)
    {
        Console.WriteLine($"  {type.Name}");
    }

    Console.WriteLine("Enter the name of the type to create an instance of:");
    var typeNameUserInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(typeNameUserInput))
    {
        return null;
    }

    var typeToCreate = constructableTypes
        .FirstOrDefault(t => string
            .Equals(
                t.Name,
                typeNameUserInput,
                StringComparison.OrdinalIgnoreCase));
    if (typeToCreate is null)
    {
        Console.WriteLine("Type not found. Check the spelling!");
        return null;
    }

    return typeToCreate;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=100)

选定类型之后,用户在不同的基于反射的激活方法之间进行选择。
虽然只实现了 `Activator.CreateInstance` 和 `ConstructorInfo.Invoke`,但 `InvokeMember` 被提及为该应用程序一个可能的扩展。

```csharp
static int? AskForActivationMethod()
{
    while (true)
    {
        Console.WriteLine("Available activation methods:");
        Console.WriteLine(" 1. Activator.CreateInstance");
        Console.WriteLine(" 2. ConstructorInfo.Invoke");
        Console.WriteLine("Enter the number of the activation method to use:");
        var activationMethodUserInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(activationMethodUserInput))
        {
            return null;
        }

        if (!int.TryParse(
            activationMethodUserInput,
            out var activationMethodNumber))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            continue;
        }

        return activationMethodNumber;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=190)

#### 实例化逻辑

应用程序根据用户的选择,把实际的对象创建委托给特定的方法。
这两个方法都会先调用 `AskForArguments` 来收集构造函数所需的数据。

```csharp
static object? ActivatorCreate(Type type)
{
    while (true)
    {
        var arguments = AskForArguments(type);
        if (arguments is null)
        {
            continue;
        }

        Console.WriteLine(
            $"Creating an instance of '{type.Name}' with Activator.CreateInstance...");
        return Activator.CreateInstance(type, arguments);
    }
}

static object? ConstructorInfoCreate(Type type)
{
    while (true)
    {
        var arguments = AskForArguments(type);
        if (arguments is null)
        {
            continue;
        }
        // Implementation continues with ConstructorInfo.Invoke
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=250)

#### 构造函数与参数检查

为了处理带参数的类型,应用程序使用 `GetConstructors()` 查找可用的公共构造函数。
为了让这个示例保持简单,逻辑被限制为只处理恰好有一个公共构造函数的类型。
一旦确定了构造函数,就调用 `GetParameters()` 来检查所需的参数。

```csharp
static object?[]? AskForArguments(Type type)
{
    var constructors = type.GetConstructors();
    if (constructors.Length == 0)
    {
        Console.WriteLine("No public constructors found.");
        return null;
    }

    if (constructors.Length != 1)
    {
        Console.WriteLine("Multiple constructors found.");
        return null;
    }

    var constructor = constructors[0];
    var parameters = constructor.GetParameters();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=310)

#### 动态参数收集

应用程序遍历这些参数,并为每个参数提示用户输入。
它会进行类型检查,以确保能够处理该参数类型,具体支持 `int`(通过 `int.TryParse`)和 `string`。
如果类型不受支持或解析失败,应用程序会向用户提供反馈。

```csharp
foreach (var parameter in parameters)
{
    argumentIndex++;

    if (parameter.ParameterType.Equals(typeof(int)))
    {
        Console.WriteLine($"Enter an integer value for '{parameter.Name}':");
        var userInput = Console.ReadLine();
        if (!int.TryParse(userInput, out var value))
        {
            Console.WriteLine("Invalid input. Please enter an integer.");
            argumentCollectionFailed = true;
            break;
        }

        arguments[argumentIndex] = value;
    }
    else if (parameter.ParameterType.Equals(typeof(string)))
    {
        Console.WriteLine($"Enter a string value for '{parameter.Name}':");
        var value = Console.ReadLine();
        arguments[argumentIndex] = value;
    }
    else
    {
        Console.WriteLine(
            $"Unsupported parameter type '{parameter.ParameterType.Name}'.");
        return null;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=370)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
while (true)
            {
                var arguments = new object?[parameters.Length];
                int argumentIndex = -1;
                bool argumentCollectionFailed = false;

                foreach (var parameter in parameters)
                {
                    argumentIndex++;

                    if (parameter.ParameterType.Equals(typeof(int)))
                    {
                        Console.WriteLine($"Enter an integer value for '{parameter.Name}':");
                        var userInput = Console.ReadLine();
                        if (!int.TryParse(userInput, out var value))
                        {
                            Console.WriteLine("Invalid input. Please enter an integer.");
                            argumentCollectionFailed = true;
                            break;
                        }

                        arguments[argumentIndex] = value;
                    }
                    else if (parameter.ParameterType.Equals(typeof(string)))
                    {
                        Console.WriteLine($"Enter a string value for '{parameter.Name}':");
                        var value = Console.ReadLine();
                        arguments[argumentIndex] = value;
                    }
                    else
                    {
                        // TODO: how might you expand this even further for more support?
                        Console.WriteLine(
                            $"Unsupported parameter type '{parameter.ParameterType.Name}'.");
                        return null;
                    }
                }

                if (argumentCollectionFailed)
                {
                    continue;
                }

                return arguments;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ConstructableAttribute : Attribute
    {
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=460)

最后,`ConstructableAttribute` 被定义为一个仅限用于类的简单标记特性。

```csharp
[AttributeUsage(AttributeTargets.Class)]
public sealed class ConstructableAttribute : Attribute
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=460)

#### 实现激活方法

应用程序提供了两条不同的实例创建路径:使用 `Activator` 类和使用 `ConstructorInfo`。
这两个方法都依赖一个辅助函数 `AskForArguments`,它获取目标类型构造函数所需的参数。
如果 `AskForArguments` 返回 `null`,表示未能收集到有效输入,循环会继续提示用户。

在 `ActivatorCreate` 方法中,通过把类型和参数对象数组传给 `Activator.CreateInstance` 来创建实例。
在 `ConstructorInfoCreate` 方法中,代码获取该类型的构造函数。
为了让这个示例保持简单,它通过调用 `.Single()` 假定只存在一个构造函数。
然后通过在该 `ConstructorInfo` 对象上调用 `Invoke` 来创建实例。

```csharp
static object? ActivatorCreate(Type type)
{
    while (true)
    {
        var arguments = AskForArguments(type);
        if (arguments is null)
        {
            continue;
        }

        Console.WriteLine(
            $"Creating an instance of '{type.Name}' with Activator.CreateInstance...");
        return Activator.CreateInstance(type, arguments);
    }
}

static object? ConstructorInfoCreate(Type type)
{
    while (true)
    {
        var arguments = AskForArguments(type);
        if (arguments is null)
        {
            continue;
        }

        Console.WriteLine(
            $"Creating an instance of '{type.Name}' with ConstructorInfo...");
        var constructorInfo = type.GetConstructors().Single();
        return constructorInfo.Invoke(arguments);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=520)

#### 主执行循环

应用程序的主循环负责编排用户交互。
它首先识别出正在执行的程序集中所有用 `[Constructable]` 特性修饰的类型。
然后提示用户选择一个类型和一种激活方法。
如果激活返回了一个有效实例,应用程序会确认创建成功;否则,它会报告失败并重新开始循环。

```csharp
var constructableTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.GetCustomAttribute<ConstructableAttribute>() != null)
    .ToArray();
Console.WriteLine(
    $"Found {constructableTypes.Length} types with the [Constructable] attribute.");

while (true)
{
    var typeToCreate = AskForType(constructableTypes);
    if (typeToCreate is null)
    {
        continue;
    }

    var activationMethod = AskForActivationMethod();
    if (!activationMethod.HasValue)
    {
        continue;
    }

    var instance = activationMethod switch
    {
        1 => ActivatorCreate(typeToCreate),
        2 => ConstructorInfoCreate(typeToCreate),
        _ => FailActivationMethod(activationMethod.Value),
    };

    if (instance is null)
    {
        Console.WriteLine("Failed to create an instance.");
        continue;
    }

    Console.WriteLine($"Created an instance of '{typeToCreate.Name}'!");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=580)

#### 定义可构造类型

`ConstructableAttribute` 是一个通过 `[AttributeUsage(AttributeTargets.Class)]` 限制为只能用于类的标记特性。
这个特性让应用程序能够筛选出哪些类型可用于动态实例化。
在这个示例中,`TypeA` 具有一个无参构造函数,而 `TypeB` 需要一个 `int` 和一个 `string`。

```csharp
[AttributeUsage(AttributeTargets.Class)]
public sealed class ConstructableAttribute : Attribute
{
}

[Constructable]
public sealed class TypeA
{
    public TypeA()
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Hello, from TypeA!");
        Console.ForegroundColor = currentColor;
    }
}

[Constructable]
public sealed class TypeB
{
    public TypeB(int value1, string value2)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Hello, from TypeB!");
        Console.WriteLine($"  Value1: {value1}");
        Console.WriteLine($"  Value2: {value2}");
        Console.ForegroundColor = currentColor;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=625)

#### 局限与边界情况

虽然应用程序通过解析控制台输入成功处理了 `int` 和 `string` 这样的基元类型,但它在面对复杂类型时存在局限。
如果某个构造函数需要另一个类的实例(例如 `TypeB` 需要一个 `TypeA` 的实例),那么在没有专门处理或递归实例化逻辑的情况下,当前基于 CLI 的输入逻辑无法满足这一需求。

```csharp
[Constructable]
public sealed class TypeB
{
    // The current application would struggle to provide 'TypeA a' via console input
    public TypeB(int value1, string value2, TypeA a)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Hello, from TypeB!");
        Console.WriteLine($" Value1: {value1}");
        Console.WriteLine($" Value2: {value2}");
        Console.ForegroundColor = currentColor;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131042/?t=895)

# Attributes

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 6 章
> 共 4 课 · 约 33:25
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is an attribute](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-an-attribute-58131044/) | 6:58 | [↓](#1-what-is-an-attribute) |
| 2 | [Restrict to certain constructs](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/restrict-to-certain-constructs-58131045/) | 5:50 | [↓](#2-restrict-to-certain-constructs) |
| 3 | [Finding Attributes on Types](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/finding-attributes-on-types-58131046/) | 9:58 | [↓](#3-finding-attributes-on-types) |
| 4 | [Example Application](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131047/) | 10:39 | [↓](#4-example-application) |

## 1. What is an attribute

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-an-attribute-58131044/) · 6:58

### 总结

特性(attribute)是 C# 中的一种特殊类,用于给代码元素标注元数据,这些元数据存储在程序集中,并可在运行时通过反射访问。
它们让开发者能够装饰类型、方法和属性,从而向编译器或其他软件组件提供额外的上下文,例如把代码标记为已过时或可序列化。

### 核心概念

- **元数据装饰**:特性为程序集中的类型、方法、属性和参数添加元数据。
- **方括号语法**:特性通过写在目标元素上方的 `[]` 语法来应用。
- **命名约定**:特性类通常以 `Attribute` 后缀结尾,在代码中应用特性时可以省略该后缀。
- **内置特性**:.NET 为常见的元数据任务提供了 `[Serializable]` 和 `[Obsolete]` 等标准特性。
- **自定义特性**:开发者可以通过继承 `System.Attribute` 基类来创建自定义特性。
- **编译时约束**:特性参数必须是常量表达式、`typeof` 表达式或数组创建表达式。

### 课程笔记

特性是 C# 中的一项功能,它允许用元数据来装饰类型。
这些信息会被嵌入到已编译程序集的元数据中。
使用反射时,可以读取这些元数据,以检查应用在类型上的特性,并根据特性是否存在或其属性来执行逻辑。

#### 语法与命名

特性使用方括号来应用。
按照约定,每个特性类都带有 `Attribute` 后缀。
应用特性时,C# 允许使用省略该后缀的简写语法。
例如,`[Serializable]` 和 `[SerializableAttribute]` 在功能上完全相同。

```csharp
[Serializable]
public sealed class MySerializableType
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-an-attribute-58131044/?t=85)

#### Obsolete 特性

`[Obsolete]` 特性是一个内置工具,用于标记不再使用的程序元素。
它既可以应用于整个类,也可以应用于特定方法。
它支持传入参数,以便向开发者提供自定义消息,并且可以选择配置为触发编译器错误而不是警告。

```csharp
[Obsolete("This type is obsolete. Use MyNewType instead.")]
public sealed class MyObsoleteType
{
    public void TheNewFancyMethod()
    {
    }

    [Obsolete("This method is obsolete. Use TheNewFancyMethod instead.")]
    public void TheOldMethod()
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-an-attribute-58131044/?t=115)

当一个类型被标记为已过时后,在实例化该类型时,IDE 会提供可视化反馈,例如工具提示和警告。
但是,已过时类中的成员并不会自动继承已过时状态;只有当某个方法被专门标记,或其所在类型被引用时,才会触发已过时警告。

#### 创建自定义特性

自定义特性通过定义一个继承自 `System.Attribute` 基类的类来创建。
这些类可以拥有构造函数和属性,用来存储稍后将通过反射读取的数据。

#### 特性参数的约束

可以传入特性构造函数的数据受到严格约束。
因为特性是在编译时求值的,所以每个参数都必须是常量表达式。
这包括字面量(字符串、整数)、`typeof` 表达式,或特定的数组创建表达式。

尝试传入非常量值(例如 `new object()`)会导致编译器错误,提示特性参数必须是常量表达式。

```csharp
[SuperFancy("Hello, World!", 42, new object())]
public sealed class MySuperFancyType
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-an-attribute-58131044/?t=325)

要修复这个问题,特性的定义和使用都必须只涉及受支持的常量类型:

```csharp
public sealed class SuperFancyAttribute : Attribute
{
    public SuperFancyAttribute(
        string someString,
        int someInt)
    {
        SomeString = someString;
        SomeInt = someInt;
    }

    public string SomeString { get; }

    public int SomeInt { get; }
}

[SuperFancy("Hello, World!", 42)]
public sealed class MySuperFancyType
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-an-attribute-58131044/?t=385)

## 2. Restrict to certain constructs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/restrict-to-certain-constructs-58131045/) · 5:50

### 总结

默认情况下,自定义特性可以应用于任何代码元素。
为了把它们的使用限制在类、方法或参数等特定构造上,开发者会使用 AttributeUsage 特性。
该特性接收一个 AttributeTargets 按位标志枚举,从而可以精确控制特性在哪些地方有效。
如果把特性应用到不受支持的目标上,C# 编译器会产生错误,从而确保元数据只附加到合适的成员上。

### 核心概念

- AttributeUsageAttribute:一个用于配置自定义特性行为的元特性。
- AttributeTargets:一个由标志组成的枚举(例如 Class、Method、Parameter、All),定义了有效的目标。
- 按位或(|):用于允许特性出现在多个特定构造上。
- 编译器验证:在编译时强制执行特性放置规则。
- CallerMemberNameAttribute:一个专门限制在参数上的内置示例。

### 课程笔记

为了控制自定义特性可以应用在哪里,.NET 提供了 AttributeUsageAttribute。
它应用于特性类本身,通过 AttributeTargets 枚举来定义有效目标。
这种把特性递归地应用到其他特性上的方式,让开发者可以定义自定义元数据的作用范围。

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Class)]
public sealed class CustomClassAttribute : Attribute
{
    public CustomClassAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class CustomMethodAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class CustomParameterAttribute : Attribute
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/restrict-to-certain-constructs-58131045/?t=25)

AttributeTargets 枚举是一个按位标志,这意味着可以使用或运算符(|)组合多个目标。
这样,单个特性就可以在多种不同的构造上有效,而无需为每种构造分别定义特性。

```csharp
[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Constructor |
    AttributeTargets.Delegate)]
public sealed class CustomClassAttribute : Attribute
{
    public CustomClassAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/restrict-to-certain-constructs-58131045/?t=85)

当特性被限制到某个特定目标时,编译器会强制执行这些规则。
如果尝试把特性应用到其 AttributeUsage 定义中未包含的构造上,编译器会报错,提示该特性在此声明类型上无效。
完全移除 AttributeUsage 特性,则特性默认在所有可能的目标上都有效。

```csharp
[CustomClass("Super Cool Class Name")]
public class OurDecoratedClass
{
    [CustomClass("asdasd")] // This will cause a compiler error because CustomClass is restricted to Class targets
    [CustomMethod]
    public void OurMethod(
        [CustomParameter] string theInput)
    {
    
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/restrict-to-certain-constructs-58131045/?t=175)

.NET 基础类库中受限特性的一个实际例子是 CallerMemberNameAttribute。
该特性经常用在 INotifyPropertyChanged 的实现中,用来自动捕获正在被更改的属性的名称。
因为它被设计为在调用处由编译器填充,所以它被专门限制在参数上。

```csharp
public sealed class ObservableType : INotifyPropertyChanged
{
    private string? _name;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Name
    {
        get => _name;
        set
        {
            if (_name == value)
            {
                return;
            }

            _name = value;
            OnPropertyChanged();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/restrict-to-certain-constructs-58131045/?t=250)

如果尝试把 [CallerMemberName] 应用到类或方法上而不是参数上,编译器会阻止构建,因为该特性的逻辑只适用于参数注入。

## 3. Finding Attributes on Types

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/finding-attributes-on-types-58131046/) · 9:58

### 总结

本课探讨如何在实践中运用反射,来发现并操作装饰在类型、方法和参数上的特性。
它演示了如何在程序集中查询标记了特定自定义特性的类型,如何深入类成员以找到带标注的方法,以及如何检查方法参数上的元数据。
本课强调,特性代表的是与类型定义本身相关联的静态元数据,而不是与特定对象实例相关联的状态。

### 核心概念

* **程序集级查询**:结合使用 `Assembly.GetTypes()` 和 LINQ 来查找装饰了特定特性的类型。
* **成员检查**:在 `Type` 对象上使用 `GetMethods()` 来定位带标注的方法。
* **参数元数据**:通过 `GetParameters()` 和 `GetCustomAttributes()` 访问方法参数上的特性。
* **类型与实例**:理解特性是类型元数据的一部分,需要通过 `Type` 对象访问,而不是通过类的实例。
* **特性过滤**:使用 `GetCustomAttributes<T>()` 检查特性是否存在,使用 `GetCustomAttribute<T>()` 获取特性实例及其属性。

### 课程笔记

要开始通过反射处理特性,我们首先定义一个自定义特性。
在这个例子中,创建了 `MyFancyAttribute`,并使用 `AttributeUsage` 特性把它限制在类和方法上。
`AttributeTargets` 标志让该特性既可以应用于类定义,也可以应用于类内部的方法。

```csharp
[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Method)]
public sealed class MyFancyAttribute : Attribute
{
    public MyFancyAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

[MyFancyAttribute("This class has been annotated!")]
public sealed class MyType
{
    [MyFancyAttribute("This has a fancy name!")]
    public void MyMethod([CallerMemberName] string? name = null)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/finding-attributes-on-types-58131046/?t=85)

#### 查询程序集中的类型

反射让我们可以扫描整个程序集,找出装饰了特定特性的类型。
通过在正在执行的程序集上调用 `GetTypes()`,我们可以使用 LINQ 和 `GetCustomAttributes<T>()` 方法对集合进行过滤。
该方法返回在成员上找到的指定类型特性的集合。

```csharp
using System.Reflection;
using System.Runtime.CompilerServices;

var typesThatHaveMyFancyAttribute = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.GetCustomAttributes<MyFancyAttribute>().Any())
    .ToArray();

Console.WriteLine($"Found {typesThatHaveMyFancyAttribute.Length} types with the MyFancyAttribute:");
foreach (var t in typesThatHaveMyFancyAttribute)
{
    var attribute = t.GetCustomAttribute<MyFancyAttribute>();
    Console.WriteLine($"Type: {t.Name}, Name: {attribute.Name}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/finding-attributes-on-types-58131046/?t=115)

需要注意的是,特性附加在类型信息上,而不是附加在类型的实例上。
当我们调用 `GetCustomAttribute` 时,是在向 `Type` 对象请求它的元数据。
我们不需要实例化 `MyType` 就能访问存储在 `MyFancyAttribute` 中的值,因为这些元数据对于定义来说是静态的。

```text
MyType 的定义 (Type)
  +-- 元数据: [MyFancyAttribute("This class has been annotated!")]
          ^
          |  t.GetCustomAttribute<MyFancyAttribute>()
          |  (无需实例化 MyType)
```

#### 查找方法上的特性

一旦确定了某个类型,我们就可以进一步检查它的成员,例如方法。
过程是类似的:在 `Type` 对象上调用 `GetMethods()`,并根据自定义特性是否存在来过滤结果。

```csharp
[MyFancyAttribute("This class has been annotated!")]
public sealed class MyType
{
    [MyFancyAttribute("This has a fancy name!")]
    public void MyMethod([CallerMemberName] string? name = null)
    {
    }

    [MyFancyAttribute("This ALSO has a fancy name!")]
    public void MyMethod2(string? name = null)
    {
    }

    public void NotFancyAtAll(string? name = null)
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/finding-attributes-on-types-58131046/?t=400)

#### 查找参数上的特性

反射还支持对方法参数进行深入检查。
为了找到装饰了特定特性(例如内置的 `CallerMemberNameAttribute`)的参数,我们使用 `GetParameters()` 遍历方法的参数,并逐一检查是否带有目标特性。

```csharp
var methodsWithCallerMemberNameParams = type
    .GetMethods()
    .Where(method => method
        .GetCustomAttributes<MyFancyAttribute>()
        .Any())
    .Where(method => method
        .GetParameters()
        .Any(param => param
            .GetCustomAttributes<CallerMemberNameAttribute>()
            .Any()))
    .ToArray();

Console.WriteLine($"Found {methodsWithCallerMemberNameParams.Length} methods with the CallerMemberNameAttribute:");
foreach (var method in methodsWithCallerMemberNameParams)
{
    var parameter = method
        .GetParameters()
        .Single(param => param
            .GetCustomAttributes<CallerMemberNameAttribute>()
            .Any());
    Console.WriteLine($"Method: {method.Name}, Parameter: {parameter.Name}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/finding-attributes-on-types-58131046/?t=535)

```text
Assembly.GetExecutingAssembly()
  |
  +-- GetTypes()       -> type      GetCustomAttributes<MyFancyAttribute>()
        |
        +-- GetMethods()     -> method   GetCustomAttributes<MyFancyAttribute>()
              |
              +-- GetParameters() -> param  GetCustomAttributes<
                                              CallerMemberNameAttribute>()
```

这种多层级的过滤让开发者能够基于代码的装饰方式构建复杂的逻辑,例如自动识别需要特殊处理的方法,或应当注入调用方信息的参数。

## 4. Example Application

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131047/) · 10:39

### 总结

本课通过构建一个文档生成器,演示了自定义特性与反射的实际运用。
该应用程序会扫描程序集,查找装饰了自定义 DocumentationAttribute 的类型和成员,然后提取它们的描述,并以结构化的树形格式显示出来。
这个练习综合了程序集加载、类型检查、使用绑定标志获取成员,以及使用 GetCustomAttribute 获取特性等概念。

### 核心概念

*   带有 `AttributeUsage` 约束的自定义特性的定义与使用。
*   使用 `Assembly.GetExecutingAssembly` 和 `Assembly.LoadFrom` 扫描程序集。
*   通过 `assembly.GetTypes()` 发现类型。
*   使用带有特定 `BindingFlags` 的 `type.GetMembers()` 检查成员。
*   使用 `GetCustomAttribute<T>()` 扩展方法提取特性数据。
*   为控制台输出格式化反射元数据(例如 `MemberType`、`Name`)。

### 课程笔记

这个示例应用程序围绕一个用于标注各种代码元素的 `DocumentationAttribute` 展开。
该特性通过 `AttributeUsage` 被限制在特定目标上:方法、属性、构造函数和类。
它包含一个通过构造函数传入的 `Description` 属性。

```csharp
[AttributeUsage(
    AttributeTargets.Method |
    AttributeTargets.Property |
    AttributeTargets.Constructor |
    AttributeTargets.Class)]
public class DocumentationAttribute : Attribute
{
    public DocumentationAttribute(string description)
        => Description = description;

    public string Description { get; }
}

[Documentation("This is a type.")]
public sealed class MyType
{
    [Documentation("This is a constructor.")]
    public MyType()
    {
    }

    [Documentation("This is a property.")]
    public string MyProperty { get; set; }

    [Documentation("This is a method.")]
    public void MyMethod()
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131047/?t=310)

应用程序首先提示用户输入程序集路径。
如果输入为空,则默认使用 `Assembly.GetExecutingAssembly()` 获取当前正在执行的程序集。
否则,它会使用 `Assembly.LoadFrom(assemblyPath)` 加载指定的程序集。

```csharp
using System.Reflection;

Console.WriteLine(
    "Enter the path to the assembly or leave empty for " +
    "the currently executing assembly.");
var assemblyPath = Console.ReadLine();
var assembly = string.IsNullOrWhiteSpace(assemblyPath)
    ? Assembly.GetExecutingAssembly()
    : Assembly.LoadFrom(assemblyPath);

Console.WriteLine($"Assembly: {assembly.FullName}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131047/?t=130)

核心逻辑使用 `assembly.GetTypes()` 遍历程序集中的每一个类型。
对于每个类型,应用程序都会检查是否带有 `DocumentationAttribute`。
为了管理输出格式,使用了 `WriteTypeHeaderIfNeeded` 和 `WriteMembersHeaderIfNeeded` 这样的本地方法,确保每个类型或每组成员的标题只打印一次。

在类型循环内部,代码使用 `GetMembers()` 并配合 `BindingFlags.Public`、`BindingFlags.Instance` 和 `BindingFlags.Static` 来检查成员。
这会把搜索范围限制在从外部可见的公共成员上,不过如果需要,也可以扩展为包含 `NonPublic` 成员。
成员按其 `MemberType` 排序,以使报告更整洁。

```csharp
foreach (var type in assembly.GetTypes())
{
    bool writtenTypeHeader = false;
    void WriteTypeHeaderIfNeeded()
    {
        if (writtenTypeHeader)
        {
            return;
        }

        Console.WriteLine($"Type");
        Console.WriteLine($"  Name: {type.FullName}");
        writtenTypeHeader = true;
    }

    var typeDocumentation = type.GetCustomAttribute<DocumentationAttribute>();
    if (typeDocumentation != null)
    {
        WriteTypeHeaderIfNeeded();
        Console.WriteLine($"  Description: {typeDocumentation.Description}");
    }

    bool writtenMembersHeader = false;
    void WriteMembersHeaderIfNeeded(MemberInfo member)
    {
        if (writtenMembersHeader)
        {
            return;
        }

        WriteTypeHeaderIfNeeded();

        Console.WriteLine($"  Members:");
        writtenMembersHeader = true;
    }

    foreach (var member in type
        .GetMembers(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.Static)
        .OrderBy(x => x.MemberType))
    {
        var memberDocumentation = member.GetCustomAttribute<DocumentationAttribute>();
        if (memberDocumentation != null)
        {
            WriteMembersHeaderIfNeeded(member);

            Console.WriteLine($"        Type: {member.MemberType}");
            Console.WriteLine($"        Name: {member.Name}");
            Console.WriteLine($"        Description: {memberDocumentation.Description}");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131047/?t=445)

```text
foreach type in assembly.GetTypes()
  |
  +-- type.GetCustomAttribute<DocumentationAttribute>()
  |     != null -> WriteTypeHeaderIfNeeded()   (Type / Name)
  |                Description
  |
  +-- foreach member in type.GetMembers(Public | Instance | Static)
                          .OrderBy(x => x.MemberType)
        |
        +-- member.GetCustomAttribute<DocumentationAttribute>()
              != null -> WriteMembersHeaderIfNeeded(member)  (Members:)
                         Type / Name / Description
```

当应用程序针对示例 `MyType` 运行时,它会识别出类级别的文档,然后遍历其成员。
注意,构造函数是通过生成的名称 `.ctor` 来识别的。
这种模式可以扩展到为参数生成文档:遍历方法和构造函数的 `ParameterInfo` 数组,并检查其中的特性。

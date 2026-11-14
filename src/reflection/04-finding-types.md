# Finding Types

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 4 章
> 共 4 课 · 约 46:45
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Looking Up Types](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/) | 8:04 | [↓](#1-looking-up-types) |
| 2 | [Filtering Type Information](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/filtering-type-information-58130584/) | 12:03 | [↓](#2-filtering-type-information) |
| 3 | [Assembly Scanning](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/) | 10:50 | [↓](#3-assembly-scanning) |
| 4 | [Example Application](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/) | 15:48 | [↓](#4-example-application) |

## 1. Looking Up Types

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/) · 8:04

### 总结

本课探讨在 .NET 中获取类型信息的各种技术,这是所有基于反射的操作的基础。
它涵盖了使用 typeof 关键字的编译时查找,以及使用 Type.GetType 或程序集特定方法的运行时查找。
本课还演示了如何处理命名空间、嵌套类型和私有类型,并强调反射可以访问那些原本受标准访问修饰符限制的元数据。

### 核心概念

* **编译时查找**:当类型在开发时已知时,使用 `typeof` 运算符获取一个 `Type` 实例。
* **运行时查找**:使用 `Type.GetType(string)` 按名称查找类型,这对于类型在编译时未知的动态场景至关重要。
* **嵌套类型**:使用 `+` 分隔符语法访问定义在其他类型内部的类型。
* **命名空间限定**:通过使用类型的完整命名空间限定名称,来区分同名的类型。
* **程序集级查找**:使用 `Assembly.GetType` 和 `Assembly.GetTypes` 从特定程序集中获取指定类型或所有类型。
* **访问修饰符透明性**:反射能够定位和检查类型,而不论其访问级别如何(例如 `internal` 或 `private`)。

### 课程笔记

要用反射执行任何操作,你必须先获得一个 `Type` 实例。
这个实例包含检查或操作该类型所需的元数据。

在下面的示例中,我们使用一组简单的类来演示查找技术。
其中包括顶层类、internal 类、嵌套类以及位于特定命名空间中的类。

```csharp
internal sealed class MyTypeB
{
}

public sealed class MyTypeC
{
    public sealed class MyTypeD
    {
    }

    private sealed class MyTypeE
    {
    }
}

namespace CoolNamespace
{
    public sealed class MyTypeA
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=100)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
public sealed class MyTypeA
{
}

internal sealed class MyTypeB
{
}

public sealed class MyTypeC
{
    public sealed class MyTypeD
    {
    }

    private sealed class MyTypeE
    {
    }
}

namespace CoolNamespace
{
    public sealed class MyTypeA
    {
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=85)

#### 编译时查找

获取类型信息最直接的方式是使用 `typeof` 关键字。
这是一个编译时操作,意味着编译器在构建时必须拥有对该类型的引用。
它不适用于在运行时动态加载外部 DLL 的场景。

```csharp
using System.Reflection;

Console.WriteLine("Type by typeof:");
var typeofA = typeof(MyTypeA);
var typeofB = typeof(MyTypeB);
Console.WriteLine(typeofA.AssemblyQualifiedName);
Console.WriteLine(typeofB.AssemblyQualifiedName);
Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=160)

#### 按名称进行运行时查找

当类型在编译时未知时,你可以使用 `Type.GetType()`。
该方法接受一个表示类型名称的字符串。
对于嵌套类型,.NET 使用加号(`+`)来表示父类型与嵌套类型之间的关系。
值得注意的是,反射可以找到 `private` 嵌套类型,而这些类型对应用程序的其余部分来说原本是不可访问的。

```csharp
Console.WriteLine("Type by name:");
var typeCByName = Type.GetType("MyTypeC");
var typeDByName = Type.GetType("MyTypeC+MyTypeD");
var typeEByName = Type.GetType("MyTypeC+MyTypeE");
Console.WriteLine(typeCByName.AssemblyQualifiedName);
Console.WriteLine(typeDByName.AssemblyQualifiedName);
Console.WriteLine(typeEByName.AssemblyQualifiedName);
Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=205)

```text
MyTypeC                      Type.GetType("MyTypeC")
 |-- MyTypeD   (public)      Type.GetType("MyTypeC+MyTypeD")
 +-- MyTypeE   (private)     Type.GetType("MyTypeC+MyTypeE")
```

#### 处理命名空间

如果多个类型同名但位于不同的命名空间中,你必须向 `Type.GetType()` 提供命名空间限定的名称,以确保返回正确的类型。

```csharp
Console.WriteLine("Type by name vs namespace:");
var typeAByName = Type.GetType("MyTypeA");
var typeAFromCoolNamespace = Type.GetType("CoolNamespace.MyTypeA");
Console.WriteLine(typeAByName.AssemblyQualifiedName);
Console.WriteLine(typeAFromCoolNamespace.AssemblyQualifiedName);
Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=295)

#### 程序集特定查找

你也可以查询某个特定程序集中的类型。
这在处理动态加载的程序集时尤其有用。
你可以按名称请求一个特定类型,也可以获取该程序集中定义的所有类型组成的数组。

```csharp
Console.WriteLine("Type from assembly:");
var typeAFromAssembly = Assembly.GetExecutingAssembly().GetType("MyTypeA");
Console.WriteLine(typeAFromAssembly.AssemblyQualifiedName);
Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=325)

要获取程序集中定义的每一个类型,请使用 `GetTypes()` 方法。
它会返回所有顶层类型和嵌套类型,而不论其访问修饰符如何。

```csharp
Console.WriteLine("All types from assembly:");
var allTypesFromAssembly = Assembly.GetExecutingAssembly().GetTypes();
foreach (var type in allTypesFromAssembly)
{
    Console.WriteLine(type.FullName);
}

Console.ReadLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/looking-up-types-58130588/?t=310)

## 2. Filtering Type Information

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/filtering-type-information-58130584/) · 12:03

### 总结

本课介绍在 .NET 程序集中使用反射过滤类型信息的技术。
它演示了如何使用 Type 类的属性来识别特定的类型类别,例如抽象类、密封类和接口。
本课很大一部分内容用于讲解类型层次结构检查,特别是解释为什么在处理 Type 元数据对象时,需要使用 IsAssignableTo 方法而不是标准的 is 运算符。

### 核心概念

- 使用 `Assembly.GetTypes()` 从程序集中获取所有类型。
- 使用元数据属性过滤类型:`IsAbstract`、`IsSealed` 和 `IsInterface`。
- 理解在 .NET 元数据中接口被标记为 `IsAbstract`。
- 使用 `IsAssignableTo` 在 `Type` 层面检查继承和接口实现。
- 区分实例级类型检查(`is` 运算符)与类型级元数据比较。

### 课程笔记

要开始过滤类型,你首先需要从程序集中获得一个类型集合。
这通常通过在 `Assembly` 实例上调用 `GetTypes()` 来完成。
在这些示例中,我们使用一组在继承和实现细节上各不相同的简单类型。

```csharp
public abstract class MyBaseType
{

}

public sealed class MyDerivedTypeA :
    MyBaseType
{

}

public sealed class MyDerivedTypeB :
    MyBaseType,
    IMyInterface
{

}

public interface IMyInterface
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/filtering-type-information-58130584/?t=70)

```text
          MyBaseType (abstract)        IMyInterface (interface)
            ^           ^                    ^
            |           |                    |
  MyDerivedTypeA     MyDerivedTypeB ---------+
    (sealed)           (sealed)
```

#### 按元数据标志过滤

`Type` 类提供了若干布尔属性,让你可以根据类型的定义来过滤类型。
借助 LINQ 或标准循环,你可以筛选出满足特定条件的类型。

```csharp
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var allTypes = assembly.GetTypes();

Console.WriteLine("All abstract types:");
foreach (var type in allTypes.Where(x => x.IsAbstract))
{
    Console.WriteLine(type.Name);
}
Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/filtering-type-information-58130584/?t=40)

按 `IsAbstract` 过滤时,需要注意抽象类(如 `MyBaseType`)和接口(如 `IMyInterface`)都会被返回。
在 .NET 元数据中,接口在技术上被标记为抽象,因为它们不能被直接实例化。
如果你只需要查找接口,请使用 `IsInterface` 属性。

相反,`IsSealed` 用于识别不能被扩展的类型。
运行这个过滤器时,除了你自己的密封类之外,你还可能看到编译器生成的类型(例如以 `<>c` 开头的类型),因为编译器通常会把它的内部辅助类型标记为 sealed。

```csharp
Console.WriteLine("All sealed types:");
foreach (var type in allTypes.Where(x => x.IsSealed))
{
    Console.WriteLine(type.Name);
}
Console.WriteLine();

Console.WriteLine("All interfaces:");
foreach (var type in allTypes.Where(x => x.IsInterface))
{
    Console.WriteLine(type.Name);
}
Console.WriteLine();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/filtering-type-information-58130584/?t=220)

#### 类型层次结构与可赋值性

一个常见需求是查找实现了特定接口或派生自特定基类的类型。
虽然 `is` 运算符用于实例级检查(例如 `myInstance is MyBaseType`),但它不能用于比较两个 `Type` 对象。

如果你有一个类型为 `System.Type` 的变量 `typeA`,那么表达式 `typeA is MyBaseType` 将始终为 false。
这是因为 `typeA` 是 `Type` 类的实例,而不是 `MyBaseType` 的实例。
要使用反射执行这种检查,你必须使用 `IsAssignableTo` 方法。

```csharp
Type typeA = typeof(MyDerivedTypeA);
var a = new MyDerivedTypeA();

// Instance check
bool isInstanceOf = a is MyBaseType; // True

// Reflection check
var canAssignToBaseA = typeA.IsAssignableTo(typeof(MyBaseType));
var canAssignToInterfaceA = typeA.IsAssignableTo(typeof(IMyInterface));

Type typeB = typeof(MyDerivedTypeB);
var canAssignToBaseB = typeB.IsAssignableTo(typeof(MyBaseType));
var canAssignToInterfaceB = typeB.IsAssignableTo(typeof(IMyInterface));

Console.WriteLine("Can MyDerivedTypeA be assigned to MyBaseType? " + canAssignToBaseA);
Console.WriteLine("Can MyDerivedTypeA be assigned to IMyInterface? " + canAssignToInterfaceA);
Console.WriteLine("Can MyDerivedTypeB be assigned to MyBaseType? " + canAssignToBaseB);
Console.WriteLine("Can MyDerivedTypeB be assigned to IMyInterface? " + canAssignToInterfaceB);

Console.WriteLine("All types that implement IMyInterface:");
foreach (var type in allTypes.Where(x => x.IsAssignableTo(typeof(IMyInterface))))
{
    Console.WriteLine(type.Name);
}
Console.WriteLine();

Console.WriteLine("All types that derive from MyBaseType:");
foreach (var type in allTypes.Where(x => x.IsAssignableTo(typeof(MyBaseType))))
{
    Console.WriteLine(type.Name);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/filtering-type-information-58130584/?t=610)

如果当前类型与目标类型相同,或者它位于目标类型的继承层次结构中(包括接口实现),`IsAssignableTo` 就返回 true。
这使它成为在程序集中扫描插件、处理程序或某个基础契约的特定实现时的主要工具。

## 3. Assembly Scanning

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/) · 10:50

### 总结

程序集扫描让应用程序可以在运行时从多个程序集中动态发现并加载类型信息。
通过遍历目录并加载 DLL,开发者无需显式的编译时引用就能检查跨多个项目和依赖项的类型,这对于构建插件架构这类可扩展系统至关重要。

### 核心概念

- 通过 `Assembly.LoadFrom` 动态加载程序集。
- 使用 `Directory.EnumerateFiles` 进行基于目录的扫描。
- 按文件名模式过滤程序集。
- 处理非托管或已损坏 DLL 引发的 `BadImageFormatException`。
- 访问多个已加载程序集中的类型。

### 课程笔记

反射的基础用例是使用 `Assembly.GetExecutingAssembly()` 查看当前程序集。
这样可以访问当前正在执行的项目中定义的所有类型。
要扫描一个目录,请使用 `Directory.EnumerateFiles` 查找扩展名为 `.dll` 的文件,并将它们传给 `Assembly.LoadFrom(file)`,把它们加载到当前应用程序域中。

```csharp
using System.Reflection;

// this is how we'd look at a single assembly
var assembly = Assembly.GetExecutingAssembly();
var types = assembly.GetTypes();

// how do we check an entire directory though?
static IReadOnlyList<Assembly> LoadAssembliesFromDirectory(string directoryPath)
{
    var assemblies = new List<Assembly>();
    foreach (var file in Directory.EnumerateFiles(directoryPath, "*.dll"))
    {
        var assembly = Assembly.LoadFrom(file);
        assemblies.Add(assembly);
    }

    return assemblies;
}

var assemblies = LoadAssembliesFromDirectory(".");
foreach (var asm in assemblies)
{
    Console.WriteLine(asm.FullName);

    foreach (var type in asm.GetTypes())
    {
        Console.WriteLine($"\t{type.FullName}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/?t=70)

```text
Directory.EnumerateFiles(".", "*.dll")
        |
        v  (each file)
Assembly.LoadFrom(file) --> List<Assembly>
        |
        v  (each asm)
asm.GetTypes() --> type.FullName
```

为了演示跨多个程序集的扫描,设想一个包含额外项目的解决方案,例如 `FindingTypes.ExtraTypes`。
构建该项目时,它的 DLL 会被复制到主应用程序的 bin 目录中。
随后就可以用反射发现它的类型,包括嵌套类。
在反射输出中,嵌套类型在其 `FullName` 属性中以 `+` 符号标识。

```csharp
namespace FindingTypes.ExtraTypes;

public sealed class PublicClassWithNoMembers
{
}

internal sealed class InternalClassWithNoMembers
{
}

public sealed class PublicClassWithNestedClass
{
    public sealed class NestedPublicClass
    {
    }

    internal sealed class NestedInternalClass
    {
    }

    private sealed class NestedPrivateClass
    {
    }
}

internal sealed class InternalClassWithNestedClass
{
    public sealed class NestedPublicClass
    {
    }

    internal sealed class NestedInternalClass
    {
    }

    private sealed class NestedPrivateClass
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/?t=220)

可以通过对目录搜索应用过滤器来细化扫描。
你可以针对特定的命名模式,而不是加载每一个 `.dll` 文件。
这对于隔离特定模块或插件、避免加载不必要的依赖项很有用。

```csharp
static IReadOnlyList<Assembly> LoadAssembliesFromDirectory2(
    string directoryPath,
    string filter)
{
    var assemblies = new List<Assembly>();
    foreach (var file in Directory.EnumerateFiles(directoryPath, filter))
    {
        var assembly = Assembly.LoadFrom(file);
        assemblies.Add(assembly);
    }

    return assemblies;
}

var assemblies = LoadAssembliesFromDirectory2(
    ".",
    "*ExtraTypes*.dll");
foreach (var asm in assemblies)
{
    Console.WriteLine(asm.FullName);

    foreach (var type in asm.GetTypes())
    {
        Console.WriteLine($"\t{type.FullName}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/?t=415)

程序集扫描的一个关键考量是错误处理。
如果目录中包含非托管 DLL(例如原生 C++ 库)或已损坏的文件,`Assembly.LoadFrom` 会抛出 `BadImageFormatException`。
为确保应用程序保持稳定,加载逻辑应包裹在 `try-catch` 块中,以便优雅地记录或处理失败,而不会中断对其他有效程序集的扫描。

```csharp
static IReadOnlyList<Assembly> LoadAssembliesFromDirectory3(
    string directoryPath,
    string filter)
{
    var assemblies = new List<Assembly>();
    foreach (var file in Directory.EnumerateFiles(directoryPath, filter))
    {
        try
        {
            var assembly = Assembly.LoadFrom(file);
            assemblies.Add(assembly);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load assembly from {file}: {ex.Message}");
        }
    }

    return assemblies;
}

var assemblies = LoadAssembliesFromDirectory3(
    ".",
    "*.dll");
foreach (var asm in assemblies)
{
    Console.WriteLine(asm.FullName);

    foreach (var type in asm.GetTypes())
    {
        Console.WriteLine($"\t{type.FullName}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/?t=595)

## 4. Example Application

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/) · 15:48

### 总结

本课演示如何创建一个 "snoop" 应用程序,让用户可以通过扫描目录或特定文件来检查 .NET 程序集。
该应用程序利用反射加载程序集、获取类型信息,并应用各种过滤器(例如命名空间、类型名称和可见性),帮助开发者探索未知二进制文件的内容,并理解 IsPublic 等反射属性在顶层类型与嵌套类型上的不同表现。

### 核心概念

* **程序集扫描**:使用 `Assembly.LoadFrom` 从一个目录或特定文件路径动态加载所有 DLL 文件。
* **类型获取**:使用 `assembly.GetTypes()` 提取已加载程序集中定义的所有类型。
* **基于谓词的过滤**:实现一个灵活的过滤系统,由用户输入生成一个 `Predicate<Type>` 来评估类型元数据。
* **类型元数据检查**:访问 `type.Namespace`、`type.FullName` 和 `type.IsPublic` 等属性。
* **可见性细节**:理解 `IsPublic` 只适用于顶层类型,而嵌套类型需要检查 `IsNestedPublic` 之类的属性。

### 课程笔记

应用程序首先提示用户输入目标路径,它既可以是某个特定的程序集文件,也可以是包含多个程序集的目录。

```csharp
using System.Reflection;

public sealed class FindingTypesExampleApplication
{
    public static void Run()
    {
        string? targetPath;
        while (true)
        {
            Console.WriteLine("Enter the target path to scan either:");
            Console.WriteLine("\tThe single target assembly file");
            Console.WriteLine("\tAll assemblies in the directory");

            targetPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                Console.WriteLine("Invalid target path!");
                continue;
            }

            break;
        }

        var assemblies = Directory.Exists(targetPath)
            ? LoadAssembliesFromDirectory(targetPath)
            : LoadAssembliesFromFile(targetPath);

        Console.WriteLine($"Examining {assemblies.Count} assemblies...");
        Dictionary<Assembly, IReadOnlyList<Type>> typesByAssembly = new(assemblies.Count);
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            typesByAssembly[assembly] = types;
        }

        var totalTypes = typesByAssembly.Values.Sum(types => types.Count);
        Console.WriteLine($"There are {totalTypes} types.");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=70)

为了确保应用程序在程序集扫描过程中保持健壮,它实现了安全的加载逻辑。
扫描目录时,应用程序会枚举所有 `.dll` 文件并尝试逐个加载,同时捕获任何异常(例如格式无效),以防止整个过程失败。

```csharp
private static IReadOnlyList<Assembly> LoadAssembliesFromDirectory(string directoryPath)
{
    var assemblies = new List<Assembly>();
    foreach (var file in Directory.EnumerateFiles(directoryPath, "*.dll"))
    {
        try
        {
            var assembly = Assembly.LoadFrom(file);
            assemblies.Add(assembly);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load assembly from {file}: {ex.Message}");
        }
    }

    return assemblies;
}

private static IReadOnlyList<Assembly> LoadAssembliesFromFile(string filePath)
{
    try
    {
        var assembly = Assembly.LoadFrom(filePath);
        return [assembly];
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to load assembly from {filePath}: {ex.Message}");
        return [];
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=145)

程序集加载完毕、类型被收集到一个字典中之后,应用程序进入一个循环,用户可以在其中选择并应用不同的过滤器。
随后会显示结果,列出程序集名称以及符合条件的类型的完整名称。

```csharp
while (true)
{
    var filter = AskForFilter();
    if (filter is null)
    {
        Console.WriteLine("Invalid selection!");
        continue;
    }

    foreach (var kvp in typesByAssembly)
    {
        var resultsPerAssembly = kvp.Value.Where(x => filter(x)).ToArray();
        if (resultsPerAssembly.Length == 0)
        {
            continue;
        }

        Console.WriteLine($"Assembly: {kvp.Key.FullName}");
        foreach (var type in resultsPerAssembly)
        {
            Console.WriteLine($"\tType: {type.FullName}");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=295)

`AskForFilter` 方法使用 switch 表达式把用户输入映射到特定的过滤器生成方法。
如果输入与任何已知的过滤器类型都不匹配,它就返回 `null`。

```csharp
private static Predicate<Type>? AskForFilter()
{
    Console.WriteLine("Enter the filter type:");
    Console.WriteLine("\tNamespace");
    Console.WriteLine("\tType Name");
    Console.WriteLine("\tVisibility");

    var filterType = Console.ReadLine();

    var filter = filterType switch
    {
        "Namespace" => AskForNamespaceFilter(),
        "Type Name" => AskForTypeNameFilter(),
        "Visibility" => AskForVisibilityFilter(),
        _ => null
    };

    return filter;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=310)

```text
AskForFilter()
   |-- "Namespace"  --> AskForNamespaceFilter()  --+
   |-- "Type Name"  --> AskForTypeNameFilter()   --+--> Predicate<Type>?
   |-- "Visibility" --> AskForVisibilityFilter() --+
   +-- _            --> null
```

#### 命名空间过滤

命名空间过滤器让用户可以根据命名空间字符串来匹配类型。
它支持空命名空间,并执行不区分大小写的 "contains" 检查。

```csharp
private static Predicate<Type>? AskForNamespaceFilter()
{
    Console.WriteLine("Enter the part of the namespace to match against:");
    var namespaceFilter = Console.ReadLine();

    // NOTE: we can allow empty namespace :)
    return type =>
        (string.IsNullOrEmpty(type.Namespace) && string.IsNullOrEmpty(namespaceFilter)) ||
        (!string.IsNullOrEmpty(namespaceFilter) && type.Namespace?.Contains(namespaceFilter, StringComparison.OrdinalIgnoreCase) == true);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=865)

#### 类型名称过滤

类型名称过滤器针对类型的 `FullName` 进行匹配。
如果用户输入为空,过滤器默认匹配所有类型。

```csharp
private static Predicate<Type>? AskForTypeNameFilter()
{
    Console.WriteLine("Enter the string to match against the full type name:");
    var typeNameFilter = Console.ReadLine();

    // allow match-all
    if (string.IsNullOrEmpty(typeNameFilter))
    {
        return type => true;
    }

    return type => type.FullName?.Contains(typeNameFilter, StringComparison.OrdinalIgnoreCase) == true;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=535)

#### 可见性过滤

可见性过滤检查一个类型是 public 还是非 public。
需要注意的是,`IsPublic` 属性只对顶层 public 类型返回 `true`。
嵌套类型即使声明为 `public`,其 `IsPublic` 也会返回 `false`,而需要改为检查 `IsNestedPublic`。

```csharp
private static Predicate<Type>? AskForVisibilityFilter()
{
    Console.WriteLine("Enter the visibility:");
    Console.WriteLine("\tPublic");
    Console.WriteLine("\tNot Public");

    var visibilityFilter = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(visibilityFilter))
    {
        return null;
    }

    return visibilityFilter switch
    {
        "Public" => type => type.IsPublic,
        "Not Public" => type => !type.IsPublic,
        _ => null
    };
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58130587/?t=565)

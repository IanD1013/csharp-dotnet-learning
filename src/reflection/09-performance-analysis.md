# Performance Analysis

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 9 章
> 共 3 课 · 约 38:27
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Type Discovery](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/type-discovery-58131031/) | 7:38 | [↓](#1-type-discovery) |
| 2 | [Invoking Members](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/) | 15:01 | [↓](#2-invoking-members) |
| 3 | [Construction](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/) | 15:48 | [↓](#3-construction) |

## 1. Type Discovery

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/type-discovery-58131031/) · 7:38

### 总结

本课使用 BenchmarkDotNet 探讨 .NET 中类型发现的性能影响。
它比较了从磁盘加载程序集、扫描已缓存程序集中的类型、以及使用编译期类型解析这几种方式的开销。
结果表明,虽然反射比直接访问类型慢得多,但缓存程序集引用可以抵消相当大一部分性能损失,把相对基线的开销从大约 122 倍降到 35 倍。

### 核心概念

* **BenchmarkDotNet**:衡量 .NET 代码性能的业界标准库。
* **程序集加载开销(Assembly Loading Overhead)**:与使用已有引用相比,`Assembly.LoadFile` 带来的显著性能和内存开销。
* **类型扫描(Type Scanning)**:在运行时使用 `Assembly.GetTypes()` 发现类型与编译期解析相比的开销。
* **缓存策略(Caching Strategies)**:通过把程序集引用保存在字段中而不是重新加载,来提升反射性能。
* **InternalsVisibleToAttribute**:让基准测试能够访问 internal 类型,以便进行测试和性能分析。

### 课程笔记

反射常常因其性能特征而受到批评。
虽然它能实现一些原本不可能的功能,比如绕过访问修饰符或动态加载类型,但理解它具体引入了哪些开销很重要。
为了衡量这一点,我们在一个控制台应用程序中使用 `BenchmarkDotNet`。

#### 项目配置

基准测试项目需要引用 `BenchmarkDotNet` NuGet 包,以及包含待发现类型的项目。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="BenchmarkDotNet" Version="0.14.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\FindingTypes.ExtraTypes\FindingTypes.ExtraTypes.csproj" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/type-discovery-58131031/?t=85)

#### 对类型发现进行基准测试

这些基准测试比较了获取一组类型的三种不同方式。
使用 `[MemoryDiagnoser]` 特性来跟踪内存分配,并应用 `[ShortRunJob]` 以便在开发期间更快地执行。

1.  **LoadFileAndGetTypes**:从磁盘加载一个外部程序集并扫描其中的类型。
    它代表了最高的开销。
2.  **CachedAssemblyGetTypes**:使用预先加载的程序集引用(在 `[GlobalSetup]` 中初始化)来扫描类型。
    这把 `GetTypes()` 的开销与磁盘 I/O 的开销隔离开来。
3.  **TypeOfGetTypes(基线)**:使用编译期的 `typeof` 运算符创建一个由已知类型组成的数组。
    这是最快的、不使用反射的方式。

```csharp
using BenchmarkDotNet.Attributes;
using FindingTypes.ExtraTypes;
using System.Reflection;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
[ShortRunJob]
public class TypeDiscoveryBenchmarks
{
    private Assembly? _extraTypesAssembly;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _extraTypesAssembly = Assembly.GetAssembly(typeof(PublicClassWithNoMembers));
    }

    [Benchmark]
    public Type[] LoadFileAndGetTypes()
    {
        var assembly = Assembly.LoadFile(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "FindingTypes.ExtraTypes.dll"));
        return assembly.GetTypes();
    }

    [Benchmark]
    public Type[] CachedAssemblyGetTypes()
    {
        return _extraTypesAssembly!.GetTypes();
    }

    [Benchmark(Baseline = true)]
    public Type[] TypeOfGetTypes()
    {
        return
        [
            typeof(PublicClassWithNoMembers),
            typeof(InternalClassWithNoMembers),
            typeof(PublicClassWithNestedClass),
            typeof(InternalClassWithNestedClass),
        ];
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/type-discovery-58131031/?t=130)

```text
LoadFileAndGetTypes     磁盘 "FindingTypes.ExtraTypes.dll"
                          --Assembly.LoadFile--> Assembly --GetTypes()--> Type[]

CachedAssemblyGetTypes  [GlobalSetup] _extraTypesAssembly
                          --GetTypes()--> Type[]

TypeOfGetTypes          typeof(...) x 4 (编译期)  --> Type[]   (Baseline)
```

以下是通过 `search_code` 恢复的屏幕代码:

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

[▶ 观看(Assembly Scanning)](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/assembly-scanning-58130586/?t=220)

#### 访问 internal 类型

为了让基准测试项目能够引用目标程序集中的 internal 类型(比如 `InternalClassWithNoMembers`),目标项目必须在其项目文件或程序集特性中包含 `InternalsVisibleToAttribute`。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleToAttribute">
      <_Parameter1>Benchmarks.ConsoleApp</_Parameter1>
    </AssemblyAttribute>
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/type-discovery-58131031/?t=190)

#### 执行

基准测试通过 `BenchmarkSwitcher` 来执行,它提供了一个命令行界面,用于选择并运行特定的基准测试类。

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/type-discovery-58131031/?t=250)

#### 性能分析

*   **LoadFileAndGetTypes**:这是最慢的方法,大约比基线慢 122 倍。
    它会带来显著的性能损失和更高的内存分配(约 536 字节),因为每次迭代都必须从磁盘加载程序集。
*   **CachedAssemblyGetTypes**:通过缓存程序集引用,执行时间从约 930 纳秒降到约 270 纳秒。
    虽然仍比基线慢大约 35 倍,但它比从磁盘加载要高效得多。
*   **TypeOfGetTypes**:基线方法的执行时间约为 7.5 纳秒,内存分配极少(数组占 56 字节)。

最主要的结论是:如果反射无法避免,就必须缓存程序集引用。
如果类型在编译期已知,就完全避免反射,改用 `typeof` 运算符。

## 2. Invoking Members

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/) · 15:01

### 总结

本课对 .NET 中的成员调用进行了详细的性能分析,比较了直接访问与通过反射访问字段、属性和方法。
它使用 BenchmarkDotNet 展示了反射引入的显著开销,尤其是在元数据查找没有被缓存的情况下。
本课涵盖了调用实例成员和静态成员的机制、缓存 FieldInfo、PropertyInfo 和 MethodInfo 带来的性能收益,以及基于反射的访问所对应的内存分配模式。
结果表明,虽然反射比直接访问慢好几个数量级,但在长时间运行的操作中,这一开销可能可以忽略不计。

### 核心概念

* 直接访问成员与基于反射调用之间的性能差距。
* 缓存元数据对象(FieldInfo、PropertyInfo、MethodInfo)对于减轻反射开销的关键作用。
* 调用静态成员的语法要求,特别是为实例参数传入 null。
* 使用反射访问字段和属性时的内存分配开销(GC 压力)。
* 解读 BenchmarkDotNet 的结果,包括平均执行时间以及相对基线的比率。

### 课程笔记

为了分析成员调用的性能,我们使用一个包含各种 public 成员的目标类。
需要 public 访问权限,才能建立一个直接访问的基线,用来与反射进行比较。

```csharp
using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace Benchmarks.ConsoleApp;

public sealed class ClassToInvoke
{
    public static int StaticPublicField;

    public int PublicField;

    public static int StaticPublicProperty { get; set; }

    public int PublicProperty { get; set; }

    public static void StaticPublicMethod()
    {
    }

    public void PublicMethod()
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=40)

#### 字段访问性能

通过反射访问字段时,我们比较三种场景:直接访问(基线)、使用缓存的 `FieldInfo` 对象进行反射,以及每次调用时都查找 `FieldInfo` 的反射。
在 `[GlobalSetup]` 方法中缓存元数据可以避免重复查找,从而显著提升性能。

```csharp
public class MemberInvocation_GetInstanceFieldBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _fieldInfo = _targetType.GetField("PublicField");
    }

    [Benchmark(Baseline = true)]
    public int GetField_NoReflection()
    {
        return _instance!.PublicField;
    }

    [Benchmark]
    public int GetField_CachedFieldInfo()
    {
        return (int)_fieldInfo!.GetValue(_instance)!;
    }

    [Benchmark]
    public int GetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("PublicField");
        return (int)fieldInfo!.GetValue(_instance)!;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=130)

```text
GetField_NoReflection     _instance.PublicField                   (Baseline)
GetField_CachedFieldInfo  [GlobalSetup] _fieldInfo --GetValue--> int
GetField_FieldInfo        GetField("PublicField") --GetValue--> int
                          (每次调用都查找)
```

对于静态字段,`GetValue` 方法需要传入 `null` 作为参数,因为没有可供操作的实例。

```csharp
public class MemberInvocation_GetStaticFieldBenchmarks
{
    private Type? _targetType;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _fieldInfo = _targetType.GetField("StaticPublicField");
    }

    [Benchmark(Baseline = true)]
    public int GetField_NoReflection()
    {
        return ClassToInvoke.StaticPublicField;
    }

    [Benchmark]
    public int GetField_CachedFieldInfo()
    {
        return (int)_fieldInfo!.GetValue(null)!;
    }

    [Benchmark]
    public int GetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("StaticPublicField");
        return (int)fieldInfo!.GetValue(null)!;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=205)

#### 属性访问性能

在这些基准测试中,属性的表现与字段类似,尽管从技术上讲,属性是 getter 和 setter 方法的语法糖。
`PropertyInfo` 的用法遵循与 `FieldInfo` 相同的模式。

```csharp
public class MemberInvocation_GetInstancePropertyBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private PropertyInfo? _propertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _propertyInfo = _targetType.GetProperty("PublicProperty");
    }

    [Benchmark(Baseline = true)]
    public int GetProperty_NoReflection()
    {
        return _instance!.PublicProperty;
    }

    [Benchmark]
    public int GetProperty_CachedPropertyInfo()
    {
        return (int)_propertyInfo!.GetValue(_instance)!;
    }

    [Benchmark]
    public int GetProperty_PropertyInfo()
    {
        var propertyInfo = _targetType!.GetProperty("PublicProperty");
        return (int)propertyInfo!.GetValue(_instance)!;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=220)

静态属性同样需要向 `GetValue` 或 `SetValue` 传入 `null`。

```csharp
public class MemberInvocation_GetStaticPropertyBenchmarks
{
    private Type? _targetType;
    private PropertyInfo? _propertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _propertyInfo = _targetType.GetProperty("StaticPublicProperty");
    }

    [Benchmark(Baseline = true)]
    public int GetProperty_NoReflection()
    {
        return ClassToInvoke.StaticPublicProperty;
    }

    [Benchmark]
    public int GetProperty_CachedPropertyInfo()
    {
        return (int)_propertyInfo!.GetValue(null)!;
    }

    [Benchmark]
    public int GetProperty_PropertyInfo()
    {
        var propertyInfo = _targetType!.GetProperty("StaticPublicProperty");
        return (int)propertyInfo!.GetValue(null)!;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=235)

#### 设置成员的值

为字段和属性设置值,与获取值遵循相同的性能模式。
在这些基准测试中,给成员赋的是一个常量值(123456)。

```csharp
public class MemberInvocation_SetInstanceFieldBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _fieldInfo = _targetType.GetField("PublicField");
    }

    [Benchmark(Baseline = true)]
    public void SetField_NoReflection()
    {
        _instance!.PublicField = 123456;
    }

    [Benchmark]
    public void SetField_CachedFieldInfo()
    {
        _fieldInfo!.SetValue(_instance, 123456);
    }

    [Benchmark]
    public void SetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("PublicField")!;
        fieldInfo!.SetValue(_instance, 123456);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=265)

#### 方法调用性能

通过 `MethodInfo.Invoke` 调用方法比直接调用慢得多。
当方法没有参数时,向 `Invoke` 的第二个参数传入 `null`。

```csharp
public class MemberInvocation_CallInstanceMethodBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private MethodInfo? _methodInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _methodInfo = _targetType.GetMethod("PublicMethod");
    }

    [Benchmark(Baseline = true)]
    public void CallMethod_NoReflection()
    {
        _instance!.PublicMethod();
    }

    [Benchmark]
    public void CallMethod_CachedMethodInfo()
    {
        _methodInfo!.Invoke(_instance, null);
    }

    [Benchmark]
    public void CallMethod_MethodInfo()
    {
        var methodInfo = _targetType!.GetMethod("PublicMethod")!;
        methodInfo.Invoke(_instance, null);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=310)

调用静态方法遵循相同的模式,为实例参数传入 `null`。

```csharp
public class MemberInvocation_CallStaticMethodBenchmarks
{
    private Type? _targetType;
    private MethodInfo? _methodInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _methodInfo = _targetType.GetMethod("StaticPublicMethod");
    }

    [Benchmark(Baseline = true)]
    public void CallMethod_NoReflection()
    {
        ClassToInvoke.StaticPublicMethod();
    }

    [Benchmark]
    public void CallMethod_CachedMethodInfo()
    {
        _methodInfo!.Invoke(null, null);
    }

    [Benchmark]
    public void CallMethod_MethodInfo()
    {
        var methodInfo = _targetType!.GetMethod("StaticPublicMethod")!;
        methodInfo.Invoke(null, null);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=385)

#### 结果分析

基准测试结果显示,直接调用快如闪电,对空方法常常测得不到一纳秒的时间。
相比之下,基于反射的调用可能慢上好几个数量级。

```text
| Method                      | Mean      | Error     | StdDev    | Median    | Ratio | Allocated |
|---------------------------- |----------:|----------:|----------:|----------:|------:|----------:|
| CallMethod_NoReflection     | 0.0057 ns | 0.1796 ns | 0.0098 ns | 0.0000 ns |     ? |         - |
| CallMethod_CachedMethodInfo | 8.4844 ns | 4.2543 ns | 0.2332 ns | 8.4398 ns |     ? |         - |
| CallMethod_MethodInfo       | 38.0583 ns| 42.1824 ns| 2.3122 ns | 37.8771 ns|     ? |         - |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/invoking-members-58131033/?t=415)

性能数据中的关键观察:

1.  **缓存**:与每次调用都查找元数据相比,缓存 `MethodInfo` 或 `FieldInfo` 通常能让操作快一个数量级。
2.  **内存分配**:通过反射访问字段和属性常常会产生内存分配(例如 24 字节),这可能在热路径上造成 GC 压力。
    直接访问不分配任何内存。
3.  **相对开销**:虽然 30-50 纳秒客观上很快,但它比基线慢得多。
    不过,如果被调用的方法执行的是长时间运行的操作(例如耗时数秒),反射的开销就变得可以忽略不计。

## 3. Construction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/) · 15:48

### 总结

反射常常因性能差而受到批评,但它的影响会因实例化方式以及是否涉及参数而有很大差异。
本课针对无参数类、带 string 参数的类以及主构造函数,对标准构造函数与 `Activator.CreateInstance`、`Type.InvokeMember` 和 `ConstructorInfo.Invoke`(包括缓存与不缓存两种)进行了基准测试。
结果表明,虽然反射比直接实例化慢,但缓存 `ConstructorInfo` 能带来可观的性能提升,常常胜过其他基于反射的方式。

### 核心概念

* 直接调用构造函数与反射相比的基线性能。
* `Activator.CreateInstance` 在有参数与无参数场景下的开销。
* 使用 `Type.InvokeMember` 创建对象所带来的性能损失。
* 通过缓存 `ConstructorInfo` 来避免重复查找的优化。
* 使用反射时,经典构造函数与主构造函数之间的性能相当。

### 课程笔记

为了分析通过反射实例化对象的性能,我们使用三个不同的类来代表常见场景:一个无参数的类、一个带有接收 string 的经典构造函数的类,以及一个使用 C# 主构造函数并带 string 参数的类。

```csharp
using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace Benchmarks.ConsoleApp;

public class ParameterlessClass
{
}

public class ClassicStringParameterClass
{
    private readonly string _value;

    public ClassicStringParameterClass(string value)
    {
        _value = value;
    }
}

public class PrimaryConstructorStringParameterClass(
    string _value)
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/?t=60)

#### 无参数实例化

第一组基准测试聚焦于 `ParameterlessClass`。
准备工作包括在 `GlobalSetup` 阶段获取 `Type` 和 `ConstructorInfo`,以确保基准测试只衡量实例化逻辑,而不包含元数据查找的开销。

```csharp
[ShortRunJob]
public class Instantiation_ParameterlessClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(ParameterlessClass);
        _constructorInfo = _type.GetConstructor(Type.EmptyTypes);
    }

    [Benchmark(Baseline = true)]
    public ParameterlessClass Constructor()
    {
        return new ParameterlessClass();
    }

    [Benchmark]
    public object? Activator_Create_Instance()
    {
        return Activator.CreateInstance(_type!);
    }

    [Benchmark]
    public object? Type_Invoke_Member()
    {
        return _type!.InvokeMember(
            null,
            BindingFlags.CreateInstance,
            null,
            null,
            null);
    }

    [Benchmark]
    public object? Constructor_Info_Invoke()
    {
        return _constructorInfo!.Invoke(null);
    }

    [Benchmark]
    public object Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor(Type.EmptyTypes);
        return constructorInfo!.Invoke(null);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/?t=175)

在无参数的情况下,`Activator.CreateInstance` 相对高效,只比直接调用构造函数慢大约一倍(约 10ns 对 4.5ns)。
`ConstructorInfo.Invoke`(缓存)大约慢三倍。
然而,`Type.InvokeMember` 要慢得多,落后基线将近 27 倍。

#### 带参数实例化

当构造函数需要参数时,性能差距会扩大。
`ClassicStringParameterClass` 的基准测试把直接实例化与反射方式进行比较,后者必须传入一个包含实参的 object 数组。

```csharp
[ShortRunJob]
public class Instantiation_ClassicStringParameterClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(ClassicStringParameterClass);
        _constructorInfo = _type.GetConstructor([typeof(string)]);
    }

    [Benchmark(Baseline = true)]
    public ClassicStringParameterClass Constructor()
    {
        return new ClassicStringParameterClass("Hello World!");
    }

    [Benchmark]
    public object? Activator_Create_Instance()
    {
        return Activator.CreateInstance(
            _type!,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public object? Type_Invoke_Member()
    {
        return _type!.InvokeMember(
            null,
            BindingFlags.CreateInstance,
            null,
            null,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public object? Constructor_Info_Invoke()
    {
        return _constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }

    [Benchmark]
    public object? Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor([typeof(string)]);
        return constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/?t=340)

对于带参数的构造函数,`Activator.CreateInstance` 和 `Type.InvokeMember` 大约比基线慢 40 倍。
性能最好的反射方式是使用缓存的 `ConstructorInfo`,它只慢大约 5 倍。
即使每次都查找 `ConstructorInfo`,在这个场景中它仍然比 `Activator.CreateInstance` 快,不过仍比基线慢 26 倍。

#### 主构造函数

`PrimaryConstructorStringParameterClass` 的基准测试表明,就反射性能而言,主构造函数的表现与经典构造函数完全相同。
通过反射实例化类型时,使用主构造函数既没有明显的性能损失,也没有性能收益。

```csharp
[ShortRunJob]
public class Instantiation_PrimaryConstructorStringParameterClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(PrimaryConstructorStringParameterClass);
        _constructorInfo = _type.GetConstructor([typeof(string)]);
    }

    [Benchmark(Baseline = true)]
    public PrimaryConstructorStringParameterClass Constructor()
    {
        return new PrimaryConstructorStringParameterClass("Hello World!");
    }

    [Benchmark]
    public object? Activator_Create_Instance()
    {
        return Activator.CreateInstance(
            _type!,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public object? Type_Invoke_Member()
    {
        return _type!.InvokeMember(
            null,
            BindingFlags.CreateInstance,
            null,
            null,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public object? Constructor_Info_Invoke()
    {
        return _constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/?t=430)

#### 基准测试结果汇总

下表汇总了经典 string 参数类的性能比率,说明了缓存 `ConstructorInfo` 相对于其他基于反射的实例化方式的显著优势。

```csharp
| Method                            | Mean       | Ratio |
|---------------------------------- |-----------:|------:|
| Constructor                       |   4.264 ns |  1.00 |
| Activator_Create_Instance         | 173.261 ns | 40.65 |
| Type_Invoke_Member                | 182.637 ns | 42.85 |
| Constructor_Info_Invoke           |  22.378 ns |  5.25 |
| Find_Constructor_Info_Then_Invoke | 112.554 ns | 26.40 |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/construction-58131034/?t=535)

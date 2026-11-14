# Beyond Reflection

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 10 章
> 共 3 课 · 约 33:57
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Unsafe Accessors](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessors-58131091/) | 10:52 | [↓](#1-unsafe-accessors) |
| 2 | [Unsafe Accessor Performance](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/) | 13:37 | [↓](#2-unsafe-accessor-performance) |
| 3 | [Code Generation](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/code-generation-58131093/) | 8:28 | [↓](#3-code-generation) |

## 1. Unsafe Accessors

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessors-58131091/) · 10:52

### 总结

Unsafe accessor 提供了一种高性能机制,可以访问类的私有成员,而没有传统反射的开销。
通过在 extern static 方法上使用 [UnsafeAccessor] 特性,开发者可以定位私有构造函数、字段和方法。
虽然这种方式要求目标类型在编译时可用(不同于反射的动态发现),但它让运行时能够生成高度优化的访问路径,包括获取指向私有字段的直接 ref 引用的能力。

### 核心概念

- **UnsafeAccessorAttribute**:用于定义私有成员访问点的核心特性。
- **UnsafeAccessorKind**:定义目标成员类型的枚举:`Constructor`、`Field`、`Method`、`StaticField` 和 `StaticMethod`。
- **属性访问**:属性没有专门的 kind;必须通过编译器生成的 getter 或 setter 方法来访问(例如 `get_PropertyName`)。
- **ref 返回**:使用 `ref` 关键字访问字段,可以通过直接引用对私有成员进行读和写。
- **静态成员处理**:静态成员的 accessor 在方法签名中仍然需要一个目标类型的参数,但调用时传入 `null` 值。
- **编译时耦合**:与反射不同,unsafe accessor 要求目标类型在编译时已知,因为 `extern static` 方法必须在签名中引用该类型。

### 课程笔记

为了演示 unsafe accessor,考虑一个所有成员(包括构造函数、字段、方法和属性)都被标记为 private 的类。
这个类作为我们 accessor 的目标。

```csharp
public class OurType
{
    private static int _staticPrivateField = 123456;
    private int _privateField;

    private OurType(int i)
    {
        _privateField = i;
    }

    private static void StaticPrivateMethod()
    {
        Console.WriteLine($"Static private: {_staticPrivateField}");
    }

    private void PrivateMethod()
    {
        Console.WriteLine($"Instance private: {_privateField}");
    }

    private static int StaticPrivateProperty { get => _staticPrivateField; }
    private int PrivateProperty { get => _privateField; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessors-58131091/?t=40)

Unsafe accessor 通过声明带有 `[UnsafeAccessor]` 特性的 `extern static` 方法来定义。
你必须指定 `UnsafeAccessorKind`,对于构造函数以外的成员,还要指定目标成员的 `Name`。

Unsafe accessor 与反射之间的一个关键区别在于属性的处理方式。
反射把属性当作独立的成员,而 unsafe accessor 要求你使用 `get_` 或 `set_` 前缀直接定位底层的 getter 或 setter 方法。
此外,定位字段时,在返回类型中使用 `ref` 关键字可以让你获得该字段的引用,从而能够直接修改它的值。

```csharp
[UnsafeAccessor(UnsafeAccessorKind.Constructor)]
extern static OurType PrivateCtor(int i);

[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_privateField")]
extern static ref int GetSetPrivateField(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "_staticPrivateField")]
extern static ref int GetSetStaticPrivateField(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_PrivateProperty")]
extern static int GetPrivateProperty(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "PrivateMethod")]
extern static void PrivateMethod(OurType instance);

[UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "StaticPrivateMethod")]
extern static void StaticPrivateMethod(OurType instance);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessors-58131091/?t=100)

```text
extern static accessor         UnsafeAccessorKind   OurType member
---------------------------    ------------------   ----------------------
PrivateCtor(int)           --> Constructor      --> OurType(int i)
GetSetPrivateField         --> Field            --> _privateField
GetSetStaticPrivateField   --> StaticField      --> _staticPrivateField
GetPrivateProperty         --> Method           --> get_PrivateProperty
PrivateMethod              --> Method           --> PrivateMethod()
StaticPrivateMethod        --> StaticMethod     --> StaticPrivateMethod()
```

调用这些 accessor 时,实例成员需要一个有效的目标类型实例。
而静态成员则通过为实例参数传入 `null` 来调用。
由于字段的 unsafe accessor 可以返回 `ref`,你可以在不调用方法或属性 setter 的情况下更新私有状态。

```csharp
using System.Runtime.CompilerServices;

// Instantiate via private constructor
OurType instance = PrivateCtor(42);

// Access private property via getter method
int privatePropertyValue = GetPrivateProperty(instance);
Console.WriteLine($"Private property: {privatePropertyValue}");

// Get a reference to a private field
ref int referenceToPrivateField = ref GetSetPrivateField(instance);

// Get a reference to a static private field (passing null for instance)
ref int referenceToStaticPrivateField = ref GetSetStaticPrivateField(null);
Console.WriteLine($"Static private field: {referenceToStaticPrivateField}");

// Call private methods
PrivateMethod(instance);
StaticPrivateMethod(null);

// Modify the private field directly via the reference
referenceToPrivateField = 1337;
PrivateMethod(instance); // This will now print 1337
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessors-58131091/?t=535)

这种方式与反射的一个主要区别是编译时耦合。
使用反射时,你可以在运行时从程序集中动态发现类型。
Unsafe accessor 要求目标类型在编译时已知,才能定义 `extern static` 方法签名。

关于泛型,虽然在泛型类型上使用 unsafe accessor 的语法有文档说明并且能够编译,但目前它可能会导致运行时异常。
实现泛型 accessor 时,要确保 `extern static` 方法包含必要的类型参数,以匹配目标成员的签名。

## 2. Unsafe Accessor Performance

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/) · 13:37

### 总结

本课评估 UnsafeAccessorAttribute 与标准 C# 代码相比的性能特征。
借助 Benchmark.NET,分析覆盖了实例成员和静态成员的对象实例化、字段访问、属性访问和方法调用。
结果表明,unsafe accessor 提供的性能与原生 C# 调用几乎相同,并且明显快于传统反射,因此在需要访问编译时已知类型的成员时,它是理想的选择。

### 核心概念

* **性能持平**:unsafe accessor 提供的性能与原生 C# 代码几乎相同,差距通常在 2% 以内。
* **反射的替代方案**:反射允许在运行时进行动态访问,而在目标类型于编译时已知的场景下,unsafe accessor 是一种高性能的替代方案。
* **静态成员签名**:访问静态成员时,`extern static` 方法的签名中仍必须包含目标类型,但调用时传入 `null` 作为实例参数。
* **属性访问**:属性被当作方法来访问,使用 IL 命名约定 `get_PropertyName` 和 `set_PropertyName`,并搭配 `UnsafeAccessorKind.Method` 或 `UnsafeAccessorKind.StaticMethod`。
* **通过 ref 访问字段**:字段的 unsafe accessor 返回该字段的 `ref`,从而可以通过同一个 accessor 进行读取和设置。

### 课程笔记

为了评估 unsafe accessor 的性能,我们使用 Benchmark.NET 将它们与"经典"C# 代码进行比较。
虽然 unsafe accessor 可以访问私有成员,但这些基准测试聚焦于公共成员,以确保与标准代码进行同类比较,因为标准代码不借助反射就无法访问私有成员。

下面的类 `OurType` 是所有基准测试的目标,包含实例和静态的字段、属性和方法的组合。

```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using System.Reflection;
using System.Runtime.CompilerServices;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

public class OurType
{
    public static int StaticField;
    public int InstanceField;

    public static int StaticMethod()
    {
        return StaticField;
    }

    public int InstanceMethod()
    {
        return InstanceField;
    }

    public static int StaticProperty { get; set; }

    public int InstanceProperty { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=85)

### 实例化性能

第一个基准测试把通过构造函数进行的标准对象实例化,与标记为 `UnsafeAccessorKind.Constructor` 的 unsafe accessor 进行比较。

```csharp
[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_InstantiationBenchmarks
{
    [Benchmark(Baseline = true)]
    public OurType Constructor_Classic()
    {
        return new OurType();
    }

    [Benchmark]
    public OurType Constructor_Unsafe()
    {
        return UnsafeConstructor();
    }

    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    extern static OurType UnsafeConstructor();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=115)

测试中,unsafe accessor 版本只比经典构造函数慢约 2%,这比通常与 `Activator.CreateInstance` 或 `ConstructorInfo.Invoke` 相关的开销要快得多。

### 字段访问

通过 unsafe accessor 访问字段使用 `UnsafeAccessorKind.Field` 或 `UnsafeAccessorKind.StaticField`。
这些 accessor 返回底层字段的 `ref`。
对于静态字段,签名仍必须接受该类型作为第一个参数,即使调用时传入的是 `null`。

```csharp
public class UnsafeAccessors_GetFieldBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup() => _instance = new OurType();

    [Benchmark(Baseline = true)]
    public int InstanceField_Classic() => _instance!.InstanceField;

    [Benchmark]
    public int InstanceField_Unsafe()
    {
        ref int instanceField = ref GetSetInstanceField(_instance!);
        return instanceField;
    }

    [Benchmark]
    public int StaticField_Classic() => OurType.StaticField;

    [Benchmark]
    public int StaticField_Unsafe()
    {
        ref int staticField = ref GetSetStaticField(null);
        return staticField;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "InstanceField")]
    extern static ref int GetSetInstanceField(OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "StaticField")]
    extern static ref int GetSetStaticField(OurType? instance);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=175)

设置字段遵循相同的模式,利用 `ref` 返回值来完成赋值。

```csharp
public class UnsafeAccessors_SetFieldBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup() => _instance = new OurType();

    [Benchmark(Baseline = true)]
    public void InstanceField_Classic() => _instance!.InstanceField = 123456;

    [Benchmark]
    public void InstanceField_Unsafe()
    {
        ref int instanceField = ref GetSetInstanceField(_instance!);
        instanceField = 123456;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "InstanceField")]
    extern static ref int GetSetInstanceField(OurType instance);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=220)

### 属性与方法访问

属性没有专门的 `UnsafeAccessorKind`。
相反,它们通过编译器生成的名称(例如 `get_InstanceProperty`)被当作方法来访问。

```csharp
public class UnsafeAccessors_GetPropertyBenchmarks
{
    [Benchmark]
    public int StaticProperty_Unsafe()
    {
        return GetStaticProperty(null);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_InstanceProperty")]
    extern static int GetInstanceProperty(OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "get_StaticProperty")]
    extern static int GetStaticProperty(OurType? instance);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=265)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
extern static ref int GetSetInstanceField(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "StaticField")]
    extern static ref int GetSetStaticField(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_GetPropertyBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
    }

    [Benchmark(Baseline = true)]
    public int InstanceProperty_Classic()
    {
        return _instance!.InstanceProperty;
    }

    [Benchmark]
    public int InstanceProperty_Unsafe()
    {
        return GetInstanceProperty(_instance!);
    }

    [Benchmark]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=250)

```csharp
public class UnsafeAccessors_GetPropertyBenchmarks
{
    public int InstanceProperty_Unsafe()
    {
        return GetInstanceProperty(_instance!);
    }

    [Benchmark]
    public int StaticProperty_Classic()
    {
        return OurType.StaticProperty;
    }

    [Benchmark]
    public int StaticProperty_Unsafe()
    {
        return GetStaticProperty(null);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_InstanceProperty")]
    extern static int GetInstanceProperty(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "get_StaticProperty")]
    extern static int GetStaticProperty(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_SetPropertyBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=265)

```csharp
public class UnsafeAccessors_SetPropertyBenchmarks
{
    public void InstanceProperty_Unsafe()
    {
        SetInstanceProperty(_instance!, 123456);
    }

    [Benchmark]
    public void StaticProperty_Classic()
    {
        OurType.StaticProperty = 123456;
    }

    [Benchmark]
    public void StaticProperty_Unsafe()
    {
        SetStaticProperty(null, 123456);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_InstanceProperty")]
    extern static void SetInstanceProperty(
        OurType instance,
        int value);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "set_StaticProperty")]
    extern static void SetStaticProperty(
        OurType instance,
        int value);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_MethodBenchmarks
{
    private OurType? _instance;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=280)

对于方法调用,开销可以忽略不计。
在使用空方法的基准测试中,执行时间通常快到无法准确计算出与经典调用的比率,这表明 unsafe accessor 几乎不会带来任何运行时损耗。

```csharp
public class UnsafeAccessors_MethodBenchmarks
{
    [Benchmark]
    public int InstanceMethod_Unsafe()
    {
        return InvokeMethod(_instance!);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "InstanceMethod")]
    extern static int InvokeMethod(OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "StaticMethod")]
    extern static int InvokeStaticMethod(OurType? instance);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=310)

以下是通过 `search_code` 恢复的屏幕代码:

```csharp
public class UnsafeAccessors_MethodBenchmarks
{
    public int InstanceMethod_Classic()
    {
        return _instance!.InstanceMethod();
    }

    [Benchmark]
    public int InstanceMethod_Unsafe()
    {
        return InvokeMethod(_instance!);
    }

    [Benchmark]
    public int StaticMethod_Classic()
    {
        return OurType.StaticMethod();
    }

    [Benchmark]
    public int StaticMethod_Unsafe()
    {
        return InvokeStaticMethod(null);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "InstanceMethod")]
    extern static int InvokeMethod(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "StaticMethod")]
    extern static int InvokeStaticMethod(
        OurType instance);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=310)

### 基准测试结果分析

结果证实 unsafe accessor 是反射的一种高性能替代方案。
在很多情况下,例如静态字段访问,由于微优化上的波动,unsafe accessor 甚至可能显得比经典访问更快,不过一般应当认为两者是等价的。

```text
| Method              | Mean     | Error    | StdDev   | Ratio | Gen0   | Allocated | 
|-------------------- |---------:|---------:|---------:|------:|-------:|----------:|
| Constructor_Classic | 3.644 ns | 2.486 ns | 0.1363 ns |  1.00 | 0.0029 |      24 B |
| Constructor_Unsafe  | 3.730 ns | 6.987 ns | 0.3830 ns |  1.02 | 0.0029 |      24 B |
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/unsafe-accessor-performance-58131092/?t=355)

## 3. Code Generation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/code-generation-58131093/) · 8:28

### 总结

代码生成,特别是 Source Generator(源代码生成器),为涉及元数据和类型检查的场景提供了一种编译时的反射替代方案。
反射在运行时动态工作,而源代码生成器让 C# 编译器生成额外的源代码(通常借助 partial 类),用来处理正则解析、序列化和依赖注入等样板任务。
这种方式把性能开销从运行时转移到编译时,并且与传统的基于反射的技术相比,提供了更好的类型安全性和可调试性。

### 核心概念

- **编译时生成**:源代码生成器在编译过程中而不是在执行时创建代码。
- **Partial 类**:一种把开发者编写的代码与编译器生成的代码无缝合并的机制。
- **性能优化**:用生成的 "new" 表达式替换运行时反射调用(例如 `Activator.CreateInstance`),以消除运行时开销。
- **类型安全**:生成的代码要接受标准 C# 编译器的检查,比反射的后期绑定特性提供更好的安全性。
- **减少样板代码**:自动化重复模式,例如 `INotifyPropertyChanged` 或 DI 容器注册。

### 课程笔记

#### 理解源代码生成

源代码生成是 C# 编译器的一项功能,它让编译器能够在编译时检查元数据和其他信息,从而生成额外的源代码。
与在运行时产生动态行为的反射不同,源代码生成完全发生在应用程序执行之前。
这个过程通常会用到 **partial 类**。
通过把一个类定义为 partial,开发者可以编写一部分逻辑,而编译器生成的代码位于一个单独的隐藏文件中,并在编译期间合并。
这种分离确保手写的代码保持整洁,而生成的代码负责处理重复或复杂的逻辑。

```text
compile time                                         runtime
+-------------------------+
| partial class (by dev)  |--+
+-------------------------+  |   +-------------+    +-------------+
                             +-->| compilation |--->| application |
+-------------------------+  |   +-------------+    |  executes   |
| partial class (hidden,  |--+                      +-------------+
| compiler-generated)     |
+-------------------------+
```

一个突出的现代示例是正则表达式特性。
当一个 regex 被加上这个特性时,.NET 可以在 partial 类中生成具体的匹配逻辑。
这比在运行时解释 regex 要高效得多。
此外,由于结果是真正的 C# 源代码,它完全可读、可调试,开发者可以像调试应用程序的其他部分一样单步执行生成的逻辑。

#### 反射与源代码生成

虽然反射和源代码生成都处理类型信息和元数据,但根据信息何时可用,它们服务于不同的目的。

##### 1. 时机与可用性

在处理真正的动态行为、信息只有在运行时才可用时,反射是必不可少的,例如加载外部 DLL,或检查在初始构建时未知的类型。
相反,源代码生成器要求所有相关的类型信息在编译时都可用。
如果生成代码所需的数据在构建过程中缺失,就无法使用源代码生成。

##### 2. 性能影响

反射通常会带来性能损耗,因为运行时必须执行查找、验证元数据并动态调用成员。
例如,使用 `Activator.CreateInstance` 比标准的构造函数调用要慢。
源代码生成器通过在编译时生成标准的 "new" 关键字或直接的方法调用来消除这种开销。
性能成本从用户的执行时间转移到了开发者的编译时间。

##### 3. 灵活性与安全性

反射提供了最大的灵活性,允许开发者绕过某些类型系统规则来实现动态目标。
然而,这是以安全性为代价的,因为错误往往只在运行时才出现。
源代码生成器提供编译时安全性;由于输出是真正的源代码,它必须通过与项目其余部分相同的类型检查和验证规则。
这让应用程序更加健壮,更不容易出现运行时异常。

#### 源代码生成的常见用例

源代码生成器非常适合涉及大量样板代码的场景:

- **INotifyPropertyChanged**:在 WPF 等 UI 框架中,不必为每一次属性变更手动编写事件调用,生成器可以扫描特性并自动生成样板代码。
- **序列化**:如果类型在编译时已知,生成器可以创建高度优化的序列化器和反序列化器,避免在每次 JSON 或 XML 操作时都需要基于反射的检查。
- **依赖注入**:不必在容器中手动注册每个服务(例如 `services.AddTransient<IMyService, MyService>()`),源代码生成器可以使用启发式规则或特性,自动为依赖容器生成注册代码。

总之,如果行为必须在运行时动态决定,反射仍然是首要工具。
然而,如果目标是性能、安全性以及减少重复代码,源代码生成是更优的方式。

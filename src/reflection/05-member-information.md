# Member Information

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 5 章
> 共 7 课 · 约 1:24:38
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Get members of type](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/) | 14:54 | [↓](#1-get-members-of-type) |
| 2 | [BindingFlags](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/) | 9:51 | [↓](#2-bindingflags) |
| 3 | [Properties & Fields: Get and Set](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/) | 16:46 | [↓](#3-properties--fields-get-and-set) |
| 4 | [Calling Methods](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/) | 9:46 | [↓](#4-calling-methods) |
| 5 | [Constructor Info](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/) | 9:26 | [↓](#5-constructor-info) |
| 6 | [GetMember & InvokeMember](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/getmember-invokemember-58131027/) | 8:50 | [↓](#6-getmember--invokemember) |
| 7 | [Example Application](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/) | 15:05 | [↓](#7-example-application) |

## 1. Get members of type

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/) · 14:54

### 总结

本课探讨如何使用反射来检查一个类型的各种成员,包括构造函数、事件、属性、方法和字段。
它演示了复数形式的获取方法(例如 GetMethods)和单数形式的定向方法(例如带参数类型的 GetConstructor)的用法,同时强调了属性和事件这类 C# 特性是如何以底层方法的形式表示的,以及可见性如何影响成员的发现。

### 核心概念

- **成员类型**:反射把属性、方法、事件、构造函数和字段都归类为成员。
- **复数与单数获取**:`GetMembers()` 及其具体的对应方法(例如 `GetProperties()`)返回数组,而单数版本(例如 `GetProperty()`)允许按名称或签名进行定向筛选。
- **构造函数元数据**:构造函数在内部被命名为 `.ctor`,并通过其参数类型来识别。
- **语法糖**:属性和事件会被转换为支撑方法(getter/setter 以及 add/remove),这些方法会出现在方法反射的结果中。
- **默认可见性**:默认情况下,反射的获取方法通常只针对公共实例成员;访问私有成员需要指定特定的 `BindingFlags`。

### 课程笔记

为了演示成员反射,我们使用一个示例类,它包含多种常见的 C# 成员:一个字段、一个构造函数、一个事件、一个自动属性和一个方法。

```csharp
public sealed class OurExampleType
{
    private readonly int _someField;

    public OurExampleType(int someField)
    {
        _someField = someField;
    }

    public event EventHandler? SomeEvent;

    public string? SomeProperty { get; set; }

    public void DoSomething()
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=70)

#### 获取构造函数

使用 `Type` 对象,你可以调用 `GetConstructors()` 来获取所有公共构造函数。
反射用内部名称 `.ctor` 和 `void` 返回类型来标识构造函数。

```csharp
var type = typeof(OurExampleType);

var constructors = type.GetConstructors();
Console.WriteLine($"There are {constructors.Length} constructors.");
foreach (var constructor in constructors)
{
    Console.WriteLine($"\tConstructor: {constructor}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=100)

如果一个类型有多个重载,你可以使用单数形式的 `GetConstructor` 方法。
它需要一个 `Type` 对象数组,用来表示你想要匹配的参数签名。

```csharp
// Targeted approach to find a constructor taking a single int
type.GetConstructor(new Type[] { typeof(int) });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=250)

#### 事件和属性

事件和属性可以用类似的方式,通过 `GetEvents()` 和 `GetProperties()` 来获取。
虽然存在单数形式的 `GetEvent(name)` 和 `GetProperty(name)` 方法,但它们主要按名称进行筛选。
对于更复杂的筛选,例如查找可读或可写的属性,对复数形式的结果使用 LINQ 往往更灵活。

```
var events = type.GetEvents();
Console.WriteLine($"There are {events.Length} events.");
foreach (var @event in events)
{
    Console.WriteLine($"\tEvent: {@event}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=340)

对于属性,`PropertyInfo` 对象提供了 `CanRead` 和 `CanWrite` 等元数据,用来检查访问器是否可用。

```csharp
var properties = type.GetProperties();
Console.WriteLine($"There are {properties.Length} properties.");
foreach (var property in properties)
{
    Console.WriteLine($"\tProperty: {property}");
    Console.WriteLine($"\tRead: {property.CanRead}");
    Console.WriteLine($"\tWrite: {property.CanWrite}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=565)

#### 方法与支撑成员

调用 `GetMethods()` 时,结果中包含的不只是源代码中显式定义的方法。
这是因为:
1.  **继承**:来自 `System.Object` 的方法(例如 `ToString`、`Equals`、`GetHashCode` 和 `GetType`)也会被包含进来。
2.  **支撑方法**:C# 的属性和事件是语法糖。
    反射会揭示由编译器生成的底层 `get_`、`set_`、`add_` 和 `remove_` 方法。

```csharp
var methods = type.GetMethods();
Console.WriteLine($"There are {methods.Length} methods.");
foreach (var method in methods)
{
    Console.WriteLine($"\tMethod: {method}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=715)

```text
  C# 源代码                       GetMethods() 中可见的方法
+------------------------+       +-----------------------------+
| SomeProperty {get;set;}| ----> | get_SomeProperty            |
|                        |       | set_SomeProperty            |
+------------------------+       +-----------------------------+
| event SomeEvent        | ----> | add_SomeEvent               |
|                        |       | remove_SomeEvent            |
+------------------------+       +-----------------------------+
| DoSomething()          | ----> | DoSomething                 |
+------------------------+       +-----------------------------+
| (System.Object)        | ----> | ToString, Equals,           |
|                        |       | GetHashCode, GetType        |
+------------------------+       +-----------------------------+
```

#### 字段与可见性

默认情况下,即使字段存在,调用 `GetFields()` 也可能返回零个结果。
出现这种情况是因为默认的反射范围通常会排除非公共成员。
要获取 private 或 protected 字段,你必须向方法调用提供 `BindingFlags`。

```csharp
// Using BindingFlags to find non-public instance fields
var fields = type.GetFields(
    System.Reflection.BindingFlags.NonPublic |
    System.Reflection.BindingFlags.Instance);

Console.WriteLine($"There are {fields.Length} fields.");
foreach (var field in fields)
{
    Console.WriteLine($"\tField: {field}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/get-members-of-type-58131029/?t=880)

## 2. BindingFlags

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/) · 9:51

### 总结

BindingFlags 是一组选项,与 .NET 反射方法配合使用,用来筛选从一个类型返回的成员。
默认情况下,GetMethods 或 GetConstructors 这类反射方法只返回公共实例成员,以尊重类所设计的公共 API。
然而,通过用按位 OR 运算符组合 NonPublic、Static 和 Instance 等标志,开发者可以检查私有字段、internal 方法以及编译器生成的支撑成员,而这些成员在标准检查中是被隐藏的。

### 核心概念

- **BindingFlags 枚举**:一个位掩码,用于定义反射过程中成员发现的条件。
- **按位 OR 运算符(|)**:用来组合多个标志(例如 Public | NonPublic)以扩大搜索范围的机制。
- **Public 与 NonPublic**:Public 获取具有 public 访问级别的成员,而 NonPublic 获取 private、protected 和 internal 成员。
- **Instance 与 Static**:Instance 筛选属于对象实例的成员,而 Static 筛选属于类型本身的成员。
- **编译器生成的成员**:反射会揭示由编译器创建的成员,例如自动属性的支撑字段和事件的访问器方法。

### 课程笔记

默认情况下,反射会尊重开发者定义的可见性修饰符。
如果一个方法或字段被标记为 private,它通常不打算供外部使用,标准的反射调用会将其省略。
要超越这些默认行为、检查一个类型的完整内部结构,你必须借助 `BindingFlags`。

下面的类 `OurExampleType` 混合包含了 public、private、实例和静态成员,用来演示不同的标志如何影响反射查询的结果。

```csharp
public sealed class OurExampleType
{
    private static readonly string MyStaticField = "This is a static field!";
    private readonly int _someField;

    public OurExampleType(int someField)
    {
        _someField = someField;
    }

    private OurExampleType()
    {
        Console.WriteLine("This is a private constructor!");
    }

    public event EventHandler? SomeEvent;

    public static int StaticProperty { get; set; }

    public string? SomeProperty { get; set; }

    private string? SomePrivateProperty { get; set; }

    private static void StaticMethod()
    {
    }

    public void DoSomething()
    {
    }

    private void DoSomethingElse()
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=100)

#### 获取构造函数

要获取构造函数,你使用 `GetConstructors` 方法。
通过传入 `BindingFlags.Public`、`BindingFlags.NonPublic` 和 `BindingFlags.Instance` 的组合,你可以同时看到 public 构造函数和 private 无参构造函数。
这些标志使用按位 OR(`|`)运算符组合在一起,其效果是同时打开所有指定的选项。

```csharp
using System.Reflection;

var type = typeof(OurExampleType);

var constructors = type.GetConstructors(
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance);
Console.WriteLine($"There are {constructors.Length} constructors.");
foreach (var constructor in constructors)
{
    Console.WriteLine($"\tConstructor: {constructor}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=115)

如果你只想看到 private 构造函数,可以省略 `Public` 标志,只使用 `NonPublic` 和 `Instance`。

```csharp
var constructors = type.GetConstructors(
    BindingFlags.NonPublic |
    BindingFlags.Instance);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=205)

#### 按作用范围筛选属性

属性可以根据它们是基于实例的还是静态的来进行筛选。
如果你只用 `Instance` 标志查询属性,静态属性即使是 public 的也会被排除。

```csharp
var properties = type.GetProperties(
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance);
Console.WriteLine($"There are {properties.Length} properties.");
foreach (var property in properties)
{
    Console.WriteLine($"\tProperty: {property}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=250)

要包含 `StaticProperty` 这样的静态属性,你必须显式地把 `BindingFlags.Static` 标志加入位掩码。

```csharp
var properties = type.GetProperties(
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance |
    BindingFlags.Static);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=310)

```text
                 Instance        Static
              +---------------+----------------+
  Public      | SomeProperty  | StaticProperty |
              +---------------+----------------+
  NonPublic   | SomePrivate-  |                |
              |   Property    |                |
              +---------------+----------------+

  Public | NonPublic | Instance           -> 左列
  Public | NonPublic | Instance | Static  -> 两列
```

#### 检查方法和编译器生成的访问器

获取方法时,反射不仅返回你显式定义的方法,还会返回编译器生成的方法。
例如,每个属性都会生成一个 `get_` 和一个 `set_` 方法,每个事件都会生成 `add_` 和 `remove_` 方法。
此外,当指定了 `NonPublic` 时,`Finalize` 和 `MemberwiseClone` 这类基类方法也会变得可见。

```csharp
var methods = type.GetMethods(
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance |
    BindingFlags.Static);
Console.WriteLine($"There are {methods.Length} methods.");
foreach (var method in methods)
{
    Console.WriteLine($"\tMethod: {method}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=385)

这样一次查询的输出揭示了 .NET 编译器对访问器和继承成员所使用的内部命名约定:

```text
There are 17 methods.
        Method: Void add_SomeEvent(System.EventHandler)
        Method: Void remove_SomeEvent(System.EventHandler)
        Method: Int32 get_StaticProperty()
        Method: Void set_StaticProperty(Int32)
        Method: System.String get_SomeProperty()
        Method: Void set_SomeProperty(System.String)
        Method: System.String get_SomePrivateProperty()
        Method: Void set_SomePrivateProperty(System.String)
        Method: Void StaticMethod()
        Method: Void DoSomething()
        Method: Void DoSomethingElse()
        Method: System.Type GetType()
        Method: System.Object MemberwiseClone()
        Method: Void Finalize()
        Method: System.String ToString()
        Method: Boolean Equals(System.Object)
        Method: Int32 GetHashCode()
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=400)

#### 访问字段和支撑字段

字段通常保持为 private,以强制实施封装。
使用 `BindingFlags.NonPublic`,你就可以获取这些字段。
对于自动实现的属性,编译器会生成"支撑字段"(backing field),它们同样可以通过反射看到。
它们遵循特定的命名模式,例如 `<PropertyName>k__BackingField`。

```csharp
var fields = type.GetFields(
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance |
    BindingFlags.Static);
Console.WriteLine($"There are {fields.Length} fields.");
foreach (var field in fields)
{
    Console.WriteLine($"\tField: {field}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/bindingflags-58131023/?t=490)

## 3. Properties & Fields: Get and Set

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/) · 16:46

### 总结

本课演示如何在 .NET 中使用反射来读取和写入字段与属性的值。
它涵盖了 GetValue 和 SetValue 方法的用法、BindingFlags(包括 Static、Public 和 NonPublic)的重要性,以及实例成员访问与静态成员访问之间的区别。
关键在于,本课探讨了反射绕过实例字段上的访问修饰符和 readonly 约束的能力,同时也指出了这种能力在面对 static readonly 字段和没有 setter 的属性时的局限。

### 核心概念

- 使用 `BindingFlags` 访问 private 和 public 成员。
- 用 `GetValue` 获取值,用 `SetValue` 更新值。
- 通过把 `null` 作为实例参数传入来处理静态成员。
- 绕过实例字段上的 `readonly` 修饰符。
- 理解修改 `static readonly` 字段和没有 setter 的属性时的局限。

### 课程笔记

要通过反射与一个类的成员交互,你首先需要获取类型信息以及具体的成员元数据。
下面这个类作为这些示例的基础,它混合包含了 private、static、readonly 和自动实现的成员。

```csharp
public sealed class OurClass
{
    private static readonly string PrivateStaticField = "Hello, World!";

    private readonly int _readOnlyField = 123;
    private int _readWriteField = 42;

    public int ReadWriteAutoProperty { get; set; }

    public int ReadOverReadWriteField => _readWriteField;

    public int FullOverReadWriteField
    {
        get => _readWriteField;
        set => _readWriteField = value;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=50)

#### 使用 BindingFlags 发现成员

默认情况下,`GetFields()` 和 `GetProperties()` 只返回公共实例成员。
要看到 private 或 static 成员,你必须提供相应的 `BindingFlags`。
要获取 `OurClass` 中的所有字段,包括那个 private static 字段,你必须加入 `BindingFlags.NonPublic` 和 `BindingFlags.Static`。

```csharp
using System.Reflection;

OurClass instance = new();
var type = instance.GetType();

var fields = type.GetFields(
    BindingFlags.Instance |
    BindingFlags.NonPublic |
    BindingFlags.Public |
    BindingFlags.Static);

var properties = type.GetProperties(
    BindingFlags.Instance |
    BindingFlags.NonPublic |
    BindingFlags.Public |
    BindingFlags.Static);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=235)

#### 读取和写入字段

反射允许你使用 `GetValue` 读取 private 字段。
对于实例字段,你必须把对象实例传给 `GetValue`。
对于静态字段,你传入 `null`,因为该值关联的是类型本身,而不是某个具体实例。

```csharp
// Accessing an instance field
var readOnlyField = fields.First(f => f.Name == "_readOnlyField");
var readOnlyFieldValue = readOnlyField.GetValue(instance);
Console.WriteLine($"The value of the read-only field is {readOnlyFieldValue}.");

// Accessing a static field
var staticField = fields.First(f => f.IsStatic);
var staticFieldValue = staticField.GetValue(null);
Console.WriteLine($"The value of the static field is {staticFieldValue}.");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=415)

#### 处理属性

属性的访问方式与之类似,使用 `PropertyInfo.GetValue` 和 `PropertyInfo.SetValue`。
在自动属性上使用 `SetValue` 时,反射会自动处理底层的支撑字段。

```csharp
var readWriteAutoProperty = properties.First(p => p.Name == "ReadWriteAutoProperty");
var initialValue = readWriteAutoProperty.GetValue(instance);

readWriteAutoProperty.SetValue(instance, 789);
var updatedValue = readWriteAutoProperty.GetValue(instance);
Console.WriteLine($"The value of the read-write auto property is now {updatedValue}.");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=520)

如果一个属性没有 setter(例如 `ReadOverReadWriteField`),尝试调用 `SetValue` 会抛出 `System.ArgumentException`,因为没有可供调用的 set 方法。

```csharp
var readOverReadWriteField = properties.First(p => p.Name == "ReadOverReadWriteField");
// This will throw an exception because there is no setter
// readOverReadWriteField.SetValue(instance, 1337);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=655)

当一个属性同时拥有显式引用某个支撑字段的 getter 和 setter 时,在该属性上调用 `SetValue` 会正确地更新那个支撑字段。

```csharp
var fullOverReadWriteField = properties.First(p => p.Name == "FullOverReadWriteField");
fullOverReadWriteField.SetValue(instance, 1337);

var readWriteField = fields.First(f => f.Name == "_readWriteField");
var backingFieldValue = readWriteField.GetValue(instance);
Console.WriteLine($"The backing field is also updated: {backingFieldValue}.");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=730)

```text
  ReadOverReadWriteField  (get only) --get--> +-----------------+
                                              | _readWriteField |
  FullOverReadWriteField  (get/set)  <-get--> |                 |
                                     --set--> +-----------------+

  ReadOverReadWriteField.SetValue(...)  -> System.ArgumentException
  FullOverReadWriteField.SetValue(instance, 1337)
                                        -> _readWriteField = 1337
```

#### 绕过只读约束

反射最强大(也可能最危险)的一面之一,是能够修改 `readonly` 实例字段。
即使一个字段被标记为 `private readonly`,`SetValue` 也能在初始化之后更改它的值。

```csharp
// Modifying a private readonly instance field
readOnlyField.SetValue(instance, 456);
var newReadOnlyValue = readOnlyField.GetValue(instance);
Console.WriteLine($"The value of the read-only field is now {newReadOnlyValue}.");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=805)

然而,这种能力是有限度的。
一旦类完成了初始化,你就不能使用反射来设置 `static readonly` 字段。
尝试这样做会导致异常,因为一旦静态构造函数执行完毕,运行时就会保护该类型的静态状态。

```csharp
var staticField = fields.First(f => f.IsStatic);
// This will throw an exception: "Cannot set initonly static field"
// staticField.SetValue(null, "New Value");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/properties-fields-get-and-set-58131024/?t=925)

## 4. Calling Methods

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/) · 9:46

### 总结

本课探讨如何在 .NET 中使用反射来调用方法,演示了如何绕过访问修饰符去调用 private 实例方法和 private 静态方法。
它涵盖了通过 GetMethod 获取 MethodInfo、配置 BindingFlags 以包含静态和非公共成员,以及 Invoke 方法的机制,包括通过 object 数组传递参数和处理装箱的返回值。

### 核心概念

- **方法获取**:使用 `type.GetMethod("MethodName", bindingFlags)` 获取某个特定成员的 `MethodInfo` 对象。
- **BindingFlags**:对静态方法必须包含 `BindingFlags.Static`,对 private 方法必须包含 `BindingFlags.NonPublic`。
- **方法调用**:使用 `MethodInfo.Invoke` 在运行时执行一个方法。
- **实例与静态**:作为 `Invoke` 的第一个参数,实例方法传入对象实例,静态方法传入 `null`。
- **参数**:以 `object[]` 的形式传递实参,不需要参数时传 `null`;运行时校验会确保参数个数与签名匹配。
- **返回值**:处理 `Invoke` 的 `object` 返回类型,并进行必要的类型转换,以便在特定于类型的操作中使用结果。

### 课程笔记

反射提供了调用那些因保护级别而在编译时无法访问的方法的能力。
首先,我们定义一个目标类,它包含 private 实例方法和一个 private 静态方法。

```csharp
public sealed class OurClass
{
    private void DoSomething()
    {
        Console.WriteLine($"This is the {nameof(DoSomething)} method!");
    }

    private void DoSomethingElse(
        string stringValue,
        int integerValue)
    {
        Console.WriteLine($"{stringValue}: {integerValue}");
    }

    private static int AddNumbers(int numberA, int numberB)
    {
        return numberA + numberB;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/?t=40)

为了与这些方法交互,我们必须配置 `BindingFlags`,让反射引擎能够定位非公共、实例和静态成员。
如果省略了 `BindingFlags.Static`,反射将找不到带有 `static` 修饰符的方法。

```csharp
using System.Reflection;

var bindingFlags =
    BindingFlags.Instance |
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Static;

OurClass instance = new();
var type = instance.GetType();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/?t=130)

#### 调用无参方法

要调用一个不带参数的方法,例如 `DoSomething`,我们获取 `MethodInfo` 并调用 `Invoke`。
第一个参数是要操作的类实例。
由于 `DoSomething` 是一个实例方法,我们必须提供 `instance` 变量。
第二个参数是 `null`,因为没有参数。

```csharp
var doSomething = type.GetMethod("DoSomething", bindingFlags);
doSomething.Invoke(instance, null);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/?t=235)

#### 传递参数

调用带参数的方法时,实参必须以 `object[]` 的形式提供。
反射会在运行时校验参数个数;提供错误数量的实参会导致 `TargetParameterCountException`(参数个数不匹配)。

```csharp
var doSomethingElse = type.GetMethod("DoSomethingElse", bindingFlags);
doSomethingElse.Invoke(instance, ["Hello, World!", 42]);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/?t=340)

#### 静态方法与返回值

调用静态方法时,在 `Invoke` 调用中把 `null` 作为实例参数传入。
由于反射是在运行时解析的,`Invoke` 的返回类型始终是 `object`。
方法的任何返回值都会被装箱,这意味着必须把它转换回原始类型(例如 `int`),才能执行算术之类特定于类型的操作。

```csharp
var addNumbers = type.GetMethod("AddNumbers", bindingFlags);
var result = (int)addNumbers.Invoke(null, [10, 20]);
var half = result / 2d;
Console.WriteLine($"Result: {result}, Half: {half}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/calling-methods-58131025/?t=550)

```text
  MethodInfo.Invoke( target,   args )
                       |         |
  DoSomething          instance  null
  DoSomethingElse      instance  ["Hello, World!", 42]
  AddNumbers (static)  null      [10, 20]
                       |
                       v
                  object  --(int)-->  result
```

## 5. Constructor Info

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/) · 9:26

### 总结

本课演示如何在 .NET 中使用反射来获取构造函数信息并实例化对象。
它涵盖了使用 GetConstructors 配合特定的 BindingFlags 来查找无参、带参以及 private 构造函数。
本课还讲解了如何使用 ConstructorInfo.Invoke 调用这些构造函数、匹配参数类型和个数的重要性,以及通过反射创建对象时构造函数链的行为。

### 核心概念

- 使用 `GetConstructors` 配合 `BindingFlags` 获取构造函数。
- 通过 `ConstructorInfo.Invoke` 调用构造函数。
- 处理无参构造函数与带参构造函数。
- 访问并调用 private 构造函数。
- 反射调用期间的构造函数链行为。
- 参数个数和类型的匹配要求。

### 课程笔记

本课重点讲解使用反射获取 `ConstructorInfo` 并创建新的对象实例。
为了演示这一点,定义了一个名为 `OurClass` 的类,它有三个使用构造函数链的构造函数。
无参构造函数调用一个两参数的 public 构造函数,后者又调用一个三参数的 private 构造函数。

首先,我们获取类型信息,并定义包含 public、非公共和实例成员的 `BindingFlags`。
然后我们使用 `GetConstructors` 获取所有匹配这些标志的构造函数。

```csharp
using System.Reflection;

Console.WriteLine();

var type = typeof(OurClass);

var bindingFlags =
    BindingFlags.Public |
    BindingFlags.NonPublic |
    BindingFlags.Instance;
var constructors = type.GetConstructors(bindingFlags);

var parameterlessConstructor = constructors
    .FirstOrDefault(x => x
    .GetParameters().Length == 0);
var instance = parameterlessConstructor?.Invoke(null);
Console.WriteLine();

public sealed class OurClass
{
    public OurClass()
        : this(42, "Hello, World!")
    {
        Console.WriteLine("Parameterless constructor called.");
    }

    public OurClass(int someInt, string someString)
        : this(someInt, someString, 1.337)
    {
        Console.WriteLine("Constructor with parameters called.");
    }

    private OurClass(int someInt, string someString, double anotherNumber)
    {
        Console.WriteLine("Private constructor called.");

        Console.WriteLine($"someInt: {someInt}");
        Console.WriteLine($"someString: {someString}");
        Console.WriteLine($"anotherNumber: {anotherNumber}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/?t=40)

调用无参构造函数时,我们可以向 `Invoke` 方法传入 `null`。
尽管我们针对的是无参构造函数,整条链都会被执行。
由于 `OurClass` 中的链式逻辑,private 构造函数最先执行,接着是两参数构造函数,最后才是无参构造函数。

```text
  调用链                                   执行顺序
  OurClass()                               3. "Parameterless constructor called."
    : this(42, "Hello, World!")
      OurClass(int, string)                2. "Constructor with parameters called."
        : this(someInt, someString, 1.337)
          OurClass(int, string, double)    1. "Private constructor called."
```

要调用需要实参的构造函数,我们必须提供一个 object 数组,其元素在个数和类型上都与构造函数的参数相匹配。

```csharp
var parameterizedConstructor = constructors
    .FirstOrDefault(x => x
    .GetParameters().Length == 2);
var instance2 = parameterizedConstructor?.Invoke([
    112233,
    "Dev Leader Rocks!"]);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/?t=250)

反射之所以格外强大,是因为它可以访问那些在类外部原本无法访问的 private 构造函数。
通过筛选出 `IsPrivate` 为 true 的构造函数,我们可以直接调用它,绕过 public 的链式逻辑。

```csharp
var privateConstructor = constructors
    .FirstOrDefault(x => x.IsPrivate);
var instance3 = privateConstructor?.Invoke([
    98765,
    "Reflection is super cool!",
    Math.PI]);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/?t=400)

确保传给 `Invoke` 的参数与预期签名相匹配至关重要。
如果实参的数量不正确,会抛出 `TargetParameterCountException`。
同样,如果类型顺序错误或无法转换,则会出现 `ArgumentException`,提示该对象类型无法转换为所需的类型。

```csharp
var privateConstructor = constructors
    .FirstOrDefault(x => x.IsPrivate);
var instance3 = privateConstructor?.Invoke([
    "Reflection is super cool!",
    98765,
    Math.PI]);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/?t=460)

虽然反射对于构建对象关系映射器(ORM)或序列化器这类通用框架是必不可少的(这些框架必须创建在编译时未知的类型的实例),但当类可以直接访问时,出于性能和类型安全的考虑,更推荐使用 `new` 关键字进行标准实例化。

```csharp
var instance = new OurClass();
var instanceWithParams = new OurClass(123, "");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/constructor-info-58131026/?t=505)

## 6. GetMember & InvokeMember

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/getmember-invokemember-58131027/) · 8:50

### 总结

本课介绍 GetMember 和 InvokeMember,它们是在 .NET 反射中与类型成员交互的统一 API。
虽然存在 GetMethod 或 GetProperty 这样的专用方法,但 GetMember 提供了一种按名称获取 MemberInfo 的通用方式,其中也包括以特殊名称 ".ctor" 标识的构造函数。
InvokeMember 是一种多用途的替代方案,通过把搜索用的 BindingFlags 与 CreateInstance、InvokeMethod 或 GetProperty 这类操作标志组合起来,执行创建实例、调用方法以及获取或设置属性等操作。

### 核心概念

- **GetMember**:一个通用 API,使用 `BindingFlags` 按名称获取类型的成员。
- **构造函数命名**:在反射中,构造函数由内部名称 ".ctor" 标识。
- **InvokeMember**:一个单一方法,可以根据所提供的 `BindingFlags` 执行多种操作(实例化、方法调用、属性访问)。
- **操作类 BindingFlags**:`BindingFlags.CreateInstance`、`BindingFlags.InvokeMethod` 和 `BindingFlags.GetProperty` 等标志定义了 `InvokeMember` 应当执行的动作。
- **目标实例**:对于实例成员,`InvokeMember` 需要一个目标对象;对于构造函数(CreateInstance),目标为 null。

### 课程笔记

为了演示这些 API,我们使用一个简单的类 `MyType`,它包含一个 private 字段、一个构造函数、一个属性和一个方法。

```csharp
public sealed class MyType
{
    private string _myMessage;

    public MyType(string myMessage)
    {
        _myMessage = myMessage;
    }

    public string MyMessage => _myMessage;

    public void MyMethod()
    {
        Console.WriteLine(_myMessage);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/getmember-invokemember-58131027/?t=55)

#### 使用 GetMember

`GetMember` 允许你按字符串名称获取任意成员。
搜索公共实例成员时,你必须提供相应的 `BindingFlags`。
一个值得注意的细节是,构造函数在元数据中的名称是 ".ctor"。

```csharp
using System.Reflection;

BindingFlags searchBindingFlags =
    BindingFlags.Public |
    BindingFlags.Instance;
Type type = typeof(MyType);

var myConstructorMember = type.GetMember(".ctor", searchBindingFlags).Single();
Console.WriteLine($"My Constructor Member: {myConstructorMember.Name}");

var myMessageMember = type.GetMember("MyMessage", searchBindingFlags).Single();
Console.WriteLine($"My Message Member: {myMessageMember.Name}");

var myMethodMember = type.GetMember("MyMethod", searchBindingFlags).Single();
Console.WriteLine($"My Method Member: {myMethodMember.Name}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/getmember-invokemember-58131027/?t=100)

#### 使用 InvokeMember

`InvokeMember` 是一个强大的方法,可以取代好几个特定的反射调用。
它需要一个名称(对于构造函数可以为 null)、一个 `BindingFlags` 组合(搜索标志与操作标志进行 OR 运算)、一个 binder(通常为 null)、一个目标实例,以及一个实参数组。

要创建实例,使用 `BindingFlags.CreateInstance`。
与 `GetMember` 不同,你不需要把 ".ctor" 指定为名称;当存在 `CreateInstance` 标志时,传入 `null` 就足够了。

```csharp
var instance = (MyType?)type.InvokeMember(
    name: null,
    invokeAttr: searchBindingFlags | BindingFlags.CreateInstance,
    binder: null,
    target: null,
    args: ["Hello, World!"]);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/getmember-invokemember-58131027/?t=325)

一旦有了实例,你就可以使用 `InvokeMember` 来调用方法或访问属性。
对于方法,使用 `BindingFlags.InvokeMethod`。
对于属性,使用 `BindingFlags.GetProperty`(或 `SetProperty`)。
注意 `InvokeMember` 返回一个可空的 object,因此需要显式转换才能使用返回的值。

```csharp
type.InvokeMember(
    name: "MyMethod",
    invokeAttr: searchBindingFlags | BindingFlags.InvokeMethod,
    binder: null,
    target: instance,
    args: null);

var message = (string?)type.InvokeMember(
    name: "MyMessage",
    invokeAttr: searchBindingFlags | BindingFlags.GetProperty,
    binder: null,
    target: instance,
    args: null);
Console.WriteLine($"My Message: {message}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/getmember-invokemember-58131027/?t=385)

```text
  invokeAttr = searchBindingFlags | <操作标志>

  操作标志          name          target     args
  ---------------   -----------   --------   -----------------
  CreateInstance    null          null       ["Hello, World!"]
  InvokeMethod      "MyMethod"    instance   null
  GetProperty       "MyMessage"   instance   null
```

## 7. Example Application

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/) · 15:05

### 总结

本课通过构建一个"Object Inspector"(对象检查器)工具,演示反射的一个实际应用。
该应用允许用户选择一个对象实例并检查其元数据,包括构造函数、字段、属性和方法。
它还展示了如何在这些实例上动态调用方法、如何通过把用户输入转换为相应类型来处理方法参数,以及观察反射如何与 private 成员和自动生成的支撑字段交互。

### 核心概念

* 使用 `GetType()` 进行动态类型检查。
* 使用 `GetConstructors`、`GetFields`、`GetProperties` 和 `GetMethods` 获取成员元数据。
* 使用 `BindingFlags` 筛选 public、非公共和实例成员。
* 通过 `FieldInfo.GetValue` 和 `PropertyInfo.GetValue` 访问实例的值。
* 使用 `MethodInfo.Invoke` 进行动态方法调用。
* 使用 `Convert.ChangeType` 对动态参数进行类型转换。
* 理解自动生成的成员,例如属性的支撑字段以及 getter/setter 方法。

### 课程笔记

Object Inspector 应用的设计目的是在运行时检查对象实例并与之交互。
该应用维护一个对象实例数组,这个数组本可以通过程序集扫描来扩展,不过为简单起见,这个实现使用的是硬编码的类型。

```csharp
using System.Reflection;

Console.WriteLine("Welcome to the Object Inspector!");

object[] instances =
[
    new TypeA(),
    new TypeB()
];

while (true)
{
    Console.WriteLine("Available objects to inspect:");

    for (int i = 0; i < instances.Length; i++)
    {
        Console.WriteLine($"{i}: {instances[i].GetType().Name}");
    }

    var indexUserInput = Ask("Enter the index of the object you want to inspect:");

    if (!int.TryParse(indexUserInput, out int objectInstance) ||
        objectInstance < 0 ||
        objectInstance >= instances.Length)
    {
        Error("Invalid input. Please try again.");
        continue;
    }

    var objectToInspect = instances[objectInstance];
    Console.WriteLine($"You selected: {objectToInspect.GetType().Name}");

    PrintAllInfo(objectToInspect);
    AskInvokeMethod(objectToInspect);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=25)

被检查的对象被定义为简单的类。
`TypeA` 包含一个 private 只读字段和一个带默认值的自动属性。

```csharp
public sealed class TypeA
{
    private readonly string _devLeader = "Dev Leader";

    public int Id { get; set; } = 42_1337;
}

public sealed class TypeB
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=70)

#### 检查成员

`PrintAllInfo` 方法通过分别调用针对构造函数、字段、属性和方法的具体方法来统筹整个检查过程。
其中每个方法都使用 `BindingFlags`,以确保 public 和非公共的实例成员都能被获取到。

```csharp
if (!int.TryParse(indexUserInput, out int objectInstance) ||
    objectInstance < 0 ||
    objectInstance >= instances.Length)
{
    Error("Invalid input. Please try again.");
    continue;
}

var objectToInspect = instances[objectInstance];
Console.WriteLine($"You selected: {objectToInspect.GetType().Name}");

PrintAllInfo(objectToInspect);
AskInvokeMethod(objectToInspect);

static void PrintAllInfo(object objectToInspect)
{
    var previousColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Green;

    PrintConstructorInfo(objectToInspect);
    PrintFieldInfo(objectToInspect);
    PrintPropertyInfo(objectToInspect);
    PrintMethodInfo(objectToInspect);

    Console.ForegroundColor = previousColor;
}

static void PrintConstructorInfo(object objectToInspect)
{
    Console.WriteLine("Constructors:");
    var constructors = objectToInspect.GetType().GetConstructors(
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=160)

```text
  while (true)
    objectToInspect = instances[objectInstance]
      |
      +--> PrintAllInfo(objectToInspect)
      |      +--> PrintConstructorInfo
      |      +--> PrintFieldInfo
      |      +--> PrintPropertyInfo
      |      +--> PrintMethodInfo
      |
      +--> AskInvokeMethod(objectToInspect)
```

为了检查构造函数,该应用使用 `GetConstructors`,并通过 `GetParameters()` 遍历每个构造函数的参数。

```csharp
static void PrintConstructorInfo(object objectToInspect)
{
    Console.WriteLine("Constructors:");
    var constructors = objectToInspect.GetType().GetConstructors(
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic);
    foreach (var constructor in constructors)
    {
        Console.WriteLine($"  Constructor:");
        Console.WriteLine($"  Parameters");

        var parameters = constructor.GetParameters();
        if (parameters.Length == 0)
        {
            Console.WriteLine("    None!");
        }
        else
        {
            foreach (var parameter in parameters)
            {
                Console.WriteLine($"    {parameter.ParameterType.Name} {parameter.Name}");
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=175)

字段和属性以类似的方式,使用 `GetFields` 和 `GetProperties` 进行检查。
由于该应用操作的是对象实例,它可以使用 `GetValue` 方法获取这些成员的当前值。

```csharp
static void PrintFieldInfo(object objectToInspect)
{
    Console.WriteLine("Fields:");
    var fields = objectToInspect.GetType().GetFields(
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic);
    foreach (var field in fields)
    {
        Console.WriteLine($"  {field.FieldType.Name} {field.Name}");
        Console.WriteLine($"    Value: {field.GetValue(objectToInspect)}");
    }
}

static void PrintPropertyInfo(object objectToInspect)
{
    Console.WriteLine("Properties:");
    var properties = objectToInspect.GetType().GetProperties(
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic);
    foreach (var property in properties)
    {
        Console.WriteLine($"  {property.PropertyType.Name} {property.Name}");
        Console.WriteLine($"    Value: {property.GetValue(objectToInspect)}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=235)

检查方法时,该应用会显示返回类型、方法名和参数列表。
需要注意的是,.NET 中的自动属性会生成底层的 "getter" 和 "setter" 方法(例如 `get_Id` 和 `set_Id`)以及一个支撑字段,这些都可以通过反射看到。

```csharp
static void PrintMethodInfo(object objectToInspect)
{
    Console.WriteLine("Methods:");
    var methods = objectToInspect.GetType().GetMethods(
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic);
    foreach (var method in methods)
    {
        Console.WriteLine($"  {method.ReturnType.Name} {method.Name}");
        Console.WriteLine($"    Parameters");

        var parameters = method.GetParameters();
        if (parameters.Length == 0)
        {
            Console.WriteLine("      None!");
        }
        else
        {
            foreach (var parameter in parameters)
            {
                Console.WriteLine($"        {parameter.ParameterType.Name} {parameter.Name}");
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=285)

#### 动态调用

该应用允许用户按名称调用方法。
`GetMethod` 调用是区分大小写的,所以名称必须完全匹配。
如果方法需要参数,应用会提示用户以字符串形式输入,然后使用 `Convert.ChangeType` 把该字符串转换为所需的 `ParameterType`。
随后使用 `Invoke` 执行该方法,传入实例和一个参数值数组。

```csharp
static void AskInvokeMethod(object objectToInspect)
{
    var methodName = Ask("Enter the name of the method you want to invoke (or empty to skip):");
    if (string.IsNullOrWhiteSpace(methodName))
    {
        return;
    }

    var method = objectToInspect.GetType().GetMethod(
        methodName,
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic);
    if (method == null)
    {
        Error("Method not found.");
        return;
    }

    var parameters = method.GetParameters();
    object?[] parameterValues = new object[parameters.Length];
    for (int i = 0; i < parameters.Length; i++)
    {
        var parameterValue = Ask(
            $"Enter the value for parameter " +
            $"{parameters[i].Name} ({parameters[i].ParameterType.Name}):");
        parameterValues[i] = Convert.ChangeType(parameterValue, parameters[i].ParameterType);
    }

    object? result = method.Invoke(objectToInspect, parameterValues);

    var previousColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Method {methodName} invoked. Result: {result}");
    Console.ForegroundColor = previousColor;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=370)

```text
  methodName --GetMethod--> method
                              |
                        GetParameters()
                              |
  Ask(...) --Convert.ChangeType(.., ParameterType)--> parameterValues[i]
                              |
          method.Invoke(objectToInspect, parameterValues)
                              |
                              v
                        object? result
```

该应用使用一些简单的辅助方法来处理控制台交互和颜色管理。

```csharp
static string? Ask(string message)
{
    var previousColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(message);
    var input = Console.ReadLine();
    Console.ForegroundColor = previousColor;
    return input;
}

static void Error(string message)
{
    var previousColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ForegroundColor = previousColor;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-application-58131028/?t=535)

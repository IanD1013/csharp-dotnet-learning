# What Is Reflection?

> 课程:[From Zero to Hero: Reflection in .NET](https://dometrain.com/course/from-zero-to-hero-reflection-in-dotnet/) · 第 3 章
> 共 2 课 · 约 10:10
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What Is Reflection?](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/) | 6:06 | [↓](#1-what-is-reflection) |
| 2 | [Example Use Cases](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-use-cases-58128579/) | 4:04 | [↓](#2-example-use-cases) |

## 1. What Is Reflection?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/) · 6:06

### 总结

.NET 中的反射(Reflection)是一种强大的机制,它允许程序在运行时检查自身的元数据并与之交互。
标准的 C# 开发依赖于编译时可用的类型信息,而反射使开发者能够动态地发现类型结构、实例化对象,并调用那些直到应用程序执行时才可能知道的成员。

### 核心概念

- **元数据(Metadata)**:关于 .NET 应用程序中的类型、成员和程序集的信息。
- **运行时发现(Runtime Discovery)**:在执行期间调查类型(接口、基类、属性、方法)的能力。
- **动态交互(Dynamic Interaction)**:在没有编译时引用的情况下构造对象并调用方法。
- **System.Type**:访问元数据的主要入口,通过 `GetType()` 等方法获得。

### 课程笔记

反射是 .NET 的一个基础概念,它提供了一种与类型系统周边的元数据进行交互的方式。
在标准的 C# 编程中,变量、参数和对象都以编译时已知的特定类型来定义。
然而,某些场景需要超越静态类型定义,在运行时动态地查询有关类型的信息。

反射允许开发者调查一个类型的结构,包括它的基类、实现的接口、属性、方法和字段。
除了简单的检查之外,反射还可以用来动态构造对象实例,并调用那些在代码编译时无法访问或未知的成员。

来看一个基本的类定义:

```csharp
public sealed class NicksType
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/?t=160)

我们可以用属性来扩展这个类,以表示数据:

```csharp
public sealed class NicksType
{
    public int Value { get; set; }

    public string Name { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/?t=190)

在典型的应用程序中,你会使用编译时类型信息来实例化这个类并为它的属性赋值。
编译器会根据类定义,确保 `Value` 接收一个整数,`Name` 接收一个字符串。

```csharp
NicksType myVariable = new NicksType()
{
    Value = 1,
    Name = "Nick"
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/?t=220)

当你需要访问某个对象的、不属于当前直接逻辑流程的元数据时,反射就变得有用了。
其入口通常是 `GetType()` 方法,它返回一个表示该实例元数据的 `Type` 对象。

```csharp
myVariable.GetType();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/?t=250)

```text
myVariable (NicksType)
  Value = 1
  Name  = "Nick"
      |
      | GetType()
      v
Type (NicksType 的元数据)
      |
      +--> IsSealed
```

一旦拿到 `Type` 对象,你就可以查询具体的元数据。
例如,你可能需要在运行时检查一个类型是否为 sealed,这在自定义序列化等高级场景中,或者在使用来自外部已编译二进制文件、看不到源代码的类型时,是一个常见的需求。

```csharp
var type = myVariable.GetType();
type.IsSealed;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/what-is-reflection-58128578/?t=280)

通过访问这些元数据,你可以编写更灵活的程序,使其适应在执行期间遇到的类型。

## 2. Example Use Cases

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-reflection-in-dotnet-2871274/example-use-cases-58128579/) · 4:04

### 总结

反射是 .NET 中的一项基础技术,它支持动态的类型检查与操作。
虽然开发者未必总是直接编写反射代码,但它驱动着许多必不可少的框架和库,包括 ORM、依赖注入容器、单元测试工具和插件架构,让它们能够在运行时处理类型元数据。

### 核心概念

- 元数据的检查与操作
- 动态对象实例化
- 运行时依赖图构建
- 测试发现与执行
- 序列化与反序列化
- 可扩展的插件架构

### 课程笔记

反射是在运行时检查类型的元数据并与之交互的过程。
虽然它可能比较抽象,但它是许多常见 .NET 开发工具和模式的关键组成部分。

#### 对象关系映射器(ORM)

ORM(例如 Entity Framework)在很大程度上依赖反射来弥合数据库架构与 C# 对象之间的鸿沟。
它们使用反射来检查类的属性,并将其映射到数据库列。
此外,ORM 在从数据库检索数据时,会使用反射来动态创建这些对象的实例。

#### 依赖注入(DI)框架

像 Autofac 这样的 DI 框架使用反射在运行时构造依赖图。
当请求某个服务时,框架会检查目标类型的构造函数,以识别它的依赖项。
然后它会递归地解析并实例化这些依赖项。
虽然这会带来轻微的运行时性能损耗,但它在管理对象生命周期和解耦组件方面提供了显著的灵活性。

```text
请求服务
   |
   v
检查目标类型的构造函数 --> 识别依赖项
                              |
                              v
                 递归解析并实例化这些依赖项
```

#### 单元测试框架

xUnit 和 NUnit 等测试工具使用反射来发现程序集中的测试。
通过扫描类型层次结构以查找特定的特性(attribute),或遵循命名约定,测试运行器可以识别要执行哪些方法,并动态处理 setup 和 teardown 过程。

#### 序列化与反序列化

将对象与二进制、JSON 或 XML 等格式相互转换需要反射。
为了用外部数据源填充对象的属性,序列化器必须查看该类型的元数据,以确定存在哪些属性以及如何为它们赋值。

#### 插件系统

插件架构是反射最强大的应用之一。
这些系统通常需要加载并执行来自外部程序集的代码,而这些程序集在编译时是未知的。
反射允许宿主应用程序加载这些外部文件,检查其中是否有特定的接口或基类,并将它们的功能集成到正在运行的程序中。
这通常与依赖注入结合使用,以构建高度可扩展的软件。

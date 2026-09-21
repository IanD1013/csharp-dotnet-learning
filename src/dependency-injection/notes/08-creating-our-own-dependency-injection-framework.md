# Creating our own Dependency Injection framework

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 8 章
> 共 5 课 · 约 35:28
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Why should we even bother?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-should-we-even-bother-53953324/) | 1:25 | [↓](#1-why-should-we-even-bother) |
| 2 | [The design](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/) | 2:57 | [↓](#2-the-design) |
| 3 | [The implementation](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/) | 19:57 | [↓](#3-the-implementation) |
| 4 | [Extending the main implementation](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/) | 10:36 | [↓](#4-extending-the-main-implementation) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953329/) | 0:33 | [↓](#5-section-recap) |

---

## 1. Why should we even bother?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/why-should-we-even-bother-53953324/) · 1:25

### 总结

从零开始构建一个自定义的依赖注入(Dependency Injection,DI)框架是一种教学式的练习,目的是加深对控制反转(Inversion of Control,IoC)容器的理解。
通过把 DI 拆解成它的基本组成部分,开发者可以掌握那些通常被隐藏在高层 API 背后的底层逻辑和架构模式。
这个过程不仅澄清了依赖是如何被管理的,还引入了一些可以被借鉴、用于改进一般软件实现的编码实践。

### 核心概念

* **Learning through deconstruction(通过拆解来学习)**:通过把一个复杂系统拆分成它的基本组成部分来获得精通。
* **Internal mechanics of IoC(IoC 的内部机制)**:理解容器在幕后是如何管理服务注册与解析的。
* **Pattern adaptation(模式借鉴)**:识别框架设计中那些可以应用到普通应用程序开发里的逻辑和实践。
* **Core architectural understanding(核心架构理解)**:超越 API 的使用层面,去理解依赖管理的原则。

### 课程笔记

本章的目标是从头实现一个可用的依赖注入(DI)框架。
这个练习的根基在于这样一条原则:真正掌握一个系统,是通过把它拆解成基本组成部分来实现的。
通过从零构建一个控制反转(IoC)容器,那些通常被抽象掉的内部机制变得可见、可理解。

虽然构建自定义 DI 框架是一项可选的练习,对标准的应用程序开发来说并非严格必要,但理解其底层逻辑有相当大的好处。
这次深入剖析提供了一个真实的视角,让你看到依赖管理中涉及的挑战与解决方案。
此外,观察这个实现过程会揭示出一些架构模式和最佳实践,它们可以被直接借鉴,用来提升一般软件代码的质量与可维护性。

---

## 2. The design

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/) · 2:57

### 总结

本课勾勒出在 .NET 中构建自定义依赖注入(DI)框架的架构目标与 API 设计。
目标是复刻 .NET 内置 DI 容器的外观与使用感受,包括 service collection、service provider 这类熟悉的模式,以及 AddSingleton 这样的标准注册方法。
通过对齐现有的 Microsoft API,这个自定义框架既保持直观,又能在不依赖任何外部库的情况下展示服务注册、生命周期管理和解析的内部机制。

### 核心概念

- **API Parity(API 对等)**:把自定义框架设计成与 `Microsoft.Extensions.DependencyInjection` API 一致,以保持熟悉感。
- **Service Collection(服务集合)**:用于注册 service descriptor 的容器。
- **Service Provider(服务提供者)**:负责解析服务并管理其生命周期的引擎。
- **Service Descriptors(服务描述符)**:描述服务类型、实现类型和生命周期的元数据。
- **Resolution Methods(解析方法)**:实现 `GetRequiredService` 以获取非空的服务实例。
- **Testability(可测试性)**:使用接口来包装 `Console.WriteLine` 这类静态调用,以便更好地进行 mock。

### 课程笔记

项目从一个名为 `Consumer.ConsoleApp` 的标准控制台应用开始。
这个应用作为自定义 DI 框架的试验场。

```csharp
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=25)

设计目标是与内置的 .NET DI 容器达成 API 对等。
这需要实现一个用来保存注册信息的 `ServiceCollection`,以及一个用来解析它们的 `ServiceProvider`。
通过采用相同的命名约定,比如 `AddSingleton`、`AddTransient` 和 `GetRequiredService`,这个框架对已经熟悉标准 .NET 生态的开发者来说依然容易上手。

```csharp
var services = new ServiceCollection();


var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetRequiredService();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=55)

为了演示这个框架,这里定义了一个简单的日志服务。
把 `Console.WriteLine` 包装进 `IConsoleWriter` 这样的接口是一种提升可测试性的标准做法,因为它让控制台输出可以在单元测试中被 mock 或校验。

```csharp
namespace Consumer.ConsoleApp;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string text)
    {
        Console.WriteLine();
    }
}

public interface IConsoleWriter
{
    void WriteLine(string text);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=115)

最终的设计允许以指定的生命周期注册服务,并通过 service provider 来解析它们。
这个实现将支持用于注册和解析的泛型方法,从而确保类型安全的开发体验。
该框架仅使用 .NET SDK 构建,零外部依赖,以此说明依赖注入的核心逻辑。

```csharp
using Consumer.ConsoleApp;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();

var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetRequiredService<IConsoleWriter>();

service.WriteLine("Hello from DI");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-design-53953326/?t=145)

---

## 3. The implementation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/) · 19:57

### 总结

本课一步步演示如何创建一个名为 "Vax" 的自定义依赖注入(DI)框架。
内容涵盖用于注册的 service collection、用于解析的 service provider,以及处理 transient 和 singleton 生命周期所需的逻辑,其中包括使用反射递归解析构造函数参数。

### 核心概念

* **ServiceCollection**:一个用来注册依赖的、专门存放 service descriptor 的列表。
* **ServiceDescriptor**:一个元数据容器,保存服务类型、实现类型和生命周期。
* **ServiceLifetime**:一个枚举,定义实例是如何被管理的(Transient 或 Singleton)。
* **ServiceProvider**:负责解析并提供服务实例的核心引擎。
* **Recursive Resolution(递归解析)**:通过向容器查询,自动解析某个服务的构造函数参数的过程。
* **Lazy Initialization(延迟初始化)**:确保 Singleton 服务只在首次被请求时才实例化,以避免注册顺序带来的问题。

### 课程笔记

框架从 `ServiceCollection` 开始,它充当依赖的注册表。
它继承自 `List<ServiceDescriptor>`,因此可以存放容器最终要管理的那些服务的定义。

```csharp
namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=70)

集合中的每一项都是一个 `ServiceDescriptor`。
这个类保存 `ServiceType`(接口或基类)、`ImplementationType`(要实例化的具体类)以及 `ServiceLifetime`。

```csharp
namespace Vax;

public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = default!;

    public Type? ImplementationType { get;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=310)

为了注册服务,`ServiceCollection` 提供了 `AddSingleton` 和 `AddTransient` 这样的方法。
这些方法创建一个新的 `ServiceDescriptor` 并把它加入内部列表。

```csharp
public ServiceCollection AddSingleton<TService, TImplementation>()
{
    var serviceDescriptor = new ServiceDescriptor
    {
        ServiceType = typeof(TService),
        ImplementationType = typeof(TImplementation)
    };
    Add(serviceDescriptor);
    return this;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=295)

所有服务注册完成后,调用 `BuildServiceProvider` 方法来创建 `ServiceProvider`。
该方法把当前集合传给 provider 的构造函数。

```csharp
public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(this);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=385)

`ServiceProvider` 负责真正的类型解析。
它维护两个内部字典:一个用于 transient 服务(保存一个工厂函数),另一个用于 singleton 服务(保存一个 `Lazy<object>`)。

```csharp
public class ServiceProvider
{
    private readonly Dictionary<Type, Func<object>> _transientTypes = new();
    private readonly Dictionary<Type, Lazy<object>> _singletonTypes = new();

    internal ServiceProvider(ServiceCollection serviceCollection)
    { 

    }

    public T? GetService<T>()
    { 
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type serviceType)
    { 
        throw new NotImplementedException();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=595)

`GenerateServices` 方法遍历 `ServiceCollection` 并填充这些字典。
对于 singleton,`Activator.CreateInstance` 被包装在 `Lazy<object>` 中,以确保服务只在需要时才被创建,这样就能避免依赖注册顺序不当所引发的问题。

```csharp
private void GenerateServices(ServiceCollection serviceCollection)
{
    foreach (var serviceDescriptor in serviceCollection)
    {
        switch (serviceDescriptor.Lifetime)
        {
            case ServiceLifetime.Singleton:
                _singletonTypes[serviceDescriptor.ServiceType] = 
                    new Lazy<object>(() =>
                        Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!);
                continue;
            case ServiceLifetime.Transient:
                _transientTypes[serviceDescriptor.ServiceType] =
                    () => Activator.CreateInstance(serviceDescriptor.ImplementationType,
                        GetConstructorParameters(serviceDescriptor))!;
                continue;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=880)

为了处理那些自身还带有依赖的服务,框架使用了一个名为 `GetConstructorParameters` 的递归方法。
这个方法检查实现类型的第一个构造函数,取出它的参数,并针对每个参数类型调用 `GetService` 来解析整棵依赖树。

```csharp
private object?[] GetConstructorParameters(ServiceDescriptor descriptor)
{
    var constructorInfo = descriptor.ImplementationType.GetConstructors().First();
    var parameters = constructorInfo.GetParameters()
        .Select(x => GetService(x.ParameterType)).ToArray();

    return null;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=805)

`GetService` 方法的逻辑先检查 singleton 字典。
如果找到匹配项,它就从 `Lazy` 包装器中返回对应的值。
如果没有,它就检查 transient 字典并调用工厂函数。

```csharp
public object? GetService(Type serviceType)
{
    var service = _singletonTypes.GetValueOrDefault(serviceType);

    if (service is not null)
    {
        return service.Value;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=910)

最后,这个框架可以用来解析复杂的依赖树。
例如,一个依赖 `IConsoleWriter` 的 `IdGenerator` 可以被注册并自动解析。

```csharp
using Vax;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
services.AddSingleton<IIdGenerator, IdGenerator>();

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<IIdGenerator>();
var service2 = serviceProvider.GetService<IIdGenerator>();

service1.PrintId();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/the-implementation-53953327/?t=1130)

---

## 4. Extending the main implementation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/) · 10:36

### 总结

本课聚焦于增强这个自定义依赖注入框架,使其与 .NET 内置的 DI 容器达成功能对等。
主要的改进包括:加入泛型约束以确保注册时的类型安全、支持自注册的服务、允许注册已经存在的对象实例,以及实现基于工厂的注册。
本课还涵盖了对 ServiceProvider 所做的必要修改,使其能够解析这些新的注册类型,并确保 singleton 和 transient 两种生命周期都得到遵守。

### 核心概念

- 用于类型安全的泛型约束(`where TImplementation : class, TService`)。
- 自注册重载(`AddSingleton<TService>`)。
- 手动的 `ServiceDescriptor` 注册。
- 针对已有对象的基于实例的注册。
- 使用 `Func<ServiceProvider, TService>` 的基于工厂的注册。
- 更新 `ServiceProvider` 的解析逻辑,以处理实例和工厂。

### 课程笔记

#### 使用泛型约束保证类型安全

为了防止出现具体类并未实现所指定接口的无效注册,必须给 `AddSingleton` 和 `AddTransient` 方法加上泛型约束。
通过指定 `where TImplementation : class, TService`,编译器可以确保实现类型与服务类型是兼容的。

```csharp
namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TImplementation>(Se
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TImplementation>(Se
        Add(serviceDescriptor);
        return this;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=55)

#### 自注册与手动描述符

在很多情况下,一个服务是以它自己作为实现来注册的。
为此添加了一些重载来支持这种 "self-registration" 模式,为那些不需要把接口映射到类的用户简化 API。

```csharp
ollection : List<ServiceDescriptor>

lection AddSingleton<TService>()
e : class

scriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);
scriptor);


lection AddTransient<TService>()
e : class

scriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLifetime.Singleton);
scriptor);


lection AddSingleton<TService, TImplementation>()
e : class
entation : class, TService
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=85)

此外,提供一个 `AddService` 方法可以让用户手动注册自己的 `ServiceDescriptor` 实例,从而提供最大的灵活性。

```csharp
namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>()
        where TService : class
    {
        var serviceDescriptor = AddServiceDescriptorWithLifetime<TService, TService>(ServiceLi
        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddTransient<TService>()
        where TService : class
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=175)

#### 注册已有的实例

为了支持对象已经在容器之外被实例化的场景,这里实现了一个接受 `object` 的 `AddSingleton` 重载。
这个实例会被直接保存在 `ServiceDescriptor` 中。

```csharp
public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }

    public ServiceCollection AddSingleton(object implementation)
    {
        var serviceType = implementation.GetType();
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = serviceType,
            ImplementationType = serviceType,
            Implementation = implementation,
            Lifetime = ServiceLifetime.Singleton
        };
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=220)

#### 基于工厂的注册

基于工厂的注册允许自定义实例化逻辑。
`ServiceDescriptor` 被更新,加入了一个 `ImplementationFactory` 属性,它是一个接受 `ServiceProvider` 并返回已解析对象的函数。

```csharp
public class ServiceDescriptor
{
    public Type ServiceType { get; init; } = default!;

    public Type ImplementationType { get; set; }

    public object? Implementation { get; set; }

    public Func<ServiceProvider, object>? ImplementationFactory { get; set; }

    public ServiceLifetime Lifetime { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=430)

随后 `ServiceCollection` 也被更新,加入了接受这些工厂的重载。

```csharp
ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Singleton
        };

        Add(serviceDescriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        var serviceDescriptor = new ServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory,
            Lifetime = ServiceLifetime.Singleton
        };

        Add(serviceDescriptor);
        return this;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=460)

#### 更新 Service Provider 的逻辑

`ServiceProvider` 必须做出修改,以便在它的 `GenerateServices` 方法中处理这些新的注册类型。
对于 singleton,provider 会先检查是否有预先存在的实例或工厂可用,然后才退回到基于反射的实例化。

```csharp
case ServiceLifetime.Singleton:
                    if (serviceDescriptor.Implementation is not null)
                    {
                        _singletonTypes[serviceDescriptor.ServiceType] =
                            new Lazy<object>(serviceDescriptor.Implementation);
                        continue;
                    }

                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _singletonTypes[serviceDescriptor.ServiceType] =
                            new Lazy<object>(() =>
                                serviceDescriptor.ImplementationFactory(this));
                        continue;
                    }

                    _singletonTypes[serviceDescriptor.ServiceType] =
                        new Lazy<object>(() =>
                            Activator.CreateInstance(serviceDescriptor.ImplementationType,
                                GetConstructorParameters(serviceDescriptor))!);
                    continue;
                case ServiceLifetime.Transient:
                    _transientTypes[serviceDescriptor.ServiceType] =
                        () => Activator.CreateInstance(serviceDescriptor.I
                            GetConstructorParameters(serviceDescriptor
                    continue;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=490)

对 transient 服务也应用了类似的逻辑,确保每次请求该服务时工厂都会被调用。

```csharp
GetConstructorParameters(serviceDescriptor))!);
                    continue;
                case ServiceLifetime.Transient:
                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _transientTypes[serviceDescriptor.ServiceType] =
                            () => serviceDescriptor.ImplementationFactory(this);
                        continue;
                    }

                    _transientTypes[serviceDescriptor.ServiceType] =
                        () => Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!;
                    continue;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/extending-the-main-implementation-53953328/?t=535)

---

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953329/) · 0:33

### 总结

本课为这一章"在 .NET 中构建自定义依赖注入(DI)框架"作结。
它回顾了模仿标准 Microsoft DI 容器 API 表面的架构思路、核心注册与解析逻辑的实现,以及为支持 singleton 生命周期和基于工厂的服务注册等特性而对框架所做的扩展。

### 核心概念

* 用一套熟悉的 Microsoft 风格 API 来设计自定义 IoC 容器。
* 实现一个用于服务注册的 ServiceCollection。
* 构建一个用于处理依赖解析的 ServiceProvider。
* 支持多种注册方式,包括直接传入实例和工厂委托。
* 管理诸如 Singleton 之类的服务生命周期。

### 课程笔记

这个自定义 DI 框架的开发,重点在于创建一个可用的控制反转(IoC)容器,并让它在外观和使用感受上与标准的 .NET 实现保持一致。
这种做法确保了熟悉 Microsoft DI 容器的开发者可以轻松过渡到使用这个自定义框架。

实现过程涉及通过 `ServiceCollection` 建立服务注册的核心机制。
这个集合存放着服务应当如何被创建和管理的定义。
注册完成后,调用 `BuildServiceProvider` 方法来创建那个在运行时负责解析这些依赖的引擎。

该框架支持多种注册模式,包括注册已有实例的能力以及使用工厂委托。
工厂委托尤其强大,因为它们允许你手动控制实例化过程,同时仍然借助 provider 来解析嵌套的依赖。

```csharp
using Consumer.ConsoleApp;
using Vax;

var services = new ServiceCollection();

// services.AddSingleton<IConsoleWriter, ConsoleWriter>();
// services.AddSingleton<IIdGenerator, IdGenerator>();
//

//services.AddSingleton<ConsoleWriter>();
services.AddSingleton(new ConsoleWriter());

services.AddSingleton(
    provider => new IdGenerator(provider.GetService<ConsoleWriter>()!));

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<IdGenerator>();
var service2 = serviceProvider.GetService<IdGenerator>();

service1.PrintId();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/section-recap-53953329/?t=10)

通过实现这些特性,这个自定义框架达到了与基础 DI 容器的功能对等,能够通过一套干净、流式的 API 支持 singleton 管理和复杂的依赖解析。

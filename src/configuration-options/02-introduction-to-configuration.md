# Introduction to Configuration

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 2 章
> 共 8 课 · 约 18:33
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-58112431/) | 0:49 | [↓](#1-introduction) |
| 2 | [What is Configuration?](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/what-is-configuration-58112025/) | 1:45 | [↓](#2-what-is-configuration) |
| 3 | [Why configuration is important?](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/why-configuration-is-important-58112026/) | 1:38 | [↓](#3-why-configuration-is-important) |
| 4 | [Configuration in .NET](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuration-in-net-58112027/) | 1:28 | [↓](#4-configuration-in-net) |
| 5 | [Configuration sources & providers](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuration-sources-providers-58112032/) | 1:11 | [↓](#5-configuration-sources--providers) |
| 6 | [First Configuration NuGet package](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/first-configuration-nuget-package-58112034/) | 2:47 | [↓](#6-first-configuration-nuget-package) |
| 7 | [Explore the IConfiguration and related APIs](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/) | 7:34 | [↓](#7-explore-the-iconfiguration-and-related-apis) |
| 8 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112036/) | 1:21 | [↓](#8-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-58112431/) · 0:49

### 总结

本课介绍 .NET 生态中配置的基本概念,概述配置的定义、重要性,以及配置源在现代应用开发中的作用。

### 核心概念

- 配置的定义及其用途。
- 配置在应用开发生命周期中的重要性。
- 配置在现代 .NET 应用中的作用。
- 识别影响应用行为的各种配置源。

### 课程笔记

配置是一组外部参数,它们影响应用的行为方式,而无需修改已编译的代码。
本节对这些概念做正式介绍,重点关注配置的"是什么"和"为什么"。

在现代 .NET 开发中,配置并不局限于单个文件。
它涉及多种可以相互覆盖或相互补充的来源。
理解这些来源及其所扮演的角色,对于在开发、预发布(staging)和生产等不同环境中管理应用设置至关重要。
接下来的课程将探讨可用的具体配置源,以及它们如何影响应用的整体行为。

## 2. What is Configuration?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/what-is-configuration-58112025/) · 1:45

### 总结

.NET 中的配置代表一组参数和设置,它们决定应用在不同环境中的行为方式。
通过将这些值外部化,开发者可以避免产出硬编码的制品,使同一个应用二进制文件能够根据命令行参数等外部输入来调整其功能,例如日志的详细程度。

### 核心概念

- **行为控制**:配置影响应用的执行方式,而无需修改代码。
- **环境定制**:让应用能够适应不同的环境(开发、预发布、生产)。
- **外部输入**:配置可以通过多种来源提供,包括命令行参数和开关。
- **避免硬编码**:将设置与已编译的制品解耦,以提高灵活性。

### 课程笔记

在 .NET 开发中,配置被定义为一组控制并影响应用行为的参数或设置。
没有配置系统,应用就会变成僵化的、硬编码的制品。
这样的应用虽然能运行,但用处有限,因为它们无法轻松适应不同的运行环境。

配置的主要优势在于能够根据应用所运行的环境来定制其行为。
这种灵活性确保同一个可执行文件可以根据外部设置表现出不同的行为。

提供配置的一种常见方式是命令行参数,通常也称为开关(switch)。
例如,带着 `/verbose` 标志启动一个 .NET 应用就是一种配置形式。
应用读取这个输入并相应地调整其行为,比如增加控制台输出的详细程度。

## 3. Why configuration is important?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/why-configuration-is-important-58112026/) · 1:38

### 总结

配置是软件开发的一个基础支柱,它让应用变得可维护、安全且灵活。
通过将设置外部化,开发者可以在不重新编译代码的情况下改变应用行为,通过安全的存储机制保护敏感凭据,并确保在各种不同环境中表现一致。

### 核心概念

- **可维护性**:将设置与代码解耦,从而无需重新编译即可就地改变行为。
- **安全性**:通过避免硬编码来保护连接字符串、API 密钥和令牌等敏感数据。
- **安全存储**:使用环境变量或基于云的机密管理器,例如 Azure Key Vault。
- **灵活性**:让应用能够适应不同环境,同时保持可预测性和一致性。

### 课程笔记

#### 可维护性

配置的主要好处是能够在不修改源代码的情况下影响应用的行为。
这使得更新可以就地进行,减少了仅为简单的值变更而重新编译和重新部署所带来的开销。
借助配置,逻辑本身保持稳定,而控制该逻辑的参数可以按需调整。

#### 安全性

安全性是任何应用都必须重点考虑的问题。
硬编码敏感信息是一个重大风险,必须避免。
应用通常需要访问各种资源,因此需要用到:

- 连接字符串
- 用户凭据和密码
- API 密钥
- 短期有效的令牌

这些值必须被安全地存储。
根据环境的不同,这可能意味着使用环境变量,或者在云环境中为获得更强的安全性而使用 Azure Key Vault 之类的服务。

#### 灵活性

配置提供了应用在不同上下文中运行所需的灵活性。
这不仅仅是适应某个特定环境;它还确保应用保持一致且可预测。
配置让软件在核心功能上保持稳固,同时又能适应其所运行的基础设施的具体要求。

## 4. Configuration in .NET

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuration-in-net-58112027/) · 1:28

### 总结

.NET 中的配置让应用可以受外部设置影响,而无需修改代码。
这些设置来自各种配置源,从本地文件、环境变量到安全的云端提供程序,确保在不同部署环境中的灵活性。

### 核心概念

- **配置源(Configuration Sources)**:配置数据的来源,例如文件或环境变量。
- **常见来源**:包括环境变量、命令行参数和 JSON 设置文件。
- **开发与安全**:用于本地开发的 User Secrets,以及用于生产环境安全配置的云端方案。
- **可扩展性**:支持内存(in-memory)源、key-per-file 配置以及自定义提供程序实现。
- **配置提供程序(Configuration Providers)**:在外部来源与应用之间架起桥梁的组件。

### 课程笔记

在 .NET 中,配置是根据外部输入来影响应用行为的机制。
当应用在某个特定环境中运行时,它会从各种配置源中获取数据,以决定其运行逻辑。

.NET 应用通常会使用以下几种常见的配置源:

- **环境变量**:在宿主环境中定义的系统级变量。
- **命令行参数**:启动时传给应用可执行文件的参数。
- **JSON 设置文件**:像 `appsettings.json` 这样的结构化文件,提供层级式的配置结构。

除了这些标准来源之外,.NET 还支持一些更专门的配置:

- **内存(In-memory)**:在应用代码中以集合形式定义的配置。
- **Key-per-file**:一种常用于容器化环境的模式,其中目录里的每个文件代表一个配置键。
- **User Secrets**:一种用于本地开发的工具,让开发者把敏感配置数据(如 API 密钥)存放在项目的源代码管理之外。
- **云端方案**:用于安全、集中式配置管理的托管服务。
- **自定义实现**:扩展配置系统以支持专有或独特数据源的能力。

配置源定义了数据从哪里来,而配置提供程序则是让应用能够读取并解释这些数据的底层机制。
理解来源与提供程序之间的这种并置关系,是掌握 .NET 配置系统的基础。

## 5. Configuration sources & providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/configuration-sources-providers-58112032/) · 1:11

### 总结

本课讲解 .NET 中配置源与提供程序之间的关系,强调来源代表设置的物理或逻辑来源,而提供程序是读取这些数据并将其具体化到应用中的工具。

### 核心概念

- **配置源(Configuration Source)**:设置的物理或逻辑来源,例如磁盘上的一个文件。
- **配置提供程序(Configuration Provider)**:用于从来源读取设置并将其具体化到应用中的机制或工具。
- **一对一关系**:每个配置源都有一个与之对应的特定配置提供程序。

### 课程笔记

在 .NET 配置中,配置源和配置提供程序之间是直接的一对一关系。
理解这种关系是管理应用如何使用外部设置的基础。

#### 配置源

配置源代表设置的实际来源。
它是存储配置数据的物理或逻辑位置。
例如,在使用设置文件时,位于磁盘上的那个物理文件就是配置源。
其他来源还可以包括环境变量、命令行参数或外部机密管理器。

#### 配置提供程序

配置提供程序是负责从来源读取数据并将其具体化到应用配置系统中的工具或机制。
来源是数据本身,而提供程序是知道如何解析这些数据、并让它们以键值对形式在应用中可用的功能组件。

```text
 配置源 (source)          配置提供程序 (provider)        应用
+-----------------+      +------------------------+     +------------+
| 磁盘上的设置文件| ---> | 读取并解析数据         | --> | 键值对     |
+-----------------+      +------------------------+     +------------+
        1          <---------------->       1
```

为了在实际环境中运用这些概念,开发者通常会使用特定的 NuGet 包。
这些包包含了各种来源所需的提供程序,让控制台应用或 Web 服务能够把来自多个来源的配置聚合成一个统一的系统。

## 6. First Configuration NuGet package

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/first-configuration-nuget-package-58112034/) · 2:47

### 总结

本课介绍 Microsoft.Extensions.Configuration NuGet 包,它是 .NET 应用中配置的基础。
本课演示如何使用 ConfigurationBuilder 聚合配置源,具体是使用 AddInMemoryCollection 方法提供初始数据。
本课最后展示如何把配置构建成一个 IConfigurationRoot,并使用基于字符串的索引器取值。

### 核心概念

- **Microsoft.Extensions.Configuration**:.NET 配置抽象与功能所需的核心 NuGet 包。
- **ConfigurationBuilder**:用于组合各种配置源(JSON、环境变量、内存等)的主要类。
- **AddInMemoryCollection**:一个让开发者用 `KeyValuePair<string, string?>` 的 `IEnumerable` 为配置提供初始数据的方法。
- **IConfigurationRoot**:由构建器产出的对象,代表配置层级的根。
- **索引器访问**:通过配置对象上的字符串索引器来访问配置值。

### 课程笔记

要在 .NET 控制台应用中开始使用配置,首先必须引用 `Microsoft.Extensions.Configuration` NuGet 包。
这个包为基本的配置提供程序提供了必要的实现,其中包括内存提供程序。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration"
                      Version="8.0.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/first-configuration-nuget-package-58112034/?t=10)

配置过程从实例化一个 `ConfigurationBuilder` 开始。
这个构建器充当一个容器,可以在其中注册多个配置源。
在这个示例中,使用 `AddInMemoryCollection` 方法添加了一个内存集合。
该方法期望以 `IEnumerable<KeyValuePair<string, string?>>` 的形式接收初始数据。

定义好来源之后,在构建器上调用 `Build()` 方法会返回一个 `IConfigurationRoot` 对象。
这个对象让你可以访问扁平化后的配置数据。
值通过索引器语法取得,其中键以字符串形式提供。

```csharp
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

builder.AddInMemoryCollection([
    new KeyValuePair<string, string?>("Key", "123")
]);

var config = builder.Build();

var value = config["Key"];

Console.WriteLine(value);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/first-configuration-nuget-package-58112034/?t=40)

```text
ConfigurationBuilder
  + AddInMemoryCollection(("Key", "123"))
        |
        | Build()
        v
IConfigurationRoot  --  config["Key"]  -->  "123"  -->  Console
```

应用执行时,`IConfigurationRoot` 把键 "Key" 解析为值 "123",随后将其打印到控制台。
这个简单的设置构成了更复杂的配置层级和提供程序的基础。

## 7. Explore the IConfiguration and related APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/) · 7:34

### 总结

本课探讨 Microsoft Extensions Configuration 系统的核心接口和 API,重点是 `IConfiguration` 和 `IConfigurationRoot`。
本课详细介绍配置如何被表示为一棵层级树、配置提供程序的作用,以及用于遍历节(section)、获取连接字符串和生成当前配置状态调试视图的各种方法。

### 核心概念

- **IConfigurationRoot**:代表配置层级的根,提供对底层提供程序的访问,以及一个强制重新加载的方法。
- **IConfiguration**:在配置树中访问键值对、节和子节点的主要接口。
- **层级键**:使用冒号(`:`)分隔符来表示配置键中从祖父到子级的关系。
- **配置提供程序**:把数据从来源(例如内存、JSON 文件)加载到配置系统中的具体实现。
- **基于约定的 API**:像 `GetConnectionString` 这样的专用方法,可以简化对常见结构配置数据的访问。
- **调试视图**:一个用于可视化整棵配置树、并识别每个值由哪个提供程序提供的工具。

### 课程笔记

在 Microsoft Extensions Configuration 生态中,`ConfigurationBuilder.Build()` 方法返回一个 `IConfigurationRoot`。
这个接口代表配置层级的最顶层。
由于配置是层级式的,节点可以有父节点和子节点,但根节点没有父节点。

```csharp
using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Represents the root of an <see cref="IConfiguration"/> hierarchy.
    /// </summary>
    public interface IConfigurationRoot : IConfiguration
    {
        /// <summary>
        /// Force the configuration values to be reloaded from the underlying providers.
        /// </summary>
        void Reload();

        /// <summary>
        /// The <see cref="IConfigurationProvider"/>s for this configuration.
        /// </summary>
        IEnumerable<IConfigurationProvider> Providers { get; }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=40)

`IConfigurationRoot` 接口暴露了一个 `Reload()` 方法,用于强制所有底层提供程序刷新它们的值。
它还暴露了 `Providers` 属性,让你可以查看在构建过程中注册的具体 `IConfigurationProvider` 实例。

基础的 `IConfiguration` 接口代表实际的键值属性集合。
它包含一个用于获取或设置值的索引器、用于获取节的方法,以及一个通过重新加载令牌(reload token)观察变更的机制。

```csharp
using Microsoft.Extensions.Primitives;

namespace Microsoft.Extensions.Configuration
{
    public interface IConfiguration
    {
        string? this[string key] { get; set; }

        IConfigurationSection GetSection(string key);

        IEnumerable<IConfigurationSection> GetChildren();

        IChangeToken GetReloadToken();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=100)

```text
IConfiguration
  this[key], GetSection(), GetChildren(), GetReloadToken()
      ^
      | : IConfiguration
      |
IConfigurationRoot
  Reload(), Providers
```

#### 层级键与分隔符

配置键使用冒号(`:`)作为分隔符来定义层级。
例如,名为 `Nested:Sub:Section` 的键表示一个三级关系(Grandparent:Parent:Child)。
这使得复杂的数据结构可以被扁平化为键值对,同时保持其逻辑上的组织结构。

```csharp
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

builder.AddInMemoryCollection(initialData: [
    new KeyValuePair<string, string?>("Key", value: "123 That was easy ... "),
    new KeyValuePair<string, string?>("Nested:Sub:Section", value: Math.PI.ToString()),
    new KeyValuePair<string, string?>("ConnectionStrings:Example", value: "<example-database-cs>")
]);

var config = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=160)

```text
(root)
 +-- Key                  = "123 That was easy ... "
 +-- Nested                              (Grandparent)
 |    +-- Sub                            (Parent)
 |         +-- Section    = Math.PI.ToString()   (Child)
 +-- ConnectionStrings
      +-- Example         = "<example-database-cs>"
```

#### 来源与提供程序

配置源与其提供程序之间是一对一的关系。
你可以在构建之前查看 `ConfigurationBuilder` 上的 `Sources` 属性,并在构建之后查看 `IConfigurationRoot` 上的 `Providers` 属性。

```csharp
foreach (var source in builder.Sources)
{
    Console.WriteLine($"Source: {source}");
}

// After building:
foreach (var provider in config.Providers)
{
    Console.WriteLine($"Provider: {provider}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=220)

#### 遍历配置

`GetChildren()` 方法只返回当前配置节点的直接后代。
要可视化或处理整棵树,必须使用递归。

```csharp
static void RecurseConfigSections(IConfiguration section, string? parentKey = null)
{
    foreach (var child in section.GetChildren())
    {
        var fullKey = parentKey is not null
            ? $"{parentKey}:{child.Key}"
            : child.Key;

        Console.WriteLine($"{fullKey}: {child.Value}");

        RecurseConfigSections(child, fullKey);
    }
}

RecurseConfigSections(config);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=290)

要访问配置树中的某个特定分支,请使用 `GetSection(key)`。
这个方法总是返回一个 `IConfigurationSection` 对象,即使该键不存在也是如此(这种情况下该节为空)。

```csharp
var section = config.GetSection("Nested:Sub:Section");

Console.WriteLine($"""
    Value for "{section.Path}":
      {section.Value}
    """);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=325)

#### 便捷 API 与调试

.NET 为常见的配置模式提供了便捷方法。
`GetConnectionString(name)` 是访问 `ConnectionStrings` 节下各个值的简写。

```csharp
var connectionString = config.GetConnectionString("Example");

Console.WriteLine($"Example connection string value: {connectionString}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=380)

最后,`GetDebugView()` 扩展方法会生成整个配置的一个人类可读的字符串。
它在调试时非常有用,可以准确看出某个值是由哪个提供程序提供的。
不过,它绝不应该在生产环境中使用,因为它可能会暴露机密信息。

```csharp
Console.WriteLine(config.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/explore-the-iconfiguration-and-related-apis-58112035/?t=405)

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112036/) · 1:21

### 总结

本课回顾 .NET 中配置的基础概念,强调配置如何让开发者在不修改源代码的情况下改变应用行为。
本课回顾了配置源与提供程序之间的区别,并回顾了在控制台应用中使用内存配置的初步实现。

### 核心概念

- 配置是一种在不修改源代码的情况下影响应用行为的工具。
- 配置源(数据的来源)与配置提供程序(读取数据的实现)之间的区别。
- 使用 `Microsoft.Extensions.Configuration` NuGet 包进行实现。
- 内存配置作为配置提供程序的一个基础示例。

### 课程笔记

本节建立了一个关于配置如何影响 .NET 应用行为的心智模型。
这种方式的一个主要好处是,无需更新或重新编译源代码就能调整应用的设置和逻辑。

本节对配置源和配置提供程序做了一个关键区分。
来源代表配置数据的位置或格式(例如文件或环境变量),而提供程序是负责读取这些数据并让应用可以使用它们的组件。

这些概念通过安装 `Microsoft.Extensions.Configuration` NuGet 包被应用到了一个 .NET 控制台应用中。
初步实现侧重于从内存来源中获取配置值。
虽然这些值出于演示目的是硬编码的,但它们为理解配置提供程序在 .NET 生态中如何工作提供了一个实践性的入门。
这些基础知识为更深入地探索更复杂的配置场景做好了准备。

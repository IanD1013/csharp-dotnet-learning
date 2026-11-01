# Implementing a Custom Configuration Provider

> 课程：[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 5 章
> 共 9 课 · 约 26:40
> 来源：Dometrain。由课程文档翻译整理；每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Implementing a Custom Configuration Provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-implementing-a-custom-configuration-provider-58112367/) | 1:31 | [↓](#1-introduction-to-implementing-a-custom-configuration-provider) |
| 2 | [Create the implementation class library](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/create-the-implementation-class-library-58112368/) | 1:35 | [↓](#2-create-the-implementation-class-library) |
| 3 | [Implement the IConfigurationSource](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationsource-58112369/) | 1:23 | [↓](#3-implement-the-iconfigurationsource) |
| 4 | [Implement the IConfigurationProvider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationprovider-58112370/) | 2:27 | [↓](#4-implement-the-iconfigurationprovider) |
| 5 | [Extend IConfigurationBuilder and expose AddYamlFile APIs](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/extend-iconfigurationbuilder-and-expose-addyamlfile-apis-58112371/) | 2:22 | [↓](#5-extend-iconfigurationbuilder-and-expose-addyamlfile-apis) |
| 6 | [Implementing the YAML parser](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/) | 9:35 | [↓](#6-implementing-the-yaml-parser) |
| 7 | [Review and Finalize implementation details](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/) | 2:11 | [↓](#7-review-and-finalize-implementation-details) |
| 8 | [Consuming YAML configuration with custom provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-yaml-configuration-with-custom-provider-58112374/) | 4:29 | [↓](#8-consuming-yaml-configuration-with-custom-provider) |
| 9 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112375/) | 1:07 | [↓](#9-section-recap) |

## 1. Introduction to Implementing a Custom Configuration Provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-implementing-a-custom-configuration-provider-58112367/) · 1:31

### 总结

本课介绍在 .NET 中实现自定义配置提供程序的过程，重点是创建一个基于 YAML 的提供程序。
虽然 Microsoft 提供了若干内置提供程序，但 YAML 这种在现代软件和基础设施中被广泛使用、流行且人类可读的格式并不是开箱即用的。
该实现借助 Microsoft.Extensions.Configuration.FileExtensions NuGet 包来利用现有的基于文件的配置抽象，同时探讨自定义实现所需的底层接口。

### 核心概念

- **自定义配置提供程序（Custom Configuration Providers）**：扩展 .NET 配置以支持非标准格式。
- **把 YAML 作为配置**：一种人类可读的 JSON 超集，被广泛用于自动化和基础设施。
- **File Extensions 包**：使用 `Microsoft.Extensions.Configuration.FileExtensions` 来简化基于文件的提供程序的开发。
- **接口实现**：理解自定义配置逻辑所需的核心接口。

### 课程笔记

本课重点是为 YAML 文件构建一个自定义配置提供程序。
尽管 .NET 提供了若干默认配置源，但 YAML 在软件配置、自动化和基础设施中如此普遍，却是标准库中的一个重大缺失。

为了简化基于文件的提供程序的开发，该实现使用了 `Microsoft.Extensions.Configuration.FileExtensions` NuGet 包。
这个包提供了处理常见文件相关问题的基类和抽象，例如监视文件变更以及从文件系统加载数据。
即使使用了这些辅助类，理解那些定义数据如何在 .NET 配置系统中被加载和访问的核心配置接口仍然至关重要。

从技术上讲，YAML 是 JSON 的超集，这意味着合法的 JSON 也是合法的 YAML。
它人类可读的特性和广泛的采用度使其成为自定义提供程序的理想候选。
这个实现的第一步是创建一个专门的类库来容纳提供程序逻辑。

## 2. Create the implementation class library

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/create-the-implementation-class-library-58112368/) · 1:35

### 总结

要在 .NET 中实现自定义配置提供程序，必须建立一个专门的类库，并引用必要的配置抽象和第三方解析库。
本课涵盖初始的项目搭建，包括选择构建基于 YAML 的配置源和提供程序所需的 NuGet 包。

### 核心概念

- **自定义配置提供程序架构**：需要实现 `IConfigurationSource` 和 `IConfigurationProvider` 接口。
- **依赖管理**：使用 `Microsoft.Extensions.Configuration.FileExtensions` 获取基类，使用 `YamlDotNet` 进行解析。
- **扩展方法**：通过 `IConfigurationBuilder` 扩展为使用者提供简洁的 API。
- **项目配置**：搭建一个带有合适命名空间和包引用的 .NET 8.0 类库。

### 课程笔记

实现自定义配置提供程序包含几个高层步骤。
第一，项目必须引用那些公开所需接口的抽象。
第二，你必须同时实现一个配置源和一个配置提供程序。
最后，这项功能应当通过 `IConfigurationBuilder` 类型上的扩展方法暴露给使用者。

实现从一个新的 .NET 类库项目开始。
项目文件定义了目标框架以及在 .NET 配置系统中处理 YAML 文件所需的依赖。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>DomeTrain.FromZeroToHero.Configuration.Yaml</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="8.0.1" />
    <PackageReference Include="YamlDotNet" Version="16.0.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/create-the-implementation-class-library-58112368/?t=40)

这个实现有两个必不可少的包引用：

1. **Microsoft.Extensions.Configuration.FileExtensions**：这个包提供了基于文件的提供程序所借助的配置抽象和抽象基类。
2. **YamlDotNet**：一个在 .NET 中用于序列化和反序列化 YAML 数据的第三方库。

项目结构和依赖就绪之后，下一步是实现 `IConfigurationSource` 接口。

## 3. Implement the IConfigurationSource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationsource-58112369/) · 1:23

### 总结

本课演示如何通过扩展标准 .NET 配置框架，为 YAML 文件实现一个自定义配置源。
它介绍了 IConfigurationSource 接口和 FileConfigurationSource 基类，后者为基于文件的配置提供了必要的基础设施，让开发者可以专注于构建配置提供程序的具体逻辑。

### 核心概念

- IConfigurationSource 接口定义了创建配置提供程序的契约。
- FileConfigurationSource 是一个抽象基类，所有标准的基于文件的提供程序（例如 JSON、XML）都使用它。
- Build 方法负责返回一个 IConfigurationProvider 实例。
- 在处理物理文件时，自定义源应当继承 FileConfigurationSource，以便受益于现有的文件处理逻辑。

### 课程笔记

IConfigurationSource 接口是向 IConfigurationBuilder 添加新配置源的入口点。
它只包含一个方法 Build，该方法接收一个 IConfigurationBuilder 并返回一个 IConfigurationProvider。

```csharp
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Represents a source of configuration key/values for an application.
    /// </summary>
    public interface IConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="IConfigurationProvider"/></returns>
        IConfigurationProvider Build(IConfigurationBuilder builder);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationsource-58112369/?t=10)

在实现一个从文件读取的提供程序时，推荐使用 FileConfigurationSource 抽象类。
这个类属于 file extensions NuGet 包，是所有标准文件配置提供程序的基类。
它处理诸如文件提供程序和文件路径之类的常见问题。

```csharp
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Represents a base class for file based <see cref="IConfigurationSource"/>
    /// </summary>
    public abstract class FileConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Used to access the contents of the file.
        /// </summary>
        public IFileProvider? FileProvider { get; set; }

        /// <summary>
        /// The path to the file.
        /// </summary>
        [DisallowNull]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationsource-58112369/?t=25)

要创建一个 YAML 专用的源，定义一个继承自 FileConfigurationSource 的类。
在这个实现中，必须重写 Build 方法以返回对应的提供程序。
在这个开发阶段，由于提供程序的实现尚未创建，该方法返回 default!。

```csharp
using Microsoft.Extensions.Configuration;

namespace DomeTrain.FromZeroToHero.Configuration.Yaml;

/// <summary>
/// Represents a YAML file as an <see cref="IConfigurationSource"/>.
/// </summary>
public sealed class YamlConfigurationSource
    : FileConfigurationSource
{
    /// <summary>
    /// Builds the <see cref="IConfigurationProvider"/> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
    /// <returns>A <see cref="IConfigurationProvider"/></returns>
    public override IConfigurationProvider Build(
        IConfigurationBuilder builder)
    {
        return default!;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationsource-58112369/?t=40)

```text
IConfigurationSource
  Build(IConfigurationBuilder) -> IConfigurationProvider
        ^
        |
FileConfigurationSource        (abstract; FileProvider, Path)
        ^
        |
YamlConfigurationSource        (sealed; override Build)
```

## 4. Implement the IConfigurationProvider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationprovider-58112370/) · 2:27

### 总结

本课演示如何通过扩展 .NET 配置框架的基类，为 YAML 文件实现一个自定义配置提供程序。
它详细说明了从 ConfigurationProvider 到 FileConfigurationProvider 的继承层次、实现 Load 方法以把文件流转换为内部的 Data 字典，以及把提供程序与其对应的 IConfigurationSource 集成起来。

### 核心概念

- **ConfigurationProvider**：.NET 中所有配置提供程序的抽象基类。
- **FileConfigurationProvider**：一个专用于基于文件的配置源的抽象类，负责处理文件加载和变更跟踪。
- **Data 字典**：内部的 `IDictionary<string, string?>`，提供程序在其中存储扁平化后的配置键和值。
- **流解析**：读取文件流，并把其内容（例如 YAML）映射到提供程序数据结构中的过程。
- **源与提供程序的关联**：在 `FileConfigurationSource` 中重写 `Build` 方法来实例化自定义提供程序。

### 课程笔记

在 .NET 中，自定义配置提供程序通常不会直接实现 `IConfigurationProvider` 接口。
相反，它们继承抽象的 `ConfigurationProvider` 类，该类提供了所有提供程序共享的基础实现，包括对内部数据字典的管理。

```csharp
namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Base helper class for implementing an <see cref="IConfigurationProvider"/>
    /// </summary>
    public abstract class ConfigurationProvider : IConfigurationProvider
    {
        private ConfigurationReloadToken _reloadToken = new ConfigurationReloadToken();

        /// <summary>
        /// Initializes a new <see cref="IConfigurationProvider"/>
        /// </summary>
        protected ConfigurationProvider()
        {
            Data = new Dictionary<string, string?>(comparer: StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// The configuration key value pairs for this provider.
        /// </summary>
        protected IDictionary<string, string?> Data { get; set; }

        // ... additional members for loading and retrieving values
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationprovider-58112370/?t=10)

对于基于文件的提供程序，框架还提供了另一层抽象：`FileConfigurationProvider`。
这个类位于 file extensions 包中，处理文件 I/O 的复杂性，并实现 `IDisposable` 来管理文件系统监视器。

```csharp
namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Base class for file based <see cref="ConfigurationProvider"/>.
    /// </summary>
    public abstract class FileConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private readonly IDisposable? _changeTokenRegistration;

        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings. </param>
        public FileConfigurationProvider(FileConfigurationSource source)
        {
            ThrowHelper.ThrowIfNull(source);

            Source = source;

            if (Source.ReloadOnChange && Source.FileProvider != null)
            {
                _changeTokenRegistration = ChangeToken.OnChange(
                    changeTokenProducer: () => Source.FileProvider.Watch(filter: Source.Path!),
                    changeTokenConsumer: () =>
                    {
                        Thread.Sleep(millisecondsTimeout: Source.ReloadDelay);
                        Load();
                    });
            }
        }

        public FileConfigurationSource Source { get; }

        public abstract void Load(Stream stream);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationprovider-58112370/?t=25)

`YamlConfigurationProvider` 被实现为一个继承自 `FileConfigurationProvider` 的 sealed 类。
它使用主构造函数来接收 `YamlConfigurationSource`。
这个类的主要职责是重写 `Load(Stream stream)` 方法。
框架提供文件流；提供程序必须解析这个流，并用得到的键和值填充 `Data` 字典。

```csharp
public sealed class YamlConfigurationProvider(
    YamlConfigurationSource source) : FileConfigurationProvider(source)
{
    /// <summary>
    /// Loads the YAML data from a stream.
    /// </summary>
    /// <param name="stream">The stream to read.</param>
    public override void Load(Stream stream)
    {
        try
        {
            // Implementation will parse the stream and populate Data
            Data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            throw new FormatException(
                message: "Unable to parse YAML.", innerException: ex);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationprovider-58112370/?t=70)

```text
IConfigurationProvider
        ^
        |
ConfigurationProvider          (abstract; Data dictionary)
        ^
        |
FileConfigurationProvider      (abstract; IDisposable; Load(Stream))
        ^
        |
YamlConfigurationProvider      (sealed; override Load(Stream))
```

最后，必须更新 `YamlConfigurationSource`，使其返回新提供程序的一个实例。
重写 `Build` 方法来实例化 `YamlConfigurationProvider`，并把 `this`（源实例）传给提供程序的构造函数。

```csharp
using Microsoft.Extensions.Configuration;

namespace DomeTrain.FromZeroToHero.Configuration.Yaml;

/// <summary>
/// Represents a YAML file as an <see cref="IConfigurationSource" />.
/// </summary>
public sealed class YamlConfigurationSource
    : FileConfigurationSource
{
    /// <summary>
    /// Builds the <see cref="YamlConfigurationProvider" /> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder" />.</param>
    /// <returns>A <see cref="YamlConfigurationProvider" /></returns>
    public override IConfigurationProvider Build(
        IConfigurationBuilder builder)
    {
        return new YamlConfigurationProvider(source: this);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implement-the-iconfigurationprovider-58112370/?t=135)

## 5. Extend IConfigurationBuilder and expose AddYamlFile APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/extend-iconfigurationbuilder-and-expose-addyamlfile-apis-58112371/) · 2:22

本课演示如何为 `IConfigurationBuilder` 创建扩展方法，以提供一个用于添加 YAML 配置文件的流式 API。
通过遵循内置的基于文件的提供程序的标准模式，该实现包含多个重载，用于处理文件路径、可选性以及变更时自动重新加载。
它还涵盖了 `IFileProvider` 的集成，以及使用 `YamlConfigurationSource` 来配置提供程序的行为，确保绝对路径和物理文件提供程序被正确解析。

### 核心概念

- **IConfigurationBuilder 扩展方法**：创建一个用于注册自定义配置提供程序的流式 API。
- **基于文件的提供程序模式**：为 `path`、`optional` 和 `reloadOnChange` 实现重载，以匹配标准的 .NET 配置行为。
- **IFileProvider 集成**：使用只读的文件提供程序抽象来访问文件内容。
- **参数校验**：在尝试添加源之前，确保 builder 和文件路径是有效的。
- **文件提供程序解析**：通过为最近的目录创建一个 `PhysicalFileProvider` 来处理绝对路径。

### 课程笔记

YAML 配置提供程序的实现遵循 .NET 中其他基于文件的提供程序所确立的模式。
这是通过创建一个静态类 `YamlConfigurationBuilderExtensions` 来实现的，它包含 `IConfigurationBuilder` 的 `AddYamlFile` 扩展方法的多个重载。

#### 基础重载

第一组重载允许使用者只指定路径，或者同时指定路径和一个 `optional` 标志来添加 YAML 文件。
如果 `optional` 设为 `false`，当指定路径下的文件缺失时，提供程序会抛出错误。

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace DomeTrain.FromZeroToHero.Configuration.Yaml;

/// <summary>
/// Extension methods for adding <see cref="YamlConfigurationProvider" />.
/// </summary>
public static class YamlConfigurationBuilderExtensions
{
    /// <summary> Adds the YAML configuration provider at path to builder. </summary>
    public static IConfigurationBuilder AddYamlFile(
        this IConfigurationBuilder builder,
        string path) =>
        AddYamlFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);

    /// <summary> Adds the YAML configuration provider at path to builder. </summary>
    public static IConfigurationBuilder AddYamlFile(
        this IConfigurationBuilder builder,
        string path,
        bool optional) =>
        AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/extend-iconfigurationbuilder-and-expose-addyamlfile-apis-58112371/?t=10)

#### 变更时重新加载

另一个重载包含 `reloadOnChange` 参数。
这让应用程序可以在执行期间底层 YAML 文件被更新时动态地重新加载配置。

```csharp
    /// <summary> Adds the YAML configuration provider at path to builder. </summary>
    public static IConfigurationBuilder AddYamlFile(
        this IConfigurationBuilder builder,
        string path,
        bool optional,
        bool reloadOnChange) =>
        AddYamlFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/extend-iconfigurationbuilder-and-expose-addyamlfile-apis-58112371/?t=25)

#### IFileProvider 与源配置

最全面的重载接收一个 `IFileProvider`。
这个只读抽象被映射到 `YamlConfigurationSource` 的 `FileProvider` 属性上。
这个方法负责校验参数，例如确保 builder 不为 null，以及路径不为空或空白。
随后它配置 `YamlConfigurationSource` 并将其添加到 builder 中。

```csharp
    /// <summary> Adds a Yaml configuration source to builder. </summary>
    public static IConfigurationBuilder AddYamlFile(
        this IConfigurationBuilder builder,
        IFileProvider? provider,
        string path,
        bool optional,
        bool reloadOnChange)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        return builder.AddYamlFile(configureSource: s =>
        {
            s.FileProvider = provider;
            s.Path = path;
            s.Optional = optional;
            s.ReloadOnChange = reloadOnChange;
            s.ResolveFileProvider();
        });
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/extend-iconfigurationbuilder-and-expose-addyamlfile-apis-58112371/?t=70)

如果没有显式传入文件提供程序，`ResolveFileProvider` 方法会处理传入绝对路径的情况，为最近的目录创建一个 `PhysicalFileProvider`。

#### 最终的注册重载

所有重载最终都会调用一个接收 `Action<YamlConfigurationSource>` 的最终版本。
正是这个方法真正把源添加到 `IConfigurationBuilder` 中。

```csharp
    /// <summary> Adds a Yaml configuration source to builder. </summary>
    public static IConfigurationBuilder AddYamlFile(
        this IConfigurationBuilder builder,
        Action<YamlConfigurationSource>? configureSource) =>
        builder.Add(configureSource);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/extend-iconfigurationbuilder-and-expose-addyamlfile-apis-58112371/?t=100)

```text
AddYamlFile(path)
AddYamlFile(path, optional)                    --+
AddYamlFile(path, optional, reloadOnChange)    --+
                                                 |
                                                 v
AddYamlFile(provider, path, optional, reloadOnChange)
  ThrowIfNull(builder), ThrowIfNullOrWhiteSpace(path)
  set FileProvider, Path, Optional, ReloadOnChange
  ResolveFileProvider()
                                                 |
                                                 v
AddYamlFile(Action<YamlConfigurationSource>)
  builder.Add(configureSource)
```

## 6. Implementing the YAML parser

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/) · 9:35

### 总结

本课介绍如何借助 YamlDotNet 库，为 .NET 配置提供程序实现一个自定义 YAML 解析器。
该实现受到官方 .NET JSON 配置提供程序内部结构的启发，使用一个有状态的访问者模式来遍历 YAML 节点（包括标量、序列和映射），并把它们扁平化为与 .NET 配置系统兼容的键值对字典。

### 核心概念

- 借助 YamlDotNet 进行流解析。
- 通过 source.dot.net 参考 Microsoft 的官方实现。
- 把层次化的 YAML 数据扁平化为一个扁平字典。
- 使用 `Stack<string>` 和 `ConfigurationPath.KeyDelimiter` 管理配置路径。
- 处理 YAML 节点类型：`YamlScalarNode`、`YamlSequenceNode` 和 `YamlMappingNode`。
- 为配置树实现递归的访问者模式。

### 课程笔记

自定义 YAML 配置提供程序依赖 `YamlDotNet` 库来处理 YAML 流的底层解析。
项目文件必须在标准配置扩展之外包含这个依赖。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>DomeTrain.FromZeroToHero.Configuration.Yaml</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="8.0.1" />
    <PackageReference Include="YamlDotNet" Version="16.0.0" />
  </ItemGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=10)

这个实现受到官方 .NET `JsonConfigurationProvider` 的启发，后者把解析委托给一个内部的 `JsonConfigurationFileParser` 类。
这种模式让提供程序类保持整洁，并封装了解析逻辑。

```csharp
public override void Load(Stream stream)
{
    try
    {
        Data = JsonConfigurationFileParser.Parse(stream);
    }
    catch (JsonException e)
    {
        throw new FormatException(SR.Error_JSONParseError, e);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=85)

遵循这种架构，`YamlConfigurationProvider` 被更新为调用一个自定义的 `YamlConfigurationFileParser`。

```csharp
public sealed class YamlConfigurationProvider(
    YamlConfigurationSource source) : FileConfigurationProvider(source)
{
    public override void Load(Stream stream)
    {
        try
        {
            Data = YamlConfigurationFileParser.Parse(input: stream);
        }
        catch (Exception ex)
        {
            throw new FormatException(
                message: "Unable to parse YAML.", innerException: ex);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=175)

`YamlConfigurationFileParser` 是一个 internal sealed 类，它在解析过程中维护状态：用一个 `Dictionary<string, string?>` 存放扁平化后的数据，用一个 `Stack<string>` 跟踪当前的配置路径。
`ParseStream` 方法初始化 `YamlStream`，并遍历 YAML 文件中的各个文档。

```csharp
private Dictionary<string, string?> ParseStream(Stream input)
{
    using TextReader reader = new StreamReader(input);

    var yaml = new YamlStream();
    yaml.Load(reader);

    foreach (YamlDocument document in yaml)
    {
        VisitMappingNode((YamlMappingNode)document.RootNode);
    }

    return _data;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=235)

解析器使用递归的访问者模式来遍历 YAML 树。
`VisitValue` 方法充当分发器，根据当前 `YamlNode` 的 `NodeType` 进行 switch。

```csharp
private void VisitValue(YamlNode value)
{
    Debug.Assert(_paths.Count > 0);

    _ = (value.NodeType, value) switch
    {
        (YamlNodeType.Scalar, YamlScalarNode scalar) => VisitScalarNode(scalar),
        (YamlNodeType.Sequence, YamlSequenceNode sequence) => VisitSequenceNode(sequence),
        (YamlNodeType.Mapping, YamlMappingNode mapping) => VisitMappingNode(mapping),

        _ => throw new NotSupportedException($"Node type {value.NodeType} is not supported.")
    };
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=415)

```text
ParseStream
  foreach YamlDocument -> VisitMappingNode(RootNode)
                               |
                               v
VisitValue(YamlNode) --switch on NodeType--+
   ^                                       |
   |    Scalar   -> VisitScalarNode   -> _data[key] = node.Value
   |    Sequence -> VisitSequenceNode -+
   |    Mapping  -> VisitMappingNode  -+
   |                                   |
   +------- each child: EnterContext / VisitValue / ExitContext
```

路径管理由 `EnterContext` 和 `ExitContext` 负责。
`EnterContext` 向栈中压入一个新的片段；如果已经存在父路径，就使用 `ConfigurationPath.KeyDelimiter`（冒号字符）把片段连接起来。

```csharp
private void EnterContext(string context) =>
    _paths.Push(_paths.Count > 0
        ? $"{_paths.Peek()}{ConfigurationPath.KeyDelimiter}{context}"
        : context);

private void ExitContext() => _paths.Pop();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=325)

在访问 `YamlMappingNode` 时，解析器遍历它的子节点。
它进入键的上下文，递归地访问值，然后退出该上下文。

```csharp
private bool VisitMappingNode(YamlMappingNode node)
{
    var isEmpty = true;

    foreach (var (key, value) in node.Children)
    {
        isEmpty = false;

        EnterContext(((YamlScalarNode)key).Value!);
        VisitValue(value);
        ExitContext();
    }

    SetNullIfElementIsEmpty(isEmpty);

    return isEmpty;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=400)

对于 `YamlScalarNode` 元素，解析器从栈中取出当前路径。
它确保键在字典中是唯一的，遇到重复时抛出 `FormatException`。

```csharp
private bool VisitScalarNode(YamlScalarNode node)
{
    string key = _paths.Peek();

    if (_data.ContainsKey(key))
    {
        throw new FormatException($"""
            The "{key}" key is duplicated!
            """);
    }

    if (!string.IsNullOrWhiteSpace(node.Value))
    {
        _data[key] = node.Value;
    }

    return true;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=475)

`YamlSequenceNode` 元素表示数组。
解析器遍历其子节点，使用从零开始的索引作为每个子节点的上下文，以保持数组正确的配置路径结构。

```csharp
private bool VisitSequenceNode(YamlSequenceNode node)
{
    var index = 0;

    foreach (YamlNode child in node.Children)
    {
        EnterContext(index.ToString());
        VisitValue(child);
        ExitContext();

        ++index;
    }

    SetNullIfElementIsEmpty(index is 0);

    return index is 0;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=505)

如果一个映射或序列为空，解析器会显式地在字典中把该路径的值设为 `null`，以表示一个空的配置节。

```csharp
private void SetNullIfElementIsEmpty(bool isEmpty)
{
    if (isEmpty && _paths.Count > 0)
    {
        _data[_paths.Peek()] = null;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/implementing-the-yaml-parser-58112372/?t=550)

## 7. Review and Finalize implementation details

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/) · 2:11

本课重点是收尾自定义 YAML 配置提供程序的实现，确保它遵循 .NET 配置框架所确立的标准模式。
通过审视 JsonConfigurationSource 的内部机制，本课强调了 FileConfigurationSource 基类中 EnsureDefaults 方法的重要性，它保证在构建提供程序之前，文件提供程序和异常处理器等关键属性已被正确初始化。

### 核心概念

- **标准化**：让自定义提供程序与内置的 .NET 提供程序（例如 JSON）保持一致，以获得一致的行为。
- **FileConfigurationSource**：基于文件的配置源的基类，提供 Path、Optional 和 ReloadOnChange 等属性。
- **EnsureDefaults**：一个用于从 IConfigurationBuilder 填充配置源缺失属性的方法。
- **IFileProvider**：用于访问文件内容的抽象，必须被初始化，提供程序才能工作。

### 课程笔记

自定义 YAML 提供程序的实现由一个配置源、一个配置提供程序、一个解析器和若干扩展方法组成。
解析器负责把 YAML 流转换为 .NET 配置所需的扁平字典格式。

```csharp
internal sealed class YamlConfigurationFileParser
{
    private readonly Dictionary<string, string> _data =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    private readonly Stack<string> _paths = new Stack<string>();

    public static IDictionary<string, string?> Parse(Stream input) =>
        new YamlConfigurationFileParser().ParseStream(input);

    private Dictionary<string, string?> ParseStream(Stream input)
    {
        using TextReader reader = new StreamReader(input);

        var yaml = new YamlStream();
        yaml.Load(reader);

        foreach (YamlDocument document in yaml)
        {
            VisitMappingNode((YamlMappingNode)document.RootNode);
        }

        return _data;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/?t=10)

为了确保自定义提供程序足够健壮，参考 `JsonConfigurationSource` 的参考实现会很有帮助。
在标准的 .NET 实现中，`Build` 方法在返回提供程序之前会调用 `EnsureDefaults(builder)`。

```csharp
using System;

namespace Microsoft.Extensions.Configuration.Json
{
    /// <summary>
    /// Represents a JSON file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class JsonConfigurationSource : FileConfigurationSource
    {
        /// <summary>
        /// Builds the <see="JsonConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see="IConfigurationBuilder"/>.</param>
        /// <returns>A <see="JsonConfigurationProvider"/></returns>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new JsonConfigurationProvider(this);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/?t=25)

`EnsureDefaults` 方法定义在 `FileConfigurationSource` 基类中。
它确保：如果源上没有显式设置 `FileProvider` 或 `OnLoadException` 处理器，就从 `IConfigurationBuilder` 中获取它们。
这一点至关重要，因为访问文件系统需要 `FileProvider`。

```csharp
public abstract IConfigurationProvider Build(IConfigurationBuilder builder);

/// <summary>
/// Called to use any default settings on the builder like the FileProvider or FileLoadExceptionHandler
/// </summary>
/// <param name="builder">The <see="IConfigurationBuilder"/>.</param>
public void EnsureDefaults(IConfigurationBuilder builder)
{
    FileProvider ??= builder.GetFileProvider();
    OnLoadException ??= builder.GetFileLoadExceptionHandler();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/?t=55)

`FileConfigurationSource` 提供了基于文件的配置所必需的标准属性，包括 `Path`、`Optional` 和 `ReloadOnChange`。

```csharp
public abstract class FileConfigurationSource : IConfigurationSource
{
    /// <summary>
    /// Used to access the contents of the file.
    /// </summary>
    public IFileProvider? FileProvider { get; set; }

    /// <summary>
    /// The path to the file.
    /// </summary>
    [DisallowNull]
    public string? Path { get; set; }

    /// <summary>
    /// Determines if loading the file is optional.
    /// </summary>
    public bool Optional { get; set; }

    /// <summary>
    /// Determines whether the source will be loaded if the underlying file changes.
    /// </summary>
    public bool ReloadOnChange { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/?t=70)

为了收尾 `YamlConfigurationSource`，必须更新 `Build` 方法，加入对 `EnsureDefaults(builder)` 的调用。
这确保自定义 YAML 提供程序能够访问与内置提供程序相同的基础设施（比如物理文件提供程序）。

```csharp
using Microsoft.Extensions.Configuration;

namespace DomeTrain.FromZeroToHero.Configuration.Yaml;

/// <summary>
/// Represents a YAML file as an <see="IConfigurationSource"/>.
/// </summary>
public sealed class YamlConfigurationSource
    : FileConfigurationSource
{
    /// <summary>
    /// Builds the <see="YamlConfigurationProvider"/> for this source.
    /// </summary>
    /// <param name="builder">The <see="IConfigurationBuilder"/>.</param>
    /// <returns>A <see="YamlConfigurationProvider"/></returns>
    public override IConfigurationProvider Build(
        IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);

        return new YamlConfigurationProvider(this);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/review-and-finalize-implementation-details-58112373/?t=100)

完成这些改动之后，YAML 配置提供程序就完全实现了，可以被应用程序使用。

## 8. Consuming YAML configuration with custom provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-yaml-configuration-with-custom-provider-58112374/) · 4:29

本课演示如何在 .NET Worker Service 中集成并使用自定义 YAML 配置提供程序。
它涵盖了清除默认配置源以确保隔离、注册带有环境特定覆盖的 YAML 文件，以及利用 `reloadOnChange` 特性在运行时动态更新应用程序设置而无需重启的过程。

### 核心概念

- **自定义提供程序集成**：通过引用自定义配置提供程序的项目和命名空间来使用它。
- **源隔离**：使用 `builder.Configuration.Sources.Clear()` 移除默认提供程序（JSON、环境变量等），确保只有自定义提供程序处于活动状态。
- **配置注册**：使用 `AddYamlFile` 等扩展方法来指定文件路径、可选性和重新加载行为。
- **环境特定配置**：根据当前环境动态加载配置文件（例如 `app.Development.yaml`）。
- **热重载**：借助底层的文件系统监视，在源文件被修改时实时更新 `IConfiguration` 的值。

### 课程笔记

要使用自定义 YAML 配置提供程序，应用程序需要一个指向该提供程序库的项目引用。
这样就可以使用包含配置扩展方法的那个特定命名空间。

在应用程序入口点中，默认配置源被清除，以消除来自其他配置提供程序的干扰。
然后显式地添加 YAML 提供程序。
`AddYamlFile` 方法被用来同时注册一个基础配置文件和一个环境特定的覆盖文件。
通过设置 `reloadOnChange: true`，应用程序会自动检测磁盘上 YAML 文件的变更，并在不需要重启进程的情况下更新配置值。

```csharp
using DomeTrain.FromZeroToHero.Configuration.Yaml;
using Yaml.Config;

var builder = Host.CreateApplicationBuilder(args);

// Clear out all the default config sources.
builder.Configuration.Sources.Clear();

builder.Configuration.AddYamlFile(
    path: "app.yaml",
    optional: false,
    reloadOnChange: true);

builder.Configuration.AddYamlFile(
    path: $"app.{builder.Environment.EnvironmentName}.yaml",
    optional: false,
    reloadOnChange: true);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-yaml-configuration-with-custom-provider-58112374/?t=10)

配置值定义在 YAML 文件中。
基础的 `app.yaml` 文件包含日志的默认设置和一个 15 秒的延迟。

```yaml
Logging:
  LogLevel:
    Default: Information
    Microsoft.Hosting.Lifetime: Warning
Delay: '0:0:15'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-yaml-configuration-with-custom-provider-58112374/?t=100)

环境特定的文件 `app.Development.yaml` 覆盖了这些值，为 hosting lifetime 设置了更详细的日志级别，并设置了更短的 3 秒延迟。

```yaml
Logging:
  LogLevel:
    Default: Information
    Microsoft.Hosting.Lifetime: Debug
Delay: '0:0:03'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-yaml-configuration-with-custom-provider-58112374/?t=115)

```text
Sources.Clear()
      |
      v
app.yaml                    Delay: '0:0:15'   Microsoft.Hosting.Lifetime: Warning
      |  overridden by
      v
app.Development.yaml        Delay: '0:0:03'   Microsoft.Hosting.Lifetime: Debug
      |
      v
IConfiguration  -->  Worker: config.GetValue<TimeSpan>("Delay", ...)
```

`Worker` 服务通过注入 `IConfiguration` 来使用这些设置。
在执行循环内部，使用 `config.GetValue<TimeSpan>` 获取 `Delay` 值。
由于提供程序支持重新加载，应用程序运行期间对 YAML 文件所做的任何更改，都会在循环的下一次迭代中立即反映到 `delay` 变量上。

```csharp
namespace Yaml.Config;

public sealed class Worker(
    ILogger<Worker> logger,
    IConfiguration config) : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var delay =
                config.GetValue<TimeSpan>(
                    "Delay",
                    TimeSpan.FromSeconds(5));

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Worker running at ({delay}): {time}",
                    delay, DateTimeOffset.Now);
            }

            await Task.Delay(
                delay, stoppingToken);
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-yaml-configuration-with-custom-provider-58112374/?t=145)

当应用程序运行时，修改 YAML 文件中的 `Delay` 值并保存，会触发文件系统监视器。
配置提供程序重新加载数据，正在运行的 worker 会立即采用新的延迟间隔，而无需重启。

## 9. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112375/) · 1:07

### 总结

本课回顾在 .NET 中构建自定义配置提供程序的过程，具体针对 YAML 文件。
它涵盖了 IConfigurationSource 和 IConfigurationProvider 等核心抽象的实现、为便于集成而创建的扩展方法，以及使用第三方库进行解析。
由此得到的库让应用程序能够使用 YAML 配置文件，并支持环境特定的覆盖以及通过 reloadOnChange 机制进行实时更新。

### 核心概念

- 实现 `IConfigurationSource` 来定义配置源。
- 实现 `IConfigurationProvider` 来处理数据的加载和解析。
- 使用扩展方法提供一个用于添加配置源的流式 API。
- 集成第三方库来解析特定格式（例如 YAML）。
- 通过 `reloadOnChange` 支持动态配置更新。
- 把自定义提供程序打包成可复用的类库。

### 课程笔记

实现自定义配置提供程序需要使用 .NET 配置系统提供的抽象。
这个过程通常包括实现 `IConfigurationSource` 和 `IConfigurationProvider` 接口。
源作为入口点，而提供程序包含解析底层数据（例如 YAML）并将其映射到 .NET 所使用的标准键值对结构中的逻辑。

为了简化这些提供程序的使用，标准做法是通过 `IConfigurationBuilder` 上的扩展方法来暴露它们。
这让提供程序可以无缝地集成到应用程序的启动过程中。
通过借助针对特定文件格式的第三方库，开发者可以创建健壮、可复用的类库，在多个项目之间共享，甚至发布为 NuGet 包。

在典型的实现中，可以把提供程序配置为监视文件变更。
启用 `reloadOnChange` 后，应用程序可以实时响应配置更新，而无需重启。
下面的示例演示如何清除默认配置源，并注册一个支持环境特定覆盖的自定义 YAML 提供程序。

```csharp
using DomeTrain.FromZeroToHero.Configuration.Yaml;
using Yaml.Config;

var builder = Host.CreateApplicationBuilder(args);

// Clear out all the default config sources.
builder.Configuration.Sources.Clear();

builder.Configuration.AddYamlFile(
    path: "app.yaml",
    optional: false,
    reloadOnChange: true);

builder.Configuration.AddYamlFile(
    path: $"app.{builder.Environment.EnvironmentName}.yaml",
    optional: false,
    reloadOnChange: true);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112375/?t=10)

这种方式确保应用程序保持灵活，能够高效地适应不同的环境和运行时变更。
接下来，课程将过渡到选项模式（options pattern），它建立在这些配置基础之上。

# Configuration Providers

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 4 章
> 共 14 课 · 约 34:39
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Configuration Providers](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-configuration-providers-58112303/) | 0:37 | [↓](#1-introduction-to-configuration-providers) |
| 2 | [Base File Providers](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/base-file-providers-58112304/) | 0:47 | [↓](#2-base-file-providers) |
| 3 | [Discussing the empty application builder sample app](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/discussing-the-empty-application-builder-sample-app-58112305/) | 1:12 | [↓](#3-discussing-the-empty-application-builder-sample-app) |
| 4 | [JSON configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/json-configuration-provider-58112306/) | 3:00 | [↓](#4-json-configuration-provider) |
| 5 | [XML configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/xml-configuration-provider-58112307/) | 1:51 | [↓](#5-xml-configuration-provider) |
| 6 | [INI configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/ini-configuration-provider-58112308/) | 1:48 | [↓](#6-ini-configuration-provider) |
| 7 | [User Secrets configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/user-secrets-configuration-provider-58112309/) | 1:36 | [↓](#7-user-secrets-configuration-provider) |
| 8 | [Managing user secrets](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/) | 6:36 | [↓](#8-managing-user-secrets) |
| 9 | [Environment Variables configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-variables-configuration-provider-58112311/) | 2:31 | [↓](#9-environment-variables-configuration-provider) |
| 10 | [Command-line configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/) | 3:31 | [↓](#10-command-line-configuration-provider) |
| 11 | [Key-per file configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/key-per-file-configuration-provider-58112313/) | 2:25 | [↓](#11-key-per-file-configuration-provider) |
| 12 | [Azure Key Vault configuration provider](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-key-vault-configuration-provider-58112314/) | 4:10 | [↓](#12-azure-key-vault-configuration-provider) |
| 13 | [Azure App Configuration provider with Key Vault secrets](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-app-configuration-provider-with-key-vault-secrets-58112315/) | 3:36 | [↓](#13-azure-app-configuration-provider-with-key-vault-secrets) |
| 14 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112317/) | 0:59 | [↓](#14-section-recap) |

## 1. Introduction to Configuration Providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-configuration-providers-58112303/) · 0:37

### 总结

本课介绍 .NET 中配置提供程序(configuration provider)的概念,从高层定义一路讲到具体的实现 API。
它概述了框架支持的各种来源,包括基于文件的提供程序(JSON、XML、INI)、User Secrets 和环境变量这类特定于环境的来源,以及 Azure Key Vault 这样的外部集成。

### 核心概念

- 配置提供程序 API,以及添加来源的常见模式。
- 基于文件的配置支持(JSON、XML 和 INI)。
- 特定于环境的配置(User Secrets 和环境变量)。
- 命令行参数和 Key-per-file 提供程序。
- 通过 Azure Key Vault 实现的外部云配置。

### 课程笔记

配置提供程序是 .NET 中把来自不同来源的设置加载进统一配置系统的基础机制。
这一部分聚焦于在应用程序中指定这些配置来源时所用的具体 API 和常见模式。

.NET 生态提供了大量内置提供程序。
其中包括支持 JSON、XML 和 INI 等标准格式的文件配置提供程序。
除了本地文件,系统还支持特定于环境和运行时的来源,包括环境变量、命令行参数以及 User Secrets(主要在开发期间使用)。

对于更专门或更注重安全的场景,.NET 提供了 Key-per-file(常用于 Docker/Kubernetes 环境)以及 Azure Key Vault 等基于云的方案的提供程序。

要查看配置的当前状态,以及哪些值是由哪些来源提供的,`GetDebugView()` 方法是一个很有价值的工具。
可以通过创建一个 `HostApplicationBuilder` 并把配置状态输出到控制台来演示这一点。

```csharp
var builder = Host.CreateEmptyApplicationBuilder(settings: null);

// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-configuration-providers-58112303/?t=10)

## 2. Base File Providers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/base-file-providers-58112304/) · 0:47

### 总结

本课介绍 .NET 中基础的基于文件的配置提供程序,它们用于读取、解析并理解来自 JSON、XML 和 INI 文件的配置数据。
本课强调,虽然每个提供程序处理一种特定的文件格式,但它们都共享同一个底层基础实现,并通过特定的 NuGet 包和配置来源注册集成到应用程序中。

### 核心概念

- 支持 JSON、XML 和 INI 文件格式。
- 所有实现共享同一个底层基础文件提供程序类型。
- 模块化架构,每个提供程序通过各自的 NuGet 包分发。
- 注册配置来源以启用文件读取和解析。

### 课程笔记

.NET 提供了一套专门用于处理基于文件的设置的配置提供程序。
这些提供程序能够从三种主要格式读取并解析配置数据:JSON、XML 和 INI。

虽然每个提供程序专门针对一种文件类型,但它们都构建在同一个底层基础文件提供程序实现之上。
这一共享基础确保了不同文件来源之间行为的一致性。
在实践中,这些实现通过各自独立的 NuGet 包交付。
开发者通过向配置构建器添加相应的配置来源,把这些能力纳入自己的应用程序。

```text
        base file provider (shared)
                  |
     +------------+------------+
     |            |            |
   JSON          XML          INI
```

## 3. Discussing the empty application builder sample app

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/discussing-the-empty-application-builder-sample-app-58112305/) · 1:12

### 总结

本课介绍一个示例控制台应用程序,用来演示 .NET 中的各种配置提供程序。
通过使用空的应用程序构建器,应用程序启动时没有任何预配置的默认值,从而提供一张白纸,可以手动添加并检查 JSON、XML 和环境变量等配置来源。
这种做法可以清晰地直观展示不同提供程序是如何为最终的配置对象做出贡献的。

### 核心概念

* **Host.CreateEmptyApplicationBuilder**:创建一个没有任何默认配置来源或环境设置的主机构建器。
* **IConfigurationManager**:通过 `Configuration` 属性访问,用于手动注册配置提供程序。
* **配置可视化**:使用 `GetDebugView()` 输出配置值的层次结构及其来源。
* **提供程序包**:必须引入特定的 NuGet 包(例如 XML、INI、Azure Key Vault)来扩展配置能力。

### 课程笔记

该示例应用程序是一个 .NET 控制台项目,配置了多个依赖项,以支持各种各样的配置来源。
项目文件包含了用于标准文件格式和基于云的机密管理的包。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Azure.Extensions.AspNetCore.Configuration.Secrets" Version="1.3.1" />
    <PackageReference Include="Azure.Identity" Version="1.12.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Ini" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.KeyPerFile" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Xml" Version="8.0.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/discussing-the-empty-application-builder-sample-app-58112305/?t=25)

该应用程序没有使用标准的 `Host.CreateApplicationBuilder()`(它会加载若干默认来源,例如 `appsettings.json`、环境变量和命令行参数),而是使用 `Host.CreateEmptyApplicationBuilder(settings: null)`。
这个方法确保 `builder.Configuration` 对象一开始是空的,不包含任何键或值。
这对教学很有用,可以准确看到每个提供程序向系统添加了什么。

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

// We're using an empty builder that has no pre-configured defaults.
var builder = Host.CreateEmptyApplicationBuilder(settings: null);

// File Configuration Providers

// Environment Variables ($env)

// Command-Line Args (-a)

// Key-per file (/key)

// Azure Key Vault (k=v)

// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/discussing-the-empty-application-builder-sample-app-58112305/?t=10)

`HostApplicationBuilder` 提供了对 `Configuration` 属性的访问,该属性是一个 `IConfigurationManager`。
这使得可以在应用程序构建之前手动注册各种配置来源。
此外,构建器还公开了一个 `Environment` 属性,用于管理特定于环境的逻辑。
为了检查配置的最终状态,应用程序调用 `builder.Configuration.GetDebugView()`。
这个工具方法会生成整个配置树的字符串表示,标明每个具体的值是由哪个提供程序提供的,然后把它打印到控制台以供检查。

## 4. JSON configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/json-configuration-provider-58112306/) · 3:00

### 总结

JSON 配置提供程序是 .NET 配置系统的基础组件,它让应用程序能够通过 Microsoft.Extensions.Configuration.Json 包从 JSON 文件加载设置。
它提供了 AddJsonFile 扩展方法,该方法支持一些关键特性,例如把配置文件设为可选以防止启动失败,以及启用 reloadOnChange,以便在磁盘上的文件被修改时热重载设置。
此外,它还可以利用主机的环境名称动态构造文件路径,从而实现特定于环境的配置。

### 核心概念

* `Microsoft.Extensions.Configuration.Json` NuGet 包
* `AddJsonFile` 扩展方法
* 可选与必需的配置文件
* 使用 `reloadOnChange` 实现热重载
* 特定于环境的配置覆盖

### 课程笔记

JSON 配置提供程序是 .NET 应用程序中最常用的提供程序。
它属于 `Microsoft.Extensions.Configuration.Json` 包,让开发者可以用结构化的 JSON 格式定义设置。
要使用它,需要在 `builder.Configuration` 对象上使用 `AddJsonFile` 方法注册文件。

```csharp
var builder =
    Host.CreateEmptyApplicationBuilder(settings: null);

#region File Configuration Providers
#region JSON {;}

// Microsoft.Extensions.Configuration.Json

// For example: appsettings.json
builder.Configuration.AddJsonFile(
    path: "appsettings.json",
    optional: true,
    reloadOnChange: true);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/json-configuration-provider-58112306/?t=25)

`AddJsonFile` 方法接受若干参数,用来控制文件的加载方式:

* **path**:JSON 文件的字符串路径(例如 "appsettings.json")。
* **optional**:一个布尔值。如果设为 `false`,当文件缺失时应用程序会在运行时抛出异常。如果为 `true`,应用程序会忽略缺失的文件并继续启动。
* **reloadOnChange**:一个布尔值,用于启用文件系统监视器。如果磁盘上的文件被更新,配置系统会自动把这些值重新加载进应用程序,而无需重启进程。

#### 特定于环境的配置

为了在不同环境(例如 Development、Staging 或 Production)之间支持不同的设置,可以根据主机构建器的环境属性所公开的环境名称,有条件地加载 JSON 文件。

```csharp
// For example: appsettings.Staging.json
builder.Configuration.AddJsonFile(
    path: $"appsettings.{builder.Environment.EnvironmentName}.json",
    optional: true,
    reloadOnChange: true);

#endregion
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/json-configuration-provider-58112306/?t=70)

#### JSON 文件结构

配置文件通常包含嵌套对象。
配置系统会把它们扁平化为键值结构,并使用冒号(`:`)作为层级分隔符。
一个标准的 `appsettings.json` 可能是这样的:

```json
{
  "Delay": "0:0:05",
  "TodoApiOptions": {
    "Enabled": true,
    "BaseAddress": "https://jsonplaceholder.typicode.com/todos"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Warning"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/json-configuration-provider-58112306/?t=130)

#### 调试配置

查看配置的调试输出时,可以准确看到每个值是由哪个提供程序负责的。
从 JSON 文件加载的值会标注 `JsonConfigurationProvider` 以及它们来源的具体文件名。

```plaintext
Delay=0:0:05 (JsonConfigurationProvider for 'appsettings.json' (Optional))
Logging:
  LogLevel:
    Default=Information (JsonConfigurationProvider for 'appsettings.json' (Optional))
    Microsoft.Hosting.Lifetime=Warning (JsonConfigurationProvider for 'appsettings.json' (Optional))
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (JsonConfigurationProvider for 'appsettings.json' (Optional))
  Enabled=True (JsonConfigurationProvider for 'appsettings.json' (Optional))
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/json-configuration-provider-58112306/?t=160)

## 5. XML configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/xml-configuration-provider-58112307/) · 1:51

.NET 中的 XML 配置提供程序通过 Microsoft.Extensions.Configuration.Xml 包,把基于 XML 的设置集成进配置系统。
它沿用了 JSON 配置提供程序的设计,为指定文件路径、处理可选文件以及在源文件变化时启用自动重载提供了一致的 API。
这个提供程序让开发者能以结构化的 XML 格式维护配置,该格式直接映射到应用程序的配置模型,既支持简单的键值对,也支持复杂的嵌套节。

### 核心概念

- **NuGet 包**:该提供程序通过 `Microsoft.Extensions.Configuration.Xml` 包提供。
- **扩展方法**:使用 `AddXmlFile` 向配置构建器注册 XML 来源。
- **一致性**:XML 的 API 与 JSON 和 INI 提供程序几乎相同,遵循基于文件的配置的既定模式。
- **配置映射**:XML 元素和嵌套层级直接转换为配置键和节(例如 `<Section><Key>Value</Key></Section>` 会变成 `Section:Key`)。
- **灵活性**:支持特定于环境的命名约定、可选文件加载以及 `reloadOnChange` 功能。

### 课程笔记

XML 配置提供程序通过 `Microsoft.Extensions.Configuration.Xml` NuGet 包提供。
它的实现模式与 JSON 配置提供程序几乎相同,因为两者都遵循基于文件的配置来源的标准做法。
要把 XML 文件注册为配置来源,请在 `builder.Configuration` 对象上使用 `AddXmlFile` 扩展方法。
这个方法允许你指定文件路径,并配置该文件是否可选,以及文件被修改时应用程序是否应重新加载设置。

```csharp
#region XML </>

// Microsoft.Extensions.Configuration.Xml

// For example: config.xml
builder.Configuration.AddXmlFile(
    path: "config.xml",
    optional: true,
    reloadOnChange: true);

// For example: config.Staging.xml
builder.Configuration.AddXmlFile(
    path: $"config.{builder.Environment.EnvironmentName}.xml",
    optional: true,
    reloadOnChange: true);

#endregion
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/xml-configuration-provider-58112307/?t=10)

XML 文件的结构定义了配置键的层次结构。
XML 中的元素映射为配置节和值。
例如,一个包含 `<Enabled>` 元素的 `<TodoApiOptions>` 元素会产生配置键路径 `TodoApiOptions:Enabled`。
虽然格式是 XML,但配置值和层次结构与其他提供程序保持一致。
文件名和路径完全由开发者自行决定。

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <Delay>0:0:05</Delay>
  <TodoApiOptions>
    <Enabled>true</Enabled>
    <BaseAddress>https://jsonplaceholder.typicode.com/todos</BaseAddress>
  </TodoApiOptions>
  <Logging>
    <LogLevel>
      <Default>Information</Default>
      <Microsoft.Hosting.Lifetime>Warning</Microsoft.Hosting.Lifetime>
    </LogLevel>
  </Logging>
</configuration>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/xml-configuration-provider-58112307/?t=55)

应用程序运行时,`XmlConfigurationProvider` 会解析该文件并填充配置对象。
下面的输出展示了该提供程序成功从 `config.xml` 文件加载了延迟、日志级别和 API 选项的值,同时标明了每项设置的来源和可选状态。

```text
Delay=0:0:05 (XmlConfigurationProvider for 'config.xml' (Optional))
Logging:
  LogLevel:
    Default=Information (XmlConfigurationProvider for 'config.xml' (Optional))
    Microsoft.Hosting.Lifetime=Warning (XmlConfigurationProvider for 'config.xml' (Optional))
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (XmlConfigurationProvider for 'config.xml' (Optional))
  Enabled=true (XmlConfigurationProvider for 'config.xml' (Optional))
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/xml-configuration-provider-58112307/?t=95)

## 6. INI configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/ini-configuration-provider-58112308/) · 1:48

### 总结

.NET 中的 INI 配置提供程序让应用程序可以使用 `Microsoft.Extensions.Configuration.Ini` 包从 INI 文件加载设置。
它支持标准的配置特性,例如通过节标题实现的层级键、特定于环境的文件覆盖,以及把文件标记为可选或设置为在磁盘上发生变化时自动重载。

### 核心概念

- `Microsoft.Extensions.Configuration.Ini` 包:支持 INI 所需的 NuGet 包。
- `AddIniFile` 方法:用于向配置构建器注册 INI 文件的扩展方法。
- 节标题:使用 `[SectionName]` 对相关设置进行分组。
- 层级键:支持在键或节标题中使用冒号(`:`)分隔符进行嵌套配置。
- 文件选项:支持 `optional` 和 `reloadOnChange` 参数,以实现灵活的配置管理。

### 课程笔记

INI 配置提供程序是一个基于文件的提供程序,让 .NET 应用程序可以从传统的 INI 文件读取设置。
要使用这个提供程序,必须引用 `Microsoft.Extensions.Configuration.Ini` 包。

配置是使用 `AddIniFile` 扩展方法添加到 `ConfigurationManager`(或 `ConfigurationBuilder`)的。
这个方法允许指定文件路径,并配置一些行为,例如该文件是否是应用程序启动所必需的,以及应用程序是否应监视该文件的变化。
常见做法是同时添加一个基础配置文件和一个特定于环境的覆盖文件。

```csharp
#region INI [=]

// Microsoft.Extensions.Configuration.Ini

// For example: app.ini
builder.Configuration.AddIniFile(
    path: "app.ini",
    optional: true,
    reloadOnChange: true);

// For example: app.Development.ini
builder.Configuration.AddIniFile(
    path: $"app.{builder.Environment.EnvironmentName}.ini",
    optional: true,
    reloadOnChange: true);

#endregion
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/ini-configuration-provider-58112308/?t=10)

INI 文件由键值对构成,并支持用方括号把设置分组为节。
在这些节内定义键。
.NET 配置中的层级路径是从 INI 节映射而来的;例如,`[TodoApiOptions]` 节下的键 `Enabled` 通过 `TodoApiOptions:Enabled` 访问。
也可以直接在节标题中使用冒号分隔符来定义更深的层级。

```ini
Delay="0:0:05"

[TodoApiOptions]
Enabled=True
BaseAddress="https://jsonplaceholder.typicode.com/todos"

[Logging:LogLevel]
Default=Information
Microsoft.Hosting.Lifetime=Warning
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/ini-configuration-provider-58112308/?t=55)

应用程序运行时,`IniConfigurationProvider` 会处理该文件并把它整合进配置树。
这可以通过在运行时检查配置提供程序来验证,其中每个值的来源都被标识为针对该具体文件的 `IniConfigurationProvider`。

```text
Delay=0:0:05 (IniConfigurationProvider for 'app.ini' (Optional))
Logging:
  LogLevel:
    Default=Information (IniConfigurationProvider for 'app.ini' (Optional))
    Microsoft.Hosting.Lifetime=Warning (IniConfigurationProvider for 'app.ini' (Optional))
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (IniConfigurationProvider for 'app.ini' (Optional))
  Enabled=True (IniConfigurationProvider for 'app.ini' (Optional))
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/ini-configuration-provider-58112308/?t=90)

对于偏好或需要使用 INI 格式进行基于文件的应用程序配置的开发者来说,INI 提供程序是 JSON 或 XML 之外一个简单而熟悉的替代方案。

## 7. User Secrets configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/user-secrets-configuration-provider-58112309/) · 1:36

### 总结

User Secrets 配置提供程序属于 Microsoft.Extensions.Configuration.UserSecrets NuGet 包,它专为本地开发设计,用于把敏感数据存放在源代码管理之外。
它把值以纯文本形式存储在开发者的机器上,并且在托管环境中通常是被隐式添加的。
开发者可以手动配置该提供程序为可选或必需,并指定配置是否在变化时重新加载,这有助于在管理机密时避免因机密缺失而导致应用程序崩溃。

### 核心概念

- **仅限开发**:User secrets 严格用于本地开发环境,不应在生产环境中使用。
- **纯文本存储**:配置值以未加密的文本形式存储在本地文件系统上。
- **隐式与显式注册**:虽然在托管应用程序场景中通常会被自动添加,但也可以在代码中手动注册该提供程序。
- **基于文件的选项**:支持 `optional` 和 `reloadOnChange` 等标准配置参数。

### 课程笔记

`Microsoft.Extensions.Configuration.UserSecrets` NuGet 包公开了 user secrets 配置提供程序。
这个提供程序是在本地开发期间管理敏感配置数据的专用工具。
需要注意的是,user secrets 以纯文本形式存储在开发者的机器上;它们没有被加密,其用途仅仅是让机密远离源代码仓库。

在大多数托管场景中,user secrets 提供程序是被隐式添加的。
不过,也可以手动把它添加到 `ConfigurationBuilder`。
这样做时,可以指定机密文件是否是必需的,以及文件更新时配置是否应重新加载。

```csharp
builder.Configuration.AddUserSecrets<Program>(
    optional: false,
    reloadOnChange: true);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/user-secrets-configuration-provider-58112309/?t=10)

如果 `optional` 参数设为 `false`,而本地环境上尚未初始化或配置 user secrets,应用程序会在启动时抛出 `FileNotFoundException`。
这是因为该提供程序找不到预期的机密文件。

为了让应用程序在缺少机密时也能运行,可以把 `optional` 标志设为 `true`。
这是开发环境中的一种常见配置,因为某些机密可能并不是每个开发者或每次运行应用程序都需要的。

```csharp
builder.Configuration.AddUserSecrets<Program>(
    optional: true,
    reloadOnChange: true);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/user-secrets-configuration-provider-58112309/?t=85)

把机密设为可选后,异常就不会被抛出,应用程序可以继续使用其他配置来源。
这提供了更灵活的开发体验,同时仍然允许在本地覆盖敏感设置。

## 8. Managing user secrets

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/) · 6:36

User secrets 提供了一种在开发期间把敏感配置数据存储在本地的方式,使其远离项目目录和源代码管理。
这是通过一个唯一标识符 `UserSecretsId` 实现的,它把项目映射到存储在用户配置文件目录中的一个特定 `secrets.json` 文件。

### 核心概念

- **AddUserSecrets<T>**:用于注册 user secrets 配置来源的方法,它会在指定类型所在的程序集中查找 `UserSecretsIdAttribute`。
- **UserSecretsId**:一个唯一标识符(通常是 GUID),用于在本地机器上定位 `secrets.json` 文件。
- **存储位置**:机密存储在项目范围之外的用户配置文件目录中,确保它们不会被提交到源代码管理。
- **安全性**:机密以纯文本形式存储在磁盘上。它们的目的是防止意外提交,而不是提供加密或生产级别的安全性。
- **工具**:User secrets 可以通过 IDE 功能(例如 Visual Studio 的 "Manage User Secrets")或 .NET CLI 来管理。

### 课程笔记

#### 注册 User Secrets

要在应用程序中启用 user secrets,需要在配置构建器上调用 `AddUserSecrets<T>` 方法。
这个方法会在包含指定类型(通常是 `Program`)的程序集中查找 `UserSecretsIdAttribute` 的实例。
该特性包含用于定位 `secrets.json` 文件的标识符。

```csharp
builder.Configuration.AddUserSecrets<Program>(
    optional: true,
    reloadOnChange: true);

// Stored outside of project in the "user profile".
//   Windows:
//     %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
//   Linux/macOS:
//     ~/.microsoft/usersecrets/<user_secrets_id>/secrets.json

#endregion
#endregion

> Environment Variables ($env)

> Command-Line Args (-a)

> Key-per file (/key)

> Azure Key Vault (k=v)

// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/?t=55)

```text
AddUserSecrets<Program>()
    |
    v
assembly of Program --> [UserSecretsId("<user_secrets_id>")]
    |
    v
Windows:     %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
Linux/macOS: ~/.microsoft/usersecrets/<user_secrets_id>/secrets.json
```

#### UserSecretsId 特性

虽然可以使用程序集级特性手动定义 `UserSecretsId`,但通常不建议这样做,而是推荐使用自动化工具。
在手动设置中,如果 `Program` 类型是在顶级语句中定义的,可以用一个 partial 类来引用它。

```csharp
using Microsoft.Extensions.Configuration.UserSecrets;

// Represents the user secrets ID.
//
// In most cases, this attribute is automatically generated during
// compilation by MSBuild targets included in the UserSecrets NuGet package.
// These targets use the MSBuild property 'UserSecretsId'
// to set this value.

[assembly: UserSecretsId("C3C8F559-50D3-4C9C-B558-311BFBF31D1F")]

internal partial class Program
{
    // In other words, you don't need to do this ... it will be done f
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/?t=130)

#### 项目配置

`UserSecretsId` 通常不是通过手动特性定义,而是作为 MSBuild 属性存储在项目文件(`.csproj`)中。
使用 IDE 工具时,例如右键单击项目并选择 "Manage User Secrets",如果项目文件中还没有这个属性,它会被自动生成并添加到项目文件中。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <UserSecretsId>0bf5673e-0c61-41ae-99b6-ad5effa0c434</UserSecretsId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Azure.Extensions.AspNetCore"
    <PackageReference Include="Azure.Identity" Versio
    <PackageReference Include="Microsoft.Extensions
    <PackageReference Include="Microsoft.Extensions
    <PackageReference Include="Microsoft.Extensions
    <PackageReference Include="Microsoft.Extensions
  </ItemGroup>

  <ItemGroup>
    <None Remove="app.ini" />
    <None Remove="appsettings.json" />
    <None Remove="config.xml" />
  </ItemGroup>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/?t=190)

#### 通过 CLI 管理机密

如果没有 IDE 支持或不想使用 IDE,.NET CLI 提供了一组用于管理机密的命令。
`dotnet user-secrets init` 命令通过向 `.csproj` 文件添加 `UserSecretsId` 来初始化项目。
之后就可以使用 CLI 设置、列出或删除机密。

```powershell
>_ pwsh  📂 providers  ⌛ 25ms
> dotnet user-secrets init
Set UserSecretsId to 'e268f6ec-a138-44b7-8b06-42df18dafd28' for MSBuild project 'C:\Users\d
tion-and-options\providers\providers.csproj'.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/?t=310)

要存储值,请使用 `set` 命令。
嵌套的配置键通过标准的冒号(`:`)分隔符来支持。

```powershell
> dotnet user-secrets set "Delay" "0:4:20"
Successfully saved Delay = 0:4:20 to the secret store.
> dotnet user-secrets list
Delay = 0:4:20
> dotnet user-secrets set "TodoApiOptions:Enabled" "true"
Successfully saved TodoApiOptions:Enabled = true to the secret store.
> dotnet user-secrets set "TodoApiOptions:BaseAddress" "https://jsonplaceholder.typicode.com/todos"
Successfully saved TodoApiOptions:BaseAddress = https://jsonplaceholder.typicode.com/todos to the secret store.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/?t=360)

#### 实现细节

在内部,user secrets 提供程序是用 `JsonConfigurationProvider` 实现的。
查看配置的调试输出时,机密会被标识为来自一个 `secrets.json` 文件。

```plaintext
Delay=0:0:05 (JsonConfigurationProvider for 'secrets.json' (Optional))
Logging:
  LogLevel:
    Default=Information (JsonConfigurationProvider for 'secrets.json' (Optional))
    Microsoft.Hosting.Lifetime=Warning (JsonConfigurationProvider for 'secrets.json' (Optional))
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (JsonConfigurationProvider for 'secrets.json' (Optional))
  Enabled=True (JsonConfigurationProvider for 'secrets.json' (Optional))
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/managing-user-secrets-58112310/?t=240)

## 9. Environment Variables configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-variables-configuration-provider-58112311/) · 2:31

### 总结

.NET 中的环境变量配置提供程序让应用程序可以直接从主机操作系统的环境变量中读取配置设置。
虽然可以把该提供程序配置为拉取所有可用的系统变量,但最佳实践是使用前缀,把变量限定在应用程序需要的范围内。
该提供程序支持层级配置映射,方法是在变量名中使用双下划线分隔符,提供程序会把它转换为 IConfiguration 使用的标准配置层次结构。

### 核心概念

* **NuGet 包**:`Microsoft.Extensions.Configuration.EnvironmentVariables` 提供了所需的扩展方法。
* **限定范围**:使用前缀(例如 `MY_APP_`)可以确保只有相关的变量被加载进应用程序配置。
* **层次结构**:环境变量使用双下划线(`__`)作为分隔符来表示嵌套的配置键(例如 `Section__Key`)。
* **前缀剥离**:当在 `AddEnvironmentVariables` 中指定前缀时,该前缀会从最终的配置键名中移除。

### 课程笔记

环境变量配置提供程序是 .NET 配置系统的基础组成部分,通常用于在不修改文件的情况下提供特定于环境的设置。
它由 `Microsoft.Extensions.Configuration.EnvironmentVariables` 包提供支持。

默认情况下,不带参数调用 `AddEnvironmentVariables()` 会把机器上的所有环境变量都拉进 `IConfiguration` 对象。
这包括 `PATH` 或 `TEMP` 这样的系统级变量,以及经常由 `Microsoft.Extensions.Hosting` 默认设置自动拉入的 .NET 特定变量(例如以 `DOTNET_` 为前缀的变量)。

```csharp
var builder =
    Host.CreateEmptyApplicationBuilder(settings: null);

// Microsoft.Extensions.Configuration.EnvironmentVariables
builder.Configuration.AddEnvironmentVariables();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-variables-configuration-provider-58112311/?t=10)

为了避免无关的系统变量污染应用程序配置,可以使用前缀来限定该提供程序的范围。
提供前缀后,只有以该字符串开头的环境变量才会被加载。
提供程序在把键添加到配置树时会自动剥离前缀。

```csharp
// Scoping environment variables to specific application needs
// builder.Configuration.AddEnvironmentVariables();

builder.Configuration.AddEnvironmentVariables(prefix: "MY_APP_");

// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-variables-configuration-provider-58112311/?t=40)

在 shell 中定义环境变量时,层级键用双下划线(`__`)表示。
例如,要设置 `TodoApiOptions` 节中名为 `Enabled` 的属性,环境变量应命名为 `MY_APP_TodoApiOptions__Enabled`。

```powershell
$env:MY_APP_Delay = "0:0:17"
$env:MY_APP_TodoApiOptions__Enabled = "True"
$env:MY_APP_TodoApiOptions__BaseAddress = "https://jsonplaceholder.typicode.com/todos"
dotnet run --project .\providers.csproj
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-variables-configuration-provider-58112311/?t=70)

```text
MY_APP_TodoApiOptions__Enabled
|_____| |____________|  |_____|
prefix     section        key
(stripped)    |            |
              v            v
        TodoApiOptions:Enabled
```

运行应用程序后,`GetDebugView()` 的输出确认这些值已成功从环境变量映射过来。
该提供程序被标识为带有指定前缀的 `EnvironmentVariablesConfigurationProvider`。

```powershell
Delay=0:0:17 (EnvironmentVariablesConfigurationProvider Prefix: 'MY_APP_')
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (EnvironmentVariablesConfigurationProvider Prefix: 'MY_APP_')
  Enabled=True (EnvironmentVariablesConfigurationProvider Prefix: 'MY_APP_')
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/environment-variables-configuration-provider-58112311/?t=125)

## 10. Command-line configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/) · 3:31

要在 .NET 应用程序中把命令行参数用作配置,必须引入 `Microsoft.Extensions.Configuration.CommandLine` NuGet 包。
注册该提供程序的方式是在配置构建器上调用 `AddCommandLine(args)`,并传入应用程序入口点的 `string[] args`。

### 核心概念

*   **NuGet 包**:需要 `Microsoft.Extensions.Configuration.CommandLine`。
*   **注册**:使用 `builder.Configuration.AddCommandLine(args)` 启用该提供程序。
*   **标准语法**:键通常以双短横线作为前缀(`--Key Value` 或 `--Key=Value`)。
*   **嵌套键**:层次结构用冒号(`:`)分隔符表示。
*   **开关映射**:一个字典,用于把短别名(例如 `-d`)映射到完整的配置键(例如 `Delay`)。
*   **灵活语法**:支持 `-`、`--` 和 `/` 前缀,并且支持用空格和 `=` 作为值分隔符。

### 课程笔记

最基本的实现是把 `args` 数组直接传给配置构建器。
这使得任何命令行参数都可以被解释为配置键值对。

```csharp
var builder =
    Host.CreateEmptyApplicationBuilder(null);

#region Command-Line Args (-a)

// Microsoft.Extensions.Configuration.CommandLine

builder.Configuration.AddCommandLine(args);

#endregion

// Write all the config values to the console.
Console.WriteLine(
    builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/?t=10)

运行应用程序时,可以使用双短横线前缀提供参数。
提供程序会自动把它们映射为配置值,这些值可以通过配置的调试视图查看。

```powershell
dotnet run --Delay "0:0:03"
Delay=0:0:03 (CommandLineConfigurationProvider)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/?t=30)

也可以在命令行参数中使用冒号分隔符传递嵌套的配置值。
虽然可行,但对于嵌套很深的选项,这种语法可能会变得相当冗长。

```powershell
dotnet run --TodoApiOptions:Enabled "True"
TodoApiOptions:
Enabled=True (CommandLineConfigurationProvider)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/?t=70)

为了改善开发体验,`AddCommandLine` 方法接受一个可选的 `switchMappings` 参数。
这是一个字符串到字符串映射的字典,其中键是命令行别名,值是实际的配置键路径。
这样就可以使用 `-d` 或 `--apiOn` 这样更短的标志。

```csharp
#region Command-Line Args (-a)

// Microsoft.Extensions.Configuration.CommandLine

var switchMappings =
    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // -d="0:0:05" or -d "0:0:05"
        ["-d"] = "Delay",

        // --apiOn=true /apiOn=true
        ["--apiOn"] = "TodoApiOptions:Enabled",

        // --todoUri <uri> /todoUri <uri>
        ["--todoUri"] = "TodoApiOptions:BaseAddress"
    };

builder.Configuration.AddCommandLine(
    args, switchMappings);

#endregion
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/?t=100)

命令行配置提供程序在语法上非常宽松。
它把单短横线、双短横线和正斜杠都视为有效的前缀,并且可以解析用空格或等号分隔的值。
这种灵活性让开发者可以使用最适合自己环境或个人偏好的风格。

```powershell
dotnet run -d "0:0:03" --apiOn="true" /todoUri "https://jsonplaceholder.typicode.com/todos"
Delay=0:0:03 (CommandLineConfigurationProvider)
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (CommandLineConfigurationProvider)
  Enabled=true (CommandLineConfigurationProvider)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/command-line-configuration-provider-58112312/?t=160)
## 11. Key-per file configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/key-per-file-configuration-provider-58112313/) · 2:25

### 总结

Key-per file 配置提供程序让 .NET 应用程序可以从一个目录加载配置设置,该目录中的每个文件代表一个配置键。
文件名充当键,文件内容充当值。
这个提供程序通过 `Microsoft.Extensions.Configuration.KeyPerFile` 包提供,它支持层级配置,方法是在文件名中使用双下划线表示嵌套。
对于配置被投射到文件系统中的环境来说,它是一个稳健的方案,并支持必需/可选要求以及变化时自动重载等特性。

### 核心概念

* **包**:需要 `Microsoft.Extensions.Configuration.KeyPerFile` NuGet 包。
* **映射**:文件名映射为配置键;文件内容映射为配置值。
* **嵌套**:层级键在文件系统中通过在文件名中使用双下划线(`__`)分隔符来表示。
* **配置**:通过 `IConfigurationBuilder` 上的 `AddKeyPerFile` 扩展方法添加。
* **维护**:这个提供程序作为 ASP.NET Core 的一部分进行维护,而不是由核心 .NET 运行时维护。

### 课程笔记

Key-per file 配置提供程序通过 ASP.NET Core 公开,并由该团队维护。
要使用这个提供程序,必须在项目中引入 `Microsoft.Extensions.Configuration.KeyPerFile` NuGet 包。

要注册该提供程序,需要在配置构建器上调用 `AddKeyPerFile` 方法。
这个方法把应用程序指向一个特定的目录。
然后,提供程序遍历该目录中的文件,把文件名用作键,把文件内容用作值。

```csharp
#region Key-per file (/key)

//   Microsoft.Extensions.Configuration.KeyPerFile

builder.Configuration.AddKeyPerFile(
    directoryPath: @"D:\ExampleApp",
    optional: false,
    reloadOnChange: true);

#endregion

// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/key-per-file-configuration-provider-58112313/?t=10)

在这个提供程序的典型目录结构中,文件按照它们所代表的键来命名。
对于嵌套配置,使用双下划线(`__`)作为分隔符。
例如,一个目录可能包含一个名为 `Delay` 的文件,以及其他以 `TodoApiOptions__` 为前缀的文件,用来表示 `TodoApiOptions` 对象中的属性。

下面的 PowerShell 命令演示了一个包含三个文件的目录:`Delay`、`TodoApiOptions__BaseAddress` 和 `TodoApiOptions__Enabled`。

```powershell
Get-ChildItem -File | ForEach-Object { Write-Output "$($_.Name):"; Get-Content $_.FullName; Write-Output "" }
Delay:
00:04:20

TodoApiOptions__Enabled:
True

TodoApiOptions__BaseAddress:
https://jsonplaceholder.typicode.com/todos
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/key-per-file-configuration-provider-58112313/?t=85)

```text
D:\ExampleApp\                    file content
+-- Delay                       -> 00:04:20
+-- TodoApiOptions__Enabled     -> True
+-- TodoApiOptions__BaseAddress -> https://jsonplaceholder.typicode.com/todos

              |  file name = key, content = value, "__" -> ":"
              v

Delay                      = 00:04:20
TodoApiOptions:Enabled     = True
TodoApiOptions:BaseAddress = https://jsonplaceholder.typicode.com/todos
```

应用程序运行时,`KeyPerFileConfigurationProvider` 会解释这些文件。
文件名中的双下划线被正确地转换为标准的 .NET 配置层次结构(例如 `TodoApiOptions:BaseAddress`)。
使用 `GetDebugView()` 可以验证这些值确实是从指定目录正确获取的,并能表明该提供程序是必需的还是可选的。

```powershell
>_ pwsh  📂 -> providers  ⌛ 1ms
└─>> dotnet run
Delay=00:04:20 (KeyPerFileConfigurationProvider for files in 'D:\ExampleApp\' (Required))
TodoApiOptions:
  BaseAddress=https://jsonplaceholder.typicode.com/todos (KeyPerFileConfigurationProvider for files in 'D:\ExampleApp\'
  (Required))
  Enabled=True (KeyPerFileConfigurationProvider for files in 'D:\ExampleApp\' (Required))

>_ pwsh  📂 -> providers  ⌛ 830ms
└─>>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/key-per-file-configuration-provider-58112313/?t=130)

## 12. Azure Key Vault configuration provider

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-key-vault-configuration-provider-58112314/) · 4:10

### 总结

Azure Key Vault 配置提供程序让 .NET 应用程序可以安全地从 Azure Key Vault 获取机密和配置数据。
这个提供程序由 ASP.NET Core 团队管理,它与 Azure.Identity 集成,支持通过 DefaultAzureCredential 进行安全的身份验证。
它对于满足 FIPS 140-2 Level 2 这样的高安全标准尤其有价值。
本课演示如何注册该提供程序、使用托管标识进行身份验证,以及使用经过脱敏的调试视图安全地检查配置值。

### 核心概念

- 与 Azure.Extensions.AspNetCore.Configuration.Secrets NuGet 包集成。
- 通过 Azure.Identity 和 DefaultAzureCredential 进行身份验证。
- 经 FIPS 140-2 Level 2 验证的硬件安全模型。
- 把 Key Vault 机密映射到 IConfiguration 层次结构。
- 自定义 GetDebugView,从日志中脱敏敏感信息。

### 课程笔记

Azure Key Vault 配置提供程序是 IConfigurationProvider 的一个专门实现,它从 Azure Key Vault 服务获取值。
与许多由 .NET 运行时团队管理的其他提供程序不同,这个提供程序由 ASP.NET Core 团队维护。
要实现这个提供程序,应用程序需要两个主要的 NuGet 包:用于提供程序本身的 Azure.Extensions.AspNetCore.Configuration.Secrets,以及用于处理身份验证的 Azure.Identity。
人们之所以常常选择 Azure Key Vault,是因为它的高安全标准,它在硬件安全模型方面满足联邦信息处理标准(FIPS)140-2 Level 2。

该提供程序通过 ConfigurationManager 上的 AddAzureKeyVault 扩展方法注册。
这个方法需要一个保管库的 URI 和一个凭据对象。

```csharp
#region Azure Key Vault (k=v)

// Azure.Extensions.AspNetCore.Configuration.Secrets
// Azure.Identity

builder.Configuration.AddAzureKeyVault(
    vaultUri: new Uri($"""
        https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/
        """),
    credential: new DefaultAzureCredential());

#endregion

// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-key-vault-configuration-provider-58112314/?t=10)

vaultUri 通常遵循一个标准格式:https://{YourKeyVaultName}.vault.azure.net/。
在这个实现中,保管库名称是从一个已有的配置来源(例如 User Secrets)中获取的。
身份验证使用 DefaultAzureCredential,它对本地开发和生产环境都非常有效,因为它会自动轮流尝试各种身份验证方式,包括托管标识。
连接之后,存储在 Key Vault 中的所有机密都会作为配置值公开。
其中可以包括嵌套值,它们在 Key Vault 中使用双短横线(--)作为分隔符来表示 .NET 配置中的层次结构。

```text
User Secrets: KeyVaultName
        |
        v
https://{KeyVaultName}.vault.azure.net/  <-- DefaultAzureCredential
        |
        v
Key Vault secrets  ("--" = hierarchy separator)
        |
        v
builder.Configuration
```

调试配置时,重要的是避免把敏感机密打印到控制台。
GetDebugView 方法提供了一个接受 processValue 委托的重载,可以根据提供程序类型或特定的键有条件地对值进行脱敏。

```csharp
// Write all the config values to the console.
Console.WriteLine(
    value: builder.Configuration.GetDebugView(
        processValue: static context => context switch
        {
            {
                ConfigurationProvider: AzureKeyVaultConfigurationProvider
            } => "***REDACTED***",
            _ when context.ConfigurationProvider
                .TryGet(key: "KeyVaultName", value: out var value) &&
                    value is { Length: > 0 } => "***REDACTED***",
            _ => context.Value ?? "Error!"
        }));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-key-vault-configuration-provider-58112314/?t=175)

在这段逻辑中,任何来自 AzureKeyVaultConfigurationProvider 的值都会被替换为一个脱敏占位符。
此外,KeyVaultName 本身也会被脱敏,以确保保管库的身份不会在日志中暴露。

## 13. Azure App Configuration provider with Key Vault secrets

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-app-configuration-provider-with-key-vault-secrets-58112315/) · 3:36

### 总结

Azure App Configuration 提供程序作为统一的配置来源,让 .NET 应用程序可以集中管理设置和机密。
通过使用 Microsoft.Extensions.Configuration.AzureAppConfiguration 包,开发者可以连接到一个 Azure App Configuration 实例,并透明地解析存储在 Azure Key Vault 中的机密。
这种集成通过为普通设置和敏感数据提供单一 API 简化了配置管理,同时利用 Azure.Identity 和 DefaultAzureCredential,通过托管标识或 Azure CLI 等本地开发工具确保安全的身份验证。

### 核心概念

- **统一配置**:通过单一提供程序同时访问标准设置和 Key Vault 机密。
- **Azure App Configuration 提供程序**:通过 `Microsoft.Extensions.Configuration.AzureAppConfiguration` NuGet 包实现。
- **Azure Identity 集成**:使用 `DefaultAzureCredential` 进行安全的身份验证,无需硬编码凭据。
- **Key Vault 引用**:让 Azure App Configuration 充当存储在 Azure Key Vault 中的机密的代理。
- **凭据过滤**:配置 `DefaultAzureCredentialOptions`,为本地开发排除或包含特定的身份验证方式(例如 Azure CLI)。

### 课程笔记

Azure App Configuration 提供程序是由 Azure 团队维护的 `IConfigurationProvider` 第一方实现。
它让应用程序可以从 Azure App Configuration 存储中拉取配置数据。
与 Azure Key Vault 结合使用时,它提供了一种无缝管理非敏感设置和机密的方式。

要实现这一点,必须引入 `Microsoft.Extensions.Configuration.AzureAppConfiguration` 和 `Azure.Identity` NuGet 包。
在应用程序设置中(通常使用 `HostApplicationBuilder`),在 `Configuration` 属性上调用 `AddAzureAppConfiguration`。

```csharp
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("AZURE_APP_CONFIG_CONNECTION_STRING")
        ?? throw new InvalidOperationException("A valid \"AZURE_APP_CONFIG_CONNECTION_STRING\" is required.");

    // Connects to Azure App Configuration using a connection string.
    options.Connect(connectionString);

    // Configures Azure Key Vault integration.
    options.ConfigureKeyVault(kv => kv.SetCredential(
        new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            // Enable Azure CLI credential for local development.
            ExcludeAzureCliCredential = false,

            // Exclude other credential types to narrow the authentication scope.
            ExcludeEnvironmentCredential = true,
            ExcludeInteractiveBrowserCredential = true,
            ExcludeAzurePowerShellCredential = true,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCodeCredential = true,
            ExcludeVisualStudioCredential = true
        })));
});

Console.WriteLine(builder.Configuration.GetDebugView());
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/azure-app-configuration-provider-with-key-vault-secrets-58112315/?t=85)

`options.Connect` 方法建立与 Azure App Configuration 服务的连接。
对于机密,使用 `options.ConfigureKeyVault` 方法来指定提供程序应如何向 Key Vault 进行身份验证。
在这个例子中,`DefaultAzureCredential` 被配置为专门使用 Azure CLI,同时排除环境变量或交互式浏览器等其他来源。

配置完成后,无论值是直接存储在 App Configuration 中,还是从 Key Vault 引用而来,应用程序都可以获取它们。
例如,像 "delay" 这样的非敏感值可能直接存储在 App Configuration 中,而像 "BaseAddress" 或 "Enabled" 标志这样的敏感值则存储在 Key Vault 中。
在 Azure Portal 中,Key Vault 引用由一个特定的图标标识,表示该值是指向某个机密的指针。
当应用程序请求这样的键时,提供程序会自动从 Key Vault 获取该机密,并把它作为配置树的一部分返回,使这个过程对使用方透明。

```text
application
    |  AddAzureAppConfiguration  (options.Connect)
    v
Azure App Configuration
    +-- Delay  (stored directly)
    +-- BaseAddress, Enabled  (Key Vault reference)
            |  options.ConfigureKeyVault
            v
       Azure Key Vault  <-- DefaultAzureCredential (Azure CLI)
```

## 14. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112317/) · 0:59

### 总结

本课全面回顾了 .NET 中可用的配置提供程序,范围从基于文件的提供程序到集成云的方案。
它回顾了 JSON、XML 和 INI 提供程序的实现与验证,以及 User Secrets 这样的本地开发工具和环境变量、命令行参数这类特定于环境的提供程序。
此外,它还涉及了使用 Azure Key Vault 和 Azure App Configuration 的企业级配置管理。

### 核心概念

- 文件配置提供程序(JSON、XML、INI)
- User Secrets 管理(IDE 和 CLI)
- 环境变量和命令行参数提供程序
- Azure Key Vault 和 Azure App Configuration 集成
- 使用空的应用程序构建器进行验证

### 课程笔记

本节详细概述了 .NET 中可用的 `IConfigurationProvider` 实现。
通过使用空的应用程序构建器,课程演示了如何验证并利用各种内置配置提供程序来管理不同环境下的应用程序设置。

这些示例的项目设置是一个 .NET 8.0 控制台应用程序,带有托管和基于 Azure 的配置服务的依赖项。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>DomeTrain.FromHeroToZero.ConfigurationAndOptions</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Azure.Identity" Version="1.12.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.AzureAppConfiguration" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112317/?t=10)

本节涵盖了以下提供程序和工具:
- **文件配置提供程序**:JSON、XML 和 INI 文件格式的实现细节。
- **User Secrets**:同时使用 IDE 和 .NET CLI 管理敏感的本地开发数据的技巧。
- **环境与 CLI**:使用环境变量和命令行参数在运行时覆盖或提供配置。
- **Azure 集成**:使用 Azure App Configuration 和 Azure Key Vault 集中管理配置并保护机密。

这次回顾是一个过渡,从使用内置提供程序转向开发自定义配置提供程序,以满足特定的架构需求。

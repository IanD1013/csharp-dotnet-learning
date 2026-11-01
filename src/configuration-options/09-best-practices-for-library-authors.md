# Best Practices for Library Authors

> 课程:[From Zero to Hero: Configuration and Options in .NET](https://dometrain.com/course/from-zero-to-hero-configuration-and-options-in-dotnet/) · 第 9 章
> 共 13 课 · 约 24:04
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction to Consumer-Centric (Empathy-based) Authoring](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-consumer-centric-empathy-based-authoring-58112840/) | 0:52 | [↓](#1-introduction-to-consumer-centric-empathy-based-authoring) |
| 2 | [Defining Strongly-typed WidgetOptions](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/defining-strongly-typed-widgetoptions-58112812/) | 1:26 | [↓](#2-defining-strongly-typed-widgetoptions) |
| 3 | [Debating namespace Naming: IServiceCollection Extensions](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/debating-namespace-naming-iservicecollection-extensions-58112813/) | 1:11 | [↓](#3-debating-namespace-naming-iservicecollection-extensions) |
| 4 | [Exploring an Add\* with IConfigurationSection API](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-an-add-with-iconfigurationsection-api-58112814/) | 1:55 | [↓](#4-exploring-an-add-with-iconfigurationsection-api) |
| 5 | [Consuming the Add\* with IConfigurationSection](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/) | 3:35 | [↓](#5-consuming-the-add-with-iconfigurationsection) |
| 6 | [Improving consumer experiences through meaningful error messages](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/improving-consumer-experiences-through-meaningful-error-messages-58112816/) | 1:35 | [↓](#6-improving-consumer-experiences-through-meaningful-error-messages) |
| 7 | [Confirming new consumer experiences](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/confirming-new-consumer-experiences-58112820/) | 1:52 | [↓](#7-confirming-new-consumer-experiences) |
| 8 | [Considerations when accepting Option-types as Add\* API parameters](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considerations-when-accepting-option-types-as-add-api-parameters-58112825/) | 1:52 | [↓](#8-considerations-when-accepting-option-types-as-add-api-parameters) |
| 9 | [Consuming the Add\* with Options-type API](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-options-type-api-58112835/) | 2:17 | [↓](#9-consuming-the-add-with-options-type-api) |
| 10 | [Exposing an Action API](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exposing-an-action-api-58112836/) | 1:43 | [↓](#10-exposing-an-action-api) |
| 11 | [The Importance of Consistent APIs](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/the-importance-of-consistent-apis-58112837/) | 1:58 | [↓](#11-the-importance-of-consistent-apis) |
| 12 | [Using the configureOptions delegate-based API](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-the-configureoptions-delegate-based-api-58112838/) | 2:29 | [↓](#12-using-the-configureoptions-delegate-based-api) |
| 13 | [Section Recap](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112839/) | 1:19 | [↓](#13-section-recap) |

## 1. Introduction to Consumer-Centric (Empathy-based) Authoring

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/introduction-to-consumer-centric-empathy-based-authoring-58112840/) · 0:52

### 总结

本课介绍以消费者为中心的库编写理念,强调在构建可复用组件时开发者同理心的重要性。
通过采用通用标准和最佳实践,库作者可以确保自己的代码为使用它的开发者提供一致的功能和高度的可配置性。

### 核心概念

- 面向库消费者的、基于同理心的开发。
- 代码复用以及开源所扮演的角色。
- 配置模式的标准化。
- 可配置性和一致功能带来的好处。

### 课程笔记

编写库时应当采用以消费者为中心的思维方式,把使用该库的开发者作为首要关注点。
由于代码经常在不同项目之间复用,并通过开源共享,因此建立一套通用的标准和最佳实践,对于创建高质量的软件组件至关重要。

遵循这些标准主要带来两个好处:

1. **可配置性(Configurability)**:让消费者能够轻松地根据自身的具体需求调整库的行为。
2. **一致的功能(Consistent Functionality)**:确保库的行为可预测,并遵循 .NET 生态系统中既定的模式。

这种做法的一个关键方面在于如何共享和管理强类型选项,这有助于带来更好的开发者体验和更健壮的配置管理。

## 2. Defining Strongly-typed WidgetOptions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/defining-strongly-typed-widgetoptions-58112812/) · 1:26

### 总结

本课演示如何在类库中定义强类型选项,以便在多个项目之间共享配置。
它着重介绍了如何集成 Microsoft Extensions 中的选项与配置功能,并配合数据注解(Data Annotations)实现健壮的验证。
本课重点是创建一个 `WidgetOptions` 类,它利用源生成验证和属性级约束,确保配置数据既有效又类型安全。

### 核心概念

- 用于共享配置的类库结构。
- 所需依赖:`Microsoft.Extensions.Options.ConfigurationExtensions` 和 `Microsoft.Extensions.Options.DataAnnotations`。
- 使用 `[OptionsValidator]` 实现源生成验证。
- 在选项属性上应用数据注解(`[Url]`、`[Required]`、`[AllowedValues]`)。
- 为选项定义默认值。

### 课程笔记

要在不同项目之间共享强类型选项,首先要创建一个专用的类库。
这个库充当配置对象的集中存放位置,供其他服务或应用程序使用。

该项目需要特定的依赖来支持配置绑定和验证。
具体来说,它使用了 Microsoft Extensions 中的 Options 和 Configuration,以及用于属性级验证的 Data Annotations。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>Example.Library</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.Options.DataAnnotations" Version="8.0.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/defining-strongly-typed-widgetoptions-58112812/?t=10)

`WidgetOptions` 类被定义为 `sealed partial` 类。
它实现了 `IValidateOptions<WidgetOptions>`,并使用 `[OptionsValidator]` 特性来利用源生成的验证逻辑。
这种做法确保验证在编译时就被高效地处理。

该类包含几个带有特定验证要求的属性:
- **ImageUrl**:标记了 `[Url]` 和 `[Required]`,确保它包含一个有效的 URL,且不是空字符串。
- **Color**:默认值为 "Blue",并使用 `[AllowedValues]` 将输入限制为 "Red"、"Green" 或 "Blue"。
- **Size**:一个整数属性,默认值为 10。
- **IsEnabled**:一个布尔标志,默认为 true。
- **Opacity**:一个表示小部件透明度的 double,默认为 1.0。

```csharp
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Example.Library;

[OptionsValidator]
public sealed partial class WidgetOptions : IValidateOptions<WidgetOptions>
{
    [Url, Required(AllowEmptyStrings = false)]
    public string? ImageUrl { get; set; }

    [AllowedValues(values: object?[] ["Red", "Green", "Blue"])]
    public string Color { get; set; } = "Blue";

    public int Size { get; set; } = 10;

    public bool IsEnabled { get; set; } = true;

    public double Opacity { get; set; } = 1.0;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/defining-strongly-typed-widgetoptions-58112812/?t=40)

定义好 `WidgetOptions` 对象之后,下一步是在 `IServiceCollection` 接口上定义扩展方法,以便在消费方应用程序的依赖注入容器中轻松注册这些选项。

## 3. Debating namespace Naming: IServiceCollection Extensions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/debating-namespace-naming-iservicecollection-extensions-58112813/) · 1:11

### 总结

本课探讨了在 .NET 库中为 IServiceCollection 扩展方法选择命名空间时的争论。
虽然官方指南可能自相矛盾,但讲师主张使用 Microsoft.Extensions.DependencyInjection 命名空间,以提高可发现性,并让使用者无需额外的 using 指令就能轻松使用库的服务。

### 核心概念

- 扩展方法的命名空间可发现性。
- Microsoft 官方关于扩展方法命名空间的指南中存在的矛盾。
- 在扩展核心类型时"借用"命名空间以减少使用者摩擦的做法。
- 尽量减少库使用者所需的 `using` 语句数量。

### 课程笔记

在为 `IServiceCollection` 创建扩展方法时,应该使用哪个命名空间存在相当大的争论。
Microsoft 官方文档在这个话题上给出了相互矛盾的指导。
在某些情况下,它建议使用 `Microsoft.Extensions.DependencyInjection` 命名空间。
这种做法旨在简化使用者的体验,因为只要他们已经在使用标准的依赖注入,就不需要再导入额外的命名空间来访问库的注册方法。

相反,另一些官方指南建议,除非代码由 Microsoft 自己编写,否则不要使用 `Microsoft` 命名空间前缀。

尽管存在这种冲突,库作者的一个常见最佳实践是"借用"被扩展类型的命名空间。
通过把 `IServiceCollection` 的扩展方法放在 `Microsoft.Extensions.DependencyInjection` 命名空间中,使用者一旦操作服务集合,这些方法就立即可用。
这提高了可发现性,并减少了消费方应用程序的 `Program.cs` 或 `Startup.cs` 文件中所需的样板代码。

```csharp
using Example.Library;
using Microsoft.Extensions.Configuration;

// There's some debate over this namespace!
namespace Microsoft.Extensions.DependencyInjection;

/// <summary> Represents a set of extension methods on the IServiceCollection type. </summary>
public static class ServiceCollectionExtensions
{
    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        IConfigurationSection config)
    {
        // Implementation
        return services;
    }

    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        string configSectionPath)
    {
        // Implementation
        return services;
    }

    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        Action<WidgetOptions> configureOptions)
    {
        // Implementation
        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/debating-namespace-naming-iservicecollection-extensions-58112813/?t=10)

## 4. Exploring an Add\* with IConfigurationSection API

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-an-add-with-iconfigurationsection-api-58112814/) · 1:55

### 总结

本课演示如何为 IServiceCollection 创建一个接受 IConfigurationSection 来配置选项的库扩展方法。
它涵盖了将特定配置节绑定到强类型选项类的实现细节,包括验证逻辑和用于注册选项的流式 API,从而确保库的使用者能够轻松地把库的服务与其现有的配置提供程序集成在一起。

### 核心概念

- **IServiceCollection 的扩展方法**:规范库在 .NET 依赖注入容器中的注册方式。
- **显式配置绑定**:以 `IConfigurationSection` 作为参数,让使用者能够明确指定库的设置位于其配置中的何处。
- **流式选项 API**:使用 `AddOptionsWithValidateOnStart<T>` 注册选项,并在应用程序启动时强制执行验证。
- **按节键绑定**:使用 `IConfigurationSection` 的 `Key` 属性将配置数据绑定到选项对象。
- **数据注解验证**:在服务注册流程中集成 `ValidateDataAnnotations()`,以确保配置的完整性。

### 课程笔记

编写类库时,在 `IServiceCollection` 上提供一个 `Add*` 扩展方法是简化服务注册的最佳实践。
一种常见模式是暴露一个接受 `IConfigurationSection` 的 API。
这种做法期望使用者提供与库的选项(例如 `WidgetOptions`)相对应的特定配置节。
然后,库假定该配置节的结构与选项对象相匹配,从而可以将两者绑定在一起。

这类方法的实现通常以守卫子句开头,以确保 `IServiceCollection` 和 `IConfigurationSection` 都不为 null。
在验证输入之后,该方法使用 Options API 注册选项类型并将其绑定到配置。

```csharp
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds widget services to the given <paramref name="services"/> collection.
    /// Configures the given <paramref name="widgetConfigSection"/> to
    /// the <see cref="WidgetOptions"/> object.
    /// </summary>
    /// <param name="services">
    /// The service collection to add services to.
    /// </param>
    /// <param name="widgetConfigSection">
    /// The widget configuration section to bind options to.
    /// </param>
    /// <returns>
    /// The same <paramref name="services"/> instance with other services added.
    /// </returns>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        IConfigurationSection widgetConfigSection)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(widgetConfigSection);

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .BindConfiguration(widgetConfigSection.Key)
            .ValidateDataAnnotations();

        // TODO:
        //  Add widget services to the service collection.

        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exploring-an-add-with-iconfigurationsection-api-58112814/?t=55)

在上面的实现中,使用 `AddOptionsWithValidateOnStart<WidgetOptions>()` 来注册选项。
相比标准注册方式,更推荐这种做法,因为它确保验证发生在应用程序启动时,而不是在首次访问选项时。

`BindConfiguration(widgetConfigSection.Key)` 调用会把所提供配置节中的配置值映射到 `WidgetOptions` 的属性上。
最后,调用 `ValidateDataAnnotations()` 来启用基于 `WidgetOptions` 类上所定义特性(例如 `[Required]` 或 `[Range]`)的验证。

```text
AddOptionsWithValidateOnStart<WidgetOptions>()
    |
    v
BindConfiguration(widgetConfigSection.Key)   配置节 -> WidgetOptions 属性
    |
    v
ValidateDataAnnotations()                    [Required] / [Range] ...
```

该方法返回 `IServiceCollection` 实例,以提供流式的开发者体验,使调用方能够链式地进行更多服务注册。
虽然示例中只为添加实际的小部件服务留了一个占位,但主要关注点是把配置映射到选项模式。

## 5. Consuming the Add\* with IConfigurationSection

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/) · 3:35

### 总结

本课演示消费方应用程序如何通过向服务注册方法传入 IConfigurationSection,来集成一个使用选项模式的类库。
它涵盖了消费方项目的搭建、使用类型安全的键将 appsettings.json 中的配置节映射到选项对象,以及在提供无效配置时运行时验证的行为,即抛出 OptionsValidationException。

### 核心概念

- 使用启用了选项的库所需的项目依赖。
- 使用 `IConfiguration.GetSection` 映射 `appsettings.json` 中的配置节。
- 使用 `nameof(TOptions)` 进行类型安全的配置键映射。
- 使用数据注解对选项进行运行时验证。
- 从 `IServiceProvider` 手动解析 `IOptions<T>` 以便检查。

### 课程笔记

要使用一个暴露选项的库,消费方应用程序必须先建立必要的项目引用和包引用。
在本例中,一个控制台应用程序引用了该库项目以及 `Microsoft.Extensions.Hosting` 包,以提供主机构建器和依赖注入基础设施。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>Example.Library.Consumer</RootNamespace>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="appsettings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\example-library\example-library.csproj" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0" />
  </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/?t=10)

应用程序配置定义在 `appsettings.json` 中。
JSON 的结构必须与库中定义的选项类的属性相匹配。
在初始配置中,根据库的验证规则,某些必填字段可能缺失或包含无效值。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "WidgetOptions": {
    "Color": "Pink",
    "Size": "8",
    "IsEnabled": true,
    "Opacticy": "1.0"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/?t=25)

在 `Program.cs` 中,使用 `HostApplicationBuilder` 来配置服务。
为了注册库的服务并绑定配置,调用了 `AddWidgetServices` 扩展方法。
该方法需要一个 `IConfigurationSection`。
通过使用 `builder.Configuration.GetSection(nameof(WidgetOptions))`,应用程序获取与选项类名称相对应的特定配置节点。

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Example.Library;

var builder = Host.CreateApplicationBuilder(args);

// The Add* API expects an IConfigurationSection
// that's named "WidgetOptions".
builder.Services.AddWidgetServices(
    widgetConfigSection: builder.Configuration.GetSection(
        key: nameof(WidgetOptions)));

var host = builder.Build();

await host.StartAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/?t=40)

```text
appsettings.json                       Program.cs
+----------------------+
| "WidgetOptions": {   |   GetSection(nameof(WidgetOptions))
|   "Color": ...,      | ------------------------------------+
|   "Size": ...        |                                     |
| }                    |                                     v
+----------------------+        AddWidgetServices(widgetConfigSection)
                                                 |
                                                 v
                                          WidgetOptions
```

如果 `appsettings.json` 中提供的配置无效(例如缺少必填的 `ImageUrl`,或提供了无效的 `Color`),应用程序会在启动时、首次访问选项的时候抛出 `OptionsValidationException`。
这确保了应用程序不会在无效状态下运行。

要解决这些错误,必须用满足 `WidgetOptions` 类上所定义数据注解特性(例如 `[Required]` 或 `[AllowedValues]`)的有效数据来更新 `appsettings.json`。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "WidgetOptions": {
    "Color": "Green",
    "Size": "8",
    "IsEnabled": true,
    "ImageUrl": "https://placeholder.co/300x240/512bd4/FFFFFF/png",
    "Opacticy": "1.0"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/?t=175)

一旦配置有效,应用程序就能成功启动。
出于演示目的,可以从服务提供程序中手动获取选项,以验证这些值已从配置文件中正确绑定。

```csharp
var host = builder.Build();

await host.StartAsync();

// Manual retrieval for demonstration:
var options = host.Services.GetRequiredService<IOptions<WidgetOptions>>();

var widgetOptions = options.Value;

if (widgetOptions is not null)
{
    // The widgetOptions object now contains the values from appsettings.json
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-iconfigurationsection-58112815/?t=85)

## 6. Improving consumer experiences through meaningful error messages

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/improving-consumer-experiences-through-meaningful-error-messages-58112816/) · 1:35

### 总结

在 .NET 中构建库时,在配置过程中向使用者提供清晰的反馈对于可用性至关重要。
本课的重点是通过自定义数据注解消息来提高验证错误的清晰度,并把复杂的配置逻辑抽象到可复用的 IServiceCollection 扩展方法中,这些方法利用了 BindConfiguration 和 ValidateDataAnnotations。

### 核心概念

- 使用 ErrorMessage 属性自定义数据注解的错误消息。
- 使用 AllowedValues 特性把配置选项限制为一组特定的字符串。
- 实现 IServiceCollection 扩展方法来封装库的设置逻辑。
- 组合使用 AddOptionsWithValidateOnStart、BindConfiguration 和 ValidateDataAnnotations,获得健壮的"快速失败"配置体验。

### 课程笔记

为了改善库使用者的开发者体验,在配置值无效时提供可据以行动的反馈非常重要。
默认情况下,某些数据注解可能会产生笼统的错误消息,并不能清楚地说明如何解决问题。
例如,在使用 `AllowedValues` 特性时,可以提供一个自定义的 `ErrorMessage`,引导用户输入有效值。

```csharp
[OptionsValidator]
public sealed partial class WidgetOptions : IValidateOptions<WidgetOptions>
{
    [Url, Required(AllowEmptyStrings = false)]
    public string? ImageUrl { get; set; }

    [AllowedValues(["Red", "Green", "Blue"],
        ErrorMessage = """
            Please provide either "Red", "Green", "Blue" as the color.
            """)]
    public string Color { get; set; } = "Blue";

    public int Size { get; set; } = 10;

    public bool IsEnabled { get; set; } = true;

    public double Opacity { get; set; } = 1.0;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/improving-consumer-experiences-through-meaningful-error-messages-58112816/?t=20)

除了清晰的错误消息之外,库作者还应当提供一个精简的服务注册 API。
一种常见模式是为 `IServiceCollection` 创建一个同时处理服务注册和配置绑定的扩展方法。
该方法应当验证必需的参数(例如 `IServiceCollection` 实例和配置节路径)不为 null。

使用 `AddOptionsWithValidateOnStart<T>` 方法可以确保验证在应用程序一启动时就进行,而不是在首次访问选项时。
通过链式调用 `BindConfiguration(configSectionPath)` 和 `ValidateDataAnnotations()`,库可以自动从特定配置节绑定选项,并强制执行选项类上定义的验证规则。

```csharp
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds widget services to the given <paramref name="services"/> collection.
    /// Then add options for the <see cref="WidgetOptions"/> type,
    /// and binds against the configuration with the <paramref name="configSectionPath"/>.
    /// </summary>
    /// <param name="services">
    /// The service collection to add services to.
    /// </param>
    /// <param name="configSectionPath">
    /// The widget configuration section to bind options from.
    /// </param>
    /// <returns>
    /// The same <paramref name="services"/> instance with other services added.
    /// </returns>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configSectionPath);

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .BindConfiguration(configSectionPath)
            .ValidateDataAnnotations();

        // TODO:
        // Add widget services to the service collection.

        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/improving-consumer-experiences-through-meaningful-error-messages-58112816/?t=70)

## 7. Confirming new consumer experiences

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/confirming-new-consumer-experiences-58112820/) · 1:52

为了评估一个库的使用者体验,库作者应当通过在宿主应用程序的设置中提供配置值来模拟真实的使用场景。
在这个场景中,`appsettings.json` 文件被更新为给 `Color` 属性一个无效值,以触发验证逻辑。

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "WidgetOptions": {
    "Color": "Pink",
    "Size": "8",
    "IsEnabled": true,
    "ImageUrl": "https://placeholder.co/300x240/512bd4/FFFFFF/png",
    "Opacticy": "1.0"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/confirming-new-consumer-experiences-58112820/?t=10)

### 核心概念

- 测试库注册 API 的使用者体验。
- 使用字符串字面量路径映射配置节。
- 使用 `OptionsValidationException` 在启动时验证配置。
- 向使用者呈现自定义的 DataAnnotation 错误消息。

### 课程笔记

库的使用者通过一个接受配置节路径的扩展方法来注册服务。
这个 API 把配置中指定的节(例如 `"WidgetOptions"`)直接映射到库的选项类。

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

// The Add* API expects a string that represents the path
// to map to the "WidgetOptions" object.
builder.Services.AddWidgetServices(
    configSectionPath: "WidgetOptions");

var host = builder.Build();

await host.StartAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/confirming-new-consumer-experiences-58112820/?t=40)

当应用程序启动时,配置会被验证。
如果提供的值不满足选项类中定义的条件(例如为 `Color` 属性定义的自定义 DataAnnotation 错误消息),应用程序就会启动失败并抛出 `OptionsValidationException`。

```text
fail: Microsoft.Extensions.Hosting.Internal.Host[11]
      Hosting failed to start
      Microsoft.Extensions.Options.OptionsValidationException: DataAnnotation validation failed for 'WidgetOptions' members:
'Color' with the error: 'Please provide either "Red", "Green", "Blue" as the color'.
         at Microsoft.Extensions.Options.OptionsFactory`1.Create(String name)
         at Microsoft.Extensions.Options.OptionsMonitor`1.<>c.<Get>b__10_0(String name, IOptionsFactory`1 factory)
         at Microsoft.Extensions.Options.OptionsCache`1.<>c__DisplayClass3_1`1.<GetOrAdd>b__2()
         at System.Lazy`1.ViaFactory(LazyThreadSafetyMode mode)
         at System.Lazy`1.ExecutionAndPublication(LazyHelper executionAndPublication, Boolean useDefaultConstructor)
         at System.Lazy`1.CreateValue()
         at Microsoft.Extensions.Options.OptionsCache`1.GetOrAdd[TArg](String name, Func`3 createOptions, TArg factoryArgument)
         at Microsoft.Extensions.Options.OptionsMonitor`1.Get(String name)
         at Microsoft.Extensions.DependencyInjection.OptionsBuilderExtensions.<>c__DisplayClass0_1`1.<Start>b__1()
         at Microsoft.Extensions.Options.StartupValidator.Validate()
      --- End of stack trace from previous location ---
         at Microsoft.Extensions.Options.StartupValidator.Validate()
         at Microsoft.Extensions.Hosting.Internal.Host.StartAsync(CancellationToken cancellationToken)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/confirming-new-consumer-experiences-58112820/?t=55)

在选项类中提供有意义的、逐字呈现的错误消息至关重要。
它确保使用者能够准确理解配置为何无效以及如何修正,而无需查阅外部文档或源代码。

## 8. Considerations when accepting Option-types as Add\* API parameters

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considerations-when-accepting-option-types-as-add-api-parameters-58112825/) · 1:52

### 总结

当库作者提供接受具体选项实例的扩展方法时,他们必须决定是使用 `Configure` 还是 `PostConfigure` 来应用这些选项。
使用 `PostConfigure` 可以让所提供的实例成为最终的事实来源(source of truth),覆盖之前通过选项系统应用的任何配置。
要确保传给 `Add*` 方法的显式参数优先于配置文件或其他注册调用,这种模式必不可少。

### 核心概念

- **直接注入选项**:在服务注册方法中接受一个预先构造好的选项对象(例如 `WidgetOptions`)作为参数。
- **事实来源**:使用 `PostConfigure` 确保方法调用中提供的值覆盖之前通过 `IConfiguration` 或其他 `Configure` 调用设置的任何值。
- **执行顺序**:理解 `Configure` 在所有 `PostConfigure` 调用之前运行,而 `PostConfigure` 在所有 `Configure` 调用之后运行。
- **验证**:集成 `AddOptionsWithValidateOnStart` 和 `ValidateDataAnnotations`,确保所提供的选项在应用程序启动时立即得到验证。

### 课程笔记

在设计库时,你可以提供一个扩展方法,允许使用者直接传入选项类的实例。
当使用者希望以编程方式定义设置,而不是只依赖配置提供程序时,这种做法很有用。

第一步是标准的参数验证,确保 `IServiceCollection` 和所提供的选项实例不为 null。

```csharp
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds widget services to the given <paramref name="services"/> collection.
    /// Post configures the <paramref name="widgetOptions"/> as the source of
    /// truth, overriding all other previously configured values.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="widgetOptions">The widget existing instance to bind to.</param>
    /// <returns>The same <paramref name="services"/> instance with other services added.</returns>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        WidgetOptions widgetOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(widgetOptions);

        // Implementation follows...
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considerations-when-accepting-option-types-as-add-api-parameters-58112825/?t=10)

作为库作者,你必须决定是使用 `.Configure()` 还是 `.PostConfigure()` 来应用这些选项。
这一选择取决于期望的优先级:
- `.Configure()`:这些委托在任何 `PostConfigure` 调用之前运行。
- `.PostConfigure()`:这些委托在所有 `Configure` 调用之后运行,实际上让库作者对配置拥有"最终决定权"。

```text
Configure #1 -> Configure #2 -> ... -> PostConfigure #1 -> PostConfigure #2
|<------- 所有 Configure 先运行 ------>|<--- 所有 PostConfigure 后运行 -->|
                                                          (最终决定权)
```

为了确保用户提供的 `widgetOptions` 实例充当事实来源,应使用 `PostConfigure`。
这样你就可以把所提供实例的属性映射到容器中已注册的现有选项上,覆盖之前的任何值。

```csharp
services.AddOptionsWithValidateOnStart<WidgetOptions>()
    .PostConfigure(configureOptions: WidgetOptions existing =>
    {
        // Overwrite existing values with
        // user provided values.
        existing.Color = widgetOptions.Color;
        existing.ImageUrl = widgetOptions.ImageUrl;
    })
    .ValidateDataAnnotations();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considerations-when-accepting-option-types-as-add-api-parameters-58112825/?t=25)

通过调用 `AddOptionsWithValidateOnStart`,库确保选项在应用程序一启动时就得到验证。
`PostConfigure` 块接收 `existing` 选项(它可能已被更早的配置调用填充),并用 `widgetOptions` 参数中的值更新它。

最后,该方法应当注册所有必要的服务,并返回 `IServiceCollection` 以支持方法链式调用。

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        WidgetOptions widgetOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(widgetOptions);

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .PostConfigure(configureOptions: WidgetOptions existing =>
            {
                // Overwrite existing values with
                // user provided values.
                existing.Color = widgetOptions.Color;
                existing.ImageUrl = widgetOptions.ImageUrl;
            })
            .ValidateDataAnnotations();

        // Add widget services to the service collection.

        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/considerations-when-accepting-option-types-as-add-api-parameters-58112825/?t=100)

## 9. Consuming the Add\* with Options-type API

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-options-type-api-58112835/) · 2:17

### 总结

本课演示如何使用一个接受具体选项实例的库服务注册 API。
它强调库作者有责任把用户提供的实例中的所有属性正确映射到内部的选项容器(通常使用 .PostConfigure),以确保满足使用者的预期,并正确应用配置值。

### 核心概念

- 通过具体选项实例进行服务注册。
- 把用户提供的选项映射到内部的 `IOptions` 系统。
- 使用 `.PostConfigure` 确保用户提供的值覆盖默认值。
- 避免库扩展方法中的部分映射 bug。

### 课程笔记

当库提供一个接受选项类具体实例的扩展方法时,使用者可以内联地初始化该对象。
这种做法让使用者能够在服务注册的地方指定所有配置值。

```csharp
#region .AddWidgetServices(WidgetOptions widgetOptions)

// The Add* API expects a WidgetOptions instance.
builder.Services.AddWidgetServices(
    new WidgetOptions
    {
        Color = "Red",
        ImageUrl = "https://bit.ly/net-widget",
        Size = 420,
        IsEnabled = true,
        Opacity = 0.69
    });

#endregion

var host = builder.Build();

await host.StartAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-options-type-api-58112835/?t=10)

构建主机之后,可以使用 `IOptions<T>` 接口从依赖注入容器中解析选项。
这让应用程序可以验证注册时提供的值已被正确应用到选项实例上。

```csharp
var options =
    host.Services.GetRequiredService<IOptions<WidgetOptions>>();

var widgetOptions = options.Value;

if (widgetOptions is not null)
{
    // Verify configuration values
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-options-type-api-58112835/?t=50)

库作者的一个常见陷阱是实现出部分映射的 bug。
如果库的内部实现只映射了所提供选项实例中的一部分属性,使用者的预期就无法得到满足。
例如,如果使用者设置了五个属性,而库只映射了两个,剩下的三个就会保留它们的默认值。

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWidgetServices(this IServiceCollection services, WidgetOptions widgetOptions)
    {
        ArgumentNullException.ThrowIfNull(widgetOptions);

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .PostConfigure(existing =>
            {
                // Overwrite existing values with
                // user provided values.
                existing.Color = widgetOptions.Color;
                existing.ImageUrl = widgetOptions.ImageUrl;
                // BUG: Missing Size, IsEnabled, and Opacity mapping
            })
            .ValidateDataAnnotations();

        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/consuming-the-add-with-options-type-api-58112835/?t=85)

```text
widgetOptions (使用者提供)          existing (容器中的选项)
  Color     = "Red"        ---->      Color     = "Red"
  ImageUrl  = "https://..."  ---->    ImageUrl  = "https://..."
  Size      = 420            x        Size      = 10    (默认值)
  IsEnabled = true           x        IsEnabled = true  (默认值)
  Opacity   = 0.69           x        Opacity   = 1.0   (默认值)
```

要修复这个问题,库作者必须确保在 `.PostConfigure` 委托中,把选项类型上的每一个可用属性都从用户提供的实例映射到 `existing` 实例。
这确保配置值能在依赖注入系统中正确传递,并在解析选项时被准确地体现出来。

## 10. Exposing an Action API

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exposing-an-action-api-58112836/) · 1:43

### 总结

在服务集合扩展方法中提供一个 `Action<TOptions>` 重载,可以让库的使用者直接通过代码配置选项。
这种做法补充了基于配置绑定的 API,提供了一种以编程方式设置或覆盖选项的途径。
其实现通常包括:向 DI 容器注册选项、应用所提供的配置委托,并确保执行验证逻辑(例如数据注解),以维护配置的完整性。

### 核心概念

- **基于 Action 的配置**:使用 `Action<TOptions>` 委托,让使用者以编程方式定义选项值。
- **执行顺序**:理解 `.Configure()` 调用通常在 `.PostConfigure()` 调用之前运行。
- **选项验证**:利用 `AddOptionsWithValidateOnStart<T>` 确保选项在应用程序启动时立即得到验证。
- **流式 API**:在 `OptionsBuilder<T>` 上链式调用配置和验证方法,以保持注册流程的简洁。

### 课程笔记

库作者的一个常见模式是在 `IServiceCollection` 上提供一个接受 `Action<TOptions>` 委托的扩展方法。
这让库的使用者可以用 C# 代码来配置服务的选项,而不是只依赖 `appsettings.json` 这样的外部配置文件。

实现首先验证 `IServiceCollection` 和 `Action<WidgetOptions>` 委托都不为 null。
在这个阶段,开发者必须决定是使用 `.Configure()` 还是 `.PostConfigure()` 来应用该委托。

```csharp
public static IServiceCollection AddWidgetServices(
    this IServiceCollection services,
    Action<WidgetOptions> configureOptions)
{
    ArgumentNullException.ThrowIfNull(argument: services);
    ArgumentNullException.ThrowIfNull(argument: configureOptions);

    // TODO:
    //    Determine if you want to use:
    //        .Configure(configureOptions);    // Runs before PostConfigure
    //    Or instead use:
    //        .PostConfigure(configureOptions); // Runs after all Configure
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exposing-an-action-api-58112836/?t=10)

两者的主要区别在于执行顺序:`.Configure()` 委托在任何 `.PostConfigure()` 委托之前运行。
在大多数标准场景中,`.Configure()` 是合适的选择,它让使用者的设置在标准配置阶段被应用。

为了确保选项有效,实现中使用了 `AddOptionsWithValidateOnStart<T>()`。
该方法返回一个 `OptionsBuilder<T>`,从而可以流式地链式调用配置委托和验证逻辑,例如 `ValidateDataAnnotations()`。
这确保 `WidgetOptions` 对象在应用程序一启动时就会根据其数据注解得到验证。

```csharp
public static IServiceCollection AddWidgetServices(
    this IServiceCollection services,
    Action<WidgetOptions> configureOptions)
{
    ArgumentNullException.ThrowIfNull(argument: services);
    ArgumentNullException.ThrowIfNull(argument: configureOptions);

    services.AddOptionsWithValidateOnStart<WidgetOptions>()
        .Configure(configureOptions)
        .ValidateDataAnnotations();

    // TODO:
    //    Add widget services to the service collection.

    return services;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/exposing-an-action-api-58112836/?t=40)

这种做法确保无论使用者选择哪种 API(无论是绑定到配置节,还是使用 action 委托),功能都保持一致。
选项会被绑定和验证,库也为不同的用例提供了必要的灵活性。
在这些扩展方法之间保持一致的命名,对于可预测的开发者体验同样至关重要。

## 11. The Importance of Consistent APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/the-importance-of-consistent-apis-58112837/) · 1:58

### 总结

一致的 API 命名对 .NET 库作者来说是一项关键实践,在扩展 IServiceCollection 这样的标准类型时尤其如此。
通过遵循既定约定,例如依赖注入扩展方法使用 'Add' 前缀,开发者可以创建可预测、可维护的代码库,从而促进更好的协作并减少使用者的错误。
可预测的 API 确保当前和未来的开发者都能直观地理解如何与库交互,从而让软件实现更加成功。

### 核心概念

- **标准化前缀**:在扩展 `IServiceCollection` 时使用 `Add` 前缀,以符合 .NET 生态系统的预期。
- **可读性与维护**:一致的命名让原作者和未来的维护者都更容易浏览和理解代码。
- **可预测性**:可预测的 API 名称带来可预测的使用方式,降低使用者实现出错的可能性。
- **团队协作**:共享的命名约定减少了团队环境中的困惑,确保所有开发者保持一致。

### 课程笔记

保持 API 命名的一致性,对于创建专业且易用的 .NET 库至关重要。
虽然命名常常是一项困难的任务,但遵循既定模式会显著影响开发者体验。
在 .NET 依赖注入(DI)容器的语境下,为 `IServiceCollection` 接口创建扩展方法时使用 `Add` 前缀是标准做法。
这一约定会立即向使用者表明,该方法负责把服务注册到容器中。

一致的命名带来几个关键好处:
1. **可读性**:当 API 遵循预期的模式时,代码就成了自文档化的。
2. **协作**:在团队环境中,一致性确保每个人都无需大量解释就能理解代码库,让团队合作更顺畅。
3. **防止错误**:当 API 可预测时,开发者误用它的可能性更小。如果开发者能根据命名了解一个库的意图,他们就更有可能成功地实现对它的使用。

即使是独立开发者,保持内部一致性也至关重要。
单个库内部的命名不一致,会在你回顾代码或在开发周期后期尝试使用自己的 API 时造成困惑。

下面的示例展示了一组用于小部件服务注册的、命名一致的重载。
每个重载都使用 `AddWidgetServices` 这个名称,同时提供不同的选项配置方式,例如使用 `IConfigurationSection`、字符串路径、预先构造好的选项对象,或 `Action<WidgetOptions>` 委托。

```csharp
public static class ServiceCollectionExtensions
{
    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        IConfigurationSection widgetConfigSection)
    {
        // Implementation
        return services;
    }

    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        string configSectionPath)
    {
        // Implementation
        return services;
    }

    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        WidgetOptions widgetOptions)
    {
        // Implementation
        return services;
    }

    /// <summary> Adds widget services to the given services collection. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        Action<WidgetOptions> configureOptions)
    {
        // Implementation
        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/the-importance-of-consistent-apis-58112837/?t=10)

通过以一致的命名方案提供这些重载,库作者确保使用者无论具体的配置来源是什么,都能轻松找到合适的注册方法。

## 12. Using the configureOptions delegate-based API

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-the-configureoptions-delegate-based-api-58112838/) · 2:29

### 总结

本课演示如何在 .NET 中为库服务使用基于委托的配置 API。
它涵盖了在服务注册期间使用 Action<TOptions> 手动覆盖选项值,并着重说明内置验证(例如数据注解)如何在应用程序启动时自动触发。
这确保手动配置的选项在通过 IOptions<T> 接口被使用之前,遵守已定义的约束,例如必填字段和有效的 URL 格式。

### 核心概念

- 使用 Action<TOptions> 的基于委托的配置。
- 在服务注册期间手动覆盖选项值。
- 在应用程序启动时自动验证手动配置的选项。
- 强制执行 [Required] 和 [Url] 等数据注解约束。
- 通过 IOptions<T> 接口访问已验证的选项。

### 课程笔记

基于委托的 API 允许使用者通过 Action<TOptions> 委托来配置选项。
这通常通过 AddWidgetServices 这样的扩展方法暴露出来。
在这种做法中,使用者提供一个 lambda 表达式,直接在选项对象上设置属性。

```csharp
builder.Services.AddWidgetServices(
    configureOptions: static WidgetOptions options =>
    {
        options.Color = "Seafoam Green";
        options.IsEnabled = false;
        options.Size = 77;
        options.Opacity = 1;
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-the-configureoptions-delegate-based-api-58112838/?t=40)

当应用程序主机被构建并启动时,.NET 会对已配置的选项执行验证。
如果 WidgetOptions 类使用了数据注解,任何违规都会阻止应用程序启动。
例如,如果 ImageUrl 标记了 [Required] 但没有在委托中赋值,或者 Color 被赋予了一个自定义验证特性不允许的值(例如只允许 "Red"、"Green" 或 "Blue"),就会立即抛出验证错误。

在下面的示例中,添加了 ImageUrl,但提供的值不是有效的 URL 格式。

```csharp
builder.Services.AddWidgetServices(
    configureOptions: static WidgetOptions options =>
    {
        options.Color = "Green";
        options.ImageUrl = "fake.url";
        options.IsEnabled = false;
        options.Size = 77;
        options.Opacity = 1;
    });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-the-configureoptions-delegate-based-api-58112838/?t=85)

[Url] 特性要求一个完全限定的 URI(例如以 http://、https:// 或 ftp:// 开头)。
一旦所有属性都被正确赋值并通过验证,就可以在应用程序中通过从依赖注入容器解析 IOptions<T> 来使用这些选项。

```csharp
var IOptions<WidgetOptions>? options =
    host.Services.GetRequiredService<IOptions<WidgetOptions>>();

var WidgetOptions? widgetOptions = options.Value;

if (widgetOptions is not null)
{
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/using-the-configureoptions-delegate-based-api-58112838/?t=130)

这确保了使用方代码总能收到一个有效且完全初始化的选项实例。

## 13. Section Recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112839/) · 1:19

### 总结

本课全面回顾了库作者在 .NET 中实现选项模式时的最佳实践。
它强调以消费者为中心的方法,专注于提供灵活且一致的 API,让用户能够通过多种方式配置服务,包括配置节、字符串路径和直接的对象实例。
本课还讨论了把扩展方法放在 Microsoft.Extensions.DependencyInjection 命名空间中、以提高库使用者对其可发现性的常见做法。

### 核心概念

- **以消费者为中心的设计**:优先考虑库使用者的开发者体验。
- **命名空间约定**:对扩展方法使用 `Microsoft.Extensions.DependencyInjection`,以增强 API 的可发现性。
- **API 灵活性**:为服务注册提供多个重载,以支持不同的配置来源(配置节、路径、对象)。
- **库作者的控制权**:在库的内部逻辑中管理后置配置、绑定和验证规则。
- **一致性**:在整个库的对外接口上保持统一的命名和 API 模式。

### 课程笔记

设计一个库需要以消费者为中心的视角来看待选项模式。
一个首要的考虑是扩展方法所使用的命名空间。
虽然库的核心逻辑通常位于它自己的特定命名空间中(例如 `Example.Library`),但最佳实践是把 `IServiceCollection` 扩展方法放在 `Microsoft.Extensions.DependencyInjection` 命名空间中。
这确保当使用者尝试在其启动代码中注册服务时,这些方法可以立即通过 IntelliSense 获得,而无需额外的 `using` 指令。

库作者应当暴露多个 API,让使用者能够以最适合其应用程序结构的方式配置选项。
这通常需要为注册方法(例如 `AddWidgetServices`)提供多个重载。
这些重载可以接受一个 `IConfigurationSection`、一个表示配置路径的字符串,或者选项类的一个直接实例。

```csharp
using Example.Library;
using Microsoft.Extensions.Configuration;

// There's some debate over this namespace!
namespace Microsoft.Extensions.DependencyInjection;

/// <summary> Represents a set of extension methods on the IServiceCollection type. </summary>
public static class ServiceCollectionExtensions
{
    /// <summary> Adds widget services to the given services collection. Configures widget options using a configuration section. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        IConfigurationSection widgetConfigSection)
    {
        // Implementation logic...
        return services;
    }

    /// <summary> Adds widget services to the given services collection using a configuration path. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        string configSectionPath)
    {
        // Implementation logic...
        return services;
    }

    /// <summary> Adds widget services to the given services collection using a pre-configured options object. </summary>
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        WidgetOptions widgetOptions)
    {
        // Implementation logic...
        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-configuration-and-options-in-dotnet-2870703/section-recap-58112839/?t=10)

通过评估不同的使用场景,库作者可以确定让使用者与库交互的最有效方式。
这包括决定是否支持通过委托进行配置(这允许动态调整),或者是否直接绑定到配置键。

归根结底,库作者掌控着配置的生命周期。
这包括施加验证规则、处理后置配置逻辑,以及确保绑定过程健壮的能力。
在这些不同的入口点之间保持命名和 API 设计的一致性,对于打造可预测且专业的开发者体验至关重要。

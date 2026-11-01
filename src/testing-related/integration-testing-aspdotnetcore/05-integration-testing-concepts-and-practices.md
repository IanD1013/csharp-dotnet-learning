# Integration testing concepts and practices

> 课程:[From Zero to Hero: Integration testing in ASP.NET Core](https://dometrain.com/course/from-zero-to-hero-integration-testing-in-asp-net-core/) · 第 5 章
> 共 10 课 · 约 27 分钟
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The problem with integration tests](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-integration-tests-53953887/) | 1:14 | [↓](#1-the-problem-with-integration-tests) |
| 2 | [Introducing the WebApplicationFactory](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-the-webapplicationfactory-53953888/) | 5:06 | [↓](#2-introducing-the-webapplicationfactory) |
| 3 | [Testing status codes](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-status-codes-53953890/) | 1:58 | [↓](#3-testing-status-codes) |
| 4 | [Testing text responses](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-text-responses-53953891/) | 1:31 | [↓](#4-testing-text-responses) |
| 5 | [Testing JSON responses](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-json-responses-53953893/) | 1:34 | [↓](#5-testing-json-responses) |
| 6 | [Testing response headers](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-response-headers-53953894/) | 1:00 | [↓](#6-testing-response-headers) |
| 7 | [Creating realistic test data](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-realistic-test-data-53953895/) | 5:39 | [↓](#7-creating-realistic-test-data) |
| 8 | [Cleaning up the test data](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/cleaning-up-the-test-data-53953896/) | 2:31 | [↓](#8-cleaning-up-the-test-data) |
| 9 | [Sharing a single application for multiple test classes](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/sharing-a-single-application-for-multiple-test-classes-53953897/) | 5:32 | [↓](#9-sharing-a-single-application-for-multiple-test-classes) |
| 10 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953898/) | 0:37 | [↓](#10-section-recap) |

## 1. The problem with integration tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-integration-tests-53953887/) · 1:14

### 总结

ASP.NET Core 中的集成测试经常面临与外部依赖和手动环境编排相关的挑战。
当测试需要一个预先运行的数据库和一个手动启动的 API 实例时,测试过程就会变得脆弱且难以自动化。
WebApplicationFactory 类提供了一种现代化的解决方案:它在内存中管理应用程序的生命周期和依赖,大幅减少了传统集成测试配置所带来的摩擦。

### 核心概念

- 外部基础设施依赖(例如本地数据库)。
- 手动管理 API 的生命周期。
- 硬编码测试配置的脆弱性。
- 过去为测试做项目检测和构建自动化的复杂性。
- 引入 WebApplicationFactory 作为一种更精简的替代方案。

### 课程笔记

集成测试涉及验证应用程序各个组件之间的交互。
这个过程中的一个重大障碍是对外部系统的依赖。
在典型的手动配置中,除非机器上已经运行着数据库,并且 API 已经被单独启动,否则测试无法执行。

过去,解决这些编排问题需要复杂的自定义代码。
开发者必须编写工具来在文件系统中定位项目、构建它并启动进程,仅仅是为了执行一个测试套件。
此外,应用程序的启动逻辑往往需要条件判断,以确定它是否运行在测试环境中,从而把真实服务替换为 mock 或 stub。

```csharp
using Customers.Api.Database;
using Customers.Api.Repositories;
using Customers.Api.Services;
using Customers.Api.Validation;
using FluentValidation.AspNetCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

var config = builder.Configuration;
config.AddEnvironmentVariables("CustomersApi_");

builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<Program>();
    x.DisableDataAnnotationsValidation = true;
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-integration-tests-53953887/?t=40)

ASP.NET Core 中 WebApplicationFactory 类的引入解决了这些问题。
在它出现之前,测试项目通常配置一个硬编码的 HttpClient,指向 API 的本地实例,从而对本地环境形成了脆弱的依赖。

```csharp
using System.Net;
using Xunit;

namespace Customers.Api.Tests.Integration;

public class CustomerControllerTests
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:5001")
    };

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-integration-tests-53953887/?t=55)

在这种手动配置中,测试方法完全依赖 API 的外部状态。
如果 API 没有在指定端口上运行,测试就会在请求阶段失败,这使它不适合用于自动化的 CI/CD 流水线。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-integration-tests-53953887/?t=70)

WebApplicationFactory 通过在内存中引导启动应用程序,并提供一个在内部处理路由的托管 HttpClient 来简化这一切,从而无需手动管理 API。

## 2. Introducing the WebApplicationFactory

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-the-webapplicationfactory-53953888/) · 5:06

### 总结

WebApplicationFactory 是 ASP.NET Core 集成测试的核心组件,它允许开发者在测试执行期间把 API 托管在内存中。
通过使用 Microsoft.AspNetCore.Mvc.Testing 包,开发者可以实例化一个测试服务器,它会自动处理 API 的生命周期:在测试运行时启动它,并在完成后将其销毁。
这种方式提供了一个预先配置好、直接与内存服务器通信的 HttpClient,无需外部托管或手动管理环境,同时确保测试保持快速且相互隔离。

### 核心概念

- Microsoft.AspNetCore.Mvc.Testing NuGet 包。
- 通过 WebApplicationFactory<TEntryPoint> 在内存中托管 API。
- 使用标记类型(例如 IApiMarker)进行程序集扫描。
- 自动的生命周期管理(启动与销毁)。
- 使用 xUnit 的 IClassFixture 共享资源。

### 课程笔记

要实现针对 API 实时实例运行的集成测试,你必须先安装 Microsoft.AspNetCore.Mvc.Testing NuGet 包。
这个包提供的主要工具是 WebApplicationFactory<TEntryPoint> 类。
这个类会在内存中实例化并运行 API,让你无需手动管理服务器进程就能测试完整的请求管道。

该工厂使用程序集扫描来识别它需要托管的应用程序。
它需要一个位于目标 API 项目中的类型来定位入口点。
虽然你可以使用 Program 类,但一种常见且整洁的做法是在 API 项目中定义一个空的标记接口。

```csharp
namespace Customers.Api;

public interface IApiMarker { }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-the-webapplicationfactory-53953888/?t=145)

定义好标记之后,你就可以用它来创建工厂的实例。
该工厂提供了一个 CreateClient() 方法,它返回一个预先配置好、用于与内存服务器通信的 HttpClient。
这个客户端会自动处理基地址和路由。

```csharp
public class CustomerControllerTests
{
    private readonly WebApplicationFactory<IApiMarker> _appFactory = new();
    private readonly HttpClient _httpClient;

    public CustomerControllerTests()
    {
        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-the-webapplicationfactory-53953888/?t=205)

在这种配置下,API 并不是作为独立进程运行的;它只在测试期间存在。
当测试被释放时,API 也随之被释放。
然而,在测试类中把工厂实例化为私有字段是低效的,因为它会为每个测试方法创建一个新的 API 实例。

为了优化这一点,你应该使用 xUnit 的 IClassFixture<T>。
这个接口确保 WebApplicationFactory 在每个测试类中只被实例化一次。
随后工厂实例会被注入到测试类的构造函数中,你可以在那里生成 HttpClient。

```csharp
public class CustomerControllerTests : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    public CustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-the-webapplicationfactory-53953888/?t=280)

这次重构确保类中的所有测试都复用同一个内存 API 实例,大幅减少了为每个单独测试启动和停止服务器的开销。

```text
  private field                      IClassFixture<T>
  ------------------------------     ------------------------------
  CustomerControllerTests            CustomerControllerTests
    test 1 -> new API instance         test 1 --+
    test 2 -> new API instance         test 2 --+--> one
    test 3 -> new API instance         test 3 --+    WebApplicationFactory
                                                     <IApiMarker>
```

## 3. Testing status codes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-status-codes-53953890/) · 1:58

### 总结

ASP.NET Core 中的集成测试把状态码作为请求成功或失败的主要指标。
通过使用 WebApplicationFactory 提供的 HttpClient,测试可以借助 GetAsync 等方法访问完整的 HttpResponseMessage,这些方法比返回原始内容的方法提供更多元数据。
本课强调针对具体状态码进行验证,而不是做通用的成功检查,并利用 Fluent Assertions 确保 API 在特定测试场景下返回确切的预期状态,例如 404 Not Found。

### 核心概念

* **HttpResponseMessage**:HttpClient 方法(GetAsync、PostAsync 等)返回的对象,包含完整的响应上下文。
* **状态码验证**:使用具体的 HTTP 状态码(例如 404、201)来验证 API 的行为。
* **Fluent Assertions**:一个用于针对响应状态编写更易读、更具表达力的断言的库。
* **避免 EnsureSuccessStatusCode**:为什么具体的断言优于通用的 200 范围检查。

### 课程笔记

使用 `WebApplicationFactory` 时,与 API 的交互通过 `HttpClient` 来完成。
为了正确验证某个端点的行为,建议使用返回 `HttpResponseMessage` 的 HTTP 方法重载。
这些方法包括 `GetAsync`、`PostAsync`、`PutAsync` 和 `DeleteAsync`。

使用这些方法优于使用只返回响应体(例如字符串或反序列化后的对象)的方法,因为 `HttpResponseMessage` 提供了对状态码、响应头以及其他元数据的访问,而这些对于健壮的集成测试至关重要。

```csharp
public class CustomerControllerTests : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    public CustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-status-codes-53953890/?t=10)

#### 具体断言与通用检查

虽然 `HttpResponseMessage` 类包含一个名为 `EnsureSuccessStatusCode()` 的方法,但在集成测试中通常不建议使用它。
如果状态码不在 200-299 范围内,这个方法就会抛出异常,因此它是一种对成功的通用检查。

```csharp
public bool IsSuccessStatusCode
{
    get { return ((int)_statusCode >= 200) && ((int)_statusCode <= 299); }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-status-codes-53953890/?t=95)

在一个定义良好的测试套件中,断言应该是具体的。
例如,如果某个端点预期会创建一个资源,测试就应该专门断言 `201 Created` 状态,而不是 200 范围内的任意成功码。
这种具体性确保 API 遵循正确的 RESTful 模式,并在测试失败时提供更好的诊断信息。

为了让这些断言更易读,我们使用了 `FluentAssertions` 包。
它允许在检查响应的 `StatusCode` 属性时使用自然语言式的语法。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-status-codes-53953890/?t=85)

## 4. Testing text responses

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-text-responses-53953891/) · 1:31

### 总结

测试响应体是集成测试的关键部分,而在许多场景下,把响应当作纯文本处理是最简单的方式。
ASP.NET Core 提供了直接的方法来访问响应内容,并将其转换为各种格式,包括字符串、流或字节数组。
通过使用 ReadAsStringAsync 把内容读取为字符串,开发者可以快速进行断言,验证响应中包含预期的值,即使底层数据是以 JSON 结构组织的。

### 核心概念

*   **访问响应内容**:通过 `HttpResponseMessage` 对象的 `Content` 属性访问 HTTP 响应的主体。
*   **ReadAsStringAsync**:一个异步方法,用于把响应体提取为纯字符串。
*   **基于文本的断言**:使用 FluentAssertions 等断言库来验证响应体中的子字符串或模式(例如 `text.Should().Contain("404")`)。
*   **通用测试与结构化测试**:在进行完整反序列化之前,先把结构化数据(如 JSON)当作文本做简单、通用的验证。

### 课程笔记

在进行集成测试时,仅验证状态码往往是不够的;你还必须验证响应体。
ASP.NET Core 内置了一些方法,让这个过程变得高效。

要检查响应体,你需要访问 `HttpResponseMessage` 的 `Content` 属性。
从那里开始,你有几种处理数据的选择:可以把它复制到流中、作为流读取、作为字节数组读取,或者作为字符串读取。
对于纯文本或对结构化数据的简单检查,作为字符串读取是最常见的做法。

```csharp
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var text = await response.Content.ReadAsStringAsync();

    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-text-responses-53953891/?t=40)

务必记得对 `ReadAsStringAsync()` 调用使用 `await`,因为它是一个异步操作。
一旦以字符串形式取得内容,你就可以对它执行任何标准的字符串断言。

即使 API 返回的是 JSON 对象,你仍然可以把它当作文本来做通用断言。
这在验证响应是否包含特定错误码或消息时特别有用,而无需承担把响应反序列化为特定 C# 类型的开销。
例如,你可以断言返回的文本中包含像 "404" 这样的特定错误标识。

```csharp
[Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var text = await response.Content.ReadAsStringAsync();
        text.Should().Contain("404");
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-text-responses-53953891/?t=85)

虽然这种方法对于非结构化数据或简单检查很有效,但涉及 JSON 对象的更复杂场景通常会通过把内容反序列化为类型化对象来处理,这样可以进行更精确的属性级断言。

## 5. Testing JSON responses

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-json-responses-53953893/) · 1:34

### 总结

集成测试经常需要验证 JSON 响应的内容。
通过把响应体反序列化为强类型对象(例如 ValidationProblemDetails),开发者可以对错误标题和状态码等特定字段进行精确断言,以确保 API 在各种条件下都按预期运行。

### 核心概念

- 使用 `ReadFromJsonAsync<T>` 反序列化 JSON 响应内容。
- 使用 `ValidationProblemDetails` 处理标准的 RFC 7807 错误响应。
- 对 `Title` 和 `Status` 等特定 JSON 属性进行断言。
- 验证 API 在 "Not Found" 场景下的行为。
- 验证响应元数据和扩展字段,例如 `traceId`。

### 课程笔记

在进行集成测试时,通常需要验证 JSON 响应中返回的具体数据。
这种方式的主要优势在于能够把响应体映射为强类型的 C# 对象,从而可以针对 API 的契约进行精确断言。

一开始,测试可能只验证响应的 HTTP 状态码:

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-json-responses-53953893/?t=10)

要检查实际返回的数据,可以使用 `ReadFromJsonAsync<T>` 扩展方法对响应内容进行反序列化。
当请求失败时(例如尝试获取一个不存在的客户),API 通常会返回一个 `ValidationProblemDetails` 对象。
这个对象遵循 HTTP API 问题详情(problem details)的 RFC 7807 规范。

通过把 `ValidationProblemDetails` 指定为泛型类型,响应会被映射为一个结构化对象,从而可以访问 `Title`、`Status` 和 `Type` 等属性。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    problem!.Title.Should().Be("Not Found");
    problem.Status.Should().Be(404);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-json-responses-53953893/?t=60)

在 "Not Found" 场景中,测试断言 `Title` 属性为 "Not Found",并且 `Status` 为 404。
这种细致程度确保 API 不仅返回了正确的状态码,还提供了预期的错误信息。
此外,映射响应还能让开发者验证其他元数据(例如扩展字段中的 `traceId`)是否被正确填充。
这种全面的验证确保 API 的响应保持一致,并符合其既定规范。

## 6. Testing response headers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-response-headers-53953894/) · 1:00

### 总结

ASP.NET Core 中的集成测试不只是验证响应体和状态码;响应头经常包含重要的元数据,例如资源创建场景中的 Location 头。
通过访问 HttpResponseMessage 的 Headers 属性,开发者可以对标准头的值进行断言,或者使用 GetValues 获取自定义头,从而确保 API 的通信协议得到完整验证。

### 核心概念

* 通过 `HttpResponseMessage.Headers` 属性访问响应头。
* 验证 `Location` 等标准头。
* 使用 `GetValues` 方法获取非标准头或自定义头。
* 在资源创建(HTTP 201 Created)场景中验证响应头的重要性。
* 区分常见的验证目标(状态、主体、响应头)和小众的元数据(版本、原因短语)。

### 课程笔记

在集成测试中,虽然状态码和响应体是最常被验证的部分,但 HTTP 响应头对于验证 API 契约同样重要。
这一点对返回 `201 Created` 状态的操作尤其相关,此时 `Location` 头指明了新创建资源的 URI。

要验证响应头,你需要访问 `HttpResponseMessage` 对象上的 `Headers` 属性。
这个属性提供了对服务器返回的所有响应头的访问。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    problem!.Title.Should().Be("Not Found");
    problem.Status.Should().Be(404);
    response.
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-response-headers-53953894/?t=10)

在对特定响应头进行断言时,对于标准头可以使用类型化的属性。
例如,要验证 `Location` 头,你可以访问 `response.Headers.Location` 并把它转换为字符串来进行断言。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    problem!.Title.Should().Be("Not Found");
    problem.Status.Should().Be(404);
    response.Headers.Location.ToString().Should().Be("https://localhost:5001/customers/...");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-response-headers-53953894/?t=25)

对于不包含在标准类型化集合中的响应头,可以使用 `GetValues` 方法获取与特定头名称关联的值。
虽然响应对象上还提供了 HTTP 版本或原因短语等其他属性,但它们被认为是小众的,在标准集成测试中很少作为断言对象。
主要的关注点仍然是状态码、响应体以及相关的响应头。

## 7. Creating realistic test data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-realistic-test-data-53953895/) · 5:39

### 总结

集成测试经常需要复杂的数据模型。
手动定义这些数据既耗时,又会导致测试臃肿且重复。
Bogus 库提供了一个健壮的解决方案,它使用流式 API 生成逼真的假数据(姓名、电子邮件、日期)。
通过把数据生成集中到一个 Faker<T> 实例中,测试会变得更整洁、更易维护,也更贴近真实,从而确保系统能处理与真实世界输入高度相似的数据。

### 核心概念

*   **手动录入数据**:硬编码测试数据会导致代码重复和测试方法臃肿。
*   **Bogus 库**:一个用于为 .NET 应用程序生成逼真假数据的 NuGet 包。
*   **Faker<T>**:Bogus 中的核心类,用于为特定类型定义生成规则。
*   **流式规则**:使用 `.RuleFor()` 把对象属性映射到假数据来源。
*   **逼真的数据来源**:访问预定义的数据类别,例如 `Person`(用于姓名、电子邮件和生日)、`Address`、`Company` 等。
*   **数据一致性**:在需要外部验证时,把特定值(例如硬编码的有效用户名)与生成的数据一起使用。

### 课程笔记

在编写集成测试时,一个常见的挑战是手动创建测试数据。
手动填充请求对象会让测试显得臃肿且重复,如下面这个最初的实现所示:

```csharp
public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
{
    // Arrange
    var customer = new CustomerRequest
    {
        FullName = "Nick Chapsas",
        Email = "nick@nickchapsas.com",
        GitHubUsername = "nickchapsas",
        DateOfBirth = new DateTime(1993, 01, 01)
    };

    // Act
    var response = await _httpClient.PostAsJsonAsync("customers", customer);

    // Assert
    var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    customerResponse.Should().BeEquivalentTo(customer);
    response.StatusCode.Should().Be(HttpStatusCode.Created);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-realistic-test-data-53953895/?t=10)

有人可能会尝试用 `new Random()` 生成字符串或整数来解决这个问题,但这会产生不真实的数据,无法代表真实世界的场景:

```csharp
public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
{
    // Arrange
    var customer = new CustomerRequest
    {
        FullName = new Random().Next().ToString(),
        Email = "nick@nickchapsas.com",
        GitHubUsername = "nickchapsas",
        DateOfBirth = new DateTime(1993, 01, 01)
    };
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-realistic-test-data-53953895/?t=85)

#### 实现 Bogus

要生成逼真的数据,`Bogus` NuGet 包是业界标准。
它允许创建一个生成器,在每次测试运行时都能产生唯一且逼真的数据。
生成器通常使用 `Faker<T>` 类,定义为测试类中的一个私有字段。

规则通过 `RuleFor` 方法来定义。
Bogus 提供了一个 `Person` 对象,其中包含 `FullName`、`Email` 和 `DateOfBirth` 等常用属性。

在 API 会执行外部验证的场景中(例如检查某个 GitHub 用户名是否存在),你可以把生成的数据与硬编码的有效值混合使用。
此外,在处理 `DateTime` 属性时,使用 `.Date` 通常很有用,可以确保时间部分不会在比较时导致断言失败。

```csharp
private readonly Faker<CustomerRequest> _customerGenerator = 
    new Faker<CustomerRequest>()
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.GitHubUsername, "nickchapsas")
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-realistic-test-data-53953895/?t=235)

#### 使用生成器

生成器配置好之后,你可以调用 `_customerGenerator.Generate()` 来创建请求对象的单个实例。
这会大幅简化测试的 "Arrange" 部分:

```csharp
[Fact]
public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
{
    // Arrange
    var customer = _customerGenerator.Generate();

    // Act
    var response = await _httpClient.PostAsJsonAsync("customers", customer);

    // Assert
    var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    customerResponse.Should().BeEquivalentTo(customer);
    response.StatusCode.Should().Be(HttpStatusCode.Created);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-realistic-test-data-53953895/?t=265)

Bogus 用途非常广泛,可以为地址、商业、金融等生成数据,这使它成为集成测试和单元测试中必不可少的工具。

## 8. Cleaning up the test data

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/cleaning-up-the-test-data-53953896/) · 2:31

与持久化数据库交互的集成测试经常会留下残留物,这些残留物可能干扰后续的测试运行,或者让环境变得杂乱。
为了维持一个干净且确定性的测试状态,开发者应该实现清理逻辑。
在 xUnit 中,这是通过以下方式实现的:实现 IAsyncLifetime 接口,跟踪测试期间创建的资源的标识符,并在 DisposeAsync 方法中以编程方式删除这些资源。

### 核心概念

*   **数据残留(Data Littering)**:测试生成的记录在数据库中不断累积,并在多次测试执行之间一直保留。
*   **IAsyncLifetime**:一个 xUnit 接口,提供异步的初始化(`InitializeAsync`)和清理(`DisposeAsync`)钩子。
*   **状态跟踪**:使用一个集合(例如 `List<Guid>`)来存储测试执行期间创建的实体的标识符。
*   **确定性测试**:确保测试让系统保持在它开始时的状态,防止副作用影响以后的运行。

### 课程笔记

在针对持久化数据库运行集成测试时,每个创建资源的测试都会留下一个残留物。
为了防止这种"数据残留",测试应该在结束后自行清理。
xUnit 中的 `IAsyncLifetime` 接口提供了一种异步初始化和清理的机制。

```csharp
public Task InitializeAsync()
{
    throw new NotImplementedException();
}

public Task DisposeAsync()
{
    throw new NotImplementedException();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/cleaning-up-the-test-data-53953896/?t=40)

在测试类中,如果不需要特定的初始化,`InitializeAsync` 通常可以设置为返回 `Task.CompletedTask`。
清理的重点在于 `DisposeAsync`。
为了跟踪哪些记录需要删除,要在测试类中维护一个 ID 列表:

```csharp
private readonly List<Guid> _createdIds = new();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/cleaning-up-the-test-data-53953896/?t=70)

在执行一个创建客户的测试时,会从响应中捕获生成的 ID,并将其添加到跟踪列表中。
这确保了即使测试之后在断言阶段失败,该 ID 也已经被记录下来以便清理。

```csharp
[Fact]
public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
{
    // Arrange
    var customer = _customerGenerator.Generate();

    // Act
    var response = await _httpClient.PostAsJsonAsync("customers", customer);

    // Assert
    var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    customerResponse.Should().BeEquivalentTo(customer);
    response.StatusCode.Should().Be(HttpStatusCode.Created);

    _createdIds.Add(customerResponse!.Id);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/cleaning-up-the-test-data-53953896/?t=110)

最后,`DisposeAsync` 方法遍历收集到的 ID,并调用 API 的删除端点把数据从数据库中移除。
这种方式让测试具有确定性,并确保环境中不会留下任何痕迹。

```csharp
public Task InitializeAsync() => Task.CompletedTask;

public async Task DisposeAsync()
{
    foreach (var createdId in _createdIds)
    {
        await _httpClient.DeleteAsync($"customers/{createdId}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/cleaning-up-the-test-data-53953896/?t=135)

```text
InitializeAsync()  -> Task.CompletedTask
        |
        v
Create_ReturnsCreated_WhenCustomerIsCreated
  POST customers -> customerResponse
  _createdIds.Add(customerResponse!.Id)
        |
        v
DisposeAsync()
  foreach createdId in _createdIds
    DELETE customers/{createdId}
```

## 9. Sharing a single application for multiple test classes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/sharing-a-single-application-for-multiple-test-classes-53953897/) · 5:32

### 总结

按照控制器动作把集成测试组织到不同的类中可以提高可维护性,但这会改变 xUnit 管理测试生命周期的方式。
默认情况下,不同的测试类会并行运行,每个类都会创建自己的 WebApplicationFactory 实例和测试服务器。
本课演示如何使用 xUnit 的 CollectionDefinition 和 ICollectionFixture 把多个测试类归入同一个集合,让它们共享一个应用程序实例并按顺序执行。

### 核心概念

- 按控制器动作(例如 Create、Get)把测试组织到不同的类中。
- xUnit 的默认行为:不同的测试类并行执行。
- 维护多个 WebApplicationFactory 实例的资源开销。
- 使用 ICollectionFixture<T> 在多个测试类之间共享状态。
- 使用 [CollectionDefinition] 特性定义测试集合。
- 命名测试集合内部的顺序执行行为。

### 课程笔记

随着集成测试套件不断增长,把单个控制器的所有测试都放在一个类中会变得难以维护。
为了改善结构,测试应该按控制器名称组织到文件夹中,并为每个被测试的具体方法建立单独的类。
例如,CreateCustomerControllerTests 负责 POST 请求,而 GetCustomerControllerTests 负责 GET 请求。

```csharp
public class CreateCustomerControllerTests : IAsyncLifetime, IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    private readonly Faker<CustomerRequest> _customerGenerator =
        new Faker<CustomerRequest>()
            .RuleFor(x => x.FullName, faker => faker.Person.FullName)
            .RuleFor(x => x.Email, faker => faker.Person.Email)
            .RuleFor(x => x.GitHubUsername, "nickchapsas")
            .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    private readonly List<Guid> _createdIds = new();

    public CreateCustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        
        // Act
        var response = await _httpClient.PostAsJsonAsync("customers", customer);

        // Assert
        var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        customerResponse.Should().BeEquivalentTo(customer);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        _createdIds.Add(customerResponse!.Id);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        foreach (var createdId in _createdIds)
        {
            await _httpClient.DeleteAsync($"customers/{createdId}");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/sharing-a-single-application-for-multiple-test-classes-53953897/?t=145)

当测试以这种方式拆分时,xUnit 的默认行为是并行运行这些类。
这会导致创建多个 WebApplicationFactory 实例,每个实例都会启动自己的测试服务器。
虽然这提供了隔离性,但可能会消耗大量资源。

要在多个测试类之间共享单个 WebApplicationFactory 实例,请使用 xUnit 的集合夹具(Collection Fixtures)。
首先,创建一个定义类,让它为该工厂实现 ICollectionFixture<T>,并用 [CollectionDefinition] 特性修饰它。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration;

[CollectionDefinition("CustomerApi Collection")]
public class TestCollection : ICollectionFixture<WebApplicationFactory<IApiMarker>>
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/sharing-a-single-application-for-multiple-test-classes-53953897/?t=205)

集合定义好之后,把 [Collection] 特性应用到需要共享该工厂的测试类上。
这会告诉 xUnit,这些类属于同一个集合,并且应该共享该夹具。
使用集合夹具时,通常要从各个测试类中移除 IClassFixture 接口。

```csharp
[Collection("CustomerApi Collection")]
public class GetCustomerControllerTests
{
    private readonly HttpClient _httpClient;

    public GetCustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem!.Title.Should().Be("Not Found");
        problem.Status.Should().Be(404);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/sharing-a-single-application-for-multiple-test-classes-53953897/?t=280)

通过使用命名集合,xUnit 确保该集合中的所有测试按顺序运行,而不是并行运行。
这让它们可以共享同一个 WebApplicationFactory 实例以及相同的底层状态。
为了验证这种顺序执行,可以在测试中添加一个延迟,以便在测试运行器中观察到它们一个接一个地运行。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    problem!.Title.Should().Be("Not Found");
    problem.Status.Should().Be(404);
    await Task.Delay(3000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/sharing-a-single-application-for-multiple-test-classes-53953897/?t=270)

在 IClassFixture 和 ICollectionFixture 之间如何选择,取决于你的测试需求。
如果你想要并行执行以及按类隔离,就使用 IClassFixture。
如果你需要在多个类之间共享单个应用程序实例以节省资源或共享状态,就使用 ICollectionFixture。

```text
IClassFixture (default: classes run in parallel)
  CreateCustomerControllerTests -> WebApplicationFactory #1 -> test server
  GetCustomerControllerTests    -> WebApplicationFactory #2 -> test server

ICollectionFixture ("CustomerApi Collection": tests run sequentially)
  TestCollection : ICollectionFixture<WebApplicationFactory<IApiMarker>>
  CreateCustomerControllerTests --+
                                  +--> one WebApplicationFactory<IApiMarker>
  GetCustomerControllerTests    --+
```

## 10. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953898/) · 0:37

### 总结

本课总结了当前章节中涵盖的 ASP.NET Core 集成测试基础概念。
它回顾了把 WebApplicationFactory 作为托管内存测试服务器的主要工具的实现方式、针对 HTTP 状态码和 JSON 响应进行断言的技巧,以及使用 Bogus 库生成逼真测试数据的做法。
此外,它还强调了测试数据清理的重要性,以及在多个测试类之间共享服务器状态以优化性能的策略。

### 核心概念

*   用于在内存中托管 API 的 `WebApplicationFactory`。
*   验证 HTTP 状态码和响应头。
*   把 JSON 响应内容反序列化为普通旧 CLR 对象(POCO)。
*   使用 Bogus 库生成逼真的测试数据。
*   管理测试数据的生命周期与清理。
*   在多个测试类之间共享测试夹具。

### 课程笔记

本章节把 `WebApplicationFactory` 确立为集成测试的基础类。
这个工具允许创建应用程序的内存版本,并提供一个直接与测试服务器通信的 `HttpClient`。

涵盖的测试策略包括验证各种响应类型,例如状态码、纯文本和 JSON 对象。
在处理 JSON 时,内容通常会被转换为普通旧 CLR 对象(POCO),以便更容易地进行断言。
例如,在测试一个不存在的资源时,测试会验证 API 返回 `404 Not Found` 状态以及一个结构化的 `ValidationProblemDetails` 对象。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    problem!.Title.Should().Be("Not Found");
    problem.Status.Should().Be(404);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953898/?t=10)

除了状态码之外,本章节还演示了如何验证 HTTP 响应头,以及如何使用 Bogus 库生成逼真的测试数据。
Bogus 允许创建包含随机但看起来有效的数据的复杂对象,确保测试健壮并覆盖各种数据场景。

数据管理是集成测试的一个关键方面。
本章节探讨了如何在测试执行后清理数据,以维持一个一致的环境。
最后,还介绍了共享状态的实现,展示了如何在测试套件的多个测试类之间使用同一个服务器实例,以减少开销并提高执行速度。

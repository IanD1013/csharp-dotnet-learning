# Integration testing in the real world

> 课程:[From Zero to Hero: Integration testing in ASP.NET Core](https://dometrain.com/course/from-zero-to-hero-integration-testing-in-asp-net-core/) · 第 6 章
> 共 21 课 · 约 81 分钟
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The project we will be writing integration tests for](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/) | 5:11 | [↓](#1-the-project-we-will-be-writing-integration-tests-for) |
| 2 | [Creating the WebApplicationFactory](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-the-webapplicationfactory-53953928/) | 3:46 | [↓](#2-creating-the-webapplicationfactory) |
| 3 | [The problem with data store dependencies](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-data-store-dependencies-53953929/) | 1:19 | [↓](#3-the-problem-with-data-store-dependencies) |
| 4 | [Introducing Docker](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-docker-53953930/) | 4:05 | [↓](#4-introducing-docker) |
| 5 | [Taking a look at docker compose files](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/) | 2:49 | [↓](#5-taking-a-look-at-docker-compose-files) |
| 6 | [Creating a test container for our database](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/) | 8:04 | [↓](#6-creating-a-test-container-for-our-database) |
| 7 | [The problem with third party API dependencies](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-third-party-api-dependencies-53953933/) | 1:45 | [↓](#7-the-problem-with-third-party-api-dependencies) |
| 8 | [Introducing WireMock](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-wiremock-53953934/) | 3:50 | [↓](#8-introducing-wiremock) |
| 9 | [Creating a mocked API server with WireMock](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/) | 5:57 | [↓](#9-creating-a-mocked-api-server-with-wiremock) |
| 10 | [Testing: Create a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/) | 14:39 | [↓](#10-testing-create-a-resource) |
| 11 | [Your turn to practice](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/your-turn-to-practice-53953937/) | 1:06 | [↓](#11-your-turn-to-practice) |
| 12 | [Testing: Getting a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-a-resource-53953938/) | 4:34 | [↓](#12-testing-getting-a-resource) |
| 13 | [Testing: Getting all resources](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/) | 5:05 | [↓](#13-testing-getting-all-resources) |
| 14 | [Testing: Updating a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-updating-a-resource-53953941/) | 3:51 | [↓](#14-testing-updating-a-resource) |
| 15 | [Testing: Deleting resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-deleting-resource-53953942/) | 1:25 | [↓](#15-testing-deleting-resource) |
| 16 | [Testing: API Dependency Is Unavailable](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-api-dependency-is-unavailable-53953943/) | 4:36 | [↓](#16-testing-api-dependency-is-unavailable) |
| 17 | [Dealing with requests requiring auth](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-requests-requiring-auth-53953944/) | 2:12 | [↓](#17-dealing-with-requests-requiring-auth) |
| 18 | [Dealing with background services](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-background-services-53953945/) | 1:36 | [↓](#18-dealing-with-background-services) |
| 19 | [Integration testing Minimal APIs](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/integration-testing-minimal-apis-53953946/) | 0:52 | [↓](#19-integration-testing-minimal-apis) |
| 20 | [Dealing with Entity Framework](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-entity-framework-53953947/) | 3:10 | [↓](#20-dealing-with-entity-framework) |
| 21 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953948/) | 1:06 | [↓](#21-section-recap) |

## 1. The project we will be writing integration tests for

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/) · 5:11

### 总结

本课介绍 Customers API,这是一个贴近真实场景的 ASP.NET Core RESTful 服务,也是本课程主要的测试对象。
该 API 通过标准的 CRUD 操作管理客户记录,并与两个外部依赖集成:用于持久化的 PostgreSQL 数据库,以及用于验证用户名是否存在的 GitHub API。
其实现遵循整洁架构,采用 controller-service-repository 模式,使用 FluentValidation 保证输入完整性,并用自定义值对象承载领域逻辑。
本课还介绍了运行该 API 所需的基础设施,包括 Docker Compose 配置,以及为容器化环境生成 HTTPS 证书。

### 核心概念

* **RESTful CRUD Operations**:为客户管理实现 Create、Read、Update 和 Delete 端点。
* **External Integration Points**:与 PostgreSQL 关系型数据库以及第三方 GitHub API 交互。
* **Multi-Layer Validation**:将本地输入验证(FluentValidation)与外部状态验证(GitHub API 用户校验)结合起来。
* **Infrastructure as Code**:使用 Docker Compose 编排 API 及其数据库依赖。
* **Container Security**:生成并配置 PFX 证书,以便在 Docker 容器中支持 HTTPS。

### 课程笔记

Customers API 是一个用于管理客户数据的标准 ASP.NET Core Web 服务。
一个典型的创建客户请求包含用户的全名、邮箱、GitHub 用户名和出生日期。

```json
{
    "fullName": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "nickchapsas",
    "dateOfBirth": "1993-04-20"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=75)

#### 验证逻辑

该 API 执行若干验证规则。
`fullName`、`email` 和 `gitHubUsername` 等字段不能为空。
邮箱必须符合有效格式,出生日期不能是未来的日期。
如果发生验证错误,例如 `fullName` 为空,API 会返回 400 Bad Request,并附带描述性的错误信息。

```json
{
    "fullName": "",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "nickchapsas",
    "dateOfBirth": "1993-04-20"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=85)

除了基本的格式验证之外,API 还会对 GitHub API 执行一次外部检查。
如果提供的 `gitHubUsername` 在 GitHub 上没有对应的真实资料页,创建或更新请求就会失败。
这代表了一个在测试中必须处理的关键集成点。

```text
            +--------------------+
  HTTP ---->|   Customers API    |
            +--------------------+
               |              |
               v              v
      +-------------+   +-------------+
      | PostgreSQL  |   | GitHub API  |
      | (Customers) |   | (username)  |
      +-------------+   +-------------+
```

#### API 端点

该服务实现了一组标准的 REST 端点:
* **GET /customers/{id}**:获取指定客户。找到时返回 200 OK,如果该 ID 不存在则返回 404 Not Found。
* **GET /customers**:获取数据库中当前的所有客户。
* **PUT /customers/{id}**:更新一个已有客户,遵循与创建相同的验证规则。
* **DELETE /customers/{id}**:从系统中删除一个客户。成功时返回 200 OK,客户不存在时返回 404。

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-fcbdfff87fe925f382eddc12da520f00-29ebc294e536a761-00"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=165)

更新客户时,请求体的结构与创建请求保持一致,因此可以修改 GitHub 用户名等字段。

```json
{
    "fullName": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "Elfocrash",
    "dateOfBirth": "1993-04-20"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=180)

#### 技术实现

应用在 `Program.cs` 中进行配置,数据库连接工厂、仓储和服务等依赖都在这里注册。
它还配置了一个具名 `HttpClient`,用于与 GitHub API 交互。

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new NpgsqlConnectionFactory(config.GetValue<string>("Database:ConnectionString")));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();

builder.Services.AddHttpClient("GitHub", httpClient =>
{
    httpClient.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl"));
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
});

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=225)

PostgreSQL 连接字符串和 GitHub API 基地址这两个配置值通过 `appsettings.json` 管理。

```json
{
  "Database": {
    "ConnectionString": "Server=localhost;Port=5432;Database=mydb;User ID=course;Password=changeme;"
  },
  "GitHub": {
    "ApiBaseUrl": "https://api.github.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=235)

#### 基础设施与 Docker

要在支持 HTTPS 的容器化环境中运行该 API,必须生成一个开发者证书。
这通过 `.NET dev-certs` 工具完成,导出一个 PFX 文件,并使用 Docker 配置中用到的特定密码。

```shell
>> dotnet dev-certs https -ep cert.pfx -p Test1234!
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=250)

应用启动时,会使用 `DatabaseInitializer` 确保 PostgreSQL 数据库中存在所需的表结构。

```csharp
public async Task InitializeAsync()
{
    using var connection = await _connectionFactory.CreateConnectionAsync();
    await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Customers (
    Id UUID PRIMARY KEY, 
    GitHubUsername TEXT NOT NULL,
    FullName TEXT NOT NULL,
    Email TEXT NOT NULL,
    DateOfBirth TEXT NOT NULL)");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=270)

`CustomerController` 负责处理 HTTP 请求,把业务逻辑委托给 `ICustomerService`,并返回恰当的 RESTful 响应。

```csharp
[HttpPost("customers")]
public async Task<IActionResult> Create([FromBody] CustomerRequest request)
{
    var customer = request.ToCustomer();

    await _customerService.CreateAsync(customer);

    var customerResponse = customer.ToCustomerResponse();

    return CreatedAtAction("Get", new { customerResponse.Id }, customerResponse);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-integration-tests-for-53953926/?t=285)

## 2. Creating the WebApplicationFactory

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-the-webapplicationfactory-53953928/) · 3:46

### 总结

本课演示如何在 ASP.NET Core 中为集成测试搭建一个自定义的 WebApplicationFactory。
通过继承基础的 WebApplicationFactory,并使用一个标记接口来定位 API 程序集,开发者可以创建一个专门的测试环境。
这个自定义工厂允许覆盖应用的配置,例如清除日志提供程序,防止测试试图把日志发送到 Elasticsearch 或 Datadog 等外部服务。
最后,本课展示如何使用 Xunit 的 IClassFixture 把这个自定义工厂注入到测试类中,为针对内存中 API 版本执行集成测试提供一个一致且可控的环境。

### 核心概念

* **Microsoft.AspNetCore.Mvc.Testing**:提供 WebApplicationFactory、用于内存中集成测试的核心 NuGet 包。
* **IApiMarker**:一种模式,在目标 API 项目中放一个空接口,让测试项目以类型安全的方式引用 API 程序集。
* **Custom WebApplicationFactory**:继承基类,把配置覆盖(例如数据库、外部服务或日志)集中在一处。
* **ConfigureWebHost**:用于在测试服务器启动前拦截并修改 `IWebHostBuilder` 的方法。
* **IClassFixture**:一个 Xunit 接口,用于在一个类的所有测试之间共享自定义工厂的同一个实例。

### 课程笔记

为了开始集成测试,先创建一个名为 `Customers.Api.Tests.Integration` 的新 Xunit 项目。
必要的依赖包括:用于编写可读断言的 `FluentAssertions`、用于生成逼真测试数据的 `Bogus`,以及用于启用 web application factory 基础设施的 `Microsoft.AspNetCore.Mvc.Testing`。

最初的测试类按控制器组织,先从一个测试创建客户的类开始。

```csharp
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-the-webapplicationfactory-53953928/?t=70)

为了引用 API 项目,在 `Customers.Api` 项目中添加一个标记接口。
这个接口 `IApiMarker` 充当一个类型安全的锚点,让 `WebApplicationFactory` 能够找到目标应用的 `Program.cs` 或 `Startup` 逻辑。

```csharp
namespace Customers.Api;

public interface IApiMarker { }
```

虽然可以直接把 `WebApplicationFactory<IApiMarker>` 用作 `IClassFixture`,但真实场景往往需要大量定制。
创建一个继承 `WebApplicationFactory<IApiMarker>` 的专用类,就提供了一个集中的位置来覆盖数据库或第三方集成等依赖。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Customers.Api;

namespace Customers.Api.Tests.Integration;

public class CustomerApiFactory : WebApplicationFactory<IApiMarker>
{
    
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-the-webapplicationfactory-53953928/?t=145)

自定义工厂的一个常见用途是修改日志行为。
在生产环境中,API 可能会使用把日志推送到 Elasticsearch 或 Datadog 等服务的提供程序。
在集成测试期间,应当清除这些提供程序,以确保测试不会因缺少基础设施而失败,也不会污染生产日志。
这是通过覆盖 `ConfigureWebHost` 并修改 `IWebHostBuilder` 来实现的。

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Tests.Integration;

public class CustomerApiFactory : WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-the-webapplicationfactory-53953928/?t=190)

要在测试类中使用这个工厂,需实现 `IClassFixture<CustomerApiFactory>` 接口。
然后工厂实例通过构造函数注入,并保存到一个私有字段中,供测试方法使用。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-the-webapplicationfactory-53953928/?t=225)

```text
CreateCustomerControllerTests
   : IClassFixture<CustomerApiFactory>
        |
        v
CustomerApiFactory : WebApplicationFactory<IApiMarker>
   ConfigureWebHost(IWebHostBuilder builder)
        |
        v
   ConfigureLogging -> logging.ClearProviders()
```

## 3. The problem with data store dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-data-store-dependencies-53953929/) · 1:19

### 总结

集成测试常常依赖数据库这类外部数据存储,而当这些依赖必须手动管理时,就会带来摩擦。
依赖一个持久存在、手动启动的数据库实例会导致测试脆弱,因为一旦服务没有运行或数据状态不一致,测试就会失败。
为了实现可靠且自动化的集成测试,开发者需要一种机制,在测试生命周期中以编程方式创建和销毁数据存储,确保每次运行都有一个干净的环境,无需人工干预。

### 核心概念

- **Data Store Dependencies**:集成测试需要实际的基础设施(例如 PostgreSQL)可用,才能验证整个技术栈。
- **Manual Management Overhead**:要求开发者在运行测试前手动启动 Docker 容器或本地服务,既容易出错,也不适用于 CI/CD。
- **Test Fragility**:如果底层数据存储不可用,测试会立即失败,正如停止数据库容器时所看到的那样。
- **Ephemeral Environments**:集成测试的理想状态是在测试套件开始时启动一个全新的数据库实例,并在结束后立即销毁它。
- **Data Cleanup**:以编程方式管理数据库就无需复杂的数据清理逻辑,因为整个数据存储会在使用后被销毁。

### 课程笔记

在典型的开发环境中,集成测试通常针对一个已经在运行的本地数据库实例执行。
虽然这样可以立即开始测试,但它会对必须手动管理的外部基础设施形成硬依赖。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-data-store-dependencies-53953929/?t=10)

当数据库在运行时(例如通过一个 Docker 容器),测试会如预期那样通过。
然而,如果数据库服务停止,集成测试就会失败,因为 API 无法与其数据存储建立连接。
这说明了依赖预先存在的基础设施的测试是多么脆弱。

为了走向更健壮的测试策略,基础设施应当以编程方式管理。
测试套件不应依赖一个"后台"数据库,而应自己负责自己的依赖。
这就需要使用 Docker 之类的工具,专门在测试期间启动一个数据库容器。

```csharp
//[Collection("CustomerApi Collection")]
public class CreateCustomerControllerTests : IAsyncLifetime, IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    private readonly Faker<CustomerRequest> _customerGenerator =
        new Faker<CustomerRequest>()
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-data-store-dependencies-53953929/?t=55)

实现 `IAsyncLifetime` 让测试类可以执行异步的准备和清理任务。
通过在开始时以编程方式创建数据库、在结束时将其移除,测试就变得自包含。
这种做法也解决了数据污染的问题;由于测试结束后整个数据库都会被丢弃,所以不需要编写专门的"清理"代码去删除测试运行期间创建的记录。

```text
测试开始 --> 创建数据库 --> 运行测试 --> 移除数据库 --> 测试结束
```

## 4. Introducing Docker

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-docker-53953930/) · 4:05

### 总结

Docker 是一种容器化服务,能够为不同的服务(例如数据库)创建并管理隔离的环境。
通过在容器中而不是直接在宿主操作系统上运行依赖,开发者可以轻松地创建、使用并销毁集成测试所需的确切基础设施。
这种做法已经成为行业标准,在 Windows、Linux 和 macOS 等不同操作系统之间提供一致的环境。

### 核心概念

- **Containers**:无需完整虚拟机开销即可运行服务的隔离环境。
- **Images**:容器的蓝图,可以从 Docker Hub 等镜像仓库拉取,也可以存放在本地。
- **Docker Daemon**:管理 Docker 对象的后台服务,运行在宿主操作系统之上。
- **Isolation**:让服务以不干扰宿主系统或其他容器的方式运行的能力。
- **Docker CLI**:用于管理容器生命周期的命令行工具(例如 `ps`、`start`、`stop`)。
- **Docker Desktop**:用于管理容器、镜像和卷的图形界面。

### 课程笔记

集成测试常常需要数据库等外部依赖能够按需可用。
Docker 解决这个问题的方式是让开发者为这些服务创建隔离的容器。
这样可以确保测试不依赖于一个共享的、可能已被弄脏的数据库状态。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-docker-53953930/?t=10)

与虚拟机不同,Docker 在宿主操作系统之上运行一个守护进程,由它来管理轻量级的容器。
这些容器由镜像构建而成,镜像是一种蓝图,可以从网络(通过 Docker Hub)获取,也可以存放在本地。

```text
+------------------------------------------+
|  Containers:  [ postgres ]  [ redis ]    |
+------------------------------------------+
|  Docker Daemon                           |
+------------------------------------------+
|  Host Operating System                   |
+------------------------------------------+
        ^
        | images (Docker Hub / local)
```

在典型的开发工作流中,Docker Desktop 提供了一个管理这些资源的可视化界面。
例如,可以拉取一个 Postgres 或 Redis 镜像并将其作为容器运行。
如果数据库服务停止,应用发起的连接尝试会被拒绝;一旦容器启动,服务就会立即可用。

与 Docker 的交互经常通过命令行完成。
`docker ps -a` 命令会列出所有容器,包括它们的 ID、镜像和当前状态。
可以使用容器的 ID(或其前几个字符)来控制容器。

```shell
docker ps -a
CONTAINER ID   IMAGE      COMMAND                  CREATED        STATUS              PORTS
1587a2acee33   postgres   "docker-entrypoint.s..."   18 hours ago   Up About a minute   0.0.0.0:5432->5432/tcp   customersapi-db-1
docker stop 15
15
docker ps -a
CONTAINER ID   IMAGE      COMMAND                  CREATED        STATUS              PORTS     NAMES
1587a2acee33   postgres   "docker-entrypoint.s..."   18 hours ago   Exited (0) 2 seconds ago               customersapi-db-1
docker start 15
15
docker ps -a
CONTAINER ID   IMAGE      COMMAND                  CREATED        STATUS              PORTS
1587a2acee33   postgres   "docker-entrypoint.s..."   18 hours ago   Up 1 second         0.0.0.0:5432->5432/tcp   customersapi-db-1
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-docker-53953930/?t=180)

虽然本课程关注的是集成测试而非 Docker 内部原理,但它假定你已经装好了可用的 Docker Desktop。
后续课程将演示如何使用 Docker Compose 定义并运行用于测试的多容器应用,这会简化整个测试环境的编排。

## 5. Taking a look at docker compose files

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/) · 2:49

### 总结

本课探讨用于为集成测试创建隔离环境的基础 Docker 组件。
它介绍了 Dockerfile 在定义 API 镜像方面的作用,以及 Docker Compose 在把数据库等依赖与应用一起编排方面的作用。
通过在容器中运行这些服务,开发者可以确保一个一致、可复现、与生产环境相仿的环境,同时让测试套件保持很高的执行速度。

### 核心概念

*   **Dockerfile**:Docker 用来把应用代码打包成可移植镜像的一组指令。
*   **Docker Compose**:一个配置文件(YAML),用于定义并运行多容器应用,让 API 及其数据库在隔离中运行。
*   **Isolation**:在容器中运行服务的做法,以确保测试不会干扰本地宿主环境或其他测试。
*   **Service Orchestration**:管理多个服务的生命周期与连通性(例如确保 API 只在数据库就绪后才启动)。

### 课程笔记

在测试套件中实现基于 Docker 的逻辑之前,有必要先理解管理应用环境的底层 Docker 配置文件。
在典型的集成测试场景中,目标是让 API 及其依赖在完全隔离的状态下运行。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/?t=10)

#### Dockerfile

`Dockerfile` 定义了 API 如何被构建为一个 Docker 镜像。
这个镜像相当于一个包,其中包含编译后的代码及其运行时环境。
现代 IDE 可以自动生成这些文件,它们通常遵循多阶段构建流程:一个用于还原并发布 .NET 应用的构建环境,以及一个用于执行它的更小的运行时镜像。

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
# Run this to generate it: dotnet dev-certs https -ep cert.pfx -p Test1234!
COPY ["cert.pfx", "/https/cert.pfx"]
ENTRYPOINT ["dotnet", "Customers.Api.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/?t=25)

```text
build-env (dotnet/sdk:6.0)          runtime (dotnet/aspnet:6.0)
  COPY . ./                           COPY --from=build-env /app/out .
  RUN dotnet restore        ----->    COPY cert.pfx /https/cert.pfx
  RUN dotnet publish -o out           ENTRYPOINT dotnet Customers.Api.dll
```

#### Docker Compose

Dockerfile 处理的是单个服务,而 Docker Compose 文件(`.yml`)管理的是整个系统,包括 API 及其数据库。
这让系统能够在宿主机上隔离运行。
该配置定义了环境变量、端口映射以及服务之间的依赖关系。

```yaml
version: '3.9'

services:

  api:
    build: .
    ports:
      - "5001:443"
      - "5000:80"
    environment:
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_Kestrel__Certificates__Default__Password=Test1234!
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/cert.pfx
      - ASPNETCORE_Environment=Production
      - CustomersApi_Database__ConnectionString=Server=db;Port=5432;Database=mydb;User ID=course;Password=changeme;
    depends_on:
      db:
        condition: service_started

  db:
    image: postgres
    restart: always
    environment:
      - POSTGRES_USER=course
      - POSTGRES_PASSWORD=changeme
      - POSTGRES_DB=mydb
    ports:
      - '5432:5432'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/?t=75)

要启动某个特定 compose 文件中定义的环境,请在终端中使用以下命令:

```shell
>> docker compose -f .\docker-compose-full.yml up
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/?t=85)

容器运行起来之后,就可以通过映射的端口(例如 5001)访问 API。
如果容器被停止,服务就会变得不可用,从而确保后续运行有一个干净的状态。

对于集成测试,你可能只需要在 Docker 中运行数据库,而由测试运行器管理 API。
一个简化的 compose 文件可以只定义数据库服务:

```yaml
db:
  image: postgres
  restart: always
  environment:
    - POSTGRES_USER=course
    - POSTGRES_PASSWORD=changeme
    - POSTGRES_DB=mydb
  ports:
    - '5432:5432'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/?t=130)

你还可以为镜像指定确切的版本,以确保不同环境之间的一致性:

```yaml
version: '3.9'

services:

  db:
    image: postgres:latest
    restart: always
    environment:
      - POSTGRES_USER=course
      - POSTGRES_PASSWORD=changeme
      - POSTGRES_DB=mydb
    ports:
      - '5432:5432'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/taking-a-look-at-docker-compose-files-53953931/?t=140)

Docker 容器启动得极快,这对集成测试来说是一个显著的优势,能在开发过程中带来快速的反馈循环。

## 6. Creating a test container for our database

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/) · 8:04

### 总结

本课演示如何在 ASP.NET Core 中使用 Testcontainers 库,以编程方式为集成测试管理数据库实例。
通过用动态的、由代码定义的容器取代静态的 Docker Compose 配置,开发者可以确保测试环境相互隔离,并自动处理生命周期管理。
本课涵盖了配置通用容器、使用 PostgreSQL 容器这类专用模块,以及在 WebApplicationFactory 中覆盖依赖注入服务,借助动态连接字符串让应用指向临时的测试数据库。

### 核心概念

- **Testcontainers for .NET**:一个可在测试执行期间以编程方式管理 Docker 容器的库。
- **WebApplicationFactory Customization**:扩展基础工厂,使其包含基础设施的准备工作。
- **IAsyncLifetime**:一个 xUnit 接口,用于管理测试资源的异步初始化和释放。
- **Service Overriding**:使用 `ConfigureTestServices` 把生产数据库连接替换为测试专用的连接。
- **Wait Strategies**:在继续执行测试之前,确保容器已完全可用(例如端口已可用)。
- **Specialized Database Modules**:利用 `PostgreSqlTestcontainer` 这类预配置模块,简化设置并自动生成连接字符串。

### 课程笔记

要开始实现以编程方式创建的数据库容器,先从一个标准的 `WebApplicationFactory` 实现入手。
这个工厂是为集成测试定制 Web 主机环境的入口。

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace Customers.Api.Tests.Integration;

public class CustomerApiFactory : WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/?t=10)

#### 通用 Testcontainer 配置

使用 `DotNet.Testcontainers` 包,你可以通过 `TestcontainersBuilder` 定义一个容器。
这种方式允许你指定 Docker 镜像、环境变量(与数据库镜像所要求的一致)、端口绑定,以及一个确保服务已就绪的等待策略。

```csharp
public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly TestcontainersContainer _dbContainer = 
        new TestcontainersBuilder<TestcontainersContainer>()
            .WithImage("postgres:latest")
            .WithEnvironment("POSTGRES_USER", "course")
            .WithEnvironment("POSTGRES_PASSWORD", "changeme")
            .WithEnvironment("POSTGRES_DB", "mydb")
            .WithPortBinding(5555, 5432)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // ... configuration
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/?t=145)

#### 使用 IAsyncLifetime 管理生命周期

为了确保容器在测试运行前启动、在测试结束后停止,需要实现 `IAsyncLifetime` 接口。
它提供了 `InitializeAsync` 和 `DisposeAsync` 方法,xUnit 会自动调用它们。

```csharp
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/?t=175)

#### 在 DI 容器中覆盖服务

容器运行起来之后,必须配置应用去使用它。
这是通过在 `IWebHostBuilder` 上调用 `ConfigureTestServices` 实现的。
你必须移除已有的 `IDbConnectionFactory` 注册,并替换为一个新实例,其连接字符串指向该 Docker 容器。

```csharp
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory("Server=localhost;Port=5555;Database=mydb;User ID=course;Password=changeme;"));
        });
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/?t=275)

```text
xUnit
  |
  +--> InitializeAsync()  --> _dbContainer.StartAsync()
  |
  +--> ConfigureWebHost   --> ConfigureTestServices
  |                             RemoveAll(IDbConnectionFactory)
  |                             AddSingleton(NpgsqlConnectionFactory, :5555)
  |
  +--> DisposeAsync()     --> _dbContainer.DisposeAsync()
```

#### 使用专用数据库模块

Testcontainers 为 PostgreSQL 等常见数据库提供了专用模块。
这些模块简化了配置,并处理动态端口映射,从而避免并行执行测试时出现端口冲突。
`PostgreSqlTestcontainer` 会自动提供一个 `ConnectionString` 属性,其中包含正确的动态端口。

```csharp
public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly TestcontainerDatabase _dbContainer = 
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "db",
                Username = "course",
                Password = "whatever"
            }).Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-test-container-for-our-database-53953932/?t=480)

## 7. The problem with third party API dependencies

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-third-party-api-dependencies-53953933/) · 1:45

### 总结

集成测试常常涉及外部的第三方依赖,例如本项目中用于验证用户身份的 GitHub API。
在测试期间依赖真实的外部 API 会带来若干挑战,包括速率限制、网络不稳定,以及为一个并非测试重点的功能白白消耗资源。
集成测试不应去测试外部服务的内部逻辑,而应关注应用能否正确处理外部服务契约所定义的那些响应。
在测试期间把真实 API 换成一个受控的 mock 或 fake,可以让测试套件更可靠、更可重复。

### 核心概念

- 集成测试中的外部 API 依赖。
- 速率限制与服务可靠性问题。
- 针对 API 契约而不是服务实现进行测试。
- 在测试环境中把第三方服务替换为 mock 的必要性。

### 课程笔记

应用目前依赖 GitHub API,在创建或更新客户之前验证所提供的用户名是否存在。
这部分逻辑封装在 `GitHubService` 中,它使用一个 `IHttpClientFactory` 与外部服务通信。

```csharp
using System.Net;
using System.Text.Json.Nodes;

namespace Customers.Api.Services;

public class GitHubService : IGitHubService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GitHubService(IHttpClientFactory httpClientFactory)
    { 
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> IsValidGitHubUser(string username)
    { 
        var client = _httpClientFactory.CreateClient("GitHub");
        var response = await client.GetAsync($"/users/{username}");
        if (response.StatusCode == HttpStatusCode.Forbidden)
        { 
            var responseBody = await response.Content.ReadFromJsonAsync<JsonObject>();
            var message = responseBody!["message"]!.ToString();
            throw new HttpRequestException(message);
        }
        
        return response.StatusCode == HttpStatusCode.OK;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-third-party-api-dependencies-53953933/?t=10)

在自动化集成测试中调用真实 GitHub API 的首要问题是速率限制。
频繁运行测试会很快耗尽允许的请求次数,导致与应用代码质量无关的测试失败。
此外,这些测试的目标不是验证 GitHub 的基础设施是否正常运行,而是验证应用能否根据它从一个遵循特定契约的 API 收到的响应做出正确的行为。

当用户不存在时,GitHub API 会返回 `404 Not Found` 状态码,并附带特定的 JSON 正文:

```json
{
  "message": "Not Found",
  "documentation_url": "https://docs.github.com/rest/reference/users#get-a-user"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-third-party-api-dependencies-53953933/?t=55)

反之,成功的请求会返回 `200 OK` 以及用户的资料数据。
应用正是通过查找这个状态码来验证客户的。

```json
{
  "login": "nickchapsas",
  "id": 67104228,
  "node_id": "MDQ6VXNlcjY3MTA0MjI4",
  "avatar_url": "https://avatars.githubusercontent.com/u/67104228?v=4",
  "url": "https://api.github.com/users/nickchapsas",
  "type": "User",
  "site_admin": false
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-problem-with-third-party-api-dependencies-53953933/?t=65)

```text
client.GetAsync($"/users/{username}")
   |
   +-- HttpStatusCode.Forbidden --> throw HttpRequestException(message)
   +-- HttpStatusCode.OK        --> true
   +-- 其他(例如 404 Not Found) --> false
```

为了确保测试是确定性的且足够快,应用必须能够在测试期间把真实的 GitHub API 换成一个 mock 服务。
这个 mock 服务应当按照预期的契约进行响应,让集成测试可以验证应用对各种场景(成功、未找到或被限流)的处理,而无需真正通过网络调用 GitHub。

## 8. Introducing WireMock

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-wiremock-53953934/) · 3:50

### 总结

WireMock.Net 是一个库,它允许开发者在集成测试期间运行一个本地的、可编程的 HTTP 服务器,以此模拟外部 HTTP 依赖。
通过定义特定的请求匹配器和相应的响应桩(stub),开发者可以确保测试是确定性的,并且不受第三方 API 可用性或速率限制的影响。

### 核心概念

* **WireMockServer**:一个轻量级 HTTP 服务器,可以在测试进程中启动和停止。
* **Request Matching**:定义 URL 路径和 HTTP 方法等条件,用以识别特定传入请求的过程。
* **Response Stubbing**:配置服务器,使其在找到匹配的请求时返回特定的状态码、头部和正文内容。
* **External Dependency Mocking**:使用本地 mock 服务器替换真实世界的 API(例如 GitHub),以避免限流和网络问题。
* **Resource Management**:正确释放 mock 服务器实例,以释放网络端口和资源。

### 课程笔记

为了在集成测试中模拟一个外部 API,你可以创建一个专门的项目,也可以把相关逻辑放在测试套件内部。
借助 `WireMock.Net` NuGet 包,你可以启动一个真实的 HTTP 服务器,对通过网络发来的调用做出逼真的响应。

第一步是启动服务器实例。
这会提供一个 URL,让你的应用调用它来代替真实的服务。

```csharp
using WireMock.Server;

var wiremockServer = WireMockServer.Start();

Console.WriteLine($"Wiremock is now running on: {wiremockServer.Url}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-wiremock-53953934/?t=55)

服务器运行起来之后,你可以使用 fluent API 配置它的行为。
你用 `Given()` 和 `Request.Create()` 定义一个请求模式,然后用 `RespondWith()` 和 `Response.Create()` 指定期望的响应。
重要的是,路径要包含开头的斜杠,并且在不再需要服务器时调用 `Dispose()`,以防进程挂起。

```csharp
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

var wiremockServer = WireMockServer.Start();

Console.WriteLine($"Wiremock is now running on: {wiremockServer.Url}");

wiremockServer.Given(Request.Create()
    .WithPath("/example")
    .UsingGet())
    .RespondWith(Response.Create()
        .WithBody("This is coming from WireMock")
        .WithStatusCode(200));

Console.ReadKey();
wiremockServer.Dispose();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-wiremock-53953934/?t=115)

这种方式对于 mock GitHub API 这样的第三方服务尤其有用。
你可以从外部服务捕获一个真实的 JSON 响应,并配置 WireMock 原样返回这个负载。
这样,你的系统就可以像处理来自真实提供方的数据一样处理它,而无需担心身份验证、速率限制或服务宕机。

```csharp
wiremockServer.Given(Request.Create()
    .WithPath("/users/nickchapsas")
    .UsingGet())
    .RespondWith(Response.Create()
        .WithBody(@"{
  ""login"": ""nickchapsas"",
  ""id"": 67104228,
  ""node_id"": ""MDQ6VXNlcjY3MTA0MjI4"",
  ""avatar_url"": ""https://avatars.githubusercontent.com/u/67104228?v=4"",
  ""gravatar_id"": """",
  ""url"": ""https://api.github.com/users/nickchapsas"",
  ""html_url"": ""https://github.com/nickchapsas"",
  ""followers_url"": ""https://api.github.com/users/nickchapsas/followers"",
  ""following_url"": ""https://api.github.com/users/nickchapsas/following{/other_user}"",
  ""gists_url"": ""https://api.github.com/users/nickchapsas/gists{/gist_id}"",
  ""starred_url"": ""https://api.github.com/users/nickchapsas/starred{/owner}{/repo}"",
  ""subscriptions_url"": ""https://api.github.com/users/nickchapsas/subscriptions"",
  ""organizations_url"": ""https://api.github.com/users/nickchapsas/orgs"",
  ""repos_url"": ""https://api.github.com/users/nickchapsas/repos"",
  ""events_url"": ""https://api.github.com/users/nickchapsas/events{/privacy}"",
  ""received_events_url"": ""https://api.github.com/users/nickchapsas/received_events"",
  ""type"": ""User"",
  ""site_admin"": false,
  ""name"": null,
  ""company"": null,
  ""blog"": """",
  ""location"": null,
  ""email"": null,
  ""hireable"": null,
  ""bio"": null,
  ""twitter_username"": null,
  ""public_repos"": 0,
  ""public_gists"": 0,
  ""followers"": 0,
  ""following"": 0,
  ""created_at"": ""2020-06-18T11:47:58Z"",
  ""updated_at"": ""2020-06-18T11:47:58Z""
}")
        .WithHeader("content-type", "application/json; charset=utf-8")
        .WithStatusCode(200));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-wiremock-53953934/?t=175)

## 9. Creating a mocked API server with WireMock

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/) · 5:57

本课演示如何使用 WireMock.Net 把一个 mock API 服务器集成到 ASP.NET Core 集成测试中。
通过为 WireMock 服务器创建一个专门的包装类,开发者可以把 GitHub API 这类外部依赖的生命周期和配置封装起来。
这个过程包括定义请求匹配规则、用字符串插值生成动态的 JSON 响应,以及在自定义 WebApplicationFactory 中覆盖应用的 HttpClient 配置,把流量重定向到本地 mock 服务器而不是真实的外部服务。

### 核心概念

*   把 mock 服务器逻辑封装在一个专门的 `GitHubApiServer` 类中。
*   使用 `Start()`、`Stop()` 和 `IDisposable` 管理 `WireMockServer` 的生命周期。
*   使用 `Given`、`WithPath` 和 `UsingGet` 配置 fluent 风格的请求匹配。
*   用特定的正文、头部和状态码定义 mock 响应。
*   使用字符串插值和辅助方法动态生成响应正文。
*   在 `WebApplicationFactory` 中通过 `ConfigureTestServices` 重定向 `HttpClient` 流量。
*   协调各个测试依赖(数据库容器和 mock 服务器)的启动顺序。

### 课程笔记

要把 mock API 集成到集成测试中,先在集成测试项目中安装 `WireMock.Net` 包。
一种整洁的架构模式是创建一个包装类,例如 `GitHubApiServer`,来管理内部的 `WireMockServer` 实例。
这个类应当实现 `IDisposable`,以确保测试完成后服务器被正确关闭并释放。

```csharp
using WireMock.Server;

namespace Customers.Api.Tests.Integration;

public class GitHubApiServer : IDisposable
{
    private WireMockServer _server;

    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/?t=55)

`SetupUser` 方法定义了 mock 服务器应如何响应特定的请求。
使用 WireMock 的 fluent API,你可以根据路径和 HTTP 方法来匹配请求。
例如,要 mock GitHub 的用户端点,你会匹配一个发往 `/users/{username}` 的 `GET` 请求。

```csharp
public void SetupUser(string username)
{
    _server.Given(Request.Create()
        .WithPath($"/users/{username}")
        .UsingGet())
        .RespondWith(Response.Create()
            .WithBody(@""));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/?t=100)

为了让 mock 更逼真,响应应当包含恰当的头部(例如 `application/json`)和状态码。
复杂的响应正文可以移到一个私有辅助方法中,用字符串插值注入动态数据,确保 mock 响应符合应用所期望的结构。

```csharp
public void SetupUser(string username)
{
    _server.Given(Request.Create()
        .WithPath($"/users/{username}")
        .UsingGet())
        .RespondWith(Response.Create()
            .WithBody(GenerateGitHubUserResponseBody(username))
            .WithHeader("content-type", "application/json; charset=utf-8")
            .WithStatusCode(200));
}

private static string GenerateGitHubUserResponseBody(string username)
{
    return $@"{{
  ""login"": ""{username}"",
  ""id"": 67104228,
  ""node_id"": ""MDQ6VXNlcjY3MTA0MjI4"",
  ""avatar_url"": ""https://avatars.githubusercontent.com/u/67104228?v=4"",
  ""gravatar_id"": """",
  ""url"": ""https://api.github.com/users/{username}"",
  ""html_url"": ""https://github.com/{username}"",
  ""followers_url"": ""https://api.github.com/users/{username}/followers"",
  ""following_url"": ""https://api.github.com/users/{username}/following{{/other_user}}"",
  ""gists_url"": ""https://api.github.com/users/{username}/gists{{/gist_id}}"",
  ""starred_url"": ""https://api.github.com/users/{username}/starred{{/owner}}{{/repo}}"",
  ""subscriptions_url"": ""https://api.github.com/users/{username}/subscriptions"",
  ""organizations_url"": ""https://api.github.com/users/{username}/orgs"",
  ""repos_url"": ""https://api.github.com/users/{username}/repos"",
  ""events_url"": ""https://api.github.com/users/{username}/events{{/privacy}}"",
  ""received_events_url"": ""https://api.github.com/users/{username}/received_events"",
  ""type"": ""User"",
  ""site_admin"": false,
  ""name"": null,
  ""company"": null,
  ""blog"": """",
  ""location"": null,
  ""email"": null,
  ""hireable"": null,
  ""bio"": null,
  ""twitter_username"": null,
  ""public_repos"": 0,
  ""public_gists"": 0,
  ""followers"": 0,
  ""following"": 0,
  ""created_at"": ""2020-06-18T11:47:58Z"",
  ""updated_at"": ""2020-06-18T11:47:58Z""
}}";
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/?t=175)

为了让测试工厂能够重定向流量,`GitHubApiServer` 必须通过一个属性暴露它被动态分配的 URL。

```csharp
public string Url => _server.Url!;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/?t=295)

最后,把 mock 服务器集成到 `CustomerApiFactory` 中。
服务器应当在工厂初始化时启动,并在清理时释放。
在 `ConfigureWebHost` 中,使用 `ConfigureTestServices` 覆盖 "GitHub" 客户端的 `HttpClient` 注册,把它的 `BaseAddress` 设为 mock 服务器的 URL。
这就确保了测试期间不会有任何真实请求被发送到外部的 GitHub API。

```csharp
protected override void ConfigureWebHost(IWebHostBuilder builder)
{
    builder.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    });

    builder.ConfigureTestServices(services =>
    {
        services.RemoveAll(typeof(IDbConnectionFactory));
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
        
        services.AddHttpClient("GitHub", httpClient =>
        {
            httpClient.BaseAddress = new Uri(_gitHubApiServer.Url);
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
        });
    });
}

public async Task InitializeAsync()
{
    _gitHubApiServer.Start();
    _gitHubApiServer.SetupUser(ValidGithubUser);
    await _dbContainer.StartAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/creating-a-mocked-api-server-with-wiremock-53953935/?t=325)

```text
CustomerApiFactory
  |
  +-- HttpClient "GitHub"   BaseAddress = _gitHubApiServer.Url
  |        |
  |        v
  |   GitHubApiServer (WireMockServer)   GET /users/{username} -> 200
  |
  +-- IDbConnectionFactory
           NpgsqlConnectionFactory(_dbContainer.ConnectionString)
```

## 10. Testing: Create a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/) · 14:39

### 总结

本课演示如何为 ASP.NET Core API 中的资源创建端点实现全面的集成测试。
它涵盖了使用 Bogus 库生成逼真的测试数据、用 Triple A(Arrange、Act、Assert)模式组织测试结构,以及验证成功的"happy path"场景和"unhappy path"验证失败场景。
关键技术包括:使用自定义 WebApplicationFactory 管理隔离的基础设施,例如通过 Testcontainers 管理 PostgreSQL、通过 WireMock 管理外部 API;以及处理常见的集成测试障碍,例如 DateTime 精度不匹配和针对复杂验证错误响应进行断言。

### 核心概念

- **Data Generation with Bogus**:使用 `Faker<T>` 类为测试生成逼真的、随机化的请求对象。
- **Triple A Pattern**:把测试组织为 Arrange(准备)、Act(执行)和 Assert(验证)三个阶段。
- **Infrastructure Isolation**:确保每个测试会话都针对一个干净的数据库和一个受控的外部服务 mock 运行。
- **Fluent Assertions**:利用 `Should().BeEquivalentTo()` 对请求模型和响应模型做深度对象比较。
- **Validation Testing**:断言 API 在收到无效数据或不存在的外部资源时,能正确返回带有 `ValidationProblemDetails` 的 `400 Bad Request`。
- **DateTime Precision**:处理高精度的 .NET `DateTime` 对象与仅含日期的存储格式或 JSON 序列化格式之间的不匹配。

### 课程笔记

要开始测试资源创建端点,先创建一个实现 `IClassFixture<CustomerApiFactory>` 的测试类。
这让测试可以共享这个自定义的 web application factory,由它管理数据库容器和 WireMock 服务器的生命周期。

```csharp
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;
    private readonly HttpClient _client;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = apiFactory.CreateClient();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=15)

为了让测试可靠且易于维护,要把外部依赖相关的常量提取出来。
例如,在 `CustomerApiFactory` 中定义一个有效的 GitHub 用户名,WireMock 服务器已预先配置为能识别它。

```csharp
public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public const string ValidGithubUser = "validuser";
    // ... rest of factory implementation
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=85)

使用 Bogus 库为 `CustomerRequest` 定义一个生成器。
这个生成器为每个字段创建逼真的数据,确保 API 在 happy path 测试中收到有效的输入。

```csharp
private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
    .RuleFor(x => x.Email, faker => faker.Person.Email)
    .RuleFor(x => x.FullName, faker => faker.Person.FullName)
    .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
    .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=100)

注意 `DateOfBirth` 规则上使用了 `.Date`。
这是必要的,因为 API 可能只存储或返回日期部分,而一个带毫秒精度的完整 .NET `DateTime` 在把生成的对象与 API 响应比较时可能导致断言失败。

#### Happy Path 测试

主要的测试用例验证:在提供有效数据时,用户被成功创建。
该测试遵循 Triple A 模式:生成一个请求,把它以 JSON 形式 POST 到 `customers` 端点,并断言响应状态为 `201 Created`。

```csharp
[Fact]
public async Task Create_CreatesUser_WhenDataIsValid()
{
    // Arrange
    var customer = _customerGenerator.Generate();

    // Act
    var response = await _client.PostAsJsonAsync("customers", customer);

    // Assert
    var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    customerResponse.Should().BeEquivalentTo(customer);
    response.StatusCode.Should().Be(HttpStatusCode.Created);
    response.Headers.Location!.ToString().Should()
        .Be($"http://localhost/customers/{customerResponse!.Id}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=265)

#### 测试验证错误

要测试验证逻辑,可以在 Bogus 生成器上使用 `.Clone()` 方法。
这让你可以为单个测试用例覆盖特定的规则(例如提供一个无效的邮箱),而不修改全局生成器的状态。

当 API 返回验证错误时,它通常使用 `ValidationProblemDetails` 格式。
你可以检查 `Errors` 字典,验证是正确的字段以预期的消息触发了错误。

```json
{
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "Email": [
            "nick is not a valid email address"
        ]
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=595)

下面的测试演示了针对无效邮箱地址进行断言:

```csharp
[Fact]
public async Task Create_ReturnsValidationError_WhenEmailIsInvalid()
{
    // Arrange
    const string invalidEmail = "dasdja9d3j";
    var customer = _customerGenerator.Clone()
        .RuleFor(x => x.Email, invalidEmail).Generate();

    // Act
    var response = await _client.PostAsJsonAsync("customers", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    error!.Status.Should().Be(400);
    error.Title.Should().Be("One or more validation errors occurred.");
    error.Errors["Email"][0].Should().Be($"{invalidEmail} is not a valid email address");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=725)

类似地,你也可以测试外部依赖导致验证失败的场景。
在这种情况下,如果 GitHub 用户名不存在,(在工厂中配置的)WireMock 服务器会返回 `404 Not Found`,API 再把它转换为面向调用方的 `400 Bad Request` 验证错误。

```text
POST customers (GitHubUsername = invalidGitHubUser)
      |
      v
  Customers API --GET /users/{username}--> WireMock
      |                                       |
      |<------------- 404 Not Found ----------+
      v
  400 Bad Request (ValidationProblemDetails)
```

```csharp
[Fact]
public async Task Create_ReturnsValidationError_WhenGitHubUserDoesNotExist()
{
    // Arrange
    const string invalidGitHubUser = "dasdja9d3j";
    var customer = _customerGenerator.Clone()
        .RuleFor(x => x.GitHubUsername, invalidGitHubUser).Generate();

    // Act
    var response = await _client.PostAsJsonAsync("customers", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    error!.Status.Should().Be(400);
    error.Title.Should().Be("One or more validation errors occurred.");
    error.Errors["GitHubUsername"][0].Should().Be($"There is no GitHub user with username {invalidGitHubUser}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953936/?t=840)

## 11. Your turn to practice

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/your-turn-to-practice-53953937/) · 1:06

### 总结

本课提供一个实践练习,用来巩固在 ASP.NET Core 中进行集成测试的技能。
学员的任务是实现一整套集成测试,覆盖标准的 RESTful 操作(Retrieve、List、Update 和 Delete),确保成功路径和错误处理(例如 404 Not Found 和验证失败)都被正确验证。

### 核心概念

* REST API 端点的端到端测试。
* 验证状态码(200 OK、201 Created、400 Bad Request、404 Not Found)。
* 测试数据的持久化与读取。
* 验证错误响应和 problem details。

### 课程笔记

这个练习的目标是把集成测试技术应用到一个真实的 ASP.NET Core API 上。
利用提供的源代码,你必须为 `CustomerController` 实现测试用例,确保所有 CRUD 操作在各种条件下都表现如预期。

应当在集成测试项目中实现以下测试用例:

```markdown
# Write these test cases

## Get
- Get_ReturnsCustomer_WhenCustomerExists
- Get_ReturnsNotFound_WhenCustomerDoesNotExist

## GetAll
- GetAll_ReturnsAllCustomers_WhenCustomersExist
- GetAll_ReturnsEmptyResult_WhenNoCustomersExist

## Update
- Update_UpdatesUser_WhenDataIsValid
- Update_ReturnsValidationError_WhenEmailIsInvalid
- Update_ReturnsValidationError_WhenGitHubUserDoestNotExist

## Delete
- Delete_ReturnsOk_WhenCustomerExists
- Delete_ReturnsNotFound_WhenCustomerDoesNotExist
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/your-turn-to-practice-53953937/?t=0)

成功完成这些测试,就表明你已经扎实地理解了如何在 .NET 中测试 REST API,包括如何处理外部依赖和数据库状态。

## 12. Testing: Getting a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-a-resource-53953938/) · 4:34

### 总结

本课演示如何为 ASP.NET Core 应用中的"Get Customer"端点实现集成测试。
它涵盖了使用 IClassFixture 创建一个专门的测试类以实现隔离、使用 Bogus 生成逼真的测试数据,以及实现两个主要的测试用例:验证一个已存在的客户能被正确获取,以及确保对不存在的 ID 返回"Not Found"状态。
本课强调 Arrange-Act-Assert 模式,展示如何在尝试获取资源之前先通过 POST 请求向数据库填充数据,并使用 FluentAssertions 同时验证状态码和对象等价性。

### 核心概念

* **Test Isolation**:使用 `IClassFixture<CustomerApiFactory>` 可确保测试类彼此隔离地运行,不过会创建多个数据库实例和外部服务 mock(例如 GitHub)实例。
* **Arrange-Act-Assert (AAA)**:一种结构化的测试编写方法,先准备环境(Arrange),再执行操作(Act),最后验证结果(Assert)。
* **Data Generation**:利用 `Bogus` 库为测试创建动态的、有效的请求对象。
* **Resource Seeding**:在集成测试中,常常在 Arrange 阶段通过 POST 请求创建资源,以确保在测试 GET 端点之前该资源已经存在。
* **FluentAssertions**:使用 `BeEquivalentTo` 把获取到的响应对象的属性与原始数据进行比较,并使用 `Should().Be()` 验证状态码。

### 课程笔记

为了开始测试 GET 端点,新建了一个测试类 `GetCustomerControllerTests`。
通过实现 `IClassFixture<CustomerApiFactory>`,测试套件确保 GET、POST、UPDATE 和 DELETE 测试彼此隔离地运行。
虽然这会为每个类分别创建数据库和 mock 服务器实例,但它能防止不同类型的操作之间产生副作用。
对于更大的测试套件,可以使用 collection definition 来合并这些资源。

```text
CreateCustomerControllerTests --> CustomerApiFactory #1 (db + GitHub mock)
GetCustomerControllerTests    --> CustomerApiFactory #2 (db + GitHub mock)
...                (IClassFixture<CustomerApiFactory>)
```

```csharp
namespace Customers.Api.Tests.Integration.CustomerController;

public class GetCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public GetCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-a-resource-53953938/?t=40)

#### 测试成功获取

第一个测试用例验证:当客户存在于系统中时,API 会返回该客户。
为了确保客户存在,Arrange 阶段会生成一个客户请求并把它 POST 到 `/customers` 端点。
随后在 Act 阶段用返回的 ID 执行一个 GET 请求。

```csharp
[Fact]
public async Task Get_ReturnsCustomer_WhenCustomerExists()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

    // Act
    var response = await _client.GetAsync($"customers/{createdCustomer!.Id}");

    // Assert
    var retrievedCustomer = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    retrievedCustomer.Should().BeEquivalentTo(createdCustomer);
    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-a-resource-53953938/?t=210)

#### 测试不存在的资源

第二个测试用例确保 API 能正确处理对不存在资源的请求。
它的准备工作要简单得多,只需要生成一个没有被填充到数据库中的随机 `Guid`。

```csharp
[Fact]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _client.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-a-resource-53953938/?t=250)

## 13. Testing: Getting all resources

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/) · 5:05

### 总结

本课介绍如何为 ASP.NET Core API 中的"Get All"端点实现集成测试。
它演示了如何验证该端点在资源存在时能正确返回资源列表,在数据库为空时返回一个空集合。
本课的一个关键关注点是识别并解决由同一个类中各测试共享数据库状态所引起的测试干扰。
讲师展示了如何通过实现一个手动清理步骤来保持测试隔离,确保每个测试都以可预测的数据库状态开始,或在结束时让数据库处于可预测的状态。

### 核心概念

- 针对有数据和无数据两种状态测试集合端点。
- 反序列化自定义的响应包装类型(例如 `GetAllCustomersResponse`)。
- 识别共享数据库环境中的副作用。
- 使用 `Delete` 端点实现手动清理,以确保测试隔离。

### 课程笔记

客户控制器的测试策略涉及针对 Get、GetAll、Update 和 Delete 端点的若干关键场景。
本课专门关注 `GetAll` 功能。

```markdown
# Write these test cases

## Get
- Get_ReturnsCustomer_WhenCustomerExists
- Get_ReturnsNotFound_WhenCustomerDoesNotExist

## GetAll
- GetAll_ReturnsAllCustomers_WhenCustomersExist
- GetAll_ReturnsEmptyResult_WhenNoCustomersExist

## Update
- Update_UpdatesUser_WhenDataIsValid
- Update_ReturnsValidationError_WhenEmailIsInvalid
- Update_ReturnsValidationError_WhenGitHubUserDoesNotExist

## Delete
- Delete_ReturnsOk_WhenCustomerExists
- Delete_ReturnsNotFound_WhenCustomerDoesNotExist
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/?t=10)

首先,新建一个测试类 `GetAllCustomerControllerTests`。
这个类使用 `IClassFixture<CustomerApiFactory>`,在各个测试之间共享底层的数据库和服务器实例。
使用一个 `Faker<CustomerRequest>` 来生成有效的测试数据。

```csharp
namespace Customers.Api.Tests.Integration.CustomerController;

public class GetAllCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public GetAllCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/?t=250)

#### 测试有数据的结果

第一个测试用例验证:当数据存在时,该端点返回所有客户。
测试的准备方式是先创建一个客户,然后在执行阶段调用 GET 端点,最后断言返回的列表中包含预期的客户。

```csharp
[Fact]
public async Task GetAll_ReturnsAllCustomers_WhenCustomersExist()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

    // Act
    var response = await _client.GetAsync("customers");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var customersResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
    customersResponse!.Customers.Single().Should().BeEquivalentTo(createdCustomer);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/?t=115)

#### 测试空结果

第二个测试用例验证:当数据库中不存在任何客户时,该端点返回一个空结果(但状态仍然是 200 OK)。

```csharp
[Fact]
public async Task GetAll_ReturnsEmptyResult_WhenNoCustomersExist()
{
    // Act
    var response = await _client.GetAsync("customers");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var customersResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
    customersResponse!.Customers.Should().BeEmpty();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/?t=235)

#### 处理共享状态与清理

单独运行这些测试时,它们都能通过。
然而,当运行整个类时,`GetAll_ReturnsEmptyResult_WhenNoCustomersExist` 会失败,因为它找到了上一个测试创建的用户。
由于这些测试通过 class fixture 共享同一个数据库实例,状态会在多次测试执行之间保留下来。

```text
GetAllCustomerControllerTests --IClassFixture--> 同一个数据库实例
  1. GetAll_ReturnsAllCustomers_WhenCustomersExist   POST customers -> 留下 1 个客户
  2. GetAll_ReturnsEmptyResult_WhenNoCustomersExist  GET customers  -> 不为空,失败
```

为了解决这个问题,在创建数据的测试中加入一个清理阶段。
通过在测试结束时删除所创建的资源,我们确保数据库为后续测试恢复到空状态。

```csharp
[Fact]
public async Task GetAll_ReturnsAllCustomers_WhenCustomersExist()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

    // Act
    var response = await _client.GetAsync("customers");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var customersResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
    customersResponse!.Customers.Single().Should().BeEquivalentTo(createdCustomer);

    // Cleanup
    await _client.DeleteAsync($"customers/{createdCustomer!.Id}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-resources-53953939/?t=295)

## 14. Testing: Updating a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-updating-a-resource-53953941/) · 3:51

### 总结

本课演示如何为 ASP.NET Core API 中的更新功能实现集成测试。
它介绍了复用创建测试中已有测试逻辑的策略,以验证资源可以通过 PUT 请求被成功修改。
本课详细讲解了更新场景下的 Arrange-Act-Assert 模式:先创建一个资源,再用 PutAsJsonAsync 执行更新,并断言 API 对有效数据返回 200 OK 状态码,对格式错误的邮箱或不存在的 GitHub 用户名等无效输入返回带有具体错误详情的 400 BadRequest。

### 核心概念

- 当端点共享验证逻辑时,复制测试类以节省时间。
- 使用 Arrange-Act-Assert 模式,在更新资源之前先创建它。
- 利用 `PutAsJsonAsync` 向特定的资源 ID 发送更新请求。
- 对成功的更新验证 `HttpStatusCode.OK`(200),而不是 `HttpStatusCode.Created`(201)。
- 通过对响应正文中的 `ValidationProblemDetails` 进行断言来处理验证错误。

### 课程笔记

更新部分的实现从复制 `CreateCustomerControllerTests` 类开始。
由于更新端点和创建端点共享相似的验证逻辑和数据结构,复制测试代码是实现这些测试的一种高效方式。
该测试类使用一个 `Faker<CustomerRequest>` 生成有效数据,并使用由 `CustomerApiFactory` 提供的 `HttpClient`。

```csharp
public class UpdateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public UpdateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-updating-a-resource-53953941/?t=25)

要测试一次成功的更新,测试必须先准备好一个存在于系统中的客户。
这是通过向 `customers` 端点发送一个 POST 请求,并从响应中获取所创建客户的 ID 来实现的。
资源存在之后,再生成一组新数据,并向该资源的特定 URI 发送一个 PUT 请求。
与返回 201 Created 的创建端点不同,更新端点返回 200 OK,并且不再检查 location 头部。

```csharp
[Fact]
public async Task Update_UpdatesUser_WhenDataIsValid()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

    customer = _customerGenerator.Generate();
    
    // Act
    var response = await _client.PutAsJsonAsync($"customers/{createdCustomer!.Id}", customer);

    // Assert
    var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    customerResponse.Should().BeEquivalentTo(customer);
    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-updating-a-resource-53953941/?t=100)

更新端点的验证测试遵循类似的模式。
例如,要测试一个无效的邮箱地址,先创建一个有效的客户。
然后克隆 `_customerGenerator`,并用一个无效的邮箱字符串修改它。
发送 PUT 请求后,测试断言状态码为 400 BadRequest,并且错误消息正确指出了这个无效的邮箱地址。

```csharp
[Fact]
public async Task Update_ReturnsValidationError_WhenEmailIsInvalid()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();
    
    const string invalidEmail = "dasdja9d3j";
    customer = _customerGenerator.Clone()
        .RuleFor(x => x.Email, invalidEmail).Generate();

    // Act
    var response = await _client.PutAsJsonAsync($"customers/{createdCustomer!.Id}", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    error!.Status.Should().Be(400);
    error.Title.Should().Be("One or more validation errors occurred.");
    error.Errors["Email"][0].Should().Be($"{invalidEmail} is not a valid email address");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-updating-a-resource-53953941/?t=175)

类似地,针对不存在的 GitHub 用户名的测试确保 API 在更新期间能正确验证外部服务依赖。
在创建一个初始的有效客户之后,测试尝试用一个不存在的 GitHub 用户名去更新该客户。
断言验证响应中包含预期的验证 problem details,特别检查 `GitHubUsername` 字段的错误消息。

```csharp
[Fact]
public async Task Update_ReturnsValidationError_WhenGitHubUserDoestNotExist()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();
    
    const string invalidGitHubUser = "dasdja9d3j";
    customer = _customerGenerator.Clone()
        .RuleFor(x => x.GitHubUsername, invalidGitHubUser).Generate();

    // Act
    var response = await _client.PutAsJsonAsync($"customers/{createdCustomer!.Id}", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
    error!.Status.Should().Be(400);
    error.Title.Should().Be("One or more validation errors occurred.");
    error.Errors["GitHubUsername"][0].Should().Be($"There is no GitHub user with username {invalidGitHubUser}");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-updating-a-resource-53953941/?t=230)

## 15. Testing: Deleting resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-deleting-resource-53953942/) · 1:25

本课介绍 ASP.NET Core 应用中 DELETE 端点的集成测试。
它演示了如何验证资源被成功删除(返回 200 OK),以及对不存在资源的处理(返回 404 Not Found)。
通过使用 CustomerApiFactory 和用于生成数据的 Bogus 库,这些测试确保每个操作都在拥有独立数据库实例的隔离环境中被验证,从而保持测试的可靠性和一致性。

### 核心概念

- **Status Code Parity**:Delete 操作常常与 Get 操作有相同的状态码预期(成功时为 OK,资源缺失时为 Not Found)。
- **Test Isolation**:每个测试都针对自己的数据库实例运行,以确保不受其他测试的副作用影响。
- **Resource Lifecycle Testing**:测试删除操作通常需要先创建一个资源,以确保有东西可删。
- **HttpClient.DeleteAsync**:在集成测试中发送 HTTP DELETE 请求的标准方法。

### 课程笔记

测试删除端点很直接,因为与 GET 端点一样,它通常不需要请求正文,而是依靠特定的状态码来表示成功或失败。
为覆盖删除功能的主要场景,实现了以下测试用例:

```markdown
# Write these test cases

## Get
- Get_ReturnsCustomer_WhenCustomerExists
- Get_ReturnsNotFound_WhenCustomerDoesNotExist

## GetAll
- GetAll_ReturnsAllCustomers_WhenCustomersExist
- GetAll_ReturnsEmptyResult_WhenNoCustomersExist

## Update
- Update_UpdatesUser_WhenDataIsValid
- Update_ReturnsValidationError_WhenEmailIsInvalid
- Update_ReturnsValidationError_WhenGitHubUserDoesNotExist

## Delete
- Delete_ReturnsOk_WhenCustomerExists
- Delete_ReturnsNotFound_WhenCustomerDoesNotExist
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-deleting-resource-53953942/?t=10)

测试类 `DeleteCustomerControllerTests` 的结构与其他控制器测试类似,使用 `IClassFixture<CustomerApiFactory>` 管理测试服务器和数据库的生命周期。
使用一个 `Faker<CustomerRequest>` 为最初的创建步骤生成有效数据。

```csharp
public class DeleteCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public DeleteCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-deleting-resource-53953942/?t=25)

#### 测试成功删除

要测试一次成功的删除,测试必须先创建一个客户。
客户创建完成并从响应中取得其 ID 后,对该资源的特定 URI 发起一次 `DeleteAsync` 调用。
测试断言响应状态码为 `HttpStatusCode.OK`。

```csharp
[Fact]
public async Task Delete_ReturnsOk_WhenCustomerExists()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

    // Act
    var response = await _client.DeleteAsync($"customers/{createdCustomer!.Id}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-deleting-resource-53953942/?t=40)

#### 测试不存在的资源

当尝试删除一个不存在的客户时,API 应当返回 `404 Not Found` 状态码。
测试方法是传入一个在数据库中没有对应记录的新 `Guid`。

```csharp
[Fact]
public async Task Delete_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _client.DeleteAsync($"customers/{Guid.NewGuid()}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-deleting-resource-53953942/?t=55)

通过运行这些测试,我们确保删除功能表现正确,并且集成环境能为每次测试运行正确地处理相互隔离的数据库实例。

## 16. Testing: API Dependency Is Unavailable

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-api-dependency-is-unavailable-53953943/) · 4:36

### 总结

集成测试必须考虑外部依赖变得不可用或受限的场景,例如触发 API 速率限制时。
本课演示如何使用 WireMock.Net 模拟一种被限流的状态:配置一个 mock 响应,让它返回 HTTP 403 Forbidden 状态码和特定的错误负载。
通过把这些失败模式纳入测试套件,开发者可以验证应用在压力下的行为,确保即便系统因未处理的依赖失败而返回 Internal Server Error,这种行为也是有记录、经过测试的。

### 核心概念

* **API Throttling**:模拟外部服务(例如 GitHub)在请求配额耗尽时的行为。
* **WireMock.Net Error Simulation**:配置 mock 服务器,使其返回非成功状态码(例如 403 Forbidden)和特定的错误正文。
* **Failure Mode Testing**:验证当一个必需的依赖失败时,API 会按预期响应(即使该响应是一个 500 错误)。
* **Test Data Constants**:在测试工厂中使用共享常量,确保 mock 配置与测试执行之间保持一致。

### 课程笔记

外部 API 常常实现速率限制,并通过 `x-rate-limit-remaining` 之类的头部进行告知。
当超出这些限制时,API 通常会返回 403 Forbidden 状态码,并附带一个说明已超出速率限制的 JSON 正文。

```json
{
    "login": "nickchapsas",
    "id": 67104228,
    "node_id": "MDQ6VXNlcjY3MTA0MjI4",
    "avatar_url": "https://avatars.githubusercontent.com/u/67104228?v=4",
    "gravatar_id": "",
    "url": "https://api.github.com/users/nickchapsas",
    "html_url": "https://github.com/nickchapsas",
    "followers_url": "https://api.github.com/users/nickchapsas/followers"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-api-dependency-is-unavailable-53953943/?t=10)

为了测试系统如何处理这种情况,我们必须更新 mock GitHub 服务器,让它模拟一个被限流的用户。
在 `GitHubApiServer` 类中,我们添加一个 `SetupThrottledUser` 方法。
这个方法配置 WireMock,使其在请求某个特定用户名时返回 403 状态码和标准的 GitHub 速率限制错误消息。

```csharp
public void SetupThrottledUser(string username)
{
    _server.Given(Request.Create()
            .WithPath($"/users/{username}")
            .UsingGet())
        .RespondWith(Response.Create()
            .WithBody(@"{
""message"": ""API rate limit exceeded for 127.0.0.1. (But here's the good news: Authenticated requests get a higher rate limit. Check out the documentation for more details.)"",
""documentation_url"": ""https://docs.github.com/rest/overview/resources-in-the-rest-api#rate-limiting""
}")
            .WithHeader("content-type", "application/json; charset=utf-8")
            .WithStatusCode(403));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-api-dependency-is-unavailable-53953943/?t=115)

接下来,我们更新 `CustomerApiFactory`,为被限流的用户加入一个常量,并确保在 `InitializeAsync` 阶段于 mock 服务器中初始化这个用户。

```csharp
public async Task InitializeAsync()
{
    _gitHubApiServer.Start();
    _gitHubApiServer.SetupUser(ValidGithubUser);
    _gitHubApiServer.SetupThrottledUser(ThrottledUser);
    await _dbContainer.StartAsync();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-api-dependency-is-unavailable-53953943/?t=250)

基础设施就位后,我们就可以编写一个集成测试,验证 GitHub 被限流时 API 的响应。
在这个实现中,API 没有妥善处理 `GitHubService` 在收到 403 时抛出的 `HttpRequestException`,从而导致 HTTP 500 Internal Server Error。
测试确认了这一行为。

```text
POST customers (GitHubUsername = ThrottledUser)
      |
      v
  GitHubService --GET /users/{username}--> WireMock
      |                                       |
      |<---------- 403 (rate limit) ----------+
      v
  throw HttpRequestException  -->  500 Internal Server Error
```

```csharp
[Fact]
public async Task Create_ReturnsInternalServerError_WhenGithubIsThrottled()
{
    // Arrange
    var customer = _customerGenerator.Clone()
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ThrottledUser)
        .Generate();

    // Act
    var response = await _client.PostAsJsonAsync("customers", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-api-dependency-is-unavailable-53953943/?t=220)

这种做法确保系统与外部依赖的集成不仅针对"happy path"进行了测试,也针对速率限制这类预期的失败状态进行了测试。

## 17. Dealing with requests requiring auth

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-requests-requiring-auth-53953944/) · 2:12

对有身份验证或授权要求的 API 进行集成测试,需要操纵 `HttpClient`,让它携带必要的凭据,例如 API key 或 JWT bearer token。
虽然本课程的示例应用没有实现身份验证,但同样的测试原则依然适用:配置请求头部、管理 token,并针对 401 Unauthorized 或 403 Forbidden 等预期状态码进行断言。

### 核心概念

- **Header Manipulation**:通过在 `HttpClient` 上配置头部,把身份验证当作标准的请求元数据来处理。
- **Bearer Tokens**:使用 `DefaultRequestHeaders.Authorization` 属性,以 `Bearer` 方案传递 JWT token。
- **External Authentication**:如果测试需要调用第三方服务来生成 token,就为这个外部调用使用一个专门的 `HttpClient`。
- **Reuse**:在各个测试之间复用身份验证相关的字段和客户端,而不是为单个测试用例重新创建它们。
- **Status Code Validation**:针对 `HttpStatusCode.Unauthorized` 或 `HttpStatusCode.Forbidden` 进行断言,以验证安全逻辑。

### 课程笔记

在集成测试中,身份验证和授权是通过配置用于向测试服务器发起请求的 `HttpClient` 来处理的。
由于身份验证要求因用例而差异很大,从自定义头部到复杂的权限集合都有,因此应当像对待任何其他请求属性一样对待它们。

在测试环境中,常用常量来表示特定的用户状态,例如有效用户或被限流的用户,这些常量随后用于配置 mock 服务器(例如 WireMock)和测试数据生成器。

```csharp
public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public const string ValidGithubUser = "validuser";
    public const string ThrottledUser = "throttle";
    
    private readonly TestcontainerDatabase _dbContainer =
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "db",
                Username = "course",
                Password = "whatever"
            }).Build();
    private readonly GitHubApiServer _gitHubApiServer = new ();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IHostedService));
            
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
            
            services.AddHttpClient("GitHub", httpClient =>
            {
                httpClient.BaseAddress = new Uri(_gitHubApiServer.Url);
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.Accept, "application/vnd.github.v3+json");
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
            });
        });
    }

    public async Task InitializeAsync()
    {
        _gitHubApiServer.Start();
        _gitHubApiServer.SetupUser(ValidGithubUser);
        _gitHubApiServer.SetupThrottledUser(ThrottledUser);
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        _gitHubApiServer.Dispose();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-requests-requiring-auth-53953944/?t=10)

当请求需要 JWT token 时,你可以配置 `HttpClient`,在 `Authorization` 头部中携带一个 Bearer token。
这让你可以完全控制传给 API 的头部、正文和查询参数。

```csharp
public async Task Create_ReturnsInternalServerError_WhenGithubIsThrottled()
{
    // Arrange
    var customer = _customerGenerator.Clone()
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ThrottledUser)
        .Generate();

    _client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", "your-jwt-token-here");

    // Act
    var response = await _client.PostAsJsonAsync("customers", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-requests-requiring-auth-53953944/?t=55)

如果你的测试需要调用一个外部第三方服务来生成 token,你可能需要创建一个单独的、专门用来调用外部世界的 `HttpClient`。
为了提升性能,建议在各个测试之间复用这类客户端或身份验证 token,而不是让它们专属于某个单独的测试方法。

```csharp
[Fact]
public async Task Create_ReturnsInternalServerError_WhenGithubIsThrottled()
{
    // Arrange
    var customer = _customerGenerator.Clone()
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ThrottledUser)
        .Generate();

    var externalAuthClient = new HttpClient();
    // Use externalAuthClient to fetch a token...

    // Act
    var response = await _client.PostAsJsonAsync("customers", customer);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-requests-requiring-auth-53953944/?t=85)

归根结底,测试需要身份验证的端点与测试公开端点遵循同样的模式:你控制请求元数据,并验证得到的状态码、响应正文和头部。
如果一个请求未经身份验证,你就会断言其状态码为 `Unauthorized`。

## 18. Dealing with background services

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-background-services-53953945/) · 1:36

ASP.NET Core 中的后台服务,例如继承自 `BackgroundService` 或实现 `IHostedService` 的那些,可能会消耗资源或执行超出测试范围的副作用,从而干扰集成测试。
为了确保一个干净、可预测的测试环境,通常应当在测试工厂的配置中把这些服务从依赖注入(DI)容器中移除。
这让开发者能够完全控制主机的生命周期,并防止后台任务在测试执行期间触发事件或修改状态。

### 核心概念

*   **Background Services**:实现 `IHostedService` 或继承自 `BackgroundService` 的类,它们独立于 HTTP 请求运行逻辑。
*   **Test Interference**:后台任务可能以某些方式消耗资源、触发事件或修改数据,使集成测试变得不确定。
*   **DI Container Control**:`WebApplicationFactory` 允许在测试主机构建之前完全操纵 Service Collection。
*   **Service Removal**:使用 `RemoveAll(typeof(IHostedService))` 从集成测试环境中剥离所有后台任务。

### 课程笔记

ASP.NET Core 应用中包含后台任务是很常见的,例如重复执行的定时器或一次性的启动例程。
这些任务通常通过继承 `BackgroundService` 来实现。

```csharp
namespace Customers.Api;

public class BackgroundRepeatingTask : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Background logic implementation
        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-background-services-53953945/?t=25)

这些服务在 `Program.cs` 中通过 `AddHostedService` 扩展方法注册到依赖注入(DI)容器中。

```csharp
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();

builder.Services.AddHttpClient("GitHub", httpClient =>
{
    httpClient.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl"));
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
});

builder.Services.AddHostedService<BackgroundRepeatingTask>();

var app = builder.Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-background-services-53953945/?t=40)

在集成测试场景中,运行这些后台任务往往是不希望发生的。
它们可能执行干扰测试流程的操作、消耗不必要的 CPU/内存,或触发外部事件。
由于集成测试主机应当尽可能隔离、尽可能可预测,你应当在自定义的 `WebApplicationFactory` 中移除这些服务。

通过覆盖 `ConfigureWebHost` 并使用 `ConfigureTestServices`,你可以访问 `IServiceCollection`,移除所有 `IHostedService` 注册(如果测试流程需要其中某些服务,也可以只移除特定类型)。

```csharp
protected override void ConfigureWebHost(IWebHostBuilder builder)
{
    builder.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    });

    builder.ConfigureTestServices(services =>
    {
        services.RemoveAll(typeof(IHostedService));
        
        services.RemoveAll(typeof(IDbConnectionFactory));
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
        
        services.AddHttpClient("GitHub", httpClient =>
        {
            httpClient.BaseAddress = new Uri(_gitHubApiServer.Url);
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
        });
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-background-services-53953945/?t=70)

这种做法确保后台服务不会在测试主机初始化时启动,为执行 API 层面的集成测试提供一个更干净的环境。

## 19. Integration testing Minimal APIs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/integration-testing-minimal-apis-53953946/) · 0:52

### 总结

在 ASP.NET Core 中对 Minimal API 进行集成测试,在概念上与测试基于 Controller 的 API 完全相同,因为测试框架是以外部观察者的身份与应用交互的。
由于 Minimal API 通常没有 Startup 类,所以会使用一个标记接口,为 WebApplicationFactory 提供对 API 项目程序集的引用。
这种做法确保测试套件与路由和端点定义的内部实现细节保持解耦。

### 核心概念

*   **Black-box testing**:从外部观察者的视角测试端点,确保测试与实现无关。
*   **Marker Interfaces**:当不存在 `Startup` 类时,在 API 项目中使用一个简单接口(例如 `IApiMarker`),为 `WebApplicationFactory` 提供类型安全的引用。
*   **Consistency**:无论底层实现使用的是 Controller 还是 Minimal API,集成测试都保持一致。

### 课程笔记

Minimal API 的集成测试遵循与传统的基于 Controller 的应用相同的原则。
从集成测试的视角来看,API 的内部结构(无论它采用 MVC 模式还是 Minimal API 模型)都无关紧要。
测试依旧使用 `HttpClient` 从外部调用端点,并针对响应进行断言。

主要的技术差别在于 Minimal API 项目中没有 `Startup` 类。
为了让 `WebApplicationFactory` 能找到正确的程序集和入口点,通常会在 API 项目中定义一个标记接口。

```csharp
namespace Customers.Api;

public interface IApiMarker
{
    
}
```

然后把 `WebApplicationFactory` 的类型参数设为这个标记接口。
这个工厂管理被测应用的生命周期,并允许覆盖服务。

```csharp
public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public const string ValidGithubUser = "validuser";
    public const string ThrottledUser = "throttle";
    
    private readonly TestcontainerDatabase _dbContainer =
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "db",
                Username = "course",
                Password = "whatever"
            }).Build();
    private readonly GitHubApiServer _gitHubApiServer = new ();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/integration-testing-minimal-apis-53953946/?t=50)

在 `ConfigureWebHost` 方法中,测试环境的配置方式是清除默认的日志提供程序,并用测试专用的实现替换生产服务,例如数据库容器连接和 mock GitHub API 服务器。

```csharp
protected override void ConfigureWebHost(IWebHostBuilder builder)
{
    builder.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    });

    builder.ConfigureTestServices(services =>
    {
        services.RemoveAll(typeof(IHostedService));
        
        services.RemoveAll(typeof(IDbConnectionFactory));
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
        
        services.AddHttpClient("GitHub", httpClient =>
        {
            httpClient.BaseAddress = new Uri(_gitHubApiServer.Url);
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
        });
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/integration-testing-minimal-apis-53953946/?t=10)

工厂配置好之后,测试类用它来创建一个 `HttpClient`。
测试逻辑始终聚焦于 API 的行为。
Bogus 这类数据生成工具被用来创建逼真的请求对象。

```csharp
public class DeleteCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public DeleteCustomerControllerTests(CustomerApiFactory apiFactory)
    {        _client = apiFactory.CreateClient();
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/integration-testing-minimal-apis-53953946/?t=40)

各个测试方法针对正在运行的测试实例执行 HTTP 请求。
例如,删除客户的测试会先创建一个客户,然后调用 DELETE 端点,并验证状态码。

```csharp
[Fact]
public async Task Delete_ReturnsOk_WhenCustomerExists()
{
    // Arrange
    var customer = _customerGenerator.Generate();
    var createdResponse = await _client.PostAsJsonAsync("customers", customer);
    var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

    // Act
    var response = await _client.DeleteAsync($"customers/{createdCustomer!.Id}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/integration-testing-minimal-apis-53953946/?t=25)

这种一致性确保即使底层 API 实现从 Controller 重构为 Minimal API,集成测试依然是一张可靠的安全网。

## 20. Dealing with Entity Framework

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-entity-framework-53953947/) · 3:10

### 总结

本课讨论一种常见但有缺陷的做法:使用 Entity Framework Core 的 In-Memory 数据库提供程序进行集成测试。
它强调集成测试必须验证与生产环境所用数据库系统的实际交互,因为内存提供程序在查询、索引和约束方面的行为与之不同。
本课演示如何正确地为测试配置一个真实数据库:在自定义 WebApplicationFactory 中覆盖 DbContext 注册,使用由 Testcontainer 实例提供的连接字符串,从而确保这个集成点被准确地验证。

### 核心概念

* **Avoid In-Memory Providers**:EF Core In-Memory 数据库不能在集成测试中替代真实数据库,因为它不会像 PostgreSQL 这样的生产数据库那样强制执行同样的约束,也不会执行同样的查询逻辑。
* **Integration Point Integrity**:集成测试必须测试实际的集成点;把数据库换成一个假的,会从根本上把测试的性质从集成测试变成单元测试。
* **Service Overriding**:在 `WebApplicationFactory` 中使用 `ConfigureTestServices` 移除已有的 `DbContext` 注册,并替换为指向测试容器的配置。
* **Dynamic Connection Strings**:Testcontainers 提供动态的连接字符串,必须在测试准备阶段把它传给 EF Core 的 `UseNpgsql`(或等价方法)配置。

### 课程笔记

在为使用 Entity Framework Core 的应用实现集成测试时,关键是要避免使用 `UseInMemoryDatabase` 方法。
虽然教程中常常推荐它,但使用内存提供程序会毁掉集成点。
它测试的是一个在处理请求、索引和约束方面与真实数据库有着根本差异的系统。
为了保持集成测试的有效性,你必须针对生产中使用的同一种数据库引擎进行测试,它通常通过 Testcontainers 托管在一个 Docker 容器中。

在自定义的 `WebApplicationFactory` 中,准备工作从定义数据库容器和任何外部 API mock(例如用于 GitHub 的 WireMock 服务器)开始。

```csharp
private readonly TestcontainerDatabase _dbContainer =
    new TestcontainersBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = "db",
            Username = "course",
            Password = "whatever"
        }).Build();

private readonly GitHubApiServer _gitHubApiServer = new ();

protected override void ConfigureWebHost(IWebHostBuilder builder)
{
    builder.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    });

    builder.ConfigureTestServices(services =>
    {
        services.RemoveAll(typeof(IHostedService));
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-entity-framework-53953947/?t=10)

在 `ConfigureTestServices` 内部,你必须移除已有的数据库注册,并替换为使用容器连接字符串的版本。
这既适用于直接的 `IDbConnectionFactory` 注册(如果使用 Dapper),也适用于 `DbContext` 注册。

```csharp
        services.RemoveAll(typeof(IDbConnectionFactory));
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(_dbContainer.ConnectionString));

        services.AddHttpClient("GitHub", httpClient =>
        {
            httpClient.BaseAddress = new Uri(_gitHubApiServer.Url);
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
        });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-entity-framework-53953947/?t=25)

要正确处理 Entity Framework Core,你必须专门移除 `AppDbContext`,并使用 `_dbContainer` 提供的连接字符串重新注册它。
这确保 EF Core 指向的是临时的 PostgreSQL 实例,而不是本地开发数据库或一个内存中的假实现。

```csharp
        services.RemoveAll(typeof(AppDbContext));
        services.AddDbContext<AppDbContext>(
            optionsBuilder => optionsBuilder.UseNpgsql(_dbContainer.ConnectionString));
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-entity-framework-53953947/?t=145)

```text
ConfigureTestServices:

IDbConnectionFactory --RemoveAll--> AddSingleton<IDbConnectionFactory>
                                      NpgsqlConnectionFactory(
                                        _dbContainer.ConnectionString)

AppDbContext         --RemoveAll--> AddDbContext<AppDbContext>
                                      UseNpgsql(
                                        _dbContainer.ConnectionString)
```

最后,必须管理这些依赖的生命周期。
`InitializeAsync` 方法启动外部 API 服务器和数据库容器,而 `DisposeAsync` 确保测试运行结束后资源被清理。

```csharp
public async Task InitializeAsync()
{
    _gitHubApiServer.Start();
    _gitHubApiServer.SetupUser(ValidGithubUser);
    _gitHubApiServer.SetupThrottledUser(ThrottledUser);
    await _dbContainer.StartAsync();
}

public new async Task DisposeAsync()
{
    await _dbContainer.DisposeAsync();
    _gitHubApiServer.Dispose();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/dealing-with-entity-framework-53953947/?t=175)

## 21. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953948/) · 1:06

### 总结

本课为 ASP.NET Core API 真实世界集成测试这一章作结,总结了从基础知识到高级测试模式的过渡。
关键要点包括:使用 Testcontainers 为 PostgreSQL 数据库编排隔离的测试环境,以及使用 WireMock 模拟 GitHub API 这类外部依赖。
本章演示了如何实现一个自定义 WebApplicationFactory 来管理这些资源,并提供了一套以健壮、可重复的方式测试完整 CRUD 操作、验证逻辑和错误处理场景的框架。

### 核心概念

*   **Isolated Database Testing**:借助 Testcontainers 使用 Docker,为每次测试运行提供一个干净、隔离的数据库实例。
*   **External Dependency Mocking**:利用 WireMock 模拟第三方 API(例如 GitHub),以测试系统如何处理各种外部响应。
*   **CRUD Integration Testing**:为资源的完整生命周期实现测试,包括 Create、Read、Update 和 Delete 操作。
*   **Test Environment Orchestration**:使用自定义 `WebApplicationFactory` 配置服务、管理容器生命周期,并为测试提供一个 `HttpClient`。
*   **Validation and Error Handling**:验证 API 对无效输入能正确返回 400 Bad Request,并能处理 403 Forbidden 这类外部服务错误。

### 课程笔记

"Integration testing in the real world"这一章聚焦于为 ASP.NET Core API 构建一个可用于生产的测试套件。
这种做法超越了简单的内存 mock,转而以隔离的方式使用真实的基础设施组件。

#### 测试基础设施编排

测试策略的核心是一个自定义的 `WebApplicationFactory`。
这个工厂使用 Testcontainers 管理一个 PostgreSQL 容器的生命周期,并管理一个用于模拟外部 API 的 WireMock 服务器。
它覆盖了标准的服务注册,让应用指向这些由测试控制的资源。
该工厂使用一个标记接口 `IApiMarker` 来定位 API 项目的 `Program` 类。

```csharp
public class CustomerApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public const string ValidGithubUser = "validuser";
    public const string ThrottledUser = "throttle";
    
    private readonly TestcontainerDatabase _dbContainer =
        new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "db",
                Username = "course",
                Password = "whatever"
            }).Build();
    private readonly GitHubApiServer _gitHubApiServer = new ();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IHostedService));
            
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(_ =>
                new NpgsqlConnectionFactory(_dbContainer.ConnectionString));
            
            services.AddHttpClient("GitHub", httpClient =>
            {
                httpClient.BaseAddress = new Uri(_gitHubApiServer.Url);
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.Accept, "application/vnd.github.v3+json");
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
            });
        });
    }

    public async Task InitializeAsync()
    {
        _gitHubApiServer.Start();
        _gitHubApiServer.SetupUser(ValidGithubUser);
        _gitHubApiServer.SetupThrottledUser(ThrottledUser);
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        _gitHubApiServer.Dispose();
    }
}
```

```text
               CustomerApiFactory : WebApplicationFactory<IApiMarker>
                              |
        +---------------------+----------------------+
        |                     |                      |
        v                     v                      v
 _dbContainer          _gitHubApiServer       ConfigureTestServices
 (PostgreSqlTest-      (WireMock)               RemoveAll(IHostedService)
  container)             validuser -> 200       IDbConnectionFactory
                         throttle  -> 403       HttpClient "GitHub"
```

#### Mock 外部服务

为了在不发起实际网络调用的情况下测试与 GitHub API 的交互,使用了一个包装 WireMock 的 `GitHubApiServer`。
这让测试套件能够为不同的用户名定义特定的响应,包括成功的情况,以及速率限制(限流)这类错误情况。

```csharp
public class GitHubApiServer : IDisposable
{
    private WireMockServer _server;

    public string Url => _server.Url!;

    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void SetupUser(string username)
    {
        _server.Given(Request.Create()
            .WithPath($"/users/{username}")
            .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(GenerateGitHubUserResponseBody(username))
                .WithHeader("content-type", "application/json; charset=utf-8")
                .WithStatusCode(200));
    }

    public void SetupThrottledUser(string username)
    {
        _server.Given(Request.Create()
                .WithPath($"/users/{username}")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(@"{
    \"message\": \"API rate limit exceeded for 127.0.0.1.\",
    \"documentation_url\": \"https://docs.github.com/rest/overview/resources-in-the-rest-api#rate-limiting\"
}")
                .WithHeader("content-type", "application/json; charset=utf-8")
                .WithStatusCode(403));
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }

    private static string GenerateGitHubUserResponseBody(string username)
    {
        return $@"{{
  \"login\": \"{username}\",
  \"id\": 67104228,
  \"type\": \"User\"
}}";
    }
}
```

#### 实现集成测试

基础设施就位后,测试会在不同条件下验证 API 的行为。
例如,`CreateCustomerControllerTests` 类验证在提供有效数据时可以创建客户,并且 API 会针对无效数据或外部服务失败返回恰当的验证错误。

```csharp
public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_CreatesUser_WhenDataIsValid()
    {
        // Arrange
        var customer = _customerGenerator.Generate();

        // Act
        var response = await _client.PostAsJsonAsync("customers", customer);

        // Assert
        var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        customerResponse.Should().BeEquivalentTo(customer);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
```

本章最后强调了通过练习获得实践经验的重要性,这些练习涵盖了其余的 CRUD 操作(Get、GetAll、Update、Delete)。
后续章节将从 API 集成测试过渡到 UI 测试。

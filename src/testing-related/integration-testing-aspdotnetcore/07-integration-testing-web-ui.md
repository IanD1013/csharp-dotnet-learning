# Integration testing Web UI

> 课程:[From Zero to Hero: Integration testing in ASP.NET Core](https://dometrain.com/course/from-zero-to-hero-integration-testing-in-asp-net-core/) · 第 7 章
> 共 12 课 · 约 57 分钟
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The project we will be writing UI integration tests for](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-ui-integration-tests-for-53953964/) | 2:15 | [↓](#1-the-project-we-will-be-writing-ui-integration-tests-for) |
| 2 | [What we need to do to test the UI](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-we-need-to-do-to-test-the-ui-53953965/) | 1:41 | [↓](#2-what-we-need-to-do-to-test-the-ui) |
| 3 | [Introducing Playwright](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-playwright-53953966/) | 4:12 | [↓](#3-introducing-playwright) |
| 4 | [How our Docker strategy changes](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/) | 6:37 | [↓](#4-how-our-docker-strategy-changes) |
| 5 | [Bootstraping the test project](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/) | 7:11 | [↓](#5-bootstraping-the-test-project) |
| 6 | [Testing: Create a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/) | 14:33 | [↓](#6-testing-create-a-resource) |
| 7 | [Testing: Get a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-get-a-resource-53953971/) | 6:37 | [↓](#7-testing-get-a-resource) |
| 8 | [Your turn to practice](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/your-turn-to-practice-53953972/) | 0:48 | [↓](#8-your-turn-to-practice) |
| 9 | [Testing: Getting all the resources](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-the-resources-53953974/) | 3:07 | [↓](#9-testing-getting-all-the-resources) |
| 10 | [Testing: Update a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/) | 5:57 | [↓](#10-testing-update-a-resource) |
| 11 | [Testing: Delete a resource](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/) | 3:46 | [↓](#11-testing-delete-a-resource) |
| 12 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953978/) | 0:41 | [↓](#12-section-recap) |

## 1. The project we will be writing UI integration tests for

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-writing-ui-integration-tests-for-53953964/) · 2:15

### 总结

本课介绍用于 UI 集成测试的 Blazor Server 端应用。
该应用是一个基于 CRUD 的客户管理系统,允许用户列出、创建、查看、编辑和删除客户记录。
它具备复杂的验证逻辑,包括邮箱格式检查和外部 GitHub 用户名验证。
与 API 测试相比,测试这个 UI 会带来独特的挑战,需要一种不同的连接(wiring)与交互方式,以确保 UI 元素的功能层面得到正确的执行。

### 核心概念

* Blazor Server 端 CRUD 应用。
* 由 UI 驱动的集成测试所面临的挑战。
* 功能性验证(Email、GitHub 用户名、出生日期)。
* 外部服务依赖(GitHub API)。
* 应用结构(Repositories、Services 和 Models)。

### 课程笔记

UI 集成测试的目标应用是一个 ASP.NET Core Blazor Server 项目。
虽然实现使用的是 Blazor,但这些测试原则同样适用于 MVC 等其他 Web UI 框架。
该应用管理一个客户数据库,并提供完整的一套 CRUD(创建、读取、更新、删除)操作。

应用的核心功能围绕 `Customer` 模型展开。
该模型包含 `GitHubUsername`、`FullName`、`Email` 和 `DateOfBirth` 字段。
验证是应用的关键组成部分,它通过多个层面来保证数据完整性:

1.  **Email 验证**:确保输入符合标准的邮箱格式。
2.  **GitHub 用户名验证**:一项外部检查,验证所提供的用户名是否在 GitHub 上存在。
3.  **出生日期验证**:确保所提供的日期不在未来。
4.  **全名验证**:使用正则表达式确保名称格式有效。

当用户尝试添加或编辑客户时,这些验证就会被触发。
例如,如果提供了无效的 GitHub 用户名,应用会显示一条错误消息,说明该用户不存在,从而阻止表单提交。
应用还为每个客户提供详情视图,以及删除记录的能力。

应用架构遵循标准的 service-repository 模式。
`CustomerService` 负责编排业务逻辑,而 `CustomerRepository` 使用 Dapper 和 PostgreSQL 处理数据持久化。
`GitHubService` 负责对 GitHub 的外部 API 调用。

```text
  CustomerService (业务逻辑)
     |                    |
     v                    v
  CustomerRepository   GitHubService
     |                    |
     v                    v
  Dapper + PostgreSQL  GitHub API
```

UI 集成测试被认为是整个测试套件中最具挑战性的部分。
与关注请求和响应负载的 API 测试不同,UI 集成测试需要模拟用户交互,并验证 Web 界面最终呈现的状态。
这就需要用一种不同的方式来连接应用及其依赖,以确保 UI 元素的功能层面得到正确的执行。

## 2. What we need to do to test the UI

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-we-need-to-do-to-test-the-ui-53953965/) · 1:41

### 总结

测试 Blazor Server 端 UI 需要从内存托管转向环境编排。
虽然 WebApplicationFactory 适合 API 测试,但在处理 UI 导航和 HTML 解析时它会变得过于复杂。
所提出的策略是:使用 Docker Compose 运行完整的应用栈(包括数据库和被模拟的外部服务),通过 Fluent Docker 以编程方式控制它,并使用 Playwright 进行基于浏览器的断言。

### 核心概念

- 使用 WebApplicationFactory 进行 UI 测试的挑战
- 使用 Docker Compose 进行环境编排
- 使用 WireMock 模拟外部服务
- 通过 Fluent Docker 以编程方式管理容器
- 使用 Playwright 进行浏览器自动化和端到端测试

### 课程笔记

测试 Blazor Server 端应用的用户界面需要一种不同于标准 API 测试的方法。
虽然 `WebApplicationFactory` 是 ASP.NET Core 中内存集成测试的标准方案,但它很不适合 UI 测试。
在 factory 中手动解析 HTML 和管理导航很复杂,而且往往会导致测试脆弱。

推荐的策略是使用 Docker 和 Docker Compose 编排一个完整的环境。
这种方法在容器中运行应用及其数据库,使测试套件能够与一个运行中的实例交互。
GitHub API 等外部依赖会被替换为 WireMock,以确保测试的隔离性和可靠性。

有两个关键库支撑这一策略:

- **Fluent Docker**:一个用于以编程方式控制 Docker Compose 的 .NET 包。与前面章节使用的 Testcontainers 库不同,这里使用 Fluent Docker 来管理整个容器化应用栈的生命周期。
- **Playwright**:一个模拟用户交互的浏览器自动化框架。它允许测试执行点击按钮、填写表单等操作,同时在多种浏览器引擎上验证 UI 最终呈现的状态。

```text
  Test suite
     |-- Fluent Docker --> Docker Compose
     |                       |-- app container
     |                       '-- database container
     |-- Playwright -------> browser --> app container
     '-- WireMock (替代 GitHub API) <-- app container
```

## 3. Introducing Playwright

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-playwright-53953966/) · 4:12

Playwright 是一个浏览器自动化工具,允许开发者以编程方式控制浏览器。
它是 Selenium 的现代替代品,能够在真实网站上模拟用户操作,以用于集成测试。
Playwright 可以以 headless 模式(在后台运行)或 headed 模式(对用户可见)操作浏览器。
它支持复杂的交互,包括执行 JavaScript、评估 DOM 事件以及处理各种形式的用户输入。

### 核心概念

- 用于 Web UI 集成测试的浏览器自动化。
- Headless 与 Headed 浏览器执行。
- 安装 Playwright NuGet 包和浏览器二进制文件。
- 在 CI/CD 环境中以编程方式安装 Playwright。
- 在测试中执行 JavaScript 并评估 DOM 事件。

### 课程笔记

要开始使用 Playwright,可以通过 .NET CLI 初始化一个新项目:

```bash
# Create project
dotnet new console -n PlaywrightDemo
cd PlaywrightDemo
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-playwright-53953966/?t=70)

对于测试 ASP.NET Core 应用,通常会使用一个名为 `Customers.WebApp.Tests.Integration` 的专用 xUnit 项目。
该项目引入了几个关键库:用于生成逼真测试数据的 `Bogus`、用于模拟 GitHub API 等外部服务的 `WireMock.Net`,以及用于编写富有表达力的测试验证的 `FluentAssertions`。

Playwright 内置了对多种测试框架的支持。
例如,它为 NUnit 提供了简化浏览器管理的基类:

```csharp
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class Tests : PageTest
{
    [Test]
    public async Task ShouldAdd()
    {
        int result = await Page.EvaluateAsync<int>("() => 7 + 3");
        Assert.AreEqual(10, result);
    }

    [Test]
    public async Task ShouldMultiply()
    {
        int result = await Page.EvaluateAsync<int>("() => 7 * 3");
        Assert.AreEqual(21, result);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-playwright-53953966/?t=85)

#### 安装与环境设置

Playwright 的设置过程包括两个主要步骤:添加 `Microsoft.Playwright` NuGet 包,以及安装所需的浏览器二进制文件(Chromium、Firefox 和 WebKit)。

构建项目后,Playwright 会在输出目录中生成一个 PowerShell 脚本(例如 `bin/Debug/net6.0/playwright.ps1`)。
该脚本用于下载并安装浏览器:

```powershell
pwsh bin/Debug/net6.0/playwright.ps1 install
```

在 CI/CD 环境或自动化服务器设置中,通常更倾向于以编程方式处理浏览器安装。
这可以通过在应用的初始化逻辑中调用 Playwright 的程序入口点来实现:

```csharp
namespace Customers.WebApp.Tests.Integration;

public class Startup
{
    public void Test()
    {
        
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-playwright-53953966/?t=220)

以 `"install"` 参数调用 `Microsoft.Playwright.Program.Main` 方法,以确保所有浏览器依赖都已就位:

```csharp
namespace Customers.WebApp.Tests.Integration;

public class Startup
{
    public void Test()
    {
        Microsoft.Playwright.Program.Main(new[] { "install" });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/introducing-playwright-53953966/?t=235)

这确保了在执行任何基于 UI 的集成测试之前,环境都已正确配置。

## 4. How our Docker strategy changes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/) · 6:37

### 总结

使用 Playwright 对 Web UI 进行集成测试,需要摆脱标准的 WebApplicationFactory 方法。
由于 Playwright 作为外部浏览器运行,它无法与 API 测试中通常使用的内存服务器实例交互。
取而代之的是使用 Docker Compose 策略来编排一个真实环境,让 Web UI 及其依赖(例如 PostgreSQL 数据库)作为联网的容器运行。
这种方法确保应用在一个与生产高度相似的状态下被测试,包括完整的 SSL 支持以及服务之间真实的网络通信。

### 核心概念

- Playwright 需要一个真实的、联网的服务器实例。
- 使用 Docker Compose 编排测试依赖。
- 使用 `pg_isready` 实现 Docker 健康检查,以确保服务就绪。
- 使用 Dockerfile 从本地源代码构建应用镜像。
- 通过使用 `AddEnvironmentVariables` 前缀的环境变量覆盖应用配置。
- 在容器化环境中配置 Kestrel SSL 证书。

### 课程笔记

ASP.NET Core 中的标准集成测试通常依赖 `WebApplicationFactory` 来运行服务器的内存版本。
然而,在使用 Playwright 进行 Web UI 测试时,这种方法行不通,因为 Playwright 无法调用内存中的服务器。
为了解决这个问题,我们使用 Docker Compose 为集成测试启动一个专用环境。
与 API 测试中每个测试类可能都有自己的服务器不同,这些 UI 测试共享同一个 Docker Compose 实现。

#### 配置数据库服务

环境的第一个组件是 PostgreSQL 数据库。
为了确保数据库在应用尝试连接之前已完全初始化,我们使用 `pg_isready` 命令实现一个健康检查。
这确保了依赖它的容器只会在数据库状态为 "healthy" 之后才启动。

```yaml
version: '3.9'

services:

  test-db:
    image: postgres:latest
    restart: always
    environment:      - POSTGRES_USER=course
      - POSTGRES_PASSWORD=changeme
      - POSTGRES_DB=mydb
    healthcheck:
      test: [ "CMD-SHELL", "pg_isready" ]
      interval: 2s
      timeout: 5s
      retries: 10
    ports:
      - '5435:5432'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=130)

#### 构建 Web 应用镜像

Web UI 项目包含一个 `Dockerfile`,让我们可以为应用构建容器镜像。
这个 Dockerfile 负责还原依赖、发布 release 版本以及配置运行时环境,其中包括注入 SSL 证书。

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
ENTRYPOINT ["dotnet", "Customers.WebApp.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=145)

#### 编排应用服务

在 Docker Compose 文件中,我们定义 `test-app` 服务。
我们把内部端口(SSL 用 443,非 SSL 用 80)映射到 Playwright 用来访问 UI 的外部端口。

```yaml
  test-app:
    build: ../../src/Customers.WebApp
    ports:
      - "7780:443"
      - "7779:80"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=200)

我们还必须指定环境变量,以确保容器监听正确的端口,并让 Kestrel 使用所提供的 SSL 证书。

```yaml
    environment:
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_Kestrel__Certificates__Default__Password=Test1234!
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/cert.pfx
      - ASPNETCORE_Environment=Production
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=250)

#### 通过环境变量覆盖配置

为了让应用指向测试数据库和模拟 API,我们利用 ASP.NET Core 的配置系统。
在 `Program.cs` 中,我们将应用配置为加载带有特定前缀 `CustomersWebApp_` 的环境变量。

```csharp
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
var config = builder.Configuration;
config.AddEnvironmentVariables("CustomersWebApp_");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=280)

这让我们可以使用前缀加上配置路径、并以双下划线(`__`)作为分隔符,来覆盖 `appsettings.json` 中的值。
例如,要覆盖数据库连接字符串,我们使用 `CustomersWebApp_Database__ConnectionString`。

```yaml
      - CustomersWebApp_Database__ConnectionString=Server=test-db;Port=5432;Database=mydb;User ID=course;Password=changeme;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=325)

最后,我们覆盖 GitHub API 的基础 URL,使其指向我们的 WireMock 实例。
最终的 `docker-compose.integration.yml` 确保应用已为 Playwright 执行完全引导就绪。

```yaml
version: '3.9'

services:

  test-app:
    build: ../../src/Customers.WebApp
    ports:
      - "7780:443"
      - "7779:80"
    environment:
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_Kestrel__Certificates__Default__Password=Test1234!
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/cert.pfx
      - ASPNETCORE_Environment=Production
      - CustomersWebApp_Database__ConnectionString=Server=test-db;Port=5432;Database=mydb;User ID=course;Password=changeme;
      - CustomersWebApp_GitHub__ApiBaseUrl=http://localhost:9850
    depends_on: 
      test-db:
        condition: service_healthy

  test-db:
    image: postgres:latest
    restart: always
    environment:
      - POSTGRES_USER=course
      - POSTGRES_PASSWORD=changeme
      - POSTGRES_DB=mydb
    healthcheck:
      test: [ "CMD-SHELL", "pg_isready" ]
      interval: 2s
      timeout: 5s
      retries: 10
    ports:
      - '5435:5432'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/how-our-docker-strategy-changes-53953967/?t=385)

```text
  Playwright
     |  7780 -> 443 (https)
     |  7779 -> 80  (http)
     v
  +-----------+  depends_on: service_healthy   +-----------+
  | test-app  |------------------------------->| test-db   |
  |           |  Server=test-db;Port=5432      | postgres  |
  +-----------+                                +-----------+
     |                                          5435 -> 5432
     |  GitHub__ApiBaseUrl
     v
  http://localhost:9850 (WireMock)
```

## 5. Bootstraping the test project

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/) · 7:11

### 总结

本课详细介绍了在 ASP.NET Core 中对 Web UI 进行集成测试所需的基础设施搭建。
它引入了一个共享测试上下文,用于管理外部依赖的生命周期,包括通过 WireMock 模拟的 GitHub API、通过 FluentDocker 托管在 Docker 中的被测应用,以及使用 Playwright 的浏览器自动化。
通过把这些设置集中到一个共享的上下文和集合中,测试可以针对一致的环境顺序运行,确保浏览器、数据库和外部服务在测试执行前被正确初始化,并在之后被释放。

### 核心概念

*   **共享测试上下文**:一个实现 `IAsyncLifetime` 的中心类,用于管理测试依赖的启动与拆除。
*   **共享测试集合**:一个使用 `ICollectionFixture` 的 xUnit 集合定义,用于在多个测试类之间共享状态并确保顺序执行。
*   **FluentDocker**:一个用于在测试套件内部以编程方式控制 Docker Compose 文件、从而启动应用及其数据库的库。
*   **Playwright**:一个 Web UI 自动化框架,配置为以特定选项(例如 headless 模式和慢动作执行)启动 Chromium 浏览器。
*   **WireMock**:用于在固定端口上模拟外部 API 依赖(GitHub),为被测系统提供可预测的响应。

### 课程笔记

#### 创建共享上下文

为了开始引导测试项目,我们创建一个共享上下文来管理环境。
与前面依赖 `WebApplicationFactory` 的章节不同,Web UI 测试需要一个自定义上下文来处理浏览器和外部服务。
我们定义一个实现 `IAsyncLifetime` 的 `SharedTestContext` 类,来处理异步的初始化和释放。

```csharp
namespace Customers.WebApp.Tests.Integration;

public class SharedTestContext : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/?t=40)

为了在多个测试类之间共享这个上下文并确保它们顺序运行,我们使用 `[CollectionDefinition]` 特性定义一个 `SharedTestCollection`。

```csharp
using Xunit;

namespace Customers.WebApp.Tests.Integration;

[CollectionDefinition("Test collection")]
public class SharedTestCollection : ICollectionFixture<SharedTestContext>
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/?t=70)

#### 模拟外部服务

系统会与 GitHub API 交互,而在集成测试中必须对其进行模拟。
我们复用前面章节中的 `GitHubApiServer` 实现,但将其配置为在特定端口(9850)上运行,以确保 Docker 内的应用尝试与它通信时保持一致。

```csharp
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Customers.WebApp.Tests.Integration;

public class GitHubApiServer : IDisposable
{
    private WireMockServer _server;

    public string Url => _server.Url!;

    public void Start()
    {
        _server = WireMockServer.Start(9850);
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

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }

    private static string GenerateGitHubUserResponseBody(string username)
    {
        return $@"{{
  ""login"": ""{username}"",
  ""id"": 67104228,
  ""url"": ""https://api.github.com/users/{username}"",
  ""type"": ""User""
}}";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/?t=100)

#### 使用 FluentDocker 编排 Docker

我们使用一个 Docker Compose 文件来托管应用及其数据库。
为了从代码中控制它,我们使用 `Ductus.FluentDocker` 库。
首先,我们相对于测试执行目录定位 `docker-compose.integration.yml` 文件。

```csharp
private static readonly string DockerComposeFile =
    Path.Combine(Directory.GetCurrentDirectory(), (TemplateString)"../../../docker-compose.integration.yml");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/?t=235)

然后我们使用 `Builder` 来定义组合服务。
这个配置包括移除孤立容器,以及通过检查 `AppUrl`(例如 `https://localhost:7780`)来等待应用变为健康状态。

```csharp
private readonly ICompositeService _dockerService = new Builder()
    .UseContainer()
    .UseCompose()
    .FromFile(DockerComposeFile)
    .RemoveOrphans()
    .WaitForHttp("test-app", AppUrl)
    .Build();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/?t=310)

#### 集成 Playwright 进行浏览器自动化

共享上下文的最后一个组件是 Playwright。
我们初始化一个 Playwright 实例并启动一个 Chromium 浏览器。
出于调试目的,我们可以禁用 headless 模式并启用 `SlowMo`,以实时观察测试的操作。

```csharp
public async Task InitializeAsync()
{
    GitHubApiServer.Start();
    GitHubApiServer.SetupUser(ValidGitHubUsername);
    _dockerService.Start();

    _playwright = await Playwright.CreateAsync();
    Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    {
        Headless = false,
        SlowMo = 1000
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/bootstraping-the-test-project-53953968/?t=400)

```text
  InitializeAsync()
    1. GitHubApiServer.Start()                  WireMock :9850
    2. GitHubApiServer.SetupUser(ValidGitHubUsername)
    3. _dockerService.Start()                   docker-compose.integration.yml
                                                WaitForHttp("test-app", AppUrl)
    4. Playwright.CreateAsync()
    5. _playwright.Chromium.LaunchAsync(...)    -> Browser
```

在 `DisposeAsync` 方法中,我们确保所有资源(浏览器、Playwright 实例、Docker 服务以及 WireMock 服务器)都被正确释放,以防止测试运行之间的资源泄漏。

## 6. Testing: Create a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/) · 14:33

### 总结

本课演示如何在 ASP.NET Core Blazor 应用中使用 Playwright,为资源创建流程实现端到端的 UI 集成测试。
内容涵盖通过 CSS 和 ID 选择器选取 DOM 元素、使用 Playwright 的自动化 API 与表单交互,以及使用 Bogus 生成测试数据。
本课还处理了关键的基础设施问题,例如配置 Docker Compose 健康检查以确保数据库在应用启动前可用,以及验证针对无效用户输入的 UI 层验证消息。

### 核心概念

*   **Playwright 自动化**:使用 `IPage` 进行导航、填写表单和点击元素。
*   **DOM 选择器**:使用 ID(`#id`)和 CSS 属性选择器(`input[id=fullname]`)定位元素。
*   **Bogus 集成**:为 UI 测试生成逼真的假数据。
*   **基础设施同步**:实现 Docker 健康检查,以防止因数据库启动缓慢而导致的测试失败。
*   **UI 验证测试**:断言 UI 中验证消息的存在及其内容。

### 课程笔记

要编写有效的 UI 测试,你必须先了解目标页面的结构。
借助浏览器开发者工具,你可以找出输入框和按钮的选择器。
例如,Blazor 应用中的一个标准表单组会为其输入元素使用特定的 ID。

```html
<div class="form-group">
    <!--!-->
    <label for="fullname">FullName</label>
    <!--!-->
    <input id="fullname" placeholder="FullName" class="form-control valid" _bl_f7804c62-beea-4e58-be20-adb679533f26>
</div>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/?t=40)

在把这些选择器写进 C# 测试代码之前,你可以先在浏览器控制台中用 `document.querySelector` 测试它们。

```javascript
document.querySelector('#fullname')
document.querySelector('input[fullname')
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/?t=70)

#### 测试类设置

UI 测试被组织到一个专用的测试类中,通常位于 `Pages` 文件夹内。
该类必须使用 `[Collection]` 特性来引用共享测试上下文,从而能够访问浏览器实例和应用 URL。

```csharp
using Xunit;

namespace Customers.WebApp.Tests.Integration.Pages;

[Collection("Test collection")]
public class AddCustomerTests
{
    private readonly SharedTestContext _testContext;
    private readonly Faker<Customer> _customerGenerator = new Faker<Customer>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, SharedTestContext.ValidGitHubUsername)
        .RuleFor(x => x.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth.Date));

    public AddCustomerTests(SharedTestContext testContext)
    {
        _testContext = testContext;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/?t=190)

#### 实现创建测试

主要的测试用例验证在提供有效数据时客户能被成功创建。
这包括导航到 `add-customer` 页面、填写表单,以及点击提交按钮。

```csharp
[Fact]
public async Task Create_CreateCustomer_WhenDataIsValid()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    await page.GotoAsync("add-customer");
    var customer = _customerGenerator.Generate();

    // Act
    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
    await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

    await page.ClickAsync("button[type=submit]");

    // Assert
    var linkElement = page.Locator("article>p>a").First;
    var link = await linkElement.GetAttributeAsync("href");
    await page.GotoAsync(link!);

    (await page.Locator("p[id=fullname-field]").InnerTextAsync()).Should().Be(customer.FullName);
    (await page.Locator("p[id=email-field]").InnerTextAsync()).Should().Be(customer.Email);
    (await page.Locator("p[id=github-username-field]").InnerTextAsync()).Should().Be(customer.GitHubUsername);
    (await page.Locator("p[id=dob-field]").InnerTextAsync()).Should().Be(customer.DateOfBirth.ToString("dd/MM/yyyy"));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/?t=640)

#### 基础设施同步

当针对容器化环境运行测试时,应用可能会在数据库准备好接受连接之前就尝试启动。
要解决这个问题,请更新 `docker-compose` 配置,为数据库加入 `healthcheck`,并为应用加入 `condition: service_healthy` 要求。

```yaml
test-app:
    # ... other config
    depends_on:
      test-db:
        condition: service_healthy

test-db:
    image: postgres:latest
    # ... other config
    healthcheck:
      test: [ "CMD-SHELL", "pg_isready" ]
      interval: 2s
      timeout: 5s
      retries: 10
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/?t=690)

#### 测试验证错误

集成测试还应验证 UI 能正确显示验证错误。
通过提供无效数据(例如格式错误的邮箱),你可以断言预期的错误消息出现在验证摘要中。

```csharp
[Fact]
public async Task Create_ShowsError_WhenEmailIsInvalid()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    await page.GotoAsync("add-customer");
    var customer = _customerGenerator.Generate();
    
    // Act
    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.FillAsync("input[id=email]", "notanemail");
    await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
    await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

    // Assert
    var element = page.Locator("li.validation-message").First;
    var text = await element.InnerTextAsync();
    text.Should().Be("Invalid email format");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-create-a-resource-53953969/?t=805)

## 7. Testing: Get a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-get-a-resource-53953971/) · 6:37

### 总结

本课演示如何使用 Playwright,为通过 Blazor Web UI 获取客户资源实现集成测试。
它涵盖两个主要场景:验证当记录存在时客户详情能被正确显示,以及确保在请求不存在的 ID 时显示相应的"未找到"消息。
本课还讨论了测试准备的策略,例如使用 UI 来创建前置数据与直接操作数据库之间的取舍,并强调了在断言 UI 状态时定位器(locator)精确性的重要性。

### 核心概念

* **基于 UI 的准备与基于数据库的准备**:虽然可以通过直接调用数据库来创建前置数据以简化复杂流程,但使用 UI 来创建数据能确保整个端到端流程都得到执行。
* **提取辅助方法**:把常见的 UI 交互(例如创建客户)提取为测试类中可复用的方法,以减少重复。
* **动态导航**:使用 Playwright 的 `GotoAsync` 访问动态生成的 URL,包括使用随机 GUID 来测试"未找到"场景。
* **文本断言**:使用 Playwright 定位器定位特定的 HTML 元素(例如 `<article>` 中的 `<p>` 标签),并将其内部文本与期望值进行验证。

### 课程笔记

要测试资源的获取,测试必须验证 UI 能正确显示一个已存在用户的数据。
虽然这一准备工作可以通过直接操作数据库来创建记录完成,但本课利用现有的 UI 创建流程来建立测试状态。

首先,创建测试类,并将其配置为使用共享测试上下文和集合,以确保各测试之间状态一致。

```csharp
using Bogus;
using Customers.WebApp.Models;
using FluentAssertions;
using Microsoft.Playwright;
using Xunit;

namespace Customers.WebApp.Tests.Integration.Pages;

[Collection("Test collection")]
public class GetCustomerTests
{
    private readonly SharedTestContext _testContext;
    private readonly Faker<Customer> _customerGenerator = new Faker<Customer>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, SharedTestContext.ValidGitHubUsername)
        .RuleFor(x => x.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth.Date));

    public GetCustomerTests(SharedTestContext testContext)
    {
        _testContext = testContext;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-get-a-resource-53953971/?t=280)

#### 测试成功获取

为了测试当客户存在时 UI 会返回该客户,测试遵循 Arrange-Act-Assert 模式,其中 "Arrange" 阶段包括通过 UI 创建一个客户。
这段逻辑被提取到一个辅助方法 `CreateCustomer` 中。

```csharp
[Fact]
public async Task Get_ReturnsCustomer_WhenCustomerExists()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);
    
    // Act
    var linkElement = page.Locator("article>p>a").First;
    var link = await linkElement.GetAttributeAsync("href");
    await page.GotoAsync(link!);
    
    // Assert
    (await page.Locator("p[id=fullname-field]").InnerTextAsync()).Should().Be(customer.FullName);
    (await page.Locator("p[id=email-field]").InnerTextAsync()).Should().Be(customer.Email);
    (await page.Locator("p[id=github-username-field]").InnerTextAsync()).Should().Be(customer.GitHubUsername);
    (await page.Locator("p[id=dob-field]").InnerTextAsync()).Should().Be(customer.DateOfBirth.ToString("dd/MM/yyyy"));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-get-a-resource-53953971/?t=205)

#### 测试不存在的资源

第二个测试用例确保应用能妥善处理对不存在 ID 的请求,显示 "No customer found with this id" 消息。
这个测试生成一个随机 GUID 来模拟缺失的记录。

```csharp
[Fact]
public async Task Get_ReturnsNoCustomer_WhenNoCustomerExists()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customerUrl = $"{SharedTestContext.AppUrl}/customer/{Guid.NewGuid()}";

    // Act
    await page.GotoAsync(customerUrl);

    // Assert
    (await page.Locator("p").First.InnerTextAsync())
        .Should().Be("No customer found with this id");
}

private async Task<Customer> CreateCustomer(IPage page)
{
    await page.GotoAsync("add-customer");
    var customer = _customerGenerator.Generate();

    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
    await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

    await page.ClickAsync("button[type=submit]");
    return customer;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-get-a-resource-53953971/?t=340)

在断言"未找到"状态时,要确保定位器足够具体,能够找到该消息。
在这个实现中,该消息位于导航后页面上遇到的第一个 `<p>` 标签内。

## 8. Your turn to practice

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/your-turn-to-practice-53953972/) · 0:48

本课提出一个实践挑战:使用 Playwright 完成针对 ASP.NET Core Web UI 的集成测试套件。
学员的任务是实现缺失的测试用例,包括获取所有客户、使用有效和无效数据更新客户详情,以及删除已有的客户记录,以确保应用前端能与后端服务和数据库正确交互。

### 核心概念

* 使用 Playwright 进行端到端测试
* 由 UI 驱动的数据验证
* 处理异步的浏览器事件和对话框
* 验证针对无效输入的 UI 反馈
* 自动化多步骤的用户工作流

### 课程笔记

这个练习的重点是扩展应用现有的测试套件。
目标是使用 Playwright 实现四个特定的集成测试,以验证客户管理系统的端到端功能。

#### 验证客户列表

`GetAll_ContainsCustomer_WhenCustomerExists` 测试确保应用在添加一条新记录后能正确填充客户列表。
该测试使用 Bogus 生成一个客户,通过 UI 执行创建,然后导航到列表页面以确认该记录存在。

```csharp
[Fact]
public async Task GetAll_ContainsCustomer_WhenCustomerExists()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);

    // Act
    await page.GotoAsync("/customers");

    var name = page.Locator("tbody>tr>td")
        .Filter(new LocatorFilterOptions { HasTextString = customer.FullName });

    // Assert
    (await name.First.InnerTextAsync()).Should().Be(customer.FullName);
}
```

#### 测试更新工作流

更新功能通过两个场景进行测试:成功的数据修改和验证错误处理。

`Update_UpdatesCustomer_WhenDataIsValid` 测试导航到某个已有客户的更新表单,修改全名,然后提交表单。
接着它验证更新后的信息能在该客户的详情页面上正确显示。

```csharp
[Fact]
public async Task Update_UpdatesCustomer_WhenDataIsValid()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);
    await page.GotoAsync($"update-customer/{customer.Id}");
    customer.FullName = "Another Name";

    // Act
    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.ClickAsync("button[type=submit]");

    // Assert
    var linkElement = page.Locator("article>p>a").First;
    var link = await linkElement.GetAttributeAsync("href");
    await page.GotoAsync(link!);
    
    (await page.Locator("p[id=fullname-field]").InnerTextAsync()).Should().Be(customer.FullName);
    (await page.Locator("p[id=email-field]").InnerTextAsync()).Should().Be(customer.Email);
    (await page.Locator("p[id=github-username-field]").InnerTextAsync()).Should().Be(customer.GitHubUsername);
    (await page.Locator("p[id=dob-field]").InnerTextAsync()).Should().Be(customer.DateOfBirth.ToString("dd/MM/yyyy"));
}
```

第二个更新测试 `Update_ShowsError_WhenEmailIsInvalid` 验证在提供无效邮箱格式时,UI 能正确显示验证错误。

```csharp
[Fact]
public async Task Update_ShowsError_WhenEmailIsInvalid()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);
    await page.GotoAsync($"update-customer/{customer.Id}");
    customer.Email = "invalidemail";
    
    // Act
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FocusAsync("input[id=fullname]");

    // Assert
    var element = page.Locator("li.validation-message").First;
    var text = await element.InnerTextAsync();
    text.Should().Be("Invalid email format");
}
```

#### 测试客户删除

`Delete_DeletesCustomer_WhenCustomerExists` 测试处理端到端的删除流程。
这包括通过订阅 `page.Dialog` 事件并调用 `AcceptAsync()` 来与浏览器原生的确认对话框交互。
测试最后验证该客户的详情页面现在显示"未找到"消息。

```csharp
[Fact]
public async Task Delete_DeletesCustomer_WhenCustomerExists()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);
    await page.GotoAsync($"customer/{customer.Id}");

    // Act
    page.Dialog += (_, dialog) => dialog.AcceptAsync();
    await page.ClickAsync("button.btn.btn-danger");

    // Assert
    await page.GotoAsync($"customer/{customer.Id}");
    (await page.Locator("p").First.InnerTextAsync())
        .Should().Be("No customer found with this id");
}
```

#### 测试准备辅助方法

为了保持测试整洁易读,`CreateCustomer` 辅助方法封装了通过 UI 添加新客户的多步骤过程。
它填写必填的表单字段,提交数据,并从结果 URL 中提取生成的客户 ID。

```csharp
private async Task<Customer> CreateCustomer(IPage page)
{
    await page.GotoAsync("add-customer");
    var customer = _customerGenerator.Generate();

    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
    await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

    await page.ClickAsync("button[type=submit]");
    
    var element = page.Locator("article>p>a").First;
    var link = await element!.GetAttributeAsync("href");
    var idInText = link!.Split('/').Last();
    customer.Id = Guid.Parse(idInText);
    return customer;
}
```

## 9. Testing: Getting all the resources

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-the-resources-53953974/) · 3:07

### 总结

本课演示如何通过测试 "Get All" 功能来验证 Web UI 能正确显示资源列表。
测试使用 Playwright 导航到客户索引页面,识别 HTML 表格结构,并利用定位器和过滤器确保一个先前创建的客户以正确的详情出现在列表中。
这个过程强调复用测试准备逻辑(例如共享上下文和基于 UI 的资源创建辅助方法),以保持集成测试整洁高效。

### 核心概念

- **UI 导航**:使用 `page.GotoAsync` 访问应用中的特定路由。
- **表格定位器**:使用 `tbody>tr>td` 之类的 CSS 选择器定位表格结构中的特定 HTML 元素。
- **定位器过滤**:使用带有 `HasTextString` 的 `LocatorFilterOptions`,根据动态数据筛选出特定的行或单元格。
- **断言 UI 状态**:使用 `InnerTextAsync` 和 FluentAssertions 验证浏览器中渲染的数据与期望的模型数据一致。

### 课程笔记

目标是为 Web UI 实现以下测试用例,特别聚焦于 `GetAll` 功能:

```markdown
# Write these test cases

## GetAll
- GetAll_ContainsCustomer_WhenCustomerExists

## Update
- Update_UpdateCustomer_WhenDataIsValid
- Update_ShowsError_WhenEmailIsInvalid

## Delete
- Delete_DeletesCustomer_WhenCustomerExists
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-the-resources-53953974/?t=10)

首先,创建一个名为 `GetAllCustomerTests` 的新测试类。
这个类必须与其他 UI 测试属于同一个 xUnit 集合,以确保它们通过 `SharedTestContext` 共享相同的底层测试状态和基础设施。

```csharp
namespace Customers.WebApp.Tests.Integration.Pages;

[Collection("Test collection")]
public class GetAllCustomerTests
{
    private readonly SharedTestContext _testContext;
    private readonly Faker<Customer> _customerGenerator = new Faker<Customer>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, SharedTestContext.ValidGitHubUsername)
        .RuleFor(x => x.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth.Date));

    public GetAllCustomerTests(SharedTestContext testContext)
    {
        _testContext = testContext;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-the-resources-53953974/?t=40)

要测试列表中包含某个客户,系统中必须先存在一个客户。
我们复用辅助方法 `CreateCustomer`,它会自动执行通过 `add-customer` 页面添加新客户的 UI 步骤。

```csharp
private async Task<Customer> CreateCustomer(IPage page)
{
    await page.GotoAsync("add-customer");
    var customer = _customerGenerator.Generate();

    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
    await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

    await page.ClickAsync("button[type=submit]");
    return customer;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-the-resources-53953974/?t=55)

测试实现遵循 Arrange-Act-Assert 模式。
在 Arrange 阶段,我们创建一个新的浏览器页面,并使用辅助方法创建一个客户。
在 Act 阶段,我们导航到 `/customers` 页面。
在 Assert 阶段,我们定位表格单元格,并对它们进行过滤,找到与该客户全名匹配的那一个。

```csharp
[Fact]
public async Task GetAll_ContainsCustomer_WhenCustomerExists()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);

    // Act
    await page.GotoAsync("/customers");

    var name = page.Locator("tbody>tr>td")
        .Filter(new LocatorFilterOptions { HasTextString = customer.FullName });

    // Assert
    (await name.First.InnerTextAsync()).Should().Be(customer.FullName);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-getting-all-the-resources-53953974/?t=160)

通过定位 `tbody>tr>td`,我们确保查看的正是表格主体中的数据单元格。
当表格包含多个条目时,`Filter` 方法在查找特定行方面非常有效,因为它允许我们搜索由测试数据生成的特定文本。

## 10. Testing: Update a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/) · 5:57

### 总结

本课演示如何使用 Playwright,为在 Web UI 中更新已有资源实现集成测试。
内容涵盖复用准备逻辑和数据生成、为维持测试状态而从 UI 中解析动态标识符的必要性,以及对成功更新和验证错误处理两方面的验证。
通过与表单元素交互并对最终的 UI 状态进行断言,这些测试确保了更新工作流的端到端完整性。

### 核心概念

- 在测试类之间复用测试准备和数据生成逻辑。
- 导航到带参数的路由(例如 `update-customer/{id}`)。
- 从 UI 元素(例如锚点标签)中提取资源 ID,以便后续测试步骤使用。
- 使用 `FillAsync` 和 `ClickAsync` 与表单输入交互。
- 使用 `FocusAsync` 触发 UI 验证状态。
- 对 UI 中更新后的数据进行断言,以确认持久化。

### 课程笔记

集成测试中的更新逻辑与创建逻辑有很多相似之处,主要区别在于导航以及资源的已有状态。
首先建立 `UpdateCustomerTests` 类,沿用添加客户时使用的同一套准备代码,包括 `SharedTestContext` 和一个 `Faker<Customer>` 生成器。

```csharp
[Collection("Test collection")]
public class UpdateCustomerTests
{
    private readonly SharedTestContext _testContext;
    private readonly Faker<Customer> _customerGenerator = new Faker<Customer>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, SharedTestContext.ValidGitHubUsername)
        .RuleFor(x => x.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth.Date));

    public UpdateCustomerTests(SharedTestContext testContext)
    {
        _testContext = testContext;
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/?t=40)

测试更新时的一个重大挑战是获取要修改的资源的 ID。
在这个实现中,`CreateCustomer` 辅助方法被修改为从 UI 中解析生成的 ID。
提交创建表单后,测试找到指向新客户详情的链接,从 `href` 属性中提取 ID,并将其赋给本地的 customer 对象。

```csharp
private async Task<Customer> CreateCustomer(IPage page)
{
    await page.GotoAsync("add-customer");
    var customer = _customerGenerator.Generate();

    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FillAsync("input[id=github-username]", customer.GitHubUsername);
    await page.FillAsync("input[id=dob]", customer.DateOfBirth.ToString("yyyy-MM-dd"));

    await page.ClickAsync("button[type=submit]");
    
    var element = page.Locator("article>p>a").First;
    var link = await element!.GetAttributeAsync("href");
    var idInText = link!.Split('/').Last();
    customer.Id = Guid.Parse(idInText);
    return customer;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/?t=175)

```text
  article>p>a  --GetAttributeAsync("href")-->  link
  link.Split('/').Last()                    -->  idInText
  Guid.Parse(idInText)                      -->  customer.Id
```

主要的测试用例 `Update_UpdatesCustomer_WhenDataIsValid` 遵循 Arrange-Act-Assert 模式。
它首先创建一个客户,然后使用解析出的 ID 导航到特定的更新页面。
"Act" 阶段包括用更新后的数据填写表单并提交。

```csharp
[Fact]
public async Task Update_UpdatesCustomer_WhenDataIsValid()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);
    await page.GotoAsync($"customer/{customer.Id}");
    customer.FullName = "Another Name";

    // Act
    await page.FillAsync("input[id=fullname]", customer.FullName);
    await page.ClickAsync("button[type=submit]");

    // Assert
    var linkElement = page.Locator("article>p>a").First;
    var link = await linkElement.GetAttributeAsync("href");
    await page.GotoAsync(link!);
    
    (await page.Locator("p[id=fullname-field]").InnerTextAsync()).Should().Be(customer.FullName);
    (await page.Locator("p[id=email-field]").InnerTextAsync()).Should().Be(customer.Email);
    (await page.Locator("p[id=github-username-field]").InnerTextAsync()).Should().Be(customer.GitHubUsername);
    (await page.Locator("p[id=dob-field]").InnerTextAsync()).Should().Be(customer.DateOfBirth.ToString("dd/MM/yyyy"));
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/?t=235)

在实现过程中,务必确保使用正确的路由。
例如,要进入编辑表单,必须导航到 `update-customer/{id}`,而不只是 `customer/{id}`。

```csharp
var customer = await CreateCustomer(page);
await page.GotoAsync($"update-customer/{customer.Id}");
customer.FullName = "Another Name";
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/?t=250)

最后,本课讲解了在更新过程中测试验证错误。
`Update_ShowsError_WhenEmailIsInvalid` 测试演示如何验证在输入无效邮箱时 UI 能正确显示验证消息。
通过在填写邮箱后对另一个字段调用 `FocusAsync`,测试无需提交表单就能触发客户端验证。

```csharp
[Fact]
public async Task Update_ShowsError_WhenEmailIsInvalid()
{
    // Arrange
    var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
    {
        BaseURL = SharedTestContext.AppUrl
    });
    var customer = await CreateCustomer(page);
    await page.GotoAsync($"update-customer/{customer.Id}");
    customer.Email = "invalidemail";
    
    // Act
    await page.FillAsync("input[id=email]", customer.Email);
    await page.FocusAsync("input[id=fullname]");

    // Assert
    var element = page.Locator("li.validation-message").First;
    var text = await element.InnerTextAsync();
    text.Should().Be("Invalid email format");
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-update-a-resource-53953975/?t=325)

## 11. Testing: Delete a resource

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/) · 3:46

### 总结

本课讲解如何使用 Playwright,为 ASP.NET Core Web UI 中的删除功能实现集成测试。
一个重点是处理浏览器原生的 JavaScript 确认对话框:订阅 Page.Dialog 事件,并以编程方式接受该提示。
本课还演示了使用特定的 CSS 类(例如 Bootstrap 的 .btn-danger)来定位 UI 元素并与之交互,随后通过断言资源详情页上出现"未找到"消息,来验证该资源已成功从系统中移除。

### 核心概念

*   通过 `Dialog` 事件在 Playwright 中处理 JavaScript `confirm` 对话框。
*   为执行操作的按钮使用 CSS 选择器(例如 `button.btn.btn-danger`)。
*   为资源删除实现完整的 Arrange-Act-Assert 周期。
*   通过导航到特定的资源 URL 并断言"未找到"状态,来验证资源已不存在。

### 课程笔记

测试类 `DeleteCustomerTests` 使用一个 `SharedTestContext` 和一个 Bogus `Faker` 进行初始化,后者用于为测试准备生成客户数据。

```csharp
private readonly SharedTestContext _testContext;
private readonly Faker<Customer> _customerGenerator = new Faker<Customer>()
    .RuleFor(x => x.Email, faker => faker.Person.Email)
    .RuleFor(x => x.FullName, faker => faker.Person.FullName)
    .RuleFor(x => x.GitHubUsername, SharedTestContext.ValidGitHubUsername)
    .RuleFor(x => x.DateOfBirth, faker => DateOnly.FromDateTime(faker.Person.DateOfBirth.Date));

public DeleteCustomerTests(SharedTestContext testContext)
{
    _testContext = testContext;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=25)

测试方法 `Delete_DeletesCustomer_WhenCustomerExists` 遵循标准的 Arrange-Act-Assert 模式。

```csharp
[Fact]
public async Task Delete_DeletesCustomer_WhenCustomerExists()
{
    // Arrange

    // Act

    // Assert

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=40)

在 Arrange 阶段,使用应用的基础 URL 创建一个新的浏览器页面。
使用辅助方法创建一个客户,然后浏览器导航到该客户的详情页面。

```csharp
// Arrange
var page = await _testContext.Browser.NewPageAsync(new BrowserNewPageOptions
{
    BaseURL = SharedTestContext.AppUrl
});
var customer = await CreateCustomer(page);
await page.GotoAsync($"customer/{customer.Id}");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=80)

在 Web UI 中测试删除时有一个独特的挑战,那就是处理浏览器的确认对话框。
在 Playwright 中,这是通过挂接 `page.Dialog` 事件来处理的。
通过指定一个调用 `dialog.AcceptAsync()` 的事件处理程序,测试会在确认提示出现时自动批准它。

```csharp
// Act
page.Dialog += (_, dialog) => dialog.AcceptAsync();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=100)

获取页面的 UI 逻辑被设计为在客户 ID 不存在时显示一条特定的消息。

```razor
@page "/customer"
@page "/customer/{CustomerId:guid}"

@if (CustomerId != Guid.Empty)
{
    if (_customer is null)
    {
        <p>No customer found with this id</p>
    }
    else
    {
        <h1>Customer details: @_customer.FullName</h1>
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=115)

为了触发删除,测试会点击删除按钮。
在这个应用中,该按钮通过 Bootstrap 类 `.btn-danger` 来识别。

```csharp
await page.ClickAsync("button.btn.btn-danger");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=130)

最后,断言通过尝试导航回该客户的页面并检查 "No customer found with this id" 消息,来验证删除结果。

```csharp
// Assert
await page.GotoAsync($"customer/{customer.Id}");
(await page.Locator("p").First.InnerTextAsync())
    .Should().Be("No customer found with this id");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=145)

```text
  Arrange: CreateCustomer(page) -> GotoAsync("customer/{customer.Id}")
  Act:     page.Dialog += dialog.AcceptAsync()
           ClickAsync("button.btn.btn-danger")  -> confirm 对话框被接受
  Assert:  GotoAsync("customer/{customer.Id}")
           Locator("p").First == "No customer found with this id"
```

出于调试或在开发期间进行可视化验证的目的,可以把 `SharedTestContext` 配置为以非 headless 模式运行,并带有轻微的延迟(`SlowMo`)。

```csharp
_playwright = await Playwright.CreateAsync();
Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    //Headless = false,
    SlowMo = 100
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/testing-delete-a-resource-53953976/?t=210)

## 12. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953978/) · 0:41

### 总结

本课对 ASP.NET Core 中 Web UI 的集成测试策略做了技术性总结。
它回顾了使用 WebApplicationFactory 进行基于 UI 的测试的局限性,并详细介绍了如何实现一个自定义的 Docker Compose 环境来托管应用及其依赖。
本章涵盖了为浏览器自动化配置 Playwright、使用共享测试上下文来优化资源使用,以及在 Blazor 应用中验证完整的 CRUD 功能。

### 核心概念

* **UI 测试与 API 测试**:理解为什么 `WebApplicationFactory` 并不直接适用于 UI 测试,原因在于需要一个外部的浏览器进程。
* **用于测试的 Docker Compose**:使用专用的 `docker-compose.integration.yml`,专门为测试套件编排应用及其数据库。
* **Playwright 集成**:为浏览器自动化配置 Playwright,包括 headless 模式和执行速度的选项。
* **共享测试上下文**:实现 `IAsyncLifetime` 和 xUnit 集合夹具(collection fixture),来管理 Docker 容器和浏览器实例等昂贵资源的生命周期。
* **CRUD 测试**:在 Blazor 应用中验证创建、读取、更新和删除操作。

### 课程笔记

对 Web UI 进行集成测试需要一种不同于测试标准 API 的方法。
虽然 `WebApplicationFactory` 对于内存中的 API 测试很有效,但 Playwright 之类的 UI 自动化工具要求应用托管在一个可通过网络访问的端口上。

为了支持这一点,我们实现了一个自定义的 Docker 策略。
通过为集成测试创建一个专门的 Docker Compose 文件,环境可以被自动启动和拆除。
这确保了测试针对的是一个一致的、隔离的应用实例及其底层 PostgreSQL 数据库。
`SharedTestContext` 类被开发出来以高效地管理这些资源,负责初始化模拟的 GitHub API 服务器、Docker 服务以及 Playwright 浏览器实例。

```csharp
public async Task InitializeAsync()
{
    GitHubApiServer.Start();
    GitHubApiServer.SetupUser(ValidGitHubUsername);
    _dockerService.Start();

    _playwright = await Playwright.CreateAsync();
    Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    {
        //Headless = false,
        SlowMo = 100
    });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953978/?t=10)

通过使用 xUnit 的集合夹具共享这个上下文,多个测试类可以针对同一个环境顺序运行,从而减少为每个单独测试启动容器的开销。
这套基础设施使我们得以实现完整的 CRUD 测试,确保用户能够通过 Blazor UI 被正确地创建、获取、更新和删除。

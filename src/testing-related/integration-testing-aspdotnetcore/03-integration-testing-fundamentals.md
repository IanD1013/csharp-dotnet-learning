# Integration testing fundamentals

> 课程:[From Zero to Hero: Integration testing in ASP.NET Core](https://dometrain.com/course/from-zero-to-hero-integration-testing-in-asp-net-core/) · 第 3 章
> 共 13 课 · 约 38 分钟
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The project we will be working with](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/) | 6:40 | [↓](#1-the-project-we-will-be-working-with) |
| 2 | [The 5 core integration testing steps](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-5-core-integration-testing-steps-53953801/) | 2:59 | [↓](#2-the-5-core-integration-testing-steps) |
| 3 | [Project separation](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/) | 2:39 | [↓](#3-project-separation) |
| 4 | [Why xUnit?](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-xunit-53953803/) | 1:16 | [↓](#4-why-xunit) |
| 5 | [Writing your first integration test](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/) | 4:51 | [↓](#5-writing-your-first-integration-test) |
| 6 | [Naming in integration tests](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/naming-in-integration-tests-53953805/) | 2:31 | [↓](#6-naming-in-integration-tests) |
| 7 | [Arrange, Act, Assert](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/arrange-act-assert-53953806/) | 1:48 | [↓](#7-arrange-act-assert) |
| 8 | [xUnit’s test execution model](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/xunits-test-execution-model-53953807/) | 3:27 | [↓](#8-xunits-test-execution-model) |
| 9 | [Test setup](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-setup-53953809/) | 2:04 | [↓](#9-test-setup) |
| 10 | [Test cleanup](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-cleanup-53953810/) | 1:53 | [↓](#10-test-cleanup) |
| 11 | [Parameterizing tests](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/) | 5:26 | [↓](#11-parameterizing-tests) |
| 12 | [Ignoring tests](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/ignoring-tests-53953812/) | 1:18 | [↓](#12-ignoring-tests) |
| 13 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953814/) | 0:46 | [↓](#13-section-recap) |

## 1. The project we will be working with

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/) · 6:40

### 总结

本课介绍 Customers API,这是一个完整的 RESTful ASP.NET Core 应用,在整个课程中作为集成测试的主要对象。
该 API 提供用于客户管理的 CRUD 操作,并包含真实世界中的复杂性,例如与 GitHub API 的外部服务集成、使用 PostgreSQL 和 Dapper 的数据库持久化,以及通过值对象和 FluentValidation 强制执行的领域逻辑。
本课强调测试 API 的外部行为(包括状态码、响应头和内容类型),而不是内部方法逻辑,并说明了如何搭建所需的本地环境,包括 SSL 证书和数据库连接。

### 核心概念

*   **以 API 为中心的集成测试**:关注与端点交互时 API 可观察到的行为(状态码、响应头、负载),而不是对单个类的方法做单元测试。
*   **领域驱动设计(DDD)元素**:该项目使用值对象(例如 `CustomerId`、`GitHubUsername`、`Email`)把验证和业务规则封装在领域层中。
*   **外部服务集成**:该 API 会针对外部系统验证数据,具体来说是通过 GitHub API 检查某个用户是否存在。
*   **持久化层**:使用 PostgreSQL 和 Dapper 进行数据访问,因此完整的集成测试需要真实的数据库连接。
*   **环境搭建**:需要一个本地 HTTPS 证书(`.pfx`),以确保应用在开发和测试环境中能通过 SSL 正确运行。

### 课程笔记

Customers API 是一个标准的 ASP.NET Core Web API,用于管理客户记录。
在开发环境中运行该应用时,它会初始化其托管环境和内容根目录,如启动日志所示:

```plaintext
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: I:\lab\courses\integration-testing-final\Unedited API\Customers.Api\
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=85)

#### API 功能与行为

该 API 支持完整的 CRUD 操作。
集成测试关注的是这些端点的契约和行为。
例如,创建客户时需要发送一个 JSON 负载,并收到带有生成的唯一标识符的已创建对象。

```json
{
    "fullName": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "nickchapsas",
    "dateOfBirth": "1993-04-20"
}

{
    "id": "e6b4abf8-b6db-45c5-a508-1645b20a1b20",
    "gitHubUsername": "nickchapsas",
    "fullName": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "dateOfBirth": "1993-04-20T00:00:00"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=115)

如果请求的资源不存在,API 会返回一个带有 problem details 负载的标准 `404 Not Found` 响应:

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-307992bf2c633aa4738f93dd97bf0d3c-6ad7be4e5d419b87-00"
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=145)

#### 验证逻辑

该 API 实现了多层验证。
它与 GitHub API 集成,以确保提供的 `gitHubUsername` 确实存在。
如果用户名无效,API 会返回 `400 Bad Request` 以及一条描述性的错误消息:

```json
{
    "fullName": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "h934y3485t43",
    "dateOfBirth": "1993-04-20"
}

    "errors": {
        "Customer": [
            "There is no GitHub user with username h934y3485t43"
        ]
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=180)

此外,领域层面的验证会阻止无效的数据状态,例如把出生日期设为未来的日期:

```json
{
    "fullName": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "Elfocrash",
    "dateOfBirth": "2023-04-20"
}

    "errors": {
        "DateOfBirth": [
            "Your date of birth cannot be in the future"
        ]
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=190)

#### 应用架构

该项目采用标准的 N 层架构,并侧重于领域驱动设计。

##### 控制器

`CustomerController` 负责处理 HTTP 请求,把请求映射为领域对象并调用相应的服务。

```csharp
    [HttpGet("customers")]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        var customersResponse = customers.ToCustomersResponse();
        return Ok(customersResponse);
    }
    
    [HttpPut("customers/{id:guid}")]
    public async Task<IActionResult> Update(
        [FromMultiSource] UpdateCustomerRequest request)
    {
        var existingCustomer = await _customerService.GetAsync(request.Id);

        if (existingCustomer is null)
        {            return NotFound();
        }

        var customer = request.ToCustomer();
        await _customerService.UpdateAsync(customer);

        var customerResponse = customer.ToCustomerResponse();
        return Ok(customerResponse);
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=240)

##### 领域模型

领域层使用值对象来确保数据完整性。
`Customer` 实体由这些专门的类型组成。

```csharp
using Customers.Api.Domain.Common;

namespace Customers.Api.Domain;

public class Customer
{
    public CustomerId Id { get; init; } = CustomerId.From(Guid.NewGuid());

    public GitHubUsername GitHubUsername { get; init; } = default!;

    public FullName FullName { get; init; } = default!;

    public Email Email { get; init; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=250)

##### 服务与仓储

`GitHubService` 使用 `IHttpClientFactory` 与 GitHub API 通信,而 `CustomerRepository` 使用 Dapper 与 PostgreSQL 数据库交互。

```csharp
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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=265)

```csharp
public async Task<bool> CreateAsync(CustomerDto customer)
{
    using var connection = await _connectionFactory.CreateConnectionAsync();
    var result = await connection.ExecuteAsync(
        @"INSERT INTO Customers (Id, GitHubUsername, FullName, Email, DateOfBirth) 
        VALUES (@Id, @GitHubUsername, @FullName, @Email, @DateOfBirth)",
        customer);
    return result > 0;
}

public async Task<CustomerDto?> GetAsync(Guid id)
{
    using var connection = await _connectionFactory.CreateConnectionAsync();
    return await connection.QuerySingleOrDefaultAsync<CustomerDto>(
        "SELECT * FROM Customers WHERE Id = @Id LIMIT 1", new { Id = id });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-project-we-will-be-working-with-53953800/?t=395)

```text
HTTP request
     |
     v
CustomerController
     |
     v
CustomerService
     |                         |
     v                         v
GitHubService             CustomerRepository
(IHttpClientFactory)      (Dapper)
     |                         |
     v                         v
GitHub API                PostgreSQL
```

#### 环境搭建

要在本地运行该项目,必须能访问一个 PostgreSQL 数据库(通常通过 Docker)。
此外,为了让 HTTPS 在某些测试场景中正常工作,你必须生成一个本地 PFX 证书。
在项目根目录运行以下命令:

`dotnet dev-certs https -ep cert.pfx -p test1234!`

这会创建一个密码为 `test1234!` 的 `cert.pfx` 文件,应用的配置会引用它。

## 2. The 5 core integration testing steps

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-5-core-integration-testing-steps-53953801/) · 2:59

### 总结

ASP.NET Core 中的集成测试遵循一个结构化的五步生命周期:Setup(准备)、Dependency Mocking(依赖模拟)、Execution(执行)、Assertion(断言)和 Cleanup(清理)。
这个过程确保被测系统(system-under-test)得到正确配置,外部依赖通过网络层面的模拟加以控制,并在每个测试之后恢复环境,以防止测试之间相互污染。

### 核心概念

- **Setup**:基础设施准备,包括创建数据库、编排 Docker 容器以及填充种子数据。
- **Dependency Mocking**:用可通过网络访问的伪造实现替换外部的、不可控的 API,以避免速率限制和外部故障。
- **Execution**:使用 xUnit 等框架运行测试套件,以触发被测系统。
- **Assertion**:验证系统返回了正确的数据,或达到了预期的副作用。
- **Cleanup**:必不可少的测试后过程,用于删除数据库中的残留数据,并为后续测试确保干净的状态。

### 课程笔记

集成测试涉及特定的操作顺序,以确保结果可靠且可重复。
在实现测试代码时,通常会按以下顺序遇到这些步骤:

```markdown
# The 5 integration testing steps

1. Setup
2. Dependency Mocking (API)
3. Execution
4. Assertion
5. Cleanup
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-5-core-integration-testing-steps-53953801/?t=10)

#### 1. Setup

准备阶段涉及为测试准备所需的基础设施。
这通常包括创建数据库或确保其存在、启动 Docker 容器,以及填充测试场景所需的初始数据。
这些操作确保被测系统在实际测试逻辑开始之前拥有一致的环境。

#### 2. Dependency Mocking (API)

虽然从技术上讲依赖模拟属于准备阶段,但它被当作一个显式的步骤,因为它涉及模拟开发者无法控制的外部依赖。
与驻留在内存中的单元测试 mock 不同,集成测试中针对 API 的 mock 应当是可通过网络访问的伪造实现。
这种方式让测试能够走真实的网络栈,同时避免调用生产 API 的风险,例如消耗速率限制或遭遇外部宕机。
目标是拥有一个与真实 API 表现和工作方式完全相同的伪造版本。

#### 3. Execution

执行阶段是实际执行测试的地方。
在工具和框架方面,这一步与单元测试完全相同。
在本课程中,xUnit 是首选框架,不过也可以使用 NUnit 或 MS Test 等其他框架。
执行会触发 ASP.NET Core 应用中被测试的具体功能。

#### 4. Assertion

断言是验证执行结果的过程。
它确保系统返回的数据是正确的,并且该操作对系统产生了恰当的影响。
这可能包括检查 HTTP 响应码、验证 JSON 负载,或核实数据库中的变化。

#### 5. Cleanup

清理是维护测试套件完整性的关键步骤。
尤其是在使用真实数据库这类持久化存储时,删除先前执行留下的任何残留数据至关重要。
确保每个测试之后都清理数据,可以防止测试之间相互污染,并确保每个测试都在可预测的、隔离的环境中运行。

## 3. Project separation

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/) · 2:39

### 总结

本课为一个同时包含应用代码和集成测试的解决方案建立基础结构。
它详细说明了如何使用基于目录的方式在物理上把源代码与测试代码分开,并把这些目录映射为 IDE 中的解决方案文件夹。
此外,它还定义了一套清晰的测试项目命名约定,用来区分单元测试和集成测试等不同的测试类型,以确保长期的可维护性和可发现性。

### 核心概念

* 使用 `src` 和 `tests` 文件夹进行物理目录组织。
* 把物理目录映射为 IDE 的解决方案文件夹(Solution Folders)。
* 项目命名约定:`[TargetProject].Tests.[TestType]`。
* 在 .NET 6 中初始化一个 xUnit 集成测试项目。
* 清理项目模板生成的样板文件,为实现测试做准备。

### 课程笔记

为了保持整洁且专业的架构,解决方案应当在应用代码和测试项目之间建立清晰的物理和逻辑分离。
做法是在项目根目录创建两个主要目录:`src` 用于存放功能性的应用项目,`tests` 用于存放所有测试项目。
这种物理分离确保根目录保持整洁,并为代码库提供可预测的结构。

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/?t=33)

在 IDE 中,这些物理目录通过解决方案文件夹来镜像。
必须先在文件系统上创建物理目录,再把它们作为解决方案文件夹添加到 IDE 中,以确保逻辑上的解决方案结构能正确对应磁盘上的物理布局。
文件夹映射完成后,现有的 API 项目被移动到 `src` 目录中,并作为现有项目重新添加到解决方案中。

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/?t=65)

集成测试项目被创建为一个面向 .NET 6 的新 xUnit 测试项目。
这里采用了一个特定的命名约定:`Customers.Api.Tests.Integration`。
这种层级式命名 `[TargetProject].Tests.[TestType]` 对清晰性至关重要,尤其是在包含多种测试方法的解决方案中。
它让开发者能够立刻区分单元测试和集成测试。

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/?t=120)

```text
<project root>
+-- src
|   +-- Customers.Api
+-- tests
    +-- Customers.Api.Tests.Integration
```

集成测试项目的初始配置定义在它的项目文件中,其中包含标准的 xUnit 和测试 SDK 依赖:

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>

        <IsPackable>false</IsPackable>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.1.0" />
        <PackageReference Include="xunit" Version="2.4.1" />
        <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="coverlet.collector" Version="3.1.2">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
    </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/?t=106)

最后,删除 IDE 模板生成的默认样板类文件。
这确保项目保持干净,为后续课程中实现具体的集成测试类做好准备。

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/project-separation-53953802/?t=150)

## 4. Why xUnit?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-xunit-53953803/) · 1:16

本课程选择 xUnit 作为测试框架,因为它是行业标准,也是 Microsoft 所使用的框架。
它便于高效地发现和执行测试,同时为测试生命周期管理(例如准备和清理)提供了简洁的抽象。
它与集成测试模式的兼容性,尤其是在共享上下文方面,使它成为测试 ASP.NET Core 应用的理想选择。

### 核心概念

* xUnit 作为测试发现与执行的主要引擎。
* 行业的采用情况,以及 Microsoft 内部对该框架的使用。
* 简化测试准备和清理逻辑的实现。
* 对复杂集成测试场景中共享上下文的原生支持。
* 可以灵活地使用 NUnit 等替代框架,前提是开发者理解生命周期和上下文管理。

### 课程笔记

集成测试项目使用 xUnit 作为测试发现与执行的主要框架进行配置。
项目文件中包含了支持 xUnit 运行器以及与 .NET Test SDK 集成所需的 NuGet 包。

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>

        <IsPackable>false</IsPackable>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.1.0" />
        <PackageReference Include="xunit" Version="2.4.1" />
        <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="coverlet.collector" Version="3.1.2">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
    </ItemGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-xunit-53953803/?t=10)

选择 xUnit 有几个技术上和职业上的原因:

1. **行业标准**:它是 .NET 生态系统中最流行的测试框架,Microsoft 内部的项目也在使用它。
2. **生命周期管理**:它为处理测试准备和清理(初始化和拆除)提供了简洁直观的方式。
3. **集成测试支持**:它能无缝契合集成测试的需求,特别是在多个测试用例之间处理共享上下文时。

虽然 xUnit 是本课程推荐的工具,但如果你更喜欢,也可以使用 NUnit 等其他框架。
选择替代框架的开发者必须确保自己理解如何在那些特定环境中实现共享上下文和管理测试生命周期(启动和清理)的等效功能。

## 5. Writing your first integration test

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/) · 4:51

### 总结

本课通过手动配置一个 HttpClient 与正在运行的 ASP.NET Core API 交互,演示集成测试的基本机制。
本课一开始绕开 WebApplicationFactory 这类专门的工具,以此说明测试项目如何作为一个真实的消费者,经过网络栈并访问底层数据库来验证端到端行为,例如在请求不存在的资源时收到 404 Not Found 状态码。

### 核心概念

- **手动运行 API**:在执行测试之前先运行 API 项目(例如通过 `dotnet run`),以确保网络上存在一个目标。
- **HttpClient 配置**:使用带有已定义 `BaseAddress` 的 `HttpClient` 来模拟对 API 的外部请求。
- **端到端验证**:测试整个技术栈,包括控制器、服务层和实际的数据库连接。
- **xUnit 断言**:使用 `Assert.Equal` 来验证 API 返回了预期的 `HttpStatusCode`。
- **跨进程调试**:在运行测试的同时在 API 代码中设置断点,以观察数据在系统中的流动。

### 课程笔记

要开始编写集成测试,首先要在集成测试项目中创建一个测试类。
与把代码同外部依赖隔离开的单元测试不同,集成测试要求环境处于运行状态。

```csharp
namespace Customers.Api.Tests.Integration;

public class IntegrationTest
{

}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=25)

在执行测试之前,API 必须正在运行。
可以在 API 项目目录中执行 `dotnet run` 来做到这一点。
在这个环境中,API 被配置为运行在 `https://localhost:5001` 上。
虽然存在 `WebApplicationFactory` 这类专门的工具来自动完成这件事,但从手动方式开始有助于理解测试客户端如何与服务器交互的核心概念。

接下来,定义一个异步测试方法,并用 xUnit 的 `[Fact]` 特性修饰它。

```csharp
using Xunit;

namespace Customers.Api.Tests.Integration;

public class IntegrationTest
{
    [Fact]
    public async Task Test()
    {

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=65)

为了与 API 通信,初始化一个 `HttpClient`,并把它的 `BaseAddress` 设为 API 所在的 URL。

```csharp
using Xunit;

namespace Customers.Api.Tests.Integration;

public class IntegrationTest
{
    [Fact]
    public async Task Test()
    {
        var httpClient = new HttpClient
        {            BaseAddress = new Uri("https://localhost:5001")
        };

    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=100)

第一个测试的目标是用一个数据库中不存在的随机 ID 调用 "Get Customer by ID" 端点,并期望得到 `404 Not Found` 响应。

```csharp
public class IntegrationTest
{
    [Fact]
    public async Task Test()
    {
        var httpClient = new HttpClient
        {            BaseAddress = new Uri("https://localhost:5001")
        };

        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=145)

最后,添加一个断言来验证响应的状态码。
这确认了 API 能正确处理对不存在资源的请求。

```csharp
[Fact]
public async Task Test()
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:5001")
    };

    var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=180)

当这个测试运行时,它会命中正在运行的 API 中的 `CustomerController`。
通过调试该测试,你可以观察到执行流程进入控制器的 `Get` 方法。

```csharp
[HttpGet("customers/{id:guid}")]
public async Task<IActionResult> Get([FromRoute] Guid id)
{
    var customer = await _customerService.GetAsync(id);

    if (customer is null)
    {
        return NotFound();
    }

    var customerResponse = customer.ToCustomerResponse();
    return Ok(customerResponse);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=245)

因为这是一个集成测试,服务层会继续使用真实的数据库连接来查询本地数据库。
如果记录不存在,仓储返回 null,控制器返回由测试验证的 `NotFound` 结果。

```csharp
public async Task<CustomerDto?> GetAsync(Guid id)
{
    using var connection = await _connectionFactory.CreateConnectionAsync();
    return await connection.QuerySingleOrDefaultAsync<CustomerDto>(
        "SELECT * FROM Customers WHERE Id = @Id LIMIT 1", new { Id = id });
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/writing-your-first-integration-test-53953804/?t=255)

```text
Test project                       Running API (dotnet run)
+---------------------+            +------------------------------+
| [Fact] Test()       |            | CustomerController.Get(id)   |
|   HttpClient        |  GET       |   _customerService.GetAsync  |
|   BaseAddress =     |----------->|     repository GetAsync      |
|   localhost:5001    | customers/ |       database -> null       |
|                     | {Guid}     |   return NotFound()          |
|   Assert.Equal(     |<-----------|                              |
|     NotFound, ...)  |    404     |                              |
+---------------------+            +------------------------------+
```

## 6. Naming in integration tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/naming-in-integration-tests-53953805/) · 2:31

### 总结

集成测试中有效的命名约定能让测试自带文档,从而提升可维护性和可发现性。
通过以测试类所针对的控制器或端点来命名测试类,并为测试方法使用结构化的命名模式(指明操作、预期结果和场景),开发者无需查看实现细节,一眼就能理解测试的意图和失败原因。

### 核心概念

*   **基于控制器的类命名**:以测试类所针对的具体控制器或端点组来命名测试类(例如 `CustomerControllerTests`)。
*   **结构化方法命名**:使用 `[Action]_[ExpectedResult]_[Scenario]` 模式,为测试的目的提供即时的上下文。
*   **自带文档的测试**:组织测试,使其意图在测试资源管理器或日志中就一目了然,而无需阅读源代码。
*   **可扩展性**:识别出单个测试类何时变得过大,需要按操作或端点进一步拆分。

### 课程笔记

最初的集成测试常常使用通用的命名方式,对于测试的内容或失败的原因几乎不提供任何上下文。
通用的类名和方法名要求开发者查看代码才能理解测试的范围。

```csharp
using System.Net;
using Xunit;

namespace Customers.Api.Tests.Integration;

public class IntegrationTest
{
    [Fact]
    public async Task Test()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/naming-in-integration-tests-53953805/?t=10)

为了提升可发现性,测试类应当以它所测试的入口点来命名。
对于使用控制器的 Web API,这通常意味着以控制器来命名该类。
如果使用 Minimal API,该类应当以端点组来命名(例如 `CustomerEndpointsTests`)。
这让开发者一眼就能看出系统的哪个部分正在被测试。

```csharp
using System.Net;
using Xunit;

namespace Customers.Api.Tests.Integration;

public class CustomerControllerTests
{
    [Fact]
    public async Task Test()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/naming-in-integration-tests-53953805/?t=55)

测试方法本身应当遵循一种结构化的命名约定,描述操作、预期结果以及所测试的具体场景。
一种常见的模式是 `MethodName_ExpectedResult_Scenario`。
例如,在测试一个当客户不存在时应返回 `404 Not Found` 状态码的 `GET` 请求时,名称应当体现这三个组成部分。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/naming-in-integration-tests-53953805/?t=130)

```text
Get        _ ReturnsNotFound  _ WhenCustomerDoesNotExist
MethodName   ExpectedResult     Scenario
```

这种方式造就了自带文档的测试。
如果某个测试在 CI/CD 流水线中失败,`Get_ReturnsNotFound_WhenCustomerDoesNotExist` 这个名称会立即传达测试的意图和失败的性质。
虽然把一个控制器的所有测试放在一个类里是不错的起点,但随着测试套件的增长和准备逻辑变得更复杂,这些类可以进一步拆分成更小、更聚焦的文件。

## 7. Arrange, Act, Assert

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/arrange-act-assert-53953806/) · 1:48

### 总结

Arrange、Act、Assert(AAA)模式是组织测试逻辑的一种基本结构化方法。
虽然它通常与单元测试联系在一起,但它在集成测试中同样有价值,能确保清晰性和可维护性。
通过显式地分离依赖的准备(Arrange)、被测操作的执行(Act)以及结果的验证(Assert),开发者能写出对各个资历层级的团队成员来说都更易读、更易调试的测试。

### 核心概念

*   **Arrange**:搭建环境,例如初始化 `HttpClient` 并准备测试所需的数据。
*   **Act**:执行被测试的具体操作或 API 调用。
*   **Assert**:验证操作的结果符合预期的行为和状态码。
*   **Cleanup**:一个可选阶段,用于执行拆除任务,例如从数据库中删除测试数据以保持干净的状态。
*   **可读性**:使用显式注释来划分各部分,能让各种经验水平的开发者都更容易理解代码。

### 课程笔记

一致的结构能让集成测试受益良多。
典型的测试方法体可以划分为三个不同的功能区域:准备环境、对系统执行操作,以及对结果进行断言。

在下面的示例中,测试方法初始化一个指向本地开发服务器的 `HttpClient`,针对一个不存在的客户 ID 执行 GET 请求,并验证状态码为 `404 Not Found`。

```csharp
public class CustomerControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/arrange-act-assert-53953806/?t=10)

#### AAA 模式

Arrange、Act、Assert(AAA)模式通过代码注释让这些步骤变得显式。
推荐这种结构,是因为它能保证每个代码块的意图对任何接触代码库的开发者来说都是清晰的。

1.  **Arrange**:初始化 `HttpClient` 以及任何必要的状态。
2.  **Act**:执行具体的请求调用。
3.  **Assert**:根据预期值验证响应。

```csharp
public class CustomerControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        // Act
        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/arrange-act-assert-53953806/?t=55)

#### Cleanup 阶段

在某些集成测试场景中(例如测试创建或修改了数据时),可能需要一个称为 **Cleanup** 的第四阶段。
这个阶段确保系统恢复到原来的状态,例如删除在测试执行期间创建的用户记录。
虽然清理的实现方式可能各不相同,但它的目的是保持测试隔离和环境稳定。

```csharp
public class CustomerControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        // Act
        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        // Cleanup
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/arrange-act-assert-53953806/?t=85)

建立这种结构是理解更高级测试主题的前提,包括 xUnit 执行模型如何影响集成测试的性能和资源管理。

## 8. xUnit’s test execution model

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/xunits-test-execution-model-53953807/) · 3:27

### 总结

xUnit 的执行模型决定了测试如何被隔离和并行化。
默认情况下,xUnit 按顺序运行同一个类中的测试,而不同类中的测试则并行执行。
此外,xUnit 会为每个测试方法实例化一次测试类,从而确保每次执行都有全新的状态。
理解这些行为,对于管理 HttpClient 这样的共享资源以及在集成测试中防止数据冲突至关重要。

### 核心概念

* 单个类内部按顺序执行。
* 不同测试类之间并行执行。
* 每个测试方法实例化一次类(默认行为)。
* 通过私有字段实现状态隔离和资源共享。

### 课程笔记

xUnit 采用一种特定的执行模型,决定了测试如何运行以及状态如何管理。
一条基本规则是,定义在同一个类中的测试默认按顺序执行。
可以从一个单独的测试类开始演示这一点:

```csharp
public class CustomerControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        // Act
        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/xunits-test-execution-model-53953807/?t=10)

如果向这个类中添加第二个带延迟的测试,顺序执行的特性就会显现出来。

```csharp
public class CustomerControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        // Act
        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        // Act
        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        await Task.Delay(3000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/xunits-test-execution-model-53953807/?t=25)

执行这些测试时,第一个测试运行完毕后,第二个测试才开始。
当测试在单个类的上下文中共享状态或操作相同的数据时,这种顺序执行很重要。

然而,如果把测试移到不同的类中,xUnit 就会改变其行为,并行运行它们。

```csharp
namespace Customers.Api.Tests.Integration;

public class CustomerControllerTests2
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2()
    {
        // Arrange
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001")
        };

        // Act
        var response = await httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        await Task.Delay(3000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/xunits-test-execution-model-53953807/?t=130)

位于不同类中的测试默认并行运行。
这种并行化提升了性能,但要求开发者确保数据在并行执行之间不会发生冲突。

```text
Same class (CustomerControllerTests)    Separate classes
                                        CustomerControllerTests
time -->                                  [ ...WhenCustomerDoesNotExist ]
[ ...DoesNotExist ][ ...DoesNotExist2 ] CustomerControllerTests2
     sequential                           [ ...WhenCustomerDoesNotExist2 ]
                                              parallel
```

xUnit 模型的另一个关键方面是,每执行一个测试方法,测试类就会被实例化一次。
这意味着在构造函数中或通过字段初始化完成的任何准备工作,对每个测试来说都是全新的。
这样就可以把 HttpClient 这类共享资源提取到私有字段中,让代码更简洁。

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

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2()
    {
        // Act
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/xunits-test-execution-model-53953807/?t=195)

把 HttpClient 移到私有字段之后,测试方法变得更简洁,只需关注 Act 和 Assert 阶段。
由于 xUnit 为每个测试创建一个新的类实例,每个测试仍然会得到它自己的 HttpClient 实例。

## 9. Test setup

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-setup-53953809/) · 2:04

### 总结

准备和清理是集成测试的基础,它们确保测试运行前处于已知状态,并在之后清理资源。
在 xUnit 中,执行准备工作最直接的方式是通过测试类的构造函数。
本课详细介绍如何使用构造函数进行同步初始化,以及如何使用 `IAsyncLifetime` 接口实现异步的准备和拆除逻辑。

### 核心概念

* 使用类的构造函数进行同步准备。
* 通过 `IAsyncLifetime` 接口进行异步准备和清理。
* 执行顺序:构造函数,然后是 `InitializeAsync`,然后是测试方法。
* xUnit 中每个测试实例化一次的行为。

### 课程笔记

在 xUnit 中,构造函数是放置准备代码的默认位置。
因为 xUnit 为每个测试方法创建一个新的测试类实例,所以每执行一个测试,构造函数都会在类实例化期间被触发。

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

    public CustomerControllerTests()
    {
        // Setup code here
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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-setup-53953809/?t=25)

对于需要异步初始化的场景(例如等待一个任务或为数据库填充种子数据),xUnit 提供了 `IAsyncLifetime` 接口。
实现这个接口会添加两个方法:`InitializeAsync` 和 `DisposeAsync`。

```csharp
public async Task InitializeAsync()
{
    // Example of async setup code, such as a delay or database seeding
    await Task.Delay(1000);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-setup-53953809/?t=85)

当构造函数和 `InitializeAsync` 同时存在时,执行顺序遵循特定的次序:首先调用构造函数进行同步初始化,然后调用 `InitializeAsync` 方法。
这些完成之后,测试方法本身才会执行。
测试结束后,清理工作由 `DisposeAsync` 方法处理。

```csharp
public Task DisposeAsync()
{
    // Cleanup code here
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-setup-53953809/?t=115)

```text
per test method:

CustomerControllerTests() --> InitializeAsync() --> [Fact] --> DisposeAsync()
     (sync setup)              (async setup)                    (cleanup)
```

这个生命周期确保每个测试都在一个干净、隔离的环境中运行,无论初始化逻辑是同步的还是异步的。

## 10. Test cleanup

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-cleanup-53953810/) · 1:53

### 总结

测试清理是集成测试的一个关键方面,它确保删除测试期间创建的数据,为后续的执行保持干净的状态。
在 xUnit 中,这主要通过实现 IDisposable 接口(用于同步清理)或 IAsyncLifetime 接口(用于异步操作)来实现。
因为 xUnit 为每个测试方法实例化一个新的测试类实例,这些清理例程会在每个单独的测试之后执行,从而在测试用例之间提供可靠的隔离。

### 核心概念

- **测试隔离**:xUnit 为每次测试执行创建一个新的测试类实例,确保测试默认不共享状态。
- **IDisposable**:标准的 .NET 接口,用于在 `Dispose` 方法中编写同步清理逻辑。
- **IAsyncLifetime**:xUnit 特有的接口,提供 `InitializeAsync` 和 `DisposeAsync` 来处理异步的准备和拆除。
- **执行时机**:清理方法会在测试方法完成后立即被调用,无论测试是通过还是失败。

### 课程笔记

在进行与数据库或其他持久化存储交互的集成测试时,清理测试期间创建的任何数据非常重要。
在 xUnit 中实现清理最简单的方式是实现 `IDisposable` 接口。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-cleanup-53953810/?t=10)

要添加清理逻辑,修改该类使其实现 `IDisposable`,并定义 `Dispose` 方法。
放在这个方法中的任何代码都会在测试结束后运行。

```csharp
public class CustomerControllerTests : IDisposable
{
    // ... existing code ...

    public void Dispose()
    {
        // Any cleanup code goes here
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-cleanup-53953810/?t=50)

`Dispose` 方法在每次测试执行时被调用一次。
如果一个测试类包含多个测试方法,xUnit 会在每次执行时实例化一次该类,这意味着构造函数和 `Dispose` 方法会为每一个测试调用。
这确保了测试彼此隔离地运行,除非使用了 class fixture。

在许多集成测试场景中,清理操作(例如从数据库中删除记录)是异步的。
为了处理这种情况,xUnit 提供了 `IAsyncLifetime` 接口。
实现这个接口时,你必须同时为 `InitializeAsync` 和 `DisposeAsync` 提供实现。

```csharp
public class CustomerControllerTests : IAsyncLifetime
{
    // ... existing code ...

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await Task.Delay(1000);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/test-cleanup-53953810/?t=95)

使用 `IAsyncLifetime` 时,`DisposeAsync` 方法会在测试执行完毕后由测试运行器 await。
与同步的 `Dispose` 类似,它会为类中的每个测试方法调用。

## 11. Parameterizing tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/) · 5:26

### 总结

xUnit 中的参数化测试让开发者能够针对多组输入数据执行相同的测试逻辑,这对于在不重复代码的情况下验证各种场景至关重要。
通过从 Fact 特性切换到 Theory,开发者可以为测试方法定义参数,并使用 InlineData、MemberData 或 ClassData 等特性提供数据。
这种方式提升了测试的可维护性,并让测试套件能够随着新用例或边界情况的发现而高效扩展。

### 核心概念

- 用于数据驱动测试的 `[Theory]` 特性。
- `[InlineData]`,用于把简单的硬编码值直接传给测试方法。
- `[MemberData]`,用于从测试类中的静态属性或方法获取测试数据。
- `[ClassData]`,用于把复杂的测试数据外置到一个实现了 `IEnumerable<object[]>` 的专用类中。
- 数据特性与测试方法签名之间的参数匹配。

### 课程笔记

在集成测试中,测试用例常常需要参数,例如特定的标识符或用户数据。
虽然单个测试用例可以在标准的 `[Fact]` 测试中使用硬编码的值,但用这种方式测试多个场景会导致大量重复代码。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/?t=10)

为了高效地扩展测试,xUnit 提供了 `[Theory]` 特性。
一个 theory 代表一组执行相同逻辑但使用不同输入数据的测试。
使用 `[Theory]` 时,测试方法必须用一个数据提供特性来修饰,例如 `[InlineData]`。

```csharp
[Theory]
[InlineData("9FD9A907-E86E-43BC-9A80-F2B0F274133F")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/?t=60)

`[InlineData]` 中提供的参数必须与测试的方法签名相匹配。
可以给同一个测试方法添加多个 `[InlineData]` 特性,用不同的值多次运行它。

```csharp
[Theory]
[InlineData("9FD9A907-E86E-43BC-9A80-F2B0F274133F")]
[InlineData("45905930-B282-4330-891F-381BD951D2BC")]
[InlineData("6728cd57-1e42-4fd0-9867-75a090fa2ce3")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/?t=135)

虽然 `[InlineData]` 适合简单的值,但更复杂或数量更多的数据会让测试类变得杂乱。
在这种情况下,可以使用 `[MemberData]` 指向一个返回 `IEnumerable<object[]>` 的静态成员(例如属性或方法)。
顶层集合中的每一项代表一个测试用例,而内层的 `object[]` 包含该用例的参数。

```csharp
[Theory]
[MemberData(nameof(Data))]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}

public static IEnumerable<object[]> Data { get; } = new[]
{
    new object[] { "9FD9A907-E86E-43BC-9A80-F2B0F274133F" },
    new object[] { "45905930-B282-4330-891F-381BD951D2BC" },
    new object[] { "6728cd57-1e42-4fd0-9867-75a090fa2ce3" }
};
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/?t=240)

```text
IEnumerable<object[]> Data
+-- object[] { "9FD9A907-..." }  --> test case 1 --> guidAsText
+-- object[] { "45905930-..." }  --> test case 2 --> guidAsText
+-- object[] { "6728cd57-..." }  --> test case 3 --> guidAsText
```

为了彻底分离关注点,可以使用 `[ClassData]` 特性把测试数据移到一个专用类中。
这个类必须实现 `IEnumerable<object[]>`,并提供一个产出测试参数的枚举器。

```csharp
[Theory]
[ClassData(typeof(ClassData))]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}

public class ClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "9FD9A907-E86E-43BC-9A80-F2B0F274133F" };
        yield return new object[] { "45905930-B282-4330-891F-381BD951D2BC" };
        yield return new object[] { "6728cd57-1e42-4fd0-9867-75a090fa2ce3" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/parameterizing-tests-53953811/?t=310)

## 12. Ignoring tests

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/ignoring-tests-53953812/) · 1:18

### 总结

本课讲解如何在 xUnit 中使用 Skip 属性把特定测试排除在执行之外。
它涵盖了忽略单个 [Fact] 测试、整个 [Theory] 套件,或某个 theory 中特定 [InlineData] 用例的实际用法,并强调为排除提供清晰理由的重要性。

### 核心概念

- xUnit 测试特性中的 `Skip` 属性。
- 跳过单个 `[Fact]` 测试。
- 跳过整个 `[Theory]` 测试套件。
- 跳过某个 theory 中特定的 `[InlineData]` 用例。
- 为被忽略的测试提供必需的理由。

### 课程笔记

在 xUnit 中,测试可能因为各种原因被忽略,例如某个测试当前失败但由于时间限制必须先推送,或者某个测试已经不再相关、需要在将来修订。

首先,来看一个针对客户 API 的标准集成测试类:

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/ignoring-tests-53953812/?t=10)

#### 跳过一个 Fact

要忽略单个测试,在 `[Fact]` 特性中给 `Skip` 属性赋一个字符串。
这个字符串应当明确说明该测试被忽略的原因。
测试运行器执行时,这个测试会被标记为已跳过,并在测试结果中显示所提供的原因。

```csharp
[Fact(Skip = "This doesn't work atm sorry")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/ignoring-tests-53953812/?t=40)

#### 跳过一个 Theory

如果你把 `Skip` 属性应用到 `[Theory]` 特性上,由相关 `[InlineData]` 特性定义的每一个测试用例都会被忽略。

```csharp
[Theory(Skip = "This doesn't work atm sorry")]
[InlineData("9FD9A907-E86E-43BC-9A80-F2B0F274133F")]
[InlineData("45905930-B282-4330-891F-381BD951D2BC")]
[InlineData("6728cd57-1e42-4fd0-9867-75a090fa2ce3")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/ignoring-tests-53953812/?t=55)

#### 跳过特定的 Theory 用例

如果你只需要排除某个 theory 中的一个特定用例,同时让其他用例照常运行,可以把 `Skip` 属性直接应用到单个 `[InlineData]` 特性上。

```csharp
[Theory]
[InlineData("9FD9A907-E86E-43BC-9A80-F2B0F274133F", Skip = "This doesn't work atm sorry")]
[InlineData("45905930-B282-4330-891F-381BD951D2BC")]
[InlineData("6728cd57-1e42-4fd0-9867-75a090fa2ce3")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/ignoring-tests-53953812/?t=70)

## 13. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953814/) · 0:46

本课为 ASP.NET Core 集成测试的基础部分作结。
它总结了到目前为止所涵盖的核心概念,并概述了向更高级测试技术的过渡。

### 核心概念

*   **xUnit 基础**:使用 `[Fact]` 编写单个测试,使用 `[Theory]` 配合 `[InlineData]` 编写参数化的数据驱动测试。
*   **HttpClient 集成**:在测试执行期间使用一个配置好的 `HttpClient` 与 API 端点交互。
*   **状态码验证**:断言 API 会根据不同的输入场景返回预期的 `HttpStatusCode`。
*   **课程路线**:认识到虽然基本的 xUnit 断言足以起步,但 FluentAssertions 这类高级断言库可以提供更易读、更具表达力的测试。

### 课程笔记

到这个阶段,开始使用 xUnit 和 ASP.NET Core 集成测试所需的基本功已经建立起来了。
当前的测试策略是创建一个测试类,由它管理一个指向本地 API 主机的 `HttpClient` 实例。
测试的结构是先对 API 执行一个操作,再对得到的状态码进行断言。

在下面的示例中,`Get_ReturnsNotFound_WhenCustomerDoesNotExist2` 测试演示了一种使用 `[Theory]` 的数据驱动方式。
这让同一段测试逻辑可以针对多个 GUID 字符串执行,以确保 API 在资源不存在时始终返回 `404 Not Found` 状态码。

```csharp
[Theory]
[InlineData("9FD9A907-E86E-43BC-9A80-F2B0F274133F", Skip = "This doesn't work atm sorry")]
[InlineData("45905930-B282-4330-891F-381BD951D2BC")]
[InlineData("6728cd57-1e42-4fd0-9867-75a090fa2ce3")]
public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
    string guidAsText)
{
    // Act
    var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/section-recap-53953814/?t=10)

课程的下一阶段包含一个可选部分,专门讲解不同类型的断言。
这部分内容最初来自单元测试的语境,但同样非常适用于集成测试。
它介绍了 FluentAssertions,该库提供了更接近自然语言的语法,用来验证复杂的对象和值,而不仅仅是简单的状态码。

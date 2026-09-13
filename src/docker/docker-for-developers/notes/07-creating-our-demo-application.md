# Creating our demo application

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 7 章
> 共 4 课 · 约 11:37
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123857/) | 1:11 | [↓](#1-introduction) |
| 2 | [Frontend](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/frontend-54123856/) | 2:42 | [↓](#2-frontend) |
| 3 | [Backend](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backend-54123859/) | 3:31 | [↓](#3-backend) |
| 4 | [A simple database interaction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/) | 4:13 | [↓](#4-a-simple-database-interaction) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123857/) · 1:11

### 总结

本课介绍将在整个课程中被容器化的演示应用架构。
该应用由一个 Blazor WebAssembly 前端、一个 ASP.NET Core Minimal API 后端和一个 SQL Server 数据库组成,每一部分都运行在自己的容器中,以演示多容器编排以及 Docker Compose 的用法。

### 核心概念

- **Container Isolation**:每个应用组件(前端、API 和数据库)都位于自己专属的容器中。
- **Technology Stack**:该解决方案使用 .NET 7,具体来说前端用 Blazor WebAssembly,后端用 ASP.NET Core Minimal APIs。
- **SPA Paradigm**:Blazor WebAssembly 是一个单页应用(Single Page Application)框架,可以与 React、Angular 或 Vue 相提并论。
- **Minimal APIs**:这个轻量的 .NET 框架类似于用来构建后端服务的 Node.js Express。
- **Docker Compose**:这套多容器配置的设计目的,是最终演示如何使用 Docker Compose 进行编排。

### 课程笔记

本节的目标是构建一个完整的应用栈,使其适合打包成 Docker 镜像并作为容器运行。
遵循“每个容器只关注一件事”的最佳实践,应用被拆分为三个彼此独立的部分:前端界面、后端 API,以及一个 SQL Server 数据库实例。

前端组件使用 Blazor WebAssembly 开发。
它是一个单页应用(SPA)框架,与 Angular、Vue 和 React 这类流行的 JavaScript 框架共享相同的架构范式。
后端方面,应用使用 ASP.NET Core Minimal APIs,它提供了类似于 Node.js 生态中 Express 的轻量开发体验。

除了应用代码之外,还会像课程前面演示过的那样,在一个独立的容器中运行一个 SQL Server 实例。
之所以特意选择这种多容器架构,是为了给后面使用 Docker Compose 提供一个实际场景。
Docker Compose 让开发者可以用一条命令管理并启动整个栈,包括前端、API 和数据库。

## 2. Frontend

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/frontend-54123856/) · 2:42

本课介绍如何使用 Blazor WebAssembly 完成演示应用前端组件的初始搭建与开发。

### 核心概念

- **Blazor WebAssembly (WASM)**:一个用 .NET 构建交互式 Web UI 的客户端框架。
- **Docker Support in IDEs**:在创建项目时启用 Docker 支持,以自动生成一个 Dockerfile。
- **Dependency Injection**:使用 `@inject` 指令把 `HttpClient` 这类服务提供给 Razor 组件。
- **Asynchronous Data Fetching**:利用 `GetFromJsonAsync` 从后端 API 获取 JSON 数据。
- **Conditional Rendering**:使用 Razor 语法处理 null 状态,并遍历集合进行展示。

### 课程笔记

前端应用被创建为一个 Blazor WebAssembly 项目。
在 IDE 的项目创建过程中,启用了面向 Linux 的 Docker 支持。
虽然应用一开始是在本地运行的,但这个设置能确保生成一个 `Dockerfile`,供之后容器化使用。

默认的 `Index.razor` 组件是应用主界面的起点。

```razor
@page "/"

<PageTitle>Index</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app.

<SurveyPrompt Title="How is Blazor working for you?"/>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/frontend-54123856/?t=25)

为了能与后端 API 通信,必须把 `HttpClient` 注入到组件中。
UI 中添加了一个按钮来触发数据获取过程。

```razor
@page "/"
@inject HttpClient HttpClient

<PageTitle>Index</PageTitle>

<h1>Hello, world!</h1>

<button class="btn btn-primary" @onclick="GetPodcasts">Get Podcasts</button>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/frontend-54123856/?t=55)

获取数据的逻辑实现在一个 `@code` 块中。
应用使用一个支持字段 `_podcasts` 来存储 API 返回的字符串列表。
`GetPodcasts` 方法被定义为 `async Task`,用于向位于 `http://localhost:5001/podcasts` 的后端服务发起非阻塞调用。

```csharp
@code {
    private List<string>? _podcasts;

    private async Task GetPodcasts()
    {
        _podcasts = await HttpClient.GetFromJsonAsync<List<string>>(requestUri: "http://localhost:5001/podcasts");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/frontend-54123856/?t=115)

最后,更新 UI 来渲染获取到的数据。
应用通过一个 `@if` 语句检查 `_podcasts` 列表是否已经被填充。
一旦数据可用,就渲染一个无序列表,并用 `@foreach` 循环遍历集合来显示每个播客标题。

```razor
@page "/"
@inject HttpClient HttpClient

<PageTitle>Index</PageTitle>

<h1>Hello, world!</h1>

<button class="btn btn-primary" @onclick="GetPodcasts">Get Podcasts</button>

@if (_podcasts != null)
{
    <ul>
        @foreach (var podcast in _podcasts)
        {
            <li>@podcast</li>
        }
    </ul>
}

@code {
    private List<string>? _podcasts;

    private async Task GetPodcasts()
    {
        _podcasts = await HttpClient.GetFromJsonAsync<List<string>>(requestUri: "http://localhost:5001/podcasts");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/frontend-54123856/?t=145)

## 3. Backend

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backend-54123859/) · 3:31

### 总结

本课介绍如何创建一个 .NET Minimal API 后端,为 Blazor WebAssembly 前端提供数据。
它演示了如何搭建一个返回播客标题集合的 REST 端点、如何通过 HttpClient 把两个服务集成起来,以及如何解决跨源资源共享(CORS)问题,从而让相互独立的前端和后端项目之间能够安全通信。

### 核心概念

- **Minimal APIs**:在 .NET 中以最少的依赖和样板代码构建 HTTP 服务的一种轻量方式。
- **Endpoint Routing**:使用 `MapGet` 定义 API 路由及其返回值。
- **Frontend-Backend Integration**:使用 `HttpClient` 把一个 Blazor WebAssembly 应用连接到独立的 API 服务。
- **CORS (Cross-Origin Resource Sharing)**:一种安全机制,需要显式配置才能允许某个源上的 Web 应用访问另一个源的资源。

### 课程笔记

后端使用 .NET Minimal API 模板构建,它提供了一种简洁的端点定义方式。
初始的项目结构极其基础,只包含一个 `Program.cs` 文件,用来配置 Web 应用并定义一个默认路由。

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backend-54123859/?t=25)

为了提供所需的数据,根端点被改成 `/podcasts`,返回类型也从一个简单的字符串改为一个表示各种播客标题的字符串列表。

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/podcasts", () => new List<string>
{
    "Unhandled Exception Podcast",
    "Developer Weekly Podcast",
    "The Stack Overflow Podcast",
    "The Hanselminutes Podcast",
    "The .NET Rocks Podcast",
    "The Azure Podcast",
    "The AWS Podcast",
    "The Rabbit Hole Podcast",
    "The .NET Core Podcast",
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backend-54123859/?t=100)

在前端,Blazor 应用使用 `HttpClient` 来获取这些数据。
组件中包含一个按钮,用来触发 `GetPodcasts` 方法,该方法向 API 的本地地址发起 GET 请求,并填充一个列表用于展示。

```razor
<button class="btn btn-primary" @onclick="GetPodcasts">Get Podcasts</button>

@if (_podcasts is not null)
{
    <ul>
        @foreach (var podcast in _podcasts)
        {
            <li>@podcast</li>
        }
    </ul>
}

@code {
    private List<string>? _podcasts;

    private async Task GetPodcasts()
    {        
        _podcasts = await HttpClient.GetFromJsonAsync<List<string>>("http://localhost:17860/podcasts");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backend-54123859/?t=110)

在尝试把前端连接到后端时,通常会出现 CORS 错误,因为这两个应用运行在不同的域(或端口)上。
CORS 是一项安全特性,它通过限制哪些源可以发起请求来保护 API。
为了在开发阶段解决这个问题,必须把 API 配置为允许来自任意源的请求。

具体做法是:把 CORS 服务添加到 builder 中,并以一个允许任意源的策略把 CORS 中间件应用到应用管线上。

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());

app.MapGet("/podcasts", () => new List<string>
{
    "Unhandled Exception Podcast",
    "Developer Weekly Podcast",
    "The Stack Overflow Podcast",
    "The Hanselminutes Podcast",
    "The .NET Rocks Podcast",
    "The Azure Podcast",
    "The AWS Podcast",
    "The Rabbit Hole Podcast",
    "The .NET Core Podcast",
});

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backend-54123859/?t=175)

配置好 CORS 之后,前端就可以成功地从后端 API 获取并显示播客列表。
这两个独立的项目最终会被打包成各自不同的 Docker 镜像。

## 4. A simple database interaction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/) · 4:13

### 总结

本课演示如何用基于 Dapper 的实时 SQL Server 数据库交互,替换 .NET 后端 API 中的硬编码数据。
内容包括在 Docker 容器中搭建数据库、使用 System.Data.SqlClient 配置 API 连接数据库,以及通过把数据库实体投影回预期的字符串格式来保证前端依然兼容。
这个过程涉及把 Minimal API 端点改造成异步的块体处理程序,并用 C# record 来管理数据模型。

### 核心概念

- **Lightweight ORM**:使用 Dapper 执行 SQL 语句并把结果映射为 C# 对象。
- **Containerized Database**:把一个 SQL Server 实例作为独立的 Docker 容器运行,供开发使用。
- **Minimal API Refactoring**:把基于 lambda 的端点转换为异步的块体处理程序,以容纳数据库逻辑。
- **Data Projection**:使用 LINQ 把数据库实体转换成前端期望的特定数据类型。
- **Connection Management**:为本地开发配置带有合适安全与连接参数的 `SqlConnection`。

### 课程笔记

为了摆脱硬编码数据,后端 API 被改为与一个 SQL Server 数据库交互。
这个架构由三个彼此独立的组件构成:Web 前端、后端 API 和数据库,它们最终各自运行在自己的 Docker 容器中。
为了让数据访问层保持简单,这里使用 Dapper 而不是完整的 Entity Framework 实现。

#### API Configuration

首先,必须把 `Dapper` 和 `System.Data.SqlClient` 这两个 NuGet 包添加到 API 项目中。
接着把 `/podcasts` 端点从一个简单的 lambda 重构为块体处理程序,以便能够实例化一个数据库连接。

```csharp
app.MapGet(pattern: "/podcasts", handler: ()
=>
{
    var db = new SqlConnection("");
    return new List<string>
    {
        "Unhandled Exception Podcast",
        "Developer Weekly Podcast",
        "The Stack Overflow Podcast",
        "The Hanselminutes Podcast",
        "The .NET Rocks Podcast",
        "The Azure Podcast",
        "The AWS Podcast",
        "The Rabbit Hole Podcast",
    };
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/?t=55)

#### Database Setup

使用 Docker 启动一个 SQL Server 实例。
下面的命令会带上 EULA 和 SA 密码所需的环境变量启动服务器,并映射默认的 SQL 端口 1433。

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Dometrain#123" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/?t=130)

容器运行起来之后,使用 Azure Data Studio 之类的工具创建一个名为 `podcasts` 的数据库和一个名为 `Podcasts` 的表。
表结构包含一个唯一标识符和一个标题。

```sql
CREATE TABLE Podcasts
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(MAX) NOT NULL
)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/?t=175)

#### Implementing Database Logic

在 API 中定义了一个 `Podcast` record 来映射数据库行。
端点被改为 `async`,使用 Dapper 的 `QueryAsync<T>` 方法查询数据库。

一开始直接返回原始的 `Podcast` 对象会导致前端出错,因为 Blazor 组件期望的是 `List<string>`,而不是一个包含 Id 和 Title 的对象列表。

```razor
@code {
    private List<string>? _podcasts;

    private async Task GetPodcasts()
    {
        _podcasts = await HttpClient.GetFromJsonAsync<List<string>>("http://localhost:17860/podcasts");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/?t=210)

为了解决这个问题,API 使用 LINQ 的 `.Select()` 方法对结果进行投影,只返回每个播客的 `Title` 属性,从而与现有的前端实现保持兼容。

```csharp
using System.Data.SqlClient;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());

app.MapGet("/podcasts", async () =>
{
    var db = new SqlConnection("Server=tcp:localhost;Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);

    // return new List<string>
    // {
    //     "Unhandled Exception Podcast",
    //     "Developer Weekly Podcast",
    //     "The Stack Overflow Podcast",
    //     "The Hanselminutes Podcast",
    //     "The .NET Rocks Podcast",
    //     "The Azure Podcast",
    //     "The AWS Podcast",
    //     "The Rabbit Hole Podcast",
    //     "The .NET Core Podcast",
    // };
});

app.Run();

record Podcast(Guid Id, string Title);
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/a-simple-database-interaction-54123860/?t=235)

---

## 运行 Demo

Demo 位于 `src/docker/docker-for-developers/demo-app/`,由第 2 到第 4 课的代码组成:`DockerForDevelopers.DemoApp.Frontend`(Blazor WebAssembly)、`DockerForDevelopers.DemoApp.Api`(Minimal API + Dapper + CORS),以及 `db/init.sql`(建库建表并写入课程里的 9 条播客数据)。
项目目标框架跟随 `src/Directory.Build.props` 为 net10.0;`System.Data.SqlClient` 已弃用,改用 `Microsoft.Data.SqlClient`。
文件夹不带章节编号,因为后续章节(写 Dockerfile、docker compose、卷、网络)都会继续在这同一个应用上做。

本机是 ARM64 Windows,课程使用的 `mcr.microsoft.com/mssql/server:2022-latest` 只有 amd64 镜像,在 QEMU 模拟下启动即崩溃:

```
SQL Server 2022 will run as non-root by default.
This container is running as user mssql.
To learn more visit https://go.microsoft.com/fwlink/?linkid=2099216.
qemu: uncaught target signal 11 (Segmentation fault) - core dumped
/opt/mssql/bin/launch_sqlservr.sh: line 28:    14 Segmentation fault      "$@"
```

因此改用原生支持 arm64 的 `mcr.microsoft.com/azure-sql-edge`,T-SQL 语句和连接字符串都不变。
在 x64 机器上把下面第一条命令换回课程原本的镜像即可。

### 1. 启动数据库并建表

```bash
cd src/docker/docker-for-developers/demo-app
docker run -d --name podcasts-db -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=Dometrain#123" -p 1433:1433 mcr.microsoft.com/azure-sql-edge:latest
sqlcmd -S localhost -U sa -P "Dometrain#123" -C -i db/init.sql
```

```
Changed database context to 'podcasts'.
(9 rows affected)
```

```
IMAGE                                     STATUS         PORTS                    NAMES
mcr.microsoft.com/azure-sql-edge:latest   Up 5 minutes   0.0.0.0:1433->1433/tcp   podcasts-db
```

### 2. 启动 API 和前端

```bash
dotnet run --project DockerForDevelopers.DemoApp.Api -c Release
dotnet run --project DockerForDevelopers.DemoApp.Frontend -c Release
```

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:17860
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5022
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### 3. 带 Origin 请求 API,确认 CORS 生效

```bash
curl -i -H "Origin: http://localhost:5022" http://localhost:17860/podcasts
```

```
HTTP 200
Access-Control-Allow-Origin: *
["The Hanselminutes Podcast","The .NET Core Podcast","The .NET Rocks Podcast","The Azure Podcast","The AWS Podcast","Unhandled Exception Podcast","The Stack Overflow Podcast","The Rabbit Hole Podcast","Developer Weekly Podcast"]
```

### 4. 在浏览器里点击 Get Podcasts

打开 `http://localhost:5022`,点击按钮后页面渲染出的列表(通过无头 Edge 实际点击后读取 DOM 得到):

```
The Hanselminutes Podcast
The .NET Core Podcast
The .NET Rocks Podcast
The Azure Podcast
The AWS Podcast
Unhandled Exception Podcast
The Stack Overflow Podcast
The Rabbit Hole Podcast
Developer Weekly Podcast
```

顺序与数据库返回顺序一致,`SELECT * FROM Podcasts` 没有 `ORDER BY`,所以不是插入顺序。

### 清理

```bash
docker rm -f podcasts-db
```

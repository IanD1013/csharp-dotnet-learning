# Other use-cases

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 11 章
> 共 4 课 · 约 14:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123919/) | 0:23 | [↓](#1-introduction) |
| 2 | [Docker and CI/CD](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-and-cicd-54123921/) | 3:20 | [↓](#2-docker-and-cicd) |
| 3 | [Leveraging Docker for Testing](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/) | 9:30 | [↓](#3-leveraging-docker-for-testing) |
| 4 | [Spikes](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/spikes-54123922/) | 1:27 | [↓](#4-spikes) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123919/) · 0:23

### 总结

本课介绍那些超出本地开发范围、延伸到更广泛软件开发生命周期的进阶 Docker 使用场景。
它突出了从学习 Docker 基础到把这些知识应用于自动化测试和持续集成的过渡。
主要话题包括:针对容器化数据库执行集成测试,以及在 GitHub Actions 这类 CI/CD 环境中自动化构建与测试流水线。

### 核心概念

- **Integration Testing**:用 Docker 为测试套件提供真实的基础设施(例如 SQL Server)。
- **Build-Time Testing**:把测试执行直接纳入 Dockerfile 的多阶段构建过程。
- **CI/CD Automation**:利用 GitHub Actions 在每次推送时构建 Docker 镜像并运行容器化测试。
- **Database Seeding**:使用 Docker 容器,为测试环境自动完成数据库结构和数据的初始化。

### 课程笔记

重点从容器化基础转向在开发工作流中的实际应用。
在确保测试运行于尽可能贴近生产的环境这一点上,Docker 尤其有效。

#### Testing with Real Databases

集成测试中的一个常见难题是管理数据库这类外部依赖。
Docker 让这件事变简单:它允许把一个真实的数据库实例作为容器启动起来。
这确保了测试面对的是真正的数据库引擎,而不是 mock 或内存替代品。
例如,可以在 Docker Compose 文件中定义一个 SQL Server 2022 容器,并配套一个用于初始化结构、填充测试数据的 seeding 服务。

#### Build-Time Test Execution

可以把 Dockerfile 组织成在镜像创建过程中运行测试。
通过在多阶段构建中加入一个测试阶段,只要测试没通过构建就会失败,从而阻止有问题的镜像被推送到注册表。
在 .NET 7.0 的场景下,这意味着在 build 阶段之后、最终 publish 阶段之前执行 `dotnet test`。

#### CI/CD Integration

Docker 是现代 CI/CD 流水线的基石。
像 GitHub Actions 中的自动化工作流,可以用 Docker 检出源码、登录容器注册表、用 Docker Compose 启动所需的基础设施,并构建/测试应用镜像。
这种自动化确保每一次变更在被认为可以部署之前,都已在干净、可复现的环境中得到验证。

## 2. Docker and CI/CD

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-and-cicd-54123921/) · 3:20

### 总结

把 Docker 集成进 CI/CD 流水线可以自动化构建与部署过程,确保每一次代码变更都被验证并打包成容器镜像。
借助 GitHub Actions 这类工具,开发者可以配置工作流,使其在特定路径变更时触发,打上多个标签(例如 latest 和唯一的运行编号),并使用 Buildx 这样的现代构建引擎把镜像推送到 Docker Hub 之类的注册表。

### 核心概念

*   **Automated Triggers**:用路径过滤器配置流水线,使其仅在特定子目录发生变更时执行。
*   **GitHub Actions Integration**:利用 `docker/build-push-action` 这类专门的 action 来简化容器化过程。
*   **Tagging Strategies**:实施双标签策略,`latest` 供日常使用,`github.run_number` 提供不可变、可追溯的构建。
*   **Docker Buildx**:现代构建引擎(使用 BuildKit),如今已是 `docker build` 命令的默认后端。
*   **Registry Authentication**:使用 GitHub Secrets 中的凭据,安全地把镜像推送到 Docker Hub。

### 课程笔记

在现代开发者工作流中,推送到源码管理的代码变更应当自动触发构建和部署流水线,而不是依赖手工的本地构建。
包括 GitHub Actions、Azure DevOps、Jenkins 和 Team City 在内的大多数 CI/CD 系统,都对基于 Docker 的工作流提供了完善的支持。

#### Workflow Configuration

GitHub Actions 的工作流定义在 `.github/workflows` 目录下的 YAML 文件中。
为了优化流水线执行,可以把触发条件限制在特定的分支和文件路径上。
这样可以确保 API 代码的变更不会不必要地触发前端服务的构建。

```yaml
on:
  push:
    branches:
      - "main"
    paths:
      - 'DockerCourseApi/**'
  workflow_dispatch:

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      -
        name: Checkout
        uses: actions/checkout@v3
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-and-cicd-54123921/?t=85)

#### Building and Pushing Images

在 GitHub Actions 中构建并推送镜像的标准方式是 `docker/build-push-action`。
关键配置包括:
*   **Context**:构建上下文路径(例如 `./DockerCourseApi/.`)。
*   **File**:Dockerfile 的具体路径。
*   **Tags**:最佳实践是使用多个标签。使用 `${{ github.run_number }}` 为每次构建提供唯一标识,而 `latest` 则确保最新的镜像易于获取。

```yaml
- name: Build and push
  uses: docker/build-push-action@v4
  with:
    context: ./DockerCourseApi/.
    file: ./DockerCourseApi/DockerCourseApi/Dockerfile
    push: true
    tags: |
      ${{ secrets.DOCKERHUB_USERNAME }}/api:latest
      ${{ secrets.DOCKERHUB_USERNAME }}/api:${{ github.run_number }}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-and-cicd-54123921/?t=100)

这些 action 运行时,日志会呈现标准的 Docker 构建输出,显示 Dockerfile 的每一步(例如 `WORKDIR`、`RUN dotnet build`)在 CI runner 上被执行。

#### Docker Buildx and BuildKit

现代 Docker 环境使用 **Buildx**,它是由 **BuildKit** 驱动的当代构建工具。
在较新版本的 Docker Desktop 和 CLI 中,标准的 `docker build` 命令实际上是 `docker buildx build` 的别名。

```shell
docker build
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-and-cicd-54123921/?t=175)

Buildx 带来了更好的性能、更好的缓存,以及对多平台构建的支持。
在 CI 环境中运行时,只要使用相同的 Dockerfile 和上下文,这些工具就能确保产出的容器镜像与本地构建出来的完全一致。

## 3. Leveraging Docker for Testing

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/) · 9:30

### 总结

Docker 让高保真的集成测试成为可能:开发者可以针对 SQL Server 这样的真实基础设施运行测试,而不必依赖内存 mock。
这种做法能捕获 mock 常常漏掉的环境相关问题,例如死锁。
把测试执行集成进 Docker 构建过程,并利用 Docker Compose 做依赖编排和数据库 seeding,团队就能在本地开发环境和 CI/CD 流水线之间获得一致的测试结果。

### 核心概念

*   **Real Infrastructure vs. Mocks**:使用真正容器化的数据库做集成测试,以捕获死锁和语法错误这类问题。
*   **Custom WebApplicationFactory**:专门为测试环境覆盖应用设置(例如连接字符串)。
*   **Docker Compose Orchestration**:用 `docker compose up` 启动常驻服务,用 `docker compose run` 执行数据库 seeding 这类一次性任务。
*   **In-Build Testing**:在 Dockerfile 内执行 `dotnet test`,确保只有测试通过才会产出镜像。
*   **Host Networking**:在构建期间使用 `--network host`,让容器能访问运行在宿主机网络栈上的服务。
*   **CI/CD Resilience**:在 GitHub Actions 中加入 `if: always()` 逻辑,确保即使测试失败也会执行基础设施的拆除。

### 课程笔记

#### Integration Testing with Real Databases

针对真实数据库做集成测试优于使用内存 mock,因为它能捕获现实世界中的行为,例如数据库特有的加锁和死锁。
在本实现中,一个 ASP.NET Core API 项目由一个专门的测试项目来测试,该项目针对 API 发起高层次的 HTTP 请求。

```csharp
using System.Net;
using FluentAssertions;

namespace DockerCourseApi.Tests;

public class ApiTests
{
    [Fact]
    public async Task GivenGetRequestToPodcastsEndpoint_ShouldReturnOkay()
    {
        var httpClient = new CustomWebApplicationFactory().CreateClient();
        var response = await httpClient.GetAsync("/podcasts");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/?t=55)

#### Configuration and Overrides

为了支持不同的环境(本地、Docker 和测试),应用使用 .NET 配置系统把设置绑定到一个强类型的类上。
这取代了硬编码的连接字符串。

```csharp
public class Settings
{
    public string ConnectionString { get; set; } = null!;
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/?t=160)

`Program.cs` 文件被更新为通过 `IOptions<Settings>` 注入这些设置:

```csharp
builder.Services.Configure<Settings>(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());

app.MapGet("/podcasts", async (IOptions<Settings> settings) =>
{
    var db = new SqlConnection(settings.Value.ConnectionString);

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/?t=130)

在测试项目中,使用一个 `CustomWebApplicationFactory` 来覆盖 `ConnectionString`,使其指向 `localhost`,而不是生产中使用的 Docker 内部服务名。

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace DockerCourseApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {        builder.ConfigureAppConfiguration(configBuilder =>
        {
            configBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("ConnectionString", "Server=tcp:localhost;Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"),
            });
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/?t=171)

#### Orchestrating the Test Environment

在运行测试之前,数据库必须先启动并完成 seeding。
Docker Compose 允许有针对性地管理服务:

1.  **Start the Database**:`docker compose up -d database` 启动 SQL Server 实例。
2.  **Seed the Data**:`docker compose run database-seed` 执行一个一次性容器,它会等待数据库就绪,然后运行 SQL 脚本来填充测试数据。

#### Testing within the Docker Build

`Dockerfile` 被更新为包含一条 `RUN dotnet test` 命令。
这确保只要有测试未通过,镜像构建就会失败。

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"]
RUN dotnet restore "DockerCourseApi/DockerCourseApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/build
RUN dotnet test "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj"
RUN dotnet publish "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/?t=250)

当数据库运行在一个独立容器中、而你在本地构建这个 Dockerfile 时,构建会失败,因为构建容器内部的 `localhost` 并不指向宿主机。
要解决这个问题,在构建时使用 `--network host` 标志:

`docker build -f .\DockerCourseApi\Dockerfile --network host .`

#### CI/CD Integration with GitHub Actions

在 CI/CD 流水线中应用的是同样的逻辑。
工作流启动数据库、进行 seeding,然后使用宿主机网络运行 Docker 构建。
一个关键的补充是拆除步骤上的 `if: always()` 标志,它确保即使构建或测试失败,`docker compose down` 也会执行,从而避免 CI runner 上的资源泄漏。

```yaml
    steps:
      - name: Checkout
        uses: actions/checkout@v3

      - name: Login to Docker Hub
        uses: docker/login-action@v2
        with:
          username: ${{ secrets.DOCKERHUB_USERNAME }}
          password: ${{ secrets.DOCKERHUB_TOKEN }}

      - name: Spin up database
        run: docker compose up -d database

      - name: Seed database
        run: |
          chmod +x ./Database/wait-and-run.sh
          docker compose run --build database-seed

      - name: Build
        run: |
          docker build \
            -t ${{ secrets.DOCKERHUB_USERNAME }}/api:latest \
            -t ${{ secrets.DOCKERHUB_USERNAME }}/api:${{github.run_number}} \
            -f ./DockerCourseApi/DockerCourseApi/Dockerfile \
            --network host \
            ./DockerCourseApi/.

      - name: Push
        run: |
          docker push ${{ secrets.DOCKERHUB_USERNAME }}/api:latest
          docker push ${{ secrets.DOCKERHUB_USERNAME }}/api:${{github.run_number}}

      - name: Tear down database
        if: always()
        run: docker compose down database
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-docker-for-testing-54123923/?t=355)

## 4. Spikes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/spikes-54123922/) · 1:27

### 总结

Docker 为 "spiking" 提供了理想的环境,也就是在不承担本地安装开销的前提下,快速评估新技术或新库的过程。
通过从 Docker Hub 拉取官方镜像并在容器中运行,开发者可以立刻开始针对 Redis、RabbitMQ 或各种 SQL 数据库编写代码,评估结束后就把环境丢弃,从而保持宿主机干净。

### 核心概念

*   **Spiking**:快速原型验证或技术评估,用来判断某个工具是否满足项目需求。
*   **Zero-install environment**:使用容器,免去在开发者工作站上永久安装软件的需要。
*   **Docker Hub**:一个中心注册表,用于查找并拉取 Redis 这类基础设施组件的官方镜像。
*   **Port Mapping**:把容器化的服务暴露给宿主机,让本地开发工具可以与之交互。

### 课程笔记

当项目需求提示要用某项新技术(例如 Redis)时,Docker 让你可以立刻做实验,而不必先投入一次完整的安装。
要开始一次 spike,先在 Docker Hub 上找到官方镜像,然后用 `docker run` 命令启动该服务。
把服务的默认端口(例如 Redis 的 6379)映射到宿主机,本地应用就能与容器内的实例通信。

```shell
docker run -p 6379:6379 redis
Unable to find image 'redis:latest' locally
latest: Pulling from library/redis
faef57eae888: Already exists
bb595d48e52d: Already exists
d479b54c3bb2: Already exists
2044989c541a: Already exists
01e4ba5495fa: Already exists
ed7a9fd4b0ea: Already exists
Digest: sha256:08a82d4bf8a8b4dd94e8f5408cdbad9dd184c1cf311d34176
Status: Downloaded newer image for redis:latest
1:C 15 Jul 2023 10:48:23.407 # oO0OoO0OoO0Oo Redis is startir
1:C 15 Jul 2023 10:48:23.407 # Redis version=7.0.12, bits=64
1:C 15 Jul 2023 10:48:23.407 # Warning: no config file speci
.conf
1:M 15 Jul 2023 10:48:23.407 * monotonic clock: POSIX clock_g
1:M 15 Jul 2023 10:48:23.407 * Running mode=standalone, port=63
1:M 15 Jul 2023 10:48:23.407 # Server initialized
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/spikes-54123922/?t=31)

容器跑起来之后,你就可以写最少量的代码来试这项技术。
在 .NET 环境中,这可能意味着使用 LINQPad 这样的工具,以及 `StackExchange.Redis` 这样的库,执行设置值和读取值之类的基本操作,以验证该技术是否适合你的使用场景。

```csharp
var redis = ConnectionMultiplexer.Connect("localhost");

var db = redis.GetDatabase();

var value = "Hello Dometrain";

db.StringSet("WelcomeMsg", value);

db.StringGet("WelcomeMsg").Dump();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/spikes-54123922/?t=55)

这套工作流适用于 Docker Hub 上的各种软件,包括 RabbitMQ、SQL Server 和 MySQL。
它提供了一种低摩擦的方式来启动基础设施、用它做测试,然后立刻拆掉,不在宿主系统上留下任何残留。

---

## 运行 Demo

本章在前几章留下的 demo-app 上加了三样东西:配置绑定、一个集成测试项目、以及构建期跑测试的 Dockerfile。
新增和改动的文件是:

```
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Settings.cs          (新增)
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Program.cs
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/appsettings.json
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Dockerfile
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/                   (新增)
src/docker/docker-for-developers/demo-app/docker-compose.yaml
src/docker/docker-for-developers/demo-app/.github/workflows/build-api.yml                      (新增)
src/docker/docker-for-developers/demo-app/.github/workflows/build-frontend.yml                 (新增)
```

第 9 章硬编码在 `Program.cs` 里的连接字符串,现在挪到了 `appsettings.json`,由 `Settings` 绑定:

```csharp
builder.Services.Configure<Settings>(builder.Configuration);

app.MapGet("/podcasts", async (IOptions<Settings> settings) =>
{
    await using var db = new SqlConnection(settings.Value.ConnectionString);

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);
});
```

和课程代码的出入,以及原因:

- 断言用 xunit 自带的 `Assert.Equal`,没有引入 FluentAssertions。FluentAssertions 8.x 起商用需要付费许可,为一行断言把这个引入仓库不值得,测试的形状和课程是一样的。
- 测试方法名带下划线,会被 `AnalysisLevel=latest-recommended` 的 CA1707 拦下。Given_Should 这种命名就是建立在下划线上的,所以在测试 csproj 里 `NoWarn` 掉了这一条。
- `public partial class Program;` 是必须的。顶层语句编译出来的 `Program` 是 internal,`WebApplicationFactory<Program>` 要求它 public。
- Dockerfile 的构建上下文仍是 `src/`(第 8 章的原因),所以 `COPY` 要把 Api 和 Tests 两个项目一起带进去,还要多带一个 `.editorconfig` ——`EnforceCodeStyleInBuild` 打开时构建会读它。
- `docker-compose.yaml` 的 api 服务加了 `build.network: host`,否则 `docker compose build api` 里的 `dotnet test` 连不到数据库。

### 跑起来

测试连的是 `localhost:1433`,所以顺序是固定的:先起数据库、seed,再跑测试或构建镜像。

```bash
cd src/docker/docker-for-developers/demo-app
docker compose up -d database
docker compose run --build database-seed
```

```
Waiting for SQL Server to be ready...
Not ready yet...
Not ready yet...
SQL Server is ready.
Changed database context to 'podcasts'.
(9 rows affected)
```

本地直接跑测试:

```bash
dotnet test src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Tests/DockerForDevelopers.DemoApp.Tests.csproj -c Release
```

```
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 389 ms - DockerForDevelopers.DemoApp.Tests.dll (net10.0)
```

### 构建期的测试,以及 `--network host`

```bash
docker build -t api -f DockerForDevelopers.DemoApp.Api/Dockerfile --network host ../../..
```

```
#17 [build 11/12] RUN dotnet test "DockerForDevelopers.DemoApp.Tests/DockerForDevelopers.DemoApp.Tests.csproj" -c Release
#17 3.430 Test run for /src/artifacts/bin/DockerForDevelopers.DemoApp.Tests/release/DockerForDevelopers.DemoApp.Tests.dll (.NETCoreApp,Version=v10.0)
#17 4.417 Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 374 ms - DockerForDevelopers.DemoApp.Tests.dll (net10.0)
#17 DONE 4.5s
```

把 `--network host` 去掉,第 4 课说的那个失败是真的会发生的 —— 构建容器里的 `localhost` 不通向宿主机,29 秒连接超时之后整个构建挂掉:

```
#17 34.13   Failed DockerForDevelopers.DemoApp.Tests.ApiTests.GivenGetRequestToPodcastsEndpoint_ShouldReturnOkay [29 s]
#17 34.13 Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 29 s - DockerForDevelopers.DemoApp.Tests.dll (net10.0)
ERROR: failed to build: failed to solve: process "/bin/sh -c dotnet test ..." did not complete successfully: exit code: 1
```

这正是本章的点:镜像只有在测试通过时才会产出。

### `docker compose up --build` 的两个坑

把测试放进镜像构建之后,`docker compose up --build` 就不再是一条可以独立使用的命令了。

**坑一:只有缓存失效时测试才真的跑。**
没改 Api 或 Tests 的源码时,`RUN dotnet test` 这一层直接命中缓存,测试根本不执行 —— 数据库关着也照样构建成功:

```
db is down:              (没有容器在跑)
=== docker compose build api ===
#17 CACHED  #18 CACHED  #19 CACHED  #20 CACHED  #21 CACHED
 Image api Built
```

所以"构建通过"不等于"这次测试通过了",它可能只是复用了上一次通过时的那一层。

**坑二:缓存一旦失效,冷启动的 `docker compose up --build` 会失败。**
Compose 是先把所有镜像构建完才启动容器,构建 api 时 database 还没起来,测试连不上 `localhost:1433`:

```
#38 33.62 Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 29 s
target api: failed to solve: process "/bin/sh -c dotnet test ..." did not complete successfully: exit code: 1
```

正确的顺序是把数据库的启动和 seed 拆在前面,这也正是第 3 课和 `build-api.yml` 里那几步的顺序:

```bash
docker compose up -d database
docker compose run --build database-seed
docker compose up --build -d
```

```
#30 4.467 Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 355 ms
 Container api Started
```

### 整栈确认

```bash
docker compose up -d
curl -s http://localhost:17860/podcasts
```

```
["The Stack Overflow Podcast","Unhandled Exception Podcast","The .NET Rocks Podcast","Developer Weekly Podcast","The .NET Core Podcast","The Azure Podcast","The AWS Podcast","The Hanselminutes Podcast","The Rabbit Hole Podcast"]
```

### 两个 workflow 文件

`build-api.yml` 和 `build-frontend.yml` 按课程的结构写好了,路径换成了本仓库的,但放在 `demo-app/.github/workflows/` 而不是仓库根目录。
GitHub 只读根目录下的 `.github/workflows`,所以这两份是惰性的:这个仓库是学习笔记仓库,没有 `DOCKERHUB_USERNAME` / `DOCKERHUB_TOKEN`,真挂上去只会每次 push 都红一次。
要真正启用,把文件移到仓库根的 `.github/workflows/` 并配好这两个 secret 即可。

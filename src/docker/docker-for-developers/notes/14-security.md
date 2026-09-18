# Security

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 14 章
> 共 3 课 · 约 12:26
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123974/) | 1:36 | [↓](#1-introduction) |
| 2 | [Image scanning](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/) | 4:11 | [↓](#2-image-scanning) |
| 3 | [Running containers as non-root](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/) | 6:39 | [↓](#3-running-containers-as-non-root) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123974/) · 1:36

### 总结

本课介绍 Docker 安全,重点放在开发者对容器镜像所负的责任上。
它强调了保持应用依赖、外部服务以及基础操作系统镜像处于更新状态以缓解漏洞的必要性,同时指出更广泛的基础设施安全由编排平台负责。

### 核心概念

- **Image-Centric Security**:关注所构建镜像的安全性,因为镜像是任何编排平台的部署单元。
- **Dependency Lifecycle**:保持应用级别的包(NuGet、NPM、Maven)以及外部服务依赖处于最新状态的重要性。
- **Base Image Management**:定期更新 Dockerfile 中的 `FROM` 指令,以纳入最新的操作系统级安全补丁。
- **Automated Scanning**:利用 Docker 内置的工具识别镜像中的已知漏洞。

### 课程笔记

在 Docker 开发语境下,安全是一个宽泛的主题,但对开发者而言,它的范围往往因为使用了 Kubernetes 或基于云的方案这类容器编排平台而被缩小。
这些平台处理了大部分基础设施安全问题,使开发者主要只需对自己构建的容器镜像的安全性负责。

镜像安全的一个关键方面是依赖管理。
这包括:

1. **Application Packages**:通过 NuGet、NPM 或 Maven 这样的包管理器管理类库。
2. **External Dependencies**:保护与数据库和第三方服务之间的连接。
3. **The Operating System**:在 Docker 中,操作系统由 `Dockerfile` 中指定的基础镜像决定。

维持一个安全的环境,需要确保基础镜像保持在最新状态。
更新的基础镜像包含了操作系统及其内部依赖的更新版本,从而提供针对新发现漏洞的防护。
开发者和团队应当为所有类型的依赖建立定期更新的实践。

为了支撑这些实践,Docker 提供了集成的工具,可以扫描镜像中的已知漏洞。
这让开发者能够在开发生命周期的早期识别并修复安全风险。

## 2. Image scanning

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/) · 4:11

### 总结

镜像扫描是识别 Docker 镜像中漏洞的一项关键安全实践。
`docker scan`(此前由 Snyk 提供支持)已被废弃,如今 Docker 提供 `docker scout` 作为对照 Common Vulnerabilities and Exposures(CVE)数据库分析镜像的主要工具。
本课介绍如何使用 `docker scout` CLI 和 Docker Desktop GUI 来识别、过滤和修复安全风险,并强调保持基础镜像更新以及把扫描集成进 CI/CD 流水线的重要性。

### 核心概念

- 从 `docker scan` 过渡到 `docker scout` 进行镜像分析。
- 理解 CVE(Common Vulnerabilities and Exposures)。
- 扫描本地镜像和特定版本以发现漏洞。
- 识别从基础镜像(例如 Debian)继承而来的漏洞。
- 使用 `--only-fixed` 这类过滤条件来确定修复的优先级。
- 借助 SARIF 输出把漏洞扫描集成进 CI/CD 流水线。

### 课程笔记

此前使用 Snyk 进行漏洞分析的旧 `docker scan` 命令已从 Docker CLI 中移除。
它被 `docker scout` 取代,这是一个用于镜像分析和安全报告的原生工具。

```shell
docker scan

The docker scan command has been removed.

To continue learning about the vulnerabilities of your images, and
many other features, use the new docker scout command.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/?t=10)

#### Using Docker Scout

`docker scout` 提供了若干用于检查镜像的命令,包括 `compare`、`cves`、`quickview` 和 `recommendations`。
`cves` 命令用于展示在某个软件制品中识别出的漏洞。

```shell
Usage
  docker scout [command]

Available Commands
  compare       Compare two images and display differences (experimental)
  cves          Display CVEs identified in a software artifact
  quickview     Quick overview of an image
  recommendations Display available base image updates and remediation recommendations
  version       Show Docker Scout version information
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/?t=35)

执行扫描时,`docker scout` 会对照 Common Vulnerabilities and Exposures(CVE)数据库检查镜像。
例如,扫描最新版本的 `alpine` 镜像通常不会发现漏洞,而像 `alpine:3.15.7` 这样的旧版本则可能包含若干高危问题。

```shell
docker scout cves alpine
  v SBOM of image already cached, 19 packages indexed
  v No vulnerable package detected

docker scout cves alpine:3.15.7
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/?t=70)

#### Analyzing Vulnerabilities

扫描结果中包含指向 Docker Scout 门户的超链接,提供关于具体漏洞的详细信息,包括公告和修复步骤。

```shell
Fixed version : not fixed

    0C    0H    0M    1L apt 2.2.4
pkg:deb/debian/apt@2.2.4?os_distro=bullseye&os_name=debian&os_version=11

    x LOW CVE-2011-3374
      https://scout.docker.com/v/CVE-2011-3374
      Affected range : >=2.2.4
      Fixed version  : not fixed

26 vulnerabilities found in 15 packages
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/?t=55)

在 Docker Desktop 中,Images 区域提供了一个图形界面来浏览这些漏洞。
这对于识别复杂镜像中的问题尤其有用,例如本课程中使用的 .NET API 镜像。
这个镜像使用 .NET 运行时,而后者又构建在 Debian 之上。

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

# Change user to non-root (gecos means don't interactively prompt for various info about the user)
RUN adduser --disabled-password --gecos '' appuser
USER appuser

ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

在这类镜像中发现的漏洞通常位于基础操作系统的包里(例如 Debian)。
其中一些可以通过更新基础镜像或使用 `apt-get` 手动为包打补丁来解决,但许多漏洞可能并没有可用的修复,或者只有在特定的、非标准的场景下才可能被利用。

#### CI/CD Integration

由于 `docker scout` 是一个 CLI 工具,它很容易被集成进 CI/CD 流水线。
你可以过滤结果,只显示可修复的漏洞,或者把报告导出到文件(例如 SARIF 格式),作为构建产物附加上去。

```shell
      --only-fixed              Filter to fixable CVEs
  -o, --output string           Write the report to a file.
      --type string             Type of the image to analyze. Can be one of:
                                - image
                                - oci-dir
                                - archive (docker save tarball)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-scanning-54123975/?t=235)

## 3. Running containers as non-root

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/) · 6:39

### 总结

以 root 用户运行容器带来了显著的安全隐患,因为容器共享宿主机的内核,这使得 "container escape" 这类攻击成为可能。
通过在 Dockerfile 中使用 adduser 命令和 USER 指令引入一个非 root 用户,开发者可以遵循最小权限原则。
这一实践往往需要调整应用的端口绑定,因为在许多环境中非特权用户被限制绑定 1024 以下的端口,因此必须改用 8080 之类的端口。

### 核心概念

- **Container Escape**:一种安全攻击手段,攻击者借此突破容器的隔离,在宿主机上获得 root 权限。
- **Shared Kernel**:与虚拟机不同,容器共享宿主机操作系统的内核,这放大了内核级攻击的影响。
- **Non-root User**:在容器内创建并切换到一个受限用户账户,以限制潜在损害的做法。
- **Least Privilege**:只授予用户完成其特定任务所必需的最小权限。
- **Unprivileged Port Binding**:Linux 中的一项限制,普通用户无法绑定 1024 以下的端口(例如 80 端口)。

### 课程笔记

#### The Security Risk of Root Access

默认情况下,许多容器镜像以 root 用户运行。
容器虽然是隔离的环境,但它们共享宿主机的内核。
如果攻击者成功执行了 "container escape" 攻击,他们就有可能在宿主机本身上获得 root 权限。
为了缓解这一风险,应当把容器配置为以非特权用户运行。

#### Implementing a Non-Root User

在一个用于 .NET 应用的标准多阶段 Dockerfile 中,构建阶段通常需要 root 权限来执行系统级操作和文件拷贝。
但是,最终的运行时阶段应当被修改为创建一个受限用户。

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"]
RUN dotnet restore "DockerCourseApi/DockerCourseApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/build
RUN dotnet test "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj"
RUN dotnet publish "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/publish /p:UseAppHost

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/?t=145)

要切换到非 root 用户,使用 `adduser` 命令创建一个新的用户账户,并用 `USER` 指令为 Dockerfile 的其余部分以及容器运行时切换上下文。
`--gecos ''` 标志与 `adduser` 一起使用,用于避免在构建过程中出现交互式提示。

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .

# Change user to non-root (gecos means don't interactively prompt for various info about the user)
RUN adduser --disabled-password --gecos '' appuser
USER appuser

ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/?t=175)

#### Verifying the User Identity

一旦镜像带着 `USER` 指令构建完成,容器内启动的任何 shell 会话或应用进程都会以该用户身份运行。
你可以通过把 entrypoint 覆盖为一个 shell 并运行 `whoami` 命令来验证这一点。

```shell
appuser@fb04f04d8404:/app$ whoami
appuser
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/?t=265)

#### Managing Permissions and Least Privilege

以非 root 用户运行时,如果应用需要写入文件,你可能需要显式地为特定目录授予权限。
这是通过在切换到非特权用户之前使用 `chown`(change owner)命令来实现的。

```dockerfile
# Create the user
RUN adduser --disabled-password --gecos '' appuser

# Grant the user ownership of specific directories
RUN chown -R appuser:appuser /app/wwwroot

# Switch to the user
USER appuser
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/?t=280)

#### Port Binding Considerations

在标准的 Linux 环境中,非特权用户无法绑定 1024 以下的端口。
虽然新版本的 Docker 已经修改了默认的 `sysctl` 设置,允许从端口 0 起就进行非特权绑定,但许多生产环境(例如 Kubernetes)仍然强制执行传统的限制。

```yaml
sysctls:
  - net.ipv4.ip_unprivileged_port_start=0
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/?t=325)

为了确保在所有环境中的兼容性,建议把应用配置为在容器内监听一个高于 1024 的端口(例如 8080)。
你仍然可以使用 Docker 的端口映射把它映射到宿主机的 80 端口。

```bash
docker run -it --rm -p 80:8080 --entrypoint bash api2
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/running-containers-as-non-root-54123976/?t=380)

# Building our own images

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 8 章
> 共 3 课 · 约 14:29
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Building our API image](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/) | 4:02 | [↓](#1-building-our-api-image) |
| 2 | [Dockerfile 101](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/dockerfile-101-54123871/) | 7:03 | [↓](#2-dockerfile-101) |
| 3 | [Building our frontend image](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-frontend-image-54123872/) | 3:24 | [↓](#3-building-our-frontend-image) |

## 1. Building our API image

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/) · 4:02

### 总结

本课演示如何使用 Dockerfile 把一个 .NET API 打包成自定义 Docker 镜像。
它讲解了 .dockerignore 文件与 Docker 构建上下文的重要性,说明如何在 docker build 命令中使用指定文件路径和镜像标签的参数,并着重指出当容器化的应用试图用 localhost 连接宿主机上的数据库时常见的网络陷阱。

### 核心概念

* **Dockerfile**:一个文本文件,包含用户在命令行上可以调用的、用来组装镜像的全部命令。
* **.dockerignore**:用于把文件和目录排除在 Docker 构建上下文之外的文件,以减小镜像体积和构建时间。
* **Docker Build Context**:位于指定路径或 URL 下、在构建过程中被发送给 Docker 守护进程的那一组文件。
* **Image Tagging**:使用 `-t` 参数给构建出的镜像指定名称,以及可选的版本(标签)。
* **Container Isolation**:要理解容器内部的 `localhost` 指的是容器自身的网络命名空间,而不是宿主机。

### 课程笔记

要把一个应用容器化,你必须创建一个自定义镜像。
这个过程从 `Dockerfile` 开始,它是一份用于生成镜像的指令清单。
在很多 .NET 项目里,IDE 可能会生成一个默认的多阶段 Dockerfile,负责构建、发布以及创建最终的运行时镜像。

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"]
RUN dotnet restore "DockerCourseApi/DockerCourseApi.csproj"
COPY . .
WORKDIR "/src/DockerCourseApi"
RUN dotnet build "DockerCourseApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerCourseApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/?t=10)

与 Dockerfile 相伴的是 `.dockerignore` 文件。
它类似于 `.gitignore` 文件,用来指定哪些文件和目录应当被排除在 Docker 构建上下文之外。
这可以避免把不必要的文件发送给 Docker 守护进程,比如本地构建产物(`bin`、`obj`)、版本控制数据(`.git`)以及 IDE 设置(`.vs`)。

```dockerignore
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/.vs
**/.vscode
**/.idea
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/azds.yaml
**/bin
**/charts
**/docker-compose*
**/Dockerfile*
**/node_modules
**/npm-debug.log
**/obj
**/secrets.dev.yaml
**/values.dev.yaml
LICENSE
README.md
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/?t=40)

### Building the Image

`docker build` 命令至少需要一个参数:构建上下文的路径。
使用 `.` 表示当前目录。
当这个命令运行时,Docker 会把该目录的内容(排除 `.dockerignore` 里的条目)打包成一个 tarball 并发送给 Docker 守护进程。

```shell
13:00:18 > docker build
ERROR: "docker buildx build" requires exactly 1 argument.
See 'docker buildx build --help'.

Usage:  docker buildx build [OPTIONS] PATH | URL | -

Start a build
13:00:42 > docker build .
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/?t=70)

如果 Dockerfile 不在构建上下文的根目录下,你必须用 `-f` 参数指定它的位置。
这个路径相对于执行该命令的目录。

```shell
13:00:18 > docker build
ERROR: "docker buildx build" requires exactly 1 argument.
See 'docker buildx build --help'.

Usage:  docker buildx build [OPTIONS] PATH | URL | -

Start a build
13:00:42 > docker build -f .\DockerCourseApi\Dockerfile .
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/?t=115)

为了让镜像更容易被引用,使用 `-t`(tag)参数给镜像起一个名字。
如果没有提供版本标签(例如 `:v1`),Docker 默认使用 `latest` 标签。

```shell
13:00:18 > docker build
ERROR: "docker buildx build" requires exactly 1 argument.
See 'docker buildx build --help'.

Usage:  docker buildx build [OPTIONS] PATH | URL | -

Start a build
13:00:42 > docker build -f .\DockerCourseApi\Dockerfile -t api .
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/?t=130)

### Running and Testing the Container

镜像构建好之后,可以用 `docker run` 启动它。
把宿主机端口(例如 `1234`)映射到容器内部端口(例如 `80`),就可以从外部访问这个 API。

然而,当应用试图连接数据库时,一个常见问题就会出现。
如果连接字符串被硬编码成 `localhost`,容器内的应用会在它自己隔离的环境里寻找数据库,而不是在宿主机上。
这会导致连接超时或 `SqlException`,因为 SQL Server 并没有运行在那个特定的容器里。

```text
13:01:44 > docker run -p 1234:80 api
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:80
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HMRUS80T04QF", Request id "0HMRUS80T04QF:00000005": An unhandled exception was thrown by the application.
      System.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 40 - Could not open a connection to SQL Server)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-api-image-54123869/?t=220)

在现阶段这种行为是预期之内的。
解决多容器之间的通信(API 到数据库)通常用 Docker Compose 来处理,后续课程会讲到。

## 2. Dockerfile 101

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/dockerfile-101-54123871/) · 7:03

### 总结

本课提供关于 Dockerfile 的基础性概览,重点讲解把 .NET 应用容器化所需的语法和逻辑。
它解释了 FROM、WORKDIR、COPY 和 RUN 等关键指令的用途,同时演示如何利用多阶段流程和层缓存来优化镜像构建。
通过把构建环境与运行时环境分开,开发者可以创建高效的、面向生产的镜像,其中只包含必要的产物。

### 核心概念

* **FROM**:定义当前构建阶段的基础镜像。
* **WORKDIR**:设置容器内部的工作目录,必要时会创建它。
* **COPY**:把文件从宿主机的构建上下文传输到容器的文件系统。
* **RUN**:在镜像构建过程中执行 shell 命令。
* **Layer Caching**:Docker 缓存每条指令的结果、以加速后续构建的机制。
* **Multi-stage Builds**:一种为构建和运行应用分别使用不同镜像、从而把最终镜像体积降到最小的技术。
* **EXPOSE**:一条仅作文档说明用途的指令,表明容器监听的端口。
* **ENTRYPOINT**:通过指定启动命令,把容器配置成可执行程序的形式运行。

### 课程笔记

Dockerfile 是一个脚本,包含一系列 Docker 遵循以构建镜像的指令。
在现代 IDE 中使用 .NET 时,通常会生成一个默认的 Dockerfile。
这份样板代码一般包含多个阶段,分别处理应用的构建、发布和运行。

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"]
RUN dotnet restore "DockerCourseApi/DockerCourseApi.csproj"
COPY . .
WORKDIR "/src/DockerCourseApi"
RUN dotnet build "DockerCourseApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerCourseApi.csproj" -c Release -o /app/publish /p:UseAppHost

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/dockerfile-101-54123871/?t=10)

生成的代码虽然可用,但可以重构得更简洁。
例如,`publish` 命令会自动触发一次构建,所以单独的 build 阶段并非严格必要。
下面这个重构后的版本合并了这些步骤,并把文件组织成两个主要阶段:build 和 runtime。

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"]
RUN dotnet restore "DockerCourseApi/DockerCourseApi.csproj"
COPY . .
WORKDIR "/src/DockerCourseApi"
RUN dotnet publish "DockerCourseApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/dockerfile-101-54123871/?t=70)

### Instruction Deep Dive

**Base Images (FROM)**
每个 Dockerfile 通常都以 `FROM` 命令开头。
它指定基础镜像,比如某个特定的 Linux 发行版(Ubuntu、Debian),或者像 .NET SDK 这样预先配置好的环境。
Docker 逐行执行来构建镜像,创建中间容器来执行这些指令。

**Working Directory (WORKDIR)**
`WORKDIR` 指令同时起到 `mkdir` 和 `cd` 命令的作用。
如果指定的目录不存在,它会创建该目录,并把它设为该阶段中后续所有指令的当前目录。

**The Build Context and COPY**
`COPY` 命令把文件从宿主机移动到容器中。
源路径相对于“构建上下文”,也就是传给 `docker build` 命令的那个目录(例如 `docker build .`)。

**Optimizing Layer Caching**
Docker 会把每条指令的结果作为一层缓存起来。
如果某条 `COPY` 命令用到的文件发生变化,那一层以及之后的所有层都会失效,必须重新构建。
为了优化这一点,先单独复制 `.csproj` 文件,然后执行 `dotnet restore`。
因为依赖的变化频率低于源代码,所以即使应用逻辑被修改,Docker 也能复用缓存的 “restore” 层。
其余源代码在这之后才被复制进来。

**Execution (RUN)**
`RUN` 关键字在容器内执行 shell 命令。
它被用于安装依赖或编译应用之类的任务。

**Multi-Stage Strategy**
多阶段构建用于创建小巧、安全的生产镜像。
build 阶段使用完整的 .NET SDK 镜像,其中包含所有必要的编译器和工具。
最终阶段使用体积小得多的“运行时”镜像(ASP.NET),它只包含执行已编译代码所需的东西。
产物通过 `COPY --from` 在各阶段之间转移。

**Ports and Entry Points**
`EXPOSE` 指令是一种文档说明形式,标明容器打算监听哪些端口。
它并不会真的把端口发布到宿主机上。
`ENTRYPOINT` 定义容器启动时运行的命令。
在 .NET 中,这相当于在命令行上运行 `dotnet <YourApp>.dll`,由它启动 Web 服务器进程。

## 3. Building our frontend image

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-frontend-image-54123872/) · 3:24

### 总结

本课演示如何为 Blazor WebAssembly 前端创建 Docker 镜像,强调使用多阶段构建、通过 Nginx 而不是标准 .NET 运行时来提供静态文件的重要性。
通过摒弃 IDE 默认生成的 Dockerfile - 它错误地把该项目当作服务端 ASP.NET Core 应用 - 开发者可以创建出明显更小、也更合适的镜像。
这个过程包含一个使用 .NET SDK 的 build 阶段,以及一个使用轻量 Nginx Alpine 镜像来托管已发布静态内容的最终阶段。

### 核心概念

*   **Blazor WASM as Static Content**:与标准 ASP.NET Core 应用不同,Blazor WebAssembly 项目编译成由浏览器下载并执行的静态文件和 DLL。
*   **Multi-stage Build for Frontends**:用体积较大的 SDK 镜像做编译,用轻量的 Web 服务器(比如 Nginx)作为最终的运行时镜像。
*   **Nginx for Hosting**:使用 Nginx 提供已发布 Blazor 应用的 `wwwroot` 内容。
*   **Layer Caching**:理解 Docker 如何缓存未发生变化的层,以加速后续构建。
*   **Implicit Dockerfile Discovery**:当文件名为 `Dockerfile` 且位于当前目录时,运行 `docker build` 可以不带 `-f` 参数。

### 课程笔记

在为 Blazor WebAssembly(WASM)项目创建 Dockerfile 时,标准的 IDE 模板通常会生成一个针对服务端 ASP.NET Core 应用优化的文件。
这份默认配置一般包含一个 .NET 运行时的基础镜像,而这对 WASM 项目来说是不必要的。

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DockerCourseFrontend/DockerCourseFrontend.csproj", "DockerCourseFrontend/"]
RUN dotnet restore "DockerCourseFrontend/DockerCourseFrontend.csproj"
COPY . .
WORKDIR "/src/DockerCourseFrontend"
RUN dotnet build "DockerCourseFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerCourseFrontend.csproj" -c Release -o /app/publish /p:UseAppHo

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseFrontend.dll"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-frontend-image-54123872/?t=10)

上面这份配置的问题在于,它试图在最终阶段运行 .NET CLI。
因为 Blazor WASM 产出的是静态资源,我们其实想用像 Nginx 这样的 Web 服务器来托管这个应用。
一个更高效的两阶段 Dockerfile 只在 build 和 publish 步骤中使用 .NET SDK,然后把得到的静态文件复制到 Nginx 镜像中。

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY DockerCourseFrontend.csproj .
RUN dotnet restore "DockerCourseFrontend.csproj"
COPY . .
RUN dotnet publish "DockerCourseFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-frontend-image-54123872/?t=55)

在这个优化后的版本中,第一阶段使用 `dotnet/sdk` 镜像来执行 `dotnet restore` 和 `dotnet publish`。
第二阶段以 `nginx:alpine` 作为基础镜像。
关键指令是第二阶段的 `COPY` 命令,它从 build 阶段的 `/app/publish/wwwroot` 目录取出已发布的文件,放进 `/usr/share/nginx/html` - 也就是 Nginx 用来提供内容的默认目录。

要构建这个镜像,使用 `docker build` 命令。
由于该文件名为 `Dockerfile` 且位于当前目录,你不需要用 `-f` 指定文件路径。
你可以直接把它标记为 `frontend`,并把上下文设为当前目录(`.`):

```shell
docker build -t frontend .
[+] Building 0.7s (14/14) FINISHED
=> [internal] load build definition from Dockerfile
=> => transferring dockerfile: 380B
=> [internal] load .dockerignore
=> => transferring context: 2B
=> [internal] load metadata for docker.io/library/nginx:alpine
=> [internal] load metadata for mcr.microsoft.com/dotnet/sdk:7.0
=> [build 1/6] FROM mcr.microsoft.com/dotnet/sdk:7.0
=> [internal] load build context
=> => transferring context: 65.98kB
=> [stage-1 1/2] FROM docker.io/library/nginx:alpine@sha256:2d194184b067db3598771b4cf326cfe6ad5051937ba1
=> CACHED [build 2/6] WORKDIR /src
=> CACHED [build 3/6] COPY DockerCourseFrontend.csproj .
=> CACHED [build 4/6] RUN dotnet restore "DockerCourseFrontend.csproj"
=> CACHED [build 5/6] COPY . .
=> CACHED [build 6/6] RUN dotnet publish "DockerCourseFrontend.csproj" -c Release -r
=> CACHED [stage-1 2/2] COPY --from=build /app/publish/wwwroot /usr/share/nginx/ht
=> exporting to image
=> => exporting layers
=> => writing image sha256:509cb0e7eb33693d8e7a8aa74bdccbb79b80d6478ef8178f1964b2e
=> => naming to docker.io/library/frontend
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-frontend-image-54123872/?t=130)

在构建过程中,Docker 会利用层缓存。
如果某一层(比如项目文件的复制或 restore 命令)没有变化,Docker 会使用缓存的版本。
但是,如果检测到某个文件(比如 `.csproj`)发生了变化,那一层以及 Dockerfile 中之后的每一层都会失效,必须重新构建。

镜像构建好之后,你可以把本地端口映射到容器的 80 端口(Nginx 的默认端口)来运行前端容器:

```shell
docker run -p 1234:80 frontend
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/building-our-frontend-image-54123872/?t=190)

运行这条命令之后,前端应用就可以通过 `localhost:1234` 访问。

---

## 运行 Demo

本章给第 7 章创建的 demo-app 加上了两个 Dockerfile,以及构建上下文根目录下的 `.dockerignore`:

```
src/.dockerignore
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Dockerfile
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/Dockerfile
```

构建上下文是 `src/`,不是各自的项目目录:本仓库用 Central Package Management,`Directory.Build.props` 和 `Directory.Packages.props` 放在 `src/` 下,`dotnet restore` 需要它们。
所以两条 `docker build` 都从 `src/` 执行,并用 `-f` 指定 Dockerfile 位置。

### 1. 构建两个镜像

```bash
cd src
docker build -f docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Dockerfile -t demo-app-api .
docker build -f docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/Dockerfile -t demo-app-frontend .
```

```
#15 [build 8/8] RUN dotnet publish "DockerForDevelopers.DemoApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
#15 1.178   All projects are up-to-date for restore.
#15 3.527   DockerForDevelopers.DemoApp.Api -> /src/artifacts/bin/DockerForDevelopers.DemoApp.Api/release/DockerForDevelopers.DemoApp.Api.dll
#15 3.599   DockerForDevelopers.DemoApp.Api -> /app/publish/
#15 DONE 3.7s
#16 [base 3/3] COPY --from=build /app/publish .
#16 DONE 0.1s
#17 naming to docker.io/library/demo-app-api:latest done
```

```
#14 [build 8/8] RUN dotnet publish "DockerForDevelopers.DemoApp.Frontend.csproj" -c Release -o /app/publish /p:UseAppHost=false
#14 6.991   Optimizing assemblies for size. This process might take a while.
#14 25.25   DockerForDevelopers.DemoApp.Frontend -> /app/publish/
#14 DONE 25.5s
#15 [stage-1 2/2] COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
#15 DONE 0.1s
#16 naming to docker.io/library/demo-app-frontend:latest done
```

两个 Dockerfile 的源码 `COPY` 都只拷自己那个项目目录,而不是整个 `demo-app/`:

```
#12 [build 6/8] COPY docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/ docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/
#12 [build 6/8] COPY docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/ docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/
```

两个项目之间没有 `ProjectReference`,所以各自只需要自己的源码。
按照本章讲的层缓存规则,这样改前端就不会让 API 镜像的 `COPY` 层失效。

```bash
docker image ls --filter 'reference=demo-app-*' --format 'table {{.Repository}}\t{{.Tag}}\t{{.Size}}'
```

```
REPOSITORY          TAG       SIZE
demo-app-frontend   latest    144MB
demo-app-api        latest    384MB
```

前端只装了 nginx 和一堆静态文件,所以比带 ASP.NET 运行时的 API 镜像小一半以上。

### 2. 运行 API 容器,复现 localhost 陷阱

第 7 章的 `podcasts-db` 还在宿主机上跑着(端口 1433),但 API 容器里的 `localhost` 指向容器自己:

```bash
docker run -d --name demo-api -p 1234:8080 demo-app-api
curl -s -w "\nHTTP %{http_code}" http://localhost:1234/podcasts
```

```

HTTP 500
```

```bash
docker logs demo-api
```

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HNOHF95QGKPU", Request id "0HNOHF95QGKPU:00000001": An unhandled exception was thrown by the application.
      Microsoft.Data.SqlClient.SqlException (0x80131904): A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 35 - An internal exception was caught)
       ---> System.Net.Sockets.SocketException (0x80004005): Success
         at Microsoft.Data.SqlClient.ManagedSni.SniTcpHandle.Connect(String serverName, Int32 port, TimeoutTimer timeout, SqlConnectionIPAddressPreference ipPreference, String cachedFQDN, SQLDNSInfo& pendingDNSInfo)
```

进程本身起来了,`Now listening on` 正常,失败的只有数据库连接 - 与课程里演示的现象一致,留给后面的 Docker Compose 章节解决。
容器内端口是 8080 而不是课程里的 80:.NET 8 起 `mcr.microsoft.com/dotnet/aspnet` 镜像默认以非 root 用户运行,`ASPNETCORE_HTTP_PORTS` 默认就是 8080。

### 3. 运行前端容器

```bash
docker run -d --name demo-frontend -p 1235:80 demo-app-frontend
curl -s http://localhost:1235/
```

```
    <title>DockerForDevelopers.DemoApp.Frontend</title>
HTTP 200
```

nginx 从 `/usr/share/nginx/html` 提供 Blazor 发布出来的 `wwwroot`,不需要任何 .NET 运行时。

### 清理

```bash
docker rm -f demo-api demo-frontend
```

```
demo-api
demo-frontend
```

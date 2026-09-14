# Dealing with multiple containers

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 9 章
> 共 6 课 · 约 13:43
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123884/) | 1:03 | [↓](#1-introduction) |
| 2 | [The docker-compose YAML file](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-yaml-file-54123885/) | 1:47 | [↓](#2-the-docker-compose-yaml-file) |
| 3 | [The docker compose CLI command](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/) | 3:28 | [↓](#3-the-docker-compose-cli-command) |
| 4 | [Services as DNS entries](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/services-as-dns-entries-54123887/) | 0:39 | [↓](#4-services-as-dns-entries) |
| 5 | [Using docker compose to build our images](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/) | 2:09 | [↓](#5-using-docker-compose-to-build-our-images) |
| 6 | [Seeding our database](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/) | 4:37 | [↓](#6-seeding-our-database) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123884/) · 1:03

### 总结

从一条条独立的 Docker 命令转向 Docker Compose,让开发者可以通过纳入源码管理的配置来管理多容器架构。
这种做法简化了 API、前端和数据库等服务的编排,确保整个团队都能用一条命令一致地启动复杂环境。

### 核心概念

- 编排多容器架构。
- 从手工敲 CLI 参数转向纳入源码管理的配置。
- 把 Docker Compose 当作 Bash 或 PowerShell 脚本的更健壮替代方案。
- 用 `docker compose up --build` 简化环境搭建。

### 课程笔记

随着架构复杂度上升,用 `docker run` 手工管理多个容器会变得低效。
在一个微服务环境里,哪怕只有前端、API 和数据库这么几个服务,手工执行命令也容易出错,而且难以在团队内共享。

为了保证一致性,基础设施配置应该定义在源码管理里。
虽然 shell 脚本(Bash 或 PowerShell)可以把多条 `docker run` 命令自动化,但 Docker Compose 提供了一种更强大、更标准化的方式来定义和管理这些服务。

借助 Docker Compose,开发者可以用一条命令初始化整个技术栈,包括构建镜像和给数据库灌数据:

```bash
docker compose up --build
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123884/?t=34)

这条命令会自动完成为前端和 API 构建所需镜像、并启动相关容器(例如数据库)的过程。
这样一来,每个团队成员都能启动完全相同的环境,而不需要记住每个容器各自的 CLI 参数。

## 2. The docker-compose YAML file

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-yaml-file-54123885/) · 1:47

### 总结

Docker Compose 提供了一种声明式的方式,用一个 YAML 文件来管理多容器应用,免去了写带一大堆参数的复杂 docker run 命令。
本课演示如何为前端、API 和数据库定义服务,包括端口映射和环境变量,并着重说明浏览器里的前端与容器化 API 之间的通信路径。

### 核心概念

- 通过 YAML 进行声明式的多容器管理。
- 服务级别的配置(image、container name、ports、environment)。
- 端口映射语法:host_port:container_port。
- SPA 架构下浏览器到容器的通信。
- 硬编码配置与面向生产的环境配置之间的差别。

### 课程笔记

Docker Compose 让开发者能够把多个 docker run 参数合并进一个易于管理的 YAML 文件。
这种做法用一份描述整个应用栈期望状态的结构化配置,取代了手工敲的 CLI 命令。

docker-compose.yaml 文件以一个 services 块组织,其中每个服务代表应用的一个容器化组件。
在这个例子里,整个栈包含一个 frontend、一个 API 和一个 SQL Server 数据库。

```yaml
services:

  frontend:
    build:
      context: ./DockerCourseFrontend/DockerCourseFrontend/.
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    build:
      context: ./DockerCourseApi/.
      dockerfile: DockerCourseApi/Dockerfile
    image: api
    container_name: api
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
    environment:
      - ACCEPT_EULA=true
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-yaml-file-54123885/?t=10)

#### 端口映射与环境变量

端口映射的语法沿用 docker run -p 的逻辑:冒号左边的值是宿主机端口,右边的值是容器内部端口。
例如,frontend 把宿主机端口 1234 映射到容器端口 80(Nginx 通常监听的端口)。

对于数据库这类服务,环境变量定义在 YAML 块内部。
这样就可以配置接受 EULA、设置系统管理员密码等必要设置,而不必把它们作为 CLI 标志传入。

#### 前端到 API 的通信

容器网络中一个关键的区别在于通信路径。
在这个场景里,前端是一个运行在用户浏览器中的单页应用(SPA)。
因为代码在客户端执行,它无法使用 Docker 内部网络别名去访问 API。
它必须通过宿主机映射出来的端口与 API 通信。

```csharp
// Example of the hardcoded port usage in index.razor
await Client.GetFromJsonAsync<List<string>>(requestUri: "http://localhost:17860/podcasts");
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-yaml-file-54123885/?t=70)

在上面的例子中,前端被硬编码为向 localhost:17860 请求数据。
这与 api 服务定义中暴露的端口一致。
虽然在生产环境中硬编码端口并不推荐,但它说明了浏览器是如何通过宿主机与容器化 API 交互的。

## 3. The docker compose CLI command

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/) · 3:28

### 总结

本课介绍 docker compose CLI,它是管理在 YAML 配置中定义的多容器应用的主要工具。
内容涵盖编排服务的核心命令,包括用 up 启动容器、以分离模式运行、用 ps 查看服务状态,以及用 down 干净地拆掉环境。
本课还着重说明 Docker Compose 如何聚合并用颜色区分来自多个服务的日志,从而简化分布式系统的调试。

### 核心概念

- **docker compose up**:编排网络的创建,并启动配置文件中定义的所有服务。
- **Detached Mode (-d)**:让服务在后台运行,把终端腾出来。
- **Log Aggregation**:自动附着到所有容器,并以服务名为前缀流式输出带颜色的日志。
- **docker compose ps**:提供经过筛选的容器状态视图,只显示与当前 Compose 项目相关的容器。
- **docker compose down**:停止并删除容器和网络,为后续运行保证一个干净的环境。

### 课程笔记

`docker compose` 命令用于管理定义在 `docker-compose.yaml` 文件中的应用的生命周期。
它的很多子命令(例如 `run`、`ps`、`logs` 和 `exec`)在标准 Docker CLI 里都很眼熟,只不过这里作用于项目中定义的整个服务栈。

```shell
--project-directory string   Specify an alternate working directory
                                   (default: the path of the, first
                                   specified, Compose file)
  -p, --project-name string        Project name

Commands:
  build       Build or rebuild services
  config      Parse, resolve and render compose file in canonical format
  cp          Copy files/folders between a service container and the local filesystem
  create      Creates containers for a service.
  down        Stop and remove containers, networks
  events      Receive real time events from containers.
  exec        Execute a command in a running container.
  images      List images used by the created containers
  kill        Force stop service containers.
  logs        View output from containers
  ls          List running compose projects
  pause       Pause services
  port        Print the public port for a port binding.
  ps          List containers
  pull        Pull service images
  push        Push service images
  restart     Restart service containers
  rm          Removes stopped service containers
  run         Run a one-off command on a service.
  start       Start services
  stop        Stop services
  top         Display the running processes
  unpause     Unpause services
  up          Create and start containers
  version     Show the Docker Compose version information
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/?t=10)

要启动应用,在包含 YAML 文件的目录下执行 `docker compose up`。
这条命令会创建默认网络,并为每个服务启动容器。

```shell
C: ... DockerCourse > DockerCourse 9.2 0.001s
 16:49:59 > docker compose up
[+] Building 0.0s (0/0)
time="2023-07-10T16:50:04+01:00" level=warning msg="Found orphan containers ([database-seed]) for this project. If you removed or renamed
r compose file, you can run this command with the --remove-orphans flag to clean it up."
[+] Running 4/4
 ✔ Network dockercourse_default  Created
 ✔ Container database            Created
 ✔ Container frontend            Created
 ✔ Container api                 Created
Attaching to api, database, frontend
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/?t=40)

服务运行起来之后,Docker Compose 会附着到这些容器上并流式输出它们的日志。
每个服务会被分配一种独有的颜色(例如数据库是黄色,API 是蓝色),日志行前面带有服务名前缀,这让人更容易辨认某段输出来自哪里。

```shell
frontend  | 2023/07/10 15:50:05 [notice] 1#1: start worker process 33
database  | SQL Server 2022 will run as non-root by default.
database  | This container is running as user mssql.
api       | info: Microsoft.Hosting.Lifetime[14]
api       |       Now listening on: http://[::]:80
api       | info: Microsoft.Hosting.Lifetime[0]
api       |       Application started. Press Ctrl+C to shut down.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/?t=65)

按 `Ctrl+C` 会停止容器,但不会删除它们。
要让服务在后台运行,使用分离标志:`docker compose up -d`。
容器启动后它会立刻把控制权交还给终端。

要查看当前项目所管理的容器状态,使用 `docker compose ps`。
`docker ps` 会显示系统上的所有容器,而 `docker compose ps` 会把列表筛选为只剩本地 `docker-compose.yaml` 文件中定义的那些。

```shell
16:50:42 > docker compose ps
NAME                IMAGE                                       COMMAND                  SERVICE             CREATED             STATUS              PORTS
api                 api                                         "dotnet Dock..."         api                 About a minute ago  Up About a minute   0.0.0.0:17860->80/tcp
database            mcr.microsoft.com/mssql/server:2022-latest  "/opt/mssql/..."         database            About a minute ago  Up About a minute   0.0.0.0:1433->1433/tcp
frontend            frontend                                    "/docker-ent..."         frontend            About a minute ago  Up About a minute   0.0.0.0:1234->80/tcp
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/?t=100)

你可以用 `docker compose logs` 查看所有服务的日志,或者在后面加上服务名(YAML 文件中定义的那个)来只看某个服务。

```shell
16:51:03 > docker compose logs api
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/?t=130)

要彻底拆掉环境,使用 `docker compose down`。
与 `stop` 不同,这条命令会删除容器和项目的网络。
这是重置环境的有效方式,因为它能保证下一次 `up` 从一个全新状态开始。

```shell
16:51:36 > docker compose down
[+] Running 4/4
 ✔ Container frontend           Removed
 ✔ Container database           Removed
 ✔ Container api                Removed
 ✔ Network dockercourse_default  Removed
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-docker-compose-cli-command-54123886/?t=175)

## 4. Services as DNS entries

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/services-as-dns-entries-54123887/) · 0:39

### 总结

Docker Compose 会自动把服务名当作内部网络中的 DNS 条目,从而简化了容器之间的通信。
当多个容器定义在同一个 Docker Compose 文件中时,它们可以用服务的键名来解析彼此的地址。
这就不再需要硬编码 IP 地址,也不必依赖 localhost,因为 localhost 指的是容器自身的内部回环,而不是宿主机或其他容器。

### 核心概念

- **Service Names as Hostnames**:`docker-compose.yaml` 中 `services` 小节下定义的键会充当 DNS 条目。
- **Internal Networking**:同一个 Compose 项目内的容器可以用这些名字互相通信。
- **Localhost vs. Service Discovery**:在容器内部,`localhost` 指的是容器自己;要访问其他容器必须使用服务名。

### 课程笔记

构建多容器应用时,服务之间常常需要互相通信,比如 API 连接数据库。
在 Docker Compose 环境里,YAML 文件中定义的每个服务都会自动获得一个与其服务名对应的 DNS 条目。

下面这份 `docker-compose.yaml` 定义了三个服务:`frontend`、`api` 和 `database`。

```yaml
services:

  frontend:
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    image: api
    container_name: api
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
    environment:
      - ACCEPT_EULA=true
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/services-as-dns-entries-54123887/?t=10)

因为这些服务属于同一个 Docker Compose 项目,`api` 容器可以用主机名 `database` 访问 `database` 容器。
在应用代码里,连接字符串必须相应更新。
在容器内部使用 `localhost` 指的是容器自身,而不是宿主机或其他容器。
把服务器地址改成服务名之后,Docker 的内部 DNS 就会处理路由。

```csharp
app.MapGet("/podcasts", async () =>
{
    var db = new SqlConnection("Server=tcp:database;Initial Catalog=podcasts;Persist Security Info=False;User ID=sa;Password=Dometrain#123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);
});
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/services-as-dns-entries-54123887/?t=25)

这个机制适用于 Compose 文件中定义的任何服务名。
只要这些容器是通过同一次 Docker Compose 执行启动起来的,它们就可以把这些逻辑名当作 DNS 条目来交互。

## 5. Using docker compose to build our images

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/) · 2:09

### 总结

本课演示如何在 docker-compose.yaml 文件中加入构建定义,把镜像构建过程自动化到 Docker Compose 里。
通过为每个服务指定构建上下文和 Dockerfile 位置,开发者可以直接通过 Compose 命令重新构建镜像,确保代码改动反映到容器中,而不需要额外的手工构建步骤。

### 核心概念

- **Build Context**:Docker 用作构建起点的目录,通常是源码和 Dockerfile 所在的位置。
- **Build Block**:`docker-compose.yaml` 中的一个配置小节,它替代或补充 `image` 标签,以便即时创建镜像。
- **Dockerfile Specification**:当 Dockerfile 不叫 `Dockerfile`,或者位于相对于上下文的子目录中时,可以用 `dockerfile` 属性指向具体文件。
- **Compose Build Commands**:`docker compose build` 会为所有带 build 块的服务触发构建过程,而 `docker compose up --build` 会确保在启动容器之前先重新构建镜像。

### 课程笔记

一开始,Docker Compose 文件依赖的是必须通过命令行手工构建好的既有镜像。
如果源码发生改动,就必须先用 `docker build` 重新构建镜像,Compose 才能用上更新后的版本。

```yaml
frontend:
  image: frontend
  container_name: frontend
  ports:
    - 1234:80

api:
  image: api
  container_name: api
  ports:
    - 17860:80

database:
  image: mcr.microsoft.com/mssql/server:2022-latest
  container_name: database
  environment:
    - ACCEPT_EULA=true
    - MSSQL_SA_PASSWORD=Dometrain#123
  ports:
    - 1433:1433
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/?t=10)

要把这一步自动化,可以给每个服务加一个 `build` 块。
`context` 属性定义构建应当在哪个目录下进行,该路径相对于 `docker-compose.yaml` 文件的位置。

```yaml
services:

  frontend:
    build:
      context: .
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    image: api
    container_name: api
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
    environment:
      - ACCEPT_EULA=true
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/?t=25)

在真实的项目结构里,Dockerfile 往往位于某个特定的子目录中。
`context` 必须指向包含相关源码和 Dockerfile 的那个目录。

```yaml
services:

  frontend:
    build:
      context: ./DockerCourseFrontend/DockerCourseFrontend/.
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    image: api
    container_name: api
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
    environment:
      - ACCEPT_EULA=true
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/?t=55)

对于 api 服务,上下文被设为 API 项目目录。
如果 Dockerfile 位于该上下文的某个子目录中,或者有特定的文件名,就用 `dockerfile` 属性给出从上下文到该文件的相对路径。

```yaml
services:

  frontend:
    build:
      context: ./DockerCourseFrontend/DockerCourseFrontend/.
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    build:
      context: ./DockerCourseApi/.
      dockerfile: DockerCourseApi/Dockerfile
    image: api
    container_name: api
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/?t=85)

构建配置定义好之后,你就可以用一条命令构建 Compose 文件中指定的所有镜像:

```bash
docker compose build
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/?t=100)

或者,为了确保容器启动之前镜像已经重新构建,可以给 `up` 命令加上 `--build` 标志。
当你改动了源码或连接字符串(例如把 `localhost` 改成 `database`),希望这些改动立刻反映到运行环境中时,这一点尤其有用。

```bash
docker compose up --build
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/using-docker-compose-to-build-our-images-54123888/?t=115)

## 6. Seeding our database

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/) · 4:37

### 总结

本课演示如何在多容器环境中引入一个专门的 seeding 服务,把数据库初始化自动化。
借助 Docker Compose 的依赖管理和一个自定义的工具容器,你可以确保 SQL Server 实例不仅在运行,而且已经完全初始化,之后再执行 SQL 脚本来创建表结构并写入初始数据。

### 核心概念

- **Service Dependencies**:在 Docker Compose 中用 `depends_on` 来定义容器的启动顺序。
- **Automated Seeding**:从手工执行 SQL 转向容器生命周期内的自动化流程。
- **Readiness Polling**:实现一个等待脚本,确保某个服务(比如 SQL Server)已经可以接受连接,再去执行操作。
- **Utility Containers**:利用现成的基础镜像(例如 SQL Server 镜像),借用其内置的命令行工具(如 `sqlcmd`)来完成维护任务。

### 课程笔记

要让开发环境完整可用,数据库必须自动初始化出所需的表结构和数据。
虽然 SQL Server 可以通过 Docker 启动,但它启动时是一个空实例。
为了把这一步自动化,我们用一个 SQL 脚本来创建数据库、定义表结构,并插入最初的播客记录。

```sql
CREATE DATABASE podcasts
GO

USE podcasts
GO

CREATE TABLE Podcasts
(
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(MAX) NOT NULL
)
GO

INSERT INTO Podcasts (Title)
VALUES 
('Unhandled Exception Podcast'),
('Developer Weekly Podcast'),
('The Stack Overflow Podcast'),
('The Hanselminutes Podcast'),
('The .NET Rocks Podcast'),
('The Azure Podcast'),
('The AWS Podcast'),
('The Rabbit Hole Podcast'),
('The .NET Core Podcast');
GO
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/?t=25)

在 `docker-compose.yaml` 文件中定义了一个名为 `database-seed` 的新服务。
这个服务使用 `depends_on` 属性,它确保 `database` 服务先于 seeding 服务开始执行而启动。

```yaml
database:
  image: mcr.microsoft.com/mssql/server:2022-latest
  container_name: database
  environment:    - ACCEPT_EULA=true
    - MSSQL_SA_PASSWORD=Dometrain#123
  ports:
    - 1433:1433

database-seed:
  depends_on: [ database ]
  build:
    context: Database/
    dockerfile: Dockerfile
  container_name: database-seed
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/?t=40)

`database-seed` 服务使用一个自定义的 Dockerfile。
它基于与数据库本身相同的那个 SQL Server 镜像;这样做很高效,因为该镜像已经被缓存,并且包含执行 SQL 命令所需的 `sqlcmd` 工具。
这个 Dockerfile 把一个 bash 脚本和 SQL seeding 脚本拷贝进容器。

```dockerfile
FROM mcr.microsoft.com/mssql/server:2022-latest

COPY ./wait-and-run.sh /wait-and-run.sh
COPY ./CreateDatabaseAndSeed.sql /CreateDatabaseAndSeed.sql

CMD /wait-and-run.sh
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/?t=70)

仅仅启动数据库容器还不够;seeding 服务必须等到 SQL Server 引擎完全初始化并可以接受查询。
这由一个 bash 脚本(`wait-and-run.sh`)处理,它用 `sqlcmd` 轮询数据库。
一旦收到成功响应,它就执行 seeding 脚本。

*注意:较新版本的 SQL Server 镜像需要使用 `/opt/mssql-tools18`,并加上 `-C` 标志来信任服务器证书。*

```bash
#!/bin/bash

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to be ready..."
for i in {1..50};
do
    /opt/mssql-tools18/bin/sqlcmd -C -S database -U sa -P Dometrain#123 -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]
    then
        echo "SQL Server is ready."
        break
    else
        echo "Not ready yet..."
        sleep 1
    fi
done

# Run the SQL script
/opt/mssql-tools18/bin/sqlcmd -C -S database -U sa -P Dometrain#123 -d master -i /CreateDatabaseAndSeed.sql
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/?t=130)

有了这份配置,运行 `docker compose up --build` 就能编排整个技术栈。
前端、API 和数据库容器都会启动,而 `database-seed` 容器会等数据库就绪后再向其写入数据。
这让一个新来的开发者可以克隆仓库,用一条命令在几分钟内得到一个功能完整、已有数据的运行环境。

```yaml
services:

  frontend:
    build:
      context: ./DockerCourseFrontend/DockerCourseFrontend/.
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    build:
      context: ./DockerCourseApi/.
      dockerfile: DockerCourseApi/Dockerfile
    image: api
    container_name: api
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
    environment:
      - ACCEPT_EULA=true
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433

  database-seed:
    depends_on: [ database ]
    build:
      context: Database/
      dockerfile: Dockerfile
    container_name: database-seed
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/seeding-our-database-54123889/?t=235)

---

## 运行 Demo

本章在第 7、8 章留下的 demo-app 上补齐了 Compose 编排,新增和改动的文件是:

```
src/docker/docker-for-developers/demo-app/docker-compose.yaml
src/docker/docker-for-developers/demo-app/db/Dockerfile
src/docker/docker-for-developers/demo-app/db/wait-and-run.sh
src/docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Program.cs
```

`Program.cs` 只改了一处,就是第 4 课讲的那一处:连接字符串里的 `Server=tcp:localhost` 改成 `Server=tcp:database`。
前端 `Home.razor` 里的 `http://localhost:17860/podcasts` 不用动,它已经是第 2 课说的“SPA 走宿主机映射端口”那种写法。

和课程的 `docker-compose.yaml` 有三处必要的出入:

- 构建上下文是 `src/` 而不是项目目录。第 8 章的两个 Dockerfile 需要仓库根上的 `Directory.Build.props` 和 `Directory.Packages.props` 才能 restore,所以 `context` 写成 `../../..`,再用 `dockerfile` 指到各自的 Dockerfile。
- API 容器内监听 8080(第 8 章已说明:.NET 8 起 aspnet 镜像默认非 root,`ASPNETCORE_HTTP_PORTS` 是 8080),所以映射是 `17860:8080`;前端是 nginx,仍然是 `1234:80`。宿主机端口与课程一致,前端硬编码的 17860 才能对上。
- 数据库镜像用 `mcr.microsoft.com/azure-sql-edge:latest`(本机 ARM64,原因见第 7 章),环境变量是 `ACCEPT_EULA=1`。

```yaml
services:

  frontend:
    build:
      context: ../../..
      dockerfile: docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Frontend/Dockerfile
    image: frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    build:
      context: ../../..
      dockerfile: docker/docker-for-developers/demo-app/DockerForDevelopers.DemoApp.Api/Dockerfile
    image: api
    container_name: api
    ports:
      - 17860:8080

  database:
    image: mcr.microsoft.com/azure-sql-edge:latest
    container_name: database
    environment:
      - ACCEPT_EULA=1
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433

  database-seed:
    depends_on: [ database ]
    build:
      context: db/
      dockerfile: Dockerfile
    container_name: database-seed
```

### 1. seed 容器换底:azure-sql-edge 里没有 sqlcmd

第 6 课让 seed 镜像直接 `FROM` 数据库那个 SQL Server 镜像,理由是它自带 `sqlcmd`。
azure-sql-edge 不带:

```bash
docker exec podcasts-db bash -c 'ls -d /opt/mssql-tools*'
```

```
ls: cannot access '/opt/mssql-tools*': No such file or directory
```

Microsoft 的 apt 源在 arm64 上也没有 `sqlcmd` 包(`E: Unable to locate package sqlcmd`),但 go-sqlcmd 的 GitHub release 有原生 `sqlcmd-linux-arm64.tar.bz2`。
所以 `db/Dockerfile` 换成一个小 Debian 镜像加这个二进制,`wait-and-run.sh` 里的 `/opt/mssql-tools18/bin/sqlcmd` 相应地写成 `sqlcmd`,其余逻辑和课程一字不差:

```dockerfile
FROM debian:bookworm-slim

ARG TARGETARCH
ARG SQLCMD_VERSION=1.10.0
RUN apt-get update \
 && apt-get install -y --no-install-recommends curl ca-certificates bzip2 \
 && curl -sSL "https://github.com/microsoft/go-sqlcmd/releases/download/v${SQLCMD_VERSION}/sqlcmd-linux-${TARGETARCH}.tar.bz2" \
    | tar -xj -C /usr/local/bin sqlcmd \
 && rm -rf /var/lib/apt/lists/*

COPY ./wait-and-run.sh /wait-and-run.sh
COPY ./init.sql /init.sql
RUN chmod +x /wait-and-run.sh

CMD ["/wait-and-run.sh"]
```

`TARGETARCH` 由 BuildKit 注入,所以在 x64 机器上同一份 Dockerfile 拉的是 `sqlcmd-linux-amd64`。
seed 脚本读的是第 7 章就有的 `db/init.sql`,不需要课程里那个 `CreateDatabaseAndSeed.sql`。

换成 go-sqlcmd 之后还带出一个只在这条路径上出现的偶发失败,值得记一笔。
azure-sql-edge 每次启动都会重新生成自签名证书,其中大约一半的序列号是负数,而 Go 的 x509 解析器直接拒收这种证书:

```
database-seed  | TLS Handshake failed: tls: failed to parse certificate from server: x509: negative serial number
```

课程里的 `-C`(信任服务器证书)在这里救不了场,因为解析发生在信任判断之前。
实测三种取值,只有 `-N disable` 能跳过 TLS 握手本身:

```bash
for n in disable false optional; do
  echo "=== -N $n ==="
  docker compose run --rm --no-deps database-seed sqlcmd -N "$n" -C -S database -U sa -P 'Dometrain#123' -Q "SELECT 1 AS ok"
done
```

```
=== -N disable ===
ok
-----------
          1

(1 row affected)
=== -N false ===
TLS Handshake failed: tls: failed to parse certificate from server: x509: negative serial number
TLS Handshake failed: tls: failed to parse certificate from server: x509: negative serial number

=== -N optional ===
TLS Handshake failed: tls: failed to parse certificate from server: x509: negative serial number
TLS Handshake failed: tls: failed to parse certificate from server: x509: negative serial number
```

所以 `wait-and-run.sh` 里两条 `sqlcmd` 都加上了 `-N disable`。
API 那边不受影响:`Microsoft.Data.SqlClient` 用的是平台的证书栈,`Encrypt=True;TrustServerCertificate=True` 照常连得上。
加上之后连做四轮 `down` + `up`,`database-seed` 每次都是 `Exited (0)`。

### 2. 一条命令拉起整个栈

第 7 章手工起的 `podcasts-db` 占着 1433,先拆掉:

```bash
cd src/docker/docker-for-developers/demo-app
docker rm -f podcasts-db
docker compose up --build -d
```

构建输出之后的编排部分:

```
 Image demo-app-database-seed Built
 Image frontend Built
 Image api Built
 Network demo-app_default Creating
 Network demo-app_default Creating
 Network demo-app_default Created
 Network demo-app_default Created
 Container frontend Creating
 Container api Creating
 Container database Creating
 Container frontend Created
 Container api Created
 Container database Created
 Container database-seed Creating
 Container database-seed Created
 Container frontend Starting
 Container database Starting
 Container api Starting
 Container frontend Started
 Container database Started
 Container database-seed Starting
 Container api Started
 Container database-seed Started
```

### 3. 看状态和 seed 日志

```bash
docker compose ps -a --format 'table {{.Name}}\t{{.Image}}\t{{.Status}}\t{{.Ports}}'
```

```
NAME            IMAGE                                     STATUS                      PORTS
api             api                                       Up About a minute           0.0.0.0:17860->8080/tcp, [::]:17860->8080/tcp
database        mcr.microsoft.com/azure-sql-edge:latest   Up About a minute           0.0.0.0:1433->1433/tcp, [::]:1433->1433/tcp
database-seed   demo-app-database-seed                    Exited (0) 53 seconds ago
frontend        frontend                                  Up About a minute           0.0.0.0:1234->80/tcp, [::]:1234->80/tcp
```

`database-seed` 干完活就 `Exited (0)`,这正是第 6 课描述的一次性工具容器。
轮询循环也能在日志里看到:

```bash
docker compose logs database-seed
```

```
database-seed  | Waiting for SQL Server to be ready...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | Not ready yet...
database-seed  | SQL Server is ready.
database-seed  | Changed database context to 'podcasts'.
database-seed  | (9 rows affected)
```

`depends_on` 只保证 `database` 容器先启动,不保证 SQL Server 已经能接受连接 - 那 10 次 `Not ready yet...` 就是差额,也正是这个等待脚本存在的理由。

### 4. 验证服务名当 DNS 用

API 容器里的连接字符串写的是 `Server=tcp:database`,它现在能连通:

```bash
curl -s -w "\nHTTP %{http_code}\n" http://localhost:17860/podcasts
```

```
["The Stack Overflow Podcast","The Azure Podcast","The Rabbit Hole Podcast","The .NET Core Podcast","Unhandled Exception Podcast","The AWS Podcast","Developer Weekly Podcast","The .NET Rocks Podcast","The Hanselminutes Podcast"]
HTTP 200
```

同一个名字在别的容器里也解析得到。用 seed 镜像起一个一次性容器查一下行数:

```bash
docker compose run --rm --no-deps database-seed sqlcmd -N disable -C -S database -U sa -P 'Dometrain#123' -d podcasts -Q "SELECT COUNT(*) AS Total FROM Podcasts"
```

```
Total
-----------
          9

(1 row affected)
```

对照第 8 章:当时 API 容器里还写着 `localhost`,同样一条 `curl` 拿到的是 HTTP 500 和 `SqlException`。
这一章唯一改的应用代码就是那个主机名。

### 5. 浏览器里点 Get Podcasts

打开 `http://localhost:1234`,点按钮后页面渲染出的列表(通过无头 Edge 实际点击后读取 DOM 得到):

```
The Stack Overflow Podcast
The Azure Podcast
The Rabbit Hole Podcast
The .NET Core Podcast
Unhandled Exception Podcast
The AWS Podcast
Developer Weekly Podcast
The .NET Rocks Podcast
The Hanselminutes Podcast
```

这条链路和 API 那条不一样:浏览器里的 Blazor WASM 走的是宿主机的 1234 -> nginx,再由浏览器自己发请求到宿主机的 17860,全程没有用到 `api` 这个 DNS 名。
第 2 课讲的就是这个区别。

#### 为什么前端容器里的代码写的是 localhost 而不是 api

第 2 课那句"前端无法使用 Docker 内部网络别名"第一次读容易卡住:前端明明也在容器里跑,`localhost:17860` 不该是容器自己的 17860 吗?

关键在于 **前端容器里跑的不是你的 C# 代码,只是一个静态文件服务器**。
看第 8 章那份 `Frontend/Dockerfile` 的运行阶段:

```dockerfile
FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
```

运行阶段是 `nginx:alpine`,里面连 .NET 运行时都没有,所以容器里根本没有任何东西能执行 `GetFromJsonAsync`。
Blazor WebAssembly 发布出来的 `wwwroot` 就是一堆 `.html` / `.js` / `.wasm` / `.dll` 静态文件,nginx 只负责把它们当文件发出去。
那行 C# 被编译进 `.wasm` 和 `.dll`,下载到用户浏览器之后,在浏览器的 WASM 运行时里执行。

所以整个 demo 里有三段代码,分别在三个不同的地方执行:

```
浏览器(宿主机上的一个普通进程)
  │
  │ ① GET http://localhost:1234/          ← 宿主机端口
  ▼
[frontend 容器] nginx :80 ── 把 .wasm/.dll 发给浏览器,任务结束

浏览器拿到代码,开始在本地执行 Blazor
  │
  │ ② fetch http://localhost:17860/podcasts   ← 这行代码在浏览器里跑
  ▼
宿主机 :17860 ──映射──> [api 容器] :8080
                            │
                            │ ③ Server=tcp:database    ← 这行在容器里跑
                            ▼
                       [database 容器] :1433
```

- 第 ② 步的 `localhost` 指 **浏览器所在的机器**,也就是宿主机。宿主机的 17860 被 compose 映射到了 api 容器,所以能通。
- 第 ③ 步的 `database` 才是 Docker 内部 DNS,因为那行代码确实在 api 容器内部执行。

同一份 compose 文件里一个用宿主机端口、一个用服务名,原因只是执行代码的地方不同。
Docker 的内部 DNS 只对同一个 Compose 网络里的容器生效;浏览器是宿主机上的普通进程,不在那个网络里,地址栏敲 `http://api:8080` 解析不出来。
前端代码虽然"出身"在容器里,但执行时人已经在浏览器了,享受不到容器网络的待遇。

第 8 章那次失败是同一个道理的另一面:当时 API 容器里写的是 `Server=tcp:localhost`,那行代码在容器里执行,`localhost` 指容器自己,容器里没有 SQL Server,于是 `SqlException` + HTTP 500。

这也解释了第 2 课为什么说生产环境不该硬编码端口:`localhost:17860` 把"API 在哪"焊死在了前端代码里,只要换台机器打开这个页面(比如同事从局域网访问 `http://你的IP:1234`),浏览器里的 `localhost` 就变成了同事自己的电脑,请求立刻失败。
生产上一般把 API 地址做成运行时配置,或者让 nginx 反向代理 `/api` 到后端,前端只写相对路径。

反过来,如果前端换成 Blazor Server 或 MVC 这类服务端渲染框架,代码就真的在容器里跑了,那时候才应该写 `http://api:8080`。
SPA 是特例,不是通例。

### 清理

```bash
docker compose down
```

```
 Container database-seed Stopping
 Container frontend Stopping
 Container api Stopping
 Container database-seed Stopped
 Container database-seed Removing
 Container database-seed Removed
 Container database Stopping
 Container api Stopped
 Container api Removing
 Container api Removed
 Container frontend Stopped
 Container frontend Removing
 Container frontend Removed
 Container database Stopped
 Container database Removing
 Container database Removed
 Network demo-app_default Removing
 Network demo-app_default Removed
```

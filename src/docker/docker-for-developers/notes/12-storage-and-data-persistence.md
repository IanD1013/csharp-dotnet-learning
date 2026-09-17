# Storage and data persistence

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 12 章
> 共 7 课 · 约 24:42
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction and types of persistent storage in Docker](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-and-types-of-persistent-storage-in-docker-54123931/) | 2:47 | [↓](#1-introduction-and-types-of-persistent-storage-in-docker) |
| 2 | [Creating Volumes](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/creating-volumes-54123932/) | 1:31 | [↓](#2-creating-volumes) |
| 3 | [Mounting volumes in containers](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/) | 6:26 | [↓](#3-mounting-volumes-in-containers) |
| 4 | [Mounting bind mounts in containers](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-bind-mounts-in-containers-54123934/) | 2:55 | [↓](#4-mounting-bind-mounts-in-containers) |
| 5 | [Volumes in Docker compose](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/) | 3:54 | [↓](#5-volumes-in-docker-compose) |
| 6 | [Backing up volumes](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backing-up-volumes-54123936/) | 4:21 | [↓](#6-backing-up-volumes) |
| 7 | [Anonymous volumes and the Dockerfile VOLUME instruction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/anonymous-volumes-and-the-dockerfile-volume-instruction-54123937/) | 2:48 | [↓](#7-anonymous-volumes-and-the-dockerfile-volume-instruction) |

## 1. Introduction and types of persistent storage in Docker

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-and-types-of-persistent-storage-in-docker-54123931/) · 2:47

### 总结

Docker 容器天生就是临时的;一旦容器被删除,任何写入容器可写层的数据都会丢失。
为了保证数据持久化,Docker 提供了三种主要机制:Volumes、Bind Mounts 和 tmpfs mounts。
Volumes 由 Docker 管理,并且存在于容器生命周期之外,因此非常适合像 SQL Server 这样的数据库。
Bind Mounts 把容器中的某个目录链接到宿主机文件系统上的某个具体路径,这对共享配置文件或源代码很有用。
tmpfs mounts 提供高性能的内存存储,但它是瞬时的,不会在容器生命周期之后继续存在。

### 核心概念

- **Ephemeral Storage**:默认状态,数据与容器的生命周期绑定,容器删除后数据即丢失。
- **Volumes**:由 Docker 管理的存储,保存在宿主机文件系统中由 Docker 管理的那部分位置。
- **Bind Mounts**:把宿主机上的一个文件或目录挂载到容器内的某个指定路径。
- **tmpfs Mounts**:数据只保存在宿主机的内存中,永远不会写入宿主机的文件系统。
- **Data Sharing**:volumes 和 bind mounts 都允许多个容器同时访问同一份数据。

### 课程笔记

默认情况下,Docker 容器使用一个可写层来存储数据。
如果一个运行着 SQL Server 之类数据库的容器被删除,那么在其中创建的所有记录和数据库都会被销毁。
为了避免这种情况,Docker 使用外部存储映射。

#### Volumes

volume 是通过 Docker CLI 创建和管理的存储单元。
它以宿主机上的一个目录形式存在,但与宿主机文件系统的其余部分相隔离。
当挂载一个 volume 时,容器内部会创建一个指向这块受管存储的虚拟目录。
例如,在 SQL Server 容器中,数据库引擎存放数据文件的内部目录可以映射到一个 Docker volume。

```powershell
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/mssql/server:2022-latest
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-and-types-of-persistent-storage-in-docker-54123931/?t=58)

因为 volume 独立于容器存在,所以你可以删除一个容器,再从同一个镜像创建一个新容器并指向已有的 volume,从而恢复数据。
多个容器也可以指向同一个 volume 来共享数据。

#### Bind Mounts

bind mounts 的作用与 volumes 类似,但它映射到宿主机文件系统上一个由用户指定的具体目录,而不是 Docker 管理的区域。
它通常用于把配置文件、二进制文件或源代码映射进容器。
容器内的软件会把挂载的路径看作一个本地目录,尽管数据实际上位于宿主机上。

```powershell
docker run `
  --name nginx-withvol `
  -p 1234:80 `
  -v ${pwd}/html:/usr/share/nginx/html `
  nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-and-types-of-persistent-storage-in-docker-54123931/?t=86)

Docker CLI 使用 `-v`(或 `--volume`)标志来处理 volumes 和 bind mounts 两者,尽管它们在管理方式和与宿主机的整合方式上仍然是不同的概念。

#### tmpfs Mounts

`tmpfs` mount 是一种内存存储选项。
与 volumes 和 bind mounts 不同,`tmpfs` 是瞬时的;它的生命周期严格与容器绑定。
当容器被移除时,`tmpfs` 中的数据也会被移除。
它主要用于对性能敏感的操作,在这些场景中写入宿主机的物理存储(即容器的可写层)会太慢。

## 2. Creating Volumes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/creating-volumes-54123932/) · 1:31

### 总结

本课通过命令行实践介绍如何管理 Docker volumes。
内容覆盖 volume 的基本生命周期,包括手动创建命名 volume、列出所有活动的 volume、检查 volume 元数据以确定其在宿主机上的存储位置,以及删除 volume 的过程。

### 核心概念

- `docker volume create`:用于初始化一个命名 volume 以实现持久化存储的命令。
- `docker volume ls`:用于显示所有 volume 的命令,包括命名 volume 和匿名(十六进制)volume。
- `docker volume inspect`:用于查看详细配置的命令,特别是宿主机系统上的 `Mountpoint`。
- `docker volume rm`:用于删除一个 volume 及其关联数据的命令。
- Host Storage Management:理解 volume 是由 Docker 守护进程管理的目录,位于宿主机文件系统内(在 Windows 上则位于 WSL2 虚拟机内)。

### 课程笔记

Docker volumes 通过 `docker volume` 命令套件来管理。
这个工具让开发者可以创建和管理独立于任何具体容器而存在的存储。

```bash
20:03:04 > docker volume

Usage:  docker volume COMMAND

Manage volumes

Commands:
  create      Create a volume
  inspect     Display detailed information on one or more volumes
  ls          List volumes
  prune       Remove all unused local volumes
  rm          Remove one or more volumes

Run 'docker volume COMMAND --help' for more information on a
20:04:10 > docker volume create dometrain
dometrain
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/creating-volumes-54123932/?t=10)

要查看系统上当前存在的 volume,使用 `ls` 命令。
输出会区分命名 volume(比如 `dometrain`)和匿名 volume,后者用很长的十六进制字符串标识。
当容器或镜像指定了一个没有用户定义名称的 volume 时,Docker 通常会自动创建匿名 volume。

```bash
20:04:55 > docker volume ls
DRIVER    VOLUME NAME
local     4ec4dc5e50f673851000dd1d6b75f49439ce3fc2964944f439e6c9fe5b583bdc
local     8a08259e59da6fcf30085293ded5fa4c2276efe8461a7141a66d72b07979ae02
local     9d6d1727478ed58026b1f838d713fa0a32ac2e85a87b09a2ea57da35f829e120
local     505822567de31f94721c3c9e8ea4d2b78bb63a96152d758947052746a2d228ef
local     dometrain
local     e8f6a8f40ba402175fd712ed32edb7763da2aeb3bf953fe044458c63dbbae0c8
local     f161ab1569be80f89709f93749848c8c03cff831f660f460e3061ef7e82c793d
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/creating-volumes-54123932/?t=40)

volume 本质上是宿主机文件系统上一个由 Docker 管理的目录。
要找到这个目录的确切位置,使用 `inspect` 命令。

```bash
20:05:03 > docker volume inspect dometrain
[
    {
        "CreatedAt": "2023-07-17T19:04:55Z",
        "Driver": "local",
        "Labels": null,
        "Mountpoint": "/var/lib/docker/volumes/dometrain/_data",
        "Name": "dometrain",
        "Options": null,
        "Scope": "local"
    }
]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/creating-volumes-54123932/?t=70)

`Mountpoint` 字段指出数据在宿主机上的存储位置。
在使用 Docker Desktop 配合 WSL2 的 Windows 系统上,这个路径指向 Linux 工具虚拟机内的某个位置。

最后,可以使用 `rm` 命令删除 volume。
这会把该 volume 及其全部内容从宿主机系统中移除。
如果之后还需要这个 volume,必须重新创建它。

```bash
20:06:06 > docker volume rm dometrain
dometrain
20:06:51 > docker volume create dometrain
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/creating-volumes-54123932/?t=85)

## 3. Mounting volumes in containers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/) · 6:26

### 总结

Docker volumes 提供了一种独立于容器生命周期来持久化数据的机制,确保容器被移除之后数据依然可用。
通过使用 -v 标志,开发者可以把一个命名 volume 映射到容器文件系统中的某个特定路径。
本课用一个 SQL Server 容器演示 volume 的实际用法,展示在一个容器实例中写入的数据如何持久保留,并且对后续一个全新的容器实例依然可访问。

### 核心概念

- **Volume mapping syntax**:使用 `-v [volume_name]:[container_path]` 标志。
- **Persistence**:volume 独立于容器存在;删除容器不会移除与之关联的 volume。
- **--mount syntax**:相比 `-v` 更啰嗦的替代写法,显式指定 `type`、`source` 和 `target`。
- **Managed Storage**:volume 存放在宿主机文件系统中由 Docker 管理的部分,通常位于宿主机上的某个系统文件夹内。

### 课程笔记

要把一个 volume 挂载到容器上,`docker run` 命令使用 `-v`(或 `--volume`)标志。
这个标志把一个由 Docker 管理的 volume 映射到容器内部的某个特定目录。

```powershell
"Creating SQL Server container using volume ..."
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/mssql/server:2022-latest
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/?t=25)

`-v` 标志的语法遵循与端口映射类似的模式:冒号左边的值表示宿主机上(容器之外)的 volume 名称,右边的值表示容器内部的目录路径。
如果指定的 volume 名称还不存在,Docker 会自动创建它。

虽然 `-v` 语法最为常用,但 Docker 也支持更啰嗦的 `--mount` 语法,官方文档有时出于清晰性的考虑推荐这种写法。

```powershell
# Note that --mount syntax looks like this:
# --mount "type=volume,source=sqldb-data,target=/var/opt/mssql"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/?t=85)

为了演示数据的持久性,可以用一个 PowerShell 脚本(`SqlServerVolumeDemo.ps1`)来自动化容器的生命周期。
该脚本首先通过移除已有的容器和 volume 来确保一个干净的起点,然后启动一个新的 SQL Server 容器,并把一个 volume 挂载到 `/var/opt/mssql`。

```powershell
"Ensure container doesn't exist from previous run of this script ..."
docker rm -f sqlserver-withvol

"Ensuring our volume doesn't exist ..."
docker volume rm sqldb-data

"Creating SQL Server container using volume ..."
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/mssql/server:2022-latest
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/?t=160)

容器运行起来之后,执行一个 SQL 脚本来创建 `podcasts` 数据库并写入初始数据。
这些数据存放在容器内部的 `/var/opt/mssql` 目录中,而该目录映射到宿主机上的 `sqldb-data` volume。

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

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/?t=205)

写入数据之后,容器被删除。
这个操作移除了 SQL Server 实例,但保留了 `sqldb-data` volume。
接着用相同的 volume 映射启动第二个容器。
因为该 volume 已经存在并且包含上一个实例留下的数据库文件,新容器立刻就能访问已有的数据。

```powershell
"Creating another SQL Server container using the same volume ..."
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/mssql/server:2022-latest
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/?t=235)

可以在没有任何容器运行时列出 volume 来验证持久性。
即使创建它的容器已经不在了,该 volume 依然留在 Docker 系统中。

```powershell
Deleting SQL Server container...
sqlserver-withvol
Listing all containers...
CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS     PORTS     NAMES
Listing all volumes...
DRIVER    VOLUME NAME
local     sqldb-data
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-volumes-in-containers-54123933/?t=325)

通过 SQL 客户端连接到第二个容器实例,可以确认 `podcasts` 表及其写入的记录都被保留了下来,这说明 volume 的生命周期独立于任何单个容器的生命周期。

## 4. Mounting bind mounts in containers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-bind-mounts-in-containers-54123934/) · 2:55

### 总结

bind mounts 提供了一种把宿主机上的某个具体路径映射到容器内部路径的机制,这与由 Docker 内部存储管理的 volumes 不同。
这种方式非常适合开发环境,因为它允许开发者使用本地工具和编辑器修改宿主机上的源代码,同时应用运行在容器内部,改动会立即生效。
bind mounts 同样保证数据持久化,因为数据的生命周期绑定在宿主机文件系统上,而不是容器上。

### 核心概念

- **Bind Mount Syntax**:使用 `-v` 标志,冒号左边是宿主机路径,右边是容器路径。
- **Host-to-Container Mapping**:宿主机文件系统与容器文件系统之间的直接链接。
- **Development Workflow**:在本地 IDE 中编辑代码而在容器中执行,从而实现"热重载"。
- **State Persistence**:即使容器被移除或重建,数据仍然保留在宿主机上。
- **Automatic Directory Creation**:如果宿主机目录在容器启动时尚不存在,Docker 会创建它。

### 课程笔记

volumes 使用由 Docker 管理的命名引用,而 bind mounts 使用指向宿主机上某个目录的直接路径。
作为对照,SQL Server 的标准 volume 实现在冒号左边使用一个命名 volume(`sqldb-data`):

```powershell
"Ensure container doesn't exist from previous run of this script..."
docker rm -f sqlserver-withvol

"Ensuring our volume doesn't exist..."
docker volume rm sqldb-data

"Creating SQL Server container using volume..."
docker run `
  --name sqlserver-withvol `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=Dometrain#123" `
  -p 1433:1433 `
  -d `
  -v sqldb-data:/var/opt/mssql `
  mcr.microsoft.com/mssql/server:2022-latest

# Note that --mount syntax looks like this:
# --mount "type=volume,source=sqldb-data,target=/var/opt/mssql"

Read-Host 'Press enter to continue once database is seeded'
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-bind-mounts-in-containers-54123934/?t=10)

要实现 bind mount,`-v` 标志左边必须指定一个路径。
在 PowerShell 中,`${pwd}` 表示当前工作目录。
下面的命令运行一个 Nginx 容器,并把本地的 `html` 目录映射到 Nginx 默认的网站根目录 `/usr/share/nginx/html`:

```powershell
docker rm -f nginx-withvol

docker run `
  --name nginx-withvol `
  -p 1234:80 `
  -v ${pwd}/html:/usr/share/nginx/html `
  nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-bind-mounts-in-containers-54123934/?t=25)

执行这条命令时,Docker 会检查宿主机上是否存在 `html` 目录。
如果不存在,Docker 会创建它。
一开始通过 `localhost:1234` 访问该服务时,如果目录是空的,可能会得到 "403 Forbidden" 错误,因为 Nginx 期望有一个 `index.html` 文件。

在宿主机的 `html` 目录中创建一个 `index.html` 文件,容器就能立即提供该内容。
例如:

```html
<h1>Hello Dometrain</h1>

Some other change.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/mounting-bind-mounts-in-containers-54123934/?t=100)

这种配置演示了一个很强大的开发用例:工具和运行时环境保持容器化,而源代码留在开发者自己的机器上。
对宿主机上文件所做的任何改动,容器化的进程都能立即访问到,无需重新构建镜像。
此外,由于数据保存在宿主机上,它可以通过源代码管理来维护,并且即使容器被停止或移除也依然存在。

## 5. Volumes in Docker compose

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/) · 3:54

### 总结

本课演示如何在 Docker Compose 中使用 volumes 实现数据持久化。
内容涵盖在 docker-compose.yaml 文件中定义命名 volume 的语法、基于项目名自动加前缀的默认行为,以及如何使用外部 volume 来复用已有存储。
此外,还探讨了使用 docker compose down 与 docker compose down -v 时数据的生命周期差异,并通过在不启动写入数据服务的情况下重启数据库,实际演示了数据的持久性。

### 核心概念

* **Service-level Volume Mapping**:在服务内部使用 `volumes` 键,把一个 volume 映射到容器路径。
* **Top-level Volumes Section**:在 Docker Compose 文件根级别声明命名 volume,以管理它们的生命周期。
* **Project Prefixing**:Docker Compose 默认会自动为 volume 名称加上项目(目录)名前缀。
* **External Volumes**:使用 `external: true` 属性引用在当前 Compose 项目之外创建的 volume。
* **Volume Persistence**:理解 `docker compose down` 会停止容器但保留 volume,而 `docker compose down -v` 会同时移除两者。
* **Bind Mounts in Compose**:在 Compose 文件中使用本地目录路径来做宿主机到容器的映射。

### 课程笔记

Docker Compose 文件中 volumes 的语法与 `docker run` 命令使用的标志非常接近。
就像 `-e` 对应 `environment`、`-p` 对应 `ports` 一样,`-v` 标志对应服务定义中的 `volumes` 键。

一开始,Docker Compose 中的标准数据库服务没有持久化能力,这意味着容器被移除时数据就会丢失:

```yaml
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
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/?t=10)

要使用命名 volume,你必须在具体的服务中添加一个 `volumes` 小节,并在 YAML 文件根级别的 `volumes` 小节中声明该 volume。
服务映射的语法是 `volume-name:container-path`。

```yaml
services:
  frontend:
    build:
      context: ./DockerCourseFrontend/DockerCourseFrontend/.
    image: dometraindockercourse/frontend
    container_name: frontend
    ports:
      - 1234:80

  api:
    build:
      context: ./DockerCourseApi/.
      dockerfile: DockerCourseApi/Dockerfile
    image: dometraindockercourse/api
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
    volumes:
      - sqldb-data:/var/opt/mssql

  database-seed:
    depends_on: [ database ]
    build:
      context: Database/
      dockerfile: Dockerfile
    container_name: database-seed

volumes:
  sqldb-data:
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/?t=40)

默认情况下,Docker Compose 会用包含 Compose 文件的目录名作为 volume 名称的前缀(例如 `dockercourse_sqldb-data`)。
如果你希望使用通过 Docker CLI 单独创建的 volume,并阻止这种加前缀的行为,可以把该 volume 标记为 external。

```yaml
volumes:
  sqldb-data:
    external: true
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/?t=70)

当你执行 `docker compose up` 时,Docker 会同时创建容器和已定义的 volume。

```shell
docker compose up
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/?t=115)

为了管理这些资源的生命周期,区分不同的 `down` 命令很重要。
执行 `docker compose down` 会停止并移除容器,但保留 volume,从而保证数据持久化。
要在移除容器的同时显式删除相关联的 volume,你必须使用 `-v` 标志。

```shell
docker compose down -v
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/?t=145)

验证持久性的一个常见做法是:先给数据库写入数据,然后不带 `-v` 标志关闭环境,再在禁用写入数据服务的情况下重新启动环境。
如果数据依然可访问,说明 volume 工作正常。

```yaml
  # database-seed:
  #   depends_on: [ database ]
  #   build:
  #     context: Database/
  #     dockerfile: Dockerfile
  #     container_name: database-seed
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/volumes-in-docker-compose-54123935/?t=175)

如果想快速查看 volume 的内容,Docker Desktop 在 "Volumes" 小节提供了一个图形界面,让用户可以浏览某个具体 volume 内的文件系统。

## 6. Backing up volumes

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backing-up-volumes-54123936/) · 4:21

### 总结

备份 Docker volume 采用这样一种模式:用一个临时容器在命名 volume 和宿主机文件系统之间架起桥梁。
通过把目标 volume 和一个本地目录(以 bind mount 的形式)同时挂载进容器,就可以使用 tar 这类标准 Linux 工具把 volume 的内容打包成归档文件,并把结果文件直接保存到宿主机上。
这种做法无需知道该 volume 在 Docker 宿主机上的内部路径,就能实现可移植的备份与恢复。

### 核心概念

- **Dual Mounting**:把一个命名 volume 和一个宿主机 bind mount 同时挂载到同一个容器上,以便进行数据传输。
- **Temporary Containers**:使用 `alpine` 这类轻量镜像配合 `--rm` 标志来执行一次性的维护任务。
- **Archive Utilities**:在容器内部借助 `tar` 把 volume 数据打包成单个文件。
- **Restoration Pattern**:反向执行备份流程,把位于宿主机上的归档文件解压到一个新的或已有的 Docker volume 中。

### 课程笔记

备份过程依赖 `-v` 标志在同一条 `docker run` 命令中完成两种不同类型的挂载。
第一个挂载把命名 volume 挂到容器内部的某个目录(例如 `/mydata`)。
第二个挂载是一个 bind mount,把宿主机上的当前工作目录(`${pwd}`)映射到容器内部的备份目录(例如 `/backup`)。

容器运行起来之后,执行像 `tar` 这样的归档命令。
使用 `cvf` 标志(Create、Verbose、File)把 volume 挂载点中的内容打包成一个位于 bind mount 上的归档文件。
由于 bind mount 链接到宿主机,容器退出之后这个 `.tar` 文件仍然保留在宿主机上。

恢复过程按相同逻辑反向进行。
把一个新的 volume 与包含归档文件的宿主机目录一起挂载。
然后运行带 `xvf` 标志(Extract、Verbose、File)的 `tar` 命令,把归档文件从 bind mount 解压到新的 volume 中。

这种模式用途非常广泛。
虽然这里是用 volume 演示的,但同样的技术也可以用来备份本地目录,或者使用任何容器化工具去操作宿主机上的文件,而无需把这些工具直接安装到宿主机上。

下面的 PowerShell 脚本展示了完整的工作流程,包括创建 volume、写入数据、备份和恢复:

```powershell
"Delete volume if it already exists..."
docker volume rm BackupDemoVolume

"Create volume..."
docker volume create BackupDemoVolume

"Write file to our volume..."
docker run `
  -it `
  -v BackupDemoVolume:/mydata `
  alpine `
  sh -c "echo 'Hello' > /mydata/hello.txt"

"Create backup of volume..."
docker run `
  --rm `
  -v BackupDemoVolume:/mydata `
  -v ${pwd}:/backup `
  alpine `
  sh -c "cd /mydata && tar cvf /backup/backup.tar *"

"Restore backup..."
docker run `
  --rm `
  -v RestoredVolume:/mydata `
  -v ${pwd}:/backup alpine sh `
  -c "cd /mydata && tar xvf /backup/backup.tar"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/backing-up-volumes-54123936/?t=25)

运行完这些命令后,你可以在 Docker Desktop 中验证结果。
原来的 `BackupDemoVolume` 和 `RestoredVolume` 都会包含 `hello.txt` 文件,并且本地宿主机目录中会出现一个 `backup.tar` 文件。

## 7. Anonymous volumes and the Dockerfile VOLUME instruction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/anonymous-volumes-and-the-dockerfile-volume-instruction-54123937/) · 2:48

### 总结

匿名 volume 是创建时没有显式名称的 Docker volume,在 volume 列表中显示为唯一的哈希值。
它们要么由 -v 标志中省略源名称产生,要么由 Dockerfile 中的 VOLUME 指令产生。
虽然它们通过确保容器数据被持久化到宿主机上提供了一层数据韧性,但通常不如命名 volume 好管理,因为每次运行容器都会创建一个新的匿名 volume,导致数据难以在容器实例之间复用。

### 核心概念

- **Anonymous Volumes**:用随机哈希而非用户定义名称标识的 volume。
- **CLI Creation**:在 `-v` 标志中只给出容器路径(例如 `-v /data`)即可触发。
- **Dockerfile VOLUME Instruction**:一条指令,告诉 Docker 在每次从该镜像启动容器时为指定路径创建一个匿名 volume。
- **Data Resiliency**:匿名 volume 确保数据被写入宿主机文件系统,避免容器被移除时立即丢失数据。
- **Lifecycle**:与命名 volume 不同,匿名 volume 不会被后续的容器实例自动复用。

### 课程笔记

#### Creating Anonymous Volumes via CLI

在管理存储时,通常会给 volume 分配明确的名称以便识别。
但是,如果在 `docker run` 命令中定义 volume 时没有给出源名称或冒号分隔符,Docker 就会创建一个匿名 volume。
这些 volume 在系统中显示为很长的随机十六进制哈希。

```bash
docker run -v /mydata alpine
docker volume ls
DRIVER    VOLUME NAME
local     ebf5e75ca58d9a8391e160e7a018f704a9727d3ff53a3378643a4b105d38c711
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/anonymous-volumes-and-the-dockerfile-volume-instruction-54123937/?t=10)

#### The Dockerfile VOLUME Instruction

使用那些在 Dockerfile 中包含 `VOLUME` 指令的官方镜像时,经常会遇到匿名 volume。
例如,官方的 RabbitMQ Dockerfile 为其数据目录定义了一个 volume,以便默认就具备持久化能力。

```dockerfile
FROM alpine:3.18

COPY --from=erlang-builder /usr/local/bin/ /usr/local/bin/
COPY --from=erlang-builder /usr/local/etc/ssl/ /usr/local/etc/ssl
COPY --from=erlang-builder /usr/local/lib/ /usr/local/lib/

ENV RABBITMQ_DATA_DIR=/var/lib/rabbitmq

RUN set -eux; \
# Configure OpenSSL to use system certs
    ln -vsf /etc/ssl/certs /etc/ssl/private /usr/

VOLUME $RABBITMQ_DATA_DIR
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/anonymous-volumes-and-the-dockerfile-volume-instruction-54123937/?t=55)

当运行一个带有 `VOLUME` 指令的镜像时,Docker 会自动为该路径生成一个新的匿名 volume。
如果你运行多个实例,或者在不指定命名 volume 的情况下重启容器,你会在 volume 列表中看到多个哈希值。

```bash
docker volume ls
DRIVER    VOLUME NAME
local     b76e951c7575afdb20ad52c4a8fe16239c0a9bb20e19d9e068f4
local     ebf5e75ca58d9a8391e160e7a018f704a9727d3ff53a3378643a4
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/anonymous-volumes-and-the-dockerfile-volume-instruction-54123937/?t=70)

#### Use Cases and Limitations

`VOLUME` 指令和匿名 volume 的主要目的是韧性。
通过强制把数据写到宿主机的存储上,Docker 确保数据在容器被移除之后依然存在。
然而,由于这些 volume 不容易识别,并且在启动新容器时默认不会被复用,它们更像是一种备份机制,而不是主要的数据管理策略。

对于本地开发,更推荐使用命名 volume 或 bind mounts,因为它们能带来可预期的数据复用和更简单的清理。
在使用 Kubernetes 这类编排系统的生产环境中,存储管理的处理方式不同,这使得开发者在本地很少需要用到高级 volume 特性或匿名 volume。

```shell
2023-07-21 10:53:55.095997+00:00 [notice] <0.61.0> SIGTERM received - shutting down
2023-07-21 10:53:55.103064+00:00 [info] <0.565.0> Stopping message store for directory '/var/lib/rabbitmq/mnesia/rabbit@f716c348ef86/msg_stores/vhosts/628WB79CIFDY09LJI6DKMI09L/msg_store_persistent'

11:54:03 > docker volume ls
DRIVER    VOLUME NAME
local     b76e951c7575afdb20ad52c4a8fe16239c0a9bb20e19d9e068f4f2cb17953e21
local     ebf5e75ca58d9a8391e160e7a018f704a9727d3ff53a3378643a4b105d38c711
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/anonymous-volumes-and-the-dockerfile-volume-instruction-54123937/?t=100)

---

## 运行 Demo

本章对共享的 demo-app 只有一处改动(第 5 课的 compose volume),另外按第 3、4、6 课新建了三个独立脚本。
新增和改动的文件是:

```
src/docker/docker-for-developers/demo-app/docker-compose.yaml
src/docker/docker-for-developers/12-storage-and-data-persistence/SqlServerVolumeDemo.ps1      (新增)
src/docker/docker-for-developers/12-storage-and-data-persistence/SqlServerBindMountDemo.ps1   (新增)
src/docker/docker-for-developers/12-storage-and-data-persistence/html/index.html              (新增)
src/docker/docker-for-developers/12-storage-and-data-persistence/VolumeBackup.ps1             (新增)
```

这三个脚本是自成一体的单章 demo,不是那个跨章共享 app 的一部分,所以放在编号的章节目录里而不是塞进 `demo-app/`。

和课程脚本的出入,以及原因:

- 数据库镜像用 `mcr.microsoft.com/azure-sql-edge:latest`(本机 ARM64,原因见第 7 章),环境变量是 `ACCEPT_EULA=1`。
- 课程的 `SqlServerVolumeDemo.ps1` 停在 `Read-Host`,等你用 SQL 客户端手工 seed;这里改成用一次性容器自动 seed,脚本才能从头跑到尾。azure-sql-edge 不带 sqlcmd,所以 sqlcmd 用的是第 9 章那个 seeder 镜像(`demo-app/db`)换个 tag 重建,它本来就带 go-sqlcmd 和 `/init.sql`。
- seed 容器用 `--network container:sqlserver-withvol` 共享网络命名空间,`-S localhost` 就能连上,不依赖宿主机端口映射。
- `VolumeBackup.ps1` 的写入步骤去掉 `-it` 并加上 `--rm`:没有东西读 stdin,而 `-it` 在非交互 shell 里直接报错。开头也一并删掉 `RestoredVolume`,让脚本可以重复跑。
- `SqlServerBindMountDemo.ps1` 加了 `-d`,课程是前台跑 nginx。

### 1. compose 里的 volume(第 5 课)

`database` 服务加了 `volumes`,文件根级别加了同名声明:

```yaml
  database:
    image: mcr.microsoft.com/azure-sql-edge:latest
    container_name: database
    environment:
      - ACCEPT_EULA=1
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
      - 1433:1433
    volumes:
      - sqldb-data:/var/opt/mssql

volumes:
  sqldb-data:
```

起数据库并 seed:

```bash
cd src/docker/docker-for-developers/demo-app
docker compose up -d database
docker compose run --build database-seed
```

```
 Network demo-app_default Creating
 Volume demo-app_sqldb-data Creating
 Volume demo-app_sqldb-data Created
 Network demo-app_default Created
 Container database Creating
 Container database Created
 Container database Starting
 Container database Started
Not ready yet...
Not ready yet...
Not ready yet...
SQL Server is ready.
Changed database context to 'podcasts'.
(9 rows affected)
```

volume 名字前面多了 `demo-app_`,这就是第 5 课说的项目名前缀,项目名取自 compose 文件所在的目录名。

不带 `-v` 关掉:

```bash
docker compose down
docker volume ls --filter name=sqldb
```

```
 Container database Removing
 Container database Removed
 Network demo-app_default Removing
 Network demo-app_default Removed
DRIVER    VOLUME NAME
local     demo-app_sqldb-data
```

容器没了,volume 还在。
再起一次数据库,这次完全不跑 `database-seed`,直接数行数:

```bash
docker compose up -d database
docker compose run --rm --no-deps --entrypoint sh database-seed -c "until sqlcmd -N disable -C -S database -U sa -P 'Dometrain#123' -Q 'SELECT 1' > /dev/null 2>&1; do sleep 2; done; sqlcmd -N disable -C -S database -U sa -P 'Dometrain#123' -d podcasts -Q 'SELECT COUNT(*) AS Total FROM Podcasts'"
```

```
Total
-----------
          9
```

9 行还在,而这一轮没有任何人写过数据。
最后带 `-v` 关掉,volume 才真的被删:

```bash
docker compose down -v
```

```
 Container database Stopping
 Container database Stopped
 Container database Removing
 Container database Removed
 Volume demo-app_sqldb-data Removing
 Volume demo-app_sqldb-data Removed
```

### 2. 换容器不换数据(第 3 课)

```powershell
cd src/docker/docker-for-developers/12-storage-and-data-persistence
.\SqlServerVolumeDemo.ps1
```

```
Building the sqlcmd helper image...
sha256:64322b601862f8d70c01201d66b6a61ab99739156e516f7f6cdf292216b9b9c6
Ensure container doesn't exist from previous run of this script...
Ensuring our volume doesn't exist...
Error response from daemon: get sqldb-data: no such volume
Creating SQL Server container using volume...
2ec1f8af969fb758ed55429351623ef82c2d167a10afe971df8b2feafd07d92d
Seeding the database...
Not ready yet...
Not ready yet...
Not ready yet...
Not ready yet...
Not ready yet...
Not ready yet...
SQL Server is ready.
Changed database context to 'podcasts'.
(9 rows affected)
Deleting SQL Server container...
sqlserver-withvol
Listing all containers...
CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS    PORTS     NAMES
Listing all volumes...
DRIVER    VOLUME NAME
...
local     sqldb-data
Creating another SQL Server container using the same volume...
925f9225448b0a2cb3bae0ac03a2f3761d32a3ea5770e3638dc7198f24139a9b
Querying the brand new container - the seeded rows should still be there...
Not ready yet...
Not ready yet...
Not ready yet...
Not ready yet...
Total
-----------
          9
```

(`Listing all volumes` 那段本机有四十多个匿名 volume,这里省掉了中间的哈希行。)

第二个容器是全新的,没有跑过任何 seed,`podcasts` 库里的 9 行却直接查得到。
这就是第 3 课最后那句话:volume 的生命周期独立于任何单个容器。

### 3. bind mount:403 到改文件即生效(第 4 课)

把 `html/index.html` 挪走再跑脚本,第 4 课描述的 403 是真的:

```powershell
.\SqlServerBindMountDemo.ps1
curl.exe -s -w "`nHTTP %{http_code}`n" http://localhost:1234
```

```
<html>
<head><title>403 Forbidden</title></head>
<body>
<center><h1>403 Forbidden</h1></center>
<hr><center>nginx/1.31.6</center>
</body>
</html>

HTTP 403
```

把文件放回去,不重启容器、不重新构建镜像,直接再 curl 一次:

```
<h1>Hello Dometrain</h1>

Some other change.

HTTP 200
```

nginx 读的就是宿主机上那个目录,所以宿主机上的改动对容器立刻可见。

### 4. 备份与恢复(第 6 课)

```powershell
.\VolumeBackup.ps1
```

```
Delete volumes if they already exist...
Error response from daemon: get BackupDemoVolume: no such volume
Error response from daemon: get RestoredVolume: no such volume
Create volume...
BackupDemoVolume
Write file to our volume...
Create backup of volume...
hello.txt
Restore backup...
hello.txt
Reading hello.txt back out of RestoredVolume...
Hello
```

`tar cvf` 和 `tar xvf` 各自打印的那行 `hello.txt` 就是 `v`(verbose)的输出。
`backup.tar` 落在宿主机的当前目录上,2048 字节,容器退出之后依然在,因为它写的是 bind mount 那一侧。

### 5. 匿名 volume 与 VOLUME 指令(第 7 课)

第 7 课的两条产生路径都能量出来。
`-v` 只给容器路径:

```powershell
$before = (docker volume ls -q).Count
docker run -v /mydata alpine true
$after = (docker volume ls -q).Count
"volumes: $before -> $after"
```

```
volumes: 45 -> 46
```

顺带一个课程没提但值得记的点:同样一条命令加上 `--rm`,匿名 volume 会跟着容器一起被删,数量不变。

```
volumes: 46 -> 46
```

`VOLUME` 指令那条路径,本章用的 azure-sql-edge 镜像自己就声明了三个:

```powershell
docker image inspect mcr.microsoft.com/azure-sql-edge:latest --format '{{json .Config.Volumes}}'
```

```
{"/var/opt/mssql-extensibility":{},"/var/opt/mssql-extensibility/data":{},"/var/opt/mssql-extensibility/log":{}}
```

所以每起一个这样的容器就多三个哈希,而且 `docker rm -f` 之后它们不会跟着走:

```
volumes: 46 -> 49
after docker rm -f (no -v): 49
```

上面第 2 节那份 `docker volume ls` 里四十多个哈希就是这么攒出来的。
这正是第 7 课说的"更像备份机制而不是主要的数据管理策略":数据确实没丢,但你很难说清哪个哈希是哪一次运行留下的。

### 清理

```powershell
docker rm -f sqlserver-withvol nginx-withvol
docker volume rm sqldb-data BackupDemoVolume RestoredVolume
```

# Interacting with Docker

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 4 章
> 共 3 课 · 约 8:32
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Docker CLI](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-cli-54123786/) | 3:03 | [↓](#1-docker-cli) |
| 2 | [Docker Desktop GUI](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-desktop-gui-54123787/) | 3:56 | [↓](#2-docker-desktop-gui) |
| 3 | [Visual Studio Code Extension](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/visual-studio-code-extension-54123788/) | 1:33 | [↓](#3-visual-studio-code-extension) |

## 1. Docker CLI

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-cli-54123786/) · 3:03

### 总结

Docker CLI 是与 Docker engine 交互的主要接口。
本课讲解基本的容器操作,包括运行、列出、停止和删除容器。
它着重强调了 Docker 镜像缓存系统带来的效率:一旦底层镜像已经存在于本地机器上,容器几乎可以瞬间启动。

### 核心概念

- `docker` 命令及其内置的帮助系统。
- 在 `docker run` 过程中隐式拉取镜像。
- 容器生命周期管理:列出(`ps`)、停止(`stop`)和删除(`rm`)。
- 本地镜像缓存带来的性能优势。

### 课程笔记

通过命令行与 Docker 交互的主要入口是 `docker` 命令。
不带参数运行 `docker`,或者加上 `--help` 标志运行,会给出一份完整的可用命令和管理选项清单。
虽然 Docker 自带用于容器编排的 Swarm,但它在业界很大程度上已被 Kubernetes 取代,也不是本次培训的重点。

要启动一个容器,使用 `docker run` 命令,后面跟上镜像名称。
如果本地没有该镜像,Docker 会隐式执行一次 `docker pull`,先下载所需的层,然后再启动容器。

```bash
docker run nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-cli-54123786/?t=70)

容器运行起来之后,你可以用下面这几个关键命令管理它的生命周期:
- `docker ps`:列出当前所有正在运行的容器,显示它们的 Container ID、镜像、状态和分配到的名称。
- `docker stop <ID>`:优雅地停止一个正在运行的容器,但不删除它。
- `docker ps -a`:列出所有容器,包括当前已停止或已退出的容器。
- `docker rm <ID>`:把一个已停止的容器从系统中永久删除。

```shell
C:\   0.001s
12:03:28 > docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED          STATUS          PORTS     NAMES
631029269cd6   nginx     "/docker-entrypoint...."   39 seconds ago   Up 38 seconds   80/tcp    hungry_sinoussi
C:\   0.125s
12:05:14 > docker stop 631029269cd6
631029269cd6
C:\   0.622s
12:05:25 > docker ps
CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS    PORTS     NAMES
C:\   0.125s
12:05:31 > docker ps -a
CONTAINER ID   IMAGE     COMMAND                  CREATED          STATUS                     PORTS     NAMES
631029269cd6   nginx     "/docker-entrypoint...."   59 seconds ago   Exited (0) 9 seconds ago             hungry_si
C:\   0.125s
12:05:35 > docker rm 631029269cd6
631029269cd6
C:\   0.133s
12:05:42 >
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-cli-54123786/?t=100)

相较于虚拟机,容器的一大显著优势是启动速度。
由于 Docker 在初次下载之后会把镜像缓存在本地,后续针对同一镜像执行的 `docker run` 命令几乎可以瞬间完成,因为系统只需要从已有的缓存创建一个新的容器实例。

```shell
C:\   0.001s
12:05:47 > docker run nginx
/docker-entrypoint.sh: /docker-entrypoint.d/ is not empty, will attempt to perform configuration
/docker-entrypoint.sh: Looking for shell scripts in /docker-entrypoint.d/
/docker-entrypoint.sh: Launching /docker-entrypoint.d/10-listen-on-ipv6-by-default.sh
10-listen-on-ipv6-by-default.sh: info: Getting the checksum of /etc/nginx/conf.d/default.conf
10-listen-on-ipv6-by-default.sh: info: Enabled listen on IPv6 in /etc/nginx/conf.d/default.conf
/docker-entrypoint.sh: Sourcing /docker-entrypoint.d/15-local-resolvers.envsh
/docker-entrypoint.sh: Launching /docker-entrypoint.d/20-envsubst-on-templates.sh
/docker-entrypoint.sh: Launching /docker-entrypoint.d/30-tune-worker-processes.sh
/docker-entrypoint.sh: Configuration complete; ready for start up
2023/06/27 11:05:51 [notice] 1#1: using the "epoll" event method
2023/06/27 11:05:51 [notice] 1#1: nginx/1.25.1
2023/06/27 11:05:51 [notice] 1#1: built by gcc 12.2.0 (Debian 12.2.0-14)
2023/06/27 11:05:51 [notice] 1#1: OS: Linux 5.10.102.1-microsoft-standard-WSL2
2023/06/27 11:05:51 [notice] 1#1: getrlimit(RLIMIT_NOFILE): 1048576:1048576
2023/06/27 11:05:51 [notice] 1#1: start worker processes
2023/06/27 11:05:51 [notice] 1#1: start worker process 29
2023/06/27 11:05:51 [notice] 1#1: start worker process 30
2023/06/27 11:05:51 [notice] 1#1: start worker process 31
2023/06/27 11:05:51 [notice] 1#1: start worker process 32
2023/06/27 11:05:51 [notice] 1#1: start worker process 33
2023/06/27 11:05:51 [notice] 1#1: start worker process 34
2023/06/27 11:05:51 [notice] 1#1: start worker process 35
2023/06/27 11:05:51 [notice] 1#1: start worker process 36
2023/06/27 11:05:51 [notice] 1#1: start worker process 37
2023/06/27 11:05:51 [notice] 1#1: start worker process 38
2023/06/27 11:05:51 [notice] 1#1: start worker process 39
2023/06/27 11:05:51 [notice] 1#1: start worker process 40
2023/06/27 11:05:51 [notice] 1#1: start worker process 41
2023/06/27 11:05:51 [notice] 1#1: start worker process 42
2023/06/27 11:05:51 [notice] 1#1: start worker process 43
2023/06/27 11:05:51 [notice] 1#1: start worker process 44
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-cli-54123786/?t=160)

## 2. Docker Desktop GUI

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/docker-desktop-gui-54123787/) · 3:56

### 总结

Docker Desktop 提供了一套完整的图形界面来管理 Docker 的生命周期,是命令行界面之外的一种可视化选择。
它让开发者能够监控容器性能、检查镜像层、访问容器 shell,并从 Docker Hub 快速部署第三方软件,同时还提供了用于守护进程维护和本地 Kubernetes 编排的内置工具。

### 核心概念

- **Container Management**:可视化地跟踪容器状态、ID 和基础镜像。
- **Resource Monitoring**:CPU、内存、磁盘 I/O 和网络访问的实时统计数据。
- **Image Inspection**:浏览镜像的层级结构和各个层。
- **Integrated Terminal**:无需外部终端模拟器,直接访问运行中容器的 shell。
- **Daemon Maintenance**:用于重启引擎、清除数据或恢复出厂设置的工具。
- **Kubernetes**:内置支持本地单节点集群。

### 课程笔记

#### Navigation and Container Management

Docker Desktop GUI 把资源分成三个主要类别:Containers、Images 和 Volumes。
Containers 视图以可视化方式呈现通常由 `docker ps` 命令返回的数据,包括容器名称、ID、基础镜像和当前状态。
与 CLI 不同,GUI 为容器名和镜像名提供了超链接,便于深入查看细节。

深入查看某个具体容器时,界面会提供以下几个标签页:
- **Logs**:查看标准输出流和标准错误流。
- **Inspect**:查看底层的配置和状态数据。
- **Terminal**:进入容器内的交互式 shell 来执行命令。
- **Files**:浏览容器的内部文件系统。
- **Stats**:实时监控资源消耗,包括 CPU、内存、磁盘和网络使用情况。

#### Image Management and Deployment

Images 视图列出本地机器上存储的所有镜像。
选中一个镜像会显示它的层级结构和层的组成。
对于活动中的容器,GUI 提供了快捷操作按钮,可以停止、暂停、重启或删除该实例。
一个特别有用的功能是可以复制用于启动某个容器的那条具体的 `docker run` 命令,然后把它用在自动化脚本里。

界面顶部的全局搜索栏既可以搜索本地镜像,也可以搜索 Docker Hub 上的远程镜像。
这使得快速部署成为可能;举例来说,搜索像 RabbitMQ 这样的第三方镜像并点击 "Run",就会自动拉取该镜像并实例化一个容器。

#### Global Monitoring and Settings

Docker Desktop 包含一个仪表盘,用于查看所有运行中容器的资源使用汇总图表。
它还带有一个 Learning Center,可作为官方文档的快速参考。

在维护方面,"Bug" 图标提供了一个用于排查 Docker 守护进程问题的菜单。
在这里,用户可以:
- 重启 Docker engine。
- 把环境重置为出厂默认设置。
- 清除数据和镜像以回收磁盘空间。

"Cog" 图标会打开设置菜单,用户可以在其中配置引擎并启用内置的本地 Kubernetes 环境。
启用该功能后,开发机上就直接拥有了一个单节点的 Kubernetes 集群。

## 3. Visual Studio Code Extension

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/visual-studio-code-extension-54123788/) · 1:33

### 总结

Visual Studio Code 的 Docker 扩展提供了一套完整的图形界面,可以直接在 IDE 内部管理 Docker 资源。
它让开发者能够监控容器、浏览容器文件系统、查看日志,并通过附加的 shell 与容器交互,同时还提供了检查容器元数据和管理镜像标签的工具。

### 核心概念

- **IDE Integration**:在 VS Code 内部无缝管理 Docker,类似于 JetBrains 或 Visual Studio 中的集成。
- **Container Lifecycle**:用可视化工具查看、深入了解和管理活动的以及已停止的容器。
- **File System Access**:能够浏览运行中容器的内部文件结构。
- **Log and Shell Access**:直接访问容器日志,并可附加一个交互式终端。
- **Container Inspection**:获取特定容器的详细 JSON 元数据。
- **Image Management**:查看本地镜像及其关联的标签/版本。

### 课程笔记

虽然 Docker 主要通过 CLI 来管理,但大多数现代 IDE,包括 Visual Studio Code、Visual Studio 和 JetBrains 系列产品,都提供了功能强大的 Docker 扩展。
在 VS Code 中,Docker 扩展会在侧边栏添加一个专用图标,提供本地 Docker 环境的集中视图。

#### Container Management

该扩展提供容器的层级视图。
用户可以展开一个容器来浏览它的内部文件系统,或者右键点击来执行管理任务。
主要功能包括查看日志和附加 shell。
附加 shell 会在容器内部打开一个交互式终端会话,可以在其中执行诸如 `ls` 之类的命令来查看目录结构。

```shell
lrwxrwxrwx   1 root root     10 Jun  5 14:02 libx32 -> usr/libx32
drwxr-xr-x   2 root root   4096 Jun  5 14:02 media
drwxr-xr-x   2 root root   4096 Jun  5 14:02 mnt
drwxr-xr-x   1 root root   4096 Jun 21 20:49 opt
lrwxrwxrwx   1 root root     21 Jun 21 20:50 plugins -> /opt/rabbitmq/plugins
dr-xr-xr-x 315 root root      0 Jun 27 11:10 proc
drwx------   1 root root   4096 Jun 21 20:49 root
drwxr-xr-x   5 root root   4096 Jun  5 14:05 run
lrwxrwxrwx   1 root root      8 Jun  5 14:02 sbin -> usr/sbin
drwxr-xr-x   2 root root   4096 Jun  5 14:02 srv
dr-xr-xr-x  11 root root      0 Jun 27 11:10 sys
drwxrwxrwt   1 root root   4096 Jun 21 20:49 tmp
drwxr-xr-x   1 root root   4096 Jun  5 14:02 usr
drwxr-xr-x   1 root root   4096 Jun  5 14:05 var
#
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/visual-studio-code-extension-54123788/?t=55)

"Inspect" 功能会生成一份 JSON 文档,其中包含容器完整的配置和状态,包括它的 ID、创建路径、参数和当前状态。

```json
{
    "Id": "e9fd0668fb9744103e8f6b8c40c4d5460e1a99169ddd86eb06096fbb535c6f6f",
    "Created": "2023-06-27T11:10:40.23802224Z",
    "Path": "docker-entrypoint.sh",
    "Args": [
        "rabbitmq-server"
    ],
    "State": {
        "Status": "running",
        "Running": true,
        "Paused": false,
        "Restarting": false,
        "OOMKilled": false,
        "Dead": false,
        "Pid": 9636,
        "ExitCode": 0,
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/visual-studio-code-extension-54123788/?t=70)

#### Image and Tag Management

该扩展也会显示本地镜像。
镜像按名称组织,可以展开以显示具体的标签。
标签代表同一个镜像的不同版本,例如 `latest`。
这让开发者能够高效地管理同名镜像的多个版本。

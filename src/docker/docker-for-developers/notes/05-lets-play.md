# Let’s play!

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 5 章
> 共 5 课 · 约 18:49
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123798/) | 0:48 | [↓](#1-introduction) |
| 2 | [Port mapping](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/) | 2:23 | [↓](#2-port-mapping) |
| 3 | [Detached mode and logs](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/) | 2:50 | [↓](#3-detached-mode-and-logs) |
| 4 | [Shell access and makes changes inside containers](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/) | 5:23 | [↓](#4-shell-access-and-makes-changes-inside-containers) |
| 5 | [More example 3rd party images](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/) | 7:25 | [↓](#5-more-example-3rd-party-images) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123798/) · 0:48

### 总结

本课引入一个动手模块,聚焦于 Docker CLI 的实践经验。
它概述了接下来的主题,例如端口映射、访问容器 shell 以及管理镜像标签。
主要目标是演示:无需在本地安装依赖,就能轻松运行来自 Docker Hub 的第三方软件。

### 核心概念

* 动手实践 Docker CLI
* 让容器可被访问的端口映射
* 在容器内进行交互式 shell 访问
* 理解镜像标签
* 通过 Docker Hub 运行第三方软件

### 课程笔记

本模块强调对 Docker 命令行界面(CLI)的实际应用与试验。
建议直接动手敲这些命令,以强化学习过程并确保长期记住 Docker 的概念。

接下来会讲解若干进阶主题,在基础的容器管理之上做扩展:

* **Port Mapping**:这项技术通过把宿主机端口映射到容器端口,让外部能够访问运行在容器内的服务。
* **Container Shell Access**:将探讨在运行中的容器内获取命令行界面(CLI)的方法,这对调试和手动配置至关重要。
* **Image Tags**:本模块会讲解如何使用和管理镜像标签,以指定 Docker 镜像的不同版本或变体。

本模块最后会演示如何使用取自 Docker Hub 的镜像运行各种第三方应用。
这凸显了 Docker 的一个核心优势:无需在宿主机上执行传统安装,就能在隔离环境中运行复杂软件及其依赖。

## 2. Port mapping

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/) · 2:23

### 总结

端口映射是把运行在 Docker 容器内的网络服务暴露给宿主机的机制。
默认情况下,容器与宿主机网络是隔离的,这意味着它们的内部端口无法从宿主机访问。
在 docker run 命令中使用 -p(或 --publish)标志,开发者就能弥合这道鸿沟:把宿主机上的某个端口映射到容器内的某个端口,从而让 web 服务器或消息代理之类的应用可以被外部访问。

### 核心概念

* **Network Isolation**:容器默认不会把端口暴露给宿主机。
* **The -p Flag**:与 `docker run` 一起使用,把容器端口发布到宿主机;它代表 "publish"。
* **Mapping Syntax**:定义为 `host_port:container_port`。宿主机端口是外部访问点,容器端口是内部监听端口。
* **Multiple Mappings**:通过多次使用 `-p` 标志,单个容器可以暴露多个端口。
* **Protocol Verification**:映射端口能让流量抵达容器,但客户端必须使用正确的协议(例如 HTTP 与 AMQP)才能成功与服务通信。

### 课程笔记

当一个容器在没有显式网络配置的情况下启动时,它与宿主机保持隔离。
例如,运行一个没有端口映射的 Nginx 容器,结果就是在宿主机上无法通过 `localhost` 访问该服务。

```bash
docker run nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/?t=10)

要让服务可访问,请使用 `-p`(或 `--publish`)标志。
这个标志把宿主机端口映射到容器端口。
语法是 `host_port:container_port`。
对于默认监听 80 端口的 Nginx,你可以把宿主机的 80 端口映射到容器的 80 端口,从而可以通过标准的 web 浏览器访问。

```bash
docker run -p 80:80 nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/?t=45)

宿主机端口不需要与容器的内部端口一致。
你可以指定宿主机上任意可用的端口,映射到容器的监听端口。
例如,把宿主机的 1234 端口映射到容器的 80 端口,就可以通过 `localhost:1234` 访问该服务。

```bash
docker run -p 1234:80 nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/?t=70)

容器常常运行着需要暴露多个端口的服务。
`-p` 标志可以在同一条 `docker run` 命令中多次使用。
例如在 RabbitMQ 容器中,你可能会映射 5672 端口用于 AMQP 流量,并映射 15672 端口用于管理 web 界面。

```bash
docker run -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/?t=85)

如果你尝试用 web 浏览器访问一个非 HTTP 服务(例如 5672 端口上的 AMQP 端点),浏览器会发送一个该服务无法处理的 HTTP 请求。
虽然这会导致协议错误,但容器日志中出现的这个错误恰恰确认了端口映射工作正常、流量确实到达了容器。

```shell
2023-06-28 11:57:26.346816+00:00 [info] <0.551.0> Recovering 0 queues of type rabbit classic queue took 6ms
2023-06-28 11:57:26.379197+00:00 [info] <0.716.0> Prometheus metrics: HTTP (non-TLS) listener started on port 15692
2023-06-28 11:57:26.379317+00:00 [info] <0.615.0> Ready to start client connection listeners
2023-06-28 11:57:26.380416+00:00 [info] <0.760.0> started TCP listener on [::]:5672
completed with 4 plugins.
2023-06-28 11:57:26.433427+00:00 [info] <0.615.0> Server startup complete; 4 plugins started.
2023-06-28 11:57:26.433427+00:00 [info] <0.615.0>  * rabbitmq_prometheus
2023-06-28 11:57:26.433427+00:00 [info] <0.615.0>  * rabbitmq_management
2023-06-28 11:57:26.433427+00:00 [info] <0.615.0>  * rabbitmq_web_dispatch
2023-06-28 11:57:26.433427+00:00 [info] <0.615.0>  * rabbitmq_management_agent
2023-06-28 11:57:43.443259+00:00 [info] <0.764.0> accepting AMQP connection <0.764.0> (172.17.0.1:51802 -> 172.17.0.2:5672)
2023-06-28 11:57:43.443431+00:00 [error] <0.764.0> closing AMQP connection <0.764.0> (172.17.0.1:51802 -> 172.17.0.2:5672)
2023-06-28 11:57:43.443431+00:00 [error] <0.764.0> {bad_header,<<"GET / HT">>}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/port-mapping-54123799/?t=125)

## 3. Detached mode and logs

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/) · 2:50

### 总结

本课讲解如何管理 Docker 容器的执行模式以及如何访问容器输出。
它涵盖在前台运行容器(会占用终端会话),以及使用后台(detached)模式在后台运行容器。
此外,它还详细说明如何使用 Docker CLI 获取日志、跟随日志流,以及在不中断容器运行的情况下重新连接到运行中的容器。

### 核心概念

- **Foreground Mode**:默认行为,容器日志直接流式输出到终端,并阻塞后续输入。
- **Detached Mode (`-d`)**:在后台运行容器,并把容器 ID 返回到终端。
- **Force Removal (`-f`)**:允许删除一个运行中的容器,而不必先停止它。
- **Container ID Abbreviation**:Docker 允许只用容器 ID 开头的若干唯一字符来引用容器。
- **Docker Logs**:用于获取容器历史输出或流式输出的命令。
- **Docker Attach**:把终端重新连接到运行中容器的标准输出和标准错误流的命令。

### 课程笔记

默认情况下,执行 `docker run` 命令会在前台启动容器。
在这种模式下,容器的日志会占据终端会话,使得该窗口无法再输入其他命令。

```bash
17:05:27 > docker run -p 80:80 nginx
/docker-entrypoint.sh: /docker-entrypoint.d/ is not empty, will attempt to perform configuration
/docker-entrypoint.sh: Looking for shell scripts in /docker-entrypoint.d/
/docker-entrypoint.sh: Launching /docker-entrypoint.d/10-listen-on-ipv6-by-default.sh
10-listen-on-ipv6-by-default.sh: info: Getting the checksum of /etc/nginx/conf.d/default.conf
10-listen-on-ipv6-by-default.sh: info: Enabled listen on IPv6 in /etc/nginx/conf.d/default.conf
/docker-entrypoint.sh: Sourcing /docker-entrypoint.d/15-local-resolvers.envsh
/docker-entrypoint.sh: Launching /docker-entrypoint.d/20-envsubst-on-templates.sh
/docker-entrypoint.sh: Launching /docker-entrypoint.d/30-tune-worker-processes.sh
/docker-entrypoint.sh: Configuration complete; ready for start up
2023/06/28 16:05:41 [notice] 1#1: using the "epoll" event method
2023/06/28 16:05:41 [notice] 1#1: nginx/1.25.1
2023/06/28 16:05:41 [notice] 1#1: built by gcc 12.2.0 (Debian 12.2.0-14)
2023/06/28 16:05:41 [notice] 1#1: OS: Linux 5.10.102.1-microsoft-standard-WSL2
2023/06/28 16:05:41 [notice] 1#1: getrlimit(RLIMIT_NOFILE): 1048576:1048576
2023/06/28 16:05:41 [notice] 1#1: start worker processes
2023/06/28 16:05:41 [notice] 1#1: start worker process 29
2023/06/28 16:05:41 [notice] 1#1: start worker process 30
2023/06/28 16:05:41 [notice] 1#1: start worker process 31
2023/06/28 16:05:41 [notice] 1#1: start worker process 32
2023/06/28 16:05:41 [notice] 1#1: start worker process 33
2023/06/28 16:05:41 [notice] 1#1: start worker process 34
2023/06/28 16:05:41 [notice] 1#1: start worker process 35
2023/06/28 16:05:41 [notice] 1#1: start worker process 36
2023/06/28 16:05:41 [notice] 1#1: start worker process 37
2023/06/28 16:05:41 [notice] 1#1: start worker process 38
2023/06/28 16:05:41 [notice] 1#1: start worker process 39
2023/06/28 16:05:41 [notice] 1#1: start worker process 40
2023/06/28 16:05:41 [notice] 1#1: start worker process 41
2023/06/28 16:05:41 [notice] 1#1: start worker process 42
2023/06/28 16:05:41 [notice] 1#1: start worker process 43
2023/06/28 16:05:41 [notice] 1#1: start worker process 44
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/?t=10)

当容器在前台运行时,你可以用另一个终端来管理它。
`docker ps` 命令会列出活跃的容器。
要删除一个当前正在运行的容器,请使用 `docker rm -f`。
注意,在 CLI 中指定容器 ID 时,只要开头的几个字符是唯一的,你就只需要提供这几个字符。

```bash
17:05:24 > docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED          STATUS
2385b5928773   nginx     "/docker-entrypoint.…"   12 seconds ago   Up 12 seconds
17:05:53 > docker rm -f 238
238
17:06:13 > docker ps
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/?t=25)

要让容器运行时不占用终端,请添加 `-d`(或 `--detached`)标志以使用后台模式。
这会在后台运行容器,并立即把完整的容器 ID 返回到终端。

```bash
17:06:36 > docker run -d -p 80:80 nginx
3508083b270a8ddee8cc6e51a59b9bc351718f369a8be9ca0381c52950792878
17:06:52 >
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/?t=70)

即使容器处于后台模式,你仍然可以使用 `docker logs` 命令访问它的输出。
默认情况下,该命令会输出当前的日志历史,然后把控制权交还给终端。

```bash
17:06:52 > docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED         STATUS         PORTS                NAMES
3508083b270a   nginx     "/docker-entrypoint.…"   4 seconds ago   Up 4 seconds   0.0.0.0:80->80/tcp   focused_gould
17:06:56 > docker logs 350
/docker-entrypoint.sh: /docker-entrypoint.d/ is not empty, will attempt to perform configuration
/docker-entrypoint.sh: Looking for shell scripts in /docker-entrypoint.d/
/docker-entrypoint.sh: Launching /docker-entrypoint.d/10-listen-on-ipv6-by-default.sh
10-listen-on-ipv6-by-default.sh: info: Getting the checksum of /etc/nginx/conf.d/default.conf
10-listen-on-ipv6-by-default.sh: info: Enabled listen on IPv6 in /etc/nginx/conf.d/default.conf
/docker-entrypoint.sh: Sourcing /docker-entrypoint.d/15-local-resolvers.envsh
/docker-entrypoint.sh: Launching /docker-entrypoint.d/20-envsubst-on-templates.sh
/docker-entrypoint.sh: Launching /docker-entrypoint.d/30-tune-worker-processes.sh
/docker-entrypoint.sh: Configuration complete; ready for start up
2023/06/28 16:06:52 [notice] 1#1: using the "epoll" event method
2023/06/28 16:06:52 [notice] 1#1: nginx/1.25.1
2023/06/28 16:06:52 [notice] 1#1: built by gcc 12.2.0 (Debian 12.2.0-14)
2023/06/28 16:06:52 [notice] 1#1: OS: Linux 5.10.102.1-microsoft-standard-WSL2
2023/06/28 16:06:52 [notice] 1#1: getrlimit(RLIMIT_NOFILE): 1048576:1048576
2023/06/28 16:06:52 [notice] 1#1: start worker processes
2023/06/28 16:06:52 [notice] 1#1: start worker process 29
2023/06/28 16:06:52 [notice] 1#1: start worker process 30
2023/06/28 16:06:52 [notice] 1#1: start worker process 31
2023/06/28 16:06:52 [notice] 1#1: start worker process 32
2023/06/28 16:06:52 [notice] 1#1: start worker process 33
2023/06/28 16:06:52 [notice] 1#1: start worker process 34
2023/06/28 16:06:52 [notice] 1#1: start worker process 35
2023/06/28 16:06:52 [notice] 1#1: start worker process 36
2023/06/28 16:06:52 [notice] 1#1: start worker process 37
2023/06/28 16:06:52 [notice] 1#1: start worker process 38
2023/06/28 16:06:52 [notice] 1#1: start worker process 39
2023/06/28 16:06:52 [notice] 1#1: start worker process 40
2023/06/28 16:06:52 [notice] 1#1: start worker process 41
2023/06/28 16:06:52 [notice] 1#1: start worker process 42
2023/06/28 16:06:52 [notice] 1#1: start worker process 43
2023/06/28 16:06:52 [notice] 1#1: start worker process 44
17:07:15 >
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/?t=85)

`docker logs` 命令支持若干选项来定制输出。
`-f`(或 `--follow`)标志会实时流式输出日志。
与前台模式不同,在跟随日志时按 `Ctrl+C` 会停止日志流,但**不会**停止容器本身。
其他有用的标志包括 `-n`(或 `--tail`),用于指定从日志末尾显示的行数,以及 `-t`(或 `--timestamps`),用于显示日志时间。

```bash
17:07:24 > docker logs --help

Usage:  docker logs [OPTIONS] CONTAINER

Fetch the logs of a container

Aliases:
  docker container logs, docker logs

Options:
      --details        Show extra details provided to logs
  -f, --follow         Follow log output
      --since string   Show logs since timestamp (e.g.
                       "2013-01-02T13:23:37Z") or relative (e.g. "42m"
                       for 42 minutes)
  -n, --tail string    Number of lines to show from the end of the logs
                       (default "all")
  -t, --timestamps     Show timestamps
      --until string   Show logs before a timestamp (e.g.
                       "2013-01-02T13:23:37Z") or relative (e.g. "42m"
                       for 42 minutes)

17:07:28 > docker logs 350
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/?t=100)

最后,`docker attach` 允许你把终端重新连接到运行中容器的进程。
它的行为类似于不带 `-d` 标志在前台运行容器,让你能看到新产生的输出。

```shell
C:\ 0.001s
17:07:53 > docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED          STATUS
3508083b270a   nginx     "/docker-entrypoint. ..."   About a minute ago   Up About a
C:\ 0.127s
17:07:57 > docker attach 350
172.17.0.1 - - [28/Jun/2023:16:08:34 +0000] "GET / HTTP/1.1" 200 615 "-" "Mozilla
rome/114.0.0.0 Safari/537.36" "-"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/detached-mode-and-logs-54123800/?t=130)

## 4. Shell access and makes changes inside containers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/) · 5:23

### 总结

本课演示如何使用 model context protocol 和命令行工具与运行中的 Docker 容器交互。
它涵盖通过 docker exec 访问容器的 shell、使用包管理器在容器内安装软件,以及修改内部文件。
本课还探讨了容器隔离,展示对运行中容器所做的更改不会影响原始镜像或其他容器,并介绍了用于把容器更改持久化为新镜像的 docker commit 命令。

### 核心概念

*   使用 `docker exec` 在运行中的容器里执行命令。
*   使用 `-it` 标志获得交互式终端访问。
*   使用 `apt-get` 在运行中的容器内安装工具(例如 `vim`)。
*   理解容器隔离:更改只作用于特定的容器实例。
*   使用 `docker commit` 把容器状态持久化为新镜像。

### 课程笔记

要开始与容器交互,首先以后台模式启动一个实例。
在这个例子中,启动了一个 Nginx 容器,并把 80 端口映射到宿主机。

```bash
15:31:51 > docker run -d -p 80:80 nginx
a6b3834057a4a4ad64bccb1f290f951bce6f0131233305e2d8ed8a8d6943e52dd0
15:32:47 > docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED         STATUS         PORTS
a6b3834057a4   nginx     "/docker-entrypoint..."   7 seconds ago   Up 6 seconds   0.0.0.0:80->
15:32:54 > docker exec -it a6b ls
bin  dev                     docker-entrypoint.sh  home  lib32  libx32  mnt  proc  run  srv  t
boot docker-entrypoint.d  etc                   lib   lib64  media   opt  root  sbin  sys  u
15:33:16 > docker exec -it a6b pwd
/
15:33:20 > docker exec -it a6b bash
root@a6b3834057a4:/#
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=10)

要在运行中的容器内执行命令,请使用 `docker exec`。
常用的是 `-it` 标志,其中 `i` 代表 interactive(交互),`t` 代表 TTY(终端)。
这让你可以与进程交互。
你可以运行 `ls` 或 `pwd` 这样的一次性命令,也可以通过指定 `bash` 启动一个交互式 shell 会话。
如果某个镜像特别轻量、不包含 `bash`,你应该改试 `sh`。

进入 shell 之后,你就可以浏览容器的文件系统。
Nginx 把它默认的 HTML 文件存放在 `/usr/share/nginx/html`。
由于大多数 Docker 镜像都为体积做过优化,它们通常不带 `vim` 之类的文本编辑器。
因为 Nginx 镜像是基于 Debian 的,你可以使用 `apt-get` 安装工具。

```bash
15:33:20 > docker exec -it a6b bash
root@a6b3834057a4:/# ls
bin  dev                     docker-entrypoint.sh  home  lib32  libx32  mnt  proc  run  srv  t
boot docker-entrypoint.d  etc                   lib   lib64  media   opt  root  sbin sys  u
root@a6b3834057a4:/# cd /usr/share/nginx/html
root@a6b3834057a4:/usr/share/nginx/html# ls
50x.html  index.html
root@a6b3834057a4:/usr/share/nginx/html# vim index.html
bash: vim: command not found
root@a6b3834057a4:/usr/share/nginx/html# apt-get update && apt-get install -y vim
Get:1 http://deb.debian.org/debian bookworm InRelease [147 kB]
Get:2 http://deb.debian.org/debian bookworm-updates InRelease [52.1 kB]
Get:3 http://deb.debian.org/debian-security bookworm-security InRelease [48.0 kB]
Get:4 http://deb.debian.org/debian bookworm/main amd64 Packages [8904 kB]
Get:5 http://deb.debian.org/debian-security bookworm-security/main amd64 Packa~~ [21.6 kB]
96% [4 Packages store 0 B]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=115)

安装 `vim` 之后,你就可以修改 `index.html` 文件。

```html
<!DOCTYPE html>
<html>
<head>
<title>Welcome to nginx!</title>
<style>
html { color-scheme: light dark; }
body { width: 35em; margin: 0 auto;
font-family: Tahoma, Verdana, Arial, sans-serif; }
</style>
</head>
<body>
<h1>Welcome to nginx!</h1>
</body>
</html>
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=160)

```bash
update-alternatives: using /usr/bin/vim.basic to provide /usr/bin/vim (vim) in auto mode
update-alternatives: using /usr/bin/vim.basic to provide /usr/bin/vimdiff (vimdiff) in auto mode
Processing triggers for libc-bin (2.36-9) ...
root@a6b3834057a4:/usr/share/nginx/html# ls
50x.html  index.html
root@a6b3834057a4:/usr/share/nginx/html# vim index.html
root@a6b3834057a4:/usr/share/nginx/html#
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=175)

需要理解的一点是,这些更改只存在于那个特定的运行中容器里。
它们不会影响底层的 `nginx` 镜像。
如果你用同一个镜像在另一个端口(例如 1234)上启动第二个容器,它显示的会是原始的 Nginx 默认页面,而不是修改后的版本。

```bash
15:34:51 > docker run -d -p 1234:80 nginx
aab54a0d81ecf1d58b1c3872e7cf8f9ae026d4429cbae8876e917d448f036039
15:35:04 > docker ps
CONTAINER ID   IMAGE     COMMAND                  CREATED          STATUS          POR
aab54a0d81ec   nginx     "/docker-entrypoint..."   2 seconds ago    Up 2 seconds    0.0
a6b3834057a4   nginx     "/docker-entrypoint..."   2 minutes ago    Up 2 minutes    0.0
15:35:06 > docker commit
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=205)

虽然这在生产流程中并非标准做法(生产中更倾向使用 Dockerfile),但你可以使用 `docker commit` 把对容器所做的更改持久化为一个新镜像。
这会基于容器的当前状态创建一个本地镜像。

```shell
15:35:06 > docker commit a6b3834057a4 nginx-modified
sha256:fb540ec780605aa2e00f19f1570c5090942054e04e3b4cfeea097375abd19216
15:35:51 > docker run -d -p 1235:80 nginx-modified
24748dbb09d1471884a317c0fa6b83edf187311690266909eeee91e8cb9c9145
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=250)

提交之后,你可以看到三个不同的容器:原来那个被修改过的容器、一个来自原始镜像的干净容器,以及第三个由包含这些更改的新 `nginx-modified` 镜像运行起来的容器。

```shell
15:36:03 > docker ps
CONTAINER ID   IMAGE            COMMAND                  CREATED          STATUS
24748dbb09d1   nginx-modified   "/docker-entrypoint. ..."   32 seconds ago   Up
aab54a0d81ec   nginx            "/docker-entrypoint. ..."   About a minute   Up
a6b3834057a4   nginx            "/docker-entrypoint. ..."   3 minutes ago    Up
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/shell-access-and-makes-changes-inside-containers-54123801/?t=295)

## 5. More example 3rd party images

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/) · 7:25

### 总结

本课演示使用 Docker 运行第三方软件的实际应用,包括 SQL Server、Grafana 和 WordPress。
它讲解了如何与 Docker Hub 之外的外部镜像仓库交互、如何使用环境变量配置容器,以及如何解决诸如容器命名冲突之类的常见运行时问题。
借助 Docker,开发者可以保持宿主机环境的整洁,按需启动数据库和仪表盘工具等复杂依赖,而无需在本地做永久性安装。

### 核心概念

- **Image Registries**:Docker Hub 是默认的仓库,但镜像也可以托管在其他域名上,例如 Microsoft Container Registry(`mcr.microsoft.com`)或私有的 Azure Container Registries(ACR)。
- **Registry Prefixing**:如果镜像名前面没有加域名前缀,Docker 会假定它位于 Docker Hub 上。外部仓库必须在镜像名中显式加上前缀。
- **Environment Variables**:`-e` 标志用于向容器传递必需的配置,例如接受许可协议或管理员密码。
- **Container Naming Conflicts**:Docker 要求容器名唯一。如果某个名字已被一个已停止或正在运行的容器占用,则必须先删除或重命名它,新容器才能使用这个名字。
- **On-Demand Infrastructure**:Docker 让开发者可以用针对具体项目的临时容器,取代本地安装的软件(例如 SQL Server、IIS)。

### 课程笔记

#### Working with External Registries

虽然 Docker Hub 是 Docker 的默认仓库,但许多组织把镜像托管在私有或专有的仓库上。
例如,Microsoft 把 SQL Server 镜像托管在他们自己的服务器上。
当镜像托管在 Docker Hub 之外时,镜像名必须加上该仓库的域名作为前缀。

```bash
docker pull mcr.microsoft.com/...
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/?t=40)

如果你使用的是名为 `danclark` 的私有 Azure Container Registry,镜像路径看起来会像 `danclark.azurecr.io/image-name`。
没有这个前缀,Docker 会默认去 Docker Hub 上搜索。

#### Running SQL Server

SQL Server 容器需要特定的环境变量才能初始化。
具体来说,你必须接受最终用户许可协议(EULA)并设置系统管理员(SA)密码。
密码必须满足 SQL Server 的复杂度要求(大写字母、小写字母、数字和符号)。

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-lat

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU1

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-lat
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/?t=70)

容器运行起来之后,你可以使用任意 SQL 客户端(例如 Azure Data Studio)连接它:指向 `localhost` 上映射的端口(1433),使用 `sa` 用户名和配置好的密码。

#### Managing Container Name Conflicts

当使用 `--name` 标志运行容器时,如果该名字已经分配给系统上的另一个容器,即使那个容器当前并未运行,Docker 也会返回错误。

```bash
docker run --name=grafana -p 3000:3000 grafana/grafana
docker: Error response from daemon: Conflict. The container name "/grafana" is already in use by container "0501d1aee33d928dfd
22e819c94ee1ed2". You have to remove (or rename) that container to be able to reuse that name.
See 'docker run --help'.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/?t=235)

要解决这个问题,你必须先使用 `docker rm <container_id_or_name>` 删除已有的容器,然后再尝试用同样的名字运行新容器。

#### Grafana and Observability

Grafana 是一款流行的可观测性仪表盘工具。
它可以被快速启动起来,用于可视化来自各种数据源的数据,包括 SQL Server。

```bash
docker run -d --name=grafana -p 3000:3000 grafana/grafana
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/?t=370)

一个新的 Grafana 实例的默认凭据,用户名和密码通常都是 `admin`。
首次登录时,系统会提示修改密码。

#### WordPress

WordPress 也可以用类似方式部署,不过要完整可用,它通常还需要一个像 MySQL 这样的后端数据库。
对于本地测试,你可以把内部 web 服务器端口(80)映射到本地端口,例如 8080。

```bash
docker run -p 8080:80 wordpress
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/more-example-3rd-party-images-54123802/?t=400)

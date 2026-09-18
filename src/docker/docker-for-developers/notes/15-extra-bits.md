# Extra Bits

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 15 章
> 共 3 课 · 约 15:13
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Under the hood: How containers work in Linux](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/) | 6:26 | [↓](#1-under-the-hood-how-containers-work-in-linux) |
| 2 | [The difference between ENTRYPOINT and CMD in Dockerfile](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/) | 7:29 | [↓](#2-the-difference-between-entrypoint-and-cmd-in-dockerfile) |
| 3 | [Tip: Creating command line aliases](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/tip-creating-command-line-aliases-54124064/) | 1:18 | [↓](#3-tip-creating-command-line-aliases) |

## 1. Under the hood: How containers work in Linux

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/) · 6:26

### 总结

本课探讨支撑容器化的底层 Linux 技术,具体是 namespaces、control groups(cgroups)以及联合文件系统。
通过考察在原生 Linux 主机上运行的一个 Nginx 容器,本课演示了容器并不是虚拟机,而是由 Linux 内核管理的隔离进程。
本课重点说明 Docker 如何利用这些原生特性来提供进程隔离、资源管理和分层文件系统,同时保持从宿主操作系统的可见性。

### 核心概念

- **Namespaces**:为系统资源提供隔离,例如进程 ID(PID)、网络接口和挂载点。
- **Control Groups (cgroups)**:为一组进程管理、设定优先级并限制 CPU 和内存等硬件资源。
- **Union File System**:Docker 分层镜像架构背后的技术,允许多个文件系统叠加在一起。
- **Process Visibility**:容器内运行的进程在宿主的进程列表中可见,但被隔离在它们自己的 namespace 中。
- **Security**:由于容器进程对宿主可见,以 root 身份运行容器对宿主系统有显著的安全影响。

### 课程笔记

#### Process Isolation and Visibility

容器不是虚拟机;它们是运行在宿主 Linux 内核上的隔离进程。
当在原生 Linux 主机上运行容器时,该容器内的进程从宿主的进程表中可见。
例如,启动一个 Nginx 容器,然后在宿主上搜索 Nginx 进程,就会看到 master 进程和 worker 进程。

```bash
dan@Goliath:~$ ps aux | grep nginx
dan         9808  0.0  0.0   4024  1976 pts/0    S+   14:04   0:00 grep --color=auto nginx
dan@Goliath:~$ docker run -d nginx
5715e0793efb5f9a9c74e221d350dd4c0d41c9fde1670726eb01ac2aea2d24c1
dan@Goliath:~$ ps aux | grep nginx
root        9845  0.4  0.0  11376  7868 ?        Ss   14:04   0:00 nginx: master process nginx -g daemon off;
systemd+    9889  0.0  0.0  11844  2872 ?        S    14:04   0:00 nginx: worker process
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=45)

虽然这些进程在宿主上以特定的进程 ID(PID)可见,但从容器内部查看时它们呈现得不一样。
在容器内部,主进程(入口点)被分配 PID 1。

```bash
root@5715e0793efb:/# ps aux
USER       PID %CPU %MEM    VSZ   RSS TTY      STAT START   TIME COMMAND
root         1  0.0  0.0  11376  7868 ?        Ss   13:04   0:00 nginx: master process nginx -g daemon off;
nginx       29  0.0  0.0  11844  2872 ?        S    13:04   0:00 nginx: worker process
root       45  0.0  0.0   4184  3480 pts/0    Ss   13:05   0:00 bash
root      236  0.0  0.0   8108  4108 pts/0    R+   13:06   0:00 ps aux
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=115)

#### Linux Namespaces

Namespaces 是 Linux 用来提供这种隔离的机制。
它们让内核可以向一个进程呈现系统的特定视图。
一个容器由多个 namespace 组成,包括:

- **PID**:隔离进程 ID 编号空间。
- **NET**:隔离网络接口。
- **MNT**:隔离挂载点。
- **UTS**:隔离主机名和域名。
- **IPC**:隔离进程间通信。

宿主 PID 与 namespace PID 之间的映射可以在 `/proc` 文件系统中找到。
通过查看宿主上某个特定进程的 `status` 文件,你可以看到 `NSpid` 字段,它显示该 PID 在其各个 namespace 中的样子。

```plaintext
Name:   nginx
Umask:  0022
State:  S (sleeping)
Tgid:   9889
Ngid:   0
Pid:    9889
PPid:   9845
TracerPid:      0
Uid:    101     101     101     101
Gid:    101     101     101     101
FDSize: 64
Groups: 101
NStgid: 9889    29
NSpid:  9889    29
NSpgid: 9845    1
NSsid:  9845    1
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=190)

你也可以在 `/proc/<PID>/ns` 中列出与某个进程关联的具体 namespace 文件。

```bash
dan@Goliath:/proc/9889/ns$ sudo ls
cgroup  ipc  mnt  net  pid  pid_for_children  user  uts
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=220)

#### Control Groups (cgroups)

namespaces 提供隔离,而 Control Groups(cgroups)提供资源管理。
Cgroups 让系统可以为一个容器限制或设定内存和 CPU 等资源的优先级。
这些是通过 `/sys/fs/cgroup` 目录来管理的。

```bash
dan@Goliath:/sys/fs/cgroup$ ls
9p  bpf  btrfs  cgroup  ext4  fuse  nfs  xfs
dan@Goliath:/sys/fs/cgroup$ cd cgroup
dan@Goliath:/sys/fs/cgroup/cgroup$ ls
blkio   cpuacct  devices  hugetlb  net_cls    perf_event  rdma
cpu     cpuset   freezer  memory   net_prio   pids        unifie
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=250)

Docker 会在这些 cgroup 层级结构中为每个容器创建子目录。
例如,某个特定容器的内存限制和使用统计可以在 `/sys/fs/cgroup/memory/docker/<container_id>` 中找到。

```bash
dan@Goliath:/sys/fs/cgroup/memory/docker/5715e0793efb5f9a9c74e221d350dd4c0d41c9fde1670726eb01ac2aea2d24c1$ ls
cgroup.clone_children         memory.kmem.tcp.max_usage_in_bytes  memory.oom_control
cgroup.event_control          memory.kmem.tcp.usage_in_bytes      memory.pressure_lev
cgroup.procs                  memory.kmem.usage_in_bytes          memory.soft_limj'
memory.failcnt                memory.limit_in_bytes               memory.stat
memory.force_empty            memory.max_usage_in_bytes           memory.swappi
memory.kmem.failcnt           memory.memsw.failcnt                memory.usage
memory.kmem.limit_in_bytes    memory.memsw.limit_in_bytes         memory.use_h
memory.kmem.max_usage_in_bytes memory.memsw.max_usage_in_bytes    notify_on_re
memory.kmem.tcp.failcnt       memory.memsw.usage_in_bytes         tasks
memory.kmem.tcp.limit_in_bytes memory.move_charge_at_immigrate
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=310)

#### Union File System and Security

Docker 还利用联合文件系统(Union File System)来处理镜像分层。
这让基础操作系统、应用文件和配置可以叠加成容器的一个统一的文件系统。

最后,容器进程在宿主上以 root 身份可见这一点,强调了为什么避免以 root 用户运行容器是一项安全最佳实践。
如果一个进程在容器内以 root 运行,那么宿主上对应的进程也归 root 所有。

```shell
dan@Goliath:/$ ps aux | grep nginx
root      9845  0.0  0.0  11376  7868 ?        Ss   14:04   0:00 nginx: master process nginx -g daemon off;
systemd+  9889  0.0  0.0  11844  2872 ?        S    14:04   0:00 nginx: worker process
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/under-the-hood-how-containers-work-in-linux-54124062/?t=355)

## 2. The difference between ENTRYPOINT and CMD in Dockerfile

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/) · 7:29

### 总结

本课讲解 Dockerfile 中 ENTRYPOINT 和 CMD 两条指令各自不同的角色。
ENTRYPOINT 设定容器的主可执行程序,而 CMD 提供可以在运行时轻松覆盖的默认参数。
本课演示这些指令如何相互作用、exec 形式语法的重要性,以及如何使用 --entrypoint 标志绕过容器的默认进程来进行调试。

### 核心概念

*   **ENTRYPOINT**:定义容器启动时运行的主进程或可执行程序。
*   **CMD**:为 ENTRYPOINT 提供默认参数。如果没有定义 ENTRYPOINT,CMD 就充当默认的可执行程序。
*   **Runtime Overrides**:传给 `docker run` 的参数总是覆盖 CMD 指令,但会被追加到 ENTRYPOINT 指令之后。
*   **Exec Form**:推荐的 JSON 数组语法(例如 `["executable", "param"]`),可以避免与特定 shell 相关的问题。
*   **Shell Form**:一种纯字符串语法,通过 `/bin/sh -c` 执行命令,通常不推荐使用。
*   **--entrypoint Flag**:在运行时覆盖 ENTRYPOINT 指令所需的特定 CLI 参数。

### 课程笔记

在 Dockerfile 中,`ENTRYPOINT` 和 `CMD` 有相关但不同的用途。
`ENTRYPOINT` 指定容器启动时执行的主进程。
例如,在一个 .NET 应用中,`ENTRYPOINT` 通常会是 `dotnet` 命令加上应用的 DLL。

```dockerfile
8 #RUN dotnet test "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj"
 9 RUN dotnet publish "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/publish /p:Use
10 
11 FROM mcr.microsoft.com/dotnet/aspnet:7.0
12 EXPOSE 80
13 EXPOSE 443
14 WORKDIR /app
15 COPY --from=build /app/publish .
16 
17 # Change user to non-root (gecos means don't interactively prompt for various info about the
18 RUN adduser --disabled-password --gecos '' appuser
19 USER appuser
20 
21 ENTRYPOINT ["dotnet", "DockerCourseApi.dll"]
22 CMD ["myarg1", "myarg2"]
23 
24 docker run myimage myotherarg
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=25)

`CMD` 指令为 `ENTRYPOINT` 提供默认参数。
如果用户运行 `docker run myimage myotherarg`,`myotherarg` 这个字符串会覆盖 `CMD` 的值(`myarg1`、`myarg2`),但仍然作为参数传给 `ENTRYPOINT`。

如果省略 `ENTRYPOINT`,`CMD` 中的第一个元素就会被当作可执行程序本身。

```dockerfile
8 #RUN dotnet test "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj"
 9 RUN dotnet publish "DockerCourseApi/DockerCourseApi.csproj" -c Release -o /app/publish /p:Use
10 
11 FROM mcr.microsoft.com/dotnet/aspnet:7.0
12 EXPOSE 80
13 EXPOSE 443
14 WORKDIR /app
15 COPY --from=build /app/publish .
16 
17 # Change user to non-root (gecos means don't interactively prompt for various info about the
18 RUN adduser --disabled-password --gecos '' appuser
19 USER appuser
20 
21 CMD ["myarg1", "myarg2"]
22 
23 docker run myimage myotherarg
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=100)

在这种情况下,不带参数运行容器会失败,因为 `myarg1` 不是一个有效的可执行程序。
但如果在运行时提供像 `ls` 这样的命令,就会覆盖 `CMD` 并成功执行,因为 `ls` 在该镜像中是一个有效的可执行程序。

使用一个简单的 `alpine` 基础镜像可以说明这两条指令之间的相互作用:

```dockerfile
FROM alpine

ENTRYPOINT ["echo", "hello"]
CMD ["world"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=220)

不带参数运行这个镜像会输出 `hello world`。
运行 `docker run epcmd universe` 会输出 `hello universe`,因为 `universe` 覆盖了 `CMD`("world")但被追加到了 `ENTRYPOINT`("echo hello")之后。

#### Exec Form vs. Shell Form

这些指令推荐的语法是 **exec form**,它使用 JSON 数组格式。
这确保 Docker 直接执行进程,而不是通过 shell 执行。

```dockerfile
FROM alpine

ENTRYPOINT ["ls"]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=325)

另一种选择是 **shell form**,写成一个纯字符串。

```dockerfile
FROM alpine

ENTRYPOINT ls usr
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=385)

使用 shell form 时,Docker 通过 `/bin/sh -c` 执行命令。
这通常不推荐,因为它会干扰信号(比如 SIGTERM)传递给应用进程的方式。

#### Overriding ENTRYPOINT at Runtime

`CMD` 可以通过在 `docker run` 后面追加参数来轻松覆盖,而 `ENTRYPOINT` 则更持久。
如果你试图在一个有固定 `ENTRYPOINT` 的容器中打开 shell,该 shell 命令只会被当作参数传给那个入口点。

```bash
docker run -it --rm myimage bash
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=415)

要强制使用另一个可执行程序,比如为了调试而使用 shell,你必须使用 `--entrypoint` 标志:

```bash
docker run -it --rm --entrypoint bash myimage
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/the-difference-between-entrypoint-and-cmd-in-dockerfile-54124063/?t=445)

## 3. Tip: Creating command line aliases

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/tip-creating-command-line-aliases-54124064/) · 1:18

### 总结

为 Docker 和 Docker Compose 这类常用工具创建命令行别名,是精简开发工作流的一种非常有效的方式。
通过在 shell 配置文件(例如 PowerShell 或 Bash)中把长命令映射为单字母或双字母的快捷方式,开发者可以减少重复输入并提高命令行效率。
这些别名保留了完整的功能,让用户可以向底层的二进制程序传递标准参数和标志,而不必每次都输入完整的命令名。

### 核心概念

*   **Efficiency:** 减少 `docker` 和 `docker compose` 这类高频命令的字符数以节省时间。
*   **Persistence:** 在 shell 配置文件(例如 PowerShell profile 或 `.bashrc`)中配置别名,以确保它们在每个新的终端会话中都可用。
*   **Argument Support:** 别名能正确处理后面的参数,让 `d ps` 这样的命令与 `docker ps` 功能完全一致。
*   **Tool-agnosticism:** 同样的模式也适用于 Git、Kubernetes、Terraform 和 .NET 等其他常用 CLI 工具。

### 课程笔记

频繁使用 Docker 意味着反复输入 `docker` 命令,以及更长的 `docker compose` 命令。

```shell
docker compose
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/tip-creating-command-line-aliases-54124064/?t=10)

为了优化这一点,你可以在 shell 配置文件中定义别名。
在 PowerShell 中,使用 `set-alias` 命令把一个短名称映射到特定的可执行程序或命令字符串。
例如,把 `d` 映射到 `docker`、`dc` 映射到 `docker-compose`,可以让交互快得多。
虽然在学习阶段可能会把它们注释掉以建立肌肉记忆,但在日常生产使用中强烈推荐它们。

```powershell
set-alias d docker
set-alias dc docker-compose
set-alias tf terraform
set-alias k C:\ProgramData\chocolatey\bin\kubectl.exe
set-alias kubectl C:\ProgramData\chocolatey\bin\kubectl.exe
set-alias v vagrant
set-alias g git
set-alias dn dotnet
set-alias vim nvim
set-alias gvim C:\tools\neovim\nvim-win64\bin\nvim-c
set-alias h helm -Option AllScope
set-alias kn kubens
set-alias kx kubectx
set-alias i istioctl
set-alias winmerge 'C:\Program Files\WinMerge\WinMer
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/tip-creating-command-line-aliases-54124064/?t=40)

一旦这些别名在你的 shell 中生效,你就可以把它们和完整命令名互换使用。
参数会被无缝传递,使得 `d ps` 这样的命令可以列出容器,`dc up` 可以启动一个 compose 项目。

```shell
d ps
CONTAINER ID   IMAGE          COMMAND   CREATED   STATUS   PORTS          NAMES
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/tip-creating-command-line-aliases-54124064/?t=55)

这一做法不止适用于 Docker,也适用于开发生命周期中任何频繁使用的工具。
常见的映射包括:

*   `g` 对应 `git`
*   `k` 对应 `kubectl`(Kubernetes 的主要命令行工具)
*   `dn` 对应 `dotnet`
*   `tf` 对应 `terraform`

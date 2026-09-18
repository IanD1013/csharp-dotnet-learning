# Docker Networking

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 13 章
> 共 7 课 · 约 27:27
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123950/) | 0:56 | [↓](#1-introduction) |
| 2 | [Default bridge network](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/) | 4:14 | [↓](#2-default-bridge-network) |
| 3 | [Custom bridge networks](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/) | 6:46 | [↓](#3-custom-bridge-networks) |
| 4 | [Networking on Docker Compose](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/) | 6:49 | [↓](#4-networking-on-docker-compose) |
| 5 | [Host network](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/) | 4:43 | [↓](#5-host-network) |
| 6 | [Leveraging host.docker.internal](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-hostdockerinternal-54123955/) | 0:59 | [↓](#6-leveraging-hostdockerinternal) |
| 7 | [Summary of other network types](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/summary-of-other-network-types-54123957/) | 3:00 | [↓](#7-summary-of-other-network-types) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123950/) · 0:56

### 总结

本课介绍 Docker 网络,强调虽然这个主题可能很复杂,但开发者通常只需要理解基础知识,因为 Docker 已经自动完成了大部分配置。
它为接下来探索默认网络行为以及容器之间如何通信做好了铺垫。

### 核心概念

- **Abstraction of Complexity**:Docker 自动完成了大部分网络相关的工作,使开发者可以专注于应用逻辑。
- **Developer-Focused Fundamentals**:对于常规开发任务来说,对网络有基本的理解就已经足够。
- **Default Networking**:除非另行指定,使用标准的 `docker run` 命令启动的容器会被分配到一个默认网络中。
- **Host Networking**:一种特定的网络模式,容器在其中共享宿主机的网络命名空间。

### 课程笔记

Docker 网络是一个内容相当全面的主题,可以涉及很深的层次。
不过,对于常规的开发者任务而言,往往没有必要深入钻研网络协议的细节,因为 Docker 在幕后管理了大部分配置。

在许多开发场景中,只需要极少的网络知识就能操作容器。
本课程之前的示例显式使用过 host 网络,但大多数操作依赖的是 Docker 自动提供的默认设置。
理解这些基础知识对于排查问题和处理更复杂的配置仍然很有价值。

本章的重点是开发者必须掌握的网络知识。
对于更高级的场景,例如管理一个跨多台设备运行多个 Docker 实例的家庭网络,Docker 官方文档提供了详细的教程和指南。

理解 Docker 网络的下一步,是考察执行标准 `docker run` 命令时所发生的默认网络行为。

## 2. Default bridge network

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/) · 4:14

### 总结

默认的 bridge 网络是 Docker 开箱即用提供的标准网络环境。
它允许容器之间通过 IP 地址相互通信,但缺少自定义 bridge 网络中那些高级特性,例如基于 DNS 的自动服务发现。
本课介绍如何检查默认 bridge、识别容器的 IP 地址,以及使用 ping 等标准工具验证容器之间的连通性。

### 核心概念

- **Default Networks**:Docker 初始化时带有三个默认网络:`bridge`、`host` 和 `none`。
- **Network Inspection**:`docker network inspect` 命令提供关于某个网络的详细 JSON 元数据,包括它的子网、网关以及已连接的容器。
- **Automatic Attachment**:除非在创建时指定了其他网络,否则容器会自动接入默认的 `bridge` 网络。
- **IP-based Communication**:同一个 bridge 网络中的容器可以使用各自被分配的内部 IP 地址进行通信。
- **Limitations**:默认的 bridge 网络不支持基于 DNS 的自动服务发现,也不会像自定义网络那样在不发布端口的情况下自动把端口暴露给其他容器。

### 课程笔记

Docker 为网络提供了一组管理命令,与用于 volumes 的那些命令类似。
你可以使用 `docker network ls` 列出宿主机上可用的网络。

```shell
docker network ls
NETWORK ID     NAME      DRIVER    SCOPE
9991fbb57800   bridge    bridge    local
80c667f6be02   host      host      local
4ec5fd75797c   none      null      local
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/?t=10)

默认情况下,Docker 包含三个网络。
`bridge` 网络是本课的重点,它是所有新建容器的默认网络。
要查看该网络的技术细节,例如网关和子网,可以使用 `inspect` 命令。

```json
[
    {
        "Name": "bridge",
        "Id": "9991fbb578005a13a3cb0494c1f784a5ac8025fe9362296d17db28f1dcebbb4f",
        "Driver": "bridge",
        "IPAM": {
            "Driver": "default",
            "Config": [
                {
                    "Subnet": "172.17.0.0/16",
                    "Gateway": "172.17.0.1"
                }
            ]
        },
        "Containers": {},
        "Options": {
            "com.docker.network.bridge.default_bridge": "true",
            "com.docker.network.bridge.name": "docker0"
        }
    }
]
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/?t=55)

当你在运行容器时没有指定网络,它会自动连接到这个 bridge。
例如,启动两个 Nginx 容器就会把该网络的容器列表填充起来。

```shell
docker run -d --name container-a nginx
docker run -d --name container-b nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/?t=70)

再次 inspect 这个 bridge 网络,可以看到两个容器都在 `172.17.0.0/16` 子网内被分配了各自唯一的 IP 地址。

```json
"Containers": {
    "5f0dd36da1be6ef5d7df2074185cd0d5fdd8ead0dc2961ac80090c86cc6f13ec": {
        "Name": "container-b",
        "IPv4Address": "172.17.0.3/16"
    },
    "9d8a8331104b9f2e917ac963575b5b79964a51d6645cc3e2256c9cecc1f0a5a9": {
        "Name": "container-a",
        "IPv4Address": "172.17.0.2/16"
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/?t=80)

容器通常在创建时只连接到一个网络,但也可以使用 `docker network connect` 命令连接到多个网络。
同一个网络中的容器可以直接相互通信。
为了测试这一点,我们可以运行两个交互式的 Alpine 容器。

```shell
docker run -it --rm --name container-c alpine
# In a separate terminal
docker run -it --rm --name container-d alpine
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/?t=130)

通过 inspect 该网络找到 `container-d` 的 IP 地址(这里是 `172.17.0.5`),我们就可以在 `container-c` 的 shell 中 ping 这个 IP 来验证连通性。

```bash
/ # ping 172.17.0.5
PING 172.17.0.5 (172.17.0.5): 56 data bytes
64 bytes from 172.17.0.5: seq=0 ttl=64 time=0.339 ms
64 bytes from 172.17.0.5: seq=1 ttl=64 time=0.073 ms
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/default-bridge-network-54123951/?t=220)

#### Limitations of the Default Bridge

与用户自定义网络相比,默认的 bridge 网络有若干局限:

1. **No DNS Service Discovery**:你无法通过名称访问其他容器(例如 `ping container-d`),必须使用具体的 IP 地址。
2. **Port Mapping**:你仍然需要使用 `-p`(publish)标志才能从外部访问,而且默认 bridge 不会像自定义网络那样自动把所有端口暴露给其他容器。
3. **Manual Linking**:历史上,Docker 曾使用 `--link` 标志在默认 bridge 上实现服务发现,但这如今已被视为遗留特性。自定义网络才是实现服务发现的现代方案。

## 3. Custom bridge networks

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/) · 6:46

### 总结

Docker 中的自定义 bridge 网络为容器提供了隔离的通信通道,并带来默认 bridge 网络所不具备的特性,例如按容器名称自动进行 DNS 解析。
通过创建用户自定义网络,开发者可以控制哪些容器之间能够通信、为网络指定特定的子网,并在必要时动态地把容器接入或移出多个网络,以支持跨网络通信。

### 核心概念

- **Network Isolation**:默认情况下,位于不同自定义网络中的容器之间无法相互通信。
- **Automatic DNS Resolution**:自定义 bridge 网络提供内部 DNS,使容器可以通过容器名称或 ID 相互访问。
- **Subnet Management**:每个自定义网络都会被分配一个独立的子网(例如 172.24.0.0/16)。
- **Multi-homing**:通过 `docker network connect`,容器可以同时接入多个网络。
- **Custom Hostnames**:可以使用 `--hostname` 标志为容器在网络中定义特定的 DNS 条目。

### 课程笔记

要创建自定义 bridge 网络,使用 `docker network create` 命令。
它默认使用 bridge 驱动创建一个新网络。

```bash
docker network create network-a
65592acee5c38ab0c72f8f11ba240f00e222220cc4d58f64a7baf995e3332be5
docker network create network-b
f21e456b69384e1012aab4af235e385a066b9a14b7e22f643ade27d8edc55b2f
docker network ls
NETWORK ID     NAME        DRIVER    SCOPE
c1a9b14ddb74   bridge      bridge    local
80c667f6be02   host        host      local
65592acee5c3   network-a   bridge    local
f21e456b6938   network-b   bridge    local
4ec5fd75797c   none        null      local
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=10)

inspect 这些网络可以看到,Docker 自动为它们分配了不同的子网以确保隔离。
例如,`network-a` 可能被分配到 `172.24.0.0/16`,而 `network-b` 得到 `172.25.0.0/16`。

```json
{
    "Name": "network-a",
    "Id": "65592acee5c38ab0c72f8f11ba240f00e222220cc4d58f64a7baf995e3332be5",
    "Created": "2023-08-04T20:12:32.168090829Z",
    "Scope": "local",
    "Driver": "bridge",
    "EnableIPv6": false,
    "IPAM": {
        "Driver": "default",
        "Options": {},
        "Config": [
            {
                "Subnet": "172.24.0.0/16",
                "Gateway": "172.24.0.1"
            }
        ]
    },
    "Internal": false,
    "Attachable": false,
    "Ingress": false,
    "ConfigFrom": {
        "Network": ""
    },
    "ConfigOnly": false,
    "Containers": {},
    "Options": {},
    "Labels": {}
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=85)

运行容器时,你可以通过 `--network` 标志指定网络。
处于同一个自定义网络中的容器可以相互通信,而处于不同网络中的容器则彼此隔离。

```bash
docker run -it --rm --name container-a1 --network network-a alpine
docker run -it --rm --name container-a2 --network network-a alpine
docker run -it --rm --name container-b1 --network network-b alpine
docker run -it --rm --name container-b2 --network network-b alpine
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=160)

你可以使用 `docker ps` 查看正在运行的容器及其网络分配情况。

```bash
docker ps
CONTAINER ID   IMAGE     COMMAND      CREATED          STATUS          PORTS     NAMES
afe79c084168   alpine    "/bin/sh"    54 seconds ago   Up 52 seconds             container-b1
531bac03fed2   alpine    "/bin/sh"    54 seconds ago   Up 53 seconds             container-b2
74f3c8e70fba   alpine    "/bin/sh"    56 seconds ago   Up 55 seconds             container-a2
b65a0c877c5b   alpine    "/bin/sh"    57 seconds ago   Up 56 seconds             container-a1
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=205)

在自定义网络中,Docker 提供了内部 DNS 服务。
容器可以使用彼此的容器名称或 ID 进行 ping。
但是,如果某个容器试图访问位于另一个网络中的容器,DNS 解析会失败,而对应的 IP 地址也无法到达。

```bash
20:55:15 > docker run -it --rm --name container-a1 --network network-a alpine
/ # ping 531bac03fed2
ping: bad address '531bac03fed2'
/ # ping 172.25.0.2
PING 172.25.0.2 (172.25.0.2): 56 data bytes
^C
--- 172.25.0.2 ping statistics ---
10 packets transmitted, 0 packets received, 100% packet loss
/ #

20:56:02 > docker run -it --rm --name container-b1 --network network-b alpine
/ # ping 531bac03fed2
PING 531bac03fed2 (172.25.0.2): 56 data bytes
64 bytes from 172.25.0.2: seq=0 ttl=64 time=0.825 ms
64 bytes from 172.25.0.2: seq=1 ttl=64 time=0.080 ms
64 bytes from 172.25.0.2: seq=2 ttl=64 time=0.071 ms
^C
--- 531bac03fed2 ping statistics ---
3 packets transmitted, 3 packets received, 0% packet loss
round-trip min/avg/max = 0.071/0.325/0.825 ms
/ #
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=220)

你还可以使用 `--hostname` 标志为容器指定一个特定的主机名。
这个主机名会成为该容器所在网络内的一个有效 DNS 条目。

```bash
21:19:31 > docker run -it --rm --name container-a2 --network network-a --hostname dometrain alpine
/ #
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=265)

同一网络中的其他容器现在可以解析这个自定义主机名。

```bash
/ # ping dometrain
PING dometrain (172.24.0.3): 56 data bytes
64 bytes from 172.24.0.3: seq=0 ttl=64 time=0.656 ms
64 bytes from 172.24.0.3: seq=1 ttl=64 time=0.097 ms
64 bytes from 172.24.0.3: seq=2 ttl=64 time=0.082 ms
^C
--- dometrain ping statistics ---
3 packets transmitted, 3 packets received, 0% packet loss
round-trip min/avg/max = 0.082/0.278/0.656 ms
/ #
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=280)

要让位于不同网络中的容器之间能够通信,你可以使用 `docker network connect <network> <container>` 动态地把容器连接到一个额外的网络。

```bash
21:21:16 > docker network connect network-a container-b1
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=295)

一旦同时连接到两个网络,该容器就可以访问这两个环境中的资源。
要撤销这一操作,使用 `disconnect` 命令。

```bash
docker network disconnect network-a container-b1
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=385)

断开连接之后,该容器就失去了对第二个网络的访问权限,对该网络中主机的 DNS 解析也会失败。

```bash
/ # ping dometrain
ping: bad address 'dometrain'
/ #
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/custom-bridge-networks-54123952/?t=400)

## 4. Networking on Docker Compose

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/) · 6:49

### 总结

Docker Compose 会自动为每个项目创建一个自定义 bridge 网络,通过 DNS 实现服务发现,其中服务名即为主机名。
这种编排方式管理着网络的整个生命周期,从部署时创建到拆除时移除,同时允许容器在内部通信而无需向宿主机显式映射端口。

### 核心概念

- **Automatic Bridge Network Creation**:Compose 默认为项目创建一个专用网络。
- **Service Discovery via DNS**:在 YAML 文件中定义的服务名在网络内充当主机名。
- **Network Lifecycle Management**:网络在 `up` 时创建,在 `down` 时移除。
- **Internal vs. External Port Exposure**:内部通信不需要 `ports` 指令;它只用于从宿主机访问容器。
- **Custom Network Topologies**:高级配置允许使用多个相互隔离的网络以及自定义驱动。

### 课程笔记

使用 Docker Compose 时,配置文件中定义的每个服务都会自动加入一个共享的自定义 bridge 网络。
这使得容器之间可以用服务名作为主机名顺畅地相互通信。

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

  database-seed:
    depends_on: [ database ]
    build:
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=10)

#### Network Lifecycle

当你执行 `docker compose up` 时,Docker Compose 会先创建网络,然后再启动容器。
这个网络通常以项目名(往往是目录名)作为前缀,后面跟着 `_default`。

```shell
✔ db9fd82babbe Pull complete
[+] Building 0.1s (8/8) FINISHED
=> [database-seed internal] load .dockerignore
=> => transferring context: 2B
=> [database-seed internal] load build definition from Dockerfile
=> => transferring context: 210B
=> [database-seed internal] load metadata for mcr.microsoft.com/mssql/server:2022-latest
=> [database-seed 1/3] FROM mcr.microsoft.com/mssql/server:2022-latest
=> [database-seed internal] load build context
=> => transferring context: 82B
=> CACHED [database-seed 2/3] COPY ./wait-and-run.sh /wait-and-run.sh
=> CACHED [database-seed 3/3] COPY ./CreateDatabaseAndSeed.sql /CreateDatabaseAndSeed.sql
=> [database-seed] exporting to image
=> => exporting layers
=> => writing image sha256:030d4d6c08eb48edb427fb546d1151bd66b11a4bcdbh
=> => naming to docker.io/library/dockercourse-database-seed
[+] Running 5/5
 ✔ Network dockercourse_default  Created
 ✔ Container database            Created
 ✔ Container frontend            Created
 ✔ Container api                 Created
 ✔ Container database-seed       Created
Attaching to api, database, database-seed, frontend
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=40)

你可以使用 `docker network ls` 确认这个网络的存在。
注意,用 `Ctrl+C` 或 `docker compose stop` 停止容器并不会移除网络;它会一直存在,直到你显式地拆除该项目。

```shell
✔ Container frontend       Stopped
 ✔ Container database-seed  Stopped
 ✔ Container api            Stopped
 ✔ Container database       Stopped
canceled
21:35:51 > docker compose up -d
[+] Building 0.0s (0/0)
[+] Running 4/4
 ✔ Container frontend       Started
 ✔ Container database       Started
 ✔ Container api            Started
 ✔ Container database-seed  Started
21:35:55 > docker network ls
NETWORK ID     NAME                   DRIVER    SCOPE
c1a9b14ddb74   bridge                 bridge    local
524660d71a1d   dockercourse_default   bridge    local
80c667f6be02   host                   host      local
65592acee5c3   network-a               bridge    local
f21e456b6938   network-b               bridge    local
4ec5fd75797c   none                   null      local
21:36:02 >
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=70)

要在移除容器的同时移除网络,使用 `docker compose down` 命令。

```shell
[+] Running 4/4
 ✔ Container frontend       Started
 ✔ Container database       Started
 ✔ Container api            Started
 ✔ Container database-seed  Started
21:35:55 > docker network ls
NETWORK ID     NAME                   DRIVER    SCOPE
c1a9b14ddb74   bridge                 bridge    local
524660d71a1d   dockercourse_default   bridge    local
80c667f6be02   host                   host      local
65592acee5c3   network-a               bridge    local
f21e456b6938   network-b               bridge    local
4ec5fd75797c   none                   null      local
21:36:02 > docker compose down
[+] Running 5/5
 ✔ Container frontend            Removed
 ✔ Container api                 Removed
 ✔ Container database-seed       Removed
 ✔ Container database            Removed
 ✔ Network dockercourse_default  Removed
21:37:04 > docker compose u
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=100)

#### Service Discovery and DNS

由于 Docker Compose 使用的是自定义 bridge 网络,它提供了自动的 DNS 解析。
网络中的任意容器都可以用另一个容器的服务名作为主机名来访问它。
例如,一个应用容器可以直接 ping `api` 或 `frontend` 服务。

```shell
/ # ping api
PING api (172.29.0.2): 56 data bytes
64 bytes from 172.29.0.2: seq=0 ttl=64 time=0.838 ms
64 bytes from 172.29.0.2: seq=1 ttl=64 time=0.093 ms
^C
--- api ping statistics ---
2 packets transmitted, 2 packets received, 0% packet loss
round-trip min/avg/max = 0.093/0.465/0.838 ms
/ # ping frontend
PING frontend (172.29.0.4): 56 data bytes
64 bytes from 172.29.0.4: seq=0 ttl=64 time=0.667 ms
64 bytes from 172.29.0.4: seq=1 ttl=64 time=0.125 ms
64 bytes from 172.29.0.4: seq=2 ttl=64 time=0.076 ms
^C
--- frontend ping statistics ---
3 packets transmitted, 3 packets received, 0% packet loss
round-trip min/avg/max = 0.076/0.289/0.667 ms
/ #
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=205)

#### Internal vs. External Ports

在自定义 bridge 网络中,默认情况下所有端口都会对同一网络中的其他容器开放。
`docker-compose.yml` 中的 `ports` 指令只有在你需要从宿主机访问某个服务时才有必要(例如通过浏览器或本地 SQL 客户端)。
如果像数据库这样的服务只会被同一个 Compose 项目中的 API 服务访问,那么可以去掉 `ports` 映射以获得更好的安全性。

```yaml
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
    ports:
      - 17860:80

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: database
    environment:
      - ACCEPT_EULA=true
      - MSSQL_SA_PASSWORD=Dometrain#123
    ports:
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=340)

#### Advanced Network Configuration

对于复杂场景,你可以定义多个网络并把服务分配到特定的网络中。
这样可以实现网络隔离(例如,一个 proxy 可以与 app 通信,而 app 可以与 database 通信,但 proxy 不能)。

```yaml
services:
  proxy:
    build: ./proxy
    networks:
      - frontend
  app:
    build: ./app
    networks:
      - frontend
      - backend
  db:
    image: postgres
    networks:
      - backend

networks:
  frontend:
    # Use a custom driver
    driver: custom-driver-1
  backend:
    # Use a custom driver which takes special options
    driver: custom-driver-2
    driver_opts:
      foo: "1"
      bar: "2"
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/networking-on-docker-compose-54123953/?t=370)

## 5. Host network

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/) · 4:43

### 总结

host 网络驱动允许 Docker 容器直接共享宿主机的网络命名空间,绕过标准 bridge 网络的隔离。
这意味着容器不会获得自己的 IP 地址;相反,它把服务直接绑定到宿主机的 IP 上,并把宿主机的 localhost 当作自己的 localhost。
虽然这个驱动只在 Linux 宿主机上被原生支持(包括 Windows 上的 WSL2),但对于需要高性能的场景,或者当容器化应用需要像原生运行在机器上那样与宿主机服务交互时,它非常有效。

### 核心概念

- **Linux-Native Support**:host 网络驱动只在 Linux 宿主机上原生工作。在 Windows 或 Mac 上,它运行在底层 Linux 虚拟机(例如 WSL2)的上下文中。
- **Network Stack Sharing**:容器共享宿主机的 IP 地址和端口空间,移除了网络隔离层。
- **Localhost Resolution**:在使用 host 网络的容器内部,`localhost` 指的是宿主机,而不是容器内部的回环地址。
- **Port Mapping Incompatibility**:使用 `--network host` 时,`-p`(端口发布)标志会被忽略,因为容器已经在使用宿主机的端口。
- **Port Conflicts**:如果某个端口已被宿主机上的进程占用,使用 host 网络的容器就无法绑定到同一个端口。

### 课程笔记

#### Understanding Host Isolation

默认情况下,Docker 容器与宿主机网络是隔离的。
如果像 Nginx 这样的服务直接运行在宿主机上,标准容器无法通过 `localhost` 访问到它。
在下面的示例中,宿主机上运行着一个 Nginx 实例,但一个 curl 容器无法连接到它,因为它是在自己隔离的网络命名空间内寻找该服务。

```bash
dan@dan-ubuntu:~$ systemctl start nginx
dan@dan-ubuntu:~$ docker run -it --rm curlimages/curl sh
~ $ curl localhost
curl: (7) Failed to connect to localhost port 80 after 0 ms: Couldn't connect to server
~ $ exit
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/?t=70)

#### Implementing the Host Network

要让容器通过 `localhost` 访问宿主机服务,你必须使用 `--network host` 标志。
这会移除网络隔离,使容器能够看到宿主机的网络接口。
在启用 host 网络的情况下运行同样的 curl 命令,容器就成功获取到了运行在宿主机上的 Nginx 实例返回的 HTML。

```bash
dan@dan-ubuntu:~$ docker run -it --rm --network host curlimages/curl sh
~ $ curl localhost
# (Returns HTML from the host's Nginx service)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/?t=114)

#### Port Management and Conflicts

使用 bridge 网络(默认方式)时,你用 `-p` 标志把宿主机端口映射到容器端口。
如果宿主机端口已被占用,Docker 会在绑定过程中返回错误。

```bash
dan@dan-ubuntu:~$ docker run --rm -p 80:80 nginx
docker: Error response from daemon: driver failed programming external connectivity on endpoint... Error starting userland proxy: listen tcp4 0.0.0.0:80: bind: address already in use.
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/?t=145)

使用 `--network host` 时,`-p` 标志就变得无关紧要了,因为容器会尝试直接绑定到宿主机的端口上。
如果容器化的应用(例如 Nginx)试图监听 80 端口,而宿主机的 Nginx 已经在运行,容器内的应用就会因为 "bind" 错误而崩溃。

```bash
dan@dan-ubuntu:~$ docker run --rm --network host nginx
2023/08/09 10:44:14 [emerg] 1#1: bind() to 0.0.0.0:80 failed (98: Address already in use)
nginx: [emerg] bind() to 0.0.0.0:80 failed (98: Address already in use)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/?t=205)

要解决这个问题,必须停掉宿主机上的服务,把端口让给容器。
一旦宿主机服务停止,容器就可以直接接管宿主机的 80 端口,而无需任何端口映射配置。

```bash
dan@dan-ubuntu:~$ systemctl stop nginx
dan@dan-ubuntu:~$ docker run --rm --network host nginx
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/host-network-54123954/?t=235)

## 6. Leveraging host.docker.internal

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-hostdockerinternal-54123955/) · 0:59

### 总结

本课介绍 host.docker.internal 这个 DNS 条目的用法,它是 Docker Desktop 提供的一项功能,让容器可以与运行在宿主机上的服务通信。
它是只适用于 Linux 的 host 网络模式的跨平台替代方案,使 Windows、macOS 和 Linux 上的开发者都能通过一个特定的 DNS 名称而不是 localhost 来访问宿主机上的资源,例如 SQL Server。

### 核心概念

- **host.docker.internal**:一个特殊的 DNS 条目,解析为宿主机所使用的内部 IP 地址。
- **Cross-Platform Compatibility**:与只在 Linux 上原生工作的 `--network host` 驱动不同,这个 DNS 条目在使用 Docker Desktop 时可以跨 Windows、macOS 和 Linux 工作。
- **Host Service Access**:允许容器化的应用访问直接运行在宿主机操作系统上的服务(例如数据库或 API)。
- **DNS Resolution**:当目标服务位于容器网络边界之外时,用它取代容器内部对 `localhost` 的使用。

### 课程笔记

host 网络虽然允许容器内的软件与宿主机通信,但其标准实现仅限于 Linux 环境。
为了给 Windows 和 macOS 上的开发者提供类似的功能,Docker Desktop 会自动在 host 文件中维护一些特定的 DNS 条目。

这些条目把宿主机的内部 IP 地址映射到易于识别的主机名上,从而在不同操作系统之间保持一致的网络配置方式。

```plaintext
# Added by Docker Desktop
192.168.0.2 host.docker.internal
192.168.0.2 gateway.docker.internal

# To allow the same kube context to work on the host and the container:
127.0.0.1 kubernetes.docker.internal
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/leveraging-hostdockerinternal-54123955/?t=25)

在容器内部,你可以使用 `host.docker.internal` 这个 DNS 名称访问运行在你物理机器上的服务。
例如,如果你有一个 SQL Server 实例运行在宿主机本地而不是 Docker 容器中,那么你的容器化应用无法用 `localhost` 连接它,因为 `localhost` 指的是容器自身。
把连接字符串中的服务器地址改成 `host.docker.internal`,容器就能成功把流量路由到宿主机上。

## 7. Summary of other network types

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/summary-of-other-network-types-54123957/) · 3:00

### 总结

本课全面概述了 Docker 的各种网络驱动,把标准的 bridge 和 host 网络与 None、MacVLAN、IPvlan、Overlay 等专用选项进行对比。
它解释了每种驱动的具体适用场景,例如 MacVLAN 的硬件级集成或 Overlay 的跨主机通信,同时强调对大多数开发工作流而言,bridge 和 host 网络仍然是主要工具。

### 核心概念

- **Bridge Networks**:默认的网络模式,容器在其中获得各自唯一的 IP 地址;自定义 bridge 网络支持自动的 DNS 服务发现,并且是 Docker Compose 的默认选择。
- **Host Networking**:容器共享宿主机的网络命名空间,使用宿主机的 IP 并直接共享它的端口。
- **None Network**:一种不提供任何网络能力的驱动,使容器与所有网络流量隔离。
- **MacVLAN**:为每个容器分配唯一的 MAC 地址,使其在网络上看起来像一台物理设备;需要硬件支持混杂模式(promiscuous mode)。
- **IPvlan**:让容器获得宿主机子网上的 IP 地址,同时不需要唯一的 MAC 地址,从而避免了 MacVLAN 所伴随的硬件限制。
- **Overlay Networks**:让运行在不同 Docker daemon 主机上的容器能够相互通信,通常用于 Docker Swarm 集群。

### 课程笔记

Docker 提供了若干网络驱动来应对不同的连接需求。
其中最常见的是 bridge 网络。
除非另行指定,默认的 bridge 网络会自动分配给容器。
无论是默认还是自定义的 bridge 网络,每个容器都会被分配自己的内部 IP 地址。
自定义 bridge 网络是 Docker Compose 的默认选择,它提供了增强的功能,例如内置的 DNS 服务发现,以及通过 YAML 配置进一步定制网络行为的能力。

host 网络驱动移除了容器与 Docker 宿主机之间的网络隔离。
在这种模式下,容器不会获得自己的 IP 地址;相反,它使用宿主机的 IP 并共享宿主机的端口。
这使得容器化的应用表现得就像直接运行在宿主机的网络栈上一样。

对于专门的使用场景,Docker 还提供了其他几种驱动:

- **None**:这种驱动会禁用容器的所有网络,使其与其他容器和外部网络完全隔离。
- **MacVLAN**:这种驱动为每个容器分配唯一的 MAC 地址,使它们看起来像是直接连接到本地网络路由器的物理设备。使用这种驱动的一个重要前提是,物理网络接口及相关硬件必须支持 "promiscuous mode",因为该接口需要同时处理多个 MAC 地址。
- **IPvlan**:与 MacVLAN 类似,IPvlan 允许容器拥有与宿主机网络子网一致的 IP 地址。不过,它通过共享宿主机的 MAC 地址来避免 MacVLAN 的 MAC 地址限制,同时仍然为每个容器提供唯一的 IP 地址。
- **Overlay**:这种驱动在多个 Docker daemon 主机之间创建一个分布式网络。它主要用于 Docker Swarm 环境,让位于不同物理机或虚拟机上的容器能够无缝通信。虽然它对分布式系统至关重要,但开发者在生产环境中往往会使用 Kubernetes 等其他编排工具。

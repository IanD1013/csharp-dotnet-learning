# Core concepts

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 3 章
> 共 4 课 · 约 8:47
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123775/) | 0:23 | [↓](#1-introduction) |
| 2 | [Containers](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/containers-54123776/) | 3:53 | [↓](#2-containers) |
| 3 | [Images](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/images-54123777/) | 1:47 | [↓](#3-images) |
| 4 | [Container registries and Docker Hub](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/container-registries-and-docker-hub-54123778/) | 2:44 | [↓](#4-container-registries-and-docker-hub) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/introduction-54123775/) · 0:23

### 总结

本课介绍 Docker 生态系统的核心组成部分:容器、镜像和注册表,并概述开发者可以如何利用这些技术来改进自己的软件开发流程。

### 核心概念

- **Containers**:用于运行应用程序及其依赖项的隔离环境。
- **Images**:用于实例化容器的只读模板。
- **Image Registries**:用于存储和分发 Docker 镜像的服务。
- **Developer Workflow**:把容器化实际应用起来,解决开发中特有的问题。

### 课程笔记

Docker 安装成功之后,重点就转移到管理容器化应用所需的那些基础概念上。
在部署容器之前,必须先理解容器、镜像和镜像注册表各自的角色。

容器是一个隔离环境,它把应用程序及其依赖项打包在一起,确保软件在不同的计算环境中都能一致地运行。
镜像则是用来实例化这些容器的静态只读模板。
镜像注册表充当存储和分发这些镜像的中央仓库。

本课程专门讲解开发者可以如何利用这些工具来改进自己的开发生命周期、更有效地管理依赖项,并保持环境一致性。

## 2. Containers

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/containers-54123776/) · 3:53

### 总结

容器是轻量级的虚拟化操作系统,它们共享宿主机内核,提供了一种可移植且高效的方式,把应用程序连同其运行时和依赖项一起打包。
与虚拟化硬件的虚拟机不同,容器专注于在一个隔离、不可变的环境中运行单个主进程,从而无需承担完整客户机操作系统的开销就能快速部署和扩展。

### 核心概念

- **OS Virtualization**:容器虚拟化的是操作系统并共享宿主机内核,而虚拟机虚拟化的是硬件。
- **Lightweight and Fast**:由于开销极小,容器可以在几秒内启动、停止和删除。
- **Single Responsibility**:每个容器通常只专注于单个进程,例如一个 API、一个数据库或一个后台工作进程。
- **Isolation**:每个容器都拥有自己的虚拟化文件系统和网络栈。
- **Immutability**:容器通常被当作不可变的单元对待;虽然可以通过 shell 进入,但在常规操作中很少这样做。
- **Encapsulation**:所有运行时、库和配置都打包在容器内部,从而不再依赖宿主机上的任何东西。

### 课程笔记

#### 容器与虚拟机

理解容器最有效的方式就是把它和虚拟机(VM)作对比。
一台运行着宿主操作系统(Windows、Linux 或 macOS)的标准物理机可以承载多个虚拟机。
然而虚拟机非常消耗资源,因为每一个都需要一套完整的客户机操作系统,通常还包含图形用户界面(GUI)和远程桌面(RDP)能力。

相比之下,容器虚拟化的是操作系统而不是硬件。
通过共享宿主机的内核,容器保持得极其轻量。
这种架构让开发者能在同样的硬件上运行比虚拟机多得多的容器,并且可以在几秒内把它们启动或销毁。

#### 架构与使用场景

从功能角度看,容器是为单一使用场景设计的。
虚拟机可能承载多个应用和服务,而容器通常只运行一样东西,比如某个特定的 API、一个控制台应用,或者像 Redis 这样的一块基础设施。
在现代应用架构中,不同的组件,比如前端、各种后端 API 和数据库,会被隔离到各自的容器中。

#### 环境隔离与网络

每个容器都带着自己的虚拟化文件系统和网络栈运行。
尽管容器主要以不可变的方式使用,但它们并非完全封闭:可以在容器内打开一个 shell 来执行 CLI 命令。
不过,标准的交互方式是通过宿主机进行端口映射,让外部流量能够到达容器内运行的应用程序。

#### 应用打包

对开发者而言,容器最主要的优势在于能把应用连同它运行所需的一切一起打包。
其中包括:
- 主进程(应用程序本身)。
- 所需的运行时(例如 .NET runtime、Node.js 或 Java 虚拟机)。
- 所有依赖项(例如 NuGet 或 NPM 包、DLL)。
- 本地配置文件和环境变量。

这种封装确保了比如一个 Java 应用可以运行在没有安装 JVM 的宿主机上,因为运行时完全包含在容器环境内部。

## 3. Images

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/images-54123777/) · 1:47

### 总结

Docker 镜像充当用于实例化容器的静态只读模板。
容器是应用程序的运行实例,而镜像是该应用及其环境的打包版本,在 Docker 生态系统中充当分发和版本管理的基本单元。

### 核心概念

* **Images as templates**:磁盘上的静态文件,定义容器的初始状态,类似虚拟机快照,但对日常工作流来说更为基础。
* **Container instantiation**:容器是镜像的一个活动的、正在运行的实例。可以从同一个源镜像派生出多个完全相同的容器。
* **State divergence**:容器一旦创建,对其本地文件系统所做的任何更改都只局限于那个特定实例,不会影响底层镜像,也不会影响由该镜像派生出的其他容器。
* **Distribution workflow**:镜像是分享的主要单元。开发者构建镜像并推送到注册表(例如 Docker Hub),这样别人就可以拉取它们并创建自己的本地容器实例。

### 课程笔记

在 Docker 生态系统中,镜像代表应用程序及其环境的静态状态。
虽然它们可以类比为用于备份和还原的虚拟机快照,但 Docker 镜像对开发生命周期而言更加基础。
镜像作为一个静态实体存在于磁盘上,充当容器的蓝图。

容器被定义为镜像的一个运行实例。
可以从同一个镜像启动多个容器;在创建的那一刻,这些容器是完全相同的。
然而,容器一旦开始执行,就可能与其他容器产生分歧。
例如,如果某个容器内的应用程序向本地文件系统写入数据,该更改只局限于那个特定实例。
它既不会修改原始镜像,也不会出现在由同一镜像运行的其他容器中。

镜像同时也是应用分发的机制。
开发者不是去分享一个正在运行的容器实例,而是把自己的应用打包成镜像。
这个镜像随后被上传到容器注册表。
Docker Hub 是默认的注册表,但也可以使用其他注册表。
镜像进入注册表之后,其他用户就可以把它拉取到自己的本地环境中。
Docker 工具随后会把这个下载下来的镜像作为模板来实例化新的容器,从而确保不同环境之间的一致性。

## 4. Container registries and Docker Hub

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/container-registries-and-docker-hub-54123778/) · 2:44

### 总结

容器注册表充当存储和分发 Docker 镜像的中央仓库,其中 Docker Hub 是业界标准的默认选择。
本课探讨注册表的生态,包括 Azure、AWS 和 Google 提供的各家云厂商方案,并详细讲解推送和拉取镜像的工作流,强调要根据应用的安全需求在公开仓库和私有仓库之间做出选择。

### 核心概念

- **Registry Providers**:Docker Hub 是默认选择,但备选方案还包括 Azure Container Registry、Amazon ECR、Google Artifact Registry、GitHub,以及像 Harbor 这样的开源方案。
- **Public vs. Private Repositories**:公开仓库允许任何人下载镜像,而专有的或非开源的应用需要使用私有仓库。
- **Image Ecosystem**:Docker Hub 托管着数百万个第三方镜像,涵盖 Nginx、Redis、Postgres 等流行软件。
- **Push and Pull**:上传和下载镜像的标准术语,与 Git 的工作流一致。

### 课程笔记

容器注册表是存储和分享 Docker 镜像的主要机制。
虽然 Docker Hub 是 Docker 引擎默认使用的注册表,但它并不是开发者唯一的选择。
大多数主流云厂商都提供自己的托管注册表服务,例如 Azure Container Registry、Amazon Elastic Container Registry(ECR)和 Google Artifact Registry。
此外,GitHub 也提供自己的注册表,还有像 Harbor 这样流行的开源自托管方案。

Docker Hub 的独特之处在于,它是一个包含数百万个第三方镜像的庞大公共库。
其中既有官方镜像,也有社区维护的镜像,覆盖各种核心软件栈,包括 Nginx、Redis、Postgres、Node.js、MongoDB、MySQL、RabbitMQ、WordPress 和 InfluxDB。
这些镜像让开发者能够快速拉取预先配置好的环境,而不必从零开始搭建一切。

使用 Docker Hub 时,理解公开仓库和私有仓库之间的区别很重要。
免费的个人套餐通常包含无限量的公开仓库。
但是,上传到公开仓库的任何镜像都可以被任何人下载。
对于不打算开源的专有应用,开发者应当使用私有仓库,它可以通过 Docker Hub 的付费方案或者其他注册表提供商获得。

Docker 在数据传输上沿用了和 Git 相同的术语:"push" 表示把镜像上传到注册表,"pull" 表示从注册表下载镜像。
若只想把镜像下载到本地机器而不立即启动容器,就使用 `pull` 命令。

```bash
docker pull redis
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/container-registries-and-docker-hub-54123778/?t=145)

运行这条命令会从 Docker Hub 取回 Redis 镜像并存储到本地。
这个操作虽然不会执行容器,但它确保了该镜像可供 Docker CLI 以后使用。

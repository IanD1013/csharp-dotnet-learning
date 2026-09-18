# Hosting

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 16 章
> 共 1 课 · 约 4:01
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Container hosting solutions](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/container-hosting-solutions-54127581/) | 4:01 | [↓](#1-container-hosting-solutions) |

## 1. Container hosting solutions

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/container-hosting-solutions-54127581/) · 4:01

### 总结

本课概览可用于 Docker 容器的各种基于云的托管方案,走出本地开发环境。
它勾勒出标准的部署生命周期(镜像在此被构建、推送到注册表,随后由托管提供方拉取),并突出像 Kubernetes 这样的完整编排平台与像 Azure Web Apps 或 AWS Fargate 这样的简化托管服务之间的差异。

### 核心概念

- **Deployment Lifecycle**:构建镜像并把它们推送到注册表(例如 Docker Hub、ACR),供托管方案拉取并执行的过程。
- **Production Requirements**:线上环境的必备能力,包括弹性恢复、监控、机密管理、流量管理和自动伸缩。
- **Container Orchestration**:像 Kubernetes 这样管理复杂容器化应用生命周期和伸缩的工具。
- **Managed Kubernetes Services**:各云厂商各自的实现,例如 AKS(Azure)、EKS(AWS)和 GKE(GCP)。
- **PaaS and Serverless Hosting**:简化的托管选项,例如 Azure Web Apps、Azure Container Apps 和 AWS Fargate,它们把基础设施管理抽象掉。

### 课程笔记

从本地开发过渡到生产环境,需要把关注点转向运维稳定性。
本地容器是手动管理的,而生产环境要求弹性恢复、监控、机密管理、更强的安全性、流量管理、自动自愈和伸缩。

#### The Container Deployment Workflow

在大多数托管平台上,部署容器的基本工作流是一致的:

1. 镜像在本地或通过 CI/CD 系统构建。
2. 镜像被推送到一个容器注册表(也称为镜像注册表)。
3. 托管方案从注册表拉取该镜像。
4. 托管方案基于该镜像创建并管理容器。

#### Container Orchestration with Kubernetes

对于需要强健管理的复杂应用,容器编排平台是标准做法。
虽然可以自行托管一个自管理的 Kubernetes 集群,但大多数组织会使用托管服务来降低运维负担:

- **Azure Kubernetes Service (AKS)**
- **Amazon Elastic Kubernetes Service (EKS)**
- **Google Kubernetes Engine (GKE)**
- **DigitalOcean Kubernetes**

#### Simplified Hosting Alternatives

并非每个使用场景都需要 Kubernetes。
许多 Web 应用托管服务原生支持 Docker 容器。

##### Azure Web Apps

Azure App Service 允许发布代码或 Docker 容器。
使用容器时,用户可以指定操作系统(例如 Linux),并把该服务指向像 Docker Hub 这样的注册表或一个私有注册表。
配置选项包括:

- **Registry Source**:公共(Docker Hub)或私有。
- **Authentication**:私有注册表的凭据。
- **Image and Tag**:部署所用的具体版本。
- **Startup Command**:覆盖 Dockerfile 中定义的默认 `CMD`。

##### Azure Container Apps

对于需要比标准 Web App 更强能力、但又想避免 Kubernetes 复杂度的用户,Azure Container Apps 提供了一个折中方案。
它允许指定注册表和镜像,同时把底层的编排逻辑抽象掉。

#### AWS Container Services

Amazon Web Services 为容器托管提供了若干专门的路径:

- **Elastic Container Service (ECS)**:一个完全托管的容器管理服务。
- **AWS Fargate**:面向容器的无服务器计算引擎,免去管理底层 EC2 实例的需要。

无论选择哪家提供方或哪个服务,核心机制都是一样的:托管环境充当客户端,从注册表拉取一个预先构建好的镜像来实例化应用。

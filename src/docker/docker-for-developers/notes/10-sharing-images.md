# Sharing images

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 10 章
> 共 1 课 · 约 6:36
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Pushing images to Docker Hub](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/) | 6:36 | [↓](#1-pushing-images-to-docker-hub) |

## 1. Pushing images to Docker Hub

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/) · 6:36

### 总结

把 Docker 镜像推送到中央注册表,是与团队成员共享成果、以及把应用部署到 Kubernetes 这类生产环境的必要环节。
本地构建在开发阶段很有用,但容器注册表充当的是开发环境或 CI/CD 系统与最终托管平台之间的桥梁。
本课讲解认证流程、非官方镜像必须带用户名前缀的标签,以及如何用 Docker CLI 和 Docker Compose 把镜像分发到 Docker Hub。

### 核心概念

- **Container Registries**:用于存储和共享 Docker 镜像的集中位置(例如 Docker Hub、Azure Container Registry、AWS ECR)。
- **Public vs. Private Repositories**:公共镜像任何人都能访问,而私有仓库把访问权限限制在授权用户范围内。
- **Official Images**:像 `nginx` 或 `ubuntu` 这类经过验证的镜像,不需要用户名前缀。
- **Image Tagging**:自定义镜像必须带上用户名或组织名前缀(例如 `username/repository:tag`)才能推送到注册表。
- **Authentication**:推送镜像之前,需要用 `docker login` 命令让本地环境通过注册表的认证。
- **CI/CD Integration**:在专业的工作流里,构建和推送过程通常由自动化系统处理,它们从源码管理拉取代码,并把镜像推送到注册表以供部署。

### 课程笔记

#### 分发工作流

在本机上构建的镜像必须共享出去,才能在生产环境中发挥作用。
标准模式是由开发者或 CI/CD 系统(例如 GitHub Actions、Jenkins 或 Azure DevOps)把构建好的镜像推送到容器注册表。
之后,像 Kubernetes 集群这样的托管平台就可以拉取该镜像并创建容器。
这种解耦确保部署环境不需要访问原始源码,只需要编译好的镜像。

#### 仓库设置与安全

使用 Docker Hub 时,仓库可以是公共的,也可以是私有的。
公共仓库允许任何人拉取镜像,这适合开源项目,但对专有应用代码来说存在安全风险。
私有仓库是受限的;免费的 Docker Hub 账号通常包含一个私有仓库,而付费档位或云厂商的注册表(如 ACR 或 ECR)提供更大的灵活性。

`nginx` 或 `rabbitmq` 这类 Docker 官方镜像不需要用户名前缀,因为它们经过了严格的验证流程。
但所有用户创建的镜像都必须把账号用户名作为前缀。

#### 通过 Docker Hub 认证

推送镜像之前,你必须用 `docker login` 命令让本地 Docker 客户端完成认证。
在未认证的情况下尝试推送,或者使用了错误的镜像名,都会导致 access denied 错误。

```shell
11:06:26 > docker push api
Using default tag: latest
The push refers to repository [docker.io/library/api]
da593cfd4812: Preparing
5f70bf18a086: Preparing
fc75589494d7: Preparing
dfae1c11e44d: Preparing
b3ba56d80827: Preparing
82a429d94103: Waiting
cedd91c685c9: Waiting
4b3ba104e9a8: Waiting
denied: requested access to the resource is denied
11:06:38 > docker login --help
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/?t=205)

登录时,请提供你的 Docker ID 和密码(或者用个人访问令牌,安全性更好):

```shell
11:06:50 > docker login
Login with your Docker ID to push and pull images from Docker Hub. If you don't have
Username: dometraindockercourse
Password:
Login Succeeded
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/?t=235)

#### 给镜像打标签并推送

要成功推送镜像,它必须带上注册表用户名的标签。
如果镜像是用 `api` 这样的通用名称构建的,就必须用完整名称(例如 `dometraindockercourse/api`)重新构建或重新打标签。

```shell
11:08:33 > docker build -f .\DockerCourseApi\Dockerfile -t dometraindockercourse/api .
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/?t=280)

一旦标签打对了,`docker push` 命令就会把镜像层上传到注册表。
Docker 会对这个过程做优化,只推送注册表中尚不存在的层。

```shell
11:09:23 > docker push dometraindockercourse/api
Using default tag: latest
The push refers to repository [docker.io/dometraindockercourse/api]
da593cfd4812: Pushed
5f70bf18a086: Pushed
fc75589494d7: Pushed
dfae1c11e44d: Pushing [==================================================>]  21.
b3ba56d80827: Pushed
82a429d94103: Pushing [========================>                          ]  34.5
cedd91c685c9: Pushing [========================================>          ]  25.87
4b3ba104e9a8: Pushing [==================>                                ]  25.92MB/
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/?t=295)

#### 使用 Docker Compose 推送

Docker Compose 也可以管理多容器应用的构建和推送过程。
要使用这种方式,`docker-compose.yaml` 文件里的 `image` 属性必须包含完整的带标签名称。

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
```

在 YAML 文件里正确定义好 `image` 名称之后,你就可以用 `docker compose build` 和 `docker compose push` 同时构建并推送所有服务。

```shell
11:12:50 > docker compose push
[+] Pushing 11/19
 ✔ database-seed Skipped
 ✔ database Skipped
 ✔ Pushing frontend: 78008a69cfa6 Pushed
 ✔ Pushing frontend: bdea7c663e86 Pushed
 ✔ Pushing frontend: 1b22827e15b4 Pushed
 ✔ Pushing frontend: d9f50eaf56fa Pushed
 ✔ Pushing frontend: 2530717ff0bb Pushed
 ✔ Pushing frontend: e7766bc830a8 Pushed
 ✔ Pushing frontend: cb411529b86f Pushed
 ✔ Pushing frontend: bc09720137db Pushed
 ✔ Pushing frontend: 3dab9f8bf2d2 Pushed
 - Pushing api: da593cfd4812 Layer already exists
 - Pushing api: 5f70bf18a086 Layer already exists
 - Pushing api: fc75589494d7 Layer already exists
 - Pushing api: dfae1c11e44d Layer already exists
 - Pushing api: b3ba56d80827 Layer already exists
 - Pushing api: 82a429d94103 Layer already exists
 - Pushing api: cedd91c685c9 Layer already exists
 - Pushing api: 4b3ba104e9a8 Layer already exists
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/pushing-images-to-docker-hub-54123899/?t=370)

虽然 Docker Compose 支持这些命令,但在专业环境里,更常见的做法是在 CI/CD 流水线中用具体的 Docker 命令逐个服务地构建和推送。

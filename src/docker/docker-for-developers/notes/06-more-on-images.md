# More on Images

> 课程:[From Zero to Hero: Docker for Developers](https://dometrain.com/course/from-zero-to-hero-docker-for-developers/) · 第 6 章
> 共 2 课 · 约 8:30
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Image tags](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/) | 5:37 | [↓](#1-image-tags) |
| 2 | [Image layers and caching](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-layers-and-caching-54123808/) | 2:53 | [↓](#2-image-layers-and-caching) |

## 1. Image tags

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/) · 5:37

### 总结

Docker 镜像标签是指向具体 image ID 的可变指针,让开发者可以管理版本、架构和功能变体。
虽然在不指定标签时默认使用 latest 标签,但生产环境应该使用显式的版本标签,以确保一致性并避免意外的更新。
docker tag 命令可以创建多个指向同一个 image ID 的指针,而 docker rmi 则允许删除特定标签或底层镜像本身。

### 核心概念

- **Tags as Pointers**:标签不是镜像本身的一部分,而是一个指向 Image ID(镜像内容的哈希)的独立引用。
- **Default Tagging**:如果在 pull 或 run 命令中没有指定标签,Docker 默认使用 `latest` 标签。
- **Tag Syntax**:标签使用 `repository:tag` 格式指定(例如 `rabbitmq:3-management`)。
- **Mutability**:标签是可变的,可以被重新指派到不同的 Image ID。`latest` 标签经常被作者更新,以指向最新版本。
- **Production Best Practices**:在生产环境中使用显式的版本标签,以确保环境的稳定性和可预测性。

### 课程笔记

要查看本地机器上当前存储的镜像,无论它是通过 `docker pull` 显式拉取的还是通过 `docker run` 隐式拉取的,都可以使用 `docker images` 命令。

```shell
docker images
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/?t=10)

该命令的输出包含仓库名、标签和 Image ID 这几列。
Image ID 是代表镜像内容的唯一哈希,而标签则充当指向那个具体 ID 的指针。

```shell
REPOSITORY                               TAG               IMAGE ID       CREATED        SIZE
database-migrate                         latest            a67971c867ac   3 days ago     1.57GB
nginx-modified                           latest            fb540ec78060   4 days ago     248MB
grafana/grafana                          latest            d7a5fb570941   11 days ago    328MB
wordpress                                latest            1c5f5d58cc6d   11 days ago    664MB
rabbitmq                                 latest            70926b14822d   12 days ago    225MB
mysql                                    latest            91b53e2624b4   2 weeks ago    565MB
nginx                                    latest            eb4a57159180   2 weeks ago    187MB
mcr.microsoft.com/azure-storage/azurite   latest            0bf58610baaf   3 weeks ago    224MB
mcr.microsoft.com/mssql/server           2019-latest       330e92fa0cc2   4 weeks ago    1.47GB
rabbitmq                                 3-management      fa6f2153e5c0   4 weeks ago    225MB
mcr.microsoft.com/mssql/server           2022-latest       05ce0918105b   5 weeks ago    1.47GB

docker run -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/?t=20)

在 `docker run rabbitmq:3-management` 这条命令中,冒号左边的部分是镜像名,右边的部分是标签。
如果没有指定标签,Docker 默认使用 `latest` 作为标签名。

#### Tag Use Cases and Mutability

标签被用于几个关键用途:

- **Versioning**:发布特定版本(例如 `v1`、`v2`),让用户可以把环境固定下来。
- **Feature Variants**:区分不同的构建,例如 RabbitMQ 的 `management` 标签包含一个 web UI,与标准的轻量版本相对。
- **Architecture**:区分 `arm` 和 `amd` 架构。
- **Stability**:为测试打上 `beta` 版本标签。

标签是可变的指针。
当作者创建一个新版本(例如 Version 3)时,他们会显式地把 `latest` 标签重新指派到新的 Image ID 上。
虽然 `latest` 本身就是设计成可变的,但建议把版本相关的标签(例如 `v2`)当作不可变的,以避免混淆。
在生产环境中,你应该始终保持显式,指定你所需要的确切版本标签,而不是依赖 `latest`。

#### Managing Tags Locally

`docker tag` 命令允许你显式地给镜像打标签。
格式是 `docker tag <source> <target_image>:<target_tag>`。
source 可以是一个 Image ID,也可以是一个已有的镜像名。

```bash
docker tag eb4a57159180 myimage:mytag
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/?t=250)

在执行这条命令之后运行 `docker images`,会看到多个条目具有相同的 Image ID,这说明多个指针可以指向同一个镜像。

```bash
docker images
REPOSITORY                                TAG                 IMAGE ID            CREATED
database-migrate                          latest              a67971c867ac        3 days ago
nginx-modified                            latest              fb540ec78060        4 days ago
grafana/grafana                           latest              d7a5fb570941        11 days ago
wordpress                                 latest              1c5f5d58cc6d        11 days ago
rabbitmq                                  latest              70926b14822d        12 days ago
mysql                                     latest              91b53e2624b4        2 weeks ago
myimage                                   mytag               eb4a57159180        2 weeks ago
nginx                                     latest              eb4a57159180        2 weeks ago
mcr.microsoft.com/azure-storage/azurite   latest              0bf58610baaf        3 weeks ago
mcr.microsoft.com/mssql/server            2019-latest         330e92fa0cc2        4 weeks ago
rabbitmq                                  3-management        fa6f2153e5c0        4 weeks ago
mcr.microsoft.com/mssql/server            2022-latest         05ce0918105b        5 weeks ago

docker tag eb4a57159180 nginx:v9999
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/?t=280)

要删除一个标签或一个镜像,请使用 `docker rmi`(Remove Image)命令。
如果你提供的是具体的 `image:tag`,Docker 会取消该引用的标签。
如果你提供的是一个 Image ID,或者所提供的标签是某个镜像最后剩下的引用,Docker 就会删除镜像本身。

```bash
docker rmi nginx:v9999
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-tags-54123807/?t=310)

## 2. Image layers and caching

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-layers-and-caching-54123808/) · 2:53

### 总结

Docker 镜像采用分层架构,每个镜像由多个只读层叠加在一个基础镜像之上构成,基础镜像通常是 Debian 或 Ubuntu 这样的容器操作系统。
这些层代表增量变化(deltas),并且会被单独缓存、在不同镜像之间共享,以优化存储和下载效率。
镜像层保持不可变,而只有当从镜像实例化出一个容器时,才会添加一个临时的读写层。

### 核心概念

- **Layered Architecture**:镜像由一系列叠加的只读层构建而成。
- **Base Images**:镜像的基础,通常是 Debian 或 Ubuntu 这样的最小化操作系统。
- **Deltas**:每个后续的层都代表对其下方那一层所做的具体更改或添加。
- **Layer Caching**:Docker 会缓存单个层;如果系统上已经存在某个层(例如来自另一个镜像),它就不会被重新下载。
- **Read-Only vs. Read-Write**:所有镜像层都是只读的;读写层只在运行时为容器创建。
- **Inspection**:`docker inspect` 命令可以显示一个镜像底层的层哈希。

### 课程笔记

在与 Docker 镜像交互时,系统把它们作为一组不同的层来管理,而不是作为单个文件。
这一点在拉取或删除镜像时显而易见。
例如,用 `docker rmi` 删除一个镜像会触发多个具体层哈希的删除。

```bash
docker pull nginx
Using default tag: latest
latest: Pulling from library/nginx
Digest: sha256:08bc36ad52474e528cc1ea3426b5e3f4bad8a130318e3140d6cfe29c8892c7ef
Status: Image is up to date for nginx:latest
docker.io/library/nginx:latest
docker rmi nginx
Untagged: nginx:latest
Untagged: nginx@sha256:08bc36ad52474e528cc1ea3426b5e3f4bad8a130318e3140d6cfe29c8892c7ef
Deleted: sha256:021283c8eb95be02b23db0de7f609d603553c6714785e7a673c6594a624ffbda
Deleted: sha256:a9de33035096cdf7bbaf7f3e1291701c0620d2a0e66152228abef35a79
Deleted: sha256:d66c35807d98c6f37bd2a14c6506a42d27a40fbdb564e233f7a78a
Deleted: sha256:a4c423818ed6dc12a545c349d0dc36a569546448e07229e96c72
Deleted: sha256:c04094edc9df98c870e281f3b947a7782ca6d542d8715814ac06
Deleted: sha256:c9c467815e8fe87d99f0f500495cf7f4f9096cf6c116ef2782e8
Deleted: sha256:4645f26713fbea51190f5de52b88fbe27b42efd61c0dba87c811
Deleted: sha256:24839d45ca455f36659219281e0f2304520b92347eb536ad5cc7
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-layers-and-caching-54123808/?t=15)

当拉取一个本地不存在的镜像时,Docker 会逐个下载这些层。
你可以看到每一层被拉取和解压的进度。

```bash
docker pull nginx
Using default tag: latest
latest: Pulling from library/nginx
faef57eae888: Extracting [==================================================>]  28.31MB/29.12MB
76579e9ed380: Downloading [==================================================>]  41.46MB
cf707e233955: Download complete
91bb7937700d: Download complete
4b962717ba55: Download complete
f46d7b05649a: Download complete
103501419a0a: Download complete
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-layers-and-caching-54123808/?t=45)

要查看一个镜像的内部结构,请使用 `docker inspect` 命令。
它会输出一个 JSON 对象,其中包含一个 `RootFS` 部分,列出了构成该镜像的每一层的具体 SHA256 哈希。

```json
{
"RootFS": {
    "Type": "layers",
    "Layers": [
        "sha256:24839d45ca455f36659219281e0f2304520b92347eb536ad5cc7b4dbb8163588",
        "sha256:b821d93f6666533e9d135afb55b05327ee35823bb29014d3c4744b01fc35ccc5",
        "sha256:1998c5cd2230129d55a6d8553cd57df27a400614a4d7d510017467150de89739",
        "sha256:f36897eea34df8a4bfea6e0dfaeb693eea7654cd7030bb03767188664a8a7429",
        "sha256:9fdfd12bc85b7a97fef2d42001735cfc5fe24a7371928643192b5494a02497c1",
        "sha256:434c6a715c30517afd50547922c1014d43762ebbc51151b0ecee9b0374a29f10",
        "sha256:3c9d04c9ebd5324784eb9a556a7507c5284aa735bac7a727768fed180709a69"
    ]
},
"Metadata": {
    "LastTagTime": "0001-01-01T00:00:00Z"
}
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-docker-for-developers-2731797/image-layers-and-caching-54123808/?t=55)

#### Image Hierarchy and Caching

镜像是自下而上构建的。
最底层通常是一个容器操作系统层(例如 Debian)。
额外的层叠加在其上,代表诸如复制文件或设置标签之类的更改。

这套机制最显著的优势之一就是缓存。
因为层是不可变的、并且由其内容哈希标识,所以它们可以在不同镜像之间共享。
例如,如果一个 Node.js 镜像和一个 .NET 镜像使用相同的 Debian 基础层,Docker 只需要在宿主机上存储那个基础层一次。
当拉取一个新镜像、而它共享了系统上已经存在的层时,Docker 会跳过这些下载,从而显著提升性能。

虽然镜像自身内部的所有层都是只读的,但当容器启动时,Docker 会在最顶上添加一个很薄的读写层。
这让容器可以在执行过程中做出更改,而不会修改底层的镜像。

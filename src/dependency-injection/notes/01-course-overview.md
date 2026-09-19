# Course overview

> 课程:[From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · 第 1 章
> 共 3 课 · 约 5:44
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

## 课程索引

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Welcome](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/welcome-53953078/) | 2:34 | [↓](#1-welcome) |
| 2 | [What will you learn in this course?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-will-you-learn-in-this-course-53953079/) | 1:49 | [↓](#2-what-will-you-learn-in-this-course) |
| 3 | [Who is this course for and prerequisites](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/who-is-this-course-for-and-prerequisites-53953080/) | 1:21 | [↓](#3-who-is-this-course-for-and-prerequisites) |

---

## 1. Welcome

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/welcome-53953078/) · 2:34

### 总结

本课介绍这门由 Nick Chapsas 主讲的、全面讲解 .NET 与 C# 中依赖注入(DI)的课程。
它勾勒出从基础概念一路走到高级实现细节的学习路径,并最终以构建一个自定义 DI 框架收尾。
课程强调基于高并发生产环境经验的实战应用,而不是抽象理论。

### 核心概念

- .NET 与 C# 中 DI 的基础知识。
- 面向高并发应用的高级 DI 模式与实践。
- 构建一个自定义的依赖注入框架(IoC 容器)。
- 在 .NET 与 ASP.NET Core 环境中的真实应用。
- 面向企业级软件架构的实战问题解决。

### 课程笔记

本课程全面讲解 .NET 生态中的依赖注入(DI),覆盖 C#、.NET 与 ASP.NET Core。
课程设计的目标是把开发者从最基础的内容带到高并发、高吞吐应用中所使用的高级架构模式。

课程的一个主要目标,是跳出理论化的示例,聚焦于专业软件工程中真正会遇到的实际解决方案。
这些内容基于自 .NET Core 1.0 发布以来的真实经验,聚焦于维护大规模系统所需的特定挑战与模式。

课程结构包括:

- **基础知识**:牢固建立对现代 .NET 中 DI 工作方式的理解。
- **高级模式**:探索标准文档中常被忽略的复杂场景与最佳实践。
- **内部机制**:深入剖析 DI 容器的内部运作,并最终开发出一个自定义的依赖注入框架,也就是控制反转(IoC)容器。

课程内容经过筛选,剔除了不必要的冗余,严格聚焦于日常开发所需的工具与技术,以及在专业环境中解决复杂架构问题的方法。

---

## 2. What will you learn in this course?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-will-you-learn-in-this-course-53953079/) · 1:49

### 总结

本课概览 "From Zero to Hero: Dependency Injection in .NET with C#" 这门课程,勾勒出从基础问题解决到高级实现技术的推进路线。
内容涵盖 .NET 内置的依赖注入框架、Scrutor 这类第三方增强库,以及构建一个自定义 IoC 容器,并且专门面向现代 .NET 版本。

### 核心概念

- 识别依赖注入(DI)所要解决的问题。
- .NET 内置 DI 框架的基础知识。
- 在各种项目类型中解析服务。
- 高级技巧与实现细节的拆解。
- 使用 Scrutor 库进行程序集扫描与装饰。
- 构建一个自定义 IoC 容器(Vax)。
- 版本兼容性:.NET Core 1.0 到 .NET 6+(不包含旧版 .NET Framework)。

### 课程笔记

课程从建立依赖注入背后的根本动机开始。
在把这个模式应用到软件架构之前,先理解 DI 究竟解决了哪些具体问题至关重要,因为脱离上下文地套用解决方案只会适得其反。
动机讲清楚之后,课程进入 .NET 内置依赖注入框架的基础部分,并对其内部机制做一个高层次的概览。

DI 的实际应用会通过 10 多种不同的项目类型与服务解析场景来讲解。
其中包括对实现细节的深入剖析,拆解这个框架在底层是如何运作的。
除了基本用法,课程还会讲解那些在真实项目中经常遇到、却常被开发者低估或误解的高级技巧。

为了扩展原生 .NET DI 容器的能力,课程引入了 Scrutor,这是一个用于程序集扫描与装饰器模式的库。
整条学习路径最终以开发一个名为 "Vax" 的自定义控制反转(IoC)库收尾。

请注意,课程内容严格聚焦于现代 .NET(即原先的 .NET Core),覆盖从 1.0 到 .NET 6 及以后的版本。
旧版 .NET Framework 不在本课程范围之内。

---

## 3. Who is this course for and prerequisites

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/who-is-this-course-for-and-prerequisites-53953080/) · 1:21

### 总结

本课说明课程的目标受众与技术要求,强调需要具备 C# 的熟练程度以及对 ASP.NET Core 的了解。
同时明确:课程覆盖从 .NET Core 到 .NET 6 及以后的现代 .NET 版本,但不面向旧版 .NET Framework。

### 核心概念

- C# 语言的熟练程度
- 接触过 ASP.NET Core 框架
- 兼容 .NET Core、.NET 5、.NET 6 及后续版本
- 不涉及旧版 .NET Framework
- 包含 Scrutor 与 DI 容器内部机制等高级主题

### 课程笔记

要顺利学完这门课程,学员必须能够熟练阅读和编写 C#。
此外,强烈建议此前接触过 ASP.NET Core,因为课程聚焦于原生集成在 ASP.NET Core 生态中的依赖注入(DI)特性。

课程内容从基础概念逐步推进到高级实现。
这些高级部分包括对 Scrutor 库的深入剖析,以及构建一个 DI 容器的具体机制。
这些主题虽然复杂,但对于全面理解现代 .NET 架构是必不可少的。

在版本兼容性方面,课程适用于该平台的所有现代版本,包括 .NET Core、.NET 5、.NET 6 以及所有后续版本。
核心的 DI 容器实现自首次发布以来一直保持一致,这保证了课程所讲技术的长期有效性。
请注意,本课程专为现代 .NET 设计,不涉及旧版 .NET Framework。

想跟着一起动手实现的同学,可以在每一章的第一节课中下载源代码。

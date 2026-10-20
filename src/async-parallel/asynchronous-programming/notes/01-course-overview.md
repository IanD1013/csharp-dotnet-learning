# Course overview

> 课程:[From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · 第 1 章
> 共 3 课 · 约 3:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Welcome](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/welcome-62197104/) | 1:05 | [↓](#1-welcome) |
| 2 | [What will you learn in this course?](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-will-you-learn-in-this-course-62197106/) | 1:46 | [↓](#2-what-will-you-learn-in-this-course) |
| 3 | [Who is the course for and prerequisites](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/who-is-the-course-for-and-prerequisites-62197107/) | 0:49 | [↓](#3-who-is-the-course-for-and-prerequisites) |

## 1. Welcome

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/welcome-62197104/) · 1:05

### 总结

本课介绍由 Brandon Minnick 主讲的 "From Zero to Hero: Asynchronous Programming in C#" 课程。
它借助讲师十年的经验以及在全球各地会议上讲授这一主题的丰富背景,为深入探究 .NET 异步编程的内部机制做好铺垫。

### 核心概念

- 讲师在 .NET、Xamarin、Microsoft 和 AWS 方面的背景。
- 聚焦于 C# 异步编程的内部机制。
- 更长的形式,能够比标准的会议演讲进行更透彻的探讨。

### 课程笔记

Brandon Minnick 介绍自己是一名 C# 开发者,在 .NET 生态中拥有十多年经验,曾在 Xamarin、Microsoft 和 AWS 任职。
在整个职业生涯中,他专注于 .NET 中异步编程的内部机制。

自 2018 年以来,Minnick 已经做过许多关于 async/await 的会议演讲。
然而,一小时演讲的限制往往会制约所能分享的信息深度。
这门托管在 Dometrain 上的课程消除了这些时间限制,从而能够全面地传授关于 C# 中异步编程如何运作的知识。
接下来的模块将详细介绍课程中具体的学习目标和所涵盖的技术内容。

## 2. What will you learn in this course?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-will-you-learn-in-this-course-62197106/) · 1:46

### 总结

本课概览 "From Zero to Hero: Asynchronous Programming in C#" 课程,勾勒出从基本的 async/await 用法到深层架构内部机制的学习进程。
它涵盖并行与异步的区别、编译器将异步方法转换为状态机的过程、自定义类 Task 类型的创建,以及执行上下文和同步上下文等高级运行时概念。

### 核心概念

*   **Asynchronous vs. Parallel Programming(异步编程与并行编程)**:理解两者的根本区别,以及它们在 .NET 生态中如何相互作用。
*   **Compiler-Generated Code(编译器生成的代码)**:分析使用 `async` 关键字时 C# 编译器创建的状态机和 struct。
*   **Custom Task Implementation(自定义 Task 实现)**:构建一个自定义的类 Task 类型,以理解 .NET `Task` 类的内部机制。
*   **Asynchronous Best Practices(异步最佳实践)**:使用 `ValueTask` 优化代码,并用 `ConfigureAwait(false)` 管理上下文。
*   **Runtime Internals(运行时内部机制)**:探究 `ExecutionContext`、`SynchronizationContext`、`ThreadStatic` 和 `IPrincipal`,以理解状态如何跨越异步边界进行管理。

### 课程笔记

课程首先明确区分异步编程和并行编程。
虽然这两个概念经常一起使用,但它们在 C# 开发中服务于不同的目的。
课程内容探讨 `async` 和 `await` 关键字如何促成这些模式,以及它们如何与底层线程池交互。

课程中相当大的一部分专门讲解 C# 编译器的内部机制。
当一个方法被标记为 `async` 关键字并返回 `Task` 时,编译器会生成一个隐藏的状态机,它通常实现为一个 `struct`。
理解这段生成的代码,对于掌握异步执行如何被管理、continuation 如何被调度,以及局部变量如何跨越挂起点得以保留至关重要。

为了进一步巩固对 .NET 运行时中 `Task` 类的理解,课程会从零开始构建一个自定义的 task 实现。
这个练习演示了基于 task 的异步的底层机制(例如 completion source 和 awaiter)如何运作,并展示了如何通过实现所需的模式,为自定义类型启用 `async`/`await` 支持。

课程还涵盖了编写高效异步代码的行业最佳实践。
这包括在性能关键路径上利用 `ValueTask` 来减少堆分配,以及使用 `ConfigureAwait(false)` 来管理上下文切换,并在特定类型的环境中防止潜在的死锁。

最后,课程深入探讨高级的 .NET 运行时内部机制。
这包括探究如何使用 `ExecutionContext` 和 `SynchronizationContext` 在异步边界之间保留数据和安全状态。
它还涵盖了 `ThreadStatic` 和 `IPrincipal` 在异步环境中的作用,从而完整呈现 .NET 运行时在上下文切换期间如何处理身份和状态。

## 3. Who is the course for and prerequisites

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/who-is-the-course-for-and-prerequisites-62197107/) · 0:49

### 总结

本课将课程的目标受众定义为高级 C# 开发者。
它强调,虽然课程面向所有人开放,但由于课程内容聚焦于内部机制而非基础语法,它假定学员已有异步编程概念的经验,特别是 async 和 await 关键字以及 Task 库的使用。

### 核心概念

* 目标受众:高级 C# 开发者。
* 前置知识:使用 `async`、`await` 和 `Task` 类的实践经验。
* 课程重点:异步编程的内部实现和"底层"机制。

### 课程笔记

本课程是为高级 C# 开发者设计的。
课程内容假定学员已有编写 C# 代码的经验,并且熟悉基于任务的异步模式(Task-based Asynchronous Pattern,TAP),特别是 `async` 和 `await` 关键字的使用。

本课程的主要目标是探究 .NET 中异步编程的内部机制和"底层"实现。
它不是一份基础语法的入门指南。

对于初次接触这些概念的开发者,建议先完成涵盖以下内容的入门课程:

- `async` 和 `await` 关键字。
- `Task` 和 `Task<T>` 类。
- 基础的并行编程。

在进入本课程提供的深入技术分析之前,获得这些基础知识的实践经验是必不可少的。

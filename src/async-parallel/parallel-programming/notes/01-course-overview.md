# Course overview

> 课程:[From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · 第 1 章
> 共 3 课 · 约 2:03
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Welcome](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/welcome-69955969/) | 0:38 | [↓](#1-welcome) |
| 2 | [What will you learn in this course?](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/what-will-you-learn-in-this-course-69955970/) | 0:46 | [↓](#2-what-will-you-learn-in-this-course) |
| 3 | [Who is the course for and prerequisites](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/who-is-the-course-for-and-prerequisites-69955971/) | 0:39 | [↓](#3-who-is-the-course-for-and-prerequisites) |

---

## 1. Welcome

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/welcome-69955969/) · 0:38

### 总结

这节入门课欢迎学员来到 "From Zero to Hero: Parallel Programming in C#" 课程。
讲师 Brandon Minick 是一位资深 .NET 工程师,曾在 Microsoft 和 Amazon 工作,他介绍了本课程的目标:分享十余年的生产环境经验和工作坊心得,帮助开发者掌握 C# 中的并行编程。

### 核心概念

* 讲师背景:Microsoft 和 Amazon 的高级软件工程师。
* 课程重点:实用的、可用于生产环境的 C# 并行编程。
* 实际应用:课程内容源自十余年的生产环境经验和全球工作坊。

### 课程笔记

本课程由 Brandon Minick 主讲,他是一位拥有十多年职业经验的 .NET 工程师,曾在 Microsoft 和 Amazon 担任高级职位。
本课程将十年来的生产级应用开发、会议演讲和技术工作坊经验,提炼为一份关于 C# 并行编程的全面指南。

课程内容的设计目标是带领开发者从基础知识进阶到专家级水平,重点关注在大规模环境中使用的并行模式与技术的实际实现。
这些课程中分享的知识,都扎根于将应用发布到生产环境以及主持全球工作坊所积累的真实经验。

---

## 2. What will you learn in this course?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/what-will-you-learn-in-this-course-69955970/) · 0:46

### 总结

本课给出了 "Parallel Programming in C#" 课程的整体路线图,概述了从基础概念(例如异步编程与并行编程的区别)到使用任务并行库(Task Parallel Library,TPL)、Channels 和并行 LINQ(Parallel LINQ,PLINQ)的高级实现技术的学习进程。

### 核心概念

*   异步编程与并行编程的区别。
*   任务并行库(TPL)的协调(Task.WhenAll、Task.WhenAny、Task.WhenEach)。
*   使用 Parallel.Invoke、Parallel.For 和 Parallel.ForEach 实现数据并行。
*   使用 Parallel.ForEachAsync 进行异步迭代。
*   使用 Channels 实现生产者-消费者模式。
*   使用并行 LINQ(PLINQ)进行声明式数据处理。

### 课程笔记

本课程是一份掌握 C# 并行编程的全面指南。
它首先厘清异步编程模型与并行编程模型之间的区别,确保开发者能够针对自己特定的性能需求选择正确的方法。

接着,课程会探讨任务并行库(TPL),重点是任务协调原语。
这包括使用 `Task.WhenAll` 等待多个操作完成、使用 `Task.WhenAny` 响应第一个完成的任务,以及使用 `Task.WhenEach` 在任务完成时逐个依次处理它们。

进入数据并行部分后,课程会介绍 `Parallel` 类及其用于在多个 CPU 核心上执行工作的各种方法。
关键主题包括用于并行执行多个操作的 `Parallel.Invoke`,以及基于循环的并行:`Parallel.For`、`Parallel.ForEach`,还有适用于 I/O 密集型或长时间运行的并行任务的异步版本 `Parallel.ForEachAsync`。

课程的最后几个部分会深入高级模式,包括使用 Channels 在生产者和消费者之间进行高性能、线程安全的通信。
最后,课程会介绍并行 LINQ(PLINQ),演示如何将声明式 LINQ 查询并行化,以优化数据处理管道。

---

## 3. Who is the course for and prerequisites

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/who-is-the-course-for-and-prerequisites-69955971/) · 0:39

### 总结

本课概述了 'Parallel Programming in C#' 课程的目标受众和技术前置要求。
本课程面向经验丰富的 C# 开发者,他们需要具备扎实的异步编程基础,特别是 async/await 模式和基于任务的异步模式(Task-based asynchronous pattern,TAP)。

### 核心概念

- **目标受众**:经验丰富的 C# 开发者。
- **前置要求**:熟练掌握 `async`/`await` 以及基于任务的异步模式(TAP)。
- **基础知识**:理解 `Task` 的内部机制和异步执行流程。

### 课程笔记

本课程专门面向那些已经熟练使用 `async` 和 `await` 关键字进行异步编程的资深 C# 开发者。
深入理解 `Task` 对象以及底层异步状态机的运作方式,是学好本课程内容的关键。

对于尚未精通这些领域的开发者,建议先掌握异步编程的基础知识。
这包括学习 `Task` 类、`async`/`await` 的工作机制,以及这些组件在内部是如何运作的。
本课程直接建立在这些基础之上,将异步概念应用到 C# 并行编程这一专门领域。

# Course overview

> Course: [From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · Chapter 1
> 3 lessons · ~3:40
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Welcome](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/welcome-62197104/) | 1:05 | [↓](#1-welcome) |
| 2 | [What will you learn in this course?](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-will-you-learn-in-this-course-62197106/) | 1:46 | [↓](#2-what-will-you-learn-in-this-course) |
| 3 | [Who is the course for and prerequisites](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/who-is-the-course-for-and-prerequisites-62197107/) | 0:49 | [↓](#3-who-is-the-course-for-and-prerequisites) |

---

## 1. Welcome

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/welcome-62197104/) · 1:05

### Summary

This lesson introduces the "From Zero to Hero: Asynchronous Programming in C#" course, presented by Brandon Minnick.
It sets the stage for a deep dive into the internals of .NET asynchronous programming, leveraging the instructor's decade of experience and extensive background in presenting on this topic at global conferences.

### Key concepts

- Instructor background in .NET, Xamarin, Microsoft, and AWS.
- Focus on the internal mechanics of asynchronous programming in C#.
- Extended format allowing for a more thorough exploration than standard conference sessions.

### Lesson notes

Brandon Minnick introduces himself as a C# developer with over a decade of experience in the .NET ecosystem, including roles at Xamarin, Microsoft, and AWS.
Throughout his career, he has specialized in the internals of asynchronous programming in .NET.

Since 2018, Minnick has delivered numerous conference presentations on async/await.
However, the constraints of a one-hour talk often limit the depth of information that can be shared.
This course, hosted on Dometrain, removes those time constraints, allowing for a comprehensive transfer of knowledge regarding how asynchronous programming functions in C#.
The following modules will detail the specific learning objectives and technical content covered in the curriculum.

---

## 2. What will you learn in this course?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/what-will-you-learn-in-this-course-62197106/) · 1:46

### Summary

This lesson provides an overview of the "From Zero to Hero: Asynchronous Programming in C#" course, outlining the progression from basic async/await usage to deep architectural internals.
It covers the distinction between parallelism and asynchrony, the compiler's transformation of asynchronous methods into state machines, the creation of custom task-like types, and advanced runtime concepts like execution and synchronization contexts.

### Key concepts

*   **Asynchronous vs. Parallel Programming**: Understanding the fundamental differences and how they interact within the .NET ecosystem.
*   **Compiler-Generated Code**: Analyzing the state machine and structs created by the C# compiler when using the `async` keyword.
*   **Custom Task Implementation**: Building a custom task-like type to understand the internal mechanisms of the .NET `Task` class.
*   **Asynchronous Best Practices**: Optimizing code using `ValueTask` and managing context with `ConfigureAwait(false)`.
*   **Runtime Internals**: Exploring `ExecutionContext`, `SynchronizationContext`, `ThreadStatic`, and `IPrincipal` to understand how state is managed across asynchronous boundaries.

### Lesson notes

The course begins by establishing a clear distinction between asynchronous programming and parallel programming.
While these concepts are often used together, they serve different purposes in C# development.
The curriculum explores how the `async` and `await` keywords facilitate these patterns and how they interact with the underlying thread pool.

A significant portion of the course is dedicated to the internals of the C# compiler.
When a method is marked with the `async` keyword and returns a `Task`, the compiler generates a hidden state machine, typically implemented as a `struct`.
Understanding this generated code is crucial for mastering how asynchronous execution is managed, how continuations are scheduled, and how local variables are preserved across suspension points.

To further solidify understanding of the `Task` class in the .NET runtime, the course involves building a custom task implementation from scratch.
This exercise demonstrates how the underlying mechanisms of task-based asynchrony function—such as completion sources and awaiters—and shows how to enable `async`/`await` support for custom types by implementing the required patterns.

The course also covers industry best practices for writing efficient asynchronous code.
This includes leveraging `ValueTask` for performance-critical paths to reduce heap allocations and using `ConfigureAwait(false)` to manage context switching and prevent potential deadlocks in specific environment types.

Finally, the curriculum delves into advanced .NET runtime internals.
This includes exploring how data and security state are preserved across asynchronous boundaries using `ExecutionContext` and `SynchronizationContext`.
It also covers the roles of `ThreadStatic` and `IPrincipal` in asynchronous environments to provide a complete picture of how the .NET runtime handles identity and state during context shifts.

---

## 3. Who is the course for and prerequisites

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/who-is-the-course-for-and-prerequisites-62197107/) · 0:49

### Summary

This lesson defines the target audience for the course as advanced C# developers.
It emphasizes that while the course is open to all, it assumes prior experience with asynchronous programming concepts, specifically the use of the async and await keywords and the Task library, as the curriculum focuses on internal mechanics rather than basic syntax.

### Key concepts

* Target audience: Advanced C# developers.
* Prerequisite knowledge: Practical experience with `async`, `await`, and the `Task` class.
* Course focus: Internal implementation and "under the hood" mechanics of asynchronous programming.

### Lesson notes

This course is designed for advanced C# developers.
The curriculum assumes that students have prior experience writing C# code and are familiar with the Task-based Asynchronous Pattern (TAP), specifically the use of the `async` and `await` keywords.

The primary objective of this course is to explore the internal mechanics and "under the hood" implementation of asynchronous programming in .NET.
It is not an introductory guide to basic syntax.

For developers new to these concepts, it is recommended to first complete introductory courses covering:

- The `async` and `await` keywords.
- The `Task` and `Task<T>` classes.
- Basic parallel programming.

Gaining practical experience with these fundamentals is essential before proceeding to the deep technical analysis provided in this course.

# Course overview

> Course: [From Zero to Hero: Parallel Programming in C#](https://dometrain.com/course/from-zero-to-hero-parallel-programming-in-csharp/) · Chapter 1
> 3 lessons · ~2:03
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Welcome](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/welcome-69955969/) | 0:38 | [↓](#1-welcome) |
| 2 | [What will you learn in this course?](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/what-will-you-learn-in-this-course-69955970/) | 0:46 | [↓](#2-what-will-you-learn-in-this-course) |
| 3 | [Who is the course for and prerequisites](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/who-is-the-course-for-and-prerequisites-69955971/) | 0:39 | [↓](#3-who-is-the-course-for-and-prerequisites) |

---

## 1. Welcome

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/welcome-69955969/) · 0:38

### Summary

This introductory lesson welcomes students to the "From Zero to Hero: Parallel Programming in C#" course.
Instructor Brandon Minick, a veteran .NET engineer with experience at Microsoft and Amazon, introduces the course's goal: sharing over a decade of production experience and workshop insights to help developers master parallel programming in C#.

### Key concepts

* Instructor background: Senior Software Engineer at Microsoft and Amazon.
* Course focus: Practical, production-ready parallel programming in C#.
* Real-world application: Lessons derived from over a decade of production experience and global workshops.

### Lesson notes

The course is led by Brandon Minick, a .NET engineer with more than ten years of professional experience, including senior roles at Microsoft and Amazon.
This course distills a decade of production-level application development, conference presentations, and technical workshops into a comprehensive guide on parallel programming in C#.

The curriculum is designed to transition developers from foundational knowledge to expert-level proficiency by focusing on the practical implementation of parallel patterns and techniques used in high-scale environments.
The knowledge shared throughout these lessons is grounded in real-world experience gained from publishing applications to production and hosting global workshops.

---

## 2. What will you learn in this course?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/what-will-you-learn-in-this-course-69955970/) · 0:46

### Summary

This lesson provides a high-level roadmap for the "Parallel Programming in C#" course, outlining the progression from foundational concepts like the distinction between asynchronous and parallel programming to advanced implementation techniques using the Task Parallel Library (TPL), Channels, and Parallel LINQ (PLINQ).

### Key concepts

*   Asynchronous vs. Parallel programming distinctions.
*   Task Parallel Library (TPL) coordination (Task.WhenAll, Task.WhenAny, Task.WhenEach).
*   Data parallelism with Parallel.Invoke, Parallel.For, and Parallel.ForEach.
*   Asynchronous iteration with Parallel.ForEachAsync.
*   Producer-consumer patterns using Channels.
*   Parallel LINQ (PLINQ) for declarative data processing.

### Lesson notes

The course provides a comprehensive guide to mastering parallel programming in C#.
It starts by clarifying the distinctions between asynchronous and parallel programming models to ensure developers choose the correct approach for their specific performance requirements.

The curriculum then explores the Task Parallel Library (TPL), focusing on task coordination primitives.
This includes using `Task.WhenAll` to wait for multiple operations to finish, `Task.WhenAny` to respond to the first completed task, and `Task.WhenEach` for processing tasks sequentially as they complete.

Moving into data parallelism, the course covers the `Parallel` class and its various methods for executing work across multiple CPU cores.
Key topics include `Parallel.Invoke` for executing multiple actions in parallel, and loop-based parallelism using `Parallel.For`, `Parallel.ForEach`, and the asynchronous `Parallel.ForEachAsync` for I/O-bound or long-running parallel tasks.

The final sections of the course delve into advanced patterns, including the use of Channels for high-performance, thread-safe communication between producers and consumers.
Finally, the course covers Parallel LINQ (PLINQ), demonstrating how to parallelize declarative LINQ queries to optimize data processing pipelines.

---

## 3. Who is the course for and prerequisites

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-parallel-programming-in-csharp-3256093/who-is-the-course-for-and-prerequisites-69955971/) · 0:39

### Summary

This lesson outlines the target audience and technical prerequisites for the 'Parallel Programming in C#' course.
It is designed for experienced C# developers who possess a strong foundation in asynchronous programming, specifically the async/await pattern and the Task-based asynchronous pattern (TAP).

### Key concepts

- **Target Audience**: Experienced C# developers.
- **Prerequisites**: Proficiency with `async`/`await` and the Task-based Asynchronous Pattern (TAP).
- **Foundational Knowledge**: Understanding of `Task` internals and asynchronous execution flow.

### Lesson notes

This course is specifically tailored for experienced C# developers who are already proficient in asynchronous programming using the `async` and `await` keywords.
A deep understanding of how `Task` objects and the underlying asynchronous state machine function is essential for success in this curriculum.

For developers who are not yet experts in these areas, it is recommended to first master the fundamentals of asynchronous programming.
This includes learning about the `Task` class, the mechanics of `async`/`await`, and how these components operate internally.
This course builds directly upon those foundations, applying asynchronous concepts to the specialized domain of parallel programming in C#.

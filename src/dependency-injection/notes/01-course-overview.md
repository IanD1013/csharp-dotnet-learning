# Course overview

> Course: [From Zero to Hero: Dependency Injection in .NET with C#](https://dometrain.com/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp/) · Chapter 1
> 3 lessons · ~5:44
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Welcome](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/welcome-53953078/) | 2:34 | [↓](#1-welcome) |
| 2 | [What will you learn in this course?](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-will-you-learn-in-this-course-53953079/) | 1:49 | [↓](#2-what-will-you-learn-in-this-course) |
| 3 | [Who is this course for and prerequisites](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/who-is-this-course-for-and-prerequisites-53953080/) | 1:21 | [↓](#3-who-is-this-course-for-and-prerequisites) |

---

## 1. Welcome

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/welcome-53953078/) · 2:34

**Summary**

This lesson introduces the comprehensive course on dependency injection (DI) in .NET and C#, led by Nick Chapsos.
It outlines the journey from fundamental concepts to advanced implementation details, culminating in the creation of a custom DI framework.
The course emphasizes practical, real-world application based on high-scale production experience rather than abstract theory.

**Key concepts**

*   Fundamentals of DI in .NET and C#.
*   Advanced DI patterns and practices for high-scale applications.
*   Building a custom Dependency Injection framework (IoC container).
*   Real-world application in .NET and ASP.NET Core environments.
*   Practical problem-solving for enterprise-level software architecture.

**Lesson notes**

This course provides a comprehensive guide to dependency injection (DI) within the .NET ecosystem, covering C#, .NET, and ASP.NET Core.
The curriculum is designed to take developers from the absolute basics to advanced architectural patterns used in high-scale, high-throughput applications.

A primary objective of the course is to move beyond theoretical examples and focus on practical solutions encountered in professional software engineering.
The material is based on real-world experience dating back to the release of .NET Core 1.0, focusing on the specific challenges and patterns required to maintain large-scale systems.

The course structure includes:
*   **Foundational Knowledge**: Establishing a solid understanding of how DI works in modern .NET.
*   **Advanced Patterns**: Exploring complex scenarios and best practices that are often overlooked in standard documentation.
*   **Internal Mechanics**: A deep dive into the inner workings of DI containers, culminating in the development of a custom dependency injection framework or Inversion of Control (IoC) container.

The content is curated to exclude unnecessary bloat, focusing strictly on the tools and techniques required for day-to-day development and solving complex architectural problems in a professional environment.

---

## 2. What will you learn in this course?

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/what-will-you-learn-in-this-course-53953079/) · 1:49

### Summary

This lesson provides an overview of the 'From Zero to Hero: Dependency Injection in .NET with C#' course, outlining the progression from fundamental problem-solving to advanced implementation techniques.
It covers the built-in .NET dependency injection framework, third-party enhancements like Scruter, and the creation of a custom IoC container, specifically targeting modern .NET versions.

### Key concepts

*   Problem identification for Dependency Injection (DI).
*   Built-in .NET DI framework fundamentals.
*   Service resolution across various project types.
*   Advanced techniques and implementation deconstruction.
*   Scanning and decoration using the Scruter library.
*   Building a custom IoC container (Vax).
*   Version compatibility: .NET Core 1.0 through .NET 6+ (excluding legacy .NET Framework).

### Lesson notes

The course begins by establishing the foundational motivation behind dependency injection.
Understanding the specific problems DI solves is critical before applying the pattern to software architecture, as applying a solution without context is counterproductive.
Once the motivation is clear, the curriculum moves into the fundamentals of the built-in .NET dependency injection framework, providing a high-level overview of its internal mechanics.

The practical application of DI is explored through more than 10 different project types and service resolution scenarios.
This includes a deep dive into implementation details to deconstruct how the framework functions under the hood.
Beyond basic usage, the course covers advanced techniques frequently encountered in real-world projects that are often underutilized or misunderstood by developers.

To extend the capabilities of the native .NET DI container, the course introduces Scruter, a library designed for assembly scanning and the decorator pattern.
The learning path culminates in the development of a custom Inversion of Control (IoC) library named "Vax."

Note that the content is strictly focused on modern .NET (formerly .NET Core), spanning from version 1.0 through .NET 6 and beyond.
Legacy .NET Framework is not within the scope of this course.

---

## 3. Who is this course for and prerequisites

> [Watch the lesson](https://dometrain.com/take/course/from-zero-to-hero-dependency-injection-in-dotnet-with-csharp-2724086/who-is-this-course-for-and-prerequisites-53953080/) · 1:21

**Summary**

This lesson outlines the target audience and technical requirements for the course, emphasizing proficiency in C# and familiarity with ASP.NET Core.
It clarifies that while the course covers modern .NET versions from .NET Core through .NET 6 and beyond, it is not intended for the legacy .NET Framework.

**Key concepts**

* C# language proficiency
* ASP.NET Core framework exposure
* Compatibility with .NET Core, .NET 5, .NET 6, and future versions
* Exclusion of legacy .NET Framework
* Advanced topics including Scrutor and DI container internals

**Lesson notes**

To successfully complete this course, students must be proficient in reading and writing C#.
Additionally, prior exposure to ASP.NET Core is highly recommended, as the course focuses on the dependency injection (DI) features that are natively integrated into the ASP.NET Core ecosystem.

The curriculum progresses from foundational concepts to advanced implementations.
These advanced sections include deep dives into the Scrutor library and the mechanics of building out a DI container.
While these topics are complex, they are essential for a comprehensive understanding of modern .NET architecture.

Regarding version compatibility, the course applies to all modern iterations of the platform, including .NET Core, .NET 5, .NET 6, and all subsequent versions.
The core DI container implementation has remained consistent since its initial release, ensuring the longevity of the techniques taught.
Note that this course is specifically designed for modern .NET and does not cover the legacy .NET Framework.

For those following along with the implementation, downloadable source code is available in the first lecture of each section.

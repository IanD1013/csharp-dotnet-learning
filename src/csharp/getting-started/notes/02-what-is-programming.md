# What Is Programming?

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 2
> 2 lessons · ~13:19
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Programs and our roles as developers](https://dometrain.com/take/course/getting-started-csharp-2732244/programs-and-our-roles-as-developers-54134762/) | 4:49 | [↓](#1-programs-and-our-roles-as-developers) |
| 2 | [Code and compilation](https://dometrain.com/take/course/getting-started-csharp-2732244/code-and-compilation-54134764/) | 8:30 | [↓](#2-code-and-compilation) |

---

## 1. Programs and our roles as developers

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/programs-and-our-roles-as-developers-54134762/) · 4:49

### Summary

This lesson defines the role of a software developer as a problem solver who utilizes code as a primary tool.
It emphasizes that while learning a language like C# involves mastering syntax and features, the ultimate goal is to translate business requirements and logic into readable, maintainable instructions that solve specific problems.

### Key concepts

- **Problem Solving vs. Coding**: Coding is the tool; problem solving is the core objective of a software engineer.
- **Conceptual Understanding**: Prioritizing the application of language features over rote memorization of syntax.
- **Programs as Instructions**: Software is a collection of rules, patterns, and logic translated into a programming language.
- **Readability and Collaboration**: The necessity of writing code that is understandable for teammates and future maintenance.
- **Business Alignment**: The process of translating real-world business requirements into technical implementations.

### Lesson notes

The role of a software developer, engineer, or programmer is often misunderstood as simply "writing code."
In practice, the core responsibility is problem-solving.
Writing code is the preferred tool for implementing those solutions.
This distinction is vital; memorizing syntax without understanding how to apply it to solve problems is insufficient for professional development.

A parallel can be drawn to sports: a basketball player must know how to dribble, but dribbling is merely a skill used within the broader context of the game to be efficient overall.
Similarly, a developer must know language features but must integrate them effectively to solve problems in a professional workforce.

Programs are essentially sets of rules, instructions, and patterns.
As developers, we take the logical steps required to solve a problem and translate them into a programming language.
C# is one such language that allows us to express these instructions.
This language acts as an intermediary; it uses specific keywords and a variation of the English language that humans can read and write, which is later transformed into a format the computer can execute.

Beyond technical implementation, software development is a collaborative effort.
Developers must write readable code so that colleagues can understand and maintain it.
This requires a deep understanding of how business requirements translate into code and ensuring the logic remains clear to others.
Mastering C# involves learning how to use the right tools within the language to ensure programs are both functional and understandable.

---

## 2. Code and compilation

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/code-and-compilation-54134764/) · 8:30

### Summary

C# is a compiled language within the .NET ecosystem, meaning source code is transformed into an executable format before runtime rather than being interpreted on the fly.
This process involves translating high-level C# code into Microsoft Intermediate Language (MSIL), which the .NET runtime then converts into machine-specific binary instructions using Just-In-Time (JIT) compilation.
This multi-stage approach allows for cross-language compatibility within .NET and generally results in higher execution performance compared to interpreted scripting languages.

### Key concepts

- **Compilation**: An ahead-of-time process that translates human-readable source code into a format the computer can execute.
- **Interpreted Languages**: Languages that are read and executed "on the fly" without a separate compilation step.
- **Microsoft Intermediate Language (MSIL)**: A CPU-independent instruction set that .NET languages compile into.
- **Just-In-Time (JIT) Compilation**: The process where the .NET runtime converts MSIL into machine code at the moment of execution.
- **Binary/Machine Code**: Low-level instructions specific to a processor architecture (e.g., Intel, AMD).

### Lesson notes

Software development involves bridging the gap between human-readable logic and the low-level binary instructions (ones and zeros) understood by computer processors.
To achieve this, developers use either compilation or interpretation.

#### Compilation vs. Interpretation

Compilation is an ahead-of-time process where source code is translated into an executable format before the program is run.
Interpreted languages, often associated with scripting, are processed on the fly by a runtime system that reads the raw text of the code as it executes.
While interpretation offers flexibility, compiled languages like C# generally execute faster because the translation work is completed before the program starts, reducing runtime overhead.

#### The .NET Compilation Pipeline

C# is a compiled language that operates within the .NET ecosystem.
This ecosystem supports multiple languages, including C#, F#, and Visual Basic .NET.
The compilation process for these languages involves several stages:

1. **Source Code**: Developers write logic in high-level languages like C#.
2. **Intermediate Language (IL)**: The compiler translates the source code into Microsoft Intermediate Language (MSIL). This is a CPU-independent set of instructions.
3. **Just-In-Time (JIT) Compilation**: When the program is executed, the .NET Framework's JIT compiler translates the MSIL into machine-specific binary code.
4. **Execution**: The processor (e.g., Intel or AMD) executes the binary instructions.

This architecture allows .NET to support multiple high-level languages while ensuring the final output is optimized for the specific hardware on which the application is running.

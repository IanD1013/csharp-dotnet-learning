# Getting Set Up

> Course: [Getting Started: C#](https://dometrain.com/course/getting-started-csharp/) · Chapter 3
> 4 lessons · ~24:55
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [The Integrated Developer Environment](https://dometrain.com/take/course/getting-started-csharp-2732244/the-integrated-developer-environment-54134768/) | 7:42 | [↓](#1-the-integrated-developer-environment) |
| 2 | [Installing Visual Studio](https://dometrain.com/take/course/getting-started-csharp-2732244/installing-visual-studio-54134770/) | 5:48 | [↓](#2-installing-visual-studio) |
| 3 | [Our First Program](https://dometrain.com/take/course/getting-started-csharp-2732244/our-first-program-54134771/) | 5:43 | [↓](#3-our-first-program) |
| 4 | [Structure of a Program](https://dometrain.com/take/course/getting-started-csharp-2732244/structure-of-a-program-54134772/) | 5:42 | [↓](#4-structure-of-a-program) |

---

## 1. The Integrated Developer Environment

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/the-integrated-developer-environment-54134768/) · 7:42

### Summary

An Integrated Developer Environment (IDE) is a comprehensive software suite that provides essential tools for writing, debugging, and testing code.
While code can technically be written in any text editor, IDEs like Visual Studio offer specialized features such as IntelliSense for autocompletion, real-time syntax error detection, and integrated debugging that significantly enhance developer productivity.
For C# development, Visual Studio is the primary environment, though alternatives like VS Code and JetBrains Rider are also available.

### Key concepts

- Integrated Developer Environment (IDE)
- Visual Studio, VS Code, and JetBrains Rider
- Code Editor and Solution Explorer
- IntelliSense and Tooltips
- Syntax and Compilation Error Highlighting
- Debugging and Step-by-Step Execution

### Lesson notes

The Integrated Developer Environment (IDE) is the primary toolset for software developers.
While code can be written in simple text editors like Notepad, an IDE integrates numerous features that streamline the development process.
For C# development, the standard choice is Microsoft's Visual Studio.
Other options include VS Code (a lighter version from Microsoft) and JetBrains Rider.

The Visual Studio interface consists of several key areas.
The central component is the Code Editor, where the source code is written and modified.
To the side is a navigation system (Solution Explorer), which displays the hierarchical structure of the project, including solutions, projects, and individual files like `Program.cs`.

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.Clear();
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/the-integrated-developer-environment-54134768/?t=130)

One of the most significant advantages of an IDE is the immediate access to documentation.
Hovering over a class or method, such as `Console.WriteLine`, triggers a tooltip that explains the functionality of that specific component.
This reduces the need for developers to memorize every API detail.

Productivity is further enhanced by IntelliSense, an intelligent code completion tool.
When a developer begins typing, IntelliSense provides a filtered dropdown menu of available methods and properties.
For instance, typing `Console.` will suggest actions like `Clear` or `WriteLine`.
This system allows developers to find the correct functionality based on a general idea of what they need, which can then be auto-completed using the Tab key.

IDEs also provide real-time feedback on code quality.
If the syntax is incorrect—for example, if a developer omits parentheses or includes an illegal space—the IDE highlights the error with a red squiggly line.
Hovering over these highlights provides a specific description of the compilation error.

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.Clear;
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/the-integrated-developer-environment-54134768/?t=370)

Beyond writing code, the IDE supports testing and debugging.
Developers can write and run automated tests directly within the environment to ensure their code functions as expected.
The debugging feature is particularly critical; it allows developers to execute a program line-by-line (stepping) while inspecting the current state and variables.
This granular control is essential for identifying and resolving complex logic errors.

---

## 2. Installing Visual Studio

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/installing-visual-studio-54134770/) · 5:48

### Summary

This lesson provides a comprehensive walkthrough for installing Visual Studio 2022, the recommended Integrated Development Environment (IDE) for C# development.
It covers navigating the official Microsoft download site, selecting the free Community Edition, and configuring the installer with the specific .NET desktop development workload required to follow the course curriculum.

### Key concepts

* **Visual Studio Editions**: Understanding the difference between Visual Studio (IDE) and Visual Studio Code (Editor), and identifying the Community Edition as the free version for individual developers.
* **Web Installer**: The initial download is a small bootstrap application that manages the download and installation of the full IDE components.
* **Workloads**: Modular sets of tools and libraries; specifically, the ".NET desktop development" workload is required for C# applications.
* **Installer Maintenance**: The ability to rerun the Visual Studio Installer at any time to add or remove features.

### Lesson notes

To begin the installation, navigate to [visualstudio.microsoft.com](https://visualstudio.microsoft.com) or search for "Visual Studio" in a web browser.
While Microsoft offers several development tools, this course specifically uses Visual Studio.
Visual Studio Code is an alternative, but it requires additional manual configuration not covered in this series.

For most users, **Visual Studio Community 2022** is the appropriate choice.
It is a free, full-featured version that includes all the tools necessary for C# development.
After clicking the download button, a small web installer (approximately 4MB) will download.
This installer acts as a gateway to the full installation process, downloading the necessary components based on your configuration.

Upon launching the installer, you will be presented with the "Workloads" screen.
This allows you to customize the installation to save disk space by only installing the components you need.
To follow along with this course, select the checkbox for **.NET desktop development**.
This workload includes the essential compilers, libraries, and project templates for building C# applications.
The right-hand pane of the installer displays the specific components included in the selected workload.

If you need to change your configuration later—for example, to add web or mobile development tools—you can simply rerun the installer application to add or remove workloads.
Once the .NET desktop development workload is selected, click the **Install** button in the bottom-right corner to begin the download and installation of the IDE.

---

## 3. Our First Program

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/our-first-program-54134771/) · 5:43

### Summary

This lesson covers the initial steps of creating a C# application using the Visual Studio Console App template.
It demonstrates how to navigate the project creation wizard, select the appropriate cross-platform template, configure project and solution names, and target the .NET 8 framework.
The lesson concludes by running the default "Hello, World!" code to verify the environment.

### Key concepts

- Visual Studio Project Templates
- Cross-platform C# Console Application
- Solution vs. Project hierarchy
- .NET 8 Framework selection
- Console.WriteLine output
- Executing via the Debugger

### Lesson notes

To begin, launch Visual Studio and select **Create a new project** from the start window.
In the template search bar, type "console app" and select the C# **Console App** template that includes tags for Linux, macOS, and Windows.
This ensures the project is cross-platform and compatible with multiple operating systems.

On the configuration screen, provide a **Project name** such as "MyFirstProgram".
The **Solution name** will automatically update to match.
In .NET development, a Solution acts as a top-level container that can house multiple projects; for a simple first program, a one-to-one mapping between the project and solution is standard.

Next, select the target framework.
For modern C# development, choose **.NET 8.0 (Long Term Support)** and keep the default settings for other options.
Once the project is created, Visual Studio generates a `Program.cs` file with the following default code:

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/our-first-program-54134771/?t=190)

The first line is a comment pointing to documentation for the top-level statement template.
The second line uses the `Console.WriteLine` method to output text to the terminal.
To execute the program, click the green **Play** button (labeled with the project name) in the top toolbar.
A console window will appear displaying "Hello, World!", confirming that the code compiled and ran successfully.

---

## 4. Structure of a Program

> [Watch the lesson](https://dometrain.com/take/course/getting-started-csharp-2732244/structure-of-a-program-54134772/) · 5:42

### Summary

This lesson explores the structural components of a C# application, focusing on the Visual Studio interface, the hierarchy of solutions and projects, and the basic mechanics of code execution.
It explains how source code in .cs files is organized, how comments are used to annotate code without affecting performance, and how the compilation process transforms source code into an executable file located in the project's output directory.

### Key concepts

- **Solution Explorer**: The interface for navigating the hierarchy of solutions, projects, and files.
- **File Hierarchy**: Solutions contain projects, which in turn contain code files (typically with a .cs extension).
- **Comments**: Text preceded by `//` that is ignored by the compiler and used for human documentation.
- **Execution Flow**: The default behavior where the computer processes instructions sequentially from top to bottom.
- **Compilation Output**: The process of turning source code into an executable (.exe) file, typically stored in the `bin/Debug` folder during development.

### Lesson notes

#### Visual Studio Environment

The Visual Studio interface is divided into several key areas.
The center area is the **Text Editor**, where source code is written.
To the left is the **Solution Explorer**, which functions similarly to a file browser (like Windows Explorer or macOS Finder).

The organizational hierarchy follows a specific order:
1. **Solution**: The top-level container for the software being developed.
2. **Project**: Contained within a solution; a solution can host multiple projects.
3. **Code Files**: Individual files within a project, such as `Program.cs`, where the actual C# code resides.

#### Code Structure and Comments

A basic C# program, such as the "Hello World" template, consists of instructions and documentation.

```csharp
// See https://aka.ms/new-console-template
// for more information
Console.WriteLine("Hello, World!");
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/structure-of-a-program-54134772/?t=45)

In C#, two forward slashes (`//`) indicate a **comment**.
Comments are intended for developers to explain the logic or purpose of the code.
They are completely ignored during compilation and have no impact on the program's execution at runtime.

#### Execution Flow

Programs generally follow a top-to-bottom execution flow.
The computer executes the first line of code, then the second, and so on.
While advanced structures like loops or functions can redirect this flow, the fundamental progression remains sequential within code blocks.

```csharp
// See https://aka.ms/new-console-template
// for more information
Console.WriteLine("Hello, World!");

// 1 this line
// 2 then this line
// 3 then this one...
// ...
```

[▶ Watch](https://dometrain.com/take/course/getting-started-csharp-2732244/structure-of-a-program-54134772/?t=105)

#### Compilation and Output

When a program is compiled, the source code in the `.cs` file is transformed into an executable file.
In Visual Studio, running a program in **Debug mode** (the default development setting) generates this output in a specific directory:

- **Source File**: `Program.cs`
- **Compiled Executable**: `Program.exe`
- **Output Location**: The `bin/Debug` directory within the project folder.

The compiled `.exe` file is what the computer actually runs, whereas the `.cs` file is the human-readable version used for development.

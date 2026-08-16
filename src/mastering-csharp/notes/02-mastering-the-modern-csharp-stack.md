# Mastering the Modern C# Stack

> Course: [Mastering: C#](https://dometrain.com/course/mastering-csharp/) · Chapter 2
> 15 lessons · ~22:55
> Source: Dometrain. Assembled from the lesson documents; every section links to its lesson.

---

## Lesson index

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Overview](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958819/) | 1:00 | [↓](#1-overview) |
| 2 | [Working on legacy projects?](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/) | 0:55 | [↓](#2-working-on-legacy-projects) |
| 3 | [The compilation pipeline](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/) | 1:03 | [↓](#3-the-compilation-pipeline) |
| 4 | [Multi-targeting](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/) | 0:50 | [↓](#4-multi-targeting) |
| 5 | [Adding multi-targeting](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/) | 1:03 | [↓](#5-adding-multi-targeting) |
| 6 | [What is target framework moniker (TFM)?](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/) | 1:20 | [↓](#6-what-is-target-framework-moniker-tfm) |
| 7 | [C# Language version vs. Target framework](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/) | 2:48 | [↓](#7-c-language-version-vs-target-framework) |
| 8 | [Using init-only and required properties in .NET Framework](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/) | 1:40 | [↓](#8-using-init-only-and-required-properties-in-net-framework) |
| 9 | [Compiler language specific types](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/) | 1:32 | [↓](#9-compiler-language-specific-types) |
| 10 | [PolySharp: source-only polyfills for C# Language](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/) | 0:54 | [↓](#10-polysharp-source-only-polyfills-for-c-language) |
| 11 | [Using PolySharp for init-only and required properties](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/) | 0:53 | [↓](#11-using-polysharp-for-init-only-and-required-properties) |
| 12 | [Choosing the right C# Language version](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/) | 0:43 | [↓](#12-choosing-the-right-c-language-version) |
| 13 | ['latest' vs. specific C# Language version](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/) | 3:27 | [↓](#13-latest-vs-specific-c-language-version) |
| 14 | [Using Directory.Build.props](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/) | 3:30 | [↓](#14-using-directorybuildprops) |
| 15 | [Conclusion](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/) | 1:17 | [↓](#15-conclusion) |

---

## 1. Overview

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958819/) · 1:00

### Summary

This lesson introduces the modern C# stack, focusing on the toolchain, target frameworks, and language versioning.
It explains how C# code is compiled into executables and how the choice of target framework (TFM) affects available APIs.
The lesson demonstrates that modern language features, such as init-only properties and required members, can be used even on legacy platforms like .NET Framework 4.8 by providing necessary compiler support types through polyfilling.
Furthermore, it covers how to manage language versions using the .NET SDK and how to centralize build configurations across multiple projects using Directory.Build.props to reduce duplication and ensure consistency.

### Key concepts

- C# toolchain and the compilation process from source to executable.
- Target Framework (TFM) and its role in defining API availability.
- Enabling modern C# features on .NET Framework via polyfills.
- Language versioning (LangVersion) and its relationship with the .NET SDK.
- Centralizing build properties and package management with Directory.Build.props.

### Lesson notes

The C# toolchain is the set of tools used to translate C# source code into an executable format.
This process involves the C# compiler (Roslyn) and the MSBuild build system, which interpret project files to determine how the code should be compiled and linked.

#### Target Frameworks and Multi-targeting

The `TargetFramework` property in a project file defines the target framework (TFM), which determines the API surface area available during development and the runtime required to execute the application.
In modern SDK-style projects, this is defined in the `.csproj` file.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup> 
    <OutputType>Exe</OutputType>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

Projects can also target multiple frameworks using the `TargetFrameworks` property, allowing a single project to produce separate outputs for different runtimes, such as `net48` and `net10.0`.

#### Modern C# on Legacy Frameworks

Most recent C# language features can be used even if an application targets the full .NET Framework or a library targets .NET Standard.
Features like `init`-only properties and `required` members are primarily compiler-driven but require specific metadata types to be present in the `System.Runtime.CompilerServices` namespace.
On older frameworks where these types are missing, they can be supplied manually or generated automatically using tools like PolySharp.
This process, known as polyfilling, unblocks the compiler and allows modern syntax to be used on legacy runtimes.

#### Language Versioning and SDKs

The `LangVersion` property controls which version of the C# language the compiler uses.
While it can be set to a specific version (e.g., `12.0` or `13.0`), it is often set to `latest` to take advantage of the newest features.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>
</Project>
```

When `LangVersion` is set to `latest`, the actual C# version used is determined by the version of the .NET SDK installed on the machine.
To ensure consistent builds across different environments and developer machines, the SDK version can be pinned in a `global.json` file.

```json
{
  "sdk": {
    "version": "10.0.106"
  }
}
```

#### Centralizing Build Configuration

For solutions with multiple projects, build configuration properties such as `LangVersion`, `ImplicitUsings`, and `Nullable` can be centralized in a `Directory.Build.props` file.
This file is automatically discovered and imported by MSBuild for all projects in its directory tree, removing the need to duplicate these settings in every individual project file.

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

This approach simplifies maintenance and ensures that all projects within a repository adhere to the same build standards and language versions.

---

## 2. Working on legacy projects?

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/) · 0:55

### Summary

Modern C# development is not restricted to .NET Core or .NET 5+; it is equally applicable to legacy projects targeting the .NET Framework.
With approximately 30% of .NET applications still running on the full framework, understanding how to decouple the C# language version from the target framework is essential.
This lesson demonstrates that by configuring the project file, developers can leverage the latest language features and syntax improvements even in legacy environments, while also noting the significant performance advantages inherent in migrating to .NET Core.

### Key concepts

- **Framework Independence**: Modern C# language features are generally independent of the underlying .NET runtime (Framework vs. Core/.NET 5+).
- **Legacy Support**: A significant portion of the ecosystem (approx. 30%) remains on .NET Framework, making modern C# skills relevant for legacy maintenance.
- **Language Version Configuration**: The C# language version can be decoupled from the target framework version using the `LangVersion` property.
- **Performance**: While .NET Core offers significant performance gains (3-5x), language improvements can still be applied to .NET Framework projects.

### Lesson notes

The transition to modern .NET (Core and beyond) offers substantial performance benefits, often resulting in a 3x to 5x improvement in execution speed.
However, the modern C# stack is not exclusive to these newer runtimes.
A significant portion of the industry—estimated at 30% of all .NET applications—continues to target the legacy .NET Framework.

This course is designed to be target framework agnostic.
The language features discussed can be implemented in projects targeting the full .NET Framework.
A common misconception among development teams is that the target framework dictates the available C# language version.
In reality, the language version can be manually specified in the project configuration.

To use the latest C# features in any project, the `LangVersion` property should be set in the `.csproj` file.
By setting this to `latest` or a specific version number, the compiler allows modern syntax even when the target framework is older.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/?t=51)

Once the language version is configured, developers can utilize modern syntax and features.
This allows for cleaner, more maintainable code in legacy codebases.
For example, modern LINQ usage and expression-bodied members can be applied to existing logic:

```csharp
using System.Linq;
using System;

namespace LatestCSharpIssues
{
    public sealed class Person
    {
        private string _name = "";

        public string DisplayName
        {
            get
            {
                string field = _name.Trim();
                return field.Length == 0 ? "John Doe" : field;
            }
        }

        public static string LastPart(string[] parts) =>
            parts.Reverse().First();
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello, World!");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/?t=51)

While the language features are available across frameworks, it is important to note that certain performance-critical libraries, such as LINQ, exhibit different performance characteristics between the full framework and .NET Core.
These differences will be explored later in the course to illustrate the benefits of platform migration alongside language modernization.

---

## 3. The compilation pipeline

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/) · 1:03

### Summary

This lesson provides a high-level overview of the C# compilation pipeline, detailing how the C# compiler (CSC) processes SDK-style projects.
It explains the relationship between source code, declarative project files, and the resulting build artifacts, specifically highlighting how modern .NET applications produce a thin wrapper executable alongside a DLL containing the compiled logic.

### Key concepts

- **C# Compiler (CSC)**: The tool responsible for translating C# source code into intermediate language (IL) stored in DLLs or executables.
- **SDK-style Projects**: Modern, declarative `.csproj` files that simplify project configuration.
- **Target Framework**: The specific version of .NET (e.g., .NET 10) that the application is built to run against.
- **Build Artifacts**: The output of the compilation process, which in modern .NET consists of a platform-specific thin wrapper executable and a DLL.

### Lesson notes

The C# compilation process begins with the source code and the project configuration.
In modern .NET development, projects use the SDK-style format, which is highly declarative and concise.
A standard console application includes a source file, such as `Program.cs`, and a project file, such as `SimpleExecutable.csproj`.

```csharp
static void Main(string[] args)
{
    Console.WriteLine("Hello, DomeTrain!");
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

The project file defines the project's metadata, including the `OutputType` and the `TargetFramework`.
For example, setting the output type to `Exe` indicates that the project should result in an executable, while the target framework specifies the runtime environment, such as `.NET 10`.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

When building the project through an IDE like Visual Studio or via the command line, the system invokes the C# compiler, known as `csc`.
This compiler is bundled with the .NET SDK, though it can also be installed independently.
The compiler's primary role is to translate the high-level C# code into a format the .NET runtime can execute.

In modern .NET (formerly .NET Core), the build output differs slightly from legacy .NET Framework.
While the project may be configured as an executable, the actual compiled code resides in a `.dll` file.
The `.exe` file generated is a "thin wrapper" or shim that facilitates the launching of the application within the specified .NET runtime.

```text
1>------ Build started: Project: SimpleExecutable, Configuration: Debug Any CPU ------
1> SimpleExecutable -> C:\Sources\GitHub\DomeTrain\MasteringCSharp\src\01\MultiTargeting\bin\Debug\net10.0\SimpleExecutable.dll
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

The build output confirms the configuration used (such as Debug mode) and the file path where the final artifacts are located.
Because the project targets a specific version, such as .NET 10, the resulting application will run using that specific runtime version.

---

## 4. Multi-targeting

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/) · 0:50

### Summary

Multi-targeting in C# allows a single project to produce multiple outputs for different target frameworks from the same source code.
By modifying the project file to use the plural TargetFrameworks property, developers can ensure their code is compiled and packaged for various environments, such as legacy .NET Framework and modern .NET versions, simultaneously.

### Key concepts

*   **Single vs. Multi-targeting**: Transitioning from a single framework output to multiple framework outputs.
*   **The TargetFrameworks Property**: Using the plural XML element in the project file to define multiple Target Framework Monikers (TFMs).
*   **Compiler Invocation**: The build process invokes the compiler once for each specified framework.
*   **Output Structure**: Separate binaries are produced in framework-specific subdirectories within the build output folder.

### Lesson notes

In most development scenarios, applications target a single framework, such as a legacy .NET Framework version or a modern version like .NET 10.
However, in specific cases, it is beneficial to use the same source code to produce different outputs depending on the target framework.
This process is known as multi-targeting.

To enable multi-targeting, the project file (.csproj) must be modified.
The singular `<TargetFramework>` element is replaced with the plural `<TargetFrameworks>` element.
This element accepts a semicolon-separated list of Target Framework Monikers (TFMs), such as `net48` and `net10.0`.

When the build process is triggered, either through Visual Studio or the command line, the compiler is invoked multiple times—once for each target framework specified.
Instead of producing a single DLL or executable, the build produces multiple versions of the application, each optimized for its specific framework.

```csharp
using System;

namespace SimpleExecutable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello, DomeTrain! TFM: {AppContext.TargetFrameworkName}");
        }
    }
}

<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFrameworks>net48;net10.0</TargetFrameworks>
  </PropertyGroup>
</Project>

1>------ Build started: Project: SimpleExecutable, Configuration: Debug Any CPU ------
1> SimpleExecutable -> C:\Sources\GitHub\DomeTrain\MasteringCSharp\src\01\MultiTargeting\bin\Debug\net10.0\SimpleExecutable.dll
1> SimpleExecutable -> C:\Sources\GitHub\DomeTrain\MasteringCSharp\src\01\MultiTargeting\bin\Debug\net48
\SimpleExecutable.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/?t=10)

As shown in the build output, the project generates distinct files for each target.
For example, a project targeting both .NET 10 and .NET Framework 4.8 will produce a `.dll` for the modern runtime and a `.exe` for the legacy framework, each located in their respective framework-specific subfolders within the `bin` directory.

---

## 5. Adding multi-targeting

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/) · 1:03

### Summary

This lesson demonstrates how to configure a C# project to target multiple .NET frameworks simultaneously.
It covers modifying the project file to use the plural `<TargetFrameworks>` property, selecting specific targets within an IDE like Visual Studio, and verifying the output using tools like ILSpy to inspect the compiled assemblies.

### Key concepts

* **Multi-targeting**: Configuring a single project to compile for multiple .NET versions.
* **TargetFramework vs TargetFrameworks**: The XML elements used in the `.csproj` file to define one or more target framework monikers (TFMs).
* **IDE Integration**: How Visual Studio allows developers to switch between targets for debugging and execution.
* **Assembly Inspection**: Using tools like ILSpy to verify the metadata and target framework of a compiled DLL or EXE.

### Lesson notes

In a standard C# project, the application targets a specific version of the .NET runtime.
To demonstrate this, consider a simple console application that prints the current Target Framework Moniker (TFM) using `AppContext.TargetFrameworkName`.

```csharp
using System;

namespace SimpleExecutable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello, DomeTrain! TFM: {AppContext.TargetFrameworkName}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/?t=10)

The project configuration is defined in the `.csproj` file.
Initially, the project targets a single framework, such as .NET 10.0, using the `<TargetFramework>` element.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/?t=25)

To enable multi-targeting, you must change the `<TargetFramework>` element to the plural `<TargetFrameworks>`.
This allows you to provide a semicolon-separated list of target framework monikers (for example, `<TargetFrameworks>net10.0;net48</TargetFrameworks>`).
Once this change is made, the application will rebuild for all specified targets, and the build output will contain separate folders for each framework.

In Visual Studio, you can select which framework to use for execution via a drop-down menu in the toolbar.
For instance, selecting ".NET 4.8" and running the application will confirm that the environment is indeed using that framework version.

To verify the compiled output, you can use ILSpy, a common tool in the .NET community.
By opening the generated executable or DLL, you can inspect the assembly metadata to ensure it targets the expected framework version, such as .NET 4.8 or .NET 10.0.

---

## 6. What is target framework moniker (TFM)?

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/) · 1:20

Target Framework Monikers (TFMs) are shorthand identifiers used in C# project files instead of full framework names.
Common examples include `net48`, `net10.0`, `net7.0`, and `netstandard`.
These monikers are essential for defining how an application is built and where it can run.

### Key concepts

- **TFMs as Shorthand**: Monikers like `net48` and `net10.0` replace verbose framework descriptions in project configuration.
- **Platform and BCL Definition**: The TFM defines the platform the code is compiled against and determines which versions of the Base Class Library (BCL) types are available.
- **Runtime Execution**: For most frameworks, the TFM specifies the runtime that will execute the application.
- **.NET Standard Exception**: Unlike other TFMs, .NET Standard defines an API surface rather than a specific runtime, enabling cross-compatibility between .NET Framework and .NET Core.
- **Metadata Visibility**: The TFM is stored in assembly metadata and is visible via tools like ILSpy, whereas the C# language version is not.

### Lesson notes

When configuring a C# project, the Target Framework Moniker (TFM) is used to specify the target environment.
In a project file, this is often seen in the `<TargetFramework>` or `<TargetFrameworks>` properties.

```xml
<TargetFrameworks>net48;net10.0</TargetFrameworks>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/?t=10)

A TFM serves three primary purposes:
1. **Compilation Target**: It defines the specific platform your code is compiled against.
2. **Type Availability**: It dictates the available common types. Targeting older frameworks restricts the developer to older versions of the Base Class Library, while newer TFMs unlock modern BCL types.
3. **Runtime Selection**: In most cases, it defines which runtime will execute the application.

The primary exception to the runtime rule is `.NET Standard`.
Instead of defining a specific runtime, `.NET Standard` defines a consistent API surface.
For instance, targeting `netstandard2.0` ensures that a library can be utilized by both .NET Framework and .NET Core applications.
The compiler guarantees that all types used by the code are available across all runtimes supported by that specific version of .NET Standard.

While the TFM is clearly visible in assembly metadata when inspected with tools like ILSpy, the C# language version used during compilation is not recorded in the metadata.
This leads to the question of how the compiler determines which language version to use and what the default settings are for a given project.

---

## 7. C# Language version vs. Target framework

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/) · 2:48

### Summary

The C# language version is typically tied to the target framework by default, but these can be decoupled using the `<LangVersion>` property in the project file.
While official documentation suggests strict pairings (e.g., C# 12 with .NET 8), many modern language features can be used on older frameworks like .NET Framework 4.8.
Features fall into three categories: those that work out of the box, those requiring specific helper types (which can be polyfilled), and a small minority that require specific runtime support.

### Key concepts

*   **Default Mapping**: The compiler selects a C# version based on the `TargetFramework` if not explicitly specified.
*   **Language Version Override**: The `<LangVersion>` property allows using newer compiler features on older runtimes.
*   **Feature Categories**: Language features are divided into those requiring no runtime support, those requiring specific attributes/types, and those requiring runtime changes.
*   **Polyfilling**: Missing types for modern features can be manually defined or provided via libraries like PolySharp to enable modern syntax on older frameworks.

### Lesson notes

There is a direct relationship between the target framework of a project and the default C# language version used by the compiler.
If the version is not explicitly defined, the compiler selects a default based on the framework:
*   **.NET Framework or .NET Standard 2.0**: C# 7.3
*   **.NET 5**: C# 9
*   **.NET 6**: C# 10
*   **.NET 7**: C# 11
*   **.NET 8**: C# 12

However, this default behavior can be overridden using the `<LangVersion>` property in the project file.
This allows an executable targeting .NET Framework 4.8 to use the latest available language features supported by the current compiler.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <LangVersion>latest</LangVersion>

    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/?t=10)

Language features in a release like C# 12 fall into three distinct categories regarding compatibility:
1.  **Runtime-Independent (approx. 66%)**: These features work out of the box regardless of the target framework. Examples include file-local types, checked operators, and default structs.
2.  **Type-Dependent (approx. 25%)**: These features work on any framework but require specific types or attributes to be available during compilation. Examples include `System.Index`, `System.Range`, and the `RequiredMemberAttribute`.
3.  **Runtime-Dependent (approx. 10%)**: These features require specific changes within the .NET runtime itself (e.g., changes introduced in .NET 7 or 8). Examples include `ref` fields and static abstract members in interfaces.

While official documentation states that specific C# versions are only supported on specific .NET versions, in practice, many core .NET tools (such as NuGet, MSBuild, and Roslyn) use the latest language features while still targeting the .NET Framework.
This pattern has also been demonstrated in official technical presentations, such as those by Scott Hanselman and Stephen Toub.

To use features like `init`-only properties or `required` members on older frameworks, the compiler needs the corresponding attributes (like `IsExternalInit` or `RequiredMemberAttribute`) to exist in the expected namespace.
These can be added manually or via source generators.

```csharp
using System;

namespace UsingInitOnly
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point{X = 1, Y = 2};
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/?t=160)

---

## 8. Using init-only and required properties in .NET Framework

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/) · 1:40

### Summary

To use modern C# features like init-only and required properties in .NET Framework applications, developers must manually configure the project's language version and provide polyfills for specific compiler-internal types.
Because the C# compiler uses a "duck typing" approach, it can utilize these features as long as the expected attribute classes exist within the System.Runtime.CompilerServices namespace, even if they are defined directly within the application code rather than the Base Class Library.

### Key concepts

*   **Language Versioning**: .NET Framework projects default to C# 7.3; manual configuration of the `LangVersion` property is required to enable newer syntax.
*   **Compiler Duck Typing**: The C# compiler identifies support for features like `init` and `required` by the presence of specific types in the `System.Runtime.CompilerServices` namespace, regardless of their source.
*   **IsExternalInit**: A marker class required by the compiler to support `init`-only properties (introduced in C# 9.0).
*   **Required Member Attributes**: `RequiredMemberAttribute` and `CompilerFeatureRequiredAttribute` are necessary to enable the `required` keyword (introduced in C# 11.0).

### Lesson notes

When working with legacy .NET Framework 4.8 projects, modern C# features such as `init`-only setters and `required` properties are not available by default.
Attempting to use them in a standard project will result in compilation errors because the default language version for .NET Framework is C# 7.3.

```csharp
using System;

namespace UsingInitOnly
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point{X = 1, Y = 2};
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/?t=10)

To enable these features, the first step is to override the language version in the project file.
Setting the `LangVersion` to `9.0` or higher enables `init` properties, while version `11.0` or higher is required for the `required` keyword.
Using the `latest` value (which resolves to version 14 in this environment) ensures all current language features are available to the compiler.

Even with the language version updated, the code will fail to compile with errors stating that `IsExternalInit` and other required members are not defined.
This is because these types are missing from the .NET Framework 4.8 Base Class Library (BCL).
However, the C# compiler exhibits a "duck-typed" nature: it does not care if these types come from the BCL or the application itself.
If the compiler can find the expected types in the `System.Runtime.CompilerServices` namespace during compilation, the features will work.

To resolve the errors, you must define the following classes within your application to satisfy the compiler's requirements:

```csharp
namespace System.Runtime.CompilerServices
{
   internal class IsExternalInit { }

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
   internal sealed class RequiredMemberAttribute : Attribute { }

   [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
   internal sealed class CompilerFeatureRequiredAttribute : Attribute
   {
       public CompilerFeatureRequiredAttribute(string featureName)
       {
           FeatureName = featureName;
       }

       public string FeatureName { get; }
       public bool IsOptional { get; init; }
   }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/?t=55)

Once these classes are available, the compiler can successfully implement the `init` and `required` logic, making these modern C# features fully functional within a .NET Framework environment.

---

## 9. Compiler language specific types

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/) · 1:32

Modern C# features frequently rely on specific types and attributes that the compiler expects to find in the runtime or project.
While these are built into modern .NET versions, developers targeting older frameworks like .NET Framework 4.8 can still utilize many of these language features by providing the missing types manually or via libraries.

### Key concepts

* **Compiler-dependent types**: The C# compiler looks for specific types (e.g., `IsExternalInit`, `RequiredMemberAttribute`) to enable syntax features.
* **Feature-specific requirements**: Different C# versions (C# 8 through C# 12) require different sets of attributes and types.
* **Targeting older frameworks**: Modern C# syntax can be used on legacy platforms like .NET Framework 4.8 if the required metadata types are provided.
* **IDE and Metadata features**: Attributes like `StringSyntaxAttribute` and `StackTraceHiddenAttribute` provide IDE-specific benefits like syntax highlighting and cleaner debugging.

### Lesson notes

The C# compiler is highly extensible, relying on the presence of specific types and attributes to enable modern language features.
While many of these features are available out-of-the-box in recent .NET versions, others require specific runtime support or the existence of metadata attributes.
Interestingly, as of C# 13 and C# 14, there are no new features that rely on this specific behavior of the compiler.

#### Feature Dependencies by Version

The requirement for specific types spans several C# versions:
* **C# 12**: Collection expression "custom builders".
* **C# 11**: Required properties and list patterns (requiring `System.Index` and `System.Range`).
* **C# 10**: Async method builders, interpolated string improvements, and calling argument expression inference (requiring special attributes).
* **C# 9**: `SkipLocalsInit` and module initializers.
* **C# 8**: Nullable reference types (requiring nullable attributes), Ranges (`System.Index` and `System.Range`), and async streams (requiring an async interface's package).

#### IDE and Metadata Features

Some features rely on attributes primarily for IDE behavior rather than core language logic:
* **Syntax Highlighting**: The `StringSyntaxAttribute` allows for custom syntax highlighting.
* **Debugging**: The `StackTraceHiddenAttribute` allows for hiding specific steps in stack traces.

#### Supporting Older Frameworks

When targeting older frameworks like .NET Framework 4.8, these types are not present in the standard library.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <LangVersion>latest</LangVersion>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/?t=64)

However, the compiler only checks for the existence of these types by name and namespace; it does not strictly require them to come from the official runtime libraries.
By manually defining the expected attributes and classes within the `System.Runtime.CompilerServices` namespace, developers can unlock modern features on legacy platforms.
This approach allows for the use of `init` and `required` properties even when the underlying framework does not natively support them.

```csharp
using System;

namespace UsingInitOnly
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point { X = 1, Y = 2 };
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}

namespace System.Runtime.CompilerServices
{
   internal class IsExternalInit { }

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
   internal sealed class RequiredMemberAttribute : Attribute { }

   [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
   internal sealed class CompilerFeatureRequiredAttribute : Attribute
   {
       public CompilerFeatureRequiredAttribute(string featureName)
       {
           FeatureName = featureName;
       }

       public string FeatureName { get; }
       public bool IsOptional { get; init; }
   }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/?t=78)

While manual maintenance of these types is possible, it becomes complex when managing multiple target frameworks, as different subsets of types are required for different versions.

---

## 10. PolySharp: source-only polyfills for C# Language

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/) · 0:54

PolySharp is a source-only polyfill library that enables modern C# language features on older target frameworks by injecting required support types directly into the assembly at compile time.
These types are generated as internal, preventing public API pollution, and the library acts as a compile-time-only dependency, meaning it does not appear in the final runtime distribution.

### Key concepts

- **Source-only polyfills**: Injects C# source code for missing compiler-support types directly into the project.
- **Compile-time dependency**: The library is only used during the build process and is not required at runtime.
- **Internal visibility**: Generated types are marked as `internal` to avoid conflicts and keep the public API clean.
- **Legacy framework support**: Enables features like `init` properties and `required` members on frameworks like .NET Framework 4.8.

### Lesson notes

PolySharp provides a solution for using modern C# language features in projects targeting older versions of .NET.
While the C# compiler can often understand new syntax, it requires specific types (like `IsExternalInit` for `init` properties) to be present in the target framework.
PolySharp automates the creation of these types.

At compile time, PolySharp adds the necessary source code to your assembly.
This ensures that the compiler has everything it needs to utilize modern features without requiring a newer runtime.
Because these types are generated with `internal` visibility, they do not leak into the public surface area of your library or application.

To use PolySharp, the package must be added to the project file with specific asset settings to ensure it remains a compile-time-only dependency.
The following configuration demonstrates how to enable modern C# features for a project targeting .NET Framework 4.8:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/?t=0)

Once the package is referenced, you can use modern C# keywords that would otherwise cause compilation errors on legacy frameworks.
For example, the `required` and `init` keywords can be used to define properties in a class, and the compiler will use the polyfills provided by PolySharp to satisfy the metadata requirements.

```csharp
using System;

namespace InitOnlyWithPolySharp
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point { X = 1, Y = 2 };
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/?t=15)

This approach is particularly useful for shared libraries or legacy applications where you want to leverage modern language productivity features without forcing a runtime upgrade or adding heavy external dependencies.

---

## 11. Using PolySharp for init-only and required properties

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/) · 0:53

### Summary

PolySharp provides a seamless way to use modern C# features like init-only and required properties when targeting older frameworks such as .NET Framework 4.8.
Instead of manually defining the missing compiler-support types, PolySharp acts as a source generator that injects these types as internal classes directly into the assembly.
This approach ensures that the project remains compatible with the latest language versions while maintaining a clean dependency graph through the use of compile-only assets.

### Key concepts

- Enabling modern C# features on legacy frameworks (e.g., .NET 4.8).
- Automated generation of compiler-support types via source generators.
- Using `PrivateAssets="all"` to ensure PolySharp is a compile-only dependency.
- Internal visibility of generated types to prevent conflicts in consuming projects.

### Lesson notes

When a project targets an older framework like .NET Framework 4.8 but uses the latest C# language version, features like `init` and `required` properties will fail to compile.
This is because the compiler expects specific metadata types, such as `IsExternalInit` or `RequiredMemberAttribute`, to exist in the target framework's base class library.

```csharp
using System;

namespace InitOnlyWithPolySharp
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point { X = 1, Y = 2 };
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/?t=10)

To resolve this without manually adding boilerplate classes, the PolySharp NuGet package can be added to the project.
This package uses source generators to provide the necessary types at compile time.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/?t=25)

A critical configuration detail is the use of `<PrivateAssets>all</PrivateAssets>`.
This ensures that PolySharp is a compile-only dependency; it is not passed on to projects that reference this assembly, and it does not require a runtime DLL. 

When inspecting the resulting assembly in a tool like ILSpy, the PolySharp-generated types appear as `internal` classes within their expected namespaces, such as `System.Runtime.CompilerServices` or `System.Diagnostics.CodeAnalysis`.
This allows the C# compiler to satisfy its requirements for modern syntax while keeping the assembly's public API surface clean and free of unnecessary external dependencies.

---

## 12. Choosing the right C# Language version

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/) · 0:43

### Summary

This lesson details the configuration of the LangVersion property in C# project files, explaining the differences between preview, latest, latestMajor, and specific version pinning.
It emphasizes the importance of build reproducibility, warning that using the latest setting can lead to non-deterministic builds across different environments due to variations in the installed .NET SDK.

### Key concepts

- Configuration of the LangVersion property in the .csproj file.
- The preview value for accessing experimental compiler features.
- The latest and latestMajor values for automatic version selection.
- Pinning to specific versions (e.g., 14, 15) for build stability.
- Risks of build failure in CI/CD pipelines when using non-deterministic versioning.

### Lesson notes

The C# language version is controlled via the `<LangVersion>` property within the project's `.csproj` file.
Developers have several strategies for defining which language features are available to the compiler.

#### Language Version Options

There are several keywords and values that can be assigned to `LangVersion` to determine the compiler's behavior:

- **preview**: This option enables compiler-experimental features. It is used when developers want to test upcoming C# functionality that has not yet been finalized or released in a stable SDK.
- **latest**: This setting instructs the compiler to use the highest version of C# supported by the SDK currently installed on the machine.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/?t=9)

- **latestMajor**: This option selects the latest major version available. This was particularly useful in previous years when C# utilized point releases (such as 7.1, 7.2, or 7.3). In the current release cycle, Microsoft typically focuses on major version releases, making `latestMajor` less distinct from `latest` than it once was.
- **Specific Versions**: For maximum control, developers can specify a precise version number, such as `14` or `15`. This ensures that the code is compiled against a known set of features regardless of the environment.

#### Build Reliability and the "latest" Warning

Official documentation recommends against using the `latest` language version in production environments.
The primary concern is build reliability.
Because `latest` depends on the environment's SDK, a project might build successfully on a developer's local machine but fail in a Continuous Integration (CI) pipeline if the CI environment has a different (typically older) SDK version.

To mitigate these environment discrepancies and ensure the build remains deterministic across all machines, developers often use a `global.json` file to pin the SDK version alongside the language version.

```json
{
  "sdk": {
    "version": "10.0.106"
  }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/?t=36)

By combining a specific `LangVersion` in the `.csproj` with a pinned SDK version in `global.json`, you ensure that the build process is reliable and consistent across different development and deployment stages.

---

## 13. 'latest' vs. specific C# Language version

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/) · 3:27

### Summary

Choosing between the `latest` C# language version and a specific version involves balancing automatic feature adoption against build stability.
While setting `<LangVersion>latest</LangVersion>` ensures that all projects within a solution stay synchronized with the current SDK's capabilities, it can introduce breaking changes when the .NET SDK is upgraded.
For instance, moving from C# 13 to C# 14 can break existing code due to new reserved keywords like `field` or changes in method overload resolution for LINQ extensions.
To maintain reproducible builds while using `latest`, developers should pin their SDK version using a `global.json` file, allowing for controlled upgrades across the entire development team.

### Key concepts

*   **LangVersion Property**: Configured in the `.csproj` file to determine which C# language features are available to the compiler.
*   **latest vs. Specific Version**: The `latest` setting tracks the compiler version provided by the installed .NET SDK, whereas a specific version (e.g., `13.0`) remains static regardless of SDK updates.
*   **Breaking Changes in C# 14**: The introduction of the `field` keyword for semi-auto properties and changes to overload resolution for `Reverse()` methods can break code that was valid in C# 13.
*   **SDK Pinning**: Using `global.json` ensures consistent compiler behavior across different developer machines and build environments.
*   **Project Consistency**: Using `latest` helps prevent "version divergence," where different projects in the same solution target different C# versions.

### Lesson notes

When a project targets a specific .NET version and uses the `latest` language version, the actual C# version used is determined by the installed .NET SDK.
For example, a project might be configured to use the .NET 9 SDK with the language version set to `latest` in the `.csproj` file.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>
</Project>
```

To ensure a reproducible build environment, the SDK version is pinned using a `global.json` file:

```json
{
  "sdk": {
    "version": "9.0.313"
  }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=10)

In this environment, the `latest` setting maps to C# 13.
Code that is valid in C# 13 may fail to compile if the SDK is upgraded to a version that supports C# 14 (such as .NET 10).
Consider the following implementation which is valid under the .NET 9 SDK:

```csharp
using System.Linq;
using System;

namespace LatestCSharpIssues
{
    public sealed class Person
    {
        private string _name = "";

        public string DisplayName
        {
            get
            {
                // In C# 13, 'field' is a valid identifier for a local variable.
                string field = _name.Trim();
                return field.Length == 0 ? "John Doe" : field;
            }
        }

        public static string LastPart(string[] parts) =>
            // In C# 13, this resolves to Enumerable.Reverse.
            parts.Reverse().First();
    }

    internal class Program
    {
        static void Main(string[] args)
        {            
            System.Console.WriteLine("Hello, World!");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=25)

When the SDK is upgraded to version 10, the compiler shifts to C# 14, and two primary breaking changes emerge in the code above:

1.  **The `field` Keyword**: C# 14 introduces the `field` keyword for use in semi-auto properties. Consequently, using `field` as a local variable name inside a property now results in a compiler conflict because the compiler interprets it as a reserved keyword.
2.  **Overload Resolution**: In C# 13, `parts.Reverse()` maps to the LINQ `Enumerable.Reverse` method, which returns an `IEnumerable<T>`. In C# 14, if both `using System;` and `using System.Linq;` are present, the compiler may resolve the call to `MemoryExtensions.Reverse` (a Span-based method). Because the Span-based `Reverse` method performs an in-place mutation and returns `void` (or a type incompatible with the subsequent `.First()` call), the code fails to compile.

To simulate this upgrade, the `global.json` is updated to target the newer SDK:

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestFeature"
  }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=130)

#### Trade-offs of using 'latest'

Using the `latest` language version has several advantages and disadvantages that should be considered before making a decision:

*   **Pros**:
    *   **Consistency**: It prevents different projects in a large solution from drifting onto different C# versions (e.g., some on version 8, others on 11), ensuring a uniform feature set.
    *   **Centralized Control**: The language version is effectively controlled by the SDK version defined in `global.json`. Upgrading the SDK automatically upgrades the language version across all projects in the repository.
*   **Cons**:
    *   **Unexpected Breaking Changes**: As demonstrated, an SDK upgrade can break existing code that was perfectly valid in the previous language version due to new keywords or resolution rules.
    *   **Manual Upgrades**: If a specific version is used (e.g., `<LangVersion>13.0</LangVersion>`), you must manually update every project file when a new version is released, which can be a significant maintenance burden in large solutions.

Regardless of the choice, centralizing project configurations using tools like `Directory.Build.props` can help manage these settings in a single place.

---

## 14. Using Directory.Build.props

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/) · 3:30

### Summary

Directory.Build.props is an MSBuild feature that allows developers to centralize common project properties and item references across a solution.
By moving shared configurations like target frameworks, language versions, and global NuGet packages into this file, you eliminate the need to maintain identical settings in every individual .csproj file.
This hierarchical system supports inheritance and overriding, enabling a clean, DRY (Don't Repeat Yourself) approach to build logic that is easier to manage as a project scales.

### Key concepts

*   **Legacy vs. SDK-style projects**: Legacy projects are verbose and opaque to the build system, while SDK-style projects are concise and better at handling transitive dependencies.
*   **Centralization**: Moving common properties (e.g., `TargetFramework`, `LangVersion`) to a single file to avoid duplication.
*   **Directory.Build.props**: A special MSBuild file that is automatically imported by projects in its directory or subdirectories.
*   **Hierarchical Overriding**: Properties can be overridden in nested folders by placing another `Directory.Build.props` file closer to the project.
*   **Refactoring Build Logic**: Treating build configuration like code by extracting commonalities into a single source of truth.

### Lesson notes

Modern C# development distinguishes between two project file formats: legacy-style and SDK-style.
Legacy projects are highly verbose and can be opaque to the build system.
For example, if Project A references Project B, the build system might not correctly see all package dependencies, which causes issues in large-scale solutions.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{2ECA1DC8-BD41-402A-8CC8-30736231DD54}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <RootNamespace>LegacyStyleCsProj</RootNamespace>
    <AssemblyName>LegacyStyleCsProj</AssemblyName>
    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <PreferNativeArm64>true</PreferNativeArm64>
    <Deterministic>true</Deterministic>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=40)

In contrast, SDK-style projects are much cleaner.
However, when a solution contains multiple projects (e.g., Project A, Project B, and Project C), they often duplicate the same properties, such as the target framework, language version, and common package references like PolySharp.

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=70)

To eliminate this duplication, you can extract these common properties into a file named `Directory.Build.props`.
Once these properties are moved, the individual project files only need to contain project-specific configurations, such as the `OutputType` or project references.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\ProjectA\ProjectA.csproj" />
    <ProjectReference Include="..\ProjectB\ProjectB.csproj" />
  </ItemGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=120)

The `Directory.Build.props` file acts as a central repository for shared build logic.
If you decide to change the target framework or update the language version, you only need to update this single file rather than every project in the repository.

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=130)

This system is hierarchical, similar to how `.editorconfig` files work.
You can place a `Directory.Build.props` file in a nested folder to override the properties defined in a parent directory.
The file closest to the project takes precedence, allowing for granular control over specific sub-sections of a large solution while maintaining global defaults.

---

## 15. Conclusion

> [Watch the lesson](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/) · 1:17

### Summary

This lesson concludes the module on the modern C# stack, emphasizing that approximately 90% of modern C# language features are available regardless of the target framework, including .NET Framework and .NET Standard.
While most features work out of the box, some require compiler-support types that can be provided via manual polyfills or automated tools like PolySharp.
The module also highlights the importance of centralizing build configurations using Directory.Build.props to ensure consistency and maintainability across large solutions.

### Key concepts

- **Language vs. Runtime Support**: Most C# features are compiler-driven, with only about 10% requiring specific runtime support.
- **Polyfilling**: Enabling modern features like `init` and `required` on older frameworks by providing missing support types.
- **PolySharp**: A source generator that automatically creates required polyfill types at compile-time.
- **Language Versioning**: Overriding the default C# version associated with a Target Framework (TFM) using the `LangVersion` property.
- **Centralized Configuration**: Using `Directory.Build.props` to manage properties like `LangVersion` and package references across an entire solution.

### Lesson notes

Modern C# development is largely decoupled from the underlying runtime.
Approximately 90% of the language features released in recent years can be used even when targeting legacy environments like .NET Framework 4.8 or .NET Standard.
These features are handled by the compiler and do not require specific runtime updates.

However, some features depend on specific types being present in the framework.
For example, `init`-only properties and `required` members require types like `IsExternalInit` or `RequiredMemberAttribute` within the `System.Runtime.CompilerServices` namespace.
When these types are missing in older frameworks, they can be supplied manually or generated automatically using PolySharp.

```csharp
using System;

namespace InitOnlyWithPolySharp
{
    public class Point
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Point { X = 1, Y = 2 };
            Console.WriteLine($"X: {p.X}, Y: {p.Y}");
        }
    }
}
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/?t=21)

Only about 10% of language features require explicit runtime support to function.
For the remaining features, the relationship between the Target Framework (TFM) and the C# language version is flexible.
While each TFM has a default language version, this can be explicitly overridden in the project file.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>

</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/?t=38)

To avoid duplicating these configurations across hundreds or thousands of projects, build-specific decisions should be encapsulated in a single location using a `Directory.Build.props` file.
This file allows developers to define properties like `LangVersion`, `ImplicitUsings`, and `Nullable` globally.
If a decision needs to be changed—such as switching from `latest` to a specific C# version—it only needs to be updated in this one file.

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <LangVersion>latest</LangVersion>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="PolySharp" Version="1.15.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

[▶ Watch](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/?t=46)

The next module will explore data types in depth, covering the differences between classes, structs, records, and tuples.

# Mastering the Modern C# Stack

> 课程:[Mastering: C#](https://dometrain.com/course/mastering-csharp/) · 第 2 章
> 共 15 课 · 约 22:55
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
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

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/overview-69958819/) · 1:00

### 总结

本课介绍现代 C# 技术栈,重点是工具链、目标框架和语言版本控制。
它解释了 C# 代码如何被编译成可执行文件,以及目标框架(TFM)的选择如何影响可用的 API。
本课演示了,通过 polyfill 方式提供必要的编译器支持类型,即使在 .NET Framework 4.8 这样的旧平台上,也可以使用 init-only 属性和 required 成员等现代语言特性。
此外,本课还介绍了如何借助 .NET SDK 管理语言版本,以及如何使用 Directory.Build.props 在多个项目之间集中管理构建配置,以减少重复并确保一致性。

### 核心概念

- C# 工具链以及从源代码到可执行文件的编译过程。
- 目标框架(TFM)及其在决定 API 可用性方面的作用。
- 通过 polyfill 在 .NET Framework 上启用现代 C# 特性。
- 语言版本控制(LangVersion)及其与 .NET SDK 的关系。
- 使用 Directory.Build.props 集中管理构建属性和包管理。

### 课程笔记

C# 工具链是一组用于把 C# 源代码转换为可执行格式的工具。
这一过程涉及 C# 编译器(Roslyn)和 MSBuild 构建系统,它们会解析项目文件,以确定代码应当如何编译和链接。

#### 目标框架与多目标

项目文件中的 `TargetFramework` 属性定义了目标框架(TFM),它决定了开发期间可用的 API 范围,以及运行应用程序所需的运行时。
在现代 SDK 风格的项目中,它定义在 `.csproj` 文件里。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup> 
    <OutputType>Exe</OutputType>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

项目还可以使用 `TargetFrameworks` 属性同时面向多个框架,让单个项目为不同的运行时(例如 `net48` 和 `net10.0`)分别生成输出。

#### 在旧框架上使用现代 C#

即使应用程序面向完整的 .NET Framework,或者类库面向 .NET Standard,大多数较新的 C# 语言特性仍然可以使用。
`init`-only 属性和 `required` 成员等特性主要由编译器驱动,但要求 `System.Runtime.CompilerServices` 命名空间中存在特定的元数据类型。
在缺少这些类型的旧框架上,可以手动提供它们,或者使用 PolySharp 等工具自动生成。
这一过程称为 polyfill,它为编译器扫除障碍,让现代语法可以在旧运行时上使用。

#### 语言版本控制与 SDK

`LangVersion` 属性控制编译器使用哪个版本的 C# 语言。
虽然它可以设置为某个具体版本(例如 `12.0` 或 `13.0`),但通常会设置为 `latest`,以便使用最新的特性。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>
</Project>
```

当 `LangVersion` 设置为 `latest` 时,实际使用的 C# 版本由机器上安装的 .NET SDK 版本决定。
为了确保在不同环境和不同开发者机器上构建结果一致,可以在 `global.json` 文件中固定 SDK 版本。

```json
{
  "sdk": {
    "version": "10.0.106"
  }
}
```

#### 集中管理构建配置

对于包含多个项目的解决方案,可以把 `LangVersion`、`ImplicitUsings` 和 `Nullable` 等构建配置属性集中放在一个 `Directory.Build.props` 文件中。
MSBuild 会为其目录树中的所有项目自动发现并导入该文件,因此无需在每个项目文件中重复这些设置。

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

这种做法简化了维护工作,并确保仓库中的所有项目都遵循相同的构建标准和语言版本。

---

## 2. Working on legacy projects?

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/) · 0:55

### 总结

现代 C# 开发并不局限于 .NET Core 或 .NET 5+;它同样适用于面向 .NET Framework 的旧项目。
由于大约 30% 的 .NET 应用程序仍运行在完整框架上,理解如何把 C# 语言版本与目标框架解耦至关重要。
本课演示了,通过配置项目文件,开发者即使在旧环境中也能利用最新的语言特性和语法改进,同时也指出了迁移到 .NET Core 本身所带来的显著性能优势。

### 核心概念

- **框架无关性**:现代 C# 语言特性通常与底层 .NET 运行时(Framework 与 Core/.NET 5+)无关。
- **旧项目支持**:生态系统中相当大的一部分(约 30%)仍停留在 .NET Framework 上,这使得现代 C# 技能对旧项目维护同样适用。
- **语言版本配置**:可以使用 `LangVersion` 属性把 C# 语言版本与目标框架版本解耦。
- **性能**:虽然 .NET Core 带来显著的性能提升(3-5 倍),但语言层面的改进仍然可以应用于 .NET Framework 项目。

### 课程笔记

迁移到现代 .NET(Core 及之后的版本)能带来可观的性能收益,执行速度通常可提升 3 到 5 倍。
然而,现代 C# 技术栈并非这些较新运行时所独有。
业界相当大的一部分,估计占所有 .NET 应用程序的 30%,仍在面向旧的 .NET Framework。

本课程被设计为与目标框架无关。
所讨论的语言特性都可以在面向完整 .NET Framework 的项目中实现。
开发团队中一个常见的误解是,目标框架决定了可用的 C# 语言版本。
实际上,语言版本可以在项目配置中手动指定。

要在任何项目中使用最新的 C# 特性,应当在 `.csproj` 文件中设置 `LangVersion` 属性。
把它设置为 `latest` 或某个具体的版本号后,即使目标框架较旧,编译器也会允许使用现代语法。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/?t=51)

配置好语言版本后,开发者就可以使用现代的语法和特性。
这样可以让旧代码库中的代码更简洁、更易维护。
例如,现代的 LINQ 用法和表达式主体成员可以应用到现有逻辑中:

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/working-on-legacy-projects-69958820/?t=51)

虽然这些语言特性可以跨框架使用,但需要注意的是,某些对性能至关重要的库(例如 LINQ)在完整框架和 .NET Core 之间表现出不同的性能特征。
本课程后面会探讨这些差异,以说明在语言现代化的同时进行平台迁移所带来的好处。

---

## 3. The compilation pipeline

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/) · 1:03

### 总结

本课从宏观层面概述 C# 编译管道,详细说明 C# 编译器(CSC)如何处理 SDK 风格的项目。
它解释了源代码、声明式项目文件与最终构建产物之间的关系,并特别指出现代 .NET 应用程序会生成一个薄包装可执行文件,以及一个包含已编译逻辑的 DLL。

### 核心概念

- **C# 编译器(CSC)**:负责把 C# 源代码翻译成中间语言(IL)的工具,IL 存储在 DLL 或可执行文件中。
- **SDK 风格项目**:现代的声明式 `.csproj` 文件,可以简化项目配置。
- **目标框架**:应用程序构建时所针对运行的 .NET 具体版本(例如 .NET 10)。
- **构建产物**:编译过程的输出,在现代 .NET 中由一个特定于平台的薄包装可执行文件和一个 DLL 组成。

### 课程笔记

C# 编译过程从源代码和项目配置开始。
在现代 .NET 开发中,项目使用 SDK 风格的格式,这种格式高度声明式且简洁。
一个标准的控制台应用程序包含一个源文件(例如 `Program.cs`)和一个项目文件(例如 `SimpleExecutable.csproj`)。

```csharp
static void Main(string[] args)
{
    Console.WriteLine("Hello, DomeTrain!");
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

项目文件定义项目的元数据,包括 `OutputType` 和 `TargetFramework`。
例如,把输出类型设为 `Exe` 表示该项目应生成一个可执行文件,而目标框架则指定运行时环境,例如 `.NET 10`。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

无论是通过 Visual Studio 这样的 IDE 还是通过命令行构建项目,系统都会调用 C# 编译器,也就是 `csc`。
这个编译器随 .NET SDK 一起提供,不过也可以单独安装。
编译器的主要作用是把高级的 C# 代码翻译成 .NET 运行时可以执行的格式。

在现代 .NET(以前称为 .NET Core)中,构建输出与旧版 .NET Framework 略有不同。
虽然项目可以被配置为可执行文件,但真正编译出来的代码位于一个 `.dll` 文件中。
生成的 `.exe` 文件是一个"薄包装"(thin wrapper)或垫片(shim),用于在指定的 .NET 运行时中启动应用程序。

```text
Program.cs + SimpleExecutable.csproj
        |
        v
       csc
        |
        +--> SimpleExecutable.exe   (thin wrapper / shim)
        +--> SimpleExecutable.dll   (compiled code)
```

```text
1>------ Build started: Project: SimpleExecutable, Configuration: Debug Any CPU ------
1> SimpleExecutable -> C:\Sources\GitHub\DomeTrain\MasteringCSharp\src\01\MultiTargeting\bin\Debug\net10.0\SimpleExecutable.dll
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/the-compilation-pipeline-69958821/?t=10)

构建输出确认了所使用的配置(例如 Debug 模式)以及最终产物所在的文件路径。
由于项目针对的是特定版本(例如 .NET 10),生成的应用程序将使用该特定运行时版本运行。

---

## 4. Multi-targeting

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/) · 0:50

### 总结

C# 中的多目标(multi-targeting)允许单个项目从同一份源代码为不同的目标框架生成多个输出。
通过修改项目文件、使用复数形式的 TargetFrameworks 属性,开发者可以确保自己的代码同时针对多种环境(例如旧版 .NET Framework 和现代 .NET 版本)进行编译和打包。

### 核心概念

*   **单目标与多目标**:从单一框架输出过渡到多个框架输出。
*   **TargetFrameworks 属性**:在项目文件中使用这个复数形式的 XML 元素来定义多个目标框架名字对象(Target Framework Moniker,TFM)。
*   **编译器调用**:构建过程会为每个指定的框架各调用一次编译器。
*   **输出结构**:在构建输出文件夹中,各个二进制文件分别生成在特定于框架的子目录里。

### 课程笔记

在大多数开发场景中,应用程序只针对单一框架,例如旧版 .NET Framework 的某个版本,或者 .NET 10 这样的现代版本。
不过,在某些特定情况下,使用同一份源代码、根据目标框架生成不同的输出是很有好处的。
这个过程称为多目标。

要启用多目标,必须修改项目文件(.csproj)。
单数形式的 `<TargetFramework>` 元素要替换成复数形式的 `<TargetFrameworks>` 元素。
这个元素接受一个以分号分隔的目标框架名字对象(TFM)列表,例如 `net48` 和 `net10.0`。

当构建过程被触发时,无论是通过 Visual Studio 还是命令行,编译器都会被调用多次,每个指定的目标框架各一次。
构建不会只生成单个 DLL 或可执行文件,而是生成应用程序的多个版本,每个版本都针对其特定框架进行了优化。

```text
<TargetFrameworks>net48;net10.0</TargetFrameworks>
        |
        +--> compiler (net10.0) --> bin\Debug\net10.0\SimpleExecutable.dll
        +--> compiler (net48)   --> bin\Debug\net48\SimpleExecutable.exe
```

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/multi-targeting-69958822/?t=10)

如构建输出所示,项目会为每个目标生成各自独立的文件。
例如,一个同时针对 .NET 10 和 .NET Framework 4.8 的项目,会为现代运行时生成一个 `.dll`,为旧版框架生成一个 `.exe`,它们分别位于 `bin` 目录中各自特定于框架的子文件夹里。

---

## 5. Adding multi-targeting

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/) · 1:03

### 总结

本课演示如何配置一个 C# 项目,使其同时针对多个 .NET 框架。
内容包括修改项目文件以使用复数形式的 `<TargetFrameworks>` 属性、在 Visual Studio 这样的 IDE 中选择特定目标,以及使用 ILSpy 之类的工具检查编译后的程序集来验证输出。

### 核心概念

* **多目标**:配置单个项目,使其针对多个 .NET 版本进行编译。
* **TargetFramework 与 TargetFrameworks**:在 `.csproj` 文件中用于定义一个或多个目标框架名字对象(TFM)的 XML 元素。
* **IDE 集成**:Visual Studio 如何让开发者在不同目标之间切换,以进行调试和执行。
* **程序集检查**:使用 ILSpy 之类的工具验证已编译 DLL 或 EXE 的元数据和目标框架。

### 课程笔记

在一个标准的 C# 项目中,应用程序针对 .NET 运行时的某个特定版本。
为了演示这一点,来看一个简单的控制台应用程序,它使用 `AppContext.TargetFrameworkName` 打印当前的目标框架名字对象(TFM)。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/?t=10)

项目配置定义在 `.csproj` 文件中。
最初,项目使用 `<TargetFramework>` 元素只针对单一框架,例如 .NET 10.0。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/adding-multi-targeting-69958824/?t=25)

要启用多目标,必须把 `<TargetFramework>` 元素改成复数形式的 `<TargetFrameworks>`。
这样你就可以提供一个以分号分隔的目标框架名字对象列表(例如 `<TargetFrameworks>net10.0;net48</TargetFrameworks>`)。
做出这一更改后,应用程序会针对所有指定的目标重新构建,构建输出中会为每个框架包含单独的文件夹。

在 Visual Studio 中,你可以通过工具栏中的下拉菜单选择执行时使用哪个框架。
例如,选择 ".NET 4.8" 并运行应用程序,就能确认运行环境确实在使用该框架版本。

要验证编译后的输出,可以使用 ILSpy,这是 .NET 社区中的一款常用工具。
打开生成的可执行文件或 DLL,就可以检查程序集元数据,确保它针对的是预期的框架版本,例如 .NET 4.8 或 .NET 10.0。

---

## 6. What is target framework moniker (TFM)?

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/) · 1:20

目标框架名字对象(Target Framework Moniker,TFM)是在 C# 项目文件中用来代替完整框架名称的简写标识符。
常见的例子包括 `net48`、`net10.0`、`net7.0` 和 `netstandard`。
这些名字对象对于定义应用程序如何构建以及可以在哪里运行至关重要。

### 核心概念

- **TFM 作为简写**:像 `net48` 和 `net10.0` 这样的名字对象在项目配置中取代了冗长的框架描述。
- **平台与 BCL 的定义**:TFM 定义了代码编译所针对的平台,并决定了可用的基础类库(Base Class Library,BCL)类型的版本。
- **运行时执行**:对于大多数框架,TFM 指定了将执行该应用程序的运行时。
- **.NET Standard 例外**:与其他 TFM 不同,.NET Standard 定义的是一个 API 表面,而不是某个特定的运行时,从而实现 .NET Framework 与 .NET Core 之间的交叉兼容。
- **元数据可见性**:TFM 存储在程序集元数据中,可以通过 ILSpy 等工具看到,而 C# 语言版本则不会。

### 课程笔记

配置 C# 项目时,目标框架名字对象(TFM)用于指定目标环境。
在项目文件中,它通常出现在 `<TargetFramework>` 或 `<TargetFrameworks>` 属性里。

```xml
<TargetFrameworks>net48;net10.0</TargetFrameworks>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/what-is-target-framework-moniker-tfm-69958823/?t=10)

TFM 有三个主要用途:
1. **编译目标**:它定义了代码编译所针对的具体平台。
2. **类型可用性**:它决定了可用的常用类型。以较旧的框架为目标会把开发者限制在较旧版本的基础类库上,而较新的 TFM 则会解锁现代的 BCL 类型。
3. **运行时选择**:在大多数情况下,它定义了由哪个运行时来执行应用程序。

运行时这条规则的主要例外是 `.NET Standard`。
`.NET Standard` 并不定义某个特定的运行时,而是定义一个一致的 API 表面。
例如,以 `netstandard2.0` 为目标可以确保一个库既能被 .NET Framework 应用程序使用,也能被 .NET Core 应用程序使用。
编译器保证,代码所使用的所有类型在该特定版本的 .NET Standard 所支持的所有运行时中都可用。

虽然用 ILSpy 等工具查看时,TFM 在程序集元数据中清晰可见,但编译时所用的 C# 语言版本并不会记录在元数据中。
这就引出了一个问题:编译器如何决定使用哪个语言版本,以及对于给定的项目,默认设置是什么。

---

## 7. C# Language version vs. Target framework

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/) · 2:48

### 总结

默认情况下,C# 语言版本通常与目标框架绑定,但可以通过项目文件中的 `<LangVersion>` 属性将二者解耦。
虽然官方文档给出了严格的搭配(例如 C# 12 搭配 .NET 8),但许多现代语言特性都可以在 .NET Framework 4.8 这样的旧框架上使用。
这些特性分为三类:开箱即用的特性、需要特定辅助类型(可以通过 polyfill 补齐)的特性,以及需要特定运行时支持的一小部分特性。

### 核心概念

*   **默认映射**:如果没有显式指定,编译器会根据 `TargetFramework` 选择 C# 版本。
*   **语言版本覆盖**:`<LangVersion>` 属性允许在较旧的运行时上使用较新的编译器特性。
*   **特性类别**:语言特性分为不需要运行时支持的、需要特定特性(attribute)或类型的,以及需要运行时改动的三类。
*   **Polyfill**:现代特性所缺失的类型可以手动定义,也可以通过 PolySharp 等库提供,从而在旧框架上启用现代语法。

### 课程笔记

项目的目标框架与编译器所使用的默认 C# 语言版本之间存在直接关系。
如果没有显式定义版本,编译器会根据框架选择一个默认值:
*   **.NET Framework 或 .NET Standard 2.0**: C# 7.3
*   **.NET 5**: C# 9
*   **.NET 6**: C# 10
*   **.NET 7**: C# 11
*   **.NET 8**: C# 12

不过,可以使用项目文件中的 `<LangVersion>` 属性覆盖这一默认行为。
这样,一个以 .NET Framework 4.8 为目标的可执行程序就能使用当前编译器所支持的最新语言特性。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <LangVersion>latest</LangVersion>

    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/?t=10)

就兼容性而言,像 C# 12 这样的版本中的语言特性分为三个不同的类别:
1.  **与运行时无关(约 66%)**:这些特性无论目标框架是什么都能开箱即用。例子包括文件局部类型、checked 运算符和默认结构体。
2.  **依赖类型(约 25%)**:这些特性可以在任何框架上运行,但要求编译期间能找到特定的类型或特性(attribute)。例子包括 `System.Index`、`System.Range` 和 `RequiredMemberAttribute`。
3.  **依赖运行时(约 10%)**:这些特性需要 .NET 运行时本身的特定改动(例如 .NET 7 或 8 中引入的改动)。例子包括 `ref` 字段和接口中的静态抽象成员。

虽然官方文档称特定的 C# 版本只在特定的 .NET 版本上受支持,但在实践中,许多核心 .NET 工具(例如 NuGet、MSBuild 和 Roslyn)在仍以 .NET Framework 为目标的同时使用了最新的语言特性。
这种做法也在官方技术演讲中展示过,例如 Scott Hanselman 和 Stephen Toub 的演讲。

要在旧框架上使用 `init` 专用属性或 `required` 成员等特性,编译器需要相应的特性(例如 `IsExternalInit` 或 `RequiredMemberAttribute`)存在于预期的命名空间中。
它们可以手动添加,也可以通过源代码生成器添加。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/csharp-language-version-vs-target-framework-69958825/?t=160)

---

## 8. Using init-only and required properties in .NET Framework

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/) · 1:40

### 总结

要在 .NET Framework 应用中使用 init-only 属性和 required 属性这类现代 C# 特性,开发者必须手动配置项目的语言版本,并为特定的编译器内部类型提供 polyfill。
由于 C# 编译器采用"鸭子类型"(duck typing)的方式,只要预期的特性类存在于 System.Runtime.CompilerServices 命名空间中,它就能使用这些特性,即使这些类是直接定义在应用代码中而不是基类库中。

### 核心概念

*   **语言版本控制**:.NET Framework 项目默认使用 C# 7.3;要启用更新的语法,需要手动配置 `LangVersion` 属性。
*   **编译器鸭子类型**:C# 编译器通过 `System.Runtime.CompilerServices` 命名空间中是否存在特定类型来判断是否支持 `init` 和 `required` 这类特性,而不管这些类型来自哪里。
*   **IsExternalInit**:编译器支持 `init`-only 属性(在 C# 9.0 中引入)所需的一个标记类。
*   **Required Member Attributes**:要启用 `required` 关键字(在 C# 11.0 中引入),需要 `RequiredMemberAttribute` 和 `CompilerFeatureRequiredAttribute`。

### 课程笔记

在处理遗留的 .NET Framework 4.8 项目时,`init`-only setter 和 `required` 属性这类现代 C# 特性默认不可用。
在标准项目中尝试使用它们会导致编译错误,因为 .NET Framework 的默认语言版本是 C# 7.3。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/?t=10)

要启用这些特性,第一步是在项目文件中覆盖语言版本。
把 `LangVersion` 设置为 `9.0` 或更高可以启用 `init` 属性,而 `required` 关键字则需要 `11.0` 或更高版本。
使用 `latest` 值(在当前环境中解析为版本 14)可以确保编译器能够使用当前所有的语言特性。

即使更新了语言版本,代码仍会编译失败,报错提示 `IsExternalInit` 和其他必需成员未定义。
这是因为 .NET Framework 4.8 的基类库(BCL)中缺少这些类型。
不过,C# 编译器表现出一种"鸭子类型"的特性:它并不关心这些类型来自 BCL 还是应用本身。
只要编译器在编译期间能在 `System.Runtime.CompilerServices` 命名空间中找到预期的类型,这些特性就能正常工作。

要解决这些错误,你必须在应用中定义以下类,以满足编译器的要求:

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-init-only-and-required-properties-in-dotnet-framework-69958826/?t=55)

一旦这些类可用,编译器就能成功实现 `init` 和 `required` 的逻辑,让这些现代 C# 特性在 .NET Framework 环境中完全可用。

---

## 9. Compiler language specific types

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/) · 1:32

现代 C# 特性经常依赖编译器期望在运行时或项目中找到的特定类型和特性。
虽然这些都内置在现代 .NET 版本中,但面向 .NET Framework 4.8 等旧框架的开发者,仍然可以通过手动或借助库提供缺失的类型,来使用其中许多语言特性。

### 核心概念

* **依赖编译器的类型**:C# 编译器会查找特定类型(例如 `IsExternalInit`、`RequiredMemberAttribute`)来启用语法特性。
* **特定于特性的要求**:不同的 C# 版本(C# 8 到 C# 12)需要不同的特性和类型集合。
* **面向旧框架**:只要提供所需的元数据类型,就可以在 .NET Framework 4.8 等遗留平台上使用现代 C# 语法。
* **IDE 与元数据特性**:`StringSyntaxAttribute` 和 `StackTraceHiddenAttribute` 这类特性提供 IDE 专属的好处,例如语法高亮和更清晰的调试。

### 课程笔记

C# 编译器的可扩展性很强,它依赖特定类型和特性的存在来启用现代语言特性。
虽然其中许多特性在较新的 .NET 版本中开箱即用,但另一些则需要特定的运行时支持或元数据特性的存在。
有趣的是,截至 C# 13 和 C# 14,没有新特性依赖编译器的这种特定行为。

#### 按版本划分的特性依赖

对特定类型的需求跨越了多个 C# 版本:
* **C# 12**:集合表达式的"自定义构建器"。
* **C# 11**:required 属性和列表模式(需要 `System.Index` 和 `System.Range`)。
* **C# 10**:异步方法构建器、插值字符串改进,以及调用参数表达式推断(需要特殊的特性)。
* **C# 9**:`SkipLocalsInit` 和模块初始化器。
* **C# 8**:可空引用类型(需要可空特性)、范围(`System.Index` 和 `System.Range`),以及异步流(需要一个异步接口的包)。

#### IDE 与元数据特性

有些特性依赖特性类主要是为了 IDE 行为,而不是为了核心语言逻辑:
* **语法高亮**:`StringSyntaxAttribute` 允许自定义语法高亮。
* **调试**:`StackTraceHiddenAttribute` 允许在堆栈跟踪中隐藏特定步骤。

#### 支持旧框架

面向 .NET Framework 4.8 等旧框架时,标准库中不存在这些类型。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <LangVersion>latest</LangVersion>
    <TargetFramework>net48</TargetFramework>
  </PropertyGroup>
</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/?t=64)

不过,编译器只按名称和命名空间检查这些类型是否存在;它并不严格要求这些类型来自官方运行时库。
通过在 `System.Runtime.CompilerServices` 命名空间中手动定义预期的特性和类,开发者可以在遗留平台上解锁现代特性。
这种做法使得即使底层框架并不原生支持,也能使用 `init` 和 `required` 属性。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/compiler-language-specific-types-69958827/?t=78)

虽然可以手动维护这些类型,但在管理多个目标框架时会变得复杂,因为不同的版本需要不同的类型子集。

---

## 10. PolySharp: source-only polyfills for C# Language

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/) · 0:54

PolySharp 是一个纯源码形式的 polyfill 库,它在编译时把所需的支持类型直接注入程序集,从而让较旧的目标框架也能使用现代 C# 语言特性。
这些类型以 internal 形式生成,避免污染公共 API,而且该库只作为编译时依赖存在,也就是说它不会出现在最终的运行时分发内容中。

### 核心概念

- **纯源码 polyfill**:把缺失的编译器支持类型的 C# 源代码直接注入项目。
- **编译时依赖**:该库只在构建过程中使用,运行时并不需要它。
- **internal 可见性**:生成的类型被标记为 `internal`,以避免冲突并保持公共 API 整洁。
- **旧框架支持**:在 .NET Framework 4.8 这样的框架上启用 `init` 属性和 `required` 成员等特性。

### 课程笔记

PolySharp 为在面向旧版 .NET 的项目中使用现代 C# 语言特性提供了一种解决方案。
虽然 C# 编译器通常能理解新语法,但它要求目标框架中存在特定的类型(例如 `init` 属性所需的 `IsExternalInit`)。
PolySharp 会自动创建这些类型。

在编译时,PolySharp 会把必要的源代码添加到你的程序集中。
这确保编译器拥有使用现代特性所需的一切,而无需更新的运行时。
由于这些类型以 `internal` 可见性生成,它们不会泄漏到你的库或应用程序的公共接口中。

要使用 PolySharp,必须把这个包添加到项目文件中,并配合特定的资产设置,以确保它始终只是编译时依赖。
下面的配置演示了如何为一个面向 .NET Framework 4.8 的项目启用现代 C# 特性:

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/?t=0)

引用这个包之后,你就可以使用那些在旧框架上原本会导致编译错误的现代 C# 关键字。
例如,可以用 `required` 和 `init` 关键字在类中定义属性,编译器会使用 PolySharp 提供的 polyfill 来满足元数据要求。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/polysharp-source-only-polyfills-for-csharp-language-69958828/?t=15)

对于共享库或旧应用程序来说,这种做法尤其有用:你希望利用现代语言带来的生产力特性,又不想强制升级运行时或添加沉重的外部依赖。

---

## 11. Using PolySharp for init-only and required properties

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/) · 0:53

### 总结

在面向 .NET Framework 4.8 这类较旧框架时,PolySharp 提供了一种无缝的方式来使用 init-only 属性和 required 属性等现代 C# 特性。
PolySharp 不需要你手动定义缺失的编译器支持类型,而是作为一个源代码生成器,把这些类型以 internal 类的形式直接注入程序集。
这种做法确保项目与最新的语言版本保持兼容,同时借助仅编译时资产保持依赖关系图的整洁。

### 核心概念

- 在旧框架(例如 .NET 4.8)上启用现代 C# 特性。
- 通过源代码生成器自动生成编译器支持类型。
- 使用 `PrivateAssets="all"` 确保 PolySharp 是仅编译时依赖。
- 生成类型的 internal 可见性,用于避免在使用方项目中产生冲突。

### 课程笔记

当项目面向 .NET Framework 4.8 这样的旧框架却使用最新的 C# 语言版本时,`init` 和 `required` 属性等特性将无法编译。
这是因为编译器期望目标框架的基础类库中存在特定的元数据类型,例如 `IsExternalInit` 或 `RequiredMemberAttribute`。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/?t=10)

要在不手动添加样板类的情况下解决这个问题,可以把 PolySharp NuGet 包添加到项目中。
这个包使用源代码生成器在编译时提供所需的类型。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-polysharp-for-init-only-and-required-properties-69958829/?t=25)

一个关键的配置细节是使用 `<PrivateAssets>all</PrivateAssets>`。
这确保 PolySharp 是仅编译时依赖;它不会传递给引用此程序集的项目,也不需要运行时 DLL。

在 ILSpy 这类工具中查看生成的程序集时,PolySharp 生成的类型会以 `internal` 类的形式出现在各自预期的命名空间中,例如 `System.Runtime.CompilerServices` 或 `System.Diagnostics.CodeAnalysis`。
这让 C# 编译器能够满足现代语法的要求,同时保持程序集的公共 API 接口整洁,没有不必要的外部依赖。

---

## 12. Choosing the right C# Language version

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/) · 0:43

### 总结

本课详细介绍 C# 项目文件中 LangVersion 属性的配置,解释 preview、latest、latestMajor 以及固定到特定版本之间的区别。
它强调构建可复现性的重要性,并警告说,由于所安装的 .NET SDK 存在差异,使用 latest 设置可能导致在不同环境中的构建结果不确定。

### 核心概念

- 在 .csproj 文件中配置 LangVersion 属性。
- 用于访问实验性编译器功能的 preview 值。
- 用于自动选择版本的 latest 和 latestMajor 值。
- 固定到特定版本(例如 14、15)以保证构建稳定。
- 使用不确定的版本设置时,CI/CD 流水线中出现构建失败的风险。

### 课程笔记

C# 语言版本通过项目 `.csproj` 文件中的 `<LangVersion>` 属性来控制。
开发者有多种策略来定义编译器可以使用哪些语言功能。

#### 语言版本选项

有若干关键字和值可以赋给 `LangVersion`,以决定编译器的行为:

- **preview**:此选项启用编译器的实验性功能。当开发者想测试尚未定稿、或尚未在稳定版 SDK 中发布的即将推出的 C# 功能时,就会使用它。
- **latest**:此设置指示编译器使用当前机器上所安装的 SDK 支持的最高 C# 版本。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/?t=9)

- **latestMajor**:此选项选择可用的最新主版本。在前些年 C# 使用小版本发布(例如 7.1、7.2 或 7.3)时,这一点尤其有用。在当前的发布周期中,Microsoft 通常专注于发布主版本,这使得 `latestMajor` 与 `latest` 之间的区别不再像过去那样明显。
- **特定版本**:为了获得最大的控制力,开发者可以指定一个确切的版本号,例如 `14` 或 `15`。这可以确保无论在什么环境中,代码都针对一组已知的功能进行编译。

#### 构建可靠性与 "latest" 警告

官方文档建议不要在生产环境中使用 `latest` 语言版本。
主要的顾虑是构建可靠性。
由于 `latest` 取决于环境中的 SDK,如果持续集成(CI)环境的 SDK 版本不同(通常更旧),一个项目可能在开发者的本地机器上构建成功,却在 CI 流水线中失败。

为了缓解这些环境差异,并确保构建在所有机器上都保持确定性,开发者通常会使用 `global.json` 文件,在固定语言版本的同时固定 SDK 版本。

```json
{
  "sdk": {
    "version": "10.0.106"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/choosing-the-right-csharp-language-version-69958830/?t=36)

通过把 `.csproj` 中的特定 `LangVersion` 与 `global.json` 中固定的 SDK 版本结合起来,你可以确保构建过程在不同的开发和部署阶段都可靠且一致。

---

## 13. 'latest' vs. specific C# Language version

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/) · 3:27

### 总结

在 `latest` C# 语言版本与特定版本之间做选择,需要在自动采用新功能与构建稳定性之间取得平衡。
虽然设置 `<LangVersion>latest</LangVersion>` 能确保解决方案中的所有项目都与当前 SDK 的能力保持同步,但在升级 .NET SDK 时,它可能引入破坏性变更。
例如,从 C# 13 迁移到 C# 14 时,可能因为 `field` 这样的新保留关键字,或 LINQ 扩展方法的重载解析发生变化,而破坏现有代码。
为了在使用 `latest` 的同时保持构建可复现,开发者应当使用 `global.json` 文件固定 SDK 版本,从而让整个开发团队以受控的方式进行升级。

### 核心概念

*   **LangVersion 属性**:在 `.csproj` 文件中配置,用于决定编译器可以使用哪些 C# 语言功能。
*   **latest 与特定版本**:`latest` 设置跟随所安装的 .NET SDK 提供的编译器版本,而特定版本(例如 `13.0`)无论 SDK 如何更新都保持不变。
*   **C# 14 中的破坏性变更**:为半自动属性引入的 `field` 关键字,以及 `Reverse()` 方法重载解析的变化,可能破坏在 C# 13 中有效的代码。
*   **固定 SDK**:使用 `global.json` 可以确保编译器在不同的开发者机器和构建环境中行为一致。
*   **项目一致性**:使用 `latest` 有助于防止"版本分化",即同一解决方案中的不同项目面向不同的 C# 版本。

### 课程笔记

当一个项目面向特定的 .NET 版本并使用 `latest` 语言版本时,实际使用的 C# 版本由所安装的 .NET SDK 决定。
例如,一个项目可能被配置为使用 .NET 9 SDK,并在 `.csproj` 文件中把语言版本设置为 `latest`。

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>
</Project>
```

为了确保构建环境可复现,使用 `global.json` 文件固定 SDK 版本:

```json
{
  "sdk": {
    "version": "9.0.313"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=10)

在这个环境中,`latest` 设置对应 C# 13。
如果 SDK 升级到支持 C# 14 的版本(例如 .NET 10),在 C# 13 中有效的代码可能无法编译。
来看下面这个在 .NET 9 SDK 下有效的实现:

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=25)

当 SDK 升级到版本 10 时,编译器转为使用 C# 14,上面的代码中会出现两个主要的破坏性变更:

1.  **`field` 关键字**:C# 14 引入了 `field` 关键字,用于半自动属性。因此,在属性内部把 `field` 用作局部变量名,现在会导致编译器冲突,因为编译器会把它解释为保留关键字。
2.  **重载解析**:在 C# 13 中,`parts.Reverse()` 对应 LINQ 的 `Enumerable.Reverse` 方法,它返回一个 `IEnumerable<T>`。在 C# 14 中,如果同时存在 `using System;` 和 `using System.Linq;`,编译器可能会把该调用解析为 `MemoryExtensions.Reverse`(一个基于 Span 的方法)。由于基于 Span 的 `Reverse` 方法执行就地修改并返回 `void`(或一个与后续 `.First()` 调用不兼容的类型),代码会编译失败。

为了模拟这次升级,把 `global.json` 更新为面向较新的 SDK:

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestFeature"
  }
}
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/latest-vs-specific-csharp-language-version-69958831/?t=130)

#### 使用 'latest' 的权衡

使用 `latest` 语言版本有若干优点和缺点,在做决定之前应当加以考虑:

*   **优点**:
    *   **一致性**:它可以防止大型解决方案中的不同项目漂移到不同的 C# 版本上(例如一些用版本 8,另一些用 11),从而确保功能集统一。
    *   **集中控制**:语言版本实际上由 `global.json` 中定义的 SDK 版本来控制。升级 SDK 会自动升级仓库中所有项目的语言版本。
*   **缺点**:
    *   **意外的破坏性变更**:如前所示,由于新的关键字或解析规则,SDK 升级可能破坏在上一个语言版本中完全有效的现有代码。
    *   **手动升级**:如果使用特定版本(例如 `<LangVersion>13.0</LangVersion>`),每当新版本发布时,你都必须手动更新每一个项目文件,这在大型解决方案中可能是相当大的维护负担。

无论如何选择,使用 `Directory.Build.props` 之类的工具集中管理项目配置,都有助于在一个地方管理这些设置。

---

## 14. Using Directory.Build.props

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/) · 3:30

### 总结

Directory.Build.props 是一项 MSBuild 功能,让开发者可以在整个解决方案中集中管理通用的项目属性和项引用。
把目标框架、语言版本和全局 NuGet 包等共享配置移到这个文件中,就不再需要在每个单独的 .csproj 文件里维护相同的设置。
这个分层系统支持继承和覆盖,从而让构建逻辑采用一种整洁的 DRY(Don't Repeat Yourself)方式,在项目规模扩大时也更易于管理。

### 核心概念

*   **传统风格与 SDK 风格项目**:传统项目冗长,且对构建系统不透明,而 SDK 风格项目简洁,并且更擅长处理传递依赖。
*   **集中化**:把通用属性(例如 `TargetFramework`、`LangVersion`)移到单个文件中,以避免重复。
*   **Directory.Build.props**:一种特殊的 MSBuild 文件,会被其所在目录或子目录中的项目自动导入。
*   **分层覆盖**:在更靠近项目的位置放置另一个 `Directory.Build.props` 文件,就可以在嵌套文件夹中覆盖属性。
*   **重构构建逻辑**:像对待代码一样对待构建配置,把共同部分提取到单一的事实来源中。

### 课程笔记

现代 C# 开发区分两种项目文件格式:传统风格和 SDK 风格。
传统项目非常冗长,而且可能对构建系统不透明。
例如,如果 Project A 引用了 Project B,构建系统可能无法正确看到所有包依赖,这会在大型解决方案中引发问题。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=40)

相比之下,SDK 风格项目要整洁得多。
不过,当一个解决方案包含多个项目(例如 Project A、Project B 和 Project C)时,它们往往会重复相同的属性,例如目标框架、语言版本,以及 PolySharp 这类通用包引用。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=70)

为了消除这种重复,你可以把这些通用属性提取到一个名为 `Directory.Build.props` 的文件中。
这些属性移走之后,各个项目文件只需要包含项目特有的配置,例如 `OutputType` 或项目引用。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=120)

`Directory.Build.props` 文件充当共享构建逻辑的中央存储库。
如果你决定更改目标框架或更新语言版本,只需要更新这一个文件,而不必更新存储库中的每个项目。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/using-directory-build-props-69958832/?t=130)

这个系统是分层的,类似于 `.editorconfig` 文件的工作方式。
你可以在嵌套文件夹中放置一个 `Directory.Build.props` 文件,以覆盖父目录中定义的属性。
离项目最近的文件优先,这样既能保留全局默认值,又能对大型解决方案中的特定子部分进行细粒度控制。

---

## 15. Conclusion

> [观看本课](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/) · 1:17

### 总结

本课为现代 C# 技术栈这一模块收尾,强调大约 90% 的现代 C# 语言功能都可以使用,与目标框架无关,包括 .NET Framework 和 .NET Standard。
虽然大多数功能开箱即用,但有些功能需要编译器支持类型,这些类型可以通过手动 polyfill 或 PolySharp 这类自动化工具提供。
本模块还强调了使用 Directory.Build.props 集中管理构建配置的重要性,以确保大型解决方案的一致性和可维护性。

### 核心概念

- **语言支持与运行时支持**:大多数 C# 功能由编译器驱动,只有大约 10% 需要特定的运行时支持。
- **Polyfill**:通过提供缺失的支持类型,在较旧的框架上启用 `init` 和 `required` 等现代功能。
- **PolySharp**:一个源代码生成器,会在编译时自动创建所需的 polyfill 类型。
- **语言版本控制**:使用 `LangVersion` 属性覆盖与目标框架(TFM)关联的默认 C# 版本。
- **集中式配置**:使用 `Directory.Build.props` 在整个解决方案中管理 `LangVersion` 等属性和包引用。

### 课程笔记

现代 C# 开发在很大程度上与底层运行时解耦。
近年来发布的语言功能中,大约 90% 即使在面向 .NET Framework 4.8 或 .NET Standard 等传统环境时也可以使用。
这些功能由编译器处理,不需要特定的运行时更新。

不过,有些功能依赖于框架中存在特定的类型。
例如,`init`-only 属性和 `required` 成员需要 `System.Runtime.CompilerServices` 命名空间中的 `IsExternalInit` 或 `RequiredMemberAttribute` 等类型。
当较旧的框架中缺少这些类型时,可以手动提供它们,或者使用 PolySharp 自动生成。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/?t=21)

只有大约 10% 的语言功能需要显式的运行时支持才能工作。
对于其余功能,目标框架(TFM)与 C# 语言版本之间的关系是灵活的。
虽然每个 TFM 都有一个默认的语言版本,但可以在项目文件中显式覆盖它。

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    
    <TargetFramework>net9.0</TargetFramework>
    <LangVersion>latest</LangVersion>    
  </PropertyGroup>

</Project>
```

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/?t=38)

为了避免在数百乃至数千个项目中重复这些配置,应当使用 `Directory.Build.props` 文件把与构建相关的决策封装在一个位置。
这个文件让开发者可以全局定义 `LangVersion`、`ImplicitUsings` 和 `Nullable` 等属性。
如果某个决策需要更改,例如从 `latest` 切换到特定的 C# 版本,只需要在这一个文件中更新即可。

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

[▶ 观看](https://dometrain.com/take/course/mastering-csharp-3256129/conclusion-69958833/?t=46)

下一个模块将深入探讨数据类型,涵盖类、结构体、记录和元组之间的区别。

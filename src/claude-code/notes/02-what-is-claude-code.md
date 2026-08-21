# What is Claude Code

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 2
> 7 lessons · ~33 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958766/) | 7:43 | [↓](#1-introduction) |
| 2 | [Claude Code vs Claude Chat vs Cowork](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-code-vs-claude-chat-vs-cowork-69958767/) | 4:07 | [↓](#2-claude-code-vs-claude-chat-vs-cowork) |
| 3 | [Where Claude Code Runs](https://dometrain.com/take/course/getting-started-claude-code-3256128/where-claude-code-runs-69958768/) | 3:27 | [↓](#3-where-claude-code-runs) |
| 4 | [Installing Claude Code](https://dometrain.com/take/course/getting-started-claude-code-3256128/installing-claude-code-69958769/) | 3:38 | [↓](#4-installing-claude-code) |
| 5 | [Your First Prompt](https://dometrain.com/take/course/getting-started-claude-code-3256128/your-first-prompt-69958770/) | 4:38 | [↓](#5-your-first-prompt) |
| 6 | [Navigating the Interface](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/) | 7:02 | [↓](#6-navigating-the-interface) |
| 7 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958772/) | 2:19 | [↓](#7-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958766/) · 7:43

### 摘要

Claude Code 是一款智能体式编码工具（agentic coding tool），它与标准的自动补全工具或基于聊天的 AI 助手不同，能够理解整个代码库、编辑文件并执行终端命令。
它通过一个"智能体循环"（agentic loop）运作，在其中使用大语言模型（LLM）作为推理引擎，来决定实现特定目标所需的操作，例如读取文件或运行测试。
通过充当用户与 LLM 之间的中介，Claude Code 为模型提供了直接与本地开发环境以及 MCP 服务器等外部数据源交互所需的"工具"。

### 关键概念

- **智能体式编码工具（Agentic Coding Tool）**：一种能够与环境交互以完成复杂任务的软件智能体，而不仅仅是提供文本建议。
- **智能体循环（The Agentic Loop）**：不断收集上下文、采取行动并验证结果，直到完成任务的迭代过程。
- **工具使用（Tool Use）**：使无状态的 LLM 能够执行诸如读取文件（`read_file`）或执行终端命令等操作的机制。
- **上下文收集（Context Gathering）**：智能体从代码库或网络中检索必要信息以指导 LLM 决策的步骤。
- **模型上下文协议（Model Context Protocol，MCP）**：一种协议，用于通过将 Claude Code 连接到额外的外部工具和数据来扩展其能力。

### 课程笔记

Claude Code 是一款智能体式编码工具，这使它有别于 Copilot 这样的自动补全功能、ChatGPT 这样的聊天界面，或 Cursor 这样的 AI 集成编辑器。
它旨在理解你的代码库、编辑文件、运行命令，并与其他开发工具集成，以提高生产力。

AI 智能体是一种能够与其环境交互并执行操作以完成既定目标的软件。
Claude Code 的运作方式是让大语言模型（LLM）实时参与循环，使其能够访问可以在你的机器上或项目内执行任务的工具。

#### 能力

Claude Code 可以执行多种任务：
- **代码库分析**：它可以读取并理解你的项目，以解释功能或追踪错误。
- **文件操作**：它可以执行跨多个文件的重构，并更新项目中的引用。
- **终端交互**：它可以执行构建脚本或运行测试，分析输出，并根据结果决定下一步操作。
- **网络集成**：它可以在网络上搜索文档或最新的 API 参考资料。

#### 智能体循环

Claude Code 的核心是智能体循环，它遵循一个特定的周期：
1. **提示（Prompt）**：用户提供一个任务或使命（例如，"重构这个文件"）。
2. **收集上下文（Gathering Context）**：智能体评估提示并收集必要的信息，例如读取相关文件以纳入与 LLM 的对话中。
3. **采取行动（Taking Action）**：智能体决定下一步操作，并执行一个工具（例如文件编辑或终端命令）以朝目标推进。
4. **验证（Verification）**：智能体检查目标是否已经实现。如果任务成功，流程结束；否则，返回到收集上下文阶段，进行另一轮循环。

用户可以在此循环期间与智能体交互，以提供指导或额外的上下文。

#### 工具执行与 LLM

LLM 是无状态的 API，无法直接与本地机器或代码库交互。
Claude Code 充当执行层。
当用户询问关于特定文件的问题时，Claude Code 会向 LLM 提供可用工具的列表以及如何请求这些工具的说明。

例如，如果用户询问 `README.md` 中记录了什么内容，LLM 会意识到它需要查看文件内容，并会发出对 `read_file` 的工具调用。
Claude Code 随后从磁盘读取该文件，并将内容返回给 LLM。

```markdown
# Getting Started with Claude Code

Welcome to the ["Getting Started with Claude Code"](https://dometrain.com/course/getting-started-claude-code/?ref=dometrain-github) course on Dometrain!

This course teaches you how to use Claude Code effectively as an AI-powered coding assistant. You'll learn the fundamentals of working with Claude Code, how to guide it with context files, how to set up your projects, everyday development and git workflows, how to choose models and control cost, and how to extend Claude Code with custom skills, subagents, and MCPs.
 
## Getting Started

The **main branch** contains the most up-to-date version of the code, reflecting the latest improvements, updates, and fixes.

Each section in the course has a folder in the repository. The folder contains the source code for the section, containing both a `/start` and `/end` directory—which aligns with the source code at a point in time as it relates to the course.

## Course Structure

- [Section 2: What is Claude Code](./section-02/)
- [Section 3: How Context Works](./section-03/)
- [Section 4: Setting Up Your Projects](./section-04/)
- [Section 5: Development Workflows](./section-05/)
- [Section 7: Git Workflows](./section-07/)
- [Section 8: Customising Claude Code](./section-08/)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958766/?t=282)

通过使用工具和 MCP 服务器，Claude Code 扩展了 LLM 的推理能力，使其成为应对实际开发任务的实用且有用的助手。

## 2. Claude Code vs Claude Chat vs Cowork

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-code-vs-claude-chat-vs-cowork-69958767/) · 4:07

### 摘要

Anthropic 为其 Claude 模型提供了三种不同的界面：Claude Chat、Claude Code 和 Claude Cowork。
尽管它们共享相同的底层智能，但针对不同的任务进行了优化：Chat 用于一般性对话和研究，Cowork 用于非开发者的文件自动化，而 Claude Code 则用于基于终端的软件开发，拥有完整的文件系统和命令执行访问权限。

### 关键概念

* **统一的模型核心（Unified Model Core）**：这三款产品都利用相同的 Anthropic Claude 模型，但提供不同的功能封装。
* **Claude Chat（claude.ai）**：一个基于网页的对话界面，非常适合研究、头脑风暴和处理单一任务，且无法访问本地环境。
* **Claude Code**：一款面向开发者的 CLI 工具，运行于本地终端中，能够读写文件并执行 shell 命令。
* **Claude Cowork**：一款面向非技术用户的本地机器智能体，旨在自动化文件管理、电子表格处理和文档工作流程。
* **运营效率（Operational Efficiency）**：Claude Code 应保留用于处理复杂任务；像修正拼写错误或重命名变量这样的简单编辑，手动处理效率更高。

### 课程笔记

Anthropic 提供了一套三款产品，它们利用相同的底层 Claude 模型，但设计用于不同的专业任务。
理解 Claude Chat、Claude Code 和 Claude Cowork 之间的区别，对于为特定工作流程选择合适的工具至关重要。

#### 产品比较

Claude Chat，通常简称为"Claude"，是位于 claude.ai 的一个基于网页的对话界面。
它的功能类似于一个"思考伙伴"或研究助手。
它在处理单一任务方面非常有效，例如总结一份文档或研究某个特定概念，但它缺乏执行命令或直接与本地文件系统交互的能力。

Claude Cowork 是聊天界面与以开发者为中心的工具之间的桥梁。
它专为需要在本地机器上管理文件的非开发者设计。
它的主要使用场景包括整理文件结构、在电子表格中工作以及自动化与文档相关的工作流程。

Claude Code 是面向软件开发者的专用工具。
与网页界面不同，Claude Code 直接运行于终端中。
这种本地驻留使模型能够访问文件系统，并能够运行 shell 命令，以验证所请求的任务是否已成功完成。

#### 选择合适的工具

在这些工具之间做出选择，取决于任务的性质和所处的环境：
* **Claude Code**：最适合在代码库或包含大量文本文件的项目中工作，且需要执行命令的场景。
* **Claude Chat**：最适合高层次的研究、快速提问，或不需要本地文件访问的头脑风暴环节。
* **Claude Cowork**：最适合行政任务、文档制作和电子表格管理。

#### Claude Code 的策略性使用

尽管 Claude Code 是一个强大的智能体，但对于每一项开发任务来说，它并不总是最高效的选择。
对于诸如修正一个拼写错误、重命名单个变量或基本文件格式化这类琐碎操作，初始化智能体的开销以及相关的 token 成本，可能会超过手动完成该任务所需的成本。
Claude Code 在应用于需要多步推理、架构决策或代码库内复杂研究的任务时最为有效。

## 3. Where Claude Code Runs

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/where-claude-code-runs-69958768/) · 3:27

### 摘要

Claude Code 为 AI 辅助开发提供了灵活的环境，支持独立的桌面应用程序、面向 VS Code 和 JetBrains 的 IDE 扩展，以及功能强大的命令行界面（CLI）。
虽然开发者可以选择最适合其工作流程的环境——包括集成到 GitHub Actions 等 CI/CD 流水线中——但基于终端的 CLI 通常是性能最佳、功能最完整的选项。
本课程之所以聚焦于终端实现，是因为它具有内存效率优势，并且是新功能发布的主要平台。

### 关键概念

* 支持多种环境，包括桌面端、IDE、终端和网页端。
* 与 VS Code、JetBrains、Cursor 和 Windsurf 的 IDE 兼容性。
* 终端/CLI 的优势：更早获得新功能，以及内存效率更高。
* 与 GitHub Actions 的集成能力，用于自动化代码审查。

### 课程笔记

Claude Code 被设计为可以跨多种环境运行，以适应不同的开发者工作流程。
用户可以安装 Claude 桌面应用程序，它为 Claude Code、Claude Work 以及标准的聊天功能提供了一个集中的中心入口。
对于那些更喜欢直接在代码编辑器中工作的人，VS Code 和 JetBrains IDE 都提供了相应的扩展。
由于 Cursor 和 Windsurf 等编辑器构建于与 VS Code 相同的基础之上，它们也可以利用这些扩展。

终端环境的安装通常通过一个 shell 脚本发起：

```bash
curl -fsSL https://claude.ai/install.sh | bash
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/where-claude-code-runs-69958768/?t=10)

尽管存在多种界面，但终端（CLI）是本课程中始终使用的主要环境。
CLI 通常是最先获得新功能和更新的平台，这使其成为希望使用最新能力的开发者的首选。
此外，终端界面比 IDE 集成版本更节省内存。

在终端中，Claude Code 可以执行深入的代码库分析，例如识别入口点并解释项目结构：

```plaintext
Claude Code v2.1.76
Opus 4.6 (1M Context) · Claude Enterprise
/Users/johnnie/taskflow

> I just joined the team. Can you give me a high-level overview of how this
  codebase is structured and where the main entry points are?

• I'll explore the codebase to give you a comprehensive overview.

• Explore(Explore codebase structure)
  └ Done (17 tool uses · 38.0k tokens · 28s)

* Clauding... (esc to interrupt)

• TaskFlow API - Codebase Overview

  Welcome to the team! This is a Node.js/Express REST API for task management
  with team collaboration features. Here's the project structure:

  taskflow/
  ├── src/
  │   ├── index.js         # Main entry point - starts the server
  │   └── ...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/where-claude-code-runs-69958768/?t=70)

Claude Code 还支持远程和自动化环境。
它可以将本地机器的连接桥接到网页界面，或集成到 CI/CD 流水线中。
例如，Claude 可以在 GitHub Actions 中用于执行自动化代码审查。
开发者通常会根据项目的具体需求或任务的复杂程度，在终端和 IDE 之间切换。

```javascript
while(curious) {
    question_everything();
    dig_deeper();
    connect_dots(unexpected);

    if (stuck) {
        keep_thinking();
    }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/where-claude-code-runs-69958768/?t=160)

## 4. Installing Claude Code

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/installing-claude-code-69958769/) · 3:38

### 摘要

Claude Code 是一款命令行工具，可以在 macOS、Linux 和 Windows 上原生安装。
相比 Homebrew 或 WinGet 等包管理器，更推荐使用原生安装方式，因为它支持自动更新。
安装完成后，用户必须使用 Claude 订阅或 Anthropic Console API 密钥进行身份验证。
该工具内置了诊断实用程序，例如 `/doctor` 命令，用于排查环境问题。

### 关键概念

* 原生安装与包管理器（Homebrew/WinGet）的对比
* 原生构建版本支持自动更新
* 首次运行时的身份验证与 `/login` 命令
* 基于订阅的计费方式与基于 API（Console）的计费方式
* 使用 `/doctor` 进行诊断故障排查

### 课程笔记

Claude Code 通过官方文档提供的终端命令进行安装。
虽然 macOS 用户可以使用 Homebrew，Windows 用户可以使用 WinGet，但推荐的方法是原生安装。
原生安装能够保证软件保持最新状态，因为它支持自动更新，而包管理器对于这个特定工具可能不支持此功能。

```bash
# macOS and Linux
curl -fsSL https://claude.ai/install.sh | bash

# Windows (PowerShell)
irm https://claude.ai/install.ps1 | iex

# Windows (CMD)
curl -fsSL https://claude.ai/install.cmd -o install.cmd && install.cmd
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/installing-claude-code-69958769/?t=10)

安装脚本执行完成后，Claude Code 即可使用。
要启动该工具，请导航到包含你的源代码的目录，并运行 `claude` 命令。

```bash
✓ Claude Code successfully installed!

Version: 2.1.140

Location: ~/.local/bin/claude

Next: Run claude --help to get started

✓ Installation complete!

claude
Claude Code v2.1.140
Opus 4.7 (1M context) · Claude Max
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/installing-claude-code-69958769/?t=85)

在首次执行时，Claude Code 会提示进行身份验证。
如果你需要重新进行身份验证或更改账户类型，请使用 `/login` 命令。
该命令提供三种主要的模型连接方式：带订阅的 Claude 账户（Pro、Max、Team 或 Enterprise）、Anthropic Console 账户（按 API 用量计费），或第三方平台（Amazon Bedrock、Microsoft Foundry 或 Google Vertex AI）。

```shell
> /login

Login

Claude Code can be used with your Claude subscription or billed based on API usage through your Console
account.

Select login method:

> 1. Claude account with subscription · Pro, Max, Team, or Enterprise
  2. Anthropic Console account · API usage billing
  3. 3rd-party platform · Amazon Bedrock, Microsoft Foundry, or Vertex AI
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/installing-claude-code-69958769/?t=160)

如果在使用过程中遇到错误或配置问题，`/doctor` 命令可以诊断安装情况。
它会检查当前版本、平台详情，并识别潜在的冲突，例如如果之前通过 npm 安装过旧版本，会检测到孤立的 npm 全局包。

```shell
> /doctor

Diagnostics
 Currently running: native (2.1.140)
 Commit: 89b4b3854fac
 Platform: darwin-arm64
 Path: /Users/guilhermeferreira/.local/share/claude/versions/2.1.140
 Config install method: native
 Search: OK (bundled)

 Multiple installations found
 npm-global-orphan at /opt/homebrew/lib/node_modules/@anthropic-ai/claude-code
 native at /Users/guilhermeferreira/.local/bin/claude
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/installing-claude-code-69958769/?t=130)

## 5. Your First Prompt

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/your-first-prompt-69958770/) · 4:38

### 摘要

Claude Code 提供了一个用于与代码库交互的智能体式界面。
通过在项目目录中运行 `claude` 命令，开发者可以提示模型解释架构流程、生成诸如 ASCII 艺术之类的可视化内容，并执行复杂的重构。
该工具在一个智能体循环中运作：读取文件、为文件系统更改或命令执行（例如 .NET 构建）请求权限，并提供交互式反馈，以确保在保持项目稳定性的同时达成预期结果。

### 关键概念

- 在项目目录中使用 `claude` CLI 命令初始化一个会话。
- 通过架构性和流程性提示来理解项目。
- 智能体循环：文件发现、工具使用和 token 消耗监控。
- 交互式重构，由用户引导决策（例如，在扩展方法与内联清理之间做选择）。
- 针对文件系统修改和 shell 命令执行的权限管理。
- 自动化构建验证，以确保重构的完整性。

### 课程笔记

要开始使用 Claude Code，请导航到项目目录并执行 `claude` 命令。
这会初始化一个会话，智能体可以在其中访问本地文件系统以理解项目结构。
例如，你可以提示智能体解释 .NET API 与 React 前端之间的交互方式。
在此过程中，智能体会进入一个智能体循环，读取相关文件并在分析代码库时消耗 token。

Claude Code 可以提供架构概览，识别端点、请求流程和项目边界。
你可以请求特定的可视化内容，例如 ASCII 艺术，来展示来自网页应用的 HTTP POST 请求如何到达特定的 .NET API 端点。

```csharp
var builder = WebApplication.CreateBuilder(args);
var config  = builder.Configuration;

builder.Services.AddDbContext(o =>
    o.UseNpgsql(config.GetConnectionString("Postgres")))
builder.Services.AddProblemDetails();
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/your-first-prompt-69958770/?t=175)

除了探索之外，Claude Code 还可以执行主动重构。
当被要求重构像 `Program.cs` 这样的文件时，智能体可能会要求澄清期望的方式，例如是使用扩展方法，还是进行简单的内联清理。

一旦确定了方向，智能体会提出文件系统更改建议。
如果重构涉及将逻辑迁移到扩展方法中，Claude Code 会请求权限来创建新文件，例如 `ServiceCollectionExtensions.cs`。

```csharp
using ExpenseTracker.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    internal const string FrontendCorsPolicy = "frontend";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ExpenseDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

    public static IServiceCollection AddFrontendCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy(FrontendCorsPolicy, policy => policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod());
        });
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/your-first-prompt-69958770/?t=205)

修改代码后，智能体会尝试验证更改。
它会请求运行 shell 命令的权限，例如 `dotnet build`，以确保项目仍然可以编译。
用户可以为单次执行授予权限，或者在会话的其余时间内将特定命令加入白名单。

```csharp
using ExpenseTracker.Api.Endpoints;
using ExpenseTracker.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddFrontendCors()
    .AddProblemDetails();

var app = builder.Build();

app.UseExpenseTrackerPipeline();
app.MapExpenseEndpoints();

app.Run();
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/your-first-prompt-69958770/?t=250)

构建成功后，Claude Code 会提供一份所做更改的摘要，包括创建或修改的文件，以及对整体项目结构的影响。

## 6. Navigating the Interface

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/) · 7:02

### 摘要

Claude Code 提供了一个基于终端的界面，通过斜杠命令、上下文引用和直接 shell 执行来支持高级导航。
用户可以自定义状态栏仪表盘以监控会话指标，清除对话历史以重置上下文，并使用 `@` 符号显式引用文件或目录。
此外，`!` 前缀能够直接执行终端命令——例如构建脚本或测试——确保命令输出会自动传回到对话历史中，供模型分析。

### 关键概念

- **斜杠命令（`/`）**：用于会话管理、配置和诊断的预定义功能。
- **状态栏自定义**：一个显示上下文使用情况、Git 状态和模型信息的个人仪表盘。
- **上下文引用（`@`）**：显式地将文件或文件夹添加到模型的上下文中，以使对话有据可依。
- **直接 shell 执行（`!`）**：绕过模型推理，直接在终端中运行 bash 命令。
- **命令历史**：在会话中使用方向键浏览之前的输入。

### 课程笔记

Claude Code 通过一个终端界面进行导航，该界面除了标准的自然语言提示外，还支持若干种专门的输入方法。

#### 斜杠命令

斜杠命令用于执行特定功能或配置环境。
虽然系统支持自定义命令，但默认情况下也提供了若干用于会话管理和诊断的命令。

```plaintext
/simplify               Review changed code for reuse, quality, and efficiency, then fix any issues
                        found.
/create-agent-skills    (compound-engineering) Expert guidance for creating Claude Code skills and
                        slash commands. Use when working with SKILL.md files, authoring new skills,...
/create-agent-skill     (compound-engineering) Create or edit Claude Code skills with expert
                        guidance on structure and best practices
/init                   Initialize a new CLAUDE.md file with codebase documentation
/statusline             Set up Claude Code's status line UI
/add-dir                Add a new working directory
/advisor                Configure the Advisor Tool to consult a stronger model for g...
                        moments during a task
/agents                 Manage agent configurations
/autofix-pr             Monitor and autofix any issues with the current PR
/background             Continue this session in the background and free the t
/branch                 Create a branch of the current conversation at this p
/btw                    Ask a quick side question without interrupting the ma
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/?t=40)

常用的斜杠命令包括：
- `/clear`：将对话重置到最初状态，从而在不重启应用程序的情况下开始一次全新的会话。
- `/help`：提供说明和可用命令列表。
- `/usage`：显示当前订阅用量和百分比。
- `/doctor`：诊断并验证安装和设置情况。

#### 状态栏自定义

状态栏是位于输入行下方的一个仪表盘。
它提供关于会话状态的实时反馈。
用户可以自定义此状态栏，以包含特定的指示器，例如当前路径、Git 分支、活动模型以及上下文使用百分比。

```shell
> /statusline current path, branch, git dirty indicator, model and context percentage

● statusline-setup(Update statusline with dirty indicator)
  Done (3 tool uses · 6.5k tokens · 16s)
  (ctrl+o to expand)

● Done - your statusline now shows (main*) when the working tree or index has uncommitted changes, and (main) when clean. Path, model, and context % are unchanged.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/?t=130)

添加"脏工作区指示器（dirty indicator）"会在仓库中存在未提交更改时提供一个视觉提示（例如星号），这在运行有风险的命令之前是一项关键检查。

#### 引用上下文

为了确保模型在对话中考虑特定的文件或目录，请使用 `@` 符号。
这会将所选项目显式添加到上下文中，在重构代码或分析特定文档时特别有用。

```shell
@web/src/App.tsx @api/
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/?t=280)

#### 直接 Shell 执行

`!` 前缀允许直接执行 bash 或终端命令。
这会绕过模型最初的推理过程，立即执行命令。
这些命令的输出会自动传回到对话历史中，使 Claude 能够针对结果进行推理（例如修复测试失败）。

```shell
! dotnet test
  L Running...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/?t=340)

在代码更改后直接执行测试是一种常见的工作流程，用于验证稳定性：

```shell
! dotnet test
  L Determining projects to restore ...
    All projects are up-to-date for restore.
    ExpenseTracker.Api -> /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-02/start/api/ExpenseTracker.Api/bin/Debug/net10.0/ExpenseTracker.Api.dll
    ExpenseTracker.Tests -> /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-02/start/tests/ExpenseTracker.Tests/bin/Debug/net10.0/ExpenseTracker.Tests.dll
    Test run for /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-02/start/tests/ExpenseTracker.Tests/bin/Debug/net10.0/ExpenseTracker.Tests.dll (.NETCoreApp,Version=v10
    A total of 1 test files matched the specified pattern.

    Passed!  - Failed:     0, Passed:     3, Skipped:     0, Total:     3, Duration: 38
    ExpenseTracker.Tests.dll (net10.0)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/navigating-the-interface-69958771/?t=350)

这种方法对于 Git 操作也很有用，例如在保持位于 Claude Code 界面内的同时手动执行 `git commit`。

#### 导航与历史

该界面支持标准的终端导航方式，包括使用上下方向键循环浏览之前输入的命令历史。

## 7. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958772/) · 2:19

### 摘要

本课时对 Claude Code 的初始章节进行了总结，重点关注在终端中成功完成安装和环境设置。
它强调了智能体循环这一概念的重要性，并鼓励通过诸如生成架构可视化之类的编码挑战，立即进行实践应用。

### 关键概念

- 成功完成安装并配置终端环境。
- 理解智能体循环作为 Claude Code 核心运作模型的意义。
- 在 Claude Code shell 中导航以提升开发者生产力。
- 通过诸如数据库结构可视化之类的初始任务进行实践应用。
- 过渡到后续课程中的上下文管理内容。

### 课程笔记

使用 Claude Code 的初始阶段包括完成安装并选择一个基于终端的环境。
理解不同安装选项和环境之间的区别，是建立高效工作流程的基础。

本节引入的一个核心概念是智能体循环。
这一架构框架定义了 Claude Code 作为一个自主智能体是如何运作的，即迭代地处理指令并与代码库交互。
这种思维模型对于理解该工具与标准 LLM 聊天界面的不同之处至关重要。

Claude Code 界面作为终端中的一个专用 shell 运作。
这个入口点使开发者能够高效地执行复杂任务。
为了从理论过渡到实践，鼓励用户立即上手使用该工具。
对于没有正在进行中的项目的用户，本课程的源代码提供了一个合适的实验环境。

一个实用的入门挑战是使用 Claude Code 分析一个代码库，并生成一种可视化表示，例如描绘数据库结构的 ASCII 艺术。
这项练习展示了智能体解析文件系统并将信息综合为特定格式的能力。

本课程的下一阶段聚焦于上下文管理，这是优化与 LLM 交互、并确保智能体拥有执行复杂软件工程任务所需信息的关键技能。

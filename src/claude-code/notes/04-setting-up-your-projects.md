# Setting Up Your Projects

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 4
> 8 lessons · ~44 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958779/) | 2:38 | [↓](#1-introduction) |
| 2 | [CLAUDE.md, Your Project's Brain](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-md-your-project-s-brain-69958780/) | 12:31 | [↓](#2-claudemd-your-projects-brain) |
| 3 | [Writing Effective CLAUDE.md Files](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/) | 7:49 | [↓](#3-writing-effective-claudemd-files) |
| 4 | [Modular CLAUDE.md with Imports](https://dometrain.com/take/course/getting-started-claude-code-3256128/modular-claude-md-with-imports-69958782/) | 3:55 | [↓](#4-modular-claudemd-with-imports) |
| 5 | [The .claude Directory](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/) | 5:22 | [↓](#5-the-claude-directory) |
| 6 | [Memory: How Claude Remembers](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/) | 4:38 | [↓](#6-memory-how-claude-remembers) |
| 7 | [Quiz](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958785/) | 5:00 | [↓](#7-quiz) |
| 8 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958786/) | 2:16 | [↓](#8-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958779/) · 2:38

### 摘要

上下文工程（Context Engineering）是指主动管理模型上下文窗口中的信息，以优化输出质量并减少迭代次数的实践。
在 Claude Code 中，这涉及通过提供关于项目结构、技术栈和执行命令的明确说明，使一个项目变得"对 Claude 友好"。
通过使用 `CLAUDE.md` 和 `.claude` 配置文件夹等工具，开发者可以防止 LLM 在诸如定位测试或识别构建工具等发现性任务上浪费 token。

### 关键概念

* **上下文工程（Context Engineering）**：工程师有责任整理提供给 LLM 的信息，以确保获得高质量、高效率的输出。
* **对 Claude 友好的项目（Claude-friendly Projects）**：配置了明确元数据的代码仓库，帮助 Claude Code 无需手动探索即可理解环境。
* **发现开销（Discovery Overhead）**：当 LLM 必须搜索项目以识别技术或命令结构时，产生的不必要的 token 和时间消耗。
* **上下文管理工具**：例如 `CLAUDE.md` 和 `.claude` 目录，用于存储持久的项目规则和配置。

### 课程笔记

管理上下文窗口对于控制成本和保持输出质量至关重要。
随着上下文窗口接近其上限，LLM 响应的质量可能会下降。
除了资源管理之外，上下文中的具体内容也会严重影响达成解决方案所需的迭代次数。
工程师必须实践"上下文工程"，以确保系统只处理相关信息。

如果没有主动的上下文管理，Claude Code 就必须执行发现步骤。
例如，如果在没有事先上下文的项目中被要求运行性能测试，LLM 必须首先确定测试位于何处，判断使用的是哪种测试框架，并了解执行这些测试所需的具体命令。
这种多步骤的发现过程会消耗 token 并增加完成任务所需的时间。

为了避免这种开销，项目应该通过明确定义这些细节，变得"对 Claude 友好"。
这通过若干机制实现，包括 `CLAUDE.md` 文件和 `.claude` 配置文件夹。
这些文件使开发者能够设置全局适用或按上下文适用的规则，确保 Claude Code 能够立即找到相关资源并执行命令。

`CLAUDE.md` 文件作为项目技术栈、常用命令和特定编码标准的主要参考：

```markdown
# CLAUDE.md

Expense Tracker web app.

## Stack

- .NET 10 Minimal API (`api/ExpenseTracker.Api`)
- React 19 / Vite SPA (`web/`) 
- PostgreSQL 18 via Docker.

## Common commands

```sh
docker compose up -d                                            # Postgres on :5432
dotnet ef database update --project api/ExpenseTracker.Api      # apply migrations
dotnet run --project api/ExpenseTracker.Api                     # API on :5057
npm --prefix web install && npm --prefix web run dev            # SPA on :5173

dotnet build                                                    # ExpenseTracker.slnx
dotnet test                                                     # Docker must be running
dotnet test --filter "FullyQualifiedName~ExpenseApiTests.Should_return_201"
npm --prefix web run typecheck
npm --prefix web run build

dotnet ef migrations add <Name> --project api/ExpenseTracker.Api
```

Warnings fail the build (`TreatWarningsAsErrors=true`). Fix them, don't suppress.

## Rules

### API

- Add new routes as a route group + static handlers + a `MapXxxEndpoints` extension called from `Program.cs`. Don't put handlers inline in `Program.cs`.
- Validate inputs by hand in the handler and return `Results.ValidationProblem`. Don't add FluentValidation or DataAnnotations.
- Put wire types in `Contracts/` as records. Don't return `Domain/Expense` from endpoints.
- Let Postgres set `CreatedAt` via the `now() at time zone 'utc'` default. Don't assign it in C#.

### Tests

- `PostgresApiFactory` is registered as an `AssemblyFixture` in `AssemblyFixtures.cs`. Inject it via constructor on test classes; don't add `IClassFixture<PostgresApiFactory>` or register it again.
- Tests use Testcontainers Postgres, not the `docker compose` instance. Don't reuse the dev connection string in tests.

### Web

- Use bare `/api/...` paths in the SPA. Vite proxies them to `:5057`; don't hardcode the API origin.
- Serialize/parse `DateOnly` as `YYYY-MM-DD` strings on both sides. Don't use `Date` objects on the wire.

### Layout

- Project folders are `Product.Component` PascalCase (`ExpenseTracker.Api`, `ExpenseTracker.Tests`) inside generic buckets (`api/`, `tests/`).
- Solution file is `.slnx` (XML). Don't generate or commit a `.sln`.

# Domain

@docs/domain-glossary.md
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958779/?t=117)

此外，`.claude/settings.json` 文件可用于管理权限并限制对敏感文件（例如环境变量或本地设置）的访问，确保它们不会被拉入 LLM 的上下文中：

```json
{
    "permissions": {
        "deny": [
            "Read(**/appsettings*.json)",
            "Read(**/.env)",
            "Read(**/.env.*)",
            "Read(**/*.env)"
        ]
    }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958779/?t=117)

## 2. CLAUDE.md, Your Project's Brain

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-md-your-project-s-brain-69958780/) · 12:31

### 摘要

`CLAUDE.md` 是 Claude Code 的主要配置文件，充当一份持久的"入职指南"，为智能体提供关键的项目上下文，包括技术栈、构建命令和架构约定。
通过将此文件自动注入到每一次对话中，Claude Code 避免了对代码库做出错误的假设，特别是在遗留或复杂的环境中。
开发者可以使用 `/init` 命令来搭建此文件的初始版本，或者在项目级、用户级和本地级文件之间管理一套分层的指令体系，以平衡团队共享标准与个人工作流程。

### 关键概念

- **持久内存**：为 Claude 提供对项目的一致理解，类似于一份入职指南。
- **自动上下文**：该文件会在用户提示之前自动被添加到每一次对话上下文中。
- **初始化**：`/init` 命令通过扫描 `.csproj` 或 `package.json` 等项目文件，自动完成 `CLAUDE.md` 的创建。
- **精简（Pruning）**：对于保持性能至关重要；移除 Claude 能够自行发现的冗余信息，可防止上下文性能下降。
- **指令层级**：支持项目级（`CLAUDE.md`）、全局用户级（`~/.claude/CLAUDE.md`）和项目本地级（`CLAUDE.local.md`）配置。

### 课程笔记

在使用 Claude Code 时，`CLAUDE.md` 是代码仓库中最关键的文件之一。
它充当一份入职指南，为智能体提供关于项目规则和结构的持久内存。
如果没有这个文件，智能体会将每一次新会话都当作它第一次看到该项目，从而导致错误的假设和潜在的失误。
其他智能体可能会使用一个名为 `agents.md` 的类似文件来达到同样的目的。

当一次对话开始时，Claude Code 会自动将 `CLAUDE.md` 的内容与系统指令和工具一起纳入其中。
这确保了每一个提示都会在项目特定要求的上下文中被解读。

#### 初始化 CLAUDE.md

对于现有项目，可以使用 `/init` 命令来搭建 `CLAUDE.md` 的初始版本。
Claude 会扫描代码仓库中的重要文件——例如项目文件、包清单和应用程序工厂——以理解技术栈和常见操作。

```markdown
# CLAUDE.md
Expense Tracker web app.

## Stack
- .NET 10 Minimal API (`api/ExpenseTracker.Api`)
- React 19 / Vite SPA (`web/`) 
- PostgreSQL 18 via Docker.

## Common commands

```sh
docker compose up -d                                            # Postgres on :5432
dotnet ef database update --project api/ExpenseTracker.Api      # apply migrations
dotnet run --project api/ExpenseTracker.Api                     # API on :5057
npm --prefix web install && npm --prefix web run dev            # SPA on :5173

dotnet build                                                    # ExpenseTracker.slnx
dotnet test                                                     # Docker must be running
dotnet test --filter "FullyQualifiedName~ExpenseApiTests.Should_return_201"
npm --prefix web run typecheck
npm --prefix web run build

dotnet ef migrations add <Name> --project api/ExpenseTracker.Api
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-md-your-project-s-brain-69958780/?t=370)

#### 内容与约定

`CLAUDE.md` 并没有预定义的结构；应将其视为一个提供相关未来上下文的提示。
典型的章节包括技术栈、常用命令（例如运行测试或应用迁移）以及架构说明。
记录约定——例如方法或函数的命名结构——可以避免反复纠正生成的代码。

```markdown
## Rules

### Web ↔ API
- Use bare `/api/...` paths in the SPA. Vite proxies them to `:5057`; don't hardcode the API origin.
- Serialize/parse `DateOnly` as `YYYY-MM-DD` strings on both sides. Don't use `Date` objects on the wire.

### Layout
- Project folders are `Product.Component` PascalCase (`ExpenseTracker.Api`, `ExpenseTracker.Tests`) inside generic buckets (`api/`, `tests/`).
- Solution file is `.slnx` (XML). Don't generate or commit a `.sln`.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-md-your-project-s-brain-69958780/?t=325)

#### 精简与性能

虽然 `/init` 提供了一个良好的起点，但精简该文件是一种最佳实践。
过于庞大的 `CLAUDE.md` 文件会导致性能下降。
智能体能够通过文件探索轻松发现的信息应当被移除，以保持上下文简洁而有效。

另一种做法是从一个空的 `CLAUDE.md` 开始，随着摩擦点的出现逐步添加规则。
例如，如果智能体难以理解如何运行测试，可以添加一个具体的测试章节。

```markdown
# CLAUDE.md

## Testing

To run tests execute:
```sh
dotnet test
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-md-your-project-s-brain-69958780/?t=445)

#### 指令层级

Claude Code 支持不同级别的指令，以将团队范围的标准与个人偏好区分开来：

- **项目指令**：位于 `./CLAUDE.md` 或 `./.claude/CLAUDE.md`。这些会被提交到源代码控制中，并与团队共享。
- **用户指令**：位于 `~/.claude/CLAUDE.md`。这些是适用于特定用户所有项目的全局偏好设置。
- **本地指令**：位于 `./CLAUDE.local.md`。这些是特定项目的个人偏好设置，不应推送到源代码控制中。
- **托管策略**：由组织分发的组织范围指令（本课程不涉及）。

[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/claude-md-your-project-s-brain-69958780/?t=610)

## 3. Writing Effective CLAUDE.md Files

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/) · 7:49

撰写 `CLAUDE.md` 文件是有效使用 Claude Code 最重要的步骤之一。
作为该文件的维护者，你有责任提供能够带来更好结果的指令。
本课时涵盖了产出高质量 `CLAUDE.md` 文件的建议，使其成为模型的规范性指南。

### 关键概念

- **祈使语气（Imperative Voice）**：使用具体的行动（做/不要做），而不是描述性的散文。
- **规范性规则（Prescriptive Rules）**：从解释架构如何运作，转变为指导如何实现新功能。
- **反应式文档（Reactive Documentation）**：从一个最小化的文件开始，仅在 Claude 犯错或需要指导时才添加规则。
- **人类可读性（Human Readability）**：保持简洁的结构，使用项目符号以便于人工维护。
- **关注点分离（Separation of Concerns）**：使用 linter 处理格式和语法；使用 `CLAUDE.md` 处理架构和项目特定的规则。
- **与现实同步（Reality Synchronization）**：确保文件准确反映项目的当前状态，以避免性能下降。

### 课程笔记

#### 规则编写理念

规则应以祈使语气编写。
避免使用描述性的散文，而应使用具体的行动。
诸如"使用"、"要做"或"不要做"之类具体的指令，比"通常"或"可能"之类模糊的措辞更有效。

一种强有力的技巧是使用"做/不要做"配对。
虽然一条规则定义了目标，但对应的"不要做"规则可以防止常见的错误或不良行为。
这有助于阻止 Claude 走上一条你知道对你特定项目而言是错误的路径。

#### 内容筛选

在决定包含哪些内容时，需评估 Claude 是否能够独立找到该信息，或者它目前是否正在犯错。
如果一条规则没有带来价值或没有纠正模型的方向，就应该将其省略。
从一个空的 `CLAUDE.md` 开始，随着问题的出现被动地添加规则，是一种有效的策略。

如果一行规则不够充分，请提供示例。
代码片段或文本示例可以澄清复杂的模式，并确保 Claude 正确应用规则。
此外，不要用 `CLAUDE.md` 来定义 lint 或格式化规则。
这些操作应该是快速、廉价且一致的，而这最好通过专用的 linter 来实现。
任何属于 linter 职责范围的内容，都应由 linter 本身处理，而不是交给 LLM。

#### 维护与可读性

将 `CLAUDE.md` 与项目的实际状态保持同步至关重要。
过时的文档可能比根本没有规则更糟糕，因为模型会试图强行套用不准确的模式，导致糟糕的表现和更高的 token 用量。
应将该文件视为对系统的精确、准确的文档，而不是一个通用知识库。

避免长篇的理由说明或"为什么"某条规则存在的解释；这些内容属于 wiki 或代码注释。
`CLAUDE.md` 应严格聚焦于指令。
最后，确保文件是人类可读的。
虽然 Claude 会频繁使用它，但人类必须维护它。
使用项目符号、简短的句子和清晰的结构，以确保它在项目扩展时依然易于管理。

#### 实践性转变

要评估一个 `CLAUDE.md` 文件的有效性，你可以提示 Claude 分析各版本之间的差异，并解释其中的改进之处。

```bash
/clear
Please explain what's different on our CLAUDE.md. Show befores and afters for each thing.
Bash(git diff CLAUDE.md)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/?t=320)

该文件的转变通常涉及从描述性的架构说明，转向一份规范性的规则文档。
例如，项目技术栈的描述应从一段散文式的段落，转换为一份简洁的项目符号列表，并且应移除课程标语。

之前：
```markdown
.NET 10 Minimal API (api/ExpenseTracker.Api) + React 19 / Vite SPA (web/) + PostgreSQL 18 via Docker. Course example for Dometrain's "Getting Started with Claude Code".
```
之后：
```markdown
## Stack

- .NET 10 Minimal API (`api/ExpenseTracker.Api`)
- React 19 / Vite SPA (`web/`)
- PostgreSQL 18 via Docker.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/?t=355)

架构说明应被重构为使用祈使句项目符号的"Rules"章节，为 API 定义特定的模式。
这包括关于路由分组、验证和数据库默认值的指令。
常用命令也应通过删除尾随注释来加以简化。

之前：
```markdown
**API layout.** `Program.cs` is intentionally thin - wiring lives in `Extensions/ServiceCollectionExtensions.cs` ... new routes should follow that pattern (route group + static handler + `MapXxxEndpoints` extension called from `Program.cs`). Validation is done by hand ... no FluentValidation / Data layer.
```
之后：
```markdown
### API

- Add new routes as a route group + static handlers + a `MapXxxEndpoints` extension called from `Program.cs`. Don't put handlers inline in `Program.cs`.
- Validate inputs by hand in the handler and return `Results.ValidationProblem`. Don't add FluentValidation or DataAnnotations.
- Put wire types in `Contracts/` as records. Don't return `Domain/Expense` from endpoints.
- Let Postgres set `CreatedAt` via the `now() at time zone 'utc'` default. Don't assign it in C#.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/?t=385)

测试和 web 开发的规则应被精简，以聚焦于关键约束，例如避免硬编码 API 源地址，或在测试中重复使用开发环境的连接字符串。
在这次重构过程中，如果模型能够通过代码库自行发现，诸如 CORS 配置和特定库实现细节（例如 React Query）之类的次要细节会被移除。

之后：
```markdown
### Tests

- `PostgresApiFactory` is registered as an `AssemblyFixture` in `AssemblyFixtures.cs`. Inject it via constructor on test classes; don't add `IClassFixture<PostgresApiFactory>` or register it again.
- Tests use Testcontainers Postgres, not the `docker compose` instance. Don't reuse the dev connection string in tests.

### Web

- Use bare `/api/...` paths in the SPA. Vite proxies them to `:5057`; don't hardcode the API origin.
- Serialize/parse `DateOnly` as `YYYY-MM-DD` strings on both sides. Don't use `Date` objects on the wire.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/?t=415)

最后，应通过明确的"不要做"指令来陈述布局约定，以防止诸如生成遗留解决方案文件之类的常见错误。
该章节应降级为"Rules"下的一个子章节，以维持层级结构。

之前：
```markdown
## Conventions

- Project folders are Product.Component PascalCase (ExpenseTracker.Api, ExpenseTracker.Tests) inside generic buckets (api/, tests/).
- Solution is .slnx (XML), not .sln.
```
之后：
```markdown
### Layout

- Project folders are `Product.Component` PascalCase (`ExpenseTracker.Api`, `ExpenseTracker.Tests`) inside generic buckets (`api/`, `tests/`).
- Solution file is `.slnx` (XML). Don't generate or commit a `.sln`.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/writing-effective-claude-md-files-69958781/?t=460)

## 4. Modular CLAUDE.md with Imports

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/modular-claude-md-with-imports-69958782/) · 3:55

### 摘要

管理 `CLAUDE.md` 文件的增长，对于为 AI 智能体维持清晰有效的上下文至关重要。
通过使用导入语法——在文件路径前加上 `@` 符号——开发者可以将项目指令模块化，把领域词汇表、测试指南和特定架构规则拆分到专用的 Markdown 文件中。
这种方法确保 Claude 始终立足于项目特定的术语和约束，而不会使主配置文件变得杂乱。

### 关键概念

- **模块化**：通过将内容拆分到多个逻辑文件中，防止 `CLAUDE.md` 变得过于庞大。
- **导入语法**：在 `CLAUDE.md` 中使用 `@path/to/file.md` 来包含外部文档。
- **领域基础（Domain Grounding）**：使用外部词汇表来解决关于业务逻辑和术语的常见 AI 误解。
- **上下文解析**：Claude 会自动加载并解析被引用的文件，将其作为项目上下文的一部分。
- **关注点分离**：为测试原则、领域规则和项目布局维护各自独立的文档。

### 课程笔记

在一个不断扩展的项目中，特别是在一个拥有多个应用和特定规则的单体仓库（monorepo）中，`CLAUDE.md` 文件可能会变得难以管理。
为了保持配置简洁，你可以通过导入外部文档来模块化该文件。

```markdown
# CLAUDE.md

Expense Tracker web app.

## Stack

- .NET 10 Minimal API (`api/ExpenseTracker.Api`)
- React 19 / Vite SPA (`web/`) 
- PostgreSQL 18 via Docker.

## Common commands

```sh
docker compose up -d                                            # Postgres on :5432
dotnet ef database update --project api/ExpenseTracker.Api      # apply migrations
dotnet run --project api/ExpenseTracker.Api                     # API on :5057
npm --prefix web install && npm --prefix web run dev            # SPA on :5173
```
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/modular-claude-md-with-imports-69958782/?t=10)

当 Claude 频繁误解特定领域的术语时——例如将用户提供的 `Date` 与系统生成的 `CreatedAt` 时间戳混淆——提供一份领域词汇表会很有帮助。
你可以使用 `@` 符号加上文件路径来引用一个外部的 Markdown 文件，而不是将这些规则直接硬编码到 `CLAUDE.md` 中。

```markdown
# CLAUDE.md
## Rules
### Layout

# Domain
@docs/domain-glossary.md
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/modular-claude-md-with-imports-69958782/?t=130)

当 Claude 加载 `CLAUDE.md` 时，它会自动解析这些引用，有效地将导入文件的内容扩展到上下文中。
例如，一个 `domain-glossary.md` 文件可以定义关键的区分：

```markdown
# Domain Glossary
## Expense

### `Date` vs. `CreatedAt` — don't confuse them

This is the single most important distinction in the domain:

- **`Date`** answers *"when did I spend this?"* — it's user-supplied, a calendar date with no time or zone, and it's what the user sorts and filters by.
- **`CreatedAt`** answers *"when did the system learn about this?"* — it's the audit timestamp, server-assigned, and used only as a tiebreaker when two expenses share the same `Date`.

If you find yourself reaching for `CreatedAt` in a user-facing list or filter, you almost certainly want `Date`.

## Amount

A positive `decimal` with exactly **2 decimal places** (`HasPrecision(18, 2)`). Inputs are rounded half-away-from-zero on create — `12.345` becomes `12.35`, `-0` is rejected as `<= 0`.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/modular-claude-md-with-imports-69958782/?t=175)

导入之后，Claude 就可以基于外部文档回答特定问题。
例如，如果被问及 `CreatedAt` 和 `Date` 之间的区别，Claude 会直接从词汇表中提取定义。
这种技巧使工作流程管理更为出色，因为不同的文档（例如测试指南或架构原则）可以被独立维护，同时仍能为 AI 智能体提供全面的指导。

## 5. The .claude Directory

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/) · 5:22

### 摘要

`.claude` 目录是 Claude Code 的中央配置枢纽，管理着项目特定和全局的行为。
它主要通过 `settings.json` 和 `settings.local.json` 来处理权限管理，让用户能够定义哪些命令被授权，哪些文件被限制访问。
通过利用一个层级结构——从本地覆盖到系统范围的默认设置——开发者可以确保在不同环境和团队成员之间，安全策略和自定义指令保持一致。

### 关键概念

* **.claude 目录**：项目内或系统级 Claude Code 配置文件的主要存放位置。
* **settings.local.json**：存储特定于机器的权限和配置；通常被排除在版本控制之外，用于处理本地偏好设置。
* **settings.json**：通过版本控制与团队共享的项目级配置，用于实施安全和操作标准。
* **全局配置**：位于用户主目录中的系统级 `.claude` 目录，用于跨项目的规则和设置。
* **权限控制**：为 shell 命令和文件系统访问显式定义的 `allow` 和 `deny` 列表。
* **CLAUDE.md 层级**：可以在系统级的 `CLAUDE.md` 中定义全局规则，使其应用于该机器上的每一次 Claude Code 会话。

### 课程笔记

`.claude` 目录是 Claude Code 的一个重要组成部分，作为设置、权限和高级配置的存储位置。
即使在基础层面，这个文件夹也经常被用来管理 Claude 与你的环境交互的方式。

#### 本地与项目设置

当你授予 Claude 权限来执行诸如构建或测试脚本之类的命令时，这些授权会被存储在 `settings.local.json` 中。
一旦你以"始终允许"选项接受了一次权限提示，这个文件就会被自动生成。

```json
{
  "permissions": {
    "allow": [
      "Bash(dotnet build *)",
      "Bash(dotnet test *)"
    ]
  }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/?t=40)

虽然 `settings.local.json` 旨在用于本地机器配置，不应提交到版本控制中，但你可以使用 `settings.json` 来定义项目范围的规则。
例如，如果你想确保团队中没有人意外地授予 Claude 访问敏感环境文件的权限，你可以在项目的 `settings.json` 中定义一条 `deny` 规则。

```json
{
    "permissions": {
        "deny": [
            "Read(**/appsettings*.json)",
            "Read(**/.env)",
            "Read(**/.env.*)",
            "Read(**/*.env)"
        ]
    }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/?t=130)

#### 系统级配置

Claude Code 还支持位于系统根目录（即用户主目录）的一个全局 `.claude` 目录。
放置在此处的配置会应用于该机器上的每一次 Claude 会话，无论使用的是哪个具体的代码仓库。
这包括 `CLAUDE.md` 文件中的全局规则和 `settings.json` 中的全局设置。

举例来说，一个全局的 `CLAUDE.md` 可以在所有项目中强制执行特定行为：

```markdown
# Message Rules

End every message with "Dometrain is great!"
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/?t=210)

同样，全局的 `settings.json` 包含机器级别的配置，包括允许/禁止的命令、状态栏设置和已启用的插件。

```json
{
  "permissions": {
    "allow": [
      "Bash(find:*)"
    ],
    "deny": [
      "Bash(dotnet user-secrets:*)"
    ],
    "defaultMode": "default"
  },
  "statusLine": {
    "type": "command",
    "command": "bash /Users/guilhermeferreira/.claude/statusline.sh"
  },
  "enabledPlugins": {
    "clangd-lsp@claude-plugins-official": true,
    "swift-lsp@claude-plugins-official": true
  },
  "alwaysThinkingEnabled": true
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/?t=220)

#### 验证全局规则

一旦确立了一条全局规则，它会立即生效。
在以下示例中，系统级 `CLAUDE.md` 中定义的全局规则被应用到特定项目仓库中的一次会话：

```plaintext
Claude Code v2.1.141
Opus 4.7 (1M context) • Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-04/end

> /clear
  └ (no content)

> hello

● Hello! How can I help you today?

  Dometrain is great!
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-claude-directory-69958783/?t=265)

需要注意的是，权限应始终在 `settings.json` 或 `settings.local.json` 中管理。
`CLAUDE.md` 文件旨在用于说明和指导方针，而不是用于安全保证或权限管理。

## 6. Memory: How Claude Remembers

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/) · 4:38

Claude Code 利用一套内存系统，跨会话持久保存用户偏好和习得的行为，与 `CLAUDE.md` 中定义的项目范围规则相辅相成。
该系统会从用户的纠正中捕获"自动内存"——例如偏好的输出格式——并允许手动创建针对特定仓库或全局的内存，用于诸如测试驱动开发（TDD）之类的工作流程。
这些内存以 Markdown 和 YAML 文件的形式存储在 `.claude` 目录中，确保 Claude 能够适应个别开发者的风格，并且即使在对话历史被清除后，也能保持一致的上下文。

### 关键概念

- **项目内存**：在 `CLAUDE.md` 中为项目所有用户定义的规则和约定。
- **自动内存（Auto Memory）**：从用户反馈和纠正中自动捕获的习得偏好。
- **用户内存（User Memory）**：可以是全局的，也可以是特定于单个代码仓库的个人偏好设置。
- **内存持久化**：内存被存储在 `.claude` 目录中，即使会话被清空也能保留。
- **内存管理**：`/memory` 命令提供了查看和编辑已存储内存文件的入口。

### 课程笔记

虽然 `CLAUDE.md` 用于持久保存项目范围的内存和约定，但 Claude Code 还支持一套更个人化的内存系统。
这套系统专为个人偏好和典型的纠正而设计——在这些情况下，你可能希望 Claude 调整其行为或风格，而不必更新项目的共享文档。

例如，如果你要求 Claude 解释数据库模式，它最初可能会以标准的表格或散文式描述来回应。

```shell
Claude Code v2.1.141
Opus 4.7 (1M context) · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-04/end

/clear
└ (no content)

Can you explain the database schema?
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/?t=55)

如果默认的输出样式不符合你的偏好，你可以提出纠正。
Claude 会将其识别为一个需要在未来交互中记住的偏好。

```shell
Next time, come back to me with an entity relationship diagram using ASCII art, and all the
rest of the text might be through bullet points

Read 1 file, recalled 1 memory, wrote 2 memories (ctrl+o to expand)

Got it - saved. Next schema explanation will be in ASCII ERD plus bullets.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/?t=85)

一旦保存了一条内存，它会跨会话持久保留。
即使你使用 `/clear` 命令清空当前的上下文窗口，当相关话题再次被提起时，Claude 也会调用已保存的内存。
你可以使用 `/memory`（或 `/mem`）命令查看 Claude 内存的当前状态。

```shell
- CreatedAt - timestamp defaulted by Postgres to (now() at time zone 'utc'); audit only, never
  assigned in C#.
- Composite index on (Date DESC, CreatedAt DESC) - matches the list sort order; today first,
  most-recently-entered first within a day.
- No soft-delete, no UserId, no Currency, no category/tag/merchant columns - all out of scope
  per the domain glossary.

* Baked for 20s

> /memory

/memory             Edit Claude memory files
/update-config      Use this skill to configure the Claude Code harness via
                    settings.json. Automated behaviors ("from now on when X" "ea...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/?t=115)

内存以 Markdown 和 YAML 文件的形式存储在 `.claude` 文件夹中。
这包括一个索引文件，用于追踪不同的内存节点，例如项目文件夹的命名约定或特定的输出格式。

```markdown
# Memory Index

- [.NET project folder naming] (feedback_dotnet_project_naming.md) - PascalCase `Product.Component` folders (e.g. `ExpenseTracker.Api`) inside generic buckets like `/api` and `/tests`.
- [Schema explanation format] (feedback_schema_explanation_format.md) - Lead with ASCII-art ERD, then bullet points for everything else. No prose, no markdown tables.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/?t=160)

除了自动内存生成之外，你还可以显式强制 Claude 为特定的工作流程创建内存。
例如，你可以指示 Claude 对某个特定的 API 遵循测试驱动开发（TDD）。
这会创建一个基于 YAML 的内存文件，定义该行为及其适用范围。

```yaml
name: feedback-api-tdd
description: "For the Expenses API, follow TDD - write a failing test first, run it to see it fail, then implement until it passes. Applies to the API only, not the web SPA."
metadata:
  node_type: memory
  type: feedback
  originSessionId: afc35bbe-610a-4059-81e2-64297880dc86
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/?t=250)

Claude Code 环境还通过 `settings.json` 进一步配置，用于管理权限和自动化行为。

```json
{
    "permissions": {
        "deny": [
            "Read(**/appsettings*.json)",
            "Read(**/.env)",
            "Read(**/.env.*)",
            "Read(**/*.env)"
        ]
    }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/memory-how-claude-remembers-69958784/?t=190)

## 7. Quiz

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958785/) · 5:00

没有该课时的文档 - 请直接观看。

## 8. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958786/) · 2:16

### 摘要

本节回顾强调了 `CLAUDE.md` 文件在为 Claude Code 建立项目特定的上下文和规则方面所扮演的核心角色。
它涵盖了通过 `/init` 命令或手动设置来初始化这份配置、通过文件导入集成外部文档，以及使用内存和设置来个性化开发体验。

### 关键概念

- **CLAUDE.md 配置**：为智能体定义技术栈、构建命令和架构约束的主要文件。
- **初始化策略**：使用 `/init` 命令进行自动化发现，与手动创建以获得精细控制之间的对比。
- **上下文集成**：利用 `@` 语法引用现有文件，例如领域词汇表。
- **用户个性化**：通过内存和 `.claude/settings.json` 自定义智能体的行为。
- **迭代优化**：定期更新和精简配置文件，以确保智能体保持最佳性能的重要性。

### 课程笔记

`CLAUDE.md` 文件是开发者与 Claude Code 之间交互的基础。
它为智能体提供了理解代码仓库结构和需求所需的上下文，而无需为每一次查询都扫描整个代码库。
一份维护良好的 `CLAUDE.md` 通常包括项目的技术栈、常见的开发命令，以及针对 API 开发、测试和 web 布局的具体规则。

```markdown
# CLAUDE.md

Expense Tracker web app.

## Stack

- .NET 10 Minimal API (`api/ExpenseTracker.Api`)
- React 19 / Vite SPA (`web/`) 
- PostgreSQL 18 via Docker.

## Common commands

```sh
docker compose up -d                                            # Postgres on :5432
dotnet ef database update --project api/ExpenseTracker.Api      # apply migrations
dotnet run --project api/ExpenseTracker.Api                     # API on :5057
npm --prefix web install && npm --prefix web run dev            # SPA on :5173

dotnet build                                                    # ExpenseTracker.slnx
dotnet test                                                     # Docker must be running
dotnet test --filter "FullyQualifiedName~ExpenseApiTests.Should_return_201"
npm --prefix web run typecheck
npm --prefix web run build

dotnet ef migrations add <Name> --project api/ExpenseTracker.Api
```

Warnings fail the build (`TreatWarningsAsErrors=true`). Fix them, don't suppress.

## Rules

### API

- Add new routes as a route group + static handlers + a `MapXxxEndpoints` extension called from `Program.cs`. Don't put handlers inline in `Program.cs`.
- Validate inputs by hand in the handler and return `Results.ValidationProblem`. Don't add FluentValidation or DataAnnotations.
- Put wire types in `Contracts/` as records. Don't return `Domain/Expense` from endpoints.
- Let Postgres set `CreatedAt` via the `now() at time zone 'utc'` default. Don't assign it in C#.

### Tests

- `PostgresApiFactory` is registered as an `AssemblyFixture` in `AssemblyFixtures.cs`. Inject it via constructor on test classes; don't add `IClassFixture<PostgresApiFactory>` or register it again.
- Tests use Testcontainers Postgres, not the `docker compose` instance. Don't reuse the dev connection string in tests.

### Web

- Use bare `/api/...` paths in the SPA. Vite proxies them to `:5057`; don't hardcode the API origin.
- Serialize/parse `DateOnly` as `YYYY-MM-DD` strings on both sides. Don't use `Date` objects on the wire.

### Layout

- Project folders are `Product.Component` PascalCase (`ExpenseTracker.Api`, `ExpenseTracker.Tests`) inside generic buckets (`api/`, `tests/`).
- Solution file is `.slnx` (XML). Don't generate or commit a `.sln`.

# Domain

@docs/domain-glossary.md
```

设置这个文件有两种方式。
可以运行 `/init` 命令，根据代码仓库的内容创建一份初稿。
另外，开发者也可以从一个空文件开始，随着开发过程中的需要逐步添加指令，以纠正智能体的方向。

为避免信息重复，Claude Code 允许导入外部文件。
这对于像领域词汇表这样的大型文档特别有用。
通过在 `CLAUDE.md` 中使用 `@` 符号引用这些文件，智能体能够访问项目其他地方定义的共享词汇和业务逻辑约束。

```markdown
# Domain Glossary

Shared vocabulary for the Expense Tracker. Use these terms exactly — in code, commits, PR titles, and conversations with the team — so we don't drift into synonyms ("entry", "purchase", "record") that blur meaning.

## Expense

A single outflow of money the user wants to track. The only aggregate in the system today.

An Expense is **immutable once created**: there is no edit or delete endpoint. If the user records something wrong, the current expectation is that they live with it or we add a corrective entry later. Treat this as a deliberate constraint, not a missing feature — re-confirm with the product owner before adding mutation endpoints.

### Fields

| Field         | Type             | Meaning                                                                                                  |
| ------------- | ---------------- | -------------------------------------------------------------------------------------------------------- |
| `Id`          | `Guid`           | Stable identifier. Generated client-side on the domain object, never reused.                             |
| `Amount`      | `decimal(18, 2)` | How much was spent, in the user's implicit currency (see *Currency*). Always **positive**.               |
| `Description` | `string`         | Free-text label, 1–200 chars, trimmed. Required.                                                         |
| `Date`        | `DateOnly`       | The **spent-on** date — when the money left the user's pocket. Not when the row was entered.             |
| `CreatedAt`   | `DateTimeOffset` | When the row was inserted, in UTC. Set by Postgres (`now() at time zone 'utc'`), never assigned in C#.   |

### `Date` vs. `CreatedAt` — don't confuse them

This is the single most important distinction in the domain:

- **`Date`** answers *"when did I spend this?"* — it's user-supplied, a calendar date with no time or zone, and it's what the user sorts and filters by.
- **`CreatedAt`** answers *"when did the system learn about this?"* — it's the audit timestamp, server-assigned, and used only as a tiebreaker when two expenses share the same `Date`.

If you find yourself reaching for `CreatedAt` in a user-facing list or filter, you almost certainly want `Date`.
```

对于个人偏好和特定于环境的约束，开发者可以使用内存和 `.claude/settings.json` 文件。
这使得体验得以个性化，并确保智能体不会访问敏感文件或环境变量。

```json
{
    "permissions": {
        "deny": [
            "Read(**/appsettings*.json)",
            "Read(**/.env)",
            "Read(**/.env.*)",
            "Read(**/*.env)"
        ]
    }
}
```

投入时间完善 `CLAUDE.md` 文件至关重要，因为它为每一次对话奠定了基础。
建议定期回顾并精简此文件，以确保这些规则随着项目的发展依然有效。

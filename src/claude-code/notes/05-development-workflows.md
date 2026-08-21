# Development Workflows

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 5
> 9 lessons · ~70 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958787/) | 1:41 | [↓](#1-introduction) |
| 2 | [The Workflow](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/) | 15:48 | [↓](#2-the-workflow) |
| 3 | [Working with Multiple Files](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/) | 6:05 | [↓](#3-working-with-multiple-files) |
| 4 | [Running Commands and Reading Output](https://dometrain.com/take/course/getting-started-claude-code-3256128/running-commands-and-reading-output-69958790/) | 3:42 | [↓](#4-running-commands-and-reading-output) |
| 5 | [Permissions and Modes](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/) | 8:46 | [↓](#5-permissions-and-modes) |
| 6 | [Refining and Continuing Conversations](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/) | 6:37 | [↓](#6-refining-and-continuing-conversations) |
| 7 | [Resuming Your Work](https://dometrain.com/take/course/getting-started-claude-code-3256128/resuming-your-work-69958793/) | 3:25 | [↓](#7-resuming-your-work) |
| 8 | [Practical Exercise: A Real Development Session](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/) | 22:07 | [↓](#8-practical-exercise-a-real-development-session) |
| 9 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958795/) | 1:39 | [↓](#9-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958787/) · 1:41

### 摘要

本课时介绍了"开发工作流程"这一章节，将重点从最初的设置和安装，转移到 Claude Code 的日常实际应用。
它强调了采用一套结构化开发生命周期——从最初的功能规划到最终提交——的重要性，而不是依赖临时随意的提示，从而确保开发者能够最大限度地发挥该工具的能力，并保持高质量的代码。

### 关键概念

- 从环境配置过渡到日常开发常规。
- 结构化工作流程相较于冲动、无序提示的重要性。
- 管理从功能构思到最终提交的完整开发生命周期。
- 利用 Claude Code 的特定功能来提升生产力和代码质量。

### 课程笔记

在建立了 Claude Code 的基础知识——包括安装、上下文管理和项目配置——之后，重点转向了在专业开发环境中实际应用这些工具。
虽然 Claude Code 的自然语言界面允许快速生成代码，但要获得一致的高质量结果，需要对开发工作流程采取严谨的方法。

#### 结构的重要性

Claude Code 被设计为高度易于使用，允许开发者通过自然语言提示快速生成代码。
然而，在没有明确计划的情况下直接进入实现阶段，可能导致次优的结果。
抵制立即开始构建的冲动，转而遵循一套结构化的工作流程，能够确保 AI 的输出与项目的架构和长期目标保持一致。

#### 开发生命周期

本章概述了一套全面的工作流程，旨在将一项功能从最初的需求，带到最终的版本控制提交。
这个过程涉及：
- **规划与分析**：在编写代码之前，利用 Claude 来理解需求。
- **实现**：使用 Claude Code 的特定功能，迭代地构建功能。
- **优化与提交**：确保最终输出经过打磨，并被正确地集成到代码库中。

在本章结束时，开发者将理解如何将 Claude Code 集成到日常工作流程的每一个阶段，利用其完整的功能集，为构建的每一项功能都保持高标准。

## 2. The Workflow

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/) · 15:48

### 摘要

本课时定义了一套结构化的开发工作流程，用于有效使用 Claude Code，超越简单的试错方式。
它详细介绍了一个五阶段流程——探索（Explore）、计划（Plan）、执行（Execute）、验证（Verify）和提交（Commit）——利用 Claude 的智能体能力，确保实现方案立足于代码库上下文，并通过自动化测试和子智能体审查进行验证。

### 关键概念

*   **智能体循环（The Agentic Loop）**：一个周期，其中智能体接收一个目标，收集上下文，采取行动（读取/编辑文件、运行工具），并验证结果。
*   **计划模式（Plan Mode）**：Claude Code 中用于探索和制定策略、而不立即更改代码的一种专门状态。
*   **结构化工作流程（Structured Workflow）**：一种五阶段的方法（探索、计划、执行、验证、提交），利用了智能体的天然特性。
*   **验证策略**：使用 TDD 循环、手动冒烟测试和专门的代码审查子智能体，来保证实现的正确性。
*   **具备上下文感知的提交**：利用智能体的会话历史和 Git 工具，生成精确的提交信息。

### 课程笔记

#### 智能体循环

Claude Code 在一个智能体循环内运作。
这个过程从通过提示提供的一个目标开始。
智能体随后通过读取文件来收集上下文，采取诸如编辑代码或运行终端工具之类的行动，并验证结果以检查任务是否完成。
如果任务已达成，智能体会通知用户；否则，它会重复该周期以纠正其路径。

#### 工作流程阶段

为了最大化生产力，开发者应在智能体循环之上遵循一套结构化的工作流程：

1.  **探索（Explore）**：理解代码库和现有的实现。
2.  **计划（Plan）**：在触碰代码之前，确定实现细节和策略。
3.  **执行（Execute）**：将计划转化为代码更改。
4.  **验证（Verify）**：检查实现是否与计划和需求相符。
5.  **提交（Commit）**：通过版本控制最终确定更改。

#### 探索与计划

探索和计划阶段利用了**计划模式（Plan Mode）**。
这种模式允许智能体在不进行编辑的情况下读取并推理代码，非常适合询问关于代码结构或请求流程的问题。
你甚至可以要求智能体使用 ASCII 艺术来可视化架构。

```plaintext
Claude Code v2.1.178
Opus 4.8 (1M context) · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-05/end

> Try "edit <filepath> to ..."

~/GitHub/dometrain-claude-code-getting-started/section-05/end (main) Opus 4.8 (1M context)
|| plan mode on (shift+tab to cycle)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=190)

在探索期间，你可以在尝试实现一项功能之前，先验证该功能是否已经存在。

```plaintext
> Please analyze the API and check if we have a feature to filter expenses based on a date range.
● I'll explore the API to check whether date-range filtering already exists.
* Actioning... (10s · ↓ 379 tokens)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=205)

在这个例子中，智能体分析了现有的 `ListAsync` 实现，确认当前尚不存在日期过滤功能。

```csharp
private static async Task<IResult> ListAsync(
    ExpenseDbContext db,
    CancellationToken cancellationToken)
{
    var expenses = await db.Expenses
        .AsNoTracking()
        .OrderByDescending(e => e.Date)
        .ThenByDescending(e => e.CreatedAt)
        .Select(e => new ExpenseResponse(e.Id, e.Amount, e.Description, e.Date, e.CreatedAt))
        .ToListAsync(cancellationToken);

    return Results.Ok(expenses);
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=250)

探索完成后，你需要制定一份计划。
一份稳健的计划包括技术方法、具体的文件更改，以及验证策略（例如 TDD）。
对于日期范围过滤器，该计划包括支持可选的 `from` 和 `to` 查询参数，格式为 `yyyy-MM-dd`，确保边界是闭区间，并针对无效的日期或范围返回 `ValidationProblem`。

```bash
dotnet test
dotnet run --project api/ExpenseTracker.Api
curl "http://localhost:5057/api/expenses?from=2025-05-10&to=2025-05-20"
curl -i "http://localhost:5057/api/expenses?from=2025-12-31&to=2025-01-01"    # expect 400 problem+json
curl -i "http://localhost:5057/api/expenses?from=bad"                      # expect 400 problem+json
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=535)

#### 执行

在执行阶段，智能体将计划转化为代码。
这包括更新端点处理程序，并添加用于解析过滤器的辅助方法。

```csharp
if (fromDate is { } fromValue)
{
    query = query.Where(e => e.Date >= fromValue);
}

if (toDate is { } toValue)
{
    query = query.Where(e => e.Date <= toValue);
}

var expenses = await query
    .OrderByDescending(e => e.Date)
    .ThenByDescending(e => e.CreatedAt)
    .Select(e => new ExpenseResponse(e.Id, e.Amount, e.Description, e.Date, e.CreatedAt))
    .ToListAsync(cancellationToken);

// Helper for parsing
private static DateOnly? ParseDateFilter(string? value, string field, Dictionary<string, string[]> errors)
{
    if (string.IsNullOrWhiteSpace(value))
    {
        return null;
    }

    if (DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
    {
        return parsed;
    }

    errors[field] = [$"{field} must be a valid date in YYYY-MM-DD format."];
    return null;
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=595)

#### 验证

验证确保实现是正确的。
这涉及运行在计划阶段定义的自动化测试。
如果存在测试项目，Claude 可以理解如何运行并实现新测试，以遵循 TDD 循环。

```csharp
private static async Task<Guid> CreateExpenseAsync(
    HttpClient client,
    CreateExpenseRequest request,
    CancellationToken ct)
{
    var response = await client.PostAsJsonAsync("/api/expenses", request, ct);
    response.EnsureSuccessStatusCode();

    var created = await response.Content.ReadFromJsonAsync<ExpenseResponse>(JsonOptions, ct);
    Assert.NotNull(created);
    return created.Id;
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=610)

除了自动化测试之外，你还可以生成子智能体来执行代码审查。
这些智能体在一个独立的上下文中运行，并在代码被提交之前提供建议或识别遗漏的边界情况。

#### 提交

最后，智能体可以处理提交流程。
通过执行 Git 工具并分析会话上下文，Claude 可以生成一条合适的提交信息，并暂存相关文件。

```bash
git status --short && echo "---" && git diff --stat
git checkout -b feature/expense-date-range-filter
git add api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs tests/ExpenseTracker.Tests/ExpenseApiTests.cs
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-workflow-69958788/?t=880)

## 3. Working with Multiple Files

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/) · 6:05

Claude Code 在一个代码仓库的完整上下文中运作，使其能够自主发现与任务相关的文件。
虽然它会使用诸如 grep 之类的内部工具，根据提示查找代码，但开发者可以通过使用 `@` 符号显式引用文件或文件夹，来优化性能和 token 用量。
此外，Claude Code 支持多模态输入，使用户能够提供诸如 UI 截图之类的视觉上下文，以指导实现和样式设计，这在前端开发任务中尤其有效。

### 关键概念

- **仓库级上下文**：Claude Code 在整个代码库中工作，而不局限于单个打开的文件。
- **自主发现**：智能体使用诸如 grep 和 glob 之类的工具，根据用户的提示查找相关文件。
- **手动上下文控制**：使用 `@` 符号，使开发者能够显式地将特定文件或文件夹纳入上下文。
- **Token 优化**：引用特定文件可以减少对整个代码库进行大范围扫描的需要，从而节省时间和 token。
- **多模态输入**：支持通过剪贴板或拖放方式使用图片（截图），以提供视觉需求或反馈。

### 课程笔记

Claude Code 在整个代码仓库的上下文中运作。
与需要手动复制粘贴代码片段的传统聊天式智能体不同，Claude 可以自主地在多个文件之间定位信息。
当发出的提示没有指定具体的文件引用时，智能体会利用内部工具来扫描代码库，采用诸如 grep 和 glob 模式之类的技术来识别相关文件。

#### 显式的文件和文件夹引用

开发者可以通过显式提及文件或文件夹来精确控制上下文。
这是通过 `@` 符号加上文件路径来实现的。
在你输入路径时，Claude Code 会提供自动补全建议。

```shell
> Implement the UI (the web part) for the expenses API endpoint that contains the date filters.
  @api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs .  examples on how to use the endpoint available at
  @tests/ExpenseTracker.Tests/ExpenseApiTests.cs
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=130)

引用也可以应用于文件夹级别。
这在功能驱动或模块化架构中特别有用，因为在这些架构中，你希望将 Claude 的范围限制在特定目录内。

```shell
> Implement the UI (the web part) for the expenses API endpoint that contains the date filters.
  @api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs .  examples on how to use the endpoint available at @tests/ExpenseTracker.Tests/

+ tests/ExpenseTracker.Tests/
+ tests/ExpenseTracker.Tests/ExpenseApiTests.cs
+ tests/ExpenseTracker.Tests/AssemblyFixtures.cs
+ tests/ExpenseTracker.Tests/PostgresApiFactory.cs
+ tests/ExpenseTracker.Tests/ExpenseTracker.Tests.csproj
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=145)

#### 自主探索

当文件被引用时，Claude 会首先读取这些特定的文件。
如果提供的信息不足以完成任务，智能体会探索代码库的其余部分，以寻找相关的组件、样式或逻辑。

```shell
Implement the UI (the web part) for the expenses API endpoint that contains the date filters.
@api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs .  examples on how to use the endpoint available at
@tests/ExpenseTracker.Tests/ExpenseApiTests.cs
└ Read api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs (143 lines)
└ Read tests/ExpenseTracker.Tests/ExpenseApiTests.cs (167 lines)

● I'll explore the web app structure first to understand how to fit the date filter UI in.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=160)

在这个探索阶段，Claude 使用标准的 shell 实用程序来绘制项目结构，并搜索相关字符串。

```shell
Explore(Explore web SPA structure)
└ Bash(tree -L 3 -I node_modules /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-05/end/web/src --charset
    ascii)
  Running...
  Bash(find /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-05/end/web/src -type f -o -type d | grep -v
    node_modules | sort)
  Running...
  Bash(grep -r "api/expenses" /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-05/end/web/src --include="*.ts"
    --include="*.tsx" -A 2 -B 2)
  Running...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=190)

#### 上下文管理与 Token

显式提供文件不仅通过将智能体引导至正确的位置来节省时间，还能优化 token 消耗。
你可以使用 `/context` 命令来监控当前的 token 用量以及上下文中当前存在的文件。

```shell
- The filter is part of the React Query queryKey (["expenses", { from, to }]), so changing a date refetches automatically.
- Client-side guard: if from > to the query is disabled (enabled: !invalidRange) and an inline error shows.

web/src/components/ExpenseTable.module.css - added .container, .filter, .field, and .clear styles.

> /context

+ Transmuting...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=210)

#### 使用图片进行视觉上下文引用

对于前端任务，提供图片对于满足样式或定位需求非常有效。
图片可以被添加到代码仓库中，并通过 `@` 符号引用，或者更方便的是，直接拖放到终端中，或从剪贴板粘贴。
请注意，在 macOS 上，向终端粘贴的快捷键是 Ctrl+V，而不是 Cmd+V。

```shell
> [Image #1]
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=305)

一旦添加了图片，它会被分配一个标识符（例如 `[Image #1]`），可以与文本提示结合使用，以指导智能体的实现。

```shell
> [Image #1] style the filters
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/working-with-multiple-files-69958789/?t=315)

## 4. Running Commands and Reading Output

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/running-commands-and-reading-output-69958790/) · 3:42

### 摘要

Claude Code 执行 shell 命令并解读其输出的能力，使其能够在开发过程中闭合反馈循环。
智能体不仅仅是生成代码，还可以编译应用程序、运行测试套件，并分析日志或堆栈跟踪，以验证其工作成果。
这实现了一个无需人工干预、自主进行规划、编码和验证的循环。

### 关键概念

*   工具执行：直接从智能体运行 bash 脚本、编译器和测试运行器。
*   自主反馈循环：对测试失败和错误日志做出反应，自我纠正实现。
*   上下文收集：读取堆栈跟踪和完整日志以诊断问题。
*   效率：自动化探索-计划-编码-验证-提交的循环。
*   MCP 服务器：通过浏览器自动化等外部工具扩展 Claude Code 的能力。

### 课程笔记

Claude Code 作为一个智能体，能够在开发环境中执行工具。
这种能力不仅限于读写代码，还包括编译应用程序、运行测试以及执行 bash 脚本。
通过观察这些命令的结果，智能体可以对错误做出反应，并在没有人工干预的情况下迭代解决方案。

为了演示这一点，一个 bug 被引入到某个 API 端点的验证逻辑中。
在下面来自 `ExpenseEndpoints.cs` 的代码片段中，验证检查被故意破坏，使得 API 在没有错误时返回一个验证问题（validation problem），这将导致测试套件失败。

```csharp
if (request.Date == default)
{
    errors["Date"] = ["Date is required."];
}

if (errors.Count == 0)
{
    return Results.ValidationProblem(errors);
}

var expense = new Expense
{
    Amount = decimal.Round(request.Amount, 2, MidpointRounding.AwayFromZero),
    Description = request.Description!.Trim(),
    Date = request.Date,
};

db.Expenses.Add(expense);
await db.SaveChangesAsync(cancellationToken);
await db.Entry(expense).ReloadAsync(cancellationToken);

var response = ToResponse(expense);
return Results.Created($"/api/expenses/{response.Id}", response);
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/running-commands-and-reading-output-69958790/?t=55)

当测试运行时，控制台输出会显示多个错误。
与其手动修复这些问题，Claude Code 可以被提示来处理整个验证和修复循环。

```plaintext
Run the dotnet tests and fix any problem.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/running-commands-and-reading-output-69958790/?t=85)

Claude Code 执行 `dotnet test` 命令，捕获失败的输出，并分析源代码以识别逻辑错误。
然后它会生成一个修复方案，并提议将其应用到代码库中。

```csharp
59          errors["Date"] = ["Date is required."];
60      }
61
62 -    if (errors.Count == 0)
62 +    if (errors.Count > 0)
63      {
64          return Results.ValidationProblem(errors);
65      }
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/running-commands-and-reading-output-69958790/?t=95)

这种收集上下文、采取行动并验证结果的紧密循环，是智能体工作流程的核心。
这一过程还得到了模型上下文协议（MCP）服务器的进一步增强，这些服务器可以向智能体暴露诸如网页浏览器之类的额外工具。
这使得 Claude 能够从外部来源或 UI 状态收集信息，并继续探索、规划、编码和验证的循环，直到达成期望的状态。

## 5. Permissions and Modes

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/) · 8:46

Claude Code 在本地机器上执行工具的能力——例如读取文件、编辑代码、编译应用程序或运行 CLI 命令——带来了潜在的风险。
为了管理这些风险，该环境提供了若干种执行模式，用于控制工具使用时权限的授予方式。

#### 关键概念

*   **权限模式（Permission Modes）**：四种不同的模式（默认、接受编辑、计划和自动），决定了需要多大程度的用户干预。
*   **模式切换（Mode Cycling）**：`Shift + Tab` 快捷键允许用户实时在执行模式之间切换。
*   **计划模式（Plan Mode）**：一种只读状态，用于收集上下文并提出更改建议，而不修改代码库。
*   **自动模式（Auto Mode）**：一种智能的执行状态，Claude 会评估工具的安全性，仅在操作被判定为有风险时才提示请求权限。
*   **权限持久化**：配置文件（`settings.json` 和 `settings.local.json`）允许对特定命令进行永久性的白名单或黑名单设置。

#### 课程笔记

Claude Code 运作时会带有一个指示当前权限级别的状态栏。
用户可以使用 `Shift + Tab` 快捷键在这些模式之间切换，以使环境适应当前的工作流程。

```shell
Try "write a test for ExpenseTable.tsx"

~/GitHub/dometrain-claude-code-getting-started/section-05/end (main) Opus 4.8 (1M context)
━━ accept edits on (shift+tab to cycle) · ← for agents
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/?t=70)

##### 执行模式

1.  **默认（手动）模式**：这是限制最严格的模式。
Claude 在每一次文件编辑和每一次工具执行之前都会请求明确的权限。
虽然安全，但在涉及多个文件的大型重构中，这可能会变得重复而繁琐。
2.  **接受编辑模式（Accept Edits On）**：在这种模式下，Claude 被允许自动修改文件。
但是，在执行外部工具或 bash 命令之前，它仍会暂停并请求权限。
3.  **计划模式（Plan Mode）**：这是一种只读模式。
它专为任务的初始阶段设计，此时 Claude 收集上下文并提出一份技术方案。
它不会触碰代码或执行工具。
请注意，计划模式指的是一种权限状态，而不是底层模型推理能力的改变。
4.  **自动模式（Auto Mode）**：这种模式使用一个分类器来评估命令的安全性。
如果工具执行是标准且可信的（例如一个常见的构建命令），Claude 会自动继续。
如果命令被标记为可能有害或不熟悉，它会提示请求权限。
这种模式在速度和安全之间取得了平衡，但由于额外的评估步骤，可能会产生更高的 token 成本。

##### 处理文件编辑

在默认模式下，Claude 会展示所提议更改的差异（diff），并等待用户确认。
用户可以选择接受单次编辑、在会话的其余时间内允许所有编辑，或拒绝该更改。

```csharp
138         }
139
140     private static DateOnly? ParseDateFilter(string? value, string field, Dictionary<string,
...
153         return null;
154     }
155
156 +   private static Expense ToExpense(CreateExpenseRequest request) =>
157 +       new()
158 +       {
159 +           Amount = decimal.Round(request.Amount, 2, MidpointRounding.AwayFromZero),
160 +           Description = request.Description!.Trim(),
161 +           Date = request.Date,
162 +       };
163 +
164     private static ExpenseResponse ToResponse(Expense e) =>
165         new(e.Id, e.Amount, e.Description, e.Date, e.CreatedAt);
166 }

Do you want to make this edit to ExpenseEndpoints.cs?
> 1. Yes
  2. Yes, allow all edits during this session (shift+tab)
  3. No
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/?t=280)

##### 工具权限与配置

当 Claude 需要执行一个 bash 命令时，例如压缩一个文件，如果该命令之前没有被授权过，它会提示请求权限。
用户可以授予一次性权限，或永久授权该特定命令。

```plaintext
Bash command

  cd /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-05/end/api/ExpenseTracker.Api
  Program.cs
  Zip Program.cs

Do you want to proceed?
> 1. Yes
  2. Yes, and don't ask again for: zip Program.cs.zip Program.cs
  3. No
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/?t=340)

被授权的命令存储在 `.claude/settings.local.json` 文件中。
这个文件特定于本地环境，通常会被版本控制忽略。

```json
{
  "permissions": {
    "allow": [
      "Bash(dotnet build *)",
      "Bash(dotnet test *)",
      "Bash(npm --prefix web run typecheck)",
      "Bash(zip Program.cs.zip Program.cs)"
    ]
  }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/?t=380)

为了明确阻止 Claude 运行某些工具，可以添加一个 `deny` 列表到配置中。
如果这些规则需要应用于整个团队，可以将它们移动到 `.claude/settings.json`，该文件旨在被提交到代码仓库中。

```json
{
  "permissions": {
    "allow": [
      "Bash(dotnet build *)",
      "Bash(dotnet test *)",
      "Bash(npm --prefix web run typecheck)"
    ],
    "deny": ["Bash(zip Program.cs.zip Program.cs)"]
  }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/?t=400)

在自动模式下，系统会通过其内部分类器来验证操作的安全性，从而绕过这些手动提示。

```plaintext
Bash(cd /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-05/end/api/
    Program.cs)
  L   updating: Program.cs (deflated 42%)
  L   Allowed by auto mode classifier

Done. Program.cs is zipped into api/ExpenseTracker.Api/Program.cs.zip.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/?t=475)

## 6. Refining and Continuing Conversations

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/refining-and-continuing-conversations-69958791/) · 6:37

#### 摘要

有效使用 Claude Code 进行开发，依赖于一种迭代式的工作流程，即把最初的输出视为草稿，通过持续的反馈和上下文管理不断加以优化。
开发者可以通过在计划模式和编辑模式之间切换、中断执行以提供澄清性引用，以及利用 `ask_user_question` 工具来解决歧义，从而引导模型。
至关重要的是，当某个具体的实现路径被判定为不正确时，使用回退（rewind）功能优于手动纠正，以防止上下文污染，确保被舍弃的逻辑不会影响后续的模型决策。

#### 关键概念

* **迭代优化（Iterative Refinement）**：把最初的结果视为草稿，可以通过反馈进行调整。
* **中断（Esc）**：在任务进行到一半时停止 Claude，以纠正方向或完善提示。
* **上下文窗口管理**：向上下文中添加特定的文件引用，以使实现与现有模式保持一致。
* **Ask User Question 工具**：一种机制，Claude 借此暂停以澄清假设或实现细节。
* **回退（`/rewind`）**：将对话恢复到先前状态，以移除不需要的历史记录。
* **上下文污染**：在对话历史中保留被舍弃或不正确的逻辑所产生的负面影响，这可能会在后续步骤中使模型感到困惑。

#### 课程笔记

在使用 Claude Code 时，最初的输出应被视为一份草稿。
开发过程是一个不断优化的循环，你通过向上下文窗口中添加细节，来达成期望的结果。
你可以在不同的模式下启动任务，例如**计划模式**（Claude 读取文件并提出策略）或**编辑模式**（Claude 直接进入实现阶段）。

```bash
‰ /clear

‰ Add a GET /api/expenses/recent endpoint that returns expenses from the last 7 days.

● I'll follow the project's TDD workflow for the Expenses API. Let me first look at the existing endpoint and test code.

● Reading 2 files... (ctrl+o to expand)
  └ tests/ExpenseTracker.Tests/ExpenseApiTests.cs

* Cascading... (11s · 321 tokens)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/?t=70)

如果你观察到 Claude 走上了一条错误的路径，或者做出的决定与你的需求不符，你可以按下 `Esc` 立即中断该过程。
一旦停止，你可以通过提供更多上下文来完善提示，例如引用某个特定功能在代码库其他地方是如何实现的，以确保风格上的一致性。

```bash
‰ /clear

‰ Add a GET /api/expenses/recent endpoint that returns expenses from the last 7 days.

● I'll follow the project's TDD workflow for the Expenses API. Let me first look at the existing endpoint and test code.

  Read 2 files (ctrl+o to expand)

● The endpoint and its test already exist in the working tree (the two modified files). Let me run the test to confirm it passes.

● Bash(dotnet test --filter "FullyQualifiedName~ExpenseApiTests.Should_return_only_expenses_from_the_last_7_days_when_listing_recent" 2>&1 | tail -20)
  └ Interrupted · What should Claude do instead?

‰ Match how ListAsync in @api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/?t=100)

Claude 也可能触发 `ask_user_question` 工具。
这在计划模式下很常见，当 Claude 需要就查询构建方式、验证错误或过滤方法等方面的歧义寻求澄清时。
你可以从建议的选项中进行选择，或提供自定义的提示，来指导实现结构。

```csharp
var query = db.Expenses.AsNoTracking();
query = query.Where(e => e.Date >= since && e.Date <= today);
var expenses = await query
    .OrderByDescending(e => e.Date)
    .ThenByDescending(e => e.CreatedAt)
    .Select(...)
    .ToListAsync(ct);
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/?t=130)

随着实现的推进，你可以继续调整一些细节。
例如，如果你决定更改日期的处理方式（例如从 UTC 改为服务器本地时间），你可以提示 Claude 更新相应的逻辑。

```csharp
129         var today = DateOnly.FromDateTime(DateTime.UtcNow);
130         var since = today.AddDays(-7);
131
132 -       var expenses = await db.Expenses
133 -           .AsNoTracking()
134 -           .Where(e => e.Date >= since && e.Date <= today)
132 +       var query = db.Expenses.AsNoTracking();
133 +
134 +       query = query.Where(e => e.Date >= since && e.Date <= today);
135 +
136 +       var expenses = await query
137             .OrderByDescending(e => e.Date)
138             .ThenByDescending(e => e.CreatedAt)
139             .Select(e => new ExpenseResponse(e.Id, e.Amount, e.Description, e.Date, e.CreatedAt))
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/?t=205)

```csharp
126             ExpenseDbContext db,
127             CancellationToken cancellationToken)
128         {
129 -       var today = DateOnly.FromDateTime(DateTime.UtcNow);
129 +       var today = DateOnly.FromDateTime(DateTime.Now);
130         var since = today.AddDays(-7);
131
132         var query = db.Expenses.AsNoTracking();
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/?t=235)

如果你意识到之前的一个指令或实现路径是一个错误，你有两个选项：向前推进，要求 Claude 撤销该更改；或者使用**回退（Rewind）**功能。
回退可以通过 `/rewind` 命令访问，或者通过按两次 `Esc` 来访问。
这使你能够"时间旅行"到对话历史中的某个特定点，并从那里重新开始。

```csharp
129            var today = DateOnly.FromDateTime(DateTime.UtcNow);
130            var since = today.AddDays(-7);
131
132 -          var expenses = await db.Expenses
133 -              .AsNoTracking()
134 -              .Where(e => e.Date >= since && e.Date <= today)
132 +          var query = db.Expenses.AsNoTracking();
133 +
134 +          query = query.Where(e => e.Date >= since && e.Date <= today);
135 +
136 +          var expenses = await query
137                .OrderByDescending(e => e.Date)
138                .ThenByDescending(e => e.CreatedAt)
139                .Select(e => new ExpenseResponse(e.Id, e.Amount, e.Description, e.Date, e.CreatedAt))
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/permissions-and-modes-69958792/?t=310)

在纠正重大错误时，回退在技术上优于向前推进，因为它会将错误路径完全从上下文窗口中移除。
这可以防止"上下文污染"，即 Claude 可能会利用被舍弃的决策来影响未来的结果，即使这些决策在代码层面已经被技术性地撤销。

## 7. Resuming Your Work

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/resuming-your-work-69958793/) · 3:25

Claude Code 使你能够在各个开发会话之间保持连续性。
如果你关闭了终端、关机，或切换到其他任务，你可以返回到之前的对话，以保留已经与模型建立起来的上下文、文件分析和进度。

### 关键概念

- **会话持久化**：Claude Code 会保存对话历史，使你能够恢复工作，而无需重新解释代码库或任务需求。
- **`--continue` 参数**：一个 CLI 参数，用于立即跳回到最近的一次对话。
- **`--resume` 参数**：一个 CLI 参数，会打开一个交互式的历史会话列表，允许你选择历史记录中的某个特定点来恢复。
- **斜杠命令**：内部命令（`/continue`、`/resume`、`/clear`），用于在不退出 Claude 环境的情况下管理上下文窗口。
- **上下文保留**：恢复一个会话可以避免用冗余信息"污染"上下文，并通过利用模型对当前任务已有的了解，节省时间。

### 课程笔记

#### 恢复最近的会话

当你想要准确地从上次中断的地方继续时，请使用 `--continue` 参数。
这是返回到进行中的功能实现的最高效方式。
例如，如果你之前在开发一个"最近费用"端点并关闭了会话，运行 `claude --continue` 会恢复那个特定的线程。

```shell
Want me to run the test (Should_return_only_expenses_from_the_last_7_days_when_listing_recent) to confirm it still passes?

* Churned for 30s

* recap: Goal: add a GET /api/expenses/recent endpoint returning last-7-days expenses. It's implemented, matched to ListAsync's style, and now
  anchors to server-local date. Next: run the recent-expenses test to confirm it passes. (disable recaps in /config)

> yes

● Docker needs to be running for the tests (Testcontainers Postgres). Running the recent-expenses test now.

● Bash(dotnet test --filter "FullyQualifiedName~ExpenseApiTests.Should_return_only_expenses_from_the_last_7_days_when_listing_recent" 2>&1 |
    tail -25)
  L Determining projects to restore...
    All projects are up-to-date for restore.
    ExpenseTracker.Api -> /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-05/end/api/ExpenseTracker.Api/b
    ... +8 lines (ctrl+o to expand)

● Passed - build is clean and the test is green.

  - Build: succeeded with no warnings (warnings-as-errors is on).
  - Test: Should_return_only_expenses_from_the_last_7_days_when_listing_recent -> Passed (1/1, 369 ms).

GET /api/expenses/recent returns expenses with Date in the inclusive [today-7, today] window, ordered Date desc th
structured to match ListAsync.

* Churned for 20s

/exit
L Bye!

>
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/resuming-your-work-69958793/?t=40)

#### 浏览会话历史

如果你需要返回到一个不是最近的会话，或者你想查看跨不同分支和时间段的过往工作列表，请使用 `--resume` 参数。

```shell
claude --resume
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/resuming-your-work-69958793/?t=70)

这个命令会打开一个交互式菜单，你可以在其中搜索你的历史记录。
每个条目都会显示会话标题（由 Claude 根据任务生成）、发生的时间、Git 分支以及上下文窗口的大小。

```shell
Resume session (1 of 15)

  Search...

  dometrain-claude-code-getting-started

> Add GET endpoint for recent expenses
  14 seconds ago · main · 167.3KB

  Add recent expenses API endpoint
  17 minutes ago · main · 143.6KB

  Add recent expenses API endpoint
  24 minutes ago · main · 98.1KB

  Zip ExpenseTracker.Api Program.cs file
  30 minutes ago · main · 54.8KB

  Refactor and organize ExpenseTracker API Program.cs
  42 minutes ago · main · 97.9KB

  Refactor and organize ExpenseTracker API program
  51 minutes ago · main · 86.3KB

  Refactor and organize ExpenseTracker API Program.cs
  55 minutes ago · main · 104.7KB

↓ List files from folder
  1 hour ago · main · 34KB

  Ctrl+A to show all projects · Ctrl+B to only show current branch · Space to preview · Ctrl+R to rename · Type
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/resuming-your-work-69958793/?t=80)

#### 在 Shell 中管理上下文

你不需要退出 Claude 就可以改变你的上下文。
在交互式 shell 中直接就有几个斜杠命令可用：

- `/clear`：开始一个全新的上下文窗口，实际上是在同一个项目中开始一段新的对话。
- `/resume`：从当前活动的 shell 中打开会话历史列表（与 `claude --resume` 相同）。
- `/continue`：从当前活动的 shell 中切换到最近的一次会话。

使用这些命令有助于防止"上下文污染"，这种情况发生在单个对话因混杂了不相关的任务而变得杂乱，从而使模型的准确性降低时。
通过切换会话或清空上下文，你可以确保 Claude 始终专注于你当前正在处理的特定功能或 bug。

## 8. Practical Exercise: A Real Development Session

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/) · 22:07

### 摘要

本课时通过在 .NET API 和 React SPA 中实现一项多币种功能，演示了 Claude Code 中一个端到端的开发工作流程。
它重点展示了计划模式与编码模式之间的切换、使用澄清性问题来解决实现假设，以及应用专门的子智能体来进行代码审查和简化。
该会话最后演示了使用 compact 命令进行上下文管理，以及对集成功能的手动验证。

### 关键概念

- 端到端的功能实现（API 和 UI）。
- 工作流程的切换：计划模式用于架构设计，编码/自动模式用于执行。
- 使用 `ask_user_question` 工具解决假设。
- 通过 Ctrl+G 进行计划审查和编辑。
- 使用 compact 命令进行上下文窗口管理。
- 使用 TDD 进行自动化验证，以及针对 UI 组件的手动验证。
- 使用 `/simplify` 和审查子智能体提升代码质量。

### 课程笔记

这次开发会话以一个明确的目标开始：为一个现有的费用追踪器添加多币种支持。
该功能要求在 `Expense` 实体中添加一个 `Currency` 字段，限制为 EUR、USD 和 GBP，默认值为 EUR。

```plaintext
Add multi-currency support to expenses. Add a `Currency` field to the `Expense` entity, stored as a 3-letter ISO 4217 code, restricted to a small allowed set (EUR, USD, GBP). `POST /api/expenses` accepts an optional `currency`, defaulting to EUR; reject any code outside the allowed set with `Results.ValidationProblem`. Add an EF Core migration that adds the column and backfills existing rows to EUR. Include tests on the API validation and the default behaviour. No exchange rates, no conversion, no totals.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=85)

#### 规划与假设解决

实现从计划模式开始。
在执行之前，审查所提出的计划至关重要。
按下 Ctrl+G 会将该计划作为一个 Markdown 文档在编辑器中打开，以便更轻松地阅读和编辑。
为了避免错误的假设，Claude 被提示不断提出澄清性问题，直到诸如规范化规则、数据库存储类型和迁移策略等所有细节都得到明确。

领域逻辑被集中在一个新的 `Currencies` 类中：

```csharp
namespace ExpenseTracker.Api.Domain;

public static class Currencies
{
    public const string Default = "EUR";

    public static readonly IReadOnlySet<string> Allowed =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "EUR", "USD", "GBP" };
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=295)

#### API 实现

实现涉及更新领域实体、EF Core 数据库上下文和 API 契约。
`CreateExpenseRequest` 更新为包含一个可选的 `Currency` 参数，以保持向后兼容性。

```csharp
// Expense.cs
public required string Currency { get; init; }

// ExpenseDbContext.cs
expense.Property(e => e.Currency)
    .IsRequired()
    .HasMaxLength(3);

// CreateExpenseRequest.cs
public sealed record CreateExpenseRequest(decimal Amount, string? Description, DateOnly Date, string? Currency = null);

// ExpenseResponse.cs
public sealed record ExpenseResponse(Guid Id, decimal Amount, string Description, DateOnly Date, string Currency, DateTimeOffset CreatedAt);
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=310)

#### 数据库迁移

计划被接受后，工作流程会转向自动模式。
Claude 生成一个添加 `Currency` 列的 EF Core 迁移。
该迁移遵循一种安全的更新模式：先将该列添加为可为空，用默认值（'EUR'）回填现有记录，然后将该列更改为不可为空。

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<string>(
        name: "Currency",
        table: "Expenses",
        type: "character varying(3)",
        maxLength: 3,
        nullable: true);

    migrationBuilder.Sql("UPDATE \"Expenses\" SET \"Currency\" = 'EUR';");

    migrationBuilder.AlterColumn<string>(
        name: "Currency",
        table: "Expenses",
        type: "character varying(3)",
        maxLength: 3,
        nullable: false,
        oldClrType: typeof(string),
        oldType: "character varying(3)",
        oldMaxLength: 3,
        oldNullable: true);
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=565)

#### 验证与审查

验证是通过自动化测试执行的。
Claude 首先实现失败的测试，以验证验证逻辑和默认行为。

```csharp
[Theory]
[InlineData(null, "EUR")]      // omitted entirely
[InlineData("", "EUR")]        // empty string
[InlineData("USD", "USD")]     // valid code
[InlineData("usd", "USD")]     // lowercase normalized
[InlineData("  usd  ", "USD")] // whitespace trimmed + normalized
public async Task Should_resolve_currency_on_create(string? currency, string expected)
{
    var ct = TestContext.Current.CancellationToken;
    var client = factory.CreateClient();
    object payload = currency is null
        ? new { amount = 12.50m, description = "Currency theory", date = "2026-04-01" }
        : new { amount = 12.50m, description = "Currency theory", date = "2026-04-01", currency };

    var response = await client.PostAsJsonAsync("/api/expenses", payload, JsonOptions, ct);

    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    var created = await response.Content.ReadFromJsonAsync<JsonElement>(JsonOptions, ct);
    Assert.Equal(expected, created.GetProperty("currency").GetString());
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=670)

实现完成后，可以使用 `/simplify` 命令或代码审查子智能体来识别潜在的改进，例如将重复的测试转换为 Theory 代码块。

#### UI 实现与上下文管理

在进入 UI 阶段之前，会使用 compact 命令来总结对话并减少上下文窗口的用量。
UI 任务涉及在 React 表单中添加一个货币选择器，并更新表格以显示货币符号。

```typescript
const [currency, setCurrency] = useState<Currency>(DEFAULT_CURRENCY);

// ... inside handleSubmit
mutation.mutate({ amount: parsedAmount, description: trimmed, date, currency });

// ... inside return
<label className={styles.field}>
  <span>Currency</span>
  <select
    value={currency}
    onChange={(event) => setCurrency(event.target.value as Currency)}
    required
  >
    {CURRENCIES.map((code) => (
      <option key={code} value={code}>
        {code}
      </option>
    ))}
  </select>
</label>
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=1120)

#### 最终测试

最后一步涉及同时运行 API 和 web 应用程序，进行手动冒烟测试。
Claude 可以协助应用迁移并启动服务。

```bash
docker compose up -d
dotnet ef database update --project api/ExpenseTracker.Api
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=1180)

通过向 Claude 提供截图，可以在最终提交和推送之前进行视觉优化，调整布局。

```tsx
<label className={styles.field}>
  <span>Currency</span>
  <select
    value={currency}
    onChange={(event) => setCurrency(event.target.value as Currency)}
    required
  >
    {CURRENCIES.map((code) => (
      <option key={code} value={code}>
        {code}
      </option>
    ))}
  </select>
</label>
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/practical-exercise-a-real-development-session-69958794/?t=1270)

## 9. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958795/) · 1:39

### 摘要

本课时总结了有效使用 Claude Code 的核心开发工作流程和交互技巧。
它强调了一种结构化的方法——探索、规划、实现、验证和提交——同时利用特定的工具模式，以及诸如时间旅行和检查点之类的对话管理功能，来保持对开发过程的控制。

### 关键概念

- 标准开发工作流程：探索、计划、编码、验证、提交。
- 交互模式：计划模式用于发现，自动/手动模式用于实现。
- 对话管理：回退、延续过往会话，以及跳转到检查点。
- 优化策略：了解何时应该在计划上迭代，何时应该退回到先前的状态。

### 课程笔记

Claude Code 中的开发工作流程遵循一个五步循环：探索、计划、编码、验证和提交。
在探索和计划阶段，开发者使用该工具来理解代码库，并明确更改的范围。
过渡到编码阶段涉及将计划落地为实际的实现，随后是验证步骤，以确保更改与最初的意图一致。
最后，更改被提交到代码仓库。

Claude Code 提供了不同的模式来支持这些阶段。
计划模式用于发现和规划阶段，在不立即执行的情况下进行逻辑构思。
过渡到自动模式或手动模式便于实际的编码过程。

有效的交互还依赖于高级的对话管理技巧。
这些技巧包括回退对话、延续过往的讨论，以及跳转到特定的检查点。
这些功能使开发者能够完善自己的方法，忽略没有成效的路径，或在必要时退回到先前的状态。
建议在现有代码库上练习这一工作流程，以掌握在规划与执行之间的切换。

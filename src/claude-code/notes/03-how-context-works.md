# How Context Works

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 3
> 6 lessons · ~36 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958773/) | 1:58 | [↓](#1-introduction) |
| 2 | [How the Context Window Works](https://dometrain.com/take/course/getting-started-claude-code-3256128/how-the-context-window-works-69958774/) | 13:45 | [↓](#2-how-the-context-window-works) |
| 3 | [Context Poisoning](https://dometrain.com/take/course/getting-started-claude-code-3256128/context-poisoning-69958775/) | 5:58 | [↓](#3-context-poisoning) |
| 4 | [Compaction and the /context Command](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/) | 8:17 | [↓](#4-compaction-and-the-context-command) |
| 5 | [Quiz](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958777/) | 5:00 | [↓](#5-quiz) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958778/) | 1:06 | [↓](#6-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958773/) · 1:58

### 摘要

本课时介绍了上下文在大语言模型（LLM）交互中的关键作用，特别是在 Claude Code 环境中。
它强调上下文是一种"无形资源"，会显著影响响应的准确性、系统性能和运营成本，当模型给出自信但错误的信息时，上下文往往是背后的根本原因。

### 关键概念

* **作为无形资源的上下文（Context as a Silent Resource）**：上下文是影响 LLM 行为的主要因素，却常常在选择模型或进行提示工程时被忽视。
* **准确性与幻觉（Accuracy and Hallucination）**：自信但错误的回答，往往是上下文管理不善的结果，而非模型本身的失败。
* **性能权衡（Performance Trade-offs）**：增大上下文规模会导致系统性能下降和响应质量降低。
* **上下文管理（Context Management）**：理解如何管理上下文并避免上下文污染，对于保持高质量的交互至关重要。

### 课程笔记

上下文是与任何 LLM（包括 Claude Code）交互的基础要素。
虽然用户常常将错误或"自信但错误"的答案归咎于模型的局限性或糟糕的提示，但实际上，所提供的上下文往往才是错误的真正来源。

上下文是两种"无形资源"之一——另一种是成本——它们经常被忽视，但对于有效使用 LLM 至关重要。
它不仅影响模型输出的准确性和质量，还影响系统的整体性能。
随着上下文窗口规模的增长，存在着性能下降和响应质量降低的可衡量风险。
有效的上下文管理包括理解什么是上下文、如何控制其大小，以及如何降低上下文污染的风险。

## 2. How the Context Window Works

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/how-the-context-window-works-69958774/) · 13:45

### 摘要

上下文窗口是发送给大语言模型（LLM）以生成响应的有限资源集合（token）。
由于 LLM 的运作方式类似于无状态端点，每次交互都需要重新上传完整的对话历史、系统提示、工具以及引用的文件。
管理这个窗口对于保持准确性和控制成本至关重要，因为当用量超过总容量的 30% 到 40% 时，性能往往会下降——这种现象被称为"垃圾区（dump zone）"。
Claude Code 提供 `/context` 命令来可视化当前用量，并提供 `/clear` 命令来重置该窗口，使开发者能够通过专注于小型、独立的任务来保持高质量的输出。

### 关键概念

- **上下文即状态（Context as State）**：LLM 要求所有相关信息都在每次请求中提供。
- **分词（Tokenization）**：文本是按模型特定的 token 处理的，而不是按字符或单词处理的。
- **性能下降（Performance Degradation）**：准确性和速度通常在上下文消耗超过 30% 到 40% 后开始下降。
- **成本效率（Cost Efficiency）**：更大的上下文用量会增加每次请求的成本；像 Claude 3 Opus 和 Haiku 这样的模型有不同的定价和限制。
- **Claude Code 的上下文管理**：`/context` 和 `/clear` 等工具有助于监控和精简发送给模型的信息。
- **上下文构成**：包括系统提示、工具、项目内存（`CLAUDE.md`）、技能（skills）和消息历史。

### 课程笔记

#### 上下文窗口的本质

上下文窗口代表在一次请求过程中发送给大语言模型（LLM）的信息总量。
LLM 的运作方式类似于无服务器端点；它们在调用之间不保留状态。
要评估一个提示或延续一段对话，模型需要在当前请求中包含完整的历史记录和所有相关资源。
这一系列数据——对话历史、系统指令和附加文件——构成了上下文窗口。

#### 性能与成本影响

耗尽上下文窗口会产生两个主要后果：成本和准确性。

1. **成本**：模型按输入 token 计费。
例如，Claude 3 Opus 的成本大约是每百万 token 5 美元，而像 Claude 3 Haiku 这样更小的模型每百万 token 只需 1 美元。
更大的上下文限制（例如 Opus 的 100 万 token 对比 Haiku 的 20 万 token）提供了更多空间，但价格也更高。
2. **准确性（"垃圾区"）**：虽然模型有一个硬性上限，但性能往往在远未达到该上限之前就开始下降。
观察表明，一旦上下文消耗达到 30% 到 40%，结果的准确性和响应速度就会开始下降。
这个"垃圾区"导致的结果质量，比对话早期阶段的交互质量更低。

#### 主动的上下文管理

有效使用 Claude Code 需要主动管理加载到上下文中的内容。
包含不相关的信息会使模型感到困惑。
举例来说，如果你正在计划去波尔图（Porto）旅行，而加载了关于在里斯本（Lisbon）开车的信息，但你打算在波尔图使用公共交通，那么这些信息就是适得其反的。
如果模型的上下文中同时包含驾车和公共交通两方面的信息，它可能会在你明确需要公交路线时，却给出驾车路线。
在软件开发中，加载不相关的文档或大型日志文件，会导致模型给出不正确或次优的解决方案。

#### 在 Claude Code 中监控上下文

Claude Code 提供了内置工具来监控上下文用量。
`/context` 命令会生成当前窗口的可视化展示，按类别细分用量：

- **系统提示（System Prompt）**：Anthropic 提供的基线指令。
- **系统工具（System Tools）**：Claude Code 可以执行的命令和工具的定义。
- **内存文件（Memory Files）**：项目特定的上下文文件，例如 `CLAUDE.md`。
- **技能（Skills）**：自定义智能体或专门的指令。
- **消息（Messages）**：实际的对话历史。

在新会话开始后立即运行 `/context`，通常会显示由系统提示和工具消耗的基线用量（例如 2%）。
当你与代码库交互时——例如要求 Claude 列出 React 组件——模型会将相关文件加载到上下文的"消息（Messages）"部分，从而增加用量。

#### 最佳实践

为保持高性能和高准确性：

- **使用小任务**：将工作拆分为独立、可管理的小任务。
- **频繁清空上下文**：在切换到新任务时，使用 `/clear` 命令开始一次全新的对话。
这可以防止"有毒的"上下文——例如庞大而过时的日志文件或不相关的代码片段——影响未来的响应。
- **监控用量**：定期运行 `/context`，查看哪些资源消耗的 token 最多，并识别是否有不必要的插件或 MCP 服务器正在占用上下文窗口。

## 3. Context Poisoning

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/context-poisoning-69958775/) · 5:58

### 摘要

当之前错误的或不需要的 LLM 尝试仍然留在上下文窗口中时，就会发生上下文污染，这会影响后续的迭代并降低输出质量。
与其通过提示"再试一次"或提供纠正性指令来尝试修复错误——这只会增加更多"被污染的"上下文——推荐的做法是使用 rewind 命令或双击 Esc，返回到对话和代码中的先前状态。
这种做法能够有效管理上下文窗口，减少 token 用量，并确保模型不会被失败的尝试所带偏。

### 关键概念

- **上下文窗口的影响**：会话中的每一次交互都会保留在上下文窗口中，并影响后续所有的输出。
- **上下文污染（Context Poisoning）**：不需要的代码、逻辑或指令的累积，会使 LLM 产生偏差，并可能导致结果质量下降。
- **反模式（Anti-pattern）**：试图通过"继续前进"、发出更多提示（例如"改成这样做"）来纠正错误，而不是回退状态。
- **回退（Rewinding）**：使用 `/rewind` 或"双击 Esc"快捷键，回到对话历史中某个特定点的过程。
- **Token 管理**：回退会从上下文窗口中物理移除 token，防止窗口过早达到其上限。

### 课程笔记

在典型的开发工作流程中，智能体可能被要求分析一个现有的 API 并提出改进建议。
例如，一个 Expenses API 最初可能只有用于创建和列出费用的端点。

```csharp
namespace ExpenseTracker.Api.Endpoints;

public static class ExpenseEndpoints
{
    public static IEndpointRouteBuilder MapExpenseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/expenses");

        group.MapPost("/", CreateAsync);
        group.MapGet("/", ListAsync);

        return routes;
    }

    private static async Task<IResult> CreateAsync(
        CreateExpenseRequest request,
        ExpenseDbContext db,
        CancellationToken cancellationToken)
    {
        var errors = new Dictionary<string, string[]>();

        if (request.Amount <= 0)
        {
            errors["Amount"] = ["Amount must be greater than zero."];
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            errors["Description"] = ["Description is required."];
        }
        else if (request.Description.Length > 200)
        {
            errors["Description"] = ["Description must be 200 characters or fewer."];
        }
        // ...
    }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/context-poisoning-69958775/?t=55)

如果智能体建议添加多个缺失的端点（例如按 ID 获取、PUT 和 DELETE），而用户给出一个含糊的提示，例如"继续吧"，智能体可能会一次性实现所有端点，即使用户原本只打算处理优先级最高的那一项。
这可能导致一些不希望出现的副作用，例如将领域模型中不可变的属性改为可变的：

```csharp
public sealed class Expense
{
    public Guid Id { get; init; } = Guid.NewGuid();
-   public required decimal Amount { get; init; }
-   public required string Description { get; init; }
-   public required DateOnly Date { get; init; }
+   public required decimal Amount { get; set; }
+   public required string Description { get; set; }
+   public required DateOnly Date { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/context-poisoning-69958775/?t=115)

此外，智能体可能会实现比预期更多的端点，用用户没有明确批准或希望以不同方式实现的逻辑，使代码库和上下文窗口变得杂乱。

```csharp
public static IEndpointRouteBuilder MapExpenseEndpoints(this IEndpointRouteBuilder routes)
{
    var group = routes.MapGroup("/api/expenses");

    group.MapPost("/", CreateAsync);
    group.MapGet("/", ListAsync);
    group.MapGet("/{id:guid}", GetByIdAsync);
    group.MapPut("/{id:guid}", UpdateAsync);
    group.MapDelete("/{id:guid}", DeleteAsync);

    return routes;
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/context-poisoning-69958775/?t=145)

当这种情况发生时，一个常见的错误是提示智能体"再试一次"，或者"移除 PUT 和 DELETE 端点，改用单一的 DTO"。
这是一种反模式，因为之前不正确的实现仍然留在上下文窗口中。
随着上下文窗口被填满，这些"被污染"的信息会影响后续迭代的质量。

与其向前推进以修复一个错误，你应该向后退一步。
通过使用 `/rewind` 命令或按下"双击 Esc"，你可以选择返回对话中的某个先前时刻。
这会将对话历史和代码都恢复到那个状态。

回退对 token 用量有着直接的影响。
例如，在一次不需要的实现之后，会话可能处于 42.7k token（占窗口的 4%）的水平。
在回退到"继续吧"这个提示之前的状态后，上下文可能会降到 29.2k token（3%）。
这使你能够用更精确的提示来纠正方向，例如"继续实现单一获取端点"，从而确保智能体只执行所需的特定任务，而不会受到之前失败尝试的影响。

## 4. Compaction and the /context Command

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/) · 8:17

### 摘要

压缩（Compaction）是 Claude Code 中的一项上下文管理功能，它通过总结对话历史来优化上下文窗口。
当上下文窗口接近其上限（例如 100 万 token）时，Claude Code 可以执行自动压缩来释放空间。
用户也可以使用 `/compact` 命令手动触发这一过程。
虽然压缩能显著减少 token 用量并提高成本效率，但它会用一份摘要来取代完整的消息历史，这可能导致一些细枝末节的丢失。

### 关键概念

* **上下文窗口优化**：管理 token 用量，以防止达到模型上限。
* **压缩（Compaction）**：生成对话摘要，并用该摘要替换之前消息的过程。
* **自动压缩**：当上下文接近上限时，由 Claude Code 自主执行的上下文管理。
* **手动压缩**：通过 `/compact` 命令由用户发起的优化。
* **上下文监控**：使用 `/context` 查看系统提示、工具和消息之间的 token 分布。
* **压缩摘要**：压缩事件之后保留的项目状态、已加载文件和待办工作的持久记录。

### 课程笔记

随着与 Claude Code 的对话不断推进，上下文窗口会持续增长。
如果上下文窗口达到其总容量的较高百分比（例如 50% 到 60%），情况可能会变得不够理想。
为了解决这个问题，Claude Code 使用一种称为压缩的流程。

#### 监控上下文用量

在执行优化之前，你可以使用 `/context` 命令查看上下文窗口的当前状态。
这会按类别提供 token 用量的细分，包括系统提示、已加载的工具、内存文件、技能和消息历史。

```terminal
* recap: Goal: add invoice image upload with Claude vision auto-fill to the expense tracker. Current task: plan approved, awaiting go-ahead. Next action: start Phase 1, adding the ReceiptUrl column and EF migration. (disable recaps in /config)

/context
L Context Usage
  ...
  Opus 4.7 (1M context)
  claude-opus-4-7[1m]
  61.9k/1m tokens (6%)

  Estimated usage by category
  System prompt: 8.7k tokens (0.9%)
  System tools: 13.6k tokens (1.4%)
  Memory files: 98 tokens (0.0%)
  Skills: 1.1k tokens (0.1%)
  Messages: 42.4k tokens (4.2%)
  Free space: 934.1k (93.4%)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/?t=265)

#### 手动压缩

压缩的工作原理是要求 LLM 生成迄今为止对话的摘要。
这份摘要会捕获关键事实——例如项目位置、已加载的文件和待处理的任务——同时舍弃细枝末节以及之前消息的完整记录。
这使对话能够从一个"体面的" token 用量水平继续下去。

要手动触发此过程，请使用 `/compact` 命令。

```terminal
/compact             Free up context by summarizing the conversation so far
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/?t=285)

启动后，系统会显示压缩过程的进度。

```terminal
/compact

* Compacting conversation...
  ... 8%
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/?t=300)

#### 自动压缩

如果你没有手动压缩，而对话达到了模型的 token 上限（例如 100 万 token），Claude Code 会运行自动压缩。
你会在终端中看到一条"自动压缩中"的通知。
虽然这可以防止对话因上下文限制而失败，但应避免频繁的自动压缩，以保持尽可能高的准确性，因为生成的摘要可能会丢失复杂任务所需的细微差别。

#### 验证压缩结果

压缩之后，`/context` 命令可能会显示 token 用量的显著减少。
请注意，界面有时会在压缩后立即显示 0% 的用量，这是显示上的假象，但"消息（Messages）"这一类别会反映出新摘要的实际大小。

```terminal
/context
L Context Usage
  ...
  Opus 4.7 (1M context)
  claude-opus-4-7[1m]
  0/1m tokens (0%)

  Estimated usage by category
  System prompt: 8.7k tokens (0.9%)
  System tools: 13.6k tokens (1.4%)
  Memory files: 98 tokens (0.0%)
  Skills: 1.1k tokens (0.1%)
  Messages: 14.9k tokens (1.5%)
  Free space: 961.6k (96.2%)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/?t=345)

你也可以使用自然语言让 Claude Code "展示上下文窗口中的内容"。
这会提供压缩摘要和当前对话状态的详细细分。

```plaintext
> show me what's in the context window

● Context window contents

Model: Opus 4.7 (1M context) - claude-opus-4-7[1m]
Total used: ~23.8k / 1m tokens (~2.4%)

Breakdown

Category: System prompt
Tokens: 8.7k
What's in it: Claude Code base instructions, doing-tasks/tone-and-style sections, auto-memory protocol,
  environment block
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/?t=385)

压缩摘要会专门保留项目上下文以及当前任务的状态。

```plaintext
Category: Messages
Tokens: ~14.9k
What's in it: Compaction summary + the 5 file rereads + your /context and /compact outputs

Conversation state after /compact

The compaction summary preserves:
- Project location: section-03/end (.NET 10 + React expense tracker)
- All 33 source files were previously loaded (web/ 17 files, api/ 16 files)
- Pending work: 5-phase invoice upload plan (Claude vision OCR -> pre-filled form -> persisted
  by you but not yet started
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/compaction-and-the-context-command-69958776/?t=400)

#### 上下文管理的替代方案

如果你要切换到一个完全不同的任务，通常最好使用 `/clear` 命令，而不是进行压缩。
这会完全重置上下文窗口。
此外，Claude Code 还支持"子智能体（sub-agents）"，它们是拥有各自独立上下文窗口的子进程，为上下文管理提供了另一层手段。

## 5. Quiz

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958777/) · 5:00

没有该课时的文档 - 请直接观看。

## 6. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958778/) · 1:06

### 摘要

本课时总结了 Claude Code 中上下文管理的核心原则，强调有效使用编码智能体需要对上下文窗口进行主动控制。
它回顾了用于可视化上下文的工具、诸如压缩和回退之类的手动管理技巧，以及在达到硬性上限之前，模型性能开始下降的关键阈值。

### 关键概念

- 上下文可视化与状态监控。
- 手动上下文管理（添加文件、回退历史）。
- 上下文压缩及其对性能的影响。
- 上下文窗口的限制与"性能下降区"。

### 课程笔记

上下文管理是使用 Claude Code 的一项基本支柱，与提示工程和模型选择并列。
用户必须主动监控并操控上下文窗口，以保持智能体的性能。

#### 上下文可视化与管理

可视化上下文窗口的当前状态，对于理解模型正在使用什么信息至关重要。
关键的管理技巧包括：

- **上下文注入**：显式地将文件或文档添加到上下文窗口中，为模型提供必要的项目特定知识。
- **历史回退**：将对话恢复到先前的状态，以修剪掉可能使模型困惑的不相关或错误的历史记录。
- **压缩**：执行压缩流程以总结对话历史。
这在保留之前步骤核心意图和发现的同时，减少了 token 用量。

#### 性能阈值

管理上下文需要了解两个主要的限制：

1. **硬性上限（Hard Limit）**：模型上下文窗口的最大 token 容量。
2. **性能下降区（Degradation Zone）**：在达到硬性上限之前就会到达的一个阈值，此时由于信息量过大，模型的推理能力开始下降。
这个"垃圾区"是系统性能开始下降的地方，需要主动加以控制。

有效的日常使用 Claude Code，涉及运用这些技巧，使模型保持在其最佳运作范围内。
优化开发工作流程的下一阶段，涉及项目级别的优化，以确保最相关的信息能被高效地引入上下文窗口。

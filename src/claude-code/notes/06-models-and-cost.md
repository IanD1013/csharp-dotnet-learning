# Models and Cost

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 6
> 6 lessons · ~35 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958796/) | 2:31 | [↓](#1-introduction) |
| 2 | [The Model Lineup and Effort Levels](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/) | 16:13 | [↓](#2-the-model-lineup-and-effort-levels) |
| 3 | [Plan Mode vs Effort Levels](https://dometrain.com/take/course/getting-started-claude-code-3256128/plan-mode-vs-effort-levels-69958798/) | 3:12 | [↓](#3-plan-mode-vs-effort-levels) |
| 4 | [Tracking and Managing Costs](https://dometrain.com/take/course/getting-started-claude-code-3256128/tracking-and-managing-costs-69958799/) | 4:55 | [↓](#4-tracking-and-managing-costs) |
| 5 | [Quiz](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958800/) | 5:00 | [↓](#5-quiz) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958801/) | 3:00 | [↓](#6-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958796/) · 2:31

### 摘要

本课时介绍了 Claude Code 的两项关键无形资源：上下文窗口和成本。
它解释了 Claude Code 是如何作为一个构建在 Anthropic 大语言模型（LLM）之上的智能体运作的，使用一个智能体循环来执行工具。
本课时为理解如何选择合适的模型以及如何管理开支奠定了基础，将模型选择比作换挡，以针对特定任务和效率进行优化。

### 关键概念

- **无形资源（Invisible Resources）**：上下文窗口和财务成本是需要主动管理的因素。
- **智能体架构**：Claude Code 是构建在 LLM 之上的一个智能体，利用一个智能体循环来调用工具并执行机器级别的命令。
- **模型选择**：从 Anthropic 家族中选择正确的模型，类似于驾车时换挡，以最大化效率。
- **推理与执行**：LLM 充当"大脑"（推理），而智能体循环提供了实用性（行动）。

### 课程笔记

有效使用 Claude Code 需要管理两项主要的"无形资源"：上下文窗口和操作的财务成本。
虽然这些因素可能不会立即显现，但它们对于高效使用该工具至关重要。

Claude Code 被构建为一个基于大语言模型（LLM）之上的智能体。
虽然 LLM 提供了推理能力——充当系统的"大脑"——但智能体封装层实现了一个"智能体循环"。
这个循环使系统能够调用工具，并直接在本地机器上执行命令，将一个静态模型转变为一款实用的开发者工具。

Anthropic 提供了一系列能力各异的模型。
为特定任务选择合适的模型，对于性能和成本效益都至关重要。
这个过程类似于在汽车中换挡；对每一项简单任务都使用最强大的模型是低效的。
用户必须学会"换挡"，选择能够提供必要推理强度、同时又不产生不必要开支的模型。

接下来的部分将涵盖：
- 在订阅范围内控制开支和用量。
- 理解不同的 Anthropic 模型家族。
- 根据任务需求确定何时使用特定的模型。
- 管理模型的"推理强度（effort）"级别。

## 2. The Model Lineup and Effort Levels

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/) · 16:13

### 摘要

Claude Code 使用多个 Anthropic 模型家族，每个家族在推理能力、速度和成本之间提供不同的权衡。
用户可以通过为架构规划选择 Opus 这样的高层级模型，并在实现阶段切换到 Sonnet 或 Haiku 这样高效的模型，来优化其工作流程。
除了模型选择之外，Effort（推理强度）设置还允许针对特定任务微调推理的深度，范围从低延迟响应到最大深度的分析。

### 关键概念

- 模型家族：Fable、Opus、Sonnet 和 Haiku。
- 能力与速度/成本之间的权衡。
- 使用 `/model` 命令在会话中途切换模型。
- 用于调整推理深度的 Effort 等级（从低到最高）。
- 使用 `ultrathink` 提示后缀进行一次性的深度推理。
- Ultracode 作为 xhigh 推理强度与动态工作流程的组合。

### 课程笔记

Claude Code 是一个客户端工具，与 Anthropic 托管的模型进行交互。
理解模型阵容对于平衡性能和成本至关重要。
虽然模型家族本身相对稳定，但具体版本（例如 Opus 4.8、Sonnet 4.6）会频繁迭代。

#### 模型家族

Anthropic 提供了若干个模型家族，每个都适合不同的任务：
- **Fable**：最强大也最昂贵的模型，适用于需要长时间运行分析的极其困难的问题。访问权限可能因地区或企业策略而异。
- **Opus**：针对复杂推理、架构规划和高层次系统设计进行了优化。
- **Sonnet**：一款均衡的模型，在速度和复杂度之间提供权衡，非常适合日常编码任务和实现工作。
- **Haiku**：最快、最具成本效益的模型，最适合简单、原子化的任务，例如文档格式化或快速查询。

#### 选择与切换模型

可以使用 `--model` 参数以特定模型启动一次会话：

```shell
claude --model haiku
    Claude Code v2.1.179
    Haiku 4.5 · Claude Max
    ~/GitHub/dometrain-claude-code-getting-started/section-05/end

> Try "refactor ExpenseApiTests.cs"

~/GitHub/dometrain-claude-code-getting-started/section-05/end (main) Haiku 4.5
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/?t=505)

在一次活跃的会话中，`/model` 命令允许在不重启会话的情况下切换模型。
这对于从规划阶段（使用 Opus）过渡到实现阶段（使用 Sonnet）非常有用。

```shell
/model

Select model
Switch between Claude models. Your pick becomes the default for new sessions. For other/previous model names, specify with

  1. Default (recommended)  Opus 4.8 with 1M context · Best for everyday, complex tasks
  2. Opus (1M context)      Opus 4.8 with 1M context · Best for everyday, complex tasks
  3. Sonnet                 Sonnet 4.6 · Efficient for routine tasks
  4. Haiku                  Haiku 4.5 · Fastest for quick answers
> 5. Opus ✓                 Opus 4.8 · Best for everyday, complex tasks
  6. Fable (disabled)       Claude Fable 5 is currently unavailable. Learn more: https://www.anthropic.com/news/fa

● High effort (default) <-/-> to adjust

Enter to set as default · s to use this session only · Esc to cancel
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/?t=560)

#### 推理强度（Effort）等级

推理强度等级是一个独立于所选模型的推理深度的次级控制。
提高推理强度使模型能够"思考"得更深入，这提高了处理复杂问题时的准确性，但也增加了延迟和 token 消耗。

`/effort` 命令提供了一个从"更快"（低）到"更聪明"（最高）的可视化刻度：

```shell
/effort

Effort

                             Faster                               Smarter
                               ▲
                              low     medium      high      xhigh       max

⇦/⇨ to adjust · Enter to confirm · Esc to cancel
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/?t=640)

对于需要最大推理能力、但又不想改变会话默认设置的特定提示，可以附加 `ultrathink` 后缀：

```shell
> Let's fix the bug ultrathink
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/?t=715)

#### Ultracode

"Ultracode" 设置代表了推理能力的最高层级。
它将 `xhigh` 推理强度等级与 Claude Code 的"动态工作流程"功能结合起来，使智能体能够执行更复杂的多步骤操作。

```shell
/effort

Effort

            Faster                                      Smarter
                                  ▲
            low     medium      high      xhigh      max        ultracode
                                                            xhigh + workflows

←/→ to adjust · Enter to confirm · Esc to cancel
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/the-model-lineup-and-effort-levels-69958797/?t=790)

## 3. Plan Mode vs Effort Levels

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/plan-mode-vs-effort-levels-69958798/) · 3:12

### 摘要

Claude Code 将计划模式（Plan Mode）和推理强度等级（Effort Levels）区分为两个独立但互补的设置。
计划模式决定了操作策略和 Claude 将采取的行动顺序，而推理强度等级则控制了应用于这些行动的推理深度和认知强度。
理解两者之间的区别，对于管理复杂任务的输出质量，以及在上下文窗口用量和订阅限制方面产生的相关成本，都至关重要。

### 关键概念

- **计划模式 vs. 推理强度等级**：这是两个不同的设置；改变其中一个不会自动改变另一个。
- **计划模式（"做什么"）**：聚焦于实现一项功能或重构代码所需的策略和步骤序列。
- **推理强度等级（"怎么做"）**：决定 Claude 在一个问题上应用的推理深度和认知资源。
- **使用场景**：将计划模式用于涉及多个文件的更改，将推理强度等级用于复杂的算法或深层逻辑问题。
- **组合使用**：高推理强度和计划模式可以结合使用，用于需要深度思考和结构化计划的复杂调查。
- **资源影响**：这两项设置都会影响上下文窗口用量和订阅成本。

### 课程笔记

在 Claude Code 中，计划模式和推理强度等级是两个经常被误解的独立概念。
切换到计划模式（通常通过 `Shift + Tab`）并不会提高推理强度等级。
它们是可以根据任务复杂程度独立调整的参数。

计划模式代表了操作策略。
这是 Claude 用来确定要采取什么行动的机制，例如为一项影响代码库中多个文件的新功能或重构规划所需的更改。

推理强度等级代表了推理的强度。
这个设置决定了 Claude 对问题思考的努力程度。
提高推理强度等级适用于复杂的问题，例如调试一个复杂的算法，或处理需要深度技术推理的逻辑。

这两个设置可以结合起来以获得最大效果。
例如，当调查一个需要多步骤策略和深度分析的复杂问题时，开发者可以将推理强度设置为"高"，然后进入计划模式。
这确保了 Claude 在制定实现策略时应用最大的推理能力。

用户应该意识到，计划模式和推理强度等级都会对用量产生直接影响。
这些设置会影响上下文窗口，并计入订阅限制，因此根据任务的难度有意识地使用它们非常重要。

## 4. Tracking and Managing Costs

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/tracking-and-managing-costs-69958799/) · 4:55

### 摘要

在 Claude Code 中管理成本至关重要，因为添加到上下文窗口中的每一项内容都会影响 token 用量和总体开支。
CLI 提供了若干内置工具来实时监控这些成本，包括用于查看会话和每周报告的 `/usage` 命令，以及在状态栏中显示活动模型的能力。
理解子智能体是如何消耗 token 的，并在诸如 Opus 这样高能力的模型与诸如 Sonnet 这样更具成本效益的模型之间进行策略性切换，能够使开发者在性能和预算约束之间取得平衡。

### 关键概念

* **上下文影响**：纳入上下文窗口的每一条信息，都会影响整个会话的总成本。
* **状态栏可见性**：在终端状态栏中显示当前模型，可以防止意外使用昂贵的模型。
* **用量报告**：`/usage` 命令提供了成本、时长以及按模型划分的开支的全面细分。
* **子智能体成本**：子智能体在独立的上下文窗口中运行，它们的 token 消耗会计入总会话成本。
* **模型切换**：通过在规划阶段使用高层级模型、在执行阶段使用中层级模型来优化成本。

### 课程笔记

在使用 AI 编码智能体时，成本管理是一个主要关注点。
由于每一次交互和上下文添加都会产生成本，因此有必要使用内置的终端工具来监控和控制开支。

#### 状态栏配置

控制成本的一项主要建议，是在终端状态栏上启用模型显示。
这确保了活动模型始终可见，降低了对不需要那种推理水平的任务无意中运行 Opus 这类昂贵模型的风险。

#### 监控用量与统计

Claude Code 提供了 `/usage` 命令来追踪开支。
该命令提供了当前会话成本和每周用量的详细报告。
它还按技能（skills）细分消耗，从而可以分析哪些自动化任务最消耗资源。

历史上，Claude Code 使用过诸如 `/stats`（用于活动模式和模型使用频率）和 `/cost` 之类的独立命令。
这些命令现已统一为 `/usage` 命令，作为总成本、会话时长、所做更改数量以及跨不同模型的用量模式的单一参考点。

#### 子智能体与 Token 消耗

当 Claude Code 进入规划阶段或处理复杂任务时，它可能会生成子智能体。
这些子智能体在各自的上下文窗口中运作，独立消耗 token。
由于同时管理着多个上下文，这种行为可能会显著影响一次操作的总成本。

#### 策略性模型切换

为了平衡性能和成本，开发者应该采用分层的模型策略。
例如，可以在最初的规划阶段使用像 Opus 这样的高推理模型来识别改进机会。
一旦生成了计划，用户就可以切换到像 Sonnet 这样更具成本效益的模型来实现这些更改。
这种切换确保了最昂贵的资源被保留用于复杂推理，而标准的编码任务则由更经济的模型处理。

## 5. Quiz

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958800/) · 5:00

没有该课时的文档 - 请直接观看。

## 6. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958801/) · 3:00

### 摘要

本课时总结了 Claude Code 中 AI 模型和运营成本的策略性管理。
它强调了在用于复杂推理的高性能模型，与用于标准编码任务的更快、更具成本效益的模型之间切换的能力，同时也利用了"推理强度（effort）"设置来控制模型分析的深度。

### 关键概念

- 模型选择：在能力与复杂度、成本与延迟之间取得平衡。
- 推理强度旋钮：为特定任务调整模型推理的强度。
- 成本监控：使用 `/usage` 命令追踪 token 消耗和金钱开支。
- 工作流程优化：根据当前任务（例如规划与实现）在会话中动态切换模型。

### 课程笔记

Claude Code 提供了对若干个模型家族的访问，每个家族在能力、速度和成本之间提供不同的平衡。
虽然强大的模型能够处理高度复杂的任务和精细的设计模式，但它们通常成本更高，生成响应所需的时间也更长。
相反，对于不那么需要高层次推理的直接实现任务，可以使用更快、更便宜的模型。

除了模型选择之外，"推理强度（effort）"设置还充当了一个可以在对话中途调整的旋钮。
提高推理强度使模型能够更深入地思考边界情况和架构模式，而降低推理强度则能保持交互的快速和简洁。
区分"推理强度"和"规划"很重要，因为它们代表了模型处理过程中的不同维度。

为了对一次开发会话保持财务上的把控，`/usage` 命令提供了关于 token 消耗和相关成本的实时数据。
在决定是坚持使用高性能模型还是切换到更经济的替代方案时，这种透明度至关重要。

一种推荐的工作流程是将不同的模型视为专门的队友：

1. **规划**：使用一个强大的模型（例如 Claude 3 Opus）来设计系统并理解复杂的需求。
2. **实现**：切换到一个更快、更便宜的模型来完成大部分的代码编写工作。
3. **优化与调试**：当遇到复杂逻辑、精细的算法或顽固的 bug 时，切换回一个强大的模型，或提高推理强度设置。

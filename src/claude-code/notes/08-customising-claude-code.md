# Customising Claude Code

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 8
> 6 lessons · ~32 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958808/) | 1:10 | [↓](#1-introduction) |
| 2 | [Subagents: Specialists for Specific Tasks](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/) | 10:10 | [↓](#2-subagents-specialists-for-specific-tasks) |
| 3 | [Skills: Packaged Capabilities](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/) | 5:52 | [↓](#3-skills-packaged-capabilities) |
| 4 | [MCPs: Connecting Claude to External Systems](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/) | 7:50 | [↓](#4-mcps-connecting-claude-to-external-systems) |
| 5 | [Quiz](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958812/) | 5:00 | [↓](#5-quiz) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958813/) | 1:29 | [↓](#6-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958808/) · 1:10

### 摘要

Claude Code 被设计为可扩展、可定制，以适应特定的开发工作流程。
本课时介绍了用于定制的核心要素，包括子智能体、技能（skills）和模型上下文协议（MCP）服务器，为根据项目特定需求调整该工具的行为奠定了基础。

### 关键概念

* **可扩展性**：使 Claude Code 适应项目的具体现实和约束。
* **子智能体（Sub-agents）**：在 `.claude/agents/` 中为诸如 API 文档编写之类的特定领域定义的专门角色。
* **技能（Skills）**：存储在 `.claude/skills/` 中，可复用的品牌指南或领域知识。
* **MCP 服务器**：通过模型上下文协议集成外部工具（例如 Playwright）。
* **权限（Permissions）**：通过 `.claude/settings.json` 对工具访问和文件可见性进行精细控制。

### 课程笔记

Claude Code 开箱即用地提供了一整套强大的功能，但它也能够被扩展和定制，以适应项目的具体现实情况。
这种定制使开发者能够通过使用诸如子智能体、技能和 MCP 服务器之类的关键要素，来实现并调整自己的工作流程。

#### 子智能体

子智能体是为特定任务（例如 API 文档编写）设计的专门角色。
它们被赋予了具体的使命、核心操作原则和一套专用的工具集。
例如，`openapi-rest-documenter` 智能体被配置为使用一套特定的工具和最佳实践，来处理 REST API 文档编写任务。

```markdown
---
name: "openapi-rest-documenter"
description: "Use this agent when you need to create, update, or review API documentation—especially REST APIs described with OpenAPI/Swagger specifications. This includes authoring OpenAPI definitions, documenting newly added or changed endpoints, improving existing schemas, descriptions, and examples, and auditing specs against OpenAPI best practices."
tools: Read, TaskCreate, TaskGet, TaskList, TaskStop, TaskUpdate, WebFetch, WebSearch, Edit, NotebookEdit, Write
model: sonnet
color: purple
---

You are an elite API documentation specialist with deep expertise in REST API design and the OpenAPI Specification (OAS 3.0.x and 3.1.x).
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958808/?t=18)

#### 技能（Skills）

技能允许将特定领域的知识或样式规则（例如品牌指南）封装起来，供模型在不同任务中应用。
`dometrain-branding` 技能定义了色板、排版和 CSS 模式，用于保持与 Dometrain 品牌的视觉一致性。

```markdown
---
name: dometrain-branding
description: Apply Dometrain's brand colors, typography, and styling to UI, CSS, components, slides, diagrams, or any visual asset.
---

# Dometrain Branding

Dometrain's brand is a **dark, modern, developer-focused** look: deep navy backgrounds, a signature
purple→pink→peach gradient for accents, and clean Poppins typography.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958808/?t=18)

#### MCP 服务器

模型上下文协议（MCP）使 Claude Code 能够集成外部工具和服务，这些工具和服务在 `.mcp.json` 文件中配置。
例如，可以添加一个 Playwright MCP 服务器来提供浏览器自动化能力。

```json
{
  "mcpServers": {
    "playwright": {
      "type": "stdio",
      "command": "npx",
      "args": ["@playwright/mcp@latest"],
      "env": {}
    }
  }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958808/?t=18)

#### 配置与权限

定制还包括定义工具权限和文件可见性。
`.claude/settings.json` 文件可用于限制模型对诸如环境配置这样的敏感文件的访问。

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
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958808/?t=13)

本章的目标是概述这些不同组件在底层是如何运作的，以及可以在哪些场景中应用它们，来定制你自己的工作流程。

## 2. Subagents: Specialists for Specific Tasks

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/) · 10:10

子智能体（Subagents）是 Claude 可以将工作委派给的专门助手，它们在各自独立的上下文窗口中自主运作。
这种隔离防止了主对话线程被诸如安全审计或架构审查之类专门任务所需的大量文件读取和工具执行所"污染"。
通过将这些操作转移到一个独立的进程中，主线程保持了一个更干净的上下文窗口，只在子智能体完成任务后接收一份简洁的摘要。

### 关键概念

- **上下文隔离**：子智能体在一个独立的上下文窗口中运作，防止主线程被中间的工具输出和文件读取所淹没。
- **自主委派**：主 Claude 实例会识别一个任务是否匹配某个子智能体的专长，并自动将该工作委派出去。
- **定制化**：用户可以创建针对项目的或全局的个人智能体，配备量身定制的系统提示、工具访问权限和模型选择。
- **基于 Markdown 的配置**：子智能体的定义以 Markdown 文件的形式存储，包含用于元数据的 YAML frontmatter 和用于行为的系统提示。
- **并行化**：多个子智能体可以同时运行，从而实现批量任务的更快执行。

### 课程笔记

当一个子智能体被调用时，它会接收两项主要输入：一份定义其行为的专门系统提示，以及一份具体的任务描述。
子智能体使用它自己的上下文和工具来执行该任务。
完成后，它会将其发现或行动的摘要返回给主线程，然后该子智能体进程就会被销毁。
这种架构支持并行化，因为多个子智能体可以同时处理不同的任务，而用户可以继续与主 Claude 实例交互。

Claude Code 包含若干内置智能体，例如用于代码库导航的 `Explorer`，以及用于任务编排的 `Plan`。
用户也可以使用 `/agents` 命令创建自定义智能体。

```plaintext
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> /agents

Create new agent
Creation method

> 1. Generate with Claude (recommended)
  2. Manual configuration

↑/↓ to navigate · Enter to select · Esc to go back
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/?t=370)

自定义智能体可以是针对特定项目的（存储在代码仓库中），也可以是个人化的（存储在用户配置文件中）。
在创建过程中，Claude 可以根据对其预期用途的描述，帮助生成智能体的定义。

```plaintext
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> /agents

Create new agent
Describe what this agent should do and when it should be used (be comprehensive for best results)

The agent should be a specialist on documenting APIs, especially REST APIs with
OpenAPI. It should follow standards and best practices.

Enter to submit · ctrl+g to open in editor · Esc to go back
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/?t=385)

配置过程包括定义工具访问权限（例如只读、编辑或执行工具）、选择底层模型（Sonnet、Opus 或 Haiku），以及为可视化追踪分配一个 UI 颜色。

```plaintext
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> /agents

Create new agent
Select tools


[ Continue ]
------------------------------------------------------------
  ◻ All tools
> ◻ Read-only tools
  ◻ Edit tools
  ◻ Execution tools
  ◻ MCP tools
  ◻ Other tools
------------------------------------------------------------
[ Show advanced options ]

0 of 43 tools selected

Enter to toggle selection · ↑/↓ to navigate · Esc to go back
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/?t=415)

选择特定的模型可以实现成本和性能的优化；例如，Haiku 可以用于简单的任务，而 Opus 或 Sonnet 则可以为复杂推理保留。

```plaintext
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> /agents

Create new agent
Select model

Model determines the agent's reasoning capabilities and speed.

> 1. Sonnet ✓           Efficient for routine tasks
  2. Opus               Best for everyday, complex tasks
  3. Haiku              Fastest for quick answers
  4. Inherit from parent Use the same model as the main conversation

↑/↓ to navigate · Enter to select · Esc to go back
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/?t=430)

分配一个颜色有助于在 Claude Code 的状态栏中区分子智能体的活动。

```plaintext
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> /agents

Create new agent
Choose background color

  Automatic color
> ■ Red
  ■ Blue
  ■ Green
  ■ Yellow
  ■ Purple
  ■ Orange
  ■ Pink
  ■ Cyan


Preview: @openapi-rest-documenter

↑/↓ to navigate · Enter to select · Esc to go back
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/?t=460)

子智能体是位于 `.claude/agents/` 中的 Markdown 文件。
该文件使用 YAML frontmatter 来定义智能体的名称、描述、工具、模型和颜色。
`name` 和 `description` 字段至关重要，因为它们会被注入到主上下文窗口中，使主 Claude 实例能够判断何时应主动将任务委派给这个特定的子智能体。

```markdown
---
name: "openapi-rest-documenter"
description: "Use this agent when you need to create, update, or review API documentation—especially REST APIs described with OpenAPI/Swagger specifications. This includes authoring OpenAPI definitions, documenting newly added or changed endpoints, improving existing schemas, descriptions, and examples, and auditing specs against OpenAPI best practices.\n\n<example>\nContext: The user just added a new endpoint to the Expense Tracker API and wants it documented.\nuser: \"I just added a POST /api/expenses endpoint with an amount, description, and date. Can you document it?\"\nassistant: \"I'm going to use the Agent tool to launch the openapi-rest-documenter agent to produce an OpenAPI definition for the new endpoint.\"\n<commentary>\nThe user is asking for REST API documentation of a newly added endpoint, so use the openapi-rest-documenter agent to author the OpenAPI spec fragment.\n</commentary>\n</example>\n\n<example>\nContext: A logical chunk of API code has just been written that adds new routes.\nuser: \"Please add a route group for GET /api/expenses and GET /api/expenses/{id}.\"\nassistant: \"Here are the route group and handlers: \"\n<function call omitted for brevity only for this example>\n<commentary>\nNew endpoints were added, so proactively use the openapi-rest-documenter agent to document them in the OpenAPI spec.\n</commentary>\nassistant: \"Now let me use the openapi-rest-documenter agent to document these new endpoints in OpenAPI.\"\n</example>\n\n<example>\nContext: The user wants an existing spec reviewed for quality.\nuser: \"Can you review our openapi.yaml and tell me what's missing or wrong?\"\nassistant: \"I'll use the Agent tool to launch the openapi-rest-documenter agent to audit the spec against OpenAPI best practices.\"\n<commentary>\nThe request is an OpenAPI quality review, which is exactly this agent's specialty.\n</commentary>\n</example>"
tools: Read, TaskCreate, TaskGet, TaskList, TaskStop, TaskUpdate, WebFetch, WebSearch, Edit, NotebookEdit, Write
model: sonnet
color: purple
---

You are an elite API documentation specialist with deep expertise in REST API design and the OpenAPI Specification (OAS 3.0.x and 3.1.x). You have authored and audited hundreds of production API specs, and you know the difference between documentation that merely exists and documentation that developers actually trust and use.

## Your Mission

You produce and improve API documentation—primarily OpenAPI definitions—that is accurate, complete, idiomatic, and aligned with REST and OpenAPI best practices. Unless the user explicitly says otherwise, assume you are documenting recently added or changed endpoints, not re-documenting the entire API surface.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/subagents-specialists-for-specific-tasks-69958809/?t=490)

这个 Markdown 文件的正文充当了子智能体的系统提示。
此外，子智能体可以利用一套持久化的、基于文件的内存系统（存储在 `.claude/agent-memory/` 中），跨不同对话维持上下文，例如用户偏好、项目特定规则和参考指针。
这确保了子智能体能够提供一致的指导，而无需用户在每一次会话中重复指令。

## 3. Skills: Packaged Capabilities

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/) · 5:52

Claude Code 的技能（Skills）功能允许开发者将重复性任务、特定领域的知识或自定义逻辑打包成可复用的模块。
一个技能由一个包含必需的 `SKILL.md` 文件的目录定义，该文件带有用于发现和调用的元数据，并且可以包含额外的资源或可执行脚本。
与子智能体不同，技能在当前对话的上下文窗口内运作，这使它们非常适合应用品牌指南、运行特定的测试套件，或自动化常见的开发工作流程。

### 关键概念

- **技能结构**：一个包含必需的 `SKILL.md` 文件的目录。
- **元数据**：使用 YAML frontmatter，其中 `name` 用于调用，`description` 用于发现。
- **范围**：技能可以是个人的（全局的），也可以是特定于项目的（本地的）。
- **执行上下文**：技能在当前对话的上下文窗口内运行。
- **可扩展性**：可以包含 Markdown 资源和可执行脚本（例如 Python）。

### 课程笔记

诸如解释文件命名约定、函数结构、测试执行顺序或品牌指南之类的重复性任务，都是技能的理想候选。
一个技能将这些知识打包到一个小文件夹中，供 Claude 按需访问。
要查看当前可用的技能，请使用 `/skills` 命令。

Claude 可以通过获取外部数据来协助创建技能。
例如，为了给 Dometrain 创建一个品牌技能，Claude 可以抓取该网站以提取 CSS、色板和样式。

```bash
curl -sL https://dometrain.com -A "Mozilla/5.0" | grep -oiE 'href="[^"]+\.css[^"]*|<style[^>]*>' | head -40; echo "===INLINE COLORS==="; curl -sL https://domet...
for f in cfdbfbef3882462c 09b520d2972b3e18 741cdca36c2bfbf4; do curl -sL "https://dometrain.com/_next/sta "Mozilla/5.0"; done > /tmp/dometrain...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/?t=95)

技能可以存储在两个位置：用户的个人配置文件夹，或项目内部。
个人技能可在该机器上的所有项目中使用，而特定于项目的技能则存储在 `.claude/skills` 目录中，可以被提交到版本控制中以与团队共享。

```bash
Bash(mkdir -p /Users/guilhermeferreira/GitHub/dometrain-claude-code-getting-started/section-08/end/.claude/skills
mv ~/.claude/skills/dometrain-branding /Users/guilh...)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/?t=145)

一个技能由一个包含 `SKILL.md` 文件的目录定义。
该文件必须包含 YAML frontmatter，其中带有一个 `name`（用于手动调用）和一个 `description`（供 Claude 用于自然语言发现）。
文件的正文包含 Claude 执行该任务所需的指令、指南和数据。

```markdown
---
name: dometrain-branding
description: Apply Dometrain's brand colors, typography, and styling to UI, CSS, components, slides, diagrams, or any visual asset. Use whenever asked to "make it look like Dometrain", "apply our/Dometrain branding", "use the brand colors", style a page/component on-brand, or build a Dometrain-styled visual.
---

# Dometrain Branding

Dometrain's brand is a **dark, modern, developer-focused** look: deep navy backgrounds, a signature
purple→pink→peach gradient for accents, and clean Poppins typography. When applying branding, default to
the **dark theme** — it is the primary surface across dometrain.com.

## Color palette

### Brand accents (the three signature colors)
| Token       | Hex       | Use                                                        |
| ----------- | --------- | ---------------------------------------------------------- |
| Purple      | `#756AF6` | Primary brand color — links, primary buttons, focus rings  |
| Pink        | `#CC7DDA` | Secondary accent, gradient midpoint                        |
| Peach       | `#F6BE85` | Tertiary accent, gradient endpoint, highlights             |

### Surfaces (dark theme — primary)
| Token         | Hex       | Use                                            |
| ------------- | --------- | ----------------------------------------------- |
| Background    | `#0D1130` | Page background (deep navy)                    |
| Surface       | `#1A1D3A` | Cards, panels, raised surfaces                 |
| Surface alt   | `#262C57` | Borders, dividers, hover states on surfaces    |
| Surface deep  | `#221F41` | Insets, code blocks, wells                     |

### Text
| Token         | Hex       | Use                                            |
| ------------- | --------- | ----------------------------------------------- |
| Text primary  | `#FFFFFF` | Headings and primary copy on dark              |
| Text body     | `#C2CEED` | Body text on dark (soft lavender-blue)         |
| Text muted    | `#8E94B8` | Secondary/muted text, captions                 |
| Text subtle   | `#8B99C8` | Disabled / least-emphasis text                 |

### Functional / brand-adjacent
| Token        | Hex       | Use                                             |
| ------------ | --------- | ----------------------------------------------- |
| .NET purple  | `#512BD4` | When referencing .NET specifically              |
| Success      | `#10B981` | Success / confirmation states                   |
| Error        | `#FF6B6B` | Errors / destructive actions                    |

## The signature gradient

This purple→pink→peach gradient is Dometrain's most recognizable visual element. Use it for hero text,
key headings, primary CTAs, underlines, and decorative accents — sparingly, as a highlight.

```css
/* Canonical 3-stop brand gradient */
background: linear-gradient(135deg, #756AF6, #CC7DDA 50%, #F6BE85);

/* Diagonal hero variant (as used on dometrain.com) */
background: linear-gradient(87.03deg, #756AF6 16.38%, #CC7DDA 57.23%, #F6BE85 97.64%);

/* 2-stop purple→pink for smaller accents / buttons */
background: linear-gradient(90deg, #756AF6, #CC7DDA);
```

Gradient text:
```css
.brand-gradient-text {
  background: linear-gradient(135deg, #756AF6, #CC7DDA 50%, #F6BE85);
  -webkit-background-clip: text;
  background-clip: text;
  color: transparent;
}
```

## Typography

- **UI / headings / body:** `Poppins, sans-serif`
- **Code / monospace:** `JetBrains Mono, monospace`

```css
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700&family=JetBrains+Mono:wght@400;500&display=swap');
```

Headings lean bold (600–700); body text is regular (400) in `#C2CEED` on dark surfaces.

## Drop-in CSS variables

When styling a page or component, define these custom properties and reference them rather than hardcoding hex
values:

```css
:root {
  /* Brand accents */
  --dt-purple: #756AF6;
  --dt-pink:   #CC7DDA;
  --dt-peach:  #F6BE85;

  /* Surfaces (dark) */
  --dt-bg:           #0D1130;
  --dt-surface:      #1A1D3A;
  --dt-surface-alt:  #262C57;
  --dt-surface-deep: #221F41;

  /* Text */
  --dt-text:        #FFFFFF;
  --dt-text-body:   #C2CEED;
  --dt-text-muted:  #8E94B8;

  /* Functional */
  --dt-success: #10B981;
  --dt-error:   #FF6B6B;

  /* Signature gradient */
  --dt-gradient: linear-gradient(135deg, #756AF6, #CC7DDA 50%, #F6BE85);

  /* Type */
  --dt-font-sans: 'Poppins', sans-serif;
  --dt-font-mono: 'JetBrains Mono', monospace;
}
```

## Component patterns

```css
body {
  background: var(--dt-bg);
  color: var(--dt-text-body);
  font-family: var(--dt-font-sans);
}

/* Primary CTA */
.dt-btn-primary {
  background: var(--dt-gradient);
  color: #fff;
  border: none;
  border-radius: 10px;
  padding: 0.75rem 1.5rem;
  font-family: var(--dt-font-sans);
  font-weight: 600;
  cursor: pointer;
}

/* Secondary / outline button */
.dt-btn-secondary {
  background: transparent;
  color: var(--dt-purple);
  border: 1px solid var(--dt-purple);
  border-radius: 10px;
  padding: 0.75rem 1.5rem;
}

/* Card */
.dt-card {
  background: var(--dt-surface);
  border: 1px solid var(--dt-surface-alt);
  border-radius: 12px;
  padding: 1.5rem;
}

/* Links */
a { color: var(--dt-purple); }
a:hover { border-color: rgba(117, 106, 246, 0.3); }

/* Code */
code, pre {
  font-family: var(--dt-font-mono);
  background: var(--dt-surface-deep);
}
```

## Guidelines

- **Default to dark theme** (`#0D1130` background). It's the primary brand surface.
- **Use the gradient as an accent, not a fill** — hero headings, primary CTAs, underlines, small decorative
  elements. Don't paint large background areas with it.
- **Body text is `#C2CEED`, not pure white** on dark surfaces; reserve `#FFFFFF` for headings/emphasis.
- **Generous rounding** (10–12px radii) and ample padding match the modern, friendly feel.
- **Honor existing tokens.** If the target project already defines CSS variables or a theme, map these brand
  values onto those tokens instead of introducing a parallel system.
- **Don't invent colors.** Stick to this palette; if a needed shade is missing, derive it from the nearest
  brand color (e.g. tint/shade of `#756AF6`) rather than picking an arbitrary hex.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/?t=160)

技能还可以包含资源文件（如额外的 Markdown）或可执行脚本（例如 Python），Claude 可以根据 `SKILL.md` 中的指令运行这些脚本。
与子智能体不同，技能在当前对话的上下文窗口内运作。
它们可以通过名称直接调用（类似于斜杠命令），也可以由 Claude 根据技能的描述通过自然语言发现。

```shell
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> Let's apply Dometrain's brand colors to the Web app

* Spelunking...
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/?t=250)

当一个技能被触发时，Claude 会将该技能的元数据和指令加载到上下文窗口中，以执行所请求的任务。

```shell
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> Let's apply Dometrain's brand colors to the Web app

● I'll use the Dometrain branding skill for this - it has the exact brand colors and styling guidance.

● Skill(dometrain-branding)
  └ Successfully loaded skill

● Let me look at the web app's current styling structure before applying the brand.

  Searched for 1 pattern (ctrl+o to expand)

● I'll read all the CSS files and the component files to understand the current styling system.

● Reading 3 files... (ctrl+o to expand)
  └ web/src/components/ExpenseForm.module.css

· Spelunking... (18s · 571 tokens)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/?t=295)

一旦该技能被加载，Claude 就可以将打包的知识应用到项目文件中，例如将现有样式映射到品牌的 CSS 变量上，并更新组件模块。

```shell
Claude Code v2.1.181
Opus 4.8 with high effort · Claude Max
~/GitHub/dometrain-claude-code-getting-started/section-08/end

1 setup issue: MCP · /doctor

> /clear

> Let's apply Dometrain's brand colors to the Web app

● I'll use the Dometrain branding skill for this - it has the exact brand colors and styling guidance.

● Skill(dometrain-branding)
  └ Successfully loaded skill

● Let me look at the web app's current styling structure before applying the brand.

  Searched for 1 pattern (ctrl+o to expand)

● I'll read all the CSS files and the component files to understand the current styling system.

  Read 6 files (ctrl+o to expand)

● The current design is a light theme with indigo accents, using CSS modules but no shared variables. I'll introduce
  tokens in index.css and map every surface/accent onto them. Let me update all five files.

· Spelunking... (30s · 1.5k tokens)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/skills-packaged-capabilities-69958810/?t=305)

## 4. MCPs: Connecting Claude to External Systems

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/) · 7:50

### 摘要

模型上下文协议（MCP）是一个开放标准，使 Claude Code 能够与外部系统、API 和本地可执行文件进行交互。
通过集成 MCP 服务器，开发者可以扩展 Claude 的能力，将诸如项目追踪、通过 Playwright 进行浏览器自动化，或与 Figma 之类的第三方服务交互等任务纳入其中。
这些服务器既可以在用户级别进行全局配置，也可以通过 `.mcp.json` 文件将范围限定在特定项目内，使团队能够在一个代码仓库中共享工具集，同时管理上下文窗口的效率。

### 关键概念

*   **模型上下文协议（MCP）**：一个用于将 LLM 连接到外部数据和工具的开放标准。
*   **传输类型**：MCP 服务器通过 `stdio`（本地进程）或 `http`（远程托管服务）运作。
*   **工具发现**：`/mcp` 命令允许用户查看、启用或禁用可用的工具。
*   **配置范围**：MCP 可以在用户配置文件中全局配置，也可以通过代码仓库中的 `.mcp.json` 文件进行本地配置。
*   **上下文管理**：MCP 工具会消耗上下文窗口空间；禁用不需要的服务器，或使用 CLI，可以提高效率。

### 课程笔记

Claude Code 在一个智能体循环内运作，调用工具来执行操作。
虽然许多工具是内置的，但 MCP 服务器允许你将这些能力扩展到外部系统，例如 Jira、GitHub，或诸如 Playwright 之类的专门自动化工具。

要查看当前活动的 MCP 服务器及其可用工具，可以在 Claude Code 会话中使用 `/mcp` 命令。

```shell
/mcp

Manage MCP servers
5 servers

  claude.ai
  claude.ai Google Calendar · ✓ connected · 8 tools
  claude.ai Google Drive · ✓ connected · 8 tools
> → Show unused connectors (1)

  Built-in MCPs (always available)
  claude_design · ✘ failed
  computer-use · ○ disabled
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/?t=100)

#### 安装 MCP 服务器

你可以直接从终端添加新的 MCP 服务器。
例如，要添加 Playwright 用于浏览器自动化，请使用 `claude mcp add` 命令。
这个命令通常需要指定一个名称和执行命令（通常使用 `npx` 来运行基于 Node 的工具）。

```bash
claude mcp add playwright npx @playwright/mcp@latest
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/?t=130)

MCP 服务器通常使用两种传输协议之一：
1.  **stdio（标准输入/输出）**：智能体在你的机器上启动一个本地进程，并直接与之交互。虽然该进程在本地运行，但它仍然可以向外部 API 发出网络请求。
2.  **HTTP**：智能体连接到一个托管在网络上的远程 MCP 服务器。

#### 使用 MCP 工具

一旦安装了一个 MCP 服务器，它的工具就会对智能体可用。
你可以通过 `/mcp` 界面检查这些工具，查看诸如导航 URL、点击元素或调整窗口大小之类的具体能力。

```bash
Tools for playwright
23 tools

  1.  Close browser               destructive, open-world
> 2.  Resize browser window       destructive, open-world
  3.  Get console messages        read-only, open-world
  4.  Handle a dialog             destructive, open-world
↓ 5.  Evaluate JavaScript         destructive, open-world
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/?t=205)

启用这些工具后，你可以提供高层次的自然语言指令。
Claude 会判断需要哪些 MCP 工具来完成请求，例如启动一个本地服务器，然后使用 Playwright 与 UI 交互。

```bash
> Start the API and the web application, then navigate through the browser to the home page and create a new expense of $1000 for a new laptop.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/?t=230)

#### 上下文与配置

每一个活动的 MCP 工具都会被添加到智能体的上下文窗口中。
为了保持效率，建议在不需要时使用 `/mcp` 命令禁用 MCP 服务器。
在某些情况下，使用标准的 CLI 工具比使用完整的 MCP 服务器更节省上下文。

MCP 配置可以通过在代码仓库根目录创建一个 `.mcp.json` 文件与团队共享。
当团队成员在该代码仓库中启动 Claude Code 时，他们会被提示启用特定于项目的 MCP 服务器。

```json
{
  "mcpServers": {
    "playwright": {
      "type": "stdio",
      "command": "npx",
      "args": ["@playwright/mcp@latest"],
      "env": {}
    }
  }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/mcps-connecting-claude-to-external-systems-69958811/?t=415)

## 5. Quiz

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/quiz-69958812/) · 5:00

没有该课时的文档 - 请直接观看。

## 6. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958813/) · 1:29

### 摘要

本课时探讨了从 Claude Code 的默认设置过渡到高级定制的过程，强调了使用子智能体来处理专门任务，以及使用智能体技能来定义特定的工作流程和偏好设置。
它涵盖了通过模型上下文协议（MCP）集成外部数据源，并提供了用于扩展这些能力的关键资源，例如 skills.md 文件、Anthropic 提供实现指导的官方仓库，以及用于发现与常见开发工具连接的公共 MCP 服务器的 mcpservers.org。

### 关键概念

- 子智能体
- 智能体技能
- 定制化
- 外部数据集成
- 工作流程自动化
- 模型上下文协议（MCP）
- skills.md
- Anthropic Skills 仓库
- mcpservers.org

### 课程笔记

#### 定制 Claude Code

Claude Code 可以被定制，超越默认的"开箱即用"功能，使其能够更好地符合特定的项目需求和开发者偏好。

#### 子智能体

对于专门的任务，Claude Code 支持使用子智能体。
它们在环境中充当领域专家——或"专家"——处理诸如 API 文档编写或代码审计之类的具体职责。

```markdown
---
name: "openapi-rest-documenter"
description: "Use this agent when you need to create, update, or review API documentation—especially REST APIs described with OpenAPI/Swagger specifications. This includes authoring OpenAPI definitions, documenting newly added or changed endpoints, improving existing schemas, descriptions, and examples, and auditing specs against OpenAPI best practices."
tools: Read, TaskCreate, TaskGet, TaskList, TaskStop, TaskUpdate, WebFetch, WebSearch, Edit, NotebookEdit, Write
model: sonnet
color: purple
---
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958813/?t=16)

#### 智能体技能

智能体技能使你能够教会 Claude 特定的工作流程和偏好设置。
通过提供关于以偏好方式完成一项任务所需步骤的详细描述，你可以确保在诸如品牌塑造或 UI 样式设计等重复性活动中保持一致性。

```markdown
---
name: dometrain-branding
description: Apply Dometrain's brand colors, typography, and styling to UI, CSS, components, slides, diagrams, or any visual asset.
---

# Dometrain Branding

Dometrain's brand is a **dark, modern, developer-focused** look: deep navy backgrounds, a signature purple→pink→peach gradient for accents, and clean Poppins typography.
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958813/?t=22)

#### 外部数据集成

Claude Code 可以与外部系统集成以获取数据。
这是通过模型上下文协议（MCP）实现的，它将 Claude 连接到诸如用于浏览器自动化的 Playwright 等各种外部工具和数据源。

```json
{
  "mcpServers": {
    "playwright": {
      "type": "stdio",
      "command": "npx",
      "args": ["@playwright/mcp@latest"],
      "env": {}
    }
  }
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958813/?t=32)

#### 使用 MCP 扩展能力

通过集成 MCP（模型上下文协议）服务器，Claude Code 的功能可以得到显著扩展。
这些服务器使智能体能够与外部工具和数据源交互，为智能体的环境带来额外的能力。

#### 技能与 MCP 服务器资源

要查找技能示例或了解如何构建它们，有若干个公共资源可供参考：

*   **skills.md**：一个作为各种技能仓库的公共库。它提供了可以针对特定用例进行调整的实用示例。
*   **Anthropic Skills 仓库**：由 Anthropic 提供的官方仓库，开发者可以在其中查看可用的技能，并研究它们的构建方式。
*   **mcpservers.org**：一个维护公共 MCP 服务器列表的社区网站。这个目录可供搜索，使开发者能够找到与他们日常工作流程中使用的工具相集成的服务器。

这些资源为定制智能体提供了基础，更多高级实现细节将在专门的深度课程中涵盖。

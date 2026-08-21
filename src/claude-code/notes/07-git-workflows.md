# Git Workflows

> Course: [Getting Started: Claude Code](https://dometrain.com/course/getting-started-claude-code/) · Chapter 7
> 6 lessons · ~29 min
> Source: Dometrain。整理自各课时文档，每个小节均链接回原始课时。

## 课时索引

| # | Lesson | Length | Section |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958802/) | 1:21 | [↓](#1-introduction) |
| 2 | [Commits with Claude Code](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/) | 7:13 | [↓](#2-commits-with-claude-code) |
| 3 | [Creating Pull Requests](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/) | 3:58 | [↓](#3-creating-pull-requests) |
| 4 | [Reviewing Pull Requests](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/) | 6:02 | [↓](#4-reviewing-pull-requests) |
| 5 | [Worktrees and Parallel Sessions](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/) | 7:52 | [↓](#5-worktrees-and-parallel-sessions) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958807/) | 2:14 | [↓](#6-section-recap) |

## 1. Introduction

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/introduction-69958802/) · 1:21

### 摘要

本课时介绍了 Claude Code 的 Git 工作流程这一章节，聚焦于提交完成之后所发生的开发阶段。
它为探索高级的源代码控制任务奠定了基础，例如创建和审查拉取请求，以及使用 Git 工作树（worktree）管理并行开发。

### 关键概念

* **提交之后的生命周期**：从本地提交迈向协作性的工作流程。
* **拉取请求管理**：使用 Claude Code 在 GitHub 等平台上创建和审查 PR。
* **并行工作流程**：利用 Git 工作树同时管理多个功能或修复。
* **端到端自动化**：将 Claude Code 集成到从探索到最终审查的标准开发周期中。

### 课程笔记

使用 Claude Code 的标准开发工作流程遵循一条结构化的路径：探索、规划、实现（编码）、验证，最后是提交。
虽然 Claude Code 在执行这些本地任务方面非常有效，但它的实用性还延伸到了紧随最初提交之后的协作阶段。

本章探讨了如何利用 Claude Code 执行高级的源代码控制操作，特别是在 Git 和 GitHub 的背景下。
重点从单个编码任务转向了更广泛的开发周期，涵盖以下方面：

* **拉取请求**：使用 Claude Code 的分析能力，自动化拉取请求的创建，并执行代码审查。
* **并行开发**：使用 Git 工作树管理多条并行的工作流，使你能够在不打断当前环境的情况下，在不同任务之间无缝切换。

过渡到这些工作流程，始于一次提交完成之时，架起了本地开发与基于团队的协作之间的桥梁。

## 2. Commits with Claude Code

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/) · 7:13

### 摘要

Claude Code 通过自动化文件暂存，并根据实际所做的更改生成描述性的提交信息，简化了 Git 提交流程。
除了简单的自动化之外，它还支持一套内存系统，使开发者能够强制执行特定的标准，例如 Conventional Commits，从而在无需手动开销的情况下，确保整个项目的一致性。

### 关键概念

* **Shell 模式**：使用 `!` 前缀，直接在 Claude Code 界面内执行 Git 命令。
* **自动化暂存**：当被要求提交时，Claude 会识别已修改的文件并自动暂存它们。
* **具备上下文的提交信息**：基于代码分析而非通用信息，生成详细的提交描述。
* **持久化偏好**：使用 `#` 符号，将特定的提交风格（例如 Conventional Commits）保存到 Claude 的长期内存中。
* **内存管理**：通过 `/memory` 命令访问并审查已存储的偏好设置。

### 课程笔记

在标准的开发工作流程中，开发者经常需要在编辑器和终端之间切换来执行 Git 操作。
一种常见但不太理想的做法是使用通用的提交信息：

```shell
git commit -m "fix"
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=10)

Claude Code 允许你通过使用 shell 模式，始终留在上下文窗口内。
通过在命令前加上感叹号（`!`），你可以直接执行 shell 命令。
这些命令的输出会返回到上下文窗口中，使 Claude 能够了解代码仓库的当前状态。

```shell
! git commit -m "fix"
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=40)

#### 功能实现工作流程

当开始一项新功能时，例如添加一个月度总计端点，你可以使用 shell 模式来管理分支：

```shell
! git checkout -b feat/expense-totals
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=85)

一旦创建了分支，你可以提示 Claude 实现该功能。
在这个例子中，需求是添加一个 `GET /api/expenses/totals` 端点，返回按币种分组的月度总计，按最近的月份排序。

```csharp
private static async Task<IResult> GetTotalsAsync(
    ExpenseDbContext db,
    CancellationToken cancellationToken)
{
    var grouped = await db.Expenses
        .AsNoTracking()
        .GroupBy(e => new { e.Date.Year, e.Date.Month, e.Currency })
        .Select(g => new
        {
            g.Key.Year,
            g.Key.Month,
            g.Key.Currency,
            Total = g.Sum(e => e.Amount),
        })
        .OrderByDescending(g => g.Year)
        .ThenByDescending(g => g.Month)
        .ThenBy(g => g.Currency)
        .ToListAsync(cancellationToken);

    var totals = grouped
        .Select(g => new MonthlyCurrencyTotalResponse(
            $"{g.Year:D4}-{g.Month:D2}", g.Currency, g.Total))
        .ToList();

    return Results.Ok(totals);
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=130)

#### 自动化提交

你可以直接提示 Claude "提交这个（Commit this）"，而不是手动暂存文件并编写信息。
然后 Claude 会：
1. 运行 `git status`，识别已修改和未追踪的文件。
2. 使用 `git add` 暂存相关的更改。
3. 根据实际实现的逻辑，生成一条描述性的提交信息。

```bash
Commit this.

Bash(git status --short && echo "---" && git branch --show-current)
  M api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs
  M tests/ExpenseTracker.Tests/ExpenseApiTests.cs
 ?? api/ExpenseTracker.Api/Contracts/MonthlyCurrencyTotalResponse.cs

Bash(git add api/ExpenseTracker.Api/Contracts/MonthlyCurrencyTotalResponse.cs api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs tests/ExpenseTracker.Tests/Expense...)
  [feat/expense-totals 4037cbf] Add GET /api/expenses/totals endpoint
  3 files changed, 59 insertions(+)
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=175)

#### Conventional Commits 与内存

要强制执行特定的提交风格，例如 Conventional Commits，你可以使用 `#`（井号）符号将一条偏好设置保存到 Claude 的内存中。
当你提供这样的指令时，Claude 会将之前的提交修改以匹配该风格，并为该项目的未来提交记住这一偏好。

```bash
# use Conventional Commits, subject under 72 chars, explain the why in the body
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=295)

Claude 将这些偏好设置保存为 `.claude/memory` 目录中的 Markdown 文件。
你可以使用 `/memory` 命令查看或管理这些内存。

```markdown
---
name: feedback_commit_style
description: "Git commit message conventions - Conventional Commits, short subject, why in the body."
metadata:
  node_type: memory
  type: feedback
---

Write git commits using Conventional Commits (e.g. `feat:`, `fix:`, `refactor:`),
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/commits-with-claude-code-69958803/?t=355)

## 3. Creating Pull Requests

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/) · 3:58

### 摘要

Claude Code 通过与 GitHub CLI 集成，自动化分支推送、更改摘要和 PR 创建，简化了拉取请求流程。
通过提供自然语言指令，开发者可以提示 Claude 生成技术摘要、定义测试计划，并使用诸如 "Closes #3" 之类的关闭关键字来链接特定的 GitHub issue。
这一工作流程确保了拉取请求内容翔实、遵循项目约定，并在代码更改和项目管理任务之间保持清晰的关联。

### 关键概念

*   **GitHub CLI 集成**：Claude 使用 `gh` CLI 与 GitHub 代码仓库交互，进行身份验证和 PR 管理。
*   **自动化摘要**：Claude 可以分析当前分支与基础分支之间的差异，撰写详细的 PR 描述。
*   **Issue 关联**：在 PR 正文中使用诸如 "Closes #ID" 之类的关键字，可以在 GitHub 中实现自动化的项目管理。
*   **Conventional Commits**：通过使用结构化的提交信息（例如 `feat:`、`fix:`），保持一份整洁的历史记录，提升生成的 PR 的质量。
*   **测试计划纳入**：在 PR 描述中包含一份测试计划，为审查者提供验证实现的清晰步骤。

### 课程笔记

要使用 Claude Code 管理拉取请求，环境中必须安装并验证了 GitHub CLI。
你可以使用 `gh auth status` 命令来验证身份验证状态。

```bash
gh auth status
github.com
✓ Logged in to github.com account gsferreira (keyring)
- Active account: true
- Git operations protocol: https
- Token: gho_************************************
- Token scopes: 'gist', 'read:org', 'repo', 'workflow'
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/?t=25)

#### 实现该功能

这个工作流程涉及在打开拉取请求之前完成整个技术栈的任务。
在这个场景中，API 被更新，添加了一个新的端点，用于按币种分组的月度总计。
`ExpenseEndpoints.cs` 中的实现使用 Entity Framework Core，按年、月和币种对费用进行分组。

```csharp
private static async Task<IResult> GetTotalsAsync(
    ExpenseDbContext db,
    CancellationToken cancellationToken)
{
    var grouped = await db.Expenses
        .AsNoTracking()
        .GroupBy(e => new { e.Date.Year, e.Date.Month, e.Currency })
        .Select(g => new
        {
            g.Key.Year,
            g.Key.Month,
            g.Key.Currency,
            Total = g.Sum(e => e.Amount),
        })
        .OrderByDescending(g => g.Year)
        .ThenByDescending(g => g.Month)
        .ThenBy(g => g.Currency)
        .ToListAsync(cancellationToken);

    var totals = grouped
        .Select(g => new MonthlyCurrencyTotalResponse(
            $"{g.Year:D4}-{g.Month:D2}", g.Currency, g.Total))
        .ToList();

    return Results.Ok(totals);
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/?t=40)

API 准备就绪后，前端会被更新以消费这些数据。
`MonthlyTotals.tsx` 组件使用 React Query 从新的 `/api/expenses/totals` 端点获取数据，并渲染一个显示消费总计的面板。

```tsx
export function MonthlyTotals() {
  const query = useQuery({
    queryKey: ["expenses", "totals"],
    queryFn: ({ signal }) => listMonthlyTotals(signal),
  });

  let content;
  if (query.isPending) {
    content = <p className={styles.status}>Loading totals...</p>;
  } else if (query.isError) {
    const message = query.error instanceof Error ? query.error.message : "Unknown error";
    content = (
      <p className={`${styles.status} ${styles.error}`} role="alert">
        Could not load totals: {message}
      </p>
    );
  } else if (query.data.length === 0) {
    content = <p className={styles.status}>No totals yet.</p>;
  } else {
    content = (
      <ul className={styles.list}>
        {groupByMonth(query.data).map((group) => (
          <li key={group.month} className={styles.item}>
            <span className={styles.month}>{formatMonth(group.month)}</span>
            <span className={styles.amounts}>
              {group.entries.map((entry) => (
                <span key={entry.currency} className={styles.amount}>
                  {formatCurrency(entry.total, entry.currency)}
                </span>
              ))}
            </span>
          </li>
        ))}
      </ul>
    );
  }

  return (
    <section className={styles.panel} aria-label="Monthly totals">
      <h2 className={styles.title}>Monthly totals</h2>
      {content}
    </section>
  );
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/?t=85)

#### 提交与验证

一旦实现得到验证，更改应该使用 Conventional Commits 进行提交。
如果一次提交需要完善，以便在正文中包含理由说明，Claude 可以修改本地提交。

```bash
! git log --oneline -1
407ef96 feat: add monthly spend totals endpoint grouped by currency
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/?t=55)

在打开拉取请求之前，请验证分支历史，以确保所有相关的提交都已存在。

```bash
git log --oneline
e2c24fc feat: add monthly totals panel to the SPA
407ef96 feat: add monthly spend totals endpoint grouped by currency
6a4aa41 S07 start
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/?t=100)

#### 创建拉取请求

你可以通过用自然语言描述期望的输出来提示 Claude 创建拉取请求。
一个稳健的提示应该要求 Claude：
1. 总结这些更改。
2. 包含一份简短的测试计划。
3. 关联相关的 issue（例如"用关闭关键字关联 issue #3"）。

Claude 随后会推送分支到远程仓库，并执行 `gh pr create` 命令，附带生成的标题和正文。

```bash
gh pr create --base main --head feat/expense-totals --title "feat: monthly spend totals (API + SPA panel)" --body "$(cat <<'EOF'
  ## Summary
  ...
EOF
)"
https://github.com/gsferreira/dometrain-claude-code-getting-started/pull/4
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/creating-pull-requests-69958804/?t=175)

生成的拉取请求包含一份结构化的描述、API 和 SPA 更改的摘要、测试计划，以及一个在合并时会自动关闭相关 issue 的链接。

## 4. Reviewing Pull Requests

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/) · 6:02

Claude Code 提供了一整套工具，用于管理拉取请求（PR）的生命周期，从最初的审查到处理反馈、回复评论。
通过利用 GitHub CLI（`gh`），Claude 可以分析更改、获取讨论线程，并直接在本地环境中应用修复。

### 关键概念

- **GitHub Actions 集成**：使用 `install-github-app` 命令，通过 GitHub App 在 CI/CD 中自动化审查。
- **`/review` 命令**：触发对拉取请求差异和元数据的自动化分析。
- **上下文偏见**：智能体可能对它在同一会话中生成的代码过于乐观的风险。
- **子智能体**：生成一个具有全新上下文窗口的新智能体实例，以确保独立、无偏见的审查。
- **GitHub CLI 集成**：使用 `gh` 命令查看 PR 详情、差异和评论。
- **闭环处理**：读取 PR 反馈、实施修复并回复线程的整个流程。

### 课程笔记

#### 集成与设置

你可以将 Claude 集成到你的 CI/CD 流水线中，使用 GitHub Actions。
通过运行 `install-github-app` 斜杠命令，你可以引导你的账户并将其链接到你的代码仓库，使 Claude 能够对更改做出反应，并提供自动化反馈。
但是，对于手动审查，你可以使用基于终端的工作流程。

#### 通过终端进行审查

要开始审查一个 PR，你可以使用 `--pr` 参数加上具体的 PR ID（例如 `claude --pr 4`）来启动 Claude。
这会将 PR 的对话加载到上下文中。
要对更改进行技术分析，请使用 `/review` 命令。
Claude 会使用 GitHub CLI 来检查 PR 的元数据和差异。

```bash
/review

# Claude uses the GitHub CLI to inspect the PR
gh pr view 4 --json title,body,author,baseRefName,headRefName,state,additions,deletions,changedFiles,labels
gh pr diff 4
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/?t=125)

#### 处理反馈与应用修复

当审查者提供反馈时，你可以让 Claude 获取这些评论，以便你无需离开终端就能处理它们。

```bash
# Pulling PR comments to read feedback
gh pr view 4 --json comments --jq '.comments[] | "\( .author.login) [\(.createdAt)]:\n\(.body)\n---"'
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/?t=175)

在审查完评论之后，你可以指示 Claude "闭合循环"，应用建议的更改。
Claude 会实现该修复，通过运行构建或类型检查（如 `CLAUDE.md` 中定义的）来验证它，并提交更改。

```bash
# Verifying the fix with typechecks and builds
npm --prefix web run typecheck
npm --prefix web run build

# Committing the refactor
git add web/src/components/MonthlyTotals.tsx
git commit -m "refactor: sort monthly totals in the SPA instead of trusting API order"
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/?t=190)

修复被提交后，你可以回复 PR 线程，通知审查者他们的反馈已被处理。

```bash
# Replying to the PR comment via GitHub CLI
gh pr comment 4 --body "Addressed in fe073ed. groupByMonth now sorts months descending and currencies alphabetically itself."
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/?t=205)

#### 使用子智能体进行无偏见审查

如果当前的 Claude 会话被用于编写代码，智能体可能会存在偏见，忽略一些问题。
为了获得客观的审查，你可以要求 Claude 启动一个子智能体。
这会创建一个与之前对话无关的全新上下文窗口，使子智能体能够发现主智能体可能遗漏的问题。
这在审查复杂逻辑（例如 API 数据契约和分组逻辑）时特别有用。

```csharp
public sealed record MonthlyCurrencyTotalResponse(string Month, string Currency, decimal Total);

// Endpoint mapping in the API
group.MapPost("/", CreateAsync);
group.MapGet("/", ListAsync);
group.MapGet("/totals", GetTotalsAsync);
group.MapGet("/recent", ListRecentAsync);
group.MapGet("/{id:guid}", GetByIdAsync);

private static async Task<IResult> GetTotalsAsync(
    ExpenseDbContext db,
    CancellationToken cancellationToken)
{
    var grouped = await db.Expenses
        .AsNoTracking()
        .GroupBy(e => new { e.Date.Year, e.Date.Month, e.Currency })
        .Select(g => new
        {
            g.Key.Year,
            g.Key.Month,
            g.Key.Currency,
            Total = g.Sum(e => e.Amount)
        })
        .ToListAsync(cancellationToken);

    return Results.Ok(grouped);
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/reviewing-pull-requests-69958805/?t=235)

#### 最终确定 PR

Claude 简化了提交信息和 PR 描述的创建，但开发者仍然是最终的决策者。
你负责最终的代码审查状态，也负责在反馈循环完成后，在 GitHub 上点击合并按钮。

## 5. Worktrees and Parallel Sessions

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/) · 7:52

### 摘要

Git 工作树（worktree）允许开发者在不同的目录中维护同一个代码仓库的多个检出（checkout），共享相同的 Git 历史记录，但操作不同的分支。
这在与 Claude Code 这样的编码智能体协作时特别有用，因为它能够在不中断或暂存当前功能开发的情况下，实现并行任务的执行——例如执行代码审查或修复一个生产环境的 bug。
Claude Code 通过一个专用的标志来简化这一工作流程，该标志会自动管理工作树的创建和命名。

### 关键概念

- 上下文切换与暂存更改的对比。
- 用于并行检出同一代码仓库的 Git 工作树。
- Claude Code 的 `--worktree`（`-w`）标志，用于自动化会话管理。
- 同时管理多个分支和拉取请求。
- 使用 `list` 和 `remove` 命令管理工作树的生命周期。

### 课程笔记

上下文切换是软件开发中的一个常见挑战，尤其是在处理一项功能时突然出现紧急问题。
传统的解决方案包括暂存更改或维护同一代码仓库的多个克隆。
然而，Git 工作树提供了一种更高效的替代方案，允许在不同的磁盘位置对同一代码仓库进行多次检出，同时共享相同的 Git 历史记录。

在开始一项新任务之前，你可以验证你当前所在的分支，以确保你处于正确的上下文中：

```bash
! git branch --show-current
  feat/expense-totals
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=115)

要手动创建一个工作树，请使用 `git worktree add` 命令，指定目标目录和你希望检出的分支：

```bash
git worktree add ../expense-tracker-totals feat/expense-totals
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=130)

Claude Code 通过 `--worktree` 或 `-w` 标志简化了这一流程。
当以此标志启动时，Claude 会自动创建一个受管理的工作树，通常位于 `.claude/worktrees/` 目录中。

```bash
Claude Code v2.1.179
Opus 4.8 with high effort . Claude Max
~/.../.claude/worktrees/noble-exploring-pretzel
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=175)

在这个工作树内部，Claude 在一个新分支上运作，使你能够与原始会话并行地处理一个单独的任务，而不会产生冲突。

```bash
! git branch --show-current
  L worktree-noble-exploring-pretzel
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=190)

举例来说，当一个会话处理功能开发时，一个并行的工作树会话可以为 `ExpenseEndpoints.cs` 中的 `ListAsync` 端点实现一个未来日期验证的修复。

```csharp
var today = DateOnly.FromDateTime(DateTime.UtcNow);

if (fromDate is { } fromFuture && fromFuture > today)
{
    errors["from"] = ["from cannot be in the future."];
}

if (toDate is { } toFuture && toFuture > today)
{
    errors["to"] = ["to cannot be in the future."];
}
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=285)

一旦工作完成，你可以直接从工作树会话中提交更改，并将分支推送到远程仓库。

```bash
git add api/ExpenseTracker.Api/Endpoints/ExpenseEndpoints.cs tests/ExpenseTracker.Tests/ExpenseApiTests.cs
git commit -m "feat(api): reject future dates in expense list filter"
git push -u origin worktree-noble-exploring-pretzel
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=300)

然后可以为该工作树分支创建一个拉取请求，目标指向 main 分支，同时你的原始功能分支保持不变。

```bash
gh pr create --base main --head worktree-noble-exploring-pretzel --title "feat(api): reject future dates in expense list filter" --body "Implementation of future date validation for expense filtering."
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=310)

合并 PR 并退出会话后，你可以使用标准的 Git 命令来管理你的工作树。
要查看所有活动的工作树及其位置：

```bash
git worktree list
```
[▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/worktrees-and-parallel-sessions-69958806/?t=395)

要清理，你可以使用 `git worktree remove <path>` 移除一个工作树。
这确保了随着并行任务的增多，你的环境仍然保持可管理。

## 6. Section Recap

> [▶ Watch](https://dometrain.com/take/course/getting-started-claude-code-3256128/section-recap-69958807/) · 2:14

### 摘要

Claude Code 通过自动化拉取请求的创建、促成子智能体代码审查，以及与 GitHub CLI 集成以直接在终端中管理反馈，简化了 Git 工作流程。
它还通过 Git 工作树支持并行开发，使开发者能够在隔离的环境中处理多个任务，而无需进行上下文切换，同时保持开发者作为代码更改最终决策者的角色。

### 关键概念

- **Git 自动化**：简化提交和拉取请求的创建，包括自动化的 PR 描述。
- **子智能体代码审查**：能够启动子智能体来审查你自己的代码，或协助审查同事的工作。
- **GitHub CLI 集成**：直接在 Claude Code 会话中拉取 PR 评论并对反馈做出反应，避免基于浏览器的上下文切换。
- **并行工作流程**：使用 Git 工作树在隔离环境中处理多个功能或修复，而无需暂存或搁置当前进度。
- **人在回路中**：开发者仍然负责审查差异并验证所有 AI 生成的更改。

### 课程笔记

Claude Code 通过自动化工作流程中更具描述性和管理性的部分，增强了标准的 Git 操作。
虽然像 `git status` 这样的简单命令可能不需要 AI 辅助，但 Claude Code 在生成详细的拉取请求描述和管理提交流程方面非常有效。

#### 代码审查与协作

Claude Code 可以通过启动一个子智能体来执行代码审查。
这个子智能体可以分析你当前的更改，或拉取同事分支的代码，提供第二双眼睛的审视。
通过利用本地机器上安装的 GitHub CLI，Claude Code 可以将 PR 评论和反馈直接拉入终端。
这使得开发者能够对评论做出反应并实现所请求的更改，而无需在代码编辑器和网页浏览器之间来回切换。

#### 使用工作树进行并行开发

保持工作势头的最强大功能之一，是对 Git 工作树的支持。
你不必为了处理紧急的 bug 修复而暂存当前的工作，而是可以在一个单独的工作树中启动一个新的 Claude Code 会话。
这使你能够在隔离环境中处理多个任务，甚至可以同时打开多个拉取请求，而不会让一项任务影响另一项任务。

#### 责任与验证

尽管提供了自动化，开发者仍必须保持"人在回路中（human in the loop）"的角色。
你最终要对被提交的代码负责。
审查 Claude Code 提供的差异（diff）以理解更改并确保它们在技术上是合理的，这一点至关重要。
Claude Code 作为一个工具，用于协助这一过程并识别你可能遗漏的问题，但最终的决定权在于用户。

# Introduction to integration testing

> 课程:[From Zero to Hero: Integration testing in ASP.NET Core](https://dometrain.com/course/from-zero-to-hero-integration-testing-in-asp-net-core/) · 第 2 章
> 共 5 课 · 约 19 分钟
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The types of testing](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-types-of-testing-53953782/) | 8:18 | [↓](#1-the-types-of-testing) |
| 2 | [The testing pyramid](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-testing-pyramid-53953783/) | 3:02 | [↓](#2-the-testing-pyramid) |
| 3 | [Why are integration tests so important?](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-are-integration-tests-so-important-53953784/) | 2:19 | [↓](#3-why-are-integration-tests-so-important) |
| 4 | [Why should you write integration tests?](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-should-you-write-integration-tests-53953785/) | 2:17 | [↓](#4-why-should-you-write-integration-tests) |
| 5 | [What is in scope for integration testing](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-is-in-scope-for-integration-testing-53953786/) | 3:07 | [↓](#5-what-is-in-scope-for-integration-testing) |

## 1. The types of testing

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-types-of-testing-53953782/) · 8:18

### 总结

本课全面概述了测试的全貌,把集成测试定位在隔离的单元测试与完整规模的端到端测试之间。
它定义了测试的各个层级(单元、组件、集成、端到端和性能),同时强调集成测试通过纳入数据库和网络调用等真实依赖,能更真实地反映应用程序的行为。
本课提倡以自顶向下的方式进行集成测试,从用户在 API 或 UI 层面的视角出发,以确保系统在真实场景中按预期运行。

### 核心概念

- **Unit Testing**:测试的基础,在真空环境中关注小的单元(类或方法),没有 I/O,也没有外部依赖。
- **Component Testing**:也称为窄集成(narrow integration)测试或功能测试;它把多个单元放在一起测试,同时仍然避免外部 I/O。
- **Integration Testing**:验证应用程序的集成点,包括数据库、文件系统和网络服务等真实依赖。
- **Top-Down Approach**:一种集成测试策略,从入口点(API 端点或 UI)开始,向下贯穿整个系统。
- **End-to-End (E2E) Testing**:验证跨越多个服务和基础设施组件的整个系统流程。
- **Performance Testing**:评估系统如何应对负载,包括 load、soak、spike 和 stress 测试等子类型。

### 课程笔记

#### Unit and Component Testing

单元测试被视为测试策略的基础。
"单元"并没有严格的定义;它可以小到一行代码,也可以大到整个类或方法。
单元测试的决定性特征是完全没有 I/O。
这意味着没有数据库调用、没有网络请求,也没有文件系统交互。
为了实现这种隔离,外部依赖会被替换为 mock,也就是返回预定义结果的接口内存实现。

组件测试(有时称为窄集成测试或功能测试)遵循与单元测试相同的规则,但作用范围更大。
它不是测试单个单元,而是把若干个单元作为一个功能组件放在一起测试,同时仍然保持"无 I/O"的约束。

#### Integration Testing

集成测试通过测试应用程序真实的集成点,走出了单元测试的真空环境。
与单元测试不同,集成测试包含真实的依赖,例如:
- 数据库
- 文件系统
- 网络调用

虽然这需要为依赖(比如数据库)准备一个运行中的环境,但它能更真实地反映应用程序的表现。
当数据库的行为与 mock 所假设的不同时,单元测试可能无法发现这类问题。

对于 API 和 Web 应用程序,更推荐自顶向下的方式。
这意味着测试与应用程序交互的方式和用户完全一样:调用一个 API 端点(例如 `/customers`)或与一个 UI 表单交互,并一路向下验证结果直到数据库。
这确保了行为符合用户的预期。

```text
 测试 (像用户一样)
      |
      v
 API 端点 /customers 或 UI 表单
      |
      v
   应用程序
      |
      v
   数据库 (真实依赖)
```

#### End-to-End and Performance Testing

端到端(E2E)测试验证整个系统流程。
例如,一个 E2E 测试可能包括:向一个 API 插入数据,等待数据通过一个流同步到缓存,然后验证第二个 API 能从缓存中取回这些数据。
这些测试使用的是真实的、相互连接的系统,而不是隔离的部分。
E2E 测试常被用作 staging 或生产环境中的冒烟测试,用来验证已部署的代码。

```text
 插入数据 --> API 1 --> 流 --> 缓存 <-- API 2 <-- 验证取回数据
```

性能测试是一个独立但至关重要的类别,它评估系统如何应对压力。
这涉及使用一个执行者针对某个 API 发起大量请求(例如每秒 1,000 个请求),以生成性能报告。
常见的性能测试类型包括:
- **Load testing**:在预期使用量下测试系统。
- **Soak testing**:长时间测试系统,以发现泄漏或性能退化。
- **Spike testing**:测试系统对突发流量的反应。
- **Stress testing**:找到应用程序的崩溃点。

在本课程的语境中,重点仍然是集成测试:连同其直接依赖一起,隔离地测试某个特定的 API 或 UI,以确保行为真实。

## 2. The testing pyramid

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/the-testing-pyramid-53953783/) · 3:02

### 总结

测试金字塔是一个概念框架,用于把软件测试归入三个主要层级:单元测试、集成测试和端到端测试。
这个模型展示了执行速度、维护成本和测试范围之间的权衡,并把集成测试定位为一个高价值的中间地带。
单元测试提供快速反馈且成本低,端到端测试以高昂成本提供完整的系统验证,而 ASP.NET Core 中的集成测试则提供了一种平衡的方式,以可控的复杂度覆盖了大量用例。

### 核心概念

*   **Unit Testing (Base):** 数量最多的测试;它们执行极快,维护成本低廉。
*   **Integration Testing (Middle):** 验证组件之间或与外部系统(如数据库)之间交互的测试;它们具有很高的价值成本比。
*   **End-to-End Testing (Top):** 最复杂的测试;它们验证整个系统流程,但速度慢、成本高,而且难以并行运行。
*   **The Speed-Cost Trade-off:** 沿着金字塔向上,测试运行得越慢,编写和维护的成本也越高。
*   **ASP.NET Core Tooling:** ASP.NET Core 中的现代工具简化了集成测试的创建,使其成为许多开发者的首选。

### 课程笔记

测试金字塔是组织测试策略的一个标准模型。
虽然具体的层数可能因方法论而异,但它基本上由三层组成:底部是单元测试,中间是集成测试,顶部是端到端(E2E)测试。

```text
               /\
              /  \        E2E 测试
             /    \       (最慢、成本最高)
            /------\
           /        \     集成测试
          /          \    (较快、性价比高)
         /------------\
        /              \  单元测试
       /                \ (数量最多、极快、成本低)
      /------------------\
```

金字塔的形状反映了测试数量与其运行特征之间的关系。
较低层级的测试开发成本更低、更易维护,执行速度也明显快于顶部的测试。
例如,开发者在保存文件时几乎可以瞬间运行数千个单元测试。

相比之下,端到端测试是最慢、最耗资源的。
它们需要协调整个系统,通常还涉及复杂的状态管理。
一个 E2E 测试可能会发送一个请求,然后等待相当长的时间,直到系统达到某个特定状态后才能进行验证。
此外,并行运行 E2E 测试可能很危险,因为它们经常修改共享数据,除非由专业的自动化测试人员管理,否则可能导致冲突。

集成测试占据着关键的中间层。
虽然它们比单元测试慢(主要是因为它们通常依赖数据库之类的外部基础设施),但它们仍然相对较快且具有成本效益。
在 ASP.NET Core 生态中,集成测试格外强大,因为框架提供了让这些测试易于编写的工具。
由于集成测试能够比单独使用单元测试更有效地覆盖应用程序的大部分用例,它们代表了一种高价值的选择。
许多开发者优先编写集成测试,因为它们能为应用程序的功能提供高度的信心,同时又没有完整 UI 测试或端到端测试那样令人望而却步的成本或稳定性问题。

## 3. Why are integration tests so important?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-are-integration-tests-so-important-53953784/) · 2:19

### 总结

集成测试通过验证各个软件模块作为一个整体能否正确工作,弥合了单元测试与端到端测试之间的差距。
它通过测试主要的 happy path 和 unhappy path 来确保系统符合功能需求,往往能比单独的单元测试更全面地反映系统行为,同时又比完整的端到端测试更高效。

### 核心概念

* **Group Testing**:集成测试关注的是组合在一起的模块之间的交互,而不是孤立的组件。
* **Functional Compliance**:主要目标是评估系统或组件是否满足特定的功能需求。
* **Testing Pyramid**:就数量和范围而言,集成测试位于单元测试(数量多)与端到端测试(数量少)之间。
* **Path Coverage**:集成测试通常覆盖 happy path 和 unhappy path,而单元测试更适合小众的边界情况。
* **Top-down Approach**:一种常见的方法论,测试从最高层的模块开始,沿着层级向下进行。

### 课程笔记

集成测试是软件开发中把各个软件模块组合起来并作为一个整体进行测试的阶段。
进行这一过程是为了评估系统或组件是否符合其特定的功能需求。
在标准的开发生命周期中,集成测试发生在单元测试之后、系统(或端到端)测试之前。

```text
 单元测试 --> 集成测试 --> 系统 (端到端) 测试
```

集成测试的重要性在于它能弥合孤立的逻辑检查与完整系统验证之间的差距。
它覆盖了单元测试所针对的大部分代码,有时甚至到了某些单元测试变得多余的程度。
不过,它也执行了许多通常与端到端测试相关的验证。

在测试金字塔的语境中,测试的数量很重要。
开发者应该维护一大套单元测试、适量的集成测试,以及一小组端到端测试。
虽然很容易想要大量依赖集成测试,但重要的是知道在哪里停下,以避免不必要的重叠。
区别通常在于范围:集成测试验证 happy path(成功执行)和 unhappy path(预期的错误处理),而单元测试则留给集成测试可能触及不到的小众情况和复杂的逻辑分支。

集成测试有几种策略:
* Big Bang
* Mixed
* Risky Hardest
* Top-down
* Bottom-up

自顶向下的方式是专业环境中最常见、最容易上手的方法。
它从顶层组件开始,沿着技术栈向下推进,因此是本课程首选的策略。

## 4. Why should you write integration tests?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/why-should-you-write-integration-tests-53953785/) · 2:17

### 总结

ASP.NET Core 中的集成测试通过验证组件之间的交互,对系统表现做出真实的评估,超越了单元测试中孤立的假设。
这些测试在深度与实现难度之间提供了高价值的平衡,既是一种全面的验证工具,也是为遗留代码库追加测试和进行重构时的关键安全网。

### 核心概念

- 真实的系统表现 vs. 单元测试的假设。
- 高层测试与低层测试深度之间的平衡。
- ASP.NET Core 中很高的价值投入比。
- 为不可测试的代码或遗留代码追加测试。
- 为没有依赖注入的项目进行重构时的安全网。

### 课程笔记

集成测试能更准确地反映系统各组件集成在一起时的表现。
与严重依赖 mock 和假设的单元测试不同,集成测试验证的是服务、数据库和其他依赖之间的实际交互。
这使它们成为衡量系统健康状况的一种真实手段。

在 ASP.NET Core 中,集成测试在高层测试与低层测试之间占据了一个独特的位置。
它们足够深入,能够捕捉复杂的交互缺陷,同时又相对容易实现。
由于集成测试与单元测试的覆盖范围有大量重叠,一些开发者在时间或预算受限时会优先编写集成测试,因为它们通常能在整体系统功能方面提供更直接的价值。
虽然这并不是对所有场景的首要建议,但这种方式确保了系统在测试条件下的表现是真实的。

集成测试的一个重大挑战是管理外部依赖,比如真实的数据库。
其准备和清理过程可能比单元测试的隔离环境更复杂。
不过,ASP.NET Core 中的现代工具大大简化了这一过程。

在处理缺乏依赖注入(DI)或 SOLID 原则等现代设计模式的遗留系统时,集成测试格外强大。
在组件难以被 mock、因而代码难以进行单元测试的场景中,可以为其追加集成测试。
通过用一套集成测试包裹"不可测试"的代码,开发者就构建了一道保护盾。
这使得安全的重构成为可能,重构之后就可以在改进后的架构上引入单元测试。

```text
 遗留代码 --> 用集成测试包裹 --> 安全重构 --> 引入单元测试
```

## 5. What is in scope for integration testing

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-integration-testing-in-asp-net-core-2724127/what-is-in-scope-for-integration-testing-53953786/) · 3:07

### 总结

ASP.NET Core 中的集成测试专注于验证应用程序与其各种依赖之间的交互,并区分哪些依赖应保持真实、哪些应被替换。
数据库和文件系统等核心基础设施作为真实实例保留在范围内,以确保行为真实。
相反,外部依赖(无论是 GitHub 这样的第三方服务,还是其他团队拥有的第一方 API)都会被替换为模仿原始 API 契约的 mock 服务。
这一策略通过消除速率限制和宕机等外部因素来确保测试的可靠性,同时还能模拟复杂的故障场景。

### 核心概念

- **In-Scope Infrastructure**:数据库和文件系统应当是真实实例,以提供真实的测试结果。
- **External Dependencies**:第三方 API(例如 GitHub)被视为不在直接集成的范围内,应当被替换。
- **First-Party Dependencies**:同一组织内其他团队拥有的 API 也可以被替换,以保持测试的稳定性。
- **Replacement Strategy**:不要使用单元测试风格的 mock,而是使用一个遵循相同契约(模型、请求头和响应)的替代 API。
- **User Perspective**:测试应当从使用者的视角进行,无论使用者是 API 客户端还是 UI 用户。

### 课程笔记

集成测试需要明确界定哪些组件以真实状态被执行,哪些组件被替换。

```text
            +-----------------------+
 测试 ----> |   应用程序 (API / UI) |
 (用户视角) +-----------------------+
               |                 |
               v                 v
  范围内 (真实)            被替换 (遵循相同契约)
  - 数据库                 - 第三方 API (GitHub)
  - 文件系统               - 第一方 API (其他团队)
```

#### Internal Infrastructure

对于数据库这类内部有状态组件,测试应当与真实的数据库实例交互,而不是与 mock 交互。
这确保了集成点是真实的,并且应用程序能正确处理数据的持久化和检索。
同样,文件系统交互通常也保留在范围内,这意味着应用程序在测试执行期间会与实际的文件系统交互。

#### External and Third-Party APIs

GitHub API 这样的外部依赖给集成测试带来了特定的挑战。
这些服务通常有速率限制、需要身份验证,而且会出现你无法控制的宕机。
因为你并不拥有这些服务,所以它们被视为不在测试套件直接集成的范围内。

为了处理它们,外部服务会被替换为一个功能上的替代品。
这个替代品不是标准的单元测试 mock;它是一个模仿外部 API 行为的运行中服务或组件。
它必须遵守原始 API 的契约,包括:
- 相同的数据模型。
- 相同的 HTTP 请求头。
- 相同的响应结构。

通过使用替代品,你可以模拟那些用真实 API 难以触发的特定场景,比如强制服务失败或返回特定的错误码。
这确保了系统的错误处理逻辑得到充分测试,而不必依赖第三方提供商的可用性。

#### First-Party Dependencies

这种替换策略并不局限于第三方服务。
如果你的应用程序依赖于公司内部另一个团队拥有的 API(即第一方依赖),通常也值得把那个 API 替换掉。
这可以防止你的测试因为其他团队环境中的问题而失败,并让你能够隔离地测试应用程序的集成点。

#### Testing Perspective

集成测试应当从用户的立场来执行。
对于 API,这意味着像客户端那样发起请求并对响应进行断言。
对于 UI,这涉及模拟填写表单、点击按钮等用户操作,以确保从最终用户的视角来看整个技术栈都能正常工作。

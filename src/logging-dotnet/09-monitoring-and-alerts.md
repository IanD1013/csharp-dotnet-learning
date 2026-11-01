# Monitoring and alerts

> 课程:[From Zero to Hero: Logging in .NET](https://dometrain.com/course/from-zero-to-hero-logging-in-dotnet/) · 第 9 章
> 共 5 课 · 约 12:40
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [What is monitoring?](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-monitoring-54121720/) | 1:18 | [↓](#1-what-is-monitoring) |
| 2 | [Alerting in Application Insights](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/alerting-in-application-insights-54121721/) | 1:50 | [↓](#2-alerting-in-application-insights) |
| 3 | [Error-based alerts](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/error-based-alerts-54121722/) | 5:23 | [↓](#3-error-based-alerts) |
| 4 | [Query-based alerts](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/) | 3:34 | [↓](#4-query-based-alerts) |
| 5 | [Section recap](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121724/) | 0:35 | [↓](#5-section-recap) |

## 1. What is monitoring?

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/what-is-monitoring-54121720/) · 1:18

### 总结

监控(Monitoring)是可观测性(observability)的一个基本组成部分,它通过跟踪系统操作(例如事件和指标)来识别异常和性能下降。
借助 Application Insights、Datadog 或 Elastic Stack 等平台中的结构化日志和自定义维度(custom dimensions),开发者可以实现告警系统,用于检测严重错误、停机以及第 95 百分位上的延迟问题。

### 核心概念

* 监控是可观测性的一个子集
* 通过事件和指标跟踪操作
* 使用日志和自定义维度进行告警
* 检测性能下降(例如第 95 百分位延迟)
* 与平台无关的监控(Application Insights、Datadog、Elastic Stack、CloudWatch)

### 课程笔记

监控是更广泛的可观测性领域中的一门专门学科。
它专注于跟踪各种系统操作,包括离散的事件和连续的指标,以保持对运行状况的感知。
监控策略的首要目标是主动发现异常,例如服务突然降级或性能逐渐变慢。

在 .NET 环境中,监控通常的实现方式是在日志中捕获数据,并把这些数据导出到某个监控平台。
例如,在使用 Azure Application Insights 时,日志数据可以作为自定义维度传递。
这些维度让平台能够对数据进行分类和分析,从而支持创建自动化告警。

一个健壮的监控系统应当能让人轻松识别关键问题,包括服务停机、高严重性错误以及性能回退,尤其是那些影响第 95 百分位请求的回退。
虽然 Application Insights 是常见的选择,但其背后的技术与平台无关。
无论使用 Elastic Stack、Datadog、Amazon CloudWatch 还是其他业界标准的监控方案,同样的原则都适用。

## 2. Alerting in Application Insights

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/alerting-in-application-insights-54121721/) · 1:50

### 总结

Application Insights 中的告警是通过 Azure Monitor 服务来管理的,而不是直接在 Application Insights 资源中管理。
要配置有效的告警,开发者首先必须确保遥测数据(例如来自计时操作的 trace 以及异常日志)正在被成功摄取。
通过在 Azure 门户中使用 Kusto 查询语言(KQL),开发者可以验证自定义维度和日志条目是否存在,从而为定义在应用出现意外行为或性能下降时触发的告警规则打下基础。

### 核心概念

* Application Insights 告警与 Azure Monitor 的集成。
* 使用计时操作和模拟异常生成遥测数据。
* 使用 Kusto 查询语言(KQL)查询 trace。
* 基于日志数据配置告警规则。

### 课程笔记

要为一个 .NET 应用配置告警,你不是直接在 Application Insights 资源内部去找。
而是要在 Azure 门户中进入 **Monitor** 服务,并找到 **Alerts** 部分。
告警规则就是在这里定义和管理的。

在创建告警规则之前,应用必须先把遥测数据推送到 Application Insights。
在下面的示例中,一个 Web API 控制器被配置为记录一个计时操作,并偶尔模拟失败,从而为告警提供数据。

```csharp
[HttpGet(Name = "GetWeatherForecast")]
public async Task<IEnumerable<WeatherForecast>> Get()
{
    using (_logger.BeginTimedOperation("Weather retrieval"))
    {
        await Task.Delay(Random.Shared.Next(50, 250));

        if (Random.Shared.Next(1, 10) > 8)
        {
            throw new Exception("Oops something broke");
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/alerting-in-application-insights-54121721/?t=40)

当应用运行起来、并且该端点被调用了多次之后,日志就会被传输到 Azure。
你可以进入 Application Insights 资源的 **Logs** 部分来验证这些日志是否已被摄取。
使用 Kusto 查询语言(KQL),你可以查询 `traces` 表,以确保数据(包括像操作耗时这样的自定义维度)正确出现。

```kusto
traces
| order by timestamp
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/alerting-in-application-insights-54121721/?t=85)

确认日志中已有数据之后,就可以着手设置告警了。
一个常见的起点是为基础性错误(例如未处理的异常)创建告警,以确保运维团队能收到应用意外失败的通知。

```text
.NET app (BeginTimedOperation, exceptions)
        |
        v
Application Insights  -- Logs (KQL: traces) -->  验证数据
        |
        v
Azure Monitor -> Alerts  (告警规则)
```

## 3. Error-based alerts

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/error-based-alerts-54121722/) · 5:23

### 总结

本课演示如何利用来自 .NET 应用的遥测数据,在 Azure Monitor 中配置基于错误的告警。
内容涵盖:通过抛出异常来模拟应用失败,基于异常指标或自定义 Kusto 查询来定义告警规则,以及设置操作组(Action Groups)来发送自动通知,例如电子邮件。

### 核心概念

- 为测试告警模拟间歇性失败
- Azure Monitor 告警规则(Alert Rules)
- 信号类型:指标(Metrics)与自定义日志搜索(Custom Log Search)
- 用于基于日志告警的 Kusto 查询语言(KQL)
- 聚合粒度与阈值
- 操作组与通知机制(Email、SMS、Webhooks)

### 课程笔记

要实现贴近真实的监控,应用必须生成能反映实际失败状态的遥测数据。
在这个场景中,一个 .NET API 被修改为使用 `Random.Shared.Next` 间歇性地抛出异常。
这样可以确保告警能够针对一个真实存在(尽管是模拟出来的)的错误条件进行测试。

```csharp
[HttpGet(Name = "GetWeatherForecast")]
public async Task<IEnumerable<WeatherForecast>> Get()
{
    using (_logger.BeginTimedOperation("Weather retrieval"))
    {
        await Task.Delay(Random.Shared.Next(50, 250));

        if (Random.Shared.Next(1, 10) > 8)
        {
            throw new Exception("Oops something broke");
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/error-based-alerts-54121722/?t=25)

一旦应用开始向 Application Insights 发送异常遥测数据,就可以在 Azure Monitor 中配置告警规则了。

#### 配置告警条件

告警由基于信号的条件来定义。
基于错误告警的两种主要方法包括:

1.  **指标(Metrics)**:选择由 Application Insights SDK 推送的 "exceptions" 指标。可以设置一个阈值(例如,当 exceptions > 1 时告警)。
2.  **自定义日志搜索(Custom Log Search)**:使用 Kusto 查询语言(KQL)直接查询 `exceptions` 表。

```kusto
exceptions
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/error-based-alerts-54121722/?t=95)

使用日志搜索时,你需要定义聚合粒度(例如每 1 分钟)和阈值。
如果在指定周期内查询返回的行数超过阈值,告警就会触发。

#### 操作组与通知

**操作组(Action Group)** 定义了告警触发时会发生什么。
它可以包括:

- **通知(Notifications)**:向特定团队成员发送电子邮件、短信或推送通知。
- **操作(Actions)**:触发 Azure Functions、Logic Apps 或 Webhooks,以进行自动化修复。

在 Azure 门户中,创建操作组时需要指定资源组、名称(例如 "Theme Alpha")以及通知详情(例如像 nick@dometrain.com 这样的电子邮件地址)。

#### 验证

部署告警规则之后,多次调用该 API 端点最终会导致随机异常被抛出。
Azure Monitor 会评估遥测数据;当达到阈值时,告警状态会变为 "Fired"。
随后系统会执行操作组,发送已配置的通知。
Azure 门户中的告警详情提供了一个深层链接,可直接跳回 Application Insights 中触发该事件的具体 KQL 查询结果。

```text
exceptions (metric / KQL)
        |
        v
Alert rule: 聚合粒度 + 阈值
        |  达到阈值
        v
status = "Fired"
        |
        v
Action Group --> Notifications (Email, SMS, push)
             --> Actions (Azure Functions, Logic Apps, Webhooks)
```

## 4. Query-based alerts

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/) · 3:34

Azure Application Insights 中基于查询的告警通过对日志数据执行复杂计算,提供了一种强大的应用健康监控方式。
与简单的指标告警不同,基于查询的告警使用 Kusto 查询语言(KQL)从结构化日志中计算出新的数据点,例如计算操作延迟的百分位数。

### 核心概念

*   **Kusto 查询语言(KQL)**:用于在 Application Insights 中分析日志的查询语言。
*   **自定义维度(Custom Dimensions)**:附加在日志上的结构化元数据(例如 `OperationDurationMs`),可用于筛选和聚合。
*   **百分位聚合(Percentile Aggregation)**:使用 `summarize percentile()` 识别性能离群值(例如第 95 百分位)。
*   **Event ID 与消息模板(Message Template)**:使用 `EventId` 筛选日志比按 `MessageTemplate` 字符串筛选性能更好、也更稳定。
*   **告警规则(Alert Rules)**:按指定间隔运行 KQL 查询、并根据阈值触发操作的自动化监控。
*   **操作组(Action Groups)**:定义告警触发时通知谁(例如通过电子邮件)的配置。

### 课程笔记

#### 查询 Trace 与自定义维度

日志发送到 Application Insights 时,会包含标准字段和自定义维度。
要开始分析性能,你可以查询 `traces` 表,并按时间对结果排序,以查看操作的先后顺序。

```kusto
traces
| order by timestamp
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/?t=10)

在一个典型的 .NET 应用中,可以用结构化日志来捕获某个操作的耗时。
例如,一个天气查询服务可能会记录完成该请求花了多长时间。
下面的实现使用一个计时操作来捕获这些数据:

```csharp
[HttpGet(Name = "GetWeatherForecast")]
public async Task<IEnumerable<WeatherForecast>> Get()
{
    using (_logger.BeginTimedOperation("Weather retrieval"))
    {
        await Task.Delay(Random.Shared.Next(50, 250));

        if (Random.Shared.Next(1, 10) > 8)
        {
            throw new Exception("Oops something broke");
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/?t=40)

#### 筛选与计算指标

要分析特定的操作,可以使用 `MessageTemplate` 来筛选 trace。
虽然按消息模板筛选可行,但推荐使用 `EventId`,这样性能更好,也能避免将来日志消息文本发生变化时出现问题。

```kusto
traces
| where customDimensions.MessageTemplate == 'Weather retrieval completed in {OperationDurationMs}ms'
| order by timestamp
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/?t=55)

筛选之后,就可以计算性能指标了。
把自定义维度 `OperationDurationMs` 转换为 double 后,就能计算第 95 百分位,从而了解大多数用户所经历的延迟。

```kusto
traces
| where customDimensions.MessageTemplate == 'Weather retrieval completed in {OperationDurationMs}ms'
| summarize percentile(todouble(customDimensions.OperationDurationMs), 95)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/?t=115)

```text
traces
  | where MessageTemplate == 'Weather retrieval completed in ...'
  v
OperationDurationMs  --todouble-->  percentile(..., 95)
                                          |
                                          v
                                  alert if >= 300ms
```

#### 创建告警规则

要实现自动化监控,可以在 Azure Monitor 中使用自定义日志查询创建一个新的告警规则。
这样当计算出的百分位超过特定阈值时,系统就能向你发出告警。

1.  **条件(Condition)**:把信号逻辑设置为 "Custom log query",并粘贴 KQL 查询。
2.  **查询(Query)**:使用一个返回该指标值的汇总查询。

```kql
traces
| where customDimensions.MessageTemplate == 'Weather retrieval completed in {OperationDurationMs}ms'
| summarize percentile(todouble(customDimensions.OperationDurationMs), 95)
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/query-based-alerts-54121723/?t=130)

3.  **度量(Measurement)**:设置阈值(例如大于或等于 300ms)以及聚合粒度(例如在 30 分钟的窗口内每 5 分钟一次)。
4.  **操作(Actions)**:选择一个操作组来定义通知方式,例如发送电子邮件。
5.  **详细信息(Details)**:指定一个严重性级别(例如 Warning)以及一个描述性名称,例如 "Weather retrieval degradation"。

创建完成后,Azure Monitor 会持续运行这个查询,并在天气查询操作的性能下降超出所定义的阈值时向团队发出告警。

## 5. Section recap

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-logging-in-dotnet-2731700/section-recap-54121724/) · 0:35

### 总结

本课回顾了日志与监控、告警系统的集成,重点是 Azure Application Insights。
它强调了日志如何被转化为可据以行动的监控数据,从而可以基于错误的发生或由日志指标得出的性能阈值来创建告警。

### 核心概念

* 集成 Application Insights 进行监控。
* 基于异常的告警与电子邮件通知。
* 使用日志汇总查询进行基于指标的告警。
* 对操作耗时进行阈值监控。

### 课程笔记

监控与告警这一部分演示了如何把 .NET 日志用作主动监控的数据源。
通过与 Azure Application Insights 集成,日志数据被转化为可搜索的遥测数据和指标。

#### 针对错误告警

一种基础的监控策略是基于异常或一般错误日志来触发告警。
当应用记录一条错误级别的日志条目时,可以配置 Application Insights 触发告警,并通过电子邮件通知团队。
这能确保团队立即察觉生产环境中的严重故障。

#### 基于查询的阈值告警

高级监控涉及查询日志,以计算随时间变化的指标。
告警可以基于对操作进行汇总后计算出的阈值来配置。
例如,通过记录特定任务(例如天气查询操作)的耗时,开发者可以创建查询来监控平均耗时或最大耗时。
如果计算出的值超过所定义的阈值,就会触发告警,从而能够发现性能瓶颈或性能下降。

在所提供的实现中,这些指标是通过一个 `BeginTimedOperation` 扩展来捕获的。
这个工具会创建一个 `TimedLogOperation`,它使用 `Stopwatch.GetTimestamp()` 来测量执行时间,并把结果记录为 `OperationDurationMs`。
这些日志就是 Application Insights 中基于阈值告警的原始数据。

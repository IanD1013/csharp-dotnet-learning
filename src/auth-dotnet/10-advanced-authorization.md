# Advanced Authorization

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 10 章
> 共 8 课 · 约 2:27:58
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Requirements and Handlers](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/) | 34:52 | [↓](#1-requirements-and-handlers) |
| 2 | [Resource-based Authorization](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/) | 26:13 | [↓](#2-resource-based-authorization) |
| 3 | [The MVC filter pipeline](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/) | 18:43 | [↓](#3-the-mvc-filter-pipeline) |
| 4 | [Authorization Filters](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/) | 20:48 | [↓](#4-authorization-filters) |
| 5 | [Custom Authorization Attributes](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/) | 10:37 | [↓](#5-custom-authorization-attributes) |
| 6 | [DefaultPolicy and FallbackPolicy](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/) | 19:55 | [↓](#6-defaultpolicy-and-fallbackpolicy) |
| 7 | [View-based authorization](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/) | 14:37 | [↓](#7-view-based-authorization) |
| 8 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/) | 2:13 | [↓](#8-section-recap) |

## 1. Requirements and Handlers

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/) · 34:52

虽然基于策略的授权很强大,但复杂的逻辑会导致配置杂乱,难以维护和进行单元测试。
自定义需求(requirement)和处理器(handler)提供了一种更灵活、更解耦的方式。
需求定义必须满足的条件,而处理器实现评估该条件的逻辑。

### 核心概念

- **IAuthorizationRequirement**:一个标记接口,用于定义某个特定的授权标准。
- **AuthorizationHandler<T>**:一个基类,用于实现满足某个特定需求所需的逻辑。
- **Requirement Decoupling(需求解耦)**:把"是什么"(需求)与"怎么做"(处理器)分离开,以获得更好的可维护性。
- **Multiple Handlers(多个处理器)**:可以为单个需求分配多个处理器,其中任何一个处理器都可以授予成功(OR 逻辑),但任何显式失败都会阻止访问。
- **Context Methods(上下文方法)**:使用 `context.Succeed()` 批准一个需求,使用 `context.Fail()` 显式拒绝它。
- **Dependency Injection(依赖注入)**:在服务容器中注册处理器,使授权系统可以使用它们。

### 课程笔记

随着授权策略的增长,它们常常会被多个内置需求塞得杂乱不堪,使配置难以阅读和维护。

```csharp
builder.Services.AddMyAuthorization(o =>
{
    o.AddPolicy("manager", policy =>
        policy.RequireRole("manager")
            .RequireClaim("email")
            .RequireClaim("country", ["Sweden", "Denmark"])
            .RequireClaim("JobTitle", "Manager"));
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=25)

为了解决这个问题,可以向策略中添加自定义需求。
需求是策略要成功就必须满足的一个条件。
策略中的所有需求都必须被满足,才会授予访问权限。

```csharp
o.AddPolicy("admin", policy =>
{
    // Built-in requirement
    policy.RequireRole("admin", "management");

    // Custom requirements
    policy.Requirements.Add(new EnhancedSecurityRequirement());
    policy.Requirements.Add(new WorkingHoursRequirement(9, 18));
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=55)

#### 定义需求

需求类必须实现 `IAuthorizationRequirement` 接口。
这是一个空的标记接口,没有任何方法或属性;它只是把该类标识为一个授权需求。
需求还可以存储配置数据,例如时间范围或安全级别。

```csharp
public class EnhancedSecurityRequirement : IAuthorizationRequirement
{
    public EnhancedSecurityRequirement()
    {
        // Contains no authorization logic
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=115)

```csharp
public class WorkingHoursRequirement : IAuthorizationRequirement
{
    public WorkingHoursRequirement(int startHour, int endHour)
    {
        // A requirement can accept parameters
        StartHour = startHour;
        EndHour = endHour;
    }

    public int StartHour { get; }
    public int EndHour { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=175)

#### 实现处理器

处理器包含真正的授权逻辑。
处理器继承自 `AuthorizationHandler<TRequirement>`,并且必须实现 `HandleRequirementAsync` 方法。
该方法接收 `AuthorizationHandlerContext` 以及具体的需求。

处理器可以通过三种方式对一个需求投票:
1.  **Succeed**:调用 `context.Succeed(requirement)` 批准该需求。
2.  **Fail**:调用 `context.Fail()` 显式阻止该需求,无论其他处理器如何。
3.  **Ignore**:什么都不做,让其他处理器来决定。

```csharp
protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                EnhancedSecurityRequirement req)
{
    var ipOk = IsTrustedIPAddress();

    if (ipOk == false)
        context.Fail();

    // If neither Succeed nor Fail is called, the handler is ignored
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=310)

处理器可以执行各种检查,例如验证多因素认证(MFA)声明,或使用需求参数检查营业时间。

```csharp
public class MfaHandler : AuthorizationHandler<EnhancedSecurityRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   EnhancedSecurityRequirement req)
    {
        if (context.User.HasClaim("amr", "mfa"))
        {context.Succeed(req);}
        else
        {context.Fail();}

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=385)

```csharp
public class WorkingHoursHandler : AuthorizationHandler<WorkingHoursRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   WorkingHoursRequirement requirement)
    {
        var hour = DateTime.UtcNow.Hour;

        if (hour >= requirement.StartHour && hour < requirement.EndHour)
            context.Succeed(requirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=415)

#### 依赖注入注册

处理器必须注册到依赖注入(DI)容器中。
你可以根据处理器的依赖选择合适的生命周期(Singleton、Transient 或 Scoped)。

```csharp
// Single shared instance
builder.Services.AddSingleton<IAuthorizationHandler, IPAddressHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MfaHandler>();

// Fresh instance each time
builder.Services.AddTransient<IAuthorizationHandler, WorkingHoursHandler>();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=445)

#### 重构 Finance 策略

在示例应用中,finance 策略里一段复杂的 `RequireAssertion` 逻辑被重构为一个自定义需求和多个处理器。

```csharp
o.AddPolicy("finance", policy => policy.RequireAssertion(context =>
{
    var user = context.User;
    var titleOk = user.HasClaim(claim => claim.Type == "JobTitle" && claim.Value == "finance");
    var countryOk = user.HasClaim(claim => claim.Type == "country" && (claim.Value == "Sweden" || claim.Value == "Denmark"));
    var rolesOk = user.IsInRole("finance") || user.IsInRole("management");
    return titleOk && countryOk && rolesOk;
}));
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=505)

它被替换为向策略中添加一个 `FinanceAccessRequirement`。
为了调试评估过程,可以在 `appsettings.Development.json` 中把授权的日志级别设置为 `Trace`。

```csharp
o.AddPolicy("finance", policy =>
{
    policy.Requirements.Add(new FinanceAccessRequirement());
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=640)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.AspNetCore.Authorization": "Trace"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=745)

#### 一个需求对应多个处理器

单个需求可以有多个处理器。
例如,如果用户属于 Finance 角色,或者属于 Management 角色,都可以被授予访问财务数据的权限。

```csharp
internal sealed class FinanceAccessHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    private readonly ILogger<FinanceAccessHandler> _logger;

    public FinanceAccessHandler(ILogger<FinanceAccessHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating finance requirement for user '{context.User.Identity?.Name}'");

        var user = context.User;
        var titleOk = user.HasClaim("JobTitle", "finance");
        var countryOk = user.HasClaim("country", "Sweden") || user.HasClaim("country", "Denmark");
        var hasFinanceRole = user.IsInRole("finance");

        if (hasFinanceRole && titleOk && countryOk)
        {context.Succeed(requirement);}

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=1120)

```csharp
internal sealed class ManagementAccessHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    private readonly ILogger<ManagementAccessHandler> _logger;

    public ManagementAccessHandler(ILogger<ManagementAccessHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating management requirement for user '{context.User.Identity?.Name}'");

        if (context.User.IsInRole("management"))
        {context.Succeed(requirement);}

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=1150)

#### 用 context.Fail() 阻止访问

虽然多个处理器为成功提供了一种"OR"逻辑,但调用 `context.Fail()` 会起到阻断作用。
如果任何处理器调用了 `Fail()`,即使另一个处理器调用了 `Succeed()`,该需求也会被拒绝。
这对于全局性检查很有用,例如验证用户在数据库中是否仍处于活跃状态。

```csharp
internal sealed class ActiveUserHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    private readonly ILogger<ActiveUserHandler> _logger;

    public ActiveUserHandler(ILogger<ActiveUserHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating if active user, for user '{context.User.Identity?.Name}'");

        var isActive = IsUserActiveInDatabase(context.User);

        if (isActive == false)
        {context.Fail();}

        return Task.CompletedTask;
    }

    private bool IsUserActiveInDatabase(ClaimsPrincipal user) => true;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=1645)

```text
FinanceAccessRequirement
  |
  +-- FinanceAccessHandler     -> Succeed  (finance role, title, country) \
  |                                                                        OR
  +-- ManagementAccessHandler  -> Succeed  (management role)              /
  |
  +-- ActiveUserHandler        -> Fail     (not active)  => rejected anyway
```

#### 最终的配置清理

为了保持 `Program.cs` 文件整洁,授权配置和处理器注册应当移到一个扩展方法中。

```csharp
internal static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, FinanceAccessHandler>();
        services.AddSingleton<IAuthorizationHandler, ManagementAccessHandler>();
        services.AddSingleton<IAuthorizationHandler, ActiveUserHandler>();

        services.AddAuthorization(o =>
        {
            o.AddPolicy("finance", policy =>
            {
                policy.Requirements.Add(new FinanceAccessRequirement());
            });

            o.AddPolicy("sales", policy =>
                policy.RequireRole("manager", "sales")
                      .RequireClaim("country", ["Sweden", "Denmark", "Norway"]));

            o.AddPolicy("admin", policy =>
                policy.RequireRole("admin")
                      .RequireRole("management"));

            o.AddPolicy("customer", policy =>
                    policy.RequireAuthenticatedUser());
        });

        return services;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=1975)

最后,在 `Program.cs` 中调用该扩展方法以应用配置。

```csharp
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorizationServices();
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/users/AccessDenied";
    o.LoginPath = "/users/login";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/requirements-and-handlers-69957977/?t=2035)

## 2. Resource-based Authorization

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/) · 26:13

基于资源的授权解决了这样一个局限:标准的策略评估发生在目标资源被加载之前。
基于角色或基于声明的授权可以验证用户身份,而基于资源的授权则允许编写依赖于正在访问的具体数据的逻辑,例如验证用户是否拥有某张特定发票。

### 核心概念

- **Timing Problem(时机问题)**:授权管道通常在 action 方法执行之前运行,这意味着正在访问的资源还无法用于检查。
- **Resource-based Authorization(基于资源的授权)**:同时评估用户上下文和所请求数据的具体实例的安全逻辑。
- **IAuthorizationService**:核心服务,用于在资源可用之后,在 action 方法内部以编程方式触发授权检查。
- **AuthorizationHandler<TRequirement, TResource>**:一种专用的处理器,它接收资源作为参数用于评估。
- **AuthorizeAsync**:用于把用户、资源和策略名称传给授权系统的方法。

### 课程笔记

当访问取决于具体数据时,基于角色的授权往往不够用。
例如,在一个发票系统中,多个用户可能都拥有 "finance" 角色,但每个人都只应访问自己的发票。
标准的基于角色的检查只验证用户是否拥有该角色,而不验证他们是否拥有这个具体资源。

#### 时机问题

根本的挑战在于,授权管道在你的 action 方法执行之前运行。

```csharp
[Authorize("finance")]
public IActionResult Invoice(int id)
{
    // 1. Load Invoice from database
    // 2. Return invoice
    return Ok($"Accessing Invoice {id}");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/?t=25)

在这个场景中,策略在发票被加载之前就完成了评估。
授权系统看不到正在被访问的具体资源。
等到 action 方法执行并加载发票时,授权检查早已仅凭用户的角色通过或失败了。

```text
request
  |
  v
[Authorize("finance")]  -> policy evaluated (roles only, no invoice yet)
  |
  v
Invoice(int id)         -> 1. Load Invoice from database
                           2. Return invoice
```

#### 在控制器中手动授权

一种解决方案是把特定于资源的授权逻辑直接移到 action 方法中。

```csharp
// BAD: Manual authorization in controller
[Authorize("finance")]
public IActionResult Invoice(int id)
{
    // 1. Load Invoice from database
    var invoice = _invoiceService.GetInvoiceById(id);

    // 2. Get current user ID
    var currentUserId = User.FindFirst("sub")?.Value;

    // 3. Ensure the user owns the invoice
    if (invoice.UserId != currentUserId)
        return Forbid();

    return Ok($"Accessing Invoice {id}");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/?t=145)

虽然这样可行,但它会带来几个问题:它把授权逻辑和应用代码混在一起,需要在每个处理发票的方法中重复,并且破坏了关注点分离。

#### 实现基于资源的授权

基于资源的授权允许资源流入需求,使处理器能够提出这样的问题:"这个用户拥有这张具体的发票吗?"

##### 1. 定义需求

首先,创建一个实现 `IAuthorizationRequirement` 的需求类。

```csharp
using Microsoft.AspNetCore.Authorization;

internal class InvoiceOwnershipRequirement : IAuthorizationRequirement
{
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/?t=625)

##### 2. 定义处理器

该处理器继承自 `AuthorizationHandler<TRequirement, TResource>`,它对需求和资源类型(本例中为 `Invoice`)都是强类型的。

```csharp
using Microsoft.AspNetCore.Authorization;
using StartcodeAuthorization.Features.Invoices;

internal class InvoiceOwnershipHandler : AuthorizationHandler<InvoiceOwnershipRequirement, Invoice>
{

    public InvoiceOwnershipHandler()
    {
        Console.WriteLine("InvoiceOwnershipHandler.Constructor");
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   InvoiceOwnershipRequirement requirement,
                                                   Invoice invoice)
    {
        // Get current user ID from claims
        var currentUserIdClaim = context.User.FindFirst("sub")?.Value;

        if (currentUserIdClaim == null)
        {
            // User not authenticated
            return Task.CompletedTask;
        }

        if (int.TryParse(currentUserIdClaim, out int currentUserId))
        {
            // Check if user owns this invoice
            if (invoice.UserId == currentUserId)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/?t=1360)

##### 3. 注册策略和处理器

策略必须添加到授权选项中,处理器必须注册到服务容器中。

```csharp
o.AddPolicy("invoiceowner", policy =>
        policy.Requirements.Add(new InvoiceOwnershipRequirement()));

services.AddSingleton<IAuthorizationHandler, InvoiceOwnershipHandler>();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/?t=835)

##### 4. 在控制器中调用授权

因为 `[Authorize]` 特性无法把资源传给处理器,所以你必须注入 `IAuthorizationService`,并在资源加载之后,在 action 方法内部手动调用 `AuthorizeAsync`。

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Invoices
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceDatabase invoiceDatabase;
        private readonly IAuthorizationService authorizationService;

        public InvoicesController(IInvoiceDatabase invoiceDatabase, IAuthorizationService authorizationService)
        {
            this.invoiceDatabase = invoiceDatabase;
            this.authorizationService = authorizationService;
        }

        [Authorize]
        public async Task<ActionResult<Invoice>> Invoice(int id)
        {
            // 1. Load the invoice (resource)
            var invoice = invoiceDatabase.GetInvoiceById(id);

            if (invoice == null)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }
            else
            {
                // 2. Check authorization with the resource
                var authResult = await authorizationService.AuthorizeAsync(
                    user: User,
                    resource: invoice,
                    policyName: "invoiceowner"
                );

                if (!authResult.Succeeded)
                {
                    return Forbid();
                }

                // 3. Authorization passed, return the invoice
                return invoice;
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/resource-based-authorization-69957978/?t=1180)

## 3. The MVC filter pipeline

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/) · 18:43

### 总结

MVC 过滤器管道在 action 方法层面提供了对请求处理的细粒度控制,它运行在 MVC 中间件内部,位于标准 ASP.NET Core 中间件管道之后。
它由五种过滤器类型组成(Authorization、Resource、Action、Result 和 Exception),让开发者能够拦截并修改执行流程、检查请求/响应数据,并集中处理错误。
通过实现 IAuthorizationFilter 或 IResultFilter 等特定接口,开发者可以在请求生命周期中的精确位置注入自定义逻辑,例如在模型绑定之前,或在 action 结果已经产生但尚未执行之前。

### 核心概念

*   **Filter Pipeline Execution(过滤器管道执行)**:过滤器在 MVC 中间件内部运行,位于全局中间件管道(Authentication/Authorization)完成之后。
*   **Filter Types(过滤器类型)**:
    *   **Authorization Filters**:最先运行的过滤器;用于判断用户是否有权执行该请求。
    *   **Resource Filters**:在模型绑定之前以及结果执行之后运行。
    *   **Action Filters**:紧挨着 action 方法执行之前和之后运行。
    *   **Result Filters**:在 action 结果执行之前和之后运行(例如渲染视图或序列化 JSON)。
    *   **Exception Filters**:用于处理在其他过滤器或 action 方法执行期间发生的未处理异常。
*   **Registration(注册)**:可以在 `AddControllersWithViews` 配置中通过 `MvcOptions` 全局注册过滤器。
*   **Short-circuiting(短路)**:Authorization 或 Resource 过滤器之类的过滤器可以通过在上下文对象上设置结果来提前终止请求的执行。

### 课程笔记

#### 请求管道与 MVC

ASP.NET Core 中间件处理诸如认证("你是谁?")和授权("你能访问这个资源吗?")之类的全局关注点,而 MVC 过滤器则提供了一个更细粒度的管道,专门为 MVC action 运行。
这个管道允许编写需要了解正在执行的具体控制器或 action 的逻辑。

为了观察中间件和 MVC 管道之间的交互,可以在标准的认证和授权中间件周围添加自定义日志:

```csharp
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine($"@@@ - before UseAuthentication - {context.Request.Path}");
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine($"@@@ - before UseAuthorization");
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=460)

#### 控制器结构

演示应用使用一个带有多个 action 的 `HomeController` 来演示不同的管道行为,包括标准视图、返回数据和触发异常。

```csharp
public class HomeController : Controller
{
    public HomeController()
    {
        Console.WriteLine("    *** Home Controller - Constructor");
    }

    public IActionResult Index()
    {
        Console.WriteLine("    *** Home Controller - Index method");
        return View();
    }

    public IActionResult Time()
    {
        Console.WriteLine(" *** Home Controller - Time method");
        return Ok(DateTime.Now);
    }

    public IActionResult ForbidUser()
    {
        Console.WriteLine(" *** Home Controller - Forbid method (returns Forbid)");
        return Forbid();
    }

    public IActionResult ChallengeUser()
    {
        Console.WriteLine(" *** Home Controller - Challenge method (returns challenge)");
        return Challenge();
    }

    public IActionResult Exception()
    {
        Console.WriteLine(" *** Home Controller - Exception method (throws exception)");
        throw new InvalidOperationException("Test exception for filter demo");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=315)

#### 实现 MVC 过滤器

每种过滤器类型都通过创建一个继承相应接口的类来实现。
这些过滤器把自己的执行情况记录到控制台,以便直观地看到管道顺序。

**Authorization and Resource Filters**
Authorization 过滤器在 MVC 管道中最先运行。
Resource 过滤器紧随其后,是授权之后最先运行的过滤器,包裹着管道的其余部分。

```csharp
public class MyAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("### Authorization Filter: Checking authorization...");
    }
}

public class MyResourceFilter : IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine(" ### Resource Filter: BEFORE resource execution");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine(" ### Resource Filter: AFTER resource execution");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=475)

**Action and Result Filters**
Action 过滤器包裹着 action 方法本身的执行。
Result 过滤器包裹着 action 所返回的 `IActionResult` 的执行。

```csharp
public class MyActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("  ### Action Filter: BEFORE action execution");
        Console.WriteLine($"  ### Action: {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("   ### Action Filter: AFTER action execution");
    }
}

public class MyResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("    ### Result Filter: BEFORE result execution");
        Console.WriteLine($"    ### Result Type: {context.Result?.GetType().Name}");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("    ### Result Filter: AFTER result execution");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=520)

```text
UseAuthentication -> UseAuthorization -> MVC middleware:

  MyAuthorizationFilter.OnAuthorization
  MyResourceFilter.OnResourceExecuting
    MyActionFilter.OnActionExecuting
      HomeController action
    MyActionFilter.OnActionExecuted
    MyResultFilter.OnResultExecuting
      IActionResult executed
    MyResultFilter.OnResultExecuted
  MyResourceFilter.OnResourceExecuted
```

**Exception Filters**
Exception 过滤器专门用于处理在 action 执行期间或在其他过滤器内部发生的错误。

```csharp
public class MyExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Console.WriteLine();
        Console.WriteLine("§§§ Exception Filter: Handling exception");
        Console.WriteLine($"§§§ Exception: '{context.Exception.Message}'");
        Console.WriteLine();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=565)

#### 全局注册过滤器

要激活这些过滤器,必须在服务容器中注册它们。
全局注册可以确保它们对 MVC 处理的每个请求都会运行。

```csharp
builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<MyAuthorizationFilter>();
    o.Filters.Add<MyResourceFilter>();
    o.Filters.Add<MyActionFilter>();
    o.Filters.Add<MyResultFilter>();
    o.Filters.Add<MyExceptionFilter>();
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=670)

#### 使用 Result 过滤器修改响应

Result 过滤器之所以强大,是因为它们允许在响应发送给客户端之前检查并修改它。
例如,你可以添加自定义 HTTP 头,或者在 action 方法返回的是 `ObjectResult` 时转换其返回的数据。

```csharp
public class MyResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        Console.WriteLine("    ### Result Filter: BEFORE result execution");
        Console.WriteLine($"    ### Result Type: {context.Result?.GetType().Name}");

        // Modify response before it's sent
        context.HttpContext.Response.Headers.Add("X-Custom-Header", "MyValue");

        // Access the action result and transform data
        if (context.Result is ObjectResult objectResult)
        {    
            objectResult.Value = new 
            {
                OriginalData = objectResult.Value, 
                Timestamp = DateTime.UtcNow 
            };
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        Console.WriteLine("    ### Result Filter: AFTER result execution");
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-mvc-filter-pipeline-69957979/?t=970)

## 4. Authorization Filters

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/) · 20:48

授权过滤器在 MVC 管道中最先运行,充当控制器和 action 的守门人。
它们有能力让整个请求管道短路;如果访问被拒绝,控制器不会被创建,action 方法也永远不会执行。
这些过滤器可以返回任何 `IActionResult`,包括 challenge、forbid 或重定向。
全局注册时,它们提供默认安全的保护,确保每个 action 都受到保护,而无需在每个控制器上添加特性。

### 核心概念

*   **Gatekeeper Role(守门人角色)**:授权过滤器是过滤器管道中最先执行的。
*   **Short-circuiting(短路)**:设置 `context.Result` 会立即阻止请求到达控制器。
*   **Interfaces(接口)**:同步逻辑实现 `IAuthorizationFilter`,异步逻辑实现 `IAsyncAuthorizationFilter`。
*   **Registration Scopes(注册范围)**:过滤器可以在全局、控制器级别或 action 级别应用。
*   **ServiceFilter vs. TypeFilter**:`ServiceFilter` 从依赖注入(DI)中获取过滤器,而 `TypeFilter` 按需创建实例。
*   **Context Access(上下文访问)**:`AuthorizationFilterContext` 提供对 `HttpContext`、`RouteData` 以及用于短路的 `Result` 属性的访问。

### 课程笔记

要创建授权过滤器,你必须实现 `IAuthorizationFilter` 或 `IAsyncAuthorizationFilter` 之一。
两者都提供一个授权上下文,让你可以检查当前请求和用户身份。

```csharp
public interface IAuthorizationFilter : IFilterMetadata
{
    void OnAuthorization(AuthorizationFilterContext context);
}

public interface IAsyncAuthorizationFilter : IFilterMetadata
{
    Task OnAuthorizationAsync(AuthorizationFilterContext context);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=85)

授权过滤器的一种常见模式是检查认证状态和特定权限。
如果用户未通过认证,过滤器可以返回一个 `ChallengeResult`。
如果用户已通过认证但缺少必要的角色,它可以返回一个 `ForbidResult`。

```csharp
public class ApiAuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new ChallengeResult();
            return;
        }

        if (!context.HttpContext.User.IsInRole("admin"))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=100)

#### 注册方式

过滤器可以在 `Program.cs` 中配置 MVC 时全局注册。
这确保该过滤器会对应用中的每个请求运行。

```csharp
builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<ApiAuthFilter>();
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=160)

如果你不想全局应用,可以使用 `[ServiceFilter]` 或 `[TypeFilter]`。

*   **ServiceFilter**:要求过滤器已注册到 DI 容器中。
    如果你的过滤器有日志或数据库上下文之类的依赖,这就是必需的。
*   **TypeFilter**:不需要 DI 注册,直接创建过滤器实例。

```csharp
[ServiceFilter(typeof(ApiAuthFilter))]
public class AdminController : Controller
{
    public IActionResult SomeAction() { return View(); }
}

// DI registration required for ServiceFilter
builder.Services.AddScoped<ApiAuthFilter>();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=220)

```csharp
[TypeFilter(typeof(ApiAuthFilter))]
public class UserController : Controller
{
    public IActionResult SomeOtherAction() { return View(); }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=265)

#### 实际示例

授权过滤器对于 API key 校验或 IP 拦截之类的横切关注点非常有效。
通过尽早拒绝不需要的流量,你可以避免创建控制器或执行业务逻辑的开销。

```csharp
public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.Headers["X-API-Key"];
        if (string.IsNullOrEmpty(apiKey) || apiKey != "your-secret-key")
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=325)

#### 实现一个声明转换过滤器

你还可以使用授权过滤器向当前用户的身份动态添加声明。
在下面的示例中,`AddClaimsFilter` 会为已认证用户添加一个请求时间戳和一个动态角色。

```csharp
public class AddClaimsFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var identity = context.HttpContext.User?.Identity as ClaimsIdentity;
        if (identity != null && identity.IsAuthenticated)
        {
            identity.AddClaim(new Claim(
                type: "request_time",
                value: DateTimeOffset.UtcNow.ToString(),
                valueType: ClaimValueTypes.String,
                issuer: "AddClaimsFilter"));

            identity.AddClaim(new Claim(
                type: "dynamic_role",
                value: "temp_admin",
                valueType: ClaimValueTypes.String,
                issuer: "AddClaimsFilter"));
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=760)

要把这个过滤器应用到某个特定 action 上,请使用 `[TypeFilter]` 特性。
如果该过滤器同时在全局和 action 级别应用,它会对这个 action 执行两次。

```csharp
[TypeFilter(typeof(AddClaimsFilter))]
[AllowAnonymous]
public IActionResult Contact()
{
    return View();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=520)

#### 基于路径的授权逻辑

过滤器还可以检查请求路径来强制执行特定规则。
例如,你可以要求任何访问 `/Secure/Support` 路径的用户都必须拥有 email 声明。

```csharp
var path = context.HttpContext.Request.Path;
if (path.StartsWithSegments("/Secure/Support"))
{
    var hasEmail = identity.HasClaim(c => c.Type == "email");
    if (!hasEmail)
    {
        context.Result = new ForbidResult();
        return;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-filters-69957980/?t=1075)

## 5. Custom Authorization Attributes

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/) · 10:37

### 总结

自定义授权特性和集中管理的策略常量通过消除"魔法字符串"提升了 .NET 应用的安全性和可维护性。
通过用强类型的特性或常量替换区分大小写的字符串字面量,开发者可以防止由拼写错误引起的静默授权失败,并让代码的安全意图一目了然。

### 核心概念

*   **Magic Strings(魔法字符串)**:为策略名称使用原始字符串字面量的风险,它们区分大小写,且容易出现拼写错误。
*   **Custom Attributes(自定义特性)**:通过继承 `AuthorizeAttribute` 创建专用特性,以封装特定的策略要求。
*   **Centralized Policies(集中化策略)**:使用一个静态的 `Policies` 类把策略名称定义为常量,提供单一事实来源。
*   **Type Safety(类型安全)**:借助编译器捕获那些否则只会表现为运行时授权失败的错误。
*   **Code Organization(代码组织)**:把特性和策略常量之类的安全基础设施放到专门的命名空间中(例如 `Infrastructure.Attributes`)。

### 课程笔记

ASP.NET Core 中的标准授权通常依赖带有基于字符串的策略名称的 `[Authorize]` 特性。
这种方式虽然简单,但很脆弱,因为策略名称是区分大小写的字符串。

```csharp
public class FinanceController : Controller
{
    [Authorize("finance")]
    public IActionResult Index()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=25)

这些字符串中一个简单的拼写错误就可能导致行为不一致。
例如,使用 "Finance"(首字母大写)或 "finanse"(拼错)代替 "finance",会导致应用在运行时无法应用正确的授权规则,可能阻止合法用户,或暴露受保护的端点。

```csharp
public class FinanceController : Controller
{
    [Authorize("finance")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize("Finance")]
    public IActionResult Account()
    {
        return View();
    }

    [Authorize("finanse")]
    public IActionResult Payments()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=40)

#### 实现自定义特性

为了提升安全性和清晰度,你可以创建一个代表特定策略的自定义授权特性。
通过继承 `AuthorizeAttribute` 并把策略名称传给基类构造函数,你就消除了在控制器层面使用魔法字符串的需要。

```csharp
public class RequireFinancePolicyAttribute : AuthorizeAttribute
{
    public RequireFinancePolicyAttribute() : base("finance")
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=220)

定义好之后,这个特性就可以直接应用到 action 上。
这让代码更易读,并确保授权要求通过类型系统被明确表达出来。

```csharp
[Authorize]
public class SecureController : Controller
{
    [RequireFinancePolicy]
    public IActionResult Finance()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=250)

#### 集中管理策略名称

虽然自定义特性很有效,但在大型应用中为每个策略都创建一个单独的类可能无法扩展。
一种更灵活的方式是把策略名称以常量的形式集中到一个静态类中。
这提供了一个统一的位置来管理控制器和配置中用到的所有策略字符串。

```csharp
namespace StartcodeAuthorization.Infrastructure.Attributes;

public static class Policies
{
    public const string Finance = "finance";
    public const string Sales = "sales";
    public const string Management = "management";
    public const string Admin = "admin";
    public const string Customer = "customer";
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=385)

然后你就可以在标准的 `[Authorize]` 特性中使用这些常量。
这样既保持了类型安全,又避免了创建大量特性类的开销。

```csharp
[Authorize(Policies.Sales)]
public IActionResult Sales()
{
    return View();
}

[Authorize(Policies.Admin)]
public IActionResult Admin()
{
    return View();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=415)

#### 组织安全基础设施

为了更好的可维护性,自定义特性和策略常量应当移到一个专门的位置,例如 `Infrastructure/Attributes` 文件夹。

```csharp
using Microsoft.AspNetCore.Authorization;

namespace StartcodeAuthorization.Infrastructure.Attributes;

public class RequireFinancePolicyAttribute : AuthorizeAttribute
{
    public RequireFinancePolicyAttribute() : base("finance")
    {
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=510)

最后,更新授权服务配置,让它使用同样的这些常量。
这确保了策略在 `Program.cs`(或某个扩展方法)中的定义方式,与它们在控制器中的应用方式保持一致。

```csharp
services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.Finance, policy =>
    {
        policy.Requirements.Add(new FinanceAccessRequirement());
    });

    o.AddPolicy(Policies.Sales, policy =>
        policy.RequireRole("manager", "sales")
              .RequireClaim("country", ["Sweden", "Denmark", "Norway"]));

    o.AddPolicy(Policies.Admin, policy =>
        policy.RequireRole("admin")
              .RequireRole("management"));

    o.AddPolicy(Policies.Customer, policy =>
            policy.RequireAuthenticatedUser());
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/custom-authorization-attributes-69957981/?t=595)

## 6. DefaultPolicy and FallbackPolicy

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/) · 19:55

本课介绍如何在 ASP.NET Core 中实现 `DefaultPolicy` 和 `FallbackPolicy`,以构建一个"默认安全"的应用。
它讲解如何摆脱手动逐个端点保护的方式,转向一种所有端点默认受保护、除非被显式标记为公开的模型。

### 核心概念

*   **Manual Authorization Risks(手动授权的风险)**:依赖开发者记得给每个敏感端点添加 `[Authorize]` 容易出错,而且在大型应用中难以审计。
*   **Limitations of Authorization Filters(授权过滤器的局限)**:基于 MVC 的授权过滤器无法保护其他类型的端点,例如 Minimal APIs、gRPC 服务或 SignalR hub。
*   **DefaultPolicy**:当端点上存在 `[Authorize]` 特性但没有提供具体策略名称时所应用的策略。
*   **FallbackPolicy**:当端点完全没有授权元数据(既没有 `[Authorize]` 也没有 `[AllowAnonymous]` 特性)时所应用的策略。
*   **Secure by Default(默认安全)**:一种所有端点默认都需要认证的策略,要求开发者对公开页面使用 `[AllowAnonymous]` 显式选择退出。

```text
endpoint metadata                        policy applied
---------------------------------------  -------------------------
[Authorize] (no policy name)          -> DefaultPolicy
no [Authorize] and no [AllowAnonymous] -> FallbackPolicy
[AllowAnonymous]                      -> public (explicit opt-out)
```

### 课程笔记

#### 手动授权的问题

在典型的应用中,安全性往往依赖开发者手动给控制器和 action 添加特性。
这种方式会导致常见的错误:由于忘记了某个特性或没有指定策略,敏感数据被暴露在外。

```csharp
public class OrderController : ControllerBase
{
    [Authorize] // No policy specified
    public IActionResult GetOrders()
    {
        return Ok();
    }

    public IActionResult GetPayments()
    {
        return Ok(); // Oops! Unprotected!
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/?t=55)

虽然可以使用授权过滤器来应用全局规则,但它们仅限于 MVC 和 Razor Pages 管道。
它们无法保护绕过 MVC 管道的端点,例如 Minimal APIs 或 gRPC。
为了强制实施真正全局的默认安全方式,ASP.NET Core 在授权中间件中提供了 `DefaultPolicy` 和 `FallbackPolicy`。

#### 实现 FallbackPolicy

`FallbackPolicy` 充当一张安全网。
如果某个端点没有任何授权特性,就会应用回退策略。
通过把这个策略设置为要求已认证用户,应用就变成了默认安全的。

```csharp
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.Admin, policy => policy.RequireRole("management")
                                                .RequireRole("admin"));

    o.AddPolicy(Policies.Customer, policy => policy.RequireAuthenticatedUser());

    o.AddPolicy(Policies.InvoiceOwner, policy =>
        policy.Requirements.Add(new InvoiceOwnershipRequirement()));

    o.FallbackPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/?t=460)

#### 处理重定向循环和静态资源

当应用了一个要求认证的 `FallbackPolicy` 时,它会保护每一个端点,包括主页和登录页。
这可能会导致重定向循环,因为用户必须先通过认证才能到达登录页,但他们又无法到达登录页去完成认证。
要解决这个问题,你必须显式地给公开端点添加 `[AllowAnonymous]` 特性。

```csharp
public class HomeController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/?t=635)

此外,如果应用使用了 `MapStaticAssets()`,静态文件(CSS、JavaScript、图片)会被当作端点对待。
在 `FallbackPolicy` 之下,这些资源同样需要认证,导致未认证用户的 UI 崩坏。
你必须在请求管道中显式允许对静态资源的匿名访问。

```csharp
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets().AllowAnonymous();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/?t=880)

#### 自定义 DefaultPolicy

当存在 `[Authorize]` 特性但没有指定策略名称时,就会触发 `DefaultPolicy`。
默认情况下,这个策略只要求用户已通过认证。
你可以覆盖它以在全局强制实施更严格的要求,例如要求任何标记了 `[Authorize]` 的端点都具备某个特定声明。

```csharp
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.Admin, policy => policy.RequireRole("management")
                                                .RequireRole("admin"));

    o.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    o.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireClaim("JobTitle")
        .Build();
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/defaultpolicy-and-fallbackpolicy-69957982/?t=1060)

有了这个配置,任何用简单的 `[Authorize]` 特性修饰的 action 现在都要求用户既已通过认证,又拥有 `JobTitle` 声明。

## 7. View-based authorization

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/) · 14:37

### 总结

基于视图的授权通过根据用户的权限有条件地渲染 UI 元素来改善用户体验。
虽然后端授权正确地保护了端点,但显示用户无法访问的页面链接会造成糟糕的体验。
通过把 IAuthorizationService 直接注入到 Razor 视图中,开发者可以在渲染过程中评估策略,从而隐藏或显示导航链接和内容。
必须认识到,基于视图的授权是一项提升可用性的 UI 增强,并不能替代在控制器或端点层面进行安全强制的需要。

### 核心概念

- **UX vs. Security(用户体验与安全)**:隐藏 UI 元素可以提升可用性,但它不是安全边界;控制器仍然必须强制执行授权。
- **IAuthorizationService**:在 Razor 视图中用来检查用户是否满足特定策略要求的服务。
- **Conditional Rendering(条件渲染)**:在 Razor 中使用 C# `if` 语句,根据授权结果包裹 HTML 元素。
- **Performance(性能)**:针对角色和声明的授权检查很快(在内存中),但包含数据库调用的自定义处理器如果被反复调用,可能会影响页面加载时间。
- **Optimization(优化)**:如果同一页面上需要多次进行同一项检查,应当只评估一次授权结果并存入变量中。

### 课程笔记

在标准的 .NET 应用中,授权通常在控制器层面强制执行。
虽然这保护了端点,但它常常让 UI 处于这样一种状态:用户能看到他们无权使用的功能的导航链接,从而导致"Access Denied"重定向。

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Features.Home
{
    public class SecureController : Controller
    {
        public SecureController()
        {
        }

        [Authorize(Policy = Policies.Finance)]
        public IActionResult Finance()
        {
            return View();
        }

        [Authorize(Policy = Policies.Sales)]
        public IActionResult Sales()
        {
            return View();
        }

        [Authorize(Policy = Policies.Admin)]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = Policies.Customer)]
        public IActionResult Support()
        {
            return View();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=85)

为了解决这个问题,我们实现基于视图的授权。
我们从一个标准的主页开始,它不管用户身份如何都显示所有链接。

```razor
@{
    ViewData["Title"] = "Home Page";
}

<h1>Authorization Application</h1>

<a href="/Secure/Finance">Finance page</a> (finance role)
<br />

<a href="/Secure/Sales">Sales</a> (Sales or management role)
<br />

<a href="/Secure/Admin">Admin page</a> (admin and management role)
<br />

<a href="/Secure/Support">Support</a> (Authenticated users)
<br />

<a href="/Secure/Contact">Contact us</a> (Anonymous)
<br />
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=235)

为了在视图中启用授权检查,我们必须注入 `IAuthorizationService`,并引入策略所需的命名空间。

```razor
@using Microsoft.AspNetCore.Authorization
@using StartcodeAuthorization.Infrastructure.Attributes
@inject IAuthorizationService authService
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=355)

然后我们可以使用 `AuthorizeAsync` 方法来检查当前 `User` 是否满足某个特定策略。
这次检查的结果是一个 `AuthorizationResult` 对象,它有一个 `Succeeded` 属性。
我们把 HTML 链接包裹在一个 `@if` 块中,确保它们只有在检查通过时才会被渲染。

```razor
@{
    var financeCheck = await authService.AuthorizeAsync(User, Policies.Finance);
}

@if(financeCheck.Succeeded)
{
    <a href="/Secure/Finance">Finance page</a> @:(finance role)
    <br />
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=400)

对其他受保护的链接也重复这种模式,例如 Sales 和 Admin 页面,分别使用 `Policies` 静态类中定义的相应策略。

```razor
@{
    var salesCheck = await authService.AuthorizeAsync(User, Policies.Sales);
}

@if(salesCheck.Succeeded)
{
    <a href="/Secure/Sales">Sales</a> @:(Sales or management role)
    <br />
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=565)

```razor
@{
    var adminCheck = await authService.AuthorizeAsync(User, Policies.Admin);
}

@if(adminCheck.Succeeded)
{
    <a href="/Secure/Admin">Admin page</a> @:(admin and management role)
    <br />
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=590)

对于只要求用户已登录、没有特定策略要求的链接,我们可以直接检查用户身份的 `IsAuthenticated` 属性。

```razor
@if (User.Identity?.IsAuthenticated == true)
{
    <a href="/Secure/Support">Support</a> @: (Authenticated users)
    <br />

    <a href="/Secure/Contact">Contact us</a> @: (Anonymous)
    <br />
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=685)

### 性能考量

在页面渲染期间多次调用授权服务时,性能取决于处理器的复杂度。
角色和声明检查极其快速,因为数据已经在内存中。
然而,执行数据库查找或复杂逻辑的自定义授权处理器,每次调用 `AuthorizeAsync` 时都会执行。

```csharp
public class ActiveUserHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   FinanceAccessRequirement requirement)
    {
        Console.WriteLine("ActiveUserHandler was called");
        var user = context.User;
        bool isActive = IsUserActiveInDatabase(user);

        if(isActive == false)
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }

    private bool IsUserActiveInDatabase(ClaimsPrincipal user) => true;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/view-based-authorization-69957983/?t=820)

每次对授权服务的调用都是独立运行的。
如果同一页面上需要多次进行某项策略检查,应当把结果存入变量并复用,以避免处理器的重复执行。

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/) · 2:13

本课全面回顾了 ASP.NET Core 中的高级授权技术,重点在于授权框架的可扩展性。
它涵盖了自定义需求和处理器的实现、用于细粒度访问控制的基于资源的授权,以及使用 MVC 过滤器管理横切关注点。
此外,本课还强调了自定义特性对代码复用的重要性,以及配置默认策略和回退策略以确保默认安全的应用架构的重要性。

### 核心概念

*   **Requirements and Handlers(需求与处理器)**:使用 `IAuthorizationRequirement` 和 `AuthorizationHandler` 把授权逻辑与策略解耦。
*   **Resource-based Authorization(基于资源的授权)**:在运行时针对具体的数据实例评估授权逻辑。
*   **MVC Authorization Filters(MVC 授权过滤器)**:挂接到 MVC 管道中,在 action 之前或之后执行自定义逻辑。
*   **Custom Authorization Attributes(自定义授权特性)**:创建可复用的声明式规则,以减少代码重复。
*   **Default and Fallback Policies(默认策略与回退策略)**:实现全局安全网,保护那些没有显式定义授权的端点。
*   **View-based Authorization(基于视图的授权)**:基于与控制器中相同的授权逻辑,有条件地渲染 UI 元素。

### 课程笔记

#### 自定义需求和处理器

ASP.NET Core 中的高级授权从需求和处理器开始。
这种方式允许编写超越简单角色或声明检查的自定义逻辑。
需求是一种表示特定授权规则的数据结构,而处理器包含针对用户声明评估该规则的逻辑。

```csharp
using Microsoft.AspNetCore.Authorization;

internal sealed class FinanceAccessHandler : AuthorizationHandler<FinanceAccessRequirement>
{
    private readonly ILogger<FinanceAccessHandler> _logger;

    public FinanceAccessHandler(ILogger<FinanceAccessHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                FinanceAccessRequirement requirement)
    {
        _logger.LogInformation($"Evaluating finance requirement for user '{context.User.Identity?.Name}'");

        var user = context.User;

        var titleOk = user.HasClaim("JobTitle", "finance");

        var countryOk = user.HasClaim("country", "Sweden") ||
                        user.HasClaim("country", "Denmark");

        var hasFinanceRole = user.IsInRole("finance");

        if (hasFinanceRole && titleOk && countryOk)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/?t=9)

#### 基于资源的授权

当授权决策取决于正在访问的具体数据时,就使用基于资源的授权。
例如,用户可能只有在自己是某张发票的所有者时才被允许编辑它。
其实现方式是在控制器 action 中把资源(例如一个 `Invoice` 对象)传给 `IAuthorizationService`。

```csharp
internal class InvoiceOwnershipHandler : AuthorizationHandler<InvoiceOwnershipRequirement, Invoice>
{

    public InvoiceOwnershipHandler()
    {
        Console.WriteLine("InvoiceOwnershipHandler.Constructor");
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   InvoiceOwnershipRequirement requirement,
                                                   Invoice invoice)
    {
        // Get current user ID from claims
        var currentUserIdClaim = context.User.FindFirst("sub")?.Value;

        if (currentUserIdClaim == null)
        {
            // User not authenticated
            return Task.CompletedTask;
        }

        if (int.TryParse(currentUserIdClaim, out int currentUserId))
        {
            // Check if user owns this invoice
            if (invoice.UserId == currentUserId)
            { 
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/?t=19)

#### 授权过滤器

授权过滤器在控制器或 action 级别提供细粒度的控制。
它们允许开发者在不同阶段挂接到 MVC 执行管道中,在 action 之前或之后运行代码,甚至让请求整个短路。
这非常适合日志、验证或动态注入声明之类的横切关注点。

```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace StartcodeAuthorization.Infrastructure.Filters
{
    public class AddClaimsFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User?.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                var newIdentity = new ClaimsIdentity(identity.Claims,
                                                     identity.AuthenticationType,
                                                     identity.NameClaimType,
                                                     identity.RoleClaimType);

                newIdentity.AddClaim(new Claim(type: "request_time",
                                               value: DateTimeOffset.UtcNow.ToString(),
                                               valueType: ClaimValueTypes.String,
                                               issuer: "AddClaimFilter"));

                context.HttpContext.User = new ClaimsPrincipal(newIdentity);
            }
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/?t=30)

#### 自定义授权特性

自定义特性通过创建可复用的声明式标记,简化了授权规则的应用。
通过继承 `AuthorizeAttribute`,开发者可以封装策略名称或其他逻辑,以防止常见错误并减少控制器之间的重复。

```csharp
using Microsoft.AspNetCore.Authorization;

namespace StartcodeAuthorization.Infrastructure.Attributes;

public sealed class RequirePolicyAttribute : AuthorizeAttribute
{
    public RequirePolicyAttribute(string policy) : base(policy) { }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/?t=38)

#### 默认策略与回退策略

默认策略和回退策略充当安全网。
当使用 `[Authorize]` 特性而没有指定具体策略名称时,会应用 `DefaultPolicy`。
`FallbackPolicy` 会应用到应用中每一个没有任何授权特性的端点上,确保默认情况下没有任何端点处于无保护状态。

```csharp
services.AddAuthorization(o =>
{
    o.FallbackPolicy = new AuthorizationPolicyBuilder()
                           .RequireAuthenticatedUser()
                           .Build();
    
    o.DefaultPolicy = new AuthorizationPolicyBuilder()
       .RequireAuthenticatedUser()
       .RequireClaim("JobTitle")
       .Build();

    o.AddPolicy(Policies.Finance, policy =>
    {
        policy.Requirements.Add(new FinanceAccessRequirement());
    });
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/?t=48)

#### 基于视图的授权

基于视图的授权通过有条件地显示或隐藏 UI 元素来改善用户体验。
通过在 Razor 视图中使用同一个 `IAuthorizationService`,UI 可以反映控制器中强制执行的同一套业务逻辑,确保用户获得一致的体验。

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957984/?t=59)

# Authorization

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 9 章
> 共 7 课 · 约 1:34:51
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/) | 12:11 | [↓](#1-introduction) |
| 2 | [Authorization in ASP.NET Core](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-in-aspdotnet-core-69957971/) | 5:36 | [↓](#2-authorization-in-aspnet-core) |
| 3 | [Simple Authorization](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/) | 10:11 | [↓](#3-simple-authorization) |
| 4 | [Role-Based Auth](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/) | 18:39 | [↓](#4-role-based-auth) |
| 5 | [Policy-Based Auth](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/) | 26:05 | [↓](#5-policy-based-auth) |
| 6 | [Broken Access Control](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/broken-access-control-69957975/) | 21:06 | [↓](#6-broken-access-control) |
| 7 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957976/) | 1:03 | [↓](#7-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/) · 12:11

### 总结

授权(Authorization)是确定一个已认证用户在应用程序中被允许做什么的过程。
本课介绍认证与授权之间的区别,探讨基于角色的访问控制(RBAC)和基于属性的访问控制(ABAC),并演示为什么在业务逻辑中手动进行授权检查难以维护和扩展。

### 核心概念

*   **认证 vs. 授权**:认证识别用户("你是谁?"),而授权确定其权限("你被允许做什么?")。
*   **基于角色的访问控制(RBAC)**:一种把权限分组为角色(例如 Admin、Finance)并分配给用户的系统。虽然简单,但随着业务需求增长,它可能导致"角色爆炸"和维护难题。
*   **基于属性的访问控制(ABAC)**:一种更现代、更灵活的方式,访问决策基于特定的用户属性或 claim(例如 Department、Clearance Level)做出。
*   **手动授权的陷阱**:直接在控制器中实现授权检查会导致代码重复、逻辑脆弱以及关注点分离不佳。

### 课程笔记

#### The Concept of Authorization

认证和授权是保护应用程序的两道不同的守卫。
认证确立身份,但它并不定义权限。
如果没有授权,每个已认证用户都会拥有相同级别的访问权限,而这很少是我们想要的。
一个安全的应用程序两者都需要:一个用于验证用户,另一个用于验证其访问特定资源或执行删除数据、付款等操作的权限。

#### Implementation Approaches

在 .NET 中实现授权主要有两种方式:

1.  **基于角色的访问控制(RBAC)**:定义诸如 "Employee"、"Finance" 或 "Management" 之类的角色。用户被分配一个或多个角色,每个角色代表一组权限。
2.  **基于属性的访问控制(ABAC)**:为用户分配属性(claim),例如部门或安全许可级别。访问决策根据这些属性进行评估,为复杂的业务需求提供更细粒度、适应性更强的系统。

#### Application Infrastructure

示例应用程序使用标准的 ASP.NET Core 设置。
认证使用 cookie 认证处理程序进行配置,数据则通过注册在依赖注入容器中的简单数据库类进行管理。

```csharp
using Microsoft.AspNetCore.Mvc.Razor;
using StartcodeAuthorization.Features.Invoices;
using StartcodeAuthorization.Features.Users;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/users/AccessDenied";
    o.LoginPath = "/users/login";
});

builder.Services.AddSingleton<IUserDatabase, UserDatabase>();
builder.Services.AddSingleton<IInvoiceDatabase, InvoiceDatabase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/?t=535)

#### Data and Controllers

在实现开始时,`SecureController` 包含若干端点(Finance、Sales、Admin 等),它们目前都是公开的、未受保护的。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Secure;

public class SecureController : Controller
{
    public SecureController()
    {
    }

    public IActionResult Finance()
    {
        return View();
    }

    public IActionResult Sales()
    {
        return View();
    }

    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/?t=340)

用户数据和 claim 存储在一个 `UserDatabase` 类中。
每个用户都关联着一组 claim,应用程序用它们来识别用户,并最终对其操作进行授权。

```csharp
using StartcodeAuthorization.Features.UserProfile;
using System.Security.Claims;

namespace StartcodeAuthorization.Features.Users;

public class UserDatabase : IUserDatabase
{
    private readonly static List<User> users;

    static UserDatabase() => users =
        [
            new()
            {
                UserID = 1,
                Website = "https://alice.dev",
                Country = "Canada",
                Claims =
                [
                    new("sub", "1"),
                    new("name", "Alice Svensson")
                ]
            },
            new()
            {
                UserID = 2,
                Website = "https://bobsite.com",
                Country = "Sweden",
                Claims =
                [
                        new("sub", "2"),
                        new("name", "Bob Andersson")
                ]
            },
            // ... additional users
        ];
    // ... methods for retrieval and updates
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/?t=470)

类似地,一个 `InvoiceDatabase` 管理特定于用户的发票数据。

```csharp
namespace StartcodeAuthorization.Features.Invoices
{
    public class InvoiceDatabase : IInvoiceDatabase
    {
        private static readonly List<Invoice> _invoices =
            [
                new Invoice { Id = 1, UserId = 1, InvoiceNumber = "INV-001", Description = "Office Supplies", Amount = 250.00m, InvoiceDate = DateTime.Now.AddDays(-10), Status = "Paid" },
                new Invoice { Id = 2, UserId = 1, InvoiceNumber = "INV-002", Description = "Software License", Amount = 1200.00m, InvoiceDate = DateTime.Now.AddDays(-5), Status = "Pending" },
                // ... additional invoices
            ];

        public Invoice? GetInvoiceById(int id)
        { 
            return _invoices.FirstOrDefault(i => i.Id == id);
        }

        public List<Invoice> GetInvoicesByUserId(int userId)
        { 
            return _invoices.Where(i => i.UserId == userId).ToList();
        }

        public List<Invoice> GetAllInvoices()
        { 
            return _invoices.ToList();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/?t=505)

#### Manual Authorization Anti-patterns

不建议在 action 方法内部手动实现授权。
这种方式把授权逻辑与业务逻辑混在一起,使代码难以维护和扩展。

在下面的第一个示例中,代码手动检查用户是否已认证且属于某个特定角色,然后执行一个硬编码的重定向。
在第二个示例中,多个 claim(role、department 和 clearance)在一个复杂的条件中被检查。
这种模式会导致应用程序中出现脆弱且重复的代码。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.ManualAuthorization;

public class ManualAuthExampleController : Controller
{
    public IActionResult Example1()
    {
        if (User?.Identity?.IsAuthenticated == true &&
            User.IsInRole("developer"))
        {
            return View();
        }
        else
        {
            return Redirect("/users/AccessDenied");
        }
    }

    public IActionResult Example2(int orderId)
    {
        var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
        var departmentClaim = User.Claims.FirstOrDefault(c => c.Type == "department");
        var clearanceClaim = User.Claims.FirstOrDefault(c => c.Type == "clearance");

        if (User.Identity != null &&
            User.Identity.IsAuthenticated &&
            roleClaim != null && roleClaim.Value == "developer" &&
            departmentClaim != null && departmentClaim.Value.ToLower() == "engineering" &&
            clearanceClaim != null && (clearanceClaim.Value == "high" ||
                                       clearanceClaim.Value == "very-high"))
        {
            var order = LoadOrderFromDatabase(orderId);

            return View(order);
        }

        return Forbid();
    }

    private static string? LoadOrderFromDatabase(int orderId)
    {
        return orderId.ToString();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957970/?t=595)

## 2. Authorization in ASP.NET Core

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-in-aspdotnet-core-69957971/) · 5:36

ASP.NET Core 提供了一个内置的授权系统,它与认证协同工作,以控制对应用程序资源的访问。
该系统由两部分组成:承载逻辑和规则的授权服务,以及在管道中评估请求的授权中间件。
根据用户的身份和权限,中间件决定是转发请求、质询用户以获取凭据,还是完全禁止访问。

### 核心概念

- **Authorization Service**:注册在依赖注入容器中的服务,存储授权规则和逻辑。
- **Authorization Middleware**:在请求管道中于认证之后运行的中间件,用于决定请求是否应当继续。
- **Forward**:中间件允许请求继续的结果,因为要求已满足或根本不存在要求。
- **Challenge**:由于端点需要授权,未认证用户被重定向或被提示进行认证的结果。
- **Forbid**:已认证用户因缺少必要权限而被拒绝访问的结果。
- **Pipeline Order**:在中间件管道中,授权必须始终发生在认证之后,以确保在检查权限之前已知道用户的身份。

### 课程笔记

ASP.NET Core 的授权系统集成在请求管道中,用于管理访问控制。
这一过程依赖于认证中间件和授权中间件之间的交互。
这种设计遵循关注点分离原则:认证识别用户,授权确定用户被允许做什么。

#### Authorization Outcomes

当授权中间件处理一个请求时,它会做出以下三种决定之一:

1. **Forward**:在正常情况下,中间件把请求转发给管道中的下一个组件(例如 MVC 中间件)。如果端点上没有授权要求,或者用户成功通过了所有要求,就会发生这种情况。
2. **Challenge**:如果端点需要授权但用户未认证,中间件会终止请求并发送一个 challenge 响应。这会指示认证中间件去质询用户(例如重定向到登录页面)。
3. **Forbid**:如果用户已认证,但没有访问所请求资源所需的权限,中间件会终止请求并发送一个 forbid 响应。这通常由认证中间件处理,以显示一条拒绝访问的消息。

```text
request --> UseAuthentication() --> UseAuthorization()
                                        |
          +-----------------------------+-----------------------+
          |                             |                       |
       Forward                      Challenge                 Forbid
  (无要求或通过所有要求)            (未认证)          (已认证但缺少权限)
          |                             |                       |
          v                             v                       v
  next component (MVC)          redirect to login       access denied
```

#### Implementation

要启用授权,必须在 `Program.cs` 文件中注册该服务。
这通过在服务集合上调用 `AddAuthorization` 来完成。
该服务包含用于评估访问的核心逻辑和规则。

```csharp
// Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/users/AccessDenied";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-in-aspdotnet-core-69957971/?t=265)

可以使用 lambda 表达式自定义配置,以定义特定的授权策略,这些内容将在课程后面探讨。

```csharp
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(o =>
{
    // Policy configuration
});

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/users/AccessDenied";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-in-aspdotnet-core-69957971/?t=280)

注册服务之后,必须使用 `UseAuthorization()` 把授权中间件添加到请求管道中。
中间件的顺序至关重要:`UseAuthorization()` 必须出现在 `UseAuthentication()` 之后,因为系统需要先知道用户是谁,才能确定用户被允许做什么。

```csharp
app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authorization-in-aspdotnet-core-69957971/?t=300)

## 3. Simple Authorization

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/) · 10:11

### 总结

ASP.NET Core 中的简单授权主要通过 [Authorize] 和 [AllowAnonymous] 特性来管理。
这些特性让开发者可以限制对整个控制器或特定 action 方法的访问,确保只有已认证用户才能访问受保护的资源,同时在必要之处保持公开访问。
当未认证用户尝试访问受保护的资源时,中间件会自动质询该请求,通常会按照应用程序认证配置中的定义把用户重定向到登录页面。

### 核心概念

*   **[Authorize] 特性**:仅允许已认证用户访问。
*   **[AllowAnonymous] 特性**:覆盖授权要求,允许公开访问。
*   **类级别 vs. 方法级别**:特性可以应用于整个控制器或单个 action。
*   **特性覆盖**:方法级别的特性优先于类级别的特性。
*   **Challenge 与重定向**:未认证时尝试访问受保护资源,会触发重定向到配置好的登录路径。

### 课程笔记

ASP.NET Core 中的授权确定一个已识别的用户被允许做什么。
最基本的授权形式是确保用户在访问应用程序的特定部分之前已经通过认证。

#### The Authorize Attribute

`[Authorize]` 特性是基础访问控制的主要工具。
应用它之后,用户必须登录才能访问该资源。
它可以应用在两个级别:

1.  **类级别**:把 `[Authorize]` 应用到控制器类上,会保护该控制器中的每一个 action 方法。
2.  **Action 级别**:把 `[Authorize]` 应用到某个特定方法上,只保护那一个端点。

```csharp
// All actions require authentication
[Authorize]
public class SecureController : Controller
{
    // Authorization required
    public IActionResult Index()
    {
        return View();
    }

    // Authorization required
    public IActionResult About()
    {
        return View();
    }
}

// Anonymous access by default
public class SecureController : Controller
{
    // Authorization required
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    // Anonymous access
    public IActionResult About()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=100)

#### Overriding Authorization with AllowAnonymous

有些场景下,控制器整体是受限的,但特定页面必须保持公开(例如 Contact 页面)。
`[AllowAnonymous]` 特性用于覆盖类级别的 `[Authorize]` 限制。
反过来,如果一个控制器被标记为 `[AllowAnonymous]`(或者没有任何特性),可以在特定方法上使用 `[Authorize]` 来保护它们。

**关键规则是:方法级别的特性总是覆盖类级别的特性。**

```csharp
// All actions require authentication
[Authorize]
public class SecureController : Controller
{
    // This action is secured
    public IActionResult Index()
    {
        return View();
    }

    // Overrides [Authorize]
    [AllowAnonymous]
    public IActionResult About()
    {
        return View();
    }
}

// Any user can access this controller
[AllowAnonymous]
public class SecureController: Controller
{
    // Overrides [AllowAnonymous]
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    // Anonymous access
    public IActionResult About()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=145)

#### Implementation Example

在典型的应用程序中,你可能有一个控制器,包含各种不应公开的管理类或敏感 action。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Secure;

public class SecureController : Controller
{
    public SecureController()
    {
    }

    public IActionResult Finance()
    {
        return View();
    }

    public IActionResult Sales()
    {
        return View();
    }

    public IActionResult Admin()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=250)

要保护某个特定 action,例如 `Admin` 页面,就把 `[Authorize]` 特性直接应用到该方法上:

```csharp
    public IActionResult Finance()
    {
        return View();
    }

    public IActionResult Sales()
    {
        return View();
    }

    [Authorize]
    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=285)

#### Middleware Behavior and Redirection

当未认证用户尝试访问一个标注了 `[Authorize]` 的方法时,认证中间件会质询该请求。
对于基于 cookie 的认证,这会产生一个指向登录页面的 `302 Found` 重定向。
重定向 URL 通常包含一个 `ReturnUrl` 参数,以便用户在成功登录后被送回其原本的目的地。

这一行为在 `Program.cs` 文件的认证服务设置中配置:

```csharp
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

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=340)

#### Protecting Entire Controllers

要一次性保护控制器中的所有 action,就把 `[Authorize]` 特性移到类级别。
对于敏感的控制器,这是一种更安全的"默认关闭"(default-close)方式。

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthorization.Features.Secure;

[Authorize]
public class SecureController : Controller
{
    public SecureController()
    {
    }

    public IActionResult Finance()
    {
        return View();
    }

    public IActionResult Sales()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=440)

如果这个受保护控制器中的某个特定 action 需要公开,就在该方法上应用 `[AllowAnonymous]`:

```csharp
    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Contact()
    {
        return View();
    }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/simple-authorization-69957972/?t=505)

虽然这些特性对于基础访问控制很强大,但它们只验证用户是否已登录。
它们不区分不同类型的用户(例如 Admin 与 Employee),这需要由角色和策略等更高级的授权技术来处理。

## 4. Role-Based Auth

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/) · 18:39

基于角色的授权不仅能精确控制谁可以访问应用程序,还能根据用户被分配的角色(例如 Admin、Finance、Management)精确控制他们被允许执行哪些具体操作。

### 核心概念

- **[Authorize(Roles = "...")]**:用于把控制器或 action 的访问限制为特定角色的主要特性。
- **安全默认值**:在控制器级别应用 `[Authorize]`,可确保所有 action 默认受到保护。
- **OR 逻辑**:单个 `[Authorize]` 特性配合逗号分隔的角色列表(例如 `"Admin,Manager"`),只要用户拥有其中*任意*一个角色即可访问。
- **AND 逻辑**:在单个 action 上叠加多个 `[Authorize]` 特性,要求用户拥有*所有*指定的角色。
- **角色 Claim 类型**:ASP.NET Core 期望角色使用特定的 claim 类型;如果应用程序使用自定义名称(例如 "role"),就必须配置 `ClaimsIdentity` 以识别它。
- **授权要求(Requirements)**:在内部,框架把特性转换为要求对象(例如 `RoleAuthorizationRequirement`)以进行评估。

### 课程笔记

要实现基于角色的安全,你可以在类级别应用 `[Authorize]` 特性来建立安全的默认值,然后在方法级别使用 `Roles` 属性细化访问。

```csharp
[Authorize]
public class SecureController : Controller
{
    [Authorize(Roles = "admin")]
    public IActionResult Admin()
    {
        return View();
    }

    [Authorize(Roles = "finance")]
    public IActionResult Finance()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/?t=25)

#### Logical Operators in Roles

在处理多个角色时,.NET 提供了两种不同的组合方式,它们的行为不同:

1.  **OR 条件**:使用逗号分隔的字符串。用户需要拥有 admin 角色或 management 角色之一。
2.  **AND 条件**:叠加多个特性。用户必须同时拥有 admin 角色和 management 角色。

```csharp
[Authorize]
public class SecureController : Controller
{
    // Must be admin OR management
    [Authorize(Roles = "admin,management")]
    public IActionResult Admin()
    {
        return View();
    }
}

[Authorize]
public class SecureController : Controller
{
    // Must be admin AND management
    [Authorize(Roles = "admin")]
    [Authorize(Roles = "management")]
    public IActionResult AdminManager()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/?t=115)

#### Configuring Role Claims

在真实的应用程序中,角色以 claim 的形式存储在用户的身份中。
如果你的身份提供者或数据库使用像 "role" 这样的简单字符串作为 claim 类型,ASP.NET Core 默认可能无法识别它,因为它期望角色 claim 使用完整的 SOAP schema URI。

```csharp
new()
{
    UserID = 3,
    Website = "https://finance.com",
    Country = "Sweden",
    Claims =
    [
        new("sub", "3"),
        new("name", "Finance Andersson"),
        new("email", "finance@example.com"),
        new("country", "Sweden"),
        new("JobTitle", "finance"),
        new("role", "finance")
    ]
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/?t=490)

要修复框架无法识别 "role" claim 的不匹配问题,你必须在登录过程中创建 `ClaimsIdentity` 时指定 `roleType` 参数。
这会告诉 .NET 在评估基于角色的授权时究竟要查找哪个 claim 名称。

```csharp
[HttpPost]
public async Task<IActionResult> Login(int id)
{
    var userProfile = userDatabase.GetUserProfileById(id);

    if (userProfile != null)
    {
        var identity = new ClaimsIdentity(
            claims: userProfile.Claims,
            authenticationType: "TestAuth",
            nameType: "name",
            roleType: "role");

        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties()
        {
            RedirectUri = "/"
        };
        // ... sign in logic
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/?t=550)

#### Internal Mechanics: Requirements

ASP.NET Core 使用端点路由来映射请求。
在内部,每个 `[Authorize]` 特性都会被转换为一个或多个**要求(Requirements)**。
例如:
- 一个基本的 `[Authorize]` 特性会变成 `DenyAnonymousAuthorizationRequirement`。
- 一个 `[Authorize(Roles = "admin")]` 特性会变成 `RoleAuthorizationRequirement`。

与某个端点关联的所有要求都必须满足,才会授予访问权限。
这可以通过内置的端点调试器和授权工具来检查,它们会显示应用于当前路由的元数据和要求。

```text
[Authorize]                  --> DenyAnonymousAuthorizationRequirement
[Authorize(Roles = "admin")] --> RoleAuthorizationRequirement

endpoint: 所有要求都必须满足 --> 授予访问权限
```

```csharp
[Authorize(Roles ="finance")]
public IActionResult Finance()
{
    return View();
}

[Authorize(Roles = "sales, management")]  //OR
public IActionResult Sales()
{
    return View();
}

[Authorize(Roles = "management")] // AND
[Authorize(Roles = "admin")]
public IActionResult Admin()
{
    return View();
}

public IActionResult Support()
{
    return View();
}

[AllowAnonymous]
public IActionResult Contact()
{
    return View();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/role-based-auth-69957973/?t=1105)

## 5. Policy-Based Auth

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/) · 26:05

基于策略的授权为传统的基于角色的授权提供了一种更灵活、更易维护的替代方案。
通过把授权规则与应用程序代码分离,策略使集中管理和复杂的逻辑组合成为可能。

### 核心概念

*   **解耦**:授权规则集中定义,而不是硬编码在控制器特性中。
*   **策略定义**:策略在应用程序启动期间的 `AddAuthorization` 部分中配置。
*   **要求(Requirements)**:策略由一个或多个要求组成(例如角色、claim 或自定义断言)。
*   **要求评估**:授权系统根据当前用户评估一组要求对象,以授予或拒绝访问。
*   **RequireAssertion**:一种使用布尔表达式定义自定义授权逻辑的方法。
*   **Fluent API**:`AddAuthorizationBuilder` 提供了一种现代的、流式的语法来注册策略。

### 课程笔记

#### Limitations of Role-Based Authorization

基于角色的授权适用于简单场景,但随着应用程序增长会变得难以维护。
它通常依赖于散布在整个代码库中的"魔法字符串"。
大小写不一致(例如 "finance" 与 "Finance")可能导致意料之外的授权失败。
此外,如果某个角色被重命名,就必须在整个代码库中搜索并更新。
基于角色的特性在组合检查方面的灵活性也很有限,例如同时要求某个特定角色和某个特定 claim。

```csharp
[Authorize]
public class SecureController : Controller
{
    [Authorize(Roles = "finance")]
    public IActionResult Page1()
    {
        return View();
    }

    [Authorize(Roles = "Finance")]
    public IActionResult Page2()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/?t=25)

#### Defining and Using Policies

基于策略的授权通过集中管理规则来解决这些问题。
一个策略把所有授权要求集中在一处,而应用程序代码按名称引用该策略。
这意味着控制器不需要知道具体的规则;它只需要知道应用哪个策略。

策略在 `Program.cs` 中的 `AddAuthorization` 配置里定义。
你可以要求角色、特定的 claim,或者要求用户已认证。

```csharp
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("management", policy =>
        policy.RequireRole("management")
            .RequireClaim("email")
            .RequireClaim("country", ["Sweden", "Denmark"])
            .RequireClaim("JobTitle", "Manager"));

    o.AddPolicy("sales", policy =>
        policy.RequireRole("management", "sales"));

    o.AddPolicy("customer", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/?t=160)

要使用这些策略,就在 `[Authorize]` 特性中指定策略名称。
这让控制器代码保持整洁,并确保当要求发生变化时,只需要更新策略定义。

```csharp
[Authorize]
public class SecureController : Controller
{
    [Authorize(Policy = "management")]
    public IActionResult Admin()
    {
        return View();
    }

    [Authorize(policy: "sales")]
    public IActionResult Sales()
    {
        return View();
    }

    [Authorize("customer")]
    public IActionResult Support()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/?t=205)

#### Under the Hood: Requirements

使用策略时,授权服务按名称查找策略,并生成一组要求对象。
例如,一个 "admin" 策略可能会生成一个认证要求,以及另一个 "admin" 角色要求。
然后这些要求会针对当前用户进行评估。
这与基于角色的特性所使用的底层过程相同,但策略允许集中定义和更新这些要求。

```text
[Authorize(Policy = "admin")]
        |
        v
authorization service --(按名称查找)--> policy "admin"
        |
        v
requirements: [认证要求] [role "admin"]
        |
        v
针对当前用户评估 --> 授予 / 拒绝访问
```

```csharp
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("finance", policy => policy.RequireRole("finance"));
    
    o.AddPolicy("sales", policy => policy.RequireRole("management", "sales"));
    
    o.AddPolicy("admin", policy => policy.RequireRole("management")
                                        .RequireRole("admin"));
    
    o.AddPolicy("customer", policy => policy.RequireAuthenticatedUser());
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/?t=730)

#### Modern Syntax: AddAuthorizationBuilder

ASP.NET Core 还提供了一种更新的、流式风格的 API,使用 `AddAuthorizationBuilder` 来注册策略。
虽然经典语法仍然很常见,但 builder 模式提供了一种更清晰的方式来链式定义策略。

```csharp
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("finance", policy => policy.RequireRole("finance"))
    .AddPolicy("sales", policy => policy.RequireRole("management", "sales"))
    .AddPolicy("admin", policy => policy.RequireRole("management")
                                         .RequireRole("admin"))
    .AddPolicy("customer", policy => policy.RequireAuthenticatedUser());
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/?t=850)

#### Advanced Logic with RequireAssertion

对于需要内置辅助方法无法处理的复杂逻辑的场景,`RequireAssertion` 允许你定义自定义授权逻辑。
该断言接收一个 `AuthorizationHandlerContext`,让你可以访问用户的 claim、角色以及正在被访问的资源。
你只需返回 `true` 以允许访问,或返回 `false` 以拒绝访问。

```csharp
o.AddPolicy("finance", policy => policy.RequireAssertion(context =>
{
    var user = context.User;

    var titleOk = user.HasClaim(claim => claim.Type == "JobTitle" &&
                                         claim.Value == "finance");

    var countryOk = user.HasClaim(claim => claim.Type == "country" &&
                                           (claim.Value == "Sweden" ||
                                            claim.Value == "Denmark"));

    var rolesOk = user.IsInRole("finance") || user.IsInRole("management");

    return titleOk && countryOk && rolesOk;
}));
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/policy-based-auth-69957974/?t=1525)

## 6. Broken Access Control

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/broken-access-control-69957975/) · 21:06

失效的访问控制(Broken Access Control)是当今 Web 上最常见的安全问题,并导致了大量数据泄露。
它目前在 OWASP Top 10 中排名第一,OWASP Top 10 是由开放 Web 应用程序安全项目(OWASP)发布的关键安全问题列表。

### 核心概念

*   **OWASP Top 10**:一份被广泛认可的、列出 Web 应用程序最关键安全风险的清单。
*   **垂直授权(Vertical Authorization)**:根据用户的权限级别(例如 Guest 与 Admin),控制用户是否被允许执行某个特定操作。
*   **水平授权(Horizontal Authorization)**:控制用户是否被允许访问某个特定的资源实例(例如用户 A 访问用户 B 的订单)。
*   **最小权限原则**:只授予用户完成工作所需的最少权限的安全实践。
*   **Step-up Authentication**:一种机制,用户默认以有限的权限操作,必须重新认证才能获得临时的提升权限来执行敏感任务。
*   **MVC Mass Assignment**:一种漏洞,ASP.NET Core MVC 模型绑定器会自动把请求参数映射到本不应由用户修改的模型属性上。

### 课程笔记

#### Vertical vs. Horizontal Authorization

授权通常分为两类:垂直授权和水平授权。

**垂直授权**回答的问题是:"用户是否被允许执行这个操作?"
当一个低权限用户(例如访客)成功执行了一个面向更高权限级别(例如管理员)的命令时,就发生了垂直授权失效。
这通常被形象地比作爬梯子到达更高的访问级别。

**水平授权**回答的问题是:"用户是否被允许访问这个特定的资源?"
当某个权限级别的用户访问或修改了属于同一级别其他用户的数据时,就发生了水平授权失效。
例如,如果一个用户只需在 GET 请求中修改订单 ID 就能查看另一个用户的订单,那么水平授权就是失效的。

在安全的应用程序中,每个请求都必须执行这两种检查。
垂直授权通常先被检查,因为它执行起来更快。
如果跳过其中任何一个检查,就会产生安全漏洞。

```text
  Admin  ^
         |  垂直授权:是否被允许执行这个操作?
  Guest  |
         +---------------------------------------->
            User A  <-->  User B
            水平授权:是否被允许访问这个特定资源?
```

#### Principle of Least Privilege and Step-up Authentication

最小权限原则确保:如果某个账户被攻破,潜在的损害是有限的。
应用程序可以使用 **Step-up Authentication**,而不是授予永久的管理权限。
在这种模型中,用户在日常工作中拥有普通权限,但必须再次认证(使用硬件密钥、应用或同事审批)才能执行管理任务。
这种提升后的访问权限随后会在有限的时长内(例如 10 分钟)被授予,之后恢复正常。

#### MVC Mass Assignment Attack

在 ASP.NET Core MVC 中,失效访问控制的一种常见表现是 Mass Assignment 攻击。
当一个端点接受的模型同时包含安全字段和敏感字段时,就会发生这种情况。

考虑一个用户资料更新功能。
应用程序使用一个 `UpdateUserModel` 来绑定传入的请求数据:

```csharp
namespace StartcodeAuthorization.Features.UserProfile;

public class UpdateUserModel
{
    public string? Name { get; set; }
    public List<string>? Roles { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/broken-access-control-69957975/?t=985)

`UserProfileController` 在一个 POST action 中使用这个模型来更新用户信息:

```csharp
[HttpPost]
public IActionResult UpdateUser(UpdateUserModel updateUser)
{
    var userIdClaim = User.FindFirst("sub")?.Value;

    if (!int.TryParse(userIdClaim, out var userId))
    {
        return BadRequest("Invalid user ID.");
    }

    userDatabase.UpdateUser(userId, updateUser);
    return RedirectToAction("Index");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/broken-access-control-69957975/?t=910)

在这个实现中,`UpdateUserModel` 同时绑定 `Name` 和 `Roles`。
即使用户界面只显示了 `Name` 字段,攻击者也可以使用 Fiddler 之类的工具拦截请求,并添加一个 `roles` 参数(例如 `roles=admin`)。
由于模型绑定器会自动把任何匹配的输入数据映射到模型属性上,`Roles` 列表会被填充并持久化到数据库中。
这使得普通用户可以提升自己的权限。

```text
UI form:        Name=...
attacker adds:  roles=admin   (Fiddler)
                    |
                    v
model binder --> UpdateUserModel { Name, Roles = [admin] }
                    |
                    v
userDatabase.UpdateUser(userId, updateUser) --> 持久化到数据库
```

#### Mitigation Strategies

为了防止这些攻击,必须在后端强制执行安全,而不是依赖 UI 上的限制:

1.  **使用严格的模型**:不要使用宽泛的模型,而是使用只包含用户被允许修改的字段的特定数据传输对象(DTO)(例如只包含 `Name` 属性的 `UpdateUserNameModel`)。
2.  **显式授权检查**:任何敏感操作,例如修改角色,都必须要求进行显式的授权检查,以验证当前用户有权进行该特定更改。
3.  **关注点分离**:把简单的用户操作(例如更新显示名称)与安全敏感的操作(例如管理角色)分离到不同的端点中,并为其设置不同的访问要求。

## 7. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957976/) · 1:03

### 总结

本课全面回顾了 ASP.NET Core 中的授权,强调认证(身份)与授权(权限)之间的区别。
它回顾了框架的响应机制(针对未认证用户的 Challenge 和针对未授权用户的 Forbid),并详细介绍了如何通过特性、角色和复杂策略来实现访问控制。
最后,它强调了与失效访问控制相关的安全风险。

### 核心概念

- 认证与授权之间的区别。
- 授权响应:Challenge 与 Forbid。
- 使用 `[Authorize]` 和 `[AllowAnonymous]` 特性的简单授权。
- 基于角色和基于策略的授权。
- 失效访问控制的安全影响。

### 课程笔记

ASP.NET Core 中的授权是确定一个已认证用户是否拥有访问特定资源或执行某个操作所需权限的过程。
这与认证不同,认证只关心识别用户是谁。

当对受保护资源发出请求时,授权中间件会评估用户的身份。
如果用户未认证,系统会发出 **Challenge**,通常会把用户重定向到登录页面。
如果用户已认证但不满足特定的授权要求,系统会发出 **Forbid** 响应,通常会导向一个 "Access Denied" 页面。

简单授权使用 `[Authorize]` 和 `[AllowAnonymous]` 特性来实现。
它们可以应用在控制器或 action 级别,以强制执行基本的访问要求。

```csharp
[Authorize]
public class SecureController : Controller
{
    public SecureController()
    {
    }

    [Authorize("finance")]
    public IActionResult Finance()
    {
        return View();
    }

    [Authorize("sales")]
    public IActionResult Sales()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    public IActionResult Admin()
    {
        return View();
    }

    public IActionResult Support()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Contact()
    {
        return View();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957976/?t=28)

为了实现更细粒度的控制,ASP.NET Core 支持基于角色和基于策略的授权。
策略允许实现复杂的逻辑,例如检查多个 claim 或使用自定义断言,这些都在应用程序的服务容器中配置。

```csharp
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("finance", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            var titleOk = user.HasClaim(claim => claim.Type == "JobTitle" &&
                                                 claim.Value == "finance");

            var countryOk = user.HasClaim(claim => claim.Type == "country" &&
                                                  (claim.Value == "Sweden" ||
                                                   claim.Value == "Denmark"));

            var rolesOk = user.IsInRole("finance") ||
                          user.IsInRole("management");

            return rolesOk && titleOk && countryOk;
        }));

    o.AddPolicy("sales", policy =>
        policy.RequireRole("manager", "sales")
              .RequireClaim("country", ["Sweden", "Denmark","Norway"]));

    o.AddPolicy("admin", policy =>
        policy.RequireRole("admin")
              .RequireRole("management"));

    o.AddPolicy("customer", policy =>
            policy.RequireAuthenticatedUser());
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957976/?t=37)

最后,理解并防范**失效访问控制**至关重要。
这包括确保授权检查被一致地应用,并且用户无法绕过安全逻辑去访问他们无权使用的数据或功能。
action 方法中的手动授权检查有时可以用来处理声明式特性无法覆盖的复杂场景。

```csharp
public IActionResult Example2(int orderId)
{
    var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
    var departmentClaim = User.Claims.FirstOrDefault(c => c.Type == "department");
    var clearanceClaim = User.Claims.FirstOrDefault(c => c.Type == "clearance");

    if (User.Identity != null &&
        User.Identity.IsAuthenticated &&
        roleClaim != null && roleClaim.Value == "developer" &&
        departmentClaim != null && departmentClaim.Value.ToLower() == "engineering" &&
        clearanceClaim != null && (clearanceClaim.Value == "high" ||
                                   clearanceClaim.Value == "very-high"))
    {
        var order = LoadOrderFromDatabase(orderId);

        return View(order);
    }

    return Forbid();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957976/?t=45)

# Fundamentals

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 3 章
> 共 4 课 · 约 50:17
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [The User Object](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-user-object-69957931/) | 12:28 | [↓](#1-the-user-object) |
| 2 | [Creating a ClaimsPrincipal](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/) | 16:28 | [↓](#2-creating-a-claimsprincipal) |
| 3 | [The Request Pipeline](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/) | 19:08 | [↓](#3-the-request-pipeline) |
| 4 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957934/) | 2:13 | [↓](#4-section-recap) |

## 1. The User Object

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-user-object-69957931/) · 12:28

### 总结

在 ASP.NET Core 中,User 对象代表当前的请求者,其类型为 ClaimsPrincipal。
该对象可以通过控制器和 Razor 视图中的 User 属性访问,它是 HttpContext.User 的一个快捷方式。
一个 ClaimsPrincipal 包含一组 ClaimsIdentity 对象,这些对象存储关键的身份验证细节,例如用户是否已通过身份验证、所使用的身份验证方式,以及一组代表用户属性的 claim。
开发者通常与主身份(primary identity)交互,以获取用户信息、通过 IsInRole 检查角色,以及访问特定的 claim。

### 核心概念

- **ClaimsPrincipal**:ASP.NET Core 中当前用户的基类,实现了 IPrincipal 接口。
- **ClaimsIdentity**:与用户关联的一个身份;一个 principal 可以拥有多个身份。
- **User 属性**:ControllerBase 和 Razor 视图中可用的一个属性,用于访问当前的 ClaimsPrincipal。
- **IsAuthenticated**:身份上的一个布尔属性,表示用户是否已成功通过身份验证。
- **AuthenticationType**:一个字符串,标识用于对用户进行身份验证的机制(例如 "Cookies"、"Bearer"、"Windows")。
- **Claims**:代表用户特定事实的键值对(例如姓名、电子邮件、角色)。

### 课程笔记

在 ASP.NET Core 中,每个控制器和 Razor 视图都提供对 `User` 属性的访问。
该属性代表发出请求的当前用户。
从技术上讲,`User` 属性是 `HttpContext.User` 的一个快捷方式,定义在 `ControllerBase` 类中。

```csharp
public class Controller
{
    public ClaimsPrincipal User => HttpContext?.User!;
    // ...
}

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var user1 = User;
        var user2 = HttpContext.User;
        // user1 and user2 are identical
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-user-object-69957931/?t=55)

#### ClaimsPrincipal 的结构

`User` 对象的类型是 `ClaimsPrincipal`。
在内部,一个 `ClaimsPrincipal` 包含一个 `ClaimsIdentity` 对象列表。
虽然该架构支持多个身份,但大多数应用程序只使用一个。
每个 `ClaimsIdentity` 包含三项关键信息:
1. **AuthenticationType**:一个字符串,描述用户是如何通过身份验证的(例如 "Cookies"、"JWT"、"Windows")。
2. **IsAuthenticated**:一个布尔标志,表示用户是否已登录。
3. **Claims**:一个 `Claim` 对象列表,包含特定的用户数据。

```csharp
public class ClaimsPrincipal : IPrincipal
{
    //List of identities
    List<ClaimsIdentity> _identities = new();
    // ...
}

public class ClaimsIdentity : IIdentity
{
    string AuthenticationType;
    bool IsAuthenticated;

    //List of claims
    List<Claim> _instanceClaims = new();
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-user-object-69957931/?t=120)

```text
ClaimsPrincipal (User)
 |
 +-- _identities: List<ClaimsIdentity>
      |
      +-- ClaimsIdentity
           |-- AuthenticationType  ("Cookies", "JWT", "Windows")
           |-- IsAuthenticated     (bool)
           +-- _instanceClaims: List<Claim>
                |-- Claim
                |-- Claim
                +-- ...
```

#### 实现用户信息的获取

为了显示用户详情,我们使用一个模型来捕获当前 `ClaimsPrincipal` 及其主身份的属性。

```csharp
using System.Security.Claims;

namespace StartcodeAuthentication.Features.User;

public class UserInfoModel
{
    public bool? IsAuthenticated { get; set; }

    public string AuthenticationType { get; set; }

    public List<Claim> Claims { get; set; }

    public string Name { get; set; }

    public bool IsDeveloper { get; set; }

    public bool IsAdmin { get; set; }

    public string DefaultNameClaimType { get; set; }

    public string DefaultRoleClaimType { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-user-object-69957931/?t=235)

在控制器 action 中,我们一步步提取这些详情。
我们首先访问 `User`(即 `ClaimsPrincipal`),然后将主 `Identity` 强制转换为 `ClaimsIdentity`,以访问扩展功能。
我们既可以从 `User` 对象(它汇总了所有身份的 claim)获取 claim,也可以直接从特定的 `ClaimsIdentity` 获取。

```csharp
public IActionResult Info()
{
    // 1. Get the user (ClaimsPrincipal) from the HttpContext
    ClaimsPrincipal user = User;

    // 2. Get the Primary Identity of the user
    var identity = User.Identity as ClaimsIdentity;

    // 3. Get IsAuthenticated
    var isAuthenticated = identity?.IsAuthenticated;

    // 4. Get AuthenticationType
    var authenticationType = identity?.AuthenticationType;

    // 5. Get the claims from the user
    List<Claim> claims = user?.Claims?.ToList() ?? [];

    // 6. Get the Name claim
    var name = identity?.Name;

    // 7. Check if user has developer or admin role
    var isDeveloper = user.IsInRole("developer");
    var isAdmin = user.IsInRole("admin");

    var model = new UserInfoModel()
    {
        IsAuthenticated = isAuthenticated,
        AuthenticationType = authenticationType,
        Claims = claims,
        Name = name,
        IsDeveloper = isDeveloper,
        IsAdmin = isAdmin,
        DefaultNameClaimType = identity?.NameClaimType,
        DefaultRoleClaimType = identity?.RoleClaimType
    };

    return View(model);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-user-object-69957931/?t=730)

## 2. Creating a ClaimsPrincipal

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/) · 16:28

本课讲解如何在 .NET 中从单个 `Claim` 对象开始手动构建一个 `ClaimsPrincipal`。
它解释了 claim、身份和 principal 之间的关系,并演示如何配置 `ClaimsIdentity`,通过指定身份验证类型并覆盖默认的 claim 类型映射,使 `IsAuthenticated` 和 `Name` 等属性正常工作。

### 核心概念

- **Claims**:代表用户事实的简单键值对(例如姓名、电子邮件、角色)。
- **ClaimsIdentity**:代表某个特定身份的一组 claim(例如驾照或员工工牌)。
- **ClaimsPrincipal**:可以容纳一个或多个 `ClaimsIdentity` 对象的顶层对象,代表完整的用户上下文。
- **身份验证状态**:只有在构造时提供了 `AuthenticationType` 字符串,一个身份才被视为已通过身份验证(`IsAuthenticated == true`)。
- **Claim 映射**:默认情况下,.NET 会为 `Name` 属性和 `IsInRole` 方法查找基于 URI 的长 claim 类型。
  可以使用 `nameType` 和 `roleType` 参数覆盖它们。

### 课程笔记

#### 创建 Claim

Claim 是身份系统的构建基石。
它们代表关于当前用户的单个事实。
Claim 可以以简单形式创建,只使用类型和值;也可以以完整形式创建,包含发行者(issuer)和值类型等可选参数。

```csharp
// Simple form
var claim1 = new Claim("name", "Bob");
var claim2 = new Claim(type: "country", value: "Sweden");

// Full form with all parameters
var claim3 = new Claim(type: "email",
                       value: "bob@example.com",

                       // Optional parameters (rarely used)
                       valueType: "string",
                       issuer: "IdentityServer",
                       originalIssuer: "Azure");
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/?t=25)

#### 身份层级

要为 ASP.NET Core 构建一个可用的用户对象,你必须遵循一个三步流程:
1. 将单个 claim 添加到一个列表中。
2. 使用该列表创建一个 `ClaimsIdentity`。
3. 将该身份传给 `ClaimsPrincipal` 的构造函数,创建一个 `ClaimsPrincipal`。

```csharp
// First, add the claims to a list
var myClaims = new List<Claim>()
{
    claim1,
    claim2,
    claim3
};

// Then, create an identity based on these claims
var myIdentity = new ClaimsIdentity(claims: myClaims);

// Finally, create a principal based on this identity
var principal = new ClaimsPrincipal(myIdentity);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/?t=55)

```text
claim1, claim2, claim3
        |
        v
  List<Claim> myClaims
        |
        v
  new ClaimsIdentity(claims: myClaims)   -> myIdentity
        |
        v
  new ClaimsPrincipal(myIdentity)        -> principal
```

#### 多个身份

可以把 `ClaimsPrincipal` 看作一个钱包。
正如一个钱包可以装有多种身份证件(用于基本信息的驾照和用于工作场景的员工工牌),一个 principal 也可以包装多个 `ClaimsIdentity` 对象。
这在用户数据从多个系统汇总而来的企业应用中尤其有用。

```csharp
// Employee identity from HR system
var employee = new ClaimsIdentity(new[]
{
    new Claim("employee_id", "EMP001"),
    new Claim("role", "Senior Developer")
});

// Basic user identity
var user = new ClaimsIdentity(new[]
{
    new Claim("sub", "12345"),
    new Claim("name", "John Smith"),
});

var identities = new[]
{
    employee,
    user
};

var userPrincipal = new ClaimsPrincipal(identities);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/?t=115)

```text
userPrincipal (ClaimsPrincipal)
+-----------------------------------------------+
| employee (ClaimsIdentity)                     |
|   employee_id = EMP001                        |
|   role        = Senior Developer              |
+-----------------------------------------------+
| user (ClaimsIdentity)                         |
|   sub         = 12345                         |
|   name        = John Smith                    |
+-----------------------------------------------+
```

#### 实现与身份验证状态

在 ASP.NET Core 控制器中,你可以通过设置 `HttpContext.User`,手动将一个 principal 分配给当前请求。
然而,仅仅提供 claim 和身份,并不足以让系统将该用户视为已通过身份验证。
只有当 `AuthenticationType` 被设置为一个非 null、非空的字符串(例如 "pwd"、"Cookies" 或任何自定义字符串)时,`IsAuthenticated` 属性才会返回 true。

```csharp
public virtual bool IsAuthenticated
{
    get { return !string.IsNullOrEmpty(_authenticationType); }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/?t=445)

#### 映射 Name 和 Role Claim

默认情况下,`Identity.Name` 属性和 `User.IsInRole()` 方法会使用 `ClaimTypes.Name` 和 `ClaimTypes.Role` 中定义的长 URI 标识符来查找 claim(例如 `http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name`)。
如果你的 claim 使用 "name" 或 "role" 这样的简单键,这些属性将返回 null 或 false。
要解决这个问题,你必须在 `ClaimsIdentity` 构造函数中指定 `nameType` 和 `roleType`。

在实现逻辑之前,先使用 `UserInfoModel` 为视图捕获身份的状态:

```csharp
using System.Security.Claims;

namespace StartcodeAuthentication.Features.User;

public class UserInfoModel
{
    public bool? IsAuthenticated { get; set; }
    public string AuthenticationType { get; set; }
    public List<Claim> Claims { get; set; }
    public string Name { get; set; }
    public bool IsDeveloper { get; set; }
    public bool IsAdmin { get; set; }
    public string DefaultNameClaimType { get; set; }
    public string DefaultRoleClaimType { get; set; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/?t=675)

下面 `Info` action 中的实现演示了如何创建一个带有多个角色、一个身份验证类型和自定义 claim 类型映射的 principal:

```csharp
public IActionResult Info()
{
    // Create ClaimsPrincipal User
    var myClaims = new List<Claim>()
    {
        new Claim("sub", "12345"), // sub = subject = UserId
        new Claim("name", "Bob"),
        new Claim("email", "bob@tn-data.se"),
        new Claim("role", "developer"),
        new Claim("role", "admin"),
        new Claim("role", "sales"),
    };

    var myIdentity = new ClaimsIdentity(claims: myClaims,
                                        authenticationType: "pwd",
                                        nameType: "name",
                                        roleType: "role");

    var myPrincipal = new ClaimsPrincipal(myIdentity);

    HttpContext.User = myPrincipal;

    // Retrieve data for the view
    ClaimsPrincipal user = User;
    var identity = user.Identity as ClaimsIdentity;

    var model = new UserInfoModel()
    {
        IsAuthenticated = identity?.IsAuthenticated,
        AuthenticationType = identity?.AuthenticationType,
        Claims = user?.Claims?.ToList() ?? [],
        Name = identity?.Name,
        IsDeveloper = user.IsInRole("developer"),
        IsAdmin = user.IsInRole("admin"),
        DefaultNameClaimType = identity.NameClaimType,
        DefaultRoleClaimType = identity.RoleClaimType
    };

    return View(model);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/creating-a-claimsprincipal-69957932/?t=940)

请注意,以这种方式设置 `HttpContext.User`,只会在当前请求期间保留该用户。
如果没有像 cookie 这样的持久化机制,用户在后续请求中将回到匿名状态。

## 3. The Request Pipeline

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/) · 19:08

### 总结

ASP.NET Core 中的请求管道是一系列中间件组件,用于处理传入的 HTTP 请求和传出的响应。
该过程始于一个 Web 服务器(通常是 Kestrel),它会创建一个 HttpContext 对象,代表当前的请求、响应和用户身份。
当这个上下文在管道中传递时,身份验证、路由和授权等中间件组件可以检查或修改它。
身份验证中间件专门负责识别用户,方式是填充 HttpContext.User 属性上的 ClaimsPrincipal。
开发者可以实现自定义中间件,来处理横切关注点、根据特定条件短路请求,或全局地为请求分配身份。

### 核心概念

- **Web 服务器**:Kestrel 是默认的、开源的、跨平台的 Web 服务器。
  HTTP.sys 是一个仅限 Windows 的替代方案,用于与 Windows 功能紧密集成。
- **HttpContext**:ASP.NET Core 中的核心对象,封装了 HttpRequest、HttpResponse 以及 User(ClaimsPrincipal)。
- **中间件管道**:一条双向链,其中每个组件在请求进入时处理请求,在响应返回时处理响应。
- **短路(Short-circuiting)**:中间件组件提前终止请求管道、在不调用下一个组件的情况下返回响应的能力。
- **身份验证中间件**:专门负责识别用户并填充 `HttpContext.User` 属性的组件。

### 课程笔记

#### Web 服务器与 HttpContext

当浏览器发送一个 HTTP 请求时,ASP.NET Core Web 服务器(通常是 Kestrel)接收它并创建一个 `HttpContext` 对象。
该对象代表当前请求的全部状态,并在应用程序中流转。
`HttpContext` 包含几个关键属性,包括请求数据、正在构建的响应,以及用户身份。

```csharp
public abstract class HttpContext
{
    public HttpRequest Request;
    public HttpResponse Response;

    /// <summary>
    /// Gets or sets the user for this request.
    /// </summary>
    public ClaimsPrincipal User;

    public IFeatureCollection Features;
    public ConnectionInfo Connection;
    public WebSocketManager WebSockets;
    public IDictionary<object, object?> Items;
    public ISession Session;
    public string TraceIdentifier;
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/?t=115)

最初,`HttpContext` 中的 `User` 属性包含一个匿名 principal。
当上下文在请求管道中移动时,身份验证中间件会检查请求(查找 cookie 或令牌)以识别用户。
如果找到有效的凭据,中间件就会用一个已填充的 `ClaimsPrincipal`(例如 "Bob Smith")替换匿名用户。

```text
Browser --HTTP request--> Kestrel
                             |
                             v
                   HttpContext (User = anonymous)
                             |
                             v
                 Authentication middleware
                 (looks for cookies or tokens)
                             |
                             v
                   HttpContext (User = ClaimsPrincipal "Bob Smith")
```

#### 中间件执行流程

请求管道由在 `Program.cs` 文件中配置的中间件组件组成。
可以使用 `app.Use` 添加中间件。
每个组件都会接收 `HttpContext` 以及一个指向链中下一个中间件的委托(`next`)。

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware #1 started");

    // Call the next middleware
    await next.Invoke();

    Console.WriteLine("Middleware #1 completed");
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/?t=400)

中间件遵循双向流动。
请求"向下"穿过管道中的每个组件,直到到达端点(例如一个 MVC 控制器)。
一旦端点生成了响应,该响应就会以相反的顺序,"向上"穿过相同的中间件组件返回。

```text
Middleware #1 started
Middleware #2 started
Middleware #3 started
Middleware #3 completed
Middleware #2 completed
Middleware #1 completed
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/?t=445)

```text
 request                                   response
    |                                         ^
    v                                         |
+------------------------------------------------+
| Middleware #1                                  |
|   +----------------------------------------+   |
|   | Middleware #2                          |   |
|   |   +--------------------------------+   |   |
|   |   | Middleware #3                  |   |   |
|   |   |   +------------------------+   |   |   |
|   |   |   | Endpoint (controller)  |   |   |   |
|   |   |   +------------------------+   |   |   |
|   |   +--------------------------------+   |   |
|   +----------------------------------------+   |
+------------------------------------------------+
```

#### 短路管道

中间件可以决定提前终止请求。
这被称为短路(short-circuiting)。
例如,一个中间件组件可能会检查请求路径是否被阻止,并在不调用 `next.Invoke()` 的情况下立即返回 `403 Forbidden` 状态码。

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine(" Middleware #3 started");

    if (context.Request.Path == "/blocked")
    {
        Console.WriteLine("  Access Denied!!!");
        context.Response.StatusCode = 403;  // Forbidden
        await context.Response.WriteAsync("Access Denied");
        return;
    }
    else
    {
        // Call the next middleware
        await next.Invoke();

        Console.WriteLine("  Middleware #3 completed");
    }
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/?t=640)

#### 全局分配身份

通过将身份逻辑放在一个中间件组件中,你可以确保管道中后续的每个组件(包括控制器和视图)都能获得 `ClaimsPrincipal`。
这样可以集中管理身份,而不是在各个 action 方法中分别处理。

```csharp
app.Use(async (context, next) =>
{
    var myClaims = new List<Claim>()
    {
        new Claim("sub", "12345"),
        new Claim("name", "Bob"),
        new Claim("email", "bob@tn-data.se"),
        new Claim("role", "developer"),
        new Claim("role", "admin"),
        new Claim("role", "sales"),
    };

    var myIdentity = new ClaimsIdentity(claims: myClaims,
                                        authenticationType: "pwd",
                                        nameType: "name",
                                        roleType: "role");

    var myPrincipal = new ClaimsPrincipal(myIdentity);

    context.User = myPrincipal;

    // Call the next middleware
    await next.Invoke();
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/?t=790)

#### 控制器级别的身份验证检查

一旦中间件填充了 `HttpContext` 上的 `User`,控制器就可以通过 `User` 属性访问这些数据。
这使得手动进行身份验证检查和重定向逻辑成为可能。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.Secure;

// URL: /Secure/Index
public class SecureController : Controller
{
    public IActionResult Index()
    {
        if (User?.Identity?.IsAuthenticated == true)
        {
            return View();
        }
        else
        {
            return Redirect("/user/AccessDenied");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/the-request-pipeline-69957933/?t=940)

## 4. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957934/) · 2:13

### 总结

本课全面概述了 ASP.NET Core 中的核心身份验证组件,详细说明了基于 claim 的身份对象之间的层级关系,以及它们如何集成到 HTTP 请求管道中。
它确立了 `ClaimsPrincipal` 作为核心用户表示的地位,并解释了中间件如何在 `HttpContext` 中填充这些身份数据。

### 核心概念

- **ClaimsPrincipal**:代表用户的主要对象,可在控制器和视图中访问。
- **ClaimsIdentity**:包含在 `ClaimsPrincipal` 中的一个对象,代表一个特定的身份来源。
- **Claim**:代表用户特定事实的键值对(例如姓名、电子邮件、角色)。
- **HttpContext**:代表当前 HTTP 请求和响应的对象,包含 `User` 属性。
- **请求管道**:处理请求并确立用户身份的一系列中间件。

### 课程笔记

`ClaimsPrincipal` 是 ASP.NET Core 中代表用户的主要类型。
它是在控制器和视图中用于执行身份相关逻辑的核心用户对象。
一个 `ClaimsPrincipal` 由一个或多个 `ClaimsIdentity` 对象组成,其中每个身份代表用户凭据的一个不同方面或颁发机构。

每个 `ClaimsIdentity` 包含一组 `Claim` 对象。
Claim 是一个键值对,代表关于用户的一个特定事实,例如其姓名、电子邮件地址或被分配的角色。
这些 claim 构成了应用程序中身份驱动逻辑的基础。

`HttpContext` 代表单个 HTTP 请求和响应的生命周期。
它包含一个持有 `ClaimsPrincipal` 的 `User` 属性。
当一个 HTTP 请求到达时,Web 服务器会初始化 `HttpContext`。
随后,该上下文会穿过由各种中间件组件组成的请求管道。
身份验证中间件负责识别用户,并在 `HttpContext` 中填充 `ClaimsPrincipal`。
最后,上下文到达 MVC 中间件,由它处理请求,并基于已确立的身份生成响应。
理解这一流程以及底层的对象层级,是在 ASP.NET Core 中实现安全性的基础,因为所有的授权决策都源自这些构建基石。

```text
HTTP request
     |
     v
Web server --initializes--> HttpContext (User: ClaimsPrincipal)
                                 |
                                 v
                    Authentication middleware
                    (populates ClaimsPrincipal)
                                 |
                                 v
                          MVC middleware
                       (generates response)
```

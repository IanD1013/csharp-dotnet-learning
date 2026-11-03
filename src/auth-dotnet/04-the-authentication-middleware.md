# The Authentication Middleware

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 4 章
> 共 8 课 · 约 1:26:33
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/) | 14:37 | [↓](#1-introduction) |
| 2 | [Authentication Operations](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-operations-69957936/) | 6:14 | [↓](#2-authentication-operations) |
| 3 | [Challenge](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/) | 14:54 | [↓](#3-challenge) |
| 4 | [SignIn](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957938/) | 8:59 | [↓](#4-signin) |
| 5 | [Authenticate](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/) | 22:11 | [↓](#5-authenticate) |
| 6 | [SignOut](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957940/) | 9:41 | [↓](#6-signout) |
| 7 | [Forbid](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957941/) | 8:32 | [↓](#7-forbid) |
| 8 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957942/) | 1:25 | [↓](#8-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/) · 14:37

本课从技术角度概述认证中间件在 ASP.NET Core 请求管道中如何运作,以及如何着手实现一个自定义认证系统。

### 核心概念

- **请求管道流程(Request Pipeline Flow)**:Web 服务器创建一个 `HttpContext`(初始为匿名),它流经认证中间件,如果找到有效凭据,中间件就会填充 `ClaimsPrincipal`。
- **认证处理程序(Authentication Handlers)**:负责实际认证逻辑的内部组件(例如 Cookie、Bearer token、OpenID Connect、OAuth)。
- **中间件注册(Middleware Registration)**:两步过程,即通过 `AddAuthentication` 注册服务,并通过 `UseAuthentication` 激活中间件。
- **中间件顺序(Middleware Ordering)**:在请求管道中,认证必须始终放在授权之前。
- **自定义处理程序(Custom Handlers)**:实现 `SignInAuthenticationHandler<T>` 和 `AuthenticationSchemeOptions`,以获得对认证过程的完全控制。

### 课程笔记

#### The Authentication Pipeline

当一个 HTTP 请求到达时,Web 服务器会创建一个 `HttpContext` 对象。
最初,这个上下文包含一个匿名用户。
随着上下文流经请求管道,它会到达认证中间件。
中间件检查请求中的凭据;如果找到有效凭据,它就创建一个 `ClaimsPrincipal` 并把它设置到当前上下文上。
随后上下文继续进入下一个中间件。
在返回路径上,响应会再次流经认证中间件,然后 Web 服务器使用最终的上下文生成 HTTP 响应。

```text
 HTTP 请求
     |
     v
 Web 服务器 --创建--> HttpContext (匿名用户)
     |
     v
 认证中间件 --找到有效凭据--> 创建 ClaimsPrincipal,设置到上下文
     |
     v
 下一个中间件
     |
     v (返回路径)
 认证中间件
     |
     v
 Web 服务器 --使用最终上下文--> HTTP 响应
```

在中间件内部,**认证处理程序(Authentication Handlers)** 执行实际的工作。
每个处理程序专门负责一种特定的方式,例如读取 cookie 或验证 token。
Microsoft 提供了用于 Cookies、Bearer token 和 OAuth 的内置处理程序,同时也存在许多第三方处理程序,用于 Amazon、Apple、Discord 和 Dropbox 等服务。

#### Implementing Authentication

要开始使用框架的认证系统,之前任何用于分配身份的手动中间件都应被删除或注释掉。

```csharp
/*
app.Use(async (context, next) =>
{
    Console.WriteLine(" Middleware #3 started");

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
    await next.Invoke();
});
*/
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=220)

认证在 `Program.cs` 文件中配置。
首先,使用 `AddAuthentication()` 把认证服务添加到依赖注入容器中。

```csharp
builder.Services.AddAuthentication(o =>
{
    // Customize the middleware
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=265)

接下来,必须使用 `app.UseAuthentication()` 把中间件添加到请求管道中。
关键在于,这个调用必须出现在 `app.UseRouting()` 之后、`app.UseAuthorization()` 之前。

```csharp
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=295)

#### Creating a Custom Authentication Handler

为了理解底层机制,我们实现一个自定义处理程序。
这需要一个继承自 `AuthenticationSchemeOptions` 的选项类。

```csharp
using Microsoft.AspNetCore.Authentication;

namespace CustomAuth;

public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=490)

处理程序类本身必须继承自 `SignInAuthenticationHandler<T>`,并使用自定义选项类作为泛型参数。
构造函数通过依赖注入接收 `IOptionsMonitor`、`ILoggerFactory` 和 `UrlEncoder` 等依赖。

```csharp
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CustomAuth;

public class CustomAuthHandler : SignInAuthenticationHandler<CustomAuthHandlerOptions>
{
    public CustomAuthHandler(IOptionsMonitor<CustomAuthHandlerOptions> options,
                             ILoggerFactory logger,
                             UrlEncoder encoder) : base(options, logger, encoder)
    {
        WriteToLog("CustomAuthHandler Constructor()");
    }

    private void WriteToLog(string message)
    {
        var scheme = Scheme?.Name ?? "[Default]";
        var msg = $"### [{DateTime.Now:HH:mm:ss}] - {scheme} - {message}";
        Console.WriteLine(msg);
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
    {
        throw new NotImplementedException();
    }

    protected override Task HandleSignOutAsync(AuthenticationProperties properties)
    {
        throw new NotImplementedException();
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=505)

为了简化注册,为 `AuthenticationBuilder` 创建一个调用 `AddScheme` 的静态扩展方法。

```csharp
using Microsoft.AspNetCore.Authentication;

namespace CustomAuth;

public static class CustomAuthHandlerExtensions
{
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder,
                                                      string authenticationScheme,
                                                      string? displayName,
                                                      Action<CustomAuthHandlerOptions> configureOption)
    {
        return builder.AddScheme<CustomAuthHandlerOptions, CustomAuthHandler>(authenticationScheme,
                                                                              displayName,
                                                                              configureOption);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=735)

最后,在 `Program.cs` 中注册自定义处理程序。
为了在初始搭建阶段避免运行时异常,把 `HandleAuthenticateAsync` 更新为返回 `AuthenticateResult.NoResult()`,它表示用户尚未通过认证,但允许请求继续执行。

```csharp
builder.Services.AddAuthentication(o =>
{
    // Customize the middleware
}).AddCustomAuth(authenticationScheme: "myAuth",
                 displayName: "myAuth",
                 configureOption: o =>
                 {
                 });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=775)

```csharp
protected override Task<AuthenticateResult> HandleAuthenticateAsync()
{
    return Task.FromResult(AuthenticateResult.NoResult());
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957935/?t=820)

## 2. Authentication Operations

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-operations-69957936/) · 6:14

### 总结

ASP.NET Core 认证处理程序支持五种核心操作:Authenticate、Sign in、Sign out、Forbid 和 Challenge。
这些操作构成了认证系统的基础,处理从识别传入请求中的用户到管理会话持久化和访问控制的一切事务。
本课介绍这些概念,并搭建一个测试控制器来演示它们在应用中的行为,让开发者可以手动触发并观察每一种操作。

### 核心概念

- **Authenticate**:根据请求数据(例如读取认证 cookie)确立用户身份并创建 `ClaimsPrincipal` 的过程。
- **Sign in**:持久化用户身份的过程,例如创建认证 cookie 并将其存储在浏览器中。
- **Sign out**:移除已持久化身份的过程,通常是指示浏览器删除认证 cookie。
- **Forbid**:当已认证的用户试图访问其没有权限的资源时所采取的动作,通常会重定向到 "Access Denied" 页面。
- **Challenge**:当未认证的用户试图访问受保护资源时所采取的动作,通常会重定向到登录页面。

### 课程笔记

ASP.NET Core 中的认证建立在由认证处理程序执行的五种基本操作之上。
理解这些操作对于实现任何认证系统都至关重要。

1. **Authenticate**:认证中间件会在每个传入请求上自动调用这个操作。
   它尝试确立用户的身份。
   例如,如果请求包含认证 cookie,处理程序就会提取用户信息,创建 `ClaimsPrincipal`,并把它附加到 `HttpContext` 上。
   如果成功,在请求管道的剩余部分中,该用户就不再是匿名的。
2. **Sign in**:当用户成功登录时使用它。
   它指示处理程序"记住"该用户。
   在基于 cookie 的认证中,这会创建一个 cookie,它被发送到浏览器,并包含在后续请求中。
3. **Sign out**:这是登录的逆操作。
   它告诉处理程序忘记已认证的状态。
   对于 cookie,服务器会发送一个响应头,指示浏览器删除认证 cookie,使用户回到匿名状态。
4. **Forbid**:当用户已成功认证但缺少访问特定资源所需的权限时,就会发生这种情况。
   系统会在处理程序上调用 `Forbid`,在基于 cookie 的设置中,这通常会把用户重定向到拒绝访问页面。
5. **Challenge**:当匿名用户试图访问需要认证的资源时会触发它。
   系统向用户发起质询,要求其证明自己的身份。
   在大多数 Web 应用中,这会导致重定向到登录页面。

为了演示这些操作,我们实现了一个测试控制器。
这个控制器提供用于手动触发五种操作中每一种的端点。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.AuthTest;

/// <summary>
/// Authentication test controller for demonstrating authentication operations
/// </summary>
public class AuthTestController : Controller
{
    /// <summary>
    /// Display the test page with authentication status
    /// </summary>
    public IActionResult Index()
    {
        ViewBag.IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        ViewBag.Username = User.Identity?.Name ?? "Not authenticated";

        return View();
    }

    /// <summary>
    /// Trigger authentication check
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AuthenticateUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.AuthenticateAsync()");

        // TODO: 

        // TempData["Message"] = $"Authentication result: {(result.Succeeded ? "Success" : "Failed")}";

        return RedirectToAction("Index");
    }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-operations-69957936/?t=280)

该控制器包含其余操作的占位方法,它们会随着课程推进而逐步实现。

```csharp
    /// <summary>
    /// Trigger challenge (redirects to login if not authenticated)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ChallengeUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

        // TODO: 

        return Empty;
    }

    /// <summary>
    /// Trigger sign in with test claims
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignInUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignInAsync(principal)");

        // TODO: 

        return Empty;
    }
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-operations-69957936/?t=295)

最后,`SignOut` 和 `Forbidden` 方法补全了中间件所支持的这组操作。

```csharp
    /// <summary>
    /// Trigger sign out
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignOutUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignOutAsync();");

        // TODO: 

        return Empty;
    }

    /// <summary>
    /// Trigger forbid (returns access denied)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Forbidden()
    {
        Console.WriteLine("\r\nCalling HttpContext.ForbidAsync();");

        // TODO:

        return Empty;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-operations-69957936/?t=310)

为了访问这个测试页面,在 `_Layout.cshtml` 文件中向应用的主导航栏添加了一个链接。

```razor
<nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div class="container-fluid">
            <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">StartProject</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                <ul class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Secure">Secure</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="AuthTest" asp-action="Index">AuthTest</a>
                    </li>
                </ul>
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-operations-69957936/?t=335)

## 3. Challenge

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/) · 14:54

### 核心概念

- challenge 操作的定义和用途。
- 直接与间接触发 challenge 的方法。
- 特定于处理程序的响应(302 Redirect 与 401 Unauthorized)。
- 直接调用处理程序时返回空结果的重要性。
- 在自定义认证处理程序中实现 HandleChallengeAsync。
- 在 Program.cs 中通过 lambda 表达式配置 challenge 选项。

### 课程笔记

当系统需要未认证的用户证明其身份时,就会调用 challenge 操作。
在基于 cookie 的认证系统中,这通常意味着把用户重定向到登录页面。

#### Triggering a Challenge

在控制器内部触发 challenge 主要有两种方式:

1. **直接异步调用(Direct Async Call)**:直接调用 `HttpContext.ChallengeAsync()` 会在认证处理程序上调用 challenge 操作。
2. **操作结果(Action Result)**:返回 `Challenge()`(`new ChallengeResult()` 的快捷方式)可以让认证中间件捕获该结果,并在管道稍后的阶段触发处理程序。

```csharp
public async Task<IActionResult> Challenge1()
{
    // Trigger challenge now
    await HttpContext.ChallengeAsync();

    // Return empty result
    // (challenge already executed)
    return new EmptyResult();
}

public IActionResult Challenge2()
{
    // Return challenge result
    return Challenge();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=40)

使用直接方式(`ChallengeAsync`)时,处理程序处理请求并直接在 `HttpContext` 上设置重定向响应。
由于响应已经准备好了,操作方法应当返回 `EmptyResult`。
返回其他结果,例如重定向到主页,会覆盖认证处理程序设置的响应头。
这一原则同样适用于 `SignInAsync`、`SignOutAsync` 和 `ForbidAsync` 操作。

```csharp
public async Task<IActionResult> MyPage1()
{
    await HttpContext.ChallengeAsync();

    return new EmptyResult();
}

public async Task<IActionResult> MyPage2()
{
    await HttpContext.ChallengeAsync();

    // This overrides the challenge redirect with a redirect to home
    return Redirect("/");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=235)

#### Handler-Specific Responses

对 challenge 的响应由具体的认证处理程序决定。
cookie 处理程序通常会发出一个指向登录路径的 `302 Found` 重定向。
相比之下,像 JWT-bearer 处理程序这样面向 API 的处理程序会返回 `401 Unauthorized` 响应,因为没有可以重定向到的 UI。

```http
HTTP/1.1 302 Found
Location: /User/Login
...

HTTP/1.1 401 Unauthorized
WWW-Authenticate: Bearer
...
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=175)

#### Implementing Challenge in a Custom Handler

要在自定义处理程序中支持 challenge,首先在选项类中定义一个 `LoginPath`,用来存储重定向目标。

```csharp
public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";
    public string CookieName { get; set; } = "AuthCookie";
    public string LoginPath { get; set; } = "/User/Login";
    public string AccessDeniedPath { get; set; } = "/User/AccessDenied";
    public string DefaultRedirectPath { get; set; } = "/AuthTest";
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=370)

接下来,在自定义认证处理程序中重写 `HandleChallengeAsync` 方法。
这个方法负责使用选项中定义的路径,把重定向响应写入 `HttpContext`。

```csharp
protected override Task HandleChallengeAsync(AuthenticationProperties properties)
{
    WriteToLog($"HandleChallengeAsync: User needs to authenticate, redirecting to '{Options.LoginPath}'");

    Response.Redirect(Options.LoginPath);

    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=470)

#### Using the Challenge in Controllers

在控制器中,可以通过 `Challenge()` 辅助方法触发 challenge。
它返回一个 `ChallengeResult`,该结果会被认证中间件拦截,从而触发处理程序。

```csharp
[HttpPost]
public async Task<IActionResult> ChallengeUser()
{
    Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

    // return Challenge() is a shortcut for returning a new ChallengeResult()
    return Challenge();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=640)

#### Configuration via Options

`LoginPath` 可以在 `Program.cs` 中注册服务时通过 lambda 表达式进行自定义。
这个配置在启动时执行,可以用来覆盖默认路径。

```csharp
builder.Services.AddAuthentication(o =>
{
    // Customize the middleware
}).AddCustomAuth(authenticationScheme: "myAuth",
                 displayName: "myAuth",
                 configureOption: o =>
                 {
                     o.LoginPath = "/test";
                 });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957937/?t=760)

## 4. SignIn

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957938/) · 8:59

### 总结

本课讲解 ASP.NET Core 认证中 SignIn 操作的实现。
内容涵盖从代码中触发登录的三种主要方法、构造用于表示已认证用户的 ClaimsPrincipal,以及在自定义认证处理程序中实现 HandleSignInAsync 方法,以便向浏览器颁发持久的认证 cookie。

### 核心概念

- **SignInAsync**:`HttpContext` 上的主要异步方法,用于以编程方式让用户登录。
- **ClaimsPrincipal**:表示已认证用户的对象,包含一个或多个身份及其关联的 claims。
- **SignInResult**:一种操作结果,从控制器返回时,会触发认证中间件来处理登录。
- **HandleSignInAsync**:自定义认证处理程序中负责实际持久化逻辑(例如设置 cookie)的方法。
- **Cookie 安全(Cookie Security)**:在登录过程中使用 `HttpOnly` 防止客户端脚本访问,并使用 `SameSite` 防范 CSRF。

### 课程笔记

#### Triggering the SignIn Operation

在 ASP.NET Core 中,从控制器触发登录操作有三种常见方式。
这三种方式都需要一个表示用户及其 claims 的 `ClaimsPrincipal` 对象。

1. **直接调用(Direct Call)**:直接调用 `HttpContext.SignInAsync`,并返回一个标准结果,例如重定向。
2. **SignInResult**:返回一个包含该 principal 的 `SignInResult` 新实例。
3. **SignIn 辅助方法(SignIn Helper)**:使用 `Controller` 基类提供的 `SignIn()` 快捷方法,它在内部返回一个 `SignInResult`。

```csharp
public async Task<IActionResult> SigninAsync1()
{
    // Create principal with claims
    var principal = ...
    await HttpContext.SignInAsync(principal);

    return Redirect("/");
}

public IActionResult Signin2()
{
    // Create principal with claims
    var principal = ...
    return new SignInResult(principal);
}

public IActionResult Signin3()
{
    // Create principal with claims
    var principal = ...
    return SignIn(principal); // Returns a SignInResult
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957938/?t=55)

#### Implementing the SignIn Logic

要实现登录,首先必须构造用户的身份。
这包括创建一个 claims 列表,把它们包装进 `ClaimsIdentity`,然后创建一个 `ClaimsPrincipal`。
`ClaimsIdentity` 构造函数中的 `authenticationType` 参数至关重要,因为它把该身份标记为已认证。

```csharp
[HttpPost]
public async Task<IActionResult> SignInUser()
{
    Console.WriteLine("\r\nCalling HttpContext.SignInAsync(principal)");

    //1. Load the claims for this user 
    var myClaims = new List<Claim>()
           {
               new("sub","1234"),
               new("name","Bob"),
               new("email","bob@tn-data.se"),
               new("role","developer"),
               new("role","sales"),
               new("role","admin")
           };

    var myIdentity = new ClaimsIdentity(claims: myClaims,
                                        authenticationType: "custom",
                                        nameType: "name",
                                        roleType: "role");

    var myPrincipal = new ClaimsPrincipal(myIdentity);

    await HttpContext.SignInAsync(myPrincipal);

    return Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957938/?t=310)

#### Configuring Handler Options

为了支持登录操作,必须更新自定义认证处理程序的选项,加入 cookie 名称以及操作完成后使用的默认重定向路径的设置。

```csharp
public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";

    public string CookieName { get; set; } = "AuthCookie";

    public string LoginPath { get; set; } = "/User/Login";

    public string DefaultRedirectPath { get; set; } = "/AuthTest";
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957938/?t=250)

#### Overriding HandleSignInAsync

当调用 `SignInAsync` 时,框架会调用认证处理程序中的 `HandleSignInAsync` 方法。
实际的 cookie 就是在这里创建并追加到响应中的。
出于安全考虑,cookie 应标记为 `HttpOnly`,使 JavaScript 无法访问它们,并且应设置 `SameSite` 特性(例如设为 `Lax`)以提供 CSRF 防护。

```csharp
protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
{
    var username = user.Identity?.Name ?? "Unknown";
    WriteToLog($"HandleSignInAsync: Signing in user '{username}'");

    // Set authentication cookie with configured name and expiration
    Response.Cookies.Append(key: Options.CookieName, value: username, new CookieOptions
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Lax
    });

    var redirectUri = Options.DefaultRedirectPath;
    Response.Redirect(redirectUri);

    WriteToLog($"HandleSignInAsync: Cookie '{Options.CookieName}' set, redirecting to '{redirectUri}'");

    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957938/?t=370)

cookie 设置好之后,浏览器会存储它,并在后续请求中携带它。
这使应用能够在之后的交互中识别该用户,而这由 `Authenticate` 操作来处理。

## 5. Authenticate

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/) · 22:11

### 总结

Authenticate 操作是 ASP.NET Core 中在每个传入请求上识别用户的主要机制。
默认情况下,认证中间件会自动调用这个操作来检查请求中的凭据(例如 cookie),以确立一个 ClaimsPrincipal 并把它附加到 HttpContext 上。
虽然框架会自动处理这一过程,开发者也可以手动调用 AuthenticateAsync 来获取详细的认证元数据,包括认证票据(authentication ticket)和属性,它们提供的上下文比标准的 User 属性更多。

### 核心概念

- **自动执行(Automatic Execution)**:认证中间件会在每个传入请求上调用 authenticate 操作,以确立用户身份。
- **ClaimsPrincipal**:如果认证成功,处理程序会创建一个 principal 并把它添加到 `HttpContext.User` 属性中。
- **手动调用(Manual Invocation)**:开发者可以调用 `HttpContext.AuthenticateAsync()` 来获取一个 `AuthenticateResult`,其中包含认证票据和属性等元数据。
- **管道顺序(Pipeline Order)**:把 `UseStaticFiles` 放在 `UseAuthentication` 之前,可以确保公共资源在提供时不会产生认证检查的开销。
- **无状态(Statelessness)**:authenticate 操作只作用于传入的请求;它不会修改响应(这一点不同于 Sign-In 或 Sign-Out)。

### 课程笔记

#### The Authenticate Operation

默认情况下,authenticate 操作会在每个传入请求上被调用。
它的目的是检查请求(例如检查是否有认证 cookie)并确立用户是谁。
如果成功,认证处理程序会创建一个 `ClaimsPrincipal` 并把它添加到上下文中。
随后请求继续进入管道中的下一个中间件。
在返回的路上(响应阶段),authenticate 处理程序什么也不做,因为它只专注于传入的请求。

```text
 传入请求 --> 认证中间件 (authenticate) --> 下一个中间件
                |
                +-- 检查请求 (认证 cookie)
                +-- 成功: 创建 ClaimsPrincipal,添加到上下文

 响应 <------ 认证中间件 (什么也不做) <---- 下一个中间件
```

#### Manual vs. Automatic Authentication

虽然中间件会自动调用 authenticate,你也可以使用 `AuthenticateAsync` 方法手动触发它。

```csharp
// Manually trigger authentication
AuthenticateResult result = await HttpContext.AuthenticateAsync();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=55)

访问用户信息最常见的方式是通过 `User` 属性,它返回当前的 `ClaimsPrincipal`。
不过,调用 `AuthenticateAsync` 会提供一个完整的 `AuthenticateResult` 对象,其中包含更详细的信息。

```csharp
public IActionResult Index1()
{
    // Get the ClaimsPrincipal created by Authenticate
    var user1 = HttpContext.User;
    var user2 = User;              // These two are the same
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=85)

`AuthenticateResult` 包括认证票据、认证属性,以及认证失败时的具体失败详情。

```csharp
// Get authentication details for the current request
AuthenticateResult result = await HttpContext.AuthenticateAsync();

// Extract details from the result
var principal = result.Principal;
var authenticationTicket = result.Ticket;
var authenticationProperties = result.Properties;

// Set if authentication failed
var exception = result.Failure;

// True if the authentication was successful
var isAuthenticated = result.Succeeded;

// True if no user authenticated
var notAuthenticated = result.None;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=100)

#### Middleware Pipeline Placement

`UseAuthentication` 在 `Program.cs` 文件中的位置至关重要。
通常,`UseStaticFiles` 放在 `UseAuthentication` 之前。
这确保了对 CSS、JavaScript 和图片的请求会被立即处理,而不会触发认证逻辑,从而保持应用的高效。

```csharp
app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=415)

如果把 `UseStaticFiles` 移到 `UseAuthentication` 之后,每个对静态资源的请求都会触发认证处理程序。
如果你需要保护特定文件(例如付费内容)或追踪谁访问了某些资源,这会很有用,但它会给每个请求增加显著的开销。

#### Implementing HandleAuthenticateAsync

要在自定义处理程序中实现这一逻辑,必须重写 `HandleAuthenticateAsync`。
在这个演示中,处理程序读取一个 cookie 来识别用户。
如果 cookie 不存在,它返回 `AuthenticateResult.NoResult()`。

```csharp
protected override Task<AuthenticateResult> HandleAuthenticateAsync()
{
    var path = Request?.Path ?? "[Unknown]";
    WriteToLog($"HandleAuthenticateAsync: Called for {path}");

    // Read the cookie
    var username = Request.Cookies[Options.CookieName];

    if(string.IsNullOrEmpty(username))
    {
        // No cookie found - user not authenticated
        WriteToLog($"No cookie {Options.CookieName} found - user not authenticated");
        return Task.FromResult(AuthenticateResult.NoResult());
    }
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=655)

如果找到了 cookie,处理程序会创建一个 claims 列表、一个 `ClaimsIdentity` 和一个 `ClaimsPrincipal`。
最后,它把 principal 包装进一个 `AuthenticationTicket`,并返回成功的结果。

```csharp
    else
    {
        // Cookie found - user authenticated
        var claims = new List<Claim>();
        claims.Add(new Claim("name", username));

        var myIdentity = new ClaimsIdentity(claims: claims,
                                            authenticationType: "test",
                                            nameType: "name",
                                            roleType: "role");

        var myPrincipal = new ClaimsPrincipal(myIdentity);
        var ticket = new AuthenticationTicket(myPrincipal, "CustomAuthHandler");

        WriteToLog($"HandleAuthenticateAsync: User '{username}' authenticated");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=910)

```text
 Request.Cookies[Options.CookieName]
        |
        +-- 为空 --> AuthenticateResult.NoResult()
        |
        +-- 找到 --> Claim("name", username)
                     --> ClaimsIdentity
                     --> ClaimsPrincipal
                     --> AuthenticationTicket
                     --> AuthenticateResult.Success(ticket)
```

#### Manual Authentication Demo

在控制器中,你可以使用 `AuthenticateAsync` 检查用户的状态,并通过 `TempData` 或其他机制提供反馈。

```csharp
[HttpPost]
public async Task<IActionResult> AuthenticateUser()
{
    Console.WriteLine("\r\nCalling HttpContext.AuthenticateAsync()");

    AuthenticateResult result = await HttpContext.AuthenticateAsync();

    TempData["Message"] = $"Authentication result: {(result.Succeeded ? "Success" : "Failed")}";

    return RedirectToAction("Index");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957939/?t=1135)

**安全提示:** 这里展示的实现在 cookie 中使用了明文用户名。
这非常不安全,因为用户可以在浏览器的开发者工具中手动修改 cookie 的值来冒充其他用户。
生产应用需要适当的 cookie 保护和加密。

## 6. SignOut

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957940/) · 9:41

登出是登录操作的逆操作。
当用户注销时,应用必须忘记已认证的状态。
对于基于 cookie 的认证,这要求认证处理程序指示浏览器删除认证 cookie,此后该用户会被视为匿名用户。

### 核心概念

- **登出触发(Sign-out Trigger)**:发起登出主要有三种方式:直接调用 `HttpContext.SignOutAsync()`、返回 `SignOutResult`,或使用 `SignOut()` 控制器辅助方法。
- **Cookie 删除(Cookie Deletion)**:处理程序通过发送一个把 cookie 过期日期设为过去(例如 1970 年)的响应头来删除 cookie。
- **HandleSignOutAsync**:自定义认证处理程序中实现移除认证状态逻辑的方法。
- **安全局限(Security Limitation)**:标准的登出只会从客户端移除 cookie。它不会全局性地使 cookie 失效;如果 cookie 被盗,除非实现了服务端失效机制,否则它在自然过期之前都一直有效。

### 课程笔记

#### Triggering Sign-Out

ASP.NET Core 提供了多种触发登出过程的模式。
这些模式与 Challenge 和 Forbid 等其他认证操作是一致的。

```csharp
public async Task<IActionResult> SignOutUsingHttpContext()
{
    await HttpContext.SignOutAsync();

    return Redirect("/LoggedOut");
}

public async Task<IActionResult> SignOutUsingResult()
{
    return new SignOutResult();
}

public async Task<IActionResult> SignOutUsingHelper()
{
    return SignOut(); // Returns a SignOutResult
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957940/?t=55)

#### Controller Implementation

要在控制器中实现登出,你需要调用 `HttpContext.SignOutAsync()`。
这个调用会触发认证中间件,在已注册的认证处理程序上调用 `HandleSignOutAsync` 方法。

```csharp
/// <summary>
/// Trigger sign out
/// </summary>
[HttpPost]
public async Task<IActionResult> SignOutUser()
{
    Console.WriteLine("\r\nCalling HttpContext.SignOutAsync();");

    await HttpContext.SignOutAsync();

    // You control the redirect
    return RedirectToAction("Index");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957940/?t=130)

#### Handler Implementation

在自定义认证处理程序中,必须重写 `HandleSignOutAsync` 方法,以定义如何清除认证状态。
对于基于 cookie 的处理程序,这包括从 `Response` 对象中删除 cookie,并且通常还要执行一次重定向。

```csharp
/// <summary>
/// Sign out user by removing authentication cookie
/// </summary>
protected override Task HandleSignOutAsync(AuthenticationProperties properties)
{
    WriteToLog($"HandleSignOutAsync: Signing out user, deleting cookie '{Options.CookieName}'");

    // Delete cookie by setting it with past expiration
    Response.Cookies.Delete(Options.CookieName);

    // Redirect to return URL or default
    var redirectUri = Options.DefaultRedirectPath;
    Response.Redirect(redirectUri);

    WriteToLog($"HandleSignOutAsync: Cookie deleted, redirecting to '{redirectUri}'");

    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957940/?t=280)

#### Protocol Observation

当调用 `SignOutAsync` 时,服务器会发出一个带有 `Set-Cookie` 头的响应。
这个头包含 cookie 名称,但把 `expires` 特性设置为一个过去的日期(例如 `Thu, 01 Jan 1970 00:00:00 GMT`)。
这会通知浏览器立即从其本地存储中移除该 cookie。

#### Security Considerations

认证 cookie 是高价值的攻击目标。
如果攻击者窃取了一个有效的认证 cookie,他们就可以通过在自己浏览器的开发者工具中添加该 cookie 来冒充该用户。

关键在于,标准的登出过程只会从用户的浏览器中移除 cookie。
它不会使 cookie 的签名或其在服务器上的有效性失效。
如果攻击者已经窃取了 cookie,那么即使合法用户已经登出,他们仍然保持已认证状态。
为了降低这一风险,应用需要后端支持来追踪并使会话失效,或者使用 "cookies on a diet" 技术来减少 cookie 本身存储的数据。

## 7. Forbid

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957941/) · 8:32

### 总结

Forbid 操作处理已认证用户缺少访问特定资源所需权限的场景。
与在用户身份未知时使用的 Challenge 不同,Forbid 是在用户已被识别但未获授权时调用的。
Forbid 的行为取决于认证处理程序:基于 cookie 的处理程序通常会重定向到拒绝访问页面,而 JWT bearer 处理程序会返回 403 Forbidden 状态码。

### 核心概念

- **用途(Purpose)**:拒绝缺少权限的已认证用户的访问。
- **Challenge 与 Forbid(Challenge vs. Forbid)**:Challenge 问的是"你是谁?";Forbid 说的是"你不被允许进入这里。"
- **处理程序差异(Handler Variability)**:Cookie 处理程序会重定向;JWT 处理程序返回状态码。
- **触发方式(Trigger Methods)**:`HttpContext.ForbidAsync()`、`ForbidResult`,或 `Forbid()` 控制器快捷方法。

### 课程笔记

当一个请求带着有效的认证(例如认证 cookie)到达,但应用逻辑或中间件判定该用户没有正确的权限时,就会触发 `Forbid` 操作。
在基于 cookie 的认证中,这通常会导致重定向到拒绝访问页面。

在 ASP.NET Core 控制器中触发 forbid 操作主要有三种方式:

```csharp
public async Task<IActionResult> Forbid1()
{
    await HttpContext.ForbidAsync();

    return new EmptyResult();
}

public IActionResult Forbid2()
{
    return new ForbidResult();
}

public IActionResult Forbid3()
{
    return Forbid(); // Returns a ForbidResult
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957941/?t=70)

对 forbid 调用的响应取决于所配置的认证处理程序。
例如,`CookieHandler` 通常会返回一个指向拒绝访问页面的重定向,这对 Web 应用来说是标准做法。
相比之下,API 中使用的 `JwtBearerHandler` 会返回 `403 Forbidden` 状态码,因为没有可以重定向到的 UI。

#### Challenge vs. Forbid

根据用户的状态区分 `Challenge` 和 `Forbid` 非常重要:

1. **你已认证了吗?** 如果答案是否,系统会调用 `Challenge`,提示用户证明其身份。
2. **你被允许吗?** 如果用户已认证但答案是否,系统会调用 `Forbid`,表示他们缺少权限。

如果用户既已认证又已获授权,请求就会正常继续。

```text
 请求
   |
   v
 你已认证了吗? --否--> Challenge
   | 是
   v
 你被允许吗? ----否--> Forbid
   | 是
   v
 请求正常继续
```

#### Implementing Forbid Support

要在自定义应用中实现 forbid 支持,首先更新控制器操作,使用 `HttpContext.ForbidAsync()` 触发该操作。

```csharp
/// <summary>
/// Trigger forbid (returns access denied)
/// </summary>
[HttpPost]
public async Task<IActionResult> Forbidden()
{
    Console.WriteLine("\r\nCalling HttpContext.ForbidAsync();");

    await HttpContext.ForbidAsync();

    return Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957941/?t=270)

接下来,在认证选项类中定义访问被拒绝时用户应被重定向到的路径。
这让处理程序知道重定向的目标。

```csharp
/// <summary>
/// Path to redirect when access is denied 
/// </summary>
public string AccessDeniedPath { get; set; } = "/User/AccessDenied";
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957941/?t=325)

最后,在自定义认证处理程序中重写 `HandleForbiddenAsync` 方法。
这个方法负责执行到所配置的 `AccessDeniedPath` 的实际重定向。

```csharp
/// <summary>
/// Handle forbidden (user authenticated but not authorized)
/// </summary>
protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
{
    WriteToLog($"HandleForbiddenAsync: Access denied, redirecting to '{Options.AccessDeniedPath}'");

    Response.Redirect(Options.AccessDeniedPath);    

    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957941/?t=490)

## 8. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957942/) · 1:25

### 总结

本课从宏观层面概述 ASP.NET Core 认证中间件及其作为请求守门人的角色。
它定义了认证处理程序的作用,并详细介绍了支撑整个认证框架的五种基本操作:Authenticate、Sign In、Sign Out、Forbid 和 Challenge。

### 核心概念

- **认证中间件(Authentication Middleware)**:ASP.NET Core 管道中的核心组件,它拦截 HTTP 请求以处理身份。
- **认证处理程序(Authentication Handlers)**:包含各种认证策略实现逻辑的专用组件。
- **核心操作(Core Operations)**:五种基本动作(Authenticate、Sign In、Sign Out、Forbid 和 Challenge),它们定义了系统如何与用户交互。

### 课程笔记

认证中间件是 ASP.NET Core 应用中的主要守门人,它拦截每一个传入的 HTTP 请求来评估用户的身份。
在这个中间件内部,认证处理程序为不同的认证方式提供具体的实现逻辑。

整个认证系统建立在五种核心操作之上:

1. **Authenticate**:这个操作根据请求上下文确定用户的身份。
2. **Sign In**:这个动作为后续请求持久化用户的身份,通常是通过设置认证 cookie。
3. **Sign Out**:这个动作清除用户已持久化的身份,例如通过移除认证 cookie。
4. **Forbid**:当用户已认证但没有访问特定资源所需的权限时,就会发生这种情况。
5. **Challenge**:当未认证的用户试图访问受保护的资源时会触发它,提示他们登录或提供凭据。

理解这五种操作对于有效地实现和定制 ASP.NET Core 中的认证至关重要。
后续主题将在这些概念的基础上,探讨认证方案(authentication schemes)、属性(properties)和票据(tickets)。

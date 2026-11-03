# Cookie Event Handlers

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 8 章
> 共 6 课 · 约 1:30:01
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Challenge Events](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/) | 17:14 | [↓](#1-challenge-events) |
| 2 | [SignIn Events](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/) | 17:43 | [↓](#2-signin-events) |
| 3 | [Authenticate Events](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/) | 24:30 | [↓](#3-authenticate-events) |
| 4 | [SignOut Events](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/) | 12:55 | [↓](#4-signout-events) |
| 5 | [Forbid Events](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/) | 15:11 | [↓](#5-forbid-events) |
| 6 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957969/) | 2:28 | [↓](#6-section-recap) |

## 1. Challenge Events

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/) · 17:14

### 总结

.NET 中的认证处理器通过事件提供扩展点,让开发者可以在认证生命周期中自定义行为。
cookie 处理器的 OnRedirectToLogin 事件专门用于拦截并修改质询(challenge)过程,默认情况下该过程会把未认证用户重定向到登录页。
通过重写这个事件,开发者可以实现自定义逻辑,例如对 API 请求返回 401 Unauthorized 状态码,同时对基于浏览器的导航保留标准重定向,从而确保认证体验更健壮、更能感知上下文。

### 核心概念

- **Authentication Handler Events**:允许在登录、登出或质询用户等特定操作期间运行自定义代码的钩子。
- **Challenge Operation**:当未认证用户尝试访问受保护资源时触发的过程。
- **OnRedirectToLogin**:cookie 认证处理器中的一个特定事件,用于在发生质询时自定义重定向行为。
- **API vs. Browser Handling**:对程序化的 API 调用返回 401 状态码,而不是 302 重定向到登录页的做法。
- **AuthenticationProperties**:用于存储某个特定认证请求的状态和配置(例如 `RedirectUri`)的对象。

### 课程笔记

.NET 中的认证处理器具有很强的可扩展性。
虽然基础配置通常已经足够,但复杂场景需要自定义逻辑。
每个认证处理器都暴露了多个扩展点,开发者可以在这些扩展点上为登录、或在 claims 存入 cookie 之前修改它们等操作挂接事件处理器。

```csharp
.AddCookie("cookie", o =>
{
    // Your configuration
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=55)

#### The Challenge Event

当用户必须先认证才能访问某个资源时,就会发生质询事件。
默认情况下,cookie 处理器提供一个内置的事件处理器,它执行两项主要检查:判断请求是否为 AJAX 请求(若是则返回 401 状态码),或是标准浏览器请求(重定向到配置的 `LoginPath`)。

```csharp
if (IsAjaxRequest(context.Request))
{
    // Return 401 for XMLHttpRequest,
    context.Response.Headers.Location = context.RedirectUri;
    context.Response.StatusCode = 401;
}
else
{
    // redirect for regular browser requests
    context.Response.Redirect(context.RedirectUri);
}
return Task.CompletedTask;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=160)

```text
Challenge -> OnRedirectToLogin (default)
               |
               +-- IsAjaxRequest?  yes -> StatusCode = 401
               |                          Headers.Location = RedirectUri
               |
               +-- otherwise           -> Response.Redirect(RedirectUri)
                                          (LoginPath)
```

开发者可以完全替换这一默认行为。
例如,如果用户在营业时间之外尝试登录,你可以把他们重定向到一个 "Closed for the Day" 页面。

```csharp
.AddCookie("cookie", o =>
{
    o.LoginPath = "/user/login";

    // Override default redirect behavior
    o.Events.OnRedirectToLogin = context =>
    {
        int hour = DateTime.Now.Hour;
        if (hour >= 8 && hour <= 17)
            context.Response.Redirect(context.RedirectUri);
        else
            context.Response.Redirect("/user/ClosedForTheDay");

        return Task.CompletedTask;
    };
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=205)

#### Implementing Custom Challenge Logic

在典型的 controller 中,质询通过 `Challenge` 结果来触发。
该方法可以接收 `AuthenticationProperties`,用来定义用户登录成功后应被送往何处。

```csharp
[HttpPost]
public async Task<IActionResult> ChallengeUser()
{
    Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

    var properties = new AuthenticationProperties()
    {
        RedirectUri = "/MyLoginPage",
    };

    return Challenge(properties);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=295)

在构建 API 时,重定向到登录页通常是不可取的,因为 API 客户端期望得到 401 Unauthorized 状态码。
你可以创建一个专门的 API controller 来测试这一行为。

```csharp
public class ApiController : Controller
{
    public IActionResult Index()
    {
        if (User?.Identity?.IsAuthenticated == false)
        {
            return Challenge();
        }
        else
        {
            return Ok("API Response");
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=445)

#### Customizing OnRedirectToLogin

为了区别处理浏览器请求和 API 请求,你可以在 `Program.cs` 中重写 `OnRedirectToLogin` 事件。
通过检查请求路径,你可以决定是发出重定向还是返回状态码。
如果你重写了这个事件却没有调用 `context.Response.Redirect`,自动重定向就会被抑制。

```csharp
o.Events.OnRedirectToLogin = context =>
{
    var url = context.RedirectUri;
    Console.WriteLine($"### Challenge.OnRedirectToLogin {url}");
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=565)

为了满足对所有以 `/api` 开头的路径返回 401 的需求,使用 `StartsWithSegments` 检查请求路径,并手动设置 `StatusCode`。

```csharp
o.Events.OnRedirectToLogin = context =>
{
    if(context.Request.Path.StartsWithSegments("/api"))
    {
        // For API Requests, return a 401
        context.Response.StatusCode = 401;
    }
    else
    {
        // Redirect to login page for non-API requests
        context.Response.Redirect(context.RedirectUri);
    }

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-events-69957964/?t=955)

## 2. SignIn Events

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/) · 17:43

### 总结

本课介绍 .NET cookie 登录过程中触发的三个主要事件:`OnSigningIn`、`OnSignedIn` 和 `OnRedirectToReturnUrl`。
它演示了如何拦截这些阶段:在认证 cookie 签发之前修改用户 claims,在 cookie 创建之后执行日志记录或数据库更新等副作用,以及自定义登录成功后的重定向逻辑。
通过实现这些事件处理器,开发者可以对认证流程进行细粒度控制,从而支持审计、动态补充 claims,以及对 API 请求与浏览器请求进行专门处理。

### 核心概念

* **OnSigningIn**:在 cookie 签发之前执行;是修改 `ClaimsPrincipal` 或 cookie 属性的主要位置。
* **OnSignedIn**:在 cookie 签发之后执行;用于日志记录、审计或更新数据库等非破坏性的副作用。
* **OnRedirectToReturnUrl**:管理最后的重定向步骤;对处理 AJAX 请求或强制跳转到特定落地页至关重要。
* **Context Objects**:每个事件都提供一个特定的上下文对象(例如 `CookieSigningInContext`),其中包含 `Principal`、`Request` 和 `Response` 等相关属性。
* **Extension Points**:这些事件充当钩子,可以在不修改核心中间件的情况下替换或扩展默认的认证行为。

### 课程笔记

#### Overview of Sign-In Events

cookie 认证处理器提供了三个事件来自定义登录过程。
默认情况下,`OnSigningIn` 和 `OnSignedIn` 是空的,充当扩展点。
`OnRedirectToReturnUrl` 包含默认逻辑,会根据请求是普通浏览器请求还是 AJAX/API 请求而以不同方式处理重定向。

```csharp
OnSigningIn
// Do nothing
context => Task.CompletedTask;

OnSignedIn
// Do nothing
context => Task.CompletedTask;

OnRedirectToReturnUrl
if (IsAjaxRequest(context.Request))
{
    context.Response.Headers.Location = context.RedirectUri;
}
else
{
    context.Response.Redirect(context.RedirectUri);
}
return Task.CompletedTask;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=55)

#### Event Use Cases

**1. OnSigningIn**

该事件在 cookie 签发并发送到浏览器之前被调用。
它是修改 `ClaimsPrincipal` 或其 identity 的理想位置。
常见用例包括添加一个 "last login" claim、设置 cookie 属性,或对登录尝试进行审计。

```csharp
// Called before the cookie is issued
o.Events.OnSigningIn = context =>
{
    // Get the current identity
    var identity = (ClaimsIdentity)context.Principal.Identity;

    // Add a new claim
    identity.AddClaim(new Claim("last_login",
        DateTime.UtcNow.ToString()));

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=115)

**2. OnSignedIn**

该事件在 cookie 签发之后触发。
到了这个阶段,再修改 cookie 本身已经太迟了。
它用于副作用,例如在数据库中更新用户的最后登录时间,或记录登录结果。

```csharp
// Called after the cookie is issued
o.Events.OnSignedIn = context =>
{
    //Update last login time in database
    userService.UpdateLastLogin(userId);

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=160)

**3. OnRedirectToReturnUrl**

当处理器在登录成功后重定向用户时,会调用该事件。
开发者可以用它来验证/清理重定向 URL,或者无论提供了什么 return URL,都强制重定向到某个特定页面(例如概览页)。

```csharp
// Called when redirecting after signin (if needed)
o.Events.OnRedirectToReturnUrl = context =>
{
    // Always redirect to this page
    // (ignoring context.RedirectUri)
    context.Response.Redirect("/user/overview");
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=190)

```text
SignInAsync
    |
    v
OnSigningIn            (before cookie issued: modify ClaimsPrincipal)
    |
    v
[cookie issued]
    |
    v
OnSignedIn             (after cookie issued: logging, database update)
    |
    v
OnRedirectToReturnUrl  (redirect after successful sign-in)
```

#### Implementation Demo

要实现这些事件,需要在 `Program.cs` 中把它们赋值给 `CookieAuthenticationOptions` 的 `Events` 属性。

```csharp
o.Events.OnSigningIn = context =>
{
    Console.WriteLine($"###  SignIn.OnSigningIn");
    return Task.CompletedTask;
};

o.Events.OnSignedIn = context =>
{
    Console.WriteLine($"###  SignIn.OnSignedIn");
    return Task.CompletedTask;
};

o.Events.OnRedirectToReturnUrl = context =>
{
    Console.WriteLine($"###  SignIn.OnRedirectToReturnUrl");
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=415)

如果 controller action 调用了 `SignInAsync` 并返回 `Empty` 响应,应用就依赖 `OnRedirectToReturnUrl` 事件来执行实际的浏览器重定向。
如果该处理器是空的,用户就会停留在一个空白页面上。
下面的实现可以确保重定向发生:

```csharp
o.Events.OnRedirectToReturnUrl = context =>
{
    Console.WriteLine($"### SignIn.OnRedirectToReturnUrl");

    if(context.RedirectUri is not null)
    {
        context.Response.Redirect(context.RedirectUri);
    }

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=660)

#### Customizing Claims and Logging

在 `OnSigningIn` 事件中,你可以把 identity 转换为 `ClaimsIdentity`,从而动态添加新的 claims。
这些 claims 会成为存储在 cookie 中的加密认证票据(authentication ticket)的一部分。

```csharp
o.Events.OnSigningIn = context =>
{
    Console.WriteLine($"### SignIn.OnSigningIn");

    var identity = (ClaimsIdentity)context.Principal.Identity;
    identity.AddClaim(new Claim("MembershipPoints", "1234"));

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=760)

在 `OnSignedIn` 事件中,你可以访问 `HttpContext` 来解析 `ILoggerFactory` 之类的服务,以记录该事件。

```csharp
o.Events.OnSignedIn = context =>
{
    Console.WriteLine($"### SignIn.OnSignedIn");

    var loggerFactory = context.HttpContext
                                .RequestServices.GetRequiredService<ILoggerFactory>();

    var logger = loggerFactory.CreateLogger("SignInEvents");

    logger.LogInformation("User {UserName} signed in",
        context.Principal?.Identity.Name);

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-events-69957965/?t=940)

## 3. Authenticate Events

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/) · 24:30

认证涉及检查传入的请求以确定身份。
cookie 处理器会执行几个内部步骤:读取 cookie、创建认证票据、在必要时续期票据,以及验证票据。
有两个特定的事件处理器允许开发者挂接到这一过程中。

```text
request -> read cookie -> create ticket -> renew (if needed) -> validate
                                              |                   |
                                 OnCheckSlidingExpiration   OnValidatePrincipal
```

### 核心概念

- **OnCheckSlidingExpiration**:在启用滑动过期时触发,用于判断是否应续期认证票据。
- **OnValidatePrincipal**:在每个包含有效认证票据的请求上触发,允许操作 claims 或拒绝会话。
- **ShouldRenew Property**:滑动过期上下文中的一个布尔标志,可以通过重写它来控制续期行为。
- **RejectPrincipal()**:一个使当前请求的认证会话失效的方法。
- **ReplacePrincipal()**:一个把当前 claims principal 换成新 principal 的方法。

### 课程笔记

#### The Authenticate Event Handlers

默认情况下,authenticate 事件什么都不做。
开发者必须添加行为来定制认证生命周期。
第一个事件 `OnCheckSlidingExpiration` 在启用票据续期时会在每个请求上被调用。
处理器会评估票据是否应该续期,并通过 `ShouldRenew` 属性给出建议。

```csharp
// Called when evaluating sliding expiration renewal
o.Events.OnCheckSlidingExpiration = context =>
{
    // Ticket renewal is enabled for this request
    if (context.ShouldRenew)
    {
        // Renewal recommended for this request

        if (!context.Principal.IsInRole("admin"))
        {
            // Override the handler's renewal decision
            context.ShouldRenew = false;
        }
    }
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/?t=100)

第二个事件 `OnValidatePrincipal` 会在每个带有认证票据的请求上被调用。
这让应用可以在应用的其余部分使用 `ClaimsPrincipal` 之前对其进行验证或操作。
一个常见用例是检查用户在其 cookie 签发之后是否已被封禁。

```csharp
// Called on every request with an authentication ticket
o.Events.OnValidatePrincipal = context =>
{
    // Reject authentication if user has been banned
    if (IsUserBanned(context.Principal))
    {
        context.RejectPrincipal();
    }
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/?t=160)

#### Configuring the Demo Environment

为了在示例应用中观察这些事件,认证票据的有效期被设为一个很短的时长(15 秒),并启用了滑动过期。

```csharp
[HttpPost]
public async Task<IActionResult> SignInUser()
{
    Console.WriteLine("\r\nCalling HttpContext.SignInAsync(principal)");

    // ... identity setup ...

    var properties = new AuthenticationProperties(items)
    {
        ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(15),
        AllowRefresh = true,
        RedirectUri ="/AuthTest"
    };

    await HttpContext.SignInAsync(myPrincipal, properties);

    return Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/?t=295)

#### Monitoring Ticket Lifetime

在 `OnValidatePrincipal` 事件中,你可以从 `AuthenticationProperties` 中取出签发时间戳和过期时间戳,以计算当前票据的剩余有效期。

```csharp
o.Events.OnValidatePrincipal = context =>
{
    Console.WriteLine($"### OnValidatePrincipal");

    var issued = context.Properties.IssuedUtc;
    var expires = context.Properties.ExpiresUtc;

    if(issued.HasValue && expires.HasValue)
    {
        var remaining = expires.Value - DateTimeOffset.UtcNow;

        Console.WriteLine($"### Remaining: {(int)remaining.TotalSeconds}");
    }

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/?t=595)

#### Overriding Renewal Logic

默认情况下,当票据的有效期已过去超过 50% 时,cookie 中间件会尝试续期票据。
你可以在 `OnCheckSlidingExpiration` 处理器中修改 `context.ShouldRenew` 来覆盖这一行为。
例如,你可以只在剩余时间不超过 2 秒时才允许续期。

```csharp
o.Events.OnCheckSlidingExpiration = context =>
{
    Console.WriteLine($"### OnCheckSlidingExpiration");
    Console.WriteLine($"### Elapsed:      {(int)context.ElapsedTime.TotalSeconds}");
    Console.WriteLine($"### Remaining:    {(int)context.RemainingTime.TotalSeconds}");
    Console.WriteLine($"### Should Renew: {context.ShouldRenew}");

    var remainingTime = (int)context.RemainingTime.TotalSeconds;

    if (context.ShouldRenew == true && remainingTime > 2)
        context.ShouldRenew = false;

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/?t=910)

#### Principal Manipulation

`OnValidatePrincipal` 事件提供了强大的方法来即时修改用户的身份。
你可以向当前 identity 添加 claims,完全拒绝该 principal(使用户在该请求中成为匿名用户),或者用一个完全不同的 principal 替换它。

```csharp
o.Events.OnValidatePrincipal = context =>
{
    Console.WriteLine("### OnValidatePrincipal");

    switch (new Random().Next(0, 3))
    {
        case 0:
            Console.WriteLine("### Adding custom claim to current principal");
            var identity = (ClaimsIdentity)context.Principal.Identity;
            identity.AddClaim(new Claim("MembershipPoints", "1234"));
            break;

        case 1:
            Console.WriteLine("### Rejecting principal");
            context.RejectPrincipal();
            break;

        case 2:
            Console.WriteLine("### Replacing principal");
            var claims = new List<Claim>
            {
                new("name", "Alice"),
                new("email", "alice@example.com"),
                new("role", "Developer")
            };

            var newIdentity = new ClaimsIdentity(
                claims,
                authenticationType: "TestAuth",
                nameType: "name",
                roleType: "role");

            var newPrincipal = new ClaimsPrincipal(newIdentity);
            context.ReplacePrincipal(newPrincipal);
            break;
    }

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-events-69957966/?t=1435)

## 4. SignOut Events

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/) · 12:55

### 总结

.NET cookie 认证中的登出过程涉及两个主要事件:`OnSigningOut` 和 `OnRedirectToReturnUrl`。
`OnSigningOut` 在认证 cookie 被移除之前执行,为保存状态或记录日志等清理任务提供了访问用户信息的最后机会。
`OnRedirectToReturnUrl` 是登录和登出操作共用的事件,用于管理用户最终的重定向。
理解这些事件可以让开发者自定义登出体验,例如添加自定义响应头或 cookie,同时确保用户被正确地路由到一个 "logged out" 落地页。

### 核心概念

*   **OnSigningOut**:在认证 cookie 被删除之前立即触发。它提供对当前用户的 `ClaimsPrincipal` 和 `HttpContext` 的访问。
*   **OnRedirectToReturnUrl**:登录和登出重定向共用的事件处理器。它决定操作完成后用户被送往何处。
*   **Active vs. Passive Sign-out**:这些事件只有在用户通过 `SignOutAsync` 主动登出时才会触发。当会话或认证票据自然过期时,它们不会被调用。
*   **Response Customization**:开发者可以使用这些钩子,在浏览器处理 cookie 删除之前,向登出响应追加自定义 cookie 或响应头。

### 课程笔记

#### The Sign-Out Flow

登出操作是一个直接的过程,重点是删除认证 cookie,并可选地重定向用户。
`CookieAuthenticationEvents` 类中有两个钩子可用于拦截这一流程。

1.  **OnSigningOut**:在 cookie 被删除之前调用。这是你还能访问用户信息的最后一个时间点。
2.  **OnRedirectToReturnUrl**:在 cookie 被删除之后处理重定向。这与登录过程中使用的是同一个事件。

默认情况下,`OnSigningOut` 是一个空处理器,而 `OnRedirectToReturnUrl` 包含处理标准重定向和 AJAX 请求的逻辑。

```csharp
OnSigningOut
// Do nothing
context => Task.CompletedTask;

OnRedirectToReturnUrl
if (IsAjaxRequest(context.Request))
{
    context.Response.Headers.Location = context.RedirectUri;
}
else
{
    context.Response.Redirect(context.RedirectUri);
}
return Task.CompletedTask;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/?t=85)

```text
SignOutAsync
    |
    v
OnSigningOut           (before cookie deleted: last access to user info)
    |
    v
[cookie deleted]
    |
    v
OnRedirectToReturnUrl  (redirect, same event as sign-in)
```

#### Using OnSigningOut for Cleanup

`OnSigningOut` 适用于清理任务和通知。
例如,你可以获取当前用户的 ID,以便在会话终止之前保存其购物车状态。

```csharp
// We are about to sign out the current user
o.Events.OnSigningOut = context =>
{
    var user = context.HttpContext.User;
    var userId = user?.FindFirst("userid")?.Value;

    if (userId != null)
    {    
        SaveShoppingCart(userId);
    }
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/?t=115)

#### Implementing Sign-Out Logic

要实现自定义登出,通常在 controller action 中配置 `AuthenticationProperties`,以定义 cookie 被清除后用户应被送往何处。
在 `Logout` action 中,你调用 `SignOutAsync` 并传入这些属性。

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Logout()
{
    var properties = new AuthenticationProperties()
    {
        RedirectUri = "/user/Loggedout"
    };

    await HttpContext.SignOutAsync(properties);

    return Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/?t=235)

#### Configuring Event Handlers

在 `Program.cs` 中向 cookie 认证配置添加事件处理器时,如果你重写了默认行为,就必须确保显式地处理重定向逻辑。
如果实现了 `OnRedirectToReturnUrl` 却没有执行重定向,那么即使 cookie 已被删除,用户也会停留在一个空白页面上。

```csharp
o.Events.OnSigningOut = context =>
{
    Console.WriteLine("### SignOut.OnSigningOut");
    return Task.CompletedTask;
};

o.Events.OnRedirectToReturnUrl = context =>
{
    Console.WriteLine("### OnRedirectToReturnUrl");
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/?t=370)

要修复重定向问题,请使用 `context.Response.Redirect` 方法,并传入上下文中提供的 `RedirectUri`。

```csharp
o.Events.OnRedirectToReturnUrl = context =>
{
    Console.WriteLine($"### OnRedirectToReturnUrl {context.RedirectUri}");

    context.Response.Redirect(context.RedirectUri);

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/?t=520)

#### Customizing the Sign-Out Response

`OnSigningOut` 允许你在认证 cookie 被移除之前修改响应头和 cookie。
这可以用于审计,或者向系统的其他部分发出已发生登出的信号。

```csharp
o.Events.OnSigningOut = context =>
{
    Console.WriteLine("### SignOut.OnSigningOut");

    // Add a custom response header
    context.Response.Headers.Add("X-Action", "LoggedOut");

    // Append a custom cookie indicating the sign-out time
    context.Response.Cookies.Append("UserSignedOut", DateTimeOffset.UtcNow.ToString());

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-events-69957967/?t=730)

## 5. Forbid Events

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/) · 15:11

### 总结

当已认证的用户尝试访问其缺少必要权限的资源时,就会发生 Forbid 操作。
在 ASP.NET Core Cookie 认证中,这会触发 OnRedirectToAccessDenied 事件,默认情况下它会把用户重定向到配置的拒绝访问路径,或者对 AJAX/API 请求返回 403 Forbidden 状态码。
开发者可以自定义这一行为,以实现安全审计、记录未授权的访问尝试,或修改重定向目标。
为了处理复杂逻辑并获得更好的可维护性,这些处理器应当从内联定义重构到一个继承自 CookieAuthenticationEvents 的专用类中,从而可以使用依赖注入来访问 ILogger 之类的服务。

### 核心概念

* **OnRedirectToAccessDenied**:在 forbid 操作期间触发的主要事件处理器。
* **HttpContext.ForbidAsync()**:用于以编程方式触发 forbid 逻辑的方法。
* **CookieAuthenticationEvents**:一个可被继承的基类,为处理认证事件提供结构化的方式。
* **EventsType**:`CookieAuthenticationOptions` 中的属性,用于指定一个在 DI 中注册的类来处理事件。
* **Security Auditing**:forbid 事件的一个常见用例,涉及记录用户 ID 以及其尝试访问的资源。

### 课程笔记

#### The Forbid Operation

当已认证的用户尝试访问其无权查看的资源时,会触发 forbid 操作。
它的主要任务是把用户重定向到拒绝访问页面。
因为这个操作很直接,它只使用一个事件处理器:`OnRedirectToAccessDenied`。
该事件在处理器即将执行重定向时触发,让你可以自定义或重写默认行为。

默认情况下,处理器会检查请求类型。
对于 API 或 AJAX 请求,它通常返回 403 Forbidden 状态码而不是重定向,因为把 API 调用重定向到一个 HTML 页面通常没有用处。

```csharp
o.Events.OnRedirectToAccessDenied = context =>
{
    if (IsAjaxRequest(context.Request))
    {        
        context.Response.Headers.Location = context.RedirectUri;
        context.Response.StatusCode = 403;
    }
    else
    {
        context.Response.Redirect(context.RedirectUri);
    }
    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=55)

这个事件的一个常见用例是安全审计。
你可以从上下文中获取当前用户并记录其 ID,以便在允许重定向继续之前发现可疑活动。

```csharp
o.Events.OnRedirectToAccessDenied = context =>
{
    var user = context.HttpContext.User;
    var userId = user?.FindFirst("userid")?.Value;

    LogUnauthorizedAttempt(userId);

    context.Response.Redirect(context.RedirectUri);

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=85)

#### Implementing Forbid Handlers

在典型的 controller 中,forbid 操作通过 `HttpContext.ForbidAsync()` 触发。

```csharp
[HttpPost]
public async Task<IActionResult> Forbidden()
{
    Console.WriteLine("\r\nCalling HttpContext.ForbidAsync();");

    await HttpContext.ForbidAsync();

    return Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=190)

在 `Program.cs` 中以内联方式实现处理器时,如果你重写了默认逻辑,就必须手动调用 `context.Response.Redirect(context.RedirectUri)`,否则重定向不会发生。

```csharp
o.Events.OnRedirectToAccessDenied = context =>
{
    Console.WriteLine($"### Forbid.OnRedirectToAccessDenied");

    context.Response.Redirect(context.RedirectUri);

    return Task.CompletedTask;
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=295)

#### Refactoring to Custom Event Classes

以内联方式定义许多复杂的处理器会让 `Program.cs` 难以维护。
更清晰的做法是创建一个继承自 `CookieAuthenticationEvents` 的类,并重写所需的方法。

```csharp
public class CustomCookieEvents: CookieAuthenticationEvents
{
    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {        
        context.Response.Redirect(context.RedirectUri);

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=565)

要使用这个类,你必须在依赖注入(DI)容器中注册它,然后在 cookie 配置中把该类型赋值给 `EventsType` 属性。
这种做法让你可以把 `ILogger` 之类的服务直接注入到事件处理器类中。

```csharp
builder.Services.AddScoped<CustomCookieEvents>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/User/AccessDenied";
    o.EventsType = typeof(CustomCookieEvents);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=685)

集成 DI 之后,自定义事件类的最终实现可以在处理重定向的同时进行丰富的日志记录:

```csharp
public class CustomCookieEvents : CookieAuthenticationEvents
{
    private readonly ILogger<CustomCookieEvents> logger;

    public CustomCookieEvents(ILogger<CustomCookieEvents> logger)
    {
        this.logger = logger;
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        logger.LogInformation("Access denied for user {User}. Redirecting to {redirectUrl}", 
            context.HttpContext.User.Identity?.Name, 
            context.RedirectUri);

        context.Response.Redirect(context.RedirectUri);

        return Task.CompletedTask;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-events-69957968/?t=850)

## 6. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957969/) · 2:28

### 总结

本课回顾了 .NET 认证中 cookie 事件处理器的实现,强调了重写处理器默认行为所带来的灵活性。
它介绍了定义事件的两种主要方式(内联 lambda 表达式和专用事件类),并解释了这些钩子如何支持自定义审计、安全日志记录和修改重定向逻辑等高级场景。
此外,它对比了 Cookie、OpenID Connect 和 JWT Bearer 处理器的事件模型,指出每一种都是针对有状态或无状态认证的特定操作需求而定制的。

### 核心概念

*   **Event Definition Methods**:事件可以用内联 lambda 表达式定义以处理简单逻辑,也可以用专用类定义以实现复杂且易维护的实现。
*   **Dependency Injection**:为事件使用专用类可以注入服务,例如 logger 或数据库上下文。
*   **Default Behavior Overrides**:自定义事件逻辑会替换认证处理器内置的默认行为,而不仅仅是在其后追加。
*   **Handler-Specific Events**:不同的认证方案(Cookie、OpenID Connect、JWT Bearer)根据其底层协议提供不同的事件集合(例如,无状态 API 不支持登录/登出事件)。
*   **Common Use Cases**:审计、安全合规、修改 claims principal,以及自定义重定向或响应。

### 课程笔记

.NET 中的认证可以通过事件挂接到认证过程中来进行扩展。
认证处理器中的每个操作都支持一个或多个事件,让开发者可以注入自定义逻辑。
定义这些事件主要有两种方式:直接在配置中使用 lambda 表达式,或者创建一个专用的事件类。

对于复杂的应用,通常更推荐专用类的方式,因为它能让代码保持有条理,并且完全支持依赖注入。
通过继承 `CookieAuthenticationEvents`,你可以重写特定的方法,在默认逻辑之前或之后添加自定义行为。

```csharp
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace StartcodeAuthentication;

/// <summary>
/// Custom cookie authentication events handler.
/// Override methods to add custom behavior before or after the default logic.
/// </summary>
public class CustomCookieEvents : CookieAuthenticationEvents
{
    private readonly ILogger<CustomCookieEvents> logger;

    public CustomCookieEvents(ILogger<CustomCookieEvents> logger)
    {
        this.logger = logger;
    }

    public override Task SigningIn(CookieSigningInContext context)
    {
        // Add your custom logic here

        // Call the base method to execute the default behavior
        // Remove this line to replace the default behavior entirely
        return base.SigningIn(context);
    }

    public override Task SignedIn(CookieSignedInContext context)
    {
        return base.SignedIn(context);
    }

    public override Task SigningOut(CookieSigningOutContext context)
    {
        return base.SigningOut(context);
    }

    public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        return base.ValidatePrincipal(context);
    }

    public override Task CheckSlidingExpiration(CookieSlidingExpirationContext context)
    {
        return base.CheckSlidingExpiration(context);
    }

    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        return base.RedirectToLogin(context);
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        logger.LogInformation("Access denied for user {User}", context.HttpContext.User.Identity?.Name);

        context.Response.Redirect("/Home/AccessDenied");

        return Task.CompletedTask;
    }

    public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
    {
        return base.RedirectToReturnUrl(context);
    }

    public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
    {
        return base.RedirectToLogout(context);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957969/?t=19)

这些事件的常见用例包括出于安全或合规目的的审计和日志记录。
事件还支持仅靠静态配置无法实现的高级方案,例如修改重定向、更改响应,或操作 `ClaimsPrincipal` 用户对象。

要使用专用事件类,必须先在依赖注入容器中注册它,然后通过 `EventsType` 属性把它赋给认证选项。

```csharp
builder.Services.AddScoped<CustomCookieEvents>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{
    o.AccessDeniedPath = "/User/AccessDenied";
    o.LoginPath = "/User/Login";
    o.Cookie.Name = "MyAuthCookie";

    o.ExpireTimeSpan = TimeSpan.FromDays(7);
    o.SlidingExpiration = true;

    o.EventsType = typeof(CustomCookieEvents);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957969/?t=28)

需要认识到,每个认证处理器提供的事件集合都不同。
cookie 处理器侧重于本地会话管理,而 OpenID Connect 处理器提供的是与外部身份提供者、授权码和令牌交换相关的事件。
相反,通常用于 API 的 JWT Bearer 处理器提供的是一组完全不同的事件,并且不支持登录或登出事件,因为这些操作对于无状态的 API 认证并不相关。

每个认证操作都有一个默认行为。
当你实现自定义事件处理器时,你并不只是从零开始添加行为;你是在重写并替换内置逻辑。
这让你可以完全控制每个操作在认证管道中的行为。

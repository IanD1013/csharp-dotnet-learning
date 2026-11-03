# The Authentication Internals

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 5 章
> 共 5 课 · 约 59:53
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Schemes](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/) | 17:41 | [↓](#1-schemes) |
| 2 | [Authentication Properties](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-69957949/) | 12:46 | [↓](#2-authentication-properties) |
| 3 | [Authentication Ticket](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/) | 17:36 | [↓](#3-authentication-ticket) |
| 4 | [Data Protection](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/data-protection-69957946/) | 8:54 | [↓](#4-data-protection) |
| 5 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957947/) | 2:56 | [↓](#5-section-recap) |

## 1. Schemes

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/) · 17:41

### 总结

ASP.NET Core 中的认证方案(authentication scheme)充当认证处理器的唯一标识符,使多个处理器可以在同一个认证中间件中共存。
通过定义方案,开发者可以指定由哪个处理器来负责特定操作,例如认证、质询(challenge)、登录或登出。
这种灵活性支持复杂的场景,例如为不同的身份提供方使用不同的处理器,或者同时调用多个处理器来管理多个安全令牌或 Cookie。

### 核心概念

*   **Authentication Scheme**:分配给某个认证处理器注册的唯一字符串名称。
*   **Authentication Middleware**:请求管道中负责编排所有已注册处理器的唯一组件。
*   **Default Schemes**:`AuthenticationOptions` 中的配置属性,用于把特定操作(Authenticate、Challenge、SignIn、SignOut、Forbid)映射到特定处理器。
*   **Explicit Scheme Selection**:通过直接向 `HttpContext` 方法或 Action Result 传入方案名称来覆盖默认行为的能力。
*   **Display Name**:与方案关联的、便于人阅读的字符串,通常用于登录按钮之类的 UI 元素。

### 课程笔记

在 ASP.NET Core 中,应用管道里只注册了一个认证中间件。
然而,这一个中间件可以承载多个认证处理器。

```csharp
builder.Services.AddAuthentication(o =>
{
})
.AddHandler1(o => {})
.AddHandler2(o => {});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=25)

```text
            Authentication Middleware
   +--------------------------------------------+
   |  +-------------+        +-------------+    |
   |  |  Handler1   |        |  Handler2   |    |
   |  +-------------+        +-------------+    |
   +--------------------------------------------+
```

当注册了多个处理器时,就会出现一个问题:当控制器返回 `Forbid()` 这样的结果时,系统必须确定应由哪个处理器来执行该操作。

```csharp
public IActionResult AccessDenied()
{
    return Forbid();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=85)

#### 定义方案

为了解决这个问题,每个处理器都必须用一个唯一的名称来注册,这个名称称为 **scheme**(方案)。
这个名称让中间件能够区分各个处理器。

```csharp
builder.Services.AddAuthentication(o =>
{
})
.AddHandler1("handler1", o => {})
.AddHandler2("handler2", o => {});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=100)

如果你尝试用同一个方案名称注册两个处理器,应用会抛出一个异常,说明该方案已经存在。

```csharp
builder.Services.AddAuthentication(o =>
{
    // Customize the middleware
}).AddCustomAuth(authenticationScheme: "handler1",
                displayName: "myAuth",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                })
.AddCustomAuth(authenticationScheme: "handler1",
                displayName: "myAuth",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=235)

#### 配置默认方案

当存在多个处理器时,你必须指定哪一个是默认的。
如果没有指定默认方案,而某个认证操作又在没有显式方案的情况下被触发,应用就会抛出异常。

你可以设置一个适用于所有操作的全局 `DefaultScheme`:

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "handler1";
}).AddCustomAuth(authenticationScheme: "handler1",
                displayName: "myAuth",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                })
.AddCustomAuth(authenticationScheme: "handler2",
                displayName: "myAuth",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=340)

如需更细粒度的控制,你可以把特定操作映射到不同的处理器。
例如,你可能希望由 `handler1` 处理认证和登录,而由 `handler2` 处理质询。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = "handler1";
    o.DefaultForbidScheme = "handler1";
    o.DefaultSignInScheme = "handler1";
    o.DefaultSignOutScheme = "handler1";

    o.DefaultChallengeScheme = "handler2";
})
.AddCustomAuth("handler1", o => {})
.AddCustomAuth("handler2", o => {});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=115)

```text
  Operation       Default property              Handler
  ------------    --------------------------    ----------
  Authenticate    DefaultAuthenticateScheme  -> "handler1"
  Forbid          DefaultForbidScheme        -> "handler1"
  SignIn          DefaultSignInScheme        -> "handler1"
  SignOut         DefaultSignOutScheme       -> "handler1"
  Challenge       DefaultChallengeScheme     -> "handler2"
```

#### 在控制器中显式选择方案

可以在控制器动作中通过指定方案名称来覆盖默认值。
当某个特定动作需要与应用其余部分不同的认证方式时,这很有用。

```csharp
[HttpPost]
public async Task<IActionResult> ChallengeUser()
{
    Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

    return new ChallengeResult("handler2");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=520)

或者,你也可以直接调用 `HttpContext` 上的扩展方法:

```csharp
[HttpPost]
public async Task<IActionResult> ChallengeUser()
{
    Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

    await HttpContext.ChallengeAsync("handler2");

    return RedirectToAction("Index");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=550)

#### 同时使用多个处理器

在高级场景中,你可能需要让用户同时登录到多个方案。
如果这些处理器使用 Cookie,你必须确保它们使用唯一的 Cookie 名称以避免冲突。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "handler1";
}).AddCustomAuth(authenticationScheme: "handler1",
                displayName: "myAuth",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                    o.CookieName = "AuthCookie1";
                })
  .AddCustomAuth(authenticationScheme: "handler2",
                displayName: "myAuth",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                    o.CookieName = "AuthCookie2";
                });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=670)

然后你可以为每个方案分别调用 `SignInAsync`。
注意,如果多个处理器都尝试执行重定向,顺序就很重要,而且通常更好的做法是手动控制重定向以避免冲突。

```csharp
var myPrincipal = new ClaimsPrincipal(myIdentity);

await HttpContext.SignInAsync("handler1", myPrincipal);
await HttpContext.SignInAsync("handler2", myPrincipal);

return Redirect("/");
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=895)

同样,你可以使用 `SignOutResult` 或 `SignOut` 辅助方法从多个方案中登出:

```csharp
[HttpPost]
public async Task<IActionResult> SignOutUser()
{
    Console.WriteLine("\r\nCalling HttpContext.SignOutAsync();");

    return SignOut(["handler1", "handler2"]);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=820)

#### 显示名称

`displayName` 属性是一个便于人阅读的字符串,用于"供人阅读"。
它对于构建动态登录页很有用,用户可以在页面上选择不同的提供方(例如 "Google SignIn" 与 "Facebook SignIn")。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "handler1";
}).AddCustomAuth(authenticationScheme: "handler1",
                displayName: "Google SignIn",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                    o.CookieName = "AuthCookie1";
                })
.AddCustomAuth(authenticationScheme: "handler2",
                displayName: "Facebook SignIn",
                configureOption: o =>
                {
                    o.LoginPath = "/user/login";
                    o.CookieName = "AuthCookie2";
                });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/schemes-69957948/?t=985)

## 2. Authentication Properties

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-69957949/) · 12:46

认证属性提供了一种向认证操作传递额外数据和配置的机制。
这使得行为可以是动态的,并能进行超出认证处理器中静态设置范围的定制。

### 总结

认证属性让开发者可以向登录、质询和登出等认证操作传递额外的数据和定制。
通过使用 `AuthenticationProperties` 类,开发者可以设置 `RedirectUri` 这样的内置属性,或者在 `Items` 字典中存储自定义元数据,这些元数据会被序列化并持久化到认证 Cookie 中。
该机制还支持一个 `Parameters` 字典,用于存放临时的、不序列化的数据,这些数据只用于认证处理器的本次执行。

### 核心概念

*   `AuthenticationProperties` 类:向认证处理器传递元数据的标准容器。
*   内置属性:预定义的字段,例如 `RedirectUri`、`IsPersistent`、`IssuedUtc` 和 `ExpiresUtc`。
*   `Items` 字典:一组基于字符串的键值对,会被序列化并持久化(例如存入认证 Cookie)。
*   `Parameters` 字典:一组对象,用于存放不会被序列化或持久化的临时数据。
*   动态覆盖:在运行时使用属性来覆盖静态配置选项。

### 课程笔记

#### 认证操作简介

ASP.NET Core 中的大多数认证操作都接受一个可选的 `AuthenticationProperties` 参数。
这包括 `SignInAsync`、`ChallengeAsync`、`SignOutAsync` 和 `ForbidAsync`。
这些属性让你可以提供额外的数据,以定制处理器执行该操作的方式。

```csharp
var principal = new ClaimsPrincipal();
await HttpContext.SignInAsync("handler1", principal, new AuthenticationProperties() { });

await HttpContext.ChallengeAsync("handler1", new AuthenticationProperties() { });
await HttpContext.SignOutAsync("handler1", new AuthenticationProperties() { });
await HttpContext.ForbidAsync("handler1", new AuthenticationProperties() { });

// Does not support AuthenticationProperties
var result = await HttpContext.AuthenticateAsync("handler1");
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=35)

注意,`AuthenticateAsync` 不支持认证属性。
这是因为认证只是读取当前请求的状态,不需要额外的输入数据来完成检查。

#### AuthenticationProperties 类

`AuthenticationProperties` 类为常见场景提供了若干内置属性,例如持久化和重定向。

```csharp
var properties = new AuthenticationProperties()
{
    // These are the predefined properties
    IsPersistent = true,
    RedirectUri = "/",
    IssuedUtc = DateTime.UtcNow,
    ExpiresUtc = DateTime.UtcNow.AddDays(1),
    AllowRefresh = true,
};

await HttpContext.ChallengeAsync("handler1", properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=60)

在内部,这些内置属性是对一个名为 `Items` 的字典的包装。
把 `IsPersistent` 这样的属性设为 `true`,等同于向 `Items` 字典添加一个特定的键(例如 `.persistent`)。

```csharp
var items = new Dictionary<string, string>
{
    [".persistent"] = "true",
    [".redirect"] = "/",
    [".issued"] = DateTime.UtcNow.ToString("o"),
    [".expires"] = DateTime.UtcNow.AddDays(1).ToString("o"),
    [".refresh"] = "true"
};

var prop = new AuthenticationProperties(items);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=115)

```text
  AuthenticationProperties        Items
  ------------------------        ---------------------
  IsPersistent  = true      ->    [".persistent"] = "true"
  RedirectUri   = "/"       ->    [".redirect"]   = "/"
  IssuedUtc                 ->    [".issued"]
  ExpiresUtc                ->    [".expires"]
  AllowRefresh  = true      ->    [".refresh"]    = "true"
```

#### Items 与 Parameters

`AuthenticationProperties` 维护着两个不同的字典:`Items` 和 `Parameters`。

1.  **Items**:存储与用户绑定的数据。
    这些值会被序列化,通常存储在认证 Cookie 中。
    示例包括用户偏好、会员积分或最后访问的页面。
2.  **Parameters**:存储与特定认证操作绑定的数据。
    这些值会直接传给认证处理器,但被标记了 `[JsonIgnore]`,这意味着它们不会被序列化,也不会存入 Cookie。
    示例包括 `prompt` 类型、`max_age` 或 `login_hint`。

```csharp
public class AuthenticationProperties
{
    /// <summary>
    /// State values about the authentication session.
    /// </summary>
    public IDictionary<string, string?> Items { get; }

    /// <summary>
    /// Collection of parameters that are passed to the authentication
    /// handler. These are not intended for serialization or persistence.
    /// </summary>
    [JsonIgnore]
    public IDictionary<string, object?> Parameters { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=205)

```text
  AuthenticationProperties
  +-- Items       (string -> string)  -> serialized -> auth cookie
  +-- Parameters  (string -> object)  -> [JsonIgnore] -> handler only
```

#### 演示:动态重定向

认证属性可以用来覆盖静态配置。
例如,一个自定义认证处理器可能会在其选项类中定义一个默认的重定向路径。

```csharp
public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    public string CookieName { get; set; } = "AuthCookie";
    public string LoginPath { get; set; } = "/User/Login";
    public string DefaultRedirectPath { get; set; } = "/AuthTest";
    public string AccessDeniedPath { get; set; } = "/User/AccessDenied";
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=295)

为了让重定向变成动态的(例如重定向到某个特定的用户资料页),你可以在控制器中调用 `SignInAsync` 时设置 `RedirectUri` 属性。

```csharp
var properties = new AuthenticationProperties()
{
    RedirectUri = "/users/bob"
};

await HttpContext.SignInAsync("handler1", myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=420)

在认证处理器内部,你必须更新逻辑,检查属性中是否提供了 `RedirectUri`。
如果存在就使用它;否则回退到默认路径。

```csharp
protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
{
    // ... cookie logic ...

    var redirectUri = properties.RedirectUri ?? Options.DefaultRedirectPath ?? "/";
    Response.Redirect(redirectUri);

    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=490)

#### 演示:在 Items 中存放自定义数据

你还可以把自定义数据提供给 `AuthenticationProperties` 构造函数,从而通过 `Items` 字典传递应用特定的数据。

```csharp
var items = new Dictionary<string, string>()
{
    { "Item1", "Value1" },
    { "Item2", "Value2" },
    { "Item3", "Value3" }
};

var properties = new AuthenticationProperties(items)
{
    RedirectUri = "/AuthTest"
};

await HttpContext.SignInAsync("handler1", myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-properties-mp4-69957949/?t=680)

## 3. Authentication Ticket

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/) · 17:36

### 总结

`AuthenticationTicket` 是 ASP.NET Core 认证中的核心数据结构,它把 `ClaimsPrincipal`、`AuthenticationProperties` 和认证方案打包成一个整体。
通过在登录时把这个票据序列化到认证 Cookie 中,并在认证时把它反序列化,系统确保所有用户声明(claim)和会话元数据都能跨请求保留下来。
本课演示如何用完整的 `AuthenticationTicket` 实现来替换只基于用户名的简单 Cookie,从而支持持久化复杂的身份数据和会话属性。

### 核心概念

* **AuthenticationTicket**:一个包装了 `ClaimsPrincipal`、`AuthenticationProperties` 和方案名称的容器。
* **Serialization**:使用 `TicketSerializer` 把票据转换为字节数组的过程。
* **Base64 Encoding**:用于把二进制的序列化票据转换为适合存入 Cookie 的字符串格式。
* **Claims Persistence**:通过存储完整的票据而不只是用户名,确保所有用户声明(例如角色、邮箱)都能跨请求保留。
* **Deserialization**:在认证阶段从 Cookie 值重建 `AuthenticationTicket`。

### 课程笔记

`AuthenticationTicket` 是 ASP.NET Core 认证中的核心数据结构。
它充当一个标准化的包,包装了表示一个已认证用户所需的全部信息。
具体来说,它包含认证方案(标识是哪个处理器创建了该票据)、`ClaimsPrincipal`(包含用户及其声明)以及 `AuthenticationProperties`(包含会话元数据)。

```csharp
public class AuthenticationTicket
{
    /// <summary>
    /// Gets the authentication scheme that was responsible for this ticket.
    /// </summary>
    public string AuthenticationScheme { get; }

    /// <summary>
    /// Gets the claims-principal with authenticated user identities.
    /// </summary>
    public ClaimsPrincipal Principal { get; }

    /// <summary>
    /// Additional state values for the authentication session.
    /// </summary>
    public AuthenticationProperties Properties { get; }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/?t=85)

```text
  AuthenticationTicket
  +-------------------------------------------------+
  | AuthenticationScheme  string (which handler)    |
  | Principal             ClaimsPrincipal           |
  | Properties            AuthenticationProperties  |
  +-------------------------------------------------+
```

#### 简单 Cookie 的问题

在基础实现中,认证 Cookie 里可能只存储了用户名。
然而,这种做法无法保留完整的用户声明和属性集合。
当应用让一个拥有多个角色或自定义元数据的用户登录时,除非把整个 principal 及其属性都持久化,否则这些细节就会丢失。

```csharp
[HttpPost]
public async Task<IActionResult> SignInUser()
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

    var myIdentity = new ClaimsIdentity(myClaims, "custom", "name", "role");
    var myPrincipal = new ClaimsPrincipal(myIdentity);
    // ...
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/?t=145)

#### 用 AuthenticationTicket 实现登录

为了保留完整的认证状态,`HandleSignInAsync` 方法必须把 `ClaimsPrincipal` 和 `AuthenticationProperties` 组合成一个 `AuthenticationTicket`。
然后使用 `TicketSerializer.Default.Serialize` 把这个票据序列化为字节数组。
由于 Cookie 是基于文本的,得到的字节数组在追加到响应之前会先进行 Base64 编码。

```csharp
protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
{
    var username = user.Identity?.Name ?? "Unknown";
    WriteToLog($"HandleSignInAsync: Signing in user '{username}'");

    // 1. Create the ticket
    var ticket = new AuthenticationTicket(user, properties, Scheme.Name);

    // 2. Serialize the ticket to bytes
    byte[] serializedTicket = TicketSerializer.Default.Serialize(ticket);

    // 3. Base64 encode for cookie storage
    var cookieValue = Convert.ToBase64String(serializedTicket);

    Response.Cookies.Append(key: Options.CookieName, value: cookieValue, new CookieOptions
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Lax
    });

    var redirectUrl = properties.RedirectUri ?? Options.DefaultRedirectPath ?? "/";
    Response.Redirect(redirectUrl);

    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/?t=310)

#### 从票据进行认证

在后续请求中,`HandleAuthenticateAsync` 方法读取 Cookie 并把这个过程反过来执行。
它把 Base64 字符串解码回字节数组,并使用 `TicketSerializer` 重建 `AuthenticationTicket`。
然后这个票据被包装进 `AuthenticateResult.Success` 并返回给框架。

```csharp
protected override Task<AuthenticateResult> HandleAuthenticateAsync()
{
    var path = Request?.Path ?? "[Unknown]";
    WriteToLog($"HandleAuthenticateAsync: Called for {path}");

    var authCookie = Request.Cookies[Options.CookieName];

    if (string.IsNullOrEmpty(authCookie))
    {
        WriteToLog($"No cookie {Options.CookieName} found - user not authenticated");
        return Task.FromResult(AuthenticateResult.NoResult());
    }
    else
    {
        try
        {
            // 1. Decode Base64
            var serializedTicket = Convert.FromBase64String(authCookie);

            // 2. Deserialize back to AuthenticationTicket
            var ticket = TicketSerializer.Default.Deserialize(serializedTicket);

            if (ticket != null)
                return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            // Handle invalid or tampered cookies
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
    }

    return Task.FromResult(AuthenticateResult.NoResult());
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/?t=640)

```text
  HandleSignInAsync
    AuthenticationTicket
      -> TicketSerializer.Default.Serialize    -> byte[]
      -> Convert.ToBase64String                -> cookie value
      -> Response.Cookies.Append

  HandleAuthenticateAsync
    Request.Cookies[Options.CookieName]
      -> Convert.FromBase64String              -> byte[]
      -> TicketSerializer.Default.Deserialize  -> AuthenticationTicket
      -> AuthenticateResult.Success(ticket)
```

#### 在 UI 中访问认证属性

票据一旦成功反序列化,就可以通过 `AuthenticateResult` 获得 `AuthenticationProperties`。
在 Razor 视图中,你可以调用 `Context.AuthenticateAsync()` 来获取结果,并显示 `IsPersistent`、`RedirectUri` 等属性,或者存储在票据中的自定义 item。

```razor
@using Microsoft.AspNetCore.Authentication
@{
    // Get authentication properties from the current context
    var authResult = await Context.AuthenticateAsync();
    var authProperties = authResult?.Properties;
}

<br />
<h5>Authentication Properties</h5>
@if (authProperties != null)
{
    <table class="table">
        <thead>
            <tr>
                <th class="col-md-3">Property</th>
                <th class="col-md-9">Value</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>IsPersistent</td>
                <td>@authProperties.IsPersistent</td>
            </tr>
            <tr>
                <td>RedirectUri</td>
                <td>@(authProperties.RedirectUri ?? "[Not Set]")</td>
            </tr>
            <tr>
                <td>ExpiresUtc</td>
                <td>@(authProperties.ExpiresUtc?.ToString("yyyy-MM-dd HH:mm:ss") ?? "[Not Set]")</td>
            </tr>
        </tbody>
    </table>
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/?t=880)

为了确保属性被填充,必须在控制器中把它们传入 `SignInAsync` 调用。

```csharp
var items = new Dictionary<string, string>()
{
    { "Item1", "Value1" },
    { "Item2", "Value2" },
    { "Item3", "Value3" }
};

var properties = new AuthenticationProperties(items)
{
    RedirectUri = "/AuthTest"
};

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authentication-ticket-69957945/?t=945)

## 4. Data Protection

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/data-protection-69957946/) · 8:54

### 总结

.NET 中的 Data Protection API 提供了一种加密机制,用于保护认证票据等敏感数据,防止客户端读取或篡改。
通过从简单的 Base64 编码转向使用提供程序(provider)和特定保护器(protector)的完整加密,开发者可以确保会话数据在整个请求-响应生命周期中保持机密性和完整性。

### 核心概念

- **Data Protection API**:.NET 内置的、用于对数据进行加密保护的框架。
- **Protect and Unprotect**:用于保护数据和还原数据的两个主要操作。
- **Encryption Keys**:由该 API 自动管理,用于保护数据;保护和解除保护使用的是同一个密钥。
- **Purpose Strings**:唯一标识符(例如 "MyApp" 和 "AuthTicket"),用于隔离受保护的数据,并确保只有正确的保护器才能解密它。
- **Tamper Resistance**:防止攻击者修改 Cookie 值(例如更改 admin 声明)来提升权限。

### 课程笔记

在标准的认证流程中,服务器可能会返回一个包含用户 ID 和管理员状态等会话数据的 Cookie。
如果这个 Cookie 只是经过 Base64 编码,它就容易遭到篡改。
攻击者可以截获该 Cookie,对其解码,把自己的管理员状态改为 `true`,再重新编码,从而获得未经授权的访问。

为了防止这种情况,.NET 提供了 Data Protection API。
该 API 使用加密密钥,通过两个主要操作来保护数据:`Protect` 和 `Unprotect`。
当用户登录时,应用创建一个认证票据,使用该 API 对其加密,并把得到的加密字符串存入 Cookie。
在后续请求中,服务器解密 Cookie 以还原票据。
如果 Cookie 被修改过,解密就会失败,用户也就不会通过认证。

#### 在登录中实现数据保护

为了在登录过程中保护认证票据,你必须先创建一个数据保护提供程序,然后再创建一个特定的保护器。
提供程序是主要入口,而保护器用于对特定类型的数据执行实际的加密操作。

```csharp
protected override Task HandleSignInAsync(ClaimsPrincipal user,
                                          AuthenticationProperties properties)
{
    var username = user.Identity?.Name ?? "Unknown";
    WriteToLog($"HandleSignInAsync: Signing in user '{username}'");

    var ticket = new AuthenticationTicket(user, properties, Scheme.Name);

    byte[] serializedTicket = TicketSerializer.Default.Serialize(ticket);

    var provider = DataProtectionProvider.Create("MyApp");
    var protector = provider.CreateProtector("AuthTicket");

    var protectedBytes = protector.Protect(serializedTicket);

    var cookieValue = Convert.ToBase64String(protectedBytes);

    Response.Cookies.Append(key: Options.CookieName, value: cookieValue, new CookieOptions
    {
        HttpOnly = true,
        SameSite = SameSiteMode.Lax
    });

    var redirectUrl = Options.DefaultRedirectPath;

    if (properties.RedirectUri != null)
    {
        redirectUrl = properties.RedirectUri;
    }
    
    Response.Redirect(redirectUrl);
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/data-protection-69957946/?t=295)

```text
  AuthenticationTicket
    -> TicketSerializer.Default.Serialize   -> serializedTicket
    -> protector.Protect                    -> protectedBytes
       (DataProtectionProvider.Create("MyApp")
          .CreateProtector("AuthTicket"))
    -> Convert.ToBase64String               -> cookieValue
```

票据一旦受到保护,就不再是人类可读的,也无法再用标准的 Base64 工具轻易解码。
任何对该 Cookie 进行解码的尝试都只会得到随机的、加密的数据。

#### 在认证中实现数据保护

为了让应用在后续请求中识别用户,必须更新 `HandleAuthenticateAsync` 方法来解密 Cookie。
至关重要的是,提供程序名称("MyApp")和保护器用途("AuthTicket")必须与登录过程中使用的完全一致。

```csharp
protected override Task<AuthenticateResult> HandleAuthenticateAsync()
{
    var authCookie = Request.Cookies[Options.CookieName];

    if (string.IsNullOrEmpty(authCookie))
    {
        WriteToLog($"No cookie {Options.CookieName} found - user not authenticated");
        return Task.FromResult(AuthenticateResult.NoResult());
    }
    else
    {
        try
        {
            var serializedTicket = Convert.FromBase64String(authCookie);

            var provider = DataProtectionProvider.Create("MyApp");
            var protector = provider.CreateProtector("AuthTicket");

            var unprotectedBytes = protector.Unprotect(serializedTicket);

            var ticket = TicketSerializer.Default.Deserialize(unprotectedBytes);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception ex)
        {
            // Handle bad cookie
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/data-protection-69957946/?t=505)

#### 在 .NET 内部的使用

Data Protection API 是贯穿整个 ASP.NET Core 的基础安全组件:

- **Cookie Handler**:保护认证票据。
- **Anti-forgery System**:保护用于防范跨站请求伪造(CSRF)攻击的令牌。
- **Session**:保护用于标识用户会话的会话 Cookie。
- **OpenID Connect**:保护安全认证流程所需的 state 和 nonce 值。

## 5. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957947/) · 2:56

### 总结

本课全面回顾了 .NET 中掌管认证的内部组件,包括认证方案、属性和票据。
它详细说明了这些组件如何与 Data Protection API 交互以保护认证数据,并阐明了使用声明表示身份与使用认证属性表示会话特定元数据之间的架构区别。

### 核心概念

*   **Authentication Schemes**:决定针对 Authenticate、Challenge、SignIn 和 SignOut 等特定动作执行哪些认证逻辑和处理器。
*   **Authentication Properties**:一个元数据容器,由 `Items`(持久化在票据中)和 `Parameters`(只在当前动作中使用)组成。
*   **Authentication Ticket**:一个统一的容器,存放认证方案名称、`ClaimsPrincipal` 和 `AuthenticationProperties`。
*   **Data Protection API**:对序列化后的认证票据进行加密和保护,防止篡改和未经授权的读取。
*   **Claims vs. Items**:声明表示用户的身份("谁"),而 item 表示认证会话或流程("如何")。

### 课程笔记

.NET 中的认证过程依赖若干内部组件协同工作,来管理用户身份和会话状态。

#### 内部组件

该系统建立在四大支柱之上:

1.  **Schemes**:决定每个认证动作运行哪段逻辑。
2.  **Authentication Properties**:存储随认证过程一起传递的额外数据。
3.  **Authentication Ticket**:充当主要容器,把方案、用户身份(principal)和属性组合成一个对象。
4.  **Data Protection API**:提供安全层,使用加密来保护票据数据,防止被攻击者读取或修改。

#### 认证属性与票据

`AuthenticationProperties` 对象包含两个不同的集合。
`Items` 集合存储的数据会在认证票据被序列化到 Cookie 中时被持久化。
常见属性,例如重定向 URI 或签发时间戳,通常会映射到这个集合中。
相比之下,`Parameters` 集合用于只在特定认证动作中需要的数据,在票据序列化时不会被持久化。

`AuthenticationTicket` 把认证方案、`ClaimsPrincipal` 和 `AuthenticationProperties` 打包在一起。
为了保护这些信息,Data Protection API 使用加密密钥对票据进行加密。
然后这些受保护的数据被存入 Cookie。
这确保了客户端无法篡改身份或会话元数据。

```text
  scheme + ClaimsPrincipal + AuthenticationProperties
                      |
                      v
             AuthenticationTicket
                      |
                      v   Data Protection API (encryption key)
               protected data -> cookie
```

#### 声明与认证 Item

一个常见的设计决策是:某个特定值应该作为声明存储,还是作为 `AuthenticationProperties` 中的 item 存储。

**Claims** 描述用户的身份,是 `ClaimsPrincipal` 的一部分。
它们直接通过 `User` 对象访问,用于定义用户是谁的值,例如用户 ID、姓名或邮箱地址。
声明也是授权逻辑所使用的主要机制。

**Items** 绑定于当前的登录会话,而不是身份本身。
它们支持认证流程或会话行为,例如返回 URL、登录 IP 地址或"记住我"标志。
与声明不同,item 不通过 `User` 对象访问;要在之后读取它们,你必须调用 `HttpContext.AuthenticateAsync()`,它提供对 `AuthenticationTicket` 及其属性的访问。

```csharp
var myClaims = new List<Claim>()
{
    new("sub", "1234"),
    new("name", "Bob"),
    new("email", "bob@tn-data.se"),
};

var items = new Dictionary<string, string>()
{
    { "returnUrl", "/checkout" },
    { "LoginIp", "192.168.0.1" },
    { "rememberMe", "true" },
};

var prop = new AuthenticationProperties(items);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957947/?t=115)

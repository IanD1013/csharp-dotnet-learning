# The Cookie Handler

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 6 章
> 共 7 课 · 约 1:25:07
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/) | 9:00 | [↓](#1-introduction) |
| 2 | [Challenge](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/) | 13:39 | [↓](#2-challenge) |
| 3 | [SignIn](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957952/) | 24:01 | [↓](#3-signin) |
| 4 | [Authenticate](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957953/) | 6:48 | [↓](#4-authenticate) |
| 5 | [SignOut](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/) | 19:59 | [↓](#5-signout) |
| 6 | [Forbid](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/) | 10:48 | [↓](#6-forbid) |
| 7 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/) | 0:52 | [↓](#7-section-recap) |

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/) · 9:00

### 总结

ASP.NET Core 提供了一个内置的 cookie 身份验证处理程序,它安全、经过充分测试,是生产应用的标准选择。
该处理程序使用 Data Protection API 加密身份数据,并支持五种核心身份验证操作:Authenticate、Sign In、Sign Out、Forbid 和 Challenge。
通过用这一原生实现替换自定义构建的处理程序,开发者可以获得一个健壮的身份验证系统,它能与默认身份验证方案无缝集成,并为管理会话生命周期和重定向路径提供丰富的配置选项。

### 核心概念

- **Built-in Cookie Handler**:ASP.NET Core 中可用于生产环境的身份验证处理程序,用于替换自定义实现。
- **Data Protection API**:cookie 处理程序用来加密 cookie 中用户身份的底层机制。
- **Authentication Operations**:该处理程序支持五种核心操作:Authenticate(验证 cookie)、Sign In(创建 cookie)、Sign Out(删除 cookie)、Forbid(拒绝访问)和 Challenge(重定向到登录页)。
- **Default Schemes**:配置默认方案后,应用会自动为所有身份验证操作选择 cookie 处理程序,而无需在每个控制器 action 中显式指定名称。
- **CookieAuthenticationDefaults**:一个辅助类,包含 cookie 处理程序的标准常量值,例如默认方案名 "Cookies"。

### 课程笔记

#### Transitioning to the Built-in Cookie Handler

虽然自定义身份验证处理程序有助于理解内部机制,但生产应用应当使用内置的 cookie 身份验证处理程序。
该处理程序以安全为设计目标,通过 Data Protection API 将身份信息存储在加密的 cookie 中。
它是大多数 ASP.NET Core 应用中基于 cookie 登录的默认选择。

要添加 cookie 处理程序,需要在 `Program.cs` 文件中配置身份验证服务。
这包括调用 `AddAuthentication` 来设置默认方案,以及调用 `AddCookie` 来注册处理程序及其特定选项。

```csharp
builder.Services.AddAuthentication(options =>
{
    // Authentication Configuration Options
    options.DefaultScheme = "cookie";
})
.AddCookie("cookie", options =>
{
    // Cookie Configuration Options
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/denied";

    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;

    // And more...
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/?t=55)

#### Supported Operations

cookie 处理程序支持全部五种标准身份验证操作:

1. **Authenticate**:在每个请求上读取并验证 cookie。
2. **Sign In**:在用户登录时创建 cookie。
3. **Sign Out**:在用户登出时删除 cookie。
4. **Forbid**:处理拒绝访问的场景。
5. **Challenge**:将未通过身份验证的用户重定向到登录页。

#### Refactoring from Custom Handlers

替换自定义处理程序时,需要移除特定的方案注册,并用标准的 cookie 处理程序取而代之。
通过设置 `DefaultScheme`,可以简化配置,使应用默认知道所有操作该使用哪个处理程序。

重构之前,应用可能注册了多个自定义方案:

```csharp
builder.Services.AddAuthentication(o =>
{
    // Customize the middleware
    o.DefaultScheme = "handler1";
    o.DefaultChallengeScheme = "handler2";
})
.AddCustomAuth(authenticationScheme: "handler1",
                 displayName: "Google SignIn",
                 configureOption: o =>
                 {
                     o.LoginPath = "/user/login";
                     o.CookieName = "AuthCookie1";
                 });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/?t=160)

重构之后,配置被精简为使用内置的 cookie 处理程序:

```csharp
builder.Services.AddAuthentication(o =>
{
    // Default scheme is set here or in AddCookie
}).AddCookie(o =>
{
    // Default configuration
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/?t=175)

#### Updating Controllers for Default Schemes

在 `Program.cs` 中配置好默认方案后,就可以简化控制器。
无需在 `ChallengeAsync` 或 `SignInAsync` 之类的方法中显式指定方案名,而是可以依赖默认方案。
例如,在 `AuthTestController` 中,你会从结果中移除具体的方案字符串。

```csharp
[HttpPost]
public async Task<IActionResult> ChallengeUser()
{
    Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

    // By removing the scheme name, it uses the default scheme
    return Challenge();
}

[HttpPost]
public async Task<IActionResult> SignInUser()
{
    // ... setup principal and properties
    await HttpContext.SignInAsync(myPrincipal, properties);
    return Empty;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/?t=205)

#### Scheme Naming Conventions

每个身份验证处理程序都有一个默认方案名。
对 cookie 处理程序而言,它定义在 `CookieAuthenticationDefaults` 类中。
常量 `CookieAuthenticationDefaults.AuthenticationScheme` 的值是字符串 "Cookies"。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
{

});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/?t=370)

虽然使用该常量是标准做法,但有些开发者更喜欢使用自定义字符串字面量,以避免大小写上的混淆(例如 "Cookies" 与 "cookies")。
一个常见的约定是,对默认方案和处理程序注册都使用小写、单数形式的字符串,比如 "cookie",以确保一致性,并防止因拼写错误导致身份验证静默失败。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";

}).AddCookie("cookie", o =>
{

});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/introduction-69957950/?t=475)

## 2. Challenge

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/) · 13:39

### 总结

challenge 操作是 cookie 处理程序在未通过身份验证的用户尝试访问受保护资源时,将其重定向到登录页的机制。
这一过程涉及两个主要的配置点:LoginPath,定义目标登录页;以及 RedirectUri,决定用户在成功通过身份验证后应被送往何处。
默认情况下,cookie 处理程序会自动把当前页面的 URL 作为 ReturnUrl 查询参数附加到登录路径上,让应用能在登录后把用户带回原本的目的地,从而确保无缝的用户体验。

### 核心概念

* **ChallengeAsync**:用于在代码中手动触发 challenge 的方法,调用时可以显式指定身份验证方案,也可以不指定。
* **LoginPath**:`CookieAuthenticationOptions` 中的一个配置选项,用于指定登录页的 URL(默认为 `/Account/Login`)。
* **RedirectUri**:`AuthenticationProperties` 中的一个属性,用于指定用户成功登录后应被重定向到哪里,会覆盖默认行为。
* **ReturnUrl**:在 challenge 过程中自动生成的查询字符串参数,用于记录用户原本所在的位置。
* **ChallengeResult**:MVC 控制器中用于发起 challenge 流程的 `IActionResult`。

### 课程笔记

#### Triggering the Challenge Operation

challenge 操作往往是用户访问受保护资源时与身份验证系统的第一次交互。
在代码中触发 challenge 主要有三种方式:

1. 不带参数调用 `ChallengeAsync`,以使用配置的默认方案。
2. 显式指定身份验证方案。
3. 在指定方案的同时传入 `AuthenticationProperties`,以控制行为,例如重定向目标。

```csharp
// Use the default scheme
await HttpContext.ChallengeAsync();

// Explicit scheme
await HttpContext.ChallengeAsync("cookie");

// Explicit scheme and properties
var properties = new AuthenticationProperties()
{
    RedirectUri = "/"
};

await HttpContext.ChallengeAsync("cookie", properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=25)

#### Configuration Options

有两个主要的配置选项决定 challenge 的行为。
`LoginPath` 在最初配置 cookie 处理程序时设置,定义未通过身份验证的用户的目的地。
`RedirectUri` 针对每个 challenge 请求在 `AuthenticationProperties` 中设置,定义用户成功登录后的去向。
如果没有显式设置 `RedirectUri`,处理程序会自动使用当前页面作为返回目的地。

```csharp
.AddCookie("cookie", o =>
{
    // URL to the Login page
    o.LoginPath = "/user/login";
});

var prop = new AuthenticationProperties()
{
    // Redirect target after login
    RedirectUri = "/home/welcome",
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=100)

触发 challenge 时,cookie 处理程序会构建一个重定向响应,其中 `LoginPath` 成为目的地,而 `RedirectUri`(或当前页面)会作为 `ReturnUrl` 查询参数附加上去。

```text
ChallengeAsync()
      |
      v
redirect -> LoginPath  +  ?ReturnUrl=  +  RedirectUri (or current page)
            "/user/login"               "/home/welcome"
```

#### Default Values

如果没有提供任何配置,cookie 处理程序会使用 `CookieAuthenticationDefaults` 类中定义的默认值。
默认身份验证方案是 "Cookies",默认登录路径是 `/Account/Login`。

```csharp
public static class CookieAuthenticationDefaults
{
    /// <summary>
    /// The default value used for CookieAuthenticationOptions.AuthenticationScheme
    /// </summary>
    public const string AuthenticationScheme = "Cookies";

    /// <summary>
    /// The default value used by CookieAuthenticationMiddleware for the
    /// CookieAuthenticationOptions.LoginPath
    /// </summary>
    public static readonly PathString LoginPath = new PathString("/Account/Login");

    /// <summary>
    /// The default value used by CookieAuthenticationMiddleware for the
    /// CookieAuthenticationOptions.LogoutPath
    /// </summary>
    public static readonly PathString LogoutPath = new PathString("/Account/Logout");

    /// <summary>
    /// The default value used by CookieAuthenticationMiddleware for the
    /// CookieAuthenticationOptions.AccessDeniedPath
    /// </summary>
    public static readonly PathString AccessDeniedPath = new PathString("/Account/AccessDenied");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=250)

#### Overriding the Login Path

要使用自定义登录页,必须在 `Program.cs` 中的 `AddCookie` 配置里覆盖 `LoginPath`。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
}).AddCookie("cookie", o =>
{
    o.LoginPath = "/user/login";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=295)

#### Customizing the Redirect URI in Controllers

在 MVC 控制器中,可以通过返回 `ChallengeResult` 来触发 challenge。
这让你可以指定自定义的 `AuthenticationProperties`,以控制发送到登录页的 `ReturnUrl`。

```csharp
[HttpPost]
public async Task<IActionResult> ChallengeUser()
{
    Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

    var properties = new AuthenticationProperties()
    {
        RedirectUri = "/user/info"
    };

    return new ChallengeResult(properties);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=490)

#### Handling the Return URL in the Login Flow

登录页的设计必须能从查询字符串中捕获 `ReturnUrl`,并在整个登录过程中保留它,通常是在登录表单中使用一个隐藏输入字段。

```csharp
[HttpGet]
public IActionResult Login(string ReturnUrl)
{
    return View(new LoginModel() { ReturnUrl = ReturnUrl });
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginModel loginCredentials)
{
    // After successful sign-in logic...
    return Redirect(loginCredentials.ReturnUrl ?? "/");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=640)

在 Razor 视图中,`ReturnUrl` 按如下方式保存:

```razor
@using (Html.BeginForm("Login", "User", FormMethod.Post))
{
    <!-- form fields for username and password -->
    <input asp-for="ReturnUrl" type="hidden" />
    <button type="submit" class="btn btn-primary">Login</button>
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=565)

#### Improving Security Checks with Challenge

与其使用硬编码字符串手动把未通过身份验证的用户重定向到拒绝访问页或登录页,不如使用 `Challenge()` 方法。
这样可以让身份验证系统根据所配置的方案来决定合适的下一步。

```csharp
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
            // Triggers the configured challenge behavior
            return Challenge();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/challenge-69957951/?t=790)

## 3. SignIn

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957952/) · 24:01

### 总结

SignInAsync 操作是使用 cookie 处理程序建立已通过身份验证的会话的核心机制。
调用它时,处理程序会创建一个包含用户 claims 和属性的身份验证票据(authentication ticket),通过 Data Protection API 对其加密以防止篡改,并在 HTTP 响应中发出一个 Set-Cookie 头。
登录逻辑中相当大的一部分涉及管理身份验证之后的重定向。
默认情况下,处理程序返回 200 OK 状态而不进行重定向;不过,可以将其配置为使用 ReturnUrl 查询参数(前提是请求发生在指定的 LoginPath 上),或使用在 AuthenticationProperties 中定义的显式 RedirectUri。
为了降低开放重定向攻击等安全风险,开发者应使用 LocalRedirect 或 Url.IsLocalUrl 来验证重定向目标。

### 核心概念

- SignInAsync 的内部工作流程(创建票据、加密、发出 cookie)。
- Data Protection API 在保护身份验证 cookie 方面的作用。
- ReturnUrl 与所配置的 LoginPath 之间的依赖关系。
- 使用 AuthenticationProperties.RedirectUri 覆盖重定向行为。
- 使用本地 URL 验证来防止开放重定向攻击。

### 课程笔记

SignInAsync 方法负责持久化用户的身份。
调用它时,cookie 处理程序会执行几个内部步骤:生成一个身份验证票据(包含 ClaimsPrincipal、AuthenticationProperties 和方案名),使用 Data Protection API 保护该票据以确保客户端无法读取或篡改它,最后把加密后的 cookie 附加到响应上。

```csharp
var properties = new AuthenticationProperties(items)
{
    // When the authentication ticket was issued
    IssuedUtc = DateTimeOffset.UtcNow,

    // When the authentication ticket expires
    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),

    // True if this is a persistent cookie
    IsPersistent = true,

    // Where to redirect after sign in
    RedirectUri = "/",
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957952/?t=10)

```text
SignInAsync(principal, properties)
      |
      v
+------------------------------+
| AuthenticationTicket         |
|   ClaimsPrincipal            |
|   AuthenticationProperties   |
|   scheme name                |
+------------------------------+
      |  Data Protection API (protect)
      v
encrypted cookie --> response (Set-Cookie)
```

#### Redirect Behavior and LoginPath

默认情况下,调用 SignInAsync 不会触发重定向;它只是返回一个带有 Set-Cookie 头的 200 OK 响应。
为了启用自动重定向,处理程序会查找 ReturnUrl 查询参数。
但出于安全原因,只有当登录请求发往身份验证选项中配置为 LoginPath 的路径时,cookie 处理程序才会采纳 ReturnUrl。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
}).AddCookie("cookie", o =>
{
    o.LoginPath = "/user/login";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957952/?t=505)

如果登录发生在其他端点上,ReturnUrl 会被忽略,除非开发者手动处理重定向,或在 AuthenticationProperties 中设置 RedirectUri 属性。
RedirectUri 属性始终优先于 ReturnUrl 查询参数。

#### Implementing the Login Flow

在标准的登录流程中,Login GET action 显示表单,POST action 处理凭据。
为确保 ReturnUrl 在表单提交过程中得以保留,必须把它包含在表单的 action URL 中,或作为隐藏字段。

```razor
@using StartcodeAuthentication.Features.User
@model LoginModel

<h1>Login</h1>

@using (Html.BeginForm("Login", "User", new { ReturnUrl = Model.ReturnUrl } , FormMethod.Post))
{
    <div class="form-group">
        <label asp-for="UserName"></label>
        <input asp-for="UserName" class="form-control" />
        <span asp-validation-for="UserName" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="Password"></label>
        <input asp-for="Password" class="form-control" />
        <span asp-validation-for="Password" class="text-danger"></span>
    </div>

    <input asp-for="ReturnUrl" type="hidden" />

    <button type="submit" class="btn btn-primary">Login</button>
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957952/?t=970)

#### Securing Redirects

在登录后手动重定向用户会带来开放重定向攻击的风险,攻击者可以构造一个 URL,把用户重定向到恶意的外部站点。
为防止这种情况,ASP.NET Core 提供了 LocalRedirect 方法,如果 URL 不是应用本地的,它会抛出异常。
另外,开发者也可以在重定向之前使用 Url.IsLocalUrl 来验证路径。

```csharp
var properties = new AuthenticationProperties(items)
{
    //RedirectUri = "/AuthTest"
};

await HttpContext.SignInAsync(myPrincipal, properties);

if(Url.IsLocalUrl(loginCredentials.ReturnUrl))
{
    return LocalRedirect(loginCredentials.ReturnUrl);
}
else
{
    return LocalRedirect("/");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signin-69957952/?t=1360)

## 4. Authenticate

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957953/) · 6:48

### 总结

authenticate 操作是 cookie 处理程序的核心组成部分,它检查传入的请求以确立用户的身份。
它包括通过 Data Protection API 解密 cookie,并生成一个包含 ClaimsPrincipal 和 AuthenticationProperties 的 AuthenticationTicket。
虽然这通常由中间件自动处理,但开发者可以直接调用 AuthenticateAsync 来访问详细的身份验证元数据,例如签发和过期时间戳,或持久化在 cookie 中的自定义条目。
其中一个关键区别在于:AuthenticationProperties.Items 会存储在 cookie 中,而 AuthenticationProperties.Parameters 是临时的,只在身份验证过程中使用。

### 核心概念

- **Authentication Ticket**:身份验证成功后创建的容器,保存 `ClaimsPrincipal`、`AuthenticationProperties` 以及身份验证方案的名称。
- **Data Protection API**:cookie 处理程序用来解密传入 cookie 的底层机制;如果解密失败,身份验证即失败。
- **AuthenticateAsync**:`HttpContext` 上可用的一个方法,允许手动触发身份验证逻辑以获取 `AuthenticateResult`。
- **AuthenticationProperties.Items**:一个字符串值的字典,会被序列化并持久化到身份验证 cookie 中。
- **AuthenticationProperties.Parameters**:一个对象值的字典,用于在登录等操作期间定制处理程序的行为,但不会持久化到 cookie 中。
- **Automatic Metadata**:cookie 处理程序会自动向身份验证属性中添加 `.issued` 和 `.expires` 条目,通常默认为两周的生命周期。

### 课程笔记

从高层来看,authenticate 操作会检查传入的请求,以创建一个有效的用户。
当收到一个包含 cookie 的请求时,cookie 处理程序首先使用 Data Protection API 解密数据。
如果解密失败,身份验证过程会立即失败。
如果成功,就会创建一个 `AuthenticationTicket`。
该票据包含 `ClaimsPrincipal`、`AuthenticationProperties` 和方案名。

```text
request with cookie
      |
      v
Data Protection API (decrypt) --fail--> authentication fails
      |
      | success
      v
AuthenticationTicket { ClaimsPrincipal, AuthenticationProperties, scheme }
```

虽然这一过程通常在身份验证中间件中自动发生,但当你需要访问所有身份验证细节时,可以直接调用 authenticate 操作。
它会返回一个 `AuthenticateResult` 对象,通过它可以访问票据、属性和 principal。

```csharp
// Manually trigger authentication and get the result (Default Scheme)
AuthenticateResult res1 = await HttpContext.AuthenticateAsync();

// Explicit Scheme
AuthenticateResult res2 = await HttpContext.AuthenticateAsync("cookie");

var Ticket = res2.Ticket;
var authProperties = res2.Properties;
var principal = res2.Principal;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957953/?t=70)

#### Persisting Data in Cookies

让用户登录时,可以提供 `AuthenticationProperties`。
cookie 处理程序会自动向这些属性中添加元数据,具体是 `.issued`(票据创建的时间)和 `.expires`(票据过期的时间)。
默认情况下,cookie 处理程序设置的生命周期为两周。

开发者也可以向 `AuthenticationProperties` 中的 `Items` 字典添加自定义数据。
这些条目会直接持久化到 cookie 中,并在后续请求中调用 `AuthenticateAsync` 时可用。

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginModel loginCredentials, string returnUrl = null)
{
    var userName = loginCredentials.UserName;

    var myClaims = new List<Claim>()
    {
        new("sub","1234"),
        new("name",userName),
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

    var items = new Dictionary<string, string>()
    {
        { "Item1", "Value1" },
        { "Item2", "Value2" },
        { "Item3", "Value3" },
    };

    var properties = new AuthenticationProperties(items);

    await HttpContext.SignInAsync(myPrincipal, properties);

    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
    {
        return LocalRedirect(returnUrl);
    }
    return LocalRedirect("/");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957953/?t=150)

#### Items vs. Parameters

区分 `AuthenticationProperties` 中的 `Items` 和 `Parameters` 很重要。

- **Items**:存储在身份验证票据内部,并持久化到 cookie 中。
- **Parameters**:只在特定操作(例如 `SignInAsync`)期间使用,用于定制处理程序的行为。
  它们不会存储在身份验证票据中,在后续请求中对用户进行身份验证时也不会出现。

```csharp
var parameters = new Dictionary<string, object>()
{
    { "Param1", "Value1" },
    { "Param2", "Value2" },
    { "Param3", "Value3" }
};

var items = new Dictionary<string, string>()
{
    { "Item1", "Value1" },
    { "Item2", "Value2" },
    { "Item3", "Value3" }
};

var properties = new AuthenticationProperties(items, parameters);

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957953/?t=280)

#### Inspecting Results in the UI

可以在 Razor 视图或控制器中使用 `AuthenticateAsync` 来检查当前的身份验证状态和属性。
这对于调试或向用户显示过期时间之类的元数据很有用。

```razor
@using Microsoft.AspNetCore.Authentication
@{
    // Get authentication properties manually
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
            @foreach (var item in authProperties.Items)
            {
                <tr>
                    <td>@item.Key</td>
                    <td>@item.Value</td>
                </tr>
            }
        </tbody>
    </table>
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/authenticate-69957953/?t=295)

## 5. SignOut

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/) · 19:59

### 总结

本课讲解如何使用 ASP.NET Core 的 cookie 处理程序实现登出操作,并详细说明了通过设置过去的过期日期来删除 cookie 的内部机制。
它涵盖了通过 LogoutPath 和 RedirectUri 配置重定向行为,同时强调关键的安全实践。
具体来说,它演示了为什么登出端点必须限制为 HTTP POST 请求,并使用防伪令牌防范跨站请求伪造(CSRF),以防止恶意脚本或标签触发未经授权的或自动的登出。

### 核心概念

* **Cookie Deletion**:处理程序通过发送一个过期日期在过去的 `Set-Cookie` 头来移除 cookie。
* **LogoutPath**:一个配置选项,定义 `ReturnUrl` 查询参数会被采纳的路径。
* **RedirectUri**:`AuthenticationProperties` 中的一个显式属性,会覆盖 `ReturnUrl` 逻辑。
* **HTTP POST Requirement**:登出绝不应支持 GET 请求,以防止通过 `<img>` 之类的简单 HTML 标签被未经授权地触发。
* **CSRF Protection**:使用 `[ValidateAntiForgeryToken]` 和 `@Html.AntiForgeryToken()` 来确保登出请求是有意的,并且来自应用自身的 UI。

### 课程笔记

调用 `SignOutAsync` 时,cookie 处理程序会指示浏览器删除身份验证 cookie。
这是通过以一个设在过去的过期日期重新发出该 cookie 来实现的。

```csharp
await HttpContext.SignOutAsync("cookie");
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/?t=10)

#### Configuring Sign-Out Redirects

用户登出后的重定向主要有两种处理方式:

1. **LogoutPath**:在 cookie 处理程序选项中配置,它定义处理程序会在哪个路径上查找 `ReturnUrl` 查询参数。
   如果用户在这个路径上登出,就会被重定向到 `ReturnUrl` 中的值。
2. **RedirectUri**:在登出调用时于 `AuthenticationProperties` 中设置。
   无论当前路径是什么,这个值始终优先,并覆盖任何 `ReturnUrl` 逻辑。

```csharp
var properties = new AuthenticationProperties()
{
    // Where to redirect after sign out
    RedirectUri = "/User/LoggedOut"
};

await HttpContext.SignOutAsync(properties);

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie", o =>
    {
        // Path where the ReturnUrl query string is used
        o.LogoutPath = "/user/LoggedOut";
    });
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/?t=40)

#### Implementing Logout in the Controller

在 `UserController` 中,logout 方法应调用 `SignOutAsync`,然后重定向到一个确认页面。

```csharp
[HttpPost]
public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync();

    return LocalRedirect("/user/LoggedOut");
}

public IActionResult LoggedOut()
{
    return View();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/?t=265)

#### Security: Restricting to HTTP POST

登出端点必须只支持 HTTP POST 方法。
如果端点支持 GET,攻击者就可以在恶意站点上把 `/user/logout` 这样的 URL 嵌入到 `<img>` 标签的 `src` 属性中。
当用户的浏览器尝试"加载"这张图片时,它会向登出端点发送一个 GET 请求,实际上在未经用户同意的情况下让用户登出。

#### Security: Cross-Site Request Forgery (CSRF)

即使限制为 POST,攻击者仍可以使用 JavaScript(`fetch`)来触发登出。
为防止这种情况,端点必须用防伪令牌保护。
这可以确保请求是从应用真正的登出表单发起的。

要实现这一点,请在 action 上应用 `[ValidateAntiForgeryToken]` 特性:

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync();

    return LocalRedirect("/user/loggedOut");
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/?t=760)

要调试防伪相关的问题,可以在 `appsettings.Development.json` 中提高 MVC 视图功能过滤器的日志详细程度:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.AspNetCore.Mvc.ViewFeatures.Filters": "Debug"
    }
  }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/?t=895)

最后,UI 必须使用 `@Html.AntiForgeryToken()` 辅助方法在登出表单中包含该令牌:

```razor
<li class="nav-item">
    <form action="/User/Logout" class="form-inline" method="post">
        @Html.AntiForgeryToken()
        <button type="submit" class="nav-link btn btn-link text-dark">Logout</button>
    </form>
</li>
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/signout-69957954/?t=1000)

## 6. Forbid

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/) · 10:48

### 总结

ASP.NET Core cookie 处理程序中的 Forbid 操作用于处理那些已通过身份验证、但缺少访问某资源所需权限的用户。
通过配置 AccessDeniedPath,开发者可以把这些用户重定向到自定义页面。
cookie 处理程序会自动在重定向中附加一个 ReturnUrl 查询参数,可用于记录未经授权的访问尝试或向用户提供反馈。
此外,在手动触发 forbid 结果时,AuthenticationProperties 中的 RedirectUri 属性允许开发者覆盖默认的 ReturnUrl 行为。

### 核心概念

*   **Forbid vs. Challenge**:Challenge 用于未通过身份验证的用户(重定向到登录页),而 Forbid 用于已通过身份验证但缺少授权的用户(重定向到拒绝访问页)。
*   **AccessDeniedPath**:cookie 处理程序中的一个配置项,定义被禁止请求的重定向目的地。
*   **ReturnUrl**:自动附加到拒绝访问重定向上的查询参数,指示用户原本试图访问的资源。
*   **RedirectUri**:`AuthenticationProperties` 中的一个属性,用于覆盖默认的 `ReturnUrl` 值。
*   **Logging and Feedback**:利用 `ReturnUrl` 记录未经授权的尝试,并在拒绝访问页面上为用户提供与上下文相关的帮助。

### 课程笔记

当一个已通过身份验证的用户尝试访问其无权查看的资源时,会触发 `Forbid` 操作。
在 cookie 处理程序的语境下,这会导致重定向到一个特定的 "Access Denied" 路径。

#### Configuring the Cookie Handler

默认情况下,cookie 处理程序会把被禁止的请求重定向到 `/Account/AccessDenied`。
可以通过在 `AddCookie` 配置中设置 `AccessDeniedPath` 属性来覆盖它。
此外,在手动调用 `ForbidAsync` 时,可以提供 `AuthenticationProperties` 来定制行为,例如使用 `RedirectUri` 属性覆盖返回 URL。

```csharp
.AddCookie("cookie", o =>
{
    // Default path to the Access Denied page
    o.AccessDeniedPath = "/user/AccessDenied";
});

var properties = new AuthenticationProperties()
{
    // Overrides the default ReturnUrl
    RedirectUri = "/admin/dashboard"
};

await HttpContext.ForbidAsync(properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=10)

#### Understanding ReturnUrl and RedirectUri

发生 forbid 操作时,处理程序会把用户重定向到 `AccessDeniedPath`。
它还会附加一个 `ReturnUrl` 查询参数。
默认情况下,该参数被设为被禁止访问的资源的路径。

```csharp
.AddCookie("cookie", o =>
{
    //Forbid will always redirect to this URL
    o.AccessDeniedPath = "/user/AccessDenied";
});

var prop = new AuthenticationProperties()
{
    RedirectUri = null
};
return Forbid(prop);

// Resulting Redirect:
// HTTP/1.1 302 Found
// Location: https://localhost:5001/user/AccessDenied?ReturnUrl=%2FSecure
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=40)

如果在 `AuthenticationProperties` 中设置了 `RedirectUri` 属性,处理程序会使用该值而不是当前请求路径作为 `ReturnUrl` 参数。

```csharp
.AddCookie("cookie", o =>
{
    //Forbid will always redirect to this URL
    o.AccessDeniedPath = "/user/AccessDenied";
});

var prop = new AuthenticationProperties()
{
    RedirectUri = "/dashboard"
};
return Forbid(prop);

// Resulting Redirect:
// HTTP/1.1 302 Found
// Location: https://localhost:5001/user/AccessDenied?ReturnUrl=%2Fdashboard
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=85)

#### Implementation Demo

为了演示这一点,我们可以修改一个控制器,让它总是返回 `Forbid` 结果。
这样我们就能观察重定向行为以及默认的 `AccessDeniedPath`。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.Secure;

// URL: /Secure/Index
public class SecureController : Controller
{
    public IActionResult Index()
    {
        return Forbid();

        if(User?.Identity?.IsAuthenticated == true)
        {
            return View();
        }
        else
        {
            return Challenge();
        }
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=220)

在 `Program.cs` 中,我们在身份验证服务的设置里为拒绝访问的请求配置自定义路径。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
}).AddCookie("cookie", o =>
{
    o.LoginPath = "/User/Login";
    o.AccessDeniedPath = "/user/AccessDenied";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=260)

#### Logging and User Feedback

在 `UserController` 中,我们可以注入一个 `ILogger` 来跟踪未经授权的访问尝试。
`AccessDenied` action 从查询字符串中捕获 `returnUrl`,以记录被访问的目标资源以及访问者是谁。

```csharp
public class UserController : Controller
{
    private readonly ILogger<UserController> logger;

    public UserController(ILogger<UserController> logger)
    {
        this.logger = logger;
    }

    // ... other actions
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=325)

`AccessDenied` action 记录警告,并通过 `ViewData` 把 `returnUrl` 传给视图,以提供更有帮助的用户界面。

```csharp
public IActionResult AccessDenied(string returnUrl = null)
{
    // Log it
    logger.LogWarning("Access denied for user {User} attempting to access {Resource}",
    User?.Identity?.Name, returnUrl);

    ViewData["returnUrl"] = returnUrl;
    return View();
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=565)

最后,`AccessDenied.cshtml` 视图可以利用这些数据显示与上下文相关的消息,例如一个针对被禁止访问的特定资源申请访问权限的链接。

```razor
@{
    ViewData["Title"] = "AccessDenied";
}

<h1>Access Denied</h1>

You don't have access to this page!

<br />
<br />
<a asp-controller="Home" asp-action="Index">Back to home</a>


@if (ViewData["returnUrl"] != null && User.Identity?.IsAuthenticated == true)
{
    <br />
    <br />
    <p>Think you should have access to <strong>@ViewData["returnUrl"]</strong>?</p>
    <a href="@Url.Action("RequestAccess", "Support", new { resource = ViewData["returnUrl"] })">Request Access</a>
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/forbid-69957955/?t=535)

## 7. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/) · 0:52

### 总结

本课总结了 ASP.NET Core 的 cookie 身份验证处理程序,强调它作为基于 cookie 的安全方案中标准的、可用于生产环境的解决方案的角色。
它回顾了该处理程序的五种基本操作:身份验证、登录、登出、禁止访问处理以及身份验证质询(challenge)。

### 核心概念

*   **Cookie Authentication Handler**:ASP.NET Core 中基于 cookie 的身份验证的默认、可用于生产环境的机制。
*   **Authenticate**:在每个请求上读取、解密并验证身份验证 cookie 的操作。
*   **Sign-in**:创建身份验证票据并向客户端发出加密 cookie 的过程。
*   **Sign-out**:通过使身份验证 cookie 过期来结束会话的过程。
*   **Forbid**:通过把缺少所需权限的已通过身份验证的用户重定向到特定路径来处理拒绝访问。
*   **Challenge**:为尝试访问受保护资源的未通过身份验证的用户触发登录流程。

### 课程笔记

ASP.NET Core 的 cookie 身份验证处理程序是在生产环境中管理基于会话的安全的主要工具。
它在应用的服务容器中进行配置,以定义身份验证应如何运作,包括设置默认方案以及指定登录和拒绝访问的路径。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
})
.AddCookie("cookie", o =>
{ 
    o.AccessDeniedPath = "/User/AccessDenied";
    o.LoginPath = "/User/Login";
    o.Cookie.Name = "MyAuthCookie";
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/?t=11)

该处理程序的生命周期由五种核心操作定义:

#### 1. Authenticate

处理程序会在每个请求上自动执行身份验证逻辑。
它尝试找到身份验证 cookie,解密其内容,并验证由此得到的身份验证票据,以确立用户的身份。

```csharp
protected override Task<AuthenticateResult> HandleAuthenticateAsync()
{
    var authCookie = Request.Cookies[Options.CookieName];

    if (string.IsNullOrEmpty(authCookie))
    {
        return Task.FromResult(AuthenticateResult.NoResult());
    }
    
    var ticketHelper = new AuthTicketHelper(); 
    var ticket = ticketHelper.DeserializeFromBase64(authCookie);

    if(ticket != null)
        return Task.FromResult(AuthenticateResult.Success(ticket));

    return Task.FromResult(AuthenticateResult.NoResult());
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/?t=16)

#### 2. Sign-in

在登录过程中,处理程序会基于用户的 claims 和属性创建一个身份验证票据,然后向客户端浏览器发出一个加密的 cookie。

```csharp
await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/?t=25)

#### 3. Sign-out

为结束会话,处理程序会使身份验证 cookie 过期,确保它在后续请求中不再有效。

```csharp
await HttpContext.SignOutAsync("cookie");
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/?t=31)

#### 4. Forbid

当一个已通过身份验证的用户尝试访问其不具备必要权限的资源时,处理程序会通过把用户重定向到所配置的路径来处理 "Access Denied" 场景。

```csharp
protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
{
    Response.Redirect(Options.AccessDeniedPath);    
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/?t=35)

#### 5. Challenge

如果一个未通过身份验证的用户尝试访问受保护的资源,处理程序会发出 challenge,通常会把用户重定向到登录页。

```csharp
protected override Task HandleChallengeAsync(AuthenticationProperties properties)
{
    Response.Redirect(Options.LoginPath);
    return Task.CompletedTask;
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957956/?t=41)

在下一章中,课程将探讨票据生命周期、过期以及会话管理。

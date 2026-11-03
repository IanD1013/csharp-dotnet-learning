# Cookie and Ticket Lifetime

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 7 章
> 共 7 课 · 约 1:11:39
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Session Cookies](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/session-cookies-69957957/) | 4:44 | [↓](#1-session-cookies) |
| 2 | [Persistent Cookies](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/) | 7:56 | [↓](#2-persistent-cookies) |
| 3 | [Persistent Cookies Using Max Age](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/) | 8:10 | [↓](#3-persistent-cookies-using-max-age) |
| 4 | [Ticket lifetime](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-lifetime-69957960/) | 8:52 | [↓](#4-ticket-lifetime) |
| 5 | [Ticket Renewal](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/) | 21:12 | [↓](#5-ticket-renewal) |
| 6 | [Choosing Cookie and Ticket Lifetime](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/) | 18:47 | [↓](#6-choosing-cookie-and-ticket-lifetime) |
| 7 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957963/) | 1:58 | [↓](#7-section-recap) |

## 1. Session Cookies

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/session-cookies-69957957/) · 4:44

### 总结

会话 cookie(session cookie)是 ASP.NET Core 中默认的身份验证机制,只在一次浏览器会话期间存在。
它的特点是 Set-Cookie 头中没有过期日期,并且在浏览器关闭时会被浏览器自动删除,从而确保用户在重启浏览器后必须重新进行身份验证。

### 核心概念

- **会话 cookie 与持久 cookie**:会话 cookie 驻留在浏览器内存中,在浏览器关闭时过期;而持久 cookie 存储在磁盘上,能在浏览器重启后保留下来。
- **默认行为**:在 ASP.NET Core 中,cookie 默认是基于会话的。
- **AuthenticationProperties**:`IsPersistent` 属性控制 cookie 是基于会话的(`false`)还是持久的(`true`)。
- **安全属性**:身份验证 cookie 默认带有 `Secure`、`SameSite` 和 `HttpOnly` 标志,以防范拦截、CSRF 和 XSS。

### 课程笔记

ASP.NET Core 的 cookie 身份验证处理程序可以签发两种不同类型的 cookie:会话 cookie 和持久 cookie。
会话 cookie 只在浏览器保持打开时存在;一旦浏览器进程终止,cookie 就会被移除。
相比之下,持久 cookie 会被写入客户端的磁盘,使浏览器能在重启之间记住身份验证状态。

要创建会话 cookie,必须把 `AuthenticationProperties` 对象的 `IsPersistent` 属性设为 `false`。
由于 `false` 是该属性的默认值,省略它或显式将其设为 `false` 都会得到一个会话 cookie。

```csharp
.AddCookie("cookie", o =>
{
});

var properties = new AuthenticationProperties()
{
    IsPersistent = false // This is the default
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/session-cookies-69957957/?t=55)

当用户登录时,cookie 处理程序会向 HTTP 响应添加一个 `Set-Cookie` 头。
对于会话 cookie,这个头不包含 `Expires` 或 `Max-Age` 属性。
这种缺失向浏览器表明,该 cookie 应被当作会话级别的产物来处理。

除了会话状态之外,cookie 处理程序还会默认给身份验证 cookie 应用几个安全属性:
- **Secure**:确保 cookie 只通过 HTTPS 连接传输。
- **SameSite (Lax)**:限制 cookie 在某些跨站请求场景中被发送,为防范跨站请求伪造(CSRF)提供一层保护。
- **HttpOnly**:阻止客户端脚本(JavaScript)访问 cookie,降低通过跨站脚本(XSS)窃取令牌的风险。

在一次浏览器会话期间,cookie 会随每个请求发送到服务器。
然而,如果浏览器(例如 Firefox 或 Chrome)被关闭后再重新打开,会话 cookie 就会被丢弃。
回到应用时,用户将不再处于已验证状态,必须重新登录以建立新的会话。

## 2. Persistent Cookies

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/) · 7:56

持久 cookie 存储在用户的磁盘上,在到达过期时间之前,跨浏览器会话都保持有效。
与在浏览器关闭时被丢弃的会话 cookie 不同,持久 cookie 使"保持登录"之类的功能成为可能。

### 核心概念

- **持久性**:保存到客户端磁盘、能在浏览器重启后保留下来的 cookie。
- **IsPersistent**:`AuthenticationProperties` 中的一个属性,决定是否给 cookie 添加 `expires` 属性。
- **ExpireTimeSpan**:身份验证 cookie 的全局默认时长,在 `CookieAuthenticationOptions` 中配置。
- **ExpiresUtc**:按请求进行的覆盖,设置一个具体的绝对过期时间戳。
- **DateTimeOffset**:用于过期时间的首选类型,用来处理时区并确保时间点的准确性。

### 课程笔记

要让 cookie 成为持久的,服务器必须使用 `expires` 属性(一个固定日期)或 `max-age` 属性(以秒为单位的时长)来指示浏览器记住它。
如果两者都没有提供,浏览器就会把它当作会话 cookie。
之后若要删除 cookie,则把过期时间设为过去的某个日期,或者把 `max-age` 设为零。

```http
Set-Cookie: .AspNetCore.cookie=...; expires=Fri, 14 Nov 2025 10:39:27 GMT

Set-Cookie: .AspNetCore.cookie=...; max-age=3600;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=25)

在 ASP.NET Core 中,cookie 生命周期通过若干层配置来管理。
`CookieAuthenticationOptions` 允许进行 `ExpireTimeSpan` 和 `MaxAge` 之类的全局设置。
这些设置可以在登录过程中通过提供 `AuthenticationProperties` 来覆盖。

```csharp
.AddCookie("cookie", o =>
{
    // Sets the cookie's Expires value
    o.ExpireTimeSpan = TimeSpan.FromDays(30);

    // Sets the cookie's Max-Age value
    o.Cookie.MaxAge = TimeSpan.FromMinutes(60);
});

var properties = new AuthenticationProperties()
{
    // ExpiresUtc overrides ExpireTimeSpan
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(60),
    IsPersistent = true,
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=60)

```text
CookieAuthenticationOptions (全局)
  ExpireTimeSpan   --> Expires
  Cookie.MaxAge    --> Max-Age
        |
        | 登录时被覆盖
        v
AuthenticationProperties (按请求)
  ExpiresUtc       --> 覆盖 ExpireTimeSpan
  IsPersistent = true
```

默认情况下,如果 `IsPersistent` 设为 true,ASP.NET Core 会设置 14 天的 cookie 过期时间。

```csharp
var properties = new AuthenticationProperties()
{
    IsPersistent = true
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=100)

过期时间可以针对不同类型的用户进行自定义。
例如,普通用户可能得到 7 天的过期时间,而员工得到 30 天。
这是通过在 `AuthenticationProperties` 对象上设置 `ExpiresUtc` 属性来实现的。

```csharp
.AddCookie("cookie", o =>
{
    // Default expire time for all users
    o.ExpireTimeSpan = TimeSpan.FromDays(7);
});

var properties = new AuthenticationProperties();
properties.IsPersistent = true; // Make the cookie persistent

if(isEmployee)
    properties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=115)

一个常见的用例是"保持登录"功能。
当复选框未勾选时,`IsPersistent` 为 false,创建一个在浏览器关闭时被移除的会话 cookie。
当勾选时,`IsPersistent` 为 true,创建一个带有特定过期时间的持久 cookie。

```csharp
[HttpPost]
public IActionResult Login(string username,
                           string password,
                           bool rememberMe)
{
    var properties = new AuthenticationProperties()
    {
        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
        IsPersistent = rememberMe
    };
    // ...Create principal
    return SignIn(principal, properties);
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=145)

#### 全局配置

在 `Program.cs` 中,`ExpireTimeSpan` 属性定义了该处理程序签发的所有身份验证 cookie 的默认生命周期。

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

    o.ExpireTimeSpan = TimeSpan.FromDays(7);
    o.SlidingExpiration = true;
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=340)

#### 按请求实现

在控制器内,你可以覆盖全局默认值。
使用 `DateTimeOffset.UtcNow` 可以确保过期时间从一个精确的时间点开始计算,避免本地服务器时间或时区差异带来的问题。

```csharp
var items = new Dictionary<string, string>()
{
    { "Item1", "Value1" },
    { "Item2", "Value2" },
    { "Item3", "Value3" },
};

var parameters = new Dictionary<string, object>()
{
    { "Param1", "Value1" },
    { "Param2", "Value2" },
    { "Param3", "Value3" },
};

var properties = new AuthenticationProperties(items, parameters)
{
    IsPersistent = true,
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
};

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-69957958/?t=430)

## 3. Persistent Cookies Using Max Age

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/) · 8:10

本课探讨在 .NET 中使用 `MaxAge` 属性作为 `Expires` 属性的现代替代方案来创建持久 cookie。
内容涵盖绝对过期与相对过期之间的技术差异、如何在 cookie 处理程序中全局配置 `MaxAge`,以及 `MaxAge` 和 `Expires` 同时存在时的优先级规则。

### 核心概念

- **相对时间与绝对时间**:`MaxAge` 使用秒数(相对),而 `Expires` 使用具体的时间戳(绝对)。
- **缓解时钟偏差**:使用相对时间可以避免服务器和客户端时钟不同步时出现的问题。
- **全局配置**:`MaxAge` 通过 `CookieAuthenticationOptions.Cookie.MaxAge` 设置,无法使用 `AuthenticationProperties` 按请求覆盖。
- **优先级**:当两者都提供时,浏览器优先采用 `MaxAge` 属性而不是 `Expires` 属性。
- **持久性的独立性**:只要设置了 `MaxAge`,cookie 就会成为持久的,即使在 `AuthenticationProperties` 中把 `IsPersistent` 设为 `false` 也是如此。

### 课程笔记

#### 比较 Expires 与 MaxAge

传统上,cookie 使用 `Expires` 属性来定义持久性。
`Expires` 依赖绝对时间,这会引入一些风险,例如时钟偏差(服务器和浏览器的时钟不同步),以及解析日期字符串的复杂性。

`MaxAge` 是一种更现代的方式,所有浏览器都支持。
它使用以秒为单位的相对时间,因此更易于解析,并且不受时钟同步问题的影响。
在 .NET 中,`MaxAge` 是在 cookie 处理程序配置中设置的。

设置 `MaxAge` 会使 cookie 成为持久的,即使 `IsPersistent` 属性设为 `false`。
这是因为 `IsPersistent` 专门控制 `Expires` 属性,而 `MaxAge` 是一个独立的设置。

```csharp
.AddCookie("cookie", o =>
{
    o.Cookie.MaxAge = TimeSpan.FromMinutes(60);
});

var properties = new AuthenticationProperties()
{
    // No Cookie Expires value
    IsPersistent = false
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/?t=25)

#### 属性优先级

当一个 cookie 上同时设置了 `MaxAge` 和 `Expires` 时,浏览器遵循一条特定的优先级规则:`MaxAge` 总是覆盖 `Expires`。
例如,如果 `MaxAge` 设为 60 分钟而 `Expires` 设为 60 天,cookie 将在一小时后过期。

```csharp
.AddCookie("cookie", o =>
{
    o.Cookie.MaxAge = TimeSpan.FromMinutes(60);
});

var properties = new AuthenticationProperties()
{
    ExpiresUtc = DateTime.Now.AddDays(60),
    IsPersistent = true
};

await HttpContext.SignInAsync(principal, properties);

// Resulting Header:
// Set-Cookie: .AspNetCore.cookie=...; expires= Sat, 3 Apr 2026 13:28:21 GMT; max-age=3600;
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/?t=130)

```text
Set-Cookie: ...; expires=<60 天后>; max-age=3600
                  |                   |
                  | 被覆盖             | 优先
                  v                   v
             (忽略)              cookie 在一小时后过期
```

#### .NET 中的配置

在 .NET 中,`ExpireTimeSpan` 和 `MaxAge` 是相互独立的设置。
`ExpireTimeSpan`(映射到 `Expires`)可以通过 `AuthenticationProperties.ExpiresUtc` 按用户覆盖,而 `MaxAge` 是在 `Program.cs` 中定义的全局设置,无法通过 `AuthenticationProperties` 按请求更改。

```csharp
.AddCookie("cookie", o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(30);
});

// vs

.AddCookie("cookie", o =>
{
    o.Cookie.MaxAge = TimeSpan.FromMinutes(60);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/?t=160)

#### 实现演示

要在一个标准的 .NET 应用中实现 `MaxAge`,请更新 `Program.cs` 中的身份验证配置。
添加 `o.Cookie.MaxAge` 之后,无论登录时使用的 `IsPersistent` 标志是什么,cookie 都会成为持久的。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
}).AddCookie("cookie", o =>
{
    o.LoginPath = "/User/Login";
    o.AccessDeniedPath = "/user/AccessDenied";
    o.ExpireTimeSpan = TimeSpan.FromDays(7);
    o.Cookie.MaxAge = TimeSpan.FromHours(1);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/?t=295)

在 `UserController` 中,即使把 `IsPersistent` 设为 `true` 并配上一个很长的 `ExpiresUtc`,浏览器中仍以全局的一小时 `MaxAge` 为准。

```csharp
var items = new Dictionary<string, string>()
{
    { "Item1", "Value1" },
    { "Item2", "Value2" },
    { "Item3", "Value3" },
};

var parameters = new Dictionary<string, object>()
{
    { "Param1", "Value1" },
    { "Param2", "Value2" },
    { "Param3", "Value3" },
};

var properties = new AuthenticationProperties(items, parameters)
{
    IsPersistent = true,
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
};

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/persistent-cookies-using-max-age-69957959/?t=460)

## 4. Ticket lifetime

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-lifetime-69957960/) · 8:52

### 总结

身份验证票据(ticket)和身份验证 cookie 是两个独立的概念,各自有不同的生命周期。
票据包含 ClaimsPrincipal 和身份验证属性,是受 Data Protection API 保护的真正身份载荷,而 cookie 只是传输容器。
默认情况下,两者都在 14 天后过期。
开发者可以使用 ExpireTimeSpan 管理全局默认值,或使用 ExpiresUtc 进行按用户覆盖;然而,使用 Cookie.MaxAge 只会影响 cookie 在浏览器中的持久性。
如果 cookie 的生命周期超过票据的生命周期,那么即使用户已不再处于已验证状态,浏览器也会继续发送该 cookie,这可能导致令人困惑的应用行为。

### 核心概念

- **票据与 cookie**:票据是加密的载荷;cookie 是传输机制。
- **票据内容**:包含 `ClaimsPrincipal`(身份)、`AuthenticationProperties` 以及登录方案名称。
- **Data Protection API**:用于保护票据,确保它被加密且无法被用户篡改。
- **默认生命周期**:在 ASP.NET Core 中,票据和 cookie 默认都是 14 天过期。
- **ExpireTimeSpan**:`CookieAuthenticationOptions` 中的一个配置选项,为票据和 cookie 设置默认生命周期。
- **ExpiresUtc**:`AuthenticationProperties` 中的一个属性,用于为某个特定用户会话覆盖默认生命周期。
- **Cookie.MaxAge**:一个控制 cookie 在浏览器中生命周期的属性,但不影响内部票据的有效性。

### 课程笔记

ASP.NET Core 中的身份验证依赖两个不同的组件:身份验证 cookie 和身份验证票据。
cookie 只是一个用于在浏览器和服务器之间传输数据的容器。
真正的身份数据作为身份验证票据存储在 cookie 内部。
该票据包含用户的 `ClaimsPrincipal`、`AuthenticationProperties` 以及身份验证方案的名称。
为了确保安全,票据使用 Data Protection API 进行保护,使其被加密且防篡改。

```text
+------------------------------------------+
| 身份验证 cookie (传输容器)                |
|  +------------------------------------+  |
|  | 身份验证票据 (Data Protection 保护) |  |
|  |   ClaimsPrincipal                  |  |
|  |   AuthenticationProperties         |  |
|  |   身份验证方案名称                  |  |
|  +------------------------------------+  |
+------------------------------------------+
```

#### 控制生命周期

默认情况下,ASP.NET Core 把票据和 cookie 的生命周期都设为 14 天。
开发者可以在身份验证配置期间全局控制这些生命周期,也可以在登录期间按用户控制。
`ExpireTimeSpan` 属性为两者设置默认值,而 `ExpiresUtc` 提供特定的覆盖。

```csharp
.AddCookie("cookie", o =>
{
    // Default Ticket and Cookie lifetime
    o.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var properties = new AuthenticationProperties()
{
    // Make it a persistent cookie
    IsPersistent = true,

    // Overrides Ticket and Cookie lifetime
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(60)
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-lifetime-69957960/?t=100)

#### Max-Age 不匹配

一个常见的困惑点是 `MaxAge` 的使用。
`ExpireTimeSpan` 和 `ExpiresUtc` 会同时影响票据和 cookie,而 `MaxAge` 只影响 cookie。
如果 `MaxAge` 被设为比票据生命周期更长的时长,那么即使内部票据已经过期,浏览器也会继续保存并发送该 cookie。

```csharp
.AddCookie("cookie", o =>
{
    // Sets cookie Max-Age
    // (overrides Cookie Expires)
    o.Cookie.MaxAge = TimeSpan.FromDays(120);
});

var properties = new AuthenticationProperties()
{
    IsPersistent = true,
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(60)
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-lifetime-69957960/?t=130)

在这个场景中,cookie 在浏览器中保持有效 120 天,但其中的票据只在 60 天内有效。
当用户在 60 天之后回来时,浏览器会发送该 cookie,但服务器会拒绝已过期的票据,结果尽管用户持有一个看似有效的 cookie,却被登出了。

```text
天数:  0              60                    120
       |---------------|---------------------|
cookie |=====================================|  Max-Age = 120 天
票据   |===============|                        ExpiresUtc = 60 天
                       ^
                       之后:浏览器仍发送 cookie,服务器拒绝过期票据
```

#### 实现示例

要在登录过程中实现特定的票据生命周期,你需要在控制器中调用 `SignInAsync` 之前配置 `AuthenticationProperties`。

```csharp
var items = new Dictionary<string, string>()
{
    { "Item1", "Value1" },
    { "Item2", "Value2" },
    { "Item3", "Value3" }
};

var parameters = new Dictionary<string, object>()
{
    { "Param1", "Value1" },
    { "Param2", "Value2" },
    { "Param3", "Value3" },
};

var properties = new AuthenticationProperties(items, parameters)
{
    IsPersistent = true,
    ExpiresUtc = DateTime.UtcNow.AddDays(1)
};

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-lifetime-69957960/?t=315)

要观察这种不匹配行为,你可以在全局配置中设置一个很长的 `MaxAge`,同时保持较短的票据生命周期。

```csharp
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "cookie";
}).AddCookie("cookie", o =>
{
    o.LoginPath = "/User/Login";
    o.AccessDeniedPath = "/user/AccessDenied";
    // MaxAge only affects the cookie, not the ticket
    o.Cookie.MaxAge = TimeSpan.FromDays(365);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-lifetime-69957960/?t=385)

检查应用状态时,`AuthenticationProperties` 中的 `issuedUtc` 和 `expiresUtc` 值(可在用户信息或内部 items 中看到)代表票据的实际有效期。
这些值对身份验证最为关键,因为它们决定了用户会话真正结束的时间。

## 5. Ticket Renewal

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/) · 21:12

### 总结

ASP.NET Core 中的票据续期允许根据活动自动延长用户的会话。
通过在 cookie 处理程序中启用滑动过期(sliding expiration)并在身份验证属性中允许刷新,系统会在原始生命周期过去一半后签发新的身份验证票据和 cookie。
这一机制在安全性与用户体验之间取得平衡:一方面保持单个票据的生命周期较短,另一方面确保活跃用户只要在票据过期前保持活动,就不会被迫反复登录。

### 核心概念

- **滑动过期**:cookie 处理程序中的一个全局设置,允许票据在过期前被续期。
- **允许刷新**:`AuthenticationProperties` 中的一个属性,为某个特定用户会话启用续期(默认为 true)。
- **续期窗口**:票据生命周期过去 50% 之后的时间段,在此期间的请求会触发续期。
- **票据克隆**:把 claims principal、属性和方案复制到一个带有更新后时间戳的新票据中的过程。
- **基于活动**:续期只在服务器处理请求时发生;不活跃的会话最终会过期。

### 课程笔记

票据续期让应用可以在不牺牲安全性的前提下保持用户的登录状态。
通过保持较短的票据生命周期(例如一小时),cookie 被盗的风险被降到最低。
然而,为了避免强迫用户每小时登录一次,可以启用自动票据续期。
当票据即将过期时,会签发一个新票据并设置一个新 cookie,使用户可以不受打断地继续工作。

要启用这一行为,必须配置两个设置:cookie 处理程序中的 `SlidingExpiration` 和身份验证属性中的 `AllowRefresh`。
在 ASP.NET Core 中,两者默认都是启用的。

```csharp
.AddCookie("cookie", o =>
{
    // Allows tickets to be renewed before they expire
    // (global setting for all users)
    o.SlidingExpiration = true;
});

var properties = new AuthenticationProperties()
{
    // Allows this user's ticket to be automatically renewed
    AllowRefresh = true
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=70)

#### 续期过程

当一个携带身份验证 cookie 的请求到达时,cookie 处理程序会检查票据是否应当续期。
如果需要续期,处理程序会记住这个决定。
请求处理完毕后,身份验证中间件会调用一个名为 `FinishResponse` 的函数。
如果之前做出了续期决定,票据会被克隆(复制 claims principal、身份验证属性和方案),并更新签发日期和过期日期。
随后一个新的受保护 cookie 会被签发给客户端。

```text
请求 (携带身份验证 cookie)
   |
   v
cookie 处理程序:票据是否应续期?
   |  是 -> 记住这个决定
   v
处理请求
   |
   v
身份验证中间件调用 FinishResponse
   |  已做出续期决定?
   v
克隆票据 (claims principal, 身份验证属性, 方案)
更新签发日期和过期日期
   |
   v
向客户端签发新的受保护 cookie
```

续期具体发生在票据生命周期过去一半之后。
这被称为续期窗口。
如果一个票据的生命周期是 14 天,那么在 7 天之后发出的任何请求都会使它续期。
需要注意的是,过期的票据无法续期;请求必须在续期窗口内、但在实际过期之前到达。

```text
天数: 0               7                14
      |---------------|----------------|
      |  不续期        |  续期窗口       |  已过期:无法续期
```

#### 实现续期监视器

为了观察续期行为,我们可以实现一个显示剩余票据生命周期的监视页面。
首先,我们通过移除 `UserController` 和 `Program.cs` 中自定义的生命周期覆盖,确保恢复默认的身份验证行为。

```csharp
var properties = new AuthenticationProperties(items, parameters)
{
    IsPersistent = true,
    //ExpiresUtc = DateTime.UtcNow.AddDays(1)
};

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=230)

为了让生命周期在 UI 中可见,我们添加一个辅助函数,用来格式化签发时间戳和过期时间戳之间的时长。

```razor
@functions {
    string FormatDuration(DateTimeOffset? from, DateTimeOffset? to)
    {
        if (from == null || to == null)
            return "[Unknown]";

        var duration = to.Value - from.Value;

        if (duration <= TimeSpan.Zero)
            return "expired";

        if (duration.TotalSeconds < 60)
            return $"{(int)duration.TotalSeconds} seconds";

        if (duration.TotalMinutes < 60)
            return $"{(int)duration.TotalMinutes} minutes";

        if (duration.TotalHours < 24)
            return $"{(int)duration.TotalHours} hours";

        return $"{(int)duration.TotalDays} days";
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=395)

#### 使用 JavaScript 进行实时监视

一个专门的 `TicketRenewalController` 可以通过一个 JSON 端点提供票据的剩余秒数,由一段 JavaScript 脚本每秒轮询一次。

```javascript
$(document).ready(function () {

    function pad2(n) { return n.toString().padStart(2, '0'); }

    setInterval(function () {
        $.getJSON('/TicketRenewal/getdata', function (data) {

            var now = new Date();
            var time = pad2(now.getHours()) + ":" + pad2(now.getMinutes()) + ":" + pad2(now.getSeconds());

            var text = data.message ?? "No message";
            var str = '<li>' + time + ' - ' + text + '</li>';

            $(str).appendTo('#Content');
        })
            .fail(function (xhr) {
                var now = new Date();
                var time = pad2(now.getHours()) + ":" + pad2(now.getMinutes()) + ":" + pad2(now.getSeconds());
                $(('<li>' + time + ' - ' + 'Request failed (' + xhr.status + ')' + '</li>')).appendTo('#Content');
            });

    }, 1000);
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=490)

该控制器使用 `HttpContext.AuthenticateAsync()` 获取当前票据并计算剩余时间。

```csharp
[Authorize()]
[HttpGet("/TicketRenewal/getdata")]
public async Task<IActionResult> GetData()
{
    // This runs the cookie handler Authenticate step
    var result = await HttpContext.AuthenticateAsync();

    if (!result.Succeeded || result.Properties?.ExpiresUtc == null)
    {
        return Ok(new
        {            message = "Not authenticated or no ExpiresUtc found",
            remainingSeconds = 0
        });
    }

    var expiresUtc = result.Properties.ExpiresUtc.Value;
    var nowUtc = DateTimeOffset.UtcNow;

    var remaining = expiresUtc - nowUtc;
    var remainingSeconds = (int)Math.Floor(remaining.TotalSeconds);

    if (remainingSeconds < 0)
        remainingSeconds = 0;

    var msg = $"Authentication Ticket expires in {remainingSeconds} seconds";

    return Ok(new
    {
        message = msg,
        remainingSeconds,
        issuedUtc = result.Properties.IssuedUtc,
        expiresUtc = expiresUtc
    });
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=550)

#### 测试续期约束

通过设置一个非常短的过期时间(例如 15 秒),我们可以观察到票据在越过 7.5 秒标记(其生命周期的 50%)后"跳回"到完整的生命周期。

```csharp
var properties = new AuthenticationProperties(items, parameters)
{
    IsPersistent = true,
    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(15),
    AllowRefresh = true,
};

await HttpContext.SignInAsync(myPrincipal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=715)

如果把 `AllowRefresh` 设为 `false`,那么无论是否有活动,票据都会倒计时到零并过期。
同样,如果在 `Program.cs` 配置中把 `SlidingExpiration` 设为 `false`,续期也不会发生。

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
    o.ExpireTimeSpan = TimeSpan.FromDays(7);
    o.SlidingExpiration = true; // Must be true for renewal
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/ticket-renewal-69957961/?t=925)

最后,续期只在服务器处理请求时才会发生。
如果应用被暂停,或者用户在票据的整个时长内都不活跃,票据就会过期,用户必须重新登录。

## 6. Choosing Cookie and Ticket Lifetime

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/) · 18:47

### 总结

选择合适的 cookie 和票据生命周期,对于在安全需求与用户体验之间取得平衡至关重要。
本课探讨 ASP.NET Core 中几种常见的配置场景,范围从默认的 14 天滑动会话,到高安全性的 30 分钟窗口,再到用于共享环境的固定时长会话。
本课还演示了如何使用 Fiddler 检查、重放和修改身份验证请求,为开发者提供调试和测试会话行为的强大工具。

### 核心概念

- **滑动过期**:当身份验证票据的生命周期过去一半以上时,自动对其续期。
- **会话 cookie 与持久 cookie**:会话 cookie 在浏览器关闭时过期,而持久 cookie(`IsPersistent = true`)能在浏览器重启后保留下来。
- **票据生命周期**:通过 cookie 选项中的 `ExpireTimeSpan` 或 `AuthenticationProperties` 中的 `ExpiresUtc` 控制。
- **固定会话**:禁用 `SlidingExpiration` 和 `AllowRefresh` 可以确保会话在特定时间结束,无论是否有活动。
- **Fiddler 调试**:重新发出请求、拦截 POST 数据以及按进程 ID 过滤流量的技巧。

### 课程笔记

身份验证会话的配置在很大程度上取决于应用的安全概况。
银行应用比一个简单的博客需要严格得多的会话控制。

#### 场景 1:默认配置

在一个标准的 ASP.NET Core Web 应用中,默认的 `ExpireTimeSpan` 是 14 天,并启用了 `SlidingExpiration`。
默认情况下,`IsPersistent` 为 false,这意味着浏览器使用一个在浏览器关闭时被清除的会话 cookie。

```csharp
.AddCookie("cookie", o =>
{
    // ASP.NET Core Defaults
    o.ExpireTimeSpan = TimeSpan.FromDays(14);
    o.SlidingExpiration = true;
});

var properties = new AuthenticationProperties()
{
    // ASP.NET Core Defaults
    IsPersistent = false,
    AllowRefresh = true
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=40)

在这种配置下,如果用户保持活跃,票据会在 7 天后续期。
用户最多可以不活跃 14 天,之后会话才会过期。

#### 场景 2:出于安全考虑的短会话

为了降低 cookie 被盗的风险,你可以实现更短的票据生命周期。
例如,把生命周期设为 30 分钟,可以确保攻击者只有非常有限的时间窗口来使用被劫持的会话。

```csharp
.AddCookie("cookie", o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    o.SlidingExpiration = true;
});

var properties = new AuthenticationProperties()
{
    // Defaults
    IsPersistent = false,
    AllowRefresh = true
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=100)

在这种设置下,票据会在活动 15 分钟后续期。
如果用户 30 分钟不活跃(例如午休期间),就会被自动登出。

#### 场景 3:记住我

应用通常会提供一个"记住我"复选框,让用户在便利性和安全性之间做出选择。
勾选时,应用使用一个生命周期较长(例如 14 天)的持久 cookie。
未勾选时,则退回到一个生命周期较短的会话 cookie。

```csharp
var properties = new AuthenticationProperties()
{
    IsPersistent = true,
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14)
};

// Alternative for when Remember Me is not checked
var properties = new AuthenticationProperties()
{
    IsPersistent = false,
    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=175)

#### 场景 4:固定时段(考试和共享电脑)

对于在线考试或共享自助终端之类的场景,你可能希望对会话时长施加一个硬性限制。
通过禁用 `SlidingExpiration` 和 `AllowRefresh`,票据永远不会被续期,会话会在指定时长后准时结束,即使用户处于活跃状态。

```csharp
.AddCookie("cookie", o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    // No ticket renewal
    o.SlidingExpiration = false;
});

var properties = new AuthenticationProperties()
{
    // No ticket renewal
    AllowRefresh = false,

    // Survives browser restarts
    IsPersistent = true
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=265)

#### 使用 Fiddler 调试

Fiddler 是一个在开发期间检查和操纵 HTTP 流量的强大工具。

##### 为测试恢复默认值

要测试标准行为,请确保应用没有在身份验证配置中用极短的值覆盖生命周期。

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
    o.SlidingExpiration = true;
});
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=340)

在 `UserController` 中,确保移除硬编码的过期覆盖,以观察默认的滑动行为:

```csharp
var properties = new AuthenticationProperties(items, parameters)
{
    IsPersistent = true,
    //ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(15),
    AllowRefresh = true,
};
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=370)

##### 重新发出和修改请求

Fiddler 允许你选中某个特定请求并按 **R** 键来重新发出它。
这对于调试特定的控制器 action 很有用,而无需重新加载整个页面。

你还可以使用过滤器拦截请求。
启用 **Break request on POST** 后,Fiddler 就像一个针对 HTTP 流量的调试器。
当用户提交登录表单时,Fiddler 会暂停该请求,让你可以在它到达服务器之前修改表单数据(例如更改用户名)。

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginModel loginCredentials, string returnUrl = null)
{
    var userName = loginCredentials.UserName;

    //1. Load the claims for this user 
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
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/choosing-cookie-and-ticket-lifetime-69957962/?t=820)

##### Composer 与进程过滤

**Composer** 选项卡允许你拖入一个已有请求,并手动编辑它的请求头、HTTP 方法或请求体。
注意,当使用 Composer 测试登录时,得到的身份验证 cookie 是由 Fiddler 而不是浏览器接收的,因此除非手动转移该 cookie,否则浏览器会话不会被更新。

为了减少 Fiddler 中的干扰,请使用 **Process Filter**(十字准星图标)。
把这个图标拖到某个特定窗口(例如 Visual Studio 或某个特定的浏览器实例)上,就会限制 Fiddler 只捕获来自该进程的流量。

## 7. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957963/) · 1:58

### 总结

本课全面回顾了 ASP.NET Core 中 cookie 和票据生命周期的管理。
它详细说明了如何使用 MaxAge 和 IsPersistent 配置持久 cookie,如何全局或按会话控制票据过期,以及通过滑动过期和 allow refresh 属性启用自动票据续期的具体要求。

### 核心概念

- **cookie 持久性**:通过 `MaxAge`、`Expires` 和 `IsPersistent` 标志控制。
- **优先级**:`MaxAge` 会覆盖 cookie 头中的 `Expires` 值。
- **票据生命周期**:通过 cookie 处理程序中的 `ExpireTimeSpan` 进行全局管理,或使用 `AuthenticationProperties` 中的 `ExpiresUtc` 按会话覆盖。
- **票据续期**:要求同时启用 `SlidingExpiration`(全局)和 `AllowRefresh`(按用户)。

### 课程笔记

#### Cookie 生命周期与持久性

要让身份验证 cookie 跨浏览器会话持久存在,必须在登录期间把 `AuthenticationProperties` 中的 `IsPersistent` 属性设为 `true`。
cookie 的时长由 `MaxAge` 或 `Expires` 控制。
在 cookie 处理程序配置中,如果两者都提供,`MaxAge` 优先于 `Expires`。

```csharp
.AddCookie("cookie", o => {
    o.Cookie.MaxAge = TimeSpan.FromMinutes(60); });

.AddCookie("cookie", o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(30); });

var prop = new AuthenticationProperties(items)
{
    // Overrides ExpireTimeSpan
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14),

    // Enables persistent cookie using Expires
    IsPersistent = true,
};
await HttpContext.SignInAsync(principal, prop);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957963/?t=55)

#### 票据生命周期管理

身份验证票据的生命周期通常应与 cookie 的生命周期相匹配。
默认时长使用 `CookieAuthenticationOptions` 中的 `ExpireTimeSpan` 设置。
对于特定的登录实例,可以通过在传给 `SignInAsync` 的 `AuthenticationProperties` 中设置 `ExpiresUtc` 属性来覆盖它。

```csharp
.AddCookie("cookie", o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var prop = new AuthenticationProperties(items)
{
    // Controls the ticket lifetime
    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(90),
};

await HttpContext.SignInAsync(principal, prop);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957963/?t=70)

#### 票据续期与滑动过期

身份验证票据的自动续期使用户的会话只要保持活跃就能被延长。
要发生续期,必须配置两个设置:
1.  **SlidingExpiration**:cookie 处理程序中的全局设置(默认为 `true`)。
2.  **AllowRefresh**:`AuthenticationProperties` 中的按用户设置(默认为 `true`)。

只有两者都启用,中间件才会在当前票据的生命周期过半时签发一个带有更新后过期时间的新 cookie。

```csharp
.AddCookie("cookie", o =>
{
    // Allows tickets to be renewed before they expire
    // (global setting for all users, default true)
    o.SlidingExpiration = true;
});

var properties = new AuthenticationProperties()
{
    // Allows this user's ticket to be automatically renewed
    // (default true)
    AllowRefresh = true
};

await HttpContext.SignInAsync(principal, properties);
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957963/?t=100)

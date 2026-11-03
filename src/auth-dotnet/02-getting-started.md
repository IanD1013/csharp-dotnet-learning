# Getting Started

> 课程:[Getting Started: Authentication and Authorization in .NET](https://dometrain.com/course/getting-started-authentication-and-authorization-in-dotnet/) · 第 2 章
> 共 5 课 · 约 28:55
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Course Material](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/course-material-69957926/) | 2:25 | [↓](#1-course-material) |
| 2 | [Installing Fiddler](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/installing-fiddler-69957927/) | 8:23 | [↓](#2-installing-fiddler) |
| 3 | [Fiddler and HTTPS](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/fiddler-and-https-69957928/) | 10:21 | [↓](#3-fiddler-and-https) |
| 4 | [Exploring the starter project](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/exploring-the-starter-project-69957929/) | 6:49 | [↓](#4-exploring-the-starter-project) |
| 5 | [Section Recap](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957930/) | 0:57 | [↓](#5-section-recap) |

## 1. Course Material

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/course-material-69957926/) · 2:25

本课概述了课程资料,包括三个主要的 .NET 项目和一份全面的资源指南。
这些项目的组织方式旨在引导学习者依次学习身份验证的基础知识(第 2-8 章)、授权的复杂性(第 9-10 章),以及 ASP.NET Core MVC 过滤器管道的内部工作原理。
每个项目都包含 "Start" 和 "Done" 两种状态,便于按模块逐步推进各项技术实现。

### 核心概念

- **Authentication Project**:涵盖第 2 章到第 8 章,聚焦于身份验证基础。
- **Authorization Project**:涵盖第 9 章和第 10 章,聚焦于访问控制。
- **MVC Filters Project**:用于探索 ASP.NET Core MVC 过滤器管道和授权过滤器。
- **代码组织**:项目分为 "Start" 和 "Done" 两种状态,以便于循序渐进地学习。
- **资源指南**:一个 markdown 文件,包含精选的外部工具、文档和安全文章链接。

### 课程笔记

课程资料由三个独立的 Visual Studio 解决方案和一个补充资源文件组成,每一个都针对 .NET 安全生态中的特定学习目标。

#### Authentication Project

Authentication 项目是课程第一部分的重点,横跨第 2 章到第 8 章。
它提供了一个环境,用于探索 .NET 中身份标识和身份验证的基础机制。
源代码被组织为用于初始实现的 `Chapter-02-Start` 文件夹,以及代表身份验证模块最终状态的 `Chapter-08-Done` 文件夹。

#### Authorization Project

Authorization 项目专门用于第 9 章和第 10 章,将重点转向对资源的访问控制。
它沿用与身份验证资料类似的结构,提供 `Chapter-09-Start` 文件夹和 `Chapter-10-Done` 文件夹,用于跟踪授权相关课程的进度。

#### MVC Filters Project

MVC Filters 项目用于研究 ASP.NET Core MVC 过滤器管道。
这个项目对于理解授权过滤器如何融入更广泛的请求处理流程至关重要。
它同时包含 `1 - Start Code` 和 `2 - Complete Code` 两个版本。

#### 资源文档

一个专门的 markdown 文件 `Resources.md` 作为中心仓库,收录了整个课程中引用的外部链接、工具和文档。
其中包括 Fiddler 和 Charles 等网络调试工具、OAuth 2.0 提供程序,以及 OWASP 的安全备忘单(cheat sheet)的引用。

```markdown
# Getting Started with Authentication and Authorization in .NET

## External Links: Module #1 - Welcome

* Tore's blog: https://nestenius.se
* Tore's LinkedIn: https://www.linkedin.com/in/torenestenius

 
## External Links: Section #2 - Getting Started

* Fiddler Classic
  https://getfiddler.com 

* Fiddler Everywhere
  https://www.telerik.com/fiddler/fiddler-everywhere

* Fiddler Book 
  https://fiddlerbook.com/book/

* Charles - Open Source Fiddler alternative.
  https://www.charlesproxy.com/


## External Links: Section #4 - The Authentication Middleware

* AspNet.Security.OAuth.Providers
  List of OAuth 2.0 social authentication providers for ASP.NET Core.
  https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers

* External OAuth authentication providers
  https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/other-logins

* Base64 decode and encode
  https://www.base64decode.org/

* ASP.NET Core Cookie Handler Source Code
  https://github.com/dotnet/aspnetcore/tree/main/src/Security/Authentication/Cookies/src

* Improving ASP.NET Core Security By Putting Your Cookies On A Diet
  https://nestenius.se/net/improving-asp-net-core-security-by-putting-your-cookies-on-a-diet/


## External Links: Section #6 - The Cookie Handler

* .NET Source Browser
  https://source.dot.net/
  
* Unvalidated Redirects and Forwards Cheat Sheet
  https://cheatsheetseries.owasp.org/cheatsheets/Unvalidated_Redirects_and_Forwards_Cheat_Sheet.html

* Cross-Site Request Forgery Prevention Cheat Sheet
  https://cheatsheetseries.owasp.org/cheatsheets/Cross-Site_Request_Forgery_Prevention_Cheat_Sheet.html


## External Links: Section #9 - Authorization

* Preventing mass assignment or over posting in ASP.NET Core  
  https://andrewlock.net/preventing-mass-assignment-or-over-posting-in-asp-net-core/
* Mass Assignment Cheat Sheet
  https://cheatsheetseries.owasp.org/cheatsheets/Mass_Assignment_Cheat_Sheet.html




## External Links: Section #10 - Advanced Authorization

* View-based authorization in ASP.NET Core MVC
  https://learn.microsoft.com/en-us/aspnet/core/security/authorization/views
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/course-material-69957926/?t=124)

## 2. Installing Fiddler

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/installing-fiddler-69957927/) · 8:23

### 总结

Fiddler 是一款强大的 HTTP 代理工具,用于拦截和检查应用程序与互联网之间的流量。
与浏览器开发者工具不同,Fiddler 捕获的是系统范围的流量,包括后台 API 调用和桌面应用程序的请求,因此它对于调试 cookie、令牌和安全标头等身份验证机制必不可少。
本课介绍 Fiddler Classic 与 Fiddler Everywhere 之间的区别、Fiddler Classic 的安装,以及如何使用 Raw 视图分析 HTTP 请求和响应。

### 核心概念

- **HTTP 代理**:Fiddler 位于你的应用程序和互联网之间,拦截所有流量。
- **系统范围捕获**:与浏览器开发者工具不同,Fiddler 会捕获来自桌面应用和后台服务的流量。
- **Fiddler Classic**:免费的、仅支持 Windows 的版本,适用于常规的 HTTP/HTTPS 调试。
- **Fiddler Everywhere**:跨平台的付费版本,支持 HTTP/2、WebSockets 和 gRPC 等现代协议。
- **Raw 视图**:Fiddler 中用于检查请求和响应的确切标头和正文的功能。

### 课程笔记

#### 为什么使用 Fiddler?

Fiddler 充当一个拦截 HTTP 流量的代理,让你可以检查每一个请求和响应。
这在理解身份验证的"底层"原理时特别有用,尤其是在处理 cookie、令牌和安全标头时。
虽然浏览器开发者工具很有用,但它们只显示该特定浏览器内产生的流量。
Fiddler 提供完整的、系统范围的全貌,能捕获浏览器工具会遗漏的、来自桌面应用程序和后台 API 调用的流量。

```text
+-------------+        +---------+        +----------+
| Application | -----> | Fiddler | -----> | Internet |
|  / Browser  | <----- | (proxy) | <----- |          |
+-------------+        +---------+        +----------+
```

#### Fiddler 的版本

Fiddler 有两个主要版本:
1.  **Fiddler Classic**:一款适用于 Windows 的免费工具。它支持 HTTP 和 HTTPS,足以应对大多数身份验证调试任务。
2.  **Fiddler Everywhere**:一个现代的、面向 Windows、macOS 和 Linux 的跨平台重写版本。它需要付费订阅,并支持 HTTP/2、WebSockets 和 gRPC 等较新的协议。

#### 安装与设置

要安装 Fiddler Classic,请从官方网站下载安装程序。
在首次启动时,可能会弹出各种配置对话框;对于基本使用,可以跳过这些对话框。

应用程序打开后,确保工具栏中的 **Decode** 按钮处于选中状态。
这能确保压缩的流量被自动解压,使内容可供人阅读。

#### 检查流量

当你访问某个网站(例如 `example.com`)时,Fiddler 会在左侧填充会话列表。
选中一个会话后,你可以使用右侧的 **Inspectors** 选项卡来分析数据。

**Raw** 视图是查看完整 HTTP 事务的最有效方式。
上方窗格显示请求,包括客户端发送的所有标头;下方窗格显示服务器的响应,包括状态码(例如 200 OK)、响应标头和正文(例如 HTML)。

```html
<!doctype html><html><head><title>Example Domain</title><meta name="viewport" content="width=device-width,
initial-scale=1"><style>body{background:#eee;width:60vw;margin:15vh auto;font-family:system-ui,sans-serif}h1{font-
size:1.5em}div{opacity:0.8}a:link,a:visited{color:#348}</style></head><body><div><h1>Example Domain</h1><p>This
domain is for use in documentation examples without needing permission. Avoid use in operations.</p><p><a href="https:
//iana.org/domains/example">Learn more</a></p></div></body></html>
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/installing-fiddler-69957927/?t=370)

Fiddler 也能识别失败的请求。
例如,如果浏览器尝试获取一个不存在的 `favicon.ico`,Fiddler 会显示 404 Not Found 状态以及相关的请求标头。

```http
GET http://example.com/favicon.ico HTTP/1.1
Host: example.com
Connection: keep-alive
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/146.0.0.0 Safari/537.
Accept: image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8
Referer: http://example.com/
Accept-Encoding: gzip, deflate
Accept-Language: en-US,en;q=0.9
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/installing-fiddler-69957927/?t=490)

## 3. Fiddler and HTTPS

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/fiddler-and-https-69957928/) · 10:21

### 总结

Fiddler 充当中间人代理,借助一个本地根证书来解密和检查 HTTPS 流量。
这项能力对于调试现代 Web 应用程序至关重要,因为浏览器会强制执行严格的安全策略,并且在安全连接与不安全连接上对 cookie 的处理方式不同。
通过分别与客户端和服务器建立独立的安全连接,Fiddler 让加密的请求和响应变得可见,包括后台 AJAX 调用和 JSON 负载。

### 核心概念

- **中间人(MITM)代理**:Fiddler 拦截客户端与服务器之间的流量,以便进行检查。
- **HTTPS 解密**:打破加密隧道以查看原始请求和响应数据的过程。
- **根证书**:一个在本地安装的证书,让浏览器能够信任 Fiddler 动态生成的证书。
- **与生产环境一致(Production Parity)**:在开发中使用 HTTPS 的做法,以确保浏览器行为(例如 cookie 处理)与生产环境一致。
- **JSON 检查**:使用内置查看器分析后台 API 调用返回的结构化数据。

### 课程笔记

Fiddler 解密 HTTPS 流量的能力是它最强大的功能之一。
虽然 HTTPS 被设计为安全且加密的,但 Fiddler 可以通过充当中间人代理来检查这些流量。
当浏览器发出 HTTPS 请求时,Fiddler 会拦截它,解密其内容以供检查,然后使用自己的安全连接把请求转发给目标服务器。

#### HTTPS 解密的工作原理

默认情况下,浏览器不信任 Fiddler 出示的证书。
为了能够无安全警告地顺畅检查,你必须在本地计算机上安装一个 Fiddler 根证书。
安装之后,浏览器会信任 Fiddler 为特定域签发的任何证书。

Fiddler 管理两条独立的安全连接:
1.  浏览器与 Fiddler 之间的一条安全连接。
2.  Fiddler 与实际 Web 服务器之间的另一条独立安全连接。

```text
+---------+  secure connection 1  +---------+  secure connection 2  +------------+
| Browser | <-------------------> | Fiddler | <-------------------> | Web server |
+---------+                       +---------+                       +------------+
                              (Fiddler Root
                               certificate)
```

由于 Fiddler 控制着这两条连接的两端,它可以看到经过的每一个字节的数据。
这是一种标准的开发实践,类似于 .NET 开发工具为在 localhost 上运行应用程序而安装本地证书的做法。
不过,出于安全考虑,在进行网上银行或购物等敏感操作时应关闭 Fiddler,因为它可以看到密码、令牌和信用卡信息。

#### 在 Fiddler 中启用 HTTPS

HTTPS 解密默认未启用。
要开启它:
1.  导航到 **Tools > Options**。
2.  选择 **HTTPS** 选项卡。
3.  勾选 **Decrypt HTTPS traffic**。
4.  接受提示,信任 Fiddler Root 证书并将其添加到本机的根证书列表中。
5.  重启 Fiddler 以应用更改。

#### 验证和检查流量

启用之后,Fiddler 将不再为 HTTPS 流量显示通用的 "Tunnel to" 条目,而是显示实际的请求和响应数据。
例如,访问 `dometrain.com` 这样的网站,就能看到安全通信的原始标头和正文。

用于管理流量列表的实用快捷键包括:
- **Ctrl + X**:清除会话视图中当前的请求列表。
- **Ctrl + F5**:在浏览器中强制刷新,以捕获新的流量。

#### 检查后台流量

Fiddler 在捕获由 JavaScript 应用程序产生的后台流量(例如 AJAX 调用)时特别有用。
例如,在搜索引擎中输入内容时,浏览器会在每次按键时发送获取搜索建议的请求。
Fiddler 可以捕获这些请求,并使用内置的 **JSON viewer** 显示结果,它会把原始响应字符串格式化为可读的对象。

```json
)]}'
[[["why do i like to c<b>hew on random things</b>",0,[512]],["<b
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/fiddler-and-https-69957928/?t=610)

这种程度的可见性让开发者能够诊断数据结构、标头和响应码方面的问题,否则这些问题会隐藏在加密的 HTTPS 隧道之中。

## 4. Exploring the starter project

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/exploring-the-starter-project-69957929/) · 6:49

### 总结

本课介绍整个身份验证与授权课程中使用的 ASP.NET Core 起始项目。
该项目没有采用标准的 MVC 结构,而是实现了功能文件夹(feature folders),这是一种垂直切片架构,其中控制器、模型和视图按功能而不是按类型分组。
起始代码包含公共页面的占位符、一个用于存放受保护内容的安全区域,以及一个处理登录、注销和账户详情的用户管理系统。

### 核心概念

*   **Feature Folders**:一种垂直切片架构,与某个特定功能相关的所有文件(Controllers、Models、Views)都存放在同一个文件夹中。
*   **RazorViewEngineOptions**:用于告知 Razor 视图引擎功能文件夹所需的自定义视图位置格式的配置。
*   **Secure Controller**:用于需要身份验证的页面的占位控制器。
*   **User Controller**:专门用于用户管理的控制器,包括登录、注销和账户信息。
*   **Fiddler 过滤**:在 Fiddler 中使用基于主机的过滤器隔离 `localhost` 流量,以便于调试。

### 课程笔记

起始项目是一个标准的 ASP.NET Core 应用程序,用于演示身份验证和授权功能。
与把控制器、模型和视图分别放在不同顶级目录中的传统 MVC 项目结构不同,这个项目采用了功能文件夹结构。
这种垂直切片方式确保一起变化的代码放在一起,从而简化导航和维护。

为了支持这种结构,`Program.cs` 文件中包含了一段 `RazorViewEngineOptions` 配置,用来映射自定义的视图位置。

```csharp
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/exploring-the-starter-project-69957929/?t=145)

#### 项目控制器

应用程序被组织为三个主要功能:

1.  **Home**:包含 `HomeController`,负责管理 Index、Privacy 和 Error 等面向公众的页面。

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace StartcodeAuthentication.Features.Home;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/exploring-the-starter-project-69957929/?t=235)

2.  **Secure**:包含 `SecureController`,它是身份验证的目标。
    目前它只包含一个 `Index` 操作,该操作最终会受到保护,只有经过身份验证的用户才能访问。

3.  **User**:包含 `UserController`,负责身份验证的生命周期,包括登录、注销和显示用户信息。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.User;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        return View(new LoginModel() { ReturnUrl = ReturnUrl });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginCredentials)
    {
        return Redirect(loginCredentials.ReturnUrl ?? "/");
    }


    [HttpPost]
    public async Task Logout()
    {
    }


    public IActionResult LoggedOut()
    {
        return View();
    }


    public IActionResult AccessDenied()
    {
        return View();
    }


    public IActionResult Info()
    {
        // 1. Get the user (ClaimsPrincipal) from the HttpContext

        // 2. Get the Primary Identity of the user (there might be more than one)

        // 3. Get IsAuthenticated

        // 4. Get AuthenticationType

        // 5. Get the claims from the user

        // 6. Get the Name claim

        // 7. Check if user has developer or admin role


        var model = new UserInfoModel()
        {
        };

        return View(model);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/exploring-the-starter-project-69957929/?t=250)

```text
Features/
  Home/    HomeController    (Index, Privacy, Error)
  Secure/  SecureController  (Index)
  User/    UserController    (Login, Logout, LoggedOut, AccessDenied, Info)
```

#### 使用 Fiddler 监控流量

为了有效地调试身份验证流程,可以使用 Fiddler 检查 HTTP 流量。
为了减少后台进程带来的干扰,请按以下步骤设置过滤:
1.  导航到 Fiddler 中的 **Filters** 选项卡。
2.  勾选 **Use Filters**。
3.  在 **Hosts** 区域中,从下拉列表里选择 **Show only the following Hosts**。
4.  在文本框中输入 `localhost;`。这能确保只显示发往本地开发服务器的流量。

## 5. Section Recap

> [观看本课](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957930/) · 0:57

### 总结

本课总结了初始设置阶段,涵盖课程资料的组织方式、用于检查 HTTPS 流量的 Fiddler Classic 的安装,以及身份验证起始项目的概览。
该项目使用基于功能的文件夹结构,并包含登录、注销和用户信息的控制器桩代码,为探索 .NET 身份验证和授权打下基础。

### 核心概念

* **课程资源**:获取课程所需的示例和文档。
* **流量检查**:设置 Fiddler Classic 并启用 HTTPS 支持,以监控开发中的加密流量。
* **起始项目架构**:使用 "Features" 文件夹结构代替默认的 MVC 布局,以获得更好的组织性。
* **身份验证基础**:初始化 Home、Secure 和 User 操作的控制器,为实现身份验证做准备。

### 课程笔记

课程的初始阶段侧重于搭建一个健全的开发环境,并了解所提供的资源。
这包括获取支持学习过程的全套示例和文档。

设置中的一个关键组成部分是 Fiddler Classic 的安装和配置。
这个工具用于检查客户端和服务器之间的网络流量。
为了有效地调试身份验证流程,必须在该工具中启用并配置 HTTPS 支持,以便在开发期间检查加密流量。

接下来技术实现的基础是身份验证起始项目。
这个应用程序采用基于功能的组织结构,而不是标准的 MVC 文件夹布局。
这是通过在 `Program.cs` 文件中配置 `RazorViewEngineOptions` 来识别自定义视图位置实现的。

```csharp
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

//Support features folder structure
builder.Services.Configure<RazorViewEngineOptions>(rvo =>
{
    rvo.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
    rvo.ViewLocationFormats.Add("~/Views/Shared/{0}.cshtml");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957930/?t=29)

该项目包含若干控制器,它们定义了应用程序的功能面。
`UserController` 包含 Login、Logout 和 User Info 操作所需的桩代码,随着课程深入 .NET 身份验证的核心概念,这些桩代码会被逐步填充。

```csharp
using Microsoft.AspNetCore.Mvc;

namespace StartcodeAuthentication.Features.User;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        return View(new LoginModel() { ReturnUrl = ReturnUrl });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginCredentials)
    {
        return Redirect(loginCredentials.ReturnUrl ?? "/");
    }


    [HttpPost]
    public async Task Logout()
    {
    }


    public IActionResult LoggedOut()
    {
        return View();
    }


    public IActionResult AccessDenied()
    {
        return View();
    }


    public IActionResult Info()
    {
        // 1. Get the user (ClaimsPrincipal) from the HttpContext

        // 2. Get the Primary Identity of the user (there might be more than one)

        // 3. Get IsAuthenticated

        // 4. Get AuthenticationType

        // 5. Get the claims from the user

        // 6. Get the Name claim

        // 7. Check if user has developer or admin role


        var model = new UserInfoModel()
        {
        };

        return View(model);
    }
}
```

[▶ 观看](https://dometrain.com/take/course/getting-started-authentication-and-authorization-in-dotnet-3256108/section-recap-69957930/?t=33)

有了这些工具和资源,环境就已准备就绪,可以去探索 .NET 中身份验证在底层是如何工作的了。

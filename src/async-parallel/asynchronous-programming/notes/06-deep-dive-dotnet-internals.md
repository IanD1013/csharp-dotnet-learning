# Deep Dive: .NET Internals

> 课程:[From Zero to Hero: Asynchronous Programming in C#](https://dometrain.com/course/from-zero-to-hero-asynchronous-programming-in-csharp/) · 第 6 章
> 共 5 课 · 约 40:16
> 来源:Dometrain。由课程文档翻译整理;每一节都链接到对应课程。

---

| # | 课程 | 时长 | 小节 |
| --- | --- | --- | --- |
| 1 | [Introduction](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197165/) | 1:24 | [↓](#1-introduction) |
| 2 | [ThreadStatic](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/threadstatic-62197166/) | 5:00 | [↓](#2-threadstatic) |
| 3 | [Principal](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/principal-62197167/) | 7:40 | [↓](#3-principal) |
| 4 | [ExecutionContext](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/) | 13:33 | [↓](#4-executioncontext) |
| 5 | [SynchronizationContext](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/synchronizationcontext-62197169/) | 12:39 | [↓](#5-synchronizationcontext) |

---

## 1. Introduction

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/introduction-62197165/) · 1:24

### 总结

本课介绍异步编程系列的最后一部分,重点是支撑 async 和 await 的 .NET 内部机制。
虽然开发者在日常工作中很少直接接触这些底层组件,但理解其底层架构(例如线程、任务和同步上下文)对于排查复杂 bug、掌握异步操作在底层是如何执行的至关重要。

### 核心概念

* 用于异步执行的 .NET 内部组件。
* 线程与任务之间的关系。
* 同步上下文的作用。
* 底层知识对高级调试的重要性。

### 课程笔记

本部分从任务和线程的高层用法,过渡到对 .NET 内部机制的深入探讨。
在此前已经探讨过多线程、并行执行和串行异步操作之后,重点现在转向支撑 `async-await` 模式的底层组件。

这一探讨涵盖线程和任务的内部机制,特别关注 .NET 如何管理执行流程。
关键的架构要素包括:

* **Threads**:执行的基本单元。
* **Tasks**:用于异步操作的更高层抽象。
* **Synchronization Contexts**:协调代码在 await 之后如何恢复、在何处恢复的机制。

虽然在常规应用开发中,开发者手动创建线程或自定义同步上下文并不常见,但理解这些内部机制至关重要。
这些知识为诊断和解决那些仅靠高层抽象无法解决的边缘情况 bug 提供了必要的基础。

---

## 2. ThreadStatic

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/threadstatic-62197166/) · 5:00

### 总结

ThreadStatic 特性让开发者可以定义为每个线程保持独立值的静态字段。
静态字段通常在整个应用中共享同一个值,而 ThreadStatic 确保每个线程都拥有该字段自己独立的实例。
这对线程本地存储很有用,但在使用线程池的环境(例如 ASP.NET Core)中,或者在任务可能在线程之间迁移、从而失去对原线程本地数据访问的异步代码中,需要谨慎使用。

### 核心概念

*   **ThreadStatic Attribute**:静态字段上的一个标记,指示 .NET 运行时为每个线程提供该字段的独立实例。
*   **Isolation(隔离)**:确保在一个线程上对静态变量的修改,不会影响另一个线程上同一变量的值。
*   **Thread Reuse Risks(线程复用风险)**:在 ASP.NET Core 这类池化环境中,线程会被复用于不同的请求。残留在 ThreadStatic 变量中的数据可能会保留下来,并泄漏到后续操作中。
*   **Async/Await Compatibility(Async/Await 兼容性)**:ThreadStatic 在跨越 await 边界时并不可靠,因为无法保证任务会在它开始时所在的同一线程上恢复。
*   **ASP.NET Core Alternatives(ASP.NET Core 替代方案)**:在 Web 应用中进行按请求的数据隔离时,优先使用 `HttpContext` 而不是 ThreadStatic。

### 课程笔记

`ThreadStatic` 特性应用于静态字段,用来表示该字段的值对每个线程都是独立的。
在标准场景下,一个静态字段只有一个实例,在整个应用中共享。
通过应用 `[ThreadStatic]`,.NET 会为每个访问该字段的线程分配一个独立的字段实例。

```csharp
[ThreadStatic]
private static int threadLocalValue;
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/threadstatic-62197166/?t=10)

#### 安全与线程复用

使用 `ThreadStatic` 时一个关键的考虑因素是线程复用。
在 ASP.NET Core 这类环境中,线程池管理着一组线程,这些线程会被复用于不同的 API 请求。
如果在一次请求中给某个 `ThreadStatic` 变量设置了值,而该线程之后又被复用于另一个请求,那么第二个请求可能会意外访问到第一个用户的数据。
对于 Web 应用,建议使用 `HttpContext` 而不是 `ThreadStatic` 来存储请求特定的数据,以避免这些安全隐患。

#### 异步编程的局限

在异步 C# 中,并不保证任务在其整个生命周期内都停留在同一个线程上。
当一个任务被 await 时,其后续代码(continuation)可能会在另一个线程上恢复,尤其是在使用了 `ConfigureAwait(false)` 或 `ConfigureAwaitOptions.None` 的情况下。
如果在 `await` 之前把一个值存进了 `ThreadStatic` 变量,而代码在另一个线程上恢复,那么这个值将不可用,从而导致状态不一致以及难以追踪的 bug。

#### 实现示例

为了演示线程隔离,下面的示例手动创建了两个线程。
虽然一般不建议手动创建线程,而应优先使用任务并行库(TPL)和 async/await,但这里使用它是为了明确展示不同线程如何为同一个静态字段保持各自独立的值。

```csharp
static class Program
{
	// Apply ThreadStatic attribute to make this variable thread-local.
	[ThreadStatic]
	static int threadSpecificValue;

	static void Main(string[] args)
	{
		// Initializing thread-specific value for the main thread
		threadSpecificValue = 100;

		// Output from the main thread
		Console.WriteLine($"Main thread - threadSpecificValue: {threadSpecificValue}");

		// Create two new threads
		Thread thread1 = new Thread(ThreadMethod);
		Thread thread2 = new Thread(ThreadMethod);

		// Start the threads
		thread1.Start();
		thread2.Start();

		// Wait for threads to finish
		thread1.Join();
		thread2.Join();

		// Output from the main thread after the other threads have finished
		Console.WriteLine($"Main thread after threads finished - threadSpecificValue: {threadSpecificValue}");
	}

	// Method to be run by each thread
	static void ThreadMethod()
	{
		// Initialize thread-specific value for this thread
		threadSpecificValue = Random.Shared.Next(1, 100);

		// Output from each thread
		Console.WriteLine(
			$"Thread {Environment.CurrentManagedThreadId} {nameof(threadSpecificValue)}: {threadSpecificValue}");
	}
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/threadstatic-62197166/?t=220)

在这次执行中,主线程把自己的值设为 100。
后台线程各自赋予自己的随机值。
在后台线程 join 之后控制权回到主线程时,主线程的值仍然是 100,这证明在其他线程上所做的修改并没有影响主线程的存储。

```text
 [ThreadStatic] threadSpecificValue

 Main thread      thread1                  thread2
 +----------+     +--------------------+   +--------------------+
 | 100      |     | Random.Shared.Next |   | Random.Shared.Next |
 +----------+     +--------------------+   +--------------------+
      |
      v  after thread1.Join() / thread2.Join()
 +----------+
 | 100      |
 +----------+
```

---

## 3. Principal

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/principal-62197167/) · 7:40

位于 `System.Security.Principal` 命名空间中的 `IPrincipal` 接口,是 .NET 中表示安全上下文的标准方式。
它在 ASP.NET Core 以及 WinUI 这类桌面框架中被大量使用。
在 .NET MAUI 这类移动框架中,系统通常默认使用 iOS 和 Android 的原生实现来进行身份管理。
principal 充当一个容器,装载与用户身份相关的全部信息,包括其角色、声明(claims)和具体权限。

### 核心概念

*   **IPrincipal and ClaimsPrincipal**:用于管理用户身份和安全上下文的核心接口和类。
*   **Security Context(安全上下文)**:principal 的通俗叫法,用户的角色和权限存储在其中。
*   **Thread Propagation(线程传播)**:在异步操作期间,身份信息在线程之间传递的机制。
*   **ExecutionContext**:.NET 的内部机制,负责在线程切换时保存和恢复上下文(包括 principal)。
*   **Legacy vs. Modern .NET(旧版与现代 .NET)**:principal 的处理方式从绑定于线程(.NET Framework 4.5 之前)演进为随上下文传播。

### 课程笔记

在过去,可以使用 `Thread.CurrentPrincipal` 手动把一个 principal 赋给当前线程的上下文。
这会把安全身份严格绑定到正在执行的线程上。

```csharp
Thread.CurrentPrincipal = new GenericPrincipal(new WindowsIdentity("user"), new[] { "Admin" });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/principal-62197167/?t=10)

在现代 ASP.NET Core 应用中,开发者很少需要直接与 `Thread.CurrentPrincipal` 打交道,因为框架会自动处理身份的传播。
应用的配置定义了在请求管道中如何处理身份验证和授权,通常在 `Program.cs` 中配置。

```csharp
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
.WithStaticAssets();

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=Login}/{id?}")
.WithStaticAssets();

app.MapControllerRoute(
    name: "logout",
    pattern: "{controller=Account}/{action=Logout}/{id?}")
.WithStaticAssets();

app.Run();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/principal-62197167/?t=70)

在控制器中,`User` 属性提供对 `ClaimsPrincipal`(它实现了 `IPrincipal`)的访问。
这个对象与 `HttpContext` 一起,包含请求特定的信息,即使执行上下文在线程之间切换,这些信息也必须保持一致。

在旧版 .NET Framework(4.5 版本之前)中,在异步操作期间切换线程常常会导致安全上下文丢失。
然而在现代 .NET 中,`ExecutionContext` 确保这些数据得以保留。
这一点可以通过在登录操作中使用 `ConfigureAwaitOptions.ForceYielding` 强制让出线程来演示。
即使执行在另一个线程上恢复,`HttpContext` 和 `User` principal 仍然完好无损,并保持已认证状态。

```csharp
public class AccountController : Controller
{
	public async Task<IActionResult> Login()
	{
		var user = User;

		// Simulate login (hardcoding a username and role for simplicity)
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, "testuser"),
			new Claim(ClaimTypes.Role, "Admin")
		};
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var principal = new ClaimsPrincipal(identity);

		// Sign in the user
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).ConfigureAwait(ConfigureAwaitOptions.ForceYielding | ConfigureAwaitOptions.None);

		return RedirectToAction("Index", "Home");
	}

	public async Task<IActionResult> Logout()
	{
		var user = User;

		// Sign out the user
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(ConfigureAwaitOptions.ForceYielding | ConfigureAwaitOptions.None);
		return RedirectToAction("Index", "Home");
	}
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/principal-62197167/?t=100)

通过 `ExecutionContext` 自动传播 `IPrincipal` 和 `HttpContext`,是现代 .NET 的一项核心特性,它解决了用户身份在异步上下文切换期间丢失的历史问题。

---

## 4. ExecutionContext

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/) · 13:33

ExecutionContext 是 .NET 的一项基础机制,它使环境状态(例如安全 principal、区域性设置以及 AsyncLocal<T> 数据)能够跨异步操作和线程边界传播。
虽然 async/await 会自动处理这种"流动(flow)",但开发者也可以手动捕获、运行或抑制该上下文,以控制信息在线程之间如何共享,确保即使物理线程发生变化,逻辑上的执行状态也保持一致。

### 核心概念

- **Ambient State Propagation(环境状态传播)**:自动在线程之间携带 `CultureInfo.CurrentCulture`、`Thread.CurrentPrincipal` 和 `AsyncLocal<T>` 等信息。
- **Automatic Flow(自动流动)**:`async` 和 `await` 关键字在线程之间切换时,会自动处理 `ExecutionContext` 的捕获与恢复。
- **Immutability(不可变性)**:上下文一旦设置就是不可变的,这可以防止在任务执行期间被意外覆盖。
- **Manual Control(手动控制)**:开发者可以使用 `ExecutionContext.Capture()` 和 `ExecutionContext.Run()` 手动转移上下文,或者出于安全或性能原因,使用 `ExecutionContext.SuppressFlow()` 阻止其传播。

### 课程笔记

.NET 使用 `ExecutionContext` 来确保某个逻辑执行流特有的信息得以保留,即使该执行在不同的物理线程之间移动。
这包括安全上下文(例如 `IPrincipal`)、同步上下文(用于回到特定线程,比如 UI 线程)以及区域性信息(语言设置)。

#### 初始化环境状态

在下面的示例中,我们初始化了几个由 `ExecutionContext` 管理的值:一个特定的区域性(西班牙语)、一个安全 principal,以及一个存储在 `AsyncLocal<string>` 中的值。

```csharp
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace ExecutionContextExample;

public static class Program
{
    static readonly AsyncLocal<string> asyncLocalData = new();

    public static async Task Main()
    {
        Console.WriteLine("Main thread starts");

        // Assign data controlled by ExecutionContext
        CultureInfo.CurrentCulture = new CultureInfo("es-ES");
        Thread.CurrentPrincipal = new ClaimsPrincipal();
        asyncLocalData.Value = "Initial Value";

        PrintThreadValues();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/?t=100)

`PrintThreadValues` 辅助方法演示了如何使用标准 .NET API 从当前线程访问这些值:

```csharp
    static void PrintThreadValues()
    {
        Console.WriteLine($"Thread ID: {Environment.CurrentManagedThreadId}");
        Console.WriteLine($"Culture: {CultureInfo.CurrentCulture.DisplayName}");
        Console.WriteLine($"Principal: {Thread.CurrentPrincipal?.GetType()}");
        Console.WriteLine($"AsyncLocalData: {asyncLocalData.Value}");

        Console.WriteLine();
    }
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/?t=190)

#### 手动操作上下文

虽然 `async/await` 是处理异步工作的首选方式,但手动创建线程可以演示 `ExecutionContext` 是如何被捕获和应用的。
如果启动一个新线程时没有让上下文流动过去,它将使用系统的默认值。
然而,通过使用 `ExecutionContext.Capture()` 和 `ExecutionContext.Run()`,我们可以强制后台线程在主线程的上下文中执行。

```csharp
        var mainThreadExecutionContext = ExecutionContext.Capture() ?? throw new InvalidOperationException("ExecutionContext only null when suppressed");

        var thread = new Thread(() =>
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-UK");
            Thread.CurrentPrincipal = new DomeTrainPrinciple();
            asyncLocalData.Value = "AsyncLocalData in Thread";

            Console.WriteLine("Background Thread after assigning values");
            PrintThreadValues();

            ExecutionContext.Run(mainThreadExecutionContext, _ =>
            {
                Console.WriteLine("Same Background Thread, but using MainThread's ExecutionContext");
                PrintThreadValues();
            }, null);
        });

        // Execute Thread
        thread.Start();
        thread.Join();
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/?t=250)

在这个场景中,尽管代码运行在后台线程上,对 `ExecutionContext.Run` 的调用仍会恢复从主线程捕获的西班牙语区域性和原始的 `AsyncLocal` 值。

```text
 Main thread                         Background thread
 +-----------------------------+     +--------------------------------+
 | es-ES                       |     | en-UK                          |
 | ClaimsPrincipal             |     | DomeTrainPrinciple             |
 | "Initial Value"             |     | "AsyncLocalData in Thread"     |
 +-----------------------------+     +--------------------------------+
        |                                          |
        | ExecutionContext.Capture()               |
        v                                          v
 mainThreadExecutionContext ------> ExecutionContext.Run(...)
                                     +--------------------------------+
                                     | es-ES, "Initial Value"         |
                                     +--------------------------------+
```

#### 使用 Async/Await 自动流动

在使用 `Task.Run` 搭配 `await` 时,.NET 会自动让 `ExecutionContext` 流动。
这是开发者所依赖的标准行为,用来确保用户身份或区域性设置能够跨越异步边界保持下去。

```csharp
        await Task.Run(() =>
        {
            Console.WriteLine("Print Values from Task.Run()");
            PrintThreadValues();
        });
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/?t=550)

#### 抑制流动

在某些情况下,例如处理高度敏感的信息时,或者为了在底层代码中进行性能优化,你可能希望阻止 `ExecutionContext` 流向新线程。
这可以通过 `ExecutionContext.SuppressFlow()` 实现。

```csharp
        // Prevent async/await from automatically flowing the ExecutionContext
        ExecutionContext.SuppressFlow();

        await Task.Run(() =>
        {
            Console.WriteLine("Print Values from Task.Run() With Execution Context Suppressed");
            PrintThreadValues();
        });
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/executioncontext-62197168/?t=610)

抑制流动之后,后台线程会回退到默认值(例如操作系统默认的区域性和一个为 null 的 principal),因为它不再从调用线程接收上下文。
如有需要,可以使用 `ExecutionContext.RestoreFlow()` 恢复流动。

---

## 5. SynchronizationContext

> [观看本课](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/synchronizationcontext-62197169/) · 12:39

### 总结

SynchronizationContext 是 .NET 中管理异步代码执行的一个抽象,其主要作用是让 UI 框架在被 await 的任务完成后,把执行封送(marshal)回原来的调用线程(UI 线程)。
默认情况下,await 会捕获当前上下文以便在其上恢复执行,但开发者可以使用 ConfigureAwait(false) 选择不这样做,它会把上下文设为 null,并允许后续代码在任意可用的线程池线程上运行。
虽然它对 .NET MAUI 或 WinForms 这类传统 UI 应用至关重要,但 ASP.NET Core 和 Blazor 这类现代 Web 框架并不使用 SynchronizationContext,而是依赖请求范围的数据或浏览器事件循环。

### 核心概念

* **Context Capture(上下文捕获)**:`await` 存储当前 `SynchronizationContext`,以便在同一线程上恢复执行的机制。
* **UI Marshalling(UI 封送)**:使用框架特定的实现(例如 iOS 上的 `UIKitSynchronizationContext`),从后台任务中安全地与 UI 元素交互。
* **ConfigureAwait(false)**:一种绕过上下文捕获的方法,通过允许后续代码在线程池上运行来提升性能并避免死锁。
* **HttpContext**:在没有 `SynchronizationContext` 的情况下,ASP.NET Core 用于管理请求范围状态的替代方案。
* **Browser Render Loop(浏览器渲染循环)**:Blazor 和 Web 浏览器在没有专门的 .NET `SynchronizationContext` 的情况下处理 UI 更新的机制。

### 课程笔记

`SynchronizationContext` 是 .NET 用来管理异步代码执行、并把后续代码路由回合适线程的底层机制。
这在 UI 框架中尤为重要,因为在 UI 框架中只有主线程(UI 线程)被允许更新用户界面。

#### UI 框架中的上下文

在 UI 应用中,例如一个 .NET MAUI 应用,通常由 UI 线程发起"下拉刷新"之类的操作。
当异步方法执行到 `await` 关键字时,当前线程会被释放,以保持对用户的响应。
在底层,.NET 运行时会捕获与该线程关联的 `SynchronizationContext`。
当任务完成时,运行时使用这个捕获到的上下文把执行封送回原来的线程。

在下面的示例中,我们跟踪被 await 的操作前后的线程身份,以观察这一行为:

```csharp
[RelayCommand]
async Task Refresh(CancellationToken token)
{
    var thread = Thread.CurrentThread;

    TopStoryCollection.Clear();

    var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2), token);

    try
    {
        await foreach (var story in GetTopStories(StoriesConstants.NumberOfStories, token)
                           .ConfigureAwait(false))
        {
            var threadAfterConfigureAwaitFalse = Thread.CurrentThread;

            if (!TopStoryCollection.Any(x => x.Title.Equals(story.Title, StringComparison.Ordinal)))
                InsertIntoSortedCollection(TopStoryCollection, (a, b) => b.Score.CompareTo(a.Score), story);
        }
    }
    catch (Exception e)
    {
        OnPullToRefreshFailed(e.ToString());
    }
    finally
    {
        await minimumRefreshTimeTask.ConfigureAwait(false);
        IsListRefreshing = false;
    }
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/synchronizationcontext-62197169/?t=130)

#### 内部实现

虽然 `SynchronizationContext` 是 `Thread` 类中的一个私有字段,但可以在调试时查看它。
在运行于 iOS 上的 .NET MAUI 应用中,这个字段保存的是一个 `UIKitSynchronizationContext` 实例。
这个类实现了标准的 .NET `SynchronizationContext`,但在底层使用 iOS 的 `UIKit` 来处理线程封送。

#### 禁用上下文捕获

当我们使用 `ConfigureAwait(false)` 时,就是明确告诉运行时不要捕获 `SynchronizationContext`。
这实际上会把该方法后续代码的上下文设为 `null`。
当被 await 的任务完成时,运行时看到上下文为 `null`,就会直接从线程池中挑选任意一个可用线程来继续执行。

```text
 UI thread: await ...
      |
      +-- default ----------------> capture SynchronizationContext
      |                               |  task completes
      |                               v
      |                             marshal back to UI thread
      |
      +-- ConfigureAwait(false) --> context = null
                                      |  task completes
                                      v
                                    any thread pool thread
```

这也可以使用较新的 `ConfigureAwaitOptions` 枚举来实现:

```csharp
await foreach (var story in GetTopStories(storyCount: StoriesConstants.NumberOfStories, token).ConfigureAwait(ConfigureAwaitOptions.None))
{
    var threadAfterConfigureAwaitFalse = Thread.CurrentThread;

    if (!TopStoryCollection.Any(x :StoryModel => x.Title.Equals(story.Title, StringComparison.Ordinal)))
        InsertIntoSortedCollection(TopStoryCollection, comparison: (a :StoryModel, b :StoryModel) => b.Score.CompareTo(a.Score), story);
}
```

[▶ 观看](https://dometrain.com/take/course/from-zero-to-hero-asynchronous-programming-in-csharp-3002112/synchronizationcontext-62197169/?t=415)

`ConfigureAwait(false)` 和 `ConfigureAwaitOptions.None` 执行的是相同的操作:它们阻止运行时尝试封送回调用线程,这是库代码和非 UI 逻辑的最佳实践,可以避免性能开销和潜在的死锁。

#### 没有 SynchronizationContext 的框架

并非所有 .NET 框架都使用 `SynchronizationContext`:

*   **ASP.NET Core**:这是唯一一个没有 `SynchronizationContext` 的主要 UI 相关框架。UI 更新(HTML/CSS/JS)在服务器上渲染后发送到浏览器,由浏览器处理实际的渲染循环。ASP.NET Core 不使用 `SynchronizationContext`,而是使用 `HttpContext` 来管理请求范围的数据(headers、cookies、session)。在服务中可以通过 `IHttpContextAccessor` 访问它。
*   **Blazor Server**:UI 更新在服务器上处理,并通过 SignalR 发送到浏览器。随后浏览器在自己的渲染循环中渲染这些更新,从而不再需要服务器端的 `SynchronizationContext`。
*   **Blazor WebAssembly**:所有内容都在设备本地、在浏览器的事件循环中运行。浏览器为 UI 组件管理一个渲染队列,因此不需要通过 `SynchronizationContext` 进行标准的 .NET 线程封送。

在这些框架中,`async`/`await` 总是会在线程池中任意可用的线程上继续执行,因为没有可捕获的上下文。

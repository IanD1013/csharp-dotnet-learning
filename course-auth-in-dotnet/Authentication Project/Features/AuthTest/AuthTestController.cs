using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Project.Features.AuthTest;

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

        var result = await HttpContext.AuthenticateAsync();

        TempData["Message"] = $"Authentication result: {(result.Succeeded ? "Success" : "Failed")}";

        return RedirectToAction("Index");
    }

    /// <summary>
    /// Trigger challenge (redirects to login if not authenticated)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ChallengeUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.ChallengeAsync()");

        var properties = new AuthenticationProperties
        {
            RedirectUri = "/user/info"
        };

        return new ChallengeResult(properties);
    }

    /// <summary>
    /// Trigger sign in with test claims
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignInUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignInAsync(principal)");

        var myClaims = new List<Claim>
        {
            new("sub", "12345"), // sub = subject = UserId
            new("name", "Bob"),
            new("email", "test@email.com"),
            new("role", "developer"),
            new("role", "admin"),
            new("role", "sales"),
        };

        var myIdentity = new ClaimsIdentity(claims: myClaims,
            authenticationType: "pwd",
            nameType: "name",
            roleType: "role");

        var myPrincipal = new ClaimsPrincipal(myIdentity);

        var items = new Dictionary<string, string>
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

        return Empty;
    }

    /// <summary>
    /// Trigger sign out
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SignOutUser()
    {
        Console.WriteLine("\r\nCalling HttpContext.SignOutAsync();");

        await HttpContext.SignOutAsync();

        return Empty;
    }

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
}
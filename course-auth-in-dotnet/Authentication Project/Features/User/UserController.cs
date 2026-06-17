using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Authentication_Project.Features.User;

using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private readonly ILogger<UserController> logger;

    public UserController(ILogger<UserController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        return View(new LoginModel { ReturnUrl = ReturnUrl });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginCredentials)
    {
        var username = loginCredentials.UserName;

        // validation
        var myClaims = new List<Claim>
        {
            new("sub", "12345"), // sub = subject = UserId
            new("name", username),
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

        var parameters = new Dictionary<string, object>
        {
            { "Param1", "Value1" },
            { "Param2", "Value2" },
            { "Param3", "Value3" }
        };

        var properties = new AuthenticationProperties(items, parameters)
        {
            // RedirectUri = "/AuthTest"
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
        };

        await HttpContext.SignInAsync(myPrincipal, properties);

        return LocalRedirect(Url.IsLocalUrl(loginCredentials.ReturnUrl) ? loginCredentials.ReturnUrl : "/");
    }


    [ValidateAntiForgeryToken]
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


    public IActionResult AccessDenied(string returnUrl = null)
    {
        var userName = User?.Identity?.Name ?? "Unknown";
        logger.LogWarning($"Access denied for user {userName}, attempting to access resource: '{returnUrl}'");
        return View();
    }


    public IActionResult Info()
    {
        //***************************************************************************


        //***************************************************************************

        // 1. Get the user (ClaimsPrincipal) from the HttpContext
        var user = User;

        // 2. Get the Primary Identity of the user (there might be more than one)
        var identity = user.Identity as ClaimsIdentity;

        // 3. Get IsAuthenticated
        var isAuthenticated = identity?.IsAuthenticated;

        // 4. Get AuthenticationType
        var authenticationType = identity?.AuthenticationType;

        // 5. Get the claims from the user
        var claims = User.Claims.ToList();

        // 6. Get the Name claim
        var name = identity?.Name;

        // 7. Check if user has developer or admin role
        var isDeveloper = user.IsInRole("developer");
        var isAdmin = user.IsInRole("admin");


        var model = new UserInfoModel
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
}
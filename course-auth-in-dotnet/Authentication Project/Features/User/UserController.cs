using System.Security.Claims;

namespace Authentication_Project.Features.User;

using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        return View(new LoginModel { ReturnUrl = ReturnUrl });
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
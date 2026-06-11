using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Authentication_Project.CustomAuthHandler;

public class CustomAuthHandler : SignInAuthenticationHandler<CustomAuthHandlerOptions>
{
    public CustomAuthHandler(IOptionsMonitor<CustomAuthHandlerOptions> options, ILoggerFactory logger,
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
        // read the cookie
        var username = Request.Cookies[Options.CookieName];

        if (string.IsNullOrEmpty(username))
        {
            // No cookie found - user not authenticated
            WriteToLog($"No cookie {Options.CookieName} found - user not authenticated");
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Cookie found - user authenticated
        var claims = new List<Claim> { new("name", username) };

        var myIdentity = new ClaimsIdentity(claims: claims, authenticationType: "test", nameType: "name",
            roleType: "role");

        var myPrincipal = new ClaimsPrincipal(myIdentity);

        var ticket = new AuthenticationTicket(myPrincipal, "CustomAuthHandler");

        WriteToLog($"HandleAuthenticateAsync: User '{username}' authenticated");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    protected override Task HandleSignOutAsync(AuthenticationProperties? properties)
    {
        WriteToLog($"HandleSignOutAsync: Signing out the user, deleting cookie '{Options.CookieName}'");

        // Delete the cookie
        Response.Cookies.Delete(Options.CookieName);

        // Redirect
        Response.Redirect(Options.DefaultRedirectPath);

        WriteToLog($"HandleSignOutAsync: Cookie deleted, redirecting to '{Options.DefaultRedirectPath}'");

        return Task.CompletedTask;
    }

    // Handle sign in (user authenticated) - set a cookie and redirect to the default page
    protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        var username = user.Identity?.Name ?? "Unknown";
        WriteToLog($"HandleSignInAsync: Signing in user '{username}'");

        Response.Cookies.Append(
            key: Options.CookieName,
            value: username,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            });

        var redirectUrl = Options.DefaultRedirectPath;
        Response.Redirect(redirectUrl);

        WriteToLog($"HandleSignInAsync: Cookie '{Options.CookieName}' set, redirecting to '{redirectUrl}'");

        return Task.CompletedTask;
    }


    // Handle challenge (user not authenticated) - redirect to the login page
    protected override Task HandleChallengeAsync(AuthenticationProperties? properties)
    {
        WriteToLog($"HandleChallengeAsync: User needs to authenticate, redirecting to '{Options.LoginPath}'");

        Response.Redirect(Options.LoginPath);

        return Task.CompletedTask;
    }
}
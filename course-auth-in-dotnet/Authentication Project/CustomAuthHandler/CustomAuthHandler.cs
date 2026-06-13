using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
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
        var authCookie = Request.Cookies[Options.CookieName];

        if (string.IsNullOrEmpty(authCookie))
        {
            // No cookie found - user not authenticated
            WriteToLog($"No cookie {Options.CookieName} found - user not authenticated");
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Cookie found - user authenticated
        try
        {
            var serializedTicket = Convert.FromBase64String(authCookie);
            
            var provider = DataProtectionProvider.Create("MyApp");
            var protector = provider.CreateProtector("AuthTicket");
            
            var unprotectedBytes = protector.Unprotect(serializedTicket);

            var ticket = TicketSerializer.Default.Deserialize(unprotectedBytes)!;
            
            return Task.FromResult(AuthenticateResult.Success(ticket));

        }
        catch (Exception ex)
        {
            // Handle bad cookie
            return Task.FromResult(AuthenticateResult.Fail(ex));
        }

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
        
        var ticket = new AuthenticationTicket(user, properties, Scheme.Name);
        
        var serializedTicket = TicketSerializer.Default.Serialize(ticket);

        var provider = DataProtectionProvider.Create("MyApp");
        var protector = provider.CreateProtector("AuthTicket");
        
        var protectedBytes = protector.Protect(serializedTicket);
        
        var cookieValue = Convert.ToBase64String(protectedBytes);

        Response.Cookies.Append(
            key: Options.CookieName,
            value: cookieValue,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            });

        var redirectUrl = Options.DefaultRedirectPath;
        
        if (properties?.RedirectUri != null)
        {
            redirectUrl = properties.RedirectUri;
        }
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


    // Handle forbidden (user authenticated but not authorized) 
    protected override Task HandleForbiddenAsync(AuthenticationProperties? properties)
    {
        WriteToLog($"HandleForbiddenAsync: Access denied, redirecting to '{Options.AccessDeniedPath}'");
        
        Response.Redirect(Options.AccessDeniedPath);
        
        return Task.CompletedTask;
    }
}
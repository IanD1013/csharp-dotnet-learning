using Microsoft.AspNetCore.Authentication;

namespace Authentication_Project.CustomAuthHandler;

public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    // The default scheme for this handler
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";

    // Path to redirect for login challenges
    public string LoginPath { get; set; } = "/User/Login";

    // Name of the authentication cookie (default: "AuthCookie")
    public string CookieName { get; set; } = "AuthCookie";
    
    // Default redirect path after sign in/out
    public string DefaultRedirectPath { get; set; } = "/AuthTest";
}
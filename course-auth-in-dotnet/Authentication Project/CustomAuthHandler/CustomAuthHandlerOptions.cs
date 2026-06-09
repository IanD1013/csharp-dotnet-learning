using Microsoft.AspNetCore.Authentication;

namespace Authentication_Project.CustomAuthHandler;

public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    // The default scheme for this handler
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";
    
    // Path to redirect for login challenges
    public string LoginPath { get; set; } = "/User/Login";
}
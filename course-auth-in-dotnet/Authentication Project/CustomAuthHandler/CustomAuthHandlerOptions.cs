using Microsoft.AspNetCore.Authentication;

namespace Authentication_Project.CustomAuthHandler;

public class CustomAuthHandlerOptions : AuthenticationSchemeOptions
{
    public static string DefaultAuthenticationScheme = "CustomAuthHandler";
}
using Microsoft.AspNetCore.Authentication;

namespace Authentication_Project.CustomAuthHandler;

public static class CustomAuthHandlerExtensions
{
    public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder,
        string authenticationScheme,
        string? displayName,
        Action<CustomAuthHandlerOptions> configureOption)
    {
        return builder.AddScheme<CustomAuthHandlerOptions, CustomAuthHandler>(authenticationScheme,
            displayName,
            configureOption);
    }
}
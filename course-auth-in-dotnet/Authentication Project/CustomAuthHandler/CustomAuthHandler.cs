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
        return Task.FromResult(AuthenticateResult.NoResult());
    }

    protected override Task HandleSignOutAsync(AuthenticationProperties? properties)
    {
        throw new NotImplementedException();
    }

    protected override Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        throw new NotImplementedException();
    }
}
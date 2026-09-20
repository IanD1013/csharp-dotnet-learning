using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Advanced.Demos.Api;

/// <summary>
/// Lessons 2, 4 and 5 are about things that only go wrong inside a real host, so they start one
/// here on an ephemeral loopback port and call it over HTTP. Logging goes through
/// <see cref="InlineLoggerProvider"/> so log lines land in order between the Console.WriteLine
/// calls around them instead of arriving later on a background thread.
/// </summary>
internal static class InProcessApi
{
    public static WebApplicationBuilder CreateBuilder()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Logging.ClearProviders();
        builder.Logging.AddProvider(new InlineLoggerProvider());
        builder.Logging.AddFilter("Microsoft", LogLevel.Warning);

        builder.WebHost.UseUrls("http://127.0.0.1:0");
        return builder;
    }

    public static async Task<HttpClient> StartAsync(WebApplication app)
    {
        await app.StartAsync();
        return new HttpClient { BaseAddress = new Uri(app.Urls.First()) };
    }
}

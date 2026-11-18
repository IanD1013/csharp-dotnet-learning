using DomeGym.Section5.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DomeGym.Section5.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder UseInfrastructureMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<EventualConsistencyMiddleware>();
}

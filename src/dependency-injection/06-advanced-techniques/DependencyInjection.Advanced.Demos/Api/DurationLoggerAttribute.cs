using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Advanced.Demos.Api;

/// <summary>
/// Lesson 2, the anti-pattern itself. An attribute cannot take constructor dependencies, so the
/// logger is fetched from the request's service provider instead. Nothing on this type says it
/// needs an ILogger, and a test has to build an ActionExecutingContext with an HttpContext with a
/// RequestServices provider before the method gets past this line.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class DurationLoggerAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<DurationLoggerAttribute>>();
            logger.LogInformation("Request with name {Name} completed in {Elapsed}ms",
                context.ActionDescriptor.DisplayName,
                sw.ElapsedMilliseconds);
        }
    }
}

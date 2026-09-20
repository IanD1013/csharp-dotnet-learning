using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Advanced.Demos.Api;

/// <summary>
/// Lesson 2, the same behaviour without the service locator. The filter is a service rather than
/// an attribute, so it can declare the logger in its constructor and be applied with
/// [ServiceFilter]. A test constructs it with a fake logger and nothing else.
/// </summary>
public sealed class DurationLoggerFilter : IAsyncActionFilter
{
    private readonly ILogger<DurationLoggerFilter> _logger;

    public DurationLoggerFilter(ILogger<DurationLoggerFilter> logger) => _logger = logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            _logger.LogInformation("Request with name {Name} completed in {Elapsed}ms",
                context.ActionDescriptor.DisplayName,
                sw.ElapsedMilliseconds);
        }
    }
}

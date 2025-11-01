using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogConsoleApp;
using SerilogTimings.Extensions;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Destructure.ByTransforming<Payment>(p => new { p.PaymentId, p.UserId })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

// using (logger.TimeOperation("Processing payment with id {PaymentId}", payment.PaymentId))
// {
//     await Task.Delay(50);
//     logger.Information("Received payment by user with id {UserId}", payment.UserId);
// }

var op = logger.BeginOperation("Processing payment with id {PaymentId}", payment.PaymentId);

await Task.Delay(50);
logger.Information("Received payment by user with id {UserId}", payment.UserId);

// op.Complete();
op.Abandon();

Log.CloseAndFlush();
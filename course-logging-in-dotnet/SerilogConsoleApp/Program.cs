using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogConsoleApp;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .Enrich.FromLogContext()
    .Destructure.ByTransforming<Payment>(p => new { p.PaymentId, p.UserId })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

using (LogContext.PushProperty("PaymentId", payment.PaymentId))
{
    logger.Information("Received payment by user with id {UserId}", payment.UserId);
}


Log.CloseAndFlush();
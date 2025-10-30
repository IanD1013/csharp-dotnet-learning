using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogConsoleApp;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
    .Destructure.ByTransforming<Payment>(p => new { p.PaymentId, p.UserId })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

var paymentData = new Dictionary<string, object>
{
    { "PaymentId", payment.PaymentId },
    { "UserId", payment.UserId },
    { "OccuredAt", payment.OccuredAt }
};
    
logger.Information("New payment with data: {@PaymentData}", payment);

Log.CloseAndFlush();
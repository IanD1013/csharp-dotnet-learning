using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogConsoleApp;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
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
    
logger.Information("New payment with data: {PaymentData}", payment);
logger.Information("New payment with data: {$PaymentData}", paymentData);
logger.Information("New payment with data: {@PaymentData}", paymentData);

Log.CloseAndFlush();
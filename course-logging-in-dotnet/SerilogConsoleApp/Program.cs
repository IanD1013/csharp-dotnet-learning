using Destructurama;
using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogConsoleApp;
using SerilogTimings.Extensions;

ILogger logger = new LoggerConfiguration()
    // .WriteTo.Async(x => x.Console())
    // .WriteTo.Sink<IanSink>()
    .WriteTo.Sink(new IanSink())
    .WriteTo.IanSink()
    .Destructure.UsingAttributes()
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    Email = "nick@dometrain.com",
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

logger.Information("Received payment with details {@PaymentData}", payment);

// Log.CloseAndFlush();
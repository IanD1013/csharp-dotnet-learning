using Microsoft.Extensions.Logging;

namespace HighPerformanceLoggingConsoleApp;

public static partial class LoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Information, 
        EventId = 5001,
        Message = "Payment created for {email} with amount {amount} for product {productId}")]
    public static partial void LogPaymentCreation(this ILogger logger, string email, decimal amount, int productId);
}
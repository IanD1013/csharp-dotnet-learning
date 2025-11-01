using Microsoft.Extensions.Logging;

namespace HighPerformanceLoggingConsoleApp;

public class PaymentService
{
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }

    public void CreatePayment(string email, decimal amount, int productId)
    {
        _logger.LogPaymentCreation(email, amount, productId);
    }
}
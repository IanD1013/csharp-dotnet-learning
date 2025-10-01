
using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentData = new PaymentData
{
    PaymentId = 1,
    Amount = 15.99m
};

logger.LogInformation("New Payment with Data {PaymentData}", JsonSerializer.Serialize(paymentData));  // New Payment with Data {"PaymentId":1,"Amount":15.99}
logger.LogInformation("New Payment with Data {PaymentData}", paymentData);  // New Payment with Data PaymentData -> call toString() method

class PaymentData
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}

record PaymentData2 // will implement toString()
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}
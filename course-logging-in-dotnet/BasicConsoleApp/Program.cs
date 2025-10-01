
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;
var date = DateTime.Now;

logger.LogInformation("New Payment with id {paymentId} for ${Total:C} at {Date:F}", paymentId, amount, date);
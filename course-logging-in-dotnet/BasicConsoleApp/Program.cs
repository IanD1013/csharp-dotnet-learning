using System.Text.Json;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
        x.IncludeScopes = true;
        x.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

using (logger.BeginScope("{PaymentId}", paymentId))
using (logger.BeginScope("{TotalAmount}", amount))
    
try
{
    logger.LogInformation("New payment for ${Total}", amount);
    // processing 
}
finally
{
    logger.LogInformation("Payment processing completed");
}
  

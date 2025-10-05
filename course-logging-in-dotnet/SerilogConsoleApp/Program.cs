using Serilog;

ILogger logger = new LoggerConfiguration()
    .CreateLogger();
    
logger.Information("Hello World!");
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
    // .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
    .CreateLogger();

Log.Logger = logger;
    
logger.Information("Hello World!");

Log.CloseAndFlush();
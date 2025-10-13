using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration() // specify how Serilog is configured by setting the static Log.Logger
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Host.UseSerilog(); // this hijacks everything to use Serilog

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


using _5_ExploreOptionsPattern.Logging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet(pattern: "/logging/options",
        handler: static (IOptions<LoggingOptions> options) =>
        {
            var loggingOptions = options.Value;
            
            return Results.Json(data: loggingOptions);
        })
    .WithName("GetLoggingOptions")
    .WithOpenApi();

app.Run();
using _5_ExploreOptionsPattern.Features;
using _5_ExploreOptionsPattern.Logging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Bind IOptions<LoggingOptions> to the "Logging" config section for DI
builder.Services.AddOptions<LoggingOptions>()
    .Bind(config: builder.Configuration.GetSection(
        key: LoggingOptions.LoggingConfigurationSectionName));

// Add named configuration options
builder.Services.AddOptions<FeatureOptions>(
        name: "TodoApi")
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:TodoApi"));

builder.Services.AddOptions<FeatureOptions>(
        name: "WeatherStation")
    .Bind(config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"));

// Add services to the container.
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
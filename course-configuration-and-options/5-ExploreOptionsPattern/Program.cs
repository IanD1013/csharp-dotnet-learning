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

builder.Services.Configure<FeatureOptions>(
    name: "WeatherStation",
    config: builder.Configuration.GetSection(
        key: "Features:WeatherStation"),
    configureBinder: static opts =>
    {
        opts.BindNonPublicProperties = false;
        opts.ErrorOnUnknownConfiguration = true;
    });

// Overrides (and/or merges) with existing configured bindings
builder.Services.PostConfigure<FeatureOptions>(
    name: "WeatherStation",
    configureOptions: static options =>
    {
        options.Version = new Version(1, 0);
        options.Endpoint = new Uri("https://freetestapi.com/api/v1/weathers");
        options.Tags =
        [
            "fake-weather",
            "test-api"
        ];
    });

// Override all config-bound instances of FeatureOptions
builder.Services.PostConfigureAll<FeatureOptions>(
    configureOptions: static options => options.Tags ??= []);

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


app.MapGet(pattern: "/features",
        handler: static (IOptionsSnapshot<FeatureOptions> options) =>
        {
            var todo = options.Get("TodoApi");
            var weatherStation = options.Get("WeatherStation");

            return Results.Json(data: new
            {
                TodoApis = todo,
                WeatherStation = weatherStation
            });
        })
    .WithName("GetFeatureOptions")
    .WithOpenApi();

app.Run();
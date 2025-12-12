using _6_OptionsPatternValidation;
using _6_OptionsPatternValidation.Features;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    .ValidateOnStart()
    .ValidateDataAnnotations();

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation")
    .ValidateDataAnnotations();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
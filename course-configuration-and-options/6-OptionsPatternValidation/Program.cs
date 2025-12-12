using _6_OptionsPatternValidation;
using _6_OptionsPatternValidation.Features;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    .ValidateOnStart();

builder.Services.AddOptionsWithValidateOnStart<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation");

builder.Services.TryAddEnumerable(
    descriptor: ServiceDescriptor.Singleton<IValidateOptions<FeatureOptions>, FeatureOptions>());

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
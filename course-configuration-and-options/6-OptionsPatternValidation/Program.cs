using _6_OptionsPatternValidation;
using _6_OptionsPatternValidation.Features;
using static _6_OptionsPatternValidation.Features.FeatureOptionsValidators;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOptions<FeatureOptions>(name: "TodoApi")
    .BindConfiguration(configSectionPath: "Features:TodoApi")
    .ValidateDataAnnotations();

builder.Services.AddOptions<FeatureOptions>(name: "WeatherStation")
    .BindConfiguration(configSectionPath: "Features:WeatherStation")
    .Validate(validation: EnabledWithMissingEndpoint.Validation,
        failureMessage: EnabledWithMissingEndpoint.FailureMessage);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
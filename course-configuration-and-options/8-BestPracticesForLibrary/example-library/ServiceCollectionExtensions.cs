using Microsoft.Extensions.Configuration;
using WidgetOptions = example_library.WidgetOptions;

// There's some debate over this namespace!
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    // Add with IConfigurationSection
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        IConfigurationSection widgetConfigSection)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(widgetConfigSection);

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .BindConfiguration(widgetConfigSection.Key)
            .ValidateDataAnnotations();

        return services;
    }
    
    // Add with string configSectionPath
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configSectionPath);

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .BindConfiguration(configSectionPath)
            .ValidateDataAnnotations();

        return services;
    }

    // Add with WidgetOptions
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        WidgetOptions widgetOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(widgetOptions);

        // TODO:
        //   Determine if you want to use:
        //      .Configure(configureOptions);     // Runs before PostConfigure calls
        //   Or instead use:
        //      .PostConfigure(configureOptions); // Runs after all Configure calls

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .PostConfigure(configureOptions: existing =>
            {
                // Overwrite existing values with
                // user provided values.
                existing.Color = widgetOptions.Color;
                existing.ImageUrl = widgetOptions.ImageUrl;
                existing.Opacity = widgetOptions.Opacity;
                existing.IsEnabled = widgetOptions.IsEnabled;
                existing.Size = widgetOptions.Size;
            })
            .ValidateDataAnnotations();

        return services;
    }

    // Add with Action
    public static IServiceCollection AddWidgetServices(
        this IServiceCollection services,
        Action<WidgetOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        // TODO:
        //   Determine if you want to use:
        //      .Configure(configureOptions);     // Runs before PostConfigure calls
        //   Or instead use:
        //      .PostConfigure(configureOptions); // Runs after all Configure calls

        services.AddOptionsWithValidateOnStart<WidgetOptions>()
            .Configure(configureOptions)
            .ValidateDataAnnotations();

        return services;
    }
}
using _3_ConfigurationProviders;

var builder = Host.CreateEmptyApplicationBuilder(null);

#region File Configuration Providers

#region JSON {;}

// Microsoft.Extensions.Configuration.Json

// For example: appsettings.json
builder.Configuration.AddJsonFile(
    path: "appsettings.json",
    optional: true,
    reloadOnChange: true);

// For example: appsettings.Staging.json
builder.Configuration.AddJsonFile(
    path: $"appsettings.{builder.Environment.EnvironmentName}.json",
    optional: true,
    reloadOnChange: true);

#endregion

#endregion

Console.WriteLine(builder.Configuration.GetDebugView());
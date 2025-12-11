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

#region Environment Variables
// Microsoft.Extensions.Configuration.EnvironmentVariables

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddEnvironmentVariables(prefix: "MY_APP_");

#endregion

#region Command Line Args
// Microsoft.Extensions.Configuration.CommandLine
builder.Configuration.AddCommandLine(args);

var switchMappings =
    new Dictionary<string, string>(comparer: StringComparer.OrdinalIgnoreCase)
    {
        // -d="0:0:05" or -d "0:0:05"
        ["-d"] = "Delay",

        // --apiOn=true /apiOn=true
        ["--apiOn"] = "TodoApiOptions:Enabled",

        // --todoUri <uri> /todoUri <uri>
        ["--todoUri"] = "TodoApiOptions:BaseAddress"
    };

#endregion

Console.WriteLine(builder.Configuration.GetDebugView());
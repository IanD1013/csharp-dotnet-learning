using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

builder.AddInMemoryCollection([
    new KeyValuePair<string, string?>("Key", "123 that was easy...")
]);

var config = builder.Build();

var value = config["Key"];

Console.WriteLine(value);
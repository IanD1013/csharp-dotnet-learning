using _2_GettingStarted;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

_ = builder.Configuration;

var host = builder.Build();
host.Run();
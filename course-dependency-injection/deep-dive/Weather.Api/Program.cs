using Microsoft.Extensions.DependencyInjection.Extensions;
using Weather.Api.Logging;
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

// builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
// builder.Services.AddScoped<IWeatherService, InMemoryWeatherService>();
var openWeatherServiceDescriptor = new ServiceDescriptor(typeof(IWeatherService), typeof(OpenWeatherService), ServiceLifetime.Transient);
var inMemWeatherServiceDescriptor = new ServiceDescriptor(typeof(IWeatherService), typeof(InMemoryWeatherService), ServiceLifetime.Transient);

// builder.Services.Add(openWeatherServiceDescriptor);  // 190
// builder.Services.Add(inMemWeatherServiceDescriptor); // 191

// builder.Services.TryAdd(openWeatherServiceDescriptor);  // 190
// builder.Services.TryAdd(inMemWeatherServiceDescriptor); // 190

builder.Services.TryAddEnumerable(openWeatherServiceDescriptor);    // 190
builder.Services.TryAddEnumerable(inMemWeatherServiceDescriptor);   // 191
builder.Services.TryAddEnumerable(inMemWeatherServiceDescriptor);   // 191

builder.Services.TryAddEnumerable(new [] { openWeatherServiceDescriptor, inMemWeatherServiceDescriptor });   // 191

builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

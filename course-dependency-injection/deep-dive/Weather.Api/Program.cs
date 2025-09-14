using Weather.Api.Logging;
using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.AddScoped<IWeatherService, InMemoryWeatherService>();

var openWeatherServiceDescriptor = new ServiceDescriptor(
    typeof(IWeatherService), 
    typeof(OpenWeatherService), 
    ServiceLifetime.Transient);

var weatherServiceDescriptor =
    new ServiceDescriptor(
        typeof(IWeatherService), 
        provider => new OpenWeatherService(provider.GetRequiredService<IHttpClientFactory>()), 
        ServiceLifetime.Transient);

builder.Services.Add(openWeatherServiceDescriptor);

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

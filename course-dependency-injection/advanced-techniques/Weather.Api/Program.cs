using Weather.Api.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherService, OpenWeatherService>();
builder.Services.Decorate<IWeatherService, LoggedWeatherService>();
builder.Services.TryDecorate<IWeatherService, LoggedWeatherService>();

// builder.Services.AddTransient<OpenWeatherService>();
// builder.Services.AddTransient<IWeatherService>(provider =>
//     new LoggedWeatherService(provider.GetRequiredService<OpenWeatherService>(),
//                         provider.GetRequiredService<ILogger<IWeatherService>>()));

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

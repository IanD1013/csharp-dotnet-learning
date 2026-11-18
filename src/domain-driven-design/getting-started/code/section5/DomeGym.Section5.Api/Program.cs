using DomeGym.Section5.Application;
using DomeGym.Section5.Infrastructure;
using DomeGym.Section5.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddProblemDetails();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

WebApplication app = builder.Build();
{
    app.UseExceptionHandler();
    app.UseInfrastructureMiddleware();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        await app.Services.MigrateDatabaseAsync();
    }

    app.MapControllers();

    await app.RunAsync();
}

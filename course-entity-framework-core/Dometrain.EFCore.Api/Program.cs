using System.Text.Json.Serialization;
using Dometrain.EFCore.Api.Data;
using Dometrain.EfCore.Api.Repositories;
using Dometrain.EFCore.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddTransient<IGenreRepository, GenreRepository>(); 
builder.Services.AddTransient<IBatchGenreService, BatchGenreService>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); // adds a converter so that enums are serialized as strings instead of numbers.

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add a DbContext here:
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
    optionsBuilder
        .UseSqlServer(connectionString, sqlBuilder => sqlBuilder.MaxBatchSize(50))
        .LogTo(Console.WriteLine);
},
ServiceLifetime.Scoped, // service lifetime of the db context
ServiceLifetime.Singleton); // when is our db context options changing

var app = builder.Build();

// Check if the DB was migrated: 
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
        throw new Exception("Database is not fully migrated for MoviesContext.");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
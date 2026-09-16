using Dapper;
using DockerForDevelopers.DemoApp.Api;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.Configure<Settings>(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin());

app.MapGet("/podcasts", async (IOptions<Settings> settings) =>
{
    await using var db = new SqlConnection(settings.Value.ConnectionString);

    return (await db.QueryAsync<Podcast>("SELECT * FROM Podcasts")).Select(x => x.Title);
});

app.Run();

sealed record Podcast(Guid Id, string Title);

// Top-level statements compile to an internal Program class. WebApplicationFactory<Program>
// in the test project needs it to be public, and this partial declaration is what makes it so.
public partial class Program;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("get-example", () => "Hello from GET");
app.MapPost("post-example", () => "Hello from POST");

app.MapGet("ok-object", () => Results.Ok(new
{
    Name = "Ian Dong"
}));

app.MapGet("slow-request", async () =>
{
    await Task.Delay(5000);
    return Results.Ok(new
    {
        Name = "Ian Dong"
    });
});

app.Run();
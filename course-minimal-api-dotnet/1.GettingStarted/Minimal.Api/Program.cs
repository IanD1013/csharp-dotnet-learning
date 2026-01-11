using Microsoft.AspNetCore.Mvc;
using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuidGenerator>();

var app = builder.Build();

app.MapGet("get-example", () => "Hello from GET");
app.MapPost("post-example", () => "Hello from POST");

/** THE DIFFERENT RETURN TYPES: **/
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

/** ROUTING REQUESTS: **/
app.MapGet("get", () => "This is a GET");
app.MapPost("post", () => "This is a POST");
app.MapPut("put", () => "This is a PUT");
app.MapDelete("delete", () => "This is a DELETE");

app.MapMethods("options-or-head", ["HEAD", "OPTIONS"],
    () => "Hello from either options or head");

var handler = () => "This is coming from a var";
app.MapGet("handler", handler);
app.MapGet("from-class", Example.SomeMethod);

/** ROUTE PARAMETERS AND RULES: **/
app.MapGet("get-params/{age:int}", (int age) => $"Age provided was  {age}");
app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) => $"Car id provided was {carId}");
app.MapGet("books/{isbn:length(13)}", (string isbn) => $"ISBN provided was {isbn}");

/** PARAMETER BINDING: **/
// searchTerm is from query param by convention
app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if (searchTerm is null)
    {
        return Results.NotFound();
    }

    var results = peopleService.Search(searchTerm);
    return Results.Ok(results);
});

app.MapGet("mix/{routeParam}", (
        [FromRoute] string routeParam,
        [FromQuery(Name = "query")] int queryParam,
        [FromServices] GuidGenerator guidGenerator,
        [FromHeader(Name = "Accept-Encoding")] string encoding) =>
    $"{routeParam}-{queryParam}-{guidGenerator.NewGuid}-{encoding}");

// this implies that person was from body
app.MapPost("people", ([FromBody] Person person) => Results.Ok(person));

app.Run();
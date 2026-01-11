using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);
// service registration starts here

builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuidGenerator>();

// service registration stops here
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

/** SPECIAL PARAMETER TYPES **/
// there are some special parameter types that are automatically bound if so you need them
app.MapGet("httpcontext-1", async context => { await context.Response.WriteAsync("Hello from HttpContext 1"); });

// can also explicitly define HttpContext, but it is grayed out because the HttpContext
// has a dedicated overload, and by default is implied to be the de facto way to do it if
// you only have one parameter, it is going to be the HttpContext
app.MapGet("httpcontext-2",
    async (HttpContext context) => { await context.Response.WriteAsync("Hello from HttpContext 2"); });

app.MapGet("http", async (HttpRequest request, HttpResponse response) =>
{
    var queries = request.QueryString.Value;
    await response.WriteAsync($"Hello from HttpResponse. Queries were: {queries}");
});

app.MapGet("claims", (ClaimsPrincipal user) => { });

app.MapGet("cancel", (CancellationToken token) => { });

app.Run();
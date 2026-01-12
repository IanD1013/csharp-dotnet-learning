using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);
// service registration starts here

builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuidGenerator>();

// service registration stops here
var app = builder.Build();
// Middleware registration starts here

app.MapGet("get-example", () => "Hello from GET");
app.MapPost("post-example", () => "Hello from POST");

#region The Different Return Types

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

#endregion


#region Routing Requests

app.MapGet("get", () => "This is a GET");
app.MapPost("post", () => "This is a POST");
app.MapPut("put", () => "This is a PUT");
app.MapDelete("delete", () => "This is a DELETE");

app.MapMethods("options-or-head", ["HEAD", "OPTIONS"],
    () => "Hello from either options or head");

var handler = () => "This is coming from a var";
app.MapGet("handler", handler);
app.MapGet("from-class", Example.SomeMethod);

#endregion


#region Route Parameters and Rules

/** ROUTE PARAMETERS AND RULES: **/
app.MapGet("get-params/{age:int}", (int age) => $"Age provided was  {age}");
app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) => $"Car id provided was {carId}");
app.MapGet("books/{isbn:length(13)}", (string isbn) => $"ISBN provided was {isbn}");

#endregion


#region Parameter Binding

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

// this implies that person was from body because of "Post"
app.MapPost("people", ([FromBody] Person person) => Results.Ok(person));

#endregion


#region Special Parameter Types

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

#endregion


#region Custom Parameter Binding

app.MapGet("map-point", (MapPoint point) => Results.Ok(point));
app.MapPost("map-point", (MapPoint point) => Results.Ok(point));

#endregion


#region Available Response Types

app.MapGet("simple-string", () => "Hello world");
app.MapGet("json-raw-obj", () => new { message = "Hello world" });
app.MapGet("ok-obj", () => Results.Ok(new { Message = "Hello world" }));
app.MapGet("json-obj", () => Results.Json(new { message = "Hello world" }));
app.MapGet("text-string", () => Results.Text("Hello world"));

app.MapGet("stream-result", () =>
{
    var memoryStream = new MemoryStream();
    var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
    streamWriter.Write("Hello world");
    streamWriter.Flush();
    memoryStream.Seek(0, SeekOrigin.Begin);

    return Results.Stream(memoryStream);
});

app.MapGet("redirect", () => Results.Redirect("https://google.com"));
app.MapGet("download", () => Results.File("./my-file.txt"));

#endregion

// Middleware registration stops here
app.Run();
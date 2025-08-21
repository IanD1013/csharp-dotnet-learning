using WireMock.Server;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

var wiremockServer = WireMockServer.Start();

Console.WriteLine($"Wiremock is now running on: {wiremockServer.Url}");

wiremockServer.Given(Request.Create()
        .WithPath("/example")
        .UsingGet())
    .RespondWith(Response.Create()
        .WithBody("This is coming from wiremock")
        .WithStatusCode(200));

Console.ReadKey();
wiremockServer.Dispose();
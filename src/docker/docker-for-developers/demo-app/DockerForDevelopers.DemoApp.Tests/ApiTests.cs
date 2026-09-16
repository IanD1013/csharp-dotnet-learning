using System.Net;

namespace DockerForDevelopers.DemoApp.Tests;

public class ApiTests
{
    [Fact]
    public async Task GivenGetRequestToPodcastsEndpoint_ShouldReturnOkay()
    {
        var httpClient = new CustomWebApplicationFactory().CreateClient();
        var response = await httpClient.GetAsync("/podcasts");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

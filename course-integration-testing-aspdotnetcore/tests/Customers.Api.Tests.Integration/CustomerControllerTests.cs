using System.Net;
using System.Net.Http.Json;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Customers.Api.Tests.Integration;

public class CustomerControllerTests : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;
    private readonly Faker<CustomerRequest> _customerGenerator =
        new Faker<CustomerRequest>()
            .RuleFor(x => x.FullName, faker => faker.Person.FullName)
            .RuleFor(x => x.Email, faker => faker.Person.Email)
            .RuleFor(x => x.GitHubUsername, "nickchapsas")
            .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public CustomerControllerTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenCustomerIsCreated()
    {
        // Arrange
        var customer = _customerGenerator.Generate();

        // Act
        var response = await _httpClient.PostAsJsonAsync("customers", customer);

        // Assert
        var customerResponse = await response.Content.ReadFromJsonAsync<CustomerResponse>();
        customerResponse.Should().BeEquivalentTo(customer);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem!.Title.Should().Be("Not Found");
        problem.Status.Should().Be(404);
    }
}


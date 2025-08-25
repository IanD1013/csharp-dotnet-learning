using System.Net;
using System.Net.Http.Json;
using Bogus;
using Customers.Api.Contracts.Requests;
using Customers.Api.Contracts.Responses;
using FluentAssertions;
using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class GetAllCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly HttpClient _client;

    private readonly Faker<CustomerRequest> _customerGenerator = new Faker<CustomerRequest>()
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.FullName, faker => faker.Person.FullName)
        .RuleFor(x => x.GitHubUsername, CustomerApiFactory.ValidGithubUser)
        .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth.Date);

    public GetAllCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsCustomer_WhenCustomerExists()
    {
        // Arrange
        var customer = _customerGenerator.Generate();
        var createdResponse = await _client.PostAsJsonAsync("customers", customer);
        var createdCustomer = await createdResponse.Content.ReadFromJsonAsync<CustomerResponse>();

        // Act
        var response = await _client.GetAsync($"customers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customerResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
        customerResponse!.Customers.Single().Should().BeEquivalentTo(createdCustomer);

        // Clean up
        await _client.DeleteAsync($"customers/{createdCustomer!.Id}");
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyResult_WhenNoCustomersExist()
    {
        // Act
        var response = await _client.GetAsync("customers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var customerResponse = await response.Content.ReadFromJsonAsync<GetAllCustomersResponse>();
        customerResponse!.Customers.Should().BeEmpty();
    }
}


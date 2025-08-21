using Xunit;

namespace Customers.Api.Tests.Integration.CustomerController;

public class CreateCustomerControllerTests : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _apiFactory;

    public CreateCustomerControllerTests(CustomerApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
    }
}
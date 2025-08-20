using System.Collections;
using System.Net;
using Xunit;

namespace Customers.Api.Tests.Integration;

public class CustomerControllerTests
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:5001")
    };

    [Fact]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        var response = await _httpClient.GetAsync($"customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(Data) )]
    // [ClassData(typeof(classData) )]
    public async Task Get_ReturnsNotFound_WhenCustomerDoesNotExist2(
        string guidAsText)
    {
        var response = await _httpClient.GetAsync($"customers/{Guid.Parse(guidAsText)}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    public static IEnumerable<object[]> Data { get; } = new[]
    {
        new[] { "22EF1E1A-AE11-43FB-AF55-5B6FFE968656" },
        new[] { "22EF1E1A-AE11-43FB-AF55-5B6FFE968657" },
        new[] { "22EF1E1A-AE11-43FB-AF55-5B6FFE968658" }
    };
}

public class classData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "22EF1E1A-AE11-43FB-AF55-5B6FFE968656" };
        yield return new object[] { "22EF1E1A-AE11-43FB-AF55-5B6FFE968657" };
        yield return new object[] { "22EF1E1A-AE11-43FB-AF55-5B6FFE968658" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
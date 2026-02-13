using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.BookEndpoints;
using Xunit.Abstractions;

namespace RiverBooks.Books.Tests.Endpoints;

public class BookGetById(Fixture fixture, ITestOutputHelper outputHelper) :
    TestClass<Fixture>(fixture, outputHelper)
{
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000001", "The Fellowship of the Ring")]
    [InlineData("00000000-0000-0000-0000-000000000002", "The Two Towers")]
    [InlineData("00000000-0000-0000-0000-000000000003", "The Return of the King")]
    public async Task ReturnsExpectedBookGivenIdAsync(string validId, string expectedTitle)
    {
        Guid id = Guid.Parse(validId);
        var request = new GetBookByIdRequest { Id = id };
        var testResult = await
            Fixture.Client.GETAsync<GetById, GetBookByIdRequest, BookDto>(request);

        testResult.Response.EnsureSuccessStatusCode();
        testResult.Result.Title.Should()
            .Be(expectedTitle);
    }
}
using Xunit;
using Xunit.Abstractions;

namespace AdvancedTechniques.Tests.Unit;

public class DefaultBehaviorTests : IClassFixture<MyClassFixture>
{
    // private readonly Guid _id = Guid.NewGuid();
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly MyClassFixture _fixture;

    public DefaultBehaviorTests(ITestOutputHelper testOutputHelper,
        MyClassFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    [Fact]
    public void ExampleTest1()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
    }

    [Fact]
    public void ExampleTest2()
    {
        _testOutputHelper.WriteLine($"The Guid was: {_fixture.Id}");
    }
}
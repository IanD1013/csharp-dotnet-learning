using FluentAssertions;
using Xunit;

namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    private readonly ValueSamples _sut = new();

    [Fact]
    public void StringAssertionExample()
    {
        var fullName = _sut.FullName;

        fullName.Should().Be("Ian Dong");
        fullName.Should().NotBeEmpty();
        fullName.Should().StartWith("Ian");
        fullName.Should().EndWith("Dong");
    }
}
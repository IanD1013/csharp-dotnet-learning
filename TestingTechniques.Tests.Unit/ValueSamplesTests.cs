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

    [Fact]
    public void NumberAssertionExample()
    {
        var age = _sut.Age;

        age.Should().Be(21);
        age.Should().BePositive();
        age.Should().BeGreaterThan(20);
        age.Should().BeLessOrEqualTo(21);
        age.Should().BeInRange(20, 22);
    }

    [Fact]
    public void DateAssertionExample()
    {
        var dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new(2000, 6, 9));
        dateOfBirth.Should().BeInRange(new(2000, 6, 8), new(2000, 6, 10));
        dateOfBirth.Should().BeGreaterThan(new(2000, 6, 8));
    }
}
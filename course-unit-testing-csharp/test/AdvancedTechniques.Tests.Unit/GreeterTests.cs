using FluentAssertions;
using NSubstitute;
using Xunit;

namespace AdvancedTechniques.Tests.Unit;

public class GreeterTests
{
    private readonly Greeter _sut;
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public GreeterTests()
    {
        _sut = new Greeter(_dateTimeProvider);
    }

    [Fact]
    public void GenerateGreetMessage_ShouldSayGoodEvening_WhenItsEvening()
    {
        // Arrange
        _dateTimeProvider.DateTimeNow.Returns(new DateTime(2022, 1, 1, 18, 0, 0));

        // Act
        var result = _sut.GenerateGreetMessage();

        // Assert
        result.Should().Be("Good Evening");
    }
}
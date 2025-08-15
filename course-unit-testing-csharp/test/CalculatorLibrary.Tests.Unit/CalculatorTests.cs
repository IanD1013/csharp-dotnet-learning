using CalculatorLibrary;
using FluentAssertions;
using Xunit;

namespace CalculatorLibraryTests;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(-5, -4, -9)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        // Assert.Equal(expected, result);
        result.Should().Be(expected);
    }

    [Fact]
    public void ExceptionThrownAssertionExample()
    {
        var calculator = new Calculator();

        Action result = () => calculator.Divide(1, 0);

        result.Should()
            .Throw<DivideByZeroException>()
            .WithMessage("Attempted to divide by zero.");
    }
}
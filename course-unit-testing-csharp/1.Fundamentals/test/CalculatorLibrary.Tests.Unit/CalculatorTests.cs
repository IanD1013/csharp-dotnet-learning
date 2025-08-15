using CalculatorLibrary;
using Xunit;

namespace CalculatorLibraryTests;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(5, 4, 9, Skip = "This breaks in CI")]
    [InlineData(0, 0, 0)]
    [InlineData(-5, -4, -9)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact(Skip = "Not implemented yet")]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Subtract(9, 4);

        // Assert
        Assert.Equal(5, result);
    }
}
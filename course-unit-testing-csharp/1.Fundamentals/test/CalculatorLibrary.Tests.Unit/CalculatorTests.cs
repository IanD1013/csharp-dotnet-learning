using CalculatorLibrary;
using Xunit;
using Xunit.Abstractions;

namespace CalculatorLibraryTests;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(5, 4, 9)]
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
}
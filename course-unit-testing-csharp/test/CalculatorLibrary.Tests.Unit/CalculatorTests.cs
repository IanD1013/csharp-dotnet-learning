using System.Collections;
using CalculatorLibrary;
using FluentAssertions;
using Xunit;

namespace CalculatorLibraryTests;

public class CalculatorTests
{
    private readonly Calculator _sut = new();

    [Theory]
    [MemberData(nameof(Add_TestData))]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(
        int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(CalculatorSubtractTestData))]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTheNumbersAreValidIntegers(
        int firstNumber, int secondNumber, int expectedResult)
    {
        // Act
        var result = _sut.Subtract(firstNumber, secondNumber);

        // Assert
        result.Should().Be(expectedResult);
    }

    public static IEnumerable<object[]> Add_TestData =>
        new List<object[]>
        {
            new object[] { 1, 2, 3 },
            new object[] { 10, 20, 30 }
        };
}

public class CalculatorSubtractTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, 2, -1 };
        yield return new object[] { 10, 20, -10 };
        yield return new object[] { 100, 200, -100 };
        yield return new object[] { 1000, 2000, -1000 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
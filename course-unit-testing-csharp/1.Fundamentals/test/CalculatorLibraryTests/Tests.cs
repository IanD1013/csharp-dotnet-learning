using CalculatorLibrary;
using Xunit;

namespace CalculatorLibraryTests;

public class Tests
{
    [Fact]
    public void TestAdd()
    {
        var calculator = new Calculator();

        var result = calculator.Add(1, 2);

        Assert.Equal(3, result);
    }
}
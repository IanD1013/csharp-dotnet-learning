using CalculatorLibrary;
using Xunit;
using Xunit.Abstractions;

namespace CalculatorLibraryTests;

public class CalculatorTests : IDisposable
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _outputHelper;

    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("Hello from the ctor");
    }

    [Fact]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers()
    {
        // Act
        var result = _sut.Add(1, 2);

        // Assert
        Assert.Equal(3, result);

        _outputHelper.WriteLine("Hello from the test itself");
    }

    public void Dispose()
    {
        _outputHelper.WriteLine("Hello from cleanup");
    }
}
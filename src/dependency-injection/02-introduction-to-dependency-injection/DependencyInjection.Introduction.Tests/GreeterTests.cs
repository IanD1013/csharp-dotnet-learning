using DependencyInjection.Introduction.Demos.Clock;

namespace DependencyInjection.Introduction.Tests;

/// <summary>
/// Lesson 4 of the notes: with the clock behind <see cref="IDateTimeProvider"/>, every branch is
/// reachable from a test, at any time of day.
/// </summary>
public class GreeterTests
{
    private static readonly string[] AllGreetings = ["Good morning", "Good afternoon", "Good evening"];

    [Theory]
    [InlineData(5, "Good morning")]
    [InlineData(11, "Good morning")]
    [InlineData(12, "Good afternoon")]
    [InlineData(17, "Good afternoon")]
    [InlineData(18, "Good evening")]
    [InlineData(4, "Good evening")]
    public void CreateGreetMessage_ShouldMatchTimeOfDay_WhenClockIsInjected(int hour, string expected)
    {
        // Arrange
        var dateTimeProvider = new FixedDateTimeProvider(new DateTime(2026, 9, 19, hour, 0, 0));
        var greeter = new Greeter(dateTimeProvider);

        // Act
        var message = greeter.CreateGreetMessage();

        // Assert
        Assert.Equal(expected, message);
    }

    /// <summary>
    /// The same class without the injected clock. The only assertion available is "it is one of
    /// the three", because which one comes back depends on when the suite happens to run.
    /// </summary>
    [Fact]
    public void CreateGreetMessage_CannotBePinnedToATimeOfDay_WhenClockIsHardWired()
    {
        // Arrange
        var greeter = new TightlyCoupledGreeter();

        // Act
        var message = greeter.CreateGreetMessage();

        // Assert
        Assert.Contains(message, AllGreetings);
    }
}

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

    [Fact]
    public void ObjectAssertionExample()
    {
        var expected = new User
        {
            FullName = "Ian Dong",
            Age = 21,
            DateOfBirth = new(2000, 6, 9)
        };

        var user = _sut.AppUser;
        user.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void EnumerableObjectsAssertionExample()
    {
        var expected = new User
        {
            FullName = "Ian Dong",
            Age = 21,
            DateOfBirth = new(2000, 6, 9)
        };

        var users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);
        users.Should().HaveCount(3);
        users.Should().Contain(x => x.FullName.StartsWith("Ian") && x.Age > 5);
    }

    [Fact]
    public void EnumerableNumbersAssertionExample()
    {
        var numbers = _sut.Numbers.As<int[]>();
        numbers.Should().Contain(1);
    }

    [Fact]
    public void EventRaisedAssertionExample()
    {
        var monitorSubject = _sut.Monitor();

        _sut.RaiseExampleEvent();

        monitorSubject.Should().Raise(nameof(_sut.ExampleEvent));
    }

    [Fact]
    public void TestingInternalMembersExample()
    {
        var number = _sut.InternalSecretNumber;

        number.Should().Be(42);
    }
}
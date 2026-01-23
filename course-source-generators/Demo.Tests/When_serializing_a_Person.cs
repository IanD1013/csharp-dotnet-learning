// ReSharper disable InconsistentNaming

using Demo.Domain.Entities;
using Xunit;

namespace Demo.Tests;

public class When_serializing_a_Person
{
    private const string expected =
        """
        {
          "EmailAddress": "john.smith@mailinator.com",
          "FirstName": "John",
          "LastName": "Smith",
          "PhoneNumber": "1234567890"
        }
        """;

    private static readonly Person Person = new()
    {
        EmailAddress = "john.smith@mailinator.com",
        FirstName = "John",
        LastName = "Smith",
        PhoneNumber = "1234567890"
    };

    [Fact]
    public void Then_ToJson_returns_expected()
    {
        Assert.Equal(expected, Person.ToJson());
    }

    [Fact]
    public void Then_ToJsonViaReflection_returns_expected()
    {
        Assert.Equal(expected, Person.ToJsonViaReflection());
    }
    
    [Fact]
    public void Then_ToJsonViaSystemText_returns_expected()
    {
        Assert.Equal(expected, Person.ToJsonViaSystemText());
    }
    
    [Fact]
    public void Then_ToJsonViaSystemTextGenerated_returns_expected()
    {
        Assert.Equal(expected, Person.ToJsonViaSystemTextGenerated());
    }
}
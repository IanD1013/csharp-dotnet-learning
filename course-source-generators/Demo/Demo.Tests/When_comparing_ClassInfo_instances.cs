// ReSharper disable InconsistentNaming

using Demo.Generator;
using Microsoft.CodeAnalysis;

namespace Demo.Tests;

public class When_comparing_ClassInfo_instances
{
    private readonly ClassInfo classInfo1 = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "MyNamespace",
        Name = "Name"
    };

    private readonly ClassInfo classInfo2 = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "MyNamespace",
        Name = "Name"
    };

    private readonly ClassInfo classInfo_different_name = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "Test",
        Name = "Name"
    };    
    
    private readonly ClassInfo classInfo_different_namespace = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "OtherNamespace",
        Name = "Name"
    };
    
    private readonly ClassInfo classInfo_different_accessibility = new()
    {
        Accessibility = Accessibility.Internal,
        Namespace = "MyNamespace",
        Name = "Name"
    };

    [Fact]
    public void Then_identical_values_are_equal()
    {
        Assert.Equal(classInfo1, classInfo2);
    }

    [Fact]
    public void Then_different_namespaces_are_not_equal()
    {
        Assert.NotEqual(classInfo1, classInfo_different_namespace);
    }
    
    [Fact]
    public void Then_different_names_are_not_equal()
    {
        Assert.NotEqual(classInfo1, classInfo_different_name);
    }
    
    [Fact]
    public void Then_different_accessibilities_are_not_equal()
    {
        Assert.NotEqual(classInfo1, classInfo_different_accessibility);
    }   
}
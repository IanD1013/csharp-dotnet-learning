// ReSharper disable InconsistentNaming

using Demo.Generator;
using Microsoft.CodeAnalysis;

namespace Demo.Tests;

public class When_comparing_ClassInfo_instances
{
    private readonly ClassInfo classInfo1 = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "Namespace",
        Name = "Name",
        Properties = 
        [
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property1" },
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property2" },
        ]
    };
    
    private readonly ClassInfo classInfo2 = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "Namespace",
        Name = "Name",
        Properties = 
        [
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property1" },
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property2" },
        ]
    };
    
    private readonly ClassInfo classInfo_different_name = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "Test",
        Name = "OtherName",
        Properties = 
        [
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property1" },
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property2" },
        ]
    };

    private readonly ClassInfo classInfo_different_namespace = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "OtherNamespace",
        Name = "Name",
        Properties = 
        [
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property1" },
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property2" },
        ]
    };
    
    private readonly ClassInfo classInfo_different_accessibility = new()
    {
        Accessibility = Accessibility.Internal,
        Namespace = "Namespace",
        Name = "Name",
        Properties = 
        [
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property1" },
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property2" },
        ]
    };
    
    private readonly ClassInfo classInfo_different_properties = new()
    {
        Accessibility = Accessibility.Public,
        Namespace = "Namespace",
        Name = "Name",
        Properties = 
        [
            new PropertyInfo{Accessibility = Accessibility.Public, Name = "Property1" },
        ]
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
    
    [Fact]
    public void Then_different_properties_are_not_equal()
    {
        Assert.NotEqual(classInfo1, classInfo_different_properties);
    }       
}
// ReSharper disable InconsistentNaming

using Demo.Generator;

namespace Demo.Tests;

public class When_comparing_ClassInfo_instances
{
    private readonly ClassInfo classInfo1 = new()
    {
        Namespace = "MyNamespace",
        Name = "Name"
    };

    private readonly ClassInfo classInfo2 = new()
    {
        Namespace = "MyNamespace",
        Name = "Name"
    };

    private readonly ClassInfo classInfo_different_name = new()
    {
        Namespace = "Test",
        Name = "Name"
    };    
    
    private readonly ClassInfo classInfo_different_namespace = new()
    {
        Namespace = "OtherNamespace",
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
}
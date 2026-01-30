using Demo.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Demo.Tests;

public class When_generating_a_ToJson_method
{
    private const string input =
        """
        using Demo.Generator;

        namespace Demo.Domain.Entities;

        [ToJsonSerializer]
        internal partial class Address
        {
            public required string AddressLine1 { get; init; }
            public required string AddressLine2 { get; init; }
            public required string City { get; init; }
            public required string State { get; init; }
            public required string PostalCode { get; init; }
            
            public int Foo()
            {
                return 5;
            }
        }
        """;

    private const string expectedOutput =
        """
        using System.Text;

        namespace Demo.Domain.Entities;

        internal partial class Address
        {
            public string ToJson()
            {
                var builder = new StringBuilder();
            
                builder.AppendLine("{");
                builder.AppendLine($"  \"AddressLine1\": \"{AddressLine1}\",");
                builder.AppendLine($"  \"AddressLine2\": \"{AddressLine2}\",");
                builder.AppendLine($"  \"City\": \"{City}\",");
                builder.AppendLine($"  \"PostalCode\": \"{PostalCode}\",");
                builder.AppendLine($"  \"State\": \"{State}\"");
                builder.Append('}');
            
                return builder.ToString();
            }
        }
        """;

    [Fact]
    public void Then_the_expected_output_is_generated()
    {
        var generator = new ToJsonGenerator();
        var syntaxTree = CSharpSyntaxTree.ParseText(input);
        var compilation = CSharpCompilation.Create(
            nameof(Then_the_expected_output_is_generated), // assembly name, for test like this it's arbitrary 
            [syntaxTree],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]);
        var driver = CSharpGeneratorDriver.Create(generator)
            .RunGenerators(compilation);
        var result = driver.GetRunResult();
        var output = result.GeneratedTrees.Single(t => t.FilePath.EndsWith("Address.g.cs"))
            .ToString();
        var actual = output[output.IndexOf("using", StringComparison.Ordinal)..];
        Assert.Equal(expectedOutput, actual);
    }
}
using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Demo.Generator;

[Generator]
public class ToJsonGenerator : IIncrementalGenerator
{
    private const string ToJsonSerializerAttribute =
        """
        using System;

        namespace Demo.Generator;

        [AttributeUsage(AttributeTargets.Class)]
        public class ToJsonSerializerAttribute : Attribute
        {
        }
        """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx
            .AddSource("ToJsonSerializerAttribute.g.cs", SourceText.From(ToJsonSerializerAttribute, Encoding.UTF8)));
    }
}
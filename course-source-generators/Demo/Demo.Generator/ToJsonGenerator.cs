using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        var provider = context.SyntaxProvider.CreateSyntaxProvider((node, token) => node is ClassDeclarationSyntax
        {
            AttributeLists.Count: > 0
        });
    }
}
using System;
using System.Text;
using System.Threading;
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

        var provider = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform);
    }

    private static ClassInfo? Transform(GeneratorSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)syntaxContext.Node;

        foreach (var attributeList in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeList.Attributes)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }
                
                var attributeName = attributeSyntax.Name.ToString();
                if (attributeName != "ToJsonSerializerAttribute" && attributeName != "ToJsonSerializer")
                {
                    continue;
                }
            }
        }
        
        return new ClassInfo();
    }

    private static bool Predicate(SyntaxNode node, CancellationToken token)
    {
        return node is ClassDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };
    }
}

public record struct ClassInfo  
{
}
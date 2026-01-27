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

        context.RegisterSourceOutput(provider, (productionContext, info) => throw new NotImplementedException());
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

                // Now that we've done some initial filtering, we can transform our class declaration syntax node into its equivalent semantic model for a more in-depth analysis.
                var attributeSymbolInfo = syntaxContext.SemanticModel.GetSymbolInfo(attributeSyntax);

                if (attributeSymbolInfo.Symbol is not IMethodSymbol methodSymbol)
                {
                    continue;
                }

                var attributeSymbol = methodSymbol.ContainingType;

                if (attributeSymbol.ToDisplayString() != "Demo.Generator.ToJsonSerializerAttribute" &&
                    attributeSymbol.ToDisplayString() != "Demo.Generator.ToJsonSerializer")
                {
                    continue;
                }

                var classSymbol = syntaxContext.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);

                var classInfo = new ClassInfo
                {
                    Namespace = classSymbol?.ContainingNamespace.ToDisplayString(),
                    Name = classSymbol?.Name
                };

                return classInfo;
            }
        }

        return null;
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
    public string? Namespace { get; set; }
    public string? Name { get; set; }
}
using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Demo.Generator;

[Generator]
public class ToJsonGenerator : IIncrementalGenerator
{
    private const string HelloWorld =
        """
        namespace Demo.Generator;

        public class HelloWorld 
        { 
        }
        """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx
            .AddSource("HelloWorld.g.cs", SourceText.From(HelloWorld, Encoding.UTF8)));
    }
}
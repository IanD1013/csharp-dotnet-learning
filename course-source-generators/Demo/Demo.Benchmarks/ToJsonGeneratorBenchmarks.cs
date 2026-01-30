using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Demo.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Demo.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ToJsonGeneratorBenchmarks
{
    private string _addressInput = null!;
    private string _entitiesInput = null!;

    [GlobalSetup]
    public void Setup()
    {
        _addressInput = GetResourceAsString("Address.cs");
        _entitiesInput = GetResourceAsString("Entities.cs");
    }

    [Benchmark]
    public GeneratorDriverRunResult Then_the_expected_output_is_generated()
    {
        var generator = new ToJsonGenerator();
        var syntaxTree = CSharpSyntaxTree.ParseText(_addressInput);
        var compilation = CSharpCompilation.Create(
            nameof(Then_the_expected_output_is_generated),
            [syntaxTree],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]);
        var driver = CSharpGeneratorDriver.Create(generator)
            .RunGenerators(compilation);
        var result = driver.GetRunResult();

        return result;
    }

    [Benchmark]
    public GeneratorDriverRunResult EntitiesBenchmarks()
    {
        var generator = new ToJsonGenerator();
        var syntaxTree = CSharpSyntaxTree.ParseText(_entitiesInput);
        var compilation = CSharpCompilation.Create(
            nameof(EntitiesBenchmarks),
            [syntaxTree],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]);
        var driver = CSharpGeneratorDriver.Create(generator)
            .RunGenerators(compilation);
        var result = driver.GetRunResult();

        return result;
    }

    private string GetResourceAsString(string resourceName)
    {
        var assembly = typeof(ToJsonGeneratorBenchmarks).Assembly;
        var manifestResourceNames = assembly.GetManifestResourceNames();
        resourceName = manifestResourceNames.Single(x =>
            x.Equals($"Demo.Benchmarks.{resourceName}", StringComparison.OrdinalIgnoreCase));

        using var stream = assembly.GetManifestResourceStream(resourceName) ??
                           throw new InvalidOperationException($"Resource '{resourceName}' not found.");
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}
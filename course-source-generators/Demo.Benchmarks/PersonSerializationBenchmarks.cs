using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Demo.Domain.Entities;

namespace Demo.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class PersonSerializationBenchmarks
{
    private static readonly Person Person = new()
    {
        EmailAddress = "john.smith@mailinator.com",
        FirstName = "John",
        LastName = "Smith",
        PhoneNumber = "1234567890"
    };

    [Benchmark]
    public string HandWritten()
    {
        return Person.ToJson();
    }

    [Benchmark]
    public string Reflection()
    {
        return Person.ToJsonViaReflection();
    }

    [Benchmark]
    public string SystemText()
    {
        return Person.ToJsonViaSystemText();
    }

    [Benchmark]
    public string SystemTextGenerated()
    {
        return Person.ToJsonViaSystemTextGenerated();
    }
}
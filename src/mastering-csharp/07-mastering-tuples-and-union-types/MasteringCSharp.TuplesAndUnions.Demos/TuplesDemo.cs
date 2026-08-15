using System.Reflection;
using System.Runtime.CompilerServices;
using EndpointAlias = (string host, int port);

namespace MasteringCSharp.TuplesAndUnions.Demos;

/// <summary>
/// Section 2 of the notes: what tuple syntax actually gives you.
/// Lesson: "Tuples in C#".
/// </summary>
public static class TuplesDemo
{
    public static void Run()
    {
        NamesAndMutability();
        Console.WriteLine();
        NamesAreNotPartOfTheType();
        Console.WriteLine();
        NamesInMetadata();
        Console.WriteLine();
        Aliases();
        Console.WriteLine();
        EqualityAndHashing();
        Console.WriteLine();
        HashingBeatsANaiveStruct();
        Console.WriteLine();
        DeconstructionAndPatterns();
        Console.WriteLine();
        CompositeKeys();
        Console.WriteLine();
        TuplesAsAnImplementationDetail();
    }

    private static void NamesAndMutability()
    {
        Console.WriteLine("-- Name inference, and tuples are mutable --");

        string host = "api.example.com";
        int port = 443;

        // `port` gets its name inferred from the variable; `name` is given explicitly.
        var endpoint = (name: host, port);
        Console.WriteLine($"  inferred: endpoint.port = {endpoint.port}, explicit: endpoint.name = {endpoint.name}");

        // A ValueTuple is a struct of public fields, so nothing stops you writing to it.
        endpoint.port++;
        endpoint.name = string.Empty;
        Console.WriteLine($"  after endpoint.port++ and endpoint.name = \"\": ({endpoint.name}, {endpoint.port})");
        Console.WriteLine("  There is no readonly tuple. Immutability is one reason to reach for a record struct.");
    }

    private static void NamesAreNotPartOfTheType()
    {
        Console.WriteLine("-- Names are erased at compile time --");

        var endpoint = (host: "api.example.com", port: 443);

        // Assigning across different element names compiles: the type is (string, int).
        (string addr, int portNum) renamed = endpoint;
        Console.WriteLine($"  renamed.addr = {renamed.addr}, renamed.portNum = {renamed.portNum}");

        // The names never existed at runtime. Item1/Item2 are the real fields.
        Console.WriteLine($"  endpoint.Item1 = {endpoint.Item1}, endpoint.Item2 = {endpoint.Item2}");
        Console.WriteLine($"  endpoint.GetType() = {endpoint.GetType()}");
        Console.WriteLine("  Note the runtime type carries no trace of `host` or `port`.");
    }

    /// <summary>
    /// The lesson says the compiler emits metadata so a consumer in another assembly still
    /// sees the names. That metadata is a TupleElementNamesAttribute, and it can be read back.
    /// </summary>
    private static void NamesInMetadata()
    {
        Console.WriteLine("-- Where the names actually go: TupleElementNamesAttribute --");

        var parsed = TupleMetadataDemo.ParseEndpoint("api.example.com:444");
        Console.WriteLine($"  ParseEndpoint(\"api.example.com:444\") = {parsed.host}:{parsed.port}");

        MethodInfo parseEndpoint = typeof(TupleMetadataDemo).GetMethod(nameof(TupleMetadataDemo.ParseEndpoint))!;
        var names = parseEndpoint.ReturnParameter.GetCustomAttribute<TupleElementNamesAttribute>();
        Console.WriteLine($"  return type in metadata      = {parseEndpoint.ReturnType}");
        Console.WriteLine($"  TupleElementNamesAttribute   = [{string.Join(", ", names!.TransformNames)}]");

        // The alias-declared method has the same signature; the alias name is not preserved.
        MethodInfo parseGlobal = typeof(TupleMetadataDemo).GetMethod(nameof(TupleMetadataDemo.ParseGlobalEndpoint))!;
        var globalNames = parseGlobal.ReturnParameter.GetCustomAttribute<TupleElementNamesAttribute>();
        Console.WriteLine($"  ParseGlobalEndpoint metadata = [{string.Join(", ", globalNames!.TransformNames)}]");
        Console.WriteLine("  Element names survive into metadata. The alias name `GlobalEndpoint` does not.");
    }

    private static void Aliases()
    {
        Console.WriteLine("-- Aliases: naming a shape without creating a type --");

        EndpointAlias local = ("api.example.com", 443);      // file-scoped alias
        GlobalEndpoint global = ("admin.example.com", 443);  // assembly-wide alias, see GlobalUsings.cs

        Console.WriteLine($"  EndpointAlias  = {local}");
        Console.WriteLine($"  GlobalEndpoint = {global}");
        Console.WriteLine($"  Same runtime type? {local.GetType() == global.GetType()}");
        Console.WriteLine("  Both are ValueTuple<string, int>. An alias renames, it does not create a type.");
    }

    private static void EqualityAndHashing()
    {
        Console.WriteLine("-- Structural equality, for free --");

        var p1 = (x: 1, y: 2);
        var p2 = (x: 1, y: 3);
        Console.WriteLine($"  (1,2) == (1,3) : {p1 == p2}");

        p2.y = p1.y;
        Console.WriteLine($"  (1,2) == (1,2) : {p1 == p2}");
        Console.WriteLine($"  hash codes equal: {p1.GetHashCode() == p2.GetHashCode()}");

        // Equality ignores names, exactly as assignment does.
        var differentNames = (a: 1, b: 2);
        Console.WriteLine($"  (x:1,y:2) == (a:1,b:2) : {p1 == differentNames}   <- names are not compared");

        Console.WriteLine($"  ToString() = {p1}   <- and the names are gone here too");
    }

    /// <summary>
    /// Not in the lesson, but it is what makes the composite-key section safe.
    /// Chapter 6 showed that a struct holding a reference type inherits a GetHashCode that
    /// uses the first field only. A ValueTuple of the same two fields is not affected,
    /// because it declares its own GetHashCode over every element.
    /// </summary>
    private static void HashingBeatsANaiveStruct()
    {
        Console.WriteLine("-- Why a tuple key is safe where a hand-written struct key is not --");

        Console.WriteLine("  ValueTuple<string, int>, first field held constant:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"    (\"\", {i}).GetHashCode() = {("", i).GetHashCode()}");
        }

        Console.WriteLine("  The same two fields as a plain struct with no GetHashCode of its own:");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"    new NaiveEndpointStruct(\"\", {i}).GetHashCode() = {new NaiveEndpointStruct("", i).GetHashCode()}");
        }

        var declaring = typeof(ValueTuple<string, int>).GetMethod(nameof(GetHashCode), Type.EmptyTypes)!.DeclaringType;
        Console.WriteLine($"  ValueTuple<string,int>.GetHashCode is declared by {declaring}");
        Console.WriteLine("  It overrides GetHashCode itself, so it never reaches the runtime's first-field fallback.");
    }

    private static void DeconstructionAndPatterns()
    {
        Console.WriteLine("-- Deconstruction, discards, and tuple patterns --");

        var endpoint = (host: "api.example.com", port: 443);

        (string host, int port) = endpoint;
        Console.WriteLine($"  deconstructed: {host}:{port}");

        var (_, onlyPort) = endpoint;
        Console.WriteLine($"  discarded the host, kept {onlyPort}");

        string description = (host, port) switch
        {
            ("api.example.com", 443) => "production HTTPS endpoint",
            (_, 443) => "HTTPS endpoint",
            _ => "other endpoint",
        };
        Console.WriteLine($"  tuple pattern  : {description}");

        // The swap idiom. The compiler emits the same hidden temporary you would write by hand.
        int left = 1;
        int right = 2;
        (right, left) = (left, right);
        Console.WriteLine($"  after (right, left) = (left, right): left = {left}, right = {right}");
    }

    private static void CompositeKeys()
    {
        Console.WriteLine("-- Composite keys: the payoff of structural equality --");

        var routes = new Dictionary<EndpointAlias, string>
        {
            [("api.example.com", 443)] = "production",
            [("api.example.com", 8443)] = "staging",
            [("admin.example.com", 443)] = "admin",
        };

        Console.WriteLine($"  routes[(\"api.example.com\", 443)]  = {routes[("api.example.com", 443)]}");
        Console.WriteLine($"  routes[(\"api.example.com\", 8443)] = {routes[("api.example.com", 8443)]}");

        var allowed = new HashSet<(string Host, int Port)>
        {
            ("api.example.com", 443),
            ("admin.example.com", 443),
        };

        Console.WriteLine($"  allowed.Contains((\"api.example.com\", 443))  = {allowed.Contains(("api.example.com", 443))}");
        Console.WriteLine($"  allowed.Contains((\"api.example.com\", 8443)) = {allowed.Contains(("api.example.com", 8443))}");
        Console.WriteLine("  A tuple key hashes all of its elements, unlike a hand-written struct key.");
    }

    private static void TuplesAsAnImplementationDetail()
    {
        Console.WriteLine("-- Tuples inside a type, not on its surface --");

        var l1 = new Location("/etc/hosts", 42);
        var l2 = new Location("/etc/hosts", 42);
        var l3 = new Location("/etc/hosts", 43);

        Console.WriteLine($"  l1 = {l1}, l2 = {l2}, l3 = {l3}");
        Console.WriteLine($"  l1 == l2 : {l1 == l2}   (Equals is one tuple comparison)");
        Console.WriteLine($"  l1 == l3 : {l1 == l3}");
        Console.WriteLine($"  hash(l1) == hash(l2) : {l1.GetHashCode() == l2.GetHashCode()}");
        Console.WriteLine("  Constructor, Equals and GetHashCode are each a single tuple expression.");
        Console.WriteLine("  The public API is still Path and Position: the tuple never leaks out.");
    }
}

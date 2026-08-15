namespace MasteringCSharp.TuplesAndUnions.Demos;

/// <summary>
/// Section 3 of the notes: the signal that a tuple has outgrown its job.
/// Lesson: "Knowing the Limits: When to Move to Actual Types".
/// </summary>
public static class LimitsDemo
{
    public static void Run()
    {
        TheExtensionLeak();
        Console.WriteLine();
        TheNominalFix();
    }

    /// <summary>
    /// ToEndpointString is declared on (string host, int port). Element names are not part
    /// of the type, so it attaches to every (string, int) tuple in the assembly.
    /// </summary>
    private static void TheExtensionLeak()
    {
        Console.WriteLine("-- An extension method cannot be aimed at a tuple's meaning --");

        var endpoint = (host: "api.example.com", port: 443);
        Console.WriteLine($"  endpoint.ToString()         = {endpoint}");
        Console.WriteLine($"  endpoint.ToEndpointString() = {endpoint.ToEndpointString()}");

        // Nothing about this tuple is an endpoint. It is (string, int), so it qualifies.
        var retryPolicy = (name: "attempts", count: 3);
        Console.WriteLine($"  retryPolicy.ToEndpointString() = {retryPolicy.ToEndpointString()}   <- offered on an unrelated tuple");
        Console.WriteLine("  The compiler has no way to tell the two apart: both are ValueTuple<string, int>.");
    }

    private static void TheNominalFix()
    {
        Console.WriteLine("-- A record struct gives the shape an identity --");

        var typed = new Endpoint("api.example.com", 443);
        Console.WriteLine($"  new Endpoint(\"api.example.com\", 443) = {typed}");
        Console.WriteLine($"  runtime type = {typed.GetType().Name}");

        // Endpoint is a distinct type, so ToEndpointString is not in scope for it and its
        // own ToString cannot appear on unrelated pairs. Uncommenting the next line fails
        // to compile with CS1929, which is the whole point of the section:
        //   typed.ToEndpointString();

        var alsoTyped = new Endpoint("api.example.com", 443);
        Console.WriteLine($"  value equality still works: {typed == alsoTyped}");
        Console.WriteLine("  Same cost as the tuple, no heap allocation, and the logic stays where it belongs.");
    }
}

using System.Globalization;

namespace MasteringCSharp.TuplesAndUnions.Demos;

/// <summary>
/// Lesson "Tuples in C#": a method returning a named tuple. The names live in metadata
/// as a TupleElementNamesAttribute on the return parameter, which is how a consumer in
/// another assembly still sees `host` and `port`. TuplesDemo reads that attribute back.
/// </summary>
public static class TupleMetadataDemo
{
    public static (string host, int port) ParseEndpoint(string endpoint)
    {
        int separatorIndex = endpoint.LastIndexOf(':');

        return (
            endpoint[..separatorIndex],
            int.Parse(endpoint[(separatorIndex + 1)..], CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Same shape, declared through the global alias. The alias name is not preserved:
    /// this method and <see cref="ParseEndpoint"/> have the identical signature in metadata.
    /// </summary>
    public static GlobalEndpoint ParseGlobalEndpoint(string endpoint)
    {
        int separatorIndex = endpoint.LastIndexOf(':');

        return (
            endpoint[..separatorIndex],
            int.Parse(endpoint[(separatorIndex + 1)..], CultureInfo.InvariantCulture));
    }
}

/// <summary>
/// Lesson "Knowing the Limits": an extension method written for host/port pairs.
/// It targets ValueTuple&lt;string, int&gt;, so it lands on every tuple of that shape
/// no matter what the element names say the tuple means.
/// </summary>
public static class EndpointTupleExtensions
{
    public static string ToEndpointString(this (string host, int port) endpoint) =>
        $"{endpoint.host}:{endpoint.port}";
}

/// <summary>
/// Lesson "Knowing the Limits": the nominal replacement. A distinct type, so the
/// extension above cannot reach it and its own members cannot leak elsewhere.
/// </summary>
public readonly record struct Endpoint(string Host, int Port)
{
    public override string ToString() =>
        $"{Host}:{Port}";
}

/// <summary>
/// Not from a lesson. The same two fields as a ValueTuple&lt;string, int&gt;, written as a
/// plain struct with no GetHashCode of its own, so it falls back to the runtime's
/// non-blittable path from chapter 6 and hashes the first field only.
/// Exists purely as the control for the comparison in TuplesDemo.
/// </summary>
public readonly struct NaiveEndpointStruct
{
    public NaiveEndpointStruct(string host, int port) =>
        (Host, Port) = (host, port);

    public string Host { get; }

    public int Port { get; }
}

/// <summary>
/// Lesson "Tuples in C#": tuples used as an implementation detail rather than as the API.
/// The constructor, Equals and GetHashCode each collapse into one tuple expression,
/// and the tuple never appears in the public surface.
/// </summary>
public readonly struct Location : IEquatable<Location>
{
    public Location(string path, int position) =>
        (Path, Position) = (path, position);

    public string Path { get; }

    public int Position { get; }

    public bool Equals(Location other) =>
        (Path, Position) == (other.Path, other.Position);

    public override bool Equals(object? obj) =>
        obj is Location other && Equals(other);

    // Similar to HashCode.Combine, but tuples work on .NET Framework as well,
    // where HashCode is not available.
    public override int GetHashCode() =>
        (Path?.GetHashCode(StringComparison.Ordinal) ?? 0, Position).GetHashCode();

    public override string ToString() =>
        $"{Path}:{Position}";

    public static bool operator ==(Location left, Location right) =>
        left.Equals(right);

    public static bool operator !=(Location left, Location right) =>
        !left.Equals(right);
}

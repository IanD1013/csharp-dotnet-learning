namespace MasteringCSharp.Records.Benchmarks;

/// <summary>The compiler-generated version: typed Equals, all-field GetHashCode, no boxing.</summary>
public readonly record struct LocationRecordStruct(string Path, int Position);

/// <summary>
/// The default version. Path is a reference type, so the struct is non-blittable and
/// the runtime's fallback GetHashCode uses the first field only.
/// </summary>
public readonly struct LocationDefaultStruct
{
    public string Path { get; }
    public int Position { get; }

    public LocationDefaultStruct(string path, int position) =>
        (Path, Position) = (path, position);
}

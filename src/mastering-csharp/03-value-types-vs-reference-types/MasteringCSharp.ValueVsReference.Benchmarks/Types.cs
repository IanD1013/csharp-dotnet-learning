namespace MasteringCSharp.ValueVsReference.Benchmarks;

/// <summary>
/// Value type: two ints stored inline, no header.
/// </summary>
public struct Point(int x, int y)
{
    public int X { get; } = x;

    public int Y { get; } = y;
}

/// <summary>
/// Reference type: the same two ints behind an indirection, plus 16 bytes of
/// object header and method table pointer.
/// </summary>
public class PointRef(int x, int y)
{
    public int X { get; } = x;

    public int Y { get; } = y;
}

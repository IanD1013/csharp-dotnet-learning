namespace MasteringCSharp.ValueVsReference.Demos;

/// <summary>
/// The value type half of every comparison in this chapter: two ints, stored inline.
/// </summary>
public struct Point
{
    public int X { get; set; }

    public int Y { get; set; }
}

/// <summary>
/// The reference type half: identical fields, but reached through an indirection
/// and carrying an object header plus a method table pointer.
/// </summary>
public class PointRef
{
    public int X { get; set; }

    public int Y { get; set; }
}

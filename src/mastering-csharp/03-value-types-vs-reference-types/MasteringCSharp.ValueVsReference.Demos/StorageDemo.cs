using ObjectLayoutInspector;

namespace MasteringCSharp.ValueVsReference.Demos;

/// <summary>
/// Storage semantics: where the data lives and how it is laid out.
/// Prints the real CLR layout instead of asking you to take the diagrams on faith.
/// </summary>
public static class StorageDemo
{
    public static void Run()
    {
        Console.WriteLine("A struct carries no CLR bookkeeping at all.");
        TypeLayout.PrintLayout<Point>();

        Console.WriteLine();
        Console.WriteLine("A class adds an object header and a method table pointer");
        Console.WriteLine("in front of the very same two ints, so 8 bytes of data cost 24.");
        TypeLayout.PrintLayout<PointRef>();

        Console.WriteLine();
        Console.WriteLine("An array of structs stores the values inline: one allocation total.");
        ArrayLayout.PrintLayout(BuildPoints(10));

        Console.WriteLine();
        Console.WriteLine("An array of classes stores 8-byte references instead. The array");
        Console.WriteLine("object below is the same size, but 10 more objects live off to the side.");
        ArrayLayout.PrintLayout(BuildPointRefs(10));

        Console.WriteLine();
        PrintAllocationMath(10);
    }

    /// <summary>
    /// One allocation: the values are written straight into the array's memory block.
    /// </summary>
    private static Point[] BuildPoints(int size)
    {
        var array = new Point[size];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new Point { X = i, Y = i };
        }

        return array;
    }

    /// <summary>
    /// size + 1 allocations: one for the array, one per element on the heap.
    /// </summary>
    private static PointRef[] BuildPointRefs(int size)
    {
        var array = new PointRef[size];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new PointRef { X = i, Y = i };
        }

        return array;
    }

    /// <summary>
    /// Restates the layout dump as the allocation arithmetic from the lesson.
    /// </summary>
    private static void PrintAllocationMath(int size)
    {
        const int ArrayOverhead = 24;   // 16 header + 4 length + 4 padding
        const int ReferenceSize = 8;
        const int InstanceSize = 24;    // 16 header + 4 X + 4 Y
        const int PointSize = 8;        // 4 X + 4 Y, no header

        int pointArray = ArrayOverhead + (size * PointSize);
        int refArray = ArrayOverhead + (size * ReferenceSize);
        int refTotal = refArray + (size * InstanceSize);

        Console.WriteLine($"Allocation math for {size} elements");
        Console.WriteLine($"  Point[{size}]    : 1 allocation,  {pointArray} bytes");
        Console.WriteLine($"  PointRef[{size}] : {size + 1} allocations, {refTotal} bytes " +
                          $"({refArray} array + {size} x {InstanceSize} instances)");
        Console.WriteLine($"  Reference types cost {refTotal - pointArray} extra bytes to hold the same data.");
    }
}

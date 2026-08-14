namespace MasteringCSharp.Records.Demos;

// Every type here is taken from a lesson in chapter 6 of Dometrain's "Mastering: C#".
// They are internal on purpose: CA1051 would otherwise object to the public fields on
// PersonKey, and those fields are copied verbatim from the lesson.

// ---------------------------------------------------------------------------
// Lesson 2/3: Manual Value-based Equality, Referential and Value-based Equality
// ---------------------------------------------------------------------------

/// <summary>The default: reference based equality.</summary>
internal sealed class PointClass(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
}

/// <summary>
/// Value-based equality implemented by hand: Equals, GetHashCode, ToString,
/// and the == / != operators all have to agree.
/// </summary>
internal sealed class PointValue : IEquatable<PointValue>
{
    public int X { get; }
    public int Y { get; }

    public PointValue(int x, int y)
        => (X, Y) = (x, y);

    public bool Equals(PointValue? other)
        => other is not null && X == other.X && Y == other.Y;

    public override bool Equals(object? obj)
        => Equals(obj as PointValue);

    public override int GetHashCode()
        => HashCode.Combine(X, Y);

    public override string ToString()
        => $"PointValue {{ X = {X}, Y = {Y} }}";

    public static bool operator ==(PointValue? left, PointValue? right)
        => left is null ? right is null : left.Equals(right);

    public static bool operator !=(PointValue? left, PointValue? right)
        => !(left == right);
}

/// <summary>
/// The same thing as <see cref="PointValue"/>, in one line.
/// The extra parameterless constructor is what makes object initializer syntax legal.
/// </summary>
internal record Point(int X, int Y)
{
    public Point()
        : this(0, 0)
    {
    }
}

/// <summary>
/// Exists only to show EqualityContract at work: a Point3D is never equal to a Point,
/// even when the base half of the data matches.
/// </summary>
internal sealed record Point3D(int X, int Y, int Z) : Point(X, Y);

// ---------------------------------------------------------------------------
// Lesson 5: Records Limitations
// ---------------------------------------------------------------------------

/// <summary>Default record equality compares the string case-sensitively.</summary>
internal sealed record LocationCaseSensitive(string Path, int Position);

/// <summary>
/// A wrapper that owns its own comparison rule, so the record composing it does not have
/// to hand-write Equals. The implicit operator keeps the call sites unchanged.
/// </summary>
internal readonly record struct NormalizedPath(string Path)
{
    private static readonly StringComparer PathComparer = StringComparer.OrdinalIgnoreCase;

    public static implicit operator NormalizedPath(string path) =>
        new(path);

    public bool Equals(NormalizedPath other) =>
        PathComparer.Equals(Path, other.Path);

    public override int GetHashCode() =>
        PathComparer.GetHashCode(Path);

    public override string ToString() =>
        Path;
}

/// <summary>The same record, with the comparison rule pushed into the property type.</summary>
internal sealed record Location(NormalizedPath Path, int Position);

/// <summary>
/// Records are shallowly immutable: the list reference cannot be reassigned,
/// but the list it points at is as mutable as ever.
/// </summary>
internal sealed record Basket(string Owner, List<string> Items);

// ---------------------------------------------------------------------------
// Lesson 6/7: Issues With the Default Structs Equality, Record Structs vs. Default Structs
// ---------------------------------------------------------------------------

/// <summary>
/// Non-blittable (it holds a string), so the runtime's default GetHashCode
/// falls back to using the first field only.
/// </summary>
internal readonly struct LocationDefaultStruct
{
    public string Path { get; }
    public int Position { get; }

    public LocationDefaultStruct(string path, int position) =>
        (Path, Position) = (path, position);
}

/// <summary>The same fields, declared in the opposite order. The hash code changes with them.</summary>
internal readonly struct LocationReorderedStruct
{
    public int Position { get; }
    public string Path { get; }

    public LocationReorderedStruct(string path, int position) =>
        (Path, Position) = (path, position);
}

/// <summary>The fix: the compiler generates typed, non-boxing, all-field equality.</summary>
internal readonly record struct LocationRecordStruct(string Path, int Position);

/// <summary>Blittable (no reference fields), so the default hash uses every field.</summary>
internal readonly struct BlittablePoint
{
    public int X { get; }
    public int Y { get; }

    public BlittablePoint(int x, int y) =>
        (X, Y) = (x, y);
}

/// <summary>The lesson's public-field version of the same trap.</summary>
internal struct PersonKey
{
    public string Name;
    public int Age;

    public PersonKey(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

/// <summary>Same data, correct hashing.</summary>
internal record struct PersonRecord(string Name, int Age);

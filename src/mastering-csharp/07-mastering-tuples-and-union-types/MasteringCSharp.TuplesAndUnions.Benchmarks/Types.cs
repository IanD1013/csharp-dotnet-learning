using System.Runtime.CompilerServices;

namespace MasteringCSharp.TuplesAndUnions.Benchmarks;

// Only the types the benchmarks actually measure. The fuller set, with the [Union]
// polyfill and the class-based variant, lives in the Demos project.

/// <summary>
/// Hand-written transcription of the struct the C# 15 compiler generates for
/// <c>union LookupKey(int Id, string Name)</c>. One <c>object?</c> field, so the int case
/// is boxed on construction. See the notes for why this is hand-written.
/// </summary>
public readonly struct GeneratedLookupKey
{
    public GeneratedLookupKey(int value) => Value = value;

    public GeneratedLookupKey(string value) => Value = value;

    public object? Value { get; }

    public static implicit operator GeneratedLookupKey(int value) => new(value);

    public static implicit operator GeneratedLookupKey(string value) => new(value);
}

/// <summary>
/// The deep-dive lesson's alternative: nullable fields plus TryGetValue accessors, which
/// is the shape the compiler pattern-matches against instead of reading the boxed Value.
/// </summary>
public readonly record struct ManualLookupKey
{
    private readonly int? _id;
    private readonly string? _name;

    public ManualLookupKey(int id)
    {
        _id = id;
        _name = null;
    }

    public ManualLookupKey(string name)
    {
        _id = default;
        _name = name;
    }

    public static implicit operator ManualLookupKey(int value) => new(value);

    public static implicit operator ManualLookupKey(string value) => new(value);

    public bool TryGetValue(out int value)
    {
        value = _id.GetValueOrDefault();
        return _id is not null;
    }

    public bool TryGetValue(out string? value)
    {
        value = _name;
        return _name is not null;
    }
}

/// <summary>An entry the lookups return.</summary>
public sealed record Entry(int Id, string Name);

/// <summary>The same two dictionaries, reached by a raw key or through either union shape.</summary>
public sealed class Entries
{
    private readonly Dictionary<int, Entry> _byId = new()
    {
        [42] = new Entry(42, "Launch Plan"),
        [1001] = new Entry(1001, "Archive Policy"),
    };

    private readonly Dictionary<string, Entry> _byName;

    public Entries() =>
        _byName = new Dictionary<string, Entry>(StringComparer.Ordinal)
        {
            ["Launch Plan"] = _byId[42],
            ["Archive Policy"] = _byId[1001],
        };

    public Entry Lookup(int id) => _byId[id];

    public Entry Lookup(string name) => _byName[name];

    public Entry Lookup(GeneratedLookupKey key) => key.Value switch
    {
        int id => Lookup(id),
        string name => Lookup(name),
        null => throw new InvalidOperationException("Lookup key must be initialized before it is processed."),
        _ => throw new SwitchExpressionException(key),
    };

    public Entry Lookup(ManualLookupKey key)
    {
        if (key.TryGetValue(out int id))
        {
            return Lookup(id);
        }

        if (key.TryGetValue(out string? name))
        {
            return Lookup(name!);
        }

        throw new InvalidOperationException("Lookup key must be initialized before it is processed.");
    }
}

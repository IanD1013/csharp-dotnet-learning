using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MasteringCSharp.TuplesAndUnions.Demos;

/// <summary>
/// Lesson "Union Types Under the Hood": a hand-written transcription of the struct the
/// C# 15 compiler generates for
/// <c>public readonly union LookupKey(int Id, string Name)</c>.
/// <para>
/// This is not the real feature. The `union` keyword does not parse on the .NET 10 SDK,
/// so the demo runs the decompiled shape instead. Everything the chapter measures - the
/// boxed <c>object? Value</c>, the missing ToString and equality members - is a property
/// of this shape rather than of the syntax, so the measurements still hold.
/// </para>
/// </summary>
[Union]
public readonly struct GeneratedLookupKey : IUnion
{
    /// <summary>Case constructor for the 'int Id' case. The compiler also emits the implicit conversion.</summary>
    public GeneratedLookupKey(int value) => Value = value;

    /// <summary>Case constructor for the 'string Name' case.</summary>
    public GeneratedLookupKey(string value) => Value = value;

    /// <summary>
    /// One object field holds whichever case is active. An int has to be boxed to fit.
    /// </summary>
    public object? Value { get; }

    public static implicit operator GeneratedLookupKey(int value) => new(value);

    public static implicit operator GeneratedLookupKey(string value) => new(value);

    /// <summary>
    /// What <c>this switch { int id =&gt; ..., string name =&gt; ..., null =&gt; ... }</c>
    /// lowers to: a chain of type tests against Value.
    /// </summary>
    public string Describe()
    {
        object? value = Value;

        if (value is int id)
        {
            return $"id {id}";
        }

        if (value is string name)
        {
            return $"name {name}";
        }

        // The generated struct can still be default-initialised, so Value may be null even
        // though null is not one of the declared cases. This is the branch the compiler
        // forces you to handle in a switch expression over a struct union.
        if (value is null)
        {
            return "uninitialized";
        }

        throw new SwitchExpressionException(this);
    }
}

/// <summary>
/// Lesson "Union Types: the Deep Dive": a manual union that avoids the boxing.
/// Storage is two nullable fields rather than one object, and each case is reached through
/// a TryGetValue overload, which is the shape the compiler pattern-matches against in
/// preference to reading Value.
/// <para>
/// The nullable <c>int?</c> is what separates the id-zero case from <c>default</c>.
/// A plain <c>int</c> field would make those two indistinguishable.
/// </para>
/// </summary>
[Union]
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
        ArgumentNullException.ThrowIfNull(name);
        _id = default;
        _name = name;
    }

    /// <summary>Still offered for compatibility, and still boxes. TryGetValue is the fast path.</summary>
    public object? Value => _name is not null ? _name : _id;

    public static implicit operator ManualLookupKey(int value) => new(value);

    public static implicit operator ManualLookupKey(string value) => new(value);

    public bool TryGetValue(out int value)
    {
        value = _id.GetValueOrDefault();
        return _id is not null;
    }

    public bool TryGetValue([MaybeNullWhen(false)] out string value)
    {
        value = _name;
        return _name is not null;
    }

    /// <summary>Hand-lowered form of the switch expression, using the non-boxing accessors.</summary>
    public string Describe()
    {
        if (TryGetValue(out int id))
        {
            return $"id {id}";
        }

        if (TryGetValue(out string? name))
        {
            return $"name {name}";
        }

        return "<uninitialized>";
    }

    public override string ToString() => Describe();
}

/// <summary>
/// Lesson "Union Types: the Deep Dive": the class-based variant. A class cannot be produced
/// by <c>default</c>, so there is no uninitialized case to handle and Value can be
/// non-nullable. That is what lets a switch over it be exhaustive with two arms.
/// </summary>
[Union]
public sealed class ClassLookupKey
{
    private readonly int _id;
    private readonly string? _name;

    public ClassLookupKey(int id)
    {
        _id = id;
        _name = null;
    }

    public ClassLookupKey(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _id = default;
        _name = name;
    }

    /// <summary>Non-nullable: the compiler can therefore prove the union is never empty.</summary>
    public object Value => _name is not null ? _name : _id;

    public static implicit operator ClassLookupKey(int value) => new(value);

    public static implicit operator ClassLookupKey(string value) => new(value);

    public bool TryGetValue(out int value)
    {
        value = _id;
        return _name is null;
    }

    public bool TryGetValue([MaybeNullWhen(false)] out string value)
    {
        value = _name;
        return _name is not null;
    }

    /// <summary>Two arms, no uninitialized branch, and no discard needed.</summary>
    public string Describe() =>
        TryGetValue(out string? name) ? $"name {name}" : $"id {_id}";
}

/// <summary>An entry the lookups return. Lesson "Union Types: the Basics".</summary>
public sealed record Entry(int Id, string Name);

/// <summary>
/// Lesson "Union Types: the Basics" and "Union Types Under the Hood": the same lookup
/// reached four ways, so the cost of each key representation is directly comparable.
/// </summary>
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

    /// <summary>Through the compiler-shaped union: the int case was boxed on the way in.</summary>
    public Entry Lookup(GeneratedLookupKey key) => key.Value switch
    {
        int id => Lookup(id),
        string name => Lookup(name),
        null => throw new InvalidOperationException("Lookup key must be initialized before it is processed."),
        _ => throw new SwitchExpressionException(key),
    };

    /// <summary>Through the manual union: TryGetValue reads the int back with no allocation.</summary>
    public Entry Lookup(ManualLookupKey key)
    {
        if (key.TryGetValue(out int id))
        {
            return Lookup(id);
        }

        if (key.TryGetValue(out string? name))
        {
            return Lookup(name);
        }

        throw new InvalidOperationException("Lookup key must be initialized before it is processed.");
    }
}

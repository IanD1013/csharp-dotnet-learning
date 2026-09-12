using System.Runtime.CompilerServices;
using System.Text;

namespace Level5_SIMD;

/// <summary>
/// The custom open-addressing hash table built across "Custom: FastHashTable Part 1", "Part 2"
/// and "Part 3", replacing the <see cref="Dictionary{TKey, TValue}"/> Level 4 used. Keys are the
/// raw UTF-8 station-name bytes in the mapped file, so a lookup never allocates a string.
/// </summary>
internal sealed unsafe class FastHashTable
{
    private const int DefaultCapacity = 1024;
    private const int MaxCapacity = 32768;
    private const double LoadFactorThreshold = 0.75;

    private Entry[] _entries;
    private int _count;
    private readonly bool _allowResize;

    public FastHashTable(int expectedCount = 500, bool allowResize = true)
    {
        _allowResize = allowResize;

        // Calculate initial capacity
        var targetCapacity = (int)(expectedCount / LoadFactorThreshold);
        var capacity = NextPowerOf2(Math.Max(DefaultCapacity, targetCapacity));
        capacity = Math.Min(capacity, MaxCapacity);

        _entries = new Entry[capacity];
    }

    public struct Entry
    {
        // CA1051: public fields are the point here, exactly as in "Custom: FastHashTable Part 1".
        // Properties would add a getter/setter pair on the hottest path in the challenge.
#pragma warning disable CA1051
        public byte[]? Name;          // Raw UTF-8 bytes (for comparison)
        public string? StationName;   // Cached string (created once)
        public uint Hash;
        public int Min;               // Temperature * 10
        public int Max;               // Temperature * 10
        public long Sum;              // Sum of temperatures * 10
        public long Count;
#pragma warning restore CA1051
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddOrUpdate(byte* namePtr, int nameLen, int temperature)
    {
        // Check if resize is needed before adding
        if (_allowResize && _count >= _entries.Length * LoadFactorThreshold)
        {
            var newCapacity = Math.Min(_entries.Length * 2, MaxCapacity);
            if (newCapacity > _entries.Length)
            {
                Resize(newCapacity);
            }
        }

        var hash = ComputeHash(namePtr, nameLen);
        var index = (int)(hash & (uint)(_entries.Length - 1)); // 1 cycle
        // 143 % 10 => [3] // ~3-4 cycle

        while (true)
        {
            ref Entry entry = ref _entries[index];

            if (entry.Name is null) // first visit
            {
                // New entry - copy name bytes AND create string once
                entry.Name = new byte[nameLen];

                fixed (byte* dest = entry.Name)
                {
                    Buffer.MemoryCopy(namePtr, dest, nameLen, nameLen);
                }

                entry.StationName = Encoding.UTF8.GetString(entry.Name);
                entry.Hash = hash;
                entry.Min = temperature;
                entry.Max = temperature;
                entry.Sum = temperature;
                entry.Count = 1;
                _count++;

                return;
            }

            // not the first visit
            if (entry.Hash == hash && entry.Name.Length == nameLen)
            {
                // Verify bytes match - different names can share a hash
                fixed (byte* entryName = entry.Name)
                {
                    var match = true;
                    for (var i = 0; i < nameLen; i++)
                    {
                        if (entryName[i] != namePtr[i])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        // Math.Min/Math.Max are recognized by the JIT and emitted branchless,
                        // unlike the `if (temperature < entry.Min)` pair they replace.
                        entry.Min = Math.Min(temperature, entry.Min);
                        entry.Max = Math.Max(temperature, entry.Max);

                        entry.Sum += temperature;
                        entry.Count++;
                        return;
                    }
                }
            }

            // Linear probing
            index = (index + 1) & (_entries.Length - 1);
        }
    }

    private void Resize(int newCapacity)
    {
        var oldEntries = _entries;
        _entries = new Entry[newCapacity];
        _count = 0;

        // Rehash all existing entries
        foreach (var oldEntry in oldEntries)
        {
            if (oldEntry.Name != null)
            {
                // Reinsert into new table
                var hash = oldEntry.Hash;
                var index = (int)(hash & (uint)(_entries.Length - 1));

                while (true)
                {
                    ref Entry entry = ref _entries[index];

                    if (entry.Name is null)
                    {
                        // Found empty slot - copy data
                        entry = oldEntry;
                        _count++;
                        break;
                    }

                    // Linear probing
                    index = (index + 1) & (_entries.Length - 1);
                }
            }
        }
    }

    public IEnumerable<Entry> GetEntries()
    {
        foreach (var entry in _entries)
        {
            if (entry.Name != null)
                yield return entry;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint ComputeHash(byte* ptr, int len)
    {
        // FNV-1a hash - good distribution, fast
        var hash = 2166136261u;
        for (var i = 0; i < len; i++)
        {
            hash ^= ptr[i];
            hash *= 16777619u;
        }

        return hash;
    }

    // Branchless
    private static int NextPowerOf2(int n)
    {
        if (n <= 0)
            return 1;

        n--;
        n |= n >> 1;
        n |= n >> 2;
        n |= n >> 4;
        n |= n >> 8;
        n |= n >> 16;

        return n + 1;
    }
}

using Shared;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Text;

// =============================================================================
// Level 6: AVX-512 Implementation (Maximum Bandwidth)
// =============================================================================

Console.WriteLine("=== Level 6: Expert (Optimized) ===");
Console.WriteLine($"File: {GlobalConstants.FilePath}");
Console.WriteLine($"Processor Count: {Environment.ProcessorCount}");
Console.WriteLine($"AVX2 Supported: {Avx2.IsSupported}");
Console.WriteLine($"AVX-512 Supported: {Avx512F.IsSupported}");
Console.WriteLine($"ARM AdvSimd Supported: {AdvSimd.IsSupported}");
Console.WriteLine($"Widest accelerated vector: {VectorEngine.Description}");

Console.WriteLine();

if (!File.Exists(GlobalConstants.FilePath))
{
    Console.WriteLine($"ERROR: File not found at {GlobalConstants.FilePath}");
    return;
}

if (!Avx512F.IsSupported)
{
    Console.WriteLine($"WARNING: AVX-512 not supported. Falling back to {VectorEngine.Description}.");
}

GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();

var stopwatch = Stopwatch.StartNew();

// =============================================================================
// CORE IMPLEMENTATION: AVX-512 Engine
// =============================================================================

var fileInfo = new FileInfo(GlobalConstants.FilePath);
var fileSize = fileInfo.Length;

if (fileSize == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

var threadCount = Environment.ProcessorCount;
var tasks = new Task<FixedDictionary>[threadCount];

using var mmf = MemoryMappedFile.CreateFromFile(GlobalConstants.FilePath,
                                                FileMode.Open,
                                                null,
                                                0,
                                                MemoryMappedFileAccess.Read);

using var accessor = mmf.CreateViewAccessor(0, fileSize, MemoryMappedFileAccess.Read);

unsafe
{
    byte* basePtr = null;
    accessor.SafeMemoryMappedViewHandle.AcquirePointer(ref basePtr);

    // Skip UTF-8 BOM if present
    long dataStart = 0;
    if (fileSize >= 3 && basePtr[0] == 0xEF && basePtr[1] == 0xBB && basePtr[2] == 0xBF)
    {
        dataStart = 3;
    }

    var dataSize = fileSize - dataStart;
    var chunkSize = dataSize / threadCount;

    for (var i = 0; i < threadCount; i++)
    {
        var startPos = dataStart + (i * chunkSize);
        var endPos = (i == threadCount - 1) ? fileSize : dataStart + ((i + 1) * chunkSize);

        // Align to line boundaries
        if (startPos > dataStart)
        {
            while (startPos < fileSize && basePtr[startPos - 1] != '\n')
            {
                startPos++;
            }
        }

        if (endPos < fileSize && i < threadCount - 1)
        {
            while (endPos < fileSize && basePtr[endPos - 1] != '\n')
            {
                endPos++;
            }
        }

        var ptrCopy = basePtr;

        // "Final Project with Conclusion": LongRunning tells the scheduler this task will own a
        // thread for a while, so it gets a dedicated one instead of a thread-pool slot.
        tasks[i] = Task.Factory.StartNew(
            () => ProcessChunk(ptrCopy, startPos, endPos),
            TaskCreationOptions.LongRunning);
    }

    Task.WaitAll(tasks);

    // =========================================================================
    // MERGE: Combine Results
    // =========================================================================

    var finalResults = new Dictionary<string, (int Min, int Max, long Sum, long Count)>(
        GlobalConstants.ExpectedStationCount);

    for (var i = 0; i < threadCount; i++)
    {
        using var localTable = tasks[i].Result;
        localTable.MergeTo(finalResults);
    }

    // Release pointer strictly after processing
    accessor.SafeMemoryMappedViewHandle.ReleasePointer();

    var totalLines = finalResults.Values.Sum(x => x.Count);
    stopwatch.Stop();

    // =========================================================================
    // OUTPUT: the integer-scaled temperatures go back to doubles for the 1BRC format
    // =========================================================================

    var sortedResults = finalResults
        .OrderBy(kvp => kvp.Key, StringComparer.Ordinal)
        .ToList();

    var output = ResultLogger.FormatOutput(
        sortedResults,
        s => $"{s.Min / 10.0:F1}/{s.Sum / 10.0 / s.Count:F1}/{s.Max / 10.0:F1}");

    Console.WriteLine(output);

    Console.WriteLine();
    Console.WriteLine($"Processed {totalLines:N0} rows using {threadCount} threads");
    Console.WriteLine($"Found {finalResults.Count} unique stations");
    Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");

    ResultLogger.SaveResult(
        projectName: "Level06_Expert",
        output: output,
        elapsed: stopwatch.Elapsed,
        rowCount: totalLines,
        stationCount: finalResults.Count);

    Console.WriteLine();
    Console.WriteLine("Memory Statistics:");
    Console.WriteLine($"  Working Set: {Environment.WorkingSet / 1024 / 1024:N0} MB");
    Console.WriteLine($"  GC Total Memory: {GC.GetTotalMemory(false) / 1024 / 1024:N0} MB");
    Console.WriteLine($"  Gen0 Collections: {GC.CollectionCount(0)}");
    Console.WriteLine($"  Gen1 Collections: {GC.CollectionCount(1)}");
    Console.WriteLine($"  Gen2 Collections: {GC.CollectionCount(2)}");
}

// =============================================================================
// VECTORIZED PROCESSING ENGINE
// =============================================================================
// OPTIMIZATION TECHNIQUES DEMONSTRATED:
// 1. SIMD Vectorization: Process one whole vector per iteration (64 bytes under AVX-512)
// 2. Branchless Bit Manipulation: Extract multiple delimiters from single mask
// 3. Loop Unrolling: 2x unrolled hash computation reduces loop overhead
// 4. Cache-Friendly: Sequential memory access pattern, prefetcher-optimal
// 5. Vector width chosen at run time, so the loop also works where AVX-512 does not exist
[MethodImpl(MethodImplOptions.AggressiveOptimization)]
static unsafe FixedDictionary ProcessChunk(byte* basePtr, long start, long end)
{
    var dict = new FixedDictionary();
    var ptr = basePtr + start;
    var endPtr = basePtr + end;

    // Start of the record currently being assembled. The course keeps this implicit in `ptr`,
    // because a 64-byte window opened at a record start always contains the ';' on 1BRC data.
    // A 16-byte window does not ("Ho Chi Minh City" is exactly 16 bytes), and the course's
    // `ptr += blockSize` on an empty mask would then lose where the name began, so the record
    // start is carried in its own variable and only moves when a record is finished.
    var nameStart = ptr;

    // The course runs one AVX-512 loop. This machine is win-arm64, where Vector512 has no hardware
    // behind it, so the same loop is written once per vector width and the widest accelerated one
    // is picked at run time. The body is identical: broadcast ';', compare a whole vector at once,
    // turn the comparison into a bit mask, then drain every record the mask found.
    if (Vector512.IsHardwareAccelerated)
    {
        var vSemi = Vector512.Create((byte)';');

        // Safe zone: one vector before end to prevent overread
        var safeEndPtr = endPtr - 64;

        while (ptr < safeEndPtr)
        {
            var blockStart = ptr;

            // SIMD: load 64 bytes in a single CPU instruction (vs 64 scalar loads), then compare
            // all 64 of them against ';' at once. Each set bit of the mask is one delimiter.
            var maskSemi = Vector512.Equals(Vector512.Load(ptr), vSemi).ExtractMostSignificantBits();

            if (maskSemi == 0)
            {
                ptr += 64;  // Prefetcher benefits from regular stride
                continue;
            }

            DrainBlock(blockStart, ref ptr, ref nameStart, maskSemi, in dict);
        }
    }
    else if (Vector256.IsHardwareAccelerated)
    {
        var vSemi = Vector256.Create((byte)';');
        var safeEndPtr = endPtr - 32;

        while (ptr < safeEndPtr)
        {
            var blockStart = ptr;
            var maskSemi = Vector256.Equals(Vector256.Load(ptr), vSemi).ExtractMostSignificantBits();

            if (maskSemi == 0)
            {
                ptr += 32;
                continue;
            }

            DrainBlock(blockStart, ref ptr, ref nameStart, maskSemi, in dict);
        }
    }
    else
    {
        // ARM64 lands here: AdvSimd is 16 bytes wide, so a block holds roughly one record.
        var vSemi = Vector128.Create((byte)';');
        var safeEndPtr = endPtr - 16;

        while (ptr < safeEndPtr)
        {
            var blockStart = ptr;
            var maskSemi = Vector128.Equals(Vector128.Load(ptr), vSemi).ExtractMostSignificantBits();

            if (maskSemi == 0)
            {
                ptr += 16;
                continue;
            }

            DrainBlock(blockStart, ref ptr, ref nameStart, maskSemi, in dict);
        }
    }

    // =========================================================================
    // SCALAR TAIL PROCESSING: Handle remaining bytes outside SIMD safe zone
    // =========================================================================
    // REASON: the last vector's worth of bytes cannot be safely SIMD-loaded (would overread)
    // TRADE-OFF: Slower scalar path, but only a sliver of the total data
    while (ptr < endPtr)
    {
        while (*ptr != ';')
        {
            ptr++;
        }

        var len = (int)(ptr - nameStart);

        // Hash computation (same FNV-1a algorithm as SIMD path)
        ulong hash = 0;
        for (var i = 0; i < len; i++)
        {
            hash ^= nameStart[i];
            hash *= 1099511628211ul;
        }

        ptr++;  // Skip semicolon

        var sign = 1;
        if (*ptr == '-')
        {
            sign = -1;
            ptr++;
        }

        var temp = 0;

        // SIMPLE LOOP: No SIMD optimization here (tail is tiny)
        while (*ptr != '\n')
        {
            if (*ptr != '.')
            {
                temp = (temp * 10) + (*ptr - '0');
            }

            ptr++;
        }

        ptr++;  // Skip newline

        dict.Update(nameStart, len, hash, temp * sign);
        nameStart = ptr;
    }

    return dict;
}

// =============================================================================
// INNER LOOP: Drain all semicolons from the current block
// =============================================================================
// TECHNIQUE: Process multiple records per SIMD load (amortize cost)
// Safe because temperature bytes (digits, '.', '-', '\n') are never ';'
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static unsafe void DrainBlock(byte* blockStart,
                              ref byte* ptr,
                              ref byte* nameStart,
                              ulong maskSemi,
                              in FixedDictionary dict)
{
    do
    {
        // BIT MANIPULATION: Find position of rightmost set bit (next ';')
        // TrailingZeroCount: Hardware instruction (TZCNT) - 1 cycle latency
        var semiAbs = BitOperations.TrailingZeroCount(maskSemi);

        // BRANCHLESS: Clear lowest bit using bit trick (x & (x-1))
        // Avoids conditional jump - predictable for CPU pipeline
        maskSemi &= maskSemi - 1;

        var nameLen = (int)(blockStart + semiAbs - nameStart);

        // =====================================================================
        // HASH COMPUTATION: FNV-1a with 2x loop unrolling
        // =====================================================================
        // 2x unrolling: sweet spot between parallelism and instruction cache pressure
        ulong hash = 0;
        var hi = 0;

        for (; hi + 1 < nameLen; hi += 2)
        {
            hash ^= nameStart[hi];
            hash *= 1099511628211ul;  // FNV prime

            hash ^= nameStart[hi + 1];
            hash *= 1099511628211ul;
        }

        // TAIL HANDLING: Process remaining 0-1 bytes
        if (hi < nameLen)
        {
            hash ^= nameStart[hi];
            hash *= 1099511628211ul;
        }

        ptr = blockStart + semiAbs + 1;

        // =====================================================================
        // TEMPERATURE PARSING: Aggressive branchless optimization
        // =====================================================================
        // TECHNIQUE: Load 8 bytes once, use bit manipulation to avoid branches
        // BENEFIT: Eliminates branch mispredictions (~5-10 cycle penalty each)

        var valueWrapper = *(long*)ptr;  // Single 8-byte load (1 cycle)
        var sign = 1;
        var firstByte = (byte)valueWrapper;

        // SIGN DETECTION: Still uses a branch, but a highly predictable one
        if (firstByte == '-')
        {
            sign = -1;
            ptr++;
            valueWrapper >>= 8;  // Shift to next digit
        }

        var secondByte = (byte)(valueWrapper >> 8);

        // BRANCHLESS FORMAT DETECTION: Use boolean arithmetic instead of if/else
        var isDot = secondByte == '.';

        // PATH 1: X.Y format (e.g., "5.3\n")
        var temp1Digit = (((byte)valueWrapper - '0') * 10)
                       + ((byte)(valueWrapper >> 16) - '0');

        // PATH 2: XX.Y format (e.g., "23.7\n")
        var temp2Digits = (((byte)valueWrapper - '0') * 100)
                        + ((secondByte - '0') * 10)
                        + ((byte)(valueWrapper >> 24) - '0');

        // BRANCHLESS SELECT: only one path contributes; the compiler generates CMOV
        var temperature = isDot ? temp1Digit : temp2Digits;

        // BRANCHLESS POINTER ADVANCE: 4 or 5 bytes depending on format
        ptr += isDot ? 4 : 5;

        dict.Update(nameStart, nameLen, hash, temperature * sign);
        nameStart = ptr;
    }
    while (maskSemi != 0);
}

/// <summary>
/// Not in the course, which targets x86 only: reports which vector width
/// <c>ProcessChunk</c> actually picked, so the banner does not claim AVX-512 on a machine
/// that has none.
/// </summary>
internal static class VectorEngine
{
    public static string Description =>
        Vector512.IsHardwareAccelerated ? "Vector512 (64 bytes)"
        : Vector256.IsHardwareAccelerated ? "Vector256 (32 bytes)"
        : "Vector128 (16 bytes)";
}

// =============================================================================
// NATIVE MEMORY DICTIONARY
// =============================================================================
/// <summary>
/// Fixed-capacity native-memory hash table optimized for maximum throughput.
///
/// DESIGN DECISION: Fixed vs. Dynamic Capacity
///
/// 1. NATIVE MEMORY COMPLEXITY
///    - Dynamic resize requires careful NativeMemory.AlignedRealloc or full reallocation
///    - Manual rehashing of aligned entries adds significant complexity
///
/// 2. CACHE-LINE OPTIMIZATION
///    - Each entry is padded to 192 bytes for cache-line alignment
///    - Fixed size ensures predictable cache behavior throughout execution
///
/// 3. THROUGHPUT FOCUS
///    - Fixed allocation eliminates ALL resize overhead in the hot path
///    - Zero branch mispredictions from load-factor checks
///
/// 4. KNOWN WORKLOAD
///    - 1BRC has ~10,000 unique stations maximum
///    - 16,384 capacity at a 75% load factor leaves 12,288 entries before collisions increase
///
/// COMPARISON WITH LEVEL 5:
/// - Level5_SIMD supports dynamic growth for general-purpose robustness
/// - Level6_Expert accepts fixed over-allocation for educational contrast and peak performance
/// </summary>
internal unsafe struct FixedDictionary : IDisposable
{
    // Homework: Memory-Alignment

    // Capacity sized for 10,000 unique stations
    // 10,000 / 0.75 (load factor) = 13,333 -> 16,384 (next power of 2)
    private const int Capacity = 16384;
    private const int CapacityMask = Capacity - 1;

    private Entry* _entries;

    public FixedDictionary()
    {
        // Cache-line aligned allocation (64 bytes)
        nuint alignment = 64;
        var size = (nuint)(Capacity * sizeof(Entry));
        _entries = (Entry*)NativeMemory.AlignedAlloc(size, alignment);

        // Zero initialize
        NativeMemory.Clear(_entries, size);
    }

    /// <summary>
    /// Updates a dictionary entry with one temperature measurement.
    ///
    /// OPTIMIZATION TECHNIQUES:
    /// 1. Open Addressing: linear probing minimizes indirection versus chaining
    /// 2. Bitwise Masking: hash AND mask replaces the expensive modulo operation
    /// 3. Chunked Memory Copy: 8-byte copies for medium and long names
    /// 4. Parallel Name Comparison: 8-byte comparisons for names of 12 bytes or more
    /// 5. Early Exit: hash and length check before the expensive name comparison
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Update(byte* namePtr, int nameLen, ulong hash, int temp)
    {
        // BITWISE MASKING: fast modulo for a power-of-2 capacity
        var idx = (uint)hash & CapacityMask;

        // LINEAR PROBING: open addressing with a predictable memory access pattern
        while (true)
        {
            var entry = &_entries[idx];

            // EMPTY SLOT: first occurrence of this station
            if (entry->Count == 0)
            {
                entry->Hash = hash;
                entry->Min = temp;
                entry->Max = temp;
                entry->Sum = temp;
                entry->Count = 1;
                entry->NameLen = nameLen;

                // CHUNKED COPY: 8 bytes at a time for medium and long names
                // REASON: modern CPUs have 64-bit data paths, so one instruction copies 8 bytes
                if (nameLen >= 8)
                {
                    var i = 0;

                    for (; i + 8 <= nameLen; i += 8)
                    {
                        *(ulong*)(entry->NameBuffer + i) = *(ulong*)(namePtr + i);
                    }

                    // Scalar tail: copy the remaining 1-7 bytes
                    for (; i < nameLen; i++)
                    {
                        entry->NameBuffer[i] = namePtr[i];
                    }
                }
                else
                {
                    // SCALAR PATH: short names, where chunking overhead is not worth it
                    for (var k = 0; k < nameLen; k++)
                    {
                        entry->NameBuffer[k] = namePtr[k];
                    }
                }

                return;
            }

            // COLLISION DETECTION: hash and length check before the name comparison
            if (entry->Hash == hash && entry->NameLen == nameLen)
            {
                bool match;

                // ADAPTIVE STRATEGY: choose the comparison method by name length
                if (nameLen >= 12)
                {
                    match = true;
                    var i = 0;

                    // 8-byte chunks: the CPU compares all 8 bytes in parallel
                    for (; i + 8 <= nameLen; i += 8)
                    {
                        if (*(ulong*)(entry->NameBuffer + i) != *(ulong*)(namePtr + i))
                        {
                            match = false;
                            break;
                        }
                    }

                    // Tail comparison: the remaining 1-7 bytes
                    if (match)
                    {
                        for (; i < nameLen; i++)
                        {
                            if (entry->NameBuffer[i] != namePtr[i])
                            {
                                match = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // SCALAR PATH: short names, under 12 bytes
                    match = true;
                    for (var i = 0; i < nameLen; i++)
                    {
                        if (entry->NameBuffer[i] != namePtr[i])
                        {
                            match = false;
                            break;
                        }
                    }
                }

                if (match)
                {
                    // MIN/MAX UPDATE: branch-based, and predictable thanks to temporal locality
                    // (same station -> similar temperatures -> predictable branches)
                    if (temp < entry->Min)
                    {
                        entry->Min = temp;
                    }

                    if (temp > entry->Max)
                    {
                        entry->Max = temp;
                    }

                    entry->Sum += temp;
                    entry->Count++;

                    return;
                }
            }

            // LINEAR PROBING: move to the next slot (the bitwise AND wraps around)
            idx = (idx + 1) & CapacityMask;
        }
    }

    public readonly void MergeTo(Dictionary<string, (int Min, int Max, long Sum, long Count)> target)
    {
        for (var i = 0; i < Capacity; i++)
        {
            if (_entries[i].Count > 0)
            {
                var e = &_entries[i];
                var name = Encoding.UTF8.GetString(e->NameBuffer, e->NameLen);

                ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(target, name, out var exists);
                if (exists)
                {
                    if (e->Min < val.Min)
                    {
                        val.Min = e->Min;
                    }

                    if (e->Max > val.Max)
                    {
                        val.Max = e->Max;
                    }

                    val.Sum += e->Sum;
                    val.Count += e->Count;
                }
                else
                {
                    val = (e->Min, e->Max, e->Sum, e->Count);
                }
            }
        }
    }

    public void Dispose()
    {
        if (_entries != null)
        {
            NativeMemory.AlignedFree(_entries);
            _entries = null;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8, Size = 192)]
    private struct Entry
    {
        // CA1051: public fields are the point here, exactly as in the course. Properties would add
        // a getter/setter pair on the hottest path in the challenge.
#pragma warning disable CA1051
        public ulong Hash;          // 8 bytes
        public int Min;             // 4 bytes
        public int Max;             // 4 bytes
        public long Sum;            // 8 bytes
        public long Count;          // 8 bytes
        public int NameLen;         // 4 bytes
        public fixed byte NameBuffer[100];  // 100 bytes
#pragma warning restore CA1051
        // Implicit padding to 192 bytes (3 cache lines) for optimal alignment
    }
}

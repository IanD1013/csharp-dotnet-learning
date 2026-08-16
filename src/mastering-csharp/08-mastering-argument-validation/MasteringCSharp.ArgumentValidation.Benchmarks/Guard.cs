using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MasteringCSharp.ArgumentValidation.Benchmarks;

/// <summary>
/// The lesson's benchmark subject, transcribed from
/// "Analyzing ThrowIfNull Boxing Allocations".
/// Two guards with identical bodies; only one of them may be inlined.
/// </summary>
public static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowIfNullNoInline(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    /// <summary>
    /// Not in the lesson. A generic guard is the obvious way to dodge the boxing
    /// question entirely, so it is worth measuring alongside the object? version.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ThrowIfNullGenericNoInline<T>(
        [NotNull] T? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);
}

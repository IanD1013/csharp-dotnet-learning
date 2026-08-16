using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MasteringCSharp.ArgumentValidation.Demos;

/// <summary>
/// The chapter's custom guard helper, built up across the lessons
/// "Recreating ThrowIfNull Manually" and "Implementing Custom String Validation".
/// Covered in notes lessons 2, 6 and 9.
/// </summary>
/// <remarks>
/// Three attributes do three different jobs here, and the notes separate them:
/// [CallerArgumentExpression] fills in the parameter name at the call site,
/// [NotNull] tells the compiler's flow analysis the argument is non-null once this returns,
/// and [DoesNotReturn] on the throw helper tells it the Throw call ends the path.
/// </remarks>
internal static class Guard
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
            Throw(paramName);
    }

    /// <summary>
    /// The chapter's string guard, transcribed exactly as the lesson writes it
    /// (notes lesson 9). It removes the CS8604 warning at every call site, which is what
    /// the lesson set out to do, and on net48 it moves a warning into the guard instead.
    /// See the pragma below and ThrowIfNullOrEmptyPortable.
    /// </summary>
#pragma warning disable CS8777 // the net48 warning is the finding, not an accident - see ThrowIfNullOrEmptyPortable
    public static void ThrowIfNullOrEmpty(
        [NotNull] string? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
            Throw(argument, paramName);
    }
#pragma warning restore CS8777

    /// <summary>
    /// The same guard with the one change that makes it compile clean on both targets:
    /// the null test is written out instead of delegated to an unannotated BCL method,
    /// so the compiler can verify the [NotNull] contract itself rather than taking it
    /// on trust. Behaviour is identical. Not in the course; the lesson stops at the
    /// version above.
    /// </summary>
    public static void ThrowIfNullOrEmptyPortable(
        [NotNull] string? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null || argument.Length == 0)
            Throw(argument, paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? paramName)
        => throw new ArgumentNullException(paramName);

    [DoesNotReturn]
    private static void Throw(string? argument, string? paramName)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
        throw new ArgumentException("Value cannot be empty.", paramName);
    }
}

/// <summary>
/// The naive first version from the lesson, kept so the demo can show what the
/// [CallerArgumentExpression] attribute is actually buying: without it, ParamName is null.
/// </summary>
internal static class NaiveGuard
{
    public static void ThrowIfNull(object? argument, string? paramName = null)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
    }
}

namespace System.Runtime.CompilerServices;

/// <summary>
/// Lesson "Union Types: the Deep Dive": union support is duck-typed by the compiler, so
/// these two declarations are all a project needs in order to use unions when targeting an
/// older framework. They are ordinary types, which is exactly why the feature needs no
/// runtime support and works all the way back to .NET Framework.
/// <para>
/// On the .NET 10 SDK nothing consumes them yet, because the compiler does not recognise
/// unions at all. They are here because the shapes in UnionTypes.cs reference them, and
/// because seeing how small the contract is makes the "no runtime support" claim concrete.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class UnionAttribute : Attribute;

/// <summary>
/// The interface the compiler-generated union struct implements.
/// </summary>
public interface IUnion
{
    object? Value { get; }
}

using DependencyInjection.Scrutor.Demos.AttributeMarking;

namespace DependencyInjection.Scrutor.Demos.NamespaceFiltering.Internal;

/// <summary>Lesson 7: lives in the namespace NotInNamespaces is asked to leave out.</summary>
public sealed class CacheWarmerService : ICacheWarmerService;

/// <summary>The functional interface of <see cref="CacheWarmerService"/>.</summary>
public interface ICacheWarmerService;

/// <summary>Lesson 7: carries the marker WithoutAttribute is asked to leave out.</summary>
[Scoped]
public sealed class DiagnosticsService : IDiagnosticsService;

/// <summary>The functional interface of <see cref="DiagnosticsService"/>.</summary>
public interface IDiagnosticsService;

using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DependencyInjection.Scrutor.Demos.Descriptors;

/// <summary>
/// Lesson 8: no arguments, so Scrutor registers the class as itself and as its interface,
/// both transient.
/// </summary>
[ServiceDescriptor]
public sealed class DefaultService : IDefaultService;

/// <summary>The functional interface of <see cref="DefaultService"/>.</summary>
public interface IDefaultService;

/// <summary>Lesson 8: a service type is named, so only that one type is registered.</summary>
[ServiceDescriptor(typeof(IInterfaceOnlyService))]
public sealed class InterfaceOnlyService : IInterfaceOnlyService;

/// <summary>The functional interface of <see cref="InterfaceOnlyService"/>.</summary>
public interface IInterfaceOnlyService;

/// <summary>Lesson 8: service type plus an explicit lifetime.</summary>
[ServiceDescriptor(typeof(ISingletonOnlyService), ServiceLifetime.Singleton)]
public sealed class SingletonOnlyService : ISingletonOnlyService;

/// <summary>The functional interface of <see cref="SingletonOnlyService"/>.</summary>
public interface ISingletonOnlyService;

/// <summary>
/// Lesson 8: null for the service type falls back to "self and interfaces", but the lifetime
/// given here applies to both registrations.
/// </summary>
[ServiceDescriptor(null, ServiceLifetime.Singleton)]
public sealed class SelfAndInterfaceService : ISelfAndInterfaceService;

/// <summary>The functional interface of <see cref="SelfAndInterfaceService"/>.</summary>
public interface ISelfAndInterfaceService;

/// <summary>
/// Lesson 8: two attributes, so the same implementation is registered twice, in the order the
/// attributes appear.
/// </summary>
[ServiceDescriptor(typeof(MultiService), ServiceLifetime.Singleton)]
[ServiceDescriptor(typeof(IMultiService), ServiceLifetime.Singleton)]
public sealed class MultiService : IMultiService;

/// <summary>The functional interface of <see cref="MultiService"/>.</summary>
public interface IMultiService;

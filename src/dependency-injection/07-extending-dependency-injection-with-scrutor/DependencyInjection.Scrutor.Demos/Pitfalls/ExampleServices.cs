namespace DependencyInjection.Scrutor.Demos.Pitfalls;

/// <summary>
/// Lesson 10: expensive to build and meant to be shared. A broad "EndsWith Service" filter
/// registers it transient, and nothing in the build fails to say so.
/// </summary>
public sealed class ConnectionPoolService : IConnectionPoolService
{
    /// <summary>Identifies the instance, so a second resolve is visibly a second pool.</summary>
    public Guid Id { get; } = Guid.NewGuid();
}

/// <summary>The functional interface of <see cref="ConnectionPoolService"/>.</summary>
public interface IConnectionPoolService
{
    /// <summary>Identifies the instance.</summary>
    Guid Id { get; }
}

/// <summary>
/// Lesson 10: runs once at startup and has no business being resolvable at all. It matches the
/// broad filter purely because of how it is named.
/// </summary>
public sealed class DatabaseMigrationService : IDatabaseMigrationService;

/// <summary>The functional interface of <see cref="DatabaseMigrationService"/>.</summary>
public interface IDatabaseMigrationService;

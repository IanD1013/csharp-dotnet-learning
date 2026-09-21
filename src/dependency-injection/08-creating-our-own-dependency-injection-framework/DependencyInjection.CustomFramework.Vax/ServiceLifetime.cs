namespace Vax;

/// <summary>
/// How the container manages instances of a registered service.
/// Vax stops where the course stops: there is no Scoped lifetime.
/// </summary>
public enum ServiceLifetime
{
    Singleton,
    Transient
}

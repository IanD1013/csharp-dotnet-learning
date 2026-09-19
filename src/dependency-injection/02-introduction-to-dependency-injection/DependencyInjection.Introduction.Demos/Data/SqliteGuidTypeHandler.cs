using System.Data;
using System.Runtime.CompilerServices;
using Dapper;

namespace DependencyInjection.Introduction.Demos.Data;

/// <summary>
/// Plumbing, not part of the lesson. SQLite has no GUID type, so a Guid round-trips as TEXT and
/// Dapper needs to be told how to convert it. Registering it here keeps the repository code
/// identical to the course's.
/// </summary>
public sealed class SqliteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid value) =>
        parameter.Value = value.ToString();

    public override Guid Parse(object value) => Guid.Parse((string)value);
}

internal static class SqliteTypeHandlers
{
    /// <summary>
    /// Runs once, before any code in this assembly, so both the demo and the tests are covered.
    /// </summary>
    [ModuleInitializer]
    internal static void Register() => SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
}

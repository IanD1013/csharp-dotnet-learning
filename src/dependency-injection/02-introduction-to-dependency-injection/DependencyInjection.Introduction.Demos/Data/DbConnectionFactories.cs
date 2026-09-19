using System.Data;
using Microsoft.Data.Sqlite;

namespace DependencyInjection.Introduction.Demos.Data;

/// <summary>
/// The abstraction the repository depends on. Swapping the database provider means
/// handing the repository a different implementation of this, nothing else.
/// </summary>
public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateDbConnectionAsync();
}

/// <summary>
/// SQLite on disk. The connection string arrives through <see cref="DbConnectionOptions"/>
/// rather than being hardcoded in the constructor, so the file location is the caller's decision.
/// </summary>
public sealed class SqliteFileDbConnectionFactory : IDbConnectionFactory
{
    private readonly DbConnectionOptions _connectionOptions;

    public SqliteFileDbConnectionFactory(DbConnectionOptions connectionOptions)
    {
        _connectionOptions = connectionOptions;
    }

    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionOptions.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}

/// <summary>
/// The second provider, standing in for the lesson's MySQL factory.
/// A shared-cache in-memory database lives only as long as one connection stays open,
/// so this factory owns a keep-alive connection: a first look at why lifetimes matter.
/// </summary>
public sealed class SqliteInMemoryDbConnectionFactory : IDbConnectionFactory, IDisposable
{
    private readonly DbConnectionOptions _connectionOptions;
    private readonly SqliteConnection _keepAlive;

    public SqliteInMemoryDbConnectionFactory()
    {
        _connectionOptions = new DbConnectionOptions
        {
            ConnectionString = $"Data Source=di-intro-{Guid.NewGuid():N};Mode=Memory;Cache=Shared"
        };

        _keepAlive = new SqliteConnection(_connectionOptions.ConnectionString);
        _keepAlive.Open();
    }

    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionOptions.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }

    public void Dispose() => _keepAlive.Dispose();
}

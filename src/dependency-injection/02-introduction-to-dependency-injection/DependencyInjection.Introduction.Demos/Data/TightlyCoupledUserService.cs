using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace DependencyInjection.Introduction.Demos.Data;

/// <summary>
/// Lesson 3 of the notes, the "before" version. Every layer news up the layer below it:
/// the service owns the repository, the repository owns the factory, the factory owns the
/// connection string. Nothing in this chain can be replaced from the outside, so there is no
/// seam for a test to reach in, and switching provider means editing the repository.
/// </summary>
public class TightlyCoupledUserService
{
    private readonly TightlyCoupledUserRepository _userRepository = new();

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // Do stuff here
        var users = await _userRepository.GetAllAsync();
        // Do stuff here
        return users;
    }
}

public class TightlyCoupledUserRepository
{
    private readonly TightlyCoupledSqliteDbConnectionFactory _connectionFactory = new();

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        return await connection.QueryAsync<User>("select * from Users");
    }
}

public class TightlyCoupledSqliteDbConnectionFactory
{
    private readonly DbConnectionOptions _connectionOptions = new()
    {
        ConnectionString = $"Data Source={DemoDatabase.HardWiredPath}"
    };

    public async Task<IDbConnection> CreateDbConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionOptions.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}

/// <summary>
/// The one file path both halves of the demo share, so the hard-wired chain above has
/// something real to read and the demo stays runnable.
/// </summary>
public static class DemoDatabase
{
    public static string HardWiredPath { get; } =
        Path.Combine(Path.GetTempPath(), "di-intro-demo.db");
}

using Dapper;

namespace DependencyInjection.Introduction.Demos.Data;

/// <summary>
/// Not in the course code, but the demo needs a table to query.
/// It takes the same <see cref="IDbConnectionFactory"/> as the repository, so it follows
/// whichever provider the composition root picked.
/// </summary>
public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateDbConnectionAsync();
        await connection.ExecuteAsync(
            """
            CREATE TABLE IF NOT EXISTS Users (
                Id TEXT PRIMARY KEY,
                FullName TEXT NOT NULL
            )
            """);
        await connection.ExecuteAsync("DELETE FROM Users");
    }
}

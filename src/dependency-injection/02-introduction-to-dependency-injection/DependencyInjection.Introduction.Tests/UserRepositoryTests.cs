using DependencyInjection.Introduction.Demos.Data;

namespace DependencyInjection.Introduction.Tests;

/// <summary>
/// Lesson 3 of the notes, the modularity half. <see cref="UserRepository"/> never names a
/// provider, so the test points it at a throwaway in-memory database and the production code
/// stays untouched.
/// </summary>
public class UserRepositoryTests : IAsyncLifetime, IDisposable
{
    private readonly SqliteInMemoryDbConnectionFactory _connectionFactory = new();
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userRepository = new UserRepository(_connectionFactory);
    }

    public Task InitializeAsync() => new DatabaseInitializer(_connectionFactory).InitializeAsync();

    public Task DisposeAsync()
    {
        Dispose();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _connectionFactory.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task CreateAsync_ShouldRoundTripTheUser_WhenTheProviderIsSwapped()
    {
        // Arrange
        var user = new User { FullName = "Ada Lovelace" };

        // Act
        var created = await _userRepository.CreateAsync(user);
        var stored = await _userRepository.GetByIdAsync(user.Id);

        // Assert
        Assert.True(created);
        Assert.NotNull(stored);
        Assert.Equal(user.Id, stored.Id);
        Assert.Equal("Ada Lovelace", stored.FullName);
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldRemoveTheUser_WhenItExists()
    {
        // Arrange
        var user = new User { FullName = "Grace Hopper" };
        await _userRepository.CreateAsync(user);

        // Act
        var deleted = await _userRepository.DeleteByIdAsync(user.Id);

        // Assert
        Assert.True(deleted);
        Assert.Empty(await _userRepository.GetAllAsync());
    }
}

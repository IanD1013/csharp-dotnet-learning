using DependencyInjection.Introduction.Demos.Data;

namespace DependencyInjection.Introduction.Tests;

/// <summary>
/// The hand-written fake from lesson 3: a dictionary standing in for the database.
/// It only exists because <see cref="IUserRepository"/> exists.
/// </summary>
public class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = [];

    public Task<IEnumerable<User>> GetAllAsync() =>
        Task.FromResult(_users.Values.AsEnumerable());

    public Task<User?> GetByIdAsync(Guid id) =>
        Task.FromResult(_users.GetValueOrDefault(id));

    public Task<bool> CreateAsync(User user) =>
        Task.FromResult(_users.TryAdd(user.Id, user));

    public Task<bool> DeleteByIdAsync(Guid id) =>
        Task.FromResult(_users.Remove(id));
}

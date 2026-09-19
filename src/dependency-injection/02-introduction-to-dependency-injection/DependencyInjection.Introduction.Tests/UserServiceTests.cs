using DependencyInjection.Introduction.Demos.Data;
using NSubstitute;

namespace DependencyInjection.Introduction.Tests;

/// <summary>
/// Lesson 3 of the notes, the payoff: these run with no database, no connection string and no
/// file on disk, because <see cref="UserService"/> asks for an abstraction.
/// There is no equivalent test for TightlyCoupledUserService: it news up its own repository,
/// so there is nowhere to hand it a fake.
/// </summary>
public class UserServiceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnStoredUsers_WhenRepositoryIsAHandWrittenFake()
    {
        // Arrange
        var repository = new FakeUserRepository();
        var userService = new UserService(repository);
        await repository.CreateAsync(new User { FullName = "Ada Lovelace" });

        // Act
        var users = await userService.GetAllAsync();

        // Assert
        var user = Assert.Single(users);
        Assert.Equal("Ada Lovelace", user.FullName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var mockUserRepository = Substitute.For<IUserRepository>();
        var userService = new UserService(mockUserRepository);

        var expected = new User { FullName = "Grace Hopper" };
        mockUserRepository.GetByIdAsync(expected.Id).Returns(expected);

        // Act
        var user = await userService.GetByIdAsync(expected.Id);

        // Assert
        Assert.Same(expected, user);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var mockUserRepository = Substitute.For<IUserRepository>();
        var userService = new UserService(mockUserRepository);

        mockUserRepository.GetByIdAsync(Arg.Any<Guid>()).Returns((User?)null);

        // Act
        var user = await userService.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task CreateAsync_ShouldPassTheUserThrough_WhenCalled()
    {
        // Arrange
        var mockUserRepository = Substitute.For<IUserRepository>();
        var userService = new UserService(mockUserRepository);

        var user = new User { FullName = "Alan Turing" };
        mockUserRepository.CreateAsync(user).Returns(true);

        // Act
        var created = await userService.CreateAsync(user);

        // Assert
        Assert.True(created);
        await mockUserRepository.Received(1).CreateAsync(user);
    }
}

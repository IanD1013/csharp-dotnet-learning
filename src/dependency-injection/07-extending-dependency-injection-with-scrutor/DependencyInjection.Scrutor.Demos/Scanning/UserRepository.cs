namespace DependencyInjection.Scrutor.Demos.Scanning;

/// <summary>Lesson 4: the Repository-pattern example, registered by the "EndsWith" filter.</summary>
public sealed class UserRepository : IUserRepository;

/// <summary>The interface <see cref="UserRepository"/> is matched against.</summary>
public interface IUserRepository;

/// <summary>A second repository, so the one-line registration visibly covers more than one type.</summary>
public sealed class OrderRepository : IOrderRepository;

/// <summary>The interface <see cref="OrderRepository"/> is matched against.</summary>
public interface IOrderRepository;

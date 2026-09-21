namespace DependencyInjection.CustomFramework.Demos.Services;

public interface IIdGenerator
{
    Guid Id { get; }

    void PrintId();
}

namespace Dometrain.EFCore.Api.Data;

public interface IUnitOfWorkManager
{
    void StartUnitOfWork();
    bool IsUnitOfWorkStarted { get; }
    Task<int> SaveChangesAsync();
}

public class UnitOfWorkManager(MoviesContext context) : IUnitOfWorkManager
{
    public void StartUnitOfWork()
    {
        IsUnitOfWorkStarted = true;
    }
    public bool IsUnitOfWorkStarted { get; private set; }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DomeGym.Section5.Infrastructure.Persistence;

public static class DatabaseExtensions
{
    public static async Task MigrateDatabaseAsync(this IServiceProvider services)
    {
        await using AsyncServiceScope scope = services.CreateAsyncScope();

        DomeGymDbContext dbContext = scope.ServiceProvider.GetRequiredService<DomeGymDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}

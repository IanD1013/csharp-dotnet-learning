using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dometrain.EFCore.Api.Data.Interceptors;

public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        if (eventData.Context is not MoviesContext context) return result;
        
        var tracker = context.ChangeTracker;

        var deletedEntries = tracker.Entries<Genre>()
            .Where(entry => entry.State == EntityState.Deleted);

        foreach (var deletedEntry in deletedEntries)
        {
            deletedEntry.Property<bool>("Deleted").CurrentValue = true;
            deletedEntry.State = EntityState.Modified;
        }
        
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result, 
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}
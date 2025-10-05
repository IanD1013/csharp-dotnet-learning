using Dometrain.EFCore.Api.Data;
using Dometrain.EFCore.Api.Models;
using Dometrain.EfCore.Api.Repositories;

namespace Dometrain.EFCore.Api.Services;

public interface IBatchGenreService
{
    Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres);
}

public class BatchGenreService(IGenreRepository repository, IUnitOfWorkManager uowManager) : IBatchGenreService
{
    public async Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres)
    {
        List<Genre> response = [];
        
        uowManager.StartUnitOfWork();
        
        foreach (var genre in genres)
        {
            response.Add(await repository.Create(genre));
        }

        await uowManager.SaveChangesAsync();
        
        return response;
    }
}
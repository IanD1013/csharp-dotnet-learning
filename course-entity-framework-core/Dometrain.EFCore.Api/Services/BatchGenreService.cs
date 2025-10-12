using Dometrain.EFCore.Api.Data;
using Dometrain.EFCore.Api.Models;
using Dometrain.EfCore.Api.Repositories;

namespace Dometrain.EFCore.Api.Services;

public interface IBatchGenreService
{
    Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres);
    Task<IEnumerable<Genre>> UpdateGenres(IEnumerable<Genre> genres);
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

    public async Task<IEnumerable<Genre>> UpdateGenres(IEnumerable<Genre> genres)
    {
        List<Genre> response = [];
        uowManager.StartUnitOfWork();

        // var existingGenres = await repository.GetAll(genres.Select(g => g.Id));

        foreach (var genre in genres)
        {
            var updatedGenre = await repository.Update(genre.Id, genre);
            if (updatedGenre != null)
                response.Add(updatedGenre);
        }
        
        await uowManager.SaveChangesAsync();
        
        return response;
    }
}
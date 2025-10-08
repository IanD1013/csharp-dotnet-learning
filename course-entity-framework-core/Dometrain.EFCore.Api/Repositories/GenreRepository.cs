using Dometrain.EFCore.Api.Data;
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EfCore.Api.Repositories;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAll();
    Task<Genre?> Get(int id);
    Task<Genre> Create(Genre genre);
    Task<Genre?> Update(int id, Genre genre);
    Task<bool> Delete(int id);
    Task<IEnumerable<Genre>> GetAllFromQuery();
}

public class GenreRepository(MoviesContext context, IUnitOfWorkManager uowManager) : IGenreRepository
{
    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await context.Genres.ToListAsync();
    }

    public async Task<Genre?> Get(int id)
    {
        return await context.Genres.FindAsync(id);
    }

    public async Task<Genre> Create(Genre genre)
    {
        await context.Genres.AddAsync(genre);

        if(!uowManager.IsUnitOfWorkStarted)
            await context.SaveChangesAsync();

        return genre;
    }

    public async Task<Genre?> Update(int id, Genre genre)
    {
        var existingGenre = await context.Genres.FindAsync(id);

        if (existingGenre is null)
            return null;

        existingGenre.Description = genre.Description;

        if(!uowManager.IsUnitOfWorkStarted)
            await context.SaveChangesAsync();

        return existingGenre;
    }

    public async Task<bool> Delete(int id)
    {
        var existingGenre = await context.Genres.FindAsync(id);

        if (existingGenre is null)
            return false;

        context.Genres.Remove(existingGenre);

        if(!uowManager.IsUnitOfWorkStarted)
            await context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Genre>> GetAllFromQuery()
    {
        var minimumGenreId = 2; 
        
        var genres = await context.Genres
            .FromSql($"SELECT * FROM [dbo].[Genres] WHERE ID >= {minimumGenreId}")
            // .FromSqlRaw("SELECT * FROM [dbo].[Genres] WHERE ID >= {0}", minimumGenreId)
            .Where(genre => genre.Name != "Comedy")
            .ToListAsync();
        
        return genres;
    }

    public async Task<IEnumerable<GenreName>> GetNames()
    {
        var names = await context.Database
            .SqlQuery<GenreName>($"SELECT Name FROM dbo.Genres")
            .ToListAsync();
        
        return names;
    }
}
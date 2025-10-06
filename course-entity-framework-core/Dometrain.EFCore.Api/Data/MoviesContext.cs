using Dometrain.EFCore.Api.Data.EntityMapping;
using Dometrain.EFCore.API.Data.EntityMapping;
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.Api.Data;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<ExternalInformation> ExternalInformation => Set<ExternalInformation>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
        modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
        modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
    }
}
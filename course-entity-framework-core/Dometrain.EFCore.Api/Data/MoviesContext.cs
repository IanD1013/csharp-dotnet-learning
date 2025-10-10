using Dometrain.EFCore.Api.Data.EntityMapping;
using Dometrain.EFCore.API.Data.EntityMapping;
using Dometrain.EFCore.Api.Data.Interceptors;
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.Api.Data;

public class MoviesContext(DbContextOptions<MoviesContext> options) : DbContext(options)
{
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<ExternalInformation> ExternalInformation => Set<ExternalInformation>();
    public DbSet<Actor> Actors => Set<Actor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new CinemaMovieMapping());
        modelBuilder.ApplyConfiguration(new TelevisionMovieMapping());
        modelBuilder.ApplyConfiguration(new ExternalInformationMapping());
        modelBuilder.ApplyConfiguration(new ActorMapping());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SaveChangesInterceptor());
    }
}
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.Api.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MoviesDB;User ID=sa;Password=Wowship2020!;TrustServerCertificate=True");
        optionsBuilder.LogTo(Console.WriteLine); // not proper logging but good to see what goes on in the database
        base.OnConfiguring(optionsBuilder);
    }
}
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dometrain.EFCore.Api.Data;

public class MoviesContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();
}
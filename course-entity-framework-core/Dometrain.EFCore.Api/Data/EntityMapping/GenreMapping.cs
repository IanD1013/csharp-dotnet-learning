using Dometrain.EFCore.Api.Data.ValueGenerators;
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property<DateTime>("CreatedDate")
            .HasValueGenerator<CreatedDateGenerator>();
            
        builder.HasData(new Genre { Id = 1, Name = "Drama" });
    }
}
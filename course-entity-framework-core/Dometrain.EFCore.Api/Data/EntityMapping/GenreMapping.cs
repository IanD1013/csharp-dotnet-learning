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
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();
        
        builder
            .Property<bool>("Deleted")
            .HasDefaultValue(false);
        
        builder.Property(g => g.ConcurrencyToken)
            .IsRowVersion();
        
        builder
            .HasQueryFilter(g => EF.Property<bool>(g, "Deleted") == false)
            .HasAlternateKey(g => g.Name);
    }
}
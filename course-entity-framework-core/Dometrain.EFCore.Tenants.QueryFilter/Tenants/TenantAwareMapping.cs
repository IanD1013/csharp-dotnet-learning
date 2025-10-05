using Dometrain.EFCore.Tenants.QueryFilter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.Tenants.QueryFilter.Tenants;

public abstract class TenantAwareMapping<TEntity>(MoviesContext context) : IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property<string?>("TenantId")
            .HasColumnType("varchar(32)");
        builder
            .HasIndex("TenantId");
        builder
            .HasQueryFilter(entity => EF.Property<string>(entity, "TenantId") == context.TenantId);
        
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
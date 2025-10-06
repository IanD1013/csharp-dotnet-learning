using Dometrain.EFCore.Api.Data.ValueConverters;
using Dometrain.EFCore.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dometrain.EFCore.Api.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Pictures")
            .UseTptMappingStrategy()
            // .UseTpcMappingStrategy()
            .HasKey(movie => movie.Identifier);
        
        builder.Property(movie => movie.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");

        // Be careful when mapping enum to string
        builder.Property(movie => movie.AgeRating)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();

        builder.Property(movie => movie.MainGenreName)
            .HasMaxLength(256)
            .HasColumnType("varchar");
        
        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Name)
            .HasForeignKey(movie => movie.MainGenreName);
    }
}

public class CinemaMovieMapping : IEntityTypeConfiguration<CinemaMovie>
{
    public void Configure(EntityTypeBuilder<CinemaMovie> builder)
    {
        // builder.ToTable("CinemaMovies");
    }
}

public class TelevisionMovieMapping : IEntityTypeConfiguration<TelevisionMovie>
{
    public void Configure(EntityTypeBuilder<TelevisionMovie> builder)
    {
        // builder.ToTable("TelevisionMovies");
    }
}
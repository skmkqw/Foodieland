using foodieland.Entities;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<IngredientEntity>
{
    public void Configure(EntityTypeBuilder<IngredientEntity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(e => e.IngredientQuantities)
            .WithOne(ri => ri.IngredientEntity)
            .HasForeignKey(ri => ri.IngredientId);

        builder.HasIndex(e => e.Name).IsUnique();
    }
}
using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(e => e.IngredientQuantities)
            .WithOne(ri => ri.Ingredient)
            .HasForeignKey(ri => ri.IngredientId);

        builder.HasIndex(e => e.Name).IsUnique();
    }
}
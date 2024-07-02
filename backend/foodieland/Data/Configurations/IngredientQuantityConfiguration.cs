using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class IngredientQuantityConfiguration : IEntityTypeConfiguration<IngredientQuantity>
{
    public void Configure(EntityTypeBuilder<IngredientQuantity> builder)
    {
        builder.HasKey(iq => iq.Id);

        builder.HasOne(iq => iq.Ingredient)
            .WithMany(i => i.IngredientQuantities)
            .HasForeignKey(iq => iq.IngredientId);
        
        builder.Property(iq => iq.IngredientId)
            .ValueGeneratedNever();
        
        builder.HasOne(iq => iq.Recipe)
            .WithMany(i => i.Ingredients)
            .HasForeignKey(iq => iq.RecipeId);
        
        builder.Property(iq => iq.RecipeId)
            .ValueGeneratedNever();
    }
}
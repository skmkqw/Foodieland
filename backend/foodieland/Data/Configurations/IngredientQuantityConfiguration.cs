using foodieland.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class IngredientQuantityConfiguration : IEntityTypeConfiguration<IngredientQuantityEntity>
{
    public void Configure(EntityTypeBuilder<IngredientQuantityEntity> builder)
    {
        builder.HasKey(iq => iq.Id);

        builder.HasOne(iq => iq.IngredientEntity)
            .WithMany(i => i.IngredientQuantities)
            .HasForeignKey(iq => iq.IngredientId);
        
        builder.Property(iq => iq.IngredientId)
            .ValueGeneratedNever();
        
        builder.HasOne(iq => iq.RecipeEntity)
            .WithMany(i => i.Ingredients)
            .HasForeignKey(iq => iq.RecipeId);
        
        builder.Property(iq => iq.RecipeId)
            .ValueGeneratedNever();
        
        builder.Property(ri => ri.Unit)
            .HasConversion(
                v => v.ToString(),
                v => (MeasurementUnit)Enum.Parse(typeof(MeasurementUnit), v));
    }
}
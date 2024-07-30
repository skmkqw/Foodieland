using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class FeaturedRecipeConfiguration : IEntityTypeConfiguration<FeaturedRecipe>
{
    public void Configure(EntityTypeBuilder<FeaturedRecipe> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(fr => fr.RecipeId).IsUnique();
    }
}
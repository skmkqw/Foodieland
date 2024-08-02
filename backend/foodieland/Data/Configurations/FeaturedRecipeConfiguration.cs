using foodieland.Entities;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class FeaturedRecipeConfiguration : IEntityTypeConfiguration<FeaturedRecipeEntity>
{
    public void Configure(EntityTypeBuilder<FeaturedRecipeEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(fr => fr.RecipeId).IsUnique();
    }
}
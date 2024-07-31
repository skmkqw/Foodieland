using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class LikedRecipeConfiguration : IEntityTypeConfiguration<LikedRecipe>
{
    public void Configure(EntityTypeBuilder<LikedRecipe> builder)
    {
        builder.ToTable("RecipeLikes");

        builder.HasKey(rl => new { rl.UserId, rl.RecipeId });

        builder.HasIndex(rl => rl.UserId);
        builder.HasIndex(rl => rl.RecipeId);

        builder.HasOne(rl => rl.User)
            .WithMany(u => u.LikedRecipes)
            .HasForeignKey(rl => rl.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(rl => rl.Recipe)
            .WithMany(r => r.Likes)
            .HasForeignKey(rl => rl.RecipeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
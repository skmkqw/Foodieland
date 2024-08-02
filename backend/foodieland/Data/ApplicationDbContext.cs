using foodieland.Data.Configurations;
using foodieland.Entities;
using foodieland.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }
    
    public DbSet<RecipeEntity> Recipes { get; set; }
    
    public DbSet<IngredientQuantityEntity> IngredientQuantities { get; set; }
    
    public DbSet<CookingDirectionEntity> CookingDirections { get; set; }
    
    public DbSet<NutritionInformationEntity> NutritionInformation { get; set; }
    
    public DbSet<IngredientEntity> Ingredients { get; set; }
    
    public DbSet<LikedRecipeEntity> LikedRecipes { get; set; }
    
    public DbSet<FeaturedRecipeEntity> FeaturedRecipes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientQuantityConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new CookingDirectionConfiguration());
        modelBuilder.ApplyConfiguration(new NutritionInformationConfiguration());
        modelBuilder.ApplyConfiguration(new LikedRecipeConfiguration());
        modelBuilder.ApplyConfiguration(new FeaturedRecipeConfiguration());
    }
}
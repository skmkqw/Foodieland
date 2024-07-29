using foodieland.Data.Configurations;
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
    
    public DbSet<Recipe> Recipes { get; set; }
    
    public DbSet<IngredientQuantity> IngredientQuantities { get; set; }
    
    public DbSet<CookingDirection> CookingDirections { get; set; }
    
    public DbSet<NutritionInformation> NutritionInformation { get; set; }
    
    public DbSet<Ingredient> Ingredients { get; set; }
    
    public DbSet<FeaturedRecipe> FeaturedRecipes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientQuantityConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new CookingDirectionConfiguration());
        modelBuilder.ApplyConfiguration(new NutritionInformationConfiguration());
        modelBuilder.ApplyConfiguration(new FeaturedRecipeConfiguration());
    }
}
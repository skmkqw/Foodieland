using foodieland.Data.Configurations;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
    {
    }
    
    public DbSet<Recipe> Recipes { get; set; }
    
    public DbSet<IngredientQuantity> IngredientQuantities { get; set; }
    
    public DbSet<Ingredient> Ingredients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientQuantityConfiguration());
        modelBuilder.ApplyConfiguration(new IngredientConfiguration());
        modelBuilder.ApplyConfiguration(new CookingDirectionConfiguration());
        modelBuilder.ApplyConfiguration(new NutritionInformationConfiguration());
    }
}
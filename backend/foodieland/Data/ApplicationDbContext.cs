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

        modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = new Guid("609f2244-2ca3-41f2-9e5f-ff27d893df65"),
                    Name = "Vegan Salad",
                    Description = "A healthy vegan salad.",
                    CreationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    TimeToCook = 15,
                    Category = RecipeCategories.Vegan
                },
                new Recipe
                {
                    Id = new Guid("691e4d29-9cb9-40ad-a0af-b4e20e036116"),
                    Name = "Chicken Curry",
                    Description = "A spicy chicken curry.",
                    CreationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    TimeToCook = 45,
                    Category = RecipeCategories.Chicken
                }
        );

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient
            {
                Id = new Guid("007b825e-a1c4-4d41-9bef-4cd94d7800b0"),
                Name = "Lettuce"
            },
            new Ingredient
            {
                Id = new Guid("6f65d0f2-a7a5-41c2-af0a-cfa708f51fc7"),
                Name = "Tomato"
            },
            new Ingredient
            {
                Id = new Guid("dd55872d-eb4f-43f4-8d75-38a55ad726bd"),
                Name = "Chicken Breast"
            },
            new Ingredient
            {
                Id = new Guid("01f84a9b-957b-46ff-86e2-3a1274ef56f"),
                Name = "Curry Powder"
            }
        );

        modelBuilder.Entity<IngredientQuantity>().HasData(
            new IngredientQuantity
            {
                RecipeId = new Guid("609f2244-2ca3-41f2-9e5f-ff27d893df65"),
                IngredientId = new Guid("007b825e-a1c4-4d41-9bef-4cd94d7800b0"),
                Quantity = 100,
                Unit = "g"
            },
            new IngredientQuantity
            {
                RecipeId = new Guid("609f2244-2ca3-41f2-9e5f-ff27d893df65"),
                IngredientId = new Guid("6f65d0f2-a7a5-41c2-af0a-cfa708f51fc7"),
                Quantity = 200,
                Unit = "g"
            },
            new IngredientQuantity
            {
                RecipeId = new Guid("691e4d29-9cb9-40ad-a0af-b4e20e036116"),
                IngredientId = new Guid("dd55872d-eb4f-43f4-8d75-38a55ad726bd"),
                Quantity = 300,
                Unit = "g"
            },
            new IngredientQuantity
            {
                RecipeId = new Guid("691e4d29-9cb9-40ad-a0af-b4e20e036116"),
                IngredientId = new Guid("01f84a9b-957b-46ff-86e2-3a1274ef56f"),
                Quantity = 2,
                Unit = "Tablespoon"
            }
        );
    }
}
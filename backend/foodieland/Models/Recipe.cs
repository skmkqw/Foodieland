namespace foodieland.Models;

public enum RecipeCategories
{
    Vegan,
    Breakfast,
    Lunch,
    Meat,
    Chicken,
    Fish,
    Diet,
    Dessert,
    Chocolate,
    Asian,
    Mediterranean,
    Italian,
    Mexican,
    Indian,
    Healthy,
    ComfortFood,
    GlutenFree,
    Keto,
    LowCalorie,
    SoupsAndStews,
    Salads,
    Appetizers,
    SideDishes,
    Beverages,
    Baking,
    Barbecue,
    Seafood,
    Holiday,
    Regional,
    Fusion,
    Dinner
}

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public uint TimeToCook { get; set; }
    
    public RecipeCategories Category { get; set; }
    
    public byte[]? ImageData { get; set; }

    public List<IngredientQuantity> Ingredients { get; set; } = new ();

    public List<CookingDirection> Directions { get; set; } = new ();

    public NutritionInformation? NutritionInformation { get; set; }

    public Guid CreatorId { get; set; }

    public AppUser Creator { get; set; }

    public bool IsPublished { get; set; }
  
    public List<LikedRecipe> Likes { get; set; } = new();
}
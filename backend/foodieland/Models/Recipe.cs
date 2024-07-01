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
    Asian
}
public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public uint TimeToCook { get; set; }
    
    public RecipeCategories Category { get; set; }

    public List<IngredientQuantity>? Ingredients { get; set; } = null;

    public List<CookingDirection>? Directions { get; set; } = null;

    public NutritionInformation? NutritionInformation { get; set; } = null;

    public Guid CreatorId { get; set; }

    public AppUser Creator { get; set; }
}
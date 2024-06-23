namespace foodieland.Models;

public class IngredientQuantity
{
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = new ();
    
    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = new ();

    public double Quantity { get; set; }

    public string Unit { get; set; } = string.Empty;
}
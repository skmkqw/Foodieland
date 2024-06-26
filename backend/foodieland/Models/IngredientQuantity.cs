using System.Text.Json.Serialization;

namespace foodieland.Models;

public class IngredientQuantity
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    
    [JsonIgnore]
    public Recipe Recipe { get; set; } = new ();
    
    public Guid IngredientId { get; set; }
    
    [JsonIgnore]
    public Ingredient Ingredient { get; set; } = new ();

    public double Quantity { get; set; }

    public string Unit { get; set; } = string.Empty;
}
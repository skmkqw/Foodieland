namespace foodieland.Models;

public class Ingredient
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<IngredientQuantity> IngredientQuantities { get; set; }
}
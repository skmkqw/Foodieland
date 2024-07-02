namespace foodieland.DTO.IngredientQuantities;

public class IngredientDto
{
    public Guid Id { get; set; }

    public string IngredientName { get; set; }
    
    public double Quantity { get; set; }

    public string Unit { get; set; } = string.Empty;
    
    public Guid RecipeId { get; set; }
}
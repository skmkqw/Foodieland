namespace foodieland.DTO.IngredientQuantities;

public class AddOrUpdateIngredientDto
{
    public string IngredientName { get; set; }

    public double Quantity { get; set; }

    public string Unit { get; set; } = string.Empty;
}
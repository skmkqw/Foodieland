using foodieland.Models;

namespace foodieland.DTO.IngredientQuantities;

public class IngredientDto
{
    public Guid Id { get; set; }

    public string IngredientName { get; set; }
    
    public double Quantity { get; set; }

    public MeasurementUnit Unit { get; set; } = MeasurementUnit.Gram;
}
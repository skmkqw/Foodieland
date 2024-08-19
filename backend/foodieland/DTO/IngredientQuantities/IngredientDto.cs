using foodieland.Models;

namespace foodieland.DTO.IngredientQuantities;

public class IngredientDto
{
    public string Name { get; set; }
    
    public double Amount { get; set; }

    public MeasurementUnit Unit { get; set; } = MeasurementUnit.Gram;
}
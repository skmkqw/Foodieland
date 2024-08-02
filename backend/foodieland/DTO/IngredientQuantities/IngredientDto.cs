using foodieland.Common;

namespace foodieland.DTO.IngredientQuantities;

public class IngredientDto
{
    public Guid Id { get; set; }

    public string IngredientName { get; set; }
    
    public double Quantity { get; set; }

    public MeasurementUnits Units { get; set; } = MeasurementUnits.Gram;
}
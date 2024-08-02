using foodieland.Common;

namespace foodieland.DTO.IngredientQuantities;

public class AddOrUpdateIngredientDto
{
    public string IngredientName { get; set; }

    public double Quantity { get; set; }

    public MeasurementUnits Unit { get; set; } = MeasurementUnits.Gram;
}
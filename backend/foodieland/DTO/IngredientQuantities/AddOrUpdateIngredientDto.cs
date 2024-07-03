using foodieland.Models;

namespace foodieland.DTO.IngredientQuantities;

public class AddOrUpdateIngredientDto
{
    public string IngredientName { get; set; }

    public double Quantity { get; set; }

    public MeasurementUnit Unit { get; set; } = MeasurementUnit.Gram;
}
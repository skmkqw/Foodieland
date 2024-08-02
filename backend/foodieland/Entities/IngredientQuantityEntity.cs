using System.Text.Json.Serialization;
using foodieland.Common;

namespace foodieland.Entities;

public class IngredientQuantityEntity
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; init; }
    
    [JsonIgnore]
    public RecipeEntity RecipeEntity { get; set; } = new ();
    
    public Guid IngredientId { get; set; }
    
    [JsonIgnore]
    public IngredientEntity IngredientEntity { get; set; } = new ();

    public double Quantity { get; set; }

    public MeasurementUnits Unit { get; set; } = MeasurementUnits.Gram;
}
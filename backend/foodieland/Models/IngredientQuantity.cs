using foodieland.Common;
using foodieland.DTO.IngredientQuantities;

namespace foodieland.Models;

public class IngredientQuantity
{
    public Guid Id { get; } = Guid.NewGuid();

    public Recipe Recipe { get; }
    
    public Ingredient Ingredient { get; private set; }

    public double Quantity { get; private set; }

    public MeasurementUnits Unit { get; private set; }

    //TODO Update DTOs to pass new Ingredient instead of IngredientName
    private IngredientQuantity(AddOrUpdateIngredientDto addIngredientDto)
    {
        if (addIngredientDto.Quantity >= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(addIngredientDto.Quantity));
        
        Unit = addIngredientDto.Unit;
        Quantity = addIngredientDto.Quantity;
    }

    public static IngredientQuantity Create(AddOrUpdateIngredientDto addIngredientDto)
    {
        return new IngredientQuantity(addIngredientDto);
    }
    
    //TODO Update DTOs to pass new Ingredient instead of IngredientName
    public void Update(AddOrUpdateIngredientDto updateIngredientDto)
    {
        if (updateIngredientDto.Quantity >= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(updateIngredientDto.Quantity));
        
        Unit = updateIngredientDto.Unit;
        Quantity = updateIngredientDto.Quantity;
    }
}
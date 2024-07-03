using foodieland.DTO.IngredientQuantities;
using foodieland.Models;

namespace foodieland.Mappers;

public static class IngredientQuantityMapper
{
    public static IngredientQuantity ToIngredientQuantity(this AddOrUpdateIngredientDto addOrUpdateIngredientDto, Recipe recipe, Ingredient ingredient)
    {
        return new IngredientQuantity()
        {
            Id = Guid.NewGuid(),
            Quantity = addOrUpdateIngredientDto.Quantity,
            Unit = addOrUpdateIngredientDto.Unit.Trim().ToLower(),
            RecipeId = recipe.Id,
            Recipe = recipe,
            IngredientId = ingredient.Id,
            Ingredient = ingredient
        };
    }

    public static IngredientDto ToIngredientDto(this IngredientQuantity ingredientQuantity)
    {
        return new IngredientDto()
        {
            Id = ingredientQuantity.Id,
            IngredientName = ingredientQuantity.Ingredient.Name,
            Quantity = ingredientQuantity.Quantity,
            Unit = ingredientQuantity.Unit
        };
    }
}
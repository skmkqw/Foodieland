using foodieland.DTO.IngredientQuantities;
using foodieland.Entities;
using foodieland.Models;

namespace foodieland.Mappers;

public static class IngredientQuantityMapper
{
    public static IngredientQuantityEntity ToIngredientQuantity(this AddOrUpdateIngredientDto addOrUpdateIngredientDto, RecipeEntity recipeEntity, IngredientEntity ingredientEntity)
    {
        return new IngredientQuantityEntity()
        {
            Id = Guid.NewGuid(),
            Quantity = addOrUpdateIngredientDto.Quantity,
            Unit = addOrUpdateIngredientDto.Unit,
            RecipeId = recipeEntity.Id,
            RecipeEntity = recipeEntity,
            IngredientId = ingredientEntity.Id,
            IngredientEntity = ingredientEntity
        };
    }

    public static IngredientDto ToIngredientDto(this IngredientQuantityEntity ingredientQuantityEntity)
    {
        return new IngredientDto()
        {
            Id = ingredientQuantityEntity.Id,
            IngredientName = ingredientQuantityEntity.IngredientEntity.Name,
            Quantity = ingredientQuantityEntity.Quantity,
            Units = ingredientQuantityEntity.Unit
        };
    }
}
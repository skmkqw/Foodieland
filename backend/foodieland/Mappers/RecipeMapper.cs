using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Models;

namespace foodieland.Mappers;

public static class RecipeMapper
{
    public static Recipe ToRecipe(this AddOrUpdateRecipeDto addOrUpdateRecipeDto, Guid creatorId)
    {
        return new Recipe()
        {
            Name = addOrUpdateRecipeDto.Name,
            Description = addOrUpdateRecipeDto.Description,
            TimeToCook = addOrUpdateRecipeDto.TimeToCook,
            Category = addOrUpdateRecipeDto.Category,
            CreatorId = creatorId
        };
    }

    public static RecipeDto ToRecipeDto(this Recipe recipe, List<CookingDirectionDto>? cookingDirections = null, NutritionInformationDto? nutritionInformation = null, List<IngredientDto>? ingredients = null)
    {
        return new RecipeDto()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Category = recipe.Category,
            TimeToCook = recipe.TimeToCook,
            NutritionInformation = nutritionInformation,
            CreatorId = recipe.CreatorId,
            CreationDate = recipe.CreationDate,
            Directions = cookingDirections,
            Ingredients = ingredients,
            IsPublished = recipe.IsPublished
        };
    }
}
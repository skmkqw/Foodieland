using foodieland.DTO.Recipes;
using foodieland.Models;

namespace foodieland.Mappers;

public static class RecipeMapper
{
    public static Recipe FromCreateDtoToRecipe(this AddOrUpdateRecipeDto addOrUpdateRecipeDto, Guid creatorId)
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

    public static RecipeDto FromRecipeToDto(this Recipe recipe)
    {
        return new RecipeDto()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Category = recipe.Category,
            TimeToCook = recipe.TimeToCook
        };
    }
}
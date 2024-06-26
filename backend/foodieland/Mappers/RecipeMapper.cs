using foodieland.DTO.Recipes;
using foodieland.Models;

namespace foodieland.Mappers;

public static class RecipeMapper
{
    public static Recipe FromCreateDtoToRecipe(this CreateRecipeDto createRecipeDto, Guid creatorId)
    {
        return new Recipe()
        {
            Name = createRecipeDto.Name,
            Description = createRecipeDto.Description,
            TimeToCook = createRecipeDto.TimeToCook,
            Category = createRecipeDto.Category,
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
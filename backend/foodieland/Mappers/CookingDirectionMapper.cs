using foodieland.DTO.CookingDirection;
using foodieland.Models;

namespace foodieland.Mappers;

public static class CookingDirectionMapper
{
    public static CookingDirectionDto ToCookingDirectionDto(this CookingDirection cookingDirection)
    {
        return new CookingDirectionDto()
        {
            Description = cookingDirection.Descirption,
            StepNumber = cookingDirection.StepNumber,
            Name = cookingDirection.Title
        };
    }

    public static CookingDirection ToCookingDirection(this AddOrUpdateCookingDirectionDto addOrUpdateCookingDirectionDto, Guid recipeId)
    {
        return new CookingDirection()
        {
            Id = Guid.NewGuid(),
            Descirption = addOrUpdateCookingDirectionDto.Description,
            Title = addOrUpdateCookingDirectionDto.Title,
            StepNumber = addOrUpdateCookingDirectionDto.StepNumber,
            RecipeId = recipeId
        };
    }
}

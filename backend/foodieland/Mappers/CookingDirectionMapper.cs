using foodieland.DTO.CookingDirection;
using foodieland.Entities;
using foodieland.Models;

namespace foodieland.Mappers;

public static class CookingDirectionMapper
{
    public static CookingDirectionDto ToCookingDirectionDto(this CookingDirectionEntity cookingDirectionEntity)
    {
        return new CookingDirectionDto()
        {
            Id = cookingDirectionEntity.Id,
            Description = cookingDirectionEntity.Descirption,
            StepNumber = cookingDirectionEntity.StepNumber,
            Title = cookingDirectionEntity.Title
        };
    }

    public static CookingDirectionEntity ToCookingDirection(this AddOrUpdateCookingDirectionDto addOrUpdateCookingDirectionDto, Guid recipeId)
    {
        return new CookingDirectionEntity()
        {
            Id = Guid.NewGuid(),
            Descirption = addOrUpdateCookingDirectionDto.Description,
            Title = addOrUpdateCookingDirectionDto.Title,
            StepNumber = addOrUpdateCookingDirectionDto.StepNumber,
            RecipeId = recipeId
        };
    }
}

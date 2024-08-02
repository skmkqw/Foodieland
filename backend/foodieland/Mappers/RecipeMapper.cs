using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Entities;
using foodieland.Models;

namespace foodieland.Mappers;

public class RecipeMapperParams
{
    public List<CookingDirectionDto>? CookingDirections { get; init; }
    public NutritionInformationDto? NutritionInformation { get; init; }
    public List<IngredientDto>? Ingredients { get; init; }
    public bool IsLiked { get; init; }
}

public static class RecipeMapper
{
    public static RecipeEntity ToRecipe(this AddOrUpdateRecipeDto addOrUpdateRecipeDto, Guid creatorId)
    {
        return new RecipeEntity()
        {
            Name = addOrUpdateRecipeDto.Name,
            Description = addOrUpdateRecipeDto.Description,
            TimeToCook = addOrUpdateRecipeDto.TimeToCook,
            Category = addOrUpdateRecipeDto.Category,
            CreatorId = creatorId
        };
    }

    public static RecipeDto ToRecipeDto(this RecipeEntity recipeEntity, RecipeMapperParams? mapperParams)
    {
        return new RecipeDto()
        {
            Id = recipeEntity.Id,
            Name = recipeEntity.Name,
            Description = recipeEntity.Description,
            Category = recipeEntity.Category,
            TimeToCook = recipeEntity.TimeToCook,
            NutritionInformation = mapperParams?.NutritionInformation,
            CreatorId = recipeEntity.CreatorId,
            CreationDate = recipeEntity.CreationDate,
            Directions = mapperParams?.CookingDirections,
            Ingredients = mapperParams?.Ingredients,
            IsPublished = recipeEntity.IsPublished,
            IsLiked = mapperParams?.IsLiked ?? false
        };
    }

    public static FeaturedRecipeDto ToFeaturedDto(this RecipeEntity recipeEntity)
    {
        return new FeaturedRecipeDto()
        {
            Id = recipeEntity.Id.ToString(),
            Name = recipeEntity.Name,
            Description = recipeEntity.Description,
            TimeToCook = recipeEntity.TimeToCook,
            Category = recipeEntity.Category.ToString(),
            CreatorName = $"{recipeEntity.Creator.FirstName} {recipeEntity.Creator.LastName}",
            CreationDate = recipeEntity.CreationDate.ToString("dd MMM, yyyy")
        };
    }
}
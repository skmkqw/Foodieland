using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Models;
using foodieland.Utils;

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

    public static RecipeDto ToRecipeDto(this Recipe recipe, RecipeMapperParams? mapperParams)
    {
        return new RecipeDto()
        {
            Recipe = new RecipeDetailsDto()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Category = recipe.Category,
                TimeToCook = recipe.TimeToCook,
                CreationDate = recipe.CreationDate.ToString("dd MMM, yyyy"),
                ImageData = ImageConverter.ConvertByteArrayToBase64String(recipe.ImageData),
                IsLiked = mapperParams?.IsLiked ?? false,
            },
            Creator = new RecipeCreatorDto()
            {
                CreatorId = recipe.CreatorId,
                CreatorName = recipe.Creator.FirstName + " " + recipe.Creator.LastName,
                UserImage = ImageConverter.ConvertByteArrayToBase64String(recipe.Creator.ProfileImage),
            },
            NutritionInformation = mapperParams?.NutritionInformation,
            Directions = mapperParams?.CookingDirections,
            Ingredients = mapperParams?.Ingredients,
        };
    }

    public static ShortRecipeDTo ToShortRecipeDto(this Recipe recipe, bool isLiked)
    {
        return new ShortRecipeDTo()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Category = recipe.Category,
            TimeToCook = recipe.TimeToCook,
            IsLiked = isLiked,
            ImageData = ImageConverter.ConvertByteArrayToBase64String(recipe.ImageData)
        };
    }

    public static FeaturedRecipeDto ToFeaturedDto(this Recipe recipe)
    {
        return new FeaturedRecipeDto()
        {
            Id = recipe.Id.ToString(),
            Name = recipe.Name,
            Description = recipe.Description,
            TimeToCook = recipe.TimeToCook,
            Category = recipe.Category.ToString(),
            CreatorName = $"{recipe.Creator.FirstName} {recipe.Creator.LastName}",
            CreationDate = recipe.CreationDate.ToString("dd MMM, yyyy"),
            ImageData = ImageConverter.ConvertByteArrayToBase64String(recipe.ImageData),
            UserImage = ImageConverter.ConvertByteArrayToBase64String(recipe.Creator.ProfileImage)
        };
    }
}
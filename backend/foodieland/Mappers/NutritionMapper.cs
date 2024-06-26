using foodieland.DTO.NutritionInformation;
using foodieland.Models;

namespace foodieland.Mappers;

public static class NutritionMapper
{
    public static NutritionInformation FromAddOrUpdateDtoToNutritionInformation(
        this AddOrUpdateNutritionDto addOrUpdateNutritionDto, Guid recipeId)
    {
        return new NutritionInformation()
        {
            Calories = addOrUpdateNutritionDto.Calories,
            Carbohydrate = addOrUpdateNutritionDto.Carbohydrate,
            Cholesterol = addOrUpdateNutritionDto.Cholesterol,
            Fat = addOrUpdateNutritionDto.Fat,
            Protein = addOrUpdateNutritionDto.Protein,
            RecipeId = recipeId
        };
    }

    public static NutritionInformationDto ToNutritionDto(this NutritionInformation nutritionInformation)
    {
        return new NutritionInformationDto()
        {
            Id = nutritionInformation.Id,
            Calories = nutritionInformation.Calories,
            Carbohydrate = nutritionInformation.Carbohydrate,
            Cholesterol = nutritionInformation.Cholesterol,
            Fat = nutritionInformation.Fat,
            Protein = nutritionInformation.Protein,
            RecipeId = nutritionInformation.RecipeId
        };
    }
}
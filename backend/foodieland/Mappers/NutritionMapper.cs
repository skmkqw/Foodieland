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
            Fat = addOrUpdateNutritionDto.Fat,
            Protein = addOrUpdateNutritionDto.Protein,
            RecipeId = recipeId
        };
    }

    public static NutritionInformationDto ToNutritionDto(this NutritionInformation nutritionInformation)
    {
        return new NutritionInformationDto()
        {
            Calories = nutritionInformation.Calories,
            Carbohydrate = nutritionInformation.Carbohydrate,
            Fat = nutritionInformation.Fat,
            Protein = nutritionInformation.Protein,
        };
    }
}
using foodieland.DTO.NutritionInformation;
using foodieland.Entities;
using foodieland.Models;

namespace foodieland.Mappers;

public static class NutritionMapper
{
    public static NutritionInformationEntity FromAddOrUpdateDtoToNutritionInformation(
        this AddOrUpdateNutritionDto addOrUpdateNutritionDto, Guid recipeId)
    {
        return new NutritionInformationEntity()
        {
            Calories = addOrUpdateNutritionDto.Calories,
            Carbohydrate = addOrUpdateNutritionDto.Carbohydrate,
            Cholesterol = addOrUpdateNutritionDto.Cholesterol,
            Fat = addOrUpdateNutritionDto.Fat,
            Protein = addOrUpdateNutritionDto.Protein,
            RecipeId = recipeId
        };
    }

    public static NutritionInformationDto ToNutritionDto(this NutritionInformationEntity nutritionInformationEntity)
    {
        return new NutritionInformationDto()
        {
            Id = nutritionInformationEntity.Id,
            Calories = nutritionInformationEntity.Calories,
            Carbohydrate = nutritionInformationEntity.Carbohydrate,
            Cholesterol = nutritionInformationEntity.Cholesterol,
            Fat = nutritionInformationEntity.Fat,
            Protein = nutritionInformationEntity.Protein,
        };
    }
}
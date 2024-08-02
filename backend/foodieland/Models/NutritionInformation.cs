using foodieland.DTO.NutritionInformation;

namespace foodieland.Models;

public class NutritionInformation
{
    public Guid Id { get; } = Guid.NewGuid();

    public double Calories { get; private set; }
    
    public double Fat { get; private set; }
    
    public double Protein { get; private set; }
    
    public double Carbohydrate { get; private set; }
    
    public double Cholesterol { get; private set; }

    public Recipe Recipe { get; }

    //TODO Update Dto to set Recipe
    private NutritionInformation(AddOrUpdateNutritionDto addNutritionDto)
    {
        Calories = addNutritionDto.Calories;
        Fat = addNutritionDto.Fat;
        Protein = addNutritionDto.Protein;
        Carbohydrate = addNutritionDto.Carbohydrate;
        Cholesterol = addNutritionDto.Cholesterol;
    }

    public static NutritionInformation Create(AddOrUpdateNutritionDto addNutritionDto)
    {
        if (addNutritionDto.Calories < 0)
            throw new ArgumentException("Calorie amount must be 0 or greater", nameof(addNutritionDto.Calories));

        if (addNutritionDto.Fat < 0)
            throw new ArgumentException("Fat amount must be 0 or greater", nameof(addNutritionDto.Fat));

        if (addNutritionDto.Protein < 0)
            throw new ArgumentException("Protein amount must be 0 or greater", nameof(addNutritionDto.Protein));

        if (addNutritionDto.Cholesterol < 0)
            throw new ArgumentException("Cholesterol amount must be 0 or greater", nameof(addNutritionDto.Cholesterol));

        if (addNutritionDto.Carbohydrate < 0)
            throw new ArgumentException("Carbohydrate amount must be 0 or greater", nameof(addNutritionDto.Carbohydrate));

        return new NutritionInformation(addNutritionDto);
    }
}
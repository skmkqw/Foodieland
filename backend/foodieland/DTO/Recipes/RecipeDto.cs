using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.Models;

namespace foodieland.DTO.Recipes;

public class RecipeDto
{
    public RecipeDetailsDto Recipe { get; set; }

    public RecipeCreatorDto Creator { get; set; }

    public NutritionInformationDto? NutritionInformation { get; set; }
    
    public List<IngredientDto>? Ingredients { get; set; }
    
    public List<CookingDirectionDto>? Directions { get; set; }
}
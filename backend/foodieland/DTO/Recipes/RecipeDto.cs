using foodieland.DTO.NutritionInformation;
using foodieland.Models;

namespace foodieland.DTO.Recipes;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public uint TimeToCook { get; set; }
    
    public RecipeCategories Category { get; set; }

    public NutritionInformationDto? NutritionInformation { get; set; }
}
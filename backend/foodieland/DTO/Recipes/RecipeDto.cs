using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
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
    
    public List<CookingDirectionDto>? Directions { get; set; }
    
    public List<IngredientDto>? Ingredients { get; set; }
    
    public Guid CreatorId { get; set; }
    
    public DateOnly CreationDate { get; set; } 
}
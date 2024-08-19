using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.Models;

namespace foodieland.DTO.Recipes;

public class RecipeCreatorDto
{
    public Guid CreatorId { get; set; }

    public string CreatorName { get; set; }
}

public class RecipeDetailsDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public uint TimeToCook { get; set; }

    public RecipeCategories Category { get; set; }
    
    public DateOnly CreationDate { get; set; } 
    
    public string? ImageData { get; set; }
    
    public bool IsLiked { get; set; }
}


public class RecipeDto
{
    public RecipeDetailsDto Recipe { get; set; }

    public RecipeCreatorDto Creator { get; set; }

    public NutritionInformationDto? NutritionInformation { get; set; }
    
    public List<IngredientDto>? Ingredients { get; set; }
    
    public List<CookingDirectionDto>? Directions { get; set; }
}
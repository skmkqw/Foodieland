using foodieland.Entities;

namespace foodieland.DTO.Recipes;

public class AddOrUpdateRecipeDto
{ 
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public uint TimeToCook { get; set; }
    
    public RecipeCategories Category { get; set; }
}
using foodieland.Models;

namespace foodieland.DTO.Recipes;

public class ShortRecipeDTo
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public uint TimeToCook { get; set; }
    
    public RecipeCategories Category { get; set; }
    
    public bool IsLiked { get; set; }
}
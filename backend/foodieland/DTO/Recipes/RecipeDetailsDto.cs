using foodieland.Models;

namespace foodieland.DTO.Recipes;

public class RecipeDetailsDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public uint TimeToCook { get; set; }

    public RecipeCategories Category { get; set; }
    
    public string CreationDate { get; set; } 
    
    public string? ImageData { get; set; }
    
    public bool IsLiked { get; set; }
}
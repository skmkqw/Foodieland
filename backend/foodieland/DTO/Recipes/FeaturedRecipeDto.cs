namespace foodieland.DTO.Recipes;

public class FeaturedRecipeDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public uint TimeToCook { get; set; }

    public string Category { get; set; } = string.Empty;
    
    public string CreatorName { get; set; } = string.Empty;

    public string CreationDate { get; set; } = string.Empty;
}
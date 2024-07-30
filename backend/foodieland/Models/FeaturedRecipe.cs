namespace foodieland.Models;

public class FeaturedRecipe
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }

    public Recipe Recipe { get; set; }
}
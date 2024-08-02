namespace foodieland.Entities;

public class FeaturedRecipeEntity
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }

    public RecipeEntity RecipeEntity { get; set; }
}
using foodieland.Common;
using foodieland.Models;

namespace foodieland.Entities;
public class RecipeEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public uint TimeToCook { get; set; }
    
    public RecipeCategories Category { get; set; }

    public List<IngredientQuantityEntity> Ingredients { get; set; } = new ();

    public List<CookingDirectionEntity> Directions { get; set; } = new ();

    public NutritionInformationEntity? NutritionInformation { get; set; }

    public Guid CreatorId { get; set; }

    public AppUser Creator { get; set; }

    public bool IsPublished { get; set; }
  
    public List<LikedRecipeEntity> Likes { get; set; } = new();
}
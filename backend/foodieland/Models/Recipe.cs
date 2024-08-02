
using System.ComponentModel.DataAnnotations;
using foodieland.DTO.Recipes;

namespace foodieland.Models;

public class Recipe
{
    public Guid Id { get; } = Guid.NewGuid();
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public DateOnly CreationDate { get; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    public uint TimeToCook { get; private set; }
    
    public AppUser Creator { get; }
    
    public bool IsPublished { get; private set; }
    
    public List<IngredientQuantity> Ingredients { get; private set; } = new();
    public List<CookingDirection> Directions { get; private set; } = new();
    public NutritionInformation? NutritionInformation { get; private set; }
    
    public List<LikedRecipe> Likes { get; private set; } = new();

    private Recipe(AddOrUpdateRecipeDto addRecipeDto, AppUser creator)
    {
        Name = addRecipeDto.Name;
        Description = addRecipeDto.Description;
        TimeToCook = addRecipeDto.TimeToCook;
        Creator = creator;
        IsPublished = false;
    }

    public static Recipe Create(AddOrUpdateRecipeDto addRecipeDto, AppUser creator)
    {
        if (string.IsNullOrWhiteSpace(addRecipeDto.Name))
            throw new ArgumentException("Name cannot be empty", nameof(addRecipeDto.Name));
        
        if (addRecipeDto.Name.Length > 200)
            throw new ArgumentException("Name cannot exceed 200 characters", nameof(addRecipeDto.Name));
        
        if (string.IsNullOrWhiteSpace(addRecipeDto.Description))
            throw new ArgumentException("Description cannot be empty", nameof(addRecipeDto.Description));
        
        if (addRecipeDto.Description.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters", nameof(addRecipeDto.Description));

        return new Recipe(addRecipeDto, creator);
    }
    

    public void Publish()
    {
        if (!IsReadyToPublish()) 
            throw new ValidationException("Recipe doesn't meet publishing requirements");
        IsPublished = true;
    }

    public void Unpublish() =>  IsPublished = false;
       
    private bool IsReadyToPublish() => Directions.Count > 0 && Ingredients.Count > 0 && NutritionInformation != null;
}
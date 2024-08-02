using foodieland.Entities;
using Microsoft.AspNetCore.Identity;

namespace foodieland.Models;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public List<RecipeEntity> Recipes { get; set; } = new();
    
    public List<LikedRecipeEntity> LikedRecipes { get; set; } = new();
}
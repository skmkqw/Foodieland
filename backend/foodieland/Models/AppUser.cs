using Microsoft.AspNetCore.Identity;

namespace foodieland.Models;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public byte[]? ProfileImage { get; set; }

    public List<Recipe> Recipes { get; set; } = new();
    
    public List<LikedRecipe> LikedRecipes { get; set; } = new();
}
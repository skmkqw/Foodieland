using Microsoft.AspNetCore.Identity;

namespace foodieland.Models;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<Recipe> Recipes { get; set; } = new();
}
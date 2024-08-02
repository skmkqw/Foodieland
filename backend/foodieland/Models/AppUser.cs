using foodieland.Entities;
using Microsoft.AspNetCore.Identity;

namespace foodieland.Models;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }

    public List<RecipeEntity> Recipes { get; private set; } = new();
    
    public List<LikedRecipeEntity> LikedRecipes { get; private set; } = new();

    private AppUser(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static AppUser Create(string firstName, string lastName, string email, string securityStamp)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty", nameof(firstName));
        
        if (firstName.Length > 100)
            throw new ArgumentException("First name cannot exceed 100 characters", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));
        
        if (lastName.Length > 100)
            throw new ArgumentException("Last naem cannot exceed 100 characters", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        
        if (string.IsNullOrWhiteSpace(securityStamp))
            throw new ArgumentException("Security stamp cannot be empty", nameof(securityStamp));

        var user = new AppUser(firstName, lastName)
        {
            UserName = email,
            Email = email,
            SecurityStamp = securityStamp
        };

        return user;
    }
}
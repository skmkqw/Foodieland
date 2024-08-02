namespace foodieland.Models;

public class LikedRecipe
{
    public Guid UserId => User.Id;
        
    public Guid RecipeId => Recipe.Id;

    public AppUser User { get; private set; }

    public Recipe Recipe { get; private set; }

    private LikedRecipe(AppUser user, Recipe recipe)
    {
        User = user;
        Recipe = recipe;
    }

    public static LikedRecipe Create(AppUser user, Recipe recipe)
    {
        return new LikedRecipe(user, recipe);
    }
}
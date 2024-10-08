using foodieland.Models;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository
{
    public async Task<(bool isPublished, string[]? errors)> Publish(Recipe recipe)
    {
        (bool isReadyToPublish, string[]? verificationErrors) = await VerifyRecipe(recipe);
        if (!isReadyToPublish)
        {
            return (false, verificationErrors!.ToArray());
        }
        
        recipe.IsPublished = true;
        await _context.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool isHidden, string? error)> Hide(Recipe recipe)
    {
        if (recipe.IsPublished == false)
        {
            return (false, "Recipe is already hidden");
        }

        recipe.IsPublished = false;
        await _context.SaveChangesAsync();
        return (true, null);
    }
    
    private async Task<(bool isOk, string[]? errors)> VerifyRecipe(Recipe recipe)
    {
        List<string> errors = new ();
        var imageData = recipe.ImageData;
        if (imageData == null)
        {
            errors.Add("Recipe must have a cover image");
        }
        var nutritionInformation = await GetNutritionInformation(recipe.Id);
        if (nutritionInformation == null)
        {
            errors.Add("Recipe must have nutrition information");
        }
        var cookingDirections = await GetCookingDirections(recipe.Id);
        if (cookingDirections == null || cookingDirections.Count == 0)
        {
            errors.Add("Recipe must have at least 1 cooking direction");
        }
        var ingredients = await GetIngredients(recipe.Id);
        if (ingredients == null || ingredients.Count == 0)
        {
            errors.Add("Recipe must have at least 1 ingredient");
        }
        return (errors.Count == 0, errors.ToArray());
    }
}
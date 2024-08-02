using foodieland.Entities;

namespace foodieland.Repositories;

public partial class RecipeRepository
{
    public async Task<(bool isPublished, string[]? errors)> Publish(Guid recipeId)
    {
        List<string> errors = new ();
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            errors.Add("Recipe not found");
            return (false, errors.ToArray());
        }
        (bool isReadyToPublish, string[]? verificationErrors) = await VerifyRecipe(recipe);
        if (!isReadyToPublish)
        {
            errors.AddRange(verificationErrors!);
            return (false, errors.ToArray());
        }
        
        recipe.IsPublished = true;
        await _context.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool isHidden, string? error)> Hide(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return (false, "Recipe not found");
        }

        if (recipe.IsPublished == false)
        {
            return (false, "Recipe is already hidden");
        }

        recipe.IsPublished = false;
        await _context.SaveChangesAsync();
        return (true, null);
    }
    
    private async Task<(bool isReadyToPublish, string[]? errors)> VerifyRecipe(RecipeEntity recipeEntity)
    {
        List<string> errors = new ();
        var nutritionInformation = await GetNutritionInformation(recipeEntity.Id);
        if (nutritionInformation == null)
        {
            errors.Add("Recipe must have nutrition information");
        }
        var cookingDirections = await GetCookingDirections(recipeEntity.Id);
        if (cookingDirections == null || cookingDirections.Count == 0)
        {
            errors.Add("Recipe must have at least 1 cooking direction");
        }
        var ingredients = await GetIngredients(recipeEntity.Id);
        if (ingredients == null || ingredients.Count == 0)
        {
            errors.Add("Recipe must have at least 1 ingredient");
        }
        return (errors.Count == 0, errors.ToArray());
    }
}
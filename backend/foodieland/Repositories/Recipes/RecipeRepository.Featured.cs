using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository
{
    public async Task<List<Recipe>> GetFeatured()
    {
        return await _context.FeaturedRecipes
            .Include(r => r.Recipe)
            .ThenInclude(c => c.Creator).Select(fr => fr.Recipe)
            .ToListAsync();
    }
    
    public async Task<string?> SetFeatured(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        
        if (recipe == null) 
            return "Recipe not found";

        if (recipe.IsPublished == false)
            return "Recipe is not published";

        var existingFeaturedRecipe = await _context.FeaturedRecipes.FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        
        if (existingFeaturedRecipe != null) 
            return "This recipe is already featured";

        if (_context.FeaturedRecipes.ToList().Count >= 3) 
            return "Maximum amount of featured recipes achieved";
        
        await _context.FeaturedRecipes.AddAsync(new FeaturedRecipe { RecipeId = recipeId });
        await _context.SaveChangesAsync();

        return null;
    }

    public async Task<bool> RemoveFeatured(Guid recipeId)
    {
        var featuredRecipe = await _context.FeaturedRecipes.FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        if (featuredRecipe == null)
        {
            return false;
        }

        _context.FeaturedRecipes.Remove(featuredRecipe);
        await _context.SaveChangesAsync();
        return true;
    }
}
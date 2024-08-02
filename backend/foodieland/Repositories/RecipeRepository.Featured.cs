using foodieland.Entities;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories;

public partial class RecipeRepository
{
    public async Task<List<RecipeEntity>> GetFeatured()
    {
        return await _context.FeaturedRecipes
            .Include(r => r.RecipeEntity)
            .ThenInclude(c => c.Creator).Select(fr => fr.RecipeEntity)
            .ToListAsync();
    }
    
    public async Task<string?> SetFeatured(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return "Recipe not found";
        }

        var existingFeaturedRecipe = await _context.FeaturedRecipes.FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        if (existingFeaturedRecipe != null)
        {
            return "This recipe is already featured";
        }

        if (_context.FeaturedRecipes.ToList().Count >= 3)
        {
            return "Maximum amount of featured recipes achieved";
        }

        await _context.FeaturedRecipes.AddAsync(new FeaturedRecipeEntity { RecipeId = recipeId });
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
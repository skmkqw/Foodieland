using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository
{
    public async Task<(int totalAmount, List<Recipe> likedRecipes)> GetLikedRecipes(Guid userId, int page, int pageSize)
    {
        var totalLikes = _context.LikedRecipes.Count(l => l.UserId == userId);
        var likedRecipes = await _context.LikedRecipes
            .Include(l => l.Recipe)
            .Where(l => l.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(l => l.Recipe)
            .ToListAsync();
        return (totalLikes, likedRecipes);
    }
    public async Task<bool> AddLike(Recipe recipe, Guid userId)
    {
        if (await IsLikedByUser(recipe.Id, userId)) return false;
        var like = new LikedRecipe { UserId = userId, RecipeId = recipe.Id };
        _context.LikedRecipes.Add(like);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> RemoveLike(Recipe recipe, Guid userId)
    {
        var like = await _context.LikedRecipes
            .FirstOrDefaultAsync(l => l.UserId == userId && l.RecipeId == recipe.Id);
        if (like != null)
        {
            _context.LikedRecipes.Remove(like);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    public async Task<bool> IsLikedByUser(Guid recipeId, Guid userId)
    {
        return await _context.LikedRecipes
            .AnyAsync(l => l.UserId == userId && l.RecipeId == recipeId);
    }
    
    public async Task<List<LikedRecipe>> GetLikesByUser(Guid userId)
    {
        return await _context.LikedRecipes.Where(lr => lr.UserId == userId).ToListAsync();
    }
}
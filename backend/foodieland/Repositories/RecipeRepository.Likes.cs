using foodieland.Entities;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories;

public partial class RecipeRepository
{
    public async Task<bool> AddLike(Guid recipeId, Guid userId)
    {
        if (await IsLikedByUser(recipeId, userId) || await GetById(recipeId) == null) return false;
        var like = new LikedRecipeEntity { UserId = userId, RecipeId = recipeId };
        _context.LikedRecipes.Add(like);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> RemoveLike(Guid recipeId, Guid userId)
    {
        var like = await _context.LikedRecipes
            .FirstOrDefaultAsync(l => l.UserId == userId && l.RecipeId == recipeId);
        var recipe = await GetById(recipeId);
        if (like != null && recipe != null)
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
    
    public async Task<List<LikedRecipeEntity>> GetLikedRecipesByUser(Guid userId)
    {
        return await _context.LikedRecipes.Where(lr => lr.UserId == userId).ToListAsync();
    }
}
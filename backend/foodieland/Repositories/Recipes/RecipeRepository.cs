using foodieland.Data;
using foodieland.DTO.Recipes;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _context;

    public RecipeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Recipe>> GetAll(int page, int pageSize)
    {
        return await _context.Recipes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<List<Recipe>> GetAllPublished(int page, int pageSize)
    {
        return await _context.Recipes
            .Where(r => r.IsPublished == true)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Recipe?> GetById(Guid id)
    {
        return await _context.Recipes.Include(r => r.Creator).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Recipe> Create(AddOrUpdateRecipeDto addOrUpdateRecipeDto, string creatorId)
    {
        var createdRecipe = await _context.Recipes.AddAsync(addOrUpdateRecipeDto.ToRecipe(new Guid(creatorId)));
        await _context.SaveChangesAsync();
        return createdRecipe.Entity;
    }

    public async Task<Recipe?> Update(Recipe recipe, AddOrUpdateRecipeDto recipeDto)
    {
        _context.Entry(recipe).CurrentValues.SetValues(recipeDto);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe?> AddImage(Recipe recipe, byte[] imageData)
    {
        recipe.ImageData = imageData;
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<bool> Delete(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return false;
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
        return true;
    }
}
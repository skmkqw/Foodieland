using foodieland.Data;
using foodieland.DTO.Recipes;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _context;

    public RecipeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Recipe>> GetAll()
    {
        return await _context.Recipes.ToListAsync();
    }

    public async Task<Recipe?> GetById(Guid id)
    {
        return await _context.Recipes.FindAsync(id);
    }

    public async Task<Recipe> Create(CreateRecipeDto createRecipeDto, string creatorId)
    {
        var createdRecipe = await _context.Recipes.AddAsync(createRecipeDto.FromCreateDtoToRecipe(new Guid(creatorId)));
        await _context.SaveChangesAsync();
        return createdRecipe.Entity;
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
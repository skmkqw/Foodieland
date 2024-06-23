using foodieland.Data;
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
}
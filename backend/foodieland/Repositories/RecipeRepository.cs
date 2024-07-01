using foodieland.Data;
using foodieland.DTO.NutritionInformation;
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

    public async Task<Recipe> Create(AddOrUpdateRecipeDto addOrUpdateRecipeDto, string creatorId)
    {
        var createdRecipe = await _context.Recipes.AddAsync(addOrUpdateRecipeDto.ToRecipe(new Guid(creatorId)));
        await _context.SaveChangesAsync();
        return createdRecipe.Entity;
    }

    public async Task<Recipe?> Update(Guid recipeId, AddOrUpdateRecipeDto recipeDto)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return null;
        }
        _context.Entry(recipe).CurrentValues.SetValues(recipeDto);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<NutritionInformation?> GetNutritionInformation(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return null;
        }

        var nutritionInformation = await _context.NutritionInformation.FirstOrDefaultAsync(ni => ni.RecipeId == recipeId);

        return nutritionInformation;
    }

    public async Task<(NutritionInformation? nutritionInformation, string? error)> AddNutritionInformation(Guid recipeId, AddOrUpdateNutritionDto addNutritionInfoDto)
    {
        var recipe = await _context.Recipes.Include(ni => ni.NutritionInformation).FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
        {
            return (null, "Recipe not found");
        }

        if (recipe.NutritionInformation != null)
        {
            return (null, "Recipe already has nutrition information");
        }

        var createdInfo =
            await _context.AddAsync(addNutritionInfoDto.FromAddOrUpdateDtoToNutritionInformation(recipeId));
        await _context.SaveChangesAsync();
        return (createdInfo.Entity, null);
    }

    public async Task<NutritionInformation> ChangeNutritionInformation(Guid nutritionId, AddOrUpdateNutritionDto updateNutritionInfoDto)
    {
        NutritionInformation nutritionInformation = (await _context.NutritionInformation.FindAsync(nutritionId))!;
        _context.Entry(nutritionInformation).CurrentValues.SetValues(updateNutritionInfoDto);
        await _context.SaveChangesAsync();
        return nutritionInformation!;
    }

    public async Task<List<CookingDirection>?> GetCookingDirections(Guid recipeId)
    {
        var recipe = await _context.Recipes.Include(r => r.Directions).FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
        {
            return null;
        }

        return recipe.Directions;
    }


    public async Task<List<CookingDirection>> AddCookingDirections(Guid recipeId, List<CookingDirection> directions)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var recipe = await _context.Recipes.Include(r => r.Directions).FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");
            }

            recipe.Directions ??= new List<CookingDirection>();

            foreach (var direction in directions)
            {
                _context.CookingDirections.Add(direction);
                recipe.Directions.Add(direction);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return recipe.Directions;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
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
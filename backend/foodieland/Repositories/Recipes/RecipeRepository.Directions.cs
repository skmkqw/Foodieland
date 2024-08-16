using foodieland.DTO.CookingDirection;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository
{
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
            
            foreach (var direction in directions)
            {
                await _context.CookingDirections.AddAsync(direction);
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

    public async Task<List<CookingDirection>> ChangeCookingDirections(Guid recipeId, List<AddOrUpdateCookingDirectionDto> changedCookingDirections)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            if (changedCookingDirections.Count == 0)
            {
                throw new ArgumentException("New directions list must contain at least 1 direction");
            }
            var recipe = await _context.Recipes.Include(r => r.Directions).FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");
            }

            var directions = recipe.Directions;

            if (directions.Count == 0)
            {
                throw new Exception("Recipe has no cooking directions");
            }
            

            for (int i = 0; i < changedCookingDirections.Count; i++)
            {
                if (i < directions.Count)
                {
                    _context.Entry(directions[i]).CurrentValues.SetValues(changedCookingDirections[i]);
                }
                else
                {
                    var newDirection = changedCookingDirections[i].ToCookingDirection(recipeId);
                    directions.Add(newDirection);
                    await _context.CookingDirections.AddAsync(newDirection);
                }
            }
            if (directions.Count > changedCookingDirections.Count)
            {
                for (int i = directions.Count - 1; i >= changedCookingDirections.Count; i--)
                {
                    _context.CookingDirections.Remove(directions[i]);
                }
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
}
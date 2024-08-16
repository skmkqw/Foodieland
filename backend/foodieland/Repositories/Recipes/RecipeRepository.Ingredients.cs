using foodieland.DTO.IngredientQuantities;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository
{
    public async Task<List<IngredientQuantity>?> GetIngredients(Guid recipeId)
    {
        var recipe = await _context.Recipes.Include(r => r.Ingredients)
            .ThenInclude(iq => iq.Ingredient)
            .FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
        {
            return null;
        }

        return recipe.Ingredients;
    }
    
    public async Task<List<IngredientQuantity>> AddIngredients(Guid recipeId, List<AddOrUpdateIngredientDto> ingredients)
    {
        if (ingredients == null || !ingredients.Any())
        {
            throw new ArgumentException("Ingredients list must contain at least 1 ingredient");
        }
        
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var recipe = await _context.Recipes.Include(r => r.Ingredients)
                .ThenInclude(iq => iq.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe == null)
            {
                throw new Exception("Recipe not found");
            }

            foreach (AddOrUpdateIngredientDto ingredient in ingredients)
            {
                if (string.IsNullOrWhiteSpace(ingredient.IngredientName))
                {
                    throw new ArgumentException("Ingredient name cannot be null or empty");
                }
                
                var ingredientQuantity = await CreateIngredientQuantity(recipe, ingredient);

                await _context.IngredientQuantities.AddAsync(ingredientQuantity);
            }
            

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return recipe.Ingredients;

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<IngredientQuantity>> ChangeIngredients(Guid recipeId, List<AddOrUpdateIngredientDto> changedIngredients)
    {
        if (changedIngredients.Count == 0)
        {
            throw new ArgumentException("New ingredients list must contain at least 1 direction");
        }
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var recipe = await _context.Recipes.Include(r => r.Ingredients)
                .ThenInclude(iq => iq.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
            
            if (recipe == null)
                throw new Exception("Recipe not found");

            var ingredients = recipe.Ingredients;

            if (ingredients.Count == 0)
                throw new Exception("Recipe has no ingredients");
            

            for (int i = 0; i < changedIngredients.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(changedIngredients[i].IngredientName))
                {
                    throw new ArgumentException("Ingredient name cannot be null or empty");
                }
                
                if (i < ingredients.Count)
                {
                    var ingredientQuantity = await CreateIngredientQuantity(recipe, changedIngredients[i]);

                    ingredients[i].IngredientId = ingredientQuantity.IngredientId;
                    ingredients[i].Ingredient = ingredientQuantity.Ingredient;
                    ingredients[i].Quantity = ingredientQuantity.Quantity;
                    ingredients[i].Unit = ingredientQuantity.Unit;
                }
                else
                {
                    var ingredientQuantity = await CreateIngredientQuantity(recipe, changedIngredients[i]);

                    await _context.IngredientQuantities.AddAsync(ingredientQuantity);
                }
            }
            if (ingredients.Count > changedIngredients.Count)
            {
                for (int i = ingredients.Count - 1; i >= changedIngredients.Count; i--)
                {
                    _context.IngredientQuantities.Remove(ingredients[i]);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return recipe.Ingredients;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    private bool TryFindIngredient(string ingredientName, out Ingredient? ingredient)
    {
        ingredient = _context.Ingredients.FirstOrDefault(i => i.Name == ingredientName);
        return ingredient != null;
    }

    private async Task<Ingredient> CreateIngredient(string ingredientName)
    {
        ingredientName = ingredientName.Trim();
        var newIngredient = new Ingredient
        {
            Id = Guid.NewGuid(),
            Name = char.ToUpper(ingredientName[0]) + ingredientName.Substring(1).ToLower()
        };
        var createdIngredient = await _context.Ingredients.AddAsync(newIngredient);
        return createdIngredient.Entity;
    }

    private async Task<IngredientQuantity> CreateIngredientQuantity(Recipe recipe, AddOrUpdateIngredientDto changedIngredient)
    {
        IngredientQuantity ingredientQuantity;

        if (TryFindIngredient(changedIngredient.IngredientName, out var existingIngredient))
        {
            ingredientQuantity = changedIngredient.ToIngredientQuantity(recipe, existingIngredient);
        }
        else
        {
            var newIngredient = await CreateIngredient(changedIngredient.IngredientName);
            ingredientQuantity = changedIngredient.ToIngredientQuantity(recipe, newIngredient);
        }

        return ingredientQuantity;
    }
}
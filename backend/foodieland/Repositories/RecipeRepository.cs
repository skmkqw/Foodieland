using foodieland.Data;
using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
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
        return nutritionInformation;
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

            if (recipe.Directions.Count == 0)
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
                
                var existingIngredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name.ToLower() == ingredient.IngredientName.ToLower());

                IngredientQuantity ingredientQuantity;
                if (existingIngredient != null)
                {
                    ingredientQuantity = ingredient.ToIngredientQuantity(recipe, existingIngredient);
                }
                else
                {
                    string ingredientName = ingredient.IngredientName.Trim();
                    var newIngredient = new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Name = char.ToUpper(ingredientName[0]) + ingredientName.Substring(1).ToLower()
                    };
                    await _context.Ingredients.AddAsync(newIngredient);
                    ingredientQuantity = ingredient.ToIngredientQuantity(recipe, newIngredient);
                }

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
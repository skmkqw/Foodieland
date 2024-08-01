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

    public async Task<List<Recipe>> GetFeatured()
    {
        return await _context.FeaturedRecipes
            .Include(r => r.Recipe)
            .ThenInclude(c => c.Creator).Select(fr => fr.Recipe)
            .ToListAsync();
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

    public async Task<bool> AddLike(Guid recipeId, Guid userId)
    {
        if (await IsLikedByUser(recipeId, userId) || await GetById(recipeId) == null) return false;
        var like = new LikedRecipe { UserId = userId, RecipeId = recipeId };
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

    private async Task<(bool isReadyToPublish, string[]? errors)> VerifyRecipe(Recipe recipe)
    {
        List<string> errors = new ();
        var nutritionInformation = await GetNutritionInformation(recipe.Id);
        if (nutritionInformation == null)
        {
            errors.Add("Recipe must have nutrition information");
        }
        var cookingDirections = await GetCookingDirections(recipe.Id);
        if (cookingDirections == null || cookingDirections.Count == 0)
        {
            errors.Add("Recipe must have at least 1 cooking direction");
        }
        var ingredients = await GetIngredients(recipe.Id);
        if (ingredients == null || ingredients.Count == 0)
        {
            errors.Add("Recipe must have at least 1 ingredient");
        }
        return (errors.Count == 0, errors.ToArray());
    }

    public async Task<(bool isPublished, string[]? errors)> Publish(Guid recipeId)
    {
        List<string> errors = new ();
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            errors.Add("Recipe not found");
            return (false, errors.ToArray());
        }
        (bool isReadyToPublish, string[]? verificationErrors) = await VerifyRecipe(recipe);
        if (!isReadyToPublish)
        {
            errors.AddRange(verificationErrors!);
            return (false, errors.ToArray());
        }
        
        recipe.IsPublished = true;
        await _context.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool isHidden, string? error)> Hide(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return (false, "Recipe not found");
        }

        if (recipe.IsPublished == false)
        {
            return (false, "Recipe is already hidden");
        }

        recipe.IsPublished = false;
        await _context.SaveChangesAsync();
        return (true, null);
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

        await _context.FeaturedRecipes.AddAsync(new FeaturedRecipe { RecipeId = recipeId });
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
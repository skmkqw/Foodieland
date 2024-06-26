using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Models;

namespace foodieland.Repositories;

public interface IRecipeRepository
{
    public Task<List<Recipe>> GetAll();

    public Task<Recipe?> GetById(Guid id);

    public Task<Recipe> Create(AddOrUpdateRecipeDto recipeDto, string creatorId);

    public Task<Recipe?> Update(Guid recipeId, AddOrUpdateRecipeDto recipeDto);

    public Task<(NutritionInformation? nutritionInformation, string? error)> AddNutritionInformation(Guid recipeId, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<bool> Delete(Guid recipeId);
}
using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
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

    public Task<NutritionInformation?> GetNutritionInformation(Guid recipeId);

    public Task<(NutritionInformation? nutritionInformation, string? error)> AddNutritionInformation(Guid recipeId, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<NutritionInformation> ChangeNutritionInformation(Guid nutritionId, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<List<CookingDirection>?> GetCookingDirections(Guid recipeId);

    public Task<List<CookingDirection>> AddCookingDirections(Guid recipeId, List<CookingDirection> cookingDirections);

    public Task<List<CookingDirection>> ChangeCookingDirections(Guid recipeId, List<AddOrUpdateCookingDirectionDto> changedCookingDirections);

    public Task<List<IngredientQuantity>?> GetIngredients(Guid recipeId);
    
    public Task<List<IngredientQuantity>> AddIngredients(Guid recipeId, List<AddOrUpdateIngredientDto> ingredients);

    public Task<List<IngredientQuantity>> ChangeIngredients(Guid recipeId, List<AddOrUpdateIngredientDto> changedIngredients);

    public Task<bool> AddLike(Guid recipeId, Guid userId);

    public Task<bool> RemoveLike(Guid recipeId, Guid userId);

    public Task<(bool isPublished, string[]? errors)> Publish(Guid recipeId);
    
    public Task<(bool isHidden, string? error)> Hide(Guid recipeId);

    public Task<bool> Delete(Guid recipeId);
}
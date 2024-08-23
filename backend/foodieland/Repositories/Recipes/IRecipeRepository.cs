using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Models;

namespace foodieland.Repositories.Recipes;

public interface IRecipeRepository
{
    public Task<List<Recipe>> GetAll(int page, int pageSize);
    
    public Task<List<Recipe>> GetAllPublished(int page, int pageSize);

    public Task<List<Recipe>> GetFeatured();
    
    public Task<List<Recipe>> GetLikedRecipes(Guid userId, int page, int pageSize);

    public Task<Recipe?> GetById(Guid id);

    public Task<Recipe> Create(AddOrUpdateRecipeDto recipeDto, string creatorId);

    public Task<Recipe?> Update(Recipe recipe, AddOrUpdateRecipeDto recipeDto);

    public Task<Recipe?> AddImage(Recipe recipe, byte[] imageData);

    public Task<NutritionInformation?> GetNutritionInformation(Guid recipeId);

    public Task<(NutritionInformation? nutritionInformation, string? error)> AddNutritionInformation(Recipe recipe, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<NutritionInformation> ChangeNutritionInformation(NutritionInformation nutritionInformation, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<List<CookingDirection>?> GetCookingDirections(Guid recipeId);

    public Task<List<CookingDirection>> AddCookingDirections(Recipe recipe, List<CookingDirection> cookingDirections);

    public Task<List<CookingDirection>> ChangeCookingDirections(Recipe recipe, List<AddOrUpdateCookingDirectionDto> changedCookingDirections);

    public Task<List<IngredientQuantity>?> GetIngredients(Guid recipeId);
    
    public Task<List<IngredientQuantity>> AddIngredients(Recipe recipe, List<AddOrUpdateIngredientDto> ingredients);

    public Task<List<IngredientQuantity>> ChangeIngredients(Recipe recipe, List<AddOrUpdateIngredientDto> changedIngredients);

    public Task<bool> AddLike(Recipe recipe, Guid userId);

    public Task<bool> RemoveLike(Recipe recipe, Guid userId);
    
    public Task<bool> IsLikedByUser(Guid recipeId, Guid userId);

    public Task<List<LikedRecipe>> GetLikesByUser(Guid userId);

    public Task<(bool isPublished, string[]? errors)> Publish(Recipe recipe);
    
    public Task<(bool isHidden, string? error)> Hide(Recipe recipe);

    public Task<string?> SetFeatured(Guid recipeId);

    public Task<bool> RemoveFeatured(Guid recipeId);

    public Task<bool> Delete(Recipe recipe);
}
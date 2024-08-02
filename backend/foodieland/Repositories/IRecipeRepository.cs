using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Entities;
using foodieland.Models;

namespace foodieland.Repositories;

public interface IRecipeRepository
{
    public Task<List<RecipeEntity>> GetAll(int page, int pageSize);

    public Task<List<RecipeEntity>> GetFeatured();

    public Task<RecipeEntity?> GetById(Guid id);

    public Task<RecipeEntity> Create(AddOrUpdateRecipeDto recipeDto, string creatorId);

    public Task<RecipeEntity?> Update(Guid recipeId, AddOrUpdateRecipeDto recipeDto);

    public Task<NutritionInformationEntity?> GetNutritionInformation(Guid recipeId);

    public Task<(NutritionInformationEntity? nutritionInformation, string? error)> AddNutritionInformation(Guid recipeId, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<NutritionInformationEntity> ChangeNutritionInformation(Guid nutritionId, AddOrUpdateNutritionDto addNutritionInfoDto);

    public Task<List<CookingDirectionEntity>?> GetCookingDirections(Guid recipeId);

    public Task<List<CookingDirectionEntity>> AddCookingDirections(Guid recipeId, List<CookingDirectionEntity> cookingDirections);

    public Task<List<CookingDirectionEntity>> ChangeCookingDirections(Guid recipeId, List<AddOrUpdateCookingDirectionDto> changedCookingDirections);

    public Task<List<IngredientQuantityEntity>?> GetIngredients(Guid recipeId);
    
    public Task<List<IngredientQuantityEntity>> AddIngredients(Guid recipeId, List<AddOrUpdateIngredientDto> ingredients);

    public Task<List<IngredientQuantityEntity>> ChangeIngredients(Guid recipeId, List<AddOrUpdateIngredientDto> changedIngredients);

    public Task<bool> AddLike(Guid recipeId, Guid userId);

    public Task<bool> RemoveLike(Guid recipeId, Guid userId);
    
    public Task<bool> IsLikedByUser(Guid recipeId, Guid userId);

    public Task<List<LikedRecipeEntity>> GetLikedRecipesByUser(Guid userId);

    public Task<(bool isPublished, string[]? errors)> Publish(Guid recipeId);
    
    public Task<(bool isHidden, string? error)> Hide(Guid recipeId);

    public Task<string?> SetFeatured(Guid recipeId);

    public Task<bool> RemoveFeatured(Guid recipeId);

    public Task<bool> Delete(Guid recipeId);
}
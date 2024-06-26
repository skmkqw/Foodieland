using foodieland.DTO.Recipes;
using foodieland.Models;

namespace foodieland.Repositories;

public interface IRecipeRepository
{
    public Task<List<Recipe>> GetAll();

    public Task<Recipe?> GetById(Guid id);

    public Task<Recipe> Create(CreateRecipeDto createRecipeDto, string creatorId);
}
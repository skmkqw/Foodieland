using foodieland.Models;

namespace foodieland.Repositories;

public interface IRecipeRepository
{
    public Task<List<Recipe>> GetAll();
}
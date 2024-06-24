using foodieland.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace foodieland.Controllers;

[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeRepository _resository;

    public RecipeController(IRecipeRepository repository)
    {
        _resository = repository;
    }
    
    [Authorize]
    [HttpGet("/recipes")]
    public async Task<IActionResult> GetAll()
    {
        var recipes = await _resository.GetAll();
        return Ok(recipes);
    }
}
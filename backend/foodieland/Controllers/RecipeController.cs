using System.IdentityModel.Tokens.Jwt;
using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Mappers;
using foodieland.Models;
using foodieland.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace foodieland.Controllers;

[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeRepository _repository;
    private readonly UserManager<AppUser> _userManager;

    public RecipeController(IRecipeRepository repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }
    
    [HttpGet("/recipes")]
    public async Task<IActionResult> GetAll()
    {
        var recipes = await _repository.GetAll();
        return Ok(recipes.Select(r => r.ToRecipeDto(null)));
    }
    
    [HttpGet("/recipes/{recipeId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid recipeId, [FromQuery] bool displayNutrition = false, [FromQuery] bool displayDirections = false, [FromQuery] bool displayIngredients = false)
    {
        var recipe = await _repository.GetById(recipeId);
        if (recipe == null)
        {
            return NotFound("Recipe not found");
        }
        
        var nutritionInformation = displayNutrition ? await _repository.GetNutritionInformation(recipeId) : null;
        var directions = displayDirections ? await _repository.GetCookingDirections(recipeId) : null;
        var ingredients = displayIngredients ? await _repository.GetIngredients(recipeId) : null;

        var nutritionDto = nutritionInformation?.ToNutritionDto();
        var directionsDto = directions?.Select(cd => cd.ToCookingDirectionDto()).OrderBy(cd => cd.StepNumber).ToList();
        var ingredientsDto = ingredients?.Select(iq => iq.ToIngredientDto()).ToList();

        return Ok(recipe.
            ToRecipeDto(
                new RecipeMapperParams
                {
                    CookingDirections = directionsDto, Ingredients = ingredientsDto, NutritionInformation = nutritionDto
                })
        );
    }

    [Authorize]
    [HttpPost("/recipes")]
    public async Task<IActionResult> Create([FromBody] AddOrUpdateRecipeDto addOrUpdateRecipeDto)
    {
        if (ModelState.IsValid)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();

                if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
                {
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");
                    if (userIdClaim != null)
                    {
                        var userId = userIdClaim.Value;
                    
                        var user = await _userManager.FindByIdAsync(userId);
                        if (user == null)
                        {
                            return BadRequest("Invalid user ID.");
                        }

                        var createdRecipe = await _repository.Create(addOrUpdateRecipeDto, userId);
                        return CreatedAtAction(nameof(GetById), new { recipeId = createdRecipe.Id }, createdRecipe.ToRecipeDto());
                    }
                }
            }

            return Unauthorized("Failed to determine user's identity");
        }

        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPut("/recipes/{recipeId}")]
    public async Task<IActionResult> Update([FromRoute] Guid recipeId, [FromBody] AddOrUpdateRecipeDto updateRecipeDto)
    {
        if (ModelState.IsValid)
        {
            var updatedRecipe = await _repository.Update(recipeId, updateRecipeDto);
            if (updatedRecipe == null)
            {
                return NotFound("Recipe not found");
            }

            return Ok(updatedRecipe.ToRecipeDto(null));
        }

        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/addNutrition")]
    public async Task<IActionResult> AddNutrition([FromRoute] Guid recipeId, [FromBody] AddOrUpdateNutritionDto addNutritionDto)
    {
        if (ModelState.IsValid)
        {
            (NutritionInformation? nutrition, string? error) =
                await _repository.AddNutritionInformation(recipeId, addNutritionDto);
            if (error == null)
            {
                return CreatedAtAction(nameof(GetById), new { recipeId = nutrition!.Id }, nutrition.ToNutritionDto());
            }

            return BadRequest(error);
        }

        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPut("/recipes/{recipeId}/changeNutrition")]
    public async Task<IActionResult> ChangeNutrition([FromRoute] Guid recipeId, [FromBody] AddOrUpdateNutritionDto updateNutritionDto)
    {
        if (ModelState.IsValid)
        {
            var nutritionInformation = await _repository.GetNutritionInformation(recipeId);
            if (nutritionInformation == null)
            {
                return BadRequest("Recipe has no nutrition information");
            }

            var updatedNutrition = await _repository.ChangeNutritionInformation(nutritionInformation.Id, updateNutritionDto);

            return Ok(updatedNutrition.ToNutritionDto());
        }

        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/addDirections")]
    public async Task<IActionResult> AddCookingDirections([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateCookingDirectionDto> cookingDirections)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var directions = await _repository.AddCookingDirections(recipeId, 
                    cookingDirections.Select(cd => cd.ToCookingDirection(recipeId)).ToList());
                return Ok(directions.Select(cd => cd.ToCookingDirectionDto()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPut("/recipes/{recipeId}/changeDirections")]
    public async Task<IActionResult> ChangeCookingDirections([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateCookingDirectionDto> changedCookingDirections)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var directions = await _repository.ChangeCookingDirections(recipeId, changedCookingDirections);
                return Ok(directions.Select(cd => cd.ToCookingDirectionDto()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPost("/recipes/{recipeId}/addIngredients")]
    public async Task<IActionResult> AddIngredients([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateIngredientDto> ingredientDtos)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ingredients = await _repository.AddIngredients(recipeId, ingredientDtos);
                return Ok(ingredients.Select(i => i.ToIngredientDto()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPut("recipes/{recipeId}/changeIngredients")]
    public async Task<IActionResult> ChangeIngredients([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateIngredientDto> changedIngredients)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ingredients = await _repository.ChangeIngredients(recipeId, changedIngredients);
                return Ok(ingredients.Select(i => i.ToIngredientDto()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/like")]
    public async Task<IActionResult> LikeRecipe([FromRoute] Guid recipeId, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
            {
                var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return BadRequest("Invalid user ID.");
                    }

                    bool likedSuccessfully = await _repository.AddLike(recipeId, user.Id);
                    return likedSuccessfully
                        ? Ok("Recipe liked successfully")
                        : BadRequest("Recipe not exists or already liked");
                }
            }
        }

        return Unauthorized("Failed to determine user's identity");   
    }

    [Authorize]
    [HttpPatch("/recipes/{recipeId}/publish")]
    public async Task<IActionResult> Publish([FromRoute] Guid recipeId)
    {
        (bool isPublished, string[]? errors) = await _repository.Publish(recipeId);
        if (!isPublished)
        {
            return BadRequest(errors);
        }

        return NoContent();
    }
    
    [Authorize]
    [HttpPatch("/recipes/{recipeId}/hide")]
    public async Task<IActionResult> Hide([FromRoute] Guid recipeId)
    {
        (bool isHidden, string? error) = await _repository.Hide(recipeId);
        if (!isHidden)
        {
            return BadRequest(error);
        }

        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("/recipes/{recipeId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid recipeId)
    {
        bool isDeleted = await _repository.Delete(recipeId);
        if (isDeleted)
        {
            return NoContent();
        } 
        return NotFound("Recipe not found");
    }
    
    private async Task<AppUser?> TryDetermineUser(string? authorizationHeader)
    {
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
            {
                var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    
                    return await _userManager.FindByIdAsync(userId);
                }
            }
        }

        return null;
    }
}
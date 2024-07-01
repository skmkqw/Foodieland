using System.IdentityModel.Tokens.Jwt;
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
        return Ok(recipes.Select(r => r.FromRecipeToDto()));
    }
    
    [HttpGet("/recipes/{recipeId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid recipeId, [FromQuery] bool displayNutrition = false)
    {
        var recipe = await _repository.GetById(recipeId);
        if (recipe == null)
        {
            return NotFound("Recipe not found");
        }

        if (displayNutrition)
        {
            var nutritionInformation = await _repository.GetNutritionInformation(recipeId);
            return Ok(recipe.FromRecipeToDto(nutritionInformation!.ToNutritionDto()));
        }
        return Ok(recipe.FromRecipeToDto());
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
                        return CreatedAtAction(nameof(GetById), new { createdRecipe.Id }, createdRecipe.FromRecipeToDto());
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

            return Ok(updatedRecipe.FromRecipeToDto());
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
                return CreatedAtAction(nameof(GetById), new { id = nutrition!.Id }, nutrition.ToNutritionDto());
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
}
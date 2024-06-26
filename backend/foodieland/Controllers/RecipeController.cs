using System.IdentityModel.Tokens.Jwt;
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
    
    [HttpGet("/recipes/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var recipe = await _repository.GetById(id);
        if (recipe == null)
        {
            return NotFound("Recipe not found");
        }
        return Ok(recipe.FromRecipeToDto());
    }

    [Authorize]
    [HttpPost("/recipes")]
    public async Task<IActionResult> Create([FromBody] CreateRecipeDto createRecipeDto)
    {
        if (ModelState.IsValid)
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
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

                        var createdRecipe = await _repository.Create(createRecipeDto, userId);
                        return CreatedAtAction(nameof(GetById), new { createdRecipe.Id }, createdRecipe.FromRecipeToDto());
                    }
                }
            }

            return Unauthorized("Failed to determine user's identity");
        }

        return BadRequest(ModelState);
    }
}
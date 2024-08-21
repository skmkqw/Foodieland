using foodieland.DTO.CookingDirection;
using foodieland.DTO.IngredientQuantities;
using foodieland.DTO.NutritionInformation;
using foodieland.DTO.Recipes;
using foodieland.Mappers;
using foodieland.Models;
using foodieland.Repositories.Recipes;
using foodieland.Utils;
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
    
    [Authorize(Roles = "Admin")]
    [HttpGet("/recipes")]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var recipes = await _repository.GetAll(page, pageSize);
        
        var responseData = recipes
            .Select(recipe => recipe.ToShortRecipeDto(false))
            .ToList();

        return Ok(responseData);
    }
    
    [HttpGet("/recipes/published")]
    public async Task<IActionResult> GetAllPublished(
        [FromHeader(Name = "Authorization")] string? authorizationHeader,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var recipes = await _repository.GetAllPublished(page, pageSize);
        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);

        if (user == null) return Ok(recipes.Select(r => r.ToShortRecipeDto(false)));

        var likedRecipes = await _repository.GetLikedRecipesByUser(user.Id);
        var likedRecipeIds = new HashSet<Guid>(likedRecipes.Select(lr => lr.RecipeId));
        
        var responseData = recipes.Select
                (recipe => recipe.ToShortRecipeDto(isLiked: likedRecipeIds.Contains(recipe.Id)))
            .ToList();

        return Ok(responseData);
    }


    [HttpGet("recipes/featured")]
    public async Task<IActionResult> GetFeatured()
    {
        var featuredRecipes = await _repository.GetFeatured();
        return Ok(featuredRecipes.Select(r => r.ToFeaturedDto()));
    }
    
    [HttpGet("/recipes/{recipeId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid recipeId, 
        [FromHeader(Name = "Authorization")] string? authorizationHeader,
        [FromQuery] bool displayNutrition = false, 
        [FromQuery] bool displayDirections = false, 
        [FromQuery] bool displayIngredients = false) 
    {
        var recipe = await _repository.GetById(recipeId);
        if (recipe == null)
        {
            return NotFound("Recipe not found");
        }

        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
        
        var nutritionInformation = displayNutrition ? await _repository.GetNutritionInformation(recipeId) : null;
        var directions = displayDirections ? await _repository.GetCookingDirections(recipeId) : null;
        var ingredients = displayIngredients ? await _repository.GetIngredients(recipeId) : null;
        var isLiked = user != null && await _repository.IsLikedByUser(recipeId, user.Id);

        var nutritionDto = nutritionInformation?.ToNutritionDto();
        var directionsDto = directions?.Select(cd => cd.ToCookingDirectionDto()).OrderBy(cd => cd.StepNumber).ToList();
        var ingredientsDto = ingredients?.Select(iq => iq.ToIngredientDto()).ToList();

        return Ok(recipe.
            ToRecipeDto(
                new RecipeMapperParams
                {
                    CookingDirections = directionsDto, Ingredients = ingredientsDto, NutritionInformation = nutritionDto, IsLiked = isLiked
                })
        );
    }

    [Authorize]
    [HttpPost("/recipes")]
    public async Task<IActionResult> Create([FromBody] AddOrUpdateRecipeDto addOrUpdateRecipeDto, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        if (ModelState.IsValid)
        {
            var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
            if (user != null)
            {
                var createdRecipe = await _repository.Create(addOrUpdateRecipeDto, user.Id.ToString());
                return CreatedAtAction(nameof(GetById), new { recipeId = createdRecipe.Id }, createdRecipe.ToRecipeDto(null));
            }
            return Unauthorized("Failed to determine user's identity");
        }

        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPut("/recipes/{recipeId}")]
    public async Task<IActionResult> Update([FromRoute] Guid recipeId, [FromBody] AddOrUpdateRecipeDto updateRecipeDto, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    var updatedRecipe = await _repository.Update(recipe!, updateRecipeDto);

                    if (updatedRecipe == null)
                    {
                        return BadRequest("Failed to update recipe");
                    }

                    return Ok(updatedRecipe.ToRecipeDto(null));
                }
                
                return Unauthorized("You can't update this recipe");
            }
            return NotFound("Recipe not found");
        }
        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPost("/recipes/{recipeId}/uploadImage")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid recipeId, IFormFile? image, [FromHeader] string authorizationHeader)
    {
        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image uploaded");
                }

                var imageData = ImageConverter.ConvertImageToByteArray(image);

                if (imageData == null) return BadRequest("Image data is empty");

                var updatedRecipe = await _repository.AddImage(recipe!, imageData);
        
                if (updatedRecipe == null)
                {
                    return BadRequest("Failed to add image");
                }

                return Ok(updatedRecipe.ToRecipeDto(null));
            }
            return Unauthorized("You can't update this recipe");
        }
        return NotFound("Recipe not found");
    }
    
    [Authorize]
    [HttpPost("/recipes/{recipeId}/addNutrition")]
    public async Task<IActionResult> AddNutrition([FromRoute] Guid recipeId, [FromBody] AddOrUpdateNutritionDto addNutritionDto, [FromHeader] string authorizationHeader)
    {
        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    (NutritionInformation? nutrition, string? error) = await _repository.AddNutritionInformation(recipe!, addNutritionDto);
                    if (error == null)
                    {
                        return CreatedAtAction(nameof(GetById), new { recipeId = nutrition!.Id }, nutrition.ToNutritionDto());
                    }

                    return BadRequest(error);
                }
                return Unauthorized("You can't update this recipe");
            }
            return NotFound("Recipe not found");
        }
        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPut("/recipes/{recipeId}/changeNutrition")]
    public async Task<IActionResult> ChangeNutrition([FromRoute] Guid recipeId, [FromBody] AddOrUpdateNutritionDto updateNutritionDto, [FromHeader] string authorizationHeader)
    {
        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    var nutritionInformation = await _repository.GetNutritionInformation(recipeId);
                    if (nutritionInformation == null)
                    {
                        return BadRequest("Recipe has no nutrition information");
                    }

                    var updatedNutrition = await _repository.ChangeNutritionInformation(nutritionInformation, updateNutritionDto);

                    return Ok(updatedNutrition.ToNutritionDto());
                }
                return Unauthorized("You can't update this recipe");
            }
            return NotFound("Recipe not found");
        }
        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPost("/recipes/{recipeId}/addDirections")]
    public async Task<IActionResult> AddCookingDirections([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateCookingDirectionDto> cookingDirections, [FromHeader] string? authorizationHeader)
    {
        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    try
                    {
                        var directions = await _repository.AddCookingDirections(recipe!, 
                            cookingDirections.Select(cd => cd.ToCookingDirection(recipeId)).ToList());
                        return Ok(directions.Select(cd => cd.ToCookingDirectionDto()));
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                }
                return Unauthorized("You can't update this recipe");
            }
            return NotFound("Recipe not found");
        }
        return BadRequest(ModelState);
    }
    
    //TODO Check if requesting user is a creator of a recipe
    [Authorize]
    [HttpPut("/recipes/{recipeId}/changeDirections")]
    public async Task<IActionResult> ChangeCookingDirections([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateCookingDirectionDto> changedCookingDirections, [FromHeader] string? authorizationHeader)
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
    
    //TODO Check if requesting user is a creator of a recipe
    [Authorize]
    [HttpPost("/recipes/{recipeId}/addIngredients")]
    public async Task<IActionResult> AddIngredients([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateIngredientDto> ingredientDtos, [FromHeader] string? authorizationHeader)
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
    
    //TODO Check if requesting user is a creator of a recipe
    [Authorize]
    [HttpPut("recipes/{recipeId}/changeIngredients")]
    public async Task<IActionResult> ChangeIngredients([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateIngredientDto> changedIngredients, [FromHeader] string? authorizationHeader)
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
        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
        if (user != null)
        {
            bool likedSuccessfully = await _repository.AddLike(recipeId, user.Id);
            return likedSuccessfully
                ? Ok("Recipe liked successfully")
                : BadRequest("Recipe not exists or already liked");
        }
        return Unauthorized("Failed to determine user's identity");   
    }
    
    [Authorize]
    [HttpDelete("/recipes/{recipeId}/unlike")]
    public async Task<IActionResult> UnlikeRecipe([FromRoute] Guid recipeId, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
        if (user != null)
        {
            bool unlikedSuccessfully = await _repository.RemoveLike(recipeId, user.Id);
            return unlikedSuccessfully
                ? Ok("Recipe unliked successfully")
                : BadRequest("Recipe not exists or not liked");
        }
        return Unauthorized("Failed to determine user's identity");   
    }

    [Authorize(Roles = "Admin")]
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
    
    //TODO Check if requesting user is a creator of a recipe
    [Authorize]
    [HttpPatch("/recipes/{recipeId}/hide")]
    public async Task<IActionResult> Hide([FromRoute] Guid recipeId, [FromHeader] string? authorizationHeader)
    {
        (bool isHidden, string? error) = await _repository.Hide(recipeId);
        if (!isHidden)
        {
            return BadRequest(error);
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("recipes/featured/{recipeId}")]
    public async Task<IActionResult> SetFeatured([FromRoute] Guid recipeId)
    {
        string? error = await _repository.SetFeatured(recipeId);
        if (error != null)
        {
            return BadRequest(error);
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("recipes/featured/{recipeId}")]
    public async Task<IActionResult> RemoveFeatured([FromRoute] Guid recipeId)
    {
        bool isDeleted = await _repository.RemoveFeatured(recipeId);
        if (isDeleted)
        {
            return NoContent();
        } 
        return NotFound("Recipe not found");
    }
    
    //TODO Check if requesting user is a creator of a recipe
    [Authorize]
    [HttpDelete("/recipes/{recipeId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid recipeId, [FromHeader] string? authorizationHeader)
    {
        bool isDeleted = await _repository.Delete(recipeId);
        if (isDeleted)
        {
            return NoContent();
        } 
        return NotFound("Recipe not found");
    }
    
    private async Task<(bool exists, Recipe? recipe)> TryGetRecipeAsync(Guid recipeId)
    {
        var recipe = await _repository.GetById(recipeId);
        return (recipe != null, recipe);
    }
    
    private async Task<bool> IsUserAuthorizedToModifyRecipe(Recipe recipe, string? authorizationHeader)
    {
        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
        return user != null && user.Id == recipe.CreatorId;
    }
}
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
    private readonly ILogger<RecipeController> _logger;

    public RecipeController(IRecipeRepository repository, UserManager<AppUser> userManager, ILogger<RecipeController> logger)
    {
        _repository = repository;
        _userManager = userManager;
        _logger = logger;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("/recipes")]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Fetching all recipes. Page: {Page}, PageSize: {PageSize}", page, pageSize);

        var recipes = await _repository.GetAll(page, pageSize);
        _logger.LogInformation("Fetched {RecipeCount} recipes", recipes.Count);

        var responseData = recipes
            .Select(recipe => recipe.ToShortRecipeDto(false))
            .ToList();

        _logger.LogInformation("Returning {ResponseDataCount} recipe summaries", responseData.Count);
        return Ok(responseData);
    }

    [HttpGet("/recipes/published")]
    public async Task<IActionResult> GetAllPublished(
        [FromHeader(Name = "Authorization")] string? authorizationHeader,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Fetching all published recipes. Page: {Page}, PageSize: {PageSize}", page, pageSize);

        var recipes = await _repository.GetAllPublished(page, pageSize);
        _logger.LogInformation("Fetched {RecipeCount} published recipes", recipes.Count);

        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);

        if (user == null)
        {
            _logger.LogInformation("No user found in authorization header. Returning recipes without like information.");
            return Ok(recipes.Select(r => r.ToShortRecipeDto(false)));
        }

        _logger.LogInformation("User {UserId} found. Fetching liked recipes", user.Id);

        var likedRecipes = await _repository.GetLikesByUser(user.Id);
        var likedRecipeIds = new HashSet<Guid>(likedRecipes.Select(lr => lr.RecipeId));
        var responseData = recipes
            .Select(recipe => recipe.ToShortRecipeDto(isLiked: likedRecipeIds.Contains(recipe.Id)))
            .ToList();

        _logger.LogInformation("Returning {ResponseDataCount} recipe summaries with like information", responseData.Count);
        return Ok(responseData);
    }

    [HttpGet("recipes/featured")]
    public async Task<IActionResult> GetFeatured()
    {
        _logger.LogInformation("Fetching featured recipes");

        var featuredRecipes = await _repository.GetFeatured();
        _logger.LogInformation("Fetched {RecipeCount} featured recipes", featuredRecipes.Count);

        var responseData = featuredRecipes.Select(r => r.ToFeaturedDto()).ToList();
        _logger.LogInformation("Returning {ResponseDataCount} featured recipe summaries", responseData.Count);

        return Ok(responseData);
    }

    [Authorize]
    [HttpGet("recipes/liked")]
    public async Task<IActionResult> GetLiked([FromHeader(Name = "Authorization")] string authorizationHeader, 
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Fetching liked recipes");
        
        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
        if (user == null)
        {
            _logger.LogWarning("No user found in authorization header");
            return Unauthorized("Failed to determine users identity");
        }
        
        var likedRecipes = await _repository.GetLikedRecipes(user.Id, page, pageSize);
        _logger.LogInformation("Fetched {RecipeCount} liked recipes", likedRecipes.Count);
        
        var responseData = likedRecipes.Select(r => r.ToShortRecipeDto(true)).ToList();
        _logger.LogInformation("Returning {ResponseDataCount} liked recipe summaries", responseData.Count);
        
        return Ok(responseData);
    }

    [HttpGet("/recipes/{recipeId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid recipeId, 
        [FromHeader(Name = "Authorization")] string? authorizationHeader,
        [FromQuery] bool displayNutrition = false, 
        [FromQuery] bool displayDirections = false, 
        [FromQuery] bool displayIngredients = false) 
    {
        _logger.LogInformation("Fetching recipe with ID: {RecipeId}. DisplayNutrition: {DisplayNutrition}, DisplayDirections: {DisplayDirections}, DisplayIngredients: {DisplayIngredients}",
            recipeId, displayNutrition, displayDirections, displayIngredients);

        var recipe = await _repository.GetById(recipeId);
        if (recipe == null)
        {
            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogInformation("Recipe with ID {RecipeId} found. Fetching additional information", recipeId);

        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);

        var nutritionInformation = displayNutrition ? await _repository.GetNutritionInformation(recipeId) : null;
        var directions = displayDirections ? await _repository.GetCookingDirections(recipeId) : null;
        var ingredients = displayIngredients ? await _repository.GetIngredients(recipeId) : null;
        var isLiked = user != null && await _repository.IsLikedByUser(recipeId, user.Id);

        var nutritionDto = nutritionInformation?.ToNutritionDto();
        var directionsDto = directions?.Select(cd => cd.ToCookingDirectionDto()).OrderBy(cd => cd.StepNumber).ToList();
        var ingredientsDto = ingredients?.Select(iq => iq.ToIngredientDto()).ToList();

        _logger.LogInformation("Returning detailed recipe with ID {RecipeId}. IsLiked: {IsLiked}", recipeId, isLiked);

        return Ok(recipe.ToRecipeDto(new RecipeMapperParams
        {
            CookingDirections = directionsDto,
            Ingredients = ingredientsDto,
            NutritionInformation = nutritionDto,
            IsLiked = isLiked
        }));
    }

    [Authorize]
    [HttpPost("/recipes")]
    public async Task<IActionResult> Create([FromBody] AddOrUpdateRecipeDto addOrUpdateRecipeDto, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        _logger.LogInformation("Creating a new recipe");

        if (ModelState.IsValid)
        {
            var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
            if (user != null)
            {
                _logger.LogInformation("User {UserId} identified, proceeding to create recipe", user.Id);

                var createdRecipe = await _repository.Create(addOrUpdateRecipeDto, user.Id.ToString());
                _logger.LogInformation("Recipe created with ID {RecipeId}", createdRecipe.Id);

                return CreatedAtAction(nameof(GetById), new { recipeId = createdRecipe.Id }, createdRecipe.ToRecipeDto(null));
            }

            _logger.LogWarning("Failed to determine user identity from authorization header");
            return Unauthorized("Failed to determine user's identity");
        }

        _logger.LogWarning("Invalid model state when creating recipe");
        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPut("/recipes/{recipeId}")]
    public async Task<IActionResult> Update([FromRoute] Guid recipeId, [FromBody] AddOrUpdateRecipeDto updateRecipeDto, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Updating recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to update recipe with ID {RecipeId}", recipeId);

                    var updatedRecipe = await _repository.Update(recipe!, updateRecipeDto);

                    if (updatedRecipe == null)
                    {
                        _logger.LogWarning("Failed to update recipe with ID {RecipeId}", recipeId);
                        return BadRequest("Failed to update recipe");
                    }

                    _logger.LogInformation("Recipe with ID {RecipeId} updated successfully", recipeId);
                    return Ok(updatedRecipe.ToRecipeDto(null));
                }
                
                _logger.LogWarning("User unauthorized to update recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when updating recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/uploadImage")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid recipeId, IFormFile? image, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Uploading image for recipe with ID {RecipeId}", recipeId);

        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

            if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
            {
                _logger.LogInformation("User authorized to upload image for recipe with ID {RecipeId}", recipeId);

                if (image == null || image.Length == 0)
                {
                    _logger.LogWarning("No image uploaded for recipe with ID {RecipeId}", recipeId);
                    return BadRequest("No image uploaded");
                }

                var imageData = ImageConverter.ConvertImageToByteArray(image);

                if (imageData == null)
                {
                    _logger.LogWarning("Failed to convert image to byte array for recipe with ID {RecipeId}", recipeId);
                    return BadRequest("Image data is empty");
                }

                var updatedRecipe = await _repository.AddImage(recipe!, imageData);
        
                if (updatedRecipe == null)
                {
                    _logger.LogWarning("Failed to add image to recipe with ID {RecipeId}", recipeId);
                    return BadRequest("Failed to add image");
                }

                _logger.LogInformation("Image uploaded successfully for recipe with ID {RecipeId}", recipeId);
                return Ok(updatedRecipe.ToRecipeDto(null));
            }

            _logger.LogWarning("User unauthorized to upload image for recipe with ID {RecipeId}", recipeId);
            return Unauthorized("You can't update this recipe");
        }

        _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
        return NotFound("Recipe not found");
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/addNutrition")]
    public async Task<IActionResult> AddNutrition([FromRoute] Guid recipeId, [FromBody] AddOrUpdateNutritionDto addNutritionDto, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Adding nutrition information to recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to add nutrition information to recipe with ID {RecipeId}", recipeId);

                    (NutritionInformation? nutrition, string? error) = await _repository.AddNutritionInformation(recipe!, addNutritionDto);
                    if (error == null)
                    {
                        _logger.LogInformation("Nutrition information added successfully to recipe with ID {RecipeId}", recipeId);
                        return CreatedAtAction(nameof(GetById), new { recipeId = nutrition!.Id }, nutrition.ToNutritionDto());
                    }

                    _logger.LogWarning("Failed to add nutrition information to recipe with ID {RecipeId}: {Error}", recipeId, error);
                    return BadRequest(error);
                }

                _logger.LogWarning("User unauthorized to add nutrition information to recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when adding nutrition information to recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }
    
    [Authorize]
    [HttpPut("/recipes/{recipeId}/changeNutrition")]
    public async Task<IActionResult> ChangeNutrition([FromRoute] Guid recipeId, [FromBody] AddOrUpdateNutritionDto updateNutritionDto, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Changing nutrition information for recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to modify nutrition information for recipe with ID {RecipeId}", recipeId);

                    var nutritionInformation = await _repository.GetNutritionInformation(recipeId);
                    if (nutritionInformation == null)
                    {
                        _logger.LogWarning("No nutrition information found for recipe with ID {RecipeId}", recipeId);
                        return BadRequest("Recipe has no nutrition information");
                    }

                    var updatedNutrition = await _repository.ChangeNutritionInformation(nutritionInformation, updateNutritionDto);
                    _logger.LogInformation("Nutrition information for recipe with ID {RecipeId} changed successfully", recipeId);

                    return Ok(updatedNutrition.ToNutritionDto());
                }

                _logger.LogWarning("User unauthorized to change nutrition information for recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when changing nutrition information for recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/addDirections")]
    public async Task<IActionResult> AddCookingDirections([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateCookingDirectionDto> cookingDirections, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        _logger.LogInformation("Adding cooking directions to recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to add cooking directions to recipe with ID {RecipeId}", recipeId);

                    try
                    {
                        var directions = await _repository.AddCookingDirections(recipe!, cookingDirections.Select(cd => cd.ToCookingDirection(recipeId)).ToList());
                        _logger.LogInformation("Cooking directions added successfully to recipe with ID {RecipeId}", recipeId);

                        return Ok(directions.Select(cd => cd.ToCookingDirectionDto()));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error adding cooking directions to recipe with ID {RecipeId}", recipeId);
                        return BadRequest(e.Message);
                    }
                }

                _logger.LogWarning("User unauthorized to add cooking directions to recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when adding cooking directions to recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPut("/recipes/{recipeId}/changeDirections")]
    public async Task<IActionResult> ChangeCookingDirections([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateCookingDirectionDto> changedCookingDirections, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Changing cooking directions for recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to change cooking directions for recipe with ID {RecipeId}", recipeId);

                    try
                    {
                        var directions = await _repository.ChangeCookingDirections(recipe!, changedCookingDirections);
                        _logger.LogInformation("Cooking directions for recipe with ID {RecipeId} changed successfully", recipeId);

                        return Ok(directions.Select(cd => cd.ToCookingDirectionDto()));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error changing cooking directions for recipe with ID {RecipeId}", recipeId);
                        return BadRequest(e.Message);
                    }
                }

                _logger.LogWarning("User unauthorized to change cooking directions for recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when changing cooking directions for recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPost("/recipes/{recipeId}/addIngredients")]
    public async Task<IActionResult> AddIngredients([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateIngredientDto> ingredientDtos, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Adding ingredients to recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to add ingredients to recipe with ID {RecipeId}", recipeId);

                    try
                    {
                        var ingredients = await _repository.AddIngredients(recipe!, ingredientDtos);
                        _logger.LogInformation("Ingredients added successfully to recipe with ID {RecipeId}", recipeId);

                        return Ok(ingredients.Select(i => i.ToIngredientDto()));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error adding ingredients to recipe with ID {RecipeId}", recipeId);
                        return BadRequest(e.Message);
                    }
                }

                _logger.LogWarning("User unauthorized to add ingredients to recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when adding ingredients to recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }

    [Authorize]
    [HttpPut("recipes/{recipeId}/changeIngredients")]
    public async Task<IActionResult> ChangeIngredients([FromRoute] Guid recipeId, [FromBody] List<AddOrUpdateIngredientDto> changedIngredients, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Changing ingredients for recipe with ID {RecipeId}", recipeId);

        if (ModelState.IsValid)
        {
            (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
            if (recipeExists)
            {
                _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

                if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
                {
                    _logger.LogInformation("User authorized to change ingredients for recipe with ID {RecipeId}", recipeId);

                    try
                    {
                        var ingredients = await _repository.ChangeIngredients(recipe!, changedIngredients);
                        _logger.LogInformation("Ingredients for recipe with ID {RecipeId} changed successfully", recipeId);

                        return Ok(ingredients.Select(i => i.ToIngredientDto()));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error changing ingredients for recipe with ID {RecipeId}", recipeId);
                        return BadRequest(e.Message);
                    }
                }

                _logger.LogWarning("User unauthorized to change ingredients for recipe with ID {RecipeId}", recipeId);
                return Unauthorized("You can't update this recipe");
            }

            _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
            return NotFound("Recipe not found");
        }

        _logger.LogWarning("Invalid model state when changing ingredients for recipe with ID {RecipeId}", recipeId);
        return BadRequest(ModelState);
    }


    [Authorize]
    [HttpPost("/recipes/{recipeId}/like")]
    public async Task<IActionResult> LikeRecipe([FromRoute] Guid recipeId, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Liking recipe with ID {RecipeId}", recipeId);

        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

            var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
            if (user != null)
            {
                _logger.LogInformation("User {UserId} authorized to like recipe with ID {RecipeId}", user.Id, recipeId);

                bool likedSuccessfully = await _repository.AddLike(recipe!, user.Id);
                if (likedSuccessfully)
                {
                    _logger.LogInformation("Recipe with ID {RecipeId} liked successfully by user {UserId}", recipeId, user.Id);
                    return Ok("Recipe liked successfully");
                }

                _logger.LogWarning("Failed to like recipe with ID {RecipeId}. Recipe may not exist or is already liked.", recipeId);
                return BadRequest("Recipe not exists or already liked");
            }

            _logger.LogWarning("User unauthorized to like recipe with ID {RecipeId}", recipeId);
            return Unauthorized("You can't like this recipe");
        }

        _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
        return NotFound("Recipe not found");
    }

    [Authorize]
    [HttpDelete("/recipes/{recipeId}/unlike")]
    public async Task<IActionResult> UnlikeRecipe([FromRoute] Guid recipeId, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Unliking recipe with ID {RecipeId}", recipeId);

        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

            var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
            if (user != null)
            {
                _logger.LogInformation("User {UserId} authorized to unlike recipe with ID {RecipeId}", user.Id, recipeId);

                bool unlikedSuccessfully = await _repository.RemoveLike(recipe!, user.Id);
                if (unlikedSuccessfully)
                {
                    _logger.LogInformation("Recipe with ID {RecipeId} unliked successfully by user {UserId}", recipeId, user.Id);
                    return Ok("Recipe unliked successfully");
                }

                _logger.LogWarning("Failed to unlike recipe with ID {RecipeId}. Recipe may not exist or is not liked.", recipeId);
                return BadRequest("Recipe not exists or not liked");
            }

            _logger.LogWarning("User unauthorized to unlike recipe with ID {RecipeId}", recipeId);
            return Unauthorized("You can't unlike this recipe");
        }

        _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
        return NotFound("Recipe not found");
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("/recipes/{recipeId}/publish")]
    public async Task<IActionResult> Publish([FromRoute] Guid recipeId)
    {
        _logger.LogInformation("Publishing recipe with ID {RecipeId}", recipeId);

        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

            (bool isPublished, string[]? errors) = await _repository.Publish(recipe!);
            if (!isPublished)
            {
                _logger.LogWarning("Failed to publish recipe with ID {RecipeId}. Errors: {Errors}", recipeId, string.Join(", ", errors!));
                return BadRequest(errors);
            }

            _logger.LogInformation("Recipe with ID {RecipeId} published successfully", recipeId);
            return NoContent();
        }

        _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
        return NotFound("Recipe not found");
    }

    [Authorize]
    [HttpPatch("/recipes/{recipeId}/hide")]
    public async Task<IActionResult> Hide([FromRoute] Guid recipeId, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Hiding recipe with ID {RecipeId}", recipeId);

        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

            if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
            {
                _logger.LogInformation("User authorized to hide recipe with ID {RecipeId}", recipeId);

                (bool isHidden, string? error) = await _repository.Hide(recipe!);
                if (!isHidden)
                {
                    _logger.LogWarning("Failed to hide recipe with ID {RecipeId}. Error: {Error}", recipeId, error);
                    return BadRequest(error);
                }

                _logger.LogInformation("Recipe with ID {RecipeId} hidden successfully", recipeId);
                return NoContent();
            }

            _logger.LogWarning("User unauthorized to hide recipe with ID {RecipeId}", recipeId);
            return Unauthorized("You can't update this recipe");
        }

        _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
        return NotFound("Recipe not found");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("recipes/featured/{recipeId}")]
    public async Task<IActionResult> SetFeatured([FromRoute] Guid recipeId)
    {
        _logger.LogInformation("Setting recipe with ID {RecipeId} as featured", recipeId);

        string? error = await _repository.SetFeatured(recipeId);
        if (error != null)
        {
            _logger.LogWarning("Failed to set recipe with ID {RecipeId} as featured. Error: {Error}", recipeId, error);
            return BadRequest(error);
        }

        _logger.LogInformation("Recipe with ID {RecipeId} set as featured successfully", recipeId);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("recipes/featured/{recipeId}")]
    public async Task<IActionResult> RemoveFeatured([FromRoute] Guid recipeId)
    {
        _logger.LogInformation("Removing recipe with ID {RecipeId} from featured list", recipeId);

        bool isDeleted = await _repository.RemoveFeatured(recipeId);
        if (isDeleted)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} removed from featured list successfully", recipeId);
            return NoContent();
        }

        _logger.LogWarning("Failed to remove recipe with ID {RecipeId} from featured list. Recipe not found.", recipeId);
        return NotFound("Recipe not found");
    }

    [Authorize]
    [HttpDelete("/recipes/{recipeId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid recipeId, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        _logger.LogInformation("Deleting recipe with ID {RecipeId}", recipeId);

        (bool recipeExists, Recipe? recipe) = await TryGetRecipeAsync(recipeId);
        if (recipeExists)
        {
            _logger.LogInformation("Recipe with ID {RecipeId} found", recipeId);

            if (await IsUserAuthorizedToModifyRecipe(recipe!, authorizationHeader))
            {
                bool isDeleted = await _repository.Delete(recipe!);
                if (isDeleted)
                {
                    _logger.LogInformation("Recipe with ID {RecipeId} deleted successfully", recipeId);
                    return NoContent();
                }

                _logger.LogWarning("Failed to delete recipe with ID {RecipeId}", recipeId);
                return BadRequest("Failed to delete recipe");
            }

            _logger.LogWarning("User unauthorized to delete recipe with ID {RecipeId}", recipeId);
            return Unauthorized("You can't delete this recipe");
        }

        _logger.LogWarning("Recipe with ID {RecipeId} not found", recipeId);
        return NotFound("Recipe not found");
    }

    private async Task<(bool exists, Recipe? recipe)> TryGetRecipeAsync(Guid recipeId)
    {
        _logger.LogInformation("Trying to get recipe with ID {RecipeId}", recipeId);

        var recipe = await _repository.GetById(recipeId);
        bool exists = recipe != null;

        _logger.LogInformation(exists ? "Recipe with ID {RecipeId} found" : "Recipe with ID {RecipeId} not found", recipeId);
        return (exists, recipe);
    }

    private async Task<bool> IsUserAuthorizedToModifyRecipe(Recipe recipe, string? authorizationHeader)
    {
        _logger.LogInformation("Checking if user is authorized to modify recipe with ID {RecipeId}", recipe.Id);

        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
        bool isAuthorized = user != null && user.Id == recipe.CreatorId;

        _logger.LogInformation(isAuthorized ? "User authorized to modify recipe with ID {RecipeId}" : "User unauthorized to modify recipe with ID {RecipeId}", recipe.Id);
        return isAuthorized;
    }
}
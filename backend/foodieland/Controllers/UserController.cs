using foodieland.DTO.Users;
using foodieland.Mappers;
using foodieland.Models;
using foodieland.Repositories.Users;
using foodieland.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace foodieland.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository repository, UserManager<AppUser> userManager, ILogger<UserController> logger)
    {
        _repository = repository;
        _userManager = userManager;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("/users")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GetAll method called.");

        try
        {
            var users = await _repository.GetAll();
            List<UserDto> responseData = new();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                responseData.Add(user.ToDto(roles));
            }
            
            _logger.LogInformation("Successfully retrieved {Count} users.", responseData.Count);
            return Ok(responseData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving users.");
            return StatusCode(500, "An error occurred while retrieving users.");
        }
    }

    [HttpGet("/users/{userId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        _logger.LogInformation("GetById method called with userId: {UserId}", userId);

        var user = await _repository.GetById(userId);

        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found.", userId);
            return NotFound("User not found");
        }

        var roles = await _userManager.GetRolesAsync(user);
        _logger.LogInformation("User with ID {UserId} retrieved successfully.", userId);

        return Ok(user.ToDto(roles));
    }

    [Authorize]
    [HttpPost("/users/{userId}/uploadImage")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid userId, IFormFile? image, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        _logger.LogInformation("UploadImage method called for userId: {UserId}", userId);

        var imageData = ImageConverter.ConvertImageToByteArray(image);

        if (imageData == null)
        {
            _logger.LogWarning("Image data is empty for userId: {UserId}", userId);
            return BadRequest("Image data is empty");
        }

        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);

        if (user == null)
        {
            _logger.LogWarning("Failed to determine user's identity for userId: {UserId}", userId);
            return Unauthorized("Failed to determine user's identity");
        }

        if (user.Id != userId)
        {
            _logger.LogWarning("User ID mismatch. Authenticated user ID: {AuthenticatedUserId}, Route user ID: {RouteUserId}", user.Id, userId);
            return Unauthorized("User Ids do not match");
        }

        try
        {
            var updatedUser = await _repository.AddImage(user, imageData);

            if (updatedUser == null)
            {
                _logger.LogError("An error occurred while adding user image for userId: {UserId}", userId);
                return StatusCode(500, "An internal server error occurred while adding user image.");
            }

            var roles = await _userManager.GetRolesAsync(updatedUser);
            _logger.LogInformation("User image uploaded successfully for userId: {UserId}", userId);

            return Ok(updatedUser.ToDto(roles));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while uploading image for userId: {UserId}", userId);
            return StatusCode(500, "An internal server error occurred while uploading image.");
        }
    }
}
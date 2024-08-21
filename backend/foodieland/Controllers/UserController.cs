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

    public UserController(IUserRepository repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    [HttpGet("/users")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAll();

        List<UserDto> responseData = [];
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            responseData.Add(user.ToDto(roles));
        }
        return Ok(responseData);
    }

    [HttpGet("/users/{userId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        var user = await _repository.GetById(userId);
        
        if (user == null) return NotFound("User not found");
        
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(user.ToDto(roles));
    }
    
    
    [Authorize]
    [HttpPost("/users/{userId}/uploadImage")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid userId, IFormFile? image, [FromHeader(Name = "Authorization")] string? authorizationHeader)
    {
        var imageData = ImageConverter.ConvertImageToByteArray(image);

        if (imageData == null) return BadRequest("Image data is empty");

        var user = await IdentityVerifier.TryDetermineUser(_userManager, authorizationHeader);
    
        if (user == null)
        {
            return Unauthorized("Failed to determine user's identity");
        }

        if (user.Id != userId)
        {
            return Unauthorized("User Id are not matching");
        }

        var updatedUser = await _repository.AddImage(user, imageData);

        if (updatedUser == null) return StatusCode(500, "An internal server error occurred while adding user image.");
        
        var roles = await _userManager.GetRolesAsync(updatedUser);
        
        return Ok(updatedUser.ToDto(roles));
    }
}
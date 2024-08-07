using foodieland.Data;
using foodieland.Models;
using Microsoft.AspNetCore.Identity;

namespace foodieland.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUser?> GetById(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<AppUser?> AddImage(AppUser user, byte[] imageData)
    {
        user.ProfileImage = imageData;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded) return user;

        return null;
    }
}
using foodieland.Data;
using foodieland.Models;

namespace foodieland.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<AppUser?> AddImage(Guid userId, byte[] imageData)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null;
        }

        user.ProfileImage = imageData;
        await _context.SaveChangesAsync();
        return user;
    }
}
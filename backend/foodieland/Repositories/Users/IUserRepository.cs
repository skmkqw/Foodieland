using foodieland.Models;

namespace foodieland.Repositories.Users;

public interface IUserRepository
{
    public Task<AppUser?> GetById(Guid userId);
    
    public Task<AppUser?> AddImage(Guid userId, byte[] imageData);
}
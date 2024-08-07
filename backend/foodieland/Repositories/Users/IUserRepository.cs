using foodieland.Models;

namespace foodieland.Repositories.Users;

public interface IUserRepository
{
    public Task<AppUser?> AddImage(Guid userId, byte[] imageData);
}
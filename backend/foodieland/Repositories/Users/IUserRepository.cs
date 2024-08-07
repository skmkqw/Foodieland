using foodieland.Models;

namespace foodieland.Repositories.Users;

public interface IUserRepository
{
    public Task<List<AppUser>> GetAll();
    
    public Task<AppUser?> GetById(Guid userId);
    
    public Task<AppUser?> AddImage(AppUser user, byte[] imageData);
}
using foodieland.DTO.Users;
using foodieland.Models;
using foodieland.Utils;

namespace foodieland.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this AppUser user)
    {
        return new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            ProfileImage = ImageConverter.ConvertByteArrayToBase64String(user.ProfileImage)
        };
    }
}
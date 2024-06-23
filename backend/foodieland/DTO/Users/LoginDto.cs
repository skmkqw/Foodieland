using System.ComponentModel.DataAnnotations;

namespace foodieland.DTO.Users;

public class LoginDto
{
    [EmailAddress] [Required(ErrorMessage = "Email is required")] 
    public string Email { get; set; } = string.Empty; 
		
    [Required(ErrorMessage = "Password is required")] 
    public string Password { get; set; } = string.Empty;
}
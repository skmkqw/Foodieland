using System.ComponentModel.DataAnnotations;

namespace foodieland.DTO.Users;

public class RegisterDto
{
    [Required(ErrorMessage = "User name is required")] 
    public string FirstName { get; set; } = string.Empty; 
    
    [Required(ErrorMessage = "User name is required")] 
    public string LastName { get; set; } = string.Empty; 
		
    [EmailAddress] [Required(ErrorMessage = "Email is required")] 
    public string Email { get; set; } = string.Empty; 
		
    [Required(ErrorMessage = "Password is required")] 
    public string Password { get; set; } = string.Empty;
}
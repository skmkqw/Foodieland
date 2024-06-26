using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using foodieland.DTO.Users;
using foodieland.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace foodieland.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(UserManager<AppUser> userManager, IConfiguration configuration) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "There's already an account registered for this email");
                return BadRequest(ModelState);
            }

            var user = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email,
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var token = TokenGenerator.GenerateToken(registerDto.Email, user.Id.ToString(), configuration);
                return Ok(new { token });
            }

            foreach (var errr in result.Errors)
            {
                ModelState.AddModelError("", errr.Description);
            }
        }

        return BadRequest(ModelState);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto) 
    { 
        if (ModelState.IsValid)
        { 
            var user = await userManager.FindByEmailAsync(loginDto.Email); 
            if (user != null) 
            { 
                if (await userManager.CheckPasswordAsync(user, loginDto.Password)) 
                { 
                    var token = TokenGenerator.GenerateToken(loginDto.Email, user.Id.ToString(), configuration); 
                    return Ok(new { token }); 
                } 
            }
            ModelState.AddModelError("", "Invalid username or password"); 
        } 
        return BadRequest(ModelState);
    }
}

public class TokenGenerator
{
    public static string? GenerateToken(string email, string userId, IConfiguration configuration)
    {
        var secret = configuration["JwtConfig:Secret"];
        var issuer = configuration["JwtConfig:ValidIssuer"];
        var audience = configuration["JwtConfig:ValidAudiences"];

        if (secret == null || issuer == null || audience == null)
        {
            throw new ApplicationException("Jwt is not configure!");
        }

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}


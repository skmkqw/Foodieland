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
        if (!ModelState.IsValid)
        {
            var modelStateErrors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new { errorCode = "MODEL_STATE_ERROR", message = string.Join("; ", modelStateErrors) });
        }

        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            return BadRequest(new { errorCode = "EMAIL_IN_USE", message = "There's already an account registered for this email" });
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
            await userManager.AddToRoleAsync(user, "User");
            var token = TokenGenerator.GenerateToken(user, configuration);
            return Ok(new { token });
        }

        var identityErrors = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new { errorCode = "IDENTITY_ERROR", message = string.Join("; ", identityErrors) });
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
                    var token = TokenGenerator.GenerateToken(user, configuration); 
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
    public static string? GenerateToken(AppUser user, IList<string> roles, IConfiguration configuration)
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
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = configuration["JwtConfig:ValidIssuer"],
            Audience = configuration["JwtConfig:ValidAudiences"],
            SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
        };
       
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}


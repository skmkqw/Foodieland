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
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<AppUser> userManager, IConfiguration configuration, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        _logger.LogInformation("Register method called for email: {Email}", registerDto.Email);

        if (!ModelState.IsValid)
        {
            var modelStateErrors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            _logger.LogWarning("Model state is invalid: {Errors}", string.Join("; ", modelStateErrors));
            return BadRequest(new { errorCode = "MODEL_STATE_ERROR", message = string.Join("; ", modelStateErrors) });
        }

        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed: Email {Email} is already in use", registerDto.Email);
            return BadRequest(new { errorCode = "EMAIL_IN_USE", message = "There's already an account registered for this email" });
        }

        var user = new AppUser
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserName = registerDto.Email,
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} registered successfully", user.Email);
            await _userManager.AddToRoleAsync(user, "User");
            var token = TokenGenerator.GenerateToken(user, new List<string> { "User" }, _configuration, _logger);
            return Ok(new { token });
        }

        var identityErrors = result.Errors.Select(e => e.Description).ToList();
        _logger.LogWarning("Registration failed for {Email}: {Errors}", user.Email, string.Join("; ", identityErrors));
        return BadRequest(new { errorCode = "IDENTITY_ERROR", message = string.Join("; ", identityErrors) });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        _logger.LogInformation("Login method called for email: {Email}", loginDto.Email);

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    _logger.LogInformation("User {Email} logged in successfully", loginDto.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    var token = TokenGenerator.GenerateToken(user, roles, _configuration, _logger);
                    return Ok(new { token });
                }
            }
            _logger.LogWarning("Invalid login attempt for {Email}", loginDto.Email);
            ModelState.AddModelError("", "Invalid username or password");
        }

        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        _logger.LogWarning("Login failed for {Email}: {Errors}", loginDto.Email, string.Join("; ", errors));
        return BadRequest(ModelState);
    }
}

public static class TokenGenerator
{
    public static string? GenerateToken(AppUser user, IList<string> roles, IConfiguration configuration, ILogger logger)
    {
        logger.LogInformation("Generating token for user: {Email}", user.Email);

        var secret = configuration["JwtConfig:Secret"];
        var issuer = configuration["JwtConfig:ValidIssuer"];
        var audience = configuration["JwtConfig:ValidAudiences"];

        if (secret == null || issuer == null || audience == null)
        {
            logger.LogError("JWT configuration is missing or invalid");
            throw new ApplicationException("JWT is not configured!");
        }

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        
        logger.LogInformation("Token generated successfully for user: {Email}", user.Email);

        return token;
    }
}
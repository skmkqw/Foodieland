using System.IdentityModel.Tokens.Jwt;
using foodieland.Models;
using Microsoft.AspNetCore.Identity;

namespace foodieland.Utils;

public static class IdentityVerifier
{
    public static async Task<AppUser?> TryDetermineUser(UserManager<AppUser> userManager, string? authorizationHeader)
    {
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
            {
                var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid");
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    
                    return await userManager.FindByIdAsync(userId);
                }
            }
        }

        return null;
    }
}
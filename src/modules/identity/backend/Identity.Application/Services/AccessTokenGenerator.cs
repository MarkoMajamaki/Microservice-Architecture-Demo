using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Identity.Domain;
using Shared.Application;

namespace Identity.Application;

public interface IAccessTokenGenerator
{
    Task<string> GenerateToken(ApplicationUser user);
}

public class AccessTokenGenerator : IAccessTokenGenerator
{
    private readonly UserManager<ApplicationUser> _userManager;  
    private readonly JwtSettings _jwtSettings;  

    public AccessTokenGenerator(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _jwtSettings = jwtOptions.Value;
    }

    /// <summary>
    /// Create JWT token for user
    /// </summary>
    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var authClaims = new List<Claim>  
        {  
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),  
        };  

        var userRoles = await _userManager.GetRolesAsync(user);  

        foreach (var userRole in userRoles)  
        {  
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
        }  

        string jwtSected = _jwtSettings.Secret;

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSected));  

        var jwtToken = new JwtSecurityToken(  
            issuer: _jwtSettings.ValidIssuer,  
            audience: _jwtSettings.ValidAudience,  
            expires: DateTime.Now.AddHours(3),  
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));  

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
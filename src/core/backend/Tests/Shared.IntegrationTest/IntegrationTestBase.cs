using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Shared.Tests;

/// <summary>
/// Base class for integration tests
/// </summary>
/// <typeparam name="TStartup">WebApi startup class type</typeparam>
/// <typeparam name="TDbContext">WebApi databse context</typeparam>
public class IntegrationTestBase<TStartup, TDbContext> : IClassFixture<TestApplicationFactory<TStartup, TDbContext>>
    where TStartup: class
    where TDbContext: DbContext
{    
    private readonly TestApplicationFactory<TStartup, TDbContext> _factory;

    public IntegrationTestBase(TestApplicationFactory<TStartup, TDbContext> factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Create HttpClient for testing
    /// </summary>
    /// <param name="isloggedIn">Is user logged in</param>
    protected HttpClient CreateClient(bool isloggedIn = true)
    {
        if (isloggedIn)
        {
            HttpClient client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CreateAccessToken());
            return client;
        }
        else
        {
            return _factory.CreateClient();
        }
    }

    /// <summary>
    /// Create access token for testing
    /// </summary>
    private string CreateAccessToken()
    {
        var authClaims = new List<Claim>  
        {  
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, "user@mail.com"),  
        };  

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettingsForTesting.Secret));  

        var jwtToken = new JwtSecurityToken(  
            issuer: JwtSettingsForTesting.ValidIssuer,  
            audience: JwtSettingsForTesting.ValidAudience,  
            expires: DateTime.Now.AddHours(1),  
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));  

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
using Xunit;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

using Identity.Infrastructure;
using Shared.Tests;
using Identity.Api;

namespace Identity.IntegrationTest;

public class LoginController_Tests : IClassFixture<TestApplicationFactory<Startup, IdentityContext>>
{
    private readonly TestApplicationFactory<Startup, IdentityContext> _factory;

    public LoginController_Tests(TestApplicationFactory<Startup, IdentityContext> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Login_WithUnregisteredUser()
    {            
        // Arrange
        var client = _factory.CreateClient();
        var data = new 
        { 
            Email = "test@email.com", 
            Password = "password"
        };

        // Act
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("login/login-email", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    
    [Fact]
    public async void Login_WithRegisteredUser()
    {            
        // Arrange
        var client = _factory.CreateClient();
        var data = new 
        { 
            Email = "test@email.com", 
            Password = "p4ssw0rD!123"
        };

        // Register user
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        await client.PostAsync("register/register-email", content);

        // Act
        var response = await client.PostAsync("login/login-email", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}

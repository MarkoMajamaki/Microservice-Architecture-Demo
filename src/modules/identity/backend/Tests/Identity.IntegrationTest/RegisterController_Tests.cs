using Xunit;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

using Identity.Infrastructure;
using Shared.Tests;
using Identity.Api;

namespace Identity.IntegrationTest;

public class RegisterController_Tests : IClassFixture<TestApplicationFactory<Startup, IdentityContext>>
{
    private readonly TestApplicationFactory<Startup, IdentityContext> _factory;

    public RegisterController_Tests(TestApplicationFactory<Startup, IdentityContext> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Register_WithNewUser()
    {
        // Arrange
        var client = _factory.CreateClient();

        var data = new 
        { 
            Email = "testuser@test.com",
            Password = "p4sswOrd!123" 
        };
        
        // Act
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("register/register-email", content);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async void Register_WithSimplePassword()
    {
        // Arrange
        var client = _factory.CreateClient();

        var data = new 
        { 
            Email = "testuser@test.com",
            Password = "simple" 
        };
        
        // Act
        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("register/register-email", content);

        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
}
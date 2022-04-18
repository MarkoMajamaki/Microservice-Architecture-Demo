using System.Net.Http;
using System.Text;
using System.Text.Json;
using Shared.Tests;
using Xunit;

namespace Inventory.Infrastructure;

public class ProductController_Test : IntegrationTestBase<Inventory.Api.Startup, InventoryContext>
{
    public ProductController_Test(TestApplicationFactory<Api.Startup, InventoryContext> factory) : base(factory)
    {
    }

    [Fact]
    public async void CreateProduct()
    {
        var client = CreateClient();

        var data = new 
        {
            Name = "Name",
            Description = "Description",
            Quantity = 1,
            Price = 10,
            Image = "",
        };

        StringContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        // Act
        var createProductResponse = await client.PostAsync("product/create", content);
        
        // Assert
        Assert.True(createProductResponse.IsSuccessStatusCode);
    }

}
using Order.Application;
using Order.Infrastructure;
using Shared.Tests;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Order.IntegrationTest;

public class OrderController_Test : IntegrationTestBase<Order.Api.Startup, OrderContext>
{
    public OrderController_Test(TestApplicationFactory<Order.Api.Startup, OrderContext> factory) : base(factory) {}

    [Fact]
    public async void CreateOrder()
    {
        var client = CreateClient();
        int customerId = 1;

        var data = new 
        {
            CustomerId = customerId,
            Items = new []{
                new {
                    Amount = 10,
                    ProductId = 1,
                },
                new {
                    Amount = 3,
                    ProductId = 2,
                },
                new {
                    Amount = 1,
                    ProductId = 3,
                },
            },
        };

        StringContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        // Act
        var createOrderResponse = await client.PostAsync("order/create", content);
        
        // Assert
        Assert.Equal(createOrderResponse.IsSuccessStatusCode, true);

        var getOrderResponse = await client.GetAsync("order/1");
        var getOrderResponseString = await getOrderResponse.Content.ReadAsStringAsync();
        
        var getOrderResponseTyped = JsonSerializer.Deserialize<GetOrderByIdResponse>(getOrderResponseString, 
            new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        Assert.Equal(getOrderResponseTyped.CustomerId, customerId);
    }
}
using Order.Application;
using Order.Infrastructure;
using Shared.Tests;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Order.IntegrationTest;

public class OrderController_Test : IntegrationTestBase<Order.Api.Startup, OrderContext>, IDisposable
{
    public OrderController_Test(TestApplicationFactory<Order.Api.Startup, OrderContext> factory) : base(factory) {}


    public void Dispose()
    {
        // Do database cleanup after every test here if needed (by default in memory database is used)
    }

    [Fact]
    public async void CreateOrder()
    {
        var client = CreateClient();
        int customerId = 1;
        
        var data = new 
        {
            CustomerId = customerId,
            ShipToAddress = new {
                Country = "country",
                City = "city",
                ZipCode = "zipcode",
                Street = "street"
            },
            Items = new []{
                new {
                    ProductId = 1,
                    Name = "product1",
                    Price = 1,
                    PictureUrl = "pictureUrl",
                    Quantity = 10,
                },
                new {
                    ProductId = 2,
                    Name = "product1",
                    Price = 1,
                    PictureUrl = "pictureUrl",
                    Quantity = 10,
                },
                new {
                    ProductId = 3,
                    Name = "product1",
                    Price = 1,
                    PictureUrl = "pictureUrl",
                    Quantity = 10,
                },
                new {
                    ProductId = 3,
                    Name = "product1",
                    Price = 1,
                    PictureUrl = "pictureUrl",
                    Quantity = 10,
                },
            },
        };

        StringContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        // Act
        var createOrderResponse = await client.PostAsync("order/create", content);
        var createOrderResponseString = await createOrderResponse.Content.ReadAsStringAsync();
        var createOrderResponseTyped = JsonSerializer.Deserialize<CreateOrder.Response>(createOrderResponseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal(createOrderResponse.IsSuccessStatusCode, true);

        var getOrderResponse = await client.GetAsync($"order/{createOrderResponseTyped.Id}");
        var getOrderResponseString = await getOrderResponse.Content.ReadAsStringAsync();
        var getOrderResponseTyped = JsonSerializer.Deserialize<GetOrderById.Response>(getOrderResponseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        Assert.Equal(getOrderResponseTyped.CustomerId, customerId);
        Assert.Equal(getOrderResponseTyped.Items[0].Quantity, 10);
        Assert.Equal(getOrderResponseTyped.Items[0].ProductId, 1);
        Assert.Equal(getOrderResponseTyped.Items[2].Quantity, 20);
        Assert.Equal(getOrderResponseTyped.Items[2].ProductId, 3);
    }
}
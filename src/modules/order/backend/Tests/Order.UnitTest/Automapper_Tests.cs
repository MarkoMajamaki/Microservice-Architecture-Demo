using System.Collections.Generic;
using AutoMapper;
using Order.Api;
using Order.Domain;
using Xunit;

namespace Order.UnitTest;

public class AutoMapper_Tests
{
    private IMapper _mapper;

    public AutoMapper_Tests()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<Order.Api.MappingProfile>();
            cfg.AddProfile<Order.Application.MappingProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Api_CreateOrderRequest_To_Application_CreateOrder_Command()
    {
        int customerId = 1;

        string name = "test_orderitemname";
        decimal price = 1;
        int quantity = 1;

        string country = "test_country";
        string city = "test_city";
        string zipCode = "test_zipcode";
        string street = "test_street";

        var items = new List<Order.Api.CreateOrderRequest.OrderItem>();
        for (int i = 0; i < 10; i++)
        {
            var apiOrderItem = new Order.Api.CreateOrderRequest.OrderItem()
            {
                ProductId = i,
                Name = name,
                Price = price,
                Quantity = quantity,
            };

            items.Add(apiOrderItem);
        }

        var apiAddress = new Order.Api.CreateOrderRequest.Address()
        {
            Country = country,
            City = city,
            ZipCode = zipCode,
            Street = street,
        };

        var apiOrder = new Order.Api.CreateOrderRequest()
        {
            CustomerId = customerId,
            ShipToAddress = apiAddress,
            Items = items,
        };

        var mapResult = _mapper.Map<Order.Application.CreateOrder.Command>(apiOrder);

        Assert.Equal(customerId, mapResult.CustomerId);
        Assert.Equal(country, mapResult.ShipToAddress.Country);
        Assert.Equal(city, mapResult.ShipToAddress.City);
        Assert.Equal(zipCode, mapResult.ShipToAddress.ZipCode);
        Assert.Equal(street, mapResult.ShipToAddress.Street);

        for (int i = 0; i < 10; i++)
        {
            Assert.Equal(i, mapResult.Items[i].ProductId);
            Assert.Equal(name, mapResult.Items[i].Name);
            Assert.Equal(price, mapResult.Items[i].Price);
            Assert.Equal(quantity, mapResult.Items[i].Quantity);
        }
    }

    [Fact]
    public void Api_CreateOrderRequest_OrderItem_To_Application_CreateOrder_Command_OrderItem()
    {
        int productId = 1;
        string name = "test_orderitemname";
        decimal price = 1;
        int quantity = 1;

        var apiOrderItem = new Order.Api.CreateOrderRequest.OrderItem()
        {
            ProductId = productId,
            Name = name,
            Price = price,
            Quantity = quantity,
        };

        var mapResult = _mapper.Map<Order.Application.CreateOrder.Command.OrderItem>(apiOrderItem);

        Assert.Equal(productId, mapResult.ProductId);
        Assert.Equal(name, mapResult.Name);
        Assert.Equal(price, mapResult.Price);
        Assert.Equal(quantity, mapResult.Quantity);
    }        

    [Fact]
    public void Api_CreateOrderRequest_Address_To_Application_CreateOrder_Command_Address()
    {
        string country = "test_country";
        string city = "test_city";
        string zipCode = "test_zipcode";
        string street = "test_street";
        
        var apiAddress = new Order.Api.CreateOrderRequest.Address()
        {
            Country = country,
            City = city,
            ZipCode = zipCode,
            Street = street,
        };

        var mapResult = _mapper.Map<Order.Application.CreateOrder.Command.Address>(apiAddress);

        Assert.Equal(country, mapResult.Country);
        Assert.Equal(city, mapResult.City);
        Assert.Equal(zipCode, mapResult.ZipCode);
        Assert.Equal(street, mapResult.Street);
    }

    [Fact]
    public void Application_CreateOrder_Response_To_Api_CreateOrderResponse()
    {
        // TODO
    }
}
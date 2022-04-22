using AutoMapper;
using Order.Api;
using Order.Domain;
using Xunit;

namespace Order.UnitTest;

public class AutoMapper_Tests
{
    private readonly IMapper _mapper;

    public AutoMapper_Tests(IMapper mapper)
    {
        _mapper = mapper;
    }

    [Fact]
    public void DomainOrderItemToGetOrderResponseOrderItem_Test()
    {
        int quantity = 10;
        int productId = 10;

        var domainOrderItem = new OrderItem(quantity, productId);
        var responseOrderItem = new GetOrderResponse.OrderItem()
        {
            Quantity = quantity,
            ProductId = productId
        };

        var mapResult = _mapper.Map<GetOrderResponse.OrderItem>(domainOrderItem);
        
        Assert.Equal(quantity, mapResult.Quantity);
        Assert.Equal(productId, mapResult.ProductId);
    }
}
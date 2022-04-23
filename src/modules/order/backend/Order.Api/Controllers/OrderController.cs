using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Shared.Api;

namespace Order.Api;

public class OrderController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var result = await Mediator.Send(new GetAllOrders.Query());
        return Ok(Mapper.Map<List<GetOrderResponse>>(result));
    }

    [HttpGet("vendor/{vendorId}")]
    public Task<IActionResult> GetOrdersByVendor(int vendorId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("customer/{customerId}")]
    public Task<IActionResult> GetOrdersByCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var result = await Mediator.Send(new GetOrderById.Query(orderId));
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map<GetOrderResponse>(result));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest order)
    {
        var command = Mapper.Map<CreateOrder.Command>(order);

        var result = await Mediator.Send(command);

        return Ok(Mapper.Map<CreateOrderResponse>(result));
    }
}

using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Shared.Api;

namespace Order.Api;

public class OrderController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await Mediator.Send(new GetAllOrders.Query()));
    }

    [HttpGet("vendor/{vendorId}")]
    public Task<IActionResult> GetOrdersByVendor(int vendorId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var result = await Mediator.Send(new GetOrderById.Query(orderId));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrder.Command order)
    {
        return Ok(await Mediator.Send(order));
    }
}

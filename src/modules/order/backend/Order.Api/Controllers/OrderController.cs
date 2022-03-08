using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Shared.Api;

namespace Order.Api;

public class OrderController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await Mediator.Send(new GetAllOrdersQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var result = await Mediator.Send(new GetOrderByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand order)
    {
        return Ok(await Mediator.Send(order));
    }
}

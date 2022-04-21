using Basket.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

public class BasketController : BaseController
{
    [HttpGet("{customerId}")]
    public Task<IActionResult> GetByCustomerId(int customerId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("add/{productId}")]
    public async Task<IActionResult> AddItem(int productId)
    {
        return Ok(await Mediator.Send(new AddItemToBasket.Command(productId)));
    }
}
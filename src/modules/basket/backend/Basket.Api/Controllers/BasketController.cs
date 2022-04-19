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

    [HttpPost("add")]
    public Task<IActionResult> AddItem([FromBody] AddItemToBasketCommand item)
    {
        throw new NotImplementedException();
    }
}
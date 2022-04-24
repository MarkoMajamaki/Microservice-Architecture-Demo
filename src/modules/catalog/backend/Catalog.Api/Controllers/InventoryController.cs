using Catalog.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

/// <summary>
/// Controller to handle inventory
/// </summary>
public class InventoryController : BaseController
{
    [HttpGet]
    public Task<IActionResult> GetStockInformation(int productId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("reservation/{}")]
    public Task<IActionResult> DoStockReservation(int productId, int quantity)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{categoryId}")]
    public Task<IActionResult> RemoveQuantity (int categoryId)
    {
        throw new NotImplementedException();
    }
}
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Inventory.Api;

public class InventoryController : BaseController
{
    [HttpGet("quantity/{productId}")]
    public Task<IAsyncResult> CheckQuantity(int productId)
    {
        throw new NotImplementedException();
    }
}
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Inventory.Api;

public class InventoryController : BaseController
{
    [HttpGet]
    public Task<IAsyncResult> CheckQuantity(int productId)
    {
        throw new NotImplementedException();
    }
}
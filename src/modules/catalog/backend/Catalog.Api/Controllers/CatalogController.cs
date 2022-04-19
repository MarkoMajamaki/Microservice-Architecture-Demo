using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

public class CatalogController : BaseController
{
    [HttpGet]
    public Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{productId}")]
    public Task<IActionResult> GetDetails(int productId)
    {
        throw new NotImplementedException();
    }
}
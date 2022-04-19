using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

/// <summary>
/// Controller to get catalog items for UI
/// </summary>
public class CatalogController : BaseController
{
    [HttpGet]
    public Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{categoryId}")]
    public Task<IActionResult> GetByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{productId}")]
    public Task<IActionResult> GetById(int productId)
    {
        throw new NotImplementedException();
    }
}
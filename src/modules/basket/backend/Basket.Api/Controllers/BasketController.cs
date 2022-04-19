using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

public class CatalogController : BaseController
{
    [HttpGet]
    public Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
        
    [HttpGet("{categoryId}")]
    public Task<IActionResult> GetByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }
}
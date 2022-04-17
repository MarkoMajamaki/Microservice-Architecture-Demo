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

    [HttpGet]
    public Task<IActionResult> GetDetails()
    {
        throw new NotImplementedException();
    }
}
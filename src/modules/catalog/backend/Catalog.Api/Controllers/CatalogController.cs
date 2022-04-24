using Catalog.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

/// <summary>
/// Controller to get catalog information
/// </summary>
public class CatalogController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCatalog()
    {
        return Ok(await Mediator.Send(new GetCatalogQuery()));
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCatalogByCategory(int categoryId)
    {
        return Ok(await Mediator.Send(new GetCatalogCategoryQuery(categoryId)));
    }
}
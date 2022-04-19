using Catalog.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

/// <summary>
/// Controller to manage vendor product information
/// </summary>
public class ProductController : BaseController
{
    [HttpGet("catalog")]
    public async Task<IActionResult> GetCatalog()
    {
        return Ok(await Mediator.Send(new GetCatalogQuery()));
    }

    [HttpGet("catalog/{categoryId}")]
    public async Task<IActionResult> GetCatalogByCategory(int categoryId)
    {
        return Ok(await Mediator.Send(new GetCatalogCategoryQuery(categoryId)));
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByProductId(int productId)
    {
        return Ok(await Mediator.Send(new GetProductQuery(productId)));
    }

    [HttpGet("{vendorId}")]
    public async Task<IActionResult> GetByVendorId(int vendorId)
    {
       return Ok(await Mediator.Send(new GetProductsByVendorQuery(vendorId)));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand product)
    {
       return Ok(await Mediator.Send(product));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand product)
    {
       return Ok(await Mediator.Send(product));
    }

    [HttpPost]
    [HttpPut("delete/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
       return Ok(await Mediator.Send(new DeleteProductCommand(productId)));
    }
}
using Catalog.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

/// <summary>
/// Controller to manage vendor product information
/// </summary>
public class ProductController : BaseController
{
    [HttpGet("{vendorId}")]
    public async Task<IActionResult> GetByVendor(int vendorId)
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
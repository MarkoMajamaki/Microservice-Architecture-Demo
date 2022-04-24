using Catalog.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

/// <summary>
/// Controller to manage vendor product information
/// </summary>
public class ProductController : BaseController
{
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
    public async Task<IActionResult> Update([FromBody] ProductUpdateDto product)
    {
       return Ok(await Mediator.Send(new UpdateProduct.Command
       {
           // TODO: Automapper
           Id = product.Id,
           Name = product.Name, 
           Description = product.Description,
           Quantity = product.Quantity, 
           Price = product.Price,
       }));
    }

    [HttpPost]
    [HttpPut("delete/{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
       return Ok(await Mediator.Send(new DeleteProductCommand(productId)));
    }
}
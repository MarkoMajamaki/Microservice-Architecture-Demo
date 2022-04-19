using Inventory.Application;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Inventoty.Api;

public class ProductController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
       return Ok(await Mediator.Send(new GetAllProductsQuery()));
    }

    [HttpGet("category/{categoryId}")]
    public Task<IActionResult> GetProductsByCategory(int categoryId)
    {
       // Ok(await Mediator.Send(new GetProductsByCategory()));
       throw new NotImplementedException();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand product)
    {
       return Ok(await Mediator.Send(product));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand product)
    {
       return Ok(await Mediator.Send(product));
    }

    [HttpPost]
    [HttpPut("delete/{productId}")]
    public Task<IActionResult> DeleteProduct(int productId)
    {
       // Ok(await Mediator.Send(new DeleteProduct(productId)));
       throw new NotImplementedException();
    }
}
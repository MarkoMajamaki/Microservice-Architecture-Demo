using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Inventoty.Api;

public class ProductController : BaseController
{
    [HttpGet]
    public Task<IActionResult> GetAllProducts()
    {
        // return Ok(await Mediator.Send(new GetAllProducts()));
       throw new NotImplementedException(); 
    }

    [HttpGet]
    public Task<IActionResult> GetProductsByCategory()
    {
       throw new NotImplementedException(); 
    }

    [HttpPut]
    public Task<IActionResult> CreateProduct()
    {
       throw new NotImplementedException(); 
    }

    [HttpPost]
    public Task<IActionResult> UpdateProduct()
    {
       throw new NotImplementedException(); 
    }

    [HttpPost]
    public Task<IActionResult> DeleteProduct(int productId)
    {
       throw new NotImplementedException(); 
    }
}
using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

public class SearchController : BaseController
{
    [HttpGet("{text}")]
    public Task<IActionResult> Search(string text)
    {
        throw new NotImplementedException();
    }
}
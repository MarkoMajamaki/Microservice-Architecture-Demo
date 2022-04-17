using Microsoft.AspNetCore.Mvc;
using Shared.Api;

namespace Catalog.Api;

public class SearchController : BaseController
{
    [HttpGet]
    public Task<IActionResult> Search(string text)
    {
        throw new NotImplementedException();
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Order.Api;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "OrderApi test successed!";
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "IdentityApi test successed!";
    }
}

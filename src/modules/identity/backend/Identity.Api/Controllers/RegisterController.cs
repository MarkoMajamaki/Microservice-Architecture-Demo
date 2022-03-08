using Microsoft.AspNetCore.Mvc;
using Identity.Application;
using Shared.Api;

namespace Identity.Api;

[ApiController]
[Route("[controller]")]
public class RegisterController : BaseController
{
    [HttpPost]  
    [Route("register-email")]  
    public Task<IActionResult> Register([FromBody] RegisterModel model)  
    {  
        throw new NotImplementedException();        
        // return Ok(await Mediator.Send(new RegisterCommand(model.Email, model.Password)));  
    }

    [HttpPost]  
    [Route("register-admin")]  
    public Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)  
    {  
        throw new NotImplementedException();
        // return Ok(await Mediator.Send(new RegisterAdminCommand(model.Email, model.Password)));
    }
}
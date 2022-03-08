using Microsoft.AspNetCore.Mvc;
using Identity.Application;
using Shared.Api;

namespace Identity.Api;

public class LoginController : BaseController
{
    private readonly IFacebookAuthenticationService _facebookAuthService;
    private readonly IGoogleAuthenticationService _googleAuthService;

    public LoginController(
        IFacebookAuthenticationService facebookAuthService, 
        IGoogleAuthenticationService googleAuthService)
    {
        _facebookAuthService = facebookAuthService;
        _googleAuthService = googleAuthService;
    }

    [HttpPost]  
    [Route("login-email")]  
    public async Task<IActionResult> Login([FromBody] LoginCommand request)  
    { 
        // throw new NotImplementedException();
        return Ok(await Mediator.Send(request));
    }

    /// <summary>
    /// Login with facebook token. If user is not registered do registering automatically.
    /// </summary>
    [HttpPost]  
    [Route("login-facebook")]  
    public Task<IActionResult> LoginFacebook([FromBody] AccessTokenModel model)  
    { 
        throw new NotImplementedException();
        // return Ok(await Mediator.Send(new LoginExternalCommand(model.Token, _facebookAuthService)));
    }


    /// <summary>
    /// Login with Google token. If user is not registered do registering automatically.
    /// </summary>
    [HttpPost]  
    [Route("login-google")]  
    public Task<IActionResult> LoginGoogle([FromBody] AccessTokenModel model)  
    { 

        throw new NotImplementedException();
        // return Ok(await Mediator.Send(new LoginExternalCommand(model.Token, _googleAuthService)));      
    }
}
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;

using Identity.Domain;

namespace Identity.Application;

public class LoginExternalCommandHandler : IRequestHandler<LoginExternalCommand, LoginExternalCommandResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;  
    private readonly IAccessTokenGenerator _tokenGenerator;  

    public LoginExternalCommandHandler(
        UserManager<ApplicationUser> userManager, 
        IAccessTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    /// <summary>
    /// Login with external provider
    /// </summary>
    public async Task<LoginExternalCommandResponse> Handle(LoginExternalCommand request, CancellationToken cancellationToken)
    {
        UserInfo userInfo = await request.ExternalAuthenticationService.AuthenticateAsync(request.Token);
        
        var user = await _userManager.FindByEmailAsync(userInfo.Email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = userInfo.Email,
                UserName = userInfo.Email
            };

            var createdResult = await _userManager.CreateAsync(user);

            if (createdResult.Succeeded == false)
            {
                throw new Exception();
            }
        }
        string token = await _tokenGenerator.GenerateToken(user);

        return new LoginExternalCommandResponse(user.Id, token);            
    }
}
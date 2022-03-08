using MediatR;
using Microsoft.AspNetCore.Identity;

using Identity.Domain;

namespace Identity.Application;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        IAccessTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);  
        if (user == null)
        {
            throw new UserNotFoundException();
        }
        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isPasswordValid == false)
        {
            throw new InvalidPasswordException();
        }
        
        string token = await _tokenGenerator.GenerateToken(user);

        return new LoginCommandResponse(user.Id, token); 
    }
}
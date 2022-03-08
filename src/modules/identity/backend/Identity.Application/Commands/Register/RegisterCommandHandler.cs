using MediatR;
using Microsoft.AspNetCore.Identity;

using Identity.Domain;

namespace Identity.Application;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;  

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);  
        if (userExists != null)  
        {
            throw new UserAlreadyExistException();
        }

        var user = new ApplicationUser()  
        {  
            Email = request.Email,  
            UserName = request.Email,  
            SecurityStamp = Guid.NewGuid().ToString(),  
        };  
        
        var result = await _userManager.CreateAsync(user, request.Password);  
        
        if (!result.Succeeded)  
        {
            throw new UserCreationFailedException();
        }

        return true;
    }
}
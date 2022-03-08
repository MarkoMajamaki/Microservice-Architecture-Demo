using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application;

public class LoginCommand : IRequest<LoginCommandResponse>
{
    [Required(ErrorMessage = "Email is required")]  
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]  
    public string Password { get; set; }
}

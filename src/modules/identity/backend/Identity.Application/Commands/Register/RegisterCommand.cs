using MediatR;

namespace Identity.Application;

public class RegisterCommand : IRequest<bool>
{
    public string Email { get; init; }    
    public string Password { get; init; }  

    public RegisterCommand(string emain, string password)
    {
        Email = emain;
        Password = password;
    }
}
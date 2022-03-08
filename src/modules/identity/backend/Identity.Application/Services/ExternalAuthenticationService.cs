namespace Identity.Application;

public interface IExternalAuthenticationService
{
    Task<UserInfo> AuthenticateAsync(string accessToken);
}

public record UserInfo(String Email, string Name);
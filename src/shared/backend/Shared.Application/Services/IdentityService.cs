namespace Shared.Application;

public interface IIdentityService
{
    Guid? GetUserId();

    string GetUserName();
}
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shared.Application;

public interface IIdentityService
{
    Guid? GetUserId();

    string GetUserName();
}

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Guid? GetUserId()
    {
        string id = _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(id) == false)
        {
           return Guid.Parse(id); 
        }        

        return null;
    }

    public string GetUserName()
    {
        string username = _context.HttpContext.User.FindFirstValue(System.Security.Claims.ClaimTypes.Name);

        return _context.HttpContext.User.Identity.Name;
    }
}
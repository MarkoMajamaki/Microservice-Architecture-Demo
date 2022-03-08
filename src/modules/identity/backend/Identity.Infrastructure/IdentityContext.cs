using Microsoft.AspNetCore.Identity.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore;  

using Identity.Domain;  

namespace Identity.Infrastructure;
  
public class IdentityContext : IdentityDbContext<ApplicationUser>  
{  
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)  
    {  
    }  
}
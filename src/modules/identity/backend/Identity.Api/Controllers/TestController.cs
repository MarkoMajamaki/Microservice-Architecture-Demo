using Identity.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private IConfiguration _configuration;

    public TestController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    [Route("Test")]
    public string Test()
    {
        return "IdentityApi test successed!";
    }

    [HttpGet]
    [Route("RunMigration")]
    public void RunMigration()
    {            
        string connectionString = _configuration["IDENTITY:CONNECTIONSTRING"];                

        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

        optionsBuilder.UseSqlServer(
            connectionString, 
            x => x.MigrationsAssembly("Identity.Infrastructure"));

        using (var context = new IdentityContext(optionsBuilder.Options))
        {
            context.Database.Migrate();
        }        
    }
}

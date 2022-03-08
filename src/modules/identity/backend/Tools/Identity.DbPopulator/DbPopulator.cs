using Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Identity.DbPopulator
{
    public class DbPopulator
    {
        public static void Run(string connectionString)
        {
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
}
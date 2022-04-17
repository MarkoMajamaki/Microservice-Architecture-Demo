using Shared.Api;

namespace Inventory.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingcontext, config) =>
            {
                var env = hostingcontext.HostingEnvironment;

                // find the shared folder in the parent folder
                var sharedFolder = Path.Combine(env.ContentRootPath, "../../../../shared/backend/SharedApi");

                config
                .AddJsonFile(Path.Combine(sharedFolder, "appsettings.json"), optional: true)
                .AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env.EnvironmentName}.json"), optional: true)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

                // Get mountend secret from "secrets" foleder
                var secretsFolder = "/secrets";
                if (Directory.Exists(secretsFolder))
                {
                    System.Console.WriteLine($"Add secrets from mounted secrets folder");
                    config.AddKeyPerFile(secretsFolder, "--");
                }
                else
                {
                    System.Console.WriteLine($"Mounted secrets folder not found from path: {secretsFolder}");
                }
            })            
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

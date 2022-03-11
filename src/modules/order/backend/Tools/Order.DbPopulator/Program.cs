using Shared.Tools;

namespace Order.DbPopulator;

class Program
{
    static async Task Main(string[] args)
    {      
        try
        {
            Console.WriteLine("Start populating database!");
            await DbPopulator.RunAsync(ArgsParser.ParseConnectionString(args));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Database is populated successfully!");
        }
        catch (Exception x)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Database is populating failed!");
            Console.WriteLine(x.Message);
        }
    }
}
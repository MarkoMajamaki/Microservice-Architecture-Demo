using Shared.Tools;

namespace Order.Migrations;
class Program
{
    static void Main(string[] args)
    {
        Migrations.Run(ArgsParser.ParseConnectionString(args));
    }
}
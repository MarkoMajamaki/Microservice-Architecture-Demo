using Shared.Tools;

namespace Inventory.Migrations;
class Program
{
    static void Main(string[] args)
    {
        Migrations.Run(ArgsParser.ParseConnectionString(args));
    }
}
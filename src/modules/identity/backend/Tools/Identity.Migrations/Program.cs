using Shared.Tools;

namespace Identity.Migrations;
class Program
{
    static void Main(string[] args)
    {
        Migrations.Run(ArgsParser.ParseConnectionString(args));
    }
}
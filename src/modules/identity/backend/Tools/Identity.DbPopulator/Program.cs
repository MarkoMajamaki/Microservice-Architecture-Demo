using System;
using Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

using Shared.Tools;

namespace Identity.DbPopulator;

class Program
{
    static void Main(string[] args)
    {       
        DbPopulator.Run(ArgsParser.ParseConnectionString(args));
    }
}
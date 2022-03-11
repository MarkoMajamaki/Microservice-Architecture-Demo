using System;

namespace Shared.Tools;

public class ArgsParser
{
    /// <summary>
    /// Parse connection string 
    /// </summary>
    /// <returns>Return SQL Database connection string</returns>
    /// <param name="args">Command line arguments</param>
    /// <param name="databaseName">Database name to override command line arguments</param>
    /// <returns>Connection string to SQL Database</returns>
    public static string ParseConnectionString(string[] args, string databaseName = null)
    {
        string ip = null;
        string port = null;
        string dbName = databaseName ?? null;
        string user = null;
        string password = null;

        for (int i = 0; i < args.Length; i++)
        {
            string argument = args[i];
            
            switch (argument)
            {
                case "-ip":
                    ip = args[++i];
                    continue;
                case "-port":
                    port = args[++i];
                    continue;
                case "-n":
                    dbName = args[++i];
                    continue;
                case "-u":
                    user = args[++i];
                    continue;
                case "-p":
                    password = args[++i];
                    continue;
                default:
                    throw new ArgumentException("Uknown startup parameter");
            }
        }

        if (ip == null || port == null || dbName == null || user == null || password == null)
        {
            throw new ArgumentException("Argument missing");
        }

        return $"Server={ip},{port};Initial Catalog={dbName};User={user};Password={password}";
    }
}

using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;


namespace ConsoleBankApplication
{   
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            var section = Configuration.GetSection("name");
            string output =section.Value;
            Console.WriteLine(output);
            Console.WriteLine("hello");
            Bank b1= new Bank();
            b1.MainAtm();          
        }
    }
}

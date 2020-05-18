
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;


namespace ConsoleBankApplication
{
    
    class Program
    {

        static void Main(string[] args)
        {
            Bank b1= new Bank();
            b1.MainAtm();
            
        }

    }
}

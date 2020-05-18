using System;
using ConsoleBankApplication.models;
using Microsoft.Data.Sqlite;
namespace ConsoleBankApplication.DAO
{
    public class AccountDAO
    {
        string DataSource="./bank.db";
        
        SqliteConnection conn;
        
        public AccountDAO()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./bank.db";
            connectionStringBuilder.Mode=SqliteOpenMode.ReadWriteCreate;
            conn = new SqliteConnection(connectionStringBuilder.ConnectionString);
        }
        public User Login(int UserID,int Pin)
    
        {
            string selectQuery = "SELECT UserID,Name FROM Login where UserID = "+UserID + " AND Pin = " + Pin;
                             
            SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);

            
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            User user1=new User();

            while (reader.Read())
            {
                
            
                user1.UserID=reader.GetInt32(0);
                user1.Name=reader.GetString(1);

            }
            conn.Close();
            return user1;

        }
        public User ChangePin(User ID,int newpin)
        {
            string changeQuery = "UPDATE  Login SET Pin="+newpin+" where UserID = "+ ID.UserID;
                             
            SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);

            
            conn.Open();
            SqliteDataReader reader = updateCommand.ExecuteReader();
            User user2=new User();
           
            conn.Close();
            Console.WriteLine( " your pin has been changed sucessfully");
            return user2;

        }

        public Account CheckBalance(User ID)
        {
            string checkQuery = "SELECT Balance FROM Account where UserID = "+ ID.UserID;
                             
            SqliteCommand selectCommand = new SqliteCommand(checkQuery, conn);

            
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            Account account1=new Account();
            while (reader.Read())
            {
                
            
                account1.Balance=reader.GetInt32(0);             

            }
            conn.Close();
            Console.WriteLine("your Balancs is :"+account1.Balance);
            return account1;

        }

         public Transaction Transactions(User ID)
        {
            
            string countQuery = "SELECT  COUNT(UserID) FROM Transaction where UserID = "+ ID.UserID;
            SqliteCommand countCommand = new SqliteCommand(countQuery, conn);
            conn.Open();
            SqliteDataReader reader = countCommand.ExecuteReader();
            Transaction tran1=new Transaction();
            while (reader.Read())
            {
                
            
                 int num=reader.GetInt32(0); 
                 Console.WriteLine("count : "+num);
            }
            conn.Close();
            return tran1;
            /*
            for(int i=1 ; i<=2 ; i++)
            {
                string tranQuery = "SELECT  TranID,TranType,TranAmount,TranBalance FROM Transaction where UserID = "+ ID.UserID;
                             
                SqliteCommand selectCommand = new SqliteCommand(tranQuery, conn);

            
                conn.Open();
                SqliteDataReader reader1 = selectCommand.ExecuteReader();
                Transaction tran2=new Transaction();
                while (reader1.Read())
                 {
                     
                
            
                    tran2.TranID=reader1.GetInt32(0); 
                    tran2.TranType=reader1.GetString(0);  
                    tran2.TranAmount=reader1.GetInt32(0);  
                    tran2.TranBalance=reader1.GetInt32(0);              

                  }
           
                 conn.Close();
                 Console.WriteLine( tran2.TranID+" "+ tran2.TranType+" "+tran2.TranAmount+" "+ tran2.TranBalance );
                 return tran2;
                 
                
            }*/


            

        }
    }
}
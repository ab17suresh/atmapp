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

        public Account Deposit(User ID,int amt)
        {
            Console.WriteLine("dep amount= "+ amt);
  
            string selectQuery = "SELECT  Balance FROM  Account Where UserID = "+ ID.UserID;
                             
            SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
     
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            Account account2=new Account();
            while (reader.Read())
            {
      
                account2.Balance=reader.GetInt32(0);
            }
            //conn.Close();

  
            int Totamount=account2.Balance+ amt;           
            string changeQuery = "UPDATE Account SET Balance=" + Totamount+" WHERE UserID = "+ ID.UserID;
                             
            SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);

            
            //conn.Open();
            SqliteDataReader reader1 = updateCommand.ExecuteReader();
            Account account=new Account();
           
            //conn.Close();          
            Console.WriteLine( " your transaction is sucessfully");
            Console.WriteLine( " insert start");
            //conn.Open();
            string insertQuery = "INSERT INTO Transactions (,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+account2.AccountNO+",'D',datetime(now),"+amt+","+Totamount+")";           
            SqliteCommand insertCommand = new SqliteCommand(insertQuery, conn);
         
            Console.WriteLine( " insert end");
            insertCommand.ExecuteNonQuery();  
            conn.Close();

            Console.WriteLine( " your transaction is sucessfully");
            return account;
            

        }

        public Account Withdraw(User ID,int wamt)
        {
            
            //SqliteTransaction wdprocess = conn.BeginTransaction();
            //try
            //{
               
  
                string selectQuery = "SELECT  Balance FROM  Account Where UserID = "+ ID.UserID;
                             
                SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
     
                conn.Open();
                SqliteDataReader reader = selectCommand.ExecuteReader();
                Account account3=new Account();
                reader.Read();

                account3.Balance=reader.GetInt32(0);
                    
                
                
                if(account3.Balance >= wamt)
                {
                    
                    int NewBalance= account3.Balance - wamt;
                    string changeQuery = "UPDATE Account SET Balance=" + NewBalance+" WHERE UserID = "+ ID.UserID;
                             
                    SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
                    SqliteDataReader reader1 = updateCommand.ExecuteReader();
                    Account account=new Account();


                }
                else
                {
                    Console.WriteLine("low balance");
                }
                conn.Close();
                
                //wdprocess.Commit();
                Console.WriteLine("transaction is Completed!!! ");
                return account3;

            //}
            //catch(Exception e)
            //{
             //   Console.WriteLine(e);
            //    wdprocess.Rollback();
             //   
             //   Console.WriteLine("transaction failed!!! ");

            //}
            

        } 


        public Account CheckBalance(User ID)
        {
            string checkQuery = "SELECT Balance FROM Account where UserID = "+ ID.UserID;
                             
            SqliteCommand selectCommand = new SqliteCommand(checkQuery, conn);

            
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            Account account1=new Account();
            reader.Read();
            account1.Balance=reader.GetInt32(0);  
                         

            
            conn.Close();
            Console.WriteLine("your Balancs is :"+account1.Balance);
            return account1;

        }

         public Transaction[] Transactions(User ID)
        {
           
            string countQuery = "SELECT  COUNT(*) FROM Transactions WHERE UserID = "+ ID.UserID;
            SqliteCommand countCommand = new SqliteCommand(countQuery, conn);
            
            conn.Open();
            
            Int64 num2=(Int64)countCommand.ExecuteScalar();
        
            Transaction[] transaction1= new Transaction[num2];

            string tranQuery = "SELECT  TranID,AccountNO,TranType,TranAmount,Balance FROM Transactions WHERE UserID = "+ ID.UserID;                           
            SqliteCommand selectCommand = new SqliteCommand(tranQuery, conn);    
            SqliteDataReader reader1 = selectCommand.ExecuteReader();
            
            int i=0;
            Console.WriteLine("tranID AccountNO Trantype TranAmount Balance");
            while (reader1.Read())
                {
                Transaction tran2=new Transaction();
                tran2.TranID=reader1.GetInt32(0); 
                tran2.AccountNO=reader1.GetInt32(1);
                tran2.TranType=reader1.GetString(2);  
                tran2.TranAmount=reader1.GetInt32(3);  
                tran2.Balance=reader1.GetInt32(4); 
                
                transaction1[i] = tran2;
                Console.WriteLine( "  "+tran2.TranID+"     "+tran2.AccountNO+"    "+ tran2.TranType+"      "+tran2.TranAmount+"      "+ tran2.Balance );
                
                i++;            

                }
        
                conn.Close();
                //
                return transaction1;
                 
                 
        }   


           
            


            

        
    }
}
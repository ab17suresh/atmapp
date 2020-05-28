using System;
using ConsoleBankApplication.models;
using Microsoft.Data.Sqlite;
namespace ConsoleBankApplication.DAO
{
    public class AccountDAO
    {
        //string DataSource="./bank.db";    
        SqliteConnection conn;    
        public AccountDAO()
        {
            /*IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            var section = Configuration.GetSection("ConnectionString");
            myConnectionString = section.Value;*/
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
            reader.Read();
                user1.UserID=reader.GetInt32(0);
                user1.Name=reader.GetString(1);
            conn.Close();
            return user1;
        }

        public User ChangePin(User ID,int newpin)
        {
            string changeQuery = "UPDATE  Login SET Pin="+newpin+" where UserID = "+ ID.UserID;                            
            SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);       
            conn.Open();
            updateCommand.ExecuteScalar();        
            conn.Close();
            Console.WriteLine( " your pin has been changed sucessfully");
            return ID;
        }

        public Account Deposit(User ID,int amt)
        {
            string selectQuery = "SELECT  Balance,AccountNO FROM  Account Where UserID = "+ ID.UserID;                           
            SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
            conn.Open();
            SqliteDataReader reader = selectCommand.ExecuteReader();
            Account account2=new Account();
            while (reader.Read())
            {    
                account2.Balance=reader.GetInt32(0);
                account2.AccountNO=reader.GetInt32(1);
            }
            int Totamount=account2.Balance+ amt;           
            string changeQuery = "UPDATE Account SET Balance=" + Totamount+" WHERE UserID = "+ ID.UserID;                          
            SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
            updateCommand.ExecuteScalar();        
            DateTime now=DateTime.Now;          
            string insertQuery = "INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+ID.UserID+","+account2.AccountNO+",'D','"+now+"',"+amt+","+Totamount+")";                      
            SqliteCommand insertCommand = new SqliteCommand(insertQuery, conn);        
            insertCommand.ExecuteNonQuery();  
            conn.Close();
            Console.WriteLine( " your transaction is sucessfully");
            return account2;
        }

        public Account Withdraw(User ID,int wamt)
        {
            conn.Open();
            //SqliteTransaction wdprocess = conn.BeginTransaction();
            //try
            //{
                string selectQuery = "SELECT  Balance,AccountNO FROM  Account Where UserID = "+ ID.UserID;                            
                SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);
              
                SqliteDataReader reader = selectCommand.ExecuteReader();
                Account account3=new Account();
                reader.Read();
                account3.Balance=reader.GetInt32(0);
                account3.AccountNO=reader.GetInt32(1);                   
                
                if(account3.Balance >= wamt)
                {                   
                    int NewBalance= account3.Balance - wamt;
                    string changeQuery = "UPDATE Account SET Balance=" + NewBalance+" WHERE UserID = "+ ID.UserID;                            
                    SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
                    updateCommand.ExecuteScalar();               
                    DateTime now=DateTime.Now;
                    string insertQuery ="INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+ID.UserID+","+account3.AccountNO+",'D','"+now+"',"+wamt+","+NewBalance+")";
                    SqliteCommand insertCommand = new SqliteCommand(insertQuery, conn);                
                    insertCommand.ExecuteNonQuery();  
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
               // Console.WriteLine(e);
                //wdprocess.Rollback();
                //Console.WriteLine("transaction failed!!! ");
                
            //}
            
            //return null;
        } 

        public Account Transfer(User ID,int sact,int samt)
        {  
            conn.Open(); 
            //SqliteTransaction wdprocess = conn.BeginTransaction();
            //try
            //{
                string selectQuery = "SELECT  AccountNO,Balance FROM  Account Where UserID = "+ ID.UserID;                        
                SqliteCommand selectCommand = new SqliteCommand(selectQuery, conn);      
                SqliteDataReader reader = selectCommand.ExecuteReader();
                Account account3=new Account();
                reader.Read(); 
                account3.AccountNO=reader.GetInt32(0);                   
                account3.Balance=reader.GetInt32(1);    
                string selectQuery1 = "SELECT  UserID,Balance FROM  Account Where AccountNO = "+ sact;           
                SqliteCommand selectCommand1 = new SqliteCommand(selectQuery1, conn); 
                SqliteDataReader reader4 = selectCommand1.ExecuteReader();
                Account account4=new Account();
                reader4.Read();  
                account4.UserID= reader4.GetInt32(0); 
                account4.Balance= reader4.GetInt32(1);  
                if(account3.Balance >= samt)
                    {
                        int SenderBalance= account3.Balance - samt;
                        string changeQuery = "UPDATE Account SET Balance=" + SenderBalance+" WHERE UserID = "+ ID.UserID;              
                        SqliteCommand updateCommand = new SqliteCommand(changeQuery, conn);
                        updateCommand.ExecuteScalar();                   
                        DateTime now=DateTime.Now;
                        string insertQuery ="INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+ID.UserID+","+account3.AccountNO+",'D','"+now+"',"+samt+","+SenderBalance+")";
                        SqliteCommand insertCommand = new SqliteCommand(insertQuery, conn);               
                        insertCommand.ExecuteNonQuery();  
                        int ReciverBalance= account4.Balance + samt; 
                        string changeQuery1 = "UPDATE Account SET Balance=" + ReciverBalance+" WHERE AccountNO = "+ sact;         
                        SqliteCommand updateCommand1 = new SqliteCommand(changeQuery1, conn);
                        updateCommand1.ExecuteScalar();
                        string insertQuery2 ="INSERT INTO Transactions (UserID,AccountNO,TranType,TranDate,TranAmount,Balance) Values ("+account4.UserID+","+sact+",'C','"+now+"',"+samt+","+ReciverBalance+")";
                        SqliteCommand insertCommand2 = new SqliteCommand(insertQuery2, conn);
                        insertCommand2.ExecuteNonQuery();  
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
            account1.Balance= reader.GetInt32(0);
            conn.Close();
            return account1;
        }

         public Transaction[] Transactions(User ID)
        {  
            string countQuery = "SELECT  COUNT(*) FROM Transactions WHERE UserID = "+ ID.UserID;
            SqliteCommand countCommand = new SqliteCommand(countQuery, conn);           
            conn.Open();           
            Int64 num2=(Int64)countCommand.ExecuteScalar();        
            Transaction[] transaction1= new Transaction[num2];
            string tranQuery = "SELECT  TranID,AccountNO,TranType,TranAmount,Balance,TranDate FROM Transactions WHERE UserID = "+ ID.UserID;                           
            SqliteCommand selectCommand = new SqliteCommand(tranQuery, conn);    
            SqliteDataReader reader1 = selectCommand.ExecuteReader();            
            int i=0;
            Console.WriteLine(" tranID AccountNO Trantype TranAmount   Balance     date");
            while (reader1.Read())
                {
                Transaction tran2=new Transaction();
                tran2.TranID=reader1.GetInt32(0); 
                tran2.AccountNO=reader1.GetInt32(1);
                tran2.TranType=reader1.GetString(2);  
                tran2.TranAmount=reader1.GetInt32(3);  
                tran2.Balance=reader1.GetInt32(4); 
                tran2.TranDate=reader1.GetString(5); 
                DateTime now=DateTime.Now;               
                transaction1[i] = tran2;
                Console.WriteLine( "  "+tran2.TranID+"     "+tran2.AccountNO+"    "+ tran2.TranType+"      "+tran2.TranAmount+"      "+ tran2.Balance+"      "+ tran2.TranDate );              
                i++;            
                }
                conn.Close();
                return transaction1;                              
        }   
        
    }
}
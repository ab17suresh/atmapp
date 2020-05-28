using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;
using ConsoleBankApplication.DAO;
using ConsoleBankApplication.models;

namespace ConsoleBankApplication
{
    public class Bank
    {
    int menu1 =0 ;
     int menu2 = 0;
     public int UserID = 0;
     AccountDAO accountdao;
     public Bank()
     {
         this.accountdao =new AccountDAO();
     }
     int Pin = 0;   
         //login menu
         public static void showMenu1()
         {
             Console.WriteLine(" ------------------------");
             Console.WriteLine("|   C ATM Main Menu      |");
             Console.WriteLine("|                        |");
             Console.WriteLine("| 1. Login to ATM card   |");
             Console.WriteLine("| 2. Exit                |");
             Console.WriteLine("|                        |");
             Console.WriteLine(" ------------------------");
             Console.Write("Enter your option: ");            
         }
        //main menu
         public static void showMenu2()
         {
             DateTime now=DateTime.Now;
             Console.WriteLine(" ------------------------------------------");
             Console.WriteLine("|                    "+now+" |");
             Console.WriteLine("|                                          |");
             Console.WriteLine("|          C ATM BANK LIMITED              |");
             Console.WriteLine("|           Customer Banking               |");  
             Console.WriteLine("|                                          |");      
             Console.WriteLine("|    1. Deposit Funds                      |");
             Console.WriteLine("|    2. Withdraw Funds                     |");
             Console.WriteLine("|    3. Transfer Funds                     |");
             Console.WriteLine("|    4. Check Account balance              |");
             Console.WriteLine("|    5. Display Account Transaction Log    |");
             Console.WriteLine("|    6. Change Pin                         |");
             Console.WriteLine("|                                          |");
             Console.WriteLine("|    0.Save & Exit                         |");
             Console.WriteLine(" ------------------------------------------");
             Console.Write("Enter your option: ");
         }   
 
        //main operation funtion
        public void  MainAtm()
         {   
                 showMenu1();          
                 menu1 = Convert.ToInt32( Console.ReadLine());
                     switch (menu1)
                     {
                         case 1:
                             Console.Write("Enter ATM UserID: ");
                             UserID = Convert.ToInt32(Console.ReadLine());
                             Console.Write("Enter  PIN: ");
                             Pin = Convert.ToInt32(Console.ReadLine());
                             User User1=this.accountdao.Login(UserID,Pin);                  
                             if(User1.Name != null)                            
                             {                           
                                  Console.WriteLine("hi " +User1.Name +"...!");                                
                                  showMenu2();
                                  menu2 = Convert.ToInt32( Console.ReadLine());                                                                                                                      
                                        switch (menu2)
                                        {                                        
                                            case 1:
                                                //Deposit                                               
                                                Console.Write("Enter amount : ");
                                                int DepositAmount = Convert.ToInt32(Console.ReadLine());
                                                Account account2=this.accountdao.Deposit(User1,DepositAmount);    
                                                Console.Beep();
                                                break;                                               
                                            case 2:
                                                //Withdraw
                                                Console.Write("Enter amount : ");
                                                int WithdrawAmount = Convert.ToInt32(Console.ReadLine());
                                                Account account3=this.accountdao.Withdraw(User1,WithdrawAmount);
                                                Console.Beep();
                                                break;
                                            case 3:
                                                //Transfer                                           
                                                Console.Write("Enter account NO : ");
                                                int accountno = Convert.ToInt32(Console.ReadLine());
                                                Console.Write("Enter amount : ");
                                                int sendingAmount = Convert.ToInt32(Console.ReadLine());
                                                Account account4=this.accountdao.Transfer(User1,accountno,sendingAmount);
                                                Console.Beep();
                                                break;
                                            case 4:
                                                //CheckBalance
                                                Account account1=this.accountdao.CheckBalance(User1);
                                                Console.WriteLine("your Balancs is :"+account1.Balance);
                                                Console.Beep();
                                                break;    
                                            case 5:
                                                //Transaction
                                                Transaction[] transaction1=this.accountdao.Transactions(User1);    
                                                Console.Beep();                                          
                                                break; 
                                            case 6:
                                                //ChangePin
                                                Console.Write("Enter New Pin : ");
                                                int NewPin = Convert.ToInt32(Console.ReadLine());
                                                User User2=this.accountdao.ChangePin(User1,NewPin);
                                                Console.Beep();
                                                break; 
                                            case 0:
                                                Console.WriteLine("  logout succesfully.");
                                                break;
                                            default:
                                                Console.WriteLine("Invalid Option Entered.");
                                                MainAtm();
                                                break;
                                        }
                                    }
                            else 
                            {
                                

                                Console.WriteLine("Invalid PIN.");
                            }                            
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Invalid Option Entered.");
                            MainAtm();
                            break;
                    }
                
                     Console.WriteLine("Thank you for using C ATM Bank. ");
                    
         } 
    }
}
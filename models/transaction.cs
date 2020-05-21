namespace ConsoleBankApplication.models
{
    public class Transaction
    {
       public int TranID{get;set;} 
       public int userID{get;set;} 
       public int AccountNO{get;set;} 
       public string TranType{get;set;} 
       public string TranDate{get;set;} 
       public int TranAmount{get;set;} 
       public int Balance{get;set;} 
    }
}
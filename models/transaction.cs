namespace ConsoleBankApplication.models
{
    public class Transaction
    {
       public int TranID{get;set;} 
       public int AccountNO{get;set;} 
       public string TranType{get;set;} 
       public int TranDate{get;set;} 
       public int TranAmount{get;set;} 
       public int TranBalance{get;set;} 
    }
}
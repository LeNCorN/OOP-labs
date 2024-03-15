namespace OOP_ICT.Second.Models;

public class PlayerAccountInfo
{
    public string AccountName { get; }
    public decimal Balance { get; }
    
    public PlayerAccountInfo(string accountName, decimal balance)
    {
        AccountName = accountName;
        Balance = balance;
    }
}
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.Models;

public class PlayerAccount
{
    public string AccountName { get; private set; } = "Default Account Name";
    public decimal Balance { get; private set; } = 0;

    public void SetBalance(decimal balance)
    {
        if (balance < 0)
        {
            throw PlayerAccountException.NegativeValue("Balance cannot be negative value!");
        }
        
        Balance = balance;
    }

    public void SetAccountName(string accountName)
    {
        if (accountName.Length is < 10 or > 100)
        {
            throw PlayerAccountException.InvalidAccountName(
                "Length of the account name should be from 10 to 100 characters!");
        }
        
        AccountName = accountName;
    }
    
    public void IncreaseBalance(decimal amount)
    {
        if (amount < 0)
        {
            throw PlayerAccountException.NegativeValue("Amount for increase cannot be negative value!");
        }
        
        Balance += amount;
    }

    public void DecreaseBalance(decimal amount)
    {
        if (amount < 0)
        {
            throw PlayerAccountException.NegativeValue("Amount for decrease cannot be negative value!");
        }
        
        if (Balance < amount)
        {
            throw PlayerAccountException.InsufficientBalance("Insufficient account balance!");
        }
        
        Balance -= amount;
    }
}
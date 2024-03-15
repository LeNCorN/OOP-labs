namespace OOP_ICT.Second.Models;

public class PlayerAccountBuilder
{
    private readonly PlayerAccount _playerAccount = new PlayerAccount();

    public PlayerAccountBuilder SetBalance(decimal balance)
    {
        _playerAccount.SetBalance(balance);
        return this;
    }

    public PlayerAccountBuilder SetAccountName(string accountName)
    {
        _playerAccount.SetAccountName(accountName);
        return this;
    }

    public PlayerAccount BuildPlayerAccount()
    {
        return _playerAccount;
    }
}
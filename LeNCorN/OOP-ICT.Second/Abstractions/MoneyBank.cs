using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.Abstractions;

public abstract class MoneyBank
{
    private readonly IPlayerAccountRepository _repository;
    
    protected MoneyBank(IPlayerAccountRepository repository)
    {
        _repository = repository ?? throw MoneyBankException.NullReference("Repository cannot be null!");
    }
    
    public void AddAmountToPlayerBalance(Guid playerUuid, decimal amount)
    {
        var playerAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        playerAccount.IncreaseBalance(amount);
    }
    
    public void SubtractAmountFromPlayerBalance(Guid playerUuid, decimal amount)
    {
        var playerAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        playerAccount.DecreaseBalance(amount);
    }

    public void CreateNewAccountForPlayer(Player player)
    {
        if (player is null)
        {
            throw MoneyBankException.NullReference("Player cannot be null!");
        }
        
        var newPlayerAccount = CreatePlayerAccount(player);
        _repository.SavePlayerAccount(player.Uuid, newPlayerAccount);
    }

    public PlayerAccountInfo GetPlayerAccountInfo(Guid playerUuid)
    {
        var playerAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        return new PlayerAccountInfo(playerAccount.AccountName, playerAccount.Balance);
    }

    protected abstract PlayerAccount CreatePlayerAccount(Player player);
}
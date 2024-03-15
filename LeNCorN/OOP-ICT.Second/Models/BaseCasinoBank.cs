using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.Models;

public class BaseCasinoBank : ICasinoBank
{
    private readonly IPlayerAccountRepository _repository;
    private readonly MoneyBank _moneyBank;
    
    public BaseCasinoBank(IPlayerAccountRepository repository, MoneyBank moneyBank)
    {
        _repository = repository;
        _moneyBank = moneyBank;
    }
    
    public void BuyChipsForPlayer(Guid playerUuid, decimal chipsCount)
    {
        var chipsPrice = chipsCount * Constants.ChipPrice;
        _moneyBank.SubtractAmountFromPlayerBalance(playerUuid, chipsPrice);
        
        var playerCasinoAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        playerCasinoAccount.IncreaseBalance(chipsCount);
    }

    public void SellChipsForRealMoney(Guid playerUuid, decimal chipsCount)
    {
        var playerCasinoAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        playerCasinoAccount.DecreaseBalance(chipsCount);
        
        var chipsPrice = chipsCount * Constants.ChipPrice;
        _moneyBank.AddAmountToPlayerBalance(playerUuid, chipsPrice);
    }

    public void AddChipsToPlayerCasinoBalance(Guid playerUuid, decimal chipsCount)
    {
        var playerCasinoAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        playerCasinoAccount.IncreaseBalance(chipsCount);
    }

    public void SubtractChipsFromPlayerCasinoBalance(Guid playerUuid, decimal chipsCount)
    {
        var playerCasinoAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        playerCasinoAccount.DecreaseBalance(chipsCount);
    }

    public bool CheckIsPlayerCasinoBalanceSufficient(Guid playerUuid, decimal chipsCount)
    {
        var playerCasinoAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        return playerCasinoAccount.Balance >= chipsCount;
    }

    public PlayerAccountInfo GetPlayerCasinoAccountInfo(Guid playerUuid)
    {
        var playerCasinoAccount = _repository.FindPlayerAccountByPlayerUuid(playerUuid);
        return new PlayerAccountInfo(playerCasinoAccount.AccountName, playerCasinoAccount.Balance);
    }

    public void CreateNewCasinoAccountForPlayer(Player player)
    {
        if (player is null)
        {
            throw CasinoBankException.NullReference("Player cannot be null!");
        }
        
        var newPlayerCasinoAccount = new PlayerAccountBuilder()
            .SetBalance(0)
            .SetAccountName($"Player`s bank of chips {player.Name} {player.Surname}")
            .BuildPlayerAccount();
        
        _repository.SavePlayerAccount(player.Uuid, newPlayerCasinoAccount);
    }
}
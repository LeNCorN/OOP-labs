using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.Abstractions;

public interface ICasinoBank
{
    void BuyChipsForPlayer(Guid playerUuid, decimal chipsCount);
    void SellChipsForRealMoney(Guid playerUuid, decimal chipsCount);
    void AddChipsToPlayerCasinoBalance(Guid playerUuid, decimal chipsCount);
    void SubtractChipsFromPlayerCasinoBalance(Guid playerUuid, decimal chipsCount);
    bool CheckIsPlayerCasinoBalanceSufficient(Guid playerUuid, decimal chipsCount);
    PlayerAccountInfo GetPlayerCasinoAccountInfo(Guid playerUuid);
    void CreateNewCasinoAccountForPlayer(Player player);
}
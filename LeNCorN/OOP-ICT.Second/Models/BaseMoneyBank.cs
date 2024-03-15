using OOP_ICT.Second.Abstractions;

namespace OOP_ICT.Second.Models;

public class BaseMoneyBank : MoneyBank
{
    public BaseMoneyBank(IPlayerAccountRepository repository) : base(repository) {}

    protected override PlayerAccount CreatePlayerAccount(Player player)
    {
        var playerAccountBuilder = new PlayerAccountBuilder();
        return playerAccountBuilder
            .SetBalance(0)
            .SetAccountName($"Player`s bank of money {player.Name} {player.Surname}")
            .BuildPlayerAccount();
    }
}
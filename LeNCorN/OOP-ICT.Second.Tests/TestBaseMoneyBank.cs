using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestBaseMoneyBank
{
    private readonly MoneyBank _bank = new BaseMoneyBank(new PlayerAccountRepository());
    private readonly Player _player = new Player("Test", "Player");

    public TestBaseMoneyBank()
    {
        _bank.CreateNewAccountForPlayer(_player);
    }

    [Fact]
    public void TestIsAddAmountToPlayerBalanceWorkCorrect_ReturnTrue()
    {
        _bank.AddAmountToPlayerBalance(_player.Uuid, 100);
        var playerAccountInfo = _bank.GetPlayerAccountInfo(_player.Uuid);
        
        Assert.Equal(100, playerAccountInfo.Balance);
    }

    [Fact]
    public void TestIsSubtractAmountFromPlayerBalanceWorkCorrect_ReturnTrue()
    {
        _bank.AddAmountToPlayerBalance(_player.Uuid, 100);
        _bank.SubtractAmountFromPlayerBalance(_player.Uuid, 100);
        var playerAccountInfo = _bank.GetPlayerAccountInfo(_player.Uuid);
        
        Assert.Equal(0, playerAccountInfo.Balance);
    }
}
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestBaseCasinoBank
{
    private readonly Player _player = new Player("Test", "Player");
    private readonly MoneyBank _moneyBank;
    private readonly ICasinoBank _casinoBank;
    
    public TestBaseCasinoBank()
    {
        _moneyBank = new BaseMoneyBank(new PlayerAccountRepository());
        _casinoBank = new BaseCasinoBank(new PlayerAccountRepository(), _moneyBank);
        
        _moneyBank.CreateNewAccountForPlayer(_player);
        _moneyBank.AddAmountToPlayerBalance(_player.Uuid, 100);
        
        _casinoBank.CreateNewCasinoAccountForPlayer(_player);
        _casinoBank.AddChipsToPlayerCasinoBalance(_player.Uuid, 100);
    }
    
    [Fact]
    public void TestIsAddChipsToPlayerCasinoBalanceWorkCorrect_ReturnTrue()
    {
        _casinoBank.AddChipsToPlayerCasinoBalance(_player.Uuid, 10);
        var playerCasinoAccountInfo = _casinoBank.GetPlayerCasinoAccountInfo(_player.Uuid);
        
        Assert.Equal(110, playerCasinoAccountInfo.Balance);
    }
    
    [Fact]
    public void TestIsSubtractChipsFromPlayerCasinoBalanceWorkCorrect_ReturnTrue()
    {
        _casinoBank.SubtractChipsFromPlayerCasinoBalance(_player.Uuid, 10);
        var playerCasinoAccountInfo = _casinoBank.GetPlayerCasinoAccountInfo(_player.Uuid);
        
        Assert.Equal(90, playerCasinoAccountInfo.Balance);
    }
    
    [Fact]
    public void TestIsCheckIsPlayerCasinoBalanceSufficientWorkCorrect_ReturnTrue()
    {
        Assert.True(_casinoBank.CheckIsPlayerCasinoBalanceSufficient(_player.Uuid, 100));
        Assert.False(_casinoBank.CheckIsPlayerCasinoBalanceSufficient(_player.Uuid, 200));
    }
    
    [Fact]
    public void TestIsBuyChipsForPlayerWorkCorrect_ReturnTrue()
    {
        _casinoBank.BuyChipsForPlayer(_player.Uuid, 10);
        var playerMoneyAccountInfo = _moneyBank.GetPlayerAccountInfo(_player.Uuid);
        Assert.Equal(100 - Constants.ChipPrice * 10, playerMoneyAccountInfo.Balance);
        
        var playerCasinoAccountInfo = _casinoBank.GetPlayerCasinoAccountInfo(_player.Uuid);
        Assert.Equal(110, playerCasinoAccountInfo.Balance);
    }

    [Fact]
    public void TestIsSellChipsForRealMoneyWorkCorrect_ReturnTrue()
    {
        _casinoBank.SellChipsForRealMoney(_player.Uuid, 100);
        var playerMoneyAccountInfo = _moneyBank.GetPlayerAccountInfo(_player.Uuid);
        Assert.Equal(100 + Constants.ChipPrice * 100, playerMoneyAccountInfo.Balance);
        
        var playerCasinoAccountInfo = _casinoBank.GetPlayerCasinoAccountInfo(_player.Uuid);
        Assert.Equal(0, playerCasinoAccountInfo.Balance);
    }
}
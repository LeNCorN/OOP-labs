using OOP_ICT.Second.Exceptions;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestPlayerAccount
{
    [Fact]
    public void TestThrowNegativeBalanceException_ReturnTrue()
    {
        var playerAccount = new PlayerAccount();
        var exception = Assert.Throws<PlayerAccountException>(() => playerAccount.SetBalance(-100));
        Assert.Equal("Balance cannot be negative value!", exception.Message);
    }

    [Fact]
    public void TestIsIncreaseBalanceWorkCorrect_ReturnTrue()
    {
        var playerAccount = new PlayerAccount();
        playerAccount.IncreaseBalance(amount: 100);
        Assert.Equal(100, playerAccount.Balance);
    }
    
    [Fact]
    public void TestIsDecreaseBalanceWorkCorrect_ReturnTrue()
    {
        var playerAccount = new PlayerAccount();
        playerAccount.SetBalance(100);
        
        playerAccount.DecreaseBalance(50);
        var exception = Assert.Throws<PlayerAccountException>(
            () => playerAccount.DecreaseBalance(100));
        
        Assert.Equal("Insufficient account balance!", exception.Message);
        Assert.Equal(50, playerAccount.Balance);
    }
}
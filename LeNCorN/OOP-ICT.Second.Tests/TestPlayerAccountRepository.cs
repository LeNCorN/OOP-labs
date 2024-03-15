using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestPlayerAccountRepository
{
    private readonly Guid _playerUuid = Guid.NewGuid();
    private readonly IPlayerAccountRepository _repository = new PlayerAccountRepository();

    public TestPlayerAccountRepository()
    {
        var playerAccount = new PlayerAccount();
        _repository.SavePlayerAccount(_playerUuid, playerAccount);
    }
    
    [Fact]
    public void TestThrowPlayerAccountRepositoryException_ReturnTrue()
    {
        var playerAccountNotExistsException = Assert.Throws<PlayerAccountRepositoryException>(
            () => _repository.FindPlayerAccountByPlayerUuid(Guid.NewGuid()));

        var playerAccountAlreadyExistsException = Assert.Throws<PlayerAccountRepositoryException>(
            () => _repository.SavePlayerAccount(_playerUuid, new PlayerAccount()));
        
        Assert.Equal("Player account does not exists!",playerAccountNotExistsException.Message);
        Assert.Equal("Player account already exists!",playerAccountAlreadyExistsException.Message);
    }
}
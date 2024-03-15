using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Third.Exceptions;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestBlackjackPlayer
{
    private readonly BlackjackPlayer _player = new BlackjackPlayer(new Player("Shayt", "Ilya"));
        
    [Fact]
    public void TestIncreaseCurrentBetMethod()
    {
        var exception = Assert.Throws<CardPlayerException>(
            () => _player.IncreaseCurrentBet(-100));
        
        Assert.Equal("Bet cannot be negative value!", exception.Message);
        
        _player.IncreaseCurrentBet(100);
        _player.IncreaseCurrentBet(50);
        Assert.Equal(150, _player.CurrentBet);
    }

    [Fact]
    public void TestSetInitialCardsMethod()
    {
        var initialCards = new List<Card>
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.Queen),
        };
        
        var exception = Assert.Throws<CardPlayerException>(
            () => _player.SetInitialCards(initialCards));
        
        Assert.Equal("Initial card count must be 2!", exception.Message);
        
        initialCards.RemoveAt(0);
        _player.SetInitialCards(initialCards);
        Assert.Equal(2, _player.GetCards().Count);
    }
}
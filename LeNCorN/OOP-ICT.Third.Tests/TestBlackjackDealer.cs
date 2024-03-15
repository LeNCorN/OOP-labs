using OOP_ICT.Models;
using OOP_ICT.Third.Exceptions;
using OOP_ICT.Third.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestBlackjackDealer
{
    private readonly BlackjackDealer _dealer;

    public TestBlackjackDealer()
    {
        var dealerInstance = new Dealer(new CardDeck());
        _dealer = new BlackjackDealer(dealerInstance);
    }
    
    [Fact]
    public void TestSetInitialCardsMethod()
    {
        var initialCards = new List<Card>
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.King),
        };
        
        var exception = Assert.Throws<CardPlayerException>(
            () => _dealer.SetInitialCards(initialCards));
        
        Assert.Equal("Initial card count must be 1!", exception.Message);
        
        initialCards.RemoveAt(0);
        _dealer.SetInitialCards(initialCards);
        Assert.Single(_dealer.GetCards());
    }
}
using OOP_ICT.Fourth.Models.CardCombinations;
using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class CardCombinationTest
{
    [Fact]
    public void TestOnePair_ReturnTrue()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Spades, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Queen),
            new Card(CardSuit.Hearts, CardValue.Queen),
            new Card(CardSuit.Hearts, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };
        
        var onePair = new OnePair();
        Assert.True(onePair.Check(cards));
    }
    
    [Fact]
    public void TestOnePair_ReturnFalse()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Queen),
            new Card(CardSuit.Clubs, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };
        
        var onePair = new OnePair();
        Assert.False(onePair.Check(cards));
    }
    
    [Fact]
    public void TestTwoPair_ReturnTrue()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Spades, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Hearts, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.King),
            new Card(CardSuit.Spades, CardValue.Ten),
        };
        
        var twoPair = new TwoPair();
        Assert.True(twoPair.Check(cards));
    }

    [Fact]
    public void TestTwoPair_ReturnFalse()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Diamonds, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Queen),
            new Card(CardSuit.Hearts, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };
        
        var twoPair = new TwoPair();
        Assert.False(twoPair.Check(cards));
    }
    
    [Fact]
    public void TestThreeOfKind_ReturnTrue()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };
        
        var threeOfKind = new ThreeOfKind();
        Assert.True(threeOfKind.Check(cards));
    }
    
    [Fact]
    public void TestThreeOfKind_ReturnFalse()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };

        var threeOfKind = new ThreeOfKind();
        Assert.False(threeOfKind.Check(cards));
    }
    
    [Fact]
    public void TestStraight_ReturnTrue()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Spades, CardValue.Ace),
            new Card(CardSuit.Diamonds, CardValue.King),
            new Card(CardSuit.Diamonds, CardValue.Queen),
            new Card(CardSuit.Hearts, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };
        
        var straight = new Straight();
        Assert.True(straight.Check(cards));
    }
    
    [Fact]
    public void TestStraight_ReturnFalse()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Spades, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Spades, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.Jack),
            new Card(CardSuit.Diamonds, CardValue.Seven),
        };
        
        var straight = new Straight();
        Assert.False(straight.Check(cards));
    }
    
    [Fact]
    public void TestFullHouse_ReturnTrue()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Hearts, CardValue.Queen),
            new Card(CardSuit.Diamonds, CardValue.Queen),
            new Card(CardSuit.Hearts, CardValue.Queen),
            new Card(CardSuit.Spades, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Jack),
        };
        
        var fullHouse = new FullHouse();
        Assert.True(fullHouse.Check(cards));
    }
    
    [Fact]
    public void TestFullHouse_ReturnFalse()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Diamonds, CardValue.Ace),
            new Card(CardSuit.Diamonds, CardValue.King),
            new Card(CardSuit.Diamonds, CardValue.Queen),
            new Card(CardSuit.Hearts, CardValue.Queen),
            new Card(CardSuit.Clubs, CardValue.Queen),
        };
        
        var fullHouse = new FullHouse();
        Assert.False(fullHouse.Check(cards));
    }
    
    [Fact]
    public void TestRoyalFlush_ReturnTrue()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Clubs, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.Queen),
            new Card(CardSuit.Clubs, CardValue.Jack),
            new Card(CardSuit.Clubs, CardValue.Ten),
        };
        
        var royalFlush = new RoyalFlush();
        Assert.True(royalFlush.Check(cards));
    }
    
    [Fact]
    public void TestRoyalFlush_ReturnFalse()
    {
        var cards = new List<Card>()
        {
            new Card(CardSuit.Clubs, CardValue.Ace),
            new Card(CardSuit.Diamonds, CardValue.King),
            new Card(CardSuit.Clubs, CardValue.Queen),
            new Card(CardSuit.Clubs, CardValue.Queen),
            new Card(CardSuit.Clubs, CardValue.Queen),
        };
        
        var royalFlush = new RoyalFlush();
        Assert.False(royalFlush.Check(cards));
    }
}
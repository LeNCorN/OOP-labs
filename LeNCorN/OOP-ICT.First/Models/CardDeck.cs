using OOP_ICT.Exceptions;

namespace OOP_ICT.Models;

public class CardDeck
{
    public List<Card> Cards; 
    public CardDeck()
    {
        var cardSuits = Enum.GetValues<CardSuit>();
        var cardValues = Enum.GetValues<CardValue>();
        var cards = (from value in cardValues.Reverse() from suit in cardSuits select new Card(suit, value)).ToList();
        Cards = cards;
    }
}
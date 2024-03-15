using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth.Models;

public class PokerPlayer
{
    public Player Player { get; }
    private List<Card> Cards = new ();
    public decimal CurrentBet { get; private set; } = 0;

    public PokerPlayer(Player player)
    {
        Player = player;
    }

    public void IncreaseCurrentBet(decimal betIncrease)
    {
        if (betIncrease < 0)
        {
            throw CardPlayerException.NegativeValue("Bet cannot be negative value!");
        }

        CurrentBet += betIncrease;
    }
    
    public void SetInitialCards(List<Card> cards)
    {
        if (cards.Count != Constants.InitialCardCountForPlayer)
        {
            throw CardPlayerException.InvalidInitialCardCount($"Initial card count must be {Constants.InitialCardCountForPlayer}!");
        }

        Cards = cards;
    }

    public void RemoveCard(Card card)
    {
        var playerCard = Cards.Find(
            playerCard => (playerCard.Suit == card.Suit) 
                          && (playerCard.Value == card.Value));
        
        if (playerCard is not null)
        {
            Cards.Remove(card);
        }
    }

    public IReadOnlyList<Card> GetCards() => Cards.AsReadOnly();
    
    public void AddCard(Card card) => Cards.Add(card);
}
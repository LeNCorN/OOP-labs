using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Third.Models;

public class BlackjackPlayer
{
    public Player Player { get; }
    private List<Card> Cards = new ();
    public decimal CurrentBet { get; private set; } = 0;

    public BlackjackPlayer(Player player)
    {
        Player = player;
    }
    
    public void SetInitialCards(List<Card> cards)
    {
        if (cards.Count != Constants.InitialCardCountForPlayer)
        {
            throw CardPlayerException.InvalidInitialCardCount("Initial card count must be 2!");
        }

        Cards = cards;
    }

    public void IncreaseCurrentBet(decimal betIncrease)
    {
        if (betIncrease < 0)
        {
            throw CardPlayerException.NegativeValue("Bet cannot be negative value!");
        }

        CurrentBet += betIncrease;
    }
    
    public IReadOnlyList<Card> GetCards() => Cards.AsReadOnly();
    public void AddCard(Card card) => Cards.Add(card);
}
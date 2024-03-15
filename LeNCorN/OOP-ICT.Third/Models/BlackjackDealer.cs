using OOP_ICT.Models;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Third.Models;

public class BlackjackDealer
{
    public Dealer Dealer { get; }
    protected List<Card> Cards = new ();

    public BlackjackDealer(Dealer dealer)
    {
        Dealer = dealer;
    }

    public void SetInitialCards(List<Card> cards)
    {
        if (cards.Count != Constants.InitialCardCountForDealer)
        {
            throw CardPlayerException.InvalidInitialCardCount("Initial card count must be 1!");
        }
        
        Cards = cards;
    }
    
    public IReadOnlyList<Card> GetCards() => Cards.AsReadOnly();
    
    public void AddCard(Card card) => Cards.Add(card);
}
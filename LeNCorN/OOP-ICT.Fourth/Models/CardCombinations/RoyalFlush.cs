using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class RoyalFlush : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.RoyalFlush;
    private readonly StraightFlush _straightFlush = new();
    
    private readonly HashSet<CardValue> _allowedCardValues = new HashSet<CardValue>
    {
        CardValue.Ten,
        CardValue.Jack,
        CardValue.Queen,
        CardValue.King,
        CardValue.Ace,
    };
    
    public bool Check(IEnumerable<Card> cards)
    {
        return cards.All(card => _allowedCardValues.Contains(card.Value))
               && _straightFlush.Check(cards);
    }
}
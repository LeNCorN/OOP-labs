using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class Flush : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.Flush;
    
    public bool Check(IEnumerable<Card> cards)
    {
        var cardSuitList = cards.Select(card => card.Suit).ToList();
        var cardSuitSet = cardSuitList.ToHashSet();
        return cardSuitSet.Count == 1;
    }
}
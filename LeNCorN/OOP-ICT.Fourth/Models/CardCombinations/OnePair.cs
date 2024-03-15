using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class OnePair : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.OnePair;
    
    public bool Check(IEnumerable<Card> cards)
    {
        var cardValueList = cards.Select(card => card.Value).ToList();
        var cardValueSet = cardValueList.ToHashSet();
        return cardValueList.Count != cardValueSet.Count;
    }
}
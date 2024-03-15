using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class ThreeOfKind : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.ThreeOfKind;
    
    public bool Check(IEnumerable<Card> cards)
    {
        var cardCountDict = new Dictionary<CardValue, ushort>();
        foreach (var card in cards)
        {
            if (cardCountDict.ContainsKey(card.Value))
            {
                cardCountDict[card.Value] += 1;
                continue;
            }
            
            cardCountDict.Add(card.Value, 1);
        }

        return cardCountDict.Values.Any(cardCount => cardCount >= 3);
    }
}
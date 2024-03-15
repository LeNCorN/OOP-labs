using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class FullHouse : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.FullHouse;
    
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

        var cardCountList = cardCountDict.Values.ToList();
        return cardCountList.Count == 2 && (
            (cardCountList[0] == 3 && cardCountList[1] == 2) ||
            (cardCountList[0] == 2 && cardCountList[1] == 3)
        );
    }
}
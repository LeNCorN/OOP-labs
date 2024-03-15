using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class Straight : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.Straight;
    
    public bool Check(IEnumerable<Card> cards)
    {
        var cardValueList = System.Enum.GetValues(typeof(CardValue)).Cast<CardValue>().ToList();
        var inputCardValueList = cards.Select(card => card.Value).OrderBy(value => value).ToList();

        var i = 0;
        var j = 0;
        
        while (i < cardValueList.Count && cardValueList[i] != inputCardValueList[j])
        {
            i += 1;
        }

        while (i < cardValueList.Count && j < inputCardValueList.Count)
        {
            if (cardValueList[i] != inputCardValueList[j])
            {
                return false;
            }

            i += 1;
            j += 1;
        }

        return true;
    }
}
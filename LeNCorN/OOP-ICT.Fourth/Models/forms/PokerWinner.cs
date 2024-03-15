using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Dto;

public class PokerWinner
{
    public Guid Uuid { get; }
    public IReadOnlyList<Card> Cards;
    public CombinationName CardCombination { get; }
    
    public PokerWinner(Guid uuid, IReadOnlyList<Card> cards, CombinationName cardCombination)
    {
        Uuid = uuid;
        Cards = cards;
        CardCombination = cardCombination;
    }
}
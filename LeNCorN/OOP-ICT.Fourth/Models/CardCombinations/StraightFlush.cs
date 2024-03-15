using OOP_ICT.Fourth.Abstractions;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Models.CardCombinations;

public class StraightFlush : ICardCombination
{
    public CombinationName Name { get; } = CombinationName.StraightFlush;
    private readonly Straight _straight = new();
    private readonly Flush _flush = new();

    public bool Check(IEnumerable<Card> cards)
    {
        return _straight.Check(cards) && _flush.Check(cards);
    }
}
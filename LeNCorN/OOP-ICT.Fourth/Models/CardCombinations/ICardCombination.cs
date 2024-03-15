using OOP_ICT.Fourth.Enum;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.Abstractions;

public interface ICardCombination
{
    CombinationName Name { get; }
    bool Check(IEnumerable<Card> cards);
}
using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Third.Enum;

namespace OOP_ICT.Third.Dto;

public class PlayerGameResult
{
    public Player Player { get; }
    public IReadOnlyList<Card> Cards { get; }
    public GameResultStatus ResultStatus { get; set; }

    public PlayerGameResult(Player player, IReadOnlyList<Card> cards)
    {
        Player = player;
        Cards = cards;
    }
}
using OOP_ICT.Models;

namespace OOP_ICT.Third;

public class Constants
{
    public const ushort InitialCardCountForPlayer = 2;
    public const ushort InitialCardCountForDealer = 1;
    public const ushort MaxAllowedCardSum = 21;
    public const ushort MinAllowedCardSumForDealer = 17;
    public const decimal BlackjackWinningRatio = (decimal)1.5;
    public const ushort AcePointWhenMaxAllowedCardSum = 1;
    public const uint CardCountWhenBlackjack = 2;
    public const uint CardValueWhenBlackjack = 10;
    public static readonly Dictionary<CardValue, int> BlackjackCardPoints = new Dictionary<CardValue, int>()
    {
        { CardValue.Ace , 11},
        { CardValue.King, 10},
        { CardValue.Queen, 10},
        { CardValue.Jack, 10},
        { CardValue.Ten, 10},
        { CardValue.Nine, 9},
        { CardValue.Eight, 8},
        { CardValue.Seven, 7},
        { CardValue.Six, 6},
        { CardValue.Five, 5},
        { CardValue.Four, 4},
        { CardValue.Three, 3},
        { CardValue.Two, 2}
    };
}
namespace OOP_ICT.Fourth.Dto;

public class PokerOptions
{
    public decimal MinStartBet;
    public ushort CirclesCount;

    public PokerOptions(decimal minStartBet, ushort circlesCount = 1)
    {
        MinStartBet = minStartBet;
        CirclesCount = circlesCount;
    }
}
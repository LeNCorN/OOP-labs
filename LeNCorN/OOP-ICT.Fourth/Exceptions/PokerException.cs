namespace OOP_ICT.Fourth.Exceptions;

public class PokerException : Exception
{
    private PokerException(string message) : base(message) {}
    
    public static PokerException LastCircleIsOver(string message)
    {
        return new PokerException(message);
    }

    public static PokerException InsufficientStartBet(string message)
    {
        return new PokerException(message);
    }
}
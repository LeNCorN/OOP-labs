namespace OOP_ICT.Third.Exceptions;

public class CardGameException : Exception
{
    private CardGameException(string message) : base(message) {}
    
    public static CardGameException NullReference(string message)
    {
        return new CardGameException(message);
    }
    
    public static CardGameException NotEnoughPlayersForStart(string message)
    {
        return new CardGameException(message);
    }

    public static CardGameException GameIsActive(string message)
    {
        return new CardGameException(message);
    }

    public static CardGameException PlayerIsLostGame(string message)
    {
        return new CardGameException(message);
    }
    
    public static CardGameException PlayerDoesNotExists(string message)
    {
        return new CardGameException(message);
    }

    public static CardGameException BalanceIsNotSufficientForBet(string message)
    {
        return new CardGameException(message);
    }
}
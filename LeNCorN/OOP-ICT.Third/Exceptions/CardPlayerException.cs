namespace OOP_ICT.Third.Exceptions;

public class CardPlayerException : Exception
{
    private CardPlayerException(string message) : base(message) {}
    
    public static CardPlayerException NullReference(string message)
    {
        return new CardPlayerException(message);
    }
    
    public static CardPlayerException InvalidInitialCardCount(string message)
    {
        return new CardPlayerException(message);
    }

    public static CardPlayerException NegativeValue(string message)
    {
        return new CardPlayerException(message);
    }
}
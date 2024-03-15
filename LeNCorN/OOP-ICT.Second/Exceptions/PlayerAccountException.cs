namespace OOP_ICT.Second.Exceptions;

public class PlayerAccountException : Exception
{
    private PlayerAccountException(string message) : base(message) {}

    public static PlayerAccountException NegativeValue(string message)
    {
        return new PlayerAccountException(message);
    }

    public static PlayerAccountException InsufficientBalance(string message)
    {
        return new PlayerAccountException(message);
    }

    public static PlayerAccountException InvalidAccountName(string message)
    {
        return new PlayerAccountException(message);
    }
}
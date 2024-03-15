namespace OOP_ICT.Second.Exceptions;

public class MoneyBankException : Exception
{
    private MoneyBankException(string message) : base(message) {}

    public static MoneyBankException NullReference(string message)
    {
        return new MoneyBankException(message);
    }
}
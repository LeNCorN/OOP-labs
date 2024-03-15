namespace OOP_ICT.Second.Exceptions;

public class CasinoBankException : Exception
{
    private CasinoBankException(string message) : base(message) {}

    public static CasinoBankException NullReference(string message)
    {
        return new CasinoBankException(message);
    }
}
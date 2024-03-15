namespace OOP_ICT.Second.Abstractions;

public class PlayerAccountRepositoryException : Exception
{
    private PlayerAccountRepositoryException(string message): base(message) {}

    public static PlayerAccountRepositoryException PlayerAccountDoesNotExists(string message)
    {
        return new PlayerAccountRepositoryException(message);
    }
    
    public static PlayerAccountRepositoryException PlayerAccountAlreadyExists(string message)
    {
        return new PlayerAccountRepositoryException(message);
    }

    public static PlayerAccountRepositoryException NullReference(string message)
    {
        return new PlayerAccountRepositoryException(message);
    }
    
    
}
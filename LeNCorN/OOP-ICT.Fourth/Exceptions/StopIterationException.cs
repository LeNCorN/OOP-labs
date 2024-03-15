namespace OOP_ICT.Fourth.Exceptions;

public class StopIterationException : Exception
{
    protected StopIterationException(string message) : base(message) {}

    public static StopIterationException LastIteration(string message)
    {
        return new StopIterationException(message);
    }
}
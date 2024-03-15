using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.Models;

public class BlackjackPlayer
{
    public Player Player { get; }
    public decimal CurrentBet { get; private set; } = 0;

    public BlackjackPlayer(Player player)
    {
        Player = player;
    }
    
    public void IncreaseCurrentBet(decimal betIncrease)
    {
        if (betIncrease < 0)
        {
            throw PlayerException.NegativeValue("Bet cannot be negative value!");
        }

        CurrentBet += betIncrease;
    }
}
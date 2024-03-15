using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;

namespace OOP_ICT.Third.Models;

public class BlackjackCreator
{
    private BlackjackDealer _dealer;
    private readonly Dictionary<Guid, BlackjackPlayer> _players = new Dictionary<Guid, BlackjackPlayer>();
    
    public BlackjackCreator SetDealer() 
    {
        var dealer = new Dealer(new CardDeck());
        var BlackjackDealer = new BlackjackDealer(dealer);

        _dealer = BlackjackDealer;
        return this;
    }

    public BlackjackCreator AddPlayerInGame(Player player)
    {
        var BlackjackPlayer = new BlackjackPlayer(player);
        _players.Add(player.Uuid, BlackjackPlayer);
        return this;
    }

    public Blackjack CreateGame(ICasinoBank casinoBank)
    {
        var Blackjack = new Blackjack(casinoBank, _players, _dealer);
        return Blackjack;
    }
}
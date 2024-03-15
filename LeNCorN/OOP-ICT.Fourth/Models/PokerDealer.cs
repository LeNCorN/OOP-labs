using OOP_ICT.Models;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth.Models;

public class PokerDealer
{
    public Dealer Dealer { get; }
    private List<Card> Cards = new ();
    private PokerPlayer _playerInstance;
    public PokerPlayer PlayerInstance
    {
        get => _playerInstance;
        set => _playerInstance = value;
    }

    public PokerDealer(Dealer dealer)
    {
        Dealer = dealer;
    }

    public void SetInitialCards(List<Card> cards)
    {
        if (_playerInstance is null)
        {
            throw CardPlayerException.NullReference("Player instance has not been set!");
        }

        _playerInstance.SetInitialCards(cards);
    }

    public IReadOnlyList<Card> GetCards => Cards.AsReadOnly();
}
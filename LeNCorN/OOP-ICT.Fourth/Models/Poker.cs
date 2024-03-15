using OOP_ICT.Fourth.Dto;
using OOP_ICT.Fourth.Enum;
using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth.Models;

public class Poker
{
    private decimal _totalBet = 0;
    private ushort _currentCircle = 1;
    private readonly ChainOfPokerCardCombinations _chainOfPokerCardCombinations = new();
    
    private readonly ushort _circlesCount;
    private readonly decimal _minStartBet;
    
    private readonly ICasinoBank CasinoBank;
    private readonly Dictionary<Guid, PokerPlayer> Players;
    private readonly PokerDealer Dealer;
    
    public bool IsGameActive { get; protected set; }
    
    public Poker(
        ICasinoBank casinoBank, 
        Dictionary<Guid, PokerPlayer> players, 
        PokerDealer dealer, 
        PokerOptions options)
    {
        _minStartBet = options.MinStartBet;
        _circlesCount = options.CirclesCount;
        CasinoBank = casinoBank ?? throw CardGameException.NullReference("Casino bank cannot be null!");
        Players = players ?? throw CardGameException.NullReference("Players dictionary cannot be null!");
        Dealer = dealer ?? throw CardGameException.NullReference("Dealer cannot be null!");
    }

    public void StartGame()
    {
        if (Players.Count < Constants.MinPlayerCountForStart)
        {
            throw CardGameException.NotEnoughPlayersForStart(
                $" poker must have {Constants.MinPlayerCountForStart} and more players for start!");
        }
        
        CheckPlayersStartBetsOrThrowException();
        ChooseDealerFromPlayers();
        SetTotalBet();
        
        IsGameActive = true;
    }

    public void FinishGame()
    {
        if (_currentCircle != _circlesCount)
        {
            throw CardGameException.GameIsActive("Last circle is not finish!");
        }
        
        IsGameActive = false;
    }

    public void HandOutCards()
    {
        if (IsGameActive)
        {
            throw CardGameException.GameIsActive("It is not possible to deal cards after the start of the game!");
        }
        
        Dealer.Dealer.ShuffleTheDeck();
        foreach (var player in Players.Values)
        {
            var playerCards = new List<Card>();
            for (var index = 0; index < Constants.InitialCardCountForPlayer; index++)
            {
                var card = Dealer.Dealer.DealCard();
                playerCards.Add(card);
            }
            
            player.SetInitialCards(playerCards);
        }
    }

    public void RemovePlayerFromGame(Guid playerUuid) => Players.Remove(playerUuid);

    public void DealAdditionalCardForPlayer(Guid playerUuid)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        var newCard = Dealer.Dealer.DealCard();
        playerInGame.AddCard(newCard);
    }

    public void IncreasePlayerBet(Guid playerUuid, decimal betIncrease)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        var isPlayerBalanceSufficientForBet = CasinoBank.CheckIsPlayerCasinoBalanceSufficient(
            playerUuid: playerUuid,
            chipsCount: playerInGame.CurrentBet + betIncrease);
        
        if (!isPlayerBalanceSufficientForBet)
        {
            throw CardGameException.BalanceIsNotSufficientForBet("Balance is not sufficient for bet!");
        }
        
        playerInGame.IncreaseCurrentBet(betIncrease);
        _totalBet += betIncrease;
    }

    public decimal AddWinningAmount(Guid playerUuid)
    {
        CasinoBank.AddChipsToPlayerCasinoBalance(playerUuid, _totalBet);
        return _totalBet;
    }

    public decimal SubtractLossAmount(Guid playerUuid)
    {
        var playerBet = GetPlayerBet(playerUuid);
        CasinoBank.SubtractChipsFromPlayerCasinoBalance(playerUuid, playerBet);
        return playerBet;
    }

    public IReadOnlyList<Card> GetDealerCards() => Dealer.PlayerInstance.GetCards();

    public IReadOnlyList<Card> GetPlayerCards(Guid playerUuid)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        return playerInGame.GetCards();
    }

    public decimal GetPlayerBet(Guid playerUuid)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        return playerInGame.CurrentBet;
    }
    
    public void MoveNextCircle()
    {
        if (_circlesCount == _currentCircle)
        {
            throw StopIterationException.LastIteration("Last circle is over!");
        }
        
        ChooseDealerFromPlayers();
        _currentCircle += 1;
    }

    public uint GetCirclesCount() => _circlesCount;

    public void ReplaceCardsForPLayer(Guid playerUuid, List<Card> cards)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        foreach (var card in cards)
        {
            playerInGame.RemoveCard(card);
            DealAdditionalCardForPlayer(playerUuid);
        }
    }

    public CombinationName GetStrongestCardCombinationForPlayer(Guid playerUuid)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        var playerCards = playerInGame.GetCards();
        var strongestCombination = _chainOfPokerCardCombinations.GetStrongestCombination(playerCards);
        return strongestCombination;
    }

    public PokerWinner GetWinner()
    {
        if (IsGameActive)
        {
            throw CardGameException.GameIsActive("The game is not finish yet!");
        }

        var firstPlayer = Players.Values.First();
        var firstPlayerCombination = GetStrongestCardCombinationForPlayer(firstPlayer.Player.Uuid);
        var winner = new PokerWinner(
            uuid: firstPlayer.Player.Uuid,
            cardCombination: firstPlayerCombination,
            cards: firstPlayer.GetCards()
        );

        return Players.Values.Aggregate(winner, (current, player) => ChooseWinner(player, current));
    }
    
    private PokerPlayer FindPLayerInGame(Guid playerUuid)
    {
        if (!Players.ContainsKey(playerUuid))
        {
            throw CardGameException.PlayerDoesNotExists("Player in game does not exists!");
        }

        return Players[playerUuid];
    }

    private void ChooseDealerFromPlayers()
    {
        var playerList = Players.Values.ToList();
        if (_currentCircle - 1 < playerList.Count)
        {
            Dealer.PlayerInstance = playerList[_currentCircle - 1];
            return;
        }
        
        var nextDealerIndex = (playerList.Count) % (_currentCircle - 1);
        var nextDealer = playerList[nextDealerIndex];
        Dealer.PlayerInstance = nextDealer;
    }

    private void CheckPlayersStartBetsOrThrowException()
    {
        if (Players.Values.Any(player => player.CurrentBet < _minStartBet))
        {
            throw PokerException.InsufficientStartBet($"Min bet for start game is {_minStartBet}");
        }
    }

    private void SetTotalBet()
    {
        foreach (var player in Players.Values)
        {
            _totalBet += player.CurrentBet;
        }
    }

    private Card GetStrongestCard(IReadOnlyList<Card> cards)
    {
        var strongestCard = cards[0];
        foreach (var card in cards)
        {
            if (card.Value > strongestCard.Value) strongestCard = card;
        }

        return strongestCard;
    }

    private PokerWinner ChooseWinner(PokerPlayer player, PokerWinner potentialWinner)
    {
        var strongestCombination = GetStrongestCardCombinationForPlayer(player.Player.Uuid);
        if (strongestCombination > potentialWinner.CardCombination)
        {
            return new PokerWinner(
                uuid: player.Player.Uuid,
                cardCombination: strongestCombination,
                cards: player.GetCards()
            );
        }

        if (strongestCombination != potentialWinner.CardCombination) return potentialWinner;
        var winnerStrongestCard = GetStrongestCard(potentialWinner.Cards);
        var playerStrongestCard = GetStrongestCard(player.GetCards());
        if (playerStrongestCard.Value > winnerStrongestCard.Value)
        {
            return new PokerWinner(
                uuid: player.Player.Uuid,
                cardCombination: strongestCombination,
                cards: player.GetCards()
            );
        }

        return potentialWinner;
    }
}
using OOP_ICT.Models;
using OOP_ICT.Second.Abstractions;
using OOP_ICT.Third.Dto;
using OOP_ICT.Third.Enum;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Third.Models;

public class Blackjack
{
    private readonly ICasinoBank CasinoBank;
    private readonly Dictionary<Guid, BlackjackPlayer> Players;
    private readonly BlackjackDealer Dealer;
    
    public bool IsGameActive { get; private set; }

    public Blackjack(
        ICasinoBank casinoBank,
        Dictionary<Guid, BlackjackPlayer> players,
        BlackjackDealer dealer
    )
    {
        CasinoBank = casinoBank;
        Players = players;
        Dealer = dealer;
        IsGameActive = false;
    }
    
    public void InitGame()
    {
        IsGameActive = true;
    }

    public void EndGame()
    {
        PlayDealer();
        IsGameActive = false;
    }
    
    public void HandOutCards()
    {
        
        Dealer.Dealer.ShuffleTheDeck();
        foreach (var player in Players.Values)
        {
            var playerInitialCards = new List<Card>();
            for (var i = 0; i < Constants.InitialCardCountForPlayer; i++)
            {
                var card = Dealer.Dealer.DealCard();
                playerInitialCards.Add(card);
            }
            
            player.SetInitialCards(playerInitialCards);
        }

        var dealerInitialCards = new List<Card> { Dealer.Dealer.DealCard() };
        Dealer.SetInitialCards(dealerInitialCards);
    }

    public void RemovePlayerFromGame(Guid playerUuid) => Players.Remove(playerUuid);

    public void DealAdditionalCardForPlayer(Guid playerUuid)
    {
        var playerCardsSum = CalculateCardsSum(GetPlayerCards(playerUuid));
        Console.WriteLine(playerCardsSum);
        if (playerCardsSum > Constants.MaxAllowedCardSum)
        {
            throw CardGameException.PlayerIsLostGame(
                $"Player is lost game! The number of points is more than {Constants.MaxAllowedCardSum}!");
        }
        
        var playerInGame = FindPLayerInGame(playerUuid);
        var card = Dealer.Dealer.DealCard();
        playerInGame.AddCard(card);
    }

    public void IncreasePlayerBet(Guid playerUuid, decimal betIncrease)
    {
        if (IsGameActive)
        {
            throw CardGameException.GameIsActive("You cannot increase the bet after the start of the game!");
        }
        
        var playerInGame = FindPLayerInGame(playerUuid);
        var isPlayerBalanceSufficientForBet = CasinoBank.CheckIsPlayerCasinoBalanceSufficient(
            playerUuid: playerUuid,
            chipsCount: playerInGame.CurrentBet + betIncrease);
        
        if (!isPlayerBalanceSufficientForBet)
        {
            throw CardGameException.BalanceIsNotSufficientForBet("Balance is not sufficient for bet!");
        }
        
        playerInGame.IncreaseCurrentBet(betIncrease);
    }

    public decimal AddWinningAmount(Guid playerUuid)
    {
        var playerBet = GetPlayerBet(playerUuid);
        CasinoBank.AddChipsToPlayerCasinoBalance(playerUuid, playerBet);
        return playerBet;
    }

    public decimal SubtractLossAmount(Guid playerUuid)
    {
        var playerBet = GetPlayerBet(playerUuid);
        CasinoBank.SubtractChipsFromPlayerCasinoBalance(playerUuid, playerBet);
        return playerBet;
    }

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
    
    public decimal AddBlackjackAmount(Guid playerUuid) 
    { 
        var playerBet = GetPlayerBet(playerUuid);
        var blackjackAmount = playerBet * Constants.BlackjackWinningRatio;
        CasinoBank.AddChipsToPlayerCasinoBalance(playerUuid, blackjackAmount);

        return blackjackAmount;
    }
    
    public bool CheckIsPlayerHasBlackjack(Guid playerUuid)
    {
        var playerInGame = FindPLayerInGame(playerUuid);
        var playerCards = playerInGame.GetCards();

        return (playerCards.Count == Constants.CardCountWhenBlackjack)
               && (
                   (playerCards[0].Value is CardValue.Ace &&
                    Constants.BlackjackCardPoints[playerCards[1].Value] == Constants.CardValueWhenBlackjack)
                   ||
                   (playerCards[1].Value is CardValue.Ace &&
                    Constants.BlackjackCardPoints[playerCards[0].Value] == Constants.CardValueWhenBlackjack)
               );
    }
    
    public int CalculateCardsSum(IReadOnlyList<Card> cards) 
    { 
        var cardsSum = cards
            .Where(card => card.Value is not CardValue.Ace)
            .Sum(card => Constants.BlackjackCardPoints[card.Value]);
        
        foreach (var card in cards) 
        { 
            if (card.Value is not CardValue.Ace) continue; 
            var acePoints = Constants.BlackjackCardPoints[card.Value];
            if (cardsSum + acePoints > Constants.MaxAllowedCardSum)
            {
                cardsSum += Constants.AcePointWhenMaxAllowedCardSum;
            }
            else
            {
                cardsSum += acePoints;
            }
        }

        return cardsSum;
    }
    
    public PlayerGameResult CalculateGameResultForPlayer(Guid playerUuid)
    {
        if (IsGameActive)
        {
            throw CardGameException.GameIsActive("The game is not finish yet!");
        }

        var playerInGame = FindPLayerInGame(playerUuid);
        var playerGameResult = new PlayerGameResult(
            player: playerInGame.Player,
            cards: playerInGame.GetCards());
        
        var dealerCardsSum = CalculateCardsSum(Dealer.GetCards());
        var playerCardsSum = CalculateCardsSum(GetPlayerCards(playerInGame.Player.Uuid));
        if (playerCardsSum > Constants.MaxAllowedCardSum || playerCardsSum < dealerCardsSum)
        {
            playerGameResult.ResultStatus = GameResultStatus.Defeat;
            return playerGameResult;
        }
        
        if (playerCardsSum > dealerCardsSum)
        {
            playerGameResult.ResultStatus = GameResultStatus.Victory;
            return playerGameResult;
        }

        playerGameResult.ResultStatus = GameResultStatus.Draw;
        return playerGameResult;
    }
    
    private void PlayDealer()
    {
        var dealerCards = Dealer.GetCards();
        var dealerCardsSum = CalculateCardsSum(dealerCards); 
        while (dealerCardsSum < Constants.MaxAllowedCardSum)
        {
            DealAdditionalCardForDealer();
            dealerCardsSum = CalculateCardsSum(dealerCards);
            if (dealerCardsSum > Constants.MinAllowedCardSumForDealer)
            {
                break;
            }
        }
    }
    
    private void DealAdditionalCardForDealer()
    {
        var card = Dealer.Dealer.DealCard();
        Dealer.AddCard(card);
    }
    
    private BlackjackPlayer FindPLayerInGame(Guid playerUuid)
    {
        if (!Players.ContainsKey(playerUuid))
        {
            throw CardGameException.PlayerDoesNotExists("Player in game does not exists!");
        }

        return Players[playerUuid];
    }
}
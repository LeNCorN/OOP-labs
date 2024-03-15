using OOP_ICT.Second.Abstractions;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second.Models;

public class BlackjackCasino
{
    private readonly ICasinoBank _bank;
    private readonly Dictionary<Guid, BlackjackPlayer> _blackjackPlayers = new ();
    
    public BlackjackCasino(ICasinoBank bank)
    {
        _bank = bank;
    }

    public void AddWinningAmount(Guid playerUuid)
    {
        var blackjackPlayer = FindPLayerInGame(playerUuid);
        _bank.AddChipsToPlayerCasinoBalance(blackjackPlayer.Player.Uuid, blackjackPlayer.CurrentBet);
    }

    public void SubtractLossAmount(Guid playerUuid)
    {
        var blackjackPlayer = FindPLayerInGame(playerUuid);
        _bank.SubtractChipsFromPlayerCasinoBalance(blackjackPlayer.Player.Uuid, blackjackPlayer.CurrentBet);
    }

    public void AddBlackjackAmount(Guid playerUuid)
    {
        var blackjackPlayer = FindPLayerInGame(playerUuid);
        _bank.AddChipsToPlayerCasinoBalance(blackjackPlayer.Player.Uuid,
            blackjackPlayer.CurrentBet * Constants.BlackjackWinningRatio);
    }
    
    public void AddPlayerInGame(Player player)
    {
        if (player is null)
        {
            throw new BlackjackCasinoException("Player cannot be null!");
        }
        
        if (_blackjackPlayers.ContainsKey(player.Uuid))
        {
            throw new BlackjackCasinoException("Player already in game!");
        }
        
        var newBlackjackPlayer = new BlackjackPlayer(player);
        _blackjackPlayers.Add(player.Uuid, newBlackjackPlayer);
    }

    public void IncreasePlayerBet(Guid playerUuid, int betIncrease)
    {
        var blackjackPlayer = FindPLayerInGame(playerUuid);
        var isBalanceSufficient = _bank.CheckIsPlayerCasinoBalanceSufficient(playerUuid, betIncrease);
        if (!isBalanceSufficient)
        {
            throw new BlackjackCasinoException("Insufficient balance for bet!");
        }

        _bank.SubtractChipsFromPlayerCasinoBalance(playerUuid, betIncrease);
        blackjackPlayer.IncreaseCurrentBet(betIncrease);
    }

    private BlackjackPlayer FindPLayerInGame(Guid playerUuid)
    {
        if (!_blackjackPlayers.ContainsKey(playerUuid))
        {
            throw new BlackjackCasinoException("Player in game does not exists!");
        }

        return _blackjackPlayers[playerUuid];
    }
}
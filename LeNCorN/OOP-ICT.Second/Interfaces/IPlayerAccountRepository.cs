using OOP_ICT.Second.Models;

namespace OOP_ICT.Second.Abstractions;

public interface IPlayerAccountRepository
{
    PlayerAccount FindPlayerAccountByPlayerUuid(Guid playerUuid);
    Guid SavePlayerAccount(Guid playerUuid, PlayerAccount playerAccount);
}